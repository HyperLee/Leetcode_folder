namespace leetcode_146;

class Program
{
    /// <summary>
    /// 146. LRU Cache
    /// https://leetcode.com/problems/lru-cache/description/
    /// 146. LRU 缓存
    /// https://leetcode.cn/problems/lru-cache/description/
    /// 
    /// 快取檔案置換機制 相關知識
    /// https://en.wikipedia.org/wiki/Cache_replacement_policies#LRU
    /// https://zh.wikipedia.org/zh-tw/%E5%BF%AB%E5%8F%96%E6%96%87%E4%BB%B6%E7%BD%AE%E6%8F%9B%E6%A9%9F%E5%88%B6
    /// https://ithelp.ithome.com.tw/articles/10244749
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試案例
        LRUCache lRUCache = new LRUCache(2);
        lRUCache.Put(1, 1);               // cache is {1=1}
        lRUCache.Put(2, 2);               // cache is {1=1, 2=2}
        Console.WriteLine(lRUCache.Get(1));       // 返回 1
        lRUCache.Put(3, 3);               // LRU key was 2, evicts key 2, cache is {1=1, 3=3}
        Console.WriteLine(lRUCache.Get(2));       // 返回 -1 (未找到)
        lRUCache.Put(4, 4);               // LRU key was 1, evicts key 1, cache is {4=4, 3=3}
        Console.WriteLine(lRUCache.Get(1));       // 返回 -1 (未找到)
        Console.WriteLine(lRUCache.Get(3));       // 返回 3
        Console.WriteLine(lRUCache.Get(4));       // 返回 4
    }

    /// <summary>
    /// LRU (Least Recently Used) 是一種快取置換演算法，基於「最近使用的資料更可能被再次使用」的原則。
    /// 資料結構設計
    /// 結合兩種資料結構來達到 O(1) 的時間複雜度：
    /// 1. 雙向鏈結串列 (Doubly Linked List)
    /// 用於維護資料的使用順序
    /// 最前面的節點 = 最近使用
    /// 最後面的節點 = 最久未使用
    /// 支援 O(1) 時間的節點移動和刪除
    /// 2. 雜湊表 (Dictionary)
    /// 儲存 key 到節點的映射
    /// 支援 O(1) 時間的查詢操作
    /// 
    /// LRU (Least Recently Used) 快取實作
    /// 使用雙向鏈結串列 + 雜湊表(Dictionary)的組合來實現O(1)的存取時間複雜度
    /// - 雙向鏈結串列用於維護資料的使用順序，最近使用的在前面，最少使用的在後面
    /// - 雜湊表用於實現O(1)時間的查詢操作
    /// 空間複雜度：O(capacity)
    /// 
    /// ref:
    /// https://leetcode.cn/problems/lru-cache/solutions/2456294/tu-jie-yi-zhang-tu-miao-dong-lrupythonja-czgt/
    /// https://leetcode.cn/problems/lru-cache/solutions/259678/lruhuan-cun-ji-zhi-by-leetcode-solution/
    /// https://leetcode.cn/problems/lru-cache/solutions/1449572/by-stormsunshine-d7c6/
    /// https://ithelp.ithome.com.tw/articles/10244749
    /// </summary>
    public class LRUCache 
    {
        /// <summary>
        /// 雙向鏈結串列的節點類別
        /// 儲存鍵值對及前後節點的參考
        /// </summary>
        private class Node
        {
            public int Key { get; set; }
            public int Value { get; set; }
            public Node Prev { get; set; }
            public Node Next { get; set; }

            public Node(int key, int value)
            {
                Key = key;
                Value = value;
            }
        }

        private readonly int _capacity;
        // 使用哨兵節點 (_dummy) 簡化鏈結串列的操作
        private readonly Node _dummy = new Node(0, 0); // 哨兵節點
        private readonly Dictionary<int, Node> _keyToNode = new Dictionary<int, Node>();

        public LRUCache(int capacity)
        {
            _capacity = capacity;
            _dummy.Prev = _dummy;
            _dummy.Next = _dummy;
        }

        /// <summary>
        /// 從快取中獲取值，如果鍵存在則同時將其移動到最前面（最近使用）
        /// 
        /// 若 key 存在：
        /// 1. 取得對應節點
        /// 2. 將節點移到最前面
        /// 3. 返回節點的值
        /// 否則：
        /// 返回 -1
        /// </summary>
        /// <param name="key">要查詢的鍵值</param>
        /// <returns>如果鍵存在則返回對應的值，否則返回-1</returns>
        public int Get(int key)
        {
            Node node = GetNode(key); // GetNode 會將對應節點移到鏈結串列頭部
            return node != null ? node.Value : -1;
        }

        /// <summary>
        /// 將鍵值對放入快取中
        /// 如果鍵已存在，則更新其值
        /// 如果鍵不存在，則插入新的鍵值對
        /// 當快取容量已滿時，刪除最久未使用的項目
        /// 
        /// 若 key 已存在：
        /// 1. 更新節點的值
        /// 2. 將節點移到最前面
        /// 否則：
        /// 1. 建立新節點
        /// 2. 加入雜湊表
        /// 3. 放到鏈結串列最前面
        /// 4. 如果超過容量，刪除最後一個節點
        /// </summary>
        /// <param name="key">要插入的鍵</param>
        /// <param name="value">要插入的值</param>
        public void Put(int key, int value)
        {
            Node node = GetNode(key);
            if (node != null)
            {
                node.Value = value; // 更新值
                return;
            }
            node = new Node(key, value); // 新節點
            _keyToNode[key] = node;
            PushFront(node); // 放到最前面
            if (_keyToNode.Count > _capacity)
            {
                Node backNode = _dummy.Prev;
                _keyToNode.Remove(backNode.Key);
                Remove(backNode); // 移除最後一個節點
            }
        }

        /// <summary>
        /// 獲取鍵對應的節點，如果存在則將其移到鏈結串列的前端
        /// </summary>
        private Node GetNode(int key)
        {
            if (!_keyToNode.ContainsKey(key))
            {
                return null;
            }
            Node node = _keyToNode[key];
            Remove(node);
            PushFront(node);
            return node;
        }

        /// <summary>
        /// 從雙向鏈結串列中移除指定節點
        /// </summary>
        private void Remove(Node x)
        {
            x.Prev.Next = x.Next;
            x.Next.Prev = x.Prev;
        }

        /// <summary>
        /// 將節點添加到雙向鏈結串列的前端
        /// </summary>
        private void PushFront(Node x)
        {
            x.Prev = _dummy;
            x.Next = _dummy.Next;
            x.Prev.Next = x;
            x.Next.Prev = x;
        }
    }
}
