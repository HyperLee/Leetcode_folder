using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1887
{
    internal class Program
    {
        /// <summary>
        /// 1887. Reduction Operations to Make the Array Elements Equal
        /// https://leetcode.com/problems/reduction-operations-to-make-the-array-elements-equal/?envType=daily-question&envId=2023-11-19
        /// 1887. 使数组元素相等的减少操作次数
        /// https://leetcode.cn/problems/reduction-operations-to-make-the-array-elements-equal/
        /// 
        /// 找出最大數 與 下一個 最大數
        /// 讓最大的數值減少與第二大相同
        /// 如果有多個相同最大數取下標最小的開始
        /// 直到輸入的陣列中大家數值都相同
        /// 
        /// 以此計算需要多少步驟
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums = { 5, 1, 3 };
            Console.WriteLine(ReductionOperations(nums));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/reduction-operations-to-make-the-array-elements-equal/solutions/815375/shi-shu-zu-yuan-su-xiang-deng-de-jian-sh-lt55/
        /// 我们用 cnt 统计每个元素所需的操作次数
        /// 先將nums排序,從小至大
        /// 這樣第一個就不需要減少
        /// 
        /// 排序後:
        /// 1, 3, 5
        /// 
        /// 1跟3比需要一次
        /// 3跟5對比需要兩次 因3, 5先比之後還要再跟前面的1在比一次 所以是2次
        /// 總共為 1 + 2 = 3次
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int ReductionOperations(int[] nums)
        {
            Array.Sort(nums);

            // 每個元素操作次數
            int cnt = 0;
            // 總操作次數
            int res = 0;
            int n = nums.Length;

            // 因已經排序過, 0最小. 所以從1開始
            for(int i = 1; i < n; i++)
            {
                if (nums[i] != nums[i - 1])
                {
                    // 前後不同,就需要降低
                    cnt++;
                }

                //累計總共次數
                res += cnt;
            }

            return res;

        }

    }
}
