namespace leetcode_1524;

class Program
{
    /// <summary>
    /// 1524. Number of Sub-arrays With Odd Sum
    /// https://leetcode.com/problems/number-of-sub-arrays-with-odd-sum/description/
    /// 1524. 總和為奇數的子陣列數目
    /// https://leetcode.cn/problems/number-of-sub-arrays-with-odd-sum/description/
    /// <para>
    /// Given an array of integers <c>arr</c>, return the number of subarrays with an <b>odd</b> sum.
    /// Since the answer can be very large, return it modulo <c>10^9 + 7</c>.
    /// </para>
    /// <para>
    /// 給定一個整數陣列 <c>arr</c>，回傳總和為<b>奇數</b>的子陣列數目。
    /// 由於答案可能非常大，請回傳答案除以 <c>10^9 + 7</c> 的餘數。
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    /// <remarks>
    /// 以同一組內建案例驗證兩個解法，逐項輸出比對結果。
    /// 任一比對失敗時，程序的結束碼為 1。
    /// </remarks>
    static void Main(string[] args)
    {
        (string Name, int[] Input, int Expected)[] testCases =
        [
            ("[1, 3, 5]", [1, 3, 5], 4),
            ("[2, 4, 6]", [2, 4, 6], 0),
            ("[1, 2, 3, 4, 5, 6, 7]", [1, 2, 3, 4, 5, 6, 7], 16),
            ("[1]", [1], 1),
            ("[2]", [2], 0),
            ("[1, 1]", [1, 1], 2),
            ("全為 1（長度 100,000）", Enumerable.Repeat(1, 100_000).ToArray(), 500_049_986)
        ];

        Program solution = new();
        (string Name, Func<int[], int> Solve)[] solutions =
        [
            (nameof(NumOfSubarrays), solution.NumOfSubarrays),
            (nameof(NumOfSubarrays2), solution.NumOfSubarrays2)
        ];

        int passed = 0;
        foreach ((string caseName, int[] input, int expected) in testCases)
        {
            foreach ((string methodName, Func<int[], int> solve) in solutions)
            {
                int actual = solve(input);
                bool matches = actual == expected;
                if (matches)
                {
                    passed++;
                }

                string status = matches ? "PASS" : "FAIL";
                Console.WriteLine($"{status} {methodName} | {caseName} | 預期 {expected} | 實際 {actual}");
            }
        }

        int totalComparisons = testCases.Length * solutions.Length;
        Console.WriteLine($"通過 {passed} / {totalComparisons} 次比對。");
        Environment.ExitCode = passed == totalComparisons ? 0 : 1;
    }

    /// <summary>
    /// 掃描正整數陣列，一邊累加前綴和，一邊用先前奇偶性相反的前綴數量
    /// 計算總和為奇數的子陣列個數，並逐步對 10^9 + 7 取模。
    /// </summary>
    /// <param name="arr">長度介於 1 和 100,000 之間，元素介於 1 和 100 之間的整數陣列。</param>
    /// <returns>總和為奇數的非空連續子陣列個數除以 10^9 + 7 的餘數。</returns>
    /// <remarks>
    /// 時間複雜度 O(n)，額外空間複雜度 O(1)。
    /// </remarks>
    public int NumOfSubarrays(int[] arr)
    {
        const int MODULO = 1_000_000_007;
        int odd = 0;
        // 空前綴的和為 0，先登記為偶數前綴，才能計入從索引 0 開始的區間。
        int even = 1;
        int subarrays = 0;
        int total = 0;

        foreach (int x in arr)
        {
            total += x;
            // 先與較早、奇偶性相反的前綴配對，再登記目前前綴。每步取模可控制累計值。
            if (total % 2 == 0)
            {
                subarrays = (subarrays + odd) % MODULO;
                even++;
            }
            else
            {
                subarrays = (subarrays + even) % MODULO;
                odd++;
            }
        }
        return subarrays % MODULO;
    }

    /// <summary>
    /// 先為正整數陣列建立前綴和的奇偶性陣列，再用查表統計奇偶性不同的前綴配對，
    /// 回傳總和為奇數的子陣列個數對 10^9 + 7 取模的結果。
    /// </summary>
    /// <param name="arr">長度介於 1 和 100,000 之間，元素介於 1 和 100 之間的整數陣列。</param>
    /// <returns>總和為奇數的非空連續子陣列個數除以 10^9 + 7 的餘數。</returns>
    /// <remarks>
    /// 時間複雜度 O(n)，額外空間複雜度 O(n)。
    /// </remarks>
    public int NumOfSubarrays2(int[] arr)
    {
        const int MODULO = 1_000_000_007;
        // prefixParity[0] 代表空前綴，後續才能計入從索引 0 開始的子陣列。
        int[] prefixParity = new int[arr.Length + 1];
        for (int i = 0; i < arr.Length; i++)
        {
            prefixParity[i + 1] = (prefixParity[i] + arr[i]) % 2;
        }

        // counts[0] 記錄已掃過的偶數前綴，counts[1] 記錄奇數前綴。
        int[] counts = new int[2];
        // 完整個數可能超過 int 上限，先用 long 累加，最後才取模。
        long subarrays = 0;
        foreach (int parity in prefixParity)
        {
            // XOR 1 互換 0 和 1，先查出較早的相反奇偶前綴，再登記目前前綴。
            subarrays += counts[parity ^ 1];
            counts[parity]++;
        }

        return (int)(subarrays % MODULO);
    }
}
