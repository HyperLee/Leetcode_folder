using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_997
{
    internal class Program
    {
        /// <summary>
        /// 997. Find the Town Judge
        /// https://leetcode.com/problems/find-the-town-judge/description/?envType=daily-question&envId=2024-02-22
        /// 997. 找到小镇的法官
        /// https://leetcode.cn/problems/find-the-town-judge/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[][] input = new int[][]
            {
                 new int[]{ 1, 3 },
                 new int[]{ 2, 3 }
            };

            /*
            // 宣告一個不規則陣列，其元素是整數陣列
            int[][] jaggedArray = new int[2][];

            // 初始化不規則陣列的元素，每個元素可以有不同的大小
            jaggedArray[0] = new int[2] { 1, 3 };
            jaggedArray[1] = new int[2] { 2, 3 };
            */

            int n = 3;

            Console.WriteLine(FindJudge(n, input));
            Console.ReadKey();
        }



        /// <summary>
        /// 官方解法
        /// https://leetcode.cn/problems/find-the-town-judge/solutions/1162975/zhao-dao-xiao-zhen-de-fa-guan-by-leetcod-0dcg/
        /// 
        /// 想成是一個有向圖
        /// 法官只能接收 , 沒有輸出
        /// 
        /// 圖簡單說可以類似這樣
        /// 所以比如 a 相信 b => a -> b
        ///          c 相信 b => c -> b
        ///  結論    b為法官
        ///  b屬於接收, 不會有指向別人的指標出現 
        /// 
        /// 
        /// trust[i] = [ai, bi]
        /// ai信任bi
        /// 
        /// 解題概念很不錯, 值得思考
        /// 本來想說要用Dictionary去統計.
        /// 但是應該比這麻煩多了
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <param name="trust"></param>
        /// <returns></returns>
        public static int FindJudge(int n, int[][] trust)
        {
            // 入, 接收 被指向者
            int[] indegress = new int[n + 1];
            // 出, 輸出 指向別人
            int[] outdegress = new int[n + 1];

            foreach (int[] edge in trust)
            {
                int x = edge[0], y = edge[1];
                indegress[y]++;
                outdegress[x]++;
            }

            // 法官會接收全部人的信任 故為 n - 1 (扣除自己, indegress[i] == n - 1)
            // 法官不相信別人 故 輸出為 0 (outdegress[i] == 0)
            // 法官 輸出為 0, 接收為 n - 1
            for (int i = 1; i <= n; i++)
            {
                if (indegress[i] == n - 1 && outdegress[i] == 0)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
