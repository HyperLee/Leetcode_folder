namespace leetcode_3568;

class Program
{
    /// <summary>
    /// 3568. Minimum Moves to Clean the Classroom
    /// https://leetcode.com/problems/minimum-moves-to-clean-the-classroom/description/
    /// You are given an m x n grid classroom where a student volunteer is tasked with cleaning up litter scattered around the room. Each cell in the grid is one of the following:
    /// - 'S': Starting position of the student
    /// - 'L': Litter that must be collected (once collected, the cell becomes empty)
    /// - 'R': Reset area that restores the student's energy to full capacity, regardless of their current energy level (can be used multiple times)
    /// - 'X': Obstacle the student cannot pass through
    /// - '.': Empty space
    /// You are also given an integer energy, representing the student's maximum energy capacity. The student starts with this energy from the starting position 'S'.
    /// Each move to an adjacent cell (up, down, left, or right) costs 1 unit of energy. If the energy reaches 0, the student can only continue if they are on a reset area 'R', which resets the energy to its maximum capacity energy.
    /// Return the minimum number of moves required to collect all litter items, or -1 if it's impossible.
    /// 3568. 清理教室的最少移動
    /// https://leetcode.cn/problems/minimum-moves-to-clean-the-classroom/description/
    /// 給定一個 m x n 的網格 classroom，一名學生志工負責清理散落在教室中的垃圾。網格中的每個儲存格都是以下其中一種：
    /// - 'S'：學生的起始位置
    /// - 'L'：必須收集的垃圾（收集後，該儲存格會變成空白）
    /// - 'R'：重置區域，無論學生目前的能量為何，都會將能量恢復為最大值（可以重複使用）
    /// - 'X'：學生無法通過的障礙物
    /// - '.'：空白空間
    /// 另外給定一個整數 energy，表示學生的最大能量容量。學生從起始位置 'S' 出發時擁有此能量。
    /// 每次移動到相鄰儲存格（上、下、左或右）都會消耗 1 單位能量。若能量降至 0，學生只有在重置區域 'R' 上時才能繼續移動；該區域會將能量重置為最大容量 energy。
    /// 請返回收集所有垃圾所需的最少移動次數；如果無法完成，請返回 -1。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 直接執行官方三組範例，並以預期值自動判斷每個案例是否通過。
        Program solution = new Program();
        int passed = 0;

        passed += RunTestCase(solution, "範例 1", ["S.", "XL"], 2, 2) ? 1 : 0;
        passed += RunTestCase(solution, "範例 2", ["LS", "RL"], 4, 3) ? 1 : 0;
        passed += RunTestCase(solution, "範例 3", ["L.S", "RXL"], 3, -1) ? 1 : 0;

        Console.WriteLine($"總結：{passed}/3 通過");
    }

    static readonly int[] dx = [0, 1, 0, -1];
    static readonly int[] dy = [1, 0, -1, 0];

    /// <summary>
    /// 執行一組固定測試案例，呼叫 <see cref="MinMoves"/> 計算答案，並比較實際結果與預期結果。
    /// 輸入的教室必須符合題目定義，且 <paramref name="energy"/> 必須為正整數；
    /// 回傳值表示本案例是否通過，主程式會使用它統計通過數量。
    /// </summary>
    /// <param name="solution">用來執行解法的 <see cref="Program"/> 執行個體。</param>
    /// <param name="name">顯示於主控台的案例名稱。</param>
    /// <param name="classroom">由等長字串組成的教室網格。</param>
    /// <param name="energy">學生的最大能量。</param>
    /// <param name="expected">案例預期的最少移動次數。</param>
    /// <returns>實際結果等於預期結果時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
    private static bool RunTestCase(Program solution, string name, string[] classroom, int energy, int expected)
    {
        int actual = solution.MinMoves(classroom, energy);
        bool passed = actual == expected;

        Console.WriteLine($"{name}：{(passed ? "PASS" : "FAIL")}｜預期：{expected}｜實際：{actual}");
        return passed;
    }

    /// <summary>
    /// 計算清理教室內所有垃圾所需的最少移動次數。
    /// 解法使用 BFS 依移動步數逐層搜尋，以 Bitmask 記錄已清理的垃圾，
    /// 並為每個「位置與垃圾集合」保留最高剩餘能量，剪除被支配的較差狀態。
    /// 輸入必須是包含唯一 <c>S</c>、至多十個 <c>L</c> 的合法矩形網格，且能量必須為正整數。
    /// </summary>
    /// <param name="classroom">教室網格；每格只會是 <c>S</c>、<c>L</c>、<c>R</c>、<c>X</c> 或 <c>.</c>。</param>
    /// <param name="energy">學生出發時以及進入重置區域後擁有的最大能量。</param>
    /// <returns>清理所有垃圾的最少移動次數；無法完成時回傳 <c>-1</c>。</returns>
    public int MinMoves(string[] classroom, int energy)
    {
        int rowCount = classroom.Length;
        int columnCount = classroom[0].Length;
        int[,] litterBitByCell = new int[rowCount, columnCount];
        int startRow = 0, startColumn = 0, litterCount = 0;

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                char cellType = classroom[i][j];
                if (cellType == 'S')
                {
                    startRow = i;
                    startColumn = j;
                }
                else if (cellType == 'L')
                {
                    // 每個垃圾分配一個獨立 bit，之後可用一個整數表示已清理集合。
                    litterBitByCell[i, j] = 1 << litterCount;
                    litterCount++;
                }
            }
        }

        int maskStateCount = 1 << litterCount;
        int allLitterMask = maskStateCount - 1;
        int[,,] bestEnergy = new int[rowCount, columnCount, maskStateCount];

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                for (int k = 0; k < maskStateCount; k++)
                {
                    bestEnergy[i, j, k] = -1;
                }
            }
        }

        bestEnergy[startRow, startColumn, 0] = energy;

        var queue = new Queue<(int row, int column, int litterMask, int remainingEnergy, int steps)>();
        queue.Enqueue((startRow, startColumn, 0, energy, 0));

        // 所有移動成本皆為 1，BFS 第一次取出完成狀態時就是最少步數。
        while (queue.Count > 0)
        {
            var state = queue.Dequeue();
            if (state.litterMask == allLitterMask)
            {
                return state.steps;
            }

            if (state.remainingEnergy == 0)
            {
                continue;
            }

            for (int directionIndex = 0; directionIndex < 4; directionIndex++)
            {
                int nextRow = state.row + dx[directionIndex];
                int nextColumn = state.column + dy[directionIndex];
                if (nextRow < 0 || nextRow >= rowCount ||
                    nextColumn < 0 || nextColumn >= columnCount ||
                    classroom[nextRow][nextColumn] == 'X')
                {
                    continue;
                }

                // 進入 R 仍算一次移動，但抵達後能量直接恢復至最大值。
                int nextEnergy = classroom[nextRow][nextColumn] == 'R' ? energy : state.remainingEnergy - 1;

                // 非垃圾格的 litterBitByCell 為 0；OR 運算也自然避免同一垃圾被重複計算。
                int nextLitterMask = state.litterMask | litterBitByCell[nextRow, nextColumn];

                // 同位置、同 mask 下，能量較高的狀態能完成較低能量狀態的所有後續路徑。
                if (nextEnergy > bestEnergy[nextRow, nextColumn, nextLitterMask])
                {
                    bestEnergy[nextRow, nextColumn, nextLitterMask] = nextEnergy;
                    queue.Enqueue((nextRow, nextColumn, nextLitterMask, nextEnergy, state.steps + 1));
                }
            }
        }

        return -1;
    }
}