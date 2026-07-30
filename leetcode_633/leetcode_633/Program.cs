namespace leetcode_633
{
    internal class Program
    {
        /// <summary>
        /// 633. Sum of Square Numbers
        /// https://leetcode.com/problems/sum-of-square-numbers/?envType=daily-question&envId=2024-06-17
        /// 633. 平方数之和
        /// https://leetcode.cn/problems/sum-of-square-numbers/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 依序執行七組固定案例，分別驗證雙指針與枚舉加二分搜尋兩種解法，
        /// 並列印輸入、預期結果、實際結果及最終通過項目數。
        /// 所有輸入都符合題目要求的非負 32 位元整數範圍，且不需要外部輸入。
        /// </summary>
        private static void RunSamples()
        {
            (string Name, int Input, bool Expected)[] sampleCases =
            [
                ("零", 0, true),
                ("最小正整數", 1, true),
                ("兩個相同平方數", 2, true),
                ("無法表示為平方和", 3, false),
                ("官方可行範例", 5, true),
                ("接近上限的完全平方數", 2_147_395_600, true),
                ("32 位元整數上限", int.MaxValue, false)
            ];

            int passed = 0;

            for (int index = 0; index < sampleCases.Length; index++)
            {
                (string name, int input, bool expected) = sampleCases[index];
                bool twoPointersActual = JudgeSquareSum(input);
                bool binarySearchActual = JudgeSquareSum2(input);
                bool twoPointersPassed = twoPointersActual == expected;
                bool binarySearchPassed = binarySearchActual == expected;

                passed += twoPointersPassed ? 1 : 0;
                passed += binarySearchPassed ? 1 : 0;

                Console.WriteLine($"案例 {index + 1}：{name}");
                Console.WriteLine($"輸入：c = {input}");
                Console.WriteLine($"預期：{expected}");
                Console.WriteLine(
                    $"解法一（雙指針）：{twoPointersActual} => {(twoPointersPassed ? "PASS" : "FAIL")}");
                Console.WriteLine(
                    $"解法二（枚舉 + 二分搜尋）：{binarySearchActual} => {(binarySearchPassed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            int totalChecks = sampleCases.Length * 2;
            Console.WriteLine($"總結：{passed}/{totalChecks} 項驗證通過");
        }

        /// <summary>
        /// https://leetcode.cn/problems/sum-of-square-numbers/solutions/747079/ping-fang-shu-zhi-he-by-leetcode-solutio-8ydl/
        /// 判斷非負整數 <paramref name="c"/> 是否能表示為兩個整數平方和。
        /// 從搜尋區間兩端放置指針，依平方和與目標值的比較結果單調縮小區間，
        /// 不需要列舉所有數字組合。
        /// </summary>
        /// <param name="c">要判斷的非負 32 位元整數。</param>
        /// <returns>若存在整數 a、b，使 a² + b² = c，則回傳 <see langword="true"/>；否則回傳 <see langword="false"/>。</returns>
        public static bool JudgeSquareSum(int c)
        {
            long left = 0;
            long right = (long)Math.Sqrt(c);

            while (left <= right)
            {
                // 使用 long 計算平方和，避免接近 int 上限時乘法溢位。
                long sum = left * left + right * right;

                if (sum == c)
                {
                    return true;
                }

                if (sum > c)
                {
                    // 平方和太大時只能縮小右指針；增加左指針只會讓總和更大。
                    right--;
                }
                else
                {
                    // 平方和太小時只能增加左指針；縮小右指針只會讓總和更小。
                    left++;
                }
            }

            return false;
        }

        /// <summary>
        /// 判斷非負整數 <paramref name="c"/> 是否能表示為兩個整數平方和。
        /// 此解法枚舉第一個整數，將剩餘值交由二分搜尋判斷是否為完全平方數，
        /// 藉此避免對第二個整數進行線性掃描。
        /// </summary>
        /// <param name="c">要判斷的非負 32 位元整數。</param>
        /// <returns>若存在整數 a、b，使 a² + b² = c，則回傳 <see langword="true"/>；否則回傳 <see langword="false"/>。</returns>
        public static bool JudgeSquareSum2(int c)
        {
            for (long first = 0; first * first <= c; first++)
            {
                long remaining = c - first * first;

                if (IsPerfectSquare(remaining))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 以二分搜尋判斷指定的非負整數是否為完全平方數。
        /// 搜尋範圍包含 0 與目標值本身，並以平方值和目標值的大小關係縮小區間。
        /// </summary>
        /// <param name="target">要判斷的非負整數；本專案的呼叫值不超過 <see cref="int.MaxValue"/>。</param>
        /// <returns>若存在整數 b，使 b² 等於 <paramref name="target"/>，則回傳 <see langword="true"/>；否則回傳 <see langword="false"/>。</returns>
        private static bool IsPerfectSquare(long target)
        {
            long left = 0;
            long right = target;

            while (left <= right)
            {
                long middle = left + (right - left) / 2;
                // target 最大為 int.MaxValue，因此以 long 相乘可安全涵蓋整個搜尋範圍。
                long square = middle * middle;

                if (square == target)
                {
                    return true;
                }

                if (square > target)
                {
                    right = middle - 1;
                }
                else
                {
                    left = middle + 1;
                }
            }

            return false;
        }
    }
}