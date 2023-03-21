using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2348
{
    internal class Program
    {
        /// <summary>
        /// leetcode 2348
        /// https://leetcode.com/problems/number-of-zero-filled-subarrays/
        /// 全 0 子数组的数目
        /// https://leetcode.cn/problems/number-of-zero-filled-subarrays/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums = new int[] { 1, 3, 0, 0, 2, 0, 0, 4 };
            Console.WriteLine(ZeroFilledSubarray(nums));
            Console.ReadKey();
        }



        /// <summary>
        /// 此題目須小心,注意看題目. int 不夠
        /// -109 <= nums[i] <= 109
        /// 考虑每个以 0 结尾的子数组的个数。
        /// 做法：统计连续 0 组成的长度 count，每个 count 可以贡献 count 个子数组。
        /// 
        /// 统计的时候直接相加求和
        /// 可得:每多一个连续的0, 都可以和上一个0所构成的子数组 构成一个新的子数组。
        /// 
        /// https://leetcode.cn/problems/number-of-zero-filled-subarrays/solution/by-endlesscheng-men8/
        /// https://leetcode.cn/problems/number-of-zero-filled-subarrays/solution/c-by-zardily-zy84/
        /// https://leetcode.cn/problems/number-of-zero-filled-subarrays/solution/java-by-kayleh-s3xx/
        /// 
        /// 每個count 都是一個 subarrays 
        /// 利用ans 把全部count都加總起來 就是題目所需
        /// 每多一個連續相鄰的0 都可以跟上一個subarray組合成新的subarray
        /// 
        /// 連續0   subarrays個數
        /// 1   -   1
        /// 2   -   3   (pre + cur  = 1 + 2)
        /// 3   -   6   (pre + cur  = 3 + 3)
        /// 4   -   10  (pre + cur  = 6 + 4)
        /// 5   -   15  (pre + cur  = 10 + 5)
        /// 6   -   21  (pre + cur  = 15 + 6)
        /// 7   -   28  (pre + cur  = 21 + 7)
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>

        public static long ZeroFilledSubarray(int[] nums)
        {
            long ans = 0;
            long count = 0;

            foreach(var num in nums)
            {
                if(num != 0)
                {
                    count = 0;
                }
                else
                {
                    count++;
                    ans += count;
                }
            }

            return ans;

        }
    }
}
