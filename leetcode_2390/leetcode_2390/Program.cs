using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_2390
{
    internal class Program
    {
        /// <summary>
        /// leetcode 2390. Removing Stars From a String
        /// https://leetcode.com/problems/removing-stars-from-a-string/
        /// 2390. 从字符串中移除星号
        /// https://leetcode.cn/problems/removing-stars-from-a-string/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string input = "leet**cod*e";

            Console.WriteLine(RemoveStars2(input));
            //RemoveStars3(input);
            Console.ReadKey();
        }



        /// <summary>
        /// https://leetcode.cn/problems/removing-stars-from-a-string/solution/c-jian-dan-mo-ni-by-zardily-6rh1/
        /// 
        /// Char.IsLetter 方法
        /// https://learn.microsoft.com/zh-tw/dotnet/api/system.char.isletter?view=net-7.0
        /// 
        /// RemoveAt
        /// https://www.ruyut.com/2021/12/c-list.html
        /// https://learn.microsoft.com/zh-tw/dotnet/api/system.collections.generic.list-1.removeat?view=net-7.0
        /// https://www.cnblogs.com/xu-yi/p/11026463.html
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RemoveStars(string s)
        {
            var list = new List<char>();
            foreach (var c in s)
            {
                if (char.IsLetter(c))
                {
                    list.Add(c);
                }
                else
                {
                    list.RemoveAt(list.Count - 1);
                }
            }

            //foreach (var str in list)
            //{
            //    Console.WriteLine(str);
            //}
            //return list.ToArray().ToString();


            return new string(list.ToArray());
            
        }


        /// <summary>
        /// 方法2 改用 StringBuilder 
        /// 
        /// StringBuilder.Remove(Int32, Int32) 方法
        /// https://learn.microsoft.com/zh-tw/dotnet/api/system.text.stringbuilder.remove?view=net-7.0
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RemoveStars2(string s)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var c in s) 
            {
                if(c != '*')
                {
                    sb.Append(c);
                }
                else
                {
                    sb.Remove(sb.Length - 1, 1);
                }
            }

            return sb.ToString();
        }


        /// <summary>
        /// stack
        /// 輸出需要反轉 不然順序會錯
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RemoveStars3(string s)
        {
            Stack<char> stack = new Stack<char>();

            foreach(var c in s)
            {
                if (char.IsLetter(c)) 
                {
                    stack.Push(c);
                }
                else
                {
                    stack.Pop();
                }
            }

            //var output = stack.Reverse().ToArray();
            //foreach (var c in output)
            //{
            //    Console.Write(c);
            //}

            return new string(stack.Reverse().ToArray());
        }

    }
}
