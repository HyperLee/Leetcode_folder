using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_219
{
    internal class Program
    {
        /// <summary>
        /// 219. Contains Duplicate II
        /// https://leetcode.com/problems/contains-duplicate-ii/
        /// 219. 存在重复元素 II
        /// https://leetcode.cn/problems/contains-duplicate-ii/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 1, 0, 1, 1 };
            int k = 1;

            Console.WriteLine(ContainsNearbyDuplicate(input, k));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/contains-duplicate-ii/solutions/1218075/cun-zai-zhong-fu-yuan-su-ii-by-leetcode-kluvk/
        /// 
        /// i - dic1[num] <= k
        /// i是迴圈當前第i位置
        /// dic1[num]: 取出 dic中num的存放位置是多少
        /// 運算是否小於等於k
        /// 
        /// 
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static bool ContainsNearbyDuplicate(int[] nums, int k)
        {
            Dictionary<int, int> dic1 = new Dictionary<int, int>();
            int length = nums.Length;

            for(int i = 0; i < length; i++)
            {
                int num = nums[i];

                if(dic1.ContainsKey(num) && i - dic1[num] <= k)
                {
                    return true;
                }

                if(dic1.ContainsKey(num))
                {
                    // value 存放位置以及更新位置
                    dic1[num] = i;
                }
                else
                {
                    // value是存第i位置
                    dic1.Add(num, i);
                }

            }

            return false;

        }

    }
}
