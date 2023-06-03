using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1818
{
    internal class Program
    {
        /// <summary>
        /// leetcode 1818. Minimum Absolute Sum Difference
        /// https://leetcode.com/problems/minimum-absolute-sum-difference/
        /// 1818. 绝对差值和
        /// https://leetcode.cn/problems/minimum-absolute-sum-difference/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums1 = new int[] { 1, 7, 5 };
            int[] nums2 = new int[] { 2, 3, 5 };

            Console.WriteLine(MinAbsoluteSumDiff(nums1, nums2));
            Console.ReadKey();

        }



        /// <summary>
        /// https://leetcode.cn/problems/minimum-absolute-sum-difference/solution/jue-dui-chai-zhi-he-by-leetcode-solution-gv78/
        /// 
        /// </summary>
        /// <param name="nums1"></param>
        /// <param name="nums2"></param>
        /// <returns></returns>
        public static int MinAbsoluteSumDiff(int[] nums1, int[] nums2)
        {
            const int MOD = 1000000007;
            int n = nums1.Length;
            int[] rec = new int[n];
            Array.Copy(nums1, rec, n);
            Array.Sort(rec);
            int sum = 0, maxn = 0;

            for (int i = 0; i < n; i++)
            {
                int diff = Math.Abs(nums1[i] - nums2[i]);
                sum = (sum + diff) % MOD;
                int j = BinarySearch(rec, nums2[i]);

                if (j < n)
                {
                    // nums1 比較大
                    maxn = Math.Max(maxn, diff - (rec[j] - nums2[i]));
                }

                if (j > 0)
                {
                    // nums2 比較大
                    maxn = Math.Max(maxn, diff - (nums2[i] - rec[j - 1]));
                }
            }

            return (sum - maxn + MOD) % MOD;
        }

        public static int BinarySearch(int[] rec, int target)
        {
            int low = 0, high = rec.Length - 1;

            if (rec[high] < target)
            {
                return high + 1;
            }

            while (low < high)
            {
                int mid = (high - low) / 2 + low;

                if (rec[mid] < target)
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid;
                }
            }

            return low;
        }

    }
}
