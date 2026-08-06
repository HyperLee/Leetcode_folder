namespace leetcode_2181
{
    internal class Program
    {
        /// <summary>
        /// 表示單向串列的一個節點，儲存整數值與下一個節點的參照。
        /// </summary>
        public class ListNode
        {
            public int val;
            public ListNode? next;

            /// <summary>
            /// 建立單向串列節點，可指定節點值與後續節點。
            /// </summary>
            /// <param name="val">節點儲存的整數值，預設為 0。</param>
            /// <param name="next">下一個節點；沒有後續節點時為 <see langword="null"/>。</param>
            public ListNode(int val = 0, ListNode? next = null)
            {
                this.val = val;
                this.next = next;
            }
        }

        /// <summary>
        /// 2181. Merge Nodes in Between Zeros
        /// https://leetcode.com/problems/merge-nodes-in-between-zeros/description/?envType=daily-question&envId=2024-07-04
        /// 2181. 合并零之间的节点
        /// https://leetcode.cn/problems/merge-nodes-in-between-zeros/description/
        /// </summary>
        /// <remarks>
        /// 以固定案例同時驗證原地合併與建立新串列兩種解法，並輸出預期值、實際值與比對結果。
        /// </remarks>
        /// <param name="args">命令列參數；本範例不使用。</param>
        private static void Main(string[] args)
        {
            int passed = 0;
            int total = 0;

            (int Passed, int Total) result = RunCase(
                "官方範例一",
                [0, 3, 1, 0, 4, 5, 2, 0],
                [4, 11]);
            passed += result.Passed;
            total += result.Total;

            result = RunCase(
                "官方範例二",
                [0, 1, 0, 3, 0, 2, 2, 0],
                [1, 3, 4]);
            passed += result.Passed;
            total += result.Total;

            result = RunCase("最小合法串列", [0, 7, 0], [7]);
            passed += result.Passed;
            total += result.Total;

            result = RunCase("重複總和", [0, 2, 2, 0, 1, 3, 0], [4, 4]);
            passed += result.Passed;
            total += result.Total;

            result = RunCase("節點值上界", [0, 1000, 1000, 0], [2000]);
            passed += result.Passed;
            total += result.Total;

            result = RunCase("防禦性空輸入", [], []);
            passed += result.Passed;
            total += result.Total;

            Console.WriteLine($"總結：{passed}/{total} 項測試通過");
            Environment.ExitCode = passed == total ? 0 : 1;
        }

        /// <summary>
        /// 執行一組串列案例，為兩種解法分別建立輸入，再比對合併後的節點值序列。
        /// </summary>
        /// <param name="name">顯示於輸出中的案例名稱。</param>
        /// <param name="input">以陣列表示的輸入串列；合法題目輸入必須以 0 開頭與結尾，且沒有相鄰的 0。</param>
        /// <param name="expected">預期的合併結果節點值。</param>
        /// <returns>回傳通過數與檢查總數；每組案例固定檢查兩種解法。</returns>
        private static (int Passed, int Total) RunCase(string name, int[] input, int[] expected)
        {
            Console.WriteLine($"=== {name} ===");
            Console.WriteLine($"Input: [{string.Join(", ", input)}]");
            Console.WriteLine($"Expected: [{string.Join(", ", expected)}]");

            ListNode? inPlaceInput = BuildList(input);
            int[] inPlaceActual = ToArray(MergeNodes(inPlaceInput));
            bool inPlacePassed = inPlaceActual.SequenceEqual(expected);
            Console.WriteLine(
                $"MergeNodes (原地): Actual=[{string.Join(", ", inPlaceActual)}], Result={(inPlacePassed ? "PASS" : "FAIL")}");

            ListNode? newListInput = BuildList(input);
            int[] newListActual = ToArray(MergeNodes2(newListInput));
            bool newListPassed = newListActual.SequenceEqual(expected);
            Console.WriteLine(
                $"MergeNodes2 (新串列): Actual=[{string.Join(", ", newListActual)}], Result={(newListPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return ((inPlacePassed ? 1 : 0) + (newListPassed ? 1 : 0), 2);
        }

        /// <summary>
        /// 將整數陣列依序轉換為單向串列，供每個測試與每種解法使用獨立輸入。
        /// </summary>
        /// <param name="values">要放入串列的節點值；可為空陣列。</param>
        /// <returns>建立完成的串列頭節點；輸入為空時回傳 <see langword="null"/>。</returns>
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
        /// 讀取單向串列的所有節點值，轉成易於比對與顯示的陣列。
        /// </summary>
        /// <param name="head">要讀取的串列頭節點；可為 <see langword="null"/>。</param>
        /// <returns>依串列順序排列的節點值；空串列回傳空陣列。</returns>
        private static int[] ToArray(ListNode? head)
        {
            List<int> values = [];
            ListNode? current = head;

            while (current != null)
            {
                values.Add(current.val);
                current = current.next;
            }

            return values.ToArray();
        }

        /// <summary>
        /// 原地合併串列中每兩個 0 之間的節點：將值累加到區段的第一個節點，並重接參照略過其餘節點與 0。
        /// </summary>
        /// <remarks>
        /// 輸入必須以 0 開頭與結尾，且不得有兩個相鄰的 0。此方法會修改原串列的節點值與 <c>next</c> 參照；時間複雜度為 O(n)，額外空間複雜度為 O(1)。
        /// </remarks>
        /// <param name="head">符合題目條件的串列頭節點；可為 <see langword="null"/> 以表示空輸入。</param>
        /// <returns>合併後的串列頭節點；輸入為空時回傳 <see langword="null"/>。</returns>
        public static ListNode? MergeNodes(ListNode? head)
        {
            if (head == null)
            {
                return null;
            }

            // 開頭的 0 只是區段邊界，第一個非零節點將被重用為輸出節點。
            ListNode? current = head.next;

            while (current != null && current.next != null)
            {
                if (current.next.val != 0)
                {
                    // 同一區段內累加數值，同時從串列中移除已納入總和的節點。
                    current.val += current.next.val;
                    current.next = current.next.next;
                }
                else
                {
                    // 遇到區段結尾的 0，略過它並移到下一個結果節點。
                    current.next = current.next.next;
                    current = current.next;
                }
            }

            return head.next;
        }

        /// <summary>
        /// 建立新串列來合併每兩個 0 之間的節點：以累加器收集區段總和，在遇到下一個 0 時建立結果節點。
        /// </summary>
        /// <remarks>
        /// 輸入必須以 0 開頭與結尾，且不得有兩個相鄰的 0。此方法不會修改輸入串列；時間複雜度為 O(n)，並為 k 個結果節點使用 O(k) 空間。
        /// </remarks>
        /// <param name="head">符合題目條件的串列頭節點；可為 <see langword="null"/> 以表示空輸入。</param>
        /// <returns>由新節點組成的合併串列；輸入為空時回傳 <see langword="null"/>。</returns>
        public static ListNode? MergeNodes2(ListNode? head)
        {
            if (head == null)
            {
                return null;
            }

            ListNode dummy = new ListNode();
            ListNode tail = dummy;
            ListNode? current = head.next;
            int sum = 0;

            while (current != null)
            {
                if (current.val == 0)
                {
                    // 區段結束時才建立一個結果節點，因此輸出不會包含分隔用的 0。
                    tail.next = new ListNode(sum);
                    tail = tail.next;
                    sum = 0;
                }
                else
                {
                    sum += current.val;
                }

                current = current.next;
            }

            return dummy.next;
        }
    }
}