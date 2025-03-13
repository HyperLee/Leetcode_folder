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
        var solution = new Solution();
        
        // 測試案例1
        char[][] board1 = new char[][]
        {
            new char[] { 'A', 'B', 'C', 'E' },
            new char[] { 'S', 'F', 'C', 'S' },
            new char[] { 'A', 'D', 'E', 'E' }
        };
        RunTest(solution, board1, "ABCCED", 1);  // 預期結果: true
        RunTest(solution, board1, "SEE", 2);     // 預期結果: true
        RunTest(solution, board1, "ABCB", 3);    // 預期結果: false

        // 測試案例2
        char[][] board2 = new char[][]
        {
            new char[] { 'C', 'A', 'A' },
            new char[] { 'A', 'A', 'A' },
            new char[] { 'B', 'C', 'D' }
        };
        RunTest(solution, board2, "AAB", 4);     // 預期結果: true
        RunTest(solution, board2, "CAAD", 5);    // 預期結果: true
        RunTest(solution, board2, "AABC", 6);    // 預期結果: false

        // 邊界測試案例
        char[][] board3 = new char[][] 
        {
            new char[] { 'A' }
        };
        RunTest(solution, board3, "A", 7);       // 預期結果: true
        RunTest(solution, board3, "B", 8);       // 預期結果: false
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="solution"></param>
    /// <param name="board"></param>
    /// <param name="word"></param>
    /// <param name="testNumber"></param>
    private static void RunTest(Solution solution, char[][] board, string word, int testNumber)
    {
        bool result = solution.Exist(board, word);
        Console.WriteLine($"測試 #{testNumber}");
        Console.WriteLine($"輸入單字: {word}");
        Console.WriteLine($"結果: {result}");
        Console.WriteLine("矩陣:");
        PrintBoard(board);
        Console.WriteLine(new string('-', 30));
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="board"></param>
    private static void PrintBoard(char[][] board)
    {
        foreach (var row in board)
        {
            Console.WriteLine(string.Join(" ", row));
        }
    }


    /// <summary>
    /// ref:
    /// https://leetcode.cn/problems/word-search/solutions/2927294/liang-ge-you-hua-rang-dai-ma-ji-bai-jie-g3mmm/
    /// https://leetcode.cn/problems/word-search/solutions/411613/dan-ci-sou-suo-by-leetcode-solution/
    /// https://leetcode.cn/problems/word-search/solutions/1924200/by-stormsunshine-26yc/
    /// </summary> 
    public class Solution
    {
        /// <summary>
        /// 這段程式碼定義了一個二維陣列 DIRS，用於表示在二維網格或矩陣中的四個基本移動方向：
        /// 1. { 0, -1 } 代表向上移動（y 座標減 1）
        /// 2. { 0, 1 } 代表向下移動（y 座標加 1）
        /// 3. { -1, 0 } 代表向左移動（x 座標減 1）
        /// 4. { 1, 0 } 代表向右移動（x 座標加 1）
        /// 使用時，通常會遍歷這個陣列來檢查某個位置的四個相鄰格子。
        //  例如，如果目前位置是 (x, y)，則可以用 (x + DIRS[i][0], y + DIRS[i][1]) 來取得四個相鄰位置的座標。
        /// </summary>
        /// <value></value>
        private static readonly int[][] DIRS = new int[][] { new int[] { 0, -1 }, new int[] { 0, 1 }, new int[] { -1, 0 }, new int[] { 1, 0 } };

        /// <summary>
        /// 在二維字符矩陣中搜索單詞
        /// 解題思路：
        /// 1. 使用深度優先搜索(DFS)遍歷矩陣中的每個字符
        /// 2. 利用回溯法處理搜索路徑
        /// 3. 優化方案：
        ///    - 使用字符統計提前判斷不可能的情況
        ///    - 根據首尾字符出現頻率決定搜索方向
        /// 時間複雜度：O(m*n*3^L)，其中 m,n 為矩陣維度，L 為單詞長度
        /// 空間複雜度：O(min(L,m*n))，主要為遞歸調用棧的深度
        /// </summary>
        /// <param name="board">輸入的字符矩陣</param>
        /// <param name="word">需要搜索的單詞</param>
        /// <returns>是否找到單詞</returns>
        public bool Exist(char[][] board, string word)
        {
            // 步驟1：預處理 - 統計矩陣中每個字符的出現次數
            // 使用ASCII碼作為索引，提高查找效率
            // 為了方便用陣列取代 hashset
            int[] cnt = new int[128];
            foreach (char[] row in board)
            {
                foreach (char c in row)
                {
                    cnt[c]++;
                }
            }

            // 步驟2：優化檢查 - 確保單詞中的每個字符出現次數不超過矩陣中的出現次數
            char[] w = word.ToCharArray();
            int[] wordCnt = new int[128];
            foreach (char c in w)
            {
                // 如果單詞中某個字符的出現次數超過矩陣中的出現次數，直接返回false
                if (++wordCnt[c] > cnt[c])
                {
                    return false;
                }
            }

            // 步驟3：搜索方向優化 - 根據首尾字符在矩陣中的出現頻率決定搜索方向
            // 如果尾字符出現頻率小於首字符，則反轉單詞進行搜索，可以減少搜索分支
            // 這段程式碼比較單字的首尾字母在矩陣中的出現頻率，如果尾字母出現次數較少，
            // 則反轉單字進行搜索。這樣做可以減少搜索分支，提高效率。
            if (cnt[w[w.Length - 1]] < cnt[w[0]])
            {
                w = new string(word.Reverse().ToArray()).ToCharArray();
            }

            // 步驟4：遍歷矩陣的每個位置作為起點進行DFS搜索
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (Dfs(i, j, 0, board, w))
                    {
                        return true; // 找到完整的路徑，返回true
                    }
                }
            }
            return false; // 所有可能的路徑都搜索完畢，未找到符合的路徑
        }

        /// <summary>
        /// 深度優先搜索(DFS)實現矩陣中的單詞搜索
        /// 解題思路：
        /// 1. 使用DFS遞迴搜索相鄰的格子
        /// 2. 使用原地標記法避免重複訪問（將訪問過的字符臨時改為'0'）
        /// 3. 使用回溯法恢復現場，確保不影響其他路徑的搜索
        /// 
        /// 核心邏輯：
        /// - 在每個位置檢查當前字符是否匹配
        /// - 匹配成功後，繼續搜索四個方向的相鄰格子
        /// - 使用臨時標記避免重複訪問同一位置
        /// - 回溯時恢復原始字符
        /// 
        /// 時間複雜度：O(3^L)，其中L為單詞長度（每次有3個新方向可選）
        /// 空間複雜度：O(L)，其中L為遞迴調用的最大深度
        /// 
        /// memo:遞歸過程中，為了避免重複造訪同一個格子，可以用vis數組標記。更簡單的做法是，直接修改板[ i ] [ j ]，將其置為空（或者0），
        /// 返回false前再恢復成原來的數值（恢復現場）。注意返回true的時候就不用恢復現場了，因為已經成功搜到word了。
        /// 
        /// 行索引檢查 
        ///     0 <= x && x < board.Length
        ///     確保行索引 x 在有效範圍內
        ///     x < board.Length：確保不會超出上界
        /// 列索引檢查
        ///     確保列索引 y 在有效範圍內
        ///     0 <= y：確保不會超出左界
        ///     y < board[x].Length：確保不會超出右界
        /// 遞迴搜索
        /// x, y：新的搜索位置
        /// k + 1：移動到單字的下一個字符
        /// 
        /// 0 <= y && y < board[x].Length 與 0 <= y && y < board[0].Length 有什麼差異
        ///     兩種寫法在大多數情況下結果相同，但在處理不規則矩陣時會有重要差異：
        /// 0 <= y && y < board[x].Length
        ///     使用當前行 x 的實際長度
        ///     適用於鋸齒狀陣列（每行長度可能不同）
        ///     更安全，因為檢查實際的行長度
        ///     例如，考慮這個不規則矩陣
        /// 
        /// 0 <= y && y < board[0].Length
        ///     只使用第一行的長度
        ///     假設所有行長度相同
        ///     在不規則矩陣中可能造成 IndexOutOfRangeException
        /// 
        /// 推薦：更安全的寫法
        /// if (0 <= y && y < board[x].Length)
        /// 
        /// 不推薦：假設矩陣是規則的
        /// if (0 <= y && y < board[0].Length)
        ///     如果確定是規則矩陣（所有行長度相同），兩種寫法都可以
        ///     為了程式碼的穩定性和安全性，建議使用 board[x].Length
        ///     這樣的寫法能處理所有可能的矩陣情況，不會出現索引越界錯誤
        /// </summary>
        /// <param name="i">當前行索引</param>
        /// <param name="j">當前列索引</param>
        /// <param name="k">當前匹配到單詞的位置</param>
        /// <param name="board">字符矩陣</param>
        /// <param name="word">需要匹配的單詞字符數組</param>
        /// <returns>是否找到匹配路徑</returns>
        private bool Dfs(int i, int j, int k, char[][] board, char[] word)
        {
            // 步驟1：檢查當前字符是否匹配
            // 如果當前位置的字符與目標字符不匹配，直接返回false
            if (board[i][j] != word[k]) 
            {
                return false;
            }

            // 步驟2：檢查是否已完成所有字符的匹配
            // 如果已經匹配到單詞的最後一個字符，表示找到完整路徑
            if (k == word.Length - 1) 
            {
                return true;
            }

            // 步驟3：標記當前位置為已訪問
            // 使用'0'臨時標記，因為題目保證輸入只包含大寫字母
            char temp = board[i][j];
            board[i][j] = '0';

            // 步驟4：搜索四個方向的相鄰格子
            // 通過DIRS數組遍歷上、下、左、右四個方向
            foreach (int[] d in DIRS)
            {
                int x = i + d[0];  // 計算相鄰格子的行索引
                int y = j + d[1];  // 計算相鄰格子的列索引

                // 檢查相鄰位置是否有效且可以繼續搜索
                if (0 <= x && x < board.Length &&     // 行索引在有效範圍內
                    0 <= y && y < board[x].Length &&  // 列索引在有效範圍內
                    Dfs(x, y, k + 1, board, word))   // 遞迴搜索下一個字符
                {
                    return true;  // 找到有效路徑
                }
            }

            // 步驟5：回溯 - 恢復當前位置的原始字符
            // 確保不影響其他路徑的搜索
            // 回溯步驟3的標記搜尋.
            board[i][j] = temp;
            
            // 步驟6：返回搜索結果
            // 如果所有方向都搜索失敗，返回false
            return false;
        }
    }
}
