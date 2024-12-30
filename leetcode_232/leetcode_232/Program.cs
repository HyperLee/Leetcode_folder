namespace leetcode_232
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
        }


        /// <summary>
        /// 232. Implement Queue using Stacks
        /// https://leetcode.com/problems/implement-queue-using-stacks/description/
        /// 232. 用栈实现队列
        /// https://leetcode.cn/problems/implement-queue-using-stacks/description/
        /// 
        /// ref:
        /// https://leetcode.cn/problems/implement-queue-using-stacks/solution/yong-zhan-shi-xian-dui-lie-by-leetcode-s-xnb6/
        /// 将一个栈当作输入栈，用于压入 push 传入的数据；另一个栈当作输出栈，用于 pop 和 peek 操作。
        /// 每次 pop 或 peek 时，若输出栈为空则将输入栈的全部数据依次弹出并压入输出栈，这样输出栈从栈顶往栈底的顺序就是队列从队首往队尾的顺序。
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


            /// <summary>
            /// 將元素 value 推到隊伍尾端
            /// </summary>
            /// <param name="x"></param>
            public void Push(int x)
            {
                inStack.Push(x);
            }


            /// <summary>
            /// 隊列開頭移除並返回元素
            /// </summary>
            /// <returns></returns>
            public int Pop()
            {
                if (outStack.Count == 0)
                {
                    In2Out();
                }

                
                return outStack.Pop();
            }


            /// <summary>
            /// 返回隊列開頭元素
            /// </summary>
            /// <returns></returns>
            public int Peek()
            {
                if (outStack.Count == 0)
                {
                    In2Out();
                }
                return outStack.Peek();
            }


            /// <summary>
            /// 隊列空: true
            /// 反之: false
            /// </summary>
            /// <returns></returns>
            public bool Empty()
            {
                return inStack.Count == 0 && outStack.Count == 0;
            }


            /// <summary>
            /// in 轉 out
            /// </summary>
            private void In2Out()
            {
                while (inStack.Count > 0)
                {
                    // 把 input 隊伍開頭踢出, push 到 out 資料裡面
                    outStack.Push(inStack.Pop());
                }
            }
        }

    }
}
