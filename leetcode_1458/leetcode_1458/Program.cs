namespace leetcode_1458;

class Program
{
    /// <summary>
    /// <para>
    /// 1458. Max Dot Product of Two Subsequences
    /// https://leetcode.com/problems/max-dot-product-of-two-subsequences/description/
    ///
    /// Given two arrays nums1 and nums2. Return the maximum dot product between non-empty subsequences of nums1 and nums2
    /// with the same length.
    ///
    /// A subsequence of an array is a new array formed from the original array by deleting some (possibly none) of the
    /// elements without disturbing the relative positions of the remaining elements. For example, [2,3,5] is a subsequence
    /// of [1,2,3,4,5], while [1,5,3] is not.
    ///
    /// Example 1:
    /// Input: nums1 = [2,1,-2,5], nums2 = [3,0,-6]
    /// Output: 18
    /// Explanation: Take [2,-2] from nums1 and [3,-6] from nums2. Their dot product is
    /// (2*3 + (-2)*(-6)) = 18.
    ///
    /// Example 2:
    /// Input: nums1 = [3,-2], nums2 = [2,-6,7]
    /// Output: 21
    /// Explanation: Take [3] from nums1 and [7] from nums2. Their dot product is (3*7) = 21.
    ///
    /// Example 3:
    /// Input: nums1 = [-1,-1], nums2 = [1,1]
    /// Output: -1
    /// Explanation: Take [-1] from nums1 and [1] from nums2. Their dot product is -1.
    ///
    /// Constraints:
    /// - 1 &lt;= nums1.length, nums2.length &lt;= 500
    /// - -1000 &lt;= nums1[i], nums2[i] &lt;= 1000
    /// </para>
    /// <para>
    /// 1458. 兩個子序列的最大點積
    /// https://leetcode.cn/problems/max-dot-product-of-two-subsequences/description/
    ///
    /// 給定兩個陣列 nums1 與 nums2。回傳 nums1 與 nums2 中長度相同的非空子序列之間的最大點積。
    ///
    /// 陣列的子序列是從原陣列刪除若干元素（也可以不刪除）後形成的新陣列，且不改變其餘元素的相對位置。
    /// 例如，[2,3,5] 是 [1,2,3,4,5] 的子序列，而 [1,5,3] 不是。
    ///
    /// 範例 1：
    /// 輸入：nums1 = [2,1,-2,5]，nums2 = [3,0,-6]
    /// 輸出：18
    /// 解釋：從 nums1 取 [2,-2]，從 nums2 取 [3,-6]；點積為 (2*3 + (-2)*(-6)) = 18。
    ///
    /// 範例 2：
    /// 輸入：nums1 = [3,-2]，nums2 = [2,-6,7]
    /// 輸出：21
    /// 解釋：從 nums1 取 [3]，從 nums2 取 [7]；點積為 (3*7) = 21。
    ///
    /// 範例 3：
    /// 輸入：nums1 = [-1,-1]，nums2 = [1,1]
    /// 輸出：-1
    /// 解釋：從 nums1 取 [-1]，從 nums2 取 [1]；點積為 -1。
    ///
    /// 限制條件：
    /// - 1 &lt;= nums1.length, nums2.length &lt;= 500
    /// - -1000 &lt;= nums1[i], nums2[i] &lt;= 1000
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program program = new Program();

        // 測試案例 1：nums1 = [2,1,-2,5], nums2 = [3,0,-6]
        // 預期輸出：18
        // 說明：取子序列 [2,-2] 和 [3,-6] -> (2*3 + (-2)*(-6)) = 6 + 12 = 18
        int[] nums1_1 = [2, 1, -2, 5];
        int[] nums2_1 = [3, 0, -6];
        int result1 = program.MaxDotProduct(nums1_1, nums2_1);
        Console.WriteLine($"測試案例 1: {result1}");  // 預期輸出：18

        // 測試案例 2：nums1 = [3,-2], nums2 = [2,-6,7]
        // 預期輸出：21
        // 說明：取子序列 [3] 和 [7] -> 3*7 = 21
        int[] nums1_2 = [3, -2];
        int[] nums2_2 = [2, -6, 7];
        int result2 = program.MaxDotProduct(nums1_2, nums2_2);
        Console.WriteLine($"測試案例 2: {result2}");  // 預期輸出：21

        // 測試案例 3：nums1 = [-1,-1], nums2 = [1,1]
        // 預期輸出：-1
        // 說明：必須至少選一對，最佳選擇是 [-1] 和 [1] -> -1*1 = -1
        int[] nums1_3 = [-1, -1];
        int[] nums2_3 = [1, 1];
        int result3 = program.MaxDotProduct(nums1_3, nums2_3);
        Console.WriteLine($"測試案例 3: {result3}");  // 預期輸出：-1
    }

    /// <summary>
    /// 計算兩個陣列子序列的最大點積（動態規劃解法）
    /// 
    /// 解題思路：
    /// 使用二維動態規劃陣列 dp[i][j] 表示只考慮 nums1 的前 i+1 個元素和 nums2 的前 j+1 個元素時，
    /// 可以得到的兩個長度相同的非空子序列的最大點積。
    /// 
    /// 狀態轉移方程式：
    /// dp[i][j] = max(
    ///     xij,                      // 只選擇當前這一對元素
    ///     dp[i-1][j],               // 跳過 nums1[i]
    ///     dp[i][j-1],               // 跳過 nums2[j]
    ///     dp[i-1][j-1] + xij        // 選擇當前元素對，並加上之前的最佳結果
    /// )
    /// 其中 xij = nums1[i] * nums2[j]
    /// 
    /// 時間複雜度：O(m*n)，其中 m 和 n 分別是兩個陣列的長度
    /// 空間複雜度：O(m*n)
    /// </summary>
    /// <param name="nums1">第一個整數陣列</param>
    /// <param name="nums2">第二個整數陣列</param>
    /// <returns>兩個子序列的最大點積</returns>
    public int MaxDotProduct(int[] nums1, int[] nums2)
    {
        int m = nums1.Length;
        int n = nums2.Length;
        
        // dp[i, j] 表示考慮 nums1[0..i] 和 nums2[0..j] 的最大點積
        int[,] dp = new int[m, n];

        for(int i = 0; i < m; i++)
        {
            for(int j = 0; j < n; j++)
            {
                // 計算當前位置的點積值
                int xij = nums1[i] * nums2[j];
                
                // 初始化：至少選擇當前這一對元素
                dp[i, j] = xij;

                // 情況 1：跳過 nums1[i]，使用 dp[i-1][j] 的結果
                if(i > 0)
                {
                    dp[i, j] = Math.Max(dp[i, j], dp[i - 1, j]);
                }

                // 情況 2：跳過 nums2[j]，使用 dp[i][j-1] 的結果
                if(j > 0)
                {
                    dp[i, j] = Math.Max(dp[i, j], dp[i, j - 1]);
                }

                // 情況 3：選擇當前元素對，並加上之前的最佳結果 dp[i-1][j-1]
                // 這樣可以形成更長的子序列組合
                if(i > 0 && j > 0)
                {
                    dp[i, j] = Math.Max(dp[i, j], dp[i - 1, j - 1] + xij);
                }
            }
        }
        
        // 返回考慮所有元素後的最大點積
        return dp[m - 1, n - 1];
    }
}
