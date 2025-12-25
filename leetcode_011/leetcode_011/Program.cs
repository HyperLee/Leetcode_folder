namespace leetcode_011;

class Program
{
    /// <summary>
    /// 11. Container With Most Water
    /// https://leetcode.com/problems/container-with-most-water/description/?envType=study-plan-v2&envId=leetcode-75
    /// 11. 盛最多水的容器
    /// https://leetcode.cn/problems/container-with-most-water/description/
    /// 
    /// 題目（繁體中文）：
    /// 給定一個長度為 n 的整數陣列 `height`。畫出 n 條垂直線，第 i 條線的兩個端點為 (i, 0) 與 (i, height[i])。
    /// 找出兩條線，與 x 軸一起構成一個容器，使該容器能容納最多的水。
    /// 回傳該容器能儲存的最大水量。
    /// 注意：容器不能傾斜。
    /// 
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();
        
        // 測試案例 1: 預期輸出 49
        int[] height1 = { 1, 8, 6, 2, 5, 4, 8, 3, 7 };
        int result1 = solution.MaxArea(height1);
        Console.WriteLine($"測試案例 1: height = [{string.Join(", ", height1)}]");
        Console.WriteLine($"最大面積 = {result1}");
        Console.WriteLine();

        // 測試案例 2: 預期輸出 1
        int[] height2 = { 1, 1 };
        int result2 = solution.MaxArea(height2);
        Console.WriteLine($"測試案例 2: height = [{string.Join(", ", height2)}]");
        Console.WriteLine($"最大面積 = {result2}");
        Console.WriteLine();

        // 測試案例 3: 預期輸出 16
        int[] height3 = { 4, 3, 2, 1, 4 };
        int result3 = solution.MaxArea(height3);
        Console.WriteLine($"測試案例 3: height = [{string.Join(", ", height3)}]");
        Console.WriteLine($"最大面積 = {result3}");
        Console.WriteLine();

        // 測試案例 4: 預期輸出 2
        int[] height4 = { 1, 2, 1 };
        int result4 = solution.MaxArea(height4);
        Console.WriteLine($"測試案例 4: height = [{string.Join(", ", height4)}]");
        Console.WriteLine($"最大面積 = {result4}");
    }

    /// <summary>
    /// 使用雙指針法求解容器最大盛水量
    /// 
    /// 解題思路：
    /// 1. 核心概念：使用雙指針（左右邊界）從兩端向中間逼近
    /// 2. 面積公式：S(i,j) = min(h[i], h[j]) × (j − i)
    ///    - 高度：取兩邊較短的柱子 Math.Min(height[left], height[right])
    ///    - 寬度：兩指針的距離 (right - left)
    /// 3. 為什麼取 Min？
    ///    - 容器的水位由較短的柱子決定（木桶原理）
    ///    - 若取較長的柱子，水會從短的那邊溢出
    /// 4. 指針移動策略：
    ///    - 移動較短的柱子對應的指針
    ///    - 原因：短柱子已經限制了當前的最大面積，移動它才有機會找到更大的面積
    ///    - 若移動高柱子，但是面積公式的高還是不變，會造成面積不一定會變大
    ///    - 所以移動短柱子邊界會比較好
    /// 5. 時間複雜度：O(n)，每個元素最多訪問一次
    /// 6. 空間複雜度：O(1)，只使用常數額外空間
    /// 
    /// 參考資料：
    /// - https://ithelp.ithome.com.tw/articles/10228493
    /// - https://leetcode.cn/problems/container-with-most-water/solution/container-with-most-water-shuang-zhi-zhen-fa-yi-do/
    /// - https://leetcode.cn/problems/container-with-most-water/solution/sheng-zui-duo-shui-de-rong-qi-by-leetcode-solution/
    /// </summary>
    /// <param name="height">整數陣列，代表每個位置的柱子高度</param>
    /// <returns>容器能容納的最大水量</returns>
    public int MaxArea(int[] height)
    {
        // 步驟 1: 初始化變數
        int length = height.Length; // 陣列長度
        int left = 0;                // 左指針，從最左邊開始
        int right = length - 1;      // 右指針，從最右邊開始
        int max = 0;                 // 記錄找到的最大面積

        // 步驟 2: 雙指針向中間移動，直到兩指針相遇
        while (left < right)
        {
            // 步驟 3: 計算當前左右指針形成的容器面積
            // 面積 = 高度（取兩邊較矮的） × 寬度（兩指針的距離）
            // 例如：height[left]=8, height[right]=7, distance=8
            // area = min(8,7) × 8 = 7 × 8 = 56
            int area = Math.Min(height[left], height[right]) * (right - left);
            
            // 步驟 4: 更新最大面積
            // 如果當前面積大於已記錄的最大面積，則更新
            max = Math.Max(max, area);

            // 濃縮寫法（將步驟 3 和 4 合併）：
            // max = Math.Max(max, Math.Min(height[left], height[right]) * (right - left));

            // 步驟 5: 移動指針
            // 關鍵策略：移動較短的那一邊
            // 原因：短邊限制了容器高度，移動長邊只會讓寬度變小，面積不可能增大
            //       移動短邊才有機會遇到更高的柱子，從而增大面積
            if (height[left] < height[right])
            {
                left++;  // 左邊較短，左指針右移
            }
            else
            {
                right--; // 右邊較短或等高，右指針左移
            }
        }
        
        // 步驟 6: 返回找到的最大面積
        return max;
    }
}
