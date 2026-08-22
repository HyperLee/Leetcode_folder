namespace leetcode_3622;

class Program
{
    /// <summary>
    /// LeetCode 3622: Check Divisibility by Digit Sum and Product.
    ///
    /// English:
    /// You are given a positive integer n. Determine whether n is divisible by the sum of the following two values:
    ///
    /// - The digit sum of n (the sum of its digits).
    /// - The digit product of n (the product of its digits).
    ///
    /// Return true if n is divisible by this sum; otherwise, return false.
    ///
    /// 繁體中文：
    /// 給定一個正整數 n。請判斷 n 是否能被以下兩個值的總和整除：
    ///
    /// - n 的數位總和（其各位數字的總和）。
    /// - n 的數位乘積（其各位數字的乘積）。
    ///
    /// 如果 n 能被這個總和整除，回傳 true；否則回傳 false。
    /// </summary>
    /// <param name="args">Unused command-line arguments.</param>
    /// <remarks>
    /// 此進入點使用固定測試資料同時驗證兩種解法，並以 PASS/FAIL 與程序結束碼呈現結果。
    /// The executable runs deterministic cases against both implementations and returns a non-zero
    /// exit code when any expected result does not match.
    /// </remarks>
    static void Main(string[] args)
    {
        (int Input, bool Expected, string Description)[] testCases =
        {
            (99, true, "官方範例：總和等於原數"),
            (23, false, "官方範例：無法整除"),
            (1, false, "單一數位"),
            (10, true, "含有 0，數位乘積為 0"),
            (101, false, "含有 0 且除數不整除"),
            (1_000_000, true, "限制上限")
        };

        Program solution = new Program();
        int totalChecks = 0;
        int passedChecks = 0;

        Console.WriteLine("LeetCode 3622：Check Divisibility by Digit Sum and Product");
        Console.WriteLine(new string('=', 72));

        foreach ((int Input, bool Expected, string Description) testCase in testCases)
        {
            bool simulationActual = solution.CheckDivisibility(testCase.Input);
            bool stringActual = solution.CheckDivisibilityByString(testCase.Input);
            bool simulationPassed = simulationActual == testCase.Expected;
            bool stringPassed = stringActual == testCase.Expected;

            totalChecks += 2;
            if (simulationPassed)
            {
                passedChecks++;
            }

            if (stringPassed)
            {
                passedChecks++;
            }

            Console.WriteLine($"案例：{testCase.Description}，n = {testCase.Input}");
            Console.WriteLine($"  [解法一：整數模擬] Expected = {testCase.Expected}, Actual = {simulationActual}, {(simulationPassed ? "PASS" : "FAIL")}");
            Console.WriteLine($"  [解法二：字串走訪] Expected = {testCase.Expected}, Actual = {stringActual}, {(stringPassed ? "PASS" : "FAIL")}");
        }

        Console.WriteLine(new string('-', 72));
        Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項測試通過。");
        Environment.ExitCode = passedChecks == totalChecks ? 0 : 1;
    }

    /// <summary>
    /// 解法一：使用整數運算從低位到高位逐一取出 n 的每個數字，計算數位總和與數位乘積，
    /// 最後判斷原始 n 是否能被兩者的總和整除。
    ///
    /// English:
    /// Extracts each digit with integer arithmetic, accumulates the digit sum and digit product,
    /// and checks whether the original number is divisible by their sum.
    /// The input is a positive integer from 1 through 1,000,000, and the return value is true
    /// exactly when the divisibility condition is satisfied.
    /// </summary>
    /// <param name="n">要檢查的正整數，範圍為 1 到 1,000,000。</param>
    /// <returns>若 n 能被數位總和與數位乘積的總和整除則回傳 true，否則回傳 false。</returns>
    public bool CheckDivisibility(int n)
    {
        int digitSum = 0;
        int digitProduct = 1;

        // 後續會透過除法消耗 n，因此先保存原始值供最後的整除判斷使用。
        int original = n;

        while (n > 0)
        {
            int digit = n % 10;
            n /= 10;

            // digitProduct 從 1 開始，才能正確累乘所有數位；遇到 0 時乘積自然變成 0。
            digitSum += digit;
            digitProduct *= digit;
        }

        return original % (digitSum + digitProduct) == 0;
    }

    /// <summary>
    /// 解法二：先將 n 轉成字串，再逐一走訪字元以計算數位總和與數位乘積，
    /// 最後判斷 n 是否能被兩者的總和整除。這個方法不會修改輸入值，適合用來對照整數取位法。
    ///
    /// English:
    /// Converts n to a string and visits each digit character to calculate the digit sum and product.
    /// It keeps n unchanged and returns true only when n is divisible by the combined value.
    /// The input is a positive integer from 1 through 1,000,000.
    /// </summary>
    /// <param name="n">要檢查的正整數，範圍為 1 到 1,000,000。</param>
    /// <returns>若 n 能被數位總和與數位乘積的總和整除則回傳 true，否則回傳 false。</returns>
    public bool CheckDivisibilityByString(int n)
    {
        string digits = n.ToString();
        int digitSum = 0;
        int digitProduct = 1;

        foreach (char character in digits)
        {
            // ASCII 數字字元連續排列，減去 '0' 就能得到對應的整數數位。
            int digit = character - '0';
            digitSum += digit;
            digitProduct *= digit;
        }

        return n % (digitSum + digitProduct) == 0;
    }
}