using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_491
{
    internal class Program
    {
        /// <summary>
        /// leetcode 491
        /// https://leetcode.com/problems/non-decreasing-subsequences/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] nums = { 4, 6, 7, 7 };

            //Console.WriteLine(FindSubsequences(nums));
            FindSubsequences(nums);
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/non-decreasing-subsequences/solution/491-di-zeng-zi-xu-lie-by-cuteleon/
        /// 深度优先算法/回溯法
        /// 每一层回溯记录追加的最后一个数字到Set，避免这一层发生重复；
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static IList<IList<int>> FindSubsequences(int[] nums)
        {
            var current = new List<int>();
            var result = new List<IList<int>>();
            Backtrack(0, nums, current, result);

            foreach (var a in result)
            {
                Console.WriteLine(a);
            }

            return result;

        }

        public static void Backtrack(int startIndex, int[] nums, List<int> current, List<IList<int>> result)
        {
            if (current.Count >= 2)
            {
                result.Add(new List<int>(current));
            }
            if (startIndex == nums.Length)
            {
                return;
            }

            var set = new HashSet<int>();
            for (int index = startIndex; index < nums.Length; index++)
            {
                if (set.Contains(nums[index]))
                {
                    continue;
                }
                if (current.Count > 0 && current.Last() > nums[index])
                {
                    continue;
                }

                set.Add(nums[index]);
                current.Add(nums[index]);
                Backtrack(index + 1, nums, current, result);
                current.RemoveAt(current.Count - 1);
            }
        }

    }
}
