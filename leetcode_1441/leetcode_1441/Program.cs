using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1441
{
    internal class Program
    {
        /// <summary>
        /// 1441. Build an Array With Stack Operations
        /// https://leetcode.com/problems/build-an-array-with-stack-operations/?envType=daily-question&envId=2023-11-03
        /// 1441. 用栈操作构建数组
        /// https://leetcode.cn/problems/build-an-array-with-stack-operations/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 1, 3 };
            int n = 3;
            //Console.WriteLine(BuildArray2(input, n));
            var res = BuildArray2(input, n);
            foreach(var value in res)
            {
                Console.WriteLine(value);
            }
            Console.ReadKey();

        }


        /// <summary>
        /// 官方
        /// https://leetcode.cn/problems/build-an-array-with-stack-operations/solutions/1890865/yong-zhan-cao-zuo-gou-jian-shu-zu-by-lee-omde/
        /// 
        /// 效率較好
        /// 關鍵就是要得出
        /// 因为 target 中数字是严格递增的，因此只要遍历
        /// target，在 target 中每两个连续的数字 prev 和 number 中插入 number − prev − 1
        /// 个 Push 和 Pop，再多加一个 Push 来插入当前数字即可。
        /// 
        /// 如果想不出來, 只能用方法2
        /// </summary>
        /// <param name="target"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static IList<string> BuildArray(int[] target, int n)
        {
            IList<string> res = new List<string>();
            int prev = 0;
            foreach(int number in target)
            {
                // for迴圈條件 是這解法關鍵
                for(int i = 0; i < number - prev - 1; i++)
                {
                    res.Add("Push");
                    res.Add("Pop");
                }
                res.Add("Push");
                prev = number;
            }
            return res;
        }


        /// <summary>
        /// 方法2
        /// https://leetcode.cn/problems/build-an-array-with-stack-operations/solutions/1893035/by-ac_oier-q37s/
        /// 當n 與target相符合 就 push
        /// 否則只能pop 出去
        /// 
        /// 比較直覺 易懂
        /// 
        /// [1, n] => 嚴格遞增 連續數字
        /// 所以遇到 不要的就pop出去
        /// </summary>
        /// <param name="target"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static IList<string> BuildArray2(int[] target, int n)
        {
            IList<string> ans = new List<string>();
            int m = target.Length;

            // i:[1, n], j = target長度
            for(int i = 1, j = 0; i <= n && j < m; i++)
            {
                ans.Add("Push");

                if (target[j] != i)
                {
                    ans.Add("Pop");
                }
                else
                {
                    // 相同就繼續往下
                    j++;
                }
            }

            return ans;
        }

    }
}
