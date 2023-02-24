using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_904
{
    internal class Program
    {
        /// <summary>
        /// leetcode 904
        /// https://leetcode.com/problems/fruit-into-baskets/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] input = { 1, 2, 3, 2, 2 };
            Console.WriteLine(TotalFruit(input));
            Console.ReadKey();
        }

        public static int TotalFruit(int[] fruits)
        {
            int n = fruits.Length;
            Dictionary<int, int> cnt = new Dictionary<int, int>();

            int left = 0, ans = 0;
            for (int right = 0; right < n; ++right)
            {
                cnt.Add(fruits[right], 0);
                ++cnt[fruits[right]];
                while (cnt.Count > 2)
                {
                    --cnt[fruits[left]];
                    if (cnt[fruits[left]] == 0)
                    {
                        cnt.Remove(fruits[left]);
                    }
                    ++left;
                }
                ans = Math.Max(ans, right - left + 1);
            }
            return ans;
        }


    }
}
