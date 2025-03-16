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
    /// ref:建議看連結圖示說明 比較好初步理解題目需求
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

        /// <summary>
        /// 建立一個雜湊表（Dictionary），用來儲存 key-value 對：
        /// Key: 整數型別 (int)，代表快取中的鍵值
        /// Value: Node 型別，指向雙向鏈結串列中對應的節點

        /// </summary>
        /// <typeparam name="int"></typeparam>
        /// <typeparam name="Node"></typeparam>
        /// <returns></returns>
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
            // 步驟 1: 呼叫 GetNode 取得節點
            // - 如果節點存在，GetNode 會將其移至最前端
            // - 如果不存在，GetNode 會返回 null
            Node node = GetNode(key);
            
            // 步驟 2: 返回結果
            // - 如果節點存在（非 null），返回其值
            // - 如果節點不存在（null），返回 -1
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
            // 步驟 1: 檢查 key 是否存在
            Node node = GetNode(key);
            
            // 步驟 2a: 如果 key 已存在
            if (node != null)
            {
                // - 更新節點的值
                // - GetNode 已經將節點移至最前端
                node.Value = value;
                return;
            }

            // 步驟 2b: 如果 key 不存在
            // - 建立新節點
            node = new Node(key, value);
            // - 將節點加入雜湊表
            _keyToNode[key] = node;
            // - 將節點加入鏈結串列最前端
            PushFront(node);

            // 步驟 3: 檢查容量是否超過上限
            if (_keyToNode.Count > _capacity)
            {
                // - 取得最後一個節點（最久未使用）
                Node backNode = _dummy.Prev;
                // - 從雜湊表中移除
                _keyToNode.Remove(backNode.Key);
                // - 從鏈結串列中移除
                Remove(backNode);
            }
        }

        /// <summary>
        /// 獲取鍵對應的節點，如果存在則將其移到鏈結串列的前端
        /// 
        /// 注意此段程式碼的步驟3.
        /// 先移除再加入, 所以原先存入的順序要改變
        /// 被移除會消失,再加入會讓順序在儲存的Dic裡面的最後面位置
        /// 這行為恰巧就是LRU的行為
        /// </summary>
        private Node GetNode(int key)
        {
            // 步驟 1: 檢查 key 是否存在於雜湊表中
            if (!_keyToNode.ContainsKey(key))
            {
                return null;
            }

            // 步驟 2: 從雜湊表中取得節點
            Node node = _keyToNode[key];

            // 步驟 3: 將節點移到最前端
            // - 從當前位置移除節點
            Remove(node);
            // - 將節點加入最前端
            PushFront(node);

            // 步驟 4: 返回節點
            return node;
        }

        /// <summary>
        /// 從雙向鏈結串列中移除指定節點
        /// </summary>
        private void Remove(Node x)
        {
            // 步驟 1: 將前一個節點的 Next 指向後一個節點
            x.Prev.Next = x.Next;
            // 步驟 2: 將後一個節點的 Prev 指向前一個節點
            x.Next.Prev = x.Prev;
        }

        /// <summary>
        /// 將節點添加到雙向鏈結串列的前端
        /// [前端(最新使用), 後端(久未使用)]
        /// 開頭是 dummy 後面接上各 node
        /// 最前端（最新）的節點總是接在 dummy 節點後面
        /// 最後端（最舊）的節點總是在鏈結串列的尾部
        /// </summary>
        private void PushFront(Node x)
        {
            // 步驟 1: 設定新節點的前後連結
            // - 新節點的前節點指向哨兵節點
            x.Prev = _dummy;
            // - 新節點的後節點指向原本的第一個節點
            x.Next = _dummy.Next;

            // 步驟 2: 更新相鄰節點的連結
            // - 更新哨兵節點的 Next
            // dummy 的下一個變成新節點
            x.Prev.Next = x;
            // - 更新原本第一個節點的 Prev
            x.Next.Prev = x;
        }
    }
}
