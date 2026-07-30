namespace leetcode_476
{
    internal class Program
    {
        /// <summary>
        /// 476. Number Complement
        /// https://leetcode.com/problems/number-complement/description/?envType=daily-question&envId=2024-08-22
        /// 
        /// 476. 数字的补数
        /// https://leetcode.cn/problems/number-complement/description/
        /// 
        /// 本題目比較推薦 方法一 解法
        /// 位元運算
        /// 但是需要理解一下
        /// 不然不是很好懂
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            (string Name, int Input, int Expected)[] testCases =
            [
                ("Minimum input", 1, 0),
                ("Single set bit", 2, 1),
                ("Official example", 5, 2),
                ("All bits are one", 7, 0),
                ("Mixed bits", 10, 5),
                ("Maximum input", int.MaxValue, 0)
            ];

            int passed = 0;
            foreach ((string name, int input, int expected) in testCases)
            {
                int findComplementActual = FindComplement(input);
                int findComplement2Actual = FindComplement2(input);
                bool isPassed = findComplementActual == expected
                    && findComplement2Actual == expected;

                if (isPassed)
                {
                    passed++;
                }

                Console.WriteLine($"Case: {name}");
                Console.WriteLine($"Input: {input}");
                Console.WriteLine($"Expected: {expected}");
                Console.WriteLine($"FindComplement: {findComplementActual}");
                Console.WriteLine($"FindComplement2: {findComplement2Actual}");
                Console.WriteLine($"Result: {(isPassed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"Summary: {passed}/{testCases.Length} cases passed.");
            if (passed != testCases.Length)
            {
                Environment.ExitCode = 1;
            }
        }


        /// <summary>
        /// 使用位元遮罩計算正整數的補數。
        /// 方法逐次清除最低有效位的 <c>1</c>，找出最高有效位元，
        /// 再建立相同位數且全部為 <c>1</c> 的遮罩與原數執行 XOR。
        /// 輸入須符合 <c>1 &lt;= num &lt; 2^31</c>；
        /// 時間複雜度為 <c>O(log num)</c>，空間複雜度為 <c>O(1)</c>。
        /// </summary>
        /// <param name="num">要計算補數且符合題目限制的正整數。</param>
        /// <returns>只反轉有效二進位位數、不包含前導零的補數。</returns>
        /// <remarks>
        /// 參考：
        /// https://leetcode.cn/problems/number-complement/solutions/1052788/tong-ge-lai-shua-ti-la-jian-dan-gao-xiao-k0p9/
        /// https://leetcode.cn/problems/number-complement/solutions/1050060/shu-zi-de-bu-shu-by-leetcode-solution-xtn8/
        /// https://leetcode.cn/problems/number-complement/solutions/1052783/gong-shui-san-xie-yi-ti-shuang-jie-bian-wjl0y/
        /// </remarks>
        public static int FindComplement(int num)
        {
            int highbit = 1;
            int x = num;

            // 每輪清除最低位的 1；最後記錄到的位置就是最高有效位元。
            while (x != 0)
            {
                highbit = x & (-x);
                x = x & (x - 1);
            }

            // 將最高位左移後減一可得到有效位數全部為 1 的遮罩。
            return num ^ ((highbit << 1) - 1);
        }


        /// <summary>
        /// 使用同位數的全 <c>1</c> 整數計算正整數的補數。
        /// 方法從 <c>1</c> 開始反覆執行 <c>sum = sum * 2 + 1</c>，
        /// 形成 <c>1、3、7、15...</c>，直到涵蓋輸入的最高有效位元，
        /// 再以該值減去輸入。輸入須符合 <c>1 &lt;= num &lt; 2^31</c>；
        /// 時間複雜度為 <c>O(log num)</c>，空間複雜度為 <c>O(1)</c>。
        /// </summary>
        /// <param name="num">要計算補數且符合題目限制的正整數。</param>
        /// <returns>只反轉有效二進位位數、不包含前導零的補數。</returns>
        /// <remarks>
        /// 參考：
        /// https://leetcode.cn/problems/number-complement/solutions/1631259/by-stormsunshine-onze/
        /// </remarks>
        public static int FindComplement2(int num)
        {
            int sum = 1;
            while (sum < num)
            {
                // 每次新增一個最低位的 1，依序建立 1、3、7、15...。
                sum = sum * 2 + 1;
            }

            return sum - num;
        }
    }
}
