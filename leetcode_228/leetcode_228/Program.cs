using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_228
{
    internal class Program
    {
        /// <summary>
        /// leetcode_228 Summary Ranges
        /// https://leetcode.com/problems/summary-ranges/
        /// 228. 汇总区间
        /// https://leetcode.cn/problems/summary-ranges/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = new int[] { 0, 1, 2, 4, 5, 7 };
            Console.WriteLine(SummaryRanges(input));
            Console.ReadKey();

        }


        /// <summary>
        /// https://leetcode.cn/problems/summary-ranges/solution/shuang-zhi-zhen-100-miao-dong-by-sweetie-7vo6/
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>

        public static IList<string> SummaryRanges(int[] nums)
        {
            List<string> list = new List<string>();
            // i 初始設定 第一個區間的左邊界
            int i = 0;

            for(int j = 0; j < nums.Length; j++)
            {
                // i固定之後, j在後面跑過一輪. 直到遇到不連續的遞增 nums[j] + 1 != nums[j + 1]
                // 或是 j 走道輸入資料的最右邊邊界 則當前的區間範圍就是[i, j] 寫入 StringBuilder
                if ((j + 1 == nums.Length) || (nums[j] + 1 != nums[j + 1]))
                {

                    StringBuilder sb = new StringBuilder();
                    sb.Append(nums[i]);

                    if(i != j)
                    {
                        sb.Append("->").Append(nums[j]);
                    }

                    list.Add(sb.ToString());
                    // 將 i 指向下一個區間的起始點 j + 1當作新的區間左邊界起始位置 (因i~j已經跑過一次)
                    i = j + 1;
                }
            }

            
            foreach(var value in list)
            {
                Console.WriteLine(value.ToString());
            }
            

            return list;
        }


    }
}
