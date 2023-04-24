using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1046
{
    internal class Program
    {
        /// <summary>
        /// 1046. Last Stone Weight
        /// https://leetcode.com/problems/last-stone-weight/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = new int[] { 2, 7};

            LastStoneWeight(input);
            Console.ReadKey();
        }


        /// <summary>
        /// 卡關中
        /// 看別人寫法 PriorityQueue 會編譯錯誤
        /// 不確定是不是.NET版本問題
        /// </summary>
        /// <param name="stones"></param>
        /// <returns></returns>
        public static int LastStoneWeight(int[] stones)
        {
            Array.Sort(stones);

            int a = 0;
            int b = 0;
            for(int i = stones.Length - 1; i >= 1; i--)
            {
                a = stones[i];

                for (int j = i - 1; j >= 0; j--)
                {
                    b = stones[j];

                    if(a == b)
                    {
                        int indexToRemove = a;
                        stones = stones.Where((source, index) => index != indexToRemove).ToArray();

                        int indexToRemove2 = b;
                        stones = stones.Where((source, index) => index != indexToRemove2).ToArray();
                    }
                    else if(a != b)
                    {
                        int c = a - b;

                        // 刪除 x 石頭
                        int indexToRemove = b;
                        stones = stones.Where((source, index) => index != indexToRemove).ToArray();

                        stones.Append(c);
                    }
                }
            }

            foreach(int i in stones)
            {
                Console.WriteLine(i);
            }


            return 0;
        }


    }
}
