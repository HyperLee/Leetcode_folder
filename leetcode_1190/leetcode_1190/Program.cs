using System.Text;

namespace leetcode_1190
{
    internal class Program
    {
        /// <summary>
        /// 1190. Reverse Substrings Between Each Pair of Parentheses
        /// https://leetcode.com/problems/reverse-substrings-between-each-pair-of-parentheses/description/?envType=daily-question&envId=2024-07-11
        /// 1190. 反转每对括号间的子串
        /// https://leetcode.cn/problems/reverse-substrings-between-each-pair-of-parentheses/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string s = "(u(love)i)";
            Console.WriteLine(ReverseParentheses(s));
            Console.ReadKey();
        }


        /// <summary>
        /// https://leetcode.cn/problems/reverse-substrings-between-each-pair-of-parentheses/solutions/795515/fan-zhuan-mei-dui-gua-hao-jian-de-zi-chu-gwpv/
        /// https://leetcode.cn/problems/reverse-substrings-between-each-pair-of-parentheses/solutions/1456216/by-stormsunshine-m6lv/
        /// https://leetcode.cn/problems/reverse-substrings-between-each-pair-of-parentheses/solutions/796165/chi-xiao-dou-python-zhan-gua-hao-yu-chu-kxh2t/
        /// 
        /// 括號適成對的
        /// 遇到左括號 代表是一個新的開始
        /// 右括號才會進行字串反轉
        /// 反轉之後把字串加入sb裡面
        /// 
        /// stack: 後進先出
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ReverseParentheses(string s)
        {
            // 暫存資料, 先進後出
            Stack<string> stack = new Stack<string>();
            // 輸出結果
            StringBuilder sb = new StringBuilder();

            foreach (char c in s) 
            {
                if (c == '(')
                {
                    // 遇到左括號, 都是一個新的開始(段落!?)
                    // 放入stack存放
                    stack.Push(sb.ToString());
                    // 清空sb
                    sb.Length = 0;
                }
                else if(c == ')')
                {
                    // 遇到右括號, 表示為一個段落結束
                    // 開始將儲存進sb的字串進行反轉
                    char[] arr = sb.ToString().ToCharArray();
                    // 清空sb
                    sb.Length = 0;

                    for(int i = arr.Length - 1; i >= 0; i--)
                    {
                        // 字串反轉
                        sb.Append(arr[i]);
                    }

                    // 開頭會是 stack.Pop() 取出資料
                    // sb的資料接續Pop()資料的後方
                    sb.Insert(0, stack.Pop());
                }
                else
                {
                    // 左括號後開始把每個char加入sb裡面
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }
    }
}
