namespace leetcode_225;

class Program
{
    /// <summary>
    /// 225. Implement Stack using Queues
    /// https://leetcode.com/problems/implement-stack-using-queues/description/
    /// 225. 用佇列實作堆疊
    /// https://leetcode.cn/problems/implement-stack-using-queues/description/
    ///
    /// Implement a last-in-first-out (LIFO) stack using only two queues. The implemented stack
    /// should support all the functions of a normal stack (push, top, pop, and empty).
    ///
    /// Implement the MyStack class:
    ///
    /// void push(int x) Pushes element x to the top of the stack.
    /// int pop() Removes the element on the top of the stack and returns it.
    /// int top() Returns the element on the top of the stack.
    /// boolean empty() Returns true if the stack is empty, false otherwise.
    ///
    /// Notes:
    ///
    /// You must use only standard operations of a queue, which means that only push to back,
    /// peek/pop from front, size and is empty operations are valid.
    /// Depending on your language, the queue may not be supported natively. You may simulate a
    /// queue using a list or deque (double-ended queue) as long as you use only a queue's
    /// standard operations.
    ///
    /// 請只使用兩個佇列來實作一個後進先出（LIFO）的堆疊。這個堆疊必須支援一般堆疊的所有
    /// 功能：push、top、pop 與 empty。
    ///
    /// 請實作 MyStack 類別：
    ///
    /// void push(int x) 將元素 x 推到堆疊頂端。
    /// int pop() 移除堆疊頂端元素，並回傳該元素。
    /// int top() 回傳堆疊頂端元素。
    /// boolean empty() 若堆疊為空則回傳 true，否則回傳 false。
    ///
    /// 說明：
    ///
    /// 你只能使用佇列的標準操作，也就是只能使用從尾端加入元素、從前端查看或移除元素、取得大小
    /// 與判斷是否為空等操作。
    /// 依照你使用的語言不同，可能沒有原生佇列型別；在這種情況下，你可以使用 list 或 deque
    /// （雙端佇列）來模擬佇列，但仍然只能使用佇列的標準操作。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
