using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_443
{
    internal class Program
    {
        /// <summary>
        /// 443. String Compression
        /// https://leetcode.com/problems/string-compression/?envType=study-plan-v2&envId=leetcode-75
        /// 443. 压缩字符串
        /// https://leetcode.cn/problems/string-compression/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            char[] input = { 'a', 'a', 'b', 'b', 'c'};
            Console.WriteLine(Compress(input));
            Console.ReadKey();
        }


        /// <summary>
        /// 方法1:
        /// 官方解法 雙指針
        /// https://leetcode.cn/problems/string-compression/solutions/948556/ya-suo-zi-fu-chuan-by-leetcode-solution-kbuc/
        /// 
        /// read指針移動到連續子字串最右側, write指針寫入該字符的子字串長度
        /// 
        /// 方法2:
        /// https://leetcode.cn/problems/string-compression/solutions/1780095/by-stormsunshine-bvti/
        /// </summary>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static int Compress(char[] chars)
        {
            int n = chars.Length;
            int write = 0, left = 0;

            for(int read = 0; read < n; read++)
            {
                // 字串結尾, 且下一個位置字符不連續, 代表為該連續字符最右邊
                if(read == n - 1 || chars[read] != chars[read + 1])
                {
                    // read代表連續字串字符
                    chars[write++] = chars[read];
                    // 子字串長度, 壓縮長度
                    int num = read - left + 1;

                    if(num > 1)
                    {
                        int anchor = write;

                        while(num > 0)
                        {
                            // 連續相同字符數量寫入 chars[]裡面
                            chars[write++] = (char)(num % 10 + '0');
                            num /= 10;
                        }

                        Reverse(chars, anchor, write - 1);
                    }
                    // 下一個位置
                    left = read + 1;
                }
            }

            return write;
        }



        /// <summary>
        /// 子字串長度 寫入 chars[]
        /// </summary>
        /// <param name="chars"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public static void Reverse(char[] chars, int left, int right)
        {
            while(left < right)
            {
                char temp = chars[left];
                chars[left] = chars[right];
                chars[right] = temp;

                left++;
                right--;
            }

        }

    }
}
