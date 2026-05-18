using System.Globalization;

namespace leetcode_050;

class Program
{
    private const double Tolerance = 1e-9;

    private static readonly TestCase[] TestCases =
    {
        new("Example 1", 2.0, 10, 1024.0),
        new("Example 2", 2.1, 3, 9.261),
        new("Example 3", 2.0, -2, 0.25),
        new("Zero exponent", 3.5, 0, 1.0),
        new("Identity with int.MinValue", 1.0, int.MinValue, 1.0),
        new("Negative one with int.MinValue", -1.0, int.MinValue, 1.0),
        new("Negative base odd exponent", -2.0, 5, -32.0),
        new("Negative base even exponent", -2.0, 6, 64.0)
    };

    /// <summary>
    /// English:
    /// 50. Pow(x, n)
    /// Implement pow(x, n), which calculates x raised to the power n (i.e., x^n).
    ///
    /// Example 1:
    /// Input: x = 2.00000, n = 10
    /// Output: 1024.00000
    ///
    /// Example 2:
    /// Input: x = 2.10000, n = 3
    /// Output: 9.26100
    ///
    /// Example 3:
    /// Input: x = 2.00000, n = -2
    /// Output: 0.25000
    /// Explanation: 2^-2 = 1 / 2^2 = 1 / 4 = 0.25
    ///
    /// Constraints:
    /// -100.0 &lt; x &lt; 100.0
    /// -2^31 &lt;= n &lt;= 2^31 - 1
    /// n is an integer.
    /// Either x is not zero or n &gt; 0.
    /// -10^4 &lt;= x^n &lt;= 10^4
    ///
    /// 繁體中文:
    /// 50. Pow(x, n)
    /// 實作 pow(x, n)，計算 x 的 n 次方（也就是 x^n）。
    ///
    /// 範例 1:
    /// 輸入: x = 2.00000, n = 10
    /// 輸出: 1024.00000
    ///
    /// 範例 2:
    /// 輸入: x = 2.10000, n = 3
    /// 輸出: 9.26100
    ///
    /// 範例 3:
    /// 輸入: x = 2.00000, n = -2
    /// 輸出: 0.25000
    /// 說明: 2^-2 = 1 / 2^2 = 1 / 4 = 0.25
    ///
    /// 限制條件:
    /// -100.0 < x < 100.0
    /// -2^31 <= n <= 2^31 - 1
    /// n 是整數。
    /// x 不為零，或 n &gt; 0。
    /// -10^4 <= x^n <= 10^4
    /// </summary>
    /// <param name="args">命令列參數；此示範專案未使用。</param>
    /// <returns>全部測試通過時回傳 0，否則回傳 1。</returns>
    private static int Main(string[] args)
    {
        Solution solution = new Solution();
        Solver[] solvers =
        {
            new("MyPow", solution.MyPow),
            new("MyPow2", solution.MyPow2),
            new("MyPow3", solution.MyPow3)
        };

        int passedCount = 0;
        int totalCount = solvers.Length * TestCases.Length;

        foreach (Solver solver in solvers)
        {
            Console.WriteLine($"[{solver.Name}]");

            foreach (TestCase testCase in TestCases)
            {
                if (RunCase(solver.Method, solver.Name, testCase))
                {
                    passedCount++;
                }
            }

            Console.WriteLine();
        }

        int failedCount = totalCount - passedCount;
        Console.WriteLine($"Summary: {passedCount}/{totalCount} passed, {failedCount} failed.");
        return failedCount == 0 ? 0 : 1;
    }

    /// <summary>
    /// 用途: 執行單一測試案例並列印 PASS 或 FAIL。
    /// 解題概念: 以統一的驗證入口比較期望值與實際值，並攔截例外讓所有解法都能完整跑完。
    /// 輸入條件: solver 必須是可呼叫的次方函式，testCase 需提供合法的 x、n 與 expected。
    /// 輸出結果: 回傳此案例是否通過，同時輸出可讀的驗證結果到主控台。
    /// </summary>
    /// <param name="solver">要驗證的次方函式。</param>
    /// <param name="solverName">目前輸出的解法名稱。</param>
    /// <param name="testCase">單筆測試資料。</param>
    /// <returns>案例通過時回傳 true，否則回傳 false。</returns>
    private static bool RunCase(Func<double, int, double> solver, string solverName, TestCase testCase)
    {
        try
        {
            double actual = solver(testCase.X, testCase.N);
            bool passed = AreClose(actual, testCase.Expected);

            Console.WriteLine(
                $"  {testCase.Name}: x = {FormatDouble(testCase.X)}, n = {testCase.N}, expected = {FormatDouble(testCase.Expected)}, actual = {FormatDouble(actual)}, {(passed ? "PASS" : "FAIL")}");

            return passed;
        }
        catch (Exception exception)
        {
            Console.WriteLine(
                $"  {testCase.Name}: x = {FormatDouble(testCase.X)}, n = {testCase.N}, expected = {FormatDouble(testCase.Expected)}, {solverName} threw {exception.GetType().Name}, FAIL");

            return false;
        }
    }

    /// <summary>
    /// 用途: 判斷兩個浮點數結果是否足夠接近。
    /// 解題概念: 次方計算會受到浮點表示誤差影響，因此以固定容許誤差比較結果，而不是直接使用相等判斷。
    /// 輸入條件: actual 與 expected 為同一測試案例下要比較的兩個 double 值。
    /// 輸出結果: 若誤差在 Tolerance 以內則回傳 true，否則回傳 false。
    /// </summary>
    /// <param name="actual">實際計算結果。</param>
    /// <param name="expected">期望結果。</param>
    /// <returns>兩者足夠接近時回傳 true，否則回傳 false。</returns>
    private static bool AreClose(double actual, double expected)
    {
        return Math.Abs(actual - expected) <= Tolerance;
    }

    /// <summary>
    /// 用途: 將 double 轉成穩定且易讀的字串格式。
    /// 解題概念: 使用固定文化設定與有限小數位，避免不同地區格式或過長尾數干擾測試輸出閱讀。
    /// 輸入條件: value 可為任意 double。
    /// 輸出結果: 回傳格式化後的字串。
    /// </summary>
    /// <param name="value">要顯示的數值。</param>
    /// <returns>格式化後的字串。</returns>
    private static string FormatDouble(double value)
    {
        return value.ToString("0.###############", CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// 用途: 封裝單筆測試資料。
    /// 解題概念: 把案例名稱、輸入與期望值集中管理，方便主程式逐筆驗證。
    /// 輸入條件: Name、X、N、Expected 必須對應同一筆可驗證案例。
    /// 輸出結果: 建立可供測試 harness 使用的不可變資料。
    /// </summary>
    private readonly record struct TestCase(string Name, double X, int N, double Expected);

    /// <summary>
    /// 用途: 封裝解法名稱與對應函式。
    /// 解題概念: 以統一介面讓同一組測試資料可重複驗證多個解法。
    /// 輸入條件: Name 與 Method 需正確對應。
    /// 輸出結果: 建立可供主程式迭代執行的解法描述。
    /// </summary>
    private readonly record struct Solver(string Name, Func<double, int, double> Method);
}

/// <summary>
/// 提供三種計算 Pow(x, n) 的示範解法，分別展示迭代與遞迴形式的快速冪。
/// </summary>
public class Solution
{
    /// <summary>
    /// 用途: 使用迭代式快速冪計算 x 的 n 次方。
    /// 解題概念: 先把負指數轉成倒數，再利用位元判斷奇偶與因子平方，將時間複雜度從 O(n) 降到 O(log n)。
    /// 輸入條件: x 與 n 需符合題目限制；若 x 為 0，則 n 必須大於 0。
    /// 輸出結果: 回傳 x^n 的 double 結果。
    /// </summary>
    /// <param name="x">底數。</param>
    /// <param name="n">指數。</param>
    /// <returns>x 的 n 次方結果。</returns>
    public double MyPow(double x, int n)
    {
        long exponent = n;
        double result = 1.0;
        double factor = x;

        if (exponent < 0)
        {
            // 先把 x^-n 改寫成 (1 / x)^n，並升級到 long 避免 int.MinValue 取負時溢位。
            factor = 1.0 / factor;
            exponent = -exponent;
        }

        while (exponent > 0)
        {
            if ((exponent & 1) == 1)
            {
                // 當前位元為 1，代表這一層的因子需要乘進答案。
                result *= factor;
            }

            // 每往上一層就把因子平方，同時把指數右移一位。
            factor *= factor;
            exponent >>= 1;
        }

        return result;
    }

    /// <summary>
    /// 用途: 使用遞迴 helper 的快速冪計算 x 的 n 次方。
    /// 解題概念: 先把負指數正規化成 long 的正整數，再透過遞迴只計算一半次方，最後依奇偶決定是否多乘一次 x。
    /// 輸入條件: x 與 n 需符合題目限制；若 x 為 0，則 n 必須大於 0。
    /// 輸出結果: 回傳 x^n 的 double 結果。
    /// </summary>
    /// <param name="x">底數。</param>
    /// <param name="n">指數。</param>
    /// <returns>x 的 n 次方結果。</returns>
    public double MyPow2(double x, int n)
    {
        if (n == 0)
        {
            return 1.0;
        }

        long exponent = n;

        if (exponent < 0)
        {
            return 1.0 / PowRecursive(x, -exponent);
        }

        return PowRecursive(x, exponent);
    }

    /// <summary>
    /// 用途: 作為 MyPow2 的遞迴核心，計算非負指數的快速冪。
    /// 解題概念: 每次遞迴都只求一次 x^(n/2)，再用平方組合答案；若指數是奇數，再額外補乘一次 x。
    /// 輸入條件: exponent 必須大於或等於 0。
    /// 輸出結果: 回傳 x^exponent 的 double 結果。
    /// </summary>
    /// <param name="x">底數。</param>
    /// <param name="exponent">非負整數指數。</param>
    /// <returns>x 的 exponent 次方結果。</returns>
    private static double PowRecursive(double x, long exponent)
    {
        if (exponent == 0)
        {
            return 1.0;
        }

        if (exponent == 1)
        {
            return x;
        }

        // 先解較小的子問題，再把結果平方重組回來。
        double half = PowRecursive(x, exponent / 2);
        double squared = half * half;

        if ((exponent & 1) == 0)
        {
            return squared;
        }

        return squared * x;
    }

    /// <summary>
    /// 用途: 使用自我遞迴的分治快速冪計算 x 的 n 次方。
    /// 解題概念: 直接遞迴求出 x^(n/2)，再依 n 的奇偶與正負決定補乘 x 或補除 x，保留分治寫法的簡潔性。
    /// 輸入條件: x 與 n 需符合題目限制；若 x 為 0，則 n 必須大於 0。
    /// 輸出結果: 回傳 x^n 的 double 結果。
    /// </summary>
    /// <param name="x">底數。</param>
    /// <param name="n">指數。</param>
    /// <returns>x 的 n 次方結果。</returns>
    public double MyPow3(double x, int n)
    {
        if (n == 0)
        {
            return 1.0;
        }

        // 先遞迴求出 x^(n / 2)，再用同一份子結果重建完整答案。
        double half = MyPow3(x, n / 2);
        double squared = half * half;

        if ((n & 1) == 0)
        {
            return squared;
        }

        return n > 0 ? squared * x : squared / x;
    }
}
