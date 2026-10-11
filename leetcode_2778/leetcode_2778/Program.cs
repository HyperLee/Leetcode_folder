namespace leetcode_2778;

class Program
{
    /// <summary>
    /// 2778. Sum of Squares of Special Elements
    /// https://leetcode.com/problems/sum-of-squares-of-special-elements/description/
    /// 2778. 特殊元素平方和
    /// https://leetcode.cn/problems/sum-of-squares-of-special-elements/
    ///
    /// English:
    /// You are given a 1-indexed integer array nums of length n.
    ///
    /// An element nums[i] of nums is called special if i divides n, i.e. n % i == 0.
    ///
    /// Return the sum of the squares of all special elements of nums.
    ///
    /// 繁體中文：
    /// 給定一個長度為 n、索引從 1 開始的整數陣列 nums。
    ///
    /// 如果 i 能整除 n，也就是 n % i == 0，則 nums 中的元素 nums[i] 稱為特殊元素。
    ///
    /// 回傳 nums 中所有特殊元素的平方和。
    /// </summary>
    /// <remarks>
    /// 執行八筆固定資料，分別驗證逐一枚舉與因數配對兩種解法，共十六次檢查。
    /// 不需要互動輸入；輸出每筆輸入、方法名稱、Expected、Actual 與 PASS/FAIL。
    /// 全部通過時退出碼為 0，任一檢查失敗時退出碼為 1。
    /// </remarks>
    /// <param name="args">命令列參數；此固定測試程式不使用。</param>
    static void Main(string[] args)
    {
        (string Name, int[] Nums, int Expected)[] cases =
        [
            ("Official example 1", [1, 2, 3, 4], 21),
            ("Official example 2", [2, 7, 1, 19, 18, 3], 63),
            ("Minimum length", [1], 1),
            ("Single maximum element", [50], 2500),
            ("Prime length", Enumerable.Range(1, 7).ToArray(), 50),
            ("Perfect square length", Enumerable.Range(1, 9).ToArray(), 91),
            ("Multiple divisor pairs", Enumerable.Range(1, 12).ToArray(), 210),
            ("Maximum constraints", Enumerable.Repeat(50, 50).ToArray(), 15000)
        ];
        Program solution = new Program();
        (string Name, Func<int[], int> Solve)[] methods =
        [
            (nameof(SumOfSquares), solution.SumOfSquares),
            (nameof(SumOfSquares2), solution.SumOfSquares2)
        ];
        int passed = 0;
        int total = cases.Length * methods.Length;

        foreach ((string name, int[] nums, int expected) in cases)
        {
            Console.WriteLine($"{name}: nums = [{string.Join(", ", nums)}]");
            foreach ((string methodName, Func<int[], int> solve) in methods)
            {
                int actual = solve(nums);
                bool isPassed = actual == expected;
                if (isPassed)
                {
                    passed++;
                }
                Console.WriteLine($"  {methodName}: Expected = {expected}, Actual = {actual}, {(isPassed ? "PASS" : "FAIL")}");
            }
        }

        Console.WriteLine($"Summary: {passed}/{total} checks passed.");
        Environment.ExitCode = passed == total ? 0 : 1;
    }

    /// <summary>
    /// 計算特殊元素平方和；以逐一枚舉位置的方式篩選並累加平方。
    /// 輸入須符合題目限制，回傳整數平方和。
    /// 方法一：枚举
    /// 我们枚举所有下标，将特殊元素的平方累加即可。
    /// 在代码实现中，数组下标从 0 开始，因此当下标为 i 时，需要判断 n 是否能被 i+1 整除。
    /// </summary>
    /// <remarks>
    /// 逐一檢查每個零基底索引 i，以 n % (i + 1) 判斷題目的一基底位置是否為因數。
    /// 時間複雜度 O(n)，額外空間 O(1)，不修改輸入陣列。
    /// </remarks>
    /// <param name="nums">非 null 的整數陣列；長度與每個元素皆介於 1 至 50。</param>
    /// <returns>所有位置能整除陣列長度的元素平方和。</returns>
    public int SumOfSquares(int[] nums)
    {
        int n = nums.Length;
        int ans = 0;
        for (int i = 0; i < n; i++)
        {
            // 特殊條件取決於位置：C# 索引 i 對應題目位置 i + 1，而不是 nums[i] 的值。
            if (n % (i + 1) == 0)
            {
                ans += nums[i] * nums[i];
            }
        }
        return ans;
    }

    /// <summary>
    /// 計算特殊元素平方和；以成對因數直接存取特殊位置，減少檢查次數。
    /// 輸入須符合題目限制，回傳整數平方和。
    /// 方法二：枚举因数
    /// 由于只有 n 的因数对应的元素是特殊元素，我们可以只枚举这些因数。
    /// 根据题意，如果 i 是 n 的因子，那我们也找到了另一个因子  n/i。所以只需枚举  n以内的 i。
    /// </summary>
    /// <remarks>
    /// 枚舉至平方根：因數 i 與 n / i 成對出現，分別取索引 i - 1 與 n / i - 1。
    /// 若 i * i == n，兩個因數相同，只累加一次。時間 O(√n)，額外空間 O(1)。
    /// 不修改輸入陣列；原說明的枚舉範圍在程式中具體為 i * i ≤ n。
    /// </remarks>
    /// <param name="nums">非 null 的整數陣列；長度與每個元素皆介於 1 至 50。</param>
    /// <returns>所有因數位置對應的元素平方和。</returns>
    public int SumOfSquares2(int[] nums)
    {
        int n = nums.Length;
        int ans = 0;
        // 每組因數至少有一個不大於平方根；含等號才能涵蓋完全平方數的中間因數。
        for (int i = 1; i * i <= n; i++)
        {
            if (n % i == 0)
            {
                // i 是一基底位置，存取陣列時換成零基底索引 i - 1。
                ans += nums[i - 1] * nums[i - 1];
                // 平方根因數與配對因數相同時，不可重複計算同一個元素。
                if (i * i != n)
                {
                    // 配對因數 n / i 也是一基底位置，因此其陣列索引是 n / i - 1。
                    ans += nums[n / i - 1] * nums[n / i - 1];
                }
            }
        }
        return ans;
    }
}