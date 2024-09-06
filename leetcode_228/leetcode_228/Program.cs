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
        /// 228. Summary Ranges
        /// https://leetcode.com/problems/summary-ranges/
        /// 228. 汇总区间
        /// https://leetcode.cn/problems/summary-ranges/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = new int[] { 0, 1, 2, 4, 5, 7 };
            SummaryRanges(input);
            Console.ReadKey();

        }


        /// <summary>
        /// https://leetcode.cn/problems/summary-ranges/solution/shuang-zhi-zhen-100-miao-dong-by-sweetie-7vo6/
        /// 
        /// 題目大概意思:
        /// 題目輸入的陣列已經過排序, 遞增 <可能是連續也有不連續的數字段落>
        /// 要找出連續區間(數字要連續, 不能斷), 將之輸出
        /// 
        /// 使用雙指針解題目
        /// i 指向區間起始位置(右邊界), j 向後遍歷直到不滿足區連續遞增的則當作區間結束(左邊界)
        /// 下一個區間計算 i 指向 j + 1 為區間起始位置(i ~ j 位置已經跑過, 所以從後一個開始), j 則維持上述方式找出 斷掉的位置結束
        /// 如此循環
        /// 
        /// 區間 [a, b] -> a 到 b 為連續遞增數字區間, 不能有斷開的跳過的
        /// 
        /// (j + 1 == nums.Length): 右邊界結束區間, 陣列從 0 開始所以 j + 1 是 nums 的右邊界  
        /// (nums[j] + 1 != nums[j + 1]): 區間內數字是連續遞增的所以 nums[j] + 1 要等同 nums[j + 1]  
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static IList<string> SummaryRanges(int[] nums)
        {
            List<string> res = new List<string>();
            // i 初始設定 第一個區間的左邊界
            int i = 0;

            for(int j = 0; j < nums.Length; j++)
            {
                // i 固定之後, j 在後面遍歷. 直到遇到不連續的遞增 nums[j] + 1 != nums[j + 1]
                // 或是 j 走道輸入資料的最右邊邊界 則當前的區間範圍就是[i, j] 寫入 StringBuilder
                if ((j + 1 == nums.Length) || (nums[j] + 1 != nums[j + 1]))
                {
                    // 將當前區間 [i, j] 寫入結果
                    StringBuilder sb = new StringBuilder();
                    // 區間起始(左邊界) i
                    sb.Append(nums[i]);

                    // 區間起始與結束不同位置需要加上 箭號指示
                    // 區間位置相同就不需要. 題目要求
                    if(i != j)
                    {
                        // 區間結束(右邊界) j
                        sb.Append("->").Append(nums[j]);
                    }

                    // 寫入結果
                    res.Add(sb.ToString());
                    // 將 i 指向下一個區間的起始點 j + 1 當作新的區間左邊界起始位置 (因 i ~ j 已經跑過)
                    i = j + 1;
                }
            }

            // 顯示輸出結果
            foreach(var value in res)
            {
                Console.WriteLine(value.ToString());
            }
            
            return res;
        }


    }
}
