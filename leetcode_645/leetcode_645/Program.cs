using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_645
{
    internal class Program
    {
        /// <summary>
        /// leetcode 645 Set Mismatch
        /// https://leetcode.com/problems/set-mismatch/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums = { 1, 2, 2, 4};

            FindErrorNums(nums);
            Console.ReadKey();
        }

        /// <summary>
        /// 哈希表 方法
        /// https://leetcode.cn/problems/set-mismatch/solution/cuo-wu-de-ji-he-by-leetcode-solution-1ea4/
        /// 
        /// 重复的数字在数组中出现 2 次，丢失的数字在数组中出现 0次，其余的每个数字在数组中出现 1 次。因此可
        /// 以使用哈希表记录每个元素在数组中出现的次数，然后遍历从 1 到 n 的每个数字，分别找到出现 2 次和出现
        /// 0 次的数字，即为重复的数字和丢失的数字。
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int[] FindErrorNums(int[] nums)
        {
            int[] errorNums = new int[2];
            int n = nums.Length;
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            foreach(int num in nums)
            {
                // dictionary裡面不存在的 num 就+1
                // 一開始一定都不存在, add過後就會有相同的存在數出現
                if (!dictionary.ContainsKey(num))
                {
                    dictionary.Add(num, 1);
                }
                else
                {
                    // num在dictionary已經有值就在加1
                    // dictionary重複該數 + 1
                    dictionary[num]++;
                }
            }

            for(int i = 1; i <=n; i++)
            {
                int count = 0;
                dictionary.TryGetValue(i, out count);
                if(count == 2)
                {
                    errorNums[0] = i;
                }
                else if(count == 0)
                {
                    errorNums[1] = i;
                }
            }

            // print array data
            foreach (var s in errorNums)
            {
                Console.WriteLine(s);
            }

            return errorNums;

        }


    }
}
