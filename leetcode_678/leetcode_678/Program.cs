namespace leetcode_678
{
    internal class Program
    {
        /// <summary>
        /// 678. Valid Parenthesis String
        /// https://leetcode.com/problems/valid-parenthesis-string/description/?envType=daily-question&envId=2024-04-07
        /// 678. 有效的括號字符串
        /// https://leetcode.cn/problems/valid-parenthesis-string/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string input = "(*)";
            Console.WriteLine(CheckValidString(input));
            Console.ReadKey();
        }


        /// <summary>
        /// stack
        /// https://leetcode.cn/problems/valid-parenthesis-string/solutions/992347/you-xiao-de-gua-hao-zi-fu-chuan-by-leetc-osi3/
        /// https://leetcode.cn/problems/valid-parenthesis-string/solutions/2267891/678-you-xiao-de-gua-hao-zi-fu-chuan-by-s-0vx0/
        /// 
        /// 使用兩個stack來判斷
        /// 1.左括號 stack
        /// 2.星號 stack
        /// 3.星號可以當成是左/右括號,也可以是空字串
        /// 
        /// 接下來只要遇到 右括號
        /// 就需要左括號 or 星號來配對
        /// 如果左括號以及星號 都為空, 但是有存在右括號
        /// 那肯定是false
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool CheckValidString(string s)
        {
            Stack<int> leftstack = new Stack<int>();
            Stack<int> startstack = new Stack<int>();
            int n = s.Length;
            for (int i = 0; i < n; i++)
            {
                char c = s[i];
                if (c == '(')
                {
                    // 如果遇到左括號，則將當前index位置存入左括號stack。
                    leftstack.Push(i);
                }
                else if (c == '*')
                {
                    // 如果遇到星號，則將當前index位置存入星號stack。
                    startstack.Push(i);
                }
                else
                {
                    //如果遇到右括號，则需要有一个左括號或星号和右括號匹配
                    if (leftstack.Count > 0)
                    {
                        // 如果左括號栈不为空，则从左括號栈弹出栈顶元素
                        leftstack.Pop();
                    }
                    else if (startstack.Count > 0)
                    {
                        // 如果左括號栈为空且星号栈不为空，则从星号栈弹出栈顶元素
                        startstack.Pop();
                    }
                    else
                    {
                        // 如果左括號栈和星号栈都为空，则没有字符可以和当前的右括號匹配，返回 false。
                        return false;
                    }
                }
            }

            // 題目要求, 左括號必須在右括號的前方才可以, 把星號當成是右括號
            while (leftstack.Count > 0 && startstack.Count > 0)
            {
                int leftindex = leftstack.Pop();
                int startindex = startstack.Pop();
                if (leftindex > startindex)
                {
                    return false;
                }
            }

            return leftstack.Count == 0;
        }
    }
}
