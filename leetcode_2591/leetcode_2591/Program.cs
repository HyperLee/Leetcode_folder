using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2591
{
    internal class Program
    {
        /// <summary>
        /// leetcode 2591 Distribute Money to Maximum Children
        /// https://leetcode.com/problems/distribute-money-to-maximum-children/
        /// 
        /// 将钱分给最多的儿童
        /// https://leetcode.cn/problems/distribute-money-to-maximum-children/
        /// 
        /// 規則:
        /// 1. 金錢都需要分配完畢
        /// 2. 每人至少分得一塊錢
        /// 3. 不能有人拿到四塊錢
        /// 
        /// 找出最多位能夠分得八塊錢的 人數
        /// 如果都沒有return -1;
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int money = 21;
            int children = 2;

            Console.WriteLine(DistMoney2(money, children));
            Console.ReadKey();
        }

        /// <summary>
        /// https://leetcode.com/problems/distribute-money-to-maximum-children/solutions/3316910/c-easy-and-readable-solution-faster-than-100-less-than-100/
        /// 此解法 還不理解
        /// 
        /// </summary>
        /// <param name="money"></param>
        /// <param name="children"></param>
        /// <returns></returns>

        public static int DistMoney1(int money, int children)
        {
            // 一人至少分得一塊
            money -= children;

            if (money < 0)
            {
                return -1;
            }

            if (money / 7 == children && money % 7 == 0)
            {
                return children;
            }

            // 不理解這case ? 不能有人分到四塊嗎
            if (money / 7 == children - 1 && money % 7 == 3)
            {
                return children - 2;
            }

            //
            return Math.Min(children - 1, money / 7);

        }


        /// <summary>
        /// https://leetcode.cn/problems/distribute-money-to-maximum-children/solution/fen-lei-tao-lun-o1-zuo-fa-by-endlesschen-95ef/
        /// 此解法比較好理解
        /// 
        /// 規則:
        /// 1. 金錢都需要分配完畢
        /// 2. 每人至少分得一塊錢
        /// 3. 不能有人拿到四塊錢
        /// 
        /// 找出最多位能夠分得八塊錢的 人數
        /// 如果都沒有return -1;
        /// </summary>
        /// <param name="money">金錢</param>
        /// <param name="children">人數</param>
        /// <returns></returns>
        public static int DistMoney2(int money, int children)
        {
            // 一人分一塊, 題目要求
            money -= children;

            if(money < 0)
            {
                return -1;
            }

            // 盡量找出 多少人 能夠一人分八塊的算法
            int ans = Math.Min(money / 7, children);

            // 更新 分配完 八塊錢/人  之後剩餘金錢 
            money -= ans * 7;

            // 與 尚未能分到八塊錢的人數
            children -= ans;

            // money > 0 且 每個人都分了, 找一個之前分過的人在分一次 所以要再減少一位
            if (children == 0 && money > 0)
            {
                ans--;
            }

            // 剩餘一個人且避免分到四塊錢, 需要給一位已經分八塊的 再多拿 所以在少一位
            if (children == 1 && money == 3)
            {
                ans--;
            }

            return ans;

        }


    }
}
