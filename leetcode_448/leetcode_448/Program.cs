using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_448
{
    internal class Program
    {
        /// <summary>
        /// leetcode 448 Find All Numbers Disappeared in an Array
        /// https://leetcode.com/problems/find-all-numbers-disappeared-in-an-array/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] array = {3,2,1};

            //Console.WriteLine(FindDisappearedNumbers(array));
            FindDisappearedNumbers(array);
            Console.ReadKey();
        }


        /// <summary>
        /// https://ithelp.ithome.com.tw/articles/10225673
        /// 
        /// list 基本用法 知識
        /// https://www.ruyut.com/2021/12/c-list.html
        /// 
        /// 鴿巢原理 鴿籠原理
        /// 1~n的位置表示1~n个笼子，如果出现过，相应的“鸽笼”就会被占掉，我们将数字置为负数表示被占掉了。 最
        /// 后再遍历一遍，如果“鸽笼”为正数就是没出现的数字。
        /// https://zh.wikipedia.org/zh-tw/%E9%B4%BF%E5%B7%A2%E5%8E%9F%E7%90%86
        /// 
        /// 回傳 index+1 則是 array index 起始為 0，List數字起始為 1
        /// 
        /// 數字範圍存在於 [1,n]
        /// 
        /// 取每個數字的index做標記, 以及後續答案 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static IList<int> FindDisappearedNumbers(int[] nums)
        {
            List<int> result = new List<int>();
            int numsLength = nums.Length;

            // 存在於nums的數字index, 標記為 負數
            // array從0開始 故要 減一
            for (int i = 0; i < numsLength; i++)
            {
                int index = Math.Abs(nums[i]) - 1;
                if (nums[index] > 0)
                {
                    nums[index] = -nums[index];
                }
            }

            // 沒有被標記成負數的 index，就是消失 的數字，所以將index+1 Add 到 result
            for (int i = 0; i < numsLength; i++)
            {
                if (nums[i] > 0)
                {
                    result.Add(i + 1);
                }
            }

            // List 輸出
            foreach (var str in result)
            {
                Console.WriteLine("欠缺: " + str);
            }

            return result;
        }

    }
}
