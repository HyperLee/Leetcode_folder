namespace leetcode_713
{
    internal class Program
    {
        /// <summary>
        /// 713. Subarray Product Less Than K
        /// https://leetcode.com/problems/subarray-product-less-than-k/?envType=daily-question&envId=2024-03-27
        /// 
        /// 713. 乘积小于 K 的子数组
        /// https://leetcode.cn/problems/subarray-product-less-than-k/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            (int[] Nums, int K, int Expected)[] samples =
            {
                (new int[] { 10, 5, 2, 6 }, 100, 8),
                (new int[] { 1, 2, 3 }, 0, 0),
                (new int[] { 1, 2, 3 }, 1, 0),
                (new int[] { 1 }, 2, 1),
                (new int[] { 1, 1, 1 }, 2, 6)
            };

            int passedCount = 0;

            for (int i = 0; i < samples.Length; i++)
            {
                (int[] nums, int k, int expected) = samples[i];

                if (RunSample(i + 1, nums, k, expected))
                {
                    passedCount++;
                }
            }

            Console.WriteLine($"總結：{passedCount}/{samples.Length} 筆測試通過");

            if (passedCount != samples.Length)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 計算乘積嚴格小於 <paramref name="k"/> 的連續非空子陣列數量。
        /// 利用元素皆為正整數時乘積的單調性維護滑動視窗；右端加入元素後，
        /// 若乘積不再符合條件便移動左端，讓每個元素最多進出視窗一次。
        /// </summary>
        /// <param name="nums">符合題目限制的非空正整數陣列。</param>
        /// <param name="k">子陣列乘積必須嚴格小於的非負整數上限。</param>
        /// <returns>乘積嚴格小於 <paramref name="k"/> 的連續非空子陣列數量。</returns>
        public static int NumSubarrayProductLessThanK(int[] nums, int k)
        {
            int count = 0;
            int product = 1;
            int length = nums.Length;
            int start = 0, end = 0;

            while (end < length)
            {
                // 右端加入新元素，product 始終代表目前 [start, end] 視窗的乘積。
                product *= nums[end];

                while (start <= end && product >= k)
                {
                    // 乘積過大時移除左端元素，直到視窗重新符合嚴格小於 k 的條件。
                    product /= nums[start];
                    start++;
                }

                // 固定 end 後，從 start 到 end 的每個起點都形成一個新的合法子陣列。
                count += end - start + 1;
                end++;
            }

            return count;
        }

        /// <summary>
        /// 執行一組範例並輸出輸入、預期值、實際值與驗證結果。
        /// </summary>
        /// <param name="caseNumber">輸出時顯示的案例編號。</param>
        /// <param name="nums">要交給解法計算的正整數陣列。</param>
        /// <param name="k">子陣列乘積的嚴格上限。</param>
        /// <param name="expected">手動推導的預期子陣列數量。</param>
        /// <returns>實際結果等於預期值時回傳 <see langword="true"/>。</returns>
        private static bool RunSample(int caseNumber, int[] nums, int k, int expected)
        {
            int actual = NumSubarrayProductLessThanK(nums, k);
            bool passed = actual == expected;

            Console.WriteLine($"案例 {caseNumber}");
            Console.WriteLine($"輸入：nums = [{FormatArray(nums)}], k = {k}");
            Console.WriteLine($"預期：{expected}");
            Console.WriteLine($"實際：{actual}");
            Console.WriteLine($"結果：{(passed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return passed;
        }

        /// <summary>
        /// 將整數陣列格式化成適合主控台顯示的逗號分隔文字。
        /// </summary>
        /// <param name="numbers">要格式化的整數陣列。</param>
        /// <returns>不含方括號的逗號分隔元素文字。</returns>
        private static string FormatArray(int[] numbers)
        {
            return string.Join(", ", numbers);
        }
    }
}