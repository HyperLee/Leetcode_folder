namespace leetcode_2040;

class Program
{
    /// <summary>
    /// <para>
    /// 2040. Kth Smallest Product of Two Sorted Arrays
    /// https://leetcode.com/problems/kth-smallest-product-of-two-sorted-arrays/description/
    ///
    /// Given two sorted 0-indexed integer arrays nums1 and nums2 and an integer k, return the k-th (1-based) smallest product nums1[i] * nums2[j], where 0 &lt;= i &lt; nums1.length and 0 &lt;= j &lt; nums2.length.
    ///
    /// Example 1:
    /// Input: nums1 = [2,5], nums2 = [3,4], k = 2
    /// Output: 8
    /// Explanation: The 2 smallest products are nums1[0] * nums2[0] = 2 * 3 = 6 and nums1[0] * nums2[1] = 2 * 4 = 8. The 2nd is 8.
    ///
    /// Example 2:
    /// Input: nums1 = [-4,-2,0,3], nums2 = [2,4], k = 6
    /// Output: 0
    /// Explanation: The 6 smallest products are (-4) * 4 = -16, (-4) * 2 = -8, (-2) * 4 = -8, (-2) * 2 = -4, 0 * 2 = 0, and 0 * 4 = 0. The 6th is 0.
    ///
    /// Example 3:
    /// Input: nums1 = [-2,-1,0,1,2], nums2 = [-3,-1,2,4,5], k = 3
    /// Output: -6
    /// Explanation: The 3 smallest products are (-2) * 5 = -10, (-2) * 4 = -8, and 2 * (-3) = -6. The 3rd is -6.
    ///
    /// Constraints:
    /// - 1 &lt;= nums1.length, nums2.length &lt;= 5 * 10^4
    /// - -10^5 &lt;= nums1[i], nums2[j] &lt;= 10^5
    /// - 1 &lt;= k &lt;= nums1.length * nums2.length
    /// - nums1 and nums2 are sorted.
    /// </para>
    /// <para>
    /// 2040. 兩個有序陣列的第 K 小乘積
    /// https://leetcode.cn/problems/kth-smallest-product-of-two-sorted-arrays/description/
    ///
    /// 給定兩個從 0 開始索引且已排序的整數陣列 nums1、nums2，以及整數 k，回傳所有 nums1[i] * nums2[j] 中第 k 小（從 1 開始計算）的乘積，其中 0 &lt;= i &lt; nums1.length 且 0 &lt;= j &lt; nums2.length。
    ///
    /// 範例 1：
    /// 輸入：nums1 = [2,5], nums2 = [3,4], k = 2
    /// 輸出：8
    /// 說明：最小的 2 個乘積為 nums1[0] * nums2[0] = 2 * 3 = 6 與 nums1[0] * nums2[1] = 2 * 4 = 8；第 2 小為 8。
    ///
    /// 範例 2：
    /// 輸入：nums1 = [-4,-2,0,3], nums2 = [2,4], k = 6
    /// 輸出：0
    /// 說明：最小的 6 個乘積為 (-4) * 4 = -16、(-4) * 2 = -8、(-2) * 4 = -8、(-2) * 2 = -4、0 * 2 = 0、0 * 4 = 0；第 6 小為 0。
    ///
    /// 範例 3：
    /// 輸入：nums1 = [-2,-1,0,1,2], nums2 = [-3,-1,2,4,5], k = 3
    /// 輸出：-6
    /// 說明：最小的 3 個乘積為 (-2) * 5 = -10、(-2) * 4 = -8、2 * (-3) = -6；第 3 小為 -6。
    ///
    /// 限制條件：
    /// - 1 &lt;= nums1.length, nums2.length &lt;= 5 * 10^4
    /// - -10^5 &lt;= nums1[i], nums2[j] &lt;= 10^5
    /// - 1 &lt;= k &lt;= nums1.length * nums2.length
    /// - nums1 與 nums2 已排序。
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料
        int[] nums1 = {2, 5};
        int[] nums2 = {3, 4};
        long k = 2;
        // 預期結果：10 (乘積有6個: 2*3=6, 2*4=8, 5*3=15, 5*4=20，排序後第2小是8)
        var prog = new Program();
        long result = prog.KthSmallestProduct(nums1, nums2, k);
        Console.WriteLine($"第{k}小乘積: {result}");
        // 其他測試
        int[] nums3 = {-4, -2, 0, 3};
        int[] nums4 = {2, 4};
        k = 6;
        // 預期結果：16
        Console.WriteLine($"第{k}小乘積: {prog.KthSmallestProduct(nums3, nums4, k)}");
    }


    /// <summary>
    /// 統計 nums2 中，與 x1 相乘後小於等於 v 的個數
    /// </summary>
    /// <param name="nums2"></param>
    /// <param name="x1"></param>
    /// <param name="v"></param>
    /// <returns></returns>
    int F(int[] nums2, long x1, long v)
    {
        int n2 = nums2.Length;
        int left = 0;
        int right = n2 - 1;
        
        // 二分搜尋
        while (left <= right)
        {
            int mid = (left + right) / 2;
            long prod = (long)nums2[mid] * x1;

            // x1 >= 0 時，nums2[j]*x1 單調遞增(正數 * 正數 = 越來越大)，找 <= v 的個數
            // x1 < 0 時，nums2[j]*x1 單調遞減(負數 * 正數 = 越來越小)，找 > v 的個數
            if ((x1 >= 0 && prod <= v) || (x1 < 0 && prod > v))
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }
        
        // x1 >= 0 時，left 為 <= v 的個數
        // x1 < 0 時，n2 - left 為 <= v 的個數
        if (x1 >= 0)
        {
            return left;
        }
        else
        {
            return n2 - left;
        }
    }

    /// <summary>
    /// 二分搜尋乘積區間，找第 k 小乘積
    /// 
    /// 解題說明：
    /// 1. 由於乘積的取值範圍為 [-1e10, 1e10]，可在此區間進行二分搜尋。
    /// 2. 對於每個二分值 v，計算小於等於 v 的乘積數目 count。
    /// 3. 若 count < k，代表答案偏小，需調整左界；否則調整右界。
    /// 4. 對於每個 nums1[i]，若 >=0，nums2[j]*nums1[i] 單調遞增，直接二分找 <=v 的個數；
    ///    若 <0，nums2[j]*nums1[i] 單調遞減，二分找 >v 的個數，答案為 n2-t。
    /// 5. 綜合所有 nums1[i] 統計即可。
    /// 
    /// 時間複雜度：O((n1+n2) * logM * logN)，M 為乘積區間範圍，N 為 nums2 長度。
    /// 空間複雜度：O(1)
    /// </summary>
    /// <param name="nums1">已排序整數陣列</param>
    /// <param name="nums2">已排序整數陣列</param>
    /// <param name="k">第 k 小</param>
    /// <returns>第 k 小乘積</returns>
    public long KthSmallestProduct(int[] nums1, int[] nums2, long k)
    {
        int n1 = nums1.Length;
        long left = -10000000000L, right = 10000000000L;

        // 二分搜尋答案
        while (left <= right)
        {
            long mid = (left + right) / 2;
            long count = 0;

            // 統計所有 nums1[i] 對應小於等於 mid 的乘積數目
            for (int i = 0; i < n1; i++)
            {
                count += F(nums2, nums1[i], mid);
            }

            if (count < k)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }
        return left;
    }
}
