using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_137
{
    internal class Program
    {
        /// <summary>
        /// 137. Single Number II
        /// https://leetcode.com/problems/single-number-ii/
        /// 137. 只出现一次的数字 II
        /// https://leetcode.cn/problems/single-number-ii/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 2, 2, 3, 2 };
            Console.WriteLine(SingleNumber(input));
            Console.ReadKey();
        }


        /// <summary>
        /// 利用  Dictionary去紀錄 每個數字 出現的頻率
        /// 最後找出 只出現一次的就好
        /// 
        /// 印象中可以用 bit operation方法
        /// 忽然想不起來 下次有遇到再試試看
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>

        public static int SingleNumber(int[] nums)
        {
            int res = 0;
            Dictionary<int, int> dic = new Dictionary<int, int>();

            // 統計每個num出現的次數
            for(int i = 0; i < nums.Length; i++) 
            {
                if (dic.ContainsKey(nums[i])) 
                {
                    dic[nums[i]]++;
                }
                else
                {
                    // 預設的 value 要給一次 不要給 i
                    dic.Add(nums[i], 1);
                }
            }

            // 找出只出現一次的 輸出
            foreach(var item in dic) 
            {
                if(item.Value == 1)
                {
                    res = item.Key;
                }
            }

            return res;

        }


    }
}
