namespace leetcode_2058
{
    internal class Program
    {
        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int val = 0, ListNode next = null)
            {
                this.val = val;
                this.next = next;
            }
        }

        /// <summary>
        /// 2058. Find the Minimum and Maximum Number of Nodes Between Critical Points
        /// https://leetcode.com/problems/find-the-minimum-and-maximum-number-of-nodes-between-critical-points/description/?envType=daily-question&envId=2024-07-05
        /// 2058. 找出临界点之间的最小和最大距离
        /// https://leetcode.cn/problems/find-the-minimum-and-maximum-number-of-nodes-between-critical-points/description/
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
                current.next = new ListNode(values[i]);
                current = current.next;
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
        /// https://leetcode.cn/problems/find-the-minimum-and-maximum-number-of-nodes-between-critical-points/solutions/1077097/zhao-chu-lin-jie-dian-zhi-jian-de-zui-xi-b08v/
        /// https://leetcode.cn/problems/find-the-minimum-and-maximum-number-of-nodes-between-critical-points/solutions/1075991/go-mo-ni-bian-li-lian-biao-bian-li-lin-j-rx9s/
        /// https://leetcode.cn/problems/find-the-minimum-and-maximum-number-of-nodes-between-critical-points/solutions/2612349/2058-zhao-chu-lin-jie-dian-zhi-jian-de-z-i2az/
        /// 
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static int[] NodesBetweenCriticalPoints(ListNode head)
        {
            // 儲存 最小/最大距離
            int minDis = int.MaxValue;
            int maxDis = 0;

            // 暫存, 分別為, 第一/當前/下一個 臨界點的index位置
            int firstindex = -1;
            int previndex = -1;
            int currindex = -1;

            int index = 1;
            // 第一個node不會是臨界點(最後一個node也不會是臨界點)
            // , 所以curr要從index = 1 開始往下找
            ListNode prev = head, curr = head.next;

            // curr不要是結尾 (頭尾不會有極大/極小值)
            while(curr.next != null)
            {
                ListNode next = curr.next;

                // 局部極大/極小點位置
                if((curr.val > prev.val && curr.val > next.val) || (curr.val < prev.val && curr.val < next.val))
                {
                    if(firstindex < 0)
                    {
                        // 第一個臨界點位置給予 first
                        firstindex = index;
                    }

                    // 視窗滑動 概念, 往前塞
                    previndex = currindex; ;
                    currindex = index;

                    // >= 0, 代表之前已經有寫入臨界點位置, 
                    // 有兩個點即可計算 最大/最小 距離
                    if (previndex >= 0)
                    {
                        // 最小距離代表兩個點之間相鄰, 當前位置 - 前一個位置
                        minDis = Math.Min(minDis, currindex - previndex);
                        // 最大距離代表最遠(頭尾), 當前位置 - 第一個位置
                        maxDis = Math.Max(maxDis, currindex - firstindex);
                    }

                }

                // 往右繼續走遍歷node
                prev = curr;
                curr = next;
                index++;
            }


            if (minDis <= maxDis)
            {
                // 有找到
                return new int[] { minDis, maxDis };
            }
            else
            {
                // 找不到
                return new int[] { -1, -1 };
            }
        }

    }
}
