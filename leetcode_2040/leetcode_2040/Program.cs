namespace leetcode_2040;

class Program
{
    /// <summary>
    /// 2040. Kth Smallest Product of Two Sorted Arrays
    /// https://leetcode.com/problems/kth-smallest-product-of-two-sorted-arrays/description/?envType=daily-question&envId=2025-06-25
    /// 2040. 兩個有序陣列的第 K 小乘積
    /// https://leetcode.cn/problems/kth-smallest-product-of-two-sorted-arrays/description/?envType=daily-question&envId=2025-06-25
    /// 
    /// 題目描述：
    /// 已知兩個已排序的 0 索引整數陣列 nums1 和 nums2，以及一個整數 k，
    /// 請回傳 nums1[i] * nums2[j] 的第 k 小（1-based）乘積，
    /// 其中 0 <= i < nums1.length 且 0 <= j < nums2.length。
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
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
            // x1 >= 0 時，nums2[j]*x1 單調遞增，找 <= v 的個數
            // x1 < 0 時，nums2[j]*x1 單調遞減，找 > v 的個數
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
