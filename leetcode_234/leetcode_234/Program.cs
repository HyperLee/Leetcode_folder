namespace leetcode_234;

class Program
{
    /// <summary>
    /// 單向連結串列節點；用來承載節點值與下一個節點參照，輸入為節點值與可選的下一個節點，輸出為可串接的 ListNode 物件。
    /// </summary>
    public class ListNode
    {
        public int val;
        public ListNode? next;

        /// <summary>
        /// 建立單向連結串列節點；輸入節點值與下一個節點，若未提供下一個節點則代表串列尾端，輸出為初始化完成的節點。
        /// </summary>
        /// <param name="val">目前節點的整數值。</param>
        /// <param name="next">下一個節點；null 代表目前節點是尾端。</param>
        public ListNode(int val = 0, ListNode? next = null)
        {
            this.val = val;
            this.next = next;
        }
    }


    /// <summary>
    /// 234. Palindrome Linked List
    /// https://leetcode.com/problems/palindrome-linked-list/description/
    /// 234. 回文链表
    /// https://leetcode.cn/problems/palindrome-linked-list/description/
    ///
    /// Given the head of a singly linked list, return true if it is a palindrome or false otherwise.
    ///
    /// 給定一個單向連結串列的 head，如果它是回文，則回傳 true；否則回傳 false。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new Program();

        Console.WriteLine("Palindrome Linked List sample checks");
        Console.WriteLine();

        RunSampleCase(solution, "Case 1 - even palindrome", new[] { 1, 2, 2, 1 }, true);
        RunSampleCase(solution, "Case 2 - not palindrome", new[] { 1, 2 }, false);
        RunSampleCase(solution, "Case 3 - odd palindrome", new[] { 1, 2, 3, 2, 1 }, true);
        RunSampleCase(solution, "Case 4 - single node", new[] { 1 }, true);
    }

    private ListNode? realHead;
    private bool isCrossed;

    /// <summary>
    /// 執行一筆範例資料；輸入解題物件、案例名稱、節點值與預期結果，建立連結串列後同時驗證兩種解法，輸出可讀的示範結果。
    /// </summary>
    /// <param name="solution">包含解法方法的 Program 物件。</param>
    /// <param name="caseName">顯示在主控台的案例名稱。</param>
    /// <param name="values">依序建立連結串列的節點值。</param>
    /// <param name="expected">此案例預期是否為回文。</param>
    private static void RunSampleCase(Program solution, string caseName, int[] values, bool expected)
    {
        ListNode? head = BuildLinkedList(values);
        bool recursiveResult = solution.IsPalindrome(head);
        bool listResult = solution.IsPalindrome2(head);

        Console.WriteLine($"{caseName}: [{FormatLinkedList(head)}]");
        Console.WriteLine($"  Recursive compare: {recursiveResult} (expected: {expected})");
        Console.WriteLine($"  List two-pointer:  {listResult} (expected: {expected})");
        Console.WriteLine($"  Status: {(recursiveResult == expected && listResult == expected ? "PASS" : "FAIL")}");
        Console.WriteLine();
    }

    /// <summary>
    /// 依輸入陣列建立單向連結串列；輸入為節點值序列，允許空陣列，輸出為串列 head，若無節點則回傳 null。
    /// </summary>
    /// <param name="values">用來建立節點的整數值陣列。</param>
    /// <returns>建立完成的連結串列 head；空陣列則回傳 null。</returns>
    private static ListNode? BuildLinkedList(int[] values)
    {
        if (values.Length == 0)
        {
            return null;
        }

        ListNode head = new ListNode(values[0]);
        ListNode currentNode = head;

        for (int i = 1; i < values.Length; i++)
        {
            ListNode nextNode = new ListNode(values[i]);
            currentNode.next = nextNode;
            currentNode = nextNode;
        }

        return head;
    }

    /// <summary>
    /// 將連結串列轉成易讀文字；輸入為串列 head，允許 null，輸出為以箭頭串接的節點值或 empty。
    /// </summary>
    /// <param name="head">要格式化的連結串列 head。</param>
    /// <returns>節點值的文字表示。</returns>
    private static string FormatLinkedList(ListNode? head)
    {
        List<int> values = new List<int>();
        ListNode? currentNode = head;

        while (currentNode != null)
        {
            values.Add(currentNode.val);
            currentNode = currentNode.next;
        }

        return values.Count == 0 ? "empty" : string.Join(" -> ", values);
    }

    /// <summary>
    /// 方法一：遞迴模擬前後雙指針；輸入為單向連結串列 head，允許 null，輸出為節點值是否由前往後與由後往前讀取一致。
    /// 解題概念是先遞迴到尾端，回傳時讓遞迴節點代表右側指標，並用 realHead 從左側往右移動；當左右指標相遇或交錯時停止比較。
    /// </summary>
    /// <param name="head">要判斷的連結串列 head；null 會視為回文。</param>
    /// <returns>若連結串列節點值形成回文則回傳 true，否則回傳 false。</returns>
    public bool IsPalindrome(ListNode? head)
    {
        realHead = head;
        isCrossed = false;

        return CompareFromTail(head);
    }

    /// <summary>
    /// 遞迴比較左右兩端節點；輸入為目前遞迴節點，輸出為目前已比較區段是否仍為回文。
    /// 此方法依賴 realHead 保存左側指標，並在指標相遇或交錯後避免重複比較。
    /// </summary>
    /// <param name="head">目前遞迴處理的節點；null 代表已越過尾端。</param>
    /// <returns>目前遞迴回傳路徑上的比較結果。</returns>
    private bool CompareFromTail(ListNode? head)
    {
        if (head == null)
        {
            return true;
        }

        // 先走到串列尾端，回傳展開時的 head 就會由右往左移動。
        bool result = CompareFromTail(head.next);

        if (isCrossed)
        {
            return result;
        }

        if (realHead == null)
        {
            return result;
        }

        // 左右指標相遇或交錯後，代表中間以前都已完成比對。
        if (head == realHead || realHead.next == head)
        {
            isCrossed = true;
        }

        result = result && (head.val == realHead.val);
        realHead = realHead.next;

        return result;
    }

    /// <summary>
    /// 方法二：複製節點值到 List 後使用雙指針；輸入為單向連結串列 head，允許 null，輸出為節點值是否形成回文。
    /// 解題概念是先取得可隨機存取的值序列，再從序列頭尾同步往中間比較；比較的是節點值，不是節點參照。
    /// </summary>
    /// <param name="head">要判斷的連結串列 head；null 會視為回文。</param>
    /// <returns>若連結串列節點值形成回文則回傳 true，否則回傳 false。</returns>
    public bool IsPalindrome2(ListNode? head)
    {
        List<int> values = new List<int>();
        ListNode? currentNode = head;

        // 單向串列無法從尾端往前走，因此先複製節點值，讓頭尾索引可以直接存取。
        while (currentNode != null)
        {
            values.Add(currentNode.val);
            currentNode = currentNode.next;
        }

        int front = 0;
        int back = values.Count - 1;

        // 每次比較目前最左與最右的值，全部相同才是回文。
        while (front < back)
        {
            if (values[back] != values[front])
            {
                return false;
            }

            front++;
            back--;
        }

        return true;
    }
}
