namespace leetcode_155
{
    internal class Program
    {
        /// <summary>
        /// 155. Min Stack
        /// https://leetcode.com/problems/min-stack/description/
        /// 155. 最小栈
        /// https://leetcode.cn/problems/min-stack/description/
        /// 
        /// 在 C# 中，Stack<T> 是一種 後進先出（LIFO, Last-In-First-Out） 的資料結構，類似於現實中的疊盤子，最後放上去的物品會最先被取出。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            MinStack minStack = new MinStack();
            minStack.Push(-2);
            minStack.Push(0);
            minStack.Push(-3);
            System.Console.WriteLine(minStack.GetMin()); // return -3
            minStack.Pop();
            System.Console.WriteLine(minStack.Top());    // return 0, 因為 -3 已經被彈出(所有數值都儲存)
            System.Console.WriteLine(minStack.GetMin()); // return -2, 因為 -2 是最小值(只儲存最小值)

        }
    }



    /// <summary>
    /// Design a stack that supports push, pop, top, and retrieving the minimum element in constant time.
    /// 採用 GitHub 上的解法
    /// 其他參考:
    /// https://leetcode.cn/problems/min-stack/solutions/242190/zui-xiao-zhan-by-leetcode-solution/
    /// https://leetcode.cn/problems/min-stack/solutions/2974438/ben-zhi-shi-wei-hu-qian-zhui-zui-xiao-zh-x0g8/
    /// https://leetcode.cn/problems/min-stack/solutions/1456182/by-stormsunshine-dtzd/
    /// 
    /// MinStack 設計說明
    /// 這是一個支援常數時間內取得最小值的堆疊實作。我來為您詳細說明這個解決方案：
    /// 
    /// 主要特點
    /// 1. 雙堆疊設計
    ///    stack: 主要儲存所有推入的值
    ///    minStack: 輔助堆疊，追蹤目前為止的最小值
    /// 操作時間複雜度
    /// Push: O(1)
    /// Pop: O(1)
    /// Top: O(1)
    /// GetMin: O(1)
    /// </summary>
    public class MinStack
    {
        private Stack<int> stack;      // Main stack for values
        private Stack<int> minStack;   // Stack to track minimums

        /// <summary>
        /// initialize your data structure here.
        /// </summary>
        public MinStack()
        {
            stack = new Stack<int>();
            minStack = new Stack<int>();
        }


        /// <summary>
        /// Push element x onto stack.
        /// 
        /// 將值推入主堆疊
        /// 如果該值小於或等於目前最小值，也推入 minStack
        /// </summary>
        /// <param name="val"></param>
        public void Push(int val)
        {
            stack.Push(val);
            // If minStack is empty or val is less than or equal to current minimum
            if (minStack.Count == 0 || val <= minStack.Peek())
            {
                minStack.Push(val);
            }
        }


        /// <summary>
        /// Removes the element on top of the stack.
        /// 
        /// 如果彈出的值等於目前最小值，同時從 minStack 移除
        /// </summary>
        public void Pop()
        {
            if (stack.Count == 0)
            {
                return;
            }
            // If popped value is current minimum, pop from minStack too
            if (stack.Peek() == minStack.Peek())
            {
                minStack.Pop();
            }
            stack.Pop();
        }


        /// <summary>
        /// Get the top element.
        /// Top: 返回主堆疊頂端元素
        /// 
        /// leetcode上跑 只需要寫 return 那行就好
        /// </summary>
        /// <returns></returns>
        public int Top()
        {
            if (stack.Count > 0)
            {
                return stack.Peek();
            }
            throw new InvalidOperationException("stack is empty");
        }


        /// <summary>
        /// Retrieve the minimum element in the stack.
        /// GetMin: 返回最小值堆疊頂端元素
        /// 
        /// leetcode上跑 只需要寫 return 那行就好
        /// </summary>
        /// <returns></returns>
        public int GetMin()
        {
            if (minStack.Count > 0)
            {
                return minStack.Peek();
            }
            throw new InvalidOperationException("minStack is empty");
        }
    }

}
