namespace leetcode_1952;

internal static class Program
{
    /// <summary>
    /// LeetCode 1952. Three Divisors.
    /// LeetCode 1952. 三除數。
    /// English: https://leetcode.com/problems/three-divisors/
    /// 中文：https://leetcode.cn/problems/three-divisors/
    /// English: Given an integer n, return true if n has exactly three positive divisors;
    /// otherwise, return false.
    /// 中文：給定整數 n，若 n 恰好有三個正因數則回傳 true，否則回傳 false。
    /// </summary>
    private static void Main()
    {
        TestCase[] cases =
        [
            new("Official example 1", 2, false),
            new("Official example 2", 4, true),
            new("Minimum input", 1, false),
            new("Small prime square", 9, true),
            new("Composite square", 16, false),
            new("Odd composite square", 81, false),
            new("Non-square composite", 8, false),
            new("Prime but not square", 97, false),
            new("Another prime square", 25, true),
            new("Near-limit prime square", 9_409, true),
            new("Maximum input", 10_000, false)
        ];

        Console.WriteLine("LeetCode 1952 Acceptance Harness");

        int passedChecks = 0;
        foreach (TestCase testCase in cases)
        {
            bool divisorCountActual = IsThree(testCase.Input);
            bool primeSquareActual = IsThree2(testCase.Input);

            Console.WriteLine($"Case: {testCase.Name}");
            Console.WriteLine($"Input: n = {testCase.Input}");

            CheckResult divisorCountResult = EvaluateCheck(
                "IsThree result",
                testCase.Expected,
                divisorCountActual);
            Console.WriteLine(divisorCountResult.Output);
            passedChecks += divisorCountResult.Passed ? 1 : 0;

            CheckResult primeSquareResult = EvaluateCheck(
                "IsThree2 result",
                testCase.Expected,
                primeSquareActual);
            Console.WriteLine(primeSquareResult.Output);
            passedChecks += primeSquareResult.Passed ? 1 : 0;
            Console.WriteLine();
        }

        const int totalChecks = 22;
        Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");

        if (passedChecks != totalChecks)
        {
            Environment.ExitCode = 1;
        }
    }

    private static CheckResult EvaluateCheck<T>(string checkName, T expected, T actual)
    {
        bool passed = EqualityComparer<T>.Default.Equals(expected, actual);
        string output = $"{(passed ? "PASS" : "FAIL")} {checkName} | Expected: {expected} | Actual: {actual}";
        return new CheckResult(passed, output);
    }

    /// <summary>
    /// 對題目保證介於 1 至 10000 的 <paramref name="n" />，枚舉至平方根並以成對方式
    /// 統計正因數；平方根本身只計一次，其餘整除者與商各計一次。恰有三個正因數時回傳
    /// <see langword="true" />，否則回傳 <see langword="false" />。此純函式不修改輸入、
    /// 不輸出主控台，時間複雜度為 O(√n)，結果空間與輔助空間皆為 O(1)。
    /// </summary>
    public static bool IsThree(int n)
    {
        int divisorCount = 0;

        for (int divisor = 1; divisor * divisor <= n; divisor++)
        {
            if (n % divisor != 0)
            {
                continue;
            }

            // 非平方根因數必與另一個不同的商成對出現；平方根則只能計算一次。
            divisorCount += divisor == n / divisor ? 1 : 2;
        }

        return divisorCount == 3;
    }

    /// <summary>
    /// 對題目保證介於 1 至 10000 的 <paramref name="n" />，利用「正整數恰有三個正因數
    /// 當且僅當它是質數平方」的不變量，先確認整數平方根能還原原數，再檢查平方根是否為
    /// 質數。符合時回傳 <see langword="true" />，否則回傳 <see langword="false" />。
    /// 此純函式不修改輸入、不輸出主控台，時間複雜度為 O(n^(1/4))，結果空間與輔助空間
    /// 皆為 O(1)。
    /// </summary>
    public static bool IsThree2(int n)
    {
        int squareRoot = (int)Math.Sqrt(n);

        // 必須同時排除非完全平方數，以及平方根為合數而產生更多因數的情況。
        return squareRoot * squareRoot == n && IsPrime(squareRoot);
    }

    /// <summary>
    /// 判斷由題目輸入平方根得到、介於 1 至 100 的 <paramref name="value" /> 是否為質數。
    /// 只需嘗試不大於平方根的可能因數；找到整除者時回傳 <see langword="false" />，否則
    /// 回傳 <see langword="true" />。
    /// </summary>
    private static bool IsPrime(int value)
    {
        if (value < 2)
        {
            return false;
        }

        for (int divisor = 2; divisor * divisor <= value; divisor++)
        {
            if (value % divisor == 0)
            {
                return false;
            }
        }

        // 未找到不大於平方根的因數，即不存在可與之成對的其他非平凡因數。
        return true;
    }

    private sealed record TestCase(string Name, int Input, bool Expected);

    private sealed record CheckResult(bool Passed, string Output);
}