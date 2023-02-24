using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1338
{
    internal class Program
    {
        /// <summary>
        /// leetcode 1338   Reduce Array Size to The Half  数组大小减半
        /// https://leetcode.com/problems/reduce-array-size-to-the-half/
        /// 
        /// 给你一个整数数组 arr。你可以从中选出一个整数集合，并删除这些整数在数组中的每次出现。
        /// 返回 至少 能删除数组中的一半整数的整数集合的最小大小。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] arr = { 3, 3, 3, 3, 5, 5, 5, 2, 2, 7 };

            Console.WriteLine(MinSetSize(arr));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/reduce-array-size-to-the-half/solution/3chong-fang-fa-qiu-jie-shun-xu-qiu-jie-zi-dian-pai/
        /// 解法二（字典排序）
        /// 思路：基于解法一，先对结果进行排序，然后使用排序后的结果进行统计，按道理解法一更快，但可
        /// 能.NET3.5对Dictionary的排序优化使得效率比自己查询更高。
        /// 时间复杂度：不算排序部分，O(n)
        /// 空间复杂度：O(n)
        /// 
        /// 思路：最简单朴素的解法，首先进行元素统计，然后根据统计的结果按最大到小进行计算，不过当前解法C#
        /// 会在数据大的情况下超时
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static int MinSetSize(int[] arr)
        {
            Dictionary<int, int> r = new Dictionary<int, int>();
            for (int i = 0; i < arr.Length; i++)
            {//数据统计
                if (r.ContainsKey(arr[i]))
                {
                    r[arr[i]] += 1;
                }
                else
                {
                    r[arr[i]] = 1;
                }
            }
            //排序
            var rOrder = from ro in r orderby ro.Value descending select ro;

            int c = 0, ct = 0;
            foreach (KeyValuePair<int, int> item in rOrder)
            {//累计
                c += item.Value;
                ct++;
                if (c * 2 >= arr.Length)
                {
                    return ct;
                }
            }
            return 0;//仅为编译
        
        }



    }
}
