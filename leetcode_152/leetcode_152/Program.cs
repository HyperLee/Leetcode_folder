namespace leetcode_152;

class Program
{
    /// <summary>
    /// 152. Maximum Product Subarray
    /// https://leetcode.com/problems/maximum-product-subarray/description/?envType=problem-list-v2&envId=oizxjoit
    /// 152. 乘积最大子数组
    /// https://leetcode.cn/problems/maximum-product-subarray/description/
    /// 
    /// 解題概念出發點：
    /// 1. 連續子陣列乘積最大值問題的特殊性：
    ///    - 與一般最大子陣列和的問題不同，乘積可能因為負數而改變最大最小關係
    ///    - 兩個負數相乘會變成正數，可能產生更大的結果
    ///    - 零會重置所有計算
    /// 
    /// 2. 解決方案選擇理由：
    ///    - 使用動態規劃而非暴力法，可以優化時間複雜度
    ///    - 同時追蹤最大值和最小值，因為負數會使兩者互換
    ///    - 使用滾動變數代替陣列，優化空間複雜度
    /// 
    /// 3. 兩種解法比較：
    ///    動態規劃解法 (MaxProduct)：
    ///    優點：
    ///    - 思路直觀，易於理解和實現
    ///    - 程式碼結構清晰
    ///    缺點：
    ///    - 需要額外的變數來追蹤最大和最小值
    ///    - 需要額外的邏輯來處理負數情況
    ///    
    ///    雙指針解法 (MaxProductTwoPointers)：
    ///    優點：
    ///    - 程式碼更簡潔
    ///    - 不需要額外的空間來存儲最大最小值
    ///    - 自然處理負數情況，無需特別判斷
    ///    缺點：
    ///    - 思路較不直觀
    ///    - 在處理複雜測試案例時可能較難除錯
    /// 
    /// 負數 * 大  = 小(負越多越小)
    /// 負數 * 小  = 大(負越小越大)
    /// 正數 * 大  = 大
    /// 正數 * 小  = 小
    /// 
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
