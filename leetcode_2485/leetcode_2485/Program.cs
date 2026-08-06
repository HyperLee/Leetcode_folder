namespace leetcode_2485
{
    internal class Program
    {
        /// <summary>
        /// 2485. Find the Pivot Integer
        /// https://leetcode.com/problems/find-the-pivot-integer/description/?envType=daily-question&envId=2024-03-13
        /// 2485. 找出中枢整数
        /// https://leetcode.cn/problems/find-the-pivot-integer/description/
        /// </summary>
        /// <remarks>
        /// 入口會執行固定案例，逐一比較三種解法，並在任一案例失敗時回傳非零結束碼。
        /// </remarks>
        /// <param name="args">保留主控台程式的標準命令列參數，目前不使用。</param>
        /// <returns>所有測試通過時回傳 0，否則回傳 1。</returns>
        static int Main(string[] args)
        {
            return RunSamples();
        }



        /// <summary>
        /// 題目意思是
        /// 前x個總和 要與 x之後到n 之間的總和 要相等
        /// 前x 與 後 n - x 個 總和相同
        /// 
        /// 本方法採用
        /// 利用雙指標來計算
        /// 找出 x 在哪裡
        /// 
        /// n範圍: [1, n]
        /// 
        /// 有其他解法 利用等差公式 數學算法
        /// https://leetcode.cn/problems/find-the-pivot-integer/solutions/2306030/zhao-chu-zhong-shu-zheng-shu-by-leetcode-t7gn/
        /// https://leetcode.cn/problems/find-the-pivot-integer/solutions/1993442/o1-zuo-fa-by-endlesscheng-571j/
        /// https://leetcode.cn/problems/find-the-pivot-integer/solutions/2602536/2485-zhao-chu-zhong-shu-zheng-shu-by-sto-7aja/
        /// </summary>
        /// <remarks>
        /// 這個版本保留逐一嘗試候選值的直觀流程，適合作為其他最佳化解法的正確性基準。
        /// </remarks>
        /// <param name="n">正整數上限，題目條件為 1 <= n <= 1000。</param>
        /// <returns>找到符合條件的中樞整數時回傳該值，否則回傳 -1。</returns>
        public static int PivotInteger(int n)
        {
            for (int x = 1; x <= n; x++)
            {
                long leftSum = 0;
                long rightSum = 0;

                // 將中樞值 x 同時包含在左右兩側，直接對照題目定義的兩個區間。
                for (int i = 1; i <= x; i++)
                {
                    leftSum += i;
                }

                for (int i = x; i <= n; i++)
                {
                    rightSum += i;
                }

                if (leftSum == rightSum)
                {
                    return x;
                }
            }

            return -1;
        }

        /// <summary>
        /// 以一次線性掃描尋找中樞整數，使用總和與前綴和快速取得包含 x 的右側總和。
        /// </summary>
        /// <param name="n">正整數上限，題目條件為 1 <= n <= 1000。</param>
        /// <returns>找到符合條件的中樞整數時回傳該值，否則回傳 -1。</returns>
        public static int PivotIntegerLinear(int n)
        {
            long totalSum = (long)n * (n + 1) / 2;
            long leftSum = 0;

            for (int x = 1; x <= n; x++)
            {
                leftSum += x;

                // leftSum 已包含 x；扣除 1..x 後再補回 x，即得到 x..n 的總和。
                long rightSum = totalSum - leftSum + x;
                if (leftSum == rightSum)
                {
                    return x;
                }
            }

            return -1;
        }

        /// <summary>
        /// 由等差級數公式推導中樞條件，直接以平方根判斷是否存在中樞整數。
        /// </summary>
        /// <param name="n">正整數上限，題目條件為 1 <= n <= 1000。</param>
        /// <returns>當 n(n + 1) / 2 是完全平方數時回傳其平方根，否則回傳 -1。</returns>
        public static int PivotIntegerByFormula(int n)
        {
            // sum(1..x) = sum(x..n) 可化簡為 x^2 = n(n + 1) / 2。
            long target = (long)n * (n + 1) / 2;
            long squareRoot = (long)Math.Sqrt(target);

            return squareRoot * squareRoot == target ? (int)squareRoot : -1;
        }

        /// <summary>
        /// 執行固定的邊界、官方範例與可解案例，統計三種解法的通過數。
        /// </summary>
        /// <returns>所有檢查通過時回傳 0，否則回傳 1。</returns>
        private static int RunSamples()
        {
            (string Name, int N, int Expected)[] cases =
            {
                ("官方範例一", 8, 6),
                ("官方範例二", 1, 1),
                ("官方範例三", 4, -1),
                ("小型無解案例", 2, -1),
                ("較大有效案例", 49, 35),
                ("上限案例", 1000, -1)
            };

            const int solutionsPerCase = 3;
            int passedCount = 0;
            int totalCount = cases.Length * solutionsPerCase;

            Console.WriteLine("=== 測試案例 ===");
            foreach ((string name, int n, int expected) in cases)
            {
                passedCount += RunSample(name, n, expected);
            }

            Console.WriteLine();
            Console.WriteLine($"總結：{passedCount}/{totalCount} 項測試通過");
            return passedCount == totalCount ? 0 : 1;
        }

        /// <summary>
        /// 執行單一輸入的三種解法，列印預期值、實際值與通過狀態。
        /// </summary>
        /// <param name="name">測試案例名稱。</param>
        /// <param name="n">要傳入各解法的正整數上限。</param>
        /// <param name="expected">該輸入的預期中樞整數。</param>
        /// <returns>此案例中通過的解法數量，範圍為 0 到 3。</returns>
        private static int RunSample(string name, int n, int expected)
        {
            Console.WriteLine();
            Console.WriteLine($"案例：{name}");
            Console.WriteLine($"輸入：n = {n}");
            Console.WriteLine($"Expected: {expected}");

            int passedCount = 0;
            if (RunSolution(nameof(PivotInteger), PivotInteger, n, expected))
            {
                passedCount++;
            }

            if (RunSolution(nameof(PivotIntegerLinear), PivotIntegerLinear, n, expected))
            {
                passedCount++;
            }

            if (RunSolution(nameof(PivotIntegerByFormula), PivotIntegerByFormula, n, expected))
            {
                passedCount++;
            }

            return passedCount;
        }

        /// <summary>
        /// 執行指定解法並將實際結果與預期結果比較。
        /// </summary>
        /// <param name="methodName">要顯示的解法名稱。</param>
        /// <param name="solver">接受 n 並回傳中樞整數的解法函式。</param>
        /// <param name="n">要傳入解法的正整數上限。</param>
        /// <param name="expected">預期的中樞整數。</param>
        /// <returns>實際結果符合預期時回傳 true，否則回傳 false。</returns>
        private static bool RunSolution(string methodName, Func<int, int> solver, int n, int expected)
        {
            int actual = solver(n);
            bool passed = actual == expected;
            string result = passed ? "PASS" : "FAIL";

            Console.WriteLine($"Actual ({methodName}): {actual} - {result}");
            return passed;
        }
    }
}