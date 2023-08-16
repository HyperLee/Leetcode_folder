using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_169
{
    internal class Program
    {
        /// <summary>
        /// 169. Majority Element
        /// https://leetcode.com/problems/majority-element/
        /// 169. 多数元素
        /// https://leetcode.cn/problems/majority-element/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 3, 2, 3 };
            Console.WriteLine(MajorityElement(input));
            Console.ReadKey();
        }


        public static int MajorityElement(int[] nums)
        {
            // 陣列數值 塞入dic 統計 每個num的數量有多少
            Dictionary<int, int> dic = new Dictionary<int, int>();
            foreach (int num in nums) 
            {
                if(dic.ContainsKey(num))
                {
                    dic[num]++;
                }
                else
                {
                    dic.Add(num, 1);
                }
            }

            int ans = 0;
            // 題目要求 要超過 n / 2 數量
            int n = nums.Length / 2;
            //dic.OrderByDescending(x => x.Value);

            foreach (KeyValuePair<int, int> num in dic) 
            {
                if(num.Value > n)
                {
                    ans = num.Key;
                    break;
                }
            }

            return ans;

        }

    }
}
