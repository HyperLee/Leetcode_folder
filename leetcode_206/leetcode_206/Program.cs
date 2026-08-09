namespace leetcode_206
{
    internal class Program
    {
        public class ListNode
        {
            public int val;
            public ListNode? next;

            /// <summary>
            /// 建立單向鏈結串列節點，保存目前節點值及下一個節點的參考。
            /// </summary>
            /// <param name="val">目前節點的整數值，需符合題目限制 -5000 到 5000。</param>
            /// <param name="next">下一個節點；若目前節點是尾端，則為 <see langword="null"/>。</param>
            public ListNode(int val = 0, ListNode? next = null)
            {
                this.val = val;
                this.next = next;
            }
        }

        /// <summary>
        /// <para>
        /// 206. Reverse Linked List
        /// https://leetcode.com/problems/reverse-linked-list/description/
        ///
        /// Given the head of a singly linked list, reverse the list and return the reversed list.
        ///
        /// Images: https://assets.leetcode.com/uploads/2021/02/19/rev1ex1.jpg and https://assets.leetcode.com/uploads/2021/02/19/rev1ex2.jpg
        ///
        /// Example 1:
        /// Input: head = [1,2,3,4,5]
        /// Output: [5,4,3,2,1]
        ///
        /// Example 2:
        /// Input: head = [1,2]
        /// Output: [2,1]
        ///
        /// Example 3:
        /// Input: head = []
        /// Output: []
        ///
        /// Constraints:
        /// - The number of nodes is in [0,5000].
        /// - -5000 &lt;= Node.val &lt;= 5000
        ///
        /// Follow-up: A linked list can be reversed iteratively or recursively. Can you implement both?
        /// </para>
        /// <para>
        /// 206. 反轉鏈結串列
        /// https://leetcode.cn/problems/reverse-linked-list/description/
        ///
        /// 給定單向鏈結串列的頭節點，反轉串列並回傳反轉後的串列。
        ///
        /// 圖片：https://assets.leetcode.com/uploads/2021/02/19/rev1ex1.jpg 與 https://assets.leetcode.com/uploads/2021/02/19/rev1ex2.jpg
        ///
        /// 範例 1：
        /// 輸入：head = [1,2,3,4,5]
        /// 輸出：[5,4,3,2,1]
        ///
        /// 範例 2：
        /// 輸入：head = [1,2]
        /// 輸出：[2,1]
        ///
        /// 範例 3：
        /// 輸入：head = []
        /// 輸出：[]
        ///
        /// 限制條件：
        /// - 節點數量在 [0,5000] 範圍內。
        /// - -5000 &lt;= Node.val &lt;= 5000
        ///
        /// 進階：鏈結串列可以使用迭代或遞迴反轉，你能同時實作兩種方式嗎？
        /// </para>
        /// </summary>
        /// <remarks>
        /// 主程式會以五組固定案例分別驗證兩種迭代解法，並列出預期值、實際值與 PASS/FAIL。
        /// </remarks>
        /// <param name="args">命令列參數；本範例程式不使用。</param>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 執行五組固定案例，分別驗證 <see cref="ReverseList"/> 與 <see cref="ReverseList2"/> 的反轉結果。
        /// </summary>
        /// <remarks>
        /// 每個解法都會從原始陣列重新建立鏈結串列，避免第一個原地反轉結果影響第二個解法。
        /// 輸出包含案例輸入、預期結果、兩種實際結果及最終通過項數。
        /// </remarks>
        private static void RunSamples()
        {
            SampleCase[] samples =
            [
                new SampleCase("一般多節點", [1, 2, 3, 4, 5], [5, 4, 3, 2, 1]),
                new SampleCase("兩個節點", [1, 2], [2, 1]),
                new SampleCase("單一節點", [1], [1]),
                new SampleCase("空鏈結串列", [], []),
                new SampleCase("包含重複值與負值", [-1, 2, 2, 0], [0, 2, 2, -1])
            ];

            int passedCount = 0;
            int totalChecks = samples.Length * 2;

            Console.WriteLine("LeetCode 206 - Reverse Linked List");
            Console.WriteLine();

            for (int i = 0; i < samples.Length; i++)
            {
                SampleCase sample = samples[i];
                int[] firstActual = ToArray(ReverseList(BuildList(sample.Input)));
                int[] secondActual = ToArray(ReverseList2(BuildList(sample.Input)));
                bool firstPassed = firstActual.SequenceEqual(sample.Expected);
                bool secondPassed = secondActual.SequenceEqual(sample.Expected);

                if (firstPassed)
                {
                    passedCount++;
                }

                if (secondPassed)
                {
                    passedCount++;
                }

                Console.WriteLine($"案例 {i + 1}：{sample.Description}");
                Console.WriteLine($"輸入：{FormatList(sample.Input)}");
                Console.WriteLine($"預期：{FormatList(sample.Expected)}");
                Console.WriteLine($"解法一實際：{FormatList(firstActual)} => {(firstPassed ? "PASS" : "FAIL")}");
                Console.WriteLine($"解法二實際：{FormatList(secondActual)} => {(secondPassed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passedCount}/{totalChecks} 項驗證通過");
        }

        /// <summary>
        /// 依照整數陣列的順序建立單向鏈結串列，供每次演算法驗證使用。
        /// </summary>
        /// <param name="values">要依序放入節點的整數；空陣列表示空鏈結串列。</param>
        /// <returns>建立完成的頭節點；輸入為空陣列時回傳 <see langword="null"/>。</returns>
        private static ListNode? BuildList(int[] values)
        {
            if (values.Length == 0)
            {
                return null;
            }

            ListNode head = new ListNode(values[0]);
            ListNode current = head;

            for (int i = 1; i < values.Length; i++)
            {
                current.next = new ListNode(values[i]);
                current = current.next;
            }

            return head;
        }

        /// <summary>
        /// 將鏈結串列節點依序轉為整數陣列，方便比對演算法結果及格式化輸出。
        /// </summary>
        /// <param name="head">要讀取的鏈結串列頭節點；可為 <see langword="null"/>。</param>
        /// <returns>依目前節點順序產生的陣列；空串列會回傳空陣列。</returns>
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
        /// 將整數序列格式化為方括號表示法，使主控台內容與題目範例容易對照。
        /// </summary>
        /// <param name="values">要顯示的整數序列，可為空陣列。</param>
        /// <returns>例如 <c>[1, 2, 3]</c>；空序列回傳 <c>[]</c>。</returns>
        private static string FormatList(int[] values)
        {
            return $"[{string.Join(", ", values)}]";
        }

        /// <summary>
        /// 以「逐節點拆下並接到新串列頭部」的迭代方式原地反轉單向鏈結串列。
        /// </summary>
        /// <remarks>
        /// 每輪先保存尚未處理的下一個節點，再把目前節點接到 <c>root</c> 前端。
        /// 輸入可為空串列，節點值不影響反轉流程；方法會修改原始節點的 <c>next</c> 指向。
        /// </remarks>
        /// <param name="head">要反轉的頭節點；空串列可傳入 <see langword="null"/>。</param>
        /// <returns>反轉後的新頭節點；輸入為空串列時回傳 <see langword="null"/>。</returns>
        public static ListNode? ReverseList(ListNode? head)
        {
            ListNode? root = null;

            while (head != null)
            {
                // 先保存尚未處理的下一節點，避免反轉 next 後遺失剩餘串列。
                ListNode? next = head.next;
                head.next = root;
                root = head;
                head = next;
            }

            return root;
        }

        /// <summary>
        /// 以 <c>prev</c>、<c>curr</c>、<c>next</c> 三指標迭代並原地反轉單向鏈結串列。
        /// </summary>
        /// <remarks>
        /// 迴圈中 <c>prev</c> 保持已反轉區段的頭，<c>curr</c> 指向目前節點，<c>next</c> 暫存尚未處理區段。
        /// 輸入可為空串列，節點值不影響反轉流程；方法會修改原始節點的 <c>next</c> 指向。
        /// </remarks>
        /// <param name="head">要反轉的頭節點；空串列可傳入 <see langword="null"/>。</param>
        /// <returns>反轉後的新頭節點；輸入為空串列時回傳 <see langword="null"/>。</returns>
        public static ListNode? ReverseList2(ListNode? head)
        {
            ListNode? prev = null;
            ListNode? curr = head;

            while (curr != null)
            {
                // 保存未處理區段後，將 curr 指回 prev，再讓三個指標同步向前推進。
                ListNode? next = curr.next;
                curr.next = prev;
                prev = curr;
                curr = next;
            }

            return prev;
        }

        /// <summary>
        /// 表示一筆可執行案例，包含案例說明、原始節點值與預期反轉順序。
        /// </summary>
        /// <param name="Description">案例所覆蓋的輸入情境。</param>
        /// <param name="Input">用來建立鏈結串列的節點值。</param>
        /// <param name="Expected">反轉後預期取得的節點值順序。</param>
        private sealed record SampleCase(string Description, int[] Input, int[] Expected);
    }
}
