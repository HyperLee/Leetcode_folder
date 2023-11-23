using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1630
{
    internal class Program
    {
        /// <summary>
        /// 1630. Arithmetic Subarrays
        /// https://leetcode.com/problems/arithmetic-subarrays/?envType=daily-question&envId=2023-11-23
        /// 1630. 等差子数组
        /// https://leetcode.cn/problems/arithmetic-subarrays/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums = { 4, 6, 5, 9, 3, 7 };
            int[] l = { 0, 0, 2 };
            int[] r = { 2, 3, 5 };

            //Console.WriteLine(CheckArithmeticSubarrays(nums, l, r));

            var res = CheckArithmeticSubarrays(nums, l, r);
            foreach(bool ans in res)
            {
                Console.WriteLine(ans);
            }

            Console.ReadKey();

        }


        /// <summary>
        /// 先說結論 我看不懂題目意思
        /// L與R 是做什麼用得
        /// 
        /// 官方解法:
        /// https://leetcode.cn/problems/arithmetic-subarrays/solutions/2184153/deng-chai-zi-shu-zu-by-leetcode-solution-pmrp/
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static IList<bool> CheckArithmeticSubarrays(int[] nums, int[] l, int[] r)
        {
            int n = l.Length;
            IList <bool> ans = new List<bool>();
            
            for(int i = 0; i < n; i++)
            {
                int left = l[i], right = r[i];
                int minv = nums[left], maxv = nums[left];

                // 從 L, L + 1 ~ Right; 看起來是題目要求範圍
                // 找出這一輪最大,最小數值!?
                for(int j = left + 1; j <= right; j++)
                {
                    minv = Math.Min(minv, nums[j]);
                    maxv = Math.Max(maxv, nums[j]);
                }

                if(minv == maxv)
                {
                    // 最大最小相同,解題思維會是大家都等差
                    ans.Add(true);
                    continue;
                }

                if((maxv - minv) % (right - left) != 0)
                {
                    // ? 這是什麼案例case
                    ans.Add(false);
                    continue;
                }

                int d = (maxv - minv) / (right - left);
                bool flag = true;
                bool[] seen = new bool[right - left + 1];

                // 看不懂這case 再跑什麼, 應該是解法推出的公式判斷
                for(int j = left; j <= right; j++)
                {
                    if ((nums[j] - minv) % d != 0)
                    {
                        flag = false;
                        break;
                    }

                    int t = (nums[j] - minv) / d;
                    if (seen[t])
                    {
                        flag = false;
                        break;
                    }

                    seen[t] = true;
                }
                ans.Add(flag);
            }
            return ans;
        }

    }
}
