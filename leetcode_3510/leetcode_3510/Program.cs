namespace leetcode_3510;

class Program
{
    /// <summary>
    /// 雙向鏈結串列節點，用於維護合併過程中的元素。
    /// <para>
    /// 使用雙向鏈結串列的原因：
    /// 1. 合併操作需要快速存取前驅與後繼節點。
    /// 2. 合併後需要重新連接前後節點，鏈結串列支援 O(1) 的刪除與插入。
    /// 3. 不需要直接遍歷陣列，僅需透過節點引用進行操作。
    /// </para>
    /// </summary>
    public class Node
    {
        /// <summary>
        /// 節點當前的數值，初始為原陣列對應位置的值，合併後會更新為合併的總和。
        /// </summary>
        public long value;

        /// <summary>
        /// 節點在原始陣列中的索引位置，用於判斷該節點是否已被合併（惰性刪除）。
        /// </summary>
        public int left;

        /// <summary>
        /// 指向前一個節點的參考，若為首節點則為 null。
        /// </summary>
        public Node? prev;

        /// <summary>
        /// 指向下一個節點的參考，若為末節點則為 null。
        /// </summary>
        public Node? next;

        /// <summary>
        /// 建立一個新的節點。
        /// </summary>
        /// <param name="value">節點的初始數值。</param>
        /// <param name="left">節點在原始陣列中的索引位置。</param>
        public Node(long value, int left)
        {
            this.value = value;
            this.left = left;
        }
    }

    /// <summary>
    /// 優先佇列中的項目，代表一個相鄰數對及其合併成本。
    /// <para>
    /// 實作 <see cref="IComparable{T}"/> 介面，使優先佇列能夠依據以下規則排序：
    /// 1. 優先取出合併成本（數對和）最小的項目。
    /// 2. 若成本相同，則優先取出位置最左邊的數對。
    /// </para>
    /// </summary>
    public class Item : IComparable<Item>
    {
        /// <summary>
        /// 數對中的第一個節點（左側節點）。
        /// </summary>
        public Node first;

        /// <summary>
        /// 數對中的第二個節點（右側節點）。
        /// </summary>
        public Node second;

        /// <summary>
        /// 建立此項目時的數對和（合併成本）。
        /// <para>
        /// 注意：此值為建立時的快照，用於惰性刪除時判斷數對是否仍然有效。
        /// 若節點的值已更新，則此項目為髒資料，應跳過處理。
        /// </para>
        /// </summary>
        public long cost;

        /// <summary>
        /// 建立一個新的優先佇列項目。
        /// </summary>
        /// <param name="first">數對中的第一個節點。</param>
        /// <param name="second">數對中的第二個節點。</param>
        /// <param name="cost">數對的合併成本（兩節點值的總和）。</param>
        public Item(Node first, Node second, long cost)
        {
            this.first = first;
            this.second = second;
            this.cost = cost;
        }

        /// <summary>
        /// 比較兩個項目的優先順序。
        /// </summary>
        /// <param name="other">要比較的另一個項目。</param>
        /// <returns>
        /// 負數表示此項目優先順序較高，正數表示較低，零表示相同。
        /// </returns>
        public int CompareTo(Item? other)
        {
            if (other is null)
            {
                return -1;
            }
            // 優先比較成本，成本較小者優先
            if (cost != other.cost)
            {
                return cost.CompareTo(other.cost);
            }
            // 成本相同時，位置較左者優先
            return first.left.CompareTo(other.first.left);
        }
    }    

    /// <summary>
    /// 3510. Minimum Pair Removal to Sort Array II
    /// https://leetcode.com/problems/minimum-pair-removal-to-sort-array-ii
    ///
    /// English:
    /// Given an array nums, you can perform the following operation any number of times:
    /// - Select the adjacent pair with the minimum sum in nums. If multiple such pairs exist, choose the leftmost one.
    /// - Replace the pair with their sum.
    /// Return the minimum number of operations needed to make the array non-decreasing.
    /// An array is non-decreasing if each element is greater than or equal to its previous element.
    ///
    /// 繁體中文：
    /// 給定一個陣列 nums，你可以重複執行以下操作任意次數：
    /// - 在 nums 中選擇相鄰且和最小的一對；若存在多個，選取最左邊的那一對。
    /// - 用它們的和替換該對（將兩個元素合併為一個元素）。
    /// 回傳使陣列變為非遞減所需的最少操作次數。
    /// 若陣列為非遞減，則對於每個元素都滿足 a[i] >= a[i-1]。
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試範例 1: [5, 2, 3, 1]
        // 預期輸出: 2
        // 說明: [5,2,3,1] -> [5,2,4] -> [5,6] -> 已非遞減，共 2 次操作
        int[] nums1 = [5, 2, 3, 1];
        Console.WriteLine($"範例 1: nums = [{string.Join(", ", nums1)}]");
        Console.WriteLine($"結果: {solution.MinimumPairRemoval(nums1)}");
        Console.WriteLine();

        // 測試範例 2: [1, 2, 2]
        // 預期輸出: 0
        // 說明: 陣列已經是非遞減，不需要任何操作
        int[] nums2 = [1, 2, 2];
        Console.WriteLine($"範例 2: nums = [{string.Join(", ", nums2)}]");
        Console.WriteLine($"結果: {solution.MinimumPairRemoval(nums2)}");
        Console.WriteLine();

        // 測試範例 3: [3, 2, 1]
        // 預期輸出: 1
        // 說明: [3,2,1] -> [3,3] -> 已非遞減，共 1 次操作
        int[] nums3 = [3, 2, 1];
        Console.WriteLine($"範例 3: nums = [{string.Join(", ", nums3)}]");
        Console.WriteLine($"結果: {solution.MinimumPairRemoval(nums3)}");
        Console.WriteLine();

        // 測試範例 4: [1]
        // 預期輸出: 0
        // 說明: 單一元素陣列已經是非遞減
        int[] nums4 = [1];
        Console.WriteLine($"範例 4: nums = [{string.Join(", ", nums4)}]");
        Console.WriteLine($"結果: {solution.MinimumPairRemoval(nums4)}");
    }

    /// <summary>
    /// 計算使陣列變為非遞減所需的最少合併操作次數。
    /// <para>
    /// <b>演算法：優先佇列 + 惰性刪除 + 雙向鏈結串列</b>
    /// </para>
    /// <para>
    /// <b>核心概念：</b><br/>
    /// 1. <b>維護最小相鄰數對和</b>：使用優先佇列存放所有相鄰數對，每次取出和最小的數對進行合併。<br/>
    /// 2. <b>惰性刪除</b>：合併後會產生髒資料（已失效的數對），不立即刪除，而是在取出時判斷是否有效。<br/>
    /// 3. <b>維護遞減計數</b>：追蹤陣列中有多少對相鄰元素是遞減的，當計數為 0 時表示陣列已非遞減。<br/>
    /// 4. <b>雙向鏈結串列</b>：快速存取前驅與後繼節點，支援 O(1) 的刪除與重新連接。
    /// </para>
    /// <para>
    /// <b>時間複雜度</b>：O(n log n)，其中 n 為陣列長度。<br/>
    /// <b>空間複雜度</b>：O(n)，用於儲存節點和優先佇列。
    /// </para>
    /// </summary>
    /// <param name="nums">輸入的整數陣列。</param>
    /// <returns>使陣列變為非遞減所需的最少操作次數。</returns>
    /// <example>
    /// <code>
    /// var solution = new Program();
    /// int result = solution.MinimumPairRemoval([5, 2, 3, 1]); // 回傳 2
    /// </code>
    /// </example>
    public int MinimumPairRemoval(int[] nums)
    {
        // 建立優先佇列，用於維護所有相鄰數對，按照成本（和）排序
        var pq = new PriorityQueue<Item, Item>();

        // 標記陣列，記錄某個位置的元素是否已被合併（用於惰性刪除）
        bool[] merged = new bool[nums.Length];

        // 記錄當前陣列中有多少對相鄰元素是遞減的
        int decreaseCount = 0;

        // 記錄執行的合併操作次數
        int count = 0;

        // 建立雙向鏈結串列的節點列表
        List<Node> nodes = new List<Node>();
        nodes.Add(new Node(nums[0], 0));

        // 初始化：建立雙向鏈結串列並將所有相鄰數對加入優先佇列
        for (int i = 1; i < nums.Length; i++)
        {
            // 建立新節點並連接到前一個節點
            nodes.Add(new Node(nums[i], i));
            nodes[i - 1].next = nodes[i];
            nodes[i].prev = nodes[i - 1];

            // 將相鄰數對加入優先佇列
            var item = new Item(nodes[i - 1], nodes[i], nodes[i - 1].value + nodes[i].value);
            pq.Enqueue(item, item);

            // 統計初始的遞減對數量
            if (nums[i - 1] > nums[i])
            {
                decreaseCount++;
            }
        }

        // 當還存在遞減對時，持續進行合併操作
        while (decreaseCount > 0)
        {
            // 取出成本最小的數對
            var item = pq.Dequeue();
            Node first = item.first;
            Node second = item.second;
            long cost = item.cost;

            // 惰性刪除：檢查該數對是否為髒資料
            // 1. 若任一節點已被合併，則為髒資料
            // 2. 若當前數對和與存入時不同，則為髒資料
            if (merged[first.left] || merged[second.left] || first.value + second.value != cost)
            {
                continue;
            }

            // 執行合併操作
            count++;

            // 若原本 first > second（遞減），合併後該遞減關係消失
            if (first.value > second.value)
            {
                decreaseCount--;
            }

            // 更新雙向鏈結串列：將 second 從鏈結中移除
            Node? prevNode = first.prev;
            Node? nextNode = second.next;
            first.next = nextNode;
            if (nextNode is not null)
            {
                nextNode.prev = first;
            }

            // 處理與前驅節點的關係
            if (prevNode is not null)
            {
                // 檢查 prevNode 與 first 的單調性變化
                // 若從遞減變為非遞減，decreaseCount 減一
                if (prevNode.value > first.value && prevNode.value <= cost)
                {
                    decreaseCount--;
                }
                // 若從非遞減變為遞減，decreaseCount 加一
                else if (prevNode.value <= first.value && prevNode.value > cost)
                {
                    decreaseCount++;
                }

                // 加入新的數對到優先佇列
                var newItem = new Item(prevNode, first, prevNode.value + cost);
                pq.Enqueue(newItem, newItem);
            }

            // 處理與後繼節點的關係
            if (nextNode is not null)
            {
                // 檢查合併後的節點與 nextNode 的單調性變化
                // 若從遞減變為非遞減，decreaseCount 減一
                if (second.value > nextNode.value && cost <= nextNode.value)
                {
                    decreaseCount--;
                }
                // 若從非遞減變為遞減，decreaseCount 加一
                else if (second.value <= nextNode.value && cost > nextNode.value)
                {
                    decreaseCount++;
                }

                // 加入新的數對到優先佇列
                var newItem = new Item(first, nextNode, cost + nextNode.value);
                pq.Enqueue(newItem, newItem);
            }

            // 更新 first 節點的值為合併後的總和
            first.value = cost;

            // 標記 second 節點為已合併
            merged[second.left] = true;
        }

        return count;
    }
}
