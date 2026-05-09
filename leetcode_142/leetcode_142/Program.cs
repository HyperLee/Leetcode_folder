namespace leetcode_142;

class Program
{
    /// <summary>
    /// 表示單向鏈結串列節點。輸入條件為整數節點值，next 可為 null 或指向下一個節點；
    /// 輸出結果由呼叫端透過節點參考串接成一般鏈結串列或環形鏈結串列。
    /// </summary>
    public class ListNode
    {
        public int val;
        public ListNode? next;

        /// <summary>
        /// 建立指定節點值的鏈結串列節點。輸入為節點值 x；輸出為 next 預設為 null 的節點。
        /// </summary>
        /// <param name="x">節點儲存的整數值。</param>
        public ListNode(int x)
        {
            val = x;
            next = null;
        }
    }

    /// <summary>
    /// 142. Linked List Cycle II
    /// https://leetcode.com/problems/linked-list-cycle-ii/description/
    /// 142. 環形鏈結串列 II
    /// https://leetcode.cn/problems/linked-list-cycle-ii/description/
    ///
    /// English:
    /// Given the head of a linked list, return the node where the cycle begins.
    /// If there is no cycle, return null.
    ///
    /// There is a cycle in a linked list if there is some node in the list that
    /// can be reached again by continuously following the next pointer. Internally,
    /// pos is used to denote the index of the node that tail's next pointer is
    /// connected to (0-indexed). It is -1 if there is no cycle. Note that pos is
    /// not passed as a parameter.
    ///
    /// Do not modify the linked list.
    ///
    /// 繁體中文:
    /// 給定一個鏈結串列的頭節點 head，請回傳環開始的節點。
    /// 如果鏈結串列中沒有環，請回傳 null。
    ///
    /// 如果鏈結串列中存在某個節點，可以透過不斷沿著 next 指標前進而再次到達該節點，
    /// 則表示這個鏈結串列中存在環。在內部，pos 用來表示尾節點的 next 指標連接到的節點索引
    /// (索引從 0 開始)。如果沒有環，pos 為 -1。請注意，pos 不會作為參數傳入。
    ///
    /// 請勿修改鏈結串列。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new Program();

        RunSampleCases("DetectCycle", solution.DetectCycle);
        Console.WriteLine();
        RunSampleCases("DetectCycle2", solution.DetectCycle2);
    }

    /// <summary>
    /// 使用 Floyd 快慢指標找出環形鏈結串列的入環節點。
    /// 解題概念是先讓快慢指標在環內相遇，再讓一個指標回到 head，兩者同步前進後會在入環點相遇。
    /// 輸入條件為可能為 null、可能有環且不可被修改的鏈結串列頭節點；輸出為入環節點，無環時回傳 null。
    /// </summary>
    /// <param name="head">鏈結串列頭節點，可能為 null。</param>
    /// <returns>環開始的節點；若鏈結串列沒有環則回傳 null。</returns>
    public ListNode? DetectCycle(ListNode? head)
    {
        ListNode? oneStep = head;
        ListNode? twoStep = head;

        while (twoStep != null && twoStep.next != null)
        {
            oneStep = oneStep!.next;
            twoStep = twoStep.next.next;

            if (oneStep == twoStep)
            {
                ListNode? oneStep2 = head;

                // 相遇後，head 到入環點的距離等於相遇點繼續走到入環點的距離。
                while (oneStep2 != oneStep)
                {
                    oneStep = oneStep!.next;
                    oneStep2 = oneStep2!.next;
                }

                return oneStep2;
            }
        }

        return null;
    }

    /// <summary>
    /// 使用 Floyd 快慢指標的官方推導版流程找出入環節點。
    /// 解題概念是以 fast 走兩步、slow 走一步偵測是否有環，若相遇則根據距離等式 a = c + (n - 1)r 找到環入口。
    /// 輸入條件為可能為 null、可能有環且不可被修改的鏈結串列頭節點；輸出為入環節點，無環時回傳 null。
    /// </summary>
    /// <param name="head">鏈結串列頭節點，可能為 null。</param>
    /// <returns>環開始的節點；若鏈結串列沒有環則回傳 null。</returns>
    public ListNode? DetectCycle2(ListNode? head)
    {
        if (head == null)
        {
            return null;
        }

        ListNode? slow = head;
        ListNode? fast = head;

        while (fast != null && fast.next != null)
        {
            slow = slow!.next;
            fast = fast.next.next;

            if (fast == slow)
            {
                ListNode? ptr = head;

                // ptr 從 head 出發，slow 從相遇點出發，同速前進會在入環點會合。
                while (ptr != slow)
                {
                    ptr = ptr!.next;
                    slow = slow!.next;
                }

                return ptr;
            }
        }

        return null;
    }

    /// <summary>
    /// 執行一組可重現的範例測資，驗證指定解法是否能正確找出環入口。
    /// 輸入條件為解法名稱與不修改串列的偵測函式；輸出結果會列印每筆案例的預期、實際與 PASS/FAIL。
    /// </summary>
    /// <param name="solutionName">目前執行的解法名稱。</param>
    /// <param name="detectCycle">接收鏈結串列頭節點並回傳入環節點的解法。</param>
    private static void RunSampleCases(string solutionName, Func<ListNode?, ListNode?> detectCycle)
    {
        CycleCase[] cases =
        [
            new CycleCase("Empty list", [], -1),
            new CycleCase("Single node without cycle", [1], -1),
            new CycleCase("Single node cycle to self", [1], 0),
            new CycleCase("No cycle", [3, 2, 0, -4], -1),
            new CycleCase("Cycle at head", [1, 2, 3, 4], 0),
            new CycleCase("Cycle in middle", [3, 2, 0, -4], 1),
        ];

        Console.WriteLine($"LeetCode 142 - Linked List Cycle II ({solutionName})");

        foreach (CycleCase testCase in cases)
        {
            (ListNode? head, ListNode? expectedEntry) = BuildListWithCycle(testCase.Values, testCase.Position);
            ListNode? actualEntry = detectCycle(head);
            string result = ReferenceEquals(actualEntry, expectedEntry) ? "PASS" : "FAIL";

            Console.WriteLine(
                $"{testCase.Name}: values={FormatValues(testCase.Values)}, pos={testCase.Position}, " +
                $"expected={FormatNode(expectedEntry)}, actual={FormatNode(actualEntry)}, Result: {result}");
        }
    }

    /// <summary>
    /// 依照 LeetCode 的 values 與 pos 表示法建立測試用鏈結串列。
    /// 輸入條件為節點值陣列，以及 -1 或合法節點索引；輸出為 head 與預期入環節點參考。
    /// </summary>
    /// <param name="values">依序建立鏈結串列的節點值。</param>
    /// <param name="position">尾節點 next 要連回的節點索引；-1 表示不建立環。</param>
    /// <returns>建立完成的 head，以及預期的入環節點參考；無環時預期節點為 null。</returns>
    private static (ListNode? Head, ListNode? ExpectedEntry) BuildListWithCycle(int[] values, int position)
    {
        if (values.Length == 0)
        {
            return (null, null);
        }

        ListNode[] nodes = new ListNode[values.Length];

        for (int i = 0; i < values.Length; i++)
        {
            nodes[i] = new ListNode(values[i]);

            if (i > 0)
            {
                nodes[i - 1].next = nodes[i];
            }
        }

        if (position >= 0)
        {
            nodes[^1].next = nodes[position];
        }

        ListNode? expectedEntry = position >= 0 ? nodes[position] : null;
        return (nodes[0], expectedEntry);
    }

    /// <summary>
    /// 將測試用節點值轉成不走訪鏈結串列的顯示文字，避免環形串列造成無窮迴圈。
    /// 輸入條件為原始節點值陣列；輸出為 README 與主控台可讀的陣列字串。
    /// </summary>
    /// <param name="values">原始測試節點值。</param>
    /// <returns>格式化後的節點值字串。</returns>
    private static string FormatValues(int[] values)
    {
        return $"[{string.Join(",", values)}]";
    }

    /// <summary>
    /// 將節點參考轉成顯示文字。
    /// 輸入條件為可能為 null 的節點；輸出為節點值字串，若節點不存在則輸出 null。
    /// </summary>
    /// <param name="node">要顯示的節點參考。</param>
    /// <returns>節點值或 null 字串。</returns>
    private static string FormatNode(ListNode? node)
    {
        return node == null ? "null" : node.val.ToString();
    }

    /// <summary>
    /// 儲存單筆範例測資。
    /// 輸入條件為案例名稱、節點值與 LeetCode pos；輸出結果供範例 runner 建立測試串列。
    /// </summary>
    /// <param name="Name">案例名稱。</param>
    /// <param name="Values">節點值序列。</param>
    /// <param name="Position">入環索引；-1 表示無環。</param>
    private sealed record CycleCase(string Name, int[] Values, int Position);
}
