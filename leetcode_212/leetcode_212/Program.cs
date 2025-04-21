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
        // 測試資料
        char[][] board = new char[][]
        {
            new char[] {'o', 'a', 'a', 'n'},
            new char[] {'e', 't', 'a', 'e'},
            new char[] {'i', 'h', 'k', 'r'},
            new char[] {'i', 'f', 'l', 'v'}
        };

        string[] words = new string[] {"oath", "pea", "eat", "rain"};

        Program program = new Program();
        IList<string> result = program.FindWords(board, words);

        Console.WriteLine("找到的單詞:");
        foreach (string word in result)
        {
            Console.WriteLine(word);
        }
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
    /// 主要函式，用於在字母網格中尋找所有給定的單詞
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
    /// <param name="board">字母網格，包含 m x n 個字元</param>
    /// <param name="words">要在網格中尋找的單詞列表</param>
    /// <returns>所有在網格中找到的單詞列表</returns>
    public IList<string> FindWords(char[][] board, string[] words)
    {
        // 步驟 1: 建立 Trie 物件
        Trie node = new Trie();
        
        // 步驟 2: 將所有單詞插入 Trie 中
        foreach(string word in words)
        {
            node.Insert(word);
        }

        // 步驟 3: 建立結果集合，使用 HashSet 避免重複
        HashSet<string> res = new HashSet<string>();
        
        // 步驟 4: 遍歷網格中的每個單元格作為起點
        for(int i = 0; i < board.Length; i++)
        {
            for(int j = 0; j < board[0].Length; j++)
            {
                // 步驟 5: 從當前單元格開始深度優先搜尋
                DFS(board, node, i, j, res);
            }
        }

        // 步驟 6: 將結果集合轉換為列表並返回
        return res.ToList();
    }


    /// <summary>
    /// 深度優先搜尋函式，用於從指定位置探索可能的單詞路徑
    /// DFS + 回溯法
    /// 1. 檢查當前字元是否存在於 Trie 的下一層子節點中
    /// 2. 儲存當前字元並移動到 Trie 的下一層節點
    /// 3. 檢查當前節點是否是一個完整的單詞，如果是則加入結果集合
    /// 4. 標記當前單元格已訪問，避免重複使用
    /// 5. 向四個方向探索下一個可能的字元
    /// 6. 檢查下一個位置是否有效（在網格範圍內且未被訪問）
    /// 7. 回溯，恢復當前單元格的原始字元，以便其他路徑可以使用
    /// 
    /// </summary>
    /// <param name="board">字母網格</param>
    /// <param name="node">當前 Trie 節點</param>
    /// <param name="i1">當前行座標</param>
    /// <param name="j1">當前列座標</param>
    /// <param name="res">結果集合，用於儲存找到的單詞</param>
    public void DFS(char[][] board, Trie node, int i1, int j1, HashSet<string> res) 
    {
        // 步驟 1: 檢查當前網格字元是否存在於 Trie 的下一層子節點中
        int index = board[i1][j1] - 'a';
        if(node.children[index] == null)
        {
            return; // 如果不存在，表示無法構成有效單詞路徑，直接返回
        }
        
        // 步驟 2: 儲存當前字元並移動到 Trie 的下一層節點
        char c = board[i1][j1];
        node = node.children[index];
        
        // 步驟 3: 檢查當前節點是否是一個完整的單詞
        if(!string.IsNullOrEmpty(node.word))
        {
            res.Add(node.word); // 如果是完整單詞，加入結果集合
        }
        
        // 步驟 4: 標記當前單元格已訪問，避免重複使用
        board[i1][j1] = '#'; 
        
        // 步驟 5: 向四個方向探索下一個可能的字元
        for(int i = 0; i < dirs.Length; i++)
        {
            int x = i1 + dirs[i][0]; // 計算下一個位置的行座標
            int y = j1 + dirs[i][1]; // 計算下一個位置的列座標
            
            // 步驟 6: 檢查下一個位置是否有效（在網格範圍內且未被訪問）
            if(x >= 0 && x < board.Length && y >= 0 && y < board[0].Length && board[x][y] != '#')
            {
                DFS(board, node, x, y, res); // 遞迴探索下一個位置
            }
        }
        
        // 步驟 7: 回溯，恢復當前單元格的原始字元，以便其他路徑可以使用
        board[i1][j1] = c; 
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
    /// 建構子，初始化一個空的 Trie 節點
    /// </summary>
    public Trie()
    {
        this.word = "";
        this.children = new Trie[26]; // 初始化為大小 26 的陣列，對應 26 個英文小寫字母
    }

    /// <summary>
    /// 將單詞插入 Trie 中
    /// </summary>
    /// <param name="word">要插入的單詞</param>
    public void Insert(string word)
    {
        // 從根節點開始
        Trie node = this;
        
        // 遍歷單詞中的每個字元
        foreach(char c in word)
        {
            // 計算字元在陣列中的索引位置 (例如: 'a' -> 0, 'b' -> 1, ...)
            int index = c - 'a';
            
            // 如果當前字元不存在於子節點中，則建立新節點
            if(node.children[index] == null)
            {
                node.children[index] = new Trie();
            }
            // 移動到下一層節點
            node = node.children[index];
        }
        
        // 標記完整單詞，便於在 DFS 中識別
        node.word = word;
    }
}
