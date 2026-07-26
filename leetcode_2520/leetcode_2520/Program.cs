namespace leetcode_2520;

class Program
{
    /// <summary>
    /// 2520. Count the Digits That Divide a Number
    /// https://leetcode.com/problems/count-the-digits-that-divide-a-number/description/
    /// 2520. 统计能整除数字的位数
    /// https://leetcode.cn/problems/count-the-digits-that-divide-a-number/description/
    ///
    /// Given an integer num, return the number of digits in num that divide num.
    ///
    /// An integer val divides nums if nums % val == 0.
    ///
    /// 給定一個整數 num，回傳 num 中能整除 num 的數字數量。
    ///
    /// 若 nums % val == 0，則整數 val 可以整除 nums。
    /// </summary>
    /// <remarks>
    /// 建立解題物件並執行固定測試案例，逐一顯示預期值、實際值與驗證結果，
    /// 最後輸出通過案例數。測試資料皆符合題目限制。
    /// </remarks>
    /// <param name="args">命令列參數；此範例不使用任何參數。</param>
    static void Main(string[] args)
    {
        Program solution = new();
        (int Num, int Expected)[] testCases =
        [
            (7, 1),
            (121, 2),
            (1248, 4),
            (9, 1),
            (999999999, 9)
        ];

        int passed = 0;

        Console.WriteLine("LeetCode 2520 - Count the Digits That Divide a Number");

        foreach ((int num, int expected) in testCases)
        {
            if (RunTestCase(solution, num, expected))
            {
                passed++;
            }
        }

        Console.WriteLine();
        Console.WriteLine($"{passed}/{testCases.Length} passed.");
    }

    /// <summary>
    /// 計算整數中可以整除該整數的數字個數。
    /// 解法使用逐位模擬：保留原始數字作為被除數，再以取餘數和整數除法
    /// 從最低位依序取出每個數字；若該數字能整除原始數字，就累加一次。
    /// 相同數字出現多次時，每一個位置都會分別計數。
    /// </summary>
    /// <param name="num">
    /// 要檢查的正整數。依題目限制，範圍為 1 到 10<sup>9</sup>，
    /// 且十進位表示中不包含數字 0。
    /// </param>
    /// <returns><paramref name="num"/> 中能整除原始數字的數字個數。</returns>
    public int CountDigits(int num)
    {
        // temp 用來逐位取值，num 保留為每次整除判斷的原始被除數。
        int temp = num;
        int count = 0;

        while (temp != 0)
        {
            int digit = temp % 10;

            // 每個位置都獨立判斷，因此重複出現且可整除的數字會重複計數。
            if (num % digit == 0)
            {
                count++;
            }

            // 移除已處理的最低位，繼續檢查下一個數字。
            temp /= 10;
        }

        return count;
    }

    /// <summary>
    /// 執行一筆固定測試案例，呼叫指定解題物件計算答案，
    /// 比較實際值與預期值，並將輸入、結果及 PASS/FAIL 狀態輸出至主控台。
    /// </summary>
    /// <param name="solution">提供 <see cref="CountDigits(int)"/> 解法的物件。</param>
    /// <param name="num">
    /// 測試用正整數；必須介於 1 到 10<sup>9</sup>，且十進位表示不包含數字 0。
    /// </param>
    /// <param name="expected">此測試案例預期得到的可整除數字個數。</param>
    /// <returns>實際結果與預期結果相同時為 <see langword="true"/>，否則為 <see langword="false"/>。</returns>
    private static bool RunTestCase(Program solution, int num, int expected)
    {
        int actual = solution.CountDigits(num);
        bool passed = actual == expected;

        Console.WriteLine(
            $"[{(passed ? "PASS" : "FAIL")}] num = {num}, expected = {expected}, actual = {actual}");

        return passed;
    }
}