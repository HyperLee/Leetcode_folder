using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1461
{
    internal class Program
    {
        /// <summary>
        /// leetcode 1461
        /// https://leetcode.com/problems/check-if-a-string-contains-all-binary-codes-of-size-k/
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
        }


        /// <summary>
        /// https://leetcode.com/problems/check-if-a-string-contains-all-binary-codes-of-size-k/discuss/660801/C-Solution
        /// </summary>
        /// <param name="s"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public bool HasAllCodes(string s, int k)
        {
            var hashSet = new HashSet<int>();
            int num = 0;
            for (int i = 0; i < s.Length; i++)
            {
                num = (num * 2) % (1 << k) + (s[i] - '0');
                if (i >= k - 1)
                    hashSet.Add(num);
            }

            return hashSet.Count == (1 << k);
        }

    }
}
