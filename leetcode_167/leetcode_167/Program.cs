using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_167
{
    internal class Program
    {
        /// <summary>
        /// 167. Two Sum II - Input Array Is Sorted
        /// https://leetcode.com/problems/two-sum-ii-input-array-is-sorted/
        /// 
        /// 167. 两数之和 II - 输入有序数组
        /// https://leetcode.cn/problems/two-sum-ii-input-array-is-sorted/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums = { 2, 3, 4 };
            var res = TwoSum(nums, 6);
            Console.WriteLine($"[{res[0]},{res[1]}]");
            Console.ReadKey();
        }


        /// <summary>
        /// 從 leetcode 001修改過來
        /// return 結果 + 1 就好
        /// line 52 & line 56 回傳+1
        /// 因001 從0開始
        /// 
        ///  LeetCode 1. Two Sum
        ///  https://leetcode.com/problems/two-sum/
        ///  Given nums = [2, 7, 11, 15], target = 9,
        ///  Because nums[0] + nums[1] = 2 + 7 = 9,
        ///  return [0, 1].
        ///  
        /// https://www.itread01.com/content/1543410439.html
        /// https://ithelp.ithome.com.tw/articles/10217042
        /// ContainsKey
        /// https://vimsky.com/zh-tw/examples/usage/c-sharp-dictionary-containskey-method.html
        /// 
        /// 題目描述看起來 輸入的 陣列 裡面不會出現相同的數值
        /// 所以 Dic 的 value 儲存 index 應該是不會有問題
        /// 
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int[] TwoSum(int[] numbers, int target)
        {
            // key: 輸入數值, Value: index
            Dictionary<int, int> temp = new Dictionary<int, int>();

            for (int i = 0; i < numbers.Length; i++)
            {
                int left = target - numbers[i];

                if (temp.ContainsKey(left))
                {
                    return new int[] { temp[left], i + 1 };
                }

                if (!temp.ContainsKey(numbers[i]))
                {
                    temp.Add(numbers[i], i + 1);
                }
            }

            return null;
        }



    }
}
