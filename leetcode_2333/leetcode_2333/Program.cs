namespace leetcode_2333;

class Program
{
    /// <summary>
    /// 2333. Minimum Sum of Squared Difference
    /// https://leetcode.com/problems/minimum-sum-of-squared-difference/description/
    /// 2333. 最小差值平方和
    /// https://leetcode.cn/problems/minimum-sum-of-squared-difference/description/
    ///
    /// English:
    /// You are given two positive 0-indexed integer arrays nums1 and nums2, both of length n.
    ///
    /// The sum of squared difference of arrays nums1 and nums2 is defined as the sum of
    /// (nums1[i] - nums2[i])^2 for each 0 &lt;= i &lt; n.
    ///
    /// You are also given two positive integers k1 and k2. You can modify any of the elements
    /// of nums1 by +1 or -1 at most k1 times. Similarly, you can modify any of the elements
    /// of nums2 by +1 or -1 at most k2 times.
    ///
    /// Return the minimum sum of squared difference after modifying array nums1 at most
    /// k1 times and modifying array nums2 at most k2 times.
    ///
    /// Note: You are allowed to modify the array elements to become negative integers.
    ///
    /// 繁體中文：
    /// 給定兩個長度皆為 n、索引從 0 開始的正整數陣列 nums1 和 nums2。
    ///
    /// 陣列 nums1 和 nums2 的差值平方和，定義為所有滿足 0 &lt;= i &lt; n 的索引 i
    /// 所對應的 (nums1[i] - nums2[i])^2 之總和。
    ///
    /// 另給定兩個正整數 k1 和 k2。你可以對 nums1 中任意元素進行 +1 或 -1 的修改，
    /// 總共最多操作 k1 次。同樣地，你可以對 nums2 中任意元素進行 +1 或 -1 的修改，
    /// 總共最多操作 k2 次。
    ///
    /// 回傳在對 nums1 最多操作 k1 次、對 nums2 最多操作 k2 次後，能得到的最小差值平方和。
    ///
    /// 注意：允許將陣列元素修改為負整數。
    ///
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 方法一：贪心
    /// 
    /// </summary>
    /// <param name="nums1"></param>
    /// <param name="nums2"></param>
    /// <param name="k1"></param>
    /// <param name="k2"></param>
    /// <returns></returns>
    public long MinSumSquareDiff(int[] nums1, int[] nums2, int k1, int k2)
    {
        int n = nums1.Length;
        long k = (long)k1 + k2;
        long ans = 0;
        long sum = 0;

        for(int i = 0; i < n; i++)
        {
            nums1[i] = Math.Abs(nums1[i] - nums2[i]);
            sum += nums1[i];
            ans += (long)nums1[i] * nums1[i];
        }

        if(sum <= k)
        {
            // 所有差值都可以被消除，最小差值平方和為 0
            return 0;
        }

        Array.Sort(nums1);
        for (int i = n - 1; ; i--)
        {
            int m = n - i;
            long v = nums1[i];
            long c = m *(v - (i > 0 ? nums1[i - 1] : 0));
            ans -= v * v;
            if(c < k)
            {
                k -= c;
                continue;
            }

             // 將目前最大的 m 個差值平均降低
             v -= k / m;
             long remainder = k % m;

             return ans + remainder * (v - 1) * (v - 1) + (m - remainder) * v * v;
        }
    }

    /// <summary>
    /// 方法二：二分答案
    /// 
    /// </summary>
    /// <param name="nums1"></param>
    /// <param name="nums2"></param>
    /// <param name="k1"></param>
    /// <param name="k2"></param>
    /// <returns></returns>
    public long MinSumSquareDiff2(int[] nums1, int[] nums2, int k1, int k2)
    {
        int n = nums1.Length;
        int k = k1 + k2;
        int maxDiff = 0;
        for(int i = 0; i < n; i++)
        {
            nums1[i] = Math.Abs(nums1[i] - nums2[i]);
            maxDiff = Math.Max(maxDiff, nums1[i]);
        }

        int l = 0, r = maxDiff, res = 0;
        while(l <= r)
        {
            int mid = (l + r) / 2;
            long sum = 0;
            foreach(int num in nums1)
            {
                sum += num > mid ? num - mid : 0;            
            }

            if(sum <= k)
            {
                r = mid - 1;
                res = mid;
            }
            else
            {
                l = mid + 1;
            }
        }

        foreach(int num in nums1)
        {
            if(num > res)
            {
                k -= num - res;
            }
        }

        Array.Sort(nums1);
        long ans = 0;
        for (int i = n - 1; i >= 0; i--) 
        { 
            long diff = Math.Min(nums1[i], res);
            if (k > 0 && diff > 0) {
                diff--;
                k--;
            }
            ans += diff * diff;
        }
        return ans;    
    }
}