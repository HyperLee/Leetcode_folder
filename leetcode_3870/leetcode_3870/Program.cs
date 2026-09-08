namespace leetcode_3870;

class Program
{
    /// <summary>
    /// Problem: Total Number of Commas
    ///
    /// English:
    /// You are given an integer n.
    ///
    /// Return the total number of commas used when writing all integers from [1, n] (inclusive) in standard number formatting.
    ///
    /// In standard formatting:
    ///
    /// - A comma is inserted after every three digits from the right.
    /// - Numbers with fewer than 4 digits contain no commas.
    ///
    /// 繁體中文：
    /// 給定一個整數 n。
    ///
    /// 請回傳以標準數字格式書寫從 [1, n]（包含頭尾）的所有整數時，所使用的逗號總數。
    ///
    /// 在標準格式中：
    ///
    /// - 從右側開始，每三位數字插入一個逗號。
    /// - 少於 4 位數的數字不包含逗號。
    /// </summary>
    /// <remarks>
    /// This entry point does not require command-line input and keeps the starter output
    /// so the project can be launched directly from the debugger.
    /// 此程式進入點不需要命令列輸入，並保留範例起始輸出，方便直接由偵錯器執行。
    /// </remarks>
    /// <param name="args">Command-line arguments; not used by this documentation-only example. 命令列參數；此摘要範例不使用。</param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        string[] testNames =
        {
            "官方範例 1",
            "官方範例 2",
            "逗號門檻前",
            "第一個逗號",
            "最小合法輸入",
            "最大限制"
        };
        int[] testInputs = { 1002, 998, 999, 1000, 1, 100000 };
        int[] expectedResults = { 3, 0, 0, 1, 0, 99001 };
        int passedCount = 0;

        for (int index = 0; index < testInputs.Length; index++)
        {
            if (RunTestCase(solution, testNames[index], testInputs[index], expectedResults[index]))
            {
                passedCount++;
            }
        }

        int failedCount = testInputs.Length - passedCount;
        Console.WriteLine($"總結：{passedCount}/{testInputs.Length} 通過，{failedCount} 個失敗。");
        Environment.ExitCode = failedCount == 0 ? 0 : 1;
    }

    /// <summary>
    /// 執行一組固定案例，分別驗證兩種 CountCommas 解法是否得到預期的逗號總數，並輸出 PASS 或 FAIL。
    /// 輸入條件遵循題目的 1 &lt;= n &lt;= 10^5；輸出結果為該案例是否通過。
    /// </summary>
    /// <param name="solution">要接受驗證的解法物件。</param>
    /// <param name="caseName">固定案例的顯示名稱。</param>
    /// <param name="n">要計算的整數上限。</param>
    /// <param name="expected">案例預期的逗號總數。</param>
    /// <returns>兩種解法都符合預期時回傳 true，否則回傳 false。</returns>
    private static bool RunTestCase(Program solution, string caseName, int n, int expected)
    {
        int actualByIteration = solution.CountCommas(n);
        int actualByFormula = solution.CountCommas2(n);
        bool passed = actualByIteration == expected && actualByFormula == expected;
        string result = passed ? "PASS" : "FAIL";

        Console.WriteLine(
            $"{caseName}：n = {n}，預期：{expected}，解法一：{actualByIteration}，解法二：{actualByFormula}，結果：{result}");
        return passed;
    }

    /// <summary>
    /// 方法一：遍歷計數。
    /// 在題目限制 1 &lt;= n &lt;= 10^5 下，只有 1000 到 n 的數字包含逗號，且每個數字恰好包含一個逗號。
    /// 此方法逐一檢查 1 到 n 的所有整數，遇到至少四位數的整數就累加一次，時間複雜度為 O(n)，額外空間複雜度為 O(1)。
    /// 輸入條件是符合題目限制的整數 n，輸出結果是從 1 到 n 的逗號總數。
    /// </summary>
    /// <param name="n">要計算的整數上限，範圍為 1 &lt;= n &lt;= 10^5。</param>
    /// <returns>從 1 到 n 以標準格式書寫時使用的逗號總數。</returns>
    public int CountCommas(int n)
    {
        int count = 0;

        for (int value = 1; value <= n; value++)
        {
            // 題目上限不超過 10^5，因此每個至少四位數的值恰好貢獻一個逗號。
            if (value >= 1000)
            {
                count++;
            }
        }
        return count;
    }

    /// <summary>
    /// 方法二：直接計算。
    /// 觀察到 1000 是第一個包含逗號的數字，而題目限制 n &lt;= 10^5，
    /// 因此 [1000, n] 中的每個數字都恰好貢獻一個逗號。包含逗號的數字數量為 n - 999，
    /// 再與 0 取最大值即可在 O(1) 時間、O(1) 額外空間內得到答案。
    /// 輸入條件是符合題目限制的整數 n，輸出結果是從 1 到 n 的逗號總數。
    /// </summary>
    /// <param name="n">要計算的整數上限，範圍為 1 &lt;= n &lt;= 10^5。</param>
    /// <returns>從 1 到 n 以標準格式書寫時使用的逗號總數。</returns>
    public int CountCommas2(int n)
    {
        // [1000, n] 是包含逗號的整數區間，其長度為 n - 1000 + 1 = n - 999。
        return Math.Max(n - 999, 0);
    }
}