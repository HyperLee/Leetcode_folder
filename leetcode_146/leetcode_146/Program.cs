namespace leetcode_146;

class Program
{
    /// <summary>
    /// <para>
    /// 146. LRU Cache
    /// https://leetcode.com/problems/lru-cache/description/
    ///
    /// Design a data structure that follows the constraints of a Least Recently Used (LRU) cache.
    /// Implement the LRUCache class:
    /// - LRUCache(int capacity): Initialize the LRU cache with positive size capacity.
    /// - int get(int key): Return the value of the key if it exists; otherwise, return -1.
    /// - void put(int key, int value): Update the value if the key exists. Otherwise, add the key-value pair. If this makes
    ///   the number of keys exceed capacity, evict the least recently used key.
    /// The functions get and put must each run in O(1) average time complexity.
    ///
    /// Example 1:
    /// Input:
    /// ["LRUCache","put","put","get","put","get","put","get","get","get"]
    /// [[2],[1,1],[2,2],[1],[3,3],[2],[4,4],[1],[3],[4]]
    /// Output: [null,null,null,1,null,-1,null,-1,3,4]
    /// Explanation:
    /// LRUCache lRUCache = new LRUCache(2);
    /// lRUCache.put(1, 1); // cache is {1=1}
    /// lRUCache.put(2, 2); // cache is {1=1, 2=2}
    /// lRUCache.get(1); // return 1
    /// lRUCache.put(3, 3); // key 2 was least recently used; evict it, cache is {1=1, 3=3}
    /// lRUCache.get(2); // return -1 (not found)
    /// lRUCache.put(4, 4); // key 1 was least recently used; evict it, cache is {4=4, 3=3}
    /// lRUCache.get(1); // return -1 (not found)
    /// lRUCache.get(3); // return 3
    /// lRUCache.get(4); // return 4
    ///
    /// Constraints:
    /// - 1 &lt;= capacity &lt;= 3000
    /// - 0 &lt;= key &lt;= 10^4
    /// - 0 &lt;= value &lt;= 10^5
    /// - At most 2 * 10^5 calls will be made to get and put.
    /// </para>
    /// <para>
    /// 146. LRU 快取
    /// https://leetcode.cn/problems/lru-cache/description/
    ///
    /// 設計一個符合最近最少使用（LRU）快取限制的資料結構。
    /// 實作 LRUCache 類別：
    /// - LRUCache(int capacity)：以正整數容量 capacity 初始化 LRU 快取。
    /// - int get(int key)：若 key 存在則回傳其值，否則回傳 -1。
    /// - void put(int key, int value)：若 key 存在則更新其值，否則加入鍵值對。若此操作使鍵的數量超過
    ///   capacity，則淘汰最近最少使用的 key。
    /// get 與 put 函式的平均時間複雜度都必須為 O(1)。
    ///
    /// 範例 1：
    /// 輸入：
    /// ["LRUCache","put","put","get","put","get","put","get","get","get"]
    /// [[2],[1,1],[2,2],[1],[3,3],[2],[4,4],[1],[3],[4]]
    /// 輸出：[null,null,null,1,null,-1,null,-1,3,4]
    /// 解釋：
    /// LRUCache lRUCache = new LRUCache(2);
    /// lRUCache.put(1, 1); // 快取為 {1=1}
    /// lRUCache.put(2, 2); // 快取為 {1=1, 2=2}
    /// lRUCache.get(1); // 回傳 1
    /// lRUCache.put(3, 3); // key 2 最近最少使用，將其淘汰；快取為 {1=1, 3=3}
    /// lRUCache.get(2); // 回傳 -1（找不到）
    /// lRUCache.put(4, 4); // key 1 最近最少使用，將其淘汰；快取為 {4=4, 3=3}
    /// lRUCache.get(1); // 回傳 -1（找不到）
    /// lRUCache.get(3); // 回傳 3
    /// lRUCache.get(4); // 回傳 4
    ///
    /// 限制條件：
    /// - 1 &lt;= capacity &lt;= 3000
    /// - 0 &lt;= key &lt;= 10^4
    /// - 0 &lt;= value &lt;= 10^5
    /// - get 與 put 的呼叫次數合計最多為 2 * 10^5。
    /// </para>
    /// </summary>
    static void Main(string[] args)
    {
        CacheTestCase[] testCases =
        [
            new(
                "官方範例",
                2,
                [
                    CacheOperation.Put(1, 1),
                    CacheOperation.Put(2, 2),
                    CacheOperation.Get(1, 1),
                    CacheOperation.Put(3, 3),
                    CacheOperation.Get(2, -1),
                    CacheOperation.Put(4, 4),
                    CacheOperation.Get(1, -1),
                    CacheOperation.Get(3, 3),
                    CacheOperation.Get(4, 4)
                ]),
            new(
                "更新既有鍵",
                2,
                [
                    CacheOperation.Put(1, 1),
                    CacheOperation.Put(2, 2),
                    CacheOperation.Put(1, 10),
                    CacheOperation.Put(3, 3),
                    CacheOperation.Get(1, 10),
                    CacheOperation.Get(2, -1),
                    CacheOperation.Get(3, 3)
                ]),
            new(
                "容量為 1",
                1,
                [
                    CacheOperation.Put(1, 1),
                    CacheOperation.Get(1, 1),
                    CacheOperation.Put(2, 2),
                    CacheOperation.Get(1, -1),
                    CacheOperation.Get(2, 2)
                ]),
            new(
                "鍵值邊界",
                2,
                [
                    CacheOperation.Put(0, 0),
                    CacheOperation.Put(10000, 100000),
                    CacheOperation.Get(0, 0),
                    CacheOperation.Get(10000, 100000)
                ])
        ];

        (string Name, Func<int, ILruCache> Create)[] solutions =
        [
            ("解法一：手寫雙向鏈結串列", capacity => new LRUCache(capacity)),
            ("解法二：.NET LinkedList<T>", capacity => new LRUCache2(capacity))
        ];

        int passedCases = 0;
        int totalCases = 0;
        int passedGets = 0;
        int totalGets = 0;

        foreach ((string name, Func<int, ILruCache> create) in solutions)
        {
            Console.WriteLine($"=== {name} ===");
            (int solutionPassedCases, int solutionTotalCases, int solutionPassedGets, int solutionTotalGets) =
                RunTestSuite(create, testCases);
            passedCases += solutionPassedCases;
            totalCases += solutionTotalCases;
            passedGets += solutionPassedGets;
            totalGets += solutionTotalGets;
            Console.WriteLine();
        }

        Console.WriteLine($"總結：{passedCases}/{totalCases} 組案例通過，{passedGets}/{totalGets} 次 Get 驗證通過。");
    }

    /// <summary>
    /// 執行同一組 LRU 測試案例，逐案建立指定容量的快取並比較所有 Get 的預期值與實際值。
    /// 輸入的工廠必須能建立遵守 <see cref="ILruCache"/> 契約的快取；回傳案例與查詢的通過統計。
    /// </summary>
    /// <param name="createCache">依容量建立待測 LRU 快取的工廠。</param>
    /// <param name="testCases">包含容量及 Put、Get 操作序列的固定測試案例。</param>
    /// <returns>通過案例數、案例總數、通過 Get 數與 Get 總數。</returns>
    private static (int PassedCases, int TotalCases, int PassedGets, int TotalGets) RunTestSuite(
        Func<int, ILruCache> createCache,
        CacheTestCase[] testCases)
    {
        int passedCases = 0;
        int passedGets = 0;
        int totalGets = 0;

        foreach (CacheTestCase testCase in testCases)
        {
            ILruCache cache = createCache(testCase.Capacity);
            List<int> expectedValues = [];
            List<int> actualValues = [];

            foreach (CacheOperation operation in testCase.Operations)
            {
                if (operation.Type == CacheOperationType.Put)
                {
                    cache.Put(operation.Key, operation.Value);
                    continue;
                }

                int actual = cache.Get(operation.Key);
                expectedValues.Add(operation.Expected);
                actualValues.Add(actual);
                totalGets++;

                if (actual == operation.Expected)
                {
                    passedGets++;
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

        return (passedCases, testCases.Length, passedGets, totalGets);
    }

    /// <summary>
    /// 表示測試案例中的操作種類；Put 寫入鍵值，Get 查詢並比對預期結果。
    /// </summary>
    private enum CacheOperationType
    {
        Get,
        Put
    }

    /// <summary>
    /// 表示一筆可重播的 LRU 操作，保存操作種類、鍵、寫入值及 Get 的預期結果。
    /// </summary>
    /// <param name="Type">要執行的 Get 或 Put 操作。</param>
    /// <param name="Key">題目限制內的快取鍵。</param>
    /// <param name="Value">Put 要寫入的值；Get 操作不使用此欄位。</param>
    /// <param name="Expected">Get 預期回傳的值；Put 操作不使用此欄位。</param>
    private sealed record CacheOperation(
        CacheOperationType Type,
        int Key,
        int Value,
        int Expected)
    {
        /// <summary>
        /// 建立一筆 Put 操作，以指定鍵和值更新快取，不產生查詢結果。
        /// </summary>
        /// <param name="key">要寫入的鍵，範圍為 0 到 10000。</param>
        /// <param name="value">要寫入的值，範圍為 0 到 100000。</param>
        /// <returns>可供測試執行器重播的 Put 操作。</returns>
        public static CacheOperation Put(int key, int value) =>
            new(CacheOperationType.Put, key, value, 0);

        /// <summary>
        /// 建立一筆 Get 操作，以指定鍵查詢快取並記錄預期回傳值。
        /// </summary>
        /// <param name="key">要查詢的鍵，範圍為 0 到 10000。</param>
        /// <param name="expected">鍵存在時為對應值，不存在時為 -1。</param>
        /// <returns>可供測試執行器重播並比對的 Get 操作。</returns>
        public static CacheOperation Get(int key, int expected) =>
            new(CacheOperationType.Get, key, 0, expected);
    }

    /// <summary>
    /// 表示一組獨立的 LRU 測試案例，指定名稱、正容量與依序執行的操作。
    /// </summary>
    /// <param name="Name">顯示於主控台的案例名稱。</param>
    /// <param name="Capacity">快取容量，題目保證介於 1 到 3000。</param>
    /// <param name="Operations">依時間順序執行的 Put、Get 操作。</param>
    private sealed record CacheTestCase(
        string Name,
        int Capacity,
        CacheOperation[] Operations);

    /// <summary>
    /// 定義 LRU 快取的共同操作契約。實作必須在題目保證的正容量與鍵值範圍內，
    /// 讓 Get 與 Put 都達到平均 O(1)，並在查詢或更新後維護最近使用順序。
    /// </summary>
    public interface ILruCache
    {
        /// <summary>
        /// 查詢指定鍵；命中時將該鍵標記為最近使用並回傳其值，未命中時回傳 -1。
        /// </summary>
        /// <param name="key">要查詢的鍵，範圍為 0 到 10000。</param>
        /// <returns>鍵對應的值，若鍵不存在則為 -1。</returns>
        int Get(int key);

        /// <summary>
        /// 寫入或更新指定鍵值；操作後該鍵為最近使用，超過容量時淘汰最久未使用的鍵。
        /// </summary>
        /// <param name="key">要寫入的鍵，範圍為 0 到 10000。</param>
        /// <param name="value">要寫入的值，範圍為 0 到 100000。</param>
        void Put(int key, int value);
    }

    /// <summary>
    /// 使用 Dictionary 與手寫環狀雙向鏈結串列實作 LRU 快取。
    /// Dictionary 以平均 O(1) 定位節點，串列則以 O(1) 移動節點；
    /// 哨兵後方是最近使用項目，哨兵前方是最久未使用項目。
    /// </summary>
    public class LRUCache : ILruCache
    {
        /// <summary>
        /// 儲存一筆快取鍵值及前後節點參考；建立時先自我連結，加入串列後再改接相鄰節點。
        /// </summary>
        private sealed class Node
        {
            public int Key { get; }
            public int Value { get; set; }
            public Node Prev { get; set; }
            public Node Next { get; set; }

            /// <summary>
            /// 建立指定鍵值的獨立節點。鍵和值須符合題目限制，節點初始為安全的自我環狀結構。
            /// </summary>
            /// <param name="key">節點保存的鍵。</param>
            /// <param name="value">節點保存的值。</param>
            public Node(int key, int value)
            {
                Key = key;
                Value = value;
                Prev = this;
                Next = this;
            }
        }

        private readonly int _capacity;
        private readonly Node _dummy = new(0, 0);
        private readonly Dictionary<int, Node> _keyToNode = [];

        /// <summary>
        /// 建立具有指定正容量的 LRU 快取，並初始化空的環狀哨兵串列。
        /// 題目保證容量介於 1 到 3000；建構完成後快取不包含任何鍵值。
        /// </summary>
        /// <param name="capacity">快取可容納的最大鍵數，範圍為 1 到 3000。</param>
        public LRUCache(int capacity)
        {
            _capacity = capacity;
        }

        /// <summary>
        /// 以 Dictionary 查詢指定鍵；命中時把節點移至哨兵後方並回傳值，未命中時回傳 -1。
        /// 輸入鍵範圍為 0 到 10000，平均時間複雜度為 O(1)。
        /// </summary>
        /// <param name="key">要查詢的鍵。</param>
        /// <returns>鍵對應的值，若鍵不存在則為 -1。</returns>
        public int Get(int key)
        {
            Node? node = GetNode(key);
            return node?.Value ?? -1;
        }

        /// <summary>
        /// 寫入或更新指定鍵值並將其移至最近使用位置；新增後若超過容量，
        /// 會同時從 Dictionary 與串列移除哨兵前方的最久未使用節點。
        /// </summary>
        /// <param name="key">要寫入的鍵，範圍為 0 到 10000。</param>
        /// <param name="value">要寫入的值，範圍為 0 到 100000。</param>
        public void Put(int key, int value)
        {
            Node? node = GetNode(key);
            if (node is not null)
            {
                node.Value = value;
                return;
            }

            node = new Node(key, value);
            _keyToNode[key] = node;
            PushFront(node);

            if (_keyToNode.Count <= _capacity)
            {
                return;
            }

            // 哨兵的前一個節點永遠是目前最久未使用的項目。
            Node leastRecentlyUsed = _dummy.Prev;
            _keyToNode.Remove(leastRecentlyUsed.Key);
            Remove(leastRecentlyUsed);
        }

        /// <summary>
        /// 以一次 Dictionary 查詢取得指定鍵的節點；命中時先從原位置移除再放到串列前端。
        /// </summary>
        /// <param name="key">要尋找的鍵。</param>
        /// <returns>已移到最近使用位置的節點；鍵不存在時為 null。</returns>
        private Node? GetNode(int key)
        {
            if (!_keyToNode.TryGetValue(key, out Node? node))
            {
                return null;
            }

            Remove(node);
            PushFront(node);
            return node;
        }

        /// <summary>
        /// 重新連接指定節點的前後鄰居，在 O(1) 時間內將節點移出雙向鏈結串列。
        /// </summary>
        /// <param name="node">目前已位於串列中的非哨兵節點。</param>
        private static void Remove(Node node)
        {
            node.Prev.Next = node.Next;
            node.Next.Prev = node.Prev;
        }

        /// <summary>
        /// 將指定節點插入哨兵後方，使其成為最近使用項目；原本的第一個節點順延。
        /// </summary>
        /// <param name="node">要加入最近使用位置的節點。</param>
        private void PushFront(Node node)
        {
            node.Prev = _dummy;
            node.Next = _dummy.Next;
            _dummy.Next.Prev = node;
            _dummy.Next = node;
        }
    }

    /// <summary>
    /// 使用 .NET LinkedList 與 Dictionary 實作 LRU 快取。
    /// LinkedList 的 First 表示最近使用項目、Last 表示最久未使用項目，
    /// Dictionary 保存鍵到 LinkedListNode 的映射，使查詢、移動與淘汰皆為平均 O(1)。
    /// </summary>
    public class LRUCache2 : ILruCache
    {
        private readonly int _capacity;
        private readonly LinkedList<(int Key, int Value)> _usageOrder = [];
        private readonly Dictionary<int, LinkedListNode<(int Key, int Value)>> _keyToNode = [];

        /// <summary>
        /// 建立具有指定正容量的空 LRU 快取。
        /// 題目保證容量介於 1 到 3000；建構後 LinkedList 與 Dictionary 均為空。
        /// </summary>
        /// <param name="capacity">快取可容納的最大鍵數，範圍為 1 到 3000。</param>
        public LRUCache2(int capacity)
        {
            _capacity = capacity;
        }

        /// <summary>
        /// 以 Dictionary 查詢指定鍵；命中時把對應 LinkedListNode 移至表頭並回傳值，
        /// 未命中時回傳 -1。輸入鍵範圍為 0 到 10000，平均時間複雜度為 O(1)。
        /// </summary>
        /// <param name="key">要查詢的鍵。</param>
        /// <returns>鍵對應的值，若鍵不存在則為 -1。</returns>
        public int Get(int key)
        {
            if (!_keyToNode.TryGetValue(key, out LinkedListNode<(int Key, int Value)>? node))
            {
                return -1;
            }

            MoveToFront(node);
            return node.Value.Value;
        }

        /// <summary>
        /// 寫入或更新指定鍵值並將節點移至 LinkedList 表頭；新增後若超過容量，
        /// 會移除表尾的最久未使用節點及其 Dictionary 映射。
        /// </summary>
        /// <param name="key">要寫入的鍵，範圍為 0 到 10000。</param>
        /// <param name="value">要寫入的值，範圍為 0 到 100000。</param>
        public void Put(int key, int value)
        {
            if (_keyToNode.TryGetValue(key, out LinkedListNode<(int Key, int Value)>? node))
            {
                node.Value = (key, value);
                MoveToFront(node);
                return;
            }

            LinkedListNode<(int Key, int Value)> newNode = _usageOrder.AddFirst((key, value));
            _keyToNode[key] = newNode;

            if (_keyToNode.Count <= _capacity)
            {
                return;
            }

            // LinkedList 尾端保存目前最久未使用的項目。
            LinkedListNode<(int Key, int Value)> leastRecentlyUsed = _usageOrder.Last!;
            _usageOrder.RemoveLast();
            _keyToNode.Remove(leastRecentlyUsed.Value.Key);
        }

        /// <summary>
        /// 將已存在於 LinkedList 的節點移至表頭，使其成為最近使用項目。
        /// </summary>
        /// <param name="node">要更新使用順序的 LinkedList 節點。</param>
        private void MoveToFront(LinkedListNode<(int Key, int Value)> node)
        {
            _usageOrder.Remove(node);
            _usageOrder.AddFirst(node);
        }
    }
}