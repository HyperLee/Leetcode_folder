using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_763
{
    class Program
    {
        /// <summary>
        /// leetcode 763
        /// https://leetcode.com/problems/partition-labels/discuss/632893/C-two-pass-solution
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "";
            s = "ababcbacadefegdehijhklij";
            //s = "eccbbbbdec";
            //Console.WriteLine(PartitionLabels((s)));
            PartitionLabels(s);

            Console.ReadKey();
        }


        /// <summary>
        /// The idea is traverse the string twice.
        /// In the first pass, we get the last index of each letter where it ocurrs.
        /// In the second pass, we determine the partition positions, Specifically, when j == end
        /// , all the letters between begin and end will be only included in substring S[begin, end].
        /// 
        /// Time: O(n) where n = s.Length. The algorithm traverses the input string twice.
        /// Space: O(1) since string S contains at most 26 lower case English letters.
        /// 
        /// https://leetcode-cn.com/problems/partition-labels/solution/hua-fen-zi-mu-qu-jian-by-leetcode-solution/
        /// https://www.codeleading.com/article/53382562113/
        /// 
        /// 记录每个字母最后出现的下标 last[]
        /// 初始化分区开始和结束下标start，end
        /// 遍历题目给的字符串，将Math.max(该字母出现的最后一次下标值，end)赋值给end，更新end的值，直到遍历的下标==end，证明找
        /// 到一个分区；更新start的值为end+1
        /// 重复第三步，直到遍历结束
        /// 
        /// Math.Max:
        /// Math.Max(Int32，Int32)：返回兩個32位有符號整數中較大的一個。這裏Int32是int數據類型。
        /// 在C#中，Max()是Math類方法，用於返回兩個指定數字中較大的一個。
        /// </summary>
        /// <param name="S"></param>
        /// <returns></returns>
        public static IList<int> PartitionLabels(string S)
        {
            // last index of each character
            int[] last = new int[26];
            for (int i = 0; i < S.Length; i++)
                last[S[i] - 'a'] = i;

            List<int> res = new List<int>();
            int begin = 0, end = 0;
            for (int j = 0; j < S.Length; j++)
            {
                end = Math.Max(end, last[S[j] - 'a']);
                if (j == end)
                {
                    res.Add(end - begin + 1);
                    begin = end + 1;
                }
            }

            foreach (int item in res)
            {
                //Console.WriteLine(item);
                Console.Write(item + ", ");
            }

            return res;

        }

    }
}
