using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_260
{
    internal class Program
    {
        /// <summary>
        /// leetcode 260 Single Number III
        /// https://leetcode.com/problems/single-number-iii/
        /// 260. 只出现一次的数字 III
        /// https://leetcode.cn/problems/single-number-iii/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 1, 2, 1, 3, 2, 5 };
            //Console.WriteLine(SingleNumber(input));
            SingleNumber(input);

            Console.ReadKey();

        }

        public static int[] SingleNumber(int[] nums)
        {
            List<int> list = new List<int>();

            Dictionary<int, int> dic = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                if (dic.ContainsKey(nums[i]))
                {
                    dic[nums[i]]++;
                }
                else
                {
                    dic.Add(nums[i], 1);
                }
            }

            foreach (var item in dic)
            {
                if (item.Value == 1)
                {
                    list.Add(item.Key);
                }
            }

            Console.Write("[");
            foreach(var item in list)
            {
                Console.Write(item + ", ");
            }
            Console.Write("]");

            return list.ToArray();
        }

    }
}
