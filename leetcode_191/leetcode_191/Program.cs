using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_191
{
    internal class Program
    {
        /// <summary>
        /// 191. Number of 1 Bits
        /// https://leetcode.com/problems/number-of-1-bits/
        /// 191. 位1的个数
        /// https://leetcode.cn/problems/number-of-1-bits/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
        }


        /// <summary>
        /// https://leetcode.cn/problems/number-of-1-bits/solutions/672082/wei-1de-ge-shu-by-leetcode-solution-jnwf/
        /// 檢查第 i 位置時候
        /// 可以讓 n 與 2^i 進行運算比對
        /// 只要第i位置為1 結果不為0
        /// 就代表第i位置不為空
        /// 
        /// bit運算真是我的弱點
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public int HammingWeight(uint n)
        {
            int ret = 0;
            for(int i = 0; i < 32; i++)
            {
                if((n & (1 << i)) != 0)
                {
                    ret++;
                }
            }

            return ret;
        }


    }
}
