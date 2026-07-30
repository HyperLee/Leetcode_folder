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
        var stages = new[]
        {
            new
            {
                Name = "官方基本案例",
                Words = new[] { "bad", "dad", "mad" },
                Searches = new (string Pattern, bool Expected)[]
                {
                    ("pad", false),
                    ("bad", true),
                    (".ad", true),
                    ("b..", true)
                }
            },
            new
            {
                Name = "共享前綴與長度",
                Words = new[] { "apple", "apply" },
                Searches = new (string Pattern, bool Expected)[]
                {
                    ("app", false),
                    ("a..le", true),
                    ("a..ly", true),
                    ("app.", false)
                }
            },
            new
            {
                Name = "前綴成為完整單字",
                Words = new[] { "app", "app" },
                Searches = new (string Pattern, bool Expected)[]
                {
                    ("app", true),
                    ("ap.", true)
                }
            },
            new
            {
                Name = "單字元、長度與失敗分支",
                Words = new[] { "a", "at", "code", "coder" },
                Searches = new (string Pattern, bool Expected)[]
                {
                    (".", true),
                    ("..", true),
                    ("c.de", true),
                    ("c..er", true),
                    ("z.", false)
                }
            }
        };

        WordDictionary trieDictionary = new WordDictionary();
        WordDictionary2 bucketDictionary = new WordDictionary2();
        var solutions = new (string Name, Action<string> AddWord, Func<string, bool> Search)[]
        {
            ("解法一：固定陣列 Trie + DFS", trieDictionary.AddWord, trieDictionary.Search),
            ("解法二：長度分桶 + 逐字比對", bucketDictionary.AddWord, bucketDictionary.Search)
        };

        int passedChecks = 0;
        int totalChecks = 0;

        Console.WriteLine("LeetCode 211：添加與搜尋單字 - 雙解法驗證");

        foreach (var solution in solutions)
        {
            int solutionPassed = 0;
            int solutionTotal = 0;

            Console.WriteLine($"\n{solution.Name}");

            for (int stageIndex = 0; stageIndex < stages.Length; stageIndex++)
            {
                var stage = stages[stageIndex];
                Console.WriteLine($"  階段 {stageIndex + 1}：{stage.Name}");
                Console.WriteLine($"  AddWord: {string.Join(", ", stage.Words)}");

                foreach (string word in stage.Words)
                {
                    solution.AddWord(word);
                }

                foreach (var searchCase in stage.Searches)
                {
                    bool actual = solution.Search(searchCase.Pattern);
                    bool passed = actual == searchCase.Expected;
                    solutionTotal++;
                    totalChecks++;

                    if (passed)
                    {
                        solutionPassed++;
                        passedChecks++;
                    }

                    Console.WriteLine(
                        $"    Search(\"{searchCase.Pattern}\") | Expected: {searchCase.Expected} | " +
                        $"Actual: {actual} => {(passed ? "PASS" : "FAIL")}");
                }
            }

            Console.WriteLine($"  小計：{solutionPassed}/{solutionTotal} 項驗證通過");
        }

        Console.WriteLine($"\n總結：{passedChecks}/{totalChecks} 項驗證通過");
        Console.WriteLine($"Overall: {(passedChecks == totalChecks ? "PASS" : "FAIL")}");

        if (passedChecks != totalChecks)
        {
            Environment.ExitCode = 1;
        }
    }
}

/// <summary>
/// 使用固定 26 個子節點的字典樹實作單字查詢資料結構。
/// 加入單字時共享相同字首的路徑；搜尋普通字元時沿唯一子節點前進，
/// 遇到句點萬用字元時則以深度優先搜尋嘗試目前節點的所有有效分支。
/// </summary>
public class WordDictionary
{
    private readonly Trie _root;

    /// <summary>
    /// 初始化空的固定陣列字典樹。
    /// 建構式不需要輸入；完成後根節點尚未標記任何完整單字，
    /// 可透過 <see cref="AddWord"/> 加入單字並以 <see cref="Search"/> 查詢。
    /// </summary>
    public WordDictionary()
    {
        _root = new Trie();
    }

    /// <summary>
    /// 將只含小寫英文字母的非空單字逐字插入字典樹。
    /// 方法沿既有字首路徑前進並只建立缺少的節點；沒有回傳值，
    /// 完成後最後一個節點會標記為完整單字結尾。
    /// </summary>
    /// <param name="word">要加入的單字，長度介於 1 到 25，且只含小寫英文字母。</param>
    public void AddWord(string word)
    {
        _root.Insert(word);
    }

    /// <summary>
    /// 從根節點以深度優先搜尋判斷是否存在與指定模式完整匹配的單字。
    /// 輸入模式可包含小寫英文字母與最多兩個代表任一單一字元的句點；
    /// 只有走完整個模式且停在單字結尾時才回傳 <see langword="true"/>。
    /// </summary>
    /// <param name="word">要搜尋的完整單字或含句點萬用字元的模式，長度介於 1 到 25。</param>
    /// <returns>存在完整匹配的已加入單字時為 <see langword="true"/>；否則為 <see langword="false"/>。</returns>
    public bool Search(string word)
    {
        return DFS(word, 0, _root);
    }

    /// <summary>
    /// 從指定節點與模式索引開始遞迴比對剩餘字元。
    /// 普通字元只走對應子節點，句點則嘗試所有現存子節點；
    /// 剩餘模式能抵達完整單字結尾時回傳 <see langword="true"/>。
    /// </summary>
    /// <param name="word">完整搜尋模式。</param>
    /// <param name="index">目前要處理的模式索引，範圍從 0 到模式長度。</param>
    /// <param name="node">目前已匹配字首所對應的字典樹節點。</param>
    /// <returns>從目前狀態可完成一條完整匹配路徑時為 <see langword="true"/>；否則為 <see langword="false"/>。</returns>
    private static bool DFS(string word, int index, Trie node)
    {
        if (index == word.Length)
        {
            // 走完模式仍須確認目前位置是完整單字，而不只是較長單字的字首。
            return node.isEnd;
        }

        char ch = word[index];
        if (ch == '.')
        {
            // 句點可以選擇任一現存子節點，任一分支完成匹配即可提早結束。
            for (int childIndex = 0; childIndex < node.children.Length; childIndex++)
            {
                Trie? child = node.children[childIndex];
                if (child != null && DFS(word, index + 1, child))
                {
                    return true;
                }
            }

            return false;
        }

        int exactChildIndex = ch - 'a';
        Trie? exactChild = node.children[exactChildIndex];
        return exactChild != null && DFS(word, index + 1, exactChild);
    }
}

/// <summary>
/// 使用單字長度分桶實作的單字查詢資料結構。
/// 每個長度對應一個不重複單字集合；搜尋時只檢查長度相同的候選，
/// 再逐字判斷普通字元或萬用字元是否匹配。
/// </summary>
public class WordDictionary2
{
    private readonly Dictionary<int, HashSet<string>> _wordsByLength;

    /// <summary>
    /// 初始化空的長度分桶單字資料結構。
    /// 建構式不需要輸入；完成後可透過 <see cref="AddWord"/> 加入單字，
    /// 並透過 <see cref="Search"/> 查詢完整單字或包含萬用字元的模式。
    /// </summary>
    public WordDictionary2()
    {
        _wordsByLength = new Dictionary<int, HashSet<string>>();
    }

    /// <summary>
    /// 將只含小寫英文字母的非空單字加入其長度所對應的集合。
    /// 相同單字重複加入時集合內容不會重複；方法沒有回傳值，
    /// 完成後該單字可由精確模式或合法的萬用字元模式找到。
    /// </summary>
    /// <param name="word">要加入的單字，長度介於 1 到 25，且只含小寫英文字母。</param>
    public void AddWord(string word)
    {
        if (!_wordsByLength.TryGetValue(word.Length, out HashSet<string>? words))
        {
            words = new HashSet<string>();
            _wordsByLength[word.Length] = words;
        }

        words.Add(word);
    }

    /// <summary>
    /// 搜尋是否存在與指定模式完整匹配的單字。
    /// 輸入模式長度介於 1 到 25，可包含小寫英文字母與最多兩個
    /// 代表任一單一字元的句點；方法只掃描同長度分桶，
    /// 找到完整匹配時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。
    /// </summary>
    /// <param name="word">要搜尋的完整單字或含句點萬用字元的模式。</param>
    /// <returns>存在完整匹配的已加入單字時為 <see langword="true"/>；否則為 <see langword="false"/>。</returns>
    public bool Search(string word)
    {
        if (!_wordsByLength.TryGetValue(word.Length, out HashSet<string>? words))
        {
            return false;
        }

        if (!word.Contains('.'))
        {
            return words.Contains(word);
        }

        // 萬用字元不改變長度，因此只需逐一檢查相同長度的候選單字。
        foreach (string candidate in words)
        {
            if (Matches(word, candidate))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 逐字比較搜尋模式與相同長度的候選單字。
    /// 模式中的句點可接受候選位置上的任一小寫英文字母；
    /// 所有位置皆相容時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。
    /// </summary>
    /// <param name="pattern">可包含句點萬用字元的搜尋模式。</param>
    /// <param name="candidate">與模式長度相同、只含小寫英文字母的候選單字。</param>
    /// <returns>候選單字與整個模式匹配時為 <see langword="true"/>；否則為 <see langword="false"/>。</returns>
    private static bool Matches(string pattern, string candidate)
    {
        for (int index = 0; index < pattern.Length; index++)
        {
            if (pattern[index] != '.' && pattern[index] != candidate[index])
            {
                return false;
            }
        }

        return true;
    }
}

/// <summary>
/// 表示固定小寫英文字母表的單一字典樹節點。
/// 節點保存 26 個可能的下一字元位置與完整單字結尾標記；
/// 從根節點沿字元路徑前進即可表示並共享多個單字的相同字首。
/// </summary>
public class Trie
{
    /// <summary>
    /// 取得 26 個子節點位置；索引 0 到 25 分別對應小寫字母
    /// <c>a</c> 到 <c>z</c>。句點是搜尋模式，不會儲存在此陣列。
    /// </summary>
    public Trie[] children { get; }

    /// <summary>
    /// 取得或設定目前節點是否代表某個已加入單字的結尾。
    /// </summary>
    public bool isEnd { get; set; }

    /// <summary>
    /// 初始化沒有子節點且尚未代表單字結尾的字典樹節點。
    /// 建構式不需要輸入；輸出節點包含長度為 26 的空子節點陣列。
    /// </summary>
    public Trie()
    {
        children = new Trie[26];
        isEnd = false;
    }

    /// <summary>
    /// 從目前節點開始將指定單字插入字典樹。
    /// 輸入必須是只含小寫英文字母的非空字串；方法重用既有字首節點、
    /// 建立缺少的路徑，最後將終點標記為完整單字且不回傳值。
    /// </summary>
    /// <param name="word">要插入的單字，長度介於 1 到 25，且只含小寫英文字母。</param>
    public void Insert(string word)
    {
        Trie node = this;

        for (int index = 0; index < word.Length; index++)
        {
            int childIndex = word[index] - 'a';

            // 共享既有字首，只在目前字元尚無路徑時建立新節點。
            if (node.children[childIndex] == null)
            {
                node.children[childIndex] = new Trie();
            }

            node = node.children[childIndex];
        }

        // 相同路徑可能同時是較長單字的字首，必須另外記錄完整單字終點。
        node.isEnd = true;
    }
}