namespace leetcode_2130;

/// <summary>
/// 表示題目使用的單向鏈結串列節點，保存整數值與下一個節點參考。
/// </summary>
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

internal static class Program
{
    /// <summary>
    /// LeetCode 2130. Maximum Twin Sum of a Linked List.
    /// LeetCode 2130. 鏈結串列最大孿生和。
    /// English: In an even-length linked list, node i is paired with node n - 1 - i.
    /// Return the maximum sum among all such twin pairs.
    /// 中文：在偶數長度的鏈結串列中，第 i 個節點與第 n - 1 - i 個節點互為孿生節點；
    /// 回傳所有孿生節點配對中的最大總和。
    /// English: https://leetcode.com/problems/maximum-twin-sum-of-a-linked-list/
    /// 中文：https://leetcode.cn/problems/maximum-twin-sum-of-a-linked-list/
    /// </summary>
    private static void Main()
    {
        int[] maximumLengthValues = Enumerable.Repeat(1, 100_000).ToArray();
        maximumLengthValues[^1] = 100_000;

        TestCase[] testCases =
        [
            new("Official example 1", "head=[5,4,2,1]", [5, 4, 2, 1], 6),
            new("Official example 2", "head=[4,2,2,3]", [4, 2, 2, 3], 7),
            new("Official example 3", "head=[1,100000]", [1, 100_000], 100_001),
            new(
                "Maximum inner twin sum",
                "head=[1,100000,100000,1]",
                [1, 100_000, 100_000, 1],
                200_000),
            new(
                "Maximum outer twin sum",
                "head=[100000,1,2,100000]",
                [100_000, 1, 2, 100_000],
                200_000),
            new(
                "Six-node mixed twin sums",
                "head=[9,1,2,8,7,3]",
                [9, 1, 2, 8, 7, 3],
                12),
            new(
                "Multi-digit node values",
                "head=[10,20,30,40]",
                [10, 20, 30, 40],
                50),
            new(
                "Maximum node count",
                "head=[1 x 99999, 100000] (100000 nodes)",
                maximumLengthValues,
                100_001)
        ];

        CaseResult[] results = testCases.Select(RunCase).ToArray();
        for (int index = 0; index < results.Length; index++)
        {
            CaseResult result = results[index];
            Console.WriteLine($"Case: {index + 1} - {result.Name}");
            Console.WriteLine($"Input: {result.Input}");
            Console.WriteLine(PrintCheck(
                "PairSum result",
                result.Expected,
                result.StackActual));
            Console.WriteLine(PrintCheck(
                "PairSum input preserved",
                true,
                result.StackInputPreserved));
            Console.WriteLine(PrintCheck(
                "PairSum2 result",
                result.Expected,
                result.OptimizedActual));
            Console.WriteLine(PrintCheck(
                "PairSum2 input preserved",
                true,
                result.OptimizedInputPreserved));
            Console.WriteLine();
        }

        int passedCount = results.Sum(result => result.PassedCheckCount);
        const int totalCheckCount = 32;
        Console.WriteLine($"Summary: {passedCount}/{totalCheckCount} checks passed.");

        if (passedCount != totalCheckCount)
        {
            Environment.ExitCode = 1;
        }
    }

    private static CaseResult RunCase(TestCase testCase)
    {
        ListNode stackHead = BuildList(testCase.Values);
        ListSnapshot stackSnapshot = CaptureSnapshot(stackHead);
        int stackActual = PairSum(stackHead);
        bool stackInputPreserved = IsSnapshotPreserved(stackHead, stackSnapshot);

        ListNode optimizedHead = BuildList(testCase.Values);
        ListSnapshot optimizedSnapshot = CaptureSnapshot(optimizedHead);
        int optimizedActual = PairSum2(optimizedHead);
        bool optimizedInputPreserved = IsSnapshotPreserved(optimizedHead, optimizedSnapshot);

        return new CaseResult(
            testCase.Name,
            testCase.Input,
            testCase.Expected,
            stackActual,
            stackInputPreserved,
            optimizedActual,
            optimizedInputPreserved);
    }

    /// <summary>
    /// 將題目限制內、長度為偶數的有效鏈結串列節點值依序推入 Stack，再讓前半節點與
    /// Stack 反向彈出的後半節點配對，回傳最大的孿生和。方法只讀取
    /// <paramref name="head"/>，不改變節點值、順序、連結或主控台狀態。令 n 為節點數，
    /// 時間複雜度為 O(n)，輔助空間為 O(n)，結果空間為 O(1)。
    /// </summary>
    /// <param name="head">節點數為 2 至 100,000 的偶數，且節點值介於 1 至 100,000。</param>
    /// <returns>所有第 i 與第 n - 1 - i 個節點配對中的最大節點值總和。</returns>
    public static int PairSum(ListNode head)
    {
        Stack<int> values = [];
        for (ListNode? current = head; current is not null; current = current.next)
        {
            values.Push(current.val);
        }

        int pairCount = values.Count / 2;
        int maximumTwinSum = 0;
        ListNode firstHalf = head;

        for (int index = 0; index < pairCount; index++)
        {
            // Stack 的彈出順序正好讓前半第 i 個節點配到原串列第 n - 1 - i 個節點。
            maximumTwinSum = Math.Max(maximumTwinSum, firstHalf.val + values.Pop());
            firstHalf = firstHalf.next!;
        }

        return maximumTwinSum;
    }

    /// <summary>
    /// 以快慢指標找到題目限制內、偶數長度鏈結串列的後半起點，原地反轉後半並與前半同步
    /// 掃描以取得最大孿生和，最後再次反轉以還原所有原始連結。方法結束後不保留對
    /// <paramref name="head"/> 的修改，也不改變節點值或主控台狀態。令 n 為節點數，時間
    /// 複雜度為 O(n)，輔助空間與結果空間皆為 O(1)。
    /// </summary>
    /// <param name="head">節點數為 2 至 100,000 的偶數，且節點值介於 1 至 100,000。</param>
    /// <returns>所有第 i 與第 n - 1 - i 個節點配對中的最大節點值總和。</returns>
    public static int PairSum2(ListNode head)
    {
        ListNode slow = head;
        ListNode? fast = head;

        while (fast is not null && fast.next is not null)
        {
            slow = slow.next!;
            fast = fast.next.next;
        }

        // 反轉後，secondHalf 的走訪順序就是原串列由尾端向中間的孿生順序。
        ListNode? reversedSecondHalf = ReverseList(slow);
        ListNode? firstHalf = head;
        ListNode? secondHalf = reversedSecondHalf;
        int maximumTwinSum = 0;

        while (secondHalf is not null)
        {
            maximumTwinSum = Math.Max(maximumTwinSum, firstHalf!.val + secondHalf.val);
            firstHalf = firstHalf.next;
            secondHalf = secondHalf.next;
        }

        // 對相同區段再反轉一次，恢復呼叫前每個節點的 next 參考。
        ReverseList(reversedSecondHalf);
        return maximumTwinSum;
    }

    /// <summary>
    /// 原地反轉以 <paramref name="head"/> 為起點的鏈結串列區段，將每個節點的 next 改指向
    /// 前一個節點，並回傳反轉後的新起點；輸入為 null 時回傳 null。
    /// </summary>
    /// <param name="head">要反轉的區段起點，或 null。</param>
    /// <returns>反轉後的區段起點；空區段則為 null。</returns>
    private static ListNode? ReverseList(ListNode? head)
    {
        ListNode? previous = null;
        ListNode? current = head;

        while (current is not null)
        {
            ListNode? next = current.next;
            current.next = previous;
            previous = current;
            current = next;
        }

        return previous;
    }

    private static ListNode BuildList(int[] values)
    {
        ListNode head = new(values[0]);
        ListNode tail = head;

        for (int index = 1; index < values.Length; index++)
        {
            tail.next = new ListNode(values[index]);
            tail = tail.next;
        }

        return head;
    }

    private static ListSnapshot CaptureSnapshot(ListNode head)
    {
        List<ListNode> nodes = [];
        List<int> values = [];
        List<ListNode?> nextNodes = [];

        for (ListNode? current = head; current is not null; current = current.next)
        {
            nodes.Add(current);
            values.Add(current.val);
            nextNodes.Add(current.next);
        }

        return new ListSnapshot([.. nodes], [.. values], [.. nextNodes]);
    }

    private static bool IsSnapshotPreserved(ListNode head, ListSnapshot snapshot)
    {
        ListNode? current = head;
        for (int index = 0; index < snapshot.Nodes.Length; index++)
        {
            if (current is null ||
                !ReferenceEquals(current, snapshot.Nodes[index]) ||
                current.val != snapshot.Values[index] ||
                !ReferenceEquals(current.next, snapshot.NextNodes[index]))
            {
                return false;
            }

            current = current.next;
        }

        return current is null;
    }

    private static string PrintCheck<T>(string checkName, T expected, T actual)
    {
        string status = EqualityComparer<T>.Default.Equals(expected, actual) ? "PASS" : "FAIL";
        return $"{status} {checkName} | Expected: {expected} | Actual: {actual}";
    }

    private sealed record TestCase(
        string Name,
        string Input,
        int[] Values,
        int Expected);

    private sealed record CaseResult(
        string Name,
        string Input,
        int Expected,
        int StackActual,
        bool StackInputPreserved,
        int OptimizedActual,
        bool OptimizedInputPreserved)
    {
        public int PassedCheckCount =>
            (StackActual == Expected ? 1 : 0) +
            (StackInputPreserved ? 1 : 0) +
            (OptimizedActual == Expected ? 1 : 0) +
            (OptimizedInputPreserved ? 1 : 0);
    }

    private sealed record ListSnapshot(
        ListNode[] Nodes,
        int[] Values,
        ListNode?[] NextNodes);
}