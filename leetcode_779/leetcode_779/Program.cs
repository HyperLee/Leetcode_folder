namespace leetcode_779;

internal static class Program
{
    /// <summary>
    /// 779. K-th Symbol in Grammar
    /// https://leetcode.com/problems/k-th-symbol-in-grammar/
    /// 779. 第 K 個語法符號
    /// https://leetcode.cn/problems/k-th-symbol-in-grammar/
    /// English: A grammar starts with 0 in the first row. Each following row replaces every
    /// 0 with 01 and every 1 with 10. Given a row number n and a one-based position k, return
    /// the symbol at position k in row n.
    /// 中文：語法的第一列為 0，之後每一列都將上一列的 0 替換成 01、1 替換成 10。
    /// 給定列號 n 與從 1 起算的位置 k，回傳第 n 列中第 k 個符號。
    /// </summary>
    /// <param name="args">命令列參數；本驗證器不使用此參數。</param>
    private static void Main(string[] args)
    {
        (string CaseName, int N, int K, int Expected)[] testCases =
        {
            ("Case 1: Official example and minimum input", 1, 1, 0),
            ("Case 2: Official first symbol of row 2", 2, 1, 0),
            ("Case 3: Official second symbol of row 2", 2, 2, 1),
            ("Case 4: Odd position regression", 3, 3, 1),
            ("Case 5: End of first half", 4, 4, 0),
            ("Case 6: Start of complemented half", 4, 5, 1),
            ("Case 7: Complemented-half regression", 4, 6, 0),
            ("Case 8: Multi-level complement regression", 5, 11, 0),
            ("Case 9: Maximum row first symbol", 30, 1, 0),
            ("Case 10: Maximum row last symbol", 30, 536870912, 1)
        };

        int passedCount = 0;

        Console.WriteLine("LeetCode 779 acceptance harness");

        foreach ((string caseName, int n, int k, int expected) in testCases)
        {
            int actual = KthGrammar(n, k);
            bool passed = expected == actual;
            string result = passed ? "PASS" : "FAIL";

            Console.WriteLine();
            Console.WriteLine(caseName);
            Console.WriteLine($"Input: n={n}, k={k}");
            Console.WriteLine(
                $"{result} | K-th symbol | Expected: {expected} | Actual: {actual}");

            if (passed)
            {
                passedCount++;
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Summary: {passedCount}/{testCases.Length} checks passed.");

        if (passedCount != testCases.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 在有效輸入範圍內找出第 <paramref name="n"/> 列從 1 起算第
    /// <paramref name="k"/> 個語法符號。演算法利用每列前半段等於上一列、後半段為
    /// 上一列逐位翻轉的規律，將位置遞迴映射至前一列；輸入須符合
    /// 1 &lt;= n &lt;= 30 且 1 &lt;= k &lt;= 2^(n - 1)，回傳 0 或 1。
    /// 此方法不修改輸入且不會寫入主控台。
    /// </summary>
    /// <param name="n">從 1 起算的列號，範圍為 1 到 30。</param>
    /// <param name="k">第 n 列中從 1 起算的有效位置。</param>
    /// <returns>第 n 列第 k 個符號，值為 0 或 1。</returns>
    public static int KthGrammar(int n, int k)
    {
        if (k == 1)
        {
            return 0;
        }

        int halfLength = 1 << (n - 2);

        if (k <= halfLength)
        {
            return KthGrammar(n - 1, k);
        }

        // 後半段對應上一列的相同相對位置，但每個符號都必須翻轉。
        return 1 ^ KthGrammar(n - 1, k - halfLength);
    }
}
