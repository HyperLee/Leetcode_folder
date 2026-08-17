namespace leetcode_2029;

class Program
{
    /// <summary>
    /// 2029. Stone Game IX
    /// https://leetcode.com/problems/stone-game-ix/description/
    /// 2029. 石子游戏 IX
    /// https://leetcode.cn/problems/stone-game-ix/description/
    ///
    /// English:
    /// Alice and Bob continue their games with stones. There is a row of n stones, and each stone has an associated value.
    /// You are given an integer array stones, where stones[i] is the value of the ith stone.
    ///
    /// Alice and Bob take turns, with Alice starting first. On each turn, the player may remove any stone from stones.
    /// The player who removes a stone loses if the sum of the values of all removed stones is divisible by 3.
    /// Bob will win automatically if there are no remaining stones (even if it is Alice's turn).
    ///
    /// Assuming both players play optimally, return true if Alice wins and false if Bob wins.
    ///
    /// 繁體中文：
    /// Alice 和 Bob 繼續進行石子遊戲。有一排 n 顆石子，每顆石子都有一個對應的數值。
    /// 給你一個整數陣列 stones，其中 stones[i] 是第 i 顆石子的數值。
    ///
    /// Alice 和 Bob 輪流行動，由 Alice 先手。每一回合，玩家可以從 stones 中移除任意一顆石子。
    /// 如果所有已移除石子的數值總和可以被 3 整除，移除該石子的玩家就輸了。
    /// 如果沒有剩餘石子，Bob 會自動獲勝（即使此時輪到 Alice）。
    ///
    /// 假設兩位玩家都採取最佳策略，如果 Alice 獲勝則回傳 true；如果 Bob 獲勝則回傳 false。
    /// </summary>
    /// <remarks>
    /// 執行七組固定案例，逐一比較三種解法的結果，並在任一檢查失敗時設定非零結束碼。
    /// </remarks>
    /// <param name="args">命令列參數；本程式目前未使用。</param>
    static void Main(string[] args)
    {
        Program solver = new();
        (string Name, int[] Stones, bool Expected)[] testCases =
        [
            ("官方範例 1：Alice 使 Bob 取到總和 3", [2, 1], true),
            ("官方範例 2：只有一顆石子", [2], false),
            ("官方範例 3：餘數數量平衡", [5, 1, 2, 4, 3], false),
            ("回歸案例：偶數個餘數 0 且兩類非零餘數都存在", [1, 1, 2, 2], true),
            ("奇數個餘數 0 且餘數 1 多三顆", [3, 1, 1, 1, 1, 2], true),
            ("奇數個餘數 0 但數量差不足", [3, 1, 1, 2], false),
            ("邊界案例：只有餘數 0", [3, 6, 9], false)
        ];
        (string Name, Func<int[], bool> Solve)[] solutions =
        [
            (nameof(StoneGameIX), solver.StoneGameIX),
            (nameof(StoneGameIX2), solver.StoneGameIX2),
            (nameof(StoneGameIX3), solver.StoneGameIX3)
        ];

        int passed = 0;
        int total = testCases.Length * solutions.Length;

        foreach ((string caseName, int[] stones, bool expected) in testCases)
        {
            Console.WriteLine($"案例：{caseName}");
            Console.WriteLine($"Input: [{string.Join(", ", stones)}]");

            foreach ((string solutionName, Func<int[], bool> solve) in solutions)
            {
                bool actual = solve(stones.ToArray());
                bool isPassed = actual == expected;
                passed += isPassed ? 1 : 0;

                Console.WriteLine(
                    $"  {solutionName}: Expected={expected.ToString().ToLowerInvariant()}, " +
                    $"Actual={actual.ToString().ToLowerInvariant()}, {(isPassed ? "PASS" : "FAIL")}");
            }

            Console.WriteLine();
        }

        Console.WriteLine($"總結：{passed}/{total} 項測試通過");
        if (passed != total)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 判斷 Alice 在雙方採取最佳策略時能否獲勝。
    /// 解法先將石子依除以 3 的餘數分成三類，再根據餘數 0 的數量奇偶，
    /// 檢查餘數 1 與餘數 2 是否同時存在或數量差是否超過 2。
    /// 輸入陣列長度須介於 1 到 100000，且每個石子值須介於 1 到 10000。
    /// 若 Alice 有必勝策略則回傳 true，否則回傳 false。
    /// </summary>
    /// <param name="stones">每顆石子的正整數數值；方法不會修改此陣列。</param>
    /// <returns>Alice 是否能在最佳策略下獲勝。</returns>
    public bool StoneGameIX(int[] stones)
    {
        int cnt0 = 0;
        int cnt1 = 0;
        int cnt2 = 0;

        // 勝負只取決於累加和除以 3 的餘數，無須保留石子的原始數值。
        foreach (int val in stones)
        {
            int type = val % 3;
            if (type == 0)
            {
                cnt0++;
            }
            else if (type == 1)
            {
                cnt1++;
            }
            else
            {
                cnt2++;
            }
        }

        // 偶數顆餘數 0 不改變先後手優勢，兩種非零餘數都存在時 Alice 才能迫使 Bob 輸掉。
        if (cnt0 % 2 == 0)
        {
            return cnt1 >= 1 && cnt2 >= 1;
        }

        // 奇數顆餘數 0 會交換先後手優勢，某一種非零餘數必須至少多 3 顆。
        return cnt1 - cnt2 > 2 || cnt2 - cnt1 > 2;
    }

    /// <summary>
    /// 以分類後的勝負情況判斷 Alice 能否獲勝。
    /// 解法使用長度為 3 的陣列統計各餘數，分別處理餘數 0 數量為偶數與奇數的情況；
    /// 奇數情況再由輔助方法檢查餘數 1 或餘數 2 是否形成足夠大的數量優勢。
    /// 輸入陣列長度須介於 1 到 100000，且每個石子值須介於 1 到 10000。
    /// 若 Alice 有必勝策略則回傳 true，否則回傳 false。
    /// </summary>
    /// <param name="stones">每顆石子的正整數數值；方法不會修改此陣列。</param>
    /// <returns>Alice 是否能在最佳策略下獲勝。</returns>
    public bool StoneGameIX2(int[] stones)
    {
        int[] cnt = new int[3];

        foreach (int x in stones)
        {
            cnt[x % 3]++;
        }

        if (cnt[0] % 2 == 0)
        {
            return cnt[1] > 0 && cnt[2] > 0;
        }

        // 奇數顆餘數 0 時，餘數 1 或餘數 2 任一方形成 3 顆以上的優勢即可獲勝。
        return HasWinningImbalance(cnt[1], cnt[2]) ||
               HasWinningImbalance(cnt[2], cnt[1]);
    }

    /// <summary>
    /// 檢查某一種非零餘數的石子數量是否足以形成必勝優勢。
    /// 當候選餘數比另一種餘數至少多 3 顆時，Alice 可以維持安全的取石順序，
    /// 並迫使 Bob 先使累加和成為 3 的倍數。
    /// 輸入為兩種非零餘數的計數，輸出為候選餘數是否具有必勝數量差。
    /// </summary>
    /// <param name="candidateCount">目前作為優勢候選的餘數石子數量。</param>
    /// <param name="otherCount">另一種非零餘數的石子數量。</param>
    /// <returns>候選餘數是否比另一種餘數多至少 3 顆。</returns>
    private static bool HasWinningImbalance(int candidateCount, int otherCount)
    {
        return candidateCount - otherCount > 2;
    }

    /// <summary>
    /// 以精簡數學公式判斷 Alice 在最佳策略下能否獲勝。
    /// 解法用陣列統計三種餘數；餘數 0 為偶數時要求另外兩類都存在，
    /// 餘數 0 為奇數時則直接檢查另外兩類的數量差是否大於 2。
    /// 輸入陣列長度須介於 1 到 100000，且每個石子值須介於 1 到 10000。
    /// 若 Alice 有必勝策略則回傳 true，否則回傳 false。
    /// </summary>
    /// <param name="stones">每顆石子的正整數數值；方法不會修改此陣列。</param>
    /// <returns>Alice 是否能在最佳策略下獲勝。</returns>
    public bool StoneGameIX3(int[] stones)
    {
        int[] cnt = new int[3];

        foreach (int x in stones)
        {
            cnt[x % 3]++;
        }

        if (cnt[0] % 2 == 0)
        {
            return cnt[1] > 0 && cnt[2] > 0;
        }

        return Math.Abs(cnt[1] - cnt[2]) > 2;
    }
}