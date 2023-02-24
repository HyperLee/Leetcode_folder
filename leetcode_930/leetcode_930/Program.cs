using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_930
{
    internal class Program
    {
        /// <summary>
        /// leetcode 930  Binary Subarrays With Sum
        /// 和相同的二元子数组
        /// 给你一个二元数组 nums ，和一个整数 goal ，请你统计并返回有多少个和为
        /// goal 的 非空 子数组。
        /// 
        /// 子数组 是数组的一段连续部分
        /// 
        /// https://leetcode.com/problems/binary-subarrays-with-sum/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] num = new int[] { 1, 0, 1, 0, 1 };
            int goal = 2;
            Console.WriteLine(NumSubarraysWithSum(num, goal));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/binary-subarrays-with-sum/solution/he-xiang-tong-de-er-yuan-zi-shu-zu-by-le-5caf/
        /// 方法一
        /// 
        /// https://leetcode.com/problems/binary-subarrays-with-sum/solutions/186800/binary-subarrays-with-sum/
        /// 
        /// Let P[i] = A[0] + A[1] + ... + A[i-1]. Then P[j+1] - P[i] = A[i] + A[i+1] + ... + A[j], 
        /// the sum of the subarray [i, j].
        /// Hence, we are looking for the number of i < j with P[j] - P[i] = S.
        /// 
        /// https://www.cnblogs.com/grandyang/p/12245317.html
        /// 這題 很難理解 可以看 第二 與第三連結 說明
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="goal"></param>
        /// <returns></returns>
        public static int NumSubarraysWithSum(int[] nums, int goal)
        {
            int sum = 0;
            Dictionary<int, int> cnt = new Dictionary<int, int>();
            int res = 0;
            foreach (int num in nums) 
            {
                // 統計 sum 有哪幾個數值 且放到cnt裡面 統計次數
                // key: sum of value, value: 該總和累積之次數
                if(cnt.ContainsKey(sum))
                {
                    cnt[sum]++;
                }
                else
                {
                    cnt.Add(sum, 1);
                }

                sum += num;
                int val = 0;
                // 找到與 cnt裡面 相符合 就 res + 1; 即代表解法之一
                // P[j] - P[i] = S  ==>  sum - goal
                cnt.TryGetValue(sum - goal, out val);
                res += val;
            }

            return res;
        }

    }
}
