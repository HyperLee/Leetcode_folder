using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_217
{
    internal class Program
    {
        /// <summary>
        /// 217. Contains Duplicate
        /// https://leetcode.com/problems/contains-duplicate/
        /// 217. 存在重复元素
        /// https://leetcode.cn/problems/contains-duplicate/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums = {1, 2, 3, 1};
            Console.WriteLine(ContainsDuplicate2(nums));
            Console.ReadKey();
        }


        /// <summary>
        /// 使用Dictionary 統計每個輸入的 數字 出現的次數(頻率)
        /// 
        /// 有超過2就回傳true
        /// 反之false
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static bool ContainsDuplicate(int[] nums)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();

            foreach (int i in nums) 
            {
                if(dic.ContainsKey(i))
                {
                    dic[i]++;
                }
                else
                {
                    dic.Add(i, 1);
                }
            }

            foreach(KeyValuePair<int, int> kp in dic)
            {
                if(kp.Value >= 2)
                {
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// 此方法較佳
        /// 
        /// 
        /// 只要遇到 加總一到2
        /// 就停止運算
        /// 直接回傳 true
        /// 節省時間
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>

        public static bool ContainsDuplicate2(int[] nums)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();

            for(int i = 0; i < nums.Length; i++) 
            {
                if (dic.ContainsKey(nums[i]))
                {
                    //dic[nums[i]]++;
                    //break;
                    return true;
                }
                else
                {
                    dic.Add(nums[i], 1);
                }
            }

            foreach (KeyValuePair<int, int> kp in dic)
            {
                if (kp.Value >= 2)
                {
                    return true;
                }
            }

            return false;

        }

    }
}
