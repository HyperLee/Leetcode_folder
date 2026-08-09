namespace leetcode_1498;

class Program
{
    /// <summary>
    /// <para>
    /// 1498. Number of Subsequences That Satisfy the Given Sum Condition
    /// https://leetcode.com/problems/number-of-subsequences-that-satisfy-the-given-sum-condition/description/
    ///
    /// You are given an array of integers nums and an integer target.
    /// Return the number of non-empty subsequences of nums such that the sum of the minimum and maximum element in it is less
    /// than or equal to target. Since the answer may be too large, return it modulo 10^9 + 7.
    ///
    /// Example 1:
    /// Input: nums = [3,5,6,7], target = 9
    /// Output: 4
    /// Explanation: There are 4 subsequences that satisfy the condition:
    /// [3] -&gt; 3 + 3 &lt;= 9
    /// [3,5] -&gt; 3 + 5 &lt;= 9
    /// [3,5,6] -&gt; 3 + 6 &lt;= 9
    /// [3,6] -&gt; 3 + 6 &lt;= 9
    ///
    /// Example 2:
    /// Input: nums = [3,3,6,8], target = 10
    /// Output: 6
    /// Explanation: There are 6 valid subsequences (nums may contain repeated numbers):
    /// [3], [3], [3,3], [3,6], [3,6], [3,3,6].
    ///
    /// Example 3:
    /// Input: nums = [2,3,3,4,6,7], target = 12
    /// Output: 61
    /// Explanation: There are 63 non-empty subsequences; two do not satisfy the condition: [6,7] and [7].
    /// The number of valid subsequences is 63 - 2 = 61.
    ///
    /// Constraints:
    /// - 1 &lt;= nums.length &lt;= 10^5
    /// - 1 &lt;= nums[i] &lt;= 10^6
    /// - 1 &lt;= target &lt;= 10^6
    /// </para>
    /// <para>
    /// 1498. 滿足指定總和條件的子序列數目
    /// https://leetcode.cn/problems/number-of-subsequences-that-satisfy-the-given-sum-condition/description/
    ///
    /// 給定整數陣列 nums 與整數 target。
    /// 回傳 nums 中非空子序列的數量，使該子序列的最小元素與最大元素之和小於或等於 target。由於答案可能
    /// 很大，請回傳答案對 10^9 + 7 取模後的結果。
    ///
    /// 範例 1：
    /// 輸入：nums = [3,5,6,7]，target = 9
    /// 輸出：4
    /// 解釋：共有 4 個符合條件的子序列：
    /// [3] -&gt; 3 + 3 &lt;= 9
    /// [3,5] -&gt; 3 + 5 &lt;= 9
    /// [3,5,6] -&gt; 3 + 6 &lt;= 9
    /// [3,6] -&gt; 3 + 6 &lt;= 9
    ///
    /// 範例 2：
    /// 輸入：nums = [3,3,6,8]，target = 10
    /// 輸出：6
    /// 解釋：共有 6 個符合條件的子序列（nums 可以包含重複數值）：
    /// [3]、[3]、[3,3]、[3,6]、[3,6]、[3,3,6]。
    ///
    /// 範例 3：
    /// 輸入：nums = [2,3,3,4,6,7]，target = 12
    /// 輸出：61
    /// 解釋：共有 63 個非空子序列，其中兩個不符合條件：[6,7] 與 [7]。
    /// 符合條件的子序列數量為 63 - 2 = 61。
    ///
    /// 限制條件：
    /// - 1 &lt;= nums.length &lt;= 10^5
    /// - 1 &lt;= nums[i] &lt;= 10^6
    /// - 1 &lt;= target &lt;= 10^6
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }


    private const int MOD = 1000000007;
    private const int MaxN = 100005;

    /// <summary>
    /// 解法：計算貢獻法
    /// 
    /// 思路：
    /// 1. 由於只關心子序列的最小值和最大值，不關心元素的相對順序，所以可以對陣列排序
    /// 2. 固定最小值 v_min，則最大值 v_max 必須滿足：v_min ≤ v_max ≤ target - v_min
    /// 3. 這意味著 2 × v_min ≤ target，即 v_min ≤ target/2
    /// 4. 對於每個合法的 v_min，在排序後的陣列中二分查找最大的 v_max 位置
    /// 5. 若區間 [v_min, target-v_min] 中有 x 個元素，則貢獻為 2^(x-1)（v_min 必選，其他元素可選可不選）
    /// 6. 預處理 2^i % MOD 的值以提高效率
    /// </summary>
    /// <param name="nums">整數陣列</param>
    /// <param name="target">目標值</param>
    /// <returns>滿足條件的子序列數量對 10^9+7 取餘</returns>
    public int NumSubseq(int[] nums, int target)
    {
        // 預處理 2^i % MOD 的值，避免重複計算快速幂，提高效率
        // 用遞推的方法在 O(n) 時間內預處理出所有的 2^i，均攤到每個位置是 O(1)
        int[] f = new int[MaxN];
        f[0] = 1;

        for (int i = 1; i < MaxN; i++)
        {
            f[i] = (f[i - 1] * 2) % MOD; // 預計算 2^i % MOD
        }

        // 對原序列排序，因為我們只關心最小值和最大值，不關心元素的相對順序
        // 題目回傳答案是數量, 不是子序列所以排序不影響最終答案
        Array.Sort(nums); 
        int ans = 0;

        // 枚舉所有合法的 v_min（最小值）
        // 條件：nums[i] * 2 <= target，即 v_min <= target/2
        for (int i = 0; i < nums.Length && nums[i] * 2 <= target; i++)
        {
            // 計算在固定 v_min = nums[i] 的情況下，v_max 的最大可能值
            int maxValue = target - nums[i];
            
            // 二分查找：找到最大的滿足 nums[pos] <= maxValue 的位置
            int pos = BinarySearch(nums, maxValue) - 1;
            
            // 計算當前 v_min 對答案的貢獻
            // 如果區間 [i, pos] 中有 (pos-i+1) 個元素
            // 由於 v_min 是必選的，其他 (pos-i) 個元素可選可不選
            // 所以貢獻為 2^(pos-i); 每個答案可以 "選" 與 "不選" 所以是 2^(pos-i)
            int contribute = (pos >= i) ? f[pos - i] : 0;
            ans = (ans + contribute) % MOD; // 累加貢獻並取模
        }
        return ans;
    }

    /// <summary>
    /// 二分查找函式：尋找排序陣列中大於 target 的最小值的索引
    /// 
    /// 實現的是「upper_bound」語義：
    /// - 如果存在大於 target 的元素，返回第一個大於 target 的元素的索引
    /// - 如果所有元素都 <= target，返回陣列長度
    /// 
    /// 這樣設計是為了配合主函式中的 pos = BinarySearch(nums, maxValue) - 1
    /// 來找到 <= maxValue 的最大索引
    /// </summary>
    /// <param name="nums">已排序的整數陣列</param>
    /// <param name="target">查找的目標值</param>
    /// <returns>大於 target 的最小值的索引</returns>
    private int BinarySearch(int[] nums, int target)
    {
        int low = 0;
        int high = nums.Length;
        while (low < high)
        {
            int mid = (high - low) / 2 + low;
            if (mid == nums.Length)
            {
                return mid;
            }

            int num = nums[mid];
            if (num <= target)
            {
                low = mid + 1; // 尋找大於 target 的最小值
            }
            else
            {
                high = mid; // 尋找小於等於 target 的最大值 
            }
        }
        
        return low; // 返回大於 target 的最小值的索引
    }
}
