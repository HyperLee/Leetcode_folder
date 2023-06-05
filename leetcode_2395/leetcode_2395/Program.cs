using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2395
{
    internal class Program
    {
        /// <summary>
        /// leetcode 2395 Find Subarrays With Equal Sum
        /// https://leetcode.com/problems/find-subarrays-with-equal-sum/
        /// 和相等的子数组
        /// https://leetcode.cn/problems/find-subarrays-with-equal-sum/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = new int[] { 4, 2, 4 };
            Console.WriteLine(FindSubarrays(input));
            Console.ReadKey();

        }


        /// <summary>
        /// https://leetcode.cn/problems/find-subarrays-with-equal-sum/solution/he-xiang-deng-de-zi-shu-zu-by-leetcode-s-3945/
        /// 
        /// 用hash儲存 長度為2的加總合
        /// 如果已經存在過 那就可以直接回傳ｔｒｕｅ
        /// 畢竟題目沒要求算出全部幾組,只說重複即可
        /// 故用hash來設計,自動去除 重複
        /// for 迴圈長度 要設計為 n - 1
        /// 共有 n - 1個 相鄰的數字和, 去除重複的話, 理論上不足 n - 1
        /// 代表有出現 重複
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static bool FindSubarrays(int[] nums)
        {
            int n = nums.Length;
            ISet<int> set = new HashSet<int>();

            for(int i = 0; i < n - 1; i++)
            {
                int sum = nums[i] + nums[i + 1];

                //if(!set.Add(sum))
                //{
                //    return true;
                //}

                if(set.Contains(sum) == false)
                {
                    set.Add(sum);
                }
                else
                {
                    return true;
                }

            }

            return false;
        }


        /// <summary>
        /// 利用 Dictionary 來判斷
        /// 儲存已經 加總過後的 總和
        /// 遇到 下一組 已經存在
        /// 代表已經有兩組 了
        /// 為重複
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>

        public static bool FindSubarrays2(int[] nums)
        {
            Dictionary<int, bool> dic = new Dictionary<int, bool>();

            for(int i = 0; i < nums.Length - 1; i++)
            {
                int sum = nums[i] + nums[i + 1];

                if(dic.ContainsKey(sum))
                {
                    return true;
                }
                else
                {
                    dic.Add(sum, true);
                }
            }

            return false;
        }


    }
}
