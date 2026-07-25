namespace leetcode_2024;

internal static class Program
{
    /// <summary>
    /// LeetCode 2024. Maximize the Confusion of an Exam.
    /// LeetCode 2024. 考試的最大困擾度。
    /// English: https://leetcode.com/problems/maximize-the-confusion-of-an-exam/
    /// 中文：https://leetcode.cn/problems/maximize-the-confusion-of-an-exam/
    /// English: Given an answer key containing only 'T' and 'F', and an integer k, change
    /// at most k answers so that the longest possible consecutive run of equal answers is
    /// as long as possible. Return that maximum length.
    /// 中文：給定只包含 'T' 與 'F' 的答案字串 answerKey，以及整數 k；最多可變更 k 個
    /// 答案，請讓相同答案的最長連續區段盡可能延長，並回傳其最大長度。
    /// </summary>
    private static void Main()
    {
        string maximumInput = string.Concat(Enumerable.Repeat("TF", 25_000));
        TestCase[] cases =
        [
            new("Official example 1", "TTFF", 2, 4, 4, 4),
            new("Official example 2", "TFFT", 1, 3, 2, 3),
            new("Official example 3", "TTFTTFTT", 1, 2, 5, 5),
            new("Minimum input", "T", 1, 1, 1, 1),
            new("All answers equal", "FFFF", 1, 4, 1, 4),
            new("Alternating answers", "TFTFTF", 1, 3, 3, 3),
            new("Window shrink regression", "TTFFFTTT", 1, 4, 4, 4),
            new("Replacement budget equals length", "TFTF", 4, 4, 4, 4),
            new("Maximum-length alternating input", maximumInput, 1, 3, 3, 3)
        ];

        Console.WriteLine("LeetCode 2024 Acceptance Harness");

        int passedChecks = 0;
        foreach (TestCase testCase in cases)
        {
            int overallActual = MaxConsecutiveAnswers(testCase.AnswerKey, testCase.K);
            int replacingTActual = MaxConsecutiveChar(testCase.AnswerKey, testCase.K, 'T');
            int replacingFActual = MaxConsecutiveChar(testCase.AnswerKey, testCase.K, 'F');

            Console.WriteLine($"Case: {testCase.Name}");
            Console.WriteLine(
                $"Input: answerKey = {FormatAnswerKey(testCase.AnswerKey)}, k = {testCase.K}");

            CheckResult overallResult = EvaluateCheck(
                "MaxConsecutiveAnswers result",
                testCase.ExpectedOverall,
                overallActual);
            Console.WriteLine(overallResult.Output);
            passedChecks += overallResult.Passed ? 1 : 0;

            CheckResult replacingTResult = EvaluateCheck(
                "MaxConsecutiveChar replacing 'T'",
                testCase.ExpectedWhenReplacingT,
                replacingTActual);
            Console.WriteLine(replacingTResult.Output);
            passedChecks += replacingTResult.Passed ? 1 : 0;

            CheckResult replacingFResult = EvaluateCheck(
                "MaxConsecutiveChar replacing 'F'",
                testCase.ExpectedWhenReplacingF,
                replacingFActual);
            Console.WriteLine(replacingFResult.Output);
            passedChecks += replacingFResult.Passed ? 1 : 0;
            Console.WriteLine();
        }

        const int totalChecks = 27;
        Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");

        if (passedChecks != totalChecks)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 對長度介於 1 至 50,000、只含 'T' 與 'F' 的 <paramref name="answerKey" />，
    /// 分別計算最多替換 <paramref name="k" /> 個 'T' 或 'F' 時可形成的最長同字元區段，
    /// 並回傳兩者較大值。此純函式不修改輸入也不輸出主控台；時間複雜度為 O(n)，輔助
    /// 空間為 O(1)。
    /// </summary>
    public static int MaxConsecutiveAnswers(string answerKey, int k)
    {
        return Math.Max(
            MaxConsecutiveChar(answerKey, k, 'T'),
            MaxConsecutiveChar(answerKey, k, 'F'));
    }

    /// <summary>
    /// 對題目保證有效的 <paramref name="answerKey" /> 與替換額度
    /// <paramref name="k" />，使用滑動視窗尋找最多包含 k 個
    /// <paramref name="ch" /> 的最長區段；這些字元可被替換成另一種答案，使整段一致。
    /// <paramref name="ch" /> 必須為 'T' 或 'F'。回傳最長合法視窗長度；此純函式不修改
    /// 輸入也不輸出主控台，時間複雜度為 O(n)，輔助空間為 O(1)。
    /// </summary>
    public static int MaxConsecutiveChar(string answerKey, int k, char ch)
    {
        int left = 0;
        int replacementsUsed = 0;
        int maximumLength = 0;

        for (int right = 0; right < answerKey.Length; right++)
        {
            if (answerKey[right] == ch)
            {
                replacementsUsed++;
            }

            // 超出額度時收縮左界，直到視窗重新符合「至多替換 k 個 ch」的不變量。
            while (replacementsUsed > k)
            {
                if (answerKey[left] == ch)
                {
                    replacementsUsed--;
                }

                left++;
            }

            maximumLength = Math.Max(maximumLength, right - left + 1);
        }

        return maximumLength;
    }

    private static CheckResult EvaluateCheck(string checkName, int expected, int actual)
    {
        bool passed = expected == actual;
        string output =
            $"{(passed ? "PASS" : "FAIL")} {checkName} | " +
            $"Expected: {expected} | Actual: {actual}";
        return new CheckResult(passed, output);
    }

    private static string FormatAnswerKey(string answerKey)
    {
        const int visibleCharactersPerSide = 16;
        if (answerKey.Length <= visibleCharactersPerSide * 2)
        {
            return $"\"{answerKey}\"";
        }

        return
            $"\"{answerKey[..visibleCharactersPerSide]}..." +
            $"{answerKey[^visibleCharactersPerSide..]}\" (length: {answerKey.Length})";
    }

    private sealed record TestCase(
        string Name,
        string AnswerKey,
        int K,
        int ExpectedWhenReplacingT,
        int ExpectedWhenReplacingF,
        int ExpectedOverall);

    private sealed record CheckResult(bool Passed, string Output);
}