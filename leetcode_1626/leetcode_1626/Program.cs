using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1626
{
    class Player : IComparable<Player>
    {
        public int score;
        public int age;
        public int CompareTo(Player o)
        {
            if (this.age == o.age)
            {
                return this.score - o.score;
            }
            else
            {
                return this.age - o.age;
            }
        }
    }

    internal class Program
    {
        /// <summary>
        /// leetcode 1626 Best Team With No Conflicts
        /// https://leetcode.com/problems/best-team-with-no-conflicts/description/
        /// 
        /// 年紀大 得分就比較高
        /// 年紀小 得分就比較低
        /// 不能出現 年紀大 得分比 年紀小的 還要小 這就是 衝突 有問題的案例
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] scores = { 1, 3, 5, 10, 15 };
            int[] ages = { 1, 2, 3, 4, 5 };

            Console.WriteLine(BestTeamScore(scores, ages));
            Console.ReadKey();


        }


        /// <summary>
        /// https://leetcode.cn/problems/best-team-with-no-conflicts/solution/cpai-xu-tan-xin-dong-tai-gui-hua-by-bac0id/
        /// 排序+贪心+动态规划
        /// 
        /// 把年紀 與得分 排序 由小到大
        /// </summary>
        /// <param name="scores"></param>
        /// <param name="ages"></param>
        /// <returns></returns>
        public static int BestTeamScore(int[] scores, int[] ages)
        {
            int n = scores.Length;
            Player[] a = new Player[n];
            for (int i = 0; i < n; ++i)
            {
                a[i] = new Player()
                {
                    score = scores[i],
                    age = ages[i]
                };
            }
            Array.Sort(a);
            int[] dp = new int[n];
            dp[0] = a[0].score;
            for (int i = 1; i < n; ++i)
            {
                //分数总和
                int sum = 0;
                for (int j = 0; j < i; ++j)
                {
                    //分数比当前者低的
                    if (a[i].score >= a[j].score)
                    {
                        sum = Math.Max(sum, dp[j]);
                    }
                }
                dp[i] = a[i].score + sum;
            }
            return dp.Max();
        }

    }
}
