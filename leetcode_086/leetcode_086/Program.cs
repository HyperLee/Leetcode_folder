namespace leetcode_086;

class Program
{
    /// <summary>
    /// 定義 LeetCode 86 範例與解法使用的單向鏈結串列節點。
    /// 每個節點會儲存一個整數值，以及可選的下一個節點參考。
    /// 使用此型別的演算法會以結果串列的頭節點作為輸出，
    /// 若串列為空則回傳 <c>null</c>。
    /// </summary>
    public class ListNode
    {
        public int val;
        public ListNode? next;

        /// <summary>
        /// 建立一個鏈結串列節點。
        /// 輸入值會成為節點承載的資料，選填的下一個節點會將目前節點
        /// 連接到串列剩餘部分。建構子建立出的節點可作為串列頭節點或內部節點使用。
        /// </summary>
        /// <param name="val">The integer stored in this node.</param>
        /// <param name="next">The next node in the linked list, or <c>null</c> for the tail.</param>
        public ListNode(int val = 0, ListNode? next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    /// <summary>
    /// 86. Partition List
    /// https://leetcode.com/problems/partition-list/description/
    ///
    /// Given the head of a linked list and a value x, partition it such that all nodes less than x come before nodes greater than or equal to x.
    /// You should preserve the original relative order of the nodes in each of the two partitions.
    ///
    /// 86. 分隔鏈結串列
    /// https://leetcode.cn/problems/partition-list/description/
    ///
    /// 給定一個鏈結串列的頭節點 head 和一個值 x，請將鏈結串列分隔，使所有小於 x 的節點都出現在大於或等於 x 的節點之前。
    /// 你必須保留兩個分區中各節點原本的相對順序。
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    static void Main(string[] args)
    {
        Program solution = new Program();

        Console.WriteLine("LeetCode 86 - Partition List");
        Console.WriteLine();

        RunExample(
            solution,
            "Example 1",
            values: [1, 4, 3, 2, 5, 2],
            x: 3,
            expected: [1, 2, 2, 4, 3, 5]);

        RunExample(
            solution,
            "Example 2",
            values: [2, 1],
            x: 2,
            expected: [1, 2]);

        RunExample(
            solution,
            "Example 3",
            values: [],
            x: 1,
            expected: []);
    }

    /// <summary>
    /// 分隔鏈結串列，使所有值小於 <paramref name="x"/> 的節點
    /// 都出現在值大於或等於 <paramref name="x"/> 的節點之前。
    /// 此方法使用兩個帶有虛擬頭節點的串列收集較小與較大的分區，
    /// 同時保留每個分區內原本的相對順序。輸入可以是 <c>null</c>，
    /// 輸出則是重新排列後串列的頭節點；若輸入串列為空則為 <c>null</c>。
    /// </summary>
    /// <param name="head">The head of the linked list to partition, or <c>null</c>.</param>
    /// <param name="x">The pivot value used to split nodes into two stable partitions.</param>
    /// <returns>The head of the partitioned linked list.</returns>
    public ListNode? Partition(ListNode? head, int x)
    {
        ListNode small = new ListNode(0);
        ListNode smallHead = small;
        ListNode large = new ListNode(0);
        ListNode largeHead = large;

        // Dummy heads let both partitions start empty without special-casing the first node.
        while (head is not null)
        {
            ListNode? next = head.next;

            if (head.val < x)
            {
                // Append to the small partition in traversal order to keep it stable.
                small.next = head;
                small = small.next;
            }
            else
            {
                // Append to the large partition in traversal order to keep it stable.
                large.next = head;
                large = large.next;
            }

            // The original nodes are reused, so detach each appended node from its old tail.
            head.next = null;
            head = next;
        }

        // Concatenate the two stable partitions, skipping the dummy node of the large list.
        small.next = largeHead.next;

        // Skip the dummy node of the small list to return the real result head.
        return smallHead.next;
    }

    /// <summary>
    /// 執行一個可由主控台進入點呼叫的範例。
    /// 輸入陣列會被轉換成鏈結串列，傳入 <see cref="Partition"/>，
    /// 並與預期序列比較。輸出包含 PASS/FAIL 行，以及輸入、預期結果、
    /// 實際結果，方便人工驗證。
    /// </summary>
    /// <param name="solution">The solution instance used to run the partition algorithm.</param>
    /// <param name="name">The display name of the example.</param>
    /// <param name="values">The linked-list values before partitioning.</param>
    /// <param name="x">The pivot value for this example.</param>
    /// <param name="expected">The expected linked-list values after partitioning.</param>
    private static void RunExample(Program solution, string name, int[] values, int x, int[] expected)
    {
        ListNode? head = BuildList(values);
        ListNode? result = solution.Partition(head, x);
        int[] actual = ToArray(result);
        bool passed = actual.SequenceEqual(expected);

        Console.WriteLine($"{name}: {(passed ? "PASS" : "FAIL")}");
        Console.WriteLine($"  Input: head = {FormatValues(values)}, x = {x}");
        Console.WriteLine($"  Expected: {FormatValues(expected)}");
        Console.WriteLine($"  Actual:   {FormatValues(actual)}");
        Console.WriteLine();
    }

    /// <summary>
    /// 依照陣列原本順序建立單向鏈結串列。
    /// 輸入陣列可以為空，此時輸出為 <c>null</c>。否則，
    /// 輸出會是頭節點，走訪該串列可得到與陣列相同的值。
    /// </summary>
    /// <param name="values">The values to place into the linked list.</param>
    /// <returns>The head of the linked list, or <c>null</c> for an empty input array.</returns>
    private static ListNode? BuildList(int[] values)
    {
        ListNode dummy = new ListNode(0);
        ListNode tail = dummy;

        foreach (int value in values)
        {
            tail.next = new ListNode(value);
            tail = tail.next;
        }

        return dummy.next;
    }

    /// <summary>
    /// 將鏈結串列轉換成值陣列，以便比較與顯示。
    /// 輸入可以是 <c>null</c>，表示空串列。輸出陣列會依走訪順序
    /// 包含每個節點的值。
    /// </summary>
    /// <param name="head">The head node to traverse, or <c>null</c>.</param>
    /// <returns>An array containing the linked-list values in order.</returns>
    private static int[] ToArray(ListNode? head)
    {
        List<int> values = [];

        while (head is not null)
        {
            values.Add(head.val);
            head = head.next;
        }

        return values.ToArray();
    }

    /// <summary>
    /// 將整數序列格式化成 LeetCode 風格的方括號表示法。
    /// 輸入陣列可以為空。輸出會是可讀字串，例如 <c>[1, 2, 2]</c>
    /// 或 <c>[]</c>，用於主控台範例與 README 文件。
    /// </summary>
    /// <param name="values">The values to format.</param>
    /// <returns>A bracketed string representation of the input values.</returns>
    private static string FormatValues(int[] values)
    {
        return $"[{string.Join(", ", values)}]";
    }
}
