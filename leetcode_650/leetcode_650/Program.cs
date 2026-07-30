namespace leetcode_650
{
    internal class Program
    {
        /// <summary>
        /// 650. 2 Keys Keyboard
        /// https://leetcode.com/problems/2-keys-keyboard/description/?envType=daily-question&envId=2024-08-19
        /// 
        /// 650. 两个键的键盘
        /// https://leetcode.cn/problems/2-keys-keyboard/description/
        /// 
        /// 本題目, 比較屬於 數學題目
        /// 需要推理公式
        /// 看懂才比較好解題
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 執行題目有效範圍內的固定測試案例，逐一比較三種解法與人工推導的預期結果。
        /// 測試資料涵蓋邊界、質數、合數與完全平方數；若任一解法不符合預期，
        /// 將程序結束碼設為 1，否則維持成功結束。
        /// </summary>
        private static void RunSamples()
        {
            TestCase[] testCases =
            [
                new("輸入下界", 1, 0),
                new("最小 Copy/Paste 操作", 2, 2),
                new("官方範例／質數", 3, 3),
                new("2 的冪次", 4, 4),
                new("一般合數", 6, 5),
                new("完全平方數", 9, 6),
                new("多種因數拆分", 12, 7),
                new("大質數", 997, 997),
                new("輸入上界／重複質因數", 1000, 21)
            ];

            int passed = 0;

            foreach (TestCase testCase in testCases)
            {
                if (RunSample(testCase))
                {
                    passed++;
                }

                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passed}/{testCases.Length} 組案例通過。");

            if (passed != testCases.Length)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 以同一個符合題目限制的正整數呼叫三種解法，並將各自結果與預期最少操作數比較。
        /// </summary>
        /// <param name="testCase">包含案例名稱、介於 1 至 1000 的輸入與預期結果。</param>
        /// <returns>三種解法都回傳預期結果時為 <see langword="true"/>；否則為 <see langword="false"/>。</returns>
        private static bool RunSample(TestCase testCase)
        {
            int bottomUpResult = MinSteps(testCase.N);
            int memoResult = MinSteps2(testCase.N);
            int factorizationResult = MinSteps3(testCase.N);
            bool isPassed = bottomUpResult == testCase.Expected
                && memoResult == testCase.Expected
                && factorizationResult == testCase.Expected;

            Console.WriteLine($"案例：{testCase.Name}");
            Console.WriteLine($"輸入 n = {testCase.N}");
            Console.WriteLine($"預期：{testCase.Expected}");
            Console.WriteLine($"MinSteps：{bottomUpResult}");
            Console.WriteLine($"MinSteps2：{memoResult}");
            Console.WriteLine($"MinSteps3：{factorizationResult}");
            Console.WriteLine($"結果：{(isPassed ? "PASS" : "FAIL")}");

            return isPassed;
        }


        /// <summary>
        /// 使用由小到大的動態規劃，計算畫面恰好得到 <paramref name="n"/> 個 A 的最少操作數。
        /// 對每個目標數枚舉不超過平方根的成對因數，將先完成較小數量再 Copy All 與 Paste
        /// 的操作成本轉移到目前狀態。輸入須符合題目限制 1 到 1000，回傳最少按鍵次數；
        /// 時間複雜度為 O(n√n)，空間複雜度為 O(n)。
        /// </summary>
        /// <param name="n">要在畫面上得到的 A 數量，範圍為 1 到 1000。</param>
        /// <returns>從初始的一個 A 出發，得到恰好 <paramref name="n"/> 個 A 的最少操作數。</returns>
        public static int MinSteps(int n)
        {
            int[] minimumSteps = new int[n + 1];

            for (int current = 2; current <= n; current++)
            {
                minimumSteps[current] = int.MaxValue;

                // 因數會成對出現，只需枚舉到平方根便能評估兩種因數轉移方向。
                for (int factor = 1; factor <= current / factor; factor++)
                {
                    if (current % factor == 0)
                    {
                        int complementaryFactor = current / factor;

                        // 先完成其中一個因數，再複製一次並貼上其餘倍數。
                        minimumSteps[current] = Math.Min(
                            minimumSteps[current],
                            minimumSteps[factor] + complementaryFactor);
                        minimumSteps[current] = Math.Min(
                            minimumSteps[current],
                            minimumSteps[complementaryFactor] + factor);
                    }
                }
            }

            return minimumSteps[n];
        }

        /// <summary>
        /// 使用自頂向下遞迴與記憶化，計算畫面恰好得到 <paramref name="n"/> 個 A 的最少操作數。
        /// 每次把目標拆成一對因數，遞迴求出先完成其中一個因數的成本，再加上複製與貼上成
        /// 另一個倍數的成本。輸入須符合題目限制 1 到 1000，回傳最少按鍵次數；
        /// 最寬鬆時間上界為 O(n√n)，記憶表與遞迴堆疊使用 O(n) 空間。
        /// </summary>
        /// <param name="n">要在畫面上得到的 A 數量，範圍為 1 到 1000。</param>
        /// <returns>從初始的一個 A 出發，得到恰好 <paramref name="n"/> 個 A 的最少操作數。</returns>
        public static int MinSteps2(int n)
        {
            Dictionary<int, int> memo = new()
            {
                [1] = 0
            };

            return MinStepsMemo(n, memo);
        }

        /// <summary>
        /// 遞迴計算指定目標的最少操作數，並重用已求得的有效子問題答案。
        /// 若目標沒有非平凡因數，直接以一次 Copy All 加上逐次 Paste 的目標值作為答案。
        /// </summary>
        /// <param name="target">目前要計算的 A 數量，範圍為 1 到 1000。</param>
        /// <param name="memo">保存已計算目標與其最少操作數的記憶表。</param>
        /// <returns>得到恰好 <paramref name="target"/> 個 A 的最少操作數。</returns>
        private static int MinStepsMemo(int target, Dictionary<int, int> memo)
        {
            if (memo.TryGetValue(target, out int cachedSteps))
            {
                return cachedSteps;
            }

            // 質數只能從一個 A 複製後連續貼上，因此 target 也是安全的初始上界。
            int minimumSteps = target;

            for (int factor = 2; factor <= target / factor; factor++)
            {
                if (target % factor != 0)
                {
                    continue;
                }

                int complementaryFactor = target / factor;

                // 同時評估先完成 factor 或 complementaryFactor，避免遺漏較佳的拆分方向。
                minimumSteps = Math.Min(
                    minimumSteps,
                    MinStepsMemo(factor, memo) + complementaryFactor);
                minimumSteps = Math.Min(
                    minimumSteps,
                    MinStepsMemo(complementaryFactor, memo) + factor);
            }

            memo[target] = minimumSteps;
            return minimumSteps;
        }

        /// <summary>
        /// 使用質因數分解，計算畫面恰好得到 <paramref name="n"/> 個 A 的最少操作數。
        /// 每個質因數代表一段 Copy All 加上若干次 Paste；把所有質因數相加即可得到最佳成本。
        /// 輸入須符合題目限制 1 到 1000，回傳最少按鍵次數；時間複雜度為 O(√n)，
        /// 空間複雜度為 O(1)。
        /// </summary>
        /// <param name="n">要在畫面上得到的 A 數量，範圍為 1 到 1000。</param>
        /// <returns>從初始的一個 A 出發，得到恰好 <paramref name="n"/> 個 A 的最少操作數。</returns>
        public static int MinSteps3(int n)
        {
            int remaining = n;
            int minimumSteps = 0;

            for (int factor = 2; factor <= remaining / factor; factor++)
            {
                // 重複拆出同一質因數；每拆一次就加入該操作分組的成本。
                while (remaining % factor == 0)
                {
                    minimumSteps += factor;
                    remaining /= factor;
                }
            }

            if (remaining > 1)
            {
                minimumSteps += remaining;
            }

            return minimumSteps;
        }

        private sealed record TestCase(string Name, int N, int Expected);
    }
}
