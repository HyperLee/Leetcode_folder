namespace leetcode_143
{
    internal class Program
    {
        /// <summary>
        /// 表示單向鏈結串列的節點，保存整數值以及下一個節點的參考。
        /// </summary>
        public class ListNode
        {
            public int val;
            public ListNode? next;

            /// <summary>
            /// 建立單向鏈結串列節點。
            /// </summary>
            /// <param name="val">節點保存的整數值。</param>
            /// <param name="next">下一個節點；若為串列尾端則為 <see langword="null"/>。</param>
            public ListNode(int val = 0, ListNode? next = null)
            {
                this.val = val;
                this.next = next;
            }
        }

        /// <summary>
        /// 143. Reorder List
        /// https://leetcode.com/problems/reorder-list/description/?envType=daily-question&envId=2024-03-23
        /// 143. 重排链表
        /// https://leetcode.cn/problems/reorder-list/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 執行七組固定案例，為兩種原地重排解法分別建立獨立串列，
        /// 並比對重排後的節點值順序與預期結果，最後輸出通過項目總數。
        /// </summary>
        private static void RunSamples()
        {
            (int[] Input, int[] Expected)[] testCases =
            [
                ([], []),
                ([1], [1]),
                ([1, 2], [1, 2]),
                ([1, 2, 3], [1, 3, 2]),
                ([1, 2, 3, 4], [1, 4, 2, 3]),
                ([1, 2, 3, 4, 5], [1, 5, 2, 4, 3]),
                ([1, 2, 2, 1], [1, 1, 2, 2])
            ];

            int passedCount = 0;

            for (int index = 0; index < testCases.Length; index++)
            {
                (int[] input, int[] expected) = testCases[index];
                ListNode? head1 = BuildList(input);
                ListNode? head2 = BuildList(input);

                ReorderList(head1);
                ReorderList2(head2);

                int[] actual1 = ToArray(head1);
                int[] actual2 = ToArray(head2);
                bool isPassed1 = actual1.SequenceEqual(expected);
                bool isPassed2 = actual2.SequenceEqual(expected);

                if (isPassed1)
                {
                    passedCount++;
                }

                if (isPassed2)
                {
                    passedCount++;
                }

                Console.WriteLine($"案例 {index + 1}");
                Console.WriteLine($"輸入：{FormatList(input)}");
                Console.WriteLine($"預期：{FormatList(expected)}");
                Console.WriteLine($"解法一實際：{FormatList(actual1)} => {(isPassed1 ? "PASS" : "FAIL")}");
                Console.WriteLine($"解法二實際：{FormatList(actual2)} => {(isPassed2 ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passedCount}/{testCases.Length * 2} 項檢查通過");
        }

        /// <summary>
        /// 依照輸入陣列的順序建立新的單向鏈結串列。
        /// 輸入可為空陣列；空陣列回傳 <see langword="null"/>，否則回傳第一個節點。
        /// </summary>
        /// <param name="values">要依序寫入節點的整數陣列。</param>
        /// <returns>新串列的頭節點；輸入為空陣列時回傳 <see langword="null"/>。</returns>
        private static ListNode? BuildList(int[] values)
        {
            if (values.Length == 0)
            {
                return null;
            }

            ListNode head = new ListNode(values[0]);
            ListNode current = head;

            for (int index = 1; index < values.Length; index++)
            {
                current.next = new ListNode(values[index]);
                current = current.next;
            }

            return head;
        }

        /// <summary>
        /// 從頭節點開始依序讀取非循環單向鏈結串列，並轉成整數陣列。
        /// 輸入為 <see langword="null"/> 時回傳空陣列。
        /// </summary>
        /// <param name="head">要讀取的串列頭節點。</param>
        /// <returns>依照串列節點順序組成的整數陣列。</returns>
        private static int[] ToArray(ListNode? head)
        {
            List<int> values = [];
            ListNode? current = head;

            while (current != null)
            {
                values.Add(current.val);
                current = current.next;
            }

            return [.. values];
        }

        /// <summary>
        /// 將整數序列格式化為不含空格的方括號表示法，方便主控台案例對照。
        /// </summary>
        /// <param name="values">要格式化的整數序列。</param>
        /// <returns>例如 <c>[1,4,2,3]</c> 的文字；空序列回傳 <c>[]</c>。</returns>
        private static string FormatList(IEnumerable<int> values)
        {
            return $"[{string.Join(",", values)}]";
        }

        /// <summary>
        /// 使用線性集合保存非循環單向鏈結串列的所有節點，再以左右指標交替取出
        /// 頭尾節點並重新連接。輸入可為空串列；方法不回傳新串列，也不修改節點值，
        /// 而是直接重排原節點。時間複雜度為 O(n)，額外空間複雜度為 O(n)。
        ///
        /// https://leetcode.cn/problems/reorder-list/solutions/452867/zhong-pai-lian-biao-by-leetcode-solution/
        /// https://leetcode.cn/problems/reorder-list/solutions/1394353/by-stormsunshine-4k3r/
        /// </summary>
        /// <param name="head">要原地重排的串列頭節點；可為 <see langword="null"/>。</param>
        public static void ReorderList(ListNode? head)
        {
            if (head == null)
            {
                return;
            }

            List<ListNode> nodes = [];
            ListNode? node = head;

            // 單向串列無法直接從尾端存取，先保存節點參考以取得 O(1) 隨機存取能力。
            while (node != null)
            {
                nodes.Add(node);
                node = node.next;
            }

            int left = 0;
            int right = nodes.Count - 1;

            while (left < right)
            {
                // 每輪依序接上目前最左與最右節點，使兩端節點向中央交錯排列。
                nodes[left].next = nodes[right];
                left++;

                if (left < right)
                {
                    nodes[right].next = nodes[left];
                    right--;
                }
            }

            // 明確截斷最後一個節點，避免保留原串列指向而形成循環。
            nodes[left].next = null;
        }

        /// <summary>
        /// 使用快慢指標找出非循環單向鏈結串列的中點，反轉後半段，再將前後兩段
        /// 交錯合併。輸入可為空串列；方法不回傳新串列，也不修改節點值，而是直接
        /// 重排原節點。時間複雜度為 O(n)，額外空間複雜度為 O(1)。
        /// </summary>
        /// <param name="head">要原地重排的串列頭節點；可為 <see langword="null"/>。</param>
        public static void ReorderList2(ListNode? head)
        {
            if (head?.next == null)
            {
                return;
            }

            ListNode slow = head;
            ListNode fast = head;

            // 快指標一次走兩步，慢指標停在前半段的最後一個節點。
            while (fast.next != null && fast.next.next != null)
            {
                slow = slow.next!;
                fast = fast.next.next;
            }

            ListNode? second = slow.next;
            slow.next = null;
            ListNode? previous = null;

            // 原地反轉後半段，讓原本的尾端節點成為第二段的起點。
            while (second != null)
            {
                ListNode? next = second.next;
                second.next = previous;
                previous = second;
                second = next;
            }

            ListNode first = head;
            second = previous;

            // 交替接上前半段與已反轉的後半段，直到後半段全部合併。
            while (second != null)
            {
                ListNode? nextFirst = first.next;
                ListNode? nextSecond = second.next;

                first.next = second;
                second.next = nextFirst;

                first = nextFirst!;
                second = nextSecond;
            }
        }
    }
}