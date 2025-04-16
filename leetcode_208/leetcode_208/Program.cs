namespace leetcode_208
{
    internal class Program
    {
        /// <summary>
        /// 208. Implement Trie (Prefix Tree)
        /// https://leetcode.com/problems/implement-trie-prefix-tree/description/
        /// 
        /// 208. 实现 Trie (前缀树)
        /// https://leetcode.cn/problems/implement-trie-prefix-tree/description/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Trie obj = new Trie();
            obj.Insert("apple");

            bool param_2 = obj.Search("apple");
            Console.WriteLine("param_2: " + param_2);

            bool param_3 = obj.StartsWith("app");
            Console.WriteLine("param_3: " + param_3);

            bool param_4 = obj.Search("app");
            Console.WriteLine("param_4: " + param_4);

            obj.Insert("app");

            bool param_5 = obj.Search("app");
            Console.WriteLine("param_5: " + param_5);
        }

    }

    /// <summary>
    /// 實現 Trie (前綴樹)
    /// 
    /// 解題想法與概念：
    /// 1. Trie (發音為 "try") 是一種特殊的樹形資料結構，專門用於儲存和檢索字串集合
    /// 2. 核心特點：
    ///    - 節點不儲存完整字串，而是以字元為基礎構建路徑
    ///    - 共享前綴的字串共享相同的節點路徑，節省空間
    ///    - 查詢時間複雜度為 O(m)，其中 m 是字串長度，不受已儲存字串數量影響
    /// 3. 實現方式：
    ///    - 每個節點有 26 個子節點指針（對應 26 個英文小寫字母）
    ///    - 使用 isEnd 標記表示某條路徑是否構成一個完整的單字
    ///    - 利用字元減去 'a' 計算索引位置，確保快速存取
    /// 
    /// 優勢：
    /// - 前綴查詢高效（複雜度 O(m)，m 為前綴長度）
    /// - 適合實現自動完成、拼寫檢查等功能
    /// 
    /// 時間複雜度：
    /// - 插入：O(m)，m 為字串長度
    /// - 搜尋：O(m)，m 為字串長度
    /// - 前綴搜尋：O(m)，m 為前綴長度
    /// 
    /// 空間複雜度：
    /// - O(T)，T 是所有字串的總字元數
    /// 
    /// ref:
    /// https://leetcode.cn/problems/implement-trie-prefix-tree/solutions/717239/shi-xian-trie-qian-zhui-shu-by-leetcode-ti500/
    /// https://leetcode.cn/problems/implement-trie-prefix-tree/solutions/2993894/cong-er-cha-shu-dao-er-shi-liu-cha-shu-p-xsj4/
    /// </summary>
    public class Trie
    {
        /// <summary>
        /// 標記當前節點是否代表一個完整單字的結尾
        /// 例如: 插入 "apple" 後，路徑 a->p->p->l->e 的最後一個節點 e 的 isEnd 為 true
        /// </summary>
        private bool isEnd;
        
        /// <summary>
        /// 子節點陣列，索引對應 26 個小寫字母 (children[0] 代表 'a'，children[1] 代表 'b'...)
        /// 若 children[i] 不為 null，表示當前節點有對應的子節點
        /// </summary>
        private Trie[] children;


        /// <summary>
        /// Trie 的建構函式，初始化前綴樹資料結構
        /// </summary>
        public Trie()
        {
            // 初始化節點，預設不是任何單字的結尾
            isEnd = false;
            // 建立長度為 26 的陣列，對應 26 個英文小寫字母的可能子節點
            children = new Trie[26];
        }


        /// <summary>
        /// 將單字插入到 Trie 前綴樹中
        /// </summary>
        /// <param name="word">要插入的單字</param>
        public void Insert(string word)
        {
            // 從根節點開始遍歷
            Trie node = this;
            int length = word.Length;
            
            // 逐字元處理單字
            for(int i = 0; i < length; i++)
            {
                char c = word[i];
                // 計算當前字元對應的陣列索引 (a->0, b->1, ..., z->25)
                int index = c - 'a';
                
                // 如果當前節點的子節點中不包含當前字元，則建立一個新的子節點
                if (node.children[index] == null)
                {
                    node.children[index] = new Trie();
                }
                
                // 移動到子節點，繼續處理下一個字元
                node = node.children[index];
            }
            
            // 標記單字結束位置
            node.isEnd = true;
        }


        /// <summary>
        /// 在 Trie 中搜尋完整單字
        /// Search 除了路徑存在外，還要求是完整單字的結尾
        /// 例如: 插入 "apple" 後，"apple" 返回 true，但 "app" 返回 false
        /// </summary>
        /// <param name="word">要搜尋的單字</param>
        /// <returns>單字是否存在於 Trie 中</returns>
        public bool Search(string word)
        {
            // 呼叫 SearchPrefix 方法先找到這個單字的前綴路徑
            Trie node = SearchPrefix(word);
            // 只有當路徑存在 (node != null) 且最後一個節點標記為單字結尾 (node.isEnd) 時才返回 true
            // 這確保了我們找到的是一個完整單字，而不只是某個更長單字的前綴
            return node != null && node.isEnd;
        }


        /// <summary>
        /// 檢查 Trie 中是否有任何單字以給定前綴開頭
        /// StartsWith 只關心路徑是否存在，不需要檢查是否為完整單字
        /// 例如: 插入 "apple" 後，"app" 是 "apple" 的前綴，返回 true
        /// </summary>
        /// <param name="prefix">要檢查的前綴</param>
        /// <returns>是否存在以給定前綴開頭的單字</returns>
        public bool StartsWith(string prefix)
        {
            // 呼叫 SearchPrefix 方法尋找前綴路徑
            Trie node = SearchPrefix(prefix);
            // 只要前綴路徑存在（不論是否為完整單字的結尾），即返回 true
            return node != null;
        }


        /// <summary>
        /// 在 Trie 中搜尋前綴路徑
        /// 這是一個輔助方法，被 Search 和 StartsWith 方法呼叫
        /// </summary>
        /// <param name="prefix">要搜尋的前綴</param>
        /// <returns>如果前綴存在，返回對應的節點；否則返回 null</returns>
        private Trie SearchPrefix(string prefix)
        {
            // 從根節點開始搜尋
            Trie node = this;
            int length = prefix.Length;
            
            // 逐字元遍歷前綴
            for(int i = 0; i < length; i++)
            {
                char c = prefix[i];
                // 計算當前字元對應的陣列索引
                int index = c - 'a';
                
                // 如果在當前路徑上找不到對應字元的子節點，表示前綴不存在
                if (node.children[index] == null)
                {
                    return null;
                }
                
                // 移動到子節點，繼續搜尋
                node = node.children[index];
            }

            // 返回前綴最後一個字元對應的節點
            // 如果想確認這是一個完整單字，還需要檢查 isEnd 屬性
            return node;
        }
    }

}
