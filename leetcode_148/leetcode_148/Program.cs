namespace leetcode_148;

class Program
{
    /// <summary>
    /// Represents one node in the singly linked list used by LeetCode 148.
    /// 解題概念：排序流程只會重新串接節點指標，不會建立新的資料節點內容。
    /// 輸入條件：<see cref="val"/> 是目前節點值，<see cref="next"/> 指向下一個節點或 <c>null</c>。
    /// 輸出結果：建立可供切割、合併與重組的 linked-list 節點。
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

    /// <summary>
    /// 148. Sort List
    /// https://leetcode.com/problems/sort-list/description/
    /// Given the head of a linked list, return the list after sorting it in ascending order.
    ///
    /// 148. 排序鏈結串列
    /// https://leetcode.cn/problems/sort-list/description/
    /// 給定一個 linked list 的 head，請將該 linked list 依照遞增順序排序後回傳。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        SampleCase[] samples =
        [
            new SampleCase([4, 2, 1, 3], [1, 2, 3, 4]),
            new SampleCase([-1, 5, 3, 4, 0], [-1, 0, 3, 4, 5]),
            new SampleCase([], []),
            new SampleCase([1], [1]),
            new SampleCase([3, 3, 1, 2], [1, 2, 3, 3]),
        ];

        bool allPassed = RunSamples(samples, solution.SortList);
        Environment.ExitCode = allPassed ? 0 : 1;
    }

    /// <summary>
    /// Represents one executable console sample for Sort List.
    /// 輸入條件：<see cref="Input"/> 是未排序 linked list 的節點值，<see cref="Expected"/> 是排序後的升冪結果。
    /// 輸出結果：封裝單筆範例的輸入與預期輸出，供主程式驗證演算法是否正確。
    /// </summary>
    /// <param name="Input">The original linked-list values before sorting.</param>
    /// <param name="Expected">The ascending-order values expected from the solver.</param>
    private readonly record struct SampleCase(int[] Input, int[] Expected);

    /// <summary>
    /// Runs all executable samples against the provided solver and prints PASS or FAIL for each case.
    /// 解題概念：把陣列轉回 linked list 後呼叫排序函式，再將結果還原成陣列做比對，讓 Main 可以直接充當驗證入口。
    /// 輸入條件：<paramref name="samples"/> 需提供合法的 linked-list 整數資料；<paramref name="solver"/> 需回傳排序後的 linked list 頭節點。
    /// 輸出結果：若全部範例都符合預期則回傳 <c>true</c>，否則回傳 <c>false</c>，並同步輸出每筆案例的比較結果。
    /// </summary>
    /// <param name="samples">Executable sample cases that describe the input list and expected sorted result.</param>
    /// <param name="solver">The sorting function to verify.</param>
    /// <returns><c>true</c> when every sample matches the expected output; otherwise, <c>false</c>.</returns>
    private static bool RunSamples(SampleCase[] samples, Func<ListNode?, ListNode?> solver)
    {
        bool allPassed = true;

        foreach (SampleCase sample in samples)
        {
            ListNode? inputHead = BuildList(sample.Input);
            int[] actual = ListToArray(solver(inputHead));
            bool passed = actual.SequenceEqual(sample.Expected);

            Console.WriteLine($"{(passed ? "PASS" : "FAIL")} input={FormatArray(sample.Input)}, expected={FormatArray(sample.Expected)}, actual={FormatArray(actual)}");
            allPassed &= passed;
        }

        return allPassed;
    }

    /// <summary>
    /// Builds a linked list from an integer array so the console samples can reuse the same solver signature as LeetCode.
    /// 解題概念：依序把陣列元素接到尾端，建立與輸入順序一致的單向 linked list。
    /// 輸入條件：<paramref name="values"/> 可為空陣列或任意整數序列。
    /// 輸出結果：回傳 linked list 的頭節點；若輸入為空陣列則回傳 <c>null</c>。
    /// </summary>
    /// <param name="values">The values to place into a linked list in their original order.</param>
    /// <returns>The head of the created linked list, or <c>null</c> when the array is empty.</returns>
    private static ListNode? BuildList(int[] values)
    {
        ListNode dummy = new();
        ListNode tail = dummy;

        foreach (int value in values)
        {
            tail.next = new ListNode(value);
            tail = tail.next;
        }

        return dummy.next;
    }

    /// <summary>
    /// Converts a linked list back to an array so the console runner can compare actual and expected results directly.
    /// 解題概念：排序結果仍以 linked list 表示，因此需要依序走訪節點，把節點值收集成陣列才能輸出與比對。
    /// 輸入條件：<paramref name="head"/> 可為 <c>null</c>，或為任意長度的單向 linked list。
    /// 輸出結果：回傳與 linked list 節點順序相同的整數陣列；空串列會轉成空陣列。
    /// </summary>
    /// <param name="head">The head of the linked list to flatten.</param>
    /// <returns>An array containing the linked-list values in traversal order.</returns>
    private static int[] ListToArray(ListNode? head)
    {
        List<int> values = [];

        for (ListNode? current = head; current is not null; current = current.next)
        {
            values.Add(current.val);
        }

        return [.. values];
    }

    /// <summary>
    /// Formats an integer array as a readable bracketed string for console output.
    /// 解題概念：樣本驗證需要穩定、易讀的輸出格式，方便 README 與實際執行結果保持一致。
    /// 輸入條件：<paramref name="values"/> 可為空陣列或任意整數序列。
    /// 輸出結果：回傳形如 <c>[1, 2, 3]</c> 的字串表示。
    /// </summary>
    /// <param name="values">The values to format.</param>
    /// <returns>A bracketed string representation of the array.</returns>
    private static string FormatArray(int[] values)
    {
        return $"[{string.Join(", ", values)}]";
    }

    /// <summary>
    /// Uses bottom-up merge sort to reorder the linked list into ascending order without recursion.
    /// 解題概念：把整條 linked list 視為多個已排序的小區段，先合併長度為 1 的區段，再依序合併長度為 2、4、8 的區段，
    /// 讓有序範圍逐輪倍增，直到整條串列完成排序。
    /// 輸入條件：<paramref name="head"/> 可為 <c>null</c>，或為任意長度、任意整數值的單向 linked list。
    /// 輸出結果：回傳重新串接後的升冪 linked list 頭節點；時間複雜度為 <c>O(n log n)</c>，額外空間為 <c>O(1)</c>。
    /// </summary>
    /// <param name="head">The head of the unsorted singly linked list.</param>
    /// <returns>The head of the same nodes rearranged in ascending order.</returns>
    public ListNode? SortList(ListNode? head)
    {
        int length = GetListLength(head);
        ListNode dummy = new(0, head);

        // step 表示目前每個已排序區段的長度；每輪合併完後倍增，逐步擴大有序範圍。
        for (int step = 1; step < length; step *= 2)
        {
            ListNode newListTail = dummy;
            ListNode? current = dummy.next;

            while (current is not null)
            {
                ListNode head1 = current;
                ListNode? head2 = SplitList(head1, step);

                // 再切一次即可取得下一組待處理區段；若尾巴不足 step，SplitList 會自然回傳 null。
                current = SplitList(head2, step);

                // 回傳 tail 可讓下一組合併結果用 O(1) 方式接回，不必重新掃描整段 merged list。
                (ListNode? mergedHead, ListNode mergedTail) = MergeTwoLists(head1, head2);
                newListTail.next = mergedHead;
                newListTail = mergedTail;
            }
        }

        return dummy.next;
    }

    /// <summary>
    /// Counts how many nodes are in the linked list.
    /// 解題概念：bottom-up merge sort 需要先知道整體長度，才能判斷 step 要擴大到什麼時候停止。
    /// 輸入條件：<paramref name="head"/> 可為 <c>null</c>，或為任意長度的單向 linked list。
    /// 輸出結果：回傳節點總數，空串列回傳 <c>0</c>。
    /// </summary>
    /// <param name="head">The head of the linked list to count.</param>
    /// <returns>The number of nodes in the list.</returns>
    private int GetListLength(ListNode? head)
    {
        int length = 0;

        while (head is not null)
        {
            length++;
            head = head.next;
        }

        return length;
    }

    /// <summary>
    /// Detaches the first run of up to <paramref name="size"/> nodes and returns the head of the remaining suffix.
    /// 解題概念：自底向上歸併排序每一輪都要切出固定長度的區段；這個方法負責把區段尾端斷開，方便後續兩兩合併。
    /// 輸入條件：<paramref name="head"/> 可為 <c>null</c>；<paramref name="size"/> 需為正整數，代表本輪欲切出的區段長度。
    /// 輸出結果：回傳剩餘鏈結串列的頭節點；若原串列長度不足以再延伸，則回傳 <c>null</c>。
    /// </summary>
    /// <param name="head">The head of the run to cut.</param>
    /// <param name="size">The maximum number of nodes to keep in the detached run.</param>
    /// <returns>The head of the remaining suffix after the detached run, or <c>null</c> if none remains.</returns>
    private ListNode? SplitList(ListNode? head, int size)
    {
        if (head is null)
        {
            return null;
        }

        ListNode current = head;
        for (int i = 1; i < size && current.next is not null; i++)
        {
            current = current.next;
        }

        ListNode? nextHead = current.next;
        current.next = null;
        return nextHead;
    }

    /// <summary>
    /// Merges two ascending linked-list runs and returns both the merged head and merged tail.
    /// 解題概念：利用雙指標持續挑選較小節點接到結果尾端，最後把尚未用完的剩餘區段直接接上。
    /// 輸入條件：<paramref name="list1"/> 與 <paramref name="list2"/> 各自都必須已經是升冪排序的 linked list，且至少一者非空。
    /// 輸出結果：回傳合併後串列的頭節點與尾節點，供外層排序流程快速串接下一段結果。
    /// </summary>
    /// <param name="list1">The first sorted run.</param>
    /// <param name="list2">The second sorted run.</param>
    /// <returns>The merged run's head and tail.</returns>
    private (ListNode? Head, ListNode Tail) MergeTwoLists(ListNode? list1, ListNode? list2)
    {
        if (list1 is null && list2 is null)
        {
            throw new InvalidOperationException("At least one input list is required.");
        }

        ListNode dummy = new();
        ListNode tail = dummy;

        while (list1 is not null && list2 is not null)
        {
            if (list1.val <= list2.val)
            {
                tail.next = list1;
                list1 = list1.next;
            }
            else
            {
                tail.next = list2;
                list2 = list2.next;
            }

            tail = tail.next;
        }

        tail.next = list1 ?? list2;

        while (tail.next is not null)
        {
            tail = tail.next;
        }

        return (dummy.next, tail);
    }
}
