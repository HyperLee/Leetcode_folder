namespace leetcode_020
{
    internal class Program
    {
        /// <summary>
        /// 20. Valid Parentheses
        /// https://leetcode.com/problems/valid-parentheses/
        /// 20. 有效的括号
        /// https://leetcode.cn/problems/valid-parentheses/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string a = "";
            a = "()[]{}";

            //bool r = false;
            //r = IsValid(a);
            Console.WriteLine("result:" + IsValid(a));
        }


        /// <summary>
        /// https://leetcode.com/problems/valid-parentheses/
        /// Given a string containing just the characters '(', ')', '{', '}', '[' and ']', 
        /// determine if the input string is valid.
        /// An input string is valid if:
        /// 1. Open brackets must be closed by the same type of brackets.
        /// 2. Open brackets must be closed in the correct order.
        /// Note that an empty string is also considered valid.
        /// 
        /// ref: 
        /// 1. Stack.Peek 方法
        ///    https://docs.microsoft.com/zh-tw/dotnet/api/system.collections.stack.peek?view=net-6.0
        ///    
        /// 每當遇到一個 左括號 就會期待 一個右括號 組合成一組
        /// 所以遇到一左括號就 push 一右括號 為一組
        /// 等後續有右括號進來就 pop 出去
        /// 因為括號為偶數
        /// 故最後 stack.count 為0
        /// 就代表true 皆為兩兩一組
        /// 反之false
        /// 
        /// 需要注意 左括號 對 右括號 兩兩一組
        /// 順序大小都需要相同層級才可以
        /// 
        /// 其他方法可以參考
        /// https://ithelp.ithome.com.tw/articles/10217603
        /// https://leetcode.cn/problems/valid-parentheses/solution/you-xiao-de-gua-hao-by-leetcode-solution/
        /// https://leetcode.cn/problems/valid-parentheses/solution/you-xiao-de-gua-hao-by-leetcode-learning-p2qg/
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValid(string s)
        {
            // 字串取每個單字宣告為 char
            Stack<char> _stack = new Stack<char>();

            // 長度需要是 2 的倍數 因為括號是兩個為一對. 必須是偶數
            if (s.Length % 2 != 0)
            {
                return false;
            }

            for (int i = 0; i < s.Length; i++)
            {
                //Console.WriteLine("string s[i]: " + s[i]);

                if (s[i] == '(')
                {
                    _stack.Push(')');
                }
                else if (s[i] == '[')
                {
                    _stack.Push(']');
                }
                else if (s[i] == '{')
                {
                    _stack.Push('}');
                }
                else if (_stack.Count == 0)
                {
                    // 剩餘左括號沒有可以配對
                    return false;
                }
                else if (s[i] == _stack.Peek())
                {
                    // 先前遇到一個左括號, 就push 一個右括號進入stack
                    // 現在遇到右括號與stack裡面相同, 就pop出去
                    _stack.Pop();
                    //Console.WriteLine("Loop i:" + i + ", pop _stack:" + s[i]);
                }
                else
                {
                    return false;
                }

            }

            // 為 0 代表全部括號都是成對的且方向與順序都沒問題
            if (_stack.Count == 0)
            {
                return true;
            }

            return false;
        }
    }
}
