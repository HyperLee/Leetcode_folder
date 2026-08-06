namespace leetcode_1863
{
    internal class Program
    {
        /// <summary>
        /// 1863. Sum of All Subset XOR Totals
        /// https://leetcode.com/problems/sum-of-all-subset-xor-totals/description/?envType=daily-question&envId=2024-05-20
        /// 1863. 找出所有子集的异或总和再求和
        /// https://leetcode.cn/problems/sum-of-all-subset-xor-totals/description/
        /// </summary>
        /// <remarks>
        /// 執行固定案例，比較位元遮罩枚舉與數學最佳化兩種解法，並以程序結束碼表示測試結果。
        /// </remarks>
        /// <param name="args">命令列參數；本範例不使用。</param>
        static void Main(string[] args)
        {
            Environment.ExitCode = RunSamples() ? 0 : 1;
        }

        /// <summary>
        /// 執行六組符合題目限制的固定案例，逐一驗證兩種子集 XOR 總和解法。
        /// 每組資料都有預先計算的答案，方便直接比較預期值與實際值。
        /// </summary>
        /// <returns>十二項解法檢查全部通過時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
        private static bool RunSamples()
        {
            const int caseCount = 6;
            const int solutionCount = 2;
            int passedCount = 0;

            passedCount += RunCase("1. 官方範例一", new[] { 1, 3 }, 6);
            passedCount += RunCase("2. 官方範例二", new[] { 5, 1, 6 }, 28);
            passedCount += RunCase("3. 官方範例三", new[] { 3, 4, 5, 6, 7, 8 }, 480);
            passedCount += RunCase("4. 單一元素", new[] { 5 }, 5);
            passedCount += RunCase("5. 重複值", new[] { 2, 2 }, 4);
            passedCount += RunCase("6. 長度上界", new[] { 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20 }, 40960);

            int totalCount = caseCount * solutionCount;
            Console.WriteLine();
            Console.WriteLine($"總結：{passedCount}/{totalCount} 項測試通過");
            return passedCount == totalCount;
        }

        /// <summary>
        /// 對一組輸入依序執行位元遮罩與數學最佳化解法，輸出預期值、實際值與通過狀態。
        /// </summary>
        /// <param name="name">顯示於主控台的案例名稱。</param>
        /// <param name="nums">符合題目限制的正整數陣列。</param>
        /// <param name="expected">所有子集 XOR 總和的預期結果。</param>
        /// <returns>本案例通過的解法數量，範圍為 0 到 2。</returns>
        private static int RunCase(string name, int[] nums, int expected)
        {
            Console.WriteLine();
            Console.WriteLine($"案例：{name}");
            Console.WriteLine($"Input：nums = {FormatArray(nums)}");

            int passedCount = 0;
            passedCount += RunSolution("解法一：位元遮罩枚舉", SubsetXORSum, nums, expected) ? 1 : 0;
            passedCount += RunSolution("解法二：數學最佳化", SubsetXORSumOptimized, nums, expected) ? 1 : 0;
            return passedCount;
        }

        /// <summary>
        /// 執行指定的子集 XOR 總和解法，並將實際結果與已知答案比較。
        /// </summary>
        /// <param name="solutionName">顯示於主控台的解法名稱。</param>
        /// <param name="solution">接受整數陣列並回傳子集 XOR 總和的函式。</param>
        /// <param name="nums">要交給解法處理的輸入陣列。</param>
        /// <param name="expected">此輸入的預期答案。</param>
        /// <returns>實際結果等於預期答案時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
        private static bool RunSolution(string solutionName, Func<int[], int> solution, int[] nums, int expected)
        {
            int actual = solution(nums);
            bool passed = actual == expected;

            Console.WriteLine(solutionName);
            Console.WriteLine($"Expected：{expected}");
            Console.WriteLine($"Actual：{actual}");
            Console.WriteLine($"Result：{(passed ? "PASS" : "FAIL")}");
            return passed;
        }

        /// <summary>
        /// 將整數陣列轉換為固定的方括號與逗號分隔格式，供測試輸出與 README 範例使用。
        /// </summary>
        /// <param name="values">要格式化的整數陣列。</param>
        /// <returns>例如 <c>[1, 3]</c> 的文字表示。</returns>
        private static string FormatArray(int[] values)
        {
            return $"[{string.Join(", ", values)}]";
        }

        /// <summary>
        /// 計算輸入陣列所有子集的 XOR 總和，再將每個子集的結果加總。
        /// 使用位元遮罩表示子集：遮罩第 i 位為 1 時，將 nums[i] 納入目前子集並進行 XOR。
        /// 輸入需符合題目限制：長度為 1 到 12，且每個元素為 1 到 20；方法不會修改輸入陣列。
        /// </summary>
        /// <param name="nums">用來產生所有子集的正整數陣列。</param>
        /// <returns>所有 2^n 個子集（包含空子集）的 XOR 總和。</returns>
        /// <remarks>時間複雜度為 O(n × 2^n)，額外空間複雜度為 O(1)。</remarks>
        public static int SubsetXORSum(int[] nums)
        {
            int sum = 0;
            int n = nums.Length;
            int total = 1 << n;

            // mask 的每個 bit 對應一個元素是否被選入，因此 0 到 2^n - 1 恰好涵蓋所有子集。
            for (int mask = 0; mask < total; mask++)
            {
                int value = 0;

                for (int i = 0; i < n; i++)
                {
                    if ((mask & (1 << i)) != 0)
                    {
                        // 只把目前子集選中的元素併入 XOR 累積值。
                        value ^= nums[i];
                    }
                }

                sum += value;
            }

            return sum;
        }

        /// <summary>
        /// 使用位元貢獻公式計算所有子集的 XOR 總和。
        /// 先對所有元素取 bitwise OR；只要某個 bit 至少在一個元素中出現，它就會在全部子集的 XOR 結果中出現 2^(n-1) 次。
        /// 輸入需符合題目限制：長度為 1 到 12，且每個元素為 1 到 20；方法不會修改輸入陣列。
        /// </summary>
        /// <param name="nums">用來產生所有子集的正整數陣列。</param>
        /// <returns>所有 2^n 個子集（包含空子集）的 XOR 總和。</returns>
        /// <remarks>時間複雜度為 O(n)，額外空間複雜度為 O(1)。</remarks>
        public static int SubsetXORSumOptimized(int[] nums)
        {
            int combinedOr = 0;

            foreach (int num in nums)
            {
                combinedOr |= num;
            }

            // 每個曾出現的 bit，在所有子集中恰有一半的 XOR 結果為 1。
            return combinedOr * (1 << (nums.Length - 1));
        }
    }
}