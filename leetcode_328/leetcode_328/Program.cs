namespace leetcode_328;

class Program
{
    public class ListNode
    {
        public int val;
        public ListNode? next;
        public ListNode(int val = 0, ListNode? next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    /// <summary>
    /// 328. Odd Even Linked List
    /// https://leetcode.com/problems/odd-even-linked-list/
    /// 328. 奇偶鏈表
    /// https://leetcode.cn/problems/odd-even-linked-list/description/
    /// 
    /// 給定一個單向鏈表的頭節點，請將所有位於奇數索引的節點聚集在一起，接著接上所有位於偶數索引的節點，並返回重新排序後的鏈表。
    /// 第一個節點視為奇數，第二個節點視為偶數，依此類推。
    /// 注意，在奇數組與偶數組內節點的相對順序應保持與輸入相同。
    /// 要求使用 O(1) 的額外空間複雜度和 O(n) 的時間複雜度。
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        var program = new Program();

        // 測試範例 1: [1,2,3,4,5] -> [1,3,5,2,4]
        var head1 = BuildLinkedList([1, 2, 3, 4, 5]);
        Console.WriteLine("測試 1:");
        Console.Write("輸入: ");
        PrintLinkedList(head1);
        var result1 = program.OddEvenList(head1);
        Console.Write("輸出: ");
        PrintLinkedList(result1);
        Console.WriteLine();

        // 測試範例 2: [2,1,3,5,6,4,7] -> [2,3,6,7,1,5,4]
        var head2 = BuildLinkedList([2, 1, 3, 5, 6, 4, 7]);
        Console.WriteLine("測試 2:");
        Console.Write("輸入: ");
        PrintLinkedList(head2);
        var result2 = program.OddEvenList(head2);
        Console.Write("輸出: ");
        PrintLinkedList(result2);
        Console.WriteLine();

        // 測試範例 3: 空鏈表
        ListNode? head3 = null;
        Console.WriteLine("測試 3 (空鏈表):");
        Console.Write("輸入: ");
        PrintLinkedList(head3);
        var result3 = program.OddEvenList(head3);
        Console.Write("輸出: ");
        PrintLinkedList(result3);
        Console.WriteLine();

        // 測試範例 4: 單一節點 [1] -> [1]
        var head4 = BuildLinkedList([1]);
        Console.WriteLine("測試 4 (單一節點):");
        Console.Write("輸入: ");
        PrintLinkedList(head4);
        var result4 = program.OddEvenList(head4);
        Console.Write("輸出: ");
        PrintLinkedList(result4);
    }

    /// <summary>
    /// 根據整數陣列建構鏈表
    /// </summary>
    /// <param name="values">整數陣列</param>
    /// <returns>鏈表的頭節點</returns>
    private static ListNode? BuildLinkedList(int[] values)
    {
        if (values.Length == 0)
        {
            return null;
        }

        var head = new ListNode(values[0]);
        var current = head;
        for (int i = 1; i < values.Length; i++)
        {
            current.next = new ListNode(values[i]);
            current = current.next;
        }

        return head;
    }

    /// <summary>
    /// 印出鏈表內容
    /// </summary>
    /// <param name="head">鏈表的頭節點</param>
    private static void PrintLinkedList(ListNode? head)
    {
        if (head is null)
        {
            Console.WriteLine("[]");
            return;
        }

        var values = new List<int>();
        var current = head;
        while (current is not null)
        {
            values.Add(current.val);
            current = current.next;
        }

        Console.WriteLine($"[{string.Join(", ", values)}]");
    }

    /// <summary>
    /// 奇偶鏈表重排 - 分離節點後合併
    /// <para>
    /// 解題思路：將奇數位置節點和偶數位置節點分離成兩個獨立鏈表，
    /// 最後將偶數鏈表連接在奇數鏈表之後。
    /// </para>
    /// <para>
    /// 演算法步驟：
    /// 1. 若鏈表為空，直接返回
    /// 2. 保存偶數鏈表的頭節點 evenHead = head.next
    /// 3. 使用 odd 和 even 兩個指標分別追蹤奇數和偶數節點
    /// 4. 迭代處理：odd.next 指向 even.next（下一個奇數節點），
    ///    然後 even.next 指向 odd.next（下一個偶數節點）
    /// 5. 將偶數鏈表連接到奇數鏈表末尾
    /// </para>
    /// <example>
    /// <code>
    /// 輸入: 1 -> 2 -> 3 -> 4 -> 5
    /// 輸出: 1 -> 3 -> 5 -> 2 -> 4
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="head">鏈表的頭節點</param>
    /// <returns>重排後的鏈表頭節點</returns>
    /// <remarks>
    /// 時間複雜度: O(n)，其中 n 為鏈表長度，只需遍歷一次
    /// 空間複雜度: O(1)，只使用常數額外空間
    /// </remarks>
    public ListNode? OddEvenList(ListNode? head)
    {
        // 邊界條件：空鏈表直接返回
        if (head is null)
        {
            return head;
        }

        // 保存偶數鏈表的頭節點，最後用於連接
        ListNode? evenHead = head.next;

        // odd 指向當前奇數節點，初始為頭節點（第 1 個節點）
        ListNode odd = head;

        // even 指向當前偶數節點，初始為第 2 個節點
        ListNode? even = evenHead;

        // 當偶數節點存在且其後還有節點時，持續迭代
        // even != null: 確保當前偶數節點存在
        // even.next != null: 確保還有下一個奇數節點可處理
        while (even is not null && even.next is not null)
        {
            // 將奇數節點的 next 指向下一個奇數節點（即 even 的下一個）
            odd.next = even.next;

            // odd 指標前進到下一個奇數節點
            odd = odd.next;

            // 將偶數節點的 next 指向下一個偶數節點（即新 odd 的下一個）
            even.next = odd.next;

            // even 指標前進到下一個偶數節點
            even = even.next;
        }

        // 將偶數鏈表連接到奇數鏈表末尾
        odd.next = evenHead;

        return head;
    }
}
