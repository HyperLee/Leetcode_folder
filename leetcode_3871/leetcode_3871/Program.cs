namespace leetcode_3871;

class Program
{
    /// <summary>
    /// 3871. Count Commas in Range II
    /// https://leetcode.com/problems/count-commas-in-range-ii/description/
    /// 3871. 統計範圍內的逗號 II
    /// https://leetcode.cn/problems/count-commas-in-range-ii/description/
    ///
    /// English:
    /// You are given an integer n.
    ///
    /// Return the total number of commas used when writing all integers from [1, n] (inclusive) in standard number formatting.
    ///
    /// In standard formatting:
    /// - A comma is inserted after every three digits from the right.
    /// - Numbers with fewer than 4 digits contain no commas.
    ///
    /// 繁體中文：
    /// 給定一個整數 n。
    ///
    /// 請回傳以標準數字格式書寫從 [1, n]（含端點）的所有整數時，所使用的逗號總數。
    ///
    /// 標準格式如下：
    /// - 每三位數從右側往左插入一個逗號。
    /// - 少於 4 位數的數字不包含逗號。
    /// </summary>
    /// <remarks>
    /// This entry point does not require command-line input. It runs fixed boundary cases,
    /// prints PASS or FAIL for each case, and returns a non-zero exit code when a case fails.
    /// 此進入點不需要命令列輸入，會執行固定邊界案例；只要有案例失敗，就會回傳非零結束碼。
    /// </remarks>
    /// <param name="args">Command-line arguments; no interactive input is required.</param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        string[] testNames =
        {
            "官方範例 1",
            "官方範例 2",
            "最小合法輸入",
            "逗號門檻前",
            "第一個逗號",
            "第一層門檻前",
            "第二個逗號",
            "第二個逗號後一位",
            "第三層門檻前",
            "第三個逗號",
            "第四個逗號",
            "最大限制"
        };
        long[] testInputs =
        {
            1002,
            998,
            1,
            999,
            1000,
            999999,
            1000000,
            1000001,
            999999999,
            1000000000,
            1000000000000,
            1000000000000000
        };
        long[] expectedResults =
        {
            3,
            0,
            0,
            0,
            1,
            999000,
            999002,
            999004,
            1998999000,
            1998999003,
            2998998999004,
            3998998998999005
        };
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
    /// 執行一組固定案例，驗證 CountCommas 是否回傳預期的逗號總數並輸出結果。
    /// 輸入條件遵循題目的 1 <= n <= 10^15；兩者相等時回傳 true，否則回傳 false。
    /// </summary>
    /// <param name="solution">要接受驗證的解法物件。</param>
    /// <param name="caseName">固定案例的顯示名稱。</param>
    /// <param name="n">要計算的整數上限。</param>
    /// <param name="expected">案例預期的逗號總數。</param>
    /// <returns>實際結果符合預期時回傳 true，否則回傳 false。</returns>
    private static bool RunTestCase(Program solution, string caseName, long n, long expected)
    {
        long actual = solution.CountCommas(n);
        bool passed = actual == expected;
        string result = passed ? "PASS" : "FAIL";

        Console.WriteLine(
            $"{caseName}：n = {n}，預期：{expected}，實際：{actual}，結果：{result}");
        return passed;
    }

    /// <summary>
    /// 使用門檻貢獻法，計算從 1 到 n 以標準格式書寫時的逗號總數。
    /// 每個 1000 的次方代表一個逗號位置；對於每個不超過 n 的門檻，
    /// [門檻, n] 中的每個整數各貢獻一個逗號。輸入條件為 1 <= n <= 10^15，
    /// 輸出結果為所有整數使用的逗號總數，時間複雜度為 O(log_1000 n)，額外空間複雜度為 O(1)。
    /// </summary>
    /// <param name="n">要計算的整數上限，範圍為 1 <= n <= 10^15。</param>
    /// <returns>從 1 到 n 以標準格式書寫時使用的逗號總數。</returns>
    public long CountCommas(long n)
    {
        long totalCommas = 0;

        for (long threshold = 1000; threshold <= n; threshold *= 1000)
        {
            // threshold = 1000^k；所有大於等於此門檻的數字都包含第 k 個逗號。
            // 因此 [threshold, n] 的數量 n - threshold + 1，就是這個逗號位置的貢獻。
            totalCommas += n - threshold + 1;
        }

        return totalCommas;
    }
}