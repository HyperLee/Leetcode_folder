using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_020
{
    class Program
    {
        static void Main(string[] args)
        {
            string a = "";
            a = "()";

            bool r = false;
            r = IsValid(a);
            Console.WriteLine("the program result:" + r);
            Console.ReadKey();
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
        /// 所以遇到一左就push一右 為一組
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValid(string s)
        {
            Stack<char> _stack = new Stack<char>();
            for(int i = 0; i < s.Length; i++)
            {
                Console.WriteLine("string: " + s[i]);

                if (s.Length % 2 == 1)
                    return false;

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
                    return false;
                }
                else if (s[i] == _stack.Peek())
                {
                    _stack.Pop();
                    Console.WriteLine("Loop i:" + i +", pop _stack:" + s[i]);
                }
                else
                    return false;

            }

            if (_stack.Count == 0)
                return true;

            return false;
        }

    }
}
