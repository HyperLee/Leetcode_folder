namespace leetcode_203;

class Program
{
    /// <summary>
    /// 表示 LeetCode 203 題目使用的單向鏈結串列節點，節點值為整數，並透過 next 串接下一個節點。
    /// </summary>
    public class ListNode
    {
        public int val;
        public ListNode? next;

        /// <summary>
        /// 初始化一個鏈結串列節點，可指定節點值與下一個節點；若 next 為 null，表示目前節點是串列尾端。
        /// </summary>
        /// <param name="val">目前節點儲存的整數值。</param>
        /// <param name="next">下一個節點；若為 null，代表沒有下一個節點。</param>
        public ListNode(int val = 0, ListNode? next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    /// <summary>
    /// 203. Remove Linked List Elements
    /// https://leetcode.com/problems/remove-linked-list-elements/description/
    /// 203. 移除鏈結串列元素
    /// https://leetcode.cn/problems/remove-linked-list-elements/description/
    ///
    /// Given the head of a linked list and an integer val, remove all the nodes of the linked list that has Node.val == val, and return the new head.
    /// 給定一個鏈結串列的頭節點 head 與一個整數 val，請移除鏈結串列中所有 Node.val == val 的節點，並回傳新的頭節點。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solver = new Program();

        Console.WriteLine("LeetCode 203 - Remove Linked List Elements");
        Console.WriteLine("==================================================");

        RunCase(
            solver,
            "Case 1 - Middle and tail nodes match",
            new int[] { 1, 2, 6, 3, 4, 5, 6 },
            6,
            new int[] { 1, 2, 3, 4, 5 });
        RunCase(
            solver,
            "Case 2 - Empty list",
            Array.Empty<int>(),
            1,
            Array.Empty<int>());
        RunCase(
            solver,
            "Case 3 - Every node is removed",
            new int[] { 7, 7, 7, 7 },
            7,
            Array.Empty<int>());
        RunCase(
            solver,
            "Case 4 - Head and internal nodes match",
            new int[] { 6, 1, 2, 6, 3, 6 },
            6,
            new int[] { 1, 2, 3 });
        RunCase(
            solver,
            "Case 5 - No node matches",
            new int[] { 1, 2, 3 },
            4,
            new int[] { 1, 2, 3 });
    }

    /// <summary>
    /// 使用遞迴方式移除所有節點值等於指定 val 的節點。
    /// 核心概念是先遞迴處理後續子鏈結串列，再決定目前 head 是否保留。
    /// 輸入可為空鏈結串列；輸出為移除完成後的新頭節點。
    /// </summary>
    /// <param name="head">原始鏈結串列頭節點；若為 null，表示空串列。</param>
    /// <param name="val">要移除的目標節點值。</param>
    /// <returns>移除所有目標值節點後的新頭節點；若全部刪除則回傳 null。</returns>
    public ListNode? RemoveElements(ListNode? head, int val)
    {
        if (head is null)
        {
            return null;
        }

        // 先把後面子鏈結串列清乾淨，回來時目前節點只要判斷自己要不要保留即可。
        head.next = RemoveElements(head.next, val);

        if (head.val == val)
        {
            return head.next;
        }

        return head;
    }

    /// <summary>
    /// 使用迭代與 dummy head 移除所有節點值等於指定 val 的節點。
    /// 核心概念是讓每次刪除都有穩定的前一個節點可操作，特別能正確處理頭節點被刪除的情況。
    /// 輸入可為空鏈結串列；輸出為移除完成後的新頭節點。
    /// </summary>
    /// <param name="head">原始鏈結串列頭節點；若為 null，表示空串列。</param>
    /// <param name="val">要移除的目標節點值。</param>
    /// <returns>移除所有目標值節點後的新頭節點；若全部刪除則回傳 null。</returns>
    public ListNode? RemoveElements2(ListNode? head, int val)
    {
        // Dummy head 讓真正 head 被刪除時，仍然有固定的前驅節點可以重新接回鏈結。
        ListNode dummyHead = new ListNode(0, head);
        ListNode current = dummyHead;

        while (current.next is not null)
        {
            if (current.next.val == val)
            {
                current.next = current.next.next;
            }
            else
            {
                current = current.next;
            }
        }

        return dummyHead.next;
    }

    /// <summary>
    /// 根據整數陣列建立鏈結串列，讓 sample harness 與解法可以直接使用固定測試資料。
    /// 空陣列會建立空鏈結串列，輸出為對應的頭節點。
    /// </summary>
    /// <param name="values">要轉成鏈結串列的整數序列。</param>
    /// <returns>依照輸入順序建立的鏈結串列頭節點；若 values 為空則回傳 null。</returns>
    private static ListNode? BuildList(int[] values)
    {
        ListNode? head = null;
        ListNode? tail = null;

        foreach (int value in values)
        {
            ListNode node = new ListNode(value);

            if (head is null)
            {
                head = node;
                tail = node;
            }
            else
            {
                tail!.next = node;
                tail = node;
            }
        }

        return head;
    }

    /// <summary>
    /// 深拷貝一份鏈結串列，避免多個解法共用同一組 next 參考而互相影響結果。
    /// 輸入可為空鏈結串列；輸出為結構相同但節點實體獨立的新鏈結串列。
    /// </summary>
    /// <param name="head">要複製的原始鏈結串列頭節點；若為 null，表示空串列。</param>
    /// <returns>與輸入內容相同的新鏈結串列；若輸入為 null 則回傳 null。</returns>
    private static ListNode? CloneList(ListNode? head)
    {
        if (head is null)
        {
            return null;
        }

        ListNode newHead = new ListNode(head.val);
        ListNode source = head.next!;
        ListNode target = newHead;

        while (source is not null)
        {
            target.next = new ListNode(source.val);
            target = target.next;
            source = source.next!;
        }

        return newHead;
    }

    /// <summary>
    /// 將鏈結串列格式化成 `[1,2,3]` 這種可讀字串，方便主控台輸出與 README 範例同步。
    /// 輸入可為空鏈結串列；輸出永遠是非 null 的字串表示。
    /// </summary>
    /// <param name="head">要格式化的鏈結串列頭節點；若為 null，表示空串列。</param>
    /// <returns>鏈結串列的字串表示法；空串列會回傳 `[]`。</returns>
    private static string FormatList(ListNode? head)
    {
        if (head is null)
        {
            return "[]";
        }

        List<string> values = new List<string>();
        ListNode? current = head;

        while (current is not null)
        {
            values.Add(current.val.ToString());
            current = current.next;
        }

        return $"[{string.Join(",", values)}]";
    }

    /// <summary>
    /// 執行一組固定測試案例，輸出輸入資料、預期結果、兩種解法的實際結果與 PASS/FAIL。
    /// 這個方法假設輸入陣列代表合法鏈結串列資料，輸出只負責展示並比對結果，不改變預期值。
    /// </summary>
    /// <param name="solver">提供遞迴與迭代解法的程式實例。</param>
    /// <param name="caseName">案例名稱，用來說明目前執行的是哪一種情境。</param>
    /// <param name="values">要建立成鏈結串列的輸入資料。</param>
    /// <param name="removeValue">要從鏈結串列中移除的目標值。</param>
    /// <param name="expectedValues">預期剩下的節點值順序。</param>
    private static void RunCase(Program solver, string caseName, int[] values, int removeValue, int[] expectedValues)
    {
        ListNode? input = BuildList(values);
        string inputText = FormatList(input);
        string expectedText = FormatList(BuildList(expectedValues));

        // 兩個解法都會改動 next 指標，因此每次執行前都要拿獨立副本來避免互相污染。
        ListNode? recursiveResult = solver.RemoveElements(CloneList(input), removeValue);
        ListNode? iterativeResult = solver.RemoveElements2(CloneList(input), removeValue);

        string recursiveText = FormatList(recursiveResult);
        string iterativeText = FormatList(iterativeResult);
        bool recursivePass = recursiveText == expectedText;
        bool iterativePass = iterativeText == expectedText;

        Console.WriteLine(caseName);
        Console.WriteLine($"Input: {inputText}");
        Console.WriteLine($"Remove: {removeValue}");
        Console.WriteLine($"Expected: {expectedText}");
        Console.WriteLine($"Recursive: {recursiveText} | {(recursivePass ? "PASS" : "FAIL")}");
        Console.WriteLine($"Iterative: {iterativeText} | {(iterativePass ? "PASS" : "FAIL")}");
        Console.WriteLine();
    }
}
