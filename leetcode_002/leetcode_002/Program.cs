using System.Text;

namespace leetcode_002;

class Program
{
    /// <summary>
    /// 表示反向 linked list 中的一個數字節點。
    /// </summary>
    public class ListNode
    {
        /// <summary>
        /// 目前節點儲存的數字。
        /// </summary>
        public int val;

        /// <summary>
        /// 指向下一個數字節點。
        /// </summary>
        public ListNode? next;

        /// <summary>
        /// 使用指定數字初始化節點。
        /// </summary>
        /// <param name="x">節點中的數字。</param>
        public ListNode(int x)
        {
            val = x;
        }
    }

    /// <summary>
    /// 執行題目範例與額外測試資料，並列印兩種解法的輸出結果。
    /// </summary>
    /// <param name="args">命令列參數。</param>
    private static void Main(string[] args)
    {
        Program solver = new Program();

        Console.WriteLine("LeetCode 2 - Add Two Numbers");
        Console.WriteLine();

        // 官方常見範例，涵蓋一般情況與基本進位。
        RunExample(solver, "範例 1", new[] { 2, 4, 3 }, new[] { 5, 6, 4 }, "[7 -> 0 -> 8]");
        RunExample(solver, "範例 2", new[] { 0 }, new[] { 0 }, "[0]");
        RunExample(solver, "範例 3", new[] { 9, 9, 9, 9, 9, 9, 9 }, new[] { 9, 9, 9, 9 }, "[8 -> 9 -> 9 -> 9 -> 0 -> 0 -> 0 -> 1]");

        // 額外補充單邊為 0、單一節點進位與含 0 位數的案例。
        RunExample(solver, "額外測試 1", new[] { 1, 8 }, new[] { 0 }, "[1 -> 8]");
        RunExample(solver, "額外測試 2", new[] { 9 }, new[] { 1 }, "[0 -> 1]");
        RunExample(solver, "額外測試 3", new[] { 5, 0, 5 }, new[] { 5, 9, 5 }, "[0 -> 0 -> 1 -> 1]");
    }

    /// <summary>
    /// 建立測試案例並輸出兩種解法的執行結果。
    /// </summary>
    /// <param name="solver">題目解題類別實例。</param>
    /// <param name="title">案例標題。</param>
    /// <param name="leftDigits">第一個反向 linked list 的數字。</param>
    /// <param name="rightDigits">第二個反向 linked list 的數字。</param>
    /// <param name="expected">預期輸出。</param>
    private static void RunExample(Program solver, string title, int[] leftDigits, int[] rightDigits, string expected)
    {
        string leftText = FormatList(CreateList(leftDigits));
        string rightText = FormatList(CreateList(rightDigits));

        ListNode firstResult = solver.AddTwoNumbers(CreateList(leftDigits), CreateList(rightDigits));
        ListNode secondResult = solver.AddTwoNumbers2(CreateList(leftDigits), CreateList(rightDigits));

        Console.WriteLine(title);
        Console.WriteLine($"l1 = {leftText}");
        Console.WriteLine($"l2 = {rightText}");
        Console.WriteLine($"AddTwoNumbers  = {FormatList(firstResult)}");
        Console.WriteLine($"AddTwoNumbers2 = {FormatList(secondResult)}");
        Console.WriteLine($"Expected       = {expected}");
        Console.WriteLine(new string('-', 50));
    }

    /// <summary>
    /// 依照輸入陣列建立反向 linked list，方便在 Main 中建立測試資料。
    /// </summary>
    /// <param name="digits">由低位到高位排列的數字陣列。</param>
    /// <returns>建立完成的 linked list。</returns>
    private static ListNode CreateList(int[] digits)
    {
        ArgumentNullException.ThrowIfNull(digits);

        if (digits.Length == 0)
        {
            throw new ArgumentException("The digits collection must contain at least one value.", nameof(digits));
        }

        ListNode head = new ListNode(digits[0]);
        ListNode current = head;

        for (int index = 1; index < digits.Length; index++)
        {
            ListNode nextNode = new ListNode(digits[index]);
            current.next = nextNode;
            current = nextNode;
        }

        return head;
    }

    /// <summary>
    /// 將 linked list 轉成易讀字串，方便觀察測試結果。
    /// </summary>
    /// <param name="head">linked list 起點。</param>
    /// <returns>格式化後的字串。</returns>
    private static string FormatList(ListNode? head)
    {
        if (head is null)
        {
            return "[]";
        }

        StringBuilder builder = new StringBuilder("[");

        for (ListNode? current = head; current is not null; current = current.next)
        {
            if (builder.Length > 1)
            {
                builder.Append(" -> ");
            }

            builder.Append(current.val);
        }

        builder.Append(']');
        return builder.ToString();
    }

    /// <summary>
    /// 解法一：重複利用同一個 sum 變數，同時保存上一輪進位與本輪位數加總。
    /// 因為 linked list 以反向方式儲存數字，從 head 開始同步走訪，就等同於從個位數開始做直式加法；
    /// 每一輪先把前一輪是否進位轉成 0 或 1，再加上目前兩個節點值，最後把個位數接到答案尾端。
    /// 這種寫法把進位資訊隱含在 sum 本身，程式碼較精簡，但理解時要注意 sum 在不同時機代表的意義不同。
    /// </summary>
    /// <param name="l1">第一個反向儲存數字的 linked list。</param>
    /// <param name="l2">第二個反向儲存數字的 linked list。</param>
    /// <returns>兩數相加後的反向 linked list。</returns>
    /// <example>
    /// 輸入 [2 -> 4 -> 3] 與 [5 -> 6 -> 4]，輸出 [7 -> 0 -> 8]。
    /// </example>
    /// <remarks>
    /// 解題重點在於逐位模擬直式加法、用 dummy head 簡化答案 linked list 的建立，
    /// 並在兩條 linked list 長度不同時，把缺少的位數視為 0。時間複雜度為 O(n)，額外空間複雜度為 O(n)，
    /// 其中 n 為較長 linked list 的節點數。
    /// </remarks>
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        // dummy head 讓串接節點與回傳結果都更單純。
        ListNode dummyHead = new ListNode(0);
        ListNode tail = dummyHead;
        ListNode? leftCurrent = l1;
        ListNode? rightCurrent = l2;
        int sum = 0;

        // 每一輪都處理同一個位數，直到兩條 linked list 都走完。
        while (leftCurrent is not null || rightCurrent is not null)
        {
            // 先把上一輪是否需要進位轉成 0 或 1。
            sum = sum >= 10 ? 1 : 0;

            // 若 l1 還有節點，就把目前位數加進總和並前進到下一位。
            if (leftCurrent is not null)
            {
                sum += leftCurrent.val;
                leftCurrent = leftCurrent.next;
            }

            // 若 l2 還有節點，也把目前位數加進總和並前進到下一位。
            if (rightCurrent is not null)
            {
                sum += rightCurrent.val;
                rightCurrent = rightCurrent.next;
            }

            // 本輪只留下個位數，十位數由下一輪繼續處理。
            ListNode nextNode = new ListNode(sum % 10);
            // ① 把新節點接在鏈的尾端; 把新節點「串上去」
            tail.next = nextNode;
            // ② tail 指標移動到新尾端; 更新 tail 為最新尾端
            tail = nextNode;
        }

        // 迴圈結束後若 sum 仍大於等於 10，代表最高位還有一個進位需要補上。
        if (sum >= 10)
        {
            tail.next = new ListNode(1);
        }

        // 跳過 dummy head [0]，從真正的第一個節點回傳
        return dummyHead.next!;
    }

    /// <summary>
    /// 解法二：把進位拆成獨立的 carry 變數，讓每一輪都遵循固定步驟。
    /// 因為 linked list 由低位到高位排列，只要同步向後走訪 l1 與 l2，
    /// 就能直接模擬手算直式加法的流程：取值、相加、拆出個位數、保存進位，再前往下一位。
    /// 這種寫法雖然多了一個變數，但 sum 與 carry 各司其職，可讀性與維護性都比較高。
    /// </summary>
    /// <param name="l1">第一個反向儲存數字的 linked list。</param>
    /// <param name="l2">第二個反向儲存數字的 linked list。</param>
    /// <returns>兩數相加後的反向 linked list。</returns>
    /// <example>
    /// 輸入 [2 -&gt; 4 -&gt; 3] 與 [5 -&gt; 6 -&gt; 4]，輸出 [7 -&gt; 0 -&gt; 8]。
    /// </example>
    /// <remarks>
    /// 此解法把本輪總和與下一輪進位分開管理，因此更容易逐步追蹤。時間複雜度為 O(n)，
    /// 額外空間複雜度為 O(n)，其中 n 為較長 linked list 的節點數。
    /// </remarks>
    public ListNode AddTwoNumbers2(ListNode l1, ListNode l2)
    {
        // dummy head 避免第一個答案節點需要特別處理。
        ListNode dummyHead = new ListNode(0);
        ListNode tail = dummyHead;
        ListNode? leftCurrent = l1;
        ListNode? rightCurrent = l2;
        int carry = 0;

        // 每一輪都把目前位數與進位整理成一個新答案節點。
        while (leftCurrent is not null || rightCurrent is not null)
        {
            // 任一 linked list 已走完時，該位數直接視為 0。
            int leftDigit = leftCurrent is null ? 0 : leftCurrent.val;
            int rightDigit = rightCurrent is null ? 0 : rightCurrent.val;

            // 先算出本輪總和，再拆出下一輪的進位。
            int sum = leftDigit + rightDigit + carry;
            carry = sum >= 10 ? 1 : 0;

            // 把目前個位數接到答案尾端。
            ListNode nextNode = new ListNode(sum % 10);
            tail.next = nextNode;
            tail = nextNode;

            // 當前位數已處理完成，兩條 linked list 都往下一位前進。
            if (leftCurrent is not null)
            {
                leftCurrent = leftCurrent.next;
            }

            if (rightCurrent is not null)
            {
                rightCurrent = rightCurrent.next;
            }
        }

        // 所有節點處理完後，若仍有進位就補上一個新的最高位節點。
        if (carry == 1)
        {
            tail.next = new ListNode(1);
        }

        return dummyHead.next!;
    }
}
