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
    /// 实现 Trie (前缀树)
    /// 
    /// ref:
    /// https://leetcode.cn/problems/implement-trie-prefix-tree/solutions/717239/shi-xian-trie-qian-zhui-shu-by-leetcode-ti500/
    /// https://leetcode.cn/problems/implement-trie-prefix-tree/solutions/2993894/cong-er-cha-shu-dao-er-shi-liu-cha-shu-p-xsj4/
    /// Copilot 產生的程式碼與註解
    /// </summary>
    public class Trie
    {
        // 表示当前节点是否是一个单词的结束节点
        private bool isEnd;
        // 子节点
        private Trie[] children;


        /// <summary>
        /// Initialize your data structure here.
        /// </summary>
        public Trie()
        {
            // 初始化
            isEnd = false;
            // 26 个字母
            children = new Trie[26];
        }


        /// <summary>
        /// Inserts a word into the trie.
        /// </summary>
        /// <param name="word"></param>
        public void Insert(string word)
        {
            Trie node = this;
            int length = word.Length;
            for(int i = 0; i < length; i++)
            {
                char c = word[i];
                // 如果当前节点的子节点中不包含当前字符，则创建一个新的子节点
                int index = c - 'a';
                if (node.children[index] == null)
                {
                    // 创建一个新的子节点
                    node.children[index] = new Trie();
                }
                node = node.children[index];
            }
            node.isEnd = true;
        }


        /// <summary>
        /// Returns if the word is in the trie.
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool Search(string word)
        {
            // 搜索前缀，如果前缀存在，且是一个单词的结束节点，则返回 true
            Trie node = SearchPrefix(word);
            // 如果前缀存在，且是一个单词的结束节点，则返回 true
            return node != null && node.isEnd;
        }


        /// <summary>
        /// Returns if there is any word in the trie that starts with the given prefix.
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public bool StartsWith(string prefix)
        {
            // 搜索前缀，如果前缀存在，则返回 true
            Trie node = SearchPrefix(prefix);
            // 如果前缀存在，则返回 true
            return node != null;
        }


        /// <summary>
        /// 搜索前缀
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        private Trie SearchPrefix(string prefix)
        {
            Trie node = this;
            int length = prefix.Length;
            for(int i = 0; i < length; i++)
            {
                char c = prefix[i];
                int index = c - 'a';
                if (node.children[index] == null)
                {
                    return null;
                }
                node = node.children[index];
            }

            return node;
        }
    }

}
