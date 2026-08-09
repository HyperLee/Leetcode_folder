namespace leetcode_2058
{
    internal class Program
    {
        /// <summary>
        /// 表示單向鏈結串列節點；每個節點保存一個整數值與可為空的下一節點參考。
        /// </summary>
        public class ListNode
        {
            public int val;
            public ListNode? next;

            /// <summary>
            /// 建立單向鏈結串列節點。
            /// 輸入包含節點值及可選的下一節點；建立後可透過 <see cref="next"/> 串接後續節點。
            /// </summary>
            /// <param name="val">節點保存的整數值。</param>
            /// <param name="next">下一個節點；串列尾端為 <see langword="null"/>。</param>
            public ListNode(int val = 0, ListNode? next = null)
            {
                this.val = val;
                this.next = next;
            }
        }

        /// <summary>
        /// <para>
        /// 2058. Find the Minimum and Maximum Number of Nodes Between Critical Points
        /// https://leetcode.com/problems/find-the-minimum-and-maximum-number-of-nodes-between-critical-points/description/
        ///
        /// A critical point in a linked list is a local maximum or local minimum. A node is a local maximum if its value is strictly greater than both neighbors, and a local minimum if strictly smaller than both. A node must have both a previous and next node to qualify.
        ///
        /// Return [minDistance,maxDistance], the minimum and maximum distances between two distinct critical points. If fewer than two critical points exist, return [-1,-1].
        ///
        /// Images: https://assets.leetcode.com/uploads/2021/10/13/a1.png, https://assets.leetcode.com/uploads/2021/10/13/a2.png, and https://assets.leetcode.com/uploads/2021/10/14/a5.png
        ///
        /// Example 1:
        /// Input: head = [3,1]
        /// Output: [-1,-1]
        /// Explanation: There are no critical points.
        ///
        /// Example 2:
        /// Input: head = [5,3,1,2,5,1,2]
        /// Output: [1,3]
        /// Explanation: Nodes 3, 5, and 6 are critical. The minimum distance is 6 - 5 = 1 and the maximum is 6 - 3 = 3.
        ///
        /// Example 3:
        /// Input: head = [1,3,2,2,3,2,2,2,7]
        /// Output: [3,3]
        /// Explanation: Nodes 2 and 5 are local maxima, so both distances are 5 - 2 = 3. The last node is not critical because it has no next node.
        ///
        /// Constraints:
        /// - The number of nodes is in [2,10^5].
        /// - 1 &lt;= Node.val &lt;= 10^5
        /// </para>
        /// <para>
        /// 2058. 找出臨界點之間的最小和最大距離
        /// https://leetcode.cn/problems/find-the-minimum-and-maximum-number-of-nodes-between-critical-points/description/
        ///
        /// 鏈結串列中的臨界點是局部極大值或局部極小值。若節點值嚴格大於前後兩個節點，則為局部極大值；若嚴格小於兩者，則為局部極小值。節點必須同時有前一個與下一個節點才可能是臨界點。
        ///
        /// 回傳 [minDistance,maxDistance]，分別為任兩個不同臨界點之間的最小與最大距離。若臨界點不足兩個，回傳 [-1,-1]。
        ///
        /// 圖片：https://assets.leetcode.com/uploads/2021/10/13/a1.png、https://assets.leetcode.com/uploads/2021/10/13/a2.png、https://assets.leetcode.com/uploads/2021/10/14/a5.png
        ///
        /// 範例 1：
        /// 輸入：head = [3,1]
        /// 輸出：[-1,-1]
        /// 說明：沒有臨界點。
        ///
        /// 範例 2：
        /// 輸入：head = [5,3,1,2,5,1,2]
        /// 輸出：[1,3]
        /// 說明：第 3、5、6 個節點是臨界點；最小距離為 6 - 5 = 1，最大距離為 6 - 3 = 3。
        ///
        /// 範例 3：
        /// 輸入：head = [1,3,2,2,3,2,2,2,7]
        /// 輸出：[3,3]
        /// 說明：第 2 與第 5 個節點是局部極大值，因此兩種距離都是 5 - 2 = 3。最後一個節點沒有下一個節點，所以不是臨界點。
        ///
        /// 限制條件：
        /// - 節點數量在 [2,10^5] 範圍內。
        /// - 1 &lt;= Node.val &lt;= 10^5
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            (string Name, int[] Values, int[] Expected)[] cases =
            [
                ("最短合法串列，無臨界點", [3, 1], [-1, -1]),
                ("恰好兩個相鄰臨界點", [1, 3, 1, 2], [1, 1]),
                ("三個臨界點的一般案例", [5, 3, 1, 2, 5, 1, 2], [1, 3]),
                ("兩個間隔臨界點", [1, 3, 2, 2, 3, 2, 2, 2, 7], [3, 3]),
                ("相等值平台不構成嚴格極值", [2, 3, 3, 2], [-1, -1])
            ];

            int passedChecks = 0;

            foreach ((string name, int[] values, int[] expected) in cases)
            {
                passedChecks += RunCase(name, values, expected);
            }

            int totalChecks = cases.Length * 2;
            Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");

            if (passedChecks != totalChecks)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 執行單一測試案例，分別建立獨立鏈結串列供兩種解法使用，並比較預期與實際結果。
        /// 輸入必須包含案例名稱、至少兩個合法節點值及長度為 2 的預期距離陣列；
        /// 回傳通過的解法數量，範圍為 0 到 2。
        /// </summary>
        /// <param name="name">顯示於主控台的案例名稱。</param>
        /// <param name="values">用來建立鏈結串列的節點值。</param>
        /// <param name="expected">預期的最小與最大距離。</param>
        /// <returns>本案例通過的解法數量。</returns>
        private static int RunCase(string name, int[] values, int[] expected)
        {
            int[] streamingResult = NodesBetweenCriticalPoints(BuildList(values));
            int[] collectedIndicesResult = NodesBetweenCriticalPoints2(BuildList(values));

            Console.WriteLine($"Case: {name}");
            Console.WriteLine($"Input: {FormatArray(values)}");

            int passedChecks = 0;
            passedChecks += PrintResult("Solution 1 - 串流狀態", expected, streamingResult);
            passedChecks += PrintResult("Solution 2 - 收集索引", expected, collectedIndicesResult);
            Console.WriteLine();

            return passedChecks;
        }

        /// <summary>
        /// 依照非空整數陣列的順序建立單向鏈結串列，供演算法測試使用。
        /// 輸入陣列至少包含一個節點值；回傳新建立且不與其他案例共用節點的串列頭節點。
        /// </summary>
        /// <param name="values">依序放入鏈結串列的節點值。</param>
        /// <returns>新鏈結串列的頭節點。</returns>
        private static ListNode BuildList(int[] values)
        {
            ListNode head = new ListNode(values[0]);
            ListNode current = head;

            for (int i = 1; i < values.Length; i++)
            {
                ListNode next = new ListNode(values[i]);
                current.next = next;
                current = next;
            }

            return head;
        }

        /// <summary>
        /// 比較並輸出單一解法的預期與實際距離陣列。
        /// 輸入的兩個陣列皆應為長度 2；相同時回傳 1，否則回傳 0。
        /// </summary>
        /// <param name="solutionName">解法顯示名稱。</param>
        /// <param name="expected">預期距離陣列。</param>
        /// <param name="actual">實際距離陣列。</param>
        /// <returns>通過時為 1，失敗時為 0。</returns>
        private static int PrintResult(string solutionName, int[] expected, int[] actual)
        {
            bool passed = expected.SequenceEqual(actual);

            Console.WriteLine($"  {solutionName}");
            Console.WriteLine($"    Expected: {FormatArray(expected)}");
            Console.WriteLine($"    Actual:   {FormatArray(actual)}");
            Console.WriteLine($"    Result:   {(passed ? "PASS" : "FAIL")}");

            return passed ? 1 : 0;
        }

        /// <summary>
        /// 將整數陣列格式化為不含多餘空白的方括號表示法，方便主控台與 README 對照。
        /// 輸入可為任意長度的整數陣列；回傳例如 <c>[1,3]</c> 的文字結果。
        /// </summary>
        /// <param name="values">要格式化的整數陣列。</param>
        /// <returns>方括號包覆、逗號分隔的陣列文字。</returns>
        private static string FormatArray(int[] values)
        {
            return $"[{string.Join(",", values)}]";
        }

        /// <summary>
        /// 以一次串流走訪找出臨界點之間的最小與最大距離。
        /// 走訪時只保留第一個、上一個與目前臨界點索引：相鄰臨界點求最小距離，
        /// 第一個與目前臨界點求最大距離，因此額外空間為 O(1)。
        /// </summary>
        /// <param name="head">至少包含兩個節點，且節點值符合題目限制的非空鏈結串列頭節點。</param>
        /// <returns>長度為 2 的陣列 [最小距離, 最大距離]；臨界點少於兩個時回傳 [-1, -1]。</returns>
        public static int[] NodesBetweenCriticalPoints(ListNode head)
        {
            int minDistance = int.MaxValue;
            int maxDistance = -1;
            int firstCriticalIndex = -1;
            int previousCriticalIndex = -1;

            int index = 1;
            ListNode previous = head;
            ListNode? current = head.next;

            // 頭尾節點缺少一側鄰居，不可能是臨界點，因此只檢查完整的三節點視窗。
            while (current?.next is not null)
            {
                ListNode next = current.next;

                if (IsCriticalPoint(previous, current, next))
                {
                    if (firstCriticalIndex < 0)
                    {
                        firstCriticalIndex = index;
                    }

                    if (previousCriticalIndex >= 0)
                    {
                        // 最小距離只可能出現在索引排序後的相鄰臨界點之間。
                        minDistance = Math.Min(minDistance, index - previousCriticalIndex);
                        // 目前走訪位置最遠的配對必定是第一個與目前臨界點。
                        maxDistance = index - firstCriticalIndex;
                    }

                    previousCriticalIndex = index;
                }

                previous = current;
                current = next;
                index++;
            }

            return maxDistance < 0 ? [-1, -1] : [minDistance, maxDistance];
        }

        /// <summary>
        /// 先收集所有臨界點索引，再計算臨界點之間的最小與最大距離。
        /// 相鄰索引差的最小值即為最小距離，最後與第一個索引差即為最大距離；
        /// 額外空間為 O(k)，其中 k 是臨界點數量。
        /// </summary>
        /// <param name="head">至少包含兩個節點，且節點值符合題目限制的非空鏈結串列頭節點。</param>
        /// <returns>長度為 2 的陣列 [最小距離, 最大距離]；臨界點少於兩個時回傳 [-1, -1]。</returns>
        public static int[] NodesBetweenCriticalPoints2(ListNode head)
        {
            List<int> criticalIndices = [];
            int index = 1;
            ListNode previous = head;
            ListNode? current = head.next;

            while (current?.next is not null)
            {
                ListNode next = current.next;

                if (IsCriticalPoint(previous, current, next))
                {
                    criticalIndices.Add(index);
                }

                previous = current;
                current = next;
                index++;
            }

            if (criticalIndices.Count < 2)
            {
                return [-1, -1];
            }

            int minDistance = int.MaxValue;

            for (int i = 1; i < criticalIndices.Count; i++)
            {
                // 已按走訪順序收集索引，只需比較相鄰差即可取得全域最小值。
                minDistance = Math.Min(minDistance, criticalIndices[i] - criticalIndices[i - 1]);
            }

            int maxDistance = criticalIndices[^1] - criticalIndices[0];
            return [minDistance, maxDistance];
        }

        /// <summary>
        /// 判斷三節點視窗的中間節點是否嚴格大於或嚴格小於左右鄰居。
        /// 三個輸入節點必須依串列順序相鄰且皆非空；回傳中間節點是否為臨界點。
        /// </summary>
        /// <param name="previous">目前節點的前一個節點。</param>
        /// <param name="current">要判斷的目前節點。</param>
        /// <param name="next">目前節點的下一個節點。</param>
        /// <returns>目前節點為嚴格局部極大值或極小值時回傳 <see langword="true"/>。</returns>
        private static bool IsCriticalPoint(ListNode previous, ListNode current, ListNode next)
        {
            return (current.val > previous.val && current.val > next.val)
                || (current.val < previous.val && current.val < next.val);
        }
    }
}