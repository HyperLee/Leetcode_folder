using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1925
{
    internal class Program
    {
        /// <summary>
        /// 1925. Count Square Sum Triples
        /// https://leetcode.com/problems/count-square-sum-triples/
        /// 1925. 统计平方和三元组的数目
        /// https://leetcode.cn/problems/count-square-sum-triples/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int n = 5;
            Console.WriteLine(CountTriples(n));
            Console.ReadKey();  
        }


        /// <summary>
        /// https://leetcode.cn/problems/count-square-sum-triples/solution/tong-ji-ping-fang-he-san-yuan-zu-de-shu-dfenx/
        /// 數學題, 老實說 不懂這解法
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int CountTriples(int n)
        {
            int res = 0;
            for(int a = 1; a <= n; a++)
            {
                for(int b = 1; b <= n; b++)
                {
                    int c = (int)Math.Sqrt(a * a + b * b + 1.0);

                    if(c <= n && c * c == a * a + b * b)
                    {
                        res++;
                    }
                }
            }

            return res;
        }


        /// <summary>
        /// 暴力 硬幹 列舉
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int CountTriples2(int n)
        {
            int res = 0;

            for(int a = 1; a<= n; a++)
            {
                for(int b = 1; b <=n; b++)
                {
                    for(int c = 1; c <= n; c++)
                    {
                        if(a * a + b * b == c * c)
                        {
                            res++;
                        }
                    }
                }
            }

            return res;
        }


    }
}
