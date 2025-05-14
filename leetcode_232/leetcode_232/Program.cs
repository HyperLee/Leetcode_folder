namespace leetcode_232
{
    internal class Program
    {
        /// <summary>
        /// 題目描述：
        /// 使用兩個堆疊（Stack）來實作一個佇列（Queue），需支援 push(x)、pop()、peek()、empty() 四種操作。
        /// 你只能使用標準的堆疊操作（push、pop、peek、empty）。
        /// 
        /// 解題概念與出發點：
        /// 1. 使用兩個堆疊 inStack 與 outStack。
        /// 2. push 時將元素推入 inStack。
        /// 3. pop/peek 時，若 outStack 為空，將 inStack 所有元素依序彈出並推入 outStack，這樣 outStack 的頂端即為佇列開頭。
        /// 4. empty 則判斷兩個堆疊皆為空。
        /// 這樣可確保所有操作皆符合佇列（先進先出）特性。
        /// 
        /// 232. Implement Queue using Stacks
        /// https://leetcode.com/problems/implement-queue-using-stacks/description/
        /// 232. 用栈实现队列
        /// https://leetcode.cn/problems/implement-queue-using-stacks/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // 測試 MyQueue
            MyQueue queue = new MyQueue();
            queue.Push(1);
            queue.Push(2);
            Console.WriteLine($"Peek: {queue.Peek()}"); // 預期 1
            Console.WriteLine($"Pop: {queue.Pop()}");   // 預期 1
            Console.WriteLine($"Empty: {queue.Empty()}"); // 預期 False
            Console.WriteLine($"Pop: {queue.Pop()}");   // 預期 2
            Console.WriteLine($"Empty: {queue.Empty()}"); // 預期 True
            queue.Push(3);
            queue.Push(4);
            Console.WriteLine($"Pop: {queue.Pop()}");   // 預期 3
            Console.WriteLine($"Peek: {queue.Peek()}"); // 預期 4
            Console.WriteLine($"Empty: {queue.Empty()}"); // 預期 False
        }
    }


    /// <summary>
    /// 232. Implement Queue using Stacks
    /// https://leetcode.com/problems/implement-queue-using-stacks/description/
    /// 232. 用栈实现队列
    /// https://leetcode.cn/problems/implement-queue-using-stacks/description/
    /// 
    /// ref:
    /// https://leetcode.cn/problems/implement-queue-using-stacks/solution/yong-zhan-shi-xian-dui-lie-by-leetcode-s-xnb6/
    /// 當一個棧作為輸入棧，用於壓入 push 傳入的數據；另一個棧作為輸出棧，用於 pop 和 peek 操作。 
    /// 每次 pop 或 peek 時，若輸出棧為空則將輸入棧的全部數據依次彈出並壓入輸出棧，這樣輸出棧從棧頂往棧底的順序就是隊列從隊首往隊尾的順序。
    /// 
    /// </summary>
    public class MyQueue
    {
        /// <summary>
        /// 輸入資料統一都給 inStack
        /// 這個 Stack 是用來接收 Push 的資料
        /// </summary>
        Stack<int> inStack;

        /// <summary>
        /// 輸出資料統一都給 outStack
        /// 這個 Stack 是用來接收 Pop 和 Peek 的資料
        /// 
        /// 假如 outStack 為空，則需要把 inStack 的資料轉到 outStack
        /// 這樣 outStack 的資料順序就會跟 inStack 的資料順序相反
        /// => In2Out(), 處理轉換邏輯
        /// </summary>
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
        /// 判斷是否為空
        /// 隊列空: true
        /// 反之: false
        /// </summary>
        /// <returns></returns>
        public bool Empty()
        {
            return inStack.Count == 0 && outStack.Count == 0;
        }


        /// <summary>
        /// inStack 轉(輸出) outStack
        /// 當 outStack 為空且需要進行 Pop 或 Peek 操作時，會呼叫 In2Out。
        /// </summary>
        private void In2Out()
        {
            while (inStack.Count > 0)
            {
                // 把 input 資料彈出, push 到 out 資料裡面
                // 資料從 input 隊伍的底部開始, push 到 output 隊伍的頂部
                // 這樣就可以讓 output 隊伍的資料順序跟 input 隊伍的資料順序相反
                outStack.Push(inStack.Pop());
            }
        }
    }    
}
