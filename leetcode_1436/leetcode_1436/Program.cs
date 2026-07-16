namespace leetcode_1436;

internal class Program
{
    /// <summary>
    /// 1436. Destination City
    /// 1436. 旅行終點站
    /// https://leetcode.com/problems/destination-city/
    /// https://leetcode.cn/problems/destination-city/
    /// Given directed travel paths that form one acyclic line, return the unique city with no outgoing path.
    /// 給定形成一條無環路線的有向旅行路徑，回傳唯一沒有外出路徑的終點城市。
    /// </summary>
    /// <param name="args">主控台啟動參數；本驗證器不使用。</param>
    private static void Main(string[] args)
    {
        IList<IList<string>> maximumPaths = [];
        for (int index = 0; index < 100; index++)
        {
            maximumPaths.Add([CreateCityName(index), CreateCityName(index + 1)]);
        }

        TestCase[] testCases =
        [
            new("Official example 1", "[[London -> New York], [New York -> Lima], [Lima -> Sao Paulo]]", [["London", "New York"], ["New York", "Lima"], ["Lima", "Sao Paulo"]], "Sao Paulo"),
            new("Official example 2", "[[B -> C], [D -> B], [C -> A]]", [["B", "C"], ["D", "B"], ["C", "A"]], "A"),
            new("Official example 3 / minimum input", "[[A -> Z]]", [["A", "Z"]], "Z"),
            new("Shuffled path order", "[[Gamma -> Delta], [Alpha -> Beta], [Beta -> Gamma]]", [["Gamma", "Delta"], ["Alpha", "Beta"], ["Beta", "Gamma"]], "Delta"),
            new("Early destination candidate becomes a source", "[[A -> B], [C -> D], [B -> C]]", [["A", "B"], ["C", "D"], ["B", "C"]], "D"),
            new("City names containing spaces", "[[New York -> Rio City], [Rio City -> Cape Town]]", [["New York", "Rio City"], ["Rio City", "Cape Town"]], "Cape Town"),
            new("Case-sensitive city names", "[[A -> a]]", [["A", "a"]], "a"),
            new("Maximum 100-path chain", "[100-path chain CityA -> ... -> CityCW]", maximumPaths, "CityCW")
        ];

        int passed = 0;
        foreach (TestCase testCase in testCases)
        {
            IList<IList<string>> originalPaths = ClonePaths(testCase.Paths);
            string actual = DestCity(testCase.Paths);
            bool isPassed = actual == testCase.Expected && PathsEqual(testCase.Paths, originalPaths);
            if (isPassed)
            {
                passed++;
            }

            Console.WriteLine($"Case: {testCase.Name}");
            Console.WriteLine($"Input: {testCase.Input}");
            Console.WriteLine($"Expected: {testCase.Expected}");
            Console.WriteLine($"Actual: {actual}");
            Console.WriteLine($"Result: {(isPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        Console.WriteLine($"Summary: {passed}/{testCases.Length} checks passed.");
        if (passed != testCases.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    private static string CreateCityName(int index)
    {
        string suffix = string.Empty;
        do
        {
            suffix = (char)('A' + index % 26) + suffix;
            index = index / 26 - 1;
        }
        while (index >= 0);

        return $"City{suffix}";
    }

    private static IList<IList<string>> ClonePaths(IList<IList<string>> paths)
    {
        return paths.Select(path => (IList<string>)[.. path]).ToList();
    }

    private static bool PathsEqual(IList<IList<string>> left, IList<IList<string>> right)
    {
        return left.Count == right.Count
            && left.Zip(right).All(pair => pair.First.SequenceEqual(pair.Second));
    }

    /// <summary>
    /// 在有效路徑形成單一無環旅行線的前提下，先收集所有出發城市，再找出唯一未出現在
    /// 出發城市集合中的抵達城市，並將該城市作為旅行終點回傳。
    /// </summary>
    /// <param name="paths">每個元素皆為「出發城市、抵達城市」的有效二元素路徑。</param>
    /// <returns>唯一沒有任何外出路徑的終點城市。</returns>
    public static string DestCity(IList<IList<string>> paths)
    {
        HashSet<string> departureCities = [];
        foreach (IList<string> path in paths)
        {
            departureCities.Add(path[0]);
        }

        foreach (IList<string> path in paths)
        {
            // 終點是唯一不曾作為任何路徑起點的抵達城市。
            if (!departureCities.Contains(path[1]))
            {
                return path[1];
            }
        }

        return string.Empty;
    }

    private sealed record TestCase(
        string Name,
        string Input,
        IList<IList<string>> Paths,
        string Expected);
}
