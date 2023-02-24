using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1470
{
    internal class Program
    {
        /// <summary>
        /// leetcode 1470 Shuffle the Array
        /// Given the array nums consisting of 2n elements in the form 
        /// [x1,x2,...,xn,y1,y2,...,yn].
        /// Return the array in the form [x1,y1,x2,y2,...,xn,yn].
        /// 
        /// https://leetcode.com/problems/shuffle-the-array/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums = { 2, 5, 1, 3, 4, 7 };
            int n = 3;

            //Console.WriteLine(Shuffle(nums, n));
            Shuffle(nums, n);
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/shuffle-the-array/solution/zhong-xin-pai-lie-shu-zu-by-leetcode-sol-1eps/
        /// 
        /// 交錯寫入
        /// 設定一個新的array ans 大小為兩倍n
        /// 輸入的 array  nums
        /// 前半部為 奇數
        /// 後半部為 偶數
        /// 
        /// 因題目已經說了總大小2n
        /// 故 i + n 為後半部起始點位置
        /// 交叉寫入 即可
        /// 
        /// https://leetcode.cn/problems/shuffle-the-array/solution/by-muse-77-eh8g/
        /// 可以參考 bit operation
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int[] Shuffle(int[] nums, int n)
        {
            int[] ans = new int[2 * n];
            for (int i = 0; i < n; i++)
            {
                ans[2 * i] = nums[i]; //寫入偶數位置
                ans[2 * i + 1] = nums[i + n]; // 寫入奇數位置
            }

            foreach(var a in ans)
            {
                Console.Write(a + ", ");
            }

            return ans;
        }

    }
}
