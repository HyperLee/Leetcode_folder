namespace leetcode_042;

class Program
{
    /// <summary>
    /// 42. Trapping Rain Water
    /// https://leetcode.com/problems/trapping-rain-water/description/
    /// 42. 接雨水
    /// https://leetcode.cn/problems/trapping-rain-water/description/
    /// 
    /// 題目描述:
    /// 給定一個非負整數數組 height，其中每個元素代表一個寬度為 1 的柱子的高度。
    /// 計算在這些柱子所形成的容器中，能夠接住多少雨水。
    /// </summary>
    static void Main(string[] args)
    {
        // 測試案例
        int[] test1 = new int[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 }; // 預期結果: 6
        int[] test2 = new int[] { 4, 2, 0, 3, 2, 5 };                   // 預期結果: 9

        // 執行測試並顯示結果
        Console.WriteLine($"Test Case 1: Input = [{string.Join(", ", test1)}]");
        Console.WriteLine($"Result = {Trap(test1)}");  // 應該輸出 6
        
        Console.WriteLine($"\nTest Case 2: Input = [{string.Join(", ", test2)}]");
        Console.WriteLine($"Result = {Trap(test2)}");  // 應該輸出 9
    }

    /// <summary>
    /// Traps rainwater based on the heights of the bars.
    /// 解題思路：
    /// 1. 使用雙指針方法 (Two Pointers)，從兩端向中間移動
    /// 2. 維護左右兩側的最大高度 (leftMax, rightMax)
    /// 3. 較低的一側可以確定積水量，因為水位由較低的一側決定
    /// 4. 時間複雜度 O(n)，空間複雜度 O(1)
    /// 
    /// 核心原理：
    /// 水能夠被接住的高度取決於較低的一側
    /// 如果左側較低，則可以確定左側位置的積水量
    /// 如果右側較低，則可以確定右側位置的積水量
    /// 
    /// 計算方式:
    /// 當 height[left] < height[right] 時，計算左側積水量
    /// 否則計算右側積水量
    /// 積水量 = 左側最大高度 - 當前高度
    /// 
    /// 注意停止條件 不能加上等於
    /// 1.避免重複計算：如果加上等於，會導致左右指針在同一位置時，重複計算積水量
    /// 2.算法的核心是比較左右兩側，選擇較低的一側計算積水量
    /// 當指針相遇時 (left == right)：左右兩側的最大高度相等，積水量為 0 ,且會多增加一次計算量.
    /// 
    /// <returns>The total amount of trapped rainwater.</returns>
    public static int Trap(int[] height)
    {
        // 儲存總積水量
        int res = 0;
        
        // 左右指針初始化在數組兩端
        int left = 0;
        int right = height.Length - 1;
        
        // 記錄左右兩側遇到的最大高度
        int leftMax = 0;
        int rightMax = 0;

        // 如果高度數組為空或長度為0，則無法存儲任何水
        if(height == null || height.Length == 0)
        {
            return 0;
        }

        while(left < right)
        {
            // 更新左右兩側的最大高度
            leftMax = Math.Max(leftMax, height[left]);
            rightMax = Math.Max(rightMax, height[right]);

            // 如果左側高度較小，可以確定左側的積水量
            // 積水量 = 左側最大高度 - 當前高度
            if(height[left] < height[right])
            {
                res += leftMax - height[left];
                left++;
            }
            else
            {
                // 如果右側高度較小或相等，可以確定右側的積水量
                res += rightMax - height[right];
                right--;
            }
        }
        return res;
    }
}
