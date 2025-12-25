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
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// https://ithelp.ithome.com.tw/articles/10228493
    /// https://leetcode.cn/problems/container-with-most-water/solution/container-with-most-water-shuang-zhi-zhen-fa-yi-do/
    /// https://leetcode.cn/problems/container-with-most-water/solution/sheng-zui-duo-shui-de-rong-qi-by-leetcode-solution/
    /// 
    /// 面積公式: S(i,j) = min(h[i], h[j]) × (j − i)
    /// 
    /// Math.Min(height[left], height[right]) => 面積公式的高度
    /// 找出兩邊較短的柱子，用較長的去算水會溢出來
    /// => 要取 Min, 如果是 Max 會造成水位溢出
    /// 因兩邊不等高. 水當然會溢出.
    /// 兩邊高度一致水位才不會溢出
    /// 
    /// (right - left) 是為了找出面積公式的長度
    /// 
    /// 左右邊界漸漸靠攏, 同時持續更新 面積 max 數值
    /// 
    /// 短柱子邊界移動是因為短柱子已經計算過面積
    /// ,移動他才能計算新的面積
    /// 反之如果移動高柱子, 但是面積公式的高還是不變, 會造成面積不一定會變大
    /// 所以移動短柱子邊界會比較好
    /// 
    /// 本題目除了要知道是雙指針以及面積公式
    /// 還要知道兩邊界移動時候,是高的移動還是短的移動
    /// </summary>
    /// <param name="height"></param>
    /// <returns></returns>
    public int MaxArea(int[] height)
    {
        // 輸入之 height 總數量(筆數)
        int length = height.Length;
        // 左邊界 index
        int left = 0;
        // 右邊界 index, index: 0開始
        int right = length - 1;
        // 最大面積
        int max = 0;

        // 遍歷 height
        while(left < right)
        {
            // 這兩行可縮成下面寫法.但是兩行比較好理解
            // 面積公式: Min(左右高度) * 長度(左右邊界距離)
            int area = Math.Min(height[left], height[right]) * (right - left);
            // 持續更新 最大面積
            max = Math.Max(max, area);

            //濃縮寫法
            //max = Math.Max(max, Math.Min(height[left], height[right]) * (right - left));

            // 短柱子移動
            if(height[left] < height[right])
            {
                left++;
            }
            else
            {
                right--;
            }
        }
        return max;
    }
}
