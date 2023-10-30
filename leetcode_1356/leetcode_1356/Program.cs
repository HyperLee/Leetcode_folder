using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1356
{
    internal class Program
    {
        /// <summary>
        /// 1356. Sort Integers by The Number of 1 Bits
        /// https://leetcode.com/problems/sort-integers-by-the-number-of-1-bits/?envType=daily-question&envId=2023-10-30
        /// 1356. 根据数字二进制下 1 的数目排序
        /// https://leetcode.cn/problems/sort-integers-by-the-number-of-1-bits/
        /// 
        /// 输入：arr = [0,1,2,3,4,5,6,7,8]
        /// 输出：[0,1,2,4,8,3,5,6,7]
        /// 解释：[0] 是唯一一个有 0 个 1 的数。
        /// [1,2,4,8] 都有 1 个 1 。
        /// [3,5,6] 有 2 个 1 。
        /// [7] 有 3 个 1 。
        /// 按照 1 的个数排序得到的结果数组为 [0,1,2,4,8,3,5,6,7]
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 0, 1, 2, 4, 8 };
            //Console.WriteLine(SortByBits(input));
            var res = SortByBits(input);
            foreach (int bit in res) 
            {
                Console.Write(bit + ", ");
            }

            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/sort-integers-by-the-number-of-1-bits/solutions/1491034/c-by-hhummingg-knh1/
        /// 先升序排序，再依次收集到与1的数目对应的桶中。
        /// 
        /// SortedDictionary
        /// key: 二進制有幾個 1
        /// value: list型態依序存放排序後 塞入的arr[i]
        /// 
        /// ex:
        /// dict = [0, 1]  => 0個1, value是0
        /// dict = [1, 4]  => 4個1, value是1,2,4,8
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static int[] SortByBits(int[] arr)
        {
            Array.Sort(arr);

            var dict = new SortedDictionary<int, List<int>>();

            for(int i = 0; i < arr.Length; i++)
            {
                int key = PopCount2(arr[i]);

                if(dict.ContainsKey(key))
                {
                    dict[key].Add(arr[i]);
                }
                else
                {
                    dict.Add(key, new List<int>() { arr[i] });
                }
            }

            var ret = new int[arr.Length];
            int idx = 0;

            foreach(var kvp in dict)
            {
                foreach(var num in kvp.Value)
                {
                    ret[idx++] = num;
                }
            }

            return ret;
        }


        /// <summary>
        /// 計算 二進制中 1個數有幾個
        ///  01 & 10 => 0
        ///  10 & 00 => 0
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int PopCount(int n)
        {
            int counter = 0;

            while(n > 0)
            {
                counter++;
                n = n & (n - 1);
            }

            return counter;
        }


        /// <summary>
        /// 計算 二進制中 1個數有幾個
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int PopCount2(int n)
        {
            int res = 0;
            while(n != 0)
            {
                res += (n % 2);
                n /= 2;
            }

            return res;
        }

    }
}
