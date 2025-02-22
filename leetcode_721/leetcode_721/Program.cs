namespace leetcode_721
{
    internal class Program
    {
        /// <summary>
        /// 721. Accounts Merge
        /// https://leetcode.com/problems/accounts-merge/description/
        /// 721. 账户合并
        /// https://leetcode.cn/problems/accounts-merge/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var accounts = new List<IList<string>>
            {
                new List<string> { "John", "johnsmith@mail.com", "john_newyork@mail.com" },
                new List<string> { "John", "johnsmith@mail.com", "john00@mail.com" },
                new List<string> { "Mary", "mary@mail.com" },
                new List<string> { "John", "johnnybravo@mail.com" }
            };

            var result = AccountsMerge(accounts);

            foreach (var item in result)
            {
                Console.WriteLine(string.Join(",", item));
            }

        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/accounts-merge/solutions/2844186/ha-xi-biao-dfspythonjavacgojsrust-by-end-z9nh/
        /// https://leetcode.cn/problems/accounts-merge/solutions/564305/zhang-hu-he-bing-by-leetcode-solution-3dyq/
        /// https://leetcode.cn/problems/accounts-merge/solutions/2421912/721-zhang-hu-he-bing-by-stormsunshine-bnu4/
        /// 
        /// 把每個帳戶的信箱視作一個節點，如果兩個帳戶有共同的信箱，則這兩個節點之間有一條邊。
        /// 把題目視作一個無向圖，每個節點代表一個信箱，每條邊代表兩個信箱之間有共同的帳戶。
        /// 使用DFS遍歷圖，找到所有連通分量，即為一個帳戶。   
        /// 
        /// 也可以使用並查集
        /// 這邊先使用 深度優先搜索
        /// 
        /// 構建郵件地址到帳戶索引的映射 (emailToIdx)。
        /// 使用 DFS 遍歷相關帳戶，收集唯一的郵件地址。
        /// 對收集到的郵件地址排序，加入帳戶名稱，生成結果。
        /// 
        /// 執行過程
        /// 1. 構建 emailToIdx：
        ///  "johnsmith@mail.com": [0, 2]
        ///  "john00@mail.com": [0]
        ///  "johnnybravo@mail.com": [1]
        ///  "john_newyork@mail.com": [2]
        ///  "mary@mail.com": [3]
        /// 2. 遍歷所有帳戶：
        ///  i = 0：未訪問，DFS 收集 "johnsmith@mail.com", "john00@mail.com",
        ///  "john_newyork@mail.com"（透過 "johnsmith@mail.com" 連接到索引 2）。
        ///  i = 1：未訪問，DFS 收集 "johnnybravo@mail.com"。
        ///  i = 2：已訪問，跳過。
        ///  i = 3：未訪問，DFS 收集 "mary@mail.com"。
        /// 3. 結果：
        ///  [["John", "john00@mail.com", "john_newyork@mail.com", "johnsmith@mail.com"],["John", "johnnybravo@mail.com"], ["Mary", "mary@mail.com"]]
        ///  
        /// 注意重點之一是，我們需要將郵件地址按字母順序排序(ASCII 編碼順序)，以便在最終答案中按字母順序顯示。
        /// string.CompareOrdinal 使用 字典順序 (ASCII 編碼順序) 來比較字串，這樣能確保 john00@mail.com 在 johnsmith@mail.com 之前，符合 LeetCode 要求。
        /// StringComparer.Ordinal 是用於按 ASCII 碼順序比較字符串的方法，這與文化無關，能夠避免因為不同語言環境導致的字母順序問題。
        /// 
        /// 這裡使用了 string.CompareOrdinal 作為比較方法，這是 逐字節的排序（ordinal sorting），它的特點是：
        /// 1.基於字符的 Unicode 值（code point）進行比較，不考慮文化規則
        /// 2.大小寫敏感：大寫字母（A-Z）的 Unicode 值小於小寫字母（a-z）
        /// 3.結果一致：無論在什麼語言環境下運行，排序結果都是相同的
        /// 4.符合大多數編程問題（包括 LeetCode）的期望，即簡單的字典序
        /// 
        /// string.CompareOrdinal 的工作原理
        /// 它直接比較兩個字符串的 Unicode 值，從左到右逐個字符比較
        /// 
        /// </summary>
        /// <param name="accounts"></param>
        /// <returns></returns>
        public static IList<IList<string>> AccountsMerge(IList<IList<string>> accounts)
        {
            // emailToIdx 用來存儲郵件地址到帳戶索引的映射
            // key: 郵件地址
            // Value: 是一個整數列表 (List<int>)，代表該電子郵件地址出現在 accounts 列表中的哪些索引位置。
            // 例如，若 "johnsmith@mail.com" 出現在第 0 和第 2 個帳戶，則 emailToIdx["johnsmith@mail.com"] = [0, 2]。
            Dictionary<string, List<int>> emailToIdx = new Dictionary<string, List<int>>();

            // 構建 email 到帳戶索引的映射
            // 遍歷所有帳戶
            for (int i = 0; i < accounts.Count; i++)
            {
                // 遍歷每個帳戶的所有郵件地址
                // 從 1 開始（跳過名稱），遍歷每個帳戶的郵件地址。
                for (int k = 1; k < accounts[i].Count; k++)
                {
                    // 如果 emailToIdx 中不存在該郵件地址，則創建一個新的列表
                    if (!emailToIdx.ContainsKey(accounts[i][k]))
                    {
                        // 這個郵件地址對應的帳戶索引列表
                        // 同一個mail可能會出現在好幾個地方(index), 用list來儲存多組資料
                        emailToIdx[accounts[i][k]] = new List<int>();
                    }

                    // 將該帳戶索引加入到 emailToIdx 中
                    // 將當前帳戶的索引 i 添加到與當前電子郵件地址關聯的列表中。
                    // 這表示該電子郵件地址出現在 accounts 列表的第 i 個帳戶中。
                    emailToIdx[accounts[i][k]].Add(i);
                }
            }

            // 結果列表;儲存最終合併結果的列表，每個元素是一個合併後的帳戶。
            List<IList<string>> ans = new List<IList<string>>();
            // 訪問標記陣列, 預設為 false
            bool[] vis = new bool[accounts.Count];
            // 使用 HashSet 收集 DFS 中訪問到的郵件地址
            HashSet<string> emailSet = new HashSet<string>();

            // 遍歷所有帳戶
            for (int i = 0; i < accounts.Count; i++)
            {
                if (vis[i])
                {
                    // 如果該帳戶已訪問過，跳過（避免重複處理）。
                    continue;
                }

                // 清空 emailSet，準備收集當前合併組的郵件地址。
                emailSet.Clear();
                // 遞歸遍歷;從當前帳戶 i 開始，收集所有相關的郵件地址。
                Dfs(i, accounts, emailToIdx, vis, emailSet);

                // 將 emailSet 轉為 List 並排序
                List<string> res = new List<string>(emailSet);
                // 對郵件地址按字母順序排序。 **修正排序方式**
                res.Sort(string.CompareOrdinal);
                // 將當前帳戶的名稱 (索引 0 的元素) 插入到 res 列表的開頭。
                res.Insert(0, accounts[i][0]); 

                ans.Add(res);
            }

            return ans;
        }


        /// <summary>
        /// 深度優先搜索
        /// 遞歸作用：DFS 會遍歷所有與當前帳戶透過郵件地址關聯的其他帳戶，收集所有相關郵件地址。
        /// </summary>
        /// <param name="i">當前帳戶的索引。</param>
        /// <param name="accounts">帳戶列表。</param>
        /// <param name="emailToIdx">郵件地址到帳戶索引的映射。</param>
        /// <param name="vis">訪問標記陣列。</param>
        /// <param name="emailSet">收集郵件地址的集合。</param>
        private static void Dfs(int i, IList<IList<string>> accounts, Dictionary<string, List<int>> emailToIdx, bool[] vis, HashSet<string> emailSet)
        {
            // 將當前帳戶標記為已訪問，避免重複處理。
            vis[i] = true;

            // 遍歷當前帳戶的所有郵件地址（從索引 1 開始，因為 0 是名稱）
            for (int k = 1; k < accounts[i].Count; k++)
            {
                string email = accounts[i][k];
                // 如果該電子郵件地址已經在 emailSet 中，則跳過 (避免重複)。
                if (emailSet.Contains(email))
                {
                    continue;
                }

                // 將郵件地址加入到 emailSet 中
                emailSet.Add(email);

                // 遍歷所有包含該郵件地址的帳戶索引
                foreach (int j in emailToIdx[email])
                {
                    // 如果該帳戶尚未被訪問，則遞迴呼叫 Dfs 函式，繼續合併相關的帳戶。
                    if (!vis[j])
                    {
                        Dfs(j, accounts, emailToIdx, vis, emailSet);
                    }
                }
            }
        }
    }
}
