using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_190
{
    internal class Program
    {
        /// <summary>
        /// 190. Reverse Bits
        /// https://leetcode.com/problems/reverse-bits/
        /// 190. 颠倒二进制位
        /// https://leetcode.cn/problems/reverse-bits/
        /// 
        /// 本題目偏好方法 3
        /// 比較好理解邏輯行為
        /// 但是各方法大致上想法都差不多
        /// 只是三 比較詳細拆分各步驟
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            UInt32 n = 00000000000000000000000000000001;
            //Console.WriteLine("方法1: " + reverseBits(n));
            //Console.WriteLine("方法2: " + reverseBits2(n));
            Console.WriteLine("方法3: " + reverseBits3(n));
            Console.ReadKey();
        }


        /// <summary>
        /// ref: 方法一 依據位置顛倒每個bit (由低位開始往高位運算)
        /// https://leetcode.cn/problems/reverse-bits/solutions/1623850/by-stormsunshine-wgw3/
        /// 
        /// https://www.programiz.com/csharp-programming/bitwise-operators
        /// Bitwise Left Shift 乘法 num * 2bits
        /// 42 = 101010 (In Binary)
        /// 42 << 1 = 84 (In binary 1010100)
        /// 42 << 2 = 168 (In binary 10101000)
        /// 42 << 4 = 672 (In binary 1010100000)
        /// 左移是乘法
        /// 
        /// Bitwise Right Shift 除法 floor(num / 2bits)
        /// 42 = 101010 (In Binary)
        /// 42 >> 1 = 21 (In binary 010101)
        /// 42 >> 2 = 10 (In binary 001010)
        /// 42 >> 4 = 2 (In binary 000010)
        /// 
        /// 2 的次方 shift
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>

        public static uint reverseBits(uint n)
        {
            const int BITS = 32;
            uint reversed = 0;

            for (int i = 0, j = BITS - 1; i < BITS; i++, j--)
            {
                uint bit = (n >> i) & 1;
                reversed |= bit << j;
            }

            return reversed;

        }


        /// <summary>
        /// ref: 參考方法一
        /// https://leetcode.cn/problems/reverse-bits/solutions/685436/dian-dao-er-jin-zhi-wei-by-leetcode-solu-yhxz/
        /// 将 n 视作一个长为 32 的二进制串，从低位往高位枚举 n 的每一位，将其倒序添加到翻转结果 rev 中。
        /// 代码实现中，每枚举一位就将 n 右移一位，这样当前 n 的最低位就是我们要枚举的比特位。
        /// 当 n 为 0 时即可结束循环。
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static uint reverseBits2(uint n)
        {
            uint rev = 0;
            for (int i = 0; i < 32 && n > 0; i++)
            {
                rev |= (n & 1) << (31 - i);
                n >>= 1;
            }

            return rev;
        }


        /// <summary>
        /// GPT 解法
        /// 題目輸入資料 
        /// uint: 32 位元
        /// 
        /// 核心思想： 逐位取出原數的二進位位元，並將其移到目標數的對應位置。
        /// 
        /// 程式邏輯說明
        /// 1. result <<= 1：將結果左移一位，預留空間給新位元。
        /// 2. result |= (n & 1)：取得 n 的最低位並將其加入 result。
        /// 3. n >>= 1：將原始數字右移一位，以檢查下一位元。
        /// 4. 重複 32 次：因為我們處理的是 32 位元整數。
        /// 
        /// 例如:
        /// 原始二進位:   00000000000000000000000000000001
        /// 顛倒後的二進位: 10000000000000000000000000000000
        /// Reversed Value: 2147483648
        /// 
        /// 有點類似有些題目計算
        /// 先取 % 取出資料
        /// 在用 / 取出下個位元
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static uint reverseBits3(uint n)
        {
            uint result = 0;

            // 題目輸入是 32 位元, 所以迴圈跑 32 次
            for (int i = 0; i < 32; i++)
            {
                // 左移一位，為下一位預留空間 (為新位元騰出位置)
                result <<= 1;
                // 取得 n 的最低位並加到 result (將 n 的最低位取出，並將其設置為 result 的最低位。)
                result |= (n & 1);
                // 將 n 右移一位，以便下一次取出下一位。
                n >>= 1;                  
            }
            return result;
        }


    }
}
