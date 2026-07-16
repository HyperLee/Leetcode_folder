namespace leetcode_1423;

public class Program
{
    private readonly record struct CaseResult(
        string Label,
        string Input,
        string Expected,
        string Actual,
        bool Passed);

    /// <summary>
    /// 1423. Maximum Points You Can Obtain from Cards
    /// https://leetcode.com/problems/maximum-points-you-can-obtain-from-cards/
    /// 1423. 可獲得的最大點數
    /// https://leetcode.cn/problems/maximum-points-you-can-obtain-from-cards/
    /// Given card points arranged in a row, take exactly k cards one at a time from either end and return the maximum obtainable score.
    /// 給定一列卡牌點數，每次只能從最左端或最右端取一張；恰好取出 k 張後，回傳可取得的最大總點數。
    /// </summary>
    private static void Main()
    {
        int[] maximumLengthCards = CreateMaximumLengthCards();
        List<CaseResult> results =
        [
            EvaluateCase("Case 1: official example 1", [1, 2, 3, 4, 5, 6, 1], 3, 12),
            EvaluateCase("Case 2: official example 2", [2, 2, 2], 2, 4),
            EvaluateCase("Case 3: official example 3", [9, 7, 7, 9, 7, 7, 9], 7, 55),
            EvaluateCase("Case 4: single card", [42], 1, 42),
            EvaluateCase("Case 5: k equals one", [5, 100, 2], 1, 5),
            EvaluateCase("Case 6: mixed ends and input preservation", [10, 1, 1, 10, 1], 3, 21, verifyInputUnchanged: true),
            EvaluateCase("Case 7: k equals card count", [3, 1, 4, 1], 4, 9),
            EvaluateCase("Case 8: maximum length", maximumLengthCards, 50_000, 500_000),
        ];

        int passedChecks = 0;
        Console.WriteLine("LeetCode 1423 acceptance harness");

        foreach (CaseResult result in results)
        {
            Console.WriteLine();
            Console.WriteLine(result.Label);
            Console.WriteLine($"Input: {result.Input}");
            Console.WriteLine($"Expected: {result.Expected}");
            Console.WriteLine($"Actual: {result.Actual}");
            Console.WriteLine(result.Passed ? "PASS" : "FAIL");

            if (result.Passed)
            {
                passedChecks++;
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Summary: {passedChecks}/{results.Count} checks passed.");

        if (passedChecks != results.Count)
        {
            Environment.ExitCode = 1;
        }
    }

    private static CaseResult EvaluateCase(
        string label,
        int[] cardPoints,
        int k,
        int expected,
        bool verifyInputUnchanged = false)
    {
        int[] original = (int[])cardPoints.Clone();
        int actual = MaxScore(cardPoints, k);
        bool inputUnchanged = cardPoints.SequenceEqual(original);
        bool passed = actual == expected && (!verifyInputUnchanged || inputUnchanged);
        string expectedScore = expected.ToString(System.Globalization.CultureInfo.InvariantCulture);
        string actualScore = actual.ToString(System.Globalization.CultureInfo.InvariantCulture);
        string expectedText = verifyInputUnchanged ? $"{expectedScore}; input unchanged: True" : expectedScore;
        string actualText = verifyInputUnchanged ? $"{actualScore}; input unchanged: {inputUnchanged}" : actualScore;

        return new CaseResult(
            label,
            $"cardPoints = {FormatCards(cardPoints)}, k = {k.ToString(System.Globalization.CultureInfo.InvariantCulture)}",
            expectedText,
            actualText,
            passed);
    }

    /// <summary>
    /// 計算從題目保證有效且非空的卡牌陣列兩端恰好取出 <paramref name="k"/> 張時可取得的最大點數。
    /// 方法以全部點數減去長度為 <c>cardPoints.Length - k</c> 的最小連續中段，等價表示保留該中段並取走其餘兩端；不修改輸入，回傳最大總點數。
    /// </summary>
    /// <param name="cardPoints">符合題目限制、每張點數為正整數的非空卡牌陣列。</param>
    /// <param name="k">符合題目限制、介於 1 與卡牌數量之間且必須取出的張數。</param>
    /// <returns>只從左右兩端恰好取出 <paramref name="k"/> 張可得到的最大點數。</returns>
    public static int MaxScore(int[] cardPoints, int k)
    {
        int totalScore = 0;

        foreach (int cardPoint in cardPoints)
        {
            totalScore += cardPoint;
        }

        int windowLength = cardPoints.Length - k;

        if (windowLength == 0)
        {
            return totalScore;
        }

        int currentWindowScore = 0;

        for (int index = 0; index < windowLength; index++)
        {
            currentWindowScore += cardPoints[index];
        }

        int minimumWindowScore = currentWindowScore;

        for (int right = windowLength; right < cardPoints.Length; right++)
        {
            // 固定長度視窗右移時，同時加入右端新卡並移除左端舊卡。
            currentWindowScore += cardPoints[right] - cardPoints[right - windowLength];
            minimumWindowScore = Math.Min(minimumWindowScore, currentWindowScore);
        }

        // 視窗代表未取走的連續中段；使它最小即可讓兩端被取走的總和最大。
        return totalScore - minimumWindowScore;
    }

    private static int[] CreateMaximumLengthCards()
    {
        int[] cards = new int[100_000];

        Array.Fill(cards, 1);

        for (int index = 50_000; index < cards.Length; index++)
        {
            cards[index] = 10;
        }

        return cards;
    }

    private static string FormatCards(int[] cards)
    {
        if (cards.Length <= 10)
        {
            return $"[{string.Join(',', cards.Select(card => card.ToString(System.Globalization.CultureInfo.InvariantCulture)))}]";
        }

        string length = cards.Length.ToString("N0", System.Globalization.CultureInfo.InvariantCulture);
        return $"{length} cards [{cards[0]},{cards[1]},{cards[2]},...,{cards[^3]},{cards[^2]},{cards[^1]}]";
    }
}
