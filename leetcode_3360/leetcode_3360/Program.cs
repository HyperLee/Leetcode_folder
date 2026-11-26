namespace leetcode_3360;

class Program
{
    /// <summary>
    /// 3360. Stone Removal Game
    /// https://leetcode.com/problems/stone-removal-game/description/
    /// 3360. 移除石头游戏
    /// https://leetcode.cn/problems/stone-removal-game/description/
    ///
    /// 
    /// Alice and Bob are playing a game where they take turns removing stones from a pile, with Alice going first.
    /// Alice starts by removing exactly 10 stones on her first turn.
    /// For each subsequent turn, each player removes exactly 1 fewer stone than the previous opponent.
    /// The player who cannot make a move loses the game.
    /// Given a positive integer , return true if Alice wins the game and false otherwise.
    ///
    /// 繁體中文
    /// Alice 和 Bob 輪流從一堆石頭中取走石頭，由 Alice 先手。
    /// Alice 在第一回合必須取走 恰好 10 顆石頭。
    /// 之後每個回合，玩家取走的石頭數量必須 恰好 比對手上一回合取走的數量少 1 顆。
    /// 無法行動的玩家輸掉遊戲。
    /// 給定一個正整數 ，如果 Alice 贏得遊戲，回傳 true，否則回傳 false。
    /// </summary>
    /// <remarks>
    /// 以固定範例與邊界資料驗證四種解法，並比對所有合法輸入的結果。
    /// </remarks>
    /// <param name="args">命令列引數，範例執行時不需要提供。</param>
    /// <returns>全部案例通過時回傳 0，任一案例失敗時回傳 1。</returns>
    private static int Main(string[] args)
    {
        (int Stones, bool Expected)[] testCases =
        [
            (1, false),
            (9, false),
            (10, true),
            (12, true),
            (18, true),
            (19, false),
            (26, false),
            (27, true),
            (33, true),
            (34, false),
            (39, false),
            (40, true),
            (44, true),
            (45, false),
            (48, false),
            (49, true),
            (50, true)
        ];

        Program solution = new();
        int passedCases = 0;

        foreach ((int stones, bool expected) in testCases)
        {
            bool simulationResult = solution.CanAliceWin(stones);
            bool formulaResult = solution.CanAliceWin2(stones);
            bool rangeResult = solution.CanAliceWinByRange(stones);
            bool binarySearchResult = solution.CanAliceWinByBinarySearch(stones);
            bool passed = simulationResult == expected
                && formulaResult == expected
                && rangeResult == expected
                && binarySearchResult == expected;

            if (passed)
            {
                passedCases++;
            }

            string status = passed ? "PASS" : "FAIL";
            Console.WriteLine(
                $"n={stones} Expected={expected} Simulation={simulationResult} Formula={formulaResult} "
                + $"Range={rangeResult} BinarySearch={binarySearchResult} {status}");
        }

        Console.WriteLine($"Passed {passedCases}/{testCases.Length} cases.");
        const int maxStones = 50;
        int matchingInputs = 0;

        for (int stones = 1; stones <= maxStones; stones++)
        {
            bool simulationResult = solution.CanAliceWin(stones);
            bool formulaResult = solution.CanAliceWin2(stones);
            bool rangeResult = solution.CanAliceWinByRange(stones);
            bool binarySearchResult = solution.CanAliceWinByBinarySearch(stones);

            if (formulaResult == simulationResult
                && rangeResult == simulationResult
                && binarySearchResult == simulationResult)
            {
                matchingInputs++;
            }
            else
            {
                Console.WriteLine(
                    $"Mismatch at n={stones}: Simulation={simulationResult} Formula={formulaResult} "
                    + $"Range={rangeResult} BinarySearch={binarySearchResult}");
            }
        }

        Console.WriteLine($"All four methods agree for {matchingInputs}/{maxStones} valid inputs.");
        return passedCases == testCases.Length && matchingInputs == maxStones ? 0 : 1;
    }

    /// <summary>
    /// 從第一回合的 10 顆石頭開始，逐回合模擬固定的取石數量，直到剩餘石頭不足。
    /// 輸入 1 到 50 顆石頭，完成的回合數為奇數時 Alice 獲勝。
    /// </summary>
    /// <param name="n">初始石頭數，範圍為 1 到 50。</param>
    /// <returns>Alice 獲勝時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
    public bool CanAliceWin(int n)
    {
        int stonesToRemove = 10;
        int completedTurns = 0;

        // 剩餘石頭恰好等於本回合所需數量時，仍可完成這一回合。
        while (n >= stonesToRemove)
        {
            n -= stonesToRemove;
            stonesToRemove--;
            completedTurns++;
        }

        // Alice 完成奇數回合。若完成回合總數為奇數，下一個無法取石的是 Bob。
        return completedTurns % 2 == 1;
    }

    /// <summary>
    /// 將累計取石數寫成等差級數，直接算出最多能完成的回合數。
    /// 輸入 1 到 50 顆石頭，完成的回合數為奇數時 Alice 獲勝。
    /// </summary>
    /// <param name="n">初始石頭數，範圍為 1 到 50。</param>
    /// <returns>Alice 獲勝時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
    public bool CanAliceWin2(int n)
    {
        // 累計取石數為 k(21 - k) / 2，在可行回合範圍內遞增。
        // 較小根給出回合數上限。將非負根轉為整數，得到實際可完成的最多回合數。
        int completedTurns = (int)((21 - Math.Sqrt(441 - 8 * n)) / 2);
        return completedTurns % 2 == 1;
    }

    /// <summary>
    /// 比對累計取石門檻所劃出的四個區間，判斷 Alice 是否完成最後一回合。
    /// </summary>
    /// <param name="n">初始石頭數，範圍為 1 到 50。</param>
    /// <returns>Alice 獲勝時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
    public bool CanAliceWinByRange(int n)
    {
        return n is (>= 10 and < 19)
                 or (>= 27 and < 34)
                 or (>= 40 and < 45)
                 or (>= 49 and <= 50);
    }

    /// <summary>
    /// 以二分搜尋找出最多可完成的回合數，依回合數的奇偶判斷勝負。
    /// </summary>
    /// <param name="n">初始石頭數，範圍為 1 到 50。</param>
    /// <returns>Alice 獲勝時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
    public bool CanAliceWinByBinarySearch(int n)
    {
        int low = 0;
        int high = 7; // 第 8 回合累計需要 52 顆，超過題目上限。

        while (low < high)
        {
            // 取靠右的中點，讓條件成立時下界仍會向右移動。
            int middle = low + (high - low + 1) / 2;
            int requiredStones = middle * (21 - middle) / 2;

            if (requiredStones <= n)
            {
                low = middle;
            }
            else
            {
                high = middle - 1;
            }
        }

        return low % 2 == 1;
    }
}
