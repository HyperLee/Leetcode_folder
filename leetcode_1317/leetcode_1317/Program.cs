namespace leetcode_1317;

class Program
{
    /// <summary>
    /// <para>
    /// 1317. Convert Integer to the Sum of Two No-Zero Integers
    /// https://leetcode.com/problems/convert-integer-to-the-sum-of-two-no-zero-integers/description/
    ///
    /// A No-Zero integer is a positive integer that does not contain any 0 in its decimal representation.
    ///
    /// Given an integer n, return a list of two integers [a, b] where:
    /// - a and b are No-Zero integers.
    /// - a + b = n
    ///
    /// The test cases are generated so that there is at least one valid solution. If there are many valid solutions,
    /// you can return any of them.
    ///
    /// Example 1:
    /// Input: n = 2
    /// Output: [1,1]
    /// Explanation: Let a = 1 and b = 1.
    /// Both a and b are no-zero integers, and a + b = 2 = n.
    ///
    /// Example 2:
    /// Input: n = 11
    /// Output: [2,9]
    /// Explanation: Let a = 2 and b = 9.
    /// Both a and b are no-zero integers, and a + b = 11 = n.
    /// Note that there are other valid answers as [8, 3] that can be accepted.
    ///
    /// Constraints:
    /// - 2 &lt;= n &lt;= 10^4
    /// </para>
    /// <para>
    /// 1317. 將整數轉換為兩個無零整數之和
    /// https://leetcode.cn/problems/convert-integer-to-the-sum-of-two-no-zero-integers/description/
    ///
    /// 無零整數是指十進位表示中不包含任何 0 的正整數。
    ///
    /// 給定一個整數 n，回傳由兩個整數組成的清單 [a, b]，其中：
    /// - a 與 b 都是無零整數。
    /// - a + b = n
    ///
    /// 測試案例保證至少存在一組有效解。如果存在多組有效解，可回傳其中任意一組。
    ///
    /// 範例 1：
    /// 輸入：n = 2
    /// 輸出：[1,1]
    /// 解釋：令 a = 1 且 b = 1。
    /// a 與 b 都是無零整數，而且 a + b = 2 = n。
    ///
    /// 範例 2：
    /// 輸入：n = 11
    /// 輸出：[2,9]
    /// 解釋：令 a = 2 且 b = 9。
    /// a 與 b 都是無零整數，而且 a + b = 11 = n。
    /// 請注意，其他有效答案如 [8, 3] 也會被接受。
    ///
    /// 限制條件：
    /// - 2 &lt;= n &lt;= 10^4
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 執行內建測試資料
        RunTests();
    }

    /// <summary>
    /// 執行一些範例測資並印出 GetNoZeroIntegers 的回傳結果
    /// </summary>
    static void RunTests()
    {
        // ...existing code...
        var program = new Program();
        int[] tests = new int[] {2, 11, 100, 1010, 109};
        foreach (var n in tests)
        {
            var res = program.GetNoZeroIntegers(n);
            if (res.Length == 2)
            {
                Console.WriteLine($"n={n} -> [{res[0]}, {res[1]}]");
            }
            else
            {
                Console.WriteLine($"n={n} -> No result");
            }
        }
    }

    /// <summary>
    /// <summary>
    /// 找到兩個無零整數 a 與 b 使 a + b == n。
    /// 以暴力法從 a = 1 開始嘗試，直到找到第一組同時不包含數字 '0' 的配對。
    /// 回傳長度為 2 的陣列 [a, b]（若找到），否則回傳空陣列。
    /// </summary>
    /// <param name="n">目標整數（正整數）。</param>
    /// <returns>
    /// 長度為 2 的陣列 [a, b]，代表找到的無零整數配對；若無解則回傳空陣列。
    /// </returns>
    /// <remarks>
    /// - 輸入保證至少有一組解（題目保證）。
    /// - 演算法：簡單暴力掃描 a 從 1 到 n-1，檢查 a 與 b 的十進位表示是否包含 '0'。
    /// - 時間複雜度：O(n * d)，其中 d 為轉字串判斷時的位數（通常 d = O(log n)）。
    /// - 空間複雜度：O(1)（回傳固定大小陣列）。
    /// </remarks>
    public int[] GetNoZeroIntegers(int n)
    {
        for (int a = 1; a < n; a++)
        {
            int b = n - a;
            // 將整數轉為字串並檢查是否包含 '0'
            // 這裡使用 ToString() 再 Contains('0') 為最直接的實作，易讀且對此題足夠。
            if (!a.ToString().Contains('0') && !b.ToString().Contains('0'))
            {
                // 找到第一個符合條件的配對，依題目可以回傳任一組解
                return new int[] { a, b };
            }
        }
        // 理論上題目保證有解，此行為防守式回傳（若輸入不符合題目假設時仍可安全回傳）
        return Array.Empty<int>();
    }
}
