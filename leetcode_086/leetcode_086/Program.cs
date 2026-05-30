namespace leetcode_086;

class Program
{
    /// <summary>
    /// Defines a singly linked-list node used by the LeetCode 86 examples and solution.
    /// Each node stores an integer value and an optional reference to the next node.
    /// The output of algorithms using this type is represented by returning the head node
    /// of the resulting list, or <c>null</c> when the list is empty.
    /// </summary>
    public class ListNode
    {
        public int val;
        public ListNode? next;

        /// <summary>
        /// Creates one linked-list node.
        /// The input value becomes the node payload, and the optional next node links the
        /// current node to the rest of the list. The constructed node is returned by the
        /// constructor call and can be used as a list head or an internal node.
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
    /// Partitions a linked list so every node with a value less than <paramref name="x"/>
    /// appears before nodes with values greater than or equal to <paramref name="x"/>.
    /// The method uses two dummy-headed lists to collect the small and large partitions
    /// while preserving the original relative order inside each partition. The input can
    /// be <c>null</c>, and the output is the head of the rearranged list or <c>null</c>
    /// when the input list is empty.
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
    /// Runs one executable example for the console entry point.
    /// The input array is converted into a linked list, passed to <see cref="Partition"/>,
    /// and compared with the expected sequence. The output is a PASS/FAIL line plus the
    /// input, expected result, and actual result for easy manual verification.
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
    /// Builds a singly linked list from an array in the same order.
    /// The input array can be empty, in which case the output is <c>null</c>. Otherwise,
    /// the output is the head node whose traversal yields the same values as the array.
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
    /// Converts a linked list into an array of values for comparison and display.
    /// The input can be <c>null</c>, representing an empty list. The output array contains
    /// each node value in traversal order.
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
    /// Formats an integer sequence in LeetCode-style bracket notation.
    /// The input array can be empty. The output is a readable string such as
    /// <c>[1, 2, 2]</c> or <c>[]</c> for use in console examples and README documentation.
    /// </summary>
    /// <param name="values">The values to format.</param>
    /// <returns>A bracketed string representation of the input values.</returns>
    private static string FormatValues(int[] values)
    {
        return $"[{string.Join(", ", values)}]";
    }
}
