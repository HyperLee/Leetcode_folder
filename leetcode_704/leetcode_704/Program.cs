namespace leetcode_704
{
    internal class Program
    {
        /// <summary>
        /// 704. Binary Search
        /// https://leetcode.com/problems/binary-search/description/
        /// 
        /// 704. 二分查找
        /// https://leetcode.cn/problems/binary-search/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 執行六組固定案例，逐案比較線性搜尋與二分搜尋的結果。
        /// 輸入為方法內定義、符合題目限制的遞增且不重複整數陣列；
        /// 輸出為每案的輸入、目標值、預期索引、兩種解法的 PASS/FAIL 與通過總數。
        /// </summary>
        private static void RunSamples()
        {
            SampleCase[] samples =
            [
                new("官方範例：找到目標", [-1, 0, 3, 5, 9, 12], 9, 4),
                new("官方範例：找不到目標", [-1, 0, 3, 5, 9, 12], 2, -1),
                new("單一元素命中", [5], 5, 0),
                new("首元素命中", [-10, -3, 0, 5, 9], -10, 0),
                new("尾元素命中", [-10, -3, 0, 5, 9], 9, 4),
                new("區間缺口未命中", [-10, -3, 0, 5, 9], 4, -1)
            ];

            int passedChecks = 0;
            int totalChecks = samples.Length * 2;

            for (int index = 0; index < samples.Length; index++)
            {
                SampleCase sample = samples[index];
                int linearResult = SearchLinear(sample.Nums, sample.Target);
                int binaryResult = Search(sample.Nums, sample.Target);
                bool linearPassed = linearResult == sample.Expected;
                bool binaryPassed = binaryResult == sample.Expected;

                passedChecks += linearPassed ? 1 : 0;
                passedChecks += binaryPassed ? 1 : 0;

                Console.WriteLine($"案例 {index + 1}：{sample.Name}");
                Console.WriteLine($"nums = {FormatArray(sample.Nums)}, target = {sample.Target}");
                Console.WriteLine($"預期索引：{sample.Expected}");
                Console.WriteLine($"線性搜尋：{linearResult} => {(linearPassed ? "PASS" : "FAIL")}");
                Console.WriteLine($"二分搜尋：{binaryResult} => {(binaryPassed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
            if (passedChecks != totalChecks)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 將整數陣列格式化為緊湊的方括號字串，方便測試輸出與 README 使用。
        /// 輸入為任意整數陣列，輸出格式如 <c>[-1,0,3]</c>。
        /// </summary>
        /// <param name="nums">要格式化的整數陣列。</param>
        /// <returns>以逗號分隔元素的方括號字串。</returns>
        private static string FormatArray(int[] nums)
        {
            return $"[{string.Join(",", nums)}]";
        }

        /// <summary>
        /// 使用線性搜尋由左至右逐一比較元素，作為二分搜尋的直觀基準解法。
        /// 輸入須為題目定義的非空、遞增且元素不重複的整數陣列與目標值；
        /// 找到時輸出目標所在索引，遍歷完成仍未找到時輸出 -1。
        /// 時間複雜度為 O(n)，輔助空間為 O(1)。
        /// </summary>
        /// <param name="nums">符合題目限制的非空遞增整數陣列。</param>
        /// <param name="target">要搜尋的目標整數。</param>
        /// <returns>目標所在索引；若目標不存在則回傳 -1。</returns>
        public static int SearchLinear(int[] nums, int target)
        {
            for (int index = 0; index < nums.Length; index++)
            {
                // 一旦命中即可提前結束，不必檢查後續元素。
                if (nums[index] == target)
                {
                    return index;
                }
            }

            return -1;
        }

        /// <summary>
        /// 使用閉區間二分搜尋，在每輪比較中間元素後排除不可能包含目標的一半。
        /// 輸入須為題目定義的非空、遞增且元素不重複的整數陣列與目標值；
        /// 找到時輸出目標所在索引，搜尋區間耗盡時輸出 -1。
        /// 時間複雜度為 O(log n)，輔助空間為 O(1)。
        /// </summary>
        /// <param name="nums">符合題目限制的非空遞增整數陣列。</param>
        /// <param name="target">要搜尋的目標整數。</param>
        /// <returns>目標所在索引；若目標不存在則回傳 -1。</returns>
        public static int Search(int[] nums, int target)
        {
            int left = 0;
            int right = nums.Length - 1;

            // 搜尋區間始終是閉區間 [left, right]；left > right 代表候選範圍已耗盡。
            while (left <= right)
            {
                // 先計算左右距離再加回 left，避免直接計算 left + right 可能造成溢位。
                int middle = left + (right - left) / 2;

                if (nums[middle] > target)
                {
                    // 中間值及其右側都不可能是答案，下一輪只保留左半部。
                    right = middle - 1;
                }
                else if (nums[middle] < target)
                {
                    // 中間值及其左側都不可能是答案，下一輪只保留右半部。
                    left = middle + 1;
                }
                else
                {
                    return middle;
                }
            }

            return -1;
        }

        /// <summary>
        /// 表示一組搜尋驗收案例，保存案例名稱、遞增且不重複的輸入陣列、目標值與預期索引。
        /// </summary>
        /// <param name="Name">案例的顯示名稱。</param>
        /// <param name="Nums">符合題目限制的遞增且不重複整數陣列。</param>
        /// <param name="Target">要搜尋的目標整數。</param>
        /// <param name="Expected">目標索引；不存在時為 -1。</param>
        private sealed record SampleCase(string Name, int[] Nums, int Target, int Expected);
    }
}
