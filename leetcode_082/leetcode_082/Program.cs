namespace leetcode_082;

class Program
{
    /// <summary>
    /// 表示單向鏈結串列節點，符合 LeetCode 題目提供的 ListNode 結構。
    /// 節點保存整數值與下一個節點參考；下一個節點可以是 null，代表串列結尾。
    /// </summary>
    public class ListNode
    {
        public int val;
        public ListNode? next;

        /// <summary>
        /// 建立鏈結串列節點。
        /// 輸入節點值與可選的下一個節點，輸出可串接成單向鏈結串列的節點物件。
        /// </summary>
        /// <param name="val">目前節點保存的整數值。</param>
        /// <param name="next">下一個節點；若為 null 則代表目前節點是尾端。</param>
        public ListNode(int val = 0, ListNode? next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    /// <summary>
    /// 82. Remove Duplicates from Sorted List II
    /// https://leetcode.com/problems/remove-duplicates-from-sorted-list-ii/description/
    /// 82. 删除排序链表中的重复元素 II
    /// https://leetcode.cn/problems/remove-duplicates-from-sorted-list-ii/description/
    ///
    /// English:
    /// Given the head of a sorted linked list, delete all nodes that have duplicate numbers,
    /// leaving only distinct numbers from the original list. Return the linked list sorted as well.
    ///
    /// 繁體中文:
    /// 給定一個已排序鏈結串列的頭節點，刪除所有具有重複數值的節點，
    /// 只保留原始串列中數值不重複的節點。回傳處理後仍為已排序的鏈結串列。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        int[][] samples =
        [
            [1, 2, 3, 3, 4, 4, 5],
            [1, 1, 1, 2, 3],
            [1, 1]
        ];

        Console.WriteLine("LeetCode 82 - Remove Duplicates from Sorted List II");
        Console.WriteLine("Solution 1: In-place overwrite");
        RunDemoCases(samples, solution.DeleteDuplicates);
        Console.WriteLine();
        Console.WriteLine("Solution 2: Dummy node predecessor");
        RunDemoCases(samples, solution.DeleteDuplicates2);
    }

    /// <summary>
    /// 移除已排序鏈結串列中所有重複值節點。
    /// 解題概念是利用排序特性讓相同值必定相鄰，透過 current 掃描相同值群組，
    /// 並用 prev 記錄上一個確認保留的節點。輸入可以是 null 或已排序串列；
    /// 輸出為只保留出現一次數值的串列頭節點，若沒有節點可保留則回傳 null。
    /// </summary>
    /// <param name="head">已依非遞減順序排序的鏈結串列頭節點；可為 null。</param>
    /// <returns>刪除所有重複值後的鏈結串列頭節點；若結果為空則回傳 null。</returns>
    public ListNode? DeleteDuplicates(ListNode? head)
    {
        ListNode? prev = null;
        ListNode? current = head;
        bool hasDuplicate = false;

        while (current != null && current.next != null)
        {
            if (current.val == current.next.val)
            {
                // 已排序串列的重複值會連續出現，先把相同值節點逐一略過。
                current.next = current.next.next;
                hasDuplicate = true;
            }
            else
            {
                if (hasDuplicate)
                {
                    // current 本身也屬於重複群組，改用下一個不同值覆蓋並繼續檢查。
                    current.val = current.next.val;
                    current.next = current.next.next;
                    hasDuplicate = false;
                }
                else
                {
                    prev = current;
                    current = current.next;
                }
            }
        }

        if (hasDuplicate)
        {
            if (prev == null)
            {
                return null;
            }

            // 重複群組一路延伸到尾端時，只能由上一個保留節點切斷尾端。
            prev.next = null;
        }

        return head;
    }

    /// <summary>
    /// 以 dummy node 與前驅指標移除已排序鏈結串列中所有重複值節點。
    /// 解題概念是讓 cur 永遠停在待判斷區段前一個節點，若 cur.next 與
    /// cur.next.next 值相同，就刪除整段相同值；否則 cur 前進。輸入可以是
    /// null 或已排序串列；輸出為只保留出現一次數值的串列頭節點，若沒有節點可保留則回傳 null。
    /// </summary>
    /// <param name="head">已依非遞減順序排序的鏈結串列頭節點；可為 null。</param>
    /// <returns>刪除所有重複值後的鏈結串列頭節點；若結果為空則回傳 null。</returns>
    public ListNode? DeleteDuplicates2(ListNode? head)
    {
        if (head == null)
        {
            return null;
        }

        ListNode dummy = new ListNode(0, head);
        ListNode cur = dummy;

        while (cur.next != null && cur.next.next != null)
        {
            if (cur.next.val == cur.next.next.val)
            {
                int duplicateValue = cur.next.val;

                // cur 停在重複群組前方，直接跳過整段 duplicateValue 節點。
                while (cur.next != null && cur.next.val == duplicateValue)
                {
                    cur.next = cur.next.next;
                }
            }
            else
            {
                cur = cur.next;
            }
        }

        return dummy.next;
    }

    /// <summary>
    /// 執行多組範例測資並輸出輸入與結果。
    /// 輸入為多組整數陣列與指定解法；每組測資會轉成鏈結串列後呼叫解法，
    /// 輸出為可直接比對 README 範例流程的主控台文字。
    /// </summary>
    /// <param name="samples">每一組已排序的整數測資。</param>
    /// <param name="solver">要展示的鏈結串列去重解法。</param>
    private static void RunDemoCases(int[][] samples, Func<ListNode?, ListNode?> solver)
    {
        for (int i = 0; i < samples.Length; i++)
        {
            ListNode? input = BuildList(samples[i]);
            ListNode? result = solver(CloneList(input));

            Console.WriteLine($"Case {i + 1}: input={FormatList(input)} output={FormatList(result)}");
        }
    }

    /// <summary>
    /// 將整數陣列建立為單向鏈結串列。
    /// 輸入為已排序或空陣列；輸出為對應的串列頭節點，若陣列沒有元素則回傳 null。
    /// </summary>
    /// <param name="values">要依序放入鏈結串列的整數值。</param>
    /// <returns>新建立的鏈結串列頭節點；若輸入沒有元素則回傳 null。</returns>
    private static ListNode? BuildList(params int[] values)
    {
        ListNode dummy = new ListNode();
        ListNode tail = dummy;

        foreach (int value in values)
        {
            ListNode node = new ListNode(value);
            tail.next = node;
            tail = node;
        }

        return dummy.next;
    }

    /// <summary>
    /// 複製鏈結串列，避免範例輸出因解法原地修改節點而影響原始輸入展示。
    /// 輸入可以是 null 或任意單向鏈結串列；輸出為值相同但節點參考獨立的新串列。
    /// </summary>
    /// <param name="head">要複製的鏈結串列頭節點；可為 null。</param>
    /// <returns>複製後的鏈結串列頭節點；若輸入為 null 則回傳 null。</returns>
    private static ListNode? CloneList(ListNode? head)
    {
        ListNode dummy = new ListNode();
        ListNode tail = dummy;
        ListNode? current = head;

        while (current != null)
        {
            ListNode node = new ListNode(current.val);
            tail.next = node;
            tail = node;
            current = current.next;
        }

        return dummy.next;
    }

    /// <summary>
    /// 將鏈結串列格式化為主控台與 README 使用的陣列字串。
    /// 輸入可以是 null 或任意單向鏈結串列；輸出格式為 "[1,2,3]"，
    /// 若串列為空則輸出 "[]"。
    /// </summary>
    /// <param name="head">要格式化的鏈結串列頭節點；可為 null。</param>
    /// <returns>代表鏈結串列內容的陣列格式字串。</returns>
    private static string FormatList(ListNode? head)
    {
        List<int> values = [];
        ListNode? current = head;

        while (current != null)
        {
            values.Add(current.val);
            current = current.next;
        }

        return $"[{string.Join(",", values)}]";
    }
}
