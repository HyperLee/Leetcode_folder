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
        /// 
        /// 輸入 n 轉成 二進位
        /// 計算有多少個 bit 是 1
        /// 
        /// 使用 VS 編譯都會過可以執行
        /// 但是 LeetCode
        /// 都不會過, 但是很早以前會過
        /// 感覺是官方有修改過判斷
        /// 都已經有使用 型態轉換了
        /// 但是好像 認不出來 轉換
        /// 
        /// 
        /// 左移右移可以參考 #190
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // 輸入要看成十進位
            uint input = 0000000000000000000000000000002;
            Console.WriteLine("方法1: " + HammingWeight(input));
            Console.WriteLine("方法2: " + HammingWeight2(input));
            Console.ReadKey();
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/number-of-1-bits/solutions/672082/wei-1de-ge-shu-by-leetcode-solution-jnwf/
        /// 檢查第 i 位置時候
        /// 可以讓 n 與 2^i 進行運算比對
        /// 只要第 i 位置為 1 結果不為 0
        /// 就代表第 i 位置不為空
        /// 
        /// 1 << i
        /// => 由左往右看
        /// 左移幾次
        /// 1 << 1 => 2,  2^1
        /// 1 << 2 => 4,  2^2
        /// 1 << 3 = >8,  2^3
        /// 前面 1 代表是 2 的次方
        /// 後面 數字 代表是 幾次方
        /// 
        /// uint 是 32 位元.
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int HammingWeight(uint n)
        {
            int ret = 0;
            for(int i = 0; i < 32; i++)
            {
                // 判斷第 i 位置 是不是 1
                if((n & (1 << i)) != 0)
                {
                    ret++;
                }
            }

            return ret;
        }


        /// <summary>
        /// https://leetcode.cn/problems/number-of-1-bits/solutions/2361978/191-wei-1-de-ge-shu-wei-yun-suan-qing-xi-40rw/
        /// 這方法算好理解
        /// 根据 与运算 定义，设二进制数字 n ，则有：
        /// 若 n & 1 = 0 ，则 n 二进制 最右一位 为 0 。
        /// 若 n & 1 = 1 ，则 n 二进制 最右一位 为 1 。
        /// 
        /// 根據上述描述來判斷 輸入資料
        /// 1. 判断 n 最右一位是否为 1 ，根据结果计数。
        /// 2. 将 n 右移一位（本题要求把数字 n 看作无符号数，因此使用 无符号右移 操作）。
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int HammingWeight2(uint n)
        {
            int res = 0;
            for (int i = 0; i < 32; i++)
            {
                // 判斷最右邊位置 是不是 1
                res += (int)(n & 1);
                // n 往右移動準備下一輪判斷
                n >>= 1;
            }

            return res;

        }


    }
}
