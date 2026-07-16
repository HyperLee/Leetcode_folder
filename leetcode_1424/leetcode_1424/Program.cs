namespace leetcode_1424;

public class Program
{
    private readonly record struct CaseResult(
        string Label,
        string Input,
        string Expected,
        string Actual,
        bool Passed);

    /// <summary>
    /// 1424. Diagonal Traverse II
    /// https://leetcode.com/problems/diagonal-traverse-ii/
    /// 1424. 對角線遍歷 II
    /// https://leetcode.cn/problems/diagonal-traverse-ii/
    /// Given a two-dimensional integer list, return all elements in diagonal order from lower rows to upper rows within each diagonal.
    /// 給定二維整數串列，依對角線順序回傳所有元素；同一條對角線內須由較下方列走向較上方列。
    /// </summary>
    private static void Main()
    {
        List<CaseResult> results =
        [
            EvaluateCase(
                "Case 1: official example 1",
                CreateInput([1, 2, 3], [4, 5, 6], [7, 8, 9]),
                [1, 4, 2, 7, 5, 3, 8, 6, 9]),
            EvaluateCase(
                "Case 2: official example 2",
                CreateInput([1, 2, 3, 4, 5], [6, 7], [8], [9, 10, 11], [12, 13, 14, 15, 16]),
                [1, 6, 2, 8, 7, 3, 9, 4, 12, 10, 5, 13, 11, 14, 15, 16]),
            EvaluateCase(
                "Case 3: irregular rows",
                CreateInput([1, 2, 3], [4], [5, 6, 7], [8], [9, 10, 11]),
                [1, 4, 2, 5, 3, 8, 6, 9, 7, 10, 11]),
            EvaluateCase(
                "Case 4: single row",
                CreateInput([1, 2, 3, 4, 5, 6]),
                [1, 2, 3, 4, 5, 6]),
            EvaluateCase(
                "Case 5: single value",
                CreateInput([42]),
                [42]),
            EvaluateCase(
                "Case 6: input preservation",
                CreateInput([1], [2, 3, 4], [5], [6, 7], [8, 9, 10, 11]),
                [1, 2, 5, 3, 6, 4, 8, 7, 9, 10, 11],
                verifyInputUnchanged: true),
            EvaluateMaximumRowsCase(),
        ];

        int passedChecks = 0;
        Console.WriteLine("LeetCode 1424 acceptance harness");

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
        Console.WriteLine($"Summary: {passedChecks.ToString(System.Globalization.CultureInfo.InvariantCulture)}/{results.Count.ToString(System.Globalization.CultureInfo.InvariantCulture)} checks passed.");

        if (passedChecks != results.Count)
        {
            Environment.ExitCode = 1;
        }
    }

    private static CaseResult EvaluateCase(
        string label,
        IList<IList<int>> nums,
        int[] expected,
        bool verifyInputUnchanged = false)
    {
        int[][] original = nums.Select(row => row.ToArray()).ToArray();
        int[] actual = FindDiagonalOrder(nums);
        bool inputUnchanged = nums.Count == original.Length
            && nums.Select((row, index) => row.SequenceEqual(original[index])).All(isUnchanged => isUnchanged);
        bool passed = actual.SequenceEqual(expected) && (!verifyInputUnchanged || inputUnchanged);
        string expectedText = FormatValues(expected);
        string actualText = FormatValues(actual);

        if (verifyInputUnchanged)
        {
            expectedText += "; input unchanged: True";
            actualText += $"; input unchanged: {inputUnchanged}";
        }

        return new CaseResult(label, FormatInput(nums), expectedText, actualText, passed);
    }

    private static CaseResult EvaluateMaximumRowsCase()
    {
        const int rowCount = 100_000;
        IList<IList<int>> nums = Enumerable.Range(1, rowCount)
            .Select(value => (IList<int>)new[] { value })
            .ToList();
        int[] actual = FindDiagonalOrder(nums);
        int? first = actual.Length > 0 ? actual[0] : null;
        int? middle = actual.Length > 50_000 ? actual[50_000] : null;
        int? last = actual.Length > 0 ? actual[^1] : null;
        bool passed = actual.Length == rowCount
            && first == 1
            && middle == 50_001
            && last == 100_000;

        return new CaseResult(
            "Case 7: 100,000 single-element rows",
            "100,000 rows containing [1] through [100000]",
            "length = 100000; first/middle/last = 1/50001/100000",
            $"length = {actual.Length.ToString(System.Globalization.CultureInfo.InvariantCulture)}; first/middle/last = {FormatSpotValue(first)}/{FormatSpotValue(middle)}/{FormatSpotValue(last)}",
            passed);
    }

    /// <summary>
    /// 將題目保證有效且每列非空的二維整數串列依對角線順序攤平成一維陣列。
    /// 每個 bucket 的索引固定為 <c>row + column</c>；由最後一列反向掃描，可讓同一對角線內的元素自然維持由下往上的順序。
    /// 方法不修改輸入，回傳依對角線索引遞增排列的所有元素。
    /// </summary>
    /// <param name="nums">符合題目限制、至少包含一列且每列至少包含一個整數的串列。</param>
    /// <returns>包含全部輸入元素的對角線遍歷結果。</returns>
    public static int[] FindDiagonalOrder(IList<IList<int>> nums)
    {
        int elementCount = 0;
        int maximumColumnCount = 0;

        foreach (IList<int> row in nums)
        {
            elementCount += row.Count;
            maximumColumnCount = Math.Max(maximumColumnCount, row.Count);
        }

        List<int>[] diagonals = new List<int>[nums.Count + maximumColumnCount - 1];

        for (int diagonal = 0; diagonal < diagonals.Length; diagonal++)
        {
            diagonals[diagonal] = [];
        }

        // 反向走訪列，使同一 bucket 的插入順序就是題目要求的由下往上順序。
        for (int row = nums.Count - 1; row >= 0; row--)
        {
            for (int column = 0; column < nums[row].Count; column++)
            {
                diagonals[row + column].Add(nums[row][column]);
            }
        }

        int[] result = new int[elementCount];
        int resultIndex = 0;

        // bucket 索引遞增即為對角線順序，且 bucket 內部順序已由反向列走訪固定。
        foreach (List<int> diagonal in diagonals)
        {
            foreach (int value in diagonal)
            {
                result[resultIndex++] = value;
            }
        }

        return result;
    }

    private static IList<IList<int>> CreateInput(params int[][] rows)
    {
        return rows.Select(row => (IList<int>)row).ToList();
    }

    private static string FormatInput(IList<IList<int>> nums)
    {
        return $"[{string.Join(',', nums.Select(FormatValues))}]";
    }

    private static string FormatValues(IEnumerable<int> values)
    {
        return $"[{string.Join(',', values.Select(value => value.ToString(System.Globalization.CultureInfo.InvariantCulture)))}]";
    }

    private static string FormatSpotValue(int? value)
    {
        return value?.ToString(System.Globalization.CultureInfo.InvariantCulture) ?? "<missing>";
    }
}
