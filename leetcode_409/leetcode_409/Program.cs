namespace leetcode_409
{
    internal class Program
    {
        /// <summary>
        /// 409. Longest Palindrome
        /// https://leetcode.com/problems/longest-palindrome/description/
        /// 
        /// 409. 最长回文串
        /// https://leetcode.cn/problems/longest-palindrome/description/
        /// 
        /// 題目描述:
        /// 給定一個由小寫或大寫字母組成的字串 s，返回可以使用這些字母"構成"的最長迴文（palindrome）的長度。
        /// 字母是區分大小寫的，例如，「Aa」不被視為一個迴文。
        /// ==> 注意是使用題目提供的字串來組成, 不是找出最長字串
        ///
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "abccccdd";
            Console.WriteLine("res: " + LongestPalindrome(s));
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/longest-palindrome/solutions/156931/zui-chang-hui-wen-chuan-by-leetcode-solution/
        /// https://leetcode.cn/problems/longest-palindrome/solutions/2571176/409-zui-chang-hui-wen-chuan-by-stormsuns-tgye/
        /// 
        /// 迴文（Palindrome）是一種正著讀和反著讀都一樣的字串或數字。換句話說，迴文的內容從左到右和從右到左是一模一樣的。
        /// 特點:
        /// 1. 迴文的結構以中心對稱（中心可以是單個字符或兩個字符之間的間隙）。
        /// 2. 在迴文中，所有字符的出現次數（除了最多一個）必須是偶數次。
        /// 
        /// 解法概念:
        /// 每個 char 文字出現 v 次
        /// 1. 迴文字串左右兩邊分別各放 v / 2 個 char 文字
        ///    所以兩邊加總就會是  (v / 2) * 2 個數量
        /// 2. 如果有 char 文字 只出現一次, 可以放在迴文字串的正中間位置,
        ///    但是注意只能一個.且必須是迴文字串長度為偶數情況下才可以放入.
        ///    簡單說迴文字串中奇數的 char 只能出現一個 char 文字且只能一次
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int LongestPalindrome(string s)
        {
            int[] count = new int[128];

            // 統計每個 char 文字出現次數
            foreach (char c in s)
            {
                count[c]++;
            }

            int res = 0;
            // 計算迴文字串長度
            foreach(int v in count)
            {
                // 迴文字串左右兩邊長度計算
                res += (v / 2) * 2;
                // 奇數 char 文字 + 迴文字串長度為偶數,
                // 將該奇數 char 文字 放入迴文字串正中間位置
                if(v % 2 == 1 && res % 2 == 0)
                {
                    res++;
                }
            }

            return res;
        }
    }
}
