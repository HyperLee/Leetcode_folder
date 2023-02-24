using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2244
{
    internal class Program
    {
        /// <summary>
        /// leetcode 2244
        /// https://leetcode.com/problems/minimum-rounds-to-complete-all-tasks/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] tasks = {2, 2, 3, 3, 2, 4, 4, 4, 4, 4};

            Console.WriteLine(MinimumRounds(tasks));

            Console.ReadKey();
        }


        /// <summary>
        /// hash map
        /// https://www.delftstack.com/zh-tw/howto/csharp/csharp-hashmap/
        /// https://learn.microsoft.com/zh-tw/dotnet/api/system.collections.generic.dictionary-2?view=net-7.0
        /// 
        /// kvp = KeyValuePair
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public static int MinimumRounds(int[] tasks)
        {
            var map = tasks.GroupBy(x => x).ToDictionary(x => x, x => x.Count());
            var res = 0;
            foreach (var kvp in map)
            {
                if (kvp.Value == 1) return -1;
                res += kvp.Value / 3;
                if (kvp.Value % 3 != 0) res++;
            }
            return res;
        }

    }
}
