using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_134
{
    internal class Program
    {
        /// <summary>
        /// leetcode 134
        /// https://leetcode.com/problems/gas-station/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] gas = { 1, 2, 3, 4, 5 };
            int[] cost = { 3, 4, 5, 1, 2 };

            Console.WriteLine(CanCompleteCircuit(gas, cost));
            Console.ReadKey();
        }

        /// <summary>
        /// https://leetcode.cn/problems/gas-station/solution/by-stormsunshine-8gdg/
        /// 
        /// 从下标 0 开始，从左到右遍历数组 gas 和 cost，遍历过程中维护起点 start、汽油变化量总和 sum 与当前剩
        /// 余汽油量 remain，初始时 start、sum 与 remain 都是 0。当遍历到下标 i 时，执行如下操作。
        /// 1. 计算 difference=gas[i]−cost[i]，将 sum 和 remain 分别加 difference。
        /// 2. 如果 remain<0，则将 start 更新为 i+1，将 remain 更新为 0。
        /// 遍历结束之后，如果 sum≥0 则返回 start，否则返回 −1。
        /// </summary>
        /// <param name="gas"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        public static int CanCompleteCircuit(int[] gas, int[] cost)
        {
            int start = 0;
            int sum = 0, remain = 0;
            int n = gas.Length;
            for (int i = 0; i < n; i++)
            {
                int difference = gas[i] - cost[i];
                sum += difference;
                remain += difference;
                if (remain < 0)
                {
                    start = i + 1;
                    remain = 0;
                }
            }
            return sum >= 0 ? start : -1;
        }

    }
}
