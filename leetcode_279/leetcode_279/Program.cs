namespace leetcode_279;

class Program
{
    /// <summary>
    /// 279. Perfect Squares
    /// https://leetcode.com/problems/perfect-squares/
    ///
    /// 題目描述（繁體中文）：這是題目描述
    /// 給定一個整數 n，回傳組成 n 所需的最少完全平方數數量。
    /// 完全平方數是某個整數的平方，例如 1、4、9、16；3 和 11 則不是。
    ///
    /// 279. 完全平方數
    /// https://leetcode.cn/problems/perfect-squares/description/
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var program = new Program();

        // 測試案例 1：n = 12，預期結果為 3（12 = 4 + 4 + 4）
        int n1 = 12;
        Console.WriteLine($"n = {n1}, 最少完全平方數數量 = {program.NumSquares(n1)}"); // 輸出: 3

        // 測試案例 2：n = 13，預期結果為 2（13 = 4 + 9）
        int n2 = 13;
        Console.WriteLine($"n = {n2}, 最少完全平方數數量 = {program.NumSquares(n2)}"); // 輸出: 2

        // 測試案例 3：n = 1，預期結果為 1（1 = 1）
        int n3 = 1;
        Console.WriteLine($"n = {n3}, 最少完全平方數數量 = {program.NumSquares(n3)}"); // 輸出: 1

        // 測試案例 4：n = 4，預期結果為 1（4 = 4）
        int n4 = 4;
        Console.WriteLine($"n = {n4}, 最少完全平方數數量 = {program.NumSquares(n4)}"); // 輸出: 1

        // 測試案例 5：n = 7，預期結果為 4（7 = 1 + 1 + 1 + 4）
        int n5 = 7;
        Console.WriteLine($"n = {n5}, 最少完全平方數數量 = {program.NumSquares(n5)}"); // 輸出: 4

        Console.WriteLine();
        Console.WriteLine("=== 方法二：數學解法（四平方和定理）===");

        // 使用 NumSquares2 驗證相同測試案例
        Console.WriteLine($"n = {n1}, 最少完全平方數數量 = {program.NumSquares2(n1)}"); // 輸出: 3
        Console.WriteLine($"n = {n2}, 最少完全平方數數量 = {program.NumSquares2(n2)}"); // 輸出: 2
        Console.WriteLine($"n = {n3}, 最少完全平方數數量 = {program.NumSquares2(n3)}"); // 輸出: 1
        Console.WriteLine($"n = {n4}, 最少完全平方數數量 = {program.NumSquares2(n4)}"); // 輸出: 1
        Console.WriteLine($"n = {n5}, 最少完全平方數數量 = {program.NumSquares2(n5)}"); // 輸出: 4
    }

    /// <summary>
    /// 使用動態規劃求解「完全平方數」問題。
    /// <para>
    /// <b>解題思路：</b>
    /// </para>
    /// <para>
    /// 定義狀態：f[i] 表示組成整數 i 所需的最少完全平方數數量。
    /// </para>
    /// <para>
    /// 狀態轉移方程：f[i] = 1 + min(f[i - j²])，其中 1 ≤ j ≤ √i
    /// </para>
    /// <para>
    /// 邊界條件：f[0] = 0（表示組成 0 不需要任何完全平方數）
    /// </para>
    /// <para>
    /// <b>時間複雜度：</b> O(n × √n)，外層迴圈 n 次，內層迴圈最多 √n 次。
    /// </para>
    /// <para>
    /// <b>空間複雜度：</b> O(n)，需要一個長度為 n+1 的陣列來儲存狀態。
    /// </para>
    /// </summary>
    /// <param name="n">目標整數，需要被分解為完全平方數之和</param>
    /// <returns>組成 n 所需的最少完全平方數數量</returns>
    /// <example>
    /// <code>
    /// var solution = new Program();
    /// int result = solution.NumSquares(12); // 回傳 3，因為 12 = 4 + 4 + 4
    /// </code>
    /// </example>
    public int NumSquares(int n)
    {
        // 建立 DP 陣列，f[i] 表示組成 i 所需的最少完全平方數數量
        // f[0] = 0 為邊界條件，表示組成 0 不需要任何完全平方數
        int[] f = new int[n + 1];

        // 從 1 到 n 依序計算每個數字的最少完全平方數數量
        for (int i = 1; i <= n; i++)
        {
            // 初始化為最大值，用於後續取最小值
            int min = int.MaxValue;

            // 枚舉所有可能的完全平方數 j²，其中 j² ≤ i
            // j 從 1 開始，直到 j² > i 為止
            for (int j = 1; j * j <= i; j++)
            {
                // 狀態轉移：選擇 j² 後，剩餘部分為 i - j²
                // 取所有可能選擇中的最小值
                min = Math.Min(min, f[i - j * j]);
            }

            // 當前數字的最少完全平方數數量 = 子問題最小值 + 1（加上選擇的那個完全平方數）
            f[i] = min + 1;
        }

        // 回傳組成 n 所需的最少完全平方數數量
        return f[n];
    }

    /// <summary>
    /// 使用數學方法（四平方和定理）求解「完全平方數」問題。
    /// <para>
    /// <b>解題思路：</b>
    /// </para>
    /// <para>
    /// 根據「四平方和定理」（Lagrange's four-square theorem），任意正整數都可以表示為至多四個正整數的平方和。
    /// </para>
    /// <para>
    /// 更進一步，根據「三平方和定理」，當且僅當 n = 4^k × (8m + 7) 時，n 無法表示為三個平方數之和，
    /// 此時答案必為 4。
    /// </para>
    /// <para>
    /// 排除答案為 4 的情況後，依序檢查答案是否為 1（n 本身是完全平方數）或 2（n 可表示為兩個平方數之和），
    /// 若都不是則答案為 3。
    /// </para>
    /// <para>
    /// <b>時間複雜度：</b> O(√n)，主要耗費在枚舉檢查答案是否為 2。
    /// </para>
    /// <para>
    /// <b>空間複雜度：</b> O(1)，僅使用常數空間。
    /// </para>
    /// </summary>
    /// <param name="n">目標整數，需要被分解為完全平方數之和</param>
    /// <returns>組成 n 所需的最少完全平方數數量</returns>
    /// <example>
    /// <code>
    /// var solution = new Program();
    /// int result = solution.NumSquares2(12); // 回傳 3，因為 12 = 4 + 4 + 4
    /// int result2 = solution.NumSquares2(7); // 回傳 4，因為 7 = 4^0 × (8×0 + 7)
    /// </code>
    /// </example>
    public int NumSquares2(int n)
    {
        // 情況 1：檢查 n 本身是否為完全平方數
        // 若是，則只需要 1 個完全平方數
        if (IsPerfectSquare(n))
        {
            return 1;
        }

        // 情況 4：根據三平方和定理，檢查 n 是否符合 4^k × (8m + 7) 的形式
        // 若符合，則 n 無法用少於 4 個平方數表示，答案必為 4
        if (CheckAnswer4(n))
        {
            return 4;
        }

        // 情況 2：檢查 n 是否可以表示為兩個平方數之和
        // 枚舉第一個平方數 i²，檢查 n - i² 是否為完全平方數
        for (int i = 1; i * i <= n; i++)
        {
            int j = n - i * i;
            if (IsPerfectSquare(j))
            {
                return 2;
            }
        }

        // 情況 3：排除以上情況後，答案必為 3
        return 3;
    }

    /// <summary>
    /// 判斷一個整數是否為完全平方數。
    /// <para>
    /// <b>解題思路：</b>
    /// </para>
    /// <para>
    /// 完全平方數是某個整數的平方，例如 1、4、9、16、25 等。
    /// </para>
    /// <para>
    /// 透過計算 x 的平方根並取整數部分，再將該整數平方後與原數比較，
    /// 若相等則 x 為完全平方數。
    /// </para>
    /// <para>
    /// <b>時間複雜度：</b> O(1)，僅需一次平方根運算。
    /// </para>
    /// <para>
    /// <b>空間複雜度：</b> O(1)，僅使用常數空間。
    /// </para>
    /// </summary>
    /// <param name="x">待判斷的整數</param>
    /// <returns>若 x 為完全平方數則回傳 true，否則回傳 false</returns>
    /// <example>
    /// <code>
    /// var solution = new Program();
    /// bool result1 = solution.IsPerfectSquare(16); // 回傳 true，因為 16 = 4²
    /// bool result2 = solution.IsPerfectSquare(14); // 回傳 false
    /// </code>
    /// </example>
    public bool IsPerfectSquare(int x)
    {
        // 計算 x 的平方根並取整數部分
        int y = (int)Math.Sqrt(x);

        // 若 y² == x，則 x 為完全平方數
        return y * y == x;
    }

    /// <summary>
    /// 判斷整數是否符合 4^k × (8m + 7) 的形式。
    /// <para>
    /// <b>解題思路：</b>
    /// </para>
    /// <para>
    /// 根據數論中的「三平方和定理」（Legendre's three-square theorem），
    /// 一個正整數 n 可以表示為三個平方數之和，當且僅當 n 不是 4^k × (8m + 7) 的形式。
    /// </para>
    /// <para>
    /// 因此，若 n 符合此形式，則 n 必須用至少 4 個平方數來表示。
    /// </para>
    /// <para>
    /// 演算法步驟：
    /// 1. 持續將 x 除以 4，直到 x 不再是 4 的倍數（消除 4^k 因子）
    /// 2. 檢查剩餘的數是否對 8 取餘等於 7（即符合 8m + 7 的形式）
    /// </para>
    /// <para>
    /// <b>時間複雜度：</b> O(log n)，最多需要 log₄(n) 次除法。
    /// </para>
    /// <para>
    /// <b>空間複雜度：</b> O(1)，僅使用常數空間。
    /// </para>
    /// </summary>
    /// <param name="x">待判斷的整數</param>
    /// <returns>若 x 符合 4^k × (8m + 7) 的形式則回傳 true，否則回傳 false</returns>
    /// <example>
    /// <code>
    /// var solution = new Program();
    /// bool result1 = solution.CheckAnswer4(7);  // 回傳 true，因為 7 = 4^0 × (8×0 + 7)
    /// bool result2 = solution.CheckAnswer4(28); // 回傳 true，因為 28 = 4^1 × 7 = 4^1 × (8×0 + 7)
    /// bool result3 = solution.CheckAnswer4(12); // 回傳 false
    /// </code>
    /// </example>
    public bool CheckAnswer4(int x)
    {
        // 持續除以 4，消除所有 4^k 因子
        while (x % 4 == 0)
        {
            x /= 4;
        }

        // 檢查剩餘的數是否符合 8m + 7 的形式
        return x % 8 == 7;
    }
}
