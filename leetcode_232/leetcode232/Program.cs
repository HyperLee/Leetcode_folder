using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode232
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }


    /// <summary>
    /// leetcode 232 
    /// https://leetcode.com/problems/implement-queue-using-stacks/description/
    /// 
    /// https://leetcode.cn/problems/implement-queue-using-stacks/solution/yong-zhan-shi-xian-dui-lie-by-leetcode-s-xnb6/
    /// </summary>
    public class MyQueue
    {
        Stack<int> inStack;
        Stack<int> outStack;

        public MyQueue()
        {
            inStack = new Stack<int>();
            outStack = new Stack<int>();
        }

        public void Push(int x)
        {
            inStack.Push(x);
        }

        public int Pop()
        {
            if (outStack.Count == 0)
            {
                In2Out();
            }
            return outStack.Pop();
        }

        public int Peek()
        {
            if (outStack.Count == 0)
            {
                In2Out();
            }
            return outStack.Peek();
        }

        public bool Empty()
        {
            return inStack.Count == 0 && outStack.Count == 0;
        }

        private void In2Out()
        {
            while (inStack.Count > 0)
            {
                outStack.Push(inStack.Pop());
            }
        }
    }
}
