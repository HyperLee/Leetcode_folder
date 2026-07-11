namespace leetcode_445;

internal static class Program
{
    private static int s_checks;
    private static int s_passed;

    /// <summary>
    /// 表示 LeetCode 445 使用的單向鏈結串列節點；每個節點儲存一個十進位數字，next 為 null 時表示串列尾端。
    /// </summary>
    public class ListNode
    {
        public int val;
        public ListNode? next;

        /// <summary>
        /// 建立一個鏈結串列節點，可指定節點值與下一個節點；未指定 next 時，此節點為目前串列尾端。
        /// </summary>
        /// <param name="val">節點儲存的十進位數字。</param>
        /// <param name="next">下一個節點；若為 null，表示沒有後繼節點。</param>
        public ListNode(int val = 0, ListNode? next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    /// <summary>
    /// 445. Add Two Numbers II / 兩數相加 II
    /// https://leetcode.com/problems/add-two-numbers-ii/
    /// https://leetcode.cn/problems/add-two-numbers-ii/
    /// Given two non-empty linked lists that store non-negative integers with the most significant digit first, return their sum as a linked list.
    /// 給定兩個非空鏈結串列，以最高位到最低位儲存非負整數；請回傳兩數相加後的鏈結串列。
    /// </summary>
    private static void Main()
    {
        int[] maximumDigits = new int[100];
        Array.Fill(maximumDigits, 9);
        int[] maximumExpected = new int[101];
        maximumExpected[0] = 1;

        (string Name, int[] L1, int[] L2, int[] Expected)[] cases =
        [
            ("Official example: l1 is longer", [7, 2, 4, 3], [5, 6, 4], [7, 8, 0, 7]),
            ("Official example: equal lengths", [2, 4, 3], [5, 6, 4], [8, 0, 7]),
            ("Official example: zero", [0], [0], [0]),
            ("Regression: l1 is shorter", [5, 6, 4], [7, 2, 4, 3], [7, 8, 0, 7]),
            ("Cascading carry", [9, 9, 9, 9, 9, 9, 9], [9, 9, 9, 9], [1, 0, 0, 0, 9, 9, 9, 8]),
            ("Upper-bound spot check: 100 digits", maximumDigits, [1], maximumExpected)
        ];

        Console.WriteLine("LeetCode 445 acceptance harness");
        Console.WriteLine();

        foreach ((string name, int[] l1Values, int[] l2Values, int[] expectedValues) in cases)
        {
            RunCase(name, l1Values, l2Values, expectedValues);
        }

        Console.WriteLine($"Summary: {s_passed}/{s_checks} checks passed.");

        if (s_passed != s_checks)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 使用兩個 Stack 保留各輸入串列的節點值，從最低位開始處理進位並以前插方式建立結果；輸入必須是題目保證的非空、最高位在前且無前導零串列，回傳新的相加結果且不修改輸入。
    /// </summary>
    /// <param name="l1">代表第一個非負整數的非空鏈結串列，最高位在前。</param>
    /// <param name="l2">代表第二個非負整數的非空鏈結串列，最高位在前。</param>
    /// <returns>代表兩數總和的新非空鏈結串列，最高位在前。</returns>
    public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        Stack<int> firstDigits = new Stack<int>();
        Stack<int> secondDigits = new Stack<int>();

        for (ListNode? current = l1; current is not null; current = current.next)
        {
            firstDigits.Push(current.val);
        }

        for (ListNode? current = l2; current is not null; current = current.next)
        {
            secondDigits.Push(current.val);
        }

        int sum = firstDigits.Pop() + secondDigits.Pop();
        int carry = sum / 10;
        ListNode result = new ListNode(sum % 10);

        while (firstDigits.Count > 0 || secondDigits.Count > 0 || carry != 0)
        {
            sum = carry;

            if (firstDigits.Count > 0)
            {
                sum += firstDigits.Pop();
            }

            if (secondDigits.Count > 0)
            {
                sum += secondDigits.Pop();
            }

            carry = sum / 10;

            // 由最低位往最高位計算時，將目前位數接到結果頭端即可維持最高位在前。
            result = new ListNode(sum % 10, result);
        }

        return result;
    }

    /// <summary>
    /// 依輸入陣列順序建立非空鏈結串列，供 acceptance harness 建立題目契約內的測試資料使用。
    /// </summary>
    /// <param name="values">至少含一個十進位數字的陣列。</param>
    /// <returns>與 values 順序相同的非空鏈結串列頭節點。</returns>
    private static ListNode BuildList(IReadOnlyList<int> values)
    {
        ListNode head = new ListNode(values[0]);
        ListNode tail = head;

        for (int i = 1; i < values.Count; i++)
        {
            tail.next = new ListNode(values[i]);
            tail = tail.next;
        }

        return head;
    }

    /// <summary>
    /// 比對鏈結串列與預期數字序列是否逐節點相同，確保大型案例不會因顯示摘要而漏掉中間位數的錯誤。
    /// </summary>
    /// <param name="head">要比對的鏈結串列頭節點。</param>
    /// <param name="expected">依最高位到最低位排列的預期數字序列。</param>
    /// <returns>節點數與每個節點值都與 expected 相同時回傳 true，否則回傳 false。</returns>
    private static bool MatchesValues(ListNode? head, IReadOnlyList<int> expected)
    {
        int index = 0;

        for (ListNode? current = head; current is not null; current = current.next)
        {
            if (index >= expected.Count || current.val != expected[index])
            {
                return false;
            }

            index++;
        }

        return index == expected.Count;
    }

    /// <summary>
    /// 執行一個固定案例，驗證相加結果與兩個輸入串列皆符合預期，並輸出輸入、Expected、Actual 與 PASS/FAIL。
    /// </summary>
    /// <param name="name">案例名稱，說明本次驗證涵蓋的題目情境。</param>
    /// <param name="l1Values">第一個輸入數字的各位數，最高位在前。</param>
    /// <param name="l2Values">第二個輸入數字的各位數，最高位在前。</param>
    /// <param name="expectedValues">預期總和的各位數，最高位在前。</param>
    private static void RunCase(string name, int[] l1Values, int[] l2Values, int[] expectedValues)
    {
        ListNode l1 = BuildList(l1Values);
        ListNode l2 = BuildList(l2Values);
        string l1Before = FormatList(l1);
        string l2Before = FormatList(l2);
        ListNode actual = AddTwoNumbers(l1, l2);
        bool outputMatches = MatchesValues(actual, expectedValues);
        bool inputsUnchanged = MatchesValues(l1, l1Values) && MatchesValues(l2, l2Values);
        bool passed = outputMatches && inputsUnchanged;

        Console.WriteLine($"Case {s_checks + 1}: {name}");
        Console.WriteLine($"Input: l1 = {l1Before}, l2 = {l2Before}");
        Console.WriteLine($"Expected: result = {FormatValues(expectedValues)}; inputs unchanged");
        Console.WriteLine($"Actual: result = {FormatList(actual)}; l1 after = {FormatList(l1)}; l2 after = {FormatList(l2)}");
        Console.WriteLine(passed ? "PASS" : "FAIL");
        Console.WriteLine();

        RecordCheck(passed);
    }

    /// <summary>
    /// 將鏈結串列節點轉成可讀字串；大型串列只保留首尾各三位與節點總數，避免 acceptance output 被大量重複數字淹沒。
    /// </summary>
    /// <param name="head">要格式化的鏈結串列頭節點；為 null 時表示空串列。</param>
    /// <returns>以方括號表示的節點值字串；大型串列會附帶節點數摘要。</returns>
    private static string FormatList(ListNode? head)
    {
        List<int> values = new List<int>();

        for (ListNode? current = head; current is not null; current = current.next)
        {
            values.Add(current.val);
        }

        return FormatValues(values);
    }

    /// <summary>
    /// 將數字序列格式化為主控台與 README 共用的穩定表示法；超過十二個節點時只顯示首尾摘要與總數。
    /// </summary>
    /// <param name="values">要格式化的數字序列。</param>
    /// <returns>完整或摘要化的方括號序列字串。</returns>
    private static string FormatValues(IReadOnlyList<int> values)
    {
        const int maximumDetailedNodes = 12;

        if (values.Count <= maximumDetailedNodes)
        {
            return $"[{string.Join(",", values)}]";
        }

        return $"[{string.Join(",", values.Take(3))},...,{string.Join(",", values.Skip(values.Count - 3))}] ({values.Count} nodes)";
    }

    /// <summary>
    /// 記錄一項 acceptance check；每次呼叫都遞增總數，通過時同步遞增通過數。
    /// </summary>
    /// <param name="passed">目前案例是否通過輸出與輸入不變性驗證。</param>
    private static void RecordCheck(bool passed)
    {
        s_checks++;

        if (passed)
        {
            s_passed++;
        }
    }
}
