using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_070
{
    class Program
    {
        /// <summary>
        /// 70. Climbing Stairs
        /// https://leetcode.com/problems/climbing-stairs/submissions/
        /// 70. 爬楼梯
        /// https://leetcode.cn/problems/climbing-stairs/
        /// 
        /// 本題目推薦, 解法三
        /// 易懂好寫
        /// 類似 fibonacci 優化解法
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int n = 10;

            Console.WriteLine("Method1, total step:" + ClimbStairs(n));
            Console.WriteLine("Method2, total step:" + ClimbStairs2(n));
            Console.WriteLine("Method3, total step:" + ClimbStairs3(n));
            
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.com/problems/climbing-stairs/
        /// LeetCode 70. Climbing Stairs
        /// https://zh.wikipedia.org/wiki/%E6%96%90%E6%B3%A2%E9%82%A3%E5%A5%91%E6%95%B0
        /// 1:  1
        /// 2: 1+1, 2
        /// 3: 1+1+1, 1+2, 2+1
        /// 4: 1+1+1+1, 1+2+1, 1+1+2, 2+1+1, 2+2
        /// 5: 1+1+1+1+1, 1+1+1+2, 1+1+2+1, 1+2+1+1, 2+1+1+1, 2+2+1, 2+1+2, 1+2+2
        /// 6: 1+1+1+1+1+1, 1+1+1+1+2, 1+1+1+2+1, 1+1+2+1+1, 1+2+1+1+1, 2+1+1+1+1, 1+1+2+2, ...
        /// 類似費式數列
        /// f(n-1) + f(n-2)
        /// 為了減少複雜度
        /// 找出 公式 直接利用
        /// 黃金比例恆等式解法
        /// 黃金比例恆等式解法
        /// 黃金比例恆等式解法
        /// 因此得到 F_n的一般式：
        /// 
        /// https://leetcode.cn/problems/climbing-stairs/solution/pa-lou-ti-by-leetcode-solution/
        /// 套公式
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int ClimbStairs(int n)
        {
            double a1 = 1 / Math.Sqrt(5);
            double b2 = Math.Pow((1 + Math.Sqrt(5)) / 2, n + 1);
            double c3 = Math.Pow((1 - Math.Sqrt(5)) / 2, n + 1);
            int fx = (int)(a1 * (b2 - c3));
            return fx;
        }


        /// <summary>
        /// 遞迴
        /// 當輸入很大時候, 要跑很久
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int ClimbStairs2(int n)
        {
            if(n <= 2)
            {
                return n;
            }

            return ClimbStairs2(n - 1) + ClimbStairs2(n - 2);
        }


        /// <summary>
        /// 方法3
        /// 比方法2 遞迴方法 優化
        /// 在 n 非常大時候 比較明顯
        /// 也比單純公式推理簡單
        /// 
        /// 此方法原先用來解 fibonacci
        /// 也可以用來這題目使用
        /// 類似情境
        /// 
        /// 要小心迴圈計算開始位置
        /// i 從 2 開始, 如果從 3 開始就會計算不到 3 的答案了
        /// 迴圈是從前一個開始計算
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int ClimbStairs3(int n)
        {
            if(n <= 2)
            {
                return n;
            }

            int result = 0;
            int pre = 1;
            int next = 2;

            // i 從 2 開始, 需要注意
            for (int i = 2; i < n; i++)
            {
                result = pre + next;
                pre = next;
                next = result;
            }

            return result;
        }

    }
}
