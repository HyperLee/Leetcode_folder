using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_946
{
    class Program
    {
        /// <summary>
        /// leetcode 946
        /// https://leetcode.com/problems/validate-stack-sequences/
        /// 
        /// https://www.codeleading.com/article/53753085013/
        /// https://leetcode-cn.com/problems/validate-stack-sequences/solution/yan-zheng-zhan-xu-lie-by-leetcode/
        /// 官方解法
        /// 
        /// 思路
        /// 所有的元素一定是按顺序 push 进去的，重要的是怎么 pop 出来？
        /// 假设当前栈顶元素值为 2，同时对应的 popped 序列中下一个要 pop 的值也为 2，那就必须立刻把这个值 pop 出来。
        /// 因为之后的 push 都会让栈顶元素变成不同于 2 的其他值，
        /// 这样再 pop 出来的数 popped 序列就不对应了。
        /// 
        /// 算法
        /// 将 pushed 队列中的每个数都 push 到栈中
        /// ，同时检查这个数是不是 popped 序列中下一个要 pop 的值，如果是就把它 pop 出来。
        /// 最后，检查不是所有的该 pop 出来的值都是 pop 出来了。
        /// 
        /// https://dotblogs.com.tw/h091237557/2014/05/23/145236
        /// Stack為後進先出法(first-in last-out)的群集，表示第一個進去的資料，
        /// 反而是最後一個出來的。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] pushed = new int[] { 1, 2, 3, 4, 5 };

            int[] popped = new int[] { 4, 5, 3, 2, 1 };


            //Stack<int> _stack = new Stack<int>();
            //_stack.Push(1);
            //_stack.Push(2);
            //_stack.Push(3);
            //_stack.Push(4);
            //_stack.Push(5);

            Console.WriteLine(ValidateStackSequences(pushed, popped));
            Console.ReadKey();
        }

        public static bool ValidateStackSequences(int[] pushed, int[] popped)
        {
            if (pushed.Length != popped.Length)
                return false;

            if (pushed.Length == 0 || popped.Length == 0)
                return false;

            Stack<int> newstack = new Stack<int>();
            int N = pushed.Length;
            int j = 0;
            for (int i = 0; i < pushed.Length; i++)
            {
                newstack.Push(pushed[i]);
                //while (newstack.Count != 0 && j < N && newstack.Peek() == popped[j])
                while (newstack.Count != 0  && newstack.Peek() == popped[j])
                {
                    // read value
                    string a1 = newstack.Peek().ToString();
                    string a2 = popped[j].ToString();
                    newstack.Pop();
                    j++;
                }
            }
            return N == j;
        }

    }
}
