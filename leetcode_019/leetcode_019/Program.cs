namespace leetcode_019;

class Program
{
    /// <summary>
    /// 表示單向 linked list 的節點，供題目輸入、輸出與示例建立使用。
    /// </summary>
    public class ListNode
    {
        public int val;
        public ListNode? next;

        /// <summary>
        /// 建立一個 linked list 節點，可選擇同時串接下一個節點。
        /// </summary>
        /// <param name="val">目前節點儲存的整數值。</param>
        /// <param name="next">目前節點所指向的下一個節點，沒有則為 null。</param>
        public ListNode(int val = 0, ListNode? next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    /// <summary>
    /// 19. Remove Nth Node From End of List
    /// https://leetcode.com/problems/remove-nth-node-from-end-of-list/description/
    /// 19. 删除链表的倒数第 N 个结点
    /// https://leetcode.cn/problems/remove-nth-node-from-end-of-list/description/
    ///
    /// English:
    /// Given the head of a linked list, remove the nth node from the end of the list and return its head.
    ///
    /// 繁體中文:
    /// 給定一個 linked list 的 head，請移除該 linked list 中倒數第 n 個節點，並回傳移除後的 head。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solver = new Program();
        (int[] values, int n)[] demoCases =
        [
            (new int[] { 1, 2, 3, 4, 5 }, 2),
            (new int[] { 1 }, 1),
            (new int[] { 1, 2 }, 1),
            (new int[] { 1, 2 }, 2)
        ];

        Console.WriteLine("LeetCode 19 - Remove Nth Node From End of List");

        for (int i = 0; i < demoCases.Length; i++)
        {
            (int[] values, int n) demoCase = demoCases[i];
            ListNode? input1 = CreateList(demoCase.values);
            ListNode? input2 = CreateList(demoCase.values);
            ListNode? output1 = solver.RemoveNthFromEnd(input1, demoCase.n);
            ListNode? output2 = solver.RemoveNthFromEnd2(input2, demoCase.n);

            Console.WriteLine($"Case {i + 1}");
            Console.WriteLine($"Input : head = {FormatList(CreateList(demoCase.values))}, n = {demoCase.n}");
            Console.WriteLine($"Solution 1 (雙指針)   : {FormatList(output1)}");
            Console.WriteLine($"Solution 2 (計算長度) : {FormatList(output2)}");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 以雙指針在一次走訪內移除 linked list 倒數第 n 個節點。
    /// 解題概念是先讓前指針比後指針多走 n 步，再同步前進直到前指針抵達結尾，
    /// 這樣後指針就會停在待刪節點的前一個位置。
    /// 輸入條件為 head 符合題目定義的 linked list，且 n 介於 1 到 linked list 長度之間。
    /// 輸出結果為移除指定節點後的新 linked list head；若刪除原本 head，則回傳新的第一個節點。
    /// </summary>
    /// <param name="head">linked list 的起始節點。</param>
    /// <param name="n">要刪除的倒數第 n 個位置。</param>
    /// <returns>移除指定節點後的新 linked list head。</returns>
    public ListNode? RemoveNthFromEnd(ListNode? head, int n)
    {
        // dummy node 讓「刪除 head」和一般刪除情況可以用同一套邏輯處理。
        ListNode dummy = new ListNode(0, head);
        ListNode? first = head;
        ListNode second = dummy;

        // 先拉開 first 與 second 的距離，讓 two pointers 保持 n 個節點差。
        for (int i = 0; i < n; i++)
        {
            first = first!.next;
        }

        // 當 first 抵達結尾時，second 正好停在待刪節點的前一個位置。
        while (first != null)
        {
            first = first.next;
            second = second.next!;
        }

        second.next = second.next!.next;
        return dummy.next;
    }

    /// <summary>
    /// 依照整數序列建立 linked list，供主程式示例與輸出驗證使用。
    /// 輸入條件為任意整數陣列；空陣列代表空 linked list。
    /// 輸出結果為串接完成的 linked list head，若沒有元素則回傳 null。
    /// </summary>
    /// <param name="values">要依序放入 linked list 的整數資料。</param>
    /// <returns>建立完成的 linked list head。</returns>
    private static ListNode? CreateList(int[] values)
    {
        ListNode dummy = new ListNode();
        ListNode current = dummy;

        foreach (int value in values)
        {
            current.next = new ListNode(value);
            current = current.next;
        }

        return dummy.next;
    }

    /// <summary>
    /// 將 linked list 轉成易讀字串，方便在主程式與 README 中展示輸入輸出流程。
    /// 輸入條件為任意 linked list head，可為 null。
    /// 輸出結果為方括號格式字串，例如 [1, 2, 3]；空 linked list 會顯示為 []。
    /// </summary>
    /// <param name="head">要格式化的 linked list 起始節點。</param>
    /// <returns>可直接輸出的 linked list 內容字串。</returns>
    private static string FormatList(ListNode? head)
    {
        List<string> values = [];
        ListNode? current = head;

        while (current != null)
        {
            values.Add(current.val.ToString());
            current = current.next;
        }

        return $"[{string.Join(", ", values)}]";
    }

    /// <summary>
    /// 先計算 linked list 長度，再換算出正向索引位置來移除倒數第 n 個節點。
    /// 解題概念是先走訪一次取得總長度，接著把「倒數第 n 個」轉成「正數第 length - n + 1 個」，
    /// 便能在第二次走訪時找到待刪節點的前一個位置並完成刪除。
    /// 輸入條件為 head 符合題目定義的 linked list，且 n 介於 1 到 linked list 長度之間。
    /// 輸出結果為移除指定節點後的新 linked list head；若刪除原本 head，則回傳新的第一個節點。
    /// </summary>
    /// <param name="head">linked list 的起始節點。</param>
    /// <param name="n">要刪除的倒數第 n 個位置。</param>
    /// <returns>移除指定節點後的新 linked list head。</returns>
    public ListNode? RemoveNthFromEnd2(ListNode? head, int n)
    {
        // dummy node 讓刪除 head 時也能沿用相同的定位與刪除流程。
        ListNode dummy = new ListNode(0, head);
        // 先取得 linked list 總長度，才能把倒數位置換算成正向位置。
        int length = getLength(head);
        ListNode current = dummy;

        // 只要走到待刪節點的前一格即可，方便直接改接 next。
        for (int i = 0; i < length - n; i++)
        {
            current = current.next!;
        }

        // 略過目標節點，讓前一個節點直接指向目標節點的下一個節點。
        current.next = current.next!.next;
        return dummy.next;
    }

    /// <summary>
    /// 計算 linked list 的節點總數，供解法二換算待刪節點位置使用。
    /// 輸入條件為任意 linked list head，可為 null。
    /// 輸出結果為 linked list 的節點數量；空 linked list 會回傳 0。
    /// </summary>
    /// <param name="head">要計算長度的 linked list 起始節點。</param>
    /// <returns>linked list 的節點總數。</returns>
    public int getLength(ListNode? head)
    {
        int length = 0;

        while (head != null)
        {
            length++;
            head = head.next;
        }
        return length;
    }
}
