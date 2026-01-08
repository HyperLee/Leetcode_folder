namespace leetcode_1458;

class Program
{
    /// <summary>
    /// 1458. Max Dot Product of Two Subsequences
    /// https://leetcode.com/problems/max-dot-product-of-two-subsequences/description/?envType=daily-question&envId=2026-01-08
    /// 1458. 兩個子序列的最大點積（簡體中文）
    /// https://leetcode.cn/problems/max-dot-product-of-two-subsequences/description/?envType=daily-question&envId=2026-01-08
    ///
    /// 繁體中文題目描述：
    /// 給定兩個整數陣列 `nums1` 與 `nums2`，請回傳兩個子序列（長度相同且非空）之間的最大點積（dot product）。
    /// 子序列是由原陣列刪除某些元素（可以不刪除）後得到的新陣列，保留原元素的相對順序。
    /// 例如，`[2,3,5]` 是 `[1,2,3,4,5]` 的子序列，但 `[1,5,3]` 不是子序列。
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
