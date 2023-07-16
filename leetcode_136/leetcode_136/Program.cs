using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_136
{
    class Program
    {
        /// <summary>
        /// leetcode 136
        /// https://leetcode.com/problems/single-number/
        /// 
        /// 與 leetcode 540 類似  解法共用
        /// https://leetcode.com/problems/single-element-in-a-sorted-array/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] array1 = new int[] { 1, 1, 5, 7, 7 };
            Console.Write(SingleNumber(array1));
            Console.ReadKey();
        }


        /// <summary>
        /// https://ithelp.ithome.com.tw/articles/10219111
        /// 
        /// FirstOrDefault
        /// https://vimsky.com/examples/detail/csharp-ex---Dictionary-FirstOrDefault-method.html
        /// ContainsValue 
        /// https://vimsky.com/zh-tw/examples/usage/c-sharp-dictionary-containsvalue-method.html
        /// 
        /// return dic.FirstOrDefault(x => x.Value == 1).Key;
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int SingleNumber(int[] nums)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();
            foreach (int num in nums)
            {
                if (dic.ContainsKey(num))
                {
                    dic[num] = dic[num] + 1;
                }
                else
                {
                    dic.Add(num, 1);
                }
            }

            if(dic.ContainsValue(1))
            {
                return dic.FirstOrDefault(x => x.Value == 1).Key;
            }
            else
            {
                return 0;
            }

        }

        /// <summary>
        /// 方法2:
        /// 差別在於 輸出方式 不同而已
        /// if(num.Value == 1)
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int SingleNumber2(int[] nums)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();

            for(int i = 0; i < nums.Length; i++)
            {
                if (dic.ContainsKey(nums[i]))
                {
                    dic[nums[i]]++;
                }
                else
                {
                    dic.Add((int)nums[i], 1);
                }
            }

            foreach(var num in dic)
            {
                if(num.Value == 1)
                {
                    return num.Key;
                }
            }

            return 0;
        }

    }
}
