namespace leetcode_371;

class Program
{
    /// <summary>
    /// 371. Sum of Two Integers
    /// https://leetcode.com/problems/sum-of-two-integers/description/?envType=problem-list-v2&envId=oizxjoit
    /// 371. 两整数之和
    /// https://leetcode.cn/problems/sum-of-two-integers/description/
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        RunSamples();
    }

    /// <summary>
    /// 執行固定的加法與減法案例，逐筆比較預期值和實際值，並輸出 PASS/FAIL 與通過總數。
    /// 案例不需要外部輸入，涵蓋正數、負數、零、異號、互相抵消與題目限制邊界。
    /// </summary>
    private static void RunSamples()
    {
        SampleCase[] additionCases =
        [
            new("官方範例一", 1, 2, 3),
            new("官方範例二", 2, 3, 5),
            new("兩個零", 0, 0, 0),
            new("兩個負數", -4, -7, -11),
            new("異號相加", -5, 8, 3),
            new("互相抵消", -9, 9, 0),
            new("限制上界", 1000, 1000, 2000)
        ];

        SampleCase[] subtractionCases =
        [
            new("正差", 5, 3, 2),
            new("負差", 3, 5, -2),
            new("雙負數", -5, -3, -2),
            new("負數減正數", -3, 5, -8),
            new("兩個零", 0, 0, 0),
            new("上下界相減", 1000, -1000, 2000)
        ];

        int passedCount = RunCases("GetSum 位元加法", "+", GetSum, additionCases);
        Console.WriteLine();
        passedCount += RunCases("GetDiff 二補數減法", "-", GetDiff, subtractionCases);

        int totalCount = additionCases.Length + subtractionCases.Length;
        Console.WriteLine();
        Console.WriteLine($"總結：{passedCount}/{totalCount} 項驗證通過");
    }

    /// <summary>
    /// 以指定的二元整數運算執行一組案例，逐筆輸入兩個 <see cref="int"/> 值並比較預期與實際結果。
    /// 所有主控台輸出都集中在此方法；回傳值是這一組案例的通過數量。
    /// </summary>
    /// <param name="title">顯示於案例區段開頭的運算名稱。</param>
    /// <param name="operationSymbol">顯示輸入算式時使用的運算符號。</param>
    /// <param name="operation">接收兩個整數並回傳整數結果的待驗證方法。</param>
    /// <param name="samples">包含名稱、兩個輸入與預期結果的固定案例。</param>
    /// <returns>實際結果等於預期結果的案例數量。</returns>
    private static int RunCases(
        string title,
        string operationSymbol,
        Func<int, int, int> operation,
        SampleCase[] samples)
    {
        Console.WriteLine(title);
        int passedCount = 0;

        for (int index = 0; index < samples.Length; index++)
        {
            SampleCase sample = samples[index];
            int actual = operation(sample.A, sample.B);
            bool passed = actual == sample.Expected;

            if (index > 0)
            {
                Console.WriteLine();
            }

            Console.WriteLine($"案例 {index + 1}：{sample.Name}");
            Console.WriteLine($"輸入：a = {sample.A}, b = {sample.B}（{sample.A} {operationSymbol} {sample.B}）");
            Console.WriteLine($"預期：{sample.Expected}");
            Console.WriteLine($"實際：{actual}");
            Console.WriteLine($"結果：{(passed ? "PASS" : "FAIL")}");

            if (passed)
            {
                passedCount++;
            }
        }

        return passedCount;
    }

    /// <summary>
    /// 不使用加號或減號，利用 XOR 計算不進位的和，並以 AND 後左移一位計算進位。
    /// 輸入為任意兩個 32 位元有號整數；反覆合併部分和與進位後，回傳相同位元寬度下的整數和。
    /// </summary>
    /// <param name="a">第一個 32 位元有號整數。</param>
    /// <param name="b">第二個 32 位元有號整數，迴圈中也用來保存尚未合併的進位。</param>
    /// <returns>兩個輸入在 32 位元二補數規則下相加的結果。</returns>
    public static int GetSum(int a, int b)
    {
        while (b != 0)
        {
            // XOR 只合併不同的位元；AND 找出同為 1 的位置，左移後才是下一輪進位。
            int carry = (a & b) << 1;
            a ^= b;
            b = carry;
        }

        return a;
    }

    /// <summary>
    /// 不使用減號，把減去 <paramref name="b"/> 轉換成加上其二補數 <c>~b + 1</c>，並重用
    /// <see cref="GetSum(int, int)"/> 完成運算。輸入為任意兩個 32 位元有號整數。
    /// </summary>
    /// <param name="a">被減數。</param>
    /// <param name="b">減數。</param>
    /// <returns>兩個輸入在 32 位元二補數規則下相減的結果。</returns>
    public static int GetDiff(int a, int b)
    {
        // 對 b 逐位取反再加 1 可取得 -b，因此減法可完全交由 GetSum 處理。
        return GetSum(a, GetSum(~b, 1));
    }

    /// <summary>
    /// 表示一筆可執行整數運算案例，包含案例名稱、兩個輸入以及預期結果。
    /// </summary>
    /// <param name="Name">顯示於主控台的案例名稱。</param>
    /// <param name="A">第一個輸入整數。</param>
    /// <param name="B">第二個輸入整數。</param>
    /// <param name="Expected">此運算預期得到的整數結果。</param>
    private sealed record SampleCase(string Name, int A, int B, int Expected);
}