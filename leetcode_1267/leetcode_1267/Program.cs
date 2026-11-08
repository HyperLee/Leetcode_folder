namespace leetcode_1267;

class Program
{
    /// <summary>
    /// 1267. Count Servers that Communicate
    /// https://leetcode.com/problems/count-servers-that-communicate/description/
    /// 1267. 统计参与通信的服务器
    /// https://leetcode.cn/problems/count-servers-that-communicate/description/
    ///
    /// English:
    /// You are given a map of a server center, represented as a `m * n` integer matrix grid,
    /// where 1 means that on that cell there is a server and 0 means that it is no server.
    /// Two servers are said to communicate if they are on the same row or on the same column.
    ///
    /// Return the number of servers that communicate with any other server.
    ///
    /// 繁體中文：
    /// 給定一張伺服器中心的地圖，以 m × n 的整數矩陣 grid 表示，
    /// 其中 1 表示該位置有一台伺服器，0 表示該位置沒有伺服器。
    /// 如果兩台伺服器位於同一列或同一欄，就稱這兩台伺服器可以互相通訊。
    ///
    /// 請回傳能與至少一台其他伺服器通訊的伺服器數量。
    /// </summary>
    /// <param name="args">命令列引數。</param>
    static void Main(string[] args)
    {
        // 無需輸入：以固定預期值檢查兩種解法及輸入是否保持不變。
        (string Name, int[][] Grid, int Expected)[] testCases =
        [
            ("Example1_IsolatedServers", [[1, 0], [0, 1]], 0),
            ("Example2_SharedRowAndColumn", [[1, 0], [1, 1]], 3),
            ("Example3_MixedServers", [[1, 1, 0, 0], [0, 0, 1, 0], [0, 0, 1, 0], [0, 0, 0, 1]], 4),
            ("SingleEmptyCell", [[0]], 0),
            ("SingleServer", [[1]], 0),
            ("AllEmptyCells", [[0, 0, 0], [0, 0, 0]], 0),
            ("SingleRow_SeparatedServers", [[1, 0, 1]], 2),
            ("SingleColumn_SeparatedServers", [[1], [0], [1]], 2),
            ("SingleRow_OneServer", [[0, 1, 0]], 0),
            ("SingleColumn_OneServer", [[0], [1], [0]], 0),
            ("AllServers_RectangularGrid", [[1, 1, 1], [1, 1, 1]], 6),
            ("Cross_CountEachServerOnce", [[0, 1, 0], [1, 1, 1], [0, 1, 0]], 5),
            ("MaximumGrid_AllEmpty", CreateFilledGrid(250, 250, 0), 0),
            ("MaximumGrid_AllServers", CreateFilledGrid(250, 250, 1), 62500)
        ];

        Program solution = new Program();
        (string Name, Func<int[][], int> Solve)[] solutions =
        [
            (nameof(CountServers), solution.CountServers),
            (nameof(CountServersBruteForce), solution.CountServersBruteForce)
        ];

        int passed = 0;
        foreach ((string name, int[][] grid, int expected) in testCases)
        {
            foreach ((string solutionName, Func<int[][], int> solve) in solutions)
            {
                if (RunTest(name, grid, expected, solutionName, solve))
                {
                    passed++;
                }
            }
        }

        int total = testCases.Length * solutions.Length;
        Console.WriteLine($"結果檢查：{passed}/{total} 通過（{testCases.Length} 個案例、{solutions.Length} 種解法）");
        Console.WriteLine("每次檢查亦驗證輸入未被修改；任一結果或輸入檢查失敗皆回傳非零結束碼。");
        Environment.ExitCode = passed == total ? 0 : 1;
    }

    /// <summary>
    /// 以兩次遍歷計算能與其他伺服器通訊的伺服器數量。
    /// 先統計每列、每欄的數量，再判斷目前伺服器所在列或欄是否另有伺服器。
    /// 輸入須符合題目限制，且不修改輸入；時間 O(mn)，額外空間 O(m+n)。
    /// </summary>
    /// <param name="grid">非空矩形的 0/1 矩陣，列數與欄數均介於 1 到 250。</param>
    /// <returns>能與至少一台其他伺服器通訊的伺服器數量。</returns>
    public int CountServers(int[][] grid)
    {
        int m = grid.Length;
        int n = grid[0].Length;

        int[] rowCount = new int[m];
        int[] colCount = new int[n];
        // 第一輪預先統計，避免每台伺服器重複搜尋相同的列與欄。
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (grid[i][j] == 1)
                {
                    rowCount[i]++;
                    colCount[j]++;
                }
            }
        }

        // 計數包含自己，必須 > 1 才有其他伺服器；列或欄任一成立即可。
        // 每格只判斷一次，即使列與欄都成立，也只計入一台。
        int count = 0;
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (grid[i][j] == 1 && (rowCount[i] > 1 || colCount[j] > 1))
                {
                    count++;
                }
            }
        }
        return count;
    }
    /// <summary>
    /// 逐台搜尋同列或同欄的其他伺服器，計算至少能與另一台通訊的數量。
    /// 輸入須符合題目限制；找到任一對象即可停止，且不修改輸入。
    /// 時間上界 O(mn(m+n))，額外空間 O(1)。
    /// </summary>
    /// <param name="grid">非空矩形的 0/1 矩陣，列數與欄數均介於 1 到 250。</param>
    /// <returns>能與至少一台其他伺服器通訊的伺服器數量。</returns>
    public int CountServersBruteForce(int[][] grid)
    {
        int m = grid.Length;
        int n = grid[0].Length;
        int count = 0;
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (grid[i][j] == 0)
                {
                    continue;
                }

                bool canCommunicate = false;
                for (int column = 0; column < n; column++)
                {
                    // 排除自身；只需知道有沒有對象，找到一台便停止。
                    if (column != j && grid[i][column] == 1)
                    {
                        canCommunicate = true;
                        break;
                    }
                }

                // 同列已有對象就不必再查同欄，亦避免將同一台重複計數。
                if (!canCommunicate)
                {
                    for (int row = 0; row < m; row++)
                    {
                        if (row != i && grid[row][j] == 1)
                        {
                            canCommunicate = true;
                            break;
                        }
                    }
                }

                if (canCommunicate)
                {
                    count++;
                }
            }
        }
        return count;
    }

    /// <summary>
    /// 執行一項固定預期值檢查，並以執行前的副本確認輸入內容未變。
    /// 每種解法使用獨立副本，避免彼此影響；輸出檢查明細並回傳是否通過。
    /// </summary>
    /// <param name="name">測試案例名稱。</param>
    /// <param name="grid">符合題目限制的原始測試矩陣。</param>
    /// <param name="expected">明確指定的預期伺服器數量。</param>
    /// <param name="solutionName">受測解法名稱。</param>
    /// <param name="solve">接收矩陣並回傳伺服器數量的解法。</param>
    /// <returns>結果符合預期且輸入未修改時為 true；否則為 false。</returns>
    private static bool RunTest(string name, int[][] grid, int expected,
        string solutionName, Func<int[][], int> solve)
    {
        int[][] input = grid.Select(row => row.ToArray()).ToArray();
        int actual = solve(input);
        bool unchanged = input.Length == grid.Length
            && input.Select((row, index) => row.SequenceEqual(grid[index])).All(equal => equal);
        bool passed = actual == expected && unchanged;
        Console.WriteLine($"[{(passed ? "PASS" : "FAIL")}] {name} / {solutionName}: 預期={expected}, 實際={actual}, 輸入未變={unchanged}");
        return passed;
    }

    /// <summary>
    /// 建立每列各自配置、填滿指定數值的矩形矩陣，供最大尺寸測試使用。
    /// </summary>
    /// <param name="rows">正整數列數。</param>
    /// <param name="columns">正整數欄數。</param>
    /// <param name="value">各格填入的 0 或 1。</param>
    /// <returns>指定大小且各列不共用陣列的測試矩陣。</returns>
    private static int[][] CreateFilledGrid(int rows, int columns, int value)
    {
        int[][] grid = new int[rows][];
        for (int i = 0; i < rows; i++)
        {
            grid[i] = new int[columns];
            Array.Fill(grid[i], value);
        }
        return grid;
    }
}