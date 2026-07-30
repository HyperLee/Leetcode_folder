namespace leetcode_079;

class Program
{
    /// <summary>
    /// 79. Word Search
    /// https://leetcode.com/problems/word-search/description/
    /// 79. 单词搜索
    /// https://leetcode.cn/problems/word-search/description/
    /// 
    /// 給定一個 m x n 的二維字符網格 board 和一個字符串 word 。
    /// 如果 word 存在於網格中，返回 true ；否則，返回 false 。
    /// 
    /// 單詞必須按照字母順序，通過相鄰的單元格內的字母構成，
    /// 其中"相鄰"單元格是那些水平相鄰或垂直相鄰的單元格。
    /// 同一個單元格內的字母不允許被重複使用。
    /// </summary>
    /// <param name="args"></param> 
    static void Main(string[] args)
    {
        bool allPassed = RunAllTests();
        Environment.ExitCode = allPassed ? 0 : 1;
    }

    /// <summary>
    /// 執行 Word Search 的固定驗收案例，涵蓋典型路徑、重複字元與單格邊界。
    /// 每筆案例都會比對預期結果，並確認搜尋完成後輸入棋盤未被修改。
    /// </summary>
    /// <returns>全部案例的結果與棋盤完整性皆符合預期時回傳 <see langword="true"/>。</returns>
    private static bool RunAllTests()
    {
        Solution solution = new Solution();
        int passedCount = 0;
        const int totalCount = 8;

        char[][] board1 =
        {
            new[] { 'A', 'B', 'C', 'E' },
            new[] { 'S', 'F', 'C', 'S' },
            new[] { 'A', 'D', 'E', 'E' }
        };
        passedCount += RunTest(solution, board1, "ABCCED", true, 1) ? 1 : 0;
        passedCount += RunTest(solution, board1, "SEE", true, 2) ? 1 : 0;
        passedCount += RunTest(solution, board1, "ABCB", false, 3) ? 1 : 0;

        char[][] board2 =
        {
            new[] { 'C', 'A', 'A' },
            new[] { 'A', 'A', 'A' },
            new[] { 'B', 'C', 'D' }
        };
        passedCount += RunTest(solution, board2, "AAB", true, 4) ? 1 : 0;
        passedCount += RunTest(solution, board2, "CAAD", true, 5) ? 1 : 0;
        passedCount += RunTest(solution, board2, "AABC", true, 6) ? 1 : 0;

        char[][] board3 =
        {
            new[] { 'A' }
        };
        passedCount += RunTest(solution, board3, "A", true, 7) ? 1 : 0;
        passedCount += RunTest(solution, board3, "B", false, 8) ? 1 : 0;

        Console.WriteLine($"總計：{passedCount}/{totalCount} 通過");
        return passedCount == totalCount;
    }

    /// <summary>
    /// 執行單一搜尋案例，比對實際與預期布林值，並驗證回溯後的棋盤等同輸入。
    /// </summary>
    /// <param name="solution">要驗證的 Word Search 解法。</param>
    /// <param name="board">僅含英文字母且至少包含一格的輸入棋盤。</param>
    /// <param name="word">要沿水平或垂直相鄰格搜尋的非空單字。</param>
    /// <param name="expected">此案例預期是否能找到單字。</param>
    /// <param name="testNumber">顯示於主控台的案例編號。</param>
    /// <returns>結果符合預期且棋盤保持不變時回傳 <see langword="true"/>。</returns>
    private static bool RunTest(Solution solution, char[][] board, string word, bool expected, int testNumber)
    {
        char[][] originalBoard = board.Select(row => row.ToArray()).ToArray();
        bool actual = solution.Exist(board, word);
        bool boardUnchanged = BoardsEqual(board, originalBoard);
        bool passed = actual == expected && boardUnchanged;

        Console.WriteLine($"測試 #{testNumber}：{word}");
        Console.WriteLine($"預期結果: {expected}");
        Console.WriteLine($"實際結果: {actual}");
        Console.WriteLine($"棋盤保持不變: {boardUnchanged}");
        Console.WriteLine($"判定: {(passed ? "PASS" : "FAIL")}");
        Console.WriteLine("矩陣:");
        PrintBoard(board);
        Console.WriteLine(new string('-', 30));
        return passed;
    }

    /// <summary>
    /// 將字元棋盤逐列輸出，方便核對案例內容與搜尋後狀態。
    /// </summary>
    /// <param name="board">要輸出的非空字元棋盤。</param>
    private static void PrintBoard(char[][] board)
    {
        foreach (char[] row in board)
        {
            Console.WriteLine(string.Join(" ", row));
        }
    }

    /// <summary>
    /// 逐列比較兩個鋸齒字元陣列，確認列數、各列長度與所有字元皆相同。
    /// </summary>
    /// <param name="left">搜尋完成後的棋盤。</param>
    /// <param name="right">搜尋前保存的棋盤快照。</param>
    /// <returns>兩個棋盤內容完全相同時回傳 <see langword="true"/>。</returns>
    private static bool BoardsEqual(char[][] left, char[][] right)
    {
        return left.Length == right.Length &&
            left.Zip(right, (leftRow, rightRow) => leftRow.SequenceEqual(rightRow)).All(equal => equal);
    }

    /// <summary>
    /// 提供 Word Search 的深度優先搜尋與回溯解法，並以字元統計減少不必要的搜尋。
    /// </summary>
    public class Solution
    {
        /// <summary>
        /// 定義上、下、左、右四個可移動方向，不包含對角線。
        /// </summary>
        private static readonly int[][] Directions =
        {
            new[] { 0, -1 },
            new[] { 0, 1 },
            new[] { -1, 0 },
            new[] { 1, 0 }
        };

        /// <summary>
        /// 判斷非空單字能否由棋盤中水平或垂直相鄰、且不重複使用的格子組成。
        /// 先以字元數量排除不可能的輸入，再從較少見的端點開始進行 DFS 與回溯。
        /// 搜尋期間會暫時標記格子，但回傳前一定恢復棋盤；結果不會修改呼叫者輸入。
        /// </summary>
        /// <param name="board">至少含一格、每格皆為英文字母的字元棋盤。</param>
        /// <param name="word">長度至少為一、僅含英文字母的目標單字。</param>
        /// <returns>存在符合規則的完整路徑時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
        public bool Exist(char[][] board, string word)
        {
            // 英文字母位於 ASCII 範圍，可用固定陣列快速統計棋盤供應量。
            int[] cnt = new int[128];
            foreach (char[] row in board)
            {
                foreach (char c in row)
                {
                    cnt[c]++;
                }
            }

            char[] w = word.ToCharArray();
            int[] wordCnt = new int[128];
            foreach (char c in w)
            {
                if (++wordCnt[c] > cnt[c])
                {
                    return false;
                }
            }

            // 從棋盤中較少出現的端點開始，可降低 DFS 第一層的候選起點數。
            if (cnt[w[w.Length - 1]] < cnt[w[0]])
            {
                w = new string(word.Reverse().ToArray()).ToCharArray();
            }

            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (Dfs(i, j, 0, board, w))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 從指定格子比對單字索引，暫時將已使用格子標記為 <c>'0'</c>，
        /// 再遞迴探索四個方向；完成該層搜尋後以回溯恢復原始字元。
        /// </summary>
        /// <param name="i">目前格子的有效列索引。</param>
        /// <param name="j">目前格子的有效欄索引。</param>
        /// <param name="k">目前要比對的單字索引。</param>
        /// <param name="board">搜尋期間可暫時標記、回傳前會恢復的棋盤。</param>
        /// <param name="word">依搜尋方向排列的非空目標字元陣列。</param>
        /// <returns>從目前格子可完成剩餘單字路徑時回傳 <see langword="true"/>。</returns>
        private bool Dfs(int i, int j, int k, char[][] board, char[] word)
        {
            if (board[i][j] != word[k])
            {
                return false;
            }

            if (k == word.Length - 1)
            {
                return true;
            }

            // '0' 不會和題目限定的英文字母相同，因此可代表目前路徑已使用此格。
            char temp = board[i][j];
            board[i][j] = '0';

            bool found = false;
            foreach (int[] direction in Directions)
            {
                int x = i + direction[0];
                int y = j + direction[1];

                if (0 <= x && x < board.Length &&
                    0 <= y && y < board[x].Length &&
                    Dfs(x, y, k + 1, board, word))
                {
                    found = true;
                    break;
                }
            }

            // 即使後續遞迴已找到單字，也要先恢復現場，避免公開方法改寫呼叫者的棋盤。
            board[i][j] = temp;
            return found;
        }
    }
}