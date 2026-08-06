namespace leetcode_2225;

class Program
{
    /// <summary>
    /// 2225. Find Players With Zero or One Losses
    /// https://leetcode.com/problems/find-players-with-zero-or-one-losses/description/?envType=daily-question&envId=2024-01-15
    /// 2225. 找出输掉零场或一场比赛的玩家
    /// https://leetcode.cn/problems/find-players-with-zero-or-one-losses/description/
    /// 
    /// 不規則陣列
    /// https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/builtin-types/arrays#jagged-arrays
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        (string Name, int[][] Matches, int[][] Expected)[] testCases =
        [
            (
                "題目主要範例",
                [
                    [1, 3],
                    [2, 3],
                    [3, 6],
                    [5, 6],
                    [5, 7],
                    [4, 5],
                    [4, 8],
                    [4, 9],
                    [10, 4],
                    [10, 9]
                ],
                [[1, 2, 10], [4, 5, 7, 8]]
            ),
            (
                "沒有只輸一次的玩家",
                [[2, 3], [1, 3], [5, 4], [6, 4]],
                [[1, 2, 5, 6], []]
            ),
            (
                "單場比賽邊界",
                [[1, 2]],
                [[1], [2]]
            ),
            (
                "玩家同時曾獲勝且輸多次",
                [[1, 2], [2, 3], [4, 2]],
                [[1, 4], [3]]
            )
        ];

        (string Name, Func<int[][], IList<IList<int>>> Solve)[] solutions =
        [
            ("HashSet 狀態分類", FindWinners),
            ("Dictionary 敗場計數", FindWinners2)
        ];

        int passedChecks = 0;
        int totalChecks = testCases.Length * solutions.Length;

        foreach ((string caseName, int[][] matches, int[][] expected) in testCases)
        {
            foreach ((string solutionName, Func<int[][], IList<IList<int>>> solve) in solutions)
            {
                IList<IList<int>> actual = solve(matches);
                bool passed = ResultsEqual(expected, actual);

                Console.WriteLine($"Case: {caseName}");
                Console.WriteLine($"Solution: {solutionName}");
                Console.WriteLine($"Matches: {FormatResult(matches)}");
                Console.WriteLine($"Expected: {FormatResult(expected)}");
                Console.WriteLine($"Actual: {FormatResult(actual)}");
                Console.WriteLine($"Status: {(passed ? "PASS" : "FAIL")}");
                Console.WriteLine();

                if (passed)
                {
                    passedChecks++;
                }
            }
        }

        Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed");
        Environment.ExitCode = passedChecks == totalChecks ? 0 : 1;
    }

    /// <summary>
    /// 使用三個 <see cref="HashSet{T}"/> 維護從未輸過、只輸一次與輸超過一次的玩家。
    /// 每讀取一場比賽便更新敗方所屬集合，最後將前兩類玩家遞增排序後回傳。
    /// </summary>
    /// <param name="matches">
    /// 至少包含一場比賽的合法記錄；每筆資料皆為 <c>[winner, loser]</c>，兩個玩家編號不同且比賽不重複。
    /// </param>
    /// <returns>
    /// 兩個遞增排序的清單：索引 0 是從未輸過的玩家，索引 1 是恰好輸過一次的玩家。
    /// </returns>
    public static IList<IList<int>> FindWinners(int[][] matches)
    {
        HashSet<int> noLosers = new HashSet<int>();
        HashSet<int> oneLosers = new HashSet<int>();
        HashSet<int> manyLosers = new HashSet<int>();

        foreach (int[] match in matches)
        {
            int winner = match[0];
            int loser = match[1];

            // 只有從未出現在任何敗方集合的贏家，才能歸入零敗集合。
            if (!oneLosers.Contains(winner) && !manyLosers.Contains(winner))
            {
                noLosers.Add(winner);
            }

            // 敗方依先前的敗場狀態，由零敗移到一敗，或由一敗移到多敗。
            if (noLosers.Contains(loser))
            {
                noLosers.Remove(loser);
                oneLosers.Add(loser);
            }
            else if (oneLosers.Contains(loser))
            {
                oneLosers.Remove(loser);
                manyLosers.Add(loser);
            }
            else if (!manyLosers.Contains(loser))
            {
                oneLosers.Add(loser);
            }
        }

        return
        [
            noLosers.OrderBy(player => player).ToList(),
            oneLosers.OrderBy(player => player).ToList()
        ];
    }

    /// <summary>
    /// 使用 <see cref="Dictionary{TKey,TValue}"/> 記錄每位參賽玩家的敗場數。
    /// 贏家首次出現時登記為零敗，敗方則累加敗場，最後篩選零敗與一敗玩家並遞增排序。
    /// </summary>
    /// <param name="matches">
    /// 至少包含一場比賽的合法記錄；每筆資料皆為 <c>[winner, loser]</c>，兩個玩家編號不同且比賽不重複。
    /// </param>
    /// <returns>
    /// 兩個遞增排序的清單：索引 0 是從未輸過的玩家，索引 1 是恰好輸過一次的玩家。
    /// </returns>
    public static IList<IList<int>> FindWinners2(int[][] matches)
    {
        Dictionary<int, int> playerStats = new Dictionary<int, int>();

        foreach (int[] match in matches)
        {
            int winner = match[0];
            int loser = match[1];

            // 贏家也必須先登記，否則最後無法辨認「參賽但從未輸過」。
            if (!playerStats.ContainsKey(winner))
            {
                playerStats[winner] = 0;
            }

            playerStats[loser] = playerStats.GetValueOrDefault(loser) + 1;
        }

        List<int> zeroLoss = new List<int>();
        List<int> oneLoss = new List<int>();

        foreach (var player in playerStats)
        {
            if (player.Value == 0)
            {
                zeroLoss.Add(player.Key);
            }
            else if (player.Value == 1)
            {
                oneLoss.Add(player.Key);
            }
        }

        // Dictionary 的列舉順序不是題目要求，因此兩組結果都需明確遞增排序。
        zeroLoss.Sort();
        oneLoss.Sort();

        return new List<IList<int>>
        {
            zeroLoss,
            oneLoss
        };
    }

    /// <summary>
    /// 比較預期與實際的二維結果，確認兩者的群組數量、元素數量、順序與值完全一致。
    /// </summary>
    /// <param name="expected">已按題目規格排序的預期二維結果。</param>
    /// <param name="actual">解法實際回傳的二維結果。</param>
    /// <returns>兩個結果完全一致時回傳 <see langword="true"/>；否則回傳 <see langword="false"/>。</returns>
    private static bool ResultsEqual(IEnumerable<IEnumerable<int>> expected, IEnumerable<IEnumerable<int>> actual)
    {
        int[][] expectedGroups = expected.Select(group => group.ToArray()).ToArray();
        int[][] actualGroups = actual.Select(group => group.ToArray()).ToArray();

        return expectedGroups.Length == actualGroups.Length
            && expectedGroups
                .Zip(actualGroups, (expectedGroup, actualGroup) => expectedGroup.SequenceEqual(actualGroup))
                .All(equal => equal);
    }

    /// <summary>
    /// 將比賽記錄或二維答案格式化為易於閱讀且可直接比對的巢狀陣列文字。
    /// </summary>
    /// <param name="groups">要顯示的二維整數集合；每個內層集合依目前順序輸出。</param>
    /// <returns>格式為 <c>[[a, b], [c]]</c> 的字串；空群組會顯示為 <c>[]</c>。</returns>
    private static string FormatResult(IEnumerable<IEnumerable<int>> groups)
    {
        return $"[{string.Join(", ", groups.Select(group => $"[{string.Join(", ", group)}]"))}]";
    }
}