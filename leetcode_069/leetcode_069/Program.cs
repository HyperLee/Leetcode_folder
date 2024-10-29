using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_069
{
    internal class Program
    {
        /// <summary>
        /// 69. Sqrt(x)
        /// https://leetcode.com/problems/sqrtx/description/?envType=study-plan-v2&envId=top-interview-150
        /// 
        /// 69. x 的平方根
        /// https://leetcode.cn/problems/sqrtx/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int x = 4;
            Console.WriteLine(MySqrt(x));

            Console.ReadKey();
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/sqrtx/solution/x-de-ping-fang-gen-by-leetcode-solution/
        /// 方法二：二分查找
        /// 由于 x 平方根的整数部分 ans 是满足 k^2 ≤ x 的最大 k 值
        /// ，因此我们可以对 k 进行二分查找，从而得到答案。
        /// 二分查找的下界为 0，上界可以粗略地设定为 x。
        /// 在二分查找的每一步中，我们只需要比较中间元素 mid 的平方与 x 的大小关系，并通过比较的结果调整上下界的范围。
        /// 由于我们所有的运算都是整数运算，不会存在误差，因此在得到最终的答案 ans 后，也就不需要再去尝试 ans + 1 了。
        /// 
        /// 用二分法去找出中間值 mid
        /// 然後利用數學去計算 平方值與 x 大小差異
        /// 比 x 小就把 mid 加大
        /// 比 x 大就把 mid 減小
        /// 
        /// ex:平方計算
        /// 1 ^ 2 = 1
        /// 2 ^ 2 = 4
        /// 3 ^ 2 = 9
        /// 
        /// 0 <= x <= 2^31 - 1
        /// 輸入範圍滿大的, 轉 long 比較保險. 避免溢位
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int MySqrt(int x)
        {
            int left = 0, right = x, res = -1;
            while (left <= right)
            {
                // 取整數, 不會有小數點
                int mid = left + (right - left) / 2;
                // 型態轉換. 避免溢位
                if ((long)mid * mid <= x)
                {
                    res = mid;
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return res;
        }

    }
}
