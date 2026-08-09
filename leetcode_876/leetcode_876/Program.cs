namespace leetcode_876
{
    internal class Program
    {
        /// <summary>
        /// 表示單向鏈結串列的一個節點，保存目前節點值以及下一個節點的參考。
        /// 節點值與鏈結結構遵循題目輸入；串列尾端的 <see cref="next"/> 為
        /// <see langword="null"/>。
        /// </summary>
        public class ListNode
        {
            public int val;
            public ListNode? next;

            /// <summary>
            /// 建立一個單向鏈結串列節點，並可選擇連接已存在的下一個節點。
            /// </summary>
            /// <param name="val">目前節點保存的整數值。</param>
            /// <param name="next">下一個節點；若目前節點為尾端則為 <see langword="null"/>。</param>
            public ListNode(int val = 0, ListNode? next = null)
            {
                this.val = val;
                this.next = next;
            }
        }


        /// <summary>
        /// 876. Middle of the Linked List
        /// https://leetcode.com/problems/middle-of-the-linked-list/description/
        /// <para>
        /// Given the head of a singly linked list, return the middle node of the linked list.
        ///
        /// If there are two middle nodes, return the second middle node.
        ///
        /// Example 1:
        /// Image: https://assets.leetcode.com/uploads/2021/07/23/lc-midlist1.jpg
        /// Input: head = [1,2,3,4,5]
        /// Output: [3,4,5]
        /// Explanation: The middle node of the list is node 3.
        ///
        /// Example 2:
        /// Image: https://assets.leetcode.com/uploads/2021/07/23/lc-midlist2.jpg
        /// Input: head = [1,2,3,4,5,6]
        /// Output: [4,5,6]
        /// Explanation: The list has two middle nodes with values 3 and 4, so return the second one.
        ///
        /// Constraints:
        /// - The number of nodes in the list is in [1, 100].
        /// - 1 &lt;= Node.val &lt;= 100
        /// </para>
        /// <para>
        /// 876. 鏈結串列的中間節點
        /// https://leetcode.cn/problems/middle-of-the-linked-list/description/
        ///
        /// 給定單向鏈結串列的頭節點 head，回傳該鏈結串列的中間節點。
        ///
        /// 若有兩個中間節點，回傳第二個中間節點。
        ///
        /// 範例 1：
        /// 圖片：https://assets.leetcode.com/uploads/2021/07/23/lc-midlist1.jpg
        /// 輸入：head = [1,2,3,4,5]
        /// 輸出：[3,4,5]
        /// 解釋：串列的中間節點是節點 3。
        ///
        /// 範例 2：
        /// 圖片：https://assets.leetcode.com/uploads/2021/07/23/lc-midlist2.jpg
        /// 輸入：head = [1,2,3,4,5,6]
        /// 輸出：[4,5,6]
        /// 解釋：串列有兩個中間節點，值分別為 3 與 4，因此回傳第二個。
        ///
        /// 限制條件：
        /// - 串列中的節點數量在 [1, 100] 範圍內。
        /// - 1 &lt;= Node.val &lt;= 100
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            SampleCase[] samples =
            [
                new("單一節點", [1], [1]),
                new("兩個節點", [1, 2], [2]),
                new("奇數長度", [1, 2, 3, 4, 5], [3, 4, 5]),
                new("偶數長度", [1, 2, 3, 4, 5, 6], [4, 5, 6]),
                new("既有四節點案例", [1, 2, 3, 4], [3, 4]),
                new("重複節點值", [7, 7, 7, 7], [7, 7])
            ];

            int passedChecks = 0;

            for (int index = 0; index < samples.Length; index++)
            {
                SampleResult result = RunCase(samples[index]);
                passedChecks += result.FastSlowPassed ? 1 : 0;
                passedChecks += result.TwoPassPassed ? 1 : 0;

                Console.WriteLine($"案例 {index + 1}：{result.Name}");
                Console.WriteLine($"輸入：[{FormatValues(result.Input)}]");
                Console.WriteLine($"預期：[{FormatValues(result.Expected)}]");
                Console.WriteLine(
                    $"快慢指標：[{FormatValues(result.FastSlowActual)}] => " +
                    $"{(result.FastSlowPassed ? "PASS" : "FAIL")}");
                Console.WriteLine(
                    $"兩次走訪：[{FormatValues(result.TwoPassActual)}] => " +
                    $"{(result.TwoPassPassed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            int totalChecks = samples.Length * 2;
            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項解法檢查通過");

            if (passedChecks != totalChecks)
            {
                Environment.ExitCode = 1;
            }
        }


        /// <summary>
        /// 使用快慢指標在一次走訪內找出單向鏈結串列的中間節點。
        /// 慢指標每次前進一個節點，快指標每次前進兩個節點；快指標抵達尾端時，
        /// 慢指標恰好位於中點。輸入必須是符合題目限制、至少含一個節點的無環串列。
        /// 若節點數為偶數，回傳兩個中間節點中的第二個。
        /// </summary>
        /// <param name="head">非空單向鏈結串列的頭節點。</param>
        /// <returns>中間節點；從此節點沿著鏈結即可取得題目要求的完整尾段。</returns>
        public static ListNode MiddleNode(ListNode head)
        {
            ListNode slow = head;
            ListNode? fast = head;

            while (fast is not null && fast.next is not null)
            {
                // 快指標走兩步、慢指標走一步；偶數長度時慢指標會落在第二個中點。
                slow = slow.next!;
                fast = fast.next.next;
            }

            return slow;
        }


        /// <summary>
        /// 使用兩次走訪找出單向鏈結串列的中間節點。
        /// 第一次走訪計算總節點數，第二次從頭前進「節點數除以二」步；
        /// 因索引從零開始，偶數長度時會自然選到第二個中點。
        /// 輸入必須是符合題目限制、至少含一個節點的無環串列。
        /// </summary>
        /// <param name="head">非空單向鏈結串列的頭節點。</param>
        /// <returns>中間節點；從此節點沿著鏈結即可取得題目要求的完整尾段。</returns>
        public static ListNode MiddleNode2(ListNode head)
        {
            int length = 0;
            ListNode? current = head;

            // 第一次走訪只計算長度，不變更任何節點或鏈結。
            while (current is not null)
            {
                length++;
                current = current.next;
            }

            ListNode middle = head;

            // 從零起算前進 length / 2 步，即為奇數中點或偶數的第二個中點。
            for (int step = 0; step < length / 2; step++)
            {
                middle = middle.next!;
            }

            return middle;
        }

        /// <summary>
        /// 對同一組有效節點資料建立兩條獨立鏈結串列，分別執行快慢指標與兩次走訪解法，
        /// 再將兩個回傳尾段轉為陣列，供呼叫端與預期結果進行完整內容比對。
        /// </summary>
        /// <param name="sample">包含案例名稱、非空輸入與預期尾段的固定測試案例。</param>
        /// <returns>包含兩種解法實際尾段與各自通過狀態的案例結果。</returns>
        private static SampleResult RunCase(SampleCase sample)
        {
            ListNode fastSlowInput = BuildList(sample.Input);
            ListNode twoPassInput = BuildList(sample.Input);
            int[] fastSlowActual = ToArray(MiddleNode(fastSlowInput));
            int[] twoPassActual = ToArray(MiddleNode2(twoPassInput));

            return new SampleResult(
                sample.Name,
                sample.Input,
                sample.Expected,
                fastSlowActual,
                twoPassActual);
        }

        /// <summary>
        /// 依照給定的非空整數陣列順序建立單向鏈結串列。
        /// 每次呼叫都建立全新的節點，讓不同解法不會共享輸入狀態。
        /// </summary>
        /// <param name="values">至少包含一個元素、且節點值符合題目限制的陣列。</param>
        /// <returns>新建立之單向鏈結串列的頭節點。</returns>
        private static ListNode BuildList(int[] values)
        {
            ListNode head = new(values[0]);
            ListNode tail = head;

            for (int index = 1; index < values.Length; index++)
            {
                ListNode nextNode = new(values[index]);
                tail.next = nextNode;
                tail = nextNode;
            }

            return head;
        }

        /// <summary>
        /// 從指定節點走訪至串列尾端，依序收集每個節點值。
        /// 輸入可以是任意節點或 <see langword="null"/>，且不會修改原串列。
        /// </summary>
        /// <param name="node">要開始轉換的節點；為空時產生空陣列。</param>
        /// <returns>依鏈結順序排列的節點值陣列。</returns>
        private static int[] ToArray(ListNode? node)
        {
            List<int> values = [];
            ListNode? current = node;

            while (current is not null)
            {
                values.Add(current.val);
                current = current.next;
            }

            return [.. values];
        }

        /// <summary>
        /// 將整數序列格式化為主控台與 README 共用的逗號分隔內容。
        /// 輸入為案例或執行結果陣列，輸出不包含外層方括號。
        /// </summary>
        /// <param name="values">要格式化的整數序列。</param>
        /// <returns>以逗號與空格分隔的節點值文字。</returns>
        private static string FormatValues(IEnumerable<int> values)
        {
            return string.Join(", ", values);
        }

        private sealed record SampleCase(string Name, int[] Input, int[] Expected);

        private sealed record SampleResult(
            string Name,
            int[] Input,
            int[] Expected,
            int[] FastSlowActual,
            int[] TwoPassActual)
        {
            public bool FastSlowPassed => Expected.SequenceEqual(FastSlowActual);

            public bool TwoPassPassed => Expected.SequenceEqual(TwoPassActual);
        }
    }
}