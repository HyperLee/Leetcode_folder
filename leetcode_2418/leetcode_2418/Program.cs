namespace leetcode_2418;

internal static class Program
{
    /// <summary>
    /// LeetCode 2418. Sort the People.
    /// LeetCode 2418. 按身高排序。
    /// English: Given equal-length arrays of names and distinct positive heights, where
    /// names[i] and heights[i] identify the same person, return the names ordered by height
    /// from tallest to shortest.
    /// 中文：給定等長的姓名陣列與互不相同的正整數身高陣列，其中 names[i] 與
    /// heights[i] 代表同一個人，依身高由高至低回傳姓名陣列。
    /// English: https://leetcode.com/problems/sort-the-people/
    /// 中文：https://leetcode.cn/problems/sort-the-people/
    /// </summary>
    private static void Main()
    {
        string[] maximumNames = Enumerable.Range(0, 1_000)
            .Select(CreateAlphabeticName)
            .ToArray();
        int[] maximumHeights = Enumerable.Range(1, 1_000).ToArray();
        string[] maximumExpected = maximumNames.Reverse().ToArray();

        TestCase[] testCases =
        [
            new(
                "Official example 1",
                "names=[Mary,John,Emma], heights=[180,165,170]",
                ["Mary", "John", "Emma"],
                [180, 165, 170],
                ["Mary", "Emma", "John"]),
            new(
                "Official example 2 with duplicate names",
                "names=[Alice,Bob,Bob], heights=[155,185,150]",
                ["Alice", "Bob", "Bob"],
                [155, 185, 150],
                ["Bob", "Alice", "Bob"]),
            new(
                "Minimum input",
                "names=[A], heights=[1]",
                ["A"],
                [1],
                ["A"]),
            new(
                "Strictly increasing heights",
                "names=[A,B,C,D], heights=[1,2,3,4]",
                ["A", "B", "C", "D"],
                [1, 2, 3, 4],
                ["D", "C", "B", "A"]),
            new(
                "Name order differs from height order",
                "names=[Zoe,Amy,Mia,Leo], heights=[40,10,30,20]",
                ["Zoe", "Amy", "Mia", "Leo"],
                [40, 10, 30, 20],
                ["Zoe", "Mia", "Leo", "Amy"]),
            new(
                "Height and name length boundaries",
                "names=[aaaaaaaaaaaaaaaaaaaa,Top,Middle], heights=[1,100000,50000]",
                ["aaaaaaaaaaaaaaaaaaaa", "Top", "Middle"],
                [1, 100_000, 50_000],
                ["Top", "Middle", "aaaaaaaaaaaaaaaaaaaa"]),
            new(
                "Maximum length",
                "names=[PersonAAA..PersonBML], heights=[1..1000] (length 1000)",
                maximumNames,
                maximumHeights,
                maximumExpected)
        ];

        (string Name, Func<string[], int[], string[]> Solve)[] solutions =
        [
            ("SortPeople", SortPeople),
            ("SortPeople2", SortPeople2)
        ];

        CaseResult[] results =
        [
            .. testCases.SelectMany(testCase =>
                solutions.Select(solution =>
                    RunCase(testCase, solution.Name, solution.Solve)))
        ];

        foreach (CaseResult result in results)
        {
            Console.WriteLine($"Case: {result.CaseName} [{result.SolutionName}]");
            Console.WriteLine($"Input: {result.Input}");
            Console.WriteLine(PrintSequenceCheck("result", result.Expected, result.Actual));
            Console.WriteLine(PrintCheck("names preserved", true, result.NamesPreserved));
            Console.WriteLine(PrintCheck("heights preserved", true, result.HeightsPreserved));
            Console.WriteLine();
        }

        int passedCount = results.Sum(result => result.PassedCheckCount);
        const int totalCheckCount = 42;
        Console.WriteLine($"Summary: {passedCount}/{totalCheckCount} checks passed.");

        if (passedCount != totalCheckCount)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 將題目限制內互不相同的身高映射至對應姓名，再依身高由高至低列舉，
    /// 回傳排序後的新姓名陣列。方法不修改 <paramref name="names"/>、
    /// <paramref name="heights"/> 或主控台狀態。時間複雜度為 O(n log n)，
    /// 輔助空間與回傳結果空間皆為 O(n)。
    /// </summary>
    /// <param name="names">
    /// 長度 1 至 1000 的姓名陣列；每個姓名長度為 1 至 20，且只包含英文字母。
    /// </param>
    /// <param name="heights">
    /// 與 <paramref name="names"/> 等長、元素介於 1 至 100000 且互不相同的身高陣列。
    /// </param>
    /// <returns>依對應身高由高至低排列的新姓名陣列。</returns>
    public static string[] SortPeople(string[] names, int[] heights)
    {
        Dictionary<int, string> peopleByHeight = new(names.Length);
        for (int index = 0; index < names.Length; index++)
        {
            peopleByHeight.Add(heights[index], names[index]);
        }

        // 題目保證身高唯一，因此降冪列舉每個 key 就能決定唯一的姓名順序。
        return peopleByHeight
            .OrderByDescending(person => person.Key)
            .Select(person => person.Value)
            .ToArray();
    }

    /// <summary>
    /// 建立原陣列索引並依其對應身高由高至低排序，再以排序後索引讀取姓名，
    /// 回傳新的姓名陣列。方法不修改 <paramref name="names"/>、
    /// <paramref name="heights"/> 或主控台狀態。時間複雜度為 O(n log n)，
    /// 輔助空間與回傳結果空間皆為 O(n)。
    /// </summary>
    /// <param name="names">
    /// 長度 1 至 1000 的姓名陣列；每個姓名長度為 1 至 20，且只包含英文字母。
    /// </param>
    /// <param name="heights">
    /// 與 <paramref name="names"/> 等長、元素介於 1 至 100000 且互不相同的身高陣列。
    /// </param>
    /// <returns>依對應身高由高至低排列的新姓名陣列。</returns>
    public static string[] SortPeople2(string[] names, int[] heights)
    {
        int[] indices = Enumerable.Range(0, names.Length).ToArray();

        // 索引本身承載姓名與身高的配對關係，排序索引即可保持兩個輸入陣列不變。
        Array.Sort(indices, (left, right) => heights[right].CompareTo(heights[left]));

        string[] result = new string[names.Length];
        for (int index = 0; index < indices.Length; index++)
        {
            result[index] = names[indices[index]];
        }

        return result;
    }

    private static CaseResult RunCase(
        TestCase testCase,
        string solutionName,
        Func<string[], int[], string[]> solve)
    {
        string[] names = [.. testCase.Names];
        int[] heights = [.. testCase.Heights];
        string[] originalNames = [.. names];
        int[] originalHeights = [.. heights];
        string[] actual = solve(names, heights);

        return new CaseResult(
            testCase.Name,
            solutionName,
            testCase.Input,
            testCase.Expected,
            actual,
            names.SequenceEqual(originalNames),
            heights.SequenceEqual(originalHeights));
    }

    private static string CreateAlphabeticName(int index)
    {
        char[] suffix = new char[3];
        int remaining = index;

        for (int position = suffix.Length - 1; position >= 0; position--)
        {
            suffix[position] = (char)('A' + (remaining % 26));
            remaining /= 26;
        }

        return $"Person{new string(suffix)}";
    }

    private static string PrintSequenceCheck<T>(string name, T[] expected, T[] actual)
    {
        string status = expected.SequenceEqual(actual) ? "PASS" : "FAIL";
        return $"{status} {name} | Expected: {FormatArray(expected)} | Actual: {FormatArray(actual)}";
    }

    private static string PrintCheck<T>(string name, T expected, T actual)
    {
        string status = EqualityComparer<T>.Default.Equals(expected, actual) ? "PASS" : "FAIL";
        return $"{status} {name} | Expected: {expected} | Actual: {actual}";
    }

    private static string FormatArray<T>(T[] values)
    {
        if (values.Length <= 12)
        {
            return $"[{string.Join(',', values)}]";
        }

        return $"[{string.Join(',', values.Take(3))},...,{string.Join(',', values.TakeLast(3))}] " +
            $"(length {values.Length})";
    }

    private sealed record TestCase(
        string Name,
        string Input,
        string[] Names,
        int[] Heights,
        string[] Expected);

    private sealed record CaseResult(
        string CaseName,
        string SolutionName,
        string Input,
        string[] Expected,
        string[] Actual,
        bool NamesPreserved,
        bool HeightsPreserved)
    {
        public int PassedCheckCount =>
            (Expected.SequenceEqual(Actual) ? 1 : 0) +
            (NamesPreserved ? 1 : 0) +
            (HeightsPreserved ? 1 : 0);
    }
}