using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_1897
{
    internal class Program
    {
        /// <summary>
        /// 1897. Redistribute Characters to Make All Strings Equal
        /// https://leetcode.com/problems/redistribute-characters-to-make-all-strings-equal/?envType=daily-question&envId=2024-01-02
        /// 
        /// 1897. 重新分配字符使所有字符串都相等
        /// https://leetcode.cn/problems/redistribute-characters-to-make-all-strings-equal/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string[] input = { "abc", "aabc", "bc"};
            Console.WriteLine(MakeEqual(input));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/redistribute-characters-to-make-all-strings-equal/solutions/914914/c-tong-ji-mei-ge-zi-mu-ji-shu-by-bloodbo-uxhe/
        /// https://leetcode.cn/problems/redistribute-characters-to-make-all-strings-equal/solutions/826105/zhong-xin-fen-pei-zi-fu-shi-suo-you-zi-f-r29g/
        /// 
        /// 
        /// 我们可以任意进行移动字符的操作。因此，假设 words 的长度为 n，我们只需要
        /// 使得每种字符的总出现次数能够被 n 整除，即可以存在一种操作，使得操作后所
        /// 有字符串均相等。
        /// 
        /// 解法很有趣, 竟然是以存在就代表至少一種解法
        /// 能被整除,代表至少累計次數是1. 就代表可以移動!?
        /// 或許這樣就能找出一種解法
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>

        public static bool MakeEqual(string[] words)
        {
            // 計算輸入的 words 中全部單字 出現頻率(次數)
            Dictionary<char, int> freq = new Dictionary<char, int>();
            foreach (string word in words) 
            {
                foreach (char c in word) 
                {
                    if(!freq.ContainsKey(c))
                    {
                        // 之前不存在就給1                
                        freq.Add(c, 1);
                    }
                    else
                    {
                        // 存在就累加
                        freq[c]++;
                    }
                }
            }

            // 從freq中累計的每一個字元次數,看能否被words長度整除
            // 只要能被整除代表存在至少一種方法可以使字串相同
            int n = words.Length; // words裡面有幾個字串
            foreach(var kvp in freq)
            {
                if(kvp.Value % n != 0)
                {
                    // 不能整除 代表不行
                    return false;
                }
            }

            return true;

        }
    }
}
