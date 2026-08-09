namespace leetcode_021
{
    internal class Program
    {
        /// <summary>
        /// 表示單向鏈結串列的節點，保存節點值與下一個節點參考。
        /// 節點值可為任意整數；尾節點的 <see cref="next"/> 為 <see langword="null"/>。
        /// </summary>
        public class ListNode
        {
            /// <summary>
            /// 目前節點保存的整數值。
            /// </summary>
            public int val;

            /// <summary>
            /// 下一個節點；若目前節點為串列尾端則為 <see langword="null"/>。
            /// </summary>
            public ListNode? next;

            /// <summary>
            /// 建立單向串列節點，並可選擇在建立時指定下一個節點。
            /// </summary>
            /// <param name="val">目前節點保存的整數值，預設為 0。</param>
            /// <param name="next">下一個節點；未指定時代表目前節點為串列尾端。</param>
            public ListNode(int val = 0, ListNode? next = null)
            {
                this.val = val;
                this.next = next;
            }
        }


        /// <summary>
        /// 21. Merge Two Sorted Lists
        /// https://leetcode.com/problems/merge-two-sorted-lists/description/
        /// <para>
        /// You are given the heads of two sorted linked lists list1 and list2.
        ///
        /// Merge the two lists into one sorted list. The list should be made by splicing together the nodes of the first two lists.
        ///
        /// Return the head of the merged linked list.
        ///
        /// Example 1:
        /// Input: list1 = [1,2,4], list2 = [1,3,4]
        /// Output: [1,1,2,3,4,4]
        ///
        /// Example 2:
        /// Input: list1 = [], list2 = []
        /// Output: []
        ///
        /// Example 3:
        /// Input: list1 = [], list2 = [0]
        /// Output: [0]
        ///
        /// Constraints:
        /// - The number of nodes in both lists is in the range [0, 50].
        /// - -100 &lt;= Node.val &lt;= 100
        /// - Both list1 and list2 are sorted in non-decreasing order.
        /// </para>
        /// <para>
        /// 21. 合併兩個排序鏈結串列
        /// https://leetcode.cn/problems/merge-two-sorted-lists/description/
        ///
        /// 給定兩個排序 linked list 的頭節點 list1 和 list2。
        ///
        /// 請將這兩個 linked list 合併成一個排序串列。合併後的串列應由拼接前兩個串列的節點構成。
        ///
        /// 回傳合併後 linked list 的頭節點。
        ///
        /// 範例 1：
        /// 輸入：list1 = [1,2,4], list2 = [1,3,4]
        /// 輸出：[1,1,2,3,4,4]
        ///
        /// 範例 2：
        /// 輸入：list1 = [], list2 = []
        /// 輸出：[]
        ///
        /// 範例 3：
        /// 輸入：list1 = [], list2 = [0]
        /// 輸出：[0]
        ///
        /// 限制條件：
        /// - 兩個 linked list 的節點總數介於 [0, 50]。
        /// - -100 &lt;= Node.val &lt;= 100
        /// - list1 和 list2 都以非遞減順序排序。
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 執行七組固定案例，分別驗證遞迴與迭代合併方法。
        /// 每組案例的輸入皆為非遞減排序陣列，輸出為兩種解法的比對結果與總通過數。
        /// </summary>
        private static void RunSamples()
        {
            (int[] List1, int[] List2, int[] Expected)[] cases =
            [
                ([], [], []),
                ([], [0], [0]),
                ([1], [], [1]),
                ([1, 2, 4], [1, 3, 4], [1, 1, 2, 3, 4, 4]),
                ([-10, -3, 0, 5], [-6, -3, 2, 9], [-10, -6, -3, -3, 0, 2, 5, 9]),
                ([1, 1, 2], [1, 1, 3], [1, 1, 1, 1, 2, 3]),
                ([1, 5, 9], [2, 3, 4, 6, 7, 8], [1, 2, 3, 4, 5, 6, 7, 8, 9])
            ];

            int passed = 0;

            for (int i = 0; i < cases.Length; i++)
            {
                passed += RunCase(i + 1, cases[i].List1, cases[i].List2, cases[i].Expected);
            }

            int total = cases.Length * 2;
            Console.WriteLine($"總結：{passed}/{total} 項驗證通過");
        }

        /// <summary>
        /// 執行單一測試案例，為兩種會重新串接節點的解法分別建立獨立輸入。
        /// 輸入陣列必須為非遞減排序；回傳本案例通過的解法數，範圍為 0 到 2。
        /// </summary>
        /// <param name="caseNumber">從 1 開始顯示的案例編號。</param>
        /// <param name="list1Values">第一條排序串列的節點值。</param>
        /// <param name="list2Values">第二條排序串列的節點值。</param>
        /// <param name="expected">預期的合併結果。</param>
        /// <returns>遞迴法與迭代法中通過驗證的方法數。</returns>
        private static int RunCase(int caseNumber, int[] list1Values, int[] list2Values, int[] expected)
        {
            ListNode? recursiveResult = MergeTwoLists(BuildList(list1Values), BuildList(list2Values));
            ListNode? iterativeResult = MergeTwoLists2(BuildList(list1Values), BuildList(list2Values));
            int[] recursiveValues = ToArray(recursiveResult);
            int[] iterativeValues = ToArray(iterativeResult);
            bool recursivePassed = recursiveValues.SequenceEqual(expected);
            bool iterativePassed = iterativeValues.SequenceEqual(expected);

            Console.WriteLine($"案例 {caseNumber}：l1 = {FormatList(list1Values)}，l2 = {FormatList(list2Values)}");
            Console.WriteLine(
                $"  遞迴法：預期 {FormatList(expected)}，實際 {FormatList(recursiveValues)} => {(recursivePassed ? "PASS" : "FAIL")}");
            Console.WriteLine(
                $"  迭代法：預期 {FormatList(expected)}，實際 {FormatList(iterativeValues)} => {(iterativePassed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return (recursivePassed ? 1 : 0) + (iterativePassed ? 1 : 0);
        }

        /// <summary>
        /// 將整數陣列依原順序建立為單向串列。
        /// 輸入可為空陣列；空陣列回傳 <see langword="null"/>，否則回傳串列頭節點。
        /// </summary>
        /// <param name="values">要依序寫入節點的整數值。</param>
        /// <returns>建立完成的串列頭節點，或空陣列所對應的 <see langword="null"/>。</returns>
        private static ListNode? BuildList(int[] values)
        {
            ListNode dummy = new ListNode();
            ListNode tail = dummy;

            foreach (int value in values)
            {
                tail.next = new ListNode(value);
                tail = tail.next;
            }

            return dummy.next;
        }

        /// <summary>
        /// 依序走訪單向串列並轉成整數陣列，供測試比較與輸出使用。
        /// 輸入可為 <see langword="null"/>；空串列回傳空陣列。
        /// </summary>
        /// <param name="head">要轉換的串列頭節點。</param>
        /// <returns>依串列順序保存所有節點值的陣列。</returns>
        private static int[] ToArray(ListNode? head)
        {
            List<int> values = [];

            while (head is not null)
            {
                values.Add(head.val);
                head = head.next;
            }

            return [.. values];
        }

        /// <summary>
        /// 將整數陣列格式化為易讀的串列表示法。
        /// 輸入可為空陣列；輸出格式固定為以方括號包住、逗號分隔的節點值。
        /// </summary>
        /// <param name="values">要格式化的整數陣列。</param>
        /// <returns>例如 <c>[1, 2, 3]</c>；空陣列則為 <c>[]</c>。</returns>
        private static string FormatList(int[] values)
        {
            return $"[{string.Join(", ", values)}]";
        }


        /// <summary>
        /// 以遞迴方式合併兩條非遞減排序串列。
        /// 每次選出頭節點值較小的串列，並將該節點接到剩餘串列的遞迴合併結果之前。
        /// 輸入可為空串列；輸出會重用並重新串接原有節點，形成一條非遞減排序串列。
        /// </summary>
        /// <param name="l1">第一條非遞減排序串列的頭節點，或 <see langword="null"/>。</param>
        /// <param name="l2">第二條非遞減排序串列的頭節點，或 <see langword="null"/>。</param>
        /// <returns>合併後串列的頭節點；兩個輸入皆為空時回傳 <see langword="null"/>。</returns>
        public static ListNode? MergeTwoLists(ListNode? l1, ListNode? l2)
        {
            if (l1 == null && l2 == null)
            {
                return null;
            }

            if (l1 == null)
            {
                return l2;
            }

            if (l2 == null)
            {
                return l1;
            }

            if (l1.val <= l2.val)
            {
                // l1 較小時固定為目前答案頭節點，再遞迴合併尚未處理的部分。
                l1.next = MergeTwoLists(l1.next, l2);
                return l1;
            }
            else
            {
                // l2 較小時採用對稱處理，讓每層遞迴只決定一個節點。
                l2.next = MergeTwoLists(l1, l2.next);
                return l2;
            }
        }

        /// <summary>
        /// 以迭代方式合併兩條非遞減排序串列。
        /// 使用虛擬頭節點統一首次串接流程，持續移動尾端指標並選取較小的目前節點。
        /// 輸入可為空串列；輸出會重用並重新串接原有節點，形成一條非遞減排序串列。
        /// </summary>
        /// <param name="l1">第一條非遞減排序串列的頭節點，或 <see langword="null"/>。</param>
        /// <param name="l2">第二條非遞減排序串列的頭節點，或 <see langword="null"/>。</param>
        /// <returns>合併後串列的頭節點；兩個輸入皆為空時回傳 <see langword="null"/>。</returns>
        public static ListNode? MergeTwoLists2(ListNode? l1, ListNode? l2)
        {
            ListNode dummy = new ListNode();
            ListNode tail = dummy;

            while (l1 is not null && l2 is not null)
            {
                ListNode selected;

                if (l1.val <= l2.val)
                {
                    selected = l1;
                    l1 = l1.next;
                }
                else
                {
                    selected = l2;
                    l2 = l2.next;
                }

                tail.next = selected;
                tail = selected;
            }

            // 其中一條串列已耗盡，另一條仍保持排序，可直接整段接到結果尾端。
            tail.next = l1 ?? l2;

            return dummy.next;
        }
    }
}