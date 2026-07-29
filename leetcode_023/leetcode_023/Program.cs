namespace leetcode_023;

class Program
{
    /// <summary>
    /// 表示單向鏈表節點；<see cref="val"/> 儲存節點值，
    /// <see cref="next"/> 指向下一個節點，鏈表尾端則為 <see langword="null"/>。
    /// </summary>
    public class ListNode
    {
        public int val;
        public ListNode? next;

        /// <summary>
        /// 建立單向鏈表節點。
        /// </summary>
        /// <param name="val">節點儲存的整數值。</param>
        /// <param name="next">下一個節點；省略或傳入 <see langword="null"/> 表示鏈表尾端。</param>
        public ListNode(int val = 0, ListNode? next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    /// <summary>
    /// 23. Merge k Sorted Lists
    /// https://leetcode.com/problems/merge-k-sorted-lists/description/
    /// 23. 合并 K 个升序链表
    /// https://leetcode.cn/problems/merge-k-sorts-lists/description/
    /// 
    /// 題目描述：
    /// 給你一個鏈表數組，每個鏈表都已經按升序排列
    /// 請你將所有鏈表合併到一個升序鏈表中，返回合併後的鏈表
    /// 
    /// 解題思路：
    /// 1. 使用分治法（Divide and Conquer）將問題分解為更小的子問題
    /// 2. 將 K 個鏈表分成兩半，分別處理各自的子問題
    /// 3. 使用二分法不斷將鏈表數組對半分割
    /// 4. 當分割到只剩一個或零個鏈表時，開始向上合併
    /// 5. 合併過程使用遞迴方式處理兩個已排序的鏈表
    /// 
    /// 時間複雜度：O(N log k)，其中 k 是鏈表數量，N 是所有節點總數
    /// 空間複雜度：O(log k)，因為遞迴調用的深度為 log k
    /// 
    /// 最一開始解題方法參考:21. Merge Two Sorted Lists 遞迴解法
    /// 是可以解題, 但是效率太差. 改用分治法後, 效率提升很多
    /// </summary>
    static void Main(string[] args)
    {
        SampleCase[] sampleCases =
        {
            new(
                "官方範例：三個升序鏈表",
                [[1, 4, 5], [1, 3, 4], [2, 6]],
                [1, 1, 2, 3, 4, 4, 5, 6]),
            new("空鏈表陣列", [], []),
            new("包含一個空鏈表", [[]], []),
            new("單一鏈表", [[1, 2, 3]], [1, 2, 3]),
            new(
                "包含負值",
                [[-10, -5, 0], [-6, -3, 2], [-7, 1, 4]],
                [-10, -7, -6, -5, -3, 0, 1, 2, 4]),
            new(
                "包含重複值",
                [[1, 1, 3], [1, 2, 2], [1, 1, 2]],
                [1, 1, 1, 1, 1, 2, 2, 2, 3])
        };

        int passedChecks = 0;
        int totalChecks = sampleCases.Length * 2;

        for (int index = 0; index < sampleCases.Length; index++)
        {
            SampleCase sample = sampleCases[index];
            int[] recursiveResult = ToArray(MergeKLists(BuildLists(sample.Input)));
            int[] iterativeResult = ToArray(MergeKLists2(BuildLists(sample.Input)));
            bool recursivePassed = recursiveResult.SequenceEqual(sample.Expected);
            bool iterativePassed = iterativeResult.SequenceEqual(sample.Expected);

            if (recursivePassed)
            {
                passedChecks++;
            }

            if (iterativePassed)
            {
                passedChecks++;
            }

            Console.WriteLine($"案例 {index + 1}：{sample.Name}");
            Console.WriteLine($"輸入：{FormatNestedArray(sample.Input)}");
            Console.WriteLine($"Expected：{FormatArray(sample.Expected)}");
            Console.WriteLine(
                $"解法一 Actual：{FormatArray(recursiveResult)} => {(recursivePassed ? "PASS" : "FAIL")}");
            Console.WriteLine(
                $"解法二 Actual：{FormatArray(iterativeResult)} => {(iterativePassed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
    }

    /// <summary>
    /// 以由上而下的分治法合併 K 個升序鏈表。
    /// 先遞迴切分鏈表陣列，再以遞迴方式合併兩個有序結果；
    /// 輸入可為 <see langword="null"/>、空陣列或包含空鏈表，節點會被重新串接。
    /// </summary>
    /// <param name="lists">各自以非遞減順序排列的鏈表陣列。</param>
    /// <returns>包含所有輸入節點的升序鏈表；沒有節點時回傳 <see langword="null"/>。</returns>
    public static ListNode? MergeKLists(ListNode?[]? lists)
    {
        if (lists == null || lists.Length == 0)
        {
            return null;
        }

        return MergeSort(lists, 0, lists.Length - 1);
    }

    /// <summary>
    /// 遞迴合併 <paramref name="lists"/> 中索引位於
    /// <paramref name="left"/> 至 <paramref name="right"/> 的鏈表。
    /// 每次將索引區間對半切分，直到只剩一條鏈表，再回溯合併左右結果。
    /// </summary>
    /// <param name="lists">可包含空鏈表的輸入陣列。</param>
    /// <param name="left">目前區間的起始索引，包含此位置。</param>
    /// <param name="right">目前區間的結束索引，包含此位置。</param>
    /// <returns>指定區間內所有節點組成的升序鏈表。</returns>
    private static ListNode? MergeSort(ListNode?[] lists, int left, int right)
    {
        if (left == right)
        {
            return lists[left];
        }

        if (left > right)
        {
            return null;
        }

        // 索引區間採安全的中點公式，避免 left + right 直接相加溢位。
        int mid = left + (right - left) / 2;
        ListNode? leftList = MergeSort(lists, left, mid);
        ListNode? rightList = MergeSort(lists, mid + 1, right);

        return MergeTwoListsRecursive(leftList, rightList);
    }

    /// <summary>
    /// 以遞迴方式合併兩個升序鏈表。
    /// 每層選出較小的首節點，並將其後繼指向剩餘節點的合併結果；
    /// 任一輸入可為空，原有節點的 <see cref="ListNode.next"/> 會被重新串接。
    /// </summary>
    /// <param name="left">第一個升序鏈表。</param>
    /// <param name="right">第二個升序鏈表。</param>
    /// <returns>包含兩個輸入鏈表全部節點的升序鏈表。</returns>
    private static ListNode? MergeTwoListsRecursive(ListNode? left, ListNode? right)
    {
        if (left == null)
        {
            return right;
        }

        if (right == null)
        {
            return left;
        }

        // 較小的節點一定是目前合併結果的首節點，剩餘部分維持相同子問題。
        if (left.val < right.val)
        {
            left.next = MergeTwoListsRecursive(left.next, right);
            return left;
        }

        right.next = MergeTwoListsRecursive(left, right.next);
        return right;
    }

    /// <summary>
    /// 以由下而上的分治法合併 K 個升序鏈表。
    /// 每一輪將合併間距加倍，使用迭代方式合併成對鏈表；
    /// 輸入可為 <see langword="null"/>、空陣列或包含空鏈表，節點會被重新串接。
    /// </summary>
    /// <param name="lists">各自以非遞減順序排列的鏈表陣列。</param>
    /// <returns>包含所有輸入節點的升序鏈表；沒有節點時回傳 <see langword="null"/>。</returns>
    public static ListNode? MergeKLists2(ListNode?[]? lists)
    {
        if (lists == null || lists.Length == 0)
        {
            return null;
        }

        // 淺複製工作陣列，避免各輪合併時替換呼叫端保存的鏈表首節點。
        ListNode?[] mergedLists = [.. lists];

        for (int interval = 1; interval < mergedLists.Length; interval *= 2)
        {
            for (int index = 0; index + interval < mergedLists.Length; index += interval * 2)
            {
                mergedLists[index] = MergeTwoListsIterative(
                    mergedLists[index],
                    mergedLists[index + interval]);
            }
        }

        return mergedLists[0];
    }

    /// <summary>
    /// 以迭代方式合併兩個升序鏈表。
    /// dummy head 統一處理首節點，游標每次接上較小節點；
    /// 任一輸入可為空，原有節點的 <see cref="ListNode.next"/> 會被重新串接。
    /// </summary>
    /// <param name="left">第一個升序鏈表。</param>
    /// <param name="right">第二個升序鏈表。</param>
    /// <returns>包含兩個輸入鏈表全部節點的升序鏈表。</returns>
    private static ListNode? MergeTwoListsIterative(ListNode? left, ListNode? right)
    {
        ListNode dummy = new();
        ListNode tail = dummy;

        while (left != null && right != null)
        {
            ListNode selected;

            if (left.val <= right.val)
            {
                selected = left;
                left = left.next;
            }
            else
            {
                selected = right;
                right = right.next;
            }

            tail.next = selected;
            tail = selected;
        }

        // 其中一側耗盡後，另一側已保持升序，可整段直接接到結果尾端。
        tail.next = left ?? right;
        return dummy.next;
    }

    /// <summary>
    /// 將整數陣列規格轉成彼此獨立的鏈表陣列，並保留各內層陣列的原始順序。
    /// 空的內層陣列會轉成 <see langword="null"/> 鏈表。
    /// </summary>
    /// <param name="values">每個內層陣列代表一條鏈表的節點值。</param>
    /// <returns>可交給合併解法使用的鏈表陣列。</returns>
    private static ListNode?[] BuildLists(int[][] values)
    {
        ListNode?[] lists = new ListNode?[values.Length];

        for (int index = 0; index < values.Length; index++)
        {
            ListNode dummy = new();
            ListNode tail = dummy;

            foreach (int value in values[index])
            {
                ListNode node = new(value);
                tail.next = node;
                tail = node;
            }

            lists[index] = dummy.next;
        }

        return lists;
    }

    /// <summary>
    /// 依鏈表順序收集所有節點值，供範例驗證與顯示使用。
    /// </summary>
    /// <param name="head">鏈表首節點；可為 <see langword="null"/>。</param>
    /// <returns>依節點順序排列的整數陣列；空鏈表回傳空陣列。</returns>
    private static int[] ToArray(ListNode? head)
    {
        List<int> values = [];

        while (head != null)
        {
            values.Add(head.val);
            head = head.next;
        }

        return [.. values];
    }

    /// <summary>
    /// 將多條鏈表的整數陣列規格格式化為 LeetCode 風格的巢狀陣列文字。
    /// </summary>
    /// <param name="values">要格式化的二維整數陣列。</param>
    /// <returns>例如 <c>[[1,4],[2,3]]</c> 的顯示文字。</returns>
    private static string FormatNestedArray(int[][] values) =>
        $"[{string.Join(",", values.Select(FormatArray))}]";

    /// <summary>
    /// 將整數陣列格式化為不含額外空白的中括號文字。
    /// </summary>
    /// <param name="values">要格式化的整數陣列。</param>
    /// <returns>例如 <c>[1,2,3]</c> 的顯示文字。</returns>
    private static string FormatArray(int[] values) =>
        $"[{string.Join(",", values)}]";

    /// <summary>
    /// 保存一組可重建鏈表的輸入、預期排序結果與案例名稱。
    /// </summary>
    /// <param name="Name">案例顯示名稱。</param>
    /// <param name="Input">每條輸入鏈表的節點值。</param>
    /// <param name="Expected">合併後預期的節點值順序。</param>
    private sealed record SampleCase(string Name, int[][] Input, int[] Expected);
}
