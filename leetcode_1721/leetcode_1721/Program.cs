namespace leetcode_1721;

internal class Program
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
    /// 1721. Swapping Nodes in a Linked List
    /// 1721. 交換鏈結串列中的節點
    /// https://leetcode.com/problems/swapping-nodes-in-a-linked-list/
    /// https://leetcode.cn/problems/swapping-nodes-in-a-linked-list/
    /// Given the head of a linked list and an integer k, swap the values of the kth node from
    /// the beginning and the kth node from the end, then return the head of the list.
    /// 給定鏈結串列的頭節點 head 與整數 k，交換正數第 k 個與倒數第 k 個節點的值，
    /// 並回傳鏈結串列的頭節點。
    /// </summary>
    /// <param name="args">主控台啟動參數；本驗證器不使用。</param>
    private static void Main(string[] args)
    {
        int[] maximumLengthValues = Enumerable.Range(0, 100_000)
            .Select(index => index % 101)
            .ToArray();
        int[] maximumLengthExpected = [.. maximumLengthValues];
        (maximumLengthExpected[49_999], maximumLengthExpected[50_000]) =
            (maximumLengthExpected[50_000], maximumLengthExpected[49_999]);

        TestCase[] testCases =
        [
            new("Official example 1", [1, 2, 3, 4, 5], 2, [1, 4, 3, 2, 5]),
            new(
                "Official example 2",
                [7, 9, 6, 6, 7, 8, 3, 0, 9, 5],
                5,
                [7, 9, 6, 6, 8, 7, 3, 0, 9, 5]),
            new("Single node", [1], 1, [1]),
            new("Two nodes", [1, 2], 1, [2, 1]),
            new("Odd-length middle node", [1, 2, 3], 2, [1, 2, 3]),
            new("Even-length adjacent middle nodes", [1, 2, 3, 4], 2, [1, 3, 2, 4]),
            new("Value boundaries / k equals length", [0, 25, 50, 75, 100], 5, [100, 25, 50, 75, 0]),
            new(
                "Maximum length / adjacent middle nodes",
                maximumLengthValues,
                50_000,
                maximumLengthExpected)
        ];

        SolutionVariant[] variants =
        [
            new("Length scan", SwapNodes),
            new("Two pointers", SwapNodes2)
        ];

        int passed = 0;
        foreach (TestCase testCase in testCases)
        {
            VariantResult[] results = variants
                .Select(variant => RunVariant(testCase, variant))
                .ToArray();
            bool isPassed = results.All(result => result.IsPassed);
            if (isPassed)
            {
                passed++;
            }

            Console.WriteLine($"Case: {testCase.Name}");
            Console.WriteLine($"Input: head={FormatArray(testCase.Values)}, k={testCase.K}");
            Console.WriteLine($"Expected: {FormatArray(testCase.Expected)}");
            foreach (VariantResult result in results)
            {
                Console.WriteLine($"{result.Name} actual: {FormatArray(result.Actual)}");
                Console.WriteLine($"{result.Name} returned original head: {(result.HeadPreserved ? "YES" : "NO")}");
                Console.WriteLine($"{result.Name} topology preserved: {(result.TopologyPreserved ? "YES" : "NO")}");
            }

            Console.WriteLine($"Result: {(isPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        Console.WriteLine($"Summary: {passed}/{testCases.Length} checks passed.");
        if (passed != testCases.Length)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 在 head 為包含 1 至 100,000 個節點的有效鏈結串列，且 k 介於 1 與節點數量之間時，
    /// 先計算串列長度，再定位正數第 k 個與倒數第 k 個節點並交換兩者的值；回傳原 head，
    /// 節點物件與鏈結順序保持不變。
    /// </summary>
    /// <param name="head">題目保證非 null 的鏈結串列頭節點。</param>
    /// <param name="k">以 1 為起點的有效節點位置。</param>
    /// <returns>完成指定節點值交換後的原鏈結串列頭節點。</returns>
    public static ListNode SwapNodes(ListNode head, int k)
    {
        int length = 0;
        for (ListNode? current = head; current is not null; current = current.next)
        {
            length++;
        }

        ListNode front = head;
        for (int position = 1; position < k; position++)
        {
            front = front.next!;
        }

        ListNode back = head;
        for (int position = 1; position < length - k + 1; position++)
        {
            back = back.next!;
        }

        (front.val, back.val) = (back.val, front.val);
        return head;
    }

    /// <summary>
    /// 在 head 為包含 1 至 100,000 個節點的有效鏈結串列，且 k 介於 1 與節點數量之間時，
    /// 先找到正數第 k 個節點，再讓領先指標與尾端指標維持固定距離同步前進，藉此定位倒數
    /// 第 k 個節點並交換兩者的值；回傳原 head，節點物件與鏈結順序保持不變。
    /// </summary>
    /// <param name="head">題目保證非 null 的鏈結串列頭節點。</param>
    /// <param name="k">以 1 為起點的有效節點位置。</param>
    /// <returns>完成指定節點值交換後的原鏈結串列頭節點。</returns>
    public static ListNode SwapNodes2(ListNode head, int k)
    {
        ListNode front = head;
        for (int position = 1; position < k; position++)
        {
            front = front.next!;
        }

        ListNode back = head;
        ListNode lead = front;
        while (lead.next is not null)
        {
            back = back.next!;
            lead = lead.next;
        }

        (front.val, back.val) = (back.val, front.val);
        return head;
    }

    private static VariantResult RunVariant(TestCase testCase, SolutionVariant variant)
    {
        ListNode head = CreateList(testCase.Values);
        List<ListNode> originalNodes = SnapshotNodes(head);
        ListNode resultHead = variant.Solve(head, testCase.K);
        bool headPreserved = ReferenceEquals(head, resultHead);
        bool topologyPreserved = HasSameTopology(resultHead, originalNodes);
        int[] actual = ReadValues(resultHead, originalNodes.Count + 1);
        bool isPassed = headPreserved &&
            topologyPreserved &&
            actual.SequenceEqual(testCase.Expected);

        return new VariantResult(
            variant.Name,
            actual,
            headPreserved,
            topologyPreserved,
            isPassed);
    }

    private static ListNode CreateList(int[] values)
    {
        ListNode head = new(values[0]);
        ListNode tail = head;
        for (int i = 1; i < values.Length; i++)
        {
            tail.next = new ListNode(values[i]);
            tail = tail.next;
        }

        return head;
    }

    private static List<ListNode> SnapshotNodes(ListNode head)
    {
        List<ListNode> nodes = [];
        for (ListNode? current = head; current is not null; current = current.next)
        {
            nodes.Add(current);
        }

        return nodes;
    }

    private static bool HasSameTopology(ListNode? head, IReadOnlyList<ListNode> originalNodes)
    {
        ListNode? current = head;
        foreach (ListNode originalNode in originalNodes)
        {
            if (!ReferenceEquals(current, originalNode))
            {
                return false;
            }

            current = current.next;
        }

        return current is null;
    }

    private static int[] ReadValues(ListNode? head, int maximumCount)
    {
        List<int> values = [];
        ListNode? current = head;
        while (current is not null && values.Count < maximumCount)
        {
            values.Add(current.val);
            current = current.next;
        }

        return [.. values];
    }

    private static string FormatArray(int[] values)
    {
        const int previewLength = 5;
        if (values.Length <= previewLength * 2)
        {
            return $"[{string.Join(", ", values)}]";
        }

        return $"[{string.Join(", ", values.Take(previewLength))}, ..., " +
            $"{string.Join(", ", values.TakeLast(previewLength))}] (length {values.Length})";
    }

    private sealed record TestCase(string Name, int[] Values, int K, int[] Expected);

    private sealed record SolutionVariant(string Name, Func<ListNode, int, ListNode> Solve);

    private sealed record VariantResult(
        string Name,
        int[] Actual,
        bool HeadPreserved,
        bool TopologyPreserved,
        bool IsPassed);
}