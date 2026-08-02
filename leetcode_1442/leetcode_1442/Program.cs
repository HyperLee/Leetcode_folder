namespace leetcode_1442
{
    internal class Program
    {
        /// <summary>
        /// 1442. Count Triplets That Can Form Two Arrays of Equal XOR
        /// https://leetcode.com/problems/count-triplets-that-can-form-two-arrays-of-equal-xor/description/?envType=daily-question&envId=2024-05-30
        /// 1442. 形成两个异或相等数组的三元组数目
        /// https://leetcode.cn/problems/count-triplets-that-can-form-two-arrays-of-equal-xor/description/
        /// </summary>
        /// <remarks>
        /// 以固定案例執行 CountTriplets，輸出每筆資料的 Expected、Actual 與 PASS/FAIL，
        /// 讓主控台程式同時作為可重現的範例與基本自我檢查入口。
        /// </remarks>
        /// <param name="args">主控台啟動參數；本題示範不使用外部輸入。</param>
        static void Main(string[] args)
        {
            (int[] Input, int Expected)[] testCases =
            {
                (new[] { 2, 3, 1, 6, 7 }, 4),
                (new[] { 1, 2 }, 0),
                (new[] { 1 }, 0),
                (new[] { 1, 1, 1, 1, 1 }, 10),
                (new[] { 0, 0, 0 }, 4)
            };

            int passedCases = 0;

            Console.WriteLine("LeetCode 1442 - Count Triplets That Can Form Two Arrays of Equal XOR");
            Console.WriteLine("=== 測試結果 ===");

            for (int caseIndex = 0; caseIndex < testCases.Length; caseIndex++)
            {
                (int[] Input, int Expected) testCase = testCases[caseIndex];
                int actual = CountTriplets(testCase.Input);
                bool isPassed = actual == testCase.Expected;

                Console.WriteLine($"Case {caseIndex + 1}: [{string.Join(", ", testCase.Input)}]");
                Console.WriteLine($"Expected: {testCase.Expected}, Actual: {actual}, Result: {(isPassed ? "PASS" : "FAIL")}");

                if (isPassed)
                {
                    passedCases++;
                }
            }

            Console.WriteLine($"Summary: {passedCases}/{testCases.Length} checks passed");

            if (passedCases != testCases.Length)
            {
                Environment.ExitCode = 1;
            }
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/count-triplets-that-can-form-two-arrays-of-equal-xor/solutions/579281/xing-cheng-liang-ge-yi-huo-xiang-deng-sh-jud0/
        /// https://leetcode.cn/problems/count-triplets-that-can-form-two-arrays-of-equal-xor/solutions/782679/gong-shui-san-xie-xiang-jie-shi-yong-qia-7gzm/
        /// https://leetcode.cn/problems/count-triplets-that-can-form-two-arrays-of-equal-xor/solutions/1636078/by-stormsunshine-vo87/
        /// 
        /// 根据按位异或运算的性质，a = b 等价于 a ⊕ b = 0。  << 需要注意是關鍵
        /// 從範圍[i, k]範圍內去找出 j 數值
        /// 極為題目所求的 (i, j, k) 資料, i < j <= k
        /// i: 開頭
        /// k: 結尾
        /// j: 從 i + 1 ~ k 內去找
        /// 共 k - i 種取法
        /// 
        /// </summary>
        /// <remarks>
        /// 對每個 i 與 k（i &lt; k）計算連續區間 arr[i..k] 的 XOR。
        /// 若結果為 0，代表任意切點 j（i + 1 &lt;= j &lt;= k）都能讓左右兩段 XOR 相等，
        /// 因此一次加入 k - i 個答案。依題目限制，arr 長度介於 1 到 300，元素值介於 0 到 10^8。
        /// </remarks>
        /// <param name="arr">待分析的非負整數陣列，長度為 1 到 300。</param>
        /// <returns>符合條件的 (i, j, k) 三元組數量。</returns>
        public static int CountTriplets(int[] arr)
        {
            int count = 0;
            int n = arr.Length;

            // a == b 等價於 a ^ b == 0；左右兩段合併後就是 arr[i..k]。
            for (int i = 0; i < n; i++)
            {
                for (int k = i + 1; k < n; k++)
                {
                    int xor = 0;

                    // 固定端點後逐一累積區間 XOR，避免把每個 j 的左右 XOR 重算。
                    for (int j = i; j <= k; j++)
                    {
                        xor ^= arr[j];
                    }

                    if (xor == 0)
                    {
                        // j 可取 i + 1 到 k，共 k - i 個切點。
                        count += k - i;
                    }
                }
            }

            return count;

        }
    }
}
