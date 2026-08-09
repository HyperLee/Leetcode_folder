namespace leetcode_155
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 155. Min Stack
        /// https://leetcode.com/problems/min-stack/description/
        ///
        /// Design a stack that supports push, pop, top, and retrieving the minimum element in constant time.
        /// Implement the MinStack class:
        /// - MinStack() initializes the stack object.
        /// - void push(int value) pushes value onto the stack.
        /// - void pop() removes the element on the top of the stack.
        /// - int top() gets the top element of the stack.
        /// - int getMin() retrieves the minimum element in the stack.
        /// You must implement each function with O(1) time complexity.
        ///
        /// Example 1:
        /// Input:
        /// ["MinStack","push","push","push","getMin","pop","top","getMin"]
        /// [[],[-2],[0],[-3],[],[],[],[]]
        /// Output: [null,null,null,null,-3,null,0,-2]
        /// Explanation:
        /// MinStack minStack = new MinStack();
        /// minStack.push(-2);
        /// minStack.push(0);
        /// minStack.push(-3);
        /// minStack.getMin(); // return -3
        /// minStack.pop();
        /// minStack.top(); // return 0
        /// minStack.getMin(); // return -2
        ///
        /// Constraints:
        /// - -2^31 &lt;= val &lt;= 2^31 - 1
        /// - pop, top and getMin are always called on non-empty stacks.
        /// - At most 3 * 10^4 calls are made to push, pop, top and getMin.
        /// </para>
        /// <para>
        /// 155. 最小堆疊
        /// https://leetcode.cn/problems/min-stack/description/
        ///
        /// 設計一個支援 push、pop、top，以及在常數時間內取得最小元素的堆疊。
        /// 實作 MinStack 類別：
        /// - MinStack() 初始化堆疊物件。
        /// - void push(int value) 將 value 推入堆疊。
        /// - void pop() 移除堆疊頂端的元素。
        /// - int top() 取得堆疊頂端元素。
        /// - int getMin() 取得堆疊中的最小元素。
        /// 每個函式都必須以 O(1) 時間複雜度實作。
        ///
        /// 範例 1：
        /// 輸入：
        /// ["MinStack","push","push","push","getMin","pop","top","getMin"]
        /// [[],[-2],[0],[-3],[],[],[],[]]
        /// 輸出：[null,null,null,null,-3,null,0,-2]
        /// 解釋：
        /// MinStack minStack = new MinStack();
        /// minStack.push(-2);
        /// minStack.push(0);
        /// minStack.push(-3);
        /// minStack.getMin(); // 回傳 -3
        /// minStack.pop();
        /// minStack.top(); // 回傳 0
        /// minStack.getMin(); // 回傳 -2
        ///
        /// 限制條件：
        /// - -2^31 &lt;= val &lt;= 2^31 - 1
        /// - pop、top 與 getMin 一律在非空堆疊上呼叫。
        /// - push、pop、top 與 getMin 的呼叫次數合計最多為 3 * 10^4。
        /// </para>
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
        private readonly Stack<int> stack;      // Main stack for values
        private readonly Stack<int> minStack;   // Stack to track minimums

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
            return stack.Peek();
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
            return minStack.Peek();
        }
    }

}
