namespace leetcode_1621;

class Program
{
    /// <summary>
    /// 1621. Number of Sets of K Non-Overlapping Line Segments
    /// https://leetcode.com/problems/number-of-sets-of-k-non-overlapping-line-segments/description/
    /// <para>
    /// English:
    /// Given n points on a 1-D plane, where the ith point (from 0 to n-1) is at x = i, find the number of ways we can draw exactly k non-overlapping line segments such that each segment covers two or more points. The endpoints of each segment must have integral coordinates. The k line segments do not have to cover all n points, and they are allowed to share endpoints.
    ///
    /// Return the number of ways we can draw k non-overlapping line segments. Since this number can be huge, return it modulo 10^9 + 7.
    ///
    /// Example 1:
    /// Input: n = 4, k = 2
    /// Output: 5
    /// Explanation: The two line segments are shown in red and blue.
    /// The image above shows the 5 different ways {(0,2),(2,3)}, {(0,1),(1,3)}, {(0,1),(2,3)}, {(1,2),(2,3)}, {(0,1),(1,2)}.
    ///
    /// Example 2:
    /// Input: n = 3, k = 1
    /// Output: 3
    /// Explanation: The 3 ways are {(0,1)}, {(0,2)}, {(1,2)}.
    ///
    /// Example 3:
    /// Input: n = 30, k = 7
    /// Output: 796297179
    /// Explanation: The total number of possible ways to draw 7 line segments is 3796297200. Taking this number modulo 10^9 + 7 gives us 796297179.
    ///
    /// Constraints:
    /// - 2 &lt;= n &lt;= 1000
    /// - 1 &lt;= k &lt;= n - 1
    /// </para>
    /// <para>
    /// 1621. 大小為 K 的不重疊線段數目
    /// https://leetcode.cn/problems/number-of-sets-of-k-non-overlapping-line-segments/description/
    ///
    /// 繁體中文：
    /// 給定一維平面上的 n 個點，其中第 i 個點（編號從 0 到 n - 1）位於 x = i。請找出恰好繪製 k 條互不重疊線段的方式數量，且每條線段都必須涵蓋兩個或以上的點。每條線段的端點都必須具有整數座標。這 k 條線段不需要涵蓋全部 n 個點，而且允許共用端點。
    ///
    /// 請回傳繪製 k 條互不重疊線段的方式數量。由於這個數字可能非常大，請回傳其對 10^9 + 7 取模後的結果。
    ///
    /// 範例 1：
    /// 輸入：n = 4, k = 2
    /// 輸出：5
    /// 解釋：兩條線段分別以紅色與藍色表示。
    /// 上圖展示了 5 種不同的方式：{(0,2),(2,3)}、{(0,1),(1,3)}、{(0,1),(2,3)}、{(1,2),(2,3)}、{(0,1),(1,2)}。
    ///
    /// 範例 2：
    /// 輸入：n = 3, k = 1
    /// 輸出：3
    /// 解釋：共有 3 種方式：{(0,1)}、{(0,2)}、{(1,2)}。
    ///
    /// 範例 3：
    /// 輸入：n = 30, k = 7
    /// 輸出：796297179
    /// 解釋：繪製 7 條線段的所有可能方式共有 3796297200 種。將這個數字對 10^9 + 7 取模後，結果為 796297179。
    ///
    /// 限制條件：
    /// - 2 &lt;= n &lt;= 1000
    /// - 1 &lt;= k &lt;= n - 1
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}