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
            int[] nums = { 1, 1, 3};

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
            // errorNums[0]: 重覆的數字, errorNums[1]: 消失的數字
            int[] errorNums = new int[2];
            int n = nums.Length;

            // Key: 輸入的數字, Value: 該數字出現次數
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            foreach(int num in nums)
            {
                // dictionary裡面不存在的 num 初始值給 1
                if (!dictionary.ContainsKey(num))
                {
                    dictionary.Add(num, 1);
                }
                else
                {
                    // 已經出現過 num 的就累加次數
                    dictionary[num]++;
                }
            }

            for(int i = 1; i <= n; i++)
            {
                int count = 0;

                // 找出 i 是否存在, 存在輸出true
                dictionary.TryGetValue(i, out count);

                if(count == 2)
                {
                    // 出現兩次代表重覆了
                    errorNums[0] = i;
                }
                else if(count == 0)
                {
                    // 出現0次代表為 消失的那個數字
                    errorNums[1] = i;
                }
            }

            // print array data
            Console.Write("[ ");
            foreach (var s in errorNums)
            {
                Console.Write(s + ", ");
            }
            Console.Write("]");

            return errorNums;

        }


    }
}
