namespace leetcode_3190;

class Program
{
    /// <summary>
    /// 3190. Find Minimum Operations to Make All Elements Divisible by Three
    /// https://leetcode.com/problems/find-minimum-operations-to-make-all-elements-divisible-by-three/description/?envType=daily-question&envId=2025-11-22
    /// 3190. 使所有元素都可以被 3 整除的最少操作数
    /// https://leetcode.cn/problems/find-minimum-operations-to-make-all-elements-divisible-by-three/description/?envType=daily-question&envId=2025-11-22
    ///  
    /// Given an integer array nums. In one operation, you can add or subtract 1 from any element of nums.
    /// Return the minimum number of operations to make all elements of nums divisible by 3.
    ///
    /// 給定一個整數陣列 nums。一次操作可以將任一元素加 1 或減 1。
    /// 返回使所有元素都可以被 3 整除所需的最少操作次數。
    /// </summary>
    /// <param name="args">命令列參數；本範例不使用。</param>
    static void Main(string[] args)
    {
        Program program = new Program();
        (string Name, int[] Nums, int Expected)[] testCases = new[]
        {
            ("官方範例一", new[] { 1, 2, 3, 4 }, 3),
            ("已全部整除", new[] { 3, 6, 9 }, 0),
            ("最小元素邊界", new[] { 1 }, 1),
            ("最大值與重複值", new[] { 50, 50, 50 }, 3),
            ("所有元素都需操作", new[] { 1, 2, 4, 5, 7, 8 }, 6)
        };
        (string Name, Func<int[], int> Solve)[] solutions = new (string Name, Func<int[], int> Solve)[]
        {
            (nameof(MinimumOperations), program.MinimumOperations),
            (nameof(MinimumOperations2), program.MinimumOperations2)
        };

        int passedChecks = 0;

        foreach ((string solutionName, Func<int[], int> solve) in solutions)
        {
            Console.WriteLine($"=== {solutionName} ===");

            foreach ((string caseName, int[] nums, int expected) in testCases)
            {
                int actual = solve(nums);
                bool passed = actual == expected;

                Console.WriteLine($"Case: {caseName}");
                Console.WriteLine($"Input: [{string.Join(", ", nums)}]");
                Console.WriteLine($"Expected: {expected}");
                Console.WriteLine($"Actual: {actual}");
                Console.WriteLine($"Result: {(passed ? "PASS" : "FAIL")}");
                Console.WriteLine();

                if (passed)
                {
                    passedChecks++;
                }
            }
        }

        int totalChecks = testCases.Length * solutions.Length;
        Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed");

        if (passedChecks != totalChecks)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 計算讓所有元素都能被 3 整除的最少操作數。
    /// 解法依餘數分類：餘數為 0 的元素不需操作，餘數為 1 或 2 的元素都能以一次加減 1
    /// 變成 3 的倍數，因此只要統計不能被 3 整除的元素數量。
    /// </summary>
    /// <param name="nums">非空正整數陣列；題目保證長度與每個元素值皆介於 1 到 50。</param>
    /// <returns>使陣列所有元素都可以被 3 整除所需的最少操作次數。</returns>
    public int MinimumOperations(int[] nums)
    {
        int res = 0;

        foreach (int num in nums)
        {
            // 餘數只有 1 或 2 時才需操作，而且都能在一次加減 1 後完成。
            res += num % 3 != 0 ? 1 : 0;
        }

        return res;
    }

    /// <summary>
    /// 計算讓所有元素都能被 3 整除的最少操作數。
    /// 解法先求每個元素除以 3 的餘數，再比較向下減到前一個 3 的倍數與向上加到下一個
    /// 3 的倍數所需的步數，取較小值後以 LINQ 加總。
    /// </summary>
    /// <param name="nums">非空正整數陣列；題目保證長度與每個元素值皆介於 1 到 50。</param>
    /// <returns>使陣列所有元素都可以被 3 整除所需的最少操作次數。</returns>
    public int MinimumOperations2(int[] nums)
    {
        return nums.Select(x =>
        {
            int remainder = x % 3;

            // remainder 是向下移動的距離，3 - remainder 是向上移動的距離。
            return Math.Min(remainder, 3 - remainder);
        }).Sum();
    }
}
