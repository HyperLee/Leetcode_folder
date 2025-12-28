namespace leetcode_1342;

class Program
{
    /// <summary>
    /// 1342. Number of Steps to Reduce a Number to Zero
    /// https://leetcode.com/problems/number-of-steps-to-reduce-a-number-to-zero/description/
    /// 1342. 將數字變成0 的操作次數
    /// https://leetcode.cn/problems/number-of-steps-to-reduce-a-number-to-zero/description/
    /// 
    /// 題目描述（繁體中文）:
    /// 給定一個整數 num，回傳將其變為 0 所需的步數。
    /// 每一步：若目前數字為偶數，則除以 2；否則，減 1。
    /// 
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();

        int[] testCases = [14, 8, 123, 0];
        int[] expected = [6, 4, 12, 0];

        Console.WriteLine("=== 解法一：迴圈模擬 ===");
        for (int i = 0; i < testCases.Length; i++)
        {
            int result = solution.NumberOfSteps(testCases[i]);
            Console.WriteLine($"輸入: {testCases[i]}, 步數: {result}, 預期: {expected[i]}, 結果: {(result == expected[i] ? "✓" : "✗")}");
        }

        Console.WriteLine();
        Console.WriteLine("=== 解法二：數學（二進位性質） ===");
        for (int i = 0; i < testCases.Length; i++)
        {
            int result = solution.NumberOfStepsV2(testCases[i]);
            Console.WriteLine($"輸入: {testCases[i]}, 步數: {result}, 預期: {expected[i]}, 結果: {(result == expected[i] ? "✓" : "✗")}");
        }
    }

    /// <summary>
    /// 計算將數字減少到零所需的步數。
    /// 
    /// <para><b>解題思路：</b></para>
    /// <para>
    /// 此題採用迴圈模擬的方式，根據題目規則逐步將數字減少至零：
    /// <list type="bullet">
    ///   <item>若數字為偶數：除以 2</item>
    ///   <item>若數字為奇數：減 1</item>
    /// </list>
    /// </para>
    /// 
    /// <para><b>時間複雜度：</b>O(log n)，因為偶數時除以 2 會快速減少數值</para>
    /// <para><b>空間複雜度：</b>O(1)，只使用常數額外空間</para>
    /// </summary>
    /// <param name="num">要減少到零的非負整數</param>
    /// <returns>將 num 減少到零所需的步數</returns>
    /// <example>
    /// <code>
    /// var solution = new Program();
    /// int result = solution.NumberOfSteps(14); // 回傳 6
    ///  執行過程: 14 → 7 → 6 → 3 → 2 → 1 → 0
    /// </code>
    /// </example>
    public int NumberOfSteps(int num)
    {
        // 初始化步數計數器
        int steps = 0;

        // 當數字大於 0 時持續執行
        while (num > 0)
        {
            // 判斷數字是偶數還是奇數
            if (num % 2 == 0)
            {
                // 偶數：除以 2
                num /= 2;
            }
            else
            {
                // 奇數：減 1
                num -= 1;
            }

            // 每執行一次操作，步數加 1
            steps++;
        }

        return steps;
    }

    /// <summary>
    /// 解法二：數學（二進位性質）
    /// 
    /// <para><b>解題思路：</b></para>
    /// <para>
    /// 從二進位角度分析，兩種操作的影響：
    /// <list type="bullet">
    ///   <item>偶數除以 2：相當於二進位整體右移一位</item>
    ///   <item>奇數減 1：相當於消減最低位的 1</item>
    /// </list>
    /// </para>
    /// 
    /// <para>
    /// 總操作次數 = 右移次數 + 消減次數：
    /// <list type="bullet">
    ///   <item>右移次數：num 中最高位 1 所在的位置</item>
    ///   <item>消減次數：num 中 1 的個數</item>
    /// </list>
    /// 由於最後一步右移和消減同時完成，需減 1。
    /// </para>
    /// 
    /// <para><b>時間複雜度：</b>O(log n)，遍歷二進位位數</para>
    /// <para><b>空間複雜度：</b>O(1)，只使用常數額外空間</para>
    /// </summary>
    /// <param name="num">要減少到零的非負整數</param>
    /// <returns>將 num 減少到零所需的步數</returns>
    /// <example>
    /// <code>
    /// var solution = new Program();
    /// int result = solution.NumberOfStepsV2(14); // 回傳 6
    ///  14 的二進位為 1110，最高位在第 4 位，有 3 個 1
    ///  步數 = 4 + 3 - 1 = 6
    /// </code>
    /// </example>
    public int NumberOfStepsV2(int num)
    {
        return Math.Max(GetHighestBitPosition(num) + GetBitCount(num) - 1, 0);
    }

    /// <summary>
    /// 取得數字二進位表示中最高位 1 的位置（從 1 開始計數）。
    /// </summary>
    /// <param name="x">要檢查的數字</param>
    /// <returns>最高位 1 的位置，若為 0 則回傳 0</returns>
    private static int GetHighestBitPosition(int x)
    {
        for (int i = 31; i >= 0; i--)
        {
            if (((x >> i) & 1) == 1)
            {
                return i + 1;
            }
        }

        return 0;
    }

    /// <summary>
    /// 計算數字二進位表示中 1 的個數（使用 lowbit 技巧）。
    /// </summary>
    /// <param name="x">要計算的數字</param>
    /// <returns>二進位中 1 的個數</returns>
    private static int GetBitCount(int x)
    {
        int count = 0;

        while (x != 0)
        {
            // lowbit: x & -x 取得最低位的 1
            x -= x & -x;
            count++;
        }

        return count;
    }
}
