namespace leetcode_2897;

class Program
{
    /// <summary>
    /// 2897. Apply Operations on Array to Maximize Sum of Squares
    /// https://leetcode.com/problems/apply-operations-on-array-to-maximize-sum-of-squares/description/
    /// 2897. 对数组执行操作使平方和最大
    /// https://leetcode.cn/problems/apply-operations-on-array-to-maximize-sum-of-squares/description/
    ///
    /// English:
    /// You are given a 0-indexed integer array nums and a positive integer k.
    ///
    /// You can do the following operation on the array any number of times:
    /// - Choose any two distinct indices i and j and simultaneously update the values of
    ///   nums[i] to (nums[i] AND nums[j]) and nums[j] to (nums[i] OR nums[j]).
    ///   Here, OR denotes the bitwise OR operation, and AND denotes the bitwise AND operation.
    ///
    /// You have to choose k elements from the final array and calculate the sum of their squares.
    /// Return the maximum sum of squares you can achieve.
    /// Since the answer can be very large, return it modulo 10^9 + 7.
    ///
    /// 繁體中文：
    /// 給定一個索引從 0 開始的整數陣列 nums，以及一個正整數 k。
    ///
    /// 你可以對陣列執行以下操作任意次：
    /// - 選擇任意兩個不同的索引 i 和 j，並同時將 nums[i] 更新為
    ///   (nums[i] AND nums[j])，將 nums[j] 更新為 (nums[i] OR nums[j])。
    ///   兩個新值皆使用更新前的值計算；OR 表示位元 OR（或）運算，AND 表示位元 AND（且）運算。
    ///
    /// 你必須從最終陣列中選擇 k 個元素，並計算它們的平方和。
    /// 回傳你能得到的最大平方和。
    /// 由於答案可能非常大，請回傳答案對 10^9 + 7 取模後的結果。
    /// </summary>
    /// <remarks>
    /// 直接執行 12 組固定案例，比較兩種解法的預期值、實際值及輸入是否保持不變。
    /// 所有檢查通過時退出碼為 0；任一結果或輸入保護檢查失敗時退出碼為 1。
    /// </remarks>
    /// <param name="args">命令列參數；固定測試不使用此參數。</param>
    static void Main(string[] args)
    {
        Program solver = new Program();
        (string Name, int[] Nums, int K, int Expected)[] cases =
        [
            ("Official example 1", [2, 6, 5, 8], 2, 261),
            ("Official example 2", [4, 5, 4, 7], 3, 90),
            ("Minimum input", [1], 1, 1),
            ("Choose one", [2, 6, 5, 8], 1, 225),
            ("Choose all", [2, 6, 5, 8], 4, 261),
            ("Repeated bits", [7, 7, 7], 2, 98),
            ("Disjoint bits", [1, 2, 4, 8], 2, 225),
            ("Unequal bit counts", [3, 5, 6], 2, 98),
            ("Bits exhausted", [1, 2], 2, 9),
            ("Highest used bit", [536870912, 1], 1, 207840944),
            ("Large squares", [1000000000, 1000000000], 2, 98),
            ("Maximum length", Enumerable.Repeat(1000000000, 100000).ToArray(), 100000, 4900000)
        ];
        (string Name, Func<IList<int>, int, int> Solve)[] methods =
        [
            (nameof(MaxSum), solver.MaxSum),
            (nameof(MaxSum2), solver.MaxSum2)
        ];

        int passed = 0;
        int inputPassed = 0;
        foreach ((string name, int[] nums, int k, int expected) in cases)
        {
            foreach ((string methodName, Func<IList<int>, int, int> solve) in methods)
            {
                // 每次呼叫使用獨立輸入，避免某個方法的修改影響另一個方法。
                int[] input = (int[])nums.Clone();
                int actual = solve(input, k);
                bool resultMatches = actual == expected;
                bool inputUnchanged = input.SequenceEqual(nums);
                passed += resultMatches ? 1 : 0;
                inputPassed += inputUnchanged ? 1 : 0;
                Console.WriteLine($"{name} | {methodName} | Expected: {expected} | Actual: {actual} | {(resultMatches ? "PASS" : "FAIL")} | Input unchanged: {(inputUnchanged ? "PASS" : "FAIL")}");
            }
        }

        int total = cases.Length * methods.Length;
        Console.WriteLine($"Summary: {passed}/{total} checks passed.");
        Console.WriteLine($"Input preservation: {inputPassed}/{total} checks passed.");
        Environment.ExitCode = passed == total && inputPassed == total ? 0 : 1;
    }

    /// <summary>
    /// 方法一：統計各位元的 1 數量，每次取用所有仍可用的位元，依序建立 k 個數字。
    /// 位元集中在較大的數字可增加平方和；接受題目範圍的輸入，回傳最大平方和的餘數。
    /// 時間為 O(30n + 30k)，額外空間為 O(30)，不修改輸入。
    /// </summary>
    /// <param name="nums">長度介於 1 與 100000 的串列，元素介於 1 與 1000000000。</param>
    /// <param name="k">要選取的數量，介於 1 與 nums.Count 之間。</param>
    /// <returns>最大平方和對 1000000007 取模的結果。</returns>
    public int MaxSum(IList<int> nums, int k)
    {
        const int MOD = 1000000007;
        int[] cnt = new int[30];

        // AND／OR 保留每個位元的 1 總數；只需統計可分配次數，不必模擬操作。
        foreach (int num in nums)
        {
            for (int i = 0; i < 30; i++)
            {
                if ((num & (1 << i)) != 0)
                {
                    cnt[i]++;
                }
            }
        }

        long result = 0;
        for (int i = 0; i < k; i++)
        {
            long num = 0;
            // 優先把所有可用位元集中在目前數字；高低位走訪順序不影響結果。
            for (int j = 29; j >= 0; j--)
            {
                if (cnt[j] > 0)
                {
                    num |= (1L << j);
                    // 同一位元在一個數字中只能出現一次，取用後消耗一份計數。
                    cnt[j]--;
                }
            }
            // num 使用 long，避免 int 平方溢位；每輪取模避免累加完整平方和。
            result = (result + num * num) % MOD;
        }

        return (int)result;
    }
    /// <summary>
    /// 方法二：以位元為主軸，將每個位元分配給前面的數字，集中位元以最大化平方和。
    /// 接受題目範圍內的正整數串列與選取數量 k，回傳最大平方和對 10^9 + 7 的餘數。
    /// </summary>
    /// <param name="nums">不會被修改的原始正整數串列。</param>
    /// <param name="k">要選取的數量，介於 1 與 nums.Count 之間。</param>
    /// <returns>最大平方和對 1000000007 取模的結果。</returns>
    public int MaxSum2(IList<int> nums, int k)
    {
        const int MOD = 1000000007;
        int[] cnt = new int[30];
        foreach (int num in nums)
        {
            for (int bit = 0; bit < 30; bit++)
            {
                if ((num & (1 << bit)) != 0)
                {
                    cnt[bit]++;
                }
            }
        }

        long[] values = new long[k];
        for (int bit = 0; bit < 30; bit++)
        {
            // 每個位元都分配給最前面的數字，使不同位元盡量重疊。
            // 超過 k 的份數留在未選取的元素，不需要建立完整 n 個數字。
            int count = Math.Min(cnt[bit], k);
            for (int index = 0; index < count; index++)
            {
                values[index] |= 1L << bit;
            }
        }

        long result = 0;
        foreach (long value in values)
        {
            // value 是 long，因此乘法也在 long 範圍內執行。
            result = (result + value * value) % MOD;
        }

        return (int)result;
    }
}