namespace leetcode_1550
{
    internal class Program
    {
        /// <summary>
        /// 程式進入點會以固定案例分別執行兩種解法，將預期結果與實際結果比較，並輸出每一個解法的 PASS/FAIL 狀態。
        /// 本範例不使用命令列輸入；測試資料與預期結果直接寫在 Main 中，最後彙整所有檢查的通過數量。
        ///
        /// 1550. Three Consecutive Odds
        /// https://leetcode.com/problems/three-consecutive-odds/description/?envType=daily-question&envId=2024-07-01
        /// 1550. 存在连续三个奇数的数组
        /// https://leetcode.cn/problems/three-consecutive-odds/description/
        ///
        /// Given an integer array arr, return true if there are three consecutive odd numbers in the array.
        /// Otherwise, return false.
        ///
        /// 給定一個整數陣列 arr，如果陣列中有 三個相鄰且皆為奇數 的元素，則回傳 true；
        /// 否則回傳 false。
        ///
        /// </summary>
        /// <param name="args">命令列參數；本範例不使用。</param>
        static void Main(string[] args)
        {
            int passedChecks = 0;
            const int totalChecks = 12;

            Console.WriteLine("LeetCode 1550 - Three Consecutive Odds");
            Console.WriteLine();

            passedChecks += RunTestCase(1, "官方案例：沒有三個連續奇數", new[] { 2, 6, 4, 1 }, false);
            passedChecks += RunTestCase(
                2,
                "官方案例：中段出現三個連續奇數",
                new[] { 1, 2, 34, 3, 4, 5, 7, 23, 12 },
                true);
            passedChecks += RunTestCase(3, "恰好三個元素", new[] { 1, 3, 5 }, true);
            passedChecks += RunTestCase(4, "少於三個元素", new[] { 1, 3 }, false);
            passedChecks += RunTestCase(5, "全部為偶數", new[] { 2, 4, 6, 8 }, false);
            passedChecks += RunTestCase(6, "重複奇數", new[] { 1, 1, 1, 2 }, true);

            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 通過");
        }

        /// <summary>
        /// 執行一組固定案例，分別呼叫兩個連續奇數解法，並輸出預期值、實際值與 PASS/FAIL 結果。
        /// 輸入陣列須符合題目限制：長度介於 1 到 1000，且每個元素介於 1 到 1000；
        /// 此方法會複製陣列後再交給每個解法，確保每個解法收到相同的原始資料。
        /// </summary>
        /// <param name="caseNumber">從 1 開始顯示的案例編號。</param>
        /// <param name="name">案例涵蓋情境的名稱。</param>
        /// <param name="arr">要交給兩個解法驗證的整數陣列。</param>
        /// <param name="expected">此案例預期的布林結果。</param>
        /// <returns>此案例通過的解法數量，範圍為 0 到 2。</returns>
        private static int RunTestCase(int caseNumber, string name, int[] arr, bool expected)
        {
            bool firstResult = ThreeConsecutiveOdds((int[])arr.Clone());
            bool secondResult = ThreeConsecutiveOdds2((int[])arr.Clone());
            bool firstPassed = firstResult == expected;
            bool secondPassed = secondResult == expected;

            Console.WriteLine($"案例 {caseNumber}：{name}");
            Console.WriteLine($"輸入：[{string.Join(", ", arr)}]");
            Console.WriteLine($"預期：{expected}");
            Console.WriteLine(
                $"ThreeConsecutiveOdds（固定三格視窗）：實際 {firstResult} -> {(firstPassed ? "PASS" : "FAIL")}");
            Console.WriteLine(
                $"ThreeConsecutiveOdds2（連續奇數滑動視窗）：實際 {secondResult} -> {(secondPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return (firstPassed ? 1 : 0) + (secondPassed ? 1 : 0);
        }

        /// <summary>
        /// 使用固定長度為 3 的視窗，逐一檢查每三個相鄰元素是否全部為奇數。
        /// 輸入陣列須符合題目限制：長度介於 1 到 1000，且每個元素介於 1 到 1000；
        /// 找到符合條件的視窗就回傳 true，掃描結束仍未找到則回傳 false。
        /// </summary>
        /// <param name="arr">符合題目限制的整數陣列；此方法不會修改輸入內容。</param>
        /// <returns>若存在三個相鄰且皆為奇數的元素則回傳 true，否則回傳 false。</returns>
        /// <remarks>
        /// 以索引 i 作為視窗右端點，從 i = 2 開始即可涵蓋所有長度為 3 的連續區段。
        /// 時間複雜度為 O(n)，額外空間複雜度為 O(1)。
        /// 參考資料：
        /// https://leetcode.cn/problems/three-consecutive-odds/solutions/382537/cun-zai-lian-xu-san-ge-qi-shu-de-shu-zu-by-leetcod/
        /// https://leetcode.cn/problems/three-consecutive-odds/solutions/860041/1550-cun-zai-lian-xu-san-ge-qi-shu-de-sh-tt3w/
        /// </remarks>
        public static bool ThreeConsecutiveOdds(int[] arr)
        {
            int n = arr.Length;

            // i 是三格視窗的最右索引，從 2 開始即可涵蓋每一個連續三元素區段。
            for (int i = 2; i < n; i++)
            {
                // 三個位置都為奇數時，視窗已符合條件，不必繼續掃描。
                if (arr[i - 2] % 2 == 1 && arr[i - 1] % 2 == 1 && arr[i] % 2 == 1)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 以滑動視窗維護目前連續奇數區段的左右邊界，找到長度至少為 3 的區段時回傳 true。
        /// 輸入陣列須符合題目限制：長度介於 1 到 1000，且每個元素介於 1 到 1000；
        /// 每次遇到偶數便重新設定左界，掃描結束仍未形成三個連續奇數時回傳 false。
        /// </summary>
        /// <param name="arr">符合題目限制的整數陣列；此方法不會修改輸入內容。</param>
        /// <returns>若存在三個相鄰且皆為奇數的元素則回傳 true，否則回傳 false。</returns>
        /// <remarks>
        /// right 只向右掃描一次；left 永遠指向最近一個偶數之後的位置，因此 [left, right] 代表目前的連續奇數區段。
        /// 時間複雜度為 O(n)，額外空間複雜度為 O(1)。
        /// </remarks>
        public static bool ThreeConsecutiveOdds2(int[] arr)
        {
            int n = arr.Length;

            if (n < 3)
            {
                return false;
            }

            int left = 0, right = 0;

            while (right < n)
            {
                // 遇到偶數後，下一個位置才可能開始新的連續奇數區段。
                if (arr[right] % 2 == 0)
                {
                    left = right + 1;
                }

                // [left, right] 長度達到 3，代表已找到三個相鄰奇數。
                if (right - left + 1 == 3)
                {
                    return true;
                }

                // 右界每次只向右移動一格，維持 O(n) 掃描。
                right++;
            }

            return false;
        }
    }
}
