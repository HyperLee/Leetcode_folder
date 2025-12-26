namespace leetcode_061;

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
    /// 61. Rotate List
    /// https://leetcode.com/problems/rotate-list/description/
    /// 61. 旋转链表
    /// https://leetcode.cn/problems/rotate-list/description/
    /// 
    /// Given the head of a linked list, rotate the list to the right by k places.
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var program = new Program();

        // 測試範例 1: [1,2,3,4,5], k = 2 -> [4,5,1,2,3]
        var head1 = CreateLinkedList([1, 2, 3, 4, 5]);
        Console.WriteLine("範例 1:");
        Console.Write("輸入: ");
        PrintLinkedList(head1);
        Console.WriteLine($"k = 2");
        var result1 = program.RotateRight(head1, 2);
        Console.Write("輸出: ");
        PrintLinkedList(result1);
        Console.WriteLine();

        // 測試範例 2: [0,1,2], k = 4 -> [2,0,1]
        var head2 = CreateLinkedList([0, 1, 2]);
        Console.WriteLine("範例 2:");
        Console.Write("輸入: ");
        PrintLinkedList(head2);
        Console.WriteLine($"k = 4");
        var result2 = program.RotateRight(head2, 4);
        Console.Write("輸出: ");
        PrintLinkedList(result2);
        Console.WriteLine();

        // 測試範例 3: 空鏈結串列
        Console.WriteLine("範例 3 (空鏈結串列):");
        Console.WriteLine("輸入: [], k = 1");
        var result3 = program.RotateRight(null, 1);
        Console.Write("輸出: ");
        PrintLinkedList(result3);
        Console.WriteLine();

        // 測試範例 4: 單節點鏈結串列
        var head4 = CreateLinkedList([1]);
        Console.WriteLine("範例 4 (單節點):");
        Console.Write("輸入: ");
        PrintLinkedList(head4);
        Console.WriteLine($"k = 99");
        var result4 = program.RotateRight(head4, 99);
        Console.Write("輸出: ");
        PrintLinkedList(result4);
    }

    /// <summary>
    /// 建立鏈結串列
    /// </summary>
    /// <param name="values">節點數值陣列</param>
    /// <returns>鏈結串列的頭節點</returns>
    private static ListNode? CreateLinkedList(int[] values)
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
    /// 印出鏈結串列
    /// </summary>
    /// <param name="head">鏈結串列的頭節點</param>
    private static void PrintLinkedList(ListNode? head)
    {
        Console.Write("[");
        var current = head;
        while (current is not null)
        {
            Console.Write(current.val);
            if (current.next is not null)
            {
                Console.Write(", ");
            }
            current = current.next;
        }
        Console.WriteLine("]");
    }

    /// <summary>
    /// 旋轉鏈結串列 - 將鏈結串列向右旋轉 k 個位置
    /// 
    /// <para><b>解題思路：</b></para>
    /// <para>1. 將鏈結串列首尾相連形成環形鏈結串列</para>
    /// <para>2. 找到新的斷開位置，該位置為倒數第 k+1 個節點</para>
    /// <para>3. 在該位置斷開環，形成新的鏈結串列</para>
    /// 
    /// <para><b>關鍵觀察：</b></para>
    /// <para>- 向右旋轉 k 位，等於將最後 k 個節點移到前面</para>
    /// <para>- 若 k 大於鏈結串列長度，實際旋轉次數為 k % length</para>
    /// <para>- 新的頭節點是原本的倒數第 k 個節點</para>
    /// 
    /// <para><b>時間複雜度：</b> O(n)，其中 n 為鏈結串列長度</para>
    /// <para><b>空間複雜度：</b> O(1)，只使用常數額外空間</para>
    /// </summary>
    /// <param name="head">鏈結串列的頭節點</param>
    /// <param name="k">向右旋轉的位置數</param>
    /// <returns>旋轉後的鏈結串列頭節點</returns>
    /// <example>
    /// <code>
    /// 輸入: head = [1,2,3,4,5], k = 2
    /// 輸出: [4,5,1,2,3]
    /// 
    /// 輸入: head = [0,1,2], k = 4
    /// 輸出: [2,0,1]
    /// </code>
    /// </example>
    public ListNode? RotateRight(ListNode? head, int k)
    {
        // 邊界條件：空鏈結串列或只有一個節點時，直接返回
        if (head is null || head.next is null)
        {
            return head;
        }

        // 步驟 1: 計算鏈結串列長度，並找到尾節點
        ListNode tail = head;
        int length = 1;

        // 走訪整個鏈結串列，計算總長度
        while (tail.next is not null)
        {
            tail = tail.next;
            length++;
        }

        // 步驟 2: 將尾節點連接到頭節點，形成環形鏈結串列
        tail.next = head;

        // 步驟 3: 計算新的斷開位置
        // 向右旋轉 k 位，新的尾節點是從頭開始的第 (length - k % length - 1) 個節點
        // 這樣新的頭節點就是倒數第 k 個節點
        int stepsToNewTail = length - k % length - 1;

        // 步驟 4: 找到新的尾節點位置
        ListNode newTail = head;
        while (stepsToNewTail > 0)
        {
            newTail = newTail.next!;
            stepsToNewTail--;
        }

        // 步驟 5: 斷開環，設定新的頭節點
        ListNode newHead = newTail.next!;
        newTail.next = null;

        return newHead;
    }
}
