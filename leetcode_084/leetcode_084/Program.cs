namespace leetcode_084;

public class Solution 
{
    /// <summary>
    /// 計算直方圖中最大矩形面積
    /// 解題思路：
    /// 1. 使用單調棧（Monotonic Stack）來找出每個柱子左右兩側第一個比它矮的柱子
    /// 2. 對每個柱子，計算以它為高度的最大矩形面積
    /// 3. 時間複雜度：O(n)，空間複雜度：O(n)
    /// 
    /// 注意:不要設錯哨兵，否則會出現邊界問題
    /// left[i] = (monoStack.Count == 0 ? -1 : monoStack.Peek());
    /// 哨兵預設是 -1，如果棧為空，表示左側沒有更矮的柱子
    /// 否則，棧頂元素即為左側第一個較矮的柱子
    /// 
    /// right[i] = (monoStack.Count == 0 ? n : monoStack.Peek());
    /// 哨兵預設是 n，表示右側沒有更矮的柱子
    /// 否則，棧頂元素即為右側第一個較矮的柱子
    /// 
    /// left[i] 和 right[i] 分別表示第 i 個柱子的左右邊界
    /// 面積 = (right[i] - left[i] - 1) * heights[i]
    /// monoStack 存儲的是柱子的索引，而不是柱子的高度 
    /// 
    /// 附件檔案: 單調棧.MD, 可以先閱讀後再閱讀此代碼
    /// <param name="heights">表示直方圖高度的整數陣列</param>
    /// <returns>最大矩形面積</returns>
    public int LargestRectangleArea(int[] heights) 
    {
        int n = heights.Length;
        // 儲存每個柱子左側第一個較矮的柱子的索引(index)
        int[] left = new int[n];
        // 儲存每個柱子右側第一個較矮的柱子的索引(index)
        int[] right = new int[n];
        
        // 使用單調遞增棧來找尋左/右側邊界
        Stack<int> monoStack = new Stack<int>();
        
        // 從左向右遍歷，找尋左側邊界
        for (int i = 0; i < n; i++) 
        {
            // 當棧不為空且當前柱子高度小於等於棧頂柱子高度時，彈出棧頂
            while (monoStack.Count > 0 && heights[monoStack.Peek()] >= heights[i]) 
            {
                monoStack.Pop();
            }
            // 如果棧為空，表示左側沒有更矮的柱子，設為-1
            // 否則，棧頂元素即為左側第一個較矮的柱子
            left[i] = (monoStack.Count == 0 ? -1 : monoStack.Peek());
            // 將當前柱子索引入棧
            monoStack.Push(i);
        }

        // 清空棧，準備找尋右側邊界
        monoStack.Clear();
        // 從右向左遍歷，找尋右側邊界
        for (int i = n - 1; i >= 0; i--) 
        {
            // 邏輯同上，但方向相反
            while (monoStack.Count > 0 && heights[monoStack.Peek()] >= heights[i]) 
            {
                monoStack.Pop();
            }
            // 如果棧為空，表示右側沒有更矮的柱子，設為n
            right[i] = (monoStack.Count == 0 ? n : monoStack.Peek());
            // 將當前柱子索引入棧
            monoStack.Push(i);
        }
        
        // 計算最大面積
        int res = 0;
        for (int i = 0; i < n; i++) 
        {
            // 對每個柱子，計算以它為高度的矩形面積
            // 寬度 = 右邊界 - 左邊界 - 1
            // 高度 = heights[i]
            res = Math.Max(res, (right[i] - left[i] - 1) * heights[i]);
        }
        return res;
    }
}

class Program
{
    /// <summary>
    /// 84. Largest Rectangle in Histogram
    /// https://leetcode.com/problems/largest-rectangle-in-histogram/description/
    /// 84. 柱状图中最大的矩形
    /// https://leetcode.cn/problems/largest-rectangle-in-histogram/description/ 
    /// 
    /// LeetCode 84. 柱狀圖中最大的矩形
    /// 題目描述：
    /// 給定 n 個非負整數，用來表示柱狀圖中各個柱子的高度。
    /// 每個柱子的寬度為 1，請計算在該柱狀圖中能夠勾勒出的矩形的最大面積。
    /// 解題思路：
    /// 1. 使用單調棧（Monotonic Stack）解法
    /// 2. 為什麼選擇單調棧？
    ///    - 時間複雜度為 O(n)，比暴力解法 O(n^2) 更優
    ///    - 能有效找出每個柱子左右兩側第一個較矮的柱子
    ///    - 空間複雜度為 O(n)，用於存儲左右邊界
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Solution solution = new Solution();
        
        // 測試案例 1：基本案例
        int[] test1 = new int[] { 2, 1, 5, 6, 2, 3 };
        Console.WriteLine("測試案例 1：[2, 1, 5, 6, 2, 3]");
        Console.WriteLine($"結果：{solution.LargestRectangleArea(test1)}"); // 預期輸出：10
        
        // 測試案例 2：遞增序列
        int[] test2 = new int[] { 1, 2, 3, 4, 5 };
        Console.WriteLine("\n測試案例 2：[1, 2, 3, 4, 5]");
        Console.WriteLine($"結果：{solution.LargestRectangleArea(test2)}"); // 預期輸出：9
        
        // 測試案例 3：遞減序列
        int[] test3 = new int[] { 5, 4, 3, 2, 1 };
        Console.WriteLine("\n測試案例 3：[5, 4, 3, 2, 1]");
        Console.WriteLine($"結果：{solution.LargestRectangleArea(test3)}"); // 預期輸出：9
        
        // 測試案例 4：全相等
        int[] test4 = new int[] { 2, 2, 2, 2 };
        Console.WriteLine("\n測試案例 4：[2, 2, 2, 2]");
        Console.WriteLine($"結果：{solution.LargestRectangleArea(test4)}"); // 預期輸出：8
        
        // 測試案例 5：極端案例
        int[] test5 = new int[] { 1 };
        Console.WriteLine("\n測試案例 5：[1]");
        Console.WriteLine($"結果：{solution.LargestRectangleArea(test5)}"); // 預期輸出：1
    }
}
