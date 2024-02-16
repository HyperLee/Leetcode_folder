using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1481
{
    internal class Program
    {
        /// <summary>
        /// 1481. Least Number of Unique Integers after K Removals
        /// https://leetcode.com/problems/least-number-of-unique-integers-after-k-removals/?envType=daily-question&envId=2024-02-16
        /// 1481. 不同整数的最少数目
        /// https://leetcode.cn/problems/least-number-of-unique-integers-after-k-removals/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 4, 3, 1, 1, 3, 3, 2 };
            int k = 3;
            Console.WriteLine(FindLeastNumOfUniqueInts(input, k));
            Console.ReadKey();
        }


        /// <summary>
        /// 1. 利用Dictionary統計 輸入的arr中 每個數字以及出現次數
        /// 2. 排序 依據value由小至大
        /// 3. 從value小的開始移除 
        /// 
        /// 需要注意 移除的value加總要小於等於 k
        /// 
        /// k:移除數量(次數)
        /// Key:數字
        /// value:出現次數
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="k"></param>
        /// <returns></returns>

        public static int FindLeastNumOfUniqueInts(int[] arr, int k)
        {
            // 統計 數字以及出現次數
            Dictionary<int, int> dic = new Dictionary<int, int>();
            foreach (int i in arr) 
            {
                if(dic.ContainsKey(i))
                {
                    dic[i]++;
                }
                else
                {
                    dic.Add(i, 1);
                }
            }

            // 排序 由小至大 
            var sortedDict = dic.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            // 移除
            foreach (var item in sortedDict)
            {
                // 比對當下的 value 數值(出現次數)是否小於等於 k
                if (dic[item.Key] <= k)
                {
                    // 扣除 每個value 次數
                    k -= dic[item.Key];
                    // k 扣完之後, 移除該數字
                    sortedDict.Remove(item.Key);
                }
            }
            return sortedDict.Count();

        }

    }
}
