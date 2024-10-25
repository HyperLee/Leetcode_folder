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
            Console.WriteLine("方法1: " + SingleNumber(input));
            Console.WriteLine("方法2: " + SingleNumber2(input));
            Console.ReadKey();
        }


        /// <summary>
        /// 利用 Dictionary 去紀錄 每個數字 出現的頻率
        /// 最後找出 只出現一次的就好
        /// 
        /// 印象中可以用 bit operation方法
        /// 忽然想不起來 下次有遇到再試試看
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>

        public static int SingleNumber(int[] nums)
        {
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


            /*// 方法1, 遍歷找出只出現一次的 輸出
            foreach(var item in dic) 
            {
                if(item.Value == 1)
                {
                    return item.Key;
                }
            }

            return 0;
            */

            // 方法2,  找出 dic 中 Value 有為 1 者
            if (dic.ContainsValue(1))
            {
                // 取出第一個的 key 值
                return dic.FirstOrDefault(x => x.Value == 1).Key;
            }
            else
            {
                return 0;
            }
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/single-number-ii/solutions/746993/zhi-chu-xian-yi-ci-de-shu-zi-ii-by-leetc-23t6/
        /// 与其设计逻辑函数，不如直接按计数来理解。
        /// 在对应的二进制位做加法，累计到 3 就重置为 0.（相当于模 3 加法）
        /// 比如a[0]表示a这个数字的第0个bit位，用low[0]、high[0]两个bit位表示nums[0..i]中第0个bit位上为1的个数和（模3的结果）
        /// 
        /// 用  high，low  2 个二进制位表示某个二进制位上 1 出现的次数。
        /// high 和 low 都为 1 时，说明有 3 个 1 .归零即可。
        /// 最终 high 必然为 0，而 low 若为 0：表示唯一的数在该位为 0，若 low 为 1：唯一的数在该位为 1
        /// 所以最终 low 即是答案
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int SingleNumber2(int[] nums)
        {
            int low = 0, high = 0;
            for(int i = 0; i < nums.Length; i++)
            {
                // 計數 + 1
                int carry = low & nums[i];
                low ^= nums[i];
                high |= carry;
                // 如果計數 = 3, 重置為 0
                int reset = low ^ high;
                low &= reset;
                high &= reset;
            }

            return low;
        }


    }
}
