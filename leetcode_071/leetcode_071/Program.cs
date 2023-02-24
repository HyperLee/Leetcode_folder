using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_071
{
    class Program
    {
        /// <summary>
        /// leetcode 071 Simplify Path
        /// https://leetcode.com/problems/simplify-path/
        /// https://leetcode-cn.com/problems/simplify-path/
        /// 
        /// https://blog.csdn.net/qq_39643935/article/details/78241171
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string aa = "";
            aa = "/c/b/a";
            Console.Write(SimplifyPath(aa));
            Console.ReadKey();
        }

        public static string SimplifyPath(string path)
        {
            string[] arr = path.Split('/');
            Stack<string> _stack = new Stack<string>();

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != "")
                {
                    if (arr[i] == "..")
                    {
                        if (_stack.Count > 0)
                        {
                            _stack.Pop();
                        }
                    }
                    else if (arr[i] == ".")
                    {

                    }
                    else
                    {
                        _stack.Push(arr[i]);
                    }
                }
            }

            StringBuilder sb = new StringBuilder();

            while (_stack.Count > 0)
            {
                sb.Insert(0, "/" + _stack.Pop());
            }


            // solve corner case like "/../"
            if (sb.Length == 0)
            {
                sb.Append("/");
            }

            return sb.ToString();

        }


    }
}
