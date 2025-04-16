namespace leetcode_211;

class Program
{
    /// <summary>
    /// 211. Design Add and Search Words Data Structure
    /// https://leetcode.com/problems/design-add-and-search-words-data-structure/description/?envType=problem-list-v2&envId=oizxjoit
    /// 211. 添加与搜索单词 - 数据结构设计
    /// https://leetcode.cn/problems/design-add-and-search-words-data-structure/description/
    /// 
    /// 題目概述：
    /// 設計一個資料結構支援兩種操作：添加單字和搜尋單字，搜尋時可使用萬用字元 '.' 代表任一字元。
    /// 
    /// 解題出發點：
    /// 1. 使用字典樹(Trie)資料結構來有效儲存並快速查詢單字
    /// 2. 字典樹可共享相同字首的路徑，節省記憶體空間
    /// 3. 針對萬用字元 '.' 處理，使用深度優先搜尋(DFS)來檢查所有可能匹配
    /// 4. 時間複雜度：添加單字為 O(m)，搜尋單字為 O(m) 到 O(26^m)，其中 m 為單字長度
    /// </summary>
    static void Main(string[] args)
    {
        Console.WriteLine("211. 添加與搜尋單字 - 資料結構設計");
        
        // 建立 WordDictionary 物件
        WordDictionary wordDictionary = new WordDictionary();
        
        // 添加單字測試
        Console.WriteLine("添加單字: bad, dad, mad");
        wordDictionary.AddWord("bad");
        wordDictionary.AddWord("dad");
        wordDictionary.AddWord("mad");
        
        // 搜尋單字測試
        Console.WriteLine("\n搜尋結果:");
        Console.WriteLine($"搜尋 'pad': {wordDictionary.Search("pad")}");  // 應返回 False
        Console.WriteLine($"搜尋 'bad': {wordDictionary.Search("bad")}");  // 應返回 True
        Console.WriteLine($"搜尋 '.ad': {wordDictionary.Search(".ad")}");  // 應返回 True (匹配 bad, dad, mad)
        Console.WriteLine($"搜尋 'b..': {wordDictionary.Search("b..")}");  // 應返回 True (匹配 bad)
        Console.WriteLine($"搜尋 'b.e': {wordDictionary.Search("b.e")}");  // 應返回 False
        
        // 添加更多測試案例
        Console.WriteLine("\n添加更多單字: apple, apply");
        wordDictionary.AddWord("apple");
        wordDictionary.AddWord("apply");
        
        Console.WriteLine("\n進階搜尋測試:");
        Console.WriteLine($"搜尋 'a...e': {wordDictionary.Search("a...e")}");  // 應返回 True (匹配 apple)
        Console.WriteLine($"搜尋 'a...y': {wordDictionary.Search("a...y")}");  // 應返回 True (匹配 apply)
        Console.WriteLine($"搜尋 'ap..y': {wordDictionary.Search("ap..y")}");  // 應返回 True (匹配 apply)
        Console.WriteLine($"搜尋 '....': {wordDictionary.Search("....")}");     // 應返回 False (無4字元單字)
        Console.WriteLine($"搜尋 '.....': {wordDictionary.Search(".....")}");    // 應返回 True (匹配 apple, apply)

        Console.WriteLine("\n測試完成！");
    }
}

/// <summary>
/// 使用字典樹(Trie)實現的單字查詢資料結構
/// 解題概念：
/// 1. 使用字典樹結構儲存所有添加的單字
/// 2. 每個節點代表一個字元，並有指向26個可能的子節點
/// 3. 搜尋時支援萬用字元'.'，表示可以匹配任何字元
/// 4. 對於萬用字元的情況，使用深度優先搜尋(DFS)嘗試所有可能的路徑
/// </summary>
public class WordDictionary
{
    // 字典樹的根節點
    private Trie root;
    
    /// <summary>
    /// 初始化字典資料結構
    /// </summary>
    public WordDictionary()
    {
        root = new Trie();
    }

    /// <summary>
    /// 添加單字到字典中
    /// </summary>
    /// <param name="word">要添加的單字</param>
    public void AddWord(string word)
    {
        // 直接利用Trie的Insert方法添加單字
        root.Insert(word);
    }

    /// <summary>
    /// 搜尋單字是否存在於字典中，支援萬用字元'.'
    /// </summary>
    /// <param name="word">要搜尋的單字，可包含萬用字元'.'</param>
    /// <returns>單字存在返回true，否則返回false</returns>
    public bool Search(string word)
    {
        // 從根節點開始進行深度優先搜尋
        return DFS(word, 0, root);
    }

    /// <summary>
    /// 使用深度優先搜尋遞迴查詢單字
    /// </summary>
    /// <param name="word">要搜尋的單字</param>
    /// <param name="index">目前處理的字元索引</param>
    /// <param name="node">目前搜尋到的節點</param>
    /// <returns>找到匹配返回true，否則返回false</returns>
    private bool DFS(string word, int index, Trie node)
    {
        // 遞迴終止條件：已到達單字末尾
        if(index == word.Length)
        {
            // 檢查目前節點是否是單字結尾
            return node.isEnd;
        }
        
        char ch = word[index];
        if(ch == '.') // 處理萬用字元情況
        {
            // 嘗試所有可能的子節點
            for (int i = 0; i < 26; i++) 
			{
                Trie child = node.children[i];
                // 如果子節點存在且後續字元也匹配，則返回true
                if (child != null && DFS(word, index + 1, child)) 
				{
                    return true;
                }
            }
        }
        else // 處理普通字元情況
        {
            // 計算字元對應的索引
            int childIndex = ch - 'a';
            Trie child = node.children[childIndex];
            // 檢查子節點是否存在，並繼續檢查後續字元
            if (child != null && DFS(word, index + 1, child)) 
			{
                return true;
            }
        }
        // 如果所有可能性都不匹配，返回false
        return false;
    }
}

/// <summary>
/// 字典樹(Trie)實作
/// 解題概念：
/// 1. 字典樹是一種樹形資料結構，專為字串搜尋優化設計
/// 2. 每個節點代表一個字元，從根到葉節點的路徑形成一個單字
/// 3. 共享前綴的單字共享相同的路徑，節省空間
/// 4. 搜尋和插入操作的時間複雜度為 O(m)，其中 m 是單字長度
/// </summary>
public class Trie
{
    /// <summary>
    /// 子節點陣列，每個索引對應一個小寫英文字母 (a-z)
    /// 例如：children[0] 代表字母 'a'，children[1] 代表字母 'b'，以此類推
    /// 屬性使用 {get;} 表示這是一個只讀屬性，建構後不能被修改
    /// 
    /// 注意: 萬用字元 '.', 不會儲存在 children 裡面
    /// 所以這裡的 children 陣列大小是 26
    /// 當遇到比對 萬用字元 '.' 時，會有特別 if 去判斷
    /// 會往後一個 char 繼續比對是否相同.
    /// </summary>
    public Trie[] children{get;}
    
    /// <summary>
    /// 標記當前節點是否是某個單字的結尾
    /// 屬性使用 {get;set;} 表示可以讀取也可以修改
    /// </summary>
    public bool isEnd{get;set;}
    
    /// <summary>
    /// 初始化字典樹節點
    /// </summary>
    public Trie()
    {
        // 建立長度為26的陣列，對應26個小寫英文字母
        children = new Trie[26];
        // 初始時不是單字結尾
        isEnd = false;
    }

    /// <summary>
    /// 將單字插入字典樹中
    /// </summary>
    /// <param name="word">要插入的單字</param>
    public void Insert(string word)
    {
        // 從當前節點開始
        Trie node = this;
        // 遍歷單字的每個字元
        for(int i = 0; i < word.Length; i++)
        {
            char ch = word[i];
            // 計算字元在children陣列中的索引 (0-25)
            int index = ch - 'a';
            // 如果該字元對應的子節點不存在，則建立新節點
            if(node.children[index] == null)
            {
                node.children[index] = new Trie();
            }
            // 移動到下一個節點，繼續處理單字的下一個字元
            node = node.children[index];
        }
        // 單字插入完成後，標記最後一個節點為單字結尾
        node.isEnd = true;
    }
}