namespace leetcode_152;

class Program
{
    /// <summary>
    /// <para>
    /// 152. Maximum Product Subarray
    /// https://leetcode.com/problems/maximum-product-subarray/description/
    ///
    /// Given an integer array nums, find a subarray that has the largest product, and return the product.
    /// The test cases are generated so that the answer fits in a 32-bit integer.
    /// Note that the product of an array with a single element is the value of that element.
    ///
    /// Example 1:
    /// Input: nums = [2,3,-2,4]
    /// Output: 6
    /// Explanation: [2,3] has the largest product 6.
    ///
    /// Example 2:
    /// Input: nums = [-2,0,-1]
    /// Output: 0
    /// Explanation: The result cannot be 2 because [-2,-1] is not a subarray.
    ///
    /// Constraints:
    /// - 1 &lt;= nums.length &lt;= 2 * 10^4
    /// - -10 &lt;= nums[i] &lt;= 10
    /// - The product of any subarray of nums is guaranteed to fit in a 32-bit integer.
    /// </para>
    /// <para>
    /// 152. 乘積最大子陣列
    /// https://leetcode.cn/problems/maximum-product-subarray/description/
    ///
    /// 給定整數陣列 nums，找出乘積最大的子陣列，並回傳該乘積。
    /// 測試案例保證答案可用 32 位元整數表示。
    /// 請注意，只有一個元素的陣列，其乘積就是該元素的值。
    ///
    /// 範例 1：
    /// 輸入：nums = [2,3,-2,4]
    /// 輸出：6
    /// 解釋：[2,3] 的乘積最大，為 6。
    ///
    /// 範例 2：
    /// 輸入：nums = [-2,0,-1]
    /// 輸出：0
    /// 解釋：結果不能是 2，因為 [-2,-1] 並不是子陣列。
    ///
    /// 限制條件：
    /// - 1 &lt;= nums.length &lt;= 2 * 10^4
    /// - -10 &lt;= nums[i] &lt;= 10
    /// - nums 的任何子陣列乘積都保證可用 32 位元整數表示。
    /// </para>
    /// </summary>
    static void Main(string[] args)
    {
        Program solution = new Program();

        (string Name, int[] Input, int Expected)[] testCases =
        [
            ("官方範例 1：正負數混合", [2, 3, -2, 4], 6),
            ("官方範例 2：零切割區段", [-2, 0, -1], 0),
            ("單一負數", [-2], -2),
            ("奇數個負數", [-2, -3, -4], 12),
            ("偶數個負數", [-2, -3], 6),
            ("零後重新累積", [-2, 3, 0, -4], 3),
            ("重複負數", [-2, -2, -2], 4)
        ];

        (string Name, Func<int[], int> Solve)[] solutions =
        [
            (nameof(MaxProduct), solution.MaxProduct),
            (nameof(MaxProductTwoPointers), solution.MaxProductTwoPointers)
        ];

        int passedChecks = 0;
        int totalChecks = testCases.Length * solutions.Length;

        for (int i = 0; i < testCases.Length; i++)
        {
            (string name, int[] input, int expected) = testCases[i];
            Console.WriteLine($"案例 {i + 1}：{name}");
            Console.WriteLine($"輸入：[{string.Join(", ", input)}]");
            Console.WriteLine($"預期：{expected}");

            foreach ((string solutionName, Func<int[], int> solve) in solutions)
            {
                int actual = solve([.. input]);
                bool passed = actual == expected;
                passedChecks += passed ? 1 : 0;
                Console.WriteLine($"{solutionName}：{actual}（{(passed ? "PASS" : "FAIL")}）");
            }

            Console.WriteLine();
        }

        Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
    }

    /// <summary>
    /// 計算非空整數陣列中，乘積最大的連續子陣列乘積。
    /// 此動態規劃解法同時維護以目前位置結尾的最大與最小乘積，
    /// 讓負數可將先前的最小負值轉成新的最大正值；每個元素也可選擇自行成為新區段。
    /// 輸入須至少包含一個元素，回傳值為最大非空連續子陣列乘積。
    /// 時間複雜度為 O(n)，額外空間複雜度為 O(1)。
    /// </summary>
    /// <param name="nums">至少包含一個元素的整數陣列。</param>
    /// <returns>所有非空連續子陣列中的最大乘積。</returns>
    public int MaxProduct(int[] nums)
    {
        int max = nums[0];
        int min = nums[0];
        int result = nums[0];

        for (int i = 1; i < nums.Length; i++)
        {
            // 乘上負數會顛倒大小關係，先交換才能延續正確的最大與最小狀態。
            if (nums[i] < 0)
            {
                int temp = max;
                max = min;
                min = temp;
            }

            // 選擇延續前一區段，或由目前元素重新開始一段連續子陣列。
            max = Math.Max(nums[i], max * nums[i]);
            min = Math.Min(nums[i], min * nums[i]);
            result = Math.Max(result, max);
        }

        return result;
    }

    /// <summary>
    /// 使用左右雙向乘積掃描，計算非空整數陣列中最大的連續子陣列乘積。
    /// 對每個由零分隔的區段同時累積前綴與後綴乘積；當負數個數為奇數時，
    /// 最佳答案必然能由捨棄第一個負數以前的前綴，或最後一個負數以後的後綴取得。
    /// 輸入依題意須至少包含一個元素，回傳值為最大非空連續子陣列乘積。
    /// 時間複雜度為 O(n)，額外空間複雜度為 O(1)。
    /// </summary>
    /// <param name="nums">至少包含一個元素的整數陣列。</param>
    /// <returns>所有非空連續子陣列中的最大乘積；空值或空陣列會回傳 0。</returns>
    public int MaxProductTwoPointers(int[] nums)
    {
        if (nums == null || nums.Length == 0)
        {
            return 0;
        }

        int maxProduct = nums[0];
        int n = nums.Length;
        int leftProduct = 1;
        int rightProduct = 1;

        for (int i = 0; i < n; i++)
        {
            leftProduct *= nums[i];
            rightProduct *= nums[n - 1 - i];
            maxProduct = Math.Max(maxProduct, Math.Max(leftProduct, rightProduct));

            // 零會切斷連續乘積；重設為乘法單位元，讓下一個區段重新累積。
            if (leftProduct == 0)
            {
                leftProduct = 1;
            }

            if (rightProduct == 0)
            {
                rightProduct = 1;
            }
        }

        return maxProduct;
    }
}
