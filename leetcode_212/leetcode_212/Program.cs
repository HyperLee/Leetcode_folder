namespace leetcode_212;

class Program
{
    /// <summary>
    /// 212. Word Search II
    /// https://leetcode.com/problems/word-search-ii/description/?envType=problem-list-v2&envId=oizxjoit
    /// 212. 单词搜索 II
    /// https://leetcode.cn/problems/word-search-ii/description/
    /// 
    /// 題目描述：
    /// 給定一個 m x n 的字母網格 board 和一個字典中的單詞列表 words，找出所有同時在字典和網格中出現的單詞。
    /// 單詞必須按照字母順序通過相鄰的單元格構成，其中「相鄰」單元格是那些水平或垂直相鄰的單元格。
    /// 同一單元格內的字母在一個單詞中不允許被重複使用。
    /// 
    /// 解題概念與想法：
    /// 1. 使用字典樹（Trie）來儲存單詞列表，方便快速查詢。字典樹能夠更有效率地檢查前綴匹配，相比於逐一檢查每個單詞。
    /// 2. 遍歷網格中的每個單元格，作為深度優先搜尋（DFS）的起點。透過 DFS 我們能夠探索所有可能的路徑。
    /// 3. 在 DFS 過程中，同時在 Trie 中檢查當前路徑是否構成字典中的單詞，如果是則將其加入結果集合。
    /// 4. 使用臨時標記（將訪問過的字元改為特殊字元如 '#'）來避免在同一路徑中重複訪問單元格。
    /// 5. 搜尋完成後，回溯並恢復原始網格狀態，確保每個起點都有公平的搜尋機會。
    /// 
    /// 時間複雜度：O(M * N * 4^L)，其中 M 和 N 是網格的維度，L 是單詞的最大長度，4 代表四個方向。
    /// 空間複雜度：O(K)，其中 K 是所有單詞中字元的總數（用於建立 Trie）。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        RunSamples();
    }

    /// <summary>
    /// 執行五組固定案例，涵蓋典型輸入、無解、單格邊界、共用前綴與重複搜尋路徑。
    /// 每組案例會分別驗證找到的單詞集合，以及 DFS 回溯後字母網格是否恢復原狀。
    /// </summary>
    private static void RunSamples()
    {
        (string Name, char[][] Board, string[] Words, string[] Expected)[] cases =
        {
            (
                "官方 4 x 4 範例",
                new[]
                {
                    new[] { 'o', 'a', 'a', 'n' },
                    new[] { 'e', 't', 'a', 'e' },
                    new[] { 'i', 'h', 'k', 'r' },
                    new[] { 'i', 'f', 'l', 'v' }
                },
                new[] { "oath", "pea", "eat", "rain" },
                new[] { "eat", "oath" }
            ),
            (
                "同一格不可重複使用",
                new[]
                {
                    new[] { 'a', 'b' },
                    new[] { 'c', 'd' }
                },
                new[] { "abcb" },
                Array.Empty<string>()
            ),
            (
                "單格邊界",
                new[]
                {
                    new[] { 'a' }
                },
                new[] { "a" },
                new[] { "a" }
            ),
            (
                "共用前綴",
                new[]
                {
                    new[] { 'o', 'a', 't', 'h' }
                },
                new[] { "o", "oa", "oat", "oath", "hat" },
                new[] { "o", "oa", "oat", "oath" }
            ),
            (
                "多條路徑與結果去重",
                new[]
                {
                    new[] { 'a', 'a' },
                    new[] { 'a', 'a' }
                },
                new[] { "a", "aa", "aaa", "aaaa" },
                new[] { "a", "aa", "aaa", "aaaa" }
            )
        };

        Console.WriteLine("LeetCode 212 - Word Search II");
        int passedChecks = 0;

        for (int i = 0; i < cases.Length; i++)
        {
            (string name, char[][] board, string[] words, string[] expected) = cases[i];
            passedChecks += RunCase(i + 1, name, board, words, expected);
        }

        int totalChecks = cases.Length * 2;
        Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
    }

    /// <summary>
    /// 執行單一搜尋案例，將預期與實際結果排序後比較，並確認輸入網格在回溯後未被改變。
    /// 輸入須符合題目的矩形小寫英文字母網格與唯一單詞列表限制；回傳通過的檢查數，範圍為 0 到 2。
    /// </summary>
    /// <param name="caseNumber">從 1 開始顯示的案例編號。</param>
    /// <param name="name">案例用途說明。</param>
    /// <param name="board">本案例使用的非空矩形字母網格。</param>
    /// <param name="words">要搜尋的唯一小寫英文單詞。</param>
    /// <param name="expected">預期找到的單詞集合，順序不影響比較。</param>
    /// <returns>答案集合與網格還原兩項檢查中，通過的項目數。</returns>
    private static int RunCase(
        int caseNumber,
        string name,
        char[][] board,
        string[] words,
        string[] expected)
    {
        char[][] originalBoard = CloneBoard(board);
        string[] normalizedExpected = expected.OrderBy(word => word, StringComparer.Ordinal).ToArray();
        string[] actual = new Program()
            .FindWords(board, words)
            .OrderBy(word => word, StringComparer.Ordinal)
            .ToArray();

        bool resultMatches = normalizedExpected.SequenceEqual(actual);
        bool boardRestored = BoardsEqual(originalBoard, board);

        Console.WriteLine($"案例 {caseNumber}：{name}");
        Console.WriteLine($"  Board: {FormatBoard(board)}");
        Console.WriteLine($"  Words: {FormatWords(words)}");
        Console.WriteLine($"  Expected: {FormatWords(normalizedExpected)}");
        Console.WriteLine($"  Actual:   {FormatWords(actual)} ({FormatStatus(resultMatches)})");
        Console.WriteLine($"  Board restored: {FormatStatus(boardRestored)}");
        Console.WriteLine();

        return (resultMatches ? 1 : 0) + (boardRestored ? 1 : 0);
    }

    /// <summary>
    /// 深層複製鋸齒字元陣列，讓案例執行前後可以比較每一列內容。
    /// 輸入列不可為 null；回傳的新網格不與原網格共享任何列陣列。
    /// </summary>
    /// <param name="board">要複製的字母網格。</param>
    /// <returns>內容相同且各列獨立的新網格。</returns>
    private static char[][] CloneBoard(char[][] board)
    {
        return board.Select(row => row.ToArray()).ToArray();
    }

    /// <summary>
    /// 逐列比較兩個字母網格的尺寸與內容，用於確認 DFS 的暫時標記已完全回溯。
    /// </summary>
    /// <param name="left">比較左側的網格。</param>
    /// <param name="right">比較右側的網格。</param>
    /// <returns>兩個網格每一列皆相同時回傳 true，否則回傳 false。</returns>
    private static bool BoardsEqual(char[][] left, char[][] right)
    {
        if (left.Length != right.Length)
        {
            return false;
        }

        for (int i = 0; i < left.Length; i++)
        {
            if (!left[i].SequenceEqual(right[i]))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 將字母網格格式化成穩定、易於複製到 README 的巢狀陣列文字。
    /// </summary>
    /// <param name="board">要顯示的字母網格。</param>
    /// <returns>以雙引號包住字元的巢狀陣列字串。</returns>
    private static string FormatBoard(char[][] board)
    {
        IEnumerable<string> rows = board.Select(
            row => $"[{string.Join(", ", row.Select(character => $"\"{character}\""))}]");
        return $"[{string.Join(", ", rows)}]";
    }

    /// <summary>
    /// 將單詞序列格式化成穩定的陣列文字；空集合會顯示為 []。
    /// </summary>
    /// <param name="words">要顯示的單詞序列。</param>
    /// <returns>以雙引號包住各單詞的陣列字串。</returns>
    private static string FormatWords(IEnumerable<string> words)
    {
        return $"[{string.Join(", ", words.Select(word => $"\"{word}\""))}]";
    }

    /// <summary>
    /// 將布林驗證結果轉換為 console 使用的 PASS 或 FAIL 標記。
    /// </summary>
    /// <param name="passed">驗證是否通過。</param>
    /// <returns>通過時回傳 PASS，否則回傳 FAIL。</returns>
    private static string FormatStatus(bool passed)
    {
        return passed ? "PASS" : "FAIL";
    }

    /// <summary>
    /// 遍歷方向陣列，按順時鐘方向定義（右、下、左、上）
    /// 用於 DFS 搜尋中決定下一步可能的移動方向
    /// </summary>
    /// <value>包含四個方向的位移量：[0,1] 右, [1,0] 下, [0,-1] 左, [-1,0] 上</value>
    int[][] dirs = new int[][]
    {
        new int[]{0, 1},   // 右
        new int[]{1, 0},   // 下
        new int[]{0, -1},  // 左
        new int[]{-1, 0}   // 上
    };


    /// <summary>
    /// 在非空矩形字母網格中尋找所有出現在唯一單詞列表內的單詞。
    /// 先將只含小寫英文字母的 words 建成 Trie，再從每一格執行 DFS 與前綴剪枝；
    /// 搜尋期間會暫時標記 board，但每條路徑結束前都會回溯還原。
    /// ref: 
    /// https://leetcode.cn/problems/word-search-ii/solutions/1000172/dan-ci-sou-suo-ii-by-leetcode-solution-7494/
    /// https://leetcode.cn/problems/word-search-ii/solutions/1000331/gong-shui-san-xie-yi-ti-shuang-jie-hui-s-am8f/
    /// https://leetcode.cn/problems/word-search-ii/solutions/1000184/tong-ge-lai-shua-ti-la-yi-ti-si-jie-zi-d-2igi/
    /// 
    /// 使用官方的解法為基底, 並改寫
    /// 不使用 Dictionary 來儲存單詞, 因為題目已經明確說明 輸入資料為英文小寫
    /// 所以改用陣列來儲存字元
    /// 這樣可以節省空間, 並且在搜尋的時候也能更快地找到對應的字元
    /// 目前解法使用 208. Implement Trie (Prefix Tree) 為基礎變形而來
    /// 解這題之前先去練習 208. Implement Trie (Prefix Tree)
    /// https://leetcode.com/problems/implement-trie-prefix-tree/description/
    /// </summary>
    /// <param name="board">非空的 m x n 小寫英文字母網格。</param>
    /// <param name="words">非空且內容唯一的小寫英文單詞列表。</param>
    /// <returns>所有可由相鄰格組成的單詞；回傳順序不保證固定，且不包含重複項目。</returns>
    public IList<string> FindWords(char[][] board, string[] words)
    {
        Trie node = new Trie();

        foreach (string word in words)
        {
            node.Insert(word);
        }

        HashSet<string> res = new HashSet<string>();

        // 每個網格位置都可能是單詞起點，Trie 會立即排除不屬於任何前綴的路徑。
        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[0].Length; j++)
            {
                DFS(board, node, i, j, res);
            }
        }

        return res.ToList();
    }


    /// <summary>
    /// 從指定座標沿四個方向執行 DFS，並以目前 Trie 節點判斷路徑是否仍是有效前綴。
    /// 輸入座標必須位於非空矩形網格內；找到的完整單詞會加入 res，
    /// 當前格只在本次遞迴路徑中標記為已使用，方法返回前會恢復原字元。
    /// </summary>
    /// <param name="board">字母網格</param>
    /// <param name="node">當前 Trie 節點</param>
    /// <param name="row">當前行座標</param>
    /// <param name="col">當前列座標</param>
    /// <param name="res">結果集合，用於儲存找到的單詞</param>
    public void DFS(char[][] board, Trie node, int row, int col, HashSet<string> res)
    {
        int index = board[row][col] - 'a';
        if (node.children[index] == null)
        {
            // Trie 中沒有這個前綴時立即剪枝，不再探索後續方向。
            return;
        }

        char c = board[row][col];
        node = node.children[index];

        if (!string.IsNullOrEmpty(node.word))
        {
            // HashSet 會合併由不同網格路徑找到的相同單詞。
            res.Add(node.word);
        }

        // 暫時標記目前格，確保同一條單詞路徑不會重複使用它。
        board[row][col] = '#';

        for (int i = 0; i < dirs.Length; i++)
        {
            int newrow = row + dirs[i][0];
            int newcol = col + dirs[i][1];

            if (newrow >= 0 &&
                newrow < board.Length &&
                newcol >= 0 &&
                newcol < board[0].Length &&
                board[newrow][newcol] != '#')
            {
                DFS(board, node, newrow, newcol, res);
            }
        }

        // 回溯恢復原字元，讓其他起點與分支仍可使用這個位置。
        board[row][col] = c;
    }
}


/// <summary>
/// Trie（字典樹）類別，用於有效儲存和檢索單詞集合
/// 字典樹是一種樹形資料結構，特別適合用來處理字串的前綴匹配問題
/// 在本題中，Trie 用於快速檢查在 DFS 過程中形成的字串路徑是否可能構成有效單詞
/// </summary>
public class Trie
{
    /// <summary>
    /// 如果當前節點表示一個完整的單詞，則儲存該單詞；否則為空字串
    /// </summary>
    public string word;
    
    /// <summary>
    /// 儲存所有子節點的陣列，索引 0-25 對應 'a'-'z'
    /// </summary>
    public Trie[] children;
    
    /// <summary>
    /// 建立一個空的 Trie 節點，配置 26 個小寫英文字母子節點位置，
    /// 並以空字串表示目前節點尚未對應任何完整單詞。
    /// </summary>
    public Trie()
    {
        this.word = "";
        this.children = new Trie[26];
    }

    /// <summary>
    /// 將一個非空、只含小寫英文字母的單詞逐字插入 Trie；
    /// 共用前綴會沿用既有節點，最後節點的 word 欄位會保存完整單詞供 DFS 辨識。
    /// </summary>
    /// <param name="word">要插入的單詞</param>
    public void Insert(string word)
    {
        Trie node = this;

        foreach (char c in word)
        {
            int index = c - 'a';

            // 只有缺少分支時才建立節點，讓擁有相同前綴的單詞共用路徑。
            if (node.children[index] == null)
            {
                node.children[index] = new Trie();
            }

            node = node.children[index];
        }

        node.word = word;
    }
}
