using System.Collections.Generic;

namespace leetcode_2816
{
    internal class Program
    {
        /// <summary>
        /// 表示十進位數字中的一個位數，以及指向下一個低位數節點的鏈結。
        /// </summary>
        public class ListNode
        {
            public int val;
            public ListNode? next;

            /// <summary>
            /// 建立一個鏈結串列節點，保存目前位數與下一個節點。
            /// </summary>
            /// <param name="val">目前節點代表的十進位位數，合法輸入範圍為 0 到 9。</param>
            /// <param name="next">下一個較低位數節點；若目前節點是尾端則為 <see langword="null"/>。</param>
            public ListNode(int val = 0, ListNode? next = null)
            {
                this.val = val;
                this.next = next;
            }
        }

        /// <summary>
        /// 2816. Double a Number Represented as a Linked List
        /// https://leetcode.com/problems/double-a-number-represented-as-a-linked-list/description/
        /// <para>
        /// You are given the head of a non-empty linked list representing a non-negative integer without leading zeroes.
        ///
        /// Return the head of the linked list after doubling it.
        ///
        /// Example 1:
        /// Image: https://assets.leetcode.com/uploads/2023/05/28/example.png
        /// Input: head = [1,8,9]
        /// Output: [3,7,8]
        /// Explanation: The figure above corresponds to the given linked list, which represents the number 189. Hence, the returned linked list represents 189 * 2 = 378.
        ///
        /// Example 2:
        /// Image: https://assets.leetcode.com/uploads/2023/05/28/example2.png
        /// Input: head = [9,9,9]
        /// Output: [1,9,9,8]
        /// Explanation: The figure above corresponds to the given linked list, which represents the number 999. Hence, the returned linked list represents 999 * 2 = 1998.
        ///
        /// Constraints:
        /// - The number of nodes in the list is in the range [1, 10^4].
        /// - 0 &lt;= Node.val &lt;= 9
        /// - The input is generated such that the list represents a number without leading zeros, except the number 0 itself.
        /// </para>
        /// <para>
        /// 2816. 將鏈結串列表示的數字加倍
        /// https://leetcode.cn/problems/double-a-number-represented-as-a-linked-list/description/
        ///
        /// 給定一個非空鏈結串列的 head；此串列表示一個沒有前導零的非負整數。
        ///
        /// 回傳將該數字加倍後的鏈結串列 head。
        ///
        /// 範例 1：
        /// 圖片：https://assets.leetcode.com/uploads/2023/05/28/example.png
        /// 輸入：head = [1,8,9]
        /// 輸出：[3,7,8]
        /// 解釋：上圖對應給定的鏈結串列，它表示數字 189。因此，回傳的鏈結串列表示 189 * 2 = 378。
        ///
        /// 範例 2：
        /// 圖片：https://assets.leetcode.com/uploads/2023/05/28/example2.png
        /// 輸入：head = [9,9,9]
        /// 輸出：[1,9,9,8]
        /// 解釋：上圖對應給定的鏈結串列，它表示數字 999。因此，回傳的鏈結串列表示 999 * 2 = 1998。
        ///
        /// 限制條件：
        /// - 串列的節點數量介於 [1, 10^4]。
        /// - 0 &lt;= Node.val &lt;= 9
        /// - 輸入保證串列表示的數字沒有前導零，但數字 0 本身除外。
        /// </para>
        /// </summary>
        /// <remarks>
        /// 以六組固定案例依序驗證三種解法。每次呼叫都重建鏈結串列，避免原地修改影響其他解法；
        /// 任一結果不符預期時，程式會設定非零結束碼。
        /// </remarks>
        /// <param name="args">命令列參數；本範例不使用。</param>
        static void Main(string[] args)
        {
            (string Name, int[] Input, int[] Expected)[] cases =
            [
                ("零值", [0], [0]),
                ("無進位", [1, 2, 3], [2, 4, 6]),
                ("官方一般案例", [1, 8, 9], [3, 7, 8]),
                ("單節點最高位進位", [5], [1, 0]),
                ("連續進位與重複值", [9, 9, 9], [1, 9, 9, 8]),
                ("保留既有案例", [5, 1, 1], [1, 0, 2, 2])
            ];

            (string Name, Func<ListNode, ListNode> Execute)[] solutions =
            [
                ("DoubleIt - 向右預看", DoubleIt),
                ("DoubleIt2 - 堆疊回推", DoubleIt2),
                ("DoubleIt3 - 反轉鏈結串列", DoubleIt3)
            ];

            int passed = 0;
            int total = cases.Length * solutions.Length;

            foreach ((string caseName, int[] input, int[] expected) in cases)
            {
                Console.WriteLine($"Case: {caseName}");
                Console.WriteLine($"Input: {FormatValues(input)}");

                foreach ((string solutionName, Func<ListNode, ListNode> execute) in solutions)
                {
                    ListNode head = BuildList(input);
                    int[] actual = ToValues(execute(head));
                    bool isPassed = HaveSameValues(expected, actual);

                    Console.WriteLine($"  Solution: {solutionName}");
                    Console.WriteLine($"  Expected: {FormatValues(expected)}");
                    Console.WriteLine($"  Actual: {FormatValues(actual)}");
                    Console.WriteLine($"  Result: {(isPassed ? "PASS" : "FAIL")}");

                    if (isPassed)
                    {
                        passed++;
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine($"Summary: {passed}/{total} checks passed.");

            if (passed != total)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 使用向右預看進位法，將非空鏈結串列表示的非負整數原地乘以二。
        /// 從高位往低位走訪，若下一位大於等於 5，便先把下一位將產生的進位加到目前位。
        /// </summary>
        /// <param name="head">非空、每個節點值介於 0 到 9，且除數字 0 外沒有前導零的鏈結串列。</param>
        /// <returns>翻倍後的鏈結串列頭節點；輸入節點值會被修改，最高位進位時會建立新的頭節點。</returns>
        /// <remarks>時間複雜度為 O(n)，額外空間複雜度為 O(1)。</remarks>
        public static ListNode DoubleIt(ListNode head)
        {
            // 最高位大於等於 5 時，先補 0 節點承接最後會產生的最高位進位。
            if (head.val >= 5)
            {
                head = new ListNode(0, head);
            }

            for (ListNode? current = head; current is not null; current = current.next)
            {
                current.val = current.val * 2 % 10;

                // 下一位原值大於等於 5，代表它乘以二後會向目前位進一。
                if (current.next is not null && current.next.val >= 5)
                {
                    current.val++;
                }
            }

            return head;
        }

        /// <summary>
        /// 使用堆疊由低位往高位回推，將非空鏈結串列表示的非負整數原地乘以二。
        /// 先保存所有節點，再按照正規直式乘法順序處理目前位數與進位。
        /// </summary>
        /// <param name="head">非空、每個節點值介於 0 到 9，且除數字 0 外沒有前導零的鏈結串列。</param>
        /// <returns>翻倍後的鏈結串列頭節點；輸入節點值會被修改，最高位進位時會建立新的頭節點。</returns>
        /// <remarks>時間複雜度為 O(n)，堆疊使用的額外空間複雜度為 O(n)。</remarks>
        public static ListNode DoubleIt2(ListNode head)
        {
            Stack<ListNode> nodes = [];

            for (ListNode? current = head; current is not null; current = current.next)
            {
                nodes.Push(current);
            }

            int carry = 0;

            // 從最低位開始計算，使每次產生的進位能交給下一個被彈出的高位節點。
            while (nodes.Count > 0)
            {
                ListNode current = nodes.Pop();
                int doubled = current.val * 2 + carry;
                current.val = doubled % 10;
                carry = doubled / 10;
            }

            if (carry > 0)
            {
                head = new ListNode(carry, head);
            }

            return head;
        }

        /// <summary>
        /// 使用兩次反轉鏈結串列，將非空鏈結串列表示的非負整數原地乘以二。
        /// 第一次反轉後可由最低位往最高位處理進位，完成計算後再反轉回題目要求的順序。
        /// </summary>
        /// <param name="head">非空、每個節點值介於 0 到 9，且除數字 0 外沒有前導零的鏈結串列。</param>
        /// <returns>翻倍後的鏈結串列頭節點；輸入的節點值與鏈結會在處理過程中被修改。</returns>
        /// <remarks>時間複雜度為 O(n)，額外空間複雜度為 O(1)。</remarks>
        public static ListNode DoubleIt3(ListNode head)
        {
            ListNode reversedHead = ReverseList(head);
            ListNode? current = reversedHead;
            ListNode? previous = null;
            int carry = 0;

            while (current is not null)
            {
                int doubled = current.val * 2 + carry;
                current.val = doubled % 10;
                carry = doubled / 10;
                previous = current;
                current = current.next;
            }

            // 反轉後的尾端代表原數字的最高位，仍有進位時直接接上新節點。
            if (carry > 0)
            {
                previous!.next = new ListNode(carry);
            }

            return ReverseList(reversedHead);
        }

        /// <summary>
        /// 以迭代方式原地反轉非空單向鏈結串列，供反轉解法切換計算方向。
        /// </summary>
        /// <param name="head">要反轉的非空鏈結串列頭節點。</param>
        /// <returns>反轉完成後的新頭節點。</returns>
        private static ListNode ReverseList(ListNode head)
        {
            ListNode? previous = null;
            ListNode? current = head;

            while (current is not null)
            {
                // 先保留尚未處理的節點，再改寫 next，避免失去剩餘鏈結。
                ListNode? next = current.next;
                current.next = previous;
                previous = current;
                current = next;
            }

            return previous!;
        }

        /// <summary>
        /// 依照給定的十進位位數建立非空鏈結串列，第一個元素成為最高位。
        /// </summary>
        /// <param name="values">至少包含一個元素，且每個元素介於 0 到 9 的位數集合。</param>
        /// <returns>包含相同位數順序的新鏈結串列頭節點。</returns>
        private static ListNode BuildList(IReadOnlyList<int> values)
        {
            ListNode head = new ListNode(values[0]);
            ListNode current = head;

            for (int index = 1; index < values.Count; index++)
            {
                current.next = new ListNode(values[index]);
                current = current.next;
            }

            return head;
        }

        /// <summary>
        /// 由頭到尾讀取鏈結串列中的位數，轉成便於驗證的整數陣列。
        /// </summary>
        /// <param name="head">要讀取的鏈結串列頭節點；可為 <see langword="null"/>。</param>
        /// <returns>依鏈結順序排列的節點值陣列；空鏈結串列會回傳空陣列。</returns>
        private static int[] ToValues(ListNode? head)
        {
            List<int> values = [];

            for (ListNode? current = head; current is not null; current = current.next)
            {
                values.Add(current.val);
            }

            return [.. values];
        }

        /// <summary>
        /// 比較兩組位數的長度與每個位置，判斷鏈結串列結果是否符合預期。
        /// </summary>
        /// <param name="expected">預期的位數順序。</param>
        /// <param name="actual">實際的位數順序。</param>
        /// <returns>長度及所有位數都相同時回傳 <see langword="true"/>；否則回傳 <see langword="false"/>。</returns>
        private static bool HaveSameValues(IReadOnlyList<int> expected, IReadOnlyList<int> actual)
        {
            if (expected.Count != actual.Count)
            {
                return false;
            }

            for (int index = 0; index < expected.Count; index++)
            {
                if (expected[index] != actual[index])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 將位數集合格式化為 README 與主控台共用的方括號表示法。
        /// </summary>
        /// <param name="values">要格式化的位數集合。</param>
        /// <returns>例如 <c>[1,9,9,8]</c> 的文字。</returns>
        private static string FormatValues(IEnumerable<int> values)
        {
            return $"[{string.Join(",", values)}]";
        }
    }
}