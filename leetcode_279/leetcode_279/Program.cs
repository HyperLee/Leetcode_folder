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
}
