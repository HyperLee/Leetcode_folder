using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_977
{
    internal class Program
    {
        /// <summary>
        /// 977. Squares of a Sorted Array
        /// https://leetcode.com/problems/squares-of-a-sorted-array/description/?envType=daily-question&envId=2024-03-02
        /// 977. 有序数组的平方
        /// https://leetcode.cn/problems/squares-of-a-sorted-array/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { -4, -1, 0, 3, 10 };
            
            var output = SortedSquares(input);
            foreach (int i in output) 
            {
                Console.Write(i + ", ");
            }

            Console.ReadKey();
        }


        /// <summary>
        /// 將輸入的array
        /// 取平方後排序(遞增)
        /// 
        /// Math.Pow(Double, Double) 方法
        /// 傳回具有指定乘冪數的指定數字。
        /// https://learn.microsoft.com/zh-tw/dotnet/api/system.math.pow?view=net-8.0
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int[] SortedSquares(int[] nums)
        {
            int[] input2 = new int[nums.Length];

            // 計算平方
            for(int i = 0; i < nums.Length; i++)
            {
                //input2[i] = (int)Math.Pow(nums[i], 2);
                input2[i] = nums[i] * nums[i];
            }

            // 排序
            Array.Sort(input2);

            return input2;
        }

    }
}
