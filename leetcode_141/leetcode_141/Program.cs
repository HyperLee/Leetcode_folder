namespace leetcode_141
{
    class Program
    {
        /// <summary>
        /// 表示單向鏈結串列中的一個節點，保存整數值與下一個節點的參考。
        /// </summary>
        public class ListNode
        {
            /// <summary>
            /// 取得或設定目前節點保存的整數值。
            /// </summary>
            public int val;

            /// <summary>
            /// 取得或設定下一個節點；若目前節點為鏈尾且未形成環，則為 <see langword="null"/>。
            /// </summary>
            public ListNode? next;

            /// <summary>
            /// 建立指定值的鏈結串列節點，初始狀態不連接下一個節點。
            /// </summary>
            /// <param name="x">要存放在節點中的整數值。</param>
            public ListNode(int x)
            {
                val = x;
                next = null;
            }
        }

        /// <summary>
        /// 描述一筆可執行範例，包含節點值、尾端連回索引與預期判圈結果。
        /// </summary>
        /// <param name="Name">案例顯示名稱。</param>
        /// <param name="Values">依序建立鏈結串列的節點值；可為空陣列。</param>
        /// <param name="CycleIndex">
        /// 尾節點要連回的零起始索引；<c>-1</c> 表示不建立環，其餘值必須是有效索引。
        /// </param>
        /// <param name="Expected">預期是否存在環。</param>
        private sealed record SampleCase(
            string Name,
            int[] Values,
            int CycleIndex,
            bool Expected);

        /// <summary>
        /// Floyd判圈算法(Floyd Cycle Detection Algorithm)，又稱龜兔賽跑算法(Tortoise and Hare Algorithm)
        /// 141. Linked List Cycle
        /// https://leetcode.com/problems/linked-list-cycle/description/
        /// 141. 环形链表
        /// https://leetcode.cn/problems/linked-list-cycle/description/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 執行六筆固定案例，為每筆資料建立獨立串列並比較預期與實際判圈結果。
        /// </summary>
        /// <remarks>
        /// 案例涵蓋空串列、單節點、多節點，以及尾端連回不同位置的環狀串列；
        /// 執行後會逐筆輸出 PASS/FAIL，並顯示通過筆數。
        /// </remarks>
        private static void RunSamples()
        {
            SampleCase[] samples =
            [
                new("空串列", [], -1, false),
                new("單節點無環", [1], -1, false),
                new("單節點自環", [1], 0, true),
                new("多節點無環", [1, 2, 3, 4], -1, false),
                new("尾端連回中間節點", [3, 2, 0, -4], 1, true),
                new("尾端連回頭節點", [1, 2], 0, true)
            ];

            int passed = 0;

            for (int index = 0; index < samples.Length; index++)
            {
                SampleCase sample = samples[index];
                ListNode? head = BuildList(sample.Values, sample.CycleIndex);
                bool actual = HasCycle(head);
                bool isPassed = actual == sample.Expected;

                if (isPassed)
                {
                    passed++;
                }

                Console.WriteLine($"案例 {index + 1}：{sample.Name}");
                Console.WriteLine(
                    $"輸入：head = [{string.Join(", ", sample.Values)}], cycleIndex = {sample.CycleIndex}");
                Console.WriteLine($"預期：{sample.Expected}");
                Console.WriteLine($"實際：{actual}");
                Console.WriteLine($"結果：{(isPassed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passed}/{samples.Length} 筆測試通過");
        }

        /// <summary>
        /// 依照節點值建立單向鏈結串列，並可選擇讓尾節點連回指定位置以形成環。
        /// </summary>
        /// <param name="values">按順序建立節點的整數值；空陣列會產生空串列。</param>
        /// <param name="cycleIndex">
        /// 尾節點要連回的零起始索引；<c>-1</c> 表示無環，其餘值必須落在陣列索引範圍內。
        /// </param>
        /// <returns>建立完成的頭節點；當 <paramref name="values"/> 為空時回傳 <see langword="null"/>。</returns>
        private static ListNode? BuildList(int[] values, int cycleIndex)
        {
            if (values.Length == 0)
            {
                return null;
            }

            ListNode[] nodes = new ListNode[values.Length];

            for (int index = 0; index < values.Length; index++)
            {
                nodes[index] = new ListNode(values[index]);
            }

            for (int index = 0; index < nodes.Length - 1; index++)
            {
                nodes[index].next = nodes[index + 1];
            }

            if (cycleIndex >= 0)
            {
                nodes[^1].next = nodes[cycleIndex];
            }

            return nodes[0];
        }

        /// <summary>
        /// 使用 Floyd 快慢指標判斷鏈結串列是否存在環；慢指標每次前進一步，快指標每次前進兩步。
        /// </summary>
        /// <param name="head">要檢查的鏈結串列頭節點；可為 <see langword="null"/>。</param>
        /// <returns>兩個指標相遇時回傳 <see langword="true"/>；快指標抵達鏈尾時回傳 <see langword="false"/>。</returns>
        /// <remarks>時間複雜度為 O(n)，額外空間複雜度為 O(1)，且不修改鏈結串列內容。</remarks>
        public static bool HasCycle(ListNode? head)
        {
            if (head == null || head.next == null)
            {
                return false;
            }

            ListNode? slow = head;
            ListNode? fast = head.next;

            while (fast != null && fast.next != null)
            {
                // 快慢指標在有限環內的相對距離會持續縮短；相遇即可確定存在環。
                if (ReferenceEquals(slow, fast))
                {
                    return true;
                }

                slow = slow?.next;
                fast = fast.next.next;
            }

            // 快指標能抵達 null，代表串列存在明確尾端，因此不可能有環。
            return false;
        }
    }
}
