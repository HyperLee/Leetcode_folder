using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2870
{
    internal class Program
    {
        /// <summary>
        /// 2870. Minimum Number of Operations to Make Array Empty
        /// https://leetcode.com/problems/minimum-number-of-operations-to-make-array-empty/?envType=daily-question&envId=2024-01-04
        /// 2870. 使数组为空的最少操作次数
        /// https://leetcode.cn/problems/minimum-number-of-operations-to-make-array-empty/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 2, 3, 3, 2, 2, 4, 2, 3, 4 };
            Console.WriteLine(MinOperations(input));
            Console.ReadKey();
        }


        /// <summary>
        /// 解法來源:
        /// 1.
        /// https://leetcode.cn/problems/minimum-number-of-operations-to-make-array-empty/solutions/2585899/2870-shi-shu-zu-wei-kong-de-zui-shao-cao-tmir/
        /// 2.
        /// https://leetcode.com/problems/minimum-number-of-operations-to-make-array-empty/solutions/4501929/beats-99-63-users-c-java-python-javascript-explained/?envType=daily-question&envId=2024-01-04
        /// 
        /// 利用 Dictionary 統計次數 基本上沒啥大問題
        /// 解法重點是 有效率且正確的 計算次數
        /// 
        /// 次數 = count / 3 可以理解
        /// 比較不懂得是 count + 2 
        /// 看討論區 大家都這樣寫
        /// 
        /// 解法2比較好理解
        /// 能被3 or 3的倍數 移除的就直接增加次數
        /// 不能被3移除的就額外增加移動次數
        /// count % 3 也就三種結果 
        /// 1.ex: 3 % 3 = 0
        /// 2.ex: 4 % 3 = 1
        /// 3.ex: 5 % 3 = 2
        /// 範例1就 除法
        /// 範例2, 3 就是 if 那個條件 能被2移除 所以 再增加次數
        /// 
        /// 
        /// 題目要求能被2, 3移除
        /// 所以小於 2, 3的也就是 1 就直接回傳 -1
        /// 
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int MinOperations(int[] nums)
        {
            int times = 0;
            Dictionary<int, int> counts = new Dictionary<int, int>();
            
            // 統計每個數字出現的頻率(次數)
            foreach (int i in nums) 
            {
                if (counts.ContainsKey(i)) 
                {
                    counts[i]++;
                }
                else
                {
                    counts.Add(i, 1);
                }
            }

            // key: 數字, value:頻率(次數)
            foreach(KeyValuePair<int, int> kvp in counts)
            {
                int count = kvp.Value;
                if(count == 1)
                {
                    // 2, 3才能移除.所以小於2, 3的一定是錯誤的沒辦法移除
                    return -1;
                }

                // 解法1: 推導出來的公式, 詳細看解法說明
                //times += (count + 2) / 3;

                // 解法2: 能被3 or 3的倍數移除的
                times += count / 3;
                if(count % 3 != 0)
                {
                    // 不能被3移除, 再增加一次移動次數
                    times++;
                }

            }

            return times;

        }


    }
}
