namespace leetcode_208
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 208. Implement Trie (Prefix Tree)
        /// https://leetcode.com/problems/implement-trie-prefix-tree/description/
        ///
        /// A trie, pronounced "try", or prefix tree efficiently stores and retrieves keys in a string dataset, with applications such as autocomplete and spellchecking.
        ///
        /// Implement Trie:
        /// - Trie() initializes the object.
        /// - void insert(String word) inserts word.
        /// - boolean search(String word) returns whether word was inserted.
        /// - boolean startsWith(String prefix) returns whether an inserted word starts with prefix.
        ///
        /// Example 1:
        /// Input:
        /// ["Trie","insert","search","search","startsWith","insert","search"]
        /// [[],["apple"],["apple"],["app"],["app"],["app"],["app"]]
        /// Output: [null,null,true,false,true,null,true]
        /// Explanation: Insert "apple"; search("apple") is true, search("app") is false, startsWith("app") is true; insert "app", then search("app") is true.
        ///
        /// Constraints:
        /// - 1 &lt;= word.length, prefix.length &lt;= 2000
        /// - word and prefix contain only lowercase English letters.
        /// - At most 3 * 10^4 total calls are made to insert, search, and startsWith.
        /// </para>
        /// <para>
        /// 208. 實作 Trie（前綴樹）
        /// https://leetcode.cn/problems/implement-trie-prefix-tree/description/
        ///
        /// Trie（讀音同 "try"）或前綴樹，是能高效儲存與擷取字串資料集中鍵值的樹狀資料結構，可用於自動完成與拼字檢查等功能。
        ///
        /// 實作 Trie：
        /// - Trie() 初始化物件。
        /// - void insert(String word) 插入 word。
        /// - boolean search(String word) 回傳 word 是否曾被插入。
        /// - boolean startsWith(String prefix) 回傳是否有已插入的單字以 prefix 開頭。
        ///
        /// 範例 1：
        /// 輸入：
        /// ["Trie","insert","search","search","startsWith","insert","search"]
        /// [[],["apple"],["apple"],["app"],["app"],["app"],["app"]]
        /// 輸出：[null,null,true,false,true,null,true]
        /// 說明：插入 "apple"；search("apple") 為 true、search("app") 為 false、startsWith("app") 為 true；插入 "app" 後，search("app") 為 true。
        ///
        /// 限制條件：
        /// - 1 &lt;= word.length, prefix.length &lt;= 2000
        /// - word 與 prefix 僅含小寫英文字母。
        /// - insert、search、startsWith 的總呼叫次數最多為 3 * 10^4。
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 執行固定的 Trie 操作序列；每組案例都使用獨立實例，逐項比較查詢的預期值與實際值。
        /// 輸入涵蓋官方流程、共享前綴、重複插入與單字元邊界，輸出為案例結果及查詢通過統計。
        /// </summary>
        private static void RunSamples()
        {
            TrieTestCase[] testCases =
            [
                new(
                    "官方範例 - apple 與 app",
                    [
                        TrieOperation.Insert("apple"),
                        TrieOperation.Search("apple", true),
                        TrieOperation.Search("app", false),
                        TrieOperation.StartsWith("app", true),
                        TrieOperation.Insert("app"),
                        TrieOperation.Search("app", true)
                    ]),
                new(
                    "共享前綴與不存在路徑",
                    [
                        TrieOperation.Insert("car"),
                        TrieOperation.Insert("card"),
                        TrieOperation.Insert("care"),
                        TrieOperation.Search("car", true),
                        TrieOperation.Search("ca", false),
                        TrieOperation.StartsWith("ca", true),
                        TrieOperation.StartsWith("cat", false),
                        TrieOperation.Search("card", true),
                        TrieOperation.Search("care", true)
                    ]),
                new(
                    "重複插入同一單字",
                    [
                        TrieOperation.Insert("dog"),
                        TrieOperation.Insert("dog"),
                        TrieOperation.Search("dog", true),
                        TrieOperation.StartsWith("do", true),
                        TrieOperation.Search("dogs", false)
                    ]),
                new(
                    "單字元與不存在分支",
                    [
                        TrieOperation.Insert("a"),
                        TrieOperation.Search("a", true),
                        TrieOperation.StartsWith("a", true),
                        TrieOperation.Search("b", false),
                        TrieOperation.StartsWith("z", false)
                    ])
            ];

            int passedCases = 0;
            int passedQueries = 0;
            int totalQueries = 0;

            Console.WriteLine("LeetCode 208 - Implement Trie (Prefix Tree)");
            Console.WriteLine("解法：每個節點使用 26 格子節點陣列，並以 isEnd 區分完整單字與前綴");
            Console.WriteLine();

            foreach (TrieTestCase testCase in testCases)
            {
                Trie trie = new();
                List<bool> expectedValues = [];
                List<bool> actualValues = [];

                foreach (TrieOperation operation in testCase.Operations)
                {
                    if (operation.Type == TrieOperationType.Insert)
                    {
                        trie.Insert(operation.Value);
                        continue;
                    }

                    bool actual = operation.Type == TrieOperationType.Search
                        ? trie.Search(operation.Value)
                        : trie.StartsWith(operation.Value);
                    bool expected = operation.Expected.GetValueOrDefault();

                    expectedValues.Add(expected);
                    actualValues.Add(actual);
                    totalQueries++;

                    if (actual == expected)
                    {
                        passedQueries++;
                    }
                }

                bool passed = expectedValues.SequenceEqual(actualValues);
                if (passed)
                {
                    passedCases++;
                }

                Console.WriteLine(
                    $"[{(passed ? "PASS" : "FAIL")}] {testCase.Name} | " +
                    $"Expected: [{string.Join(", ", expectedValues)}] | " +
                    $"Actual: [{string.Join(", ", actualValues)}]");
            }

            Console.WriteLine();
            Console.WriteLine(
                $"總結：{passedCases}/{testCases.Length} 組案例通過，" +
                $"{passedQueries}/{totalQueries} 次查詢驗證通過。");
        }

        /// <summary>
        /// 表示可重播的 Trie 操作種類：插入完整單字、搜尋完整單字或檢查既有單字前綴。
        /// </summary>
        private enum TrieOperationType
        {
            Insert,
            Search,
            StartsWith
        }

        /// <summary>
        /// 表示一筆 Trie 操作；輸入值須為長度 1 到 2000 的小寫英文字母字串，
        /// 查詢操作同時保存預期布林結果，插入操作不產生回傳值。
        /// </summary>
        /// <param name="Type">要執行的插入、完整單字搜尋或前綴搜尋。</param>
        /// <param name="Value">要插入或查詢的非空小寫英文字母字串。</param>
        /// <param name="Expected">查詢的預期結果；插入操作為 <see langword="null"/>。</param>
        private sealed record TrieOperation(
            TrieOperationType Type,
            string Value,
            bool? Expected)
        {
            /// <summary>
            /// 建立不包含預期回傳值的插入操作。
            /// </summary>
            /// <param name="word">要插入的非空小寫英文字母單字。</param>
            /// <returns>可供驗證器重播的插入操作。</returns>
            public static TrieOperation Insert(string word) =>
                new(TrieOperationType.Insert, word, null);

            /// <summary>
            /// 建立完整單字搜尋操作，並記錄該單字是否應已被插入。
            /// </summary>
            /// <param name="word">要搜尋的非空小寫英文字母單字。</param>
            /// <param name="expected">該完整單字預期是否存在。</param>
            /// <returns>可供驗證器重播與比對的搜尋操作。</returns>
            public static TrieOperation Search(string word, bool expected) =>
                new(TrieOperationType.Search, word, expected);

            /// <summary>
            /// 建立前綴搜尋操作，並記錄是否應存在以該字串開頭的已插入單字。
            /// </summary>
            /// <param name="prefix">要檢查的非空小寫英文字母前綴。</param>
            /// <param name="expected">預期是否存在符合前綴的單字。</param>
            /// <returns>可供驗證器重播與比對的前綴搜尋操作。</returns>
            public static TrieOperation StartsWith(string prefix, bool expected) =>
                new(TrieOperationType.StartsWith, prefix, expected);
        }

        /// <summary>
        /// 表示一組使用獨立 Trie 執行的具名案例，操作會依陣列順序重播並驗證所有查詢結果。
        /// </summary>
        /// <param name="Name">顯示於主控台的案例名稱。</param>
        /// <param name="Operations">依序執行的插入與查詢操作。</param>
        private sealed record TrieTestCase(string Name, TrieOperation[] Operations);
    }

    /// <summary>
    /// 使用固定 26 格子節點陣列實作 Trie；共享前綴共用路徑，並以 <c>isEnd</c>
    /// 標記路徑是否為已插入的完整單字。所有輸入須為非空小寫英文字母字串。
    /// </summary>
    public class Trie
    {
        /// <summary>
        /// 標記目前節點是否為已插入完整單字的結尾。
        /// </summary>
        private bool isEnd;

        /// <summary>
        /// 保存 26 個小寫字母的可選子節點；索引 0 對應 <c>a</c>，索引 25 對應 <c>z</c>。
        /// </summary>
        private readonly Trie?[] children;

        /// <summary>
        /// 建立空的 Trie 節點；初始不代表任何完整單字，且 26 個子節點都不存在。
        /// </summary>
        public Trie()
        {
            isEnd = false;
            children = new Trie?[26];
        }

        /// <summary>
        /// 將非空小寫英文字母單字插入 Trie；沿既有前綴走訪，只為缺少的字元建立節點，
        /// 最後標記完整單字結尾。此方法不產生回傳值，重複插入不會改變查詢結果。
        /// </summary>
        /// <param name="word">長度 1 到 2000、只含 <c>a</c> 到 <c>z</c> 的單字。</param>
        public void Insert(string word)
        {
            Trie node = this;

            foreach (char character in word)
            {
                int index = character - 'a';
                Trie? child = node.children[index];

                // 共享前綴直接沿用既有節點，只有路徑缺少目前字元時才配置新節點。
                if (child is null)
                {
                    child = new Trie();
                    node.children[index] = child;
                }

                node = child;
            }

            node.isEnd = true;
        }

        /// <summary>
        /// 搜尋非空小寫英文字母單字；先走訪完整路徑，再以結尾標記區分完整單字與較長單字的前綴。
        /// </summary>
        /// <param name="word">長度 1 到 2000、只含 <c>a</c> 到 <c>z</c> 的待搜尋單字。</param>
        /// <returns>路徑存在且曾被標記為完整單字時回傳 <see langword="true"/>；否則回傳 <see langword="false"/>。</returns>
        public bool Search(string word)
        {
            Trie? node = SearchPrefix(word);

            // 路徑存在仍不代表完整單字；例如只插入 apple 時，app 尚未被標記為結尾。
            return node is not null && node.isEnd;
        }

        /// <summary>
        /// 檢查是否有已插入單字以指定非空小寫英文字母前綴開頭；只要求路徑存在，不檢查完整單字標記。
        /// </summary>
        /// <param name="prefix">長度 1 到 2000、只含 <c>a</c> 到 <c>z</c> 的前綴。</param>
        /// <returns>前綴的每個字元路徑都存在時回傳 <see langword="true"/>；否則回傳 <see langword="false"/>。</returns>
        public bool StartsWith(string prefix)
        {
            return SearchPrefix(prefix) is not null;
        }

        /// <summary>
        /// 從目前節點沿指定小寫英文字母前綴逐層走訪，供完整單字搜尋與前綴搜尋共用。
        /// </summary>
        /// <param name="prefix">長度 1 到 2000、只含 <c>a</c> 到 <c>z</c> 的前綴或單字。</param>
        /// <returns>完整路徑存在時回傳最後一個節點；任一字元缺少對應子節點時回傳 <see langword="null"/>。</returns>
        private Trie? SearchPrefix(string prefix)
        {
            Trie node = this;

            foreach (char character in prefix)
            {
                int index = character - 'a';
                Trie? child = node.children[index];

                // 任一段路徑不存在，就不可能形成指定完整單字或前綴。
                if (child is null)
                {
                    return null;
                }

                node = child;
            }

            return node;
        }
    }
}