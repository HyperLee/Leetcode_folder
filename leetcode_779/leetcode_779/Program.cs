using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_779
{
    internal class Program
    {
        /// <summary>
        /// 779. K-th Symbol in Grammar
        /// https://leetcode.com/problems/k-th-symbol-in-grammar/?envType=daily-question&envId=2023-10-25
        /// 779. 第K个语法符号
        /// https://leetcode.cn/problems/k-th-symbol-in-grammar/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int n = 2, k = 2;
            Console.WriteLine(KthGrammar(n, k));
            Console.ReadKey();
        }


        /// <summary>
        /// 官方 方法二: 找规律 + 递归
        /// 
        /// https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators
        /// 左移運算子 <<
        /// </summary>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int KthGrammar(int n, int k)
        {
            if (k == 1)
            {
                return 0;
            }

            if (k > (1 << (n - 2)))
            {
                return 1 ^ KthGrammar(n - 1, k - (1 << (n - 2)));
            }

            return KthGrammar(n - 1, k);
        }

    }
}
