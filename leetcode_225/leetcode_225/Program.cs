using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode_225
{
    internal class Program
    {
        /// <summary>
        /// 225. Implement Stack using Queues
        /// https://leetcode.com/problems/implement-stack-using-queues/description/
        /// 225. 用队列实现栈
        /// https://leetcode.cn/problems/implement-stack-using-queues/description/?envType=daily-question&envId=Invalid%20Date
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

        }




    }


    /// <summary>
    /// 方法1:
    /// 
    /// https://leetcode.cn/problems/implement-stack-using-queues/solutions/1454270/by-stormsunshine-uay3/?envType=daily-question&envId=Invalid%20Date
    /// Queue 先進先出
    /// Stack 先進後出
    /// 
    /// 用Queue模擬 Stack
    /// 
    /// Queue
    /// https://zh.wikipedia.org/zh-tw/%E9%98%9F%E5%88%97
    /// https://learn.microsoft.com/zh-tw/dotnet/api/system.collections.queue?view=net-7.0
    /// https://learn.microsoft.com/zh-tw/dotnet/api/system.collections.generic.queue-1?view=net-7.0
    /// 
    /// Stack
    /// https://zh.wikipedia.org/zh-tw/%E5%A0%86%E6%A0%88
    /// https://learn.microsoft.com/zh-tw/dotnet/api/system.collections.generic.stack-1?view=net-8.0
    /// 
    /// </summary>
    public class MyStack
    {
        Queue<int> queue1;
        Queue<int> queue2;


        public MyStack()
        {
            queue1 = new Queue<int>();
            queue2 = new Queue<int>();
        }


        /// <summary>
        /// Pushes element x to the top of the stack.
        /// 
        /// 要加入得元素 x, 先放到 queue2
        /// 再來把 queue1裡面的元素 依序加入至 queue2
        /// 再來把 queue2 與 queue1 交換
        /// 即達成 加入 push 方法
        /// </summary>
        /// <param name="x"></param>
        public void Push(int x)
        {
            queue2.Enqueue(x);

            while (queue1.Count > 0) 
            {
                queue2.Enqueue(queue1.Dequeue());
            }

            Queue<int> temp = queue1;
            queue1 = queue2;
            queue2 = temp;
        }


        /// <summary>
        /// Removes the element on the top of the stack and returns it.
        /// </summary>
        /// <returns></returns>
        public int Pop()
        {
            return queue1.Dequeue();
        }


        /// <summary>
        /// Returns the element on the top of the stack.
        /// </summary>
        /// <returns></returns>
        public int Top()
        {
            return queue1.Peek();
        }


        /// <summary>
        /// Returns true if the stack is empty, false otherwise.
        /// </summary>
        /// <returns></returns>
        public bool Empty()
        {
            return queue1.Count == 0;
        }
    }
}
