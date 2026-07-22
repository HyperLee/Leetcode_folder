namespace leetcode_1743;

internal static class Program
{
    /// <summary>
    /// LeetCode 1743: Restore the Array From Adjacent Pairs / 從相鄰元素對還原陣列。
    /// https://leetcode.com/problems/restore-the-array-from-adjacent-pairs/
    /// https://leetcode.cn/problems/restore-the-array-from-adjacent-pairs/
    /// Given every adjacent pair from an array of distinct integers in arbitrary order,
    /// restore any valid original array. 給定一個相異整數陣列的所有相鄰元素對，且元素對
    /// 的順序與方向皆可能被打亂；請還原任一個符合這些相鄰關係的原始陣列。
    /// </summary>
    private static void Main()
    {
        List<CaseDefinition> definitions =
        [
            new(
                "Official example 1",
                "adjacentPairs=[[2,1],[3,4],[3,2]]",
                [[2, 1], [3, 4], [3, 2]]),
            new(
                "Official example 2",
                "adjacentPairs=[[4,-2],[1,4],[-3,1]]",
                [[4, -2], [1, 4], [-3, 1]]),
            new(
                "Minimum valid input",
                "adjacentPairs=[[100000,-100000]]",
                [[100000, -100000]]),
            new(
                "Scrambled order and directions",
                "adjacentPairs=[[7,5],[1,3],[9,7],[5,3]]",
                [[7, 5], [1, 3], [9, 7], [5, 3]]),
            new(
                "Zero is an internal value",
                "adjacentPairs=[[6,0],[-2,0]]",
                [[6, 0], [-2, 0]]),
            new(
                "Longer regression chain",
                "adjacentPairs=[[8,6],[2,4],[10,8],[6,4],[12,10]]",
                [[8, 6], [2, 4], [10, 8], [6, 4], [12, 10]]),
            CreateMaximumLengthCase()
        ];

        List<CaseResult> cases = definitions.Select(RunCase).ToList();
        cases.Add(RunEmptyResultReportingCase());

        foreach (CaseResult caseResult in cases)
        {
            Console.WriteLine($"Case: {caseResult.Name}");
            Console.WriteLine($"Input: {caseResult.Input}");
            Console.WriteLine($"Expected: {caseResult.Expected}");
            Console.WriteLine($"Actual: {caseResult.Actual}");
            Console.WriteLine($"Result: {(caseResult.Passed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        int passedCount = cases.Count(caseResult => caseResult.Passed);
        Console.WriteLine($"Summary: {passedCount}/{cases.Count} checks passed.");

        if (passedCount != cases.Count)
        {
            Environment.ExitCode = 1;
        }
    }

    private static CaseResult RunCase(CaseDefinition definition)
    {
        int[][] before = definition.AdjacentPairs.Select(pair => pair.ToArray()).ToArray();
        Solution solution = new();
        int[] actual = solution.RestoreArray(definition.AdjacentPairs);
        bool validRestoration = IsValidRestoration(definition.AdjacentPairs, actual);
        bool inputPreserved = AreEqual(before, definition.AdjacentPairs);
        string expected = definition.CompactOutput
            ? "length=100000; valid restoration; input preserved=true"
            : "valid restoration; input preserved=true";
        string actualDescription = definition.CompactOutput
            ? FormatCompactActual(actual, validRestoration, inputPreserved)
            : $"restored={FormatArray(actual)}; valid restoration={FormatBoolean(validRestoration)}; input preserved={FormatBoolean(inputPreserved)}";

        return new CaseResult(
            definition.Name,
            definition.Input,
            expected,
            actualDescription,
            validRestoration && inputPreserved);
    }

    private static CaseResult RunEmptyResultReportingCase()
    {
        const string expected =
            "length=0; endpoints=[]; valid restoration=false; input preserved=true";
        string actual = FormatCompactActual([], validRestoration: false, inputPreserved: true);

        return new CaseResult(
            "Empty result reporting regression",
            "simulated invalid restoration=[]",
            expected,
            actual,
            actual == expected);
    }

    private static string FormatCompactActual(
        int[] actual,
        bool validRestoration,
        bool inputPreserved)
    {
        string endpoints = actual.Length switch
        {
            0 => "[]",
            1 => $"[{actual[0]}]",
            _ => $"[{actual[0]},{actual[^1]}]"
        };

        return $"length={actual.Length}; endpoints={endpoints}; valid restoration={FormatBoolean(validRestoration)}; input preserved={FormatBoolean(inputPreserved)}";
    }

    private static bool IsValidRestoration(int[][] adjacentPairs, int[] restored)
    {
        if (restored.Length != adjacentPairs.Length + 1
            || restored.Distinct().Count() != restored.Length)
        {
            return false;
        }

        HashSet<int> expectedValues = adjacentPairs.SelectMany(pair => pair).ToHashSet();
        if (!expectedValues.SetEquals(restored))
        {
            return false;
        }

        HashSet<(int First, int Second)> expectedEdges = adjacentPairs
            .Select(pair => NormalizeEdge(pair[0], pair[1]))
            .ToHashSet();

        for (int index = 1; index < restored.Length; index++)
        {
            if (!expectedEdges.Contains(NormalizeEdge(restored[index - 1], restored[index])))
            {
                return false;
            }
        }

        return true;
    }

    private static (int First, int Second) NormalizeEdge(int first, int second) =>
        first < second ? (first, second) : (second, first);

    private static bool AreEqual(int[][] expected, int[][] actual) =>
        expected.Length == actual.Length
        && expected.Zip(actual).All(pair => pair.First.SequenceEqual(pair.Second));

    private static CaseDefinition CreateMaximumLengthCase()
    {
        const int length = 100000;
        const int firstValue = -50000;
        int[][] adjacentPairs = new int[length - 1][];

        for (int index = 0; index < adjacentPairs.Length; index++)
        {
            int left = firstValue + index;
            int right = left + 1;
            adjacentPairs[adjacentPairs.Length - 1 - index] =
                index % 2 == 0 ? [left, right] : [right, left];
        }

        return new CaseDefinition(
            "Maximum length spot check",
            "100000 values from -50000 through 49999; reversed pair order and alternating directions",
            adjacentPairs,
            CompactOutput: true);
    }

    private static string FormatArray(IEnumerable<int> values) => $"[{string.Join(',', values)}]";

    private static string FormatBoolean(bool value) => value ? "true" : "false";

    private sealed record CaseDefinition(
        string Name,
        string Input,
        int[][] AdjacentPairs,
        bool CompactOutput = false);

    private sealed record CaseResult(
        string Name,
        string Input,
        string Expected,
        string Actual,
        bool Passed);
}

public sealed class Solution
{
    /// <summary>
    /// 依題目保證有效的相鄰元素對還原原始陣列；先建立雙向鄰接表，再從度數為 1 的端點
    /// 線性走訪。輸入包含某個相異整數陣列的全部相鄰關係，方法不修改輸入，並回傳任一
    /// 個符合所有相鄰關係的排列；整體反向亦為合法答案。
    /// </summary>
    public int[] RestoreArray(int[][] adjacentPairs)
    {
        Dictionary<int, List<int>> neighbors = [];

        foreach (int[] pair in adjacentPairs)
        {
            AddNeighbor(neighbors, pair[0], pair[1]);
            AddNeighbor(neighbors, pair[1], pair[0]);
        }

        int[] restored = new int[adjacentPairs.Length + 1];

        // 原陣列的兩個端點各只有一個鄰居；任選其一都只會改變輸出的整體方向。
        restored[0] = neighbors.First(entry => entry.Value.Count == 1).Key;
        restored[1] = neighbors[restored[0]][0];

        for (int index = 2; index < restored.Length; index++)
        {
            List<int> candidates = neighbors[restored[index - 1]];

            // 內部節點有兩個鄰居；排除剛走過的節點後，另一個就是下一個值。
            restored[index] = candidates[0] == restored[index - 2]
                ? candidates[1]
                : candidates[0];
        }

        return restored;
    }

    /// <summary>
    /// 將一條有向鄰接紀錄加入鄰接表；輸入的節點與鄰居皆來自有效 pair。若節點首次
    /// 出現便建立清單，完成後該節點的清單會多包含一個指定鄰居。
    /// </summary>
    private static void AddNeighbor(
        Dictionary<int, List<int>> neighbors,
        int node,
        int neighbor)
    {
        if (!neighbors.TryGetValue(node, out List<int>? adjacentNodes))
        {
            adjacentNodes = [];
            neighbors[node] = adjacentNodes;
        }

        adjacentNodes.Add(neighbor);
    }
}
