using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1512
{
    internal class Program
    {
        /// <summary>
        /// 1512. Number of Good Pairs
        /// https://leetcode.com/problems/number-of-good-pairs/?envType=daily-question&envId=2023-10-03
        /// 1512. 好数对的数目
        /// https://leetcode.cn/problems/number-of-good-pairs/
        /// 
        /// 官方是採用 桶排序, 不過沒聽過.
        /// 之後再研究
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 1, 2, 3, 1, 1, 3 };
            Console.WriteLine(NumIdenticalPairs2(input));
            Console.ReadKey();
        }


        /// <summary>
        /// 單純暴力法
        /// 兩個迴圈跑全部組合
        /// 枚舉所有可能
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int NumIdenticalPairs(int[] nums)
        {
            int ans = 0;

            for(int i = 0; i < nums.Length; i++) 
            {
                for(int j = i + 1; j < nums.Length; j++) 
                {
                    if (nums[i] == nums[j])
                    {
                        ans++;
                    }
                }
            }

            return ans;
        }


        /// <summary>
        /// 方法2
        /// https://leetcode.cn/problems/number-of-good-pairs/solutions/332178/xian-yu-de-cha-xi-biao-by-0zufqimwul/
        /// 
        /// dic[i] - 1 ==> 枚舉排列組合次數
        /// 
        /// 此方法很神奇 沒有想過可以這樣解題
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int NumIdenticalPairs2(int[] nums)
        {
            int res = 0;
            Dictionary<int, int> dic = new Dictionary<int, int>();

            foreach(int i in nums) 
            {
                if(dic.ContainsKey(i))
                {
                    // 遇到相同的i就累加
                    dic[i]++;
                    // 統計 組合 
                    res += dic[i] - 1;
                }
                else
                {
                    // 初始化
                    dic.Add(i, 1);
                }
            }

            return res;
        }

    }
}
