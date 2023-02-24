using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1833
{
    internal class Program
    {
        /// <summary>
        /// leetcode 1833
        /// https://leetcode.com/problems/maximum-ice-cream-bars/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] cost = { 1, 3, 2, 4, 1 };

            Console.WriteLine(MaxIceCream(cost, 7));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/maximum-ice-cream-bars/solution/xue-gao-de-zui-da-shu-liang-by-leetcode-ia3m7/
        /// 方法一：排序 + 贪心
        /// 贪心的解法：对数组 costscosts 排序，然后按照从小到大的顺序遍历数组元素，对于每个
        /// 元素，如果该元素不超过剩余的硬币数，则将硬币数减去该元素值，表示购买了这支雪糕，当遇到
        /// 一个元素超过剩余的硬币数时，结束遍历，此时购买的雪糕数量即为可以购买雪糕的最大数量。
        /// </summary>
        /// <param name="costs"></param>
        /// <param name="coins"></param>
        /// <returns></returns>
        public static int MaxIceCream(int[] costs, int coins)
        {
            Array.Sort(costs);
            int count = 0;
            int n = costs.Length;
            for (int i = 0; i < n; i++)
            {
                int cost = costs[i];
                if (coins >= cost)
                {
                    coins -= cost;
                    count++;
                }
                else
                {
                    break;
                }
            }
            return count;
        }

    }
}
