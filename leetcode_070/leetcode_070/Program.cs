namespace leetcode_070
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 70. Climbing Stairs
        /// https://leetcode.com/problems/climbing-stairs/
        ///
        /// You are climbing a staircase. It takes n steps to reach the top.
        /// Each time you can either climb 1 or 2 steps. In how many distinct ways can you climb to the top?
        ///
        /// Example 1:
        /// Input: n = 2
        /// Output: 2
        /// Explanation: There are two ways to climb to the top.
        /// 1. 1 step + 1 step
        /// 2. 2 steps
        ///
        /// Example 2:
        /// Input: n = 3
        /// Output: 3
        /// Explanation: There are three ways to climb to the top.
        /// 1. 1 step + 1 step + 1 step
        /// 2. 1 step + 2 steps
        /// 3. 2 steps + 1 step
        ///
        /// Constraints:
        /// 1 &lt;= n &lt;= 45
        /// </para>
        /// <para>
        /// 70. 爬樓梯
        /// https://leetcode.cn/problems/climbing-stairs/
        ///
        /// 你正在爬一座樓梯，需要走 n 階才能到達頂端。
        /// 每次你可以爬 1 階或 2 階。你有多少種不同的方法可以爬到頂端？
        ///
        /// 範例 1：
        /// 輸入：n = 2
        /// 輸出：2
        /// 解釋：有兩種方法可以爬到頂端。
        /// 1. 1 階 + 1 階
        /// 2. 2 階
        ///
        /// 範例 2：
        /// 輸入：n = 3
        /// 輸出：3
        /// 解釋：有三種方法可以爬到頂端。
        /// 1. 1 階 + 1 階 + 1 階
        /// 2. 1 階 + 2 階
        /// 3. 2 階 + 1 階
        ///
        /// 限制條件：
        /// 1 &lt;= n &lt;= 45
        /// </para>
        /// </summary>
        /// <param name="args">命令列參數；本範例不使用此參數。</param>
        static void Main(string[] args)
        {
            (int N, int Expected)[] testCases =
            [
                (1, 1),
                (2, 2),
                (3, 3),
                (5, 8),
                (10, 89)
            ];

            int passed = 0;
            const int solutionCount = 3;

            Console.WriteLine("LeetCode 70 - Climbing Stairs");

            foreach ((int n, int expected) in testCases)
            {
                passed += RunTestCase(n, expected);
            }

            int total = testCases.Length * solutionCount;
            Console.WriteLine($"Overall: {passed}/{total} passed.");
        }

        /// <summary>
        /// 執行單一階梯數的完整驗證。此方法用相同輸入呼叫三種爬樓梯解法，
        /// 將各解法結果與預期答案比較並輸出 PASS/FAIL；輸入必須符合
        /// <c>1 &lt;= n &lt;= 45</c>，回傳本案例通過驗證的解法數量（0 到 3）。
        /// </summary>
        /// <param name="n">到達頂端所需的階梯數，範圍為 1 到 45。</param>
        /// <param name="expected">此階梯數的預期走法總數。</param>
        /// <returns>三種解法中結果符合預期值的數量。</returns>
        private static int RunTestCase(int n, int expected)
        {
            (string Name, int Actual)[] results =
            [
                (nameof(ClimbStairs), ClimbStairs(n)),
                (nameof(ClimbStairs2), ClimbStairs2(n)),
                (nameof(ClimbStairs3), ClimbStairs3(n))
            ];

            int passed = 0;
            Console.WriteLine($"n={n}, expected={expected}");

            foreach ((string name, int actual) in results)
            {
                bool isPassed = actual == expected;
                Console.WriteLine($"  {name}: actual={actual}, {(isPassed ? "PASS" : "FAIL")}");

                if (isPassed)
                {
                    passed++;
                }
            }

            return passed;
        }

        /// <summary>
        /// https://leetcode.com/problems/climbing-stairs/
        /// LeetCode 70. Climbing Stairs
        /// https://zh.wikipedia.org/wiki/%E6%96%90%E6%B3%A2%E9%82%A3%E5%A5%91%E6%95%B0
        /// 1:  1
        /// 2: 1+1, 2
        /// 3: 1+1+1, 1+2, 2+1
        /// 4: 1+1+1+1, 1+2+1, 1+1+2, 2+1+1, 2+2
        /// 5: 1+1+1+1+1, 1+1+1+2, 1+1+2+1, 1+2+1+1, 2+1+1+1, 2+2+1, 2+1+2, 1+2+2
        /// 6: 1+1+1+1+1+1, 1+1+1+1+2, 1+1+1+2+1, 1+1+2+1+1, 1+2+1+1+1, 2+1+1+1+1, 1+1+2+2, ...
        /// 類似費式數列
        /// f(n-1) + f(n-2)
        /// 為了減少複雜度
        /// 找出 公式 直接利用
        /// 黃金比例恆等式解法
        /// 黃金比例恆等式解法
        /// 黃金比例恆等式解法
        /// 因此得到 F_n的一般式：
        ///
        /// https://leetcode.cn/problems/climbing-stairs/solution/pa-lou-ti-by-leetcode-solution/
        /// 套公式
        ///
        /// 使用費波那契數列的黃金比例一般式直接計算 F(n+1)。輸入為 1 到 45
        /// 的階梯數，輸出每次可走 1 或 2 階時到達頂端的不同走法總數。
        /// </summary>
        /// <param name="n">到達頂端所需的階梯數，範圍為 1 到 45。</param>
        /// <returns>到達第 <paramref name="n"/> 階的不同走法總數。</returns>
        public static int ClimbStairs(int n)
        {
            // 爬 n 階對應 F(n+1)，以黃金比例及其共軛項直接計算該數值。
            double a1 = 1 / Math.Sqrt(5);
            double b2 = Math.Pow((1 + Math.Sqrt(5)) / 2, n + 1);
            double c3 = Math.Pow((1 - Math.Sqrt(5)) / 2, n + 1);

            // 題目範圍內的結果為整數；轉型移除浮點運算可能留下的小數部分。
            int fx = (int)(a1 * (b2 - c3));
            return fx;
        }


        /// <summary>
        /// 遞迴
        /// 當輸入很大時候, 要跑很久
        ///
        /// 以最後一步來自第 n-1 階或第 n-2 階建立純遞迴關係。輸入為 1 到 45
        /// 的階梯數，輸出所有不同走法的總數；此版本會重複計算相同子問題。
        /// </summary>
        /// <param name="n">到達頂端所需的階梯數，範圍為 1 到 45。</param>
        /// <returns>到達第 <paramref name="n"/> 階的不同走法總數。</returns>
        public static int ClimbStairs2(int n)
        {
            // 一階只有 1 種走法，二階有 2 種走法，作為遞迴終止條件。
            if (n <= 2)
            {
                return n;
            }

            // 最後走 1 階或 2 階的情況互斥，因此將兩個子問題的結果相加。
            return ClimbStairs2(n - 1) + ClimbStairs2(n - 2);
        }


        /// <summary>
        /// 方法3
        ///
        /// 為方法2 遞迴方法 優化
        /// 在 n 非常大時候 比較明顯
        /// 也比單純公式推理簡單
        ///
        /// 此方法原先用來解 fibonacci
        /// 也可以用來這題目使用
        /// 類似情境
        ///
        /// 要小心迴圈計算開始位置
        /// i 從 2 開始, 如果從 3 開始就會計算不到 3 的答案了
        /// 迴圈是從前一個開始計算
        ///
        /// n 範圍: [1, n], 沒有 0
        ///
        /// 以兩個變數保存相鄰階梯的答案，逐階套用 f(n)=f(n-1)+f(n-2)，
        /// 避免純遞迴的重複子問題。輸入為 1 到 45 的階梯數，輸出所有不同
        /// 走法的總數，並只使用固定數量的額外空間。
        /// </summary>
        /// <param name="n">到達頂端所需的階梯數，範圍為 1 到 45。</param>
        /// <returns>到達第 <paramref name="n"/> 階的不同走法總數。</returns>
        public static int ClimbStairs3(int n)
        {
            // 一階與二階是迭代遞推所需的兩個初始答案。
            if (n <= 2)
            {
                return n;
            }

            int result = 0;
            int pre = 1;
            int next = 2;

            // 每輪先算下一階，再同步前移兩個狀態，避免覆蓋仍需使用的舊值。
            for (int i = 2; i < n; i++)
            {
                result = pre + next;
                pre = next;
                next = result;
            }

            return result;
        }
    }
}
