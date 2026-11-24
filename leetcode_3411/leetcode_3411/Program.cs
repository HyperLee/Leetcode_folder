namespace leetcode_3411;

class Program
{
    /// <summary>
    /// 3411. Maximum Subarray With Equal Products
    /// https://leetcode.com/problems/maximum-subarray-with-equal-products/description/
    /// English
    /// You are given an array of positive integers nums.
    /// An array arr is called product equivalent if
    /// prod(arr) == lcm(arr) * gcd(arr), where:
    /// 
    /// prod(arr) is the product of all elements of arr.
    /// gcd(arr) is the GCD of all elements of arr.
    /// lcm(arr) is the LCM of all elements of arr.
    /// 
    /// Return the length of the longest product equivalent subarray of nums.
    /// 
    /// 3411. 最長乘積等價子陣列
    /// https://leetcode.cn/problems/maximum-subarray-with-equal-products/description/
    /// 繁體中文
    /// 給定一個由正整數組成的陣列 nums。
    /// 若陣列 arr 滿足 prod(arr) == lcm(arr) * gcd(arr)，
    /// 則稱它為乘積等價陣列。各函式的定義如下。
    /// 
    /// prod(arr) 是 arr 所有元素的乘積。
    /// gcd(arr) 是 arr 所有元素的最大公因數。
    /// lcm(arr) 是 arr 所有元素的最小公倍數。
    /// 
    /// 回傳 nums 中最長的乘積等價子陣列的長度。
    /// </summary>
    /// <remarks>
    /// 執行內建案例，逐筆對照兩種解法的結果與預期長度。
    /// </remarks>
    /// <param name="args">命令列引數，案例執行不需要使用。</param>
    /// <returns>所有案例通過時回傳 0，否則回傳 1。</returns>
    static int Main(string[] args)
    {
        (string Name, int[] Nums, int Expected)[] cases =
        [
            ("Example 1", [1, 2, 1, 2, 1, 1, 1], 5),
            ("Example 2", [2, 3, 4, 5, 6], 3),
            ("Example 3", [1, 2, 3, 1, 4, 5, 1], 5),
            ("Two elements with a common factor", [2, 4], 2),
            ("100 ones", Enumerable.Repeat(1, 100).ToArray(), 100),
            ("Repeated prime factor", [2, 4, 8], 2),
            ("Pairwise coprime elements", [8, 9, 5, 7], 4),
            ("100 tens", Enumerable.Repeat(10, 100).ToArray(), 2)
        ];

        Program solver = new Program();
        int passed = 0;
        foreach ((string name, int[] nums, int expected) in cases)
        {
            int enumeration = solver.MaxLength(nums);
            int slidingWindow = solver.MaxLength2(nums);
            bool matches = enumeration == expected && slidingWindow == expected;
            if (matches)
            {
                passed++;
            }

            string status = matches ? "PASS" : "FAIL";
            Console.WriteLine($"{name}: expected={expected}, MaxLength={enumeration}, MaxLength2={slidingWindow} [{status}]");
        }

        Console.WriteLine($"Passed {passed}/{cases.Length} cases.");
        return passed == cases.Length ? 0 : 1;
    }

    /// <summary>
    /// 枚舉連續子陣列，逐步維護乘積、最大公因數與最小公倍數。
    /// 若乘積大於 GCD 與 LCM 的乘積，就停止延伸同一起點。
    /// </summary>
    /// <param name="nums">含有 2 到 100 個元素的陣列，每個元素介於 1 到 10。</param>
    /// <returns>乘積等於最大公因數乘以最小公倍數的最長子陣列長度。</returns>
    public int MaxLength(int[] nums)
    {
        int res = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            int prodVal = nums[i];
            int gcdVal = nums[i];
            int lcmVal = nums[i];

            for (int j = i + 1; j < nums.Length; j++)
            {
                // 剪枝讓乘入新元素前的乘積至多為 25,200，乘入後至多為 252,000。
                prodVal = prodVal * nums[j];
                gcdVal = GCD2(gcdVal, nums[j]);
                lcmVal = lcmVal * nums[j] / GCD2(lcmVal, nums[j]);

                // 延伸區間時，乘積除以 LCM 不會下降，GCD 也不會上升。
                if (prodVal > gcdVal * lcmVal)
                {
                    break;
                }

                if (prodVal == gcdVal * lcmVal)
                {
                    res = Math.Max(res, j - i + 1);
                }
            }
        }

        return res;
    }

    /// <summary>
    /// 以滑動視窗尋找最長的兩兩互質子陣列，並計入必定乘積等價的長度 2。
    /// 使用視窗乘積與新元素的最大公因數判斷是否需要縮減視窗。
    /// </summary>
    /// <param name="nums">含有 2 到 100 個元素的陣列，每個元素介於 1 到 10。</param>
    /// <returns>乘積等於最大公因數乘以最小公倍數的最長子陣列長度。</returns>
    public int MaxLength2(int[] nums)
    {
        // 任意兩個正整數都滿足乘積等於 GCD × LCM，即使彼此有公因數。
        int best = 2;
        int product = 1;
        int start = 0;

        for (int end = 0; end < nums.Length; end++)
        {
            int current = nums[end];

            // GCD 大於 1 代表新元素與視窗內至少一個元素共用質因數。
            // 除掉左端元素以維持視窗乘積，直到新元素能與剩餘元素兩兩互質。
            while (GCD2(product, current) > 1)
            {
                product /= nums[start];
                start++;
            }

            product *= current;
            best = Math.Max(best, end - start + 1);
        }

        return best;
    }

    /// <summary>
    /// 使用歐幾里得演算法，反覆取餘數直到第二個數為零。
    /// </summary>
    /// <param name="a">第一個正整數。</param>
    /// <param name="b">第二個正整數。</param>
    /// <returns>兩個數的最大公因數，等於 1 代表彼此互質。</returns>
    private int GCD2(int a, int b)
    {
        while (b != 0)
        {
            int remainder = a % b;
            a = b;
            b = remainder;
        }

        return a;
    }
}
