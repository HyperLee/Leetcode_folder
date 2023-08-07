using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 不規則陣列
{
    internal class Program
    {
        /// <summary>
        /// https://learn.microsoft.com/zh-tw/dotnet/csharp/programming-guide/arrays/jagged-arrays
        /// 不規則陣列
        /// 宣告
        /// 取長度
        /// 以及輸出 方式
        /// 
        /// sum: 計算陣列總和
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // 輸入 座標 範例, 也可輸入其他方式 參考MSDN
            int[][] input = new int[][]
{
                 new int[]{ 1, 2 },
                 new int[]{ 2, 3 },
                 new int[]{ 3, 4 },
                 new int[]{ 4, 5 },
                 new int[]{ 5, 6 },
                 new int[]{ 6, 7 }
};

            //Console.WriteLine(input);

            // 長度
            //System.Console.WriteLine(input.Length);

            int sum = 0;
            // 輸出 陣列資料
            for (int i = 0; i < input.Length; i++)
            {
                System.Console.Write("Element({0}): ", i);

                for (int j = 0; j < input[i].Length; j++)
                {
                    System.Console.Write("{0}{1}", input[i][j], j == (input[i].Length - 1) ? "" : " ");
                    sum += input[i][j];
                }
                System.Console.WriteLine();
            }
            Console.WriteLine("sum: " + sum);
            Console.ReadKey();
        }
    }
}
