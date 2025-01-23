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
            //Console.WriteLine("Hello, World!");

            Trie obj = new Trie();
            obj.Insert("apple");
            bool param_2 = obj.Search("apple");
            bool param_3 = obj.StartsWith("app");

            Console.WriteLine("param_2: " + param_2);
            Console.WriteLine("param_3: " + param_3);
        }

    }


    public class Trie
    {
        // 表示当前节点是否是一个单词的结束节点
        private bool isEnd;
        // 子节点
        private IDictionary<char, Trie> children;


        /// <summary>
        /// Initialize your data structure here.
        /// </summary>
        public Trie()
        {
            isEnd = false;
            children = new Dictionary<char, Trie>();
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
                // 如果当前节点的子节点中不包含当前字符，则添加
                node.children.TryAdd(c, new Trie());
                // 移动到子节点
                node = node.children[c];
            }
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
                // 如果当前节点的子节点中不包含当前字符，则返回 null
                if (!node.children.ContainsKey(c))
                {
                    return null;
                }
                // 移动到子节点
                node = node.children[c];
            }

            return node;
        }
    }

}
