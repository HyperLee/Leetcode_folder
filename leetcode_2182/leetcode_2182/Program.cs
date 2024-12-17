using System.Text;

namespace leetcode_2182
{
    internal class Program
    {
        /// <summary>
        /// 2182. Construct String With Repeat Limit
        /// https://leetcode.com/problems/construct-string-with-repeat-limit/description/?envType=daily-question&envId=2024-12-17
        /// 
        /// 2182. 构造限制重复的字符串
        /// https://leetcode.cn/problems/construct-string-with-repeat-limit/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "cczazcc";
            int repeatLimit = 3;

            Console.WriteLine("ans: " + RepeatLimitedString(s, repeatLimit));
            //Console.ReadKey();
        }

        const int N = 26;


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/construct-string-with-repeat-limit/solutions/1300982/gou-zao-xian-zhi-zhong-fu-de-zi-fu-chuan-v02s/
        /// https://leetcode.cn/problems/construct-string-with-repeat-limit/solutions/1278723/cong-da-dao-xiao-tan-xin-by-endlesscheng-b7ob/
        /// https://leetcode.cn/problems/construct-string-with-repeat-limit/solutions/2781436/2182-gou-zao-xian-zhi-zhong-fu-de-zi-fu-ow48f/
        /// 
        /// 使用 s 來建立新的 字串 repeatLimitedString 
        /// 每個 char "連續出現" 次數不能超過 repeatLimit
        /// s 中的 char 可以不必全部都使用過
        /// 返回 字典序 最大
        /// 盡量字串長度越長越好, 以及使用 字典序大的 char (a ~ z, 小至大)
        /// 
        /// 貪婪(貪心)演算法 + 雙指針
        /// 
        /// 主要概念:
        /// 每次选择当前剩余的字典序最大的 char (i)加到字符串末尾；如果字符串末尾的 char 已经连续出现了 repeatLimit 次，则将字典序次
        /// 大的 char (j)加到字符串末尾，随后继续选择当前剩余的字典序最大的 char (i)加到字符串末尾
        /// ，直至使用完 char 或没有新的 char 可以合法加入。
        /// 
        /// i 與 j 互換時候 m 重製
        /// 當 持續加入時候 m 累積
        /// </summary>
        /// <param name="s">string 輸入字串</param>
        /// <param name="repeatLimit">integer 限制每個char 使用最大次數</param>
        /// <returns></returns>
        public static string RepeatLimitedString(string s, int repeatLimit)
        {
            int[] count = new int[N];
            foreach (char c in s)
            {
                // 統計每個 char 出現次數(數量)
                count[c - 'a']++;
            }

            // 輸出結果 res
            StringBuilder res = new StringBuilder();
            // 紀錄已填入 res 的 char 連續次數
            int m = 0;
            // 從字典序大的開始找, i: 當前未使用字典序最大 char, j: 當前未使用字典序次大的 char
            for (int i = N - 1, j = N - 2; i >= 0 && j >= 0;)
            {
                if (count[i] == 0)
                {
                    // 最大字典序 i 回合
                    // 當前 char 已經填完, 填入後面的 char, 重置 m
                    m = 0;
                    i--;
                }
                else if (m < repeatLimit)
                {
                    // 當前 char 未超過限制
                    count[i]--;
                    // 轉成 char 加入 res
                    res.Append((char)('a' + i));
                    m++;
                }
                else if (j >= i || count[j] == 0)
                {
                    // i 這邊次數達到上限, 換 j 這邊使用
                    // 當前 char 已經超過限制, 查找可填入的其他 char 
                    j--;
                }
                else
                {
                    // 次大字典序 j 回合
                    // 當前 char 已經超過限制, 填入其他 char , 並且重置 m
                    count[j]--;
                    // 轉成 char 加入 res
                    res.Append((char)('a' + j));
                    m = 0;
                }
            }

            return res.ToString();
        }

    }
}
