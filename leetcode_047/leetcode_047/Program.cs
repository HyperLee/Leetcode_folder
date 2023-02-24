using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_047
{
    class Program
    {
        /// <summary>
        /// leetcode 047
        /// https://leetcode.com/problems/permutations-ii/
        /// https://leetcode.cn/problems/permutations-ii/solution/quan-pai-lie-ii-by-leetcode-solution/
        /// 
        /// https://docs.microsoft.com/zh-tw/dotnet/api/system.collections.ilist?view=net-6.0
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] array1 = new int[] { 1, 1, 2 };
            //PermuteUnique(array1);
            Console.WriteLine(PermuteUnique(array1));
            Console.ReadKey();
        }


        /// <summary>
        /// https://blog.csdn.net/qq_39643935/article/details/78213658
        /// why if(i > 0 && nums[i] == nums[i - 1] && used[i - 1] || used[i]) continue; can avoid duplicates?
        /// https://leetcode.com/problems/permutations-ii/discuss/589917/C-solution
        /// 實際上參考這個
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static IList<IList<int>> PermuteUnique(int[] nums)
        {
            List<IList<int>> res = new List<IList<int>>();
            Array.Sort(nums);
            bool[] used = new bool[nums.Length];
            Backtracking(nums, new List<int>(), res, used);
            return res;
        }

        private static void Backtracking(int[] nums, List<int> list, List<IList<int>> res, bool[] used)
        {
            if (list.Count == nums.Length)
            {
                res.Add(new List<int>(list));
                return;
            }
            else
            {
                for (int i = 0; i < nums.Length; i++)
                {
                    if (i > 0 && nums[i] == nums[i - 1] && used[i - 1] || used[i]) continue;

                    list.Add(nums[i]);
                    used[i] = true;
                    Backtracking(nums, list, res, used);
                    list.RemoveAt(list.Count - 1);
                    used[i] = false;
                }
            }
        }

    }
}
