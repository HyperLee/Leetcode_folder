using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_744
{
    internal class Program
    {
        /// <summary>
        /// 744. Find Smallest Letter Greater Than Target
        /// https://leetcode.com/problems/find-smallest-letter-greater-than-target/
        /// 
        /// 744. 寻找比目标字母大的最小字母
        /// https://leetcode.cn/problems/find-smallest-letter-greater-than-target/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string[] letters = { "c", "f", "j"};
            string target = "a";

        }


        /// <summary>
        /// https://leetcode.cn/problems/find-smallest-letter-greater-than-target/solution/xun-zhao-bi-mu-biao-zi-mu-da-de-zui-xiao-lhm7/
        /// 
        /// 利用二分法查找
        /// https://blog.csdn.net/m0_50086696/article/details/123353057
        /// 
        /// </summary>
        /// <param name="letters"></param>
        /// <param name="target"></param>
        /// <returns></returns>

        public static char NextGreatestLetter(char[] letters, char target)
        {
            int lenght = letters.Length;

            // target 比 輸入的 letters的最後一個字 還要大 就代表 找不到 直接回傳 第一個字
            if(target >= letters[lenght - 1])
            {
                return letters[0];
            }

            int low = 0;
            int high = lenght - 1;
            while(low <= high) 
            {
                int mid = (high - low) / 2 + low;

                if (letters[mid] > target)
                {
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }

            return letters[low];

        }



    }
}
