using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2670
{
    internal class Program
    {
        /// <summary>
        /// 2670. Find the Distinct Difference Array
        /// https://leetcode.com/problems/find-the-distinct-difference-array/
        /// 2670. 找出不同元素数目差数组
        /// https://leetcode.cn/problems/find-the-distinct-difference-array/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums = new int[] { 1, 2 };

            DistinctDifferenceArray(nums);
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/find-the-distinct-difference-array/solution/on-qian-hou-zhui-fen-jie-pythonjavacgo-b-wssq/
        /// 
        /// https://leetcode.com/problems/find-the-distinct-difference-array/solutions/3495101/c-hashset/
        /// 
        /// 
        /// 老實說看不董解法 用意
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int[] DistinctDifferenceArray(int[] nums)
        {
            int n = nums.Length;
            int[] result = new int[n];
            HashSet<int> distinctElements = new HashSet<int>();

            string aa = nums[n - 1].ToString();
            distinctElements.Add(nums[n - 1]);
            for (int i = n - 2; i >= 0; i--)
            {
                result[i] = -distinctElements.Count;

                string bb = nums[i].ToString();
                distinctElements.Add(nums[i]);
            }

            distinctElements.Clear();

            for (int i = 0; i <= n - 1; i++)
            {
                string bb = nums[i].ToString();
                distinctElements.Add(nums[i]);
                result[i] += distinctElements.Count;
            }

            
            Console.Write("[");
            foreach (int i in result)
            {
                Console.Write(i + ", ");
            }
            Console.Write("]");

            return result;
        }

    }
}
