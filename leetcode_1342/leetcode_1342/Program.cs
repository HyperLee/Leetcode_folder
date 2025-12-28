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

        // 測試資料 1: num = 14
        // 14 → 7 → 6 → 3 → 2 → 1 → 0 (共 6 步)
        int num1 = 14;
        Console.WriteLine($"輸入: {num1}, 步數: {solution.NumberOfSteps(num1)}"); // 預期輸出: 6

        // 測試資料 2: num = 8
        // 8 → 4 → 2 → 1 → 0 (共 4 步)
        int num2 = 8;
        Console.WriteLine($"輸入: {num2}, 步數: {solution.NumberOfSteps(num2)}"); // 預期輸出: 4

        // 測試資料 3: num = 123
        // 123 → 122 → 61 → 60 → 30 → 15 → 14 → 7 → 6 → 3 → 2 → 1 → 0 (共 12 步)
        int num3 = 123;
        Console.WriteLine($"輸入: {num3}, 步數: {solution.NumberOfSteps(num3)}"); // 預期輸出: 12

        // 測試資料 4: num = 0 (邊界條件)
        int num4 = 0;
        Console.WriteLine($"輸入: {num4}, 步數: {solution.NumberOfSteps(num4)}"); // 預期輸出: 0
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
}
