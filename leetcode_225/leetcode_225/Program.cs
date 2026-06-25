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

/// <summary>
/// Queue 先進先出
/// Stack 先進後出
/// 
/// 用Queue模擬 Stack
/// 
/// 方法一：两个队列
/// 为了满足栈的特性，即最后入栈的元素最先出栈，在使用队列实现栈时，应满足队列前端的元素是最后入栈的元素。可以使用两个队
/// 列实现栈的操作，其中 queue 1用于存储栈内的元素，queue 2​作为入栈操作的辅助队列。
/// 
/// 入栈操作时，首先将元素入队到 queue 2，然后将 queue1的全部元素依次出队并入队到 queue 2，此时 queue 2的前端的元素即为新入
/// 栈的元素，再将 queue 1和 queue 2互换，则 queue 1的元素即为栈内的元素，queue 1的前端和后端分别对应栈顶和栈底。
/// 
/// 由于每次入栈操作都确保 queue 1的前端元素为栈顶元素，因此出栈操作和获得栈顶元素操作都可以简单实现。出栈操作只需要移除
/// queue1 的前端元素并返回即可，获得栈顶元素操作只需要获得 queue1 的前端元素并返回即可（不移除元素）。
/// 
/// 由于 queue1 用于存储栈内的元素，判断栈是否为空时，只需要判断 queue1是否为空即可。
/// </summary>
public class MyStack {
    Queue<int> queue1;
    Queue<int> queue2;

    public MyStack() {
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
    public void Push(int x) {
        queue2.Enqueue(x);

        while(queue1.Count > 0)
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
    public int Pop() {
        return queue1.Dequeue();
    }
    
    /// <summary>
    /// Returns the element on the top of the stack.
    /// </summary>
    /// <returns></returns>
    public int Top() {
        return queue1.Peek();
    }
    
    /// <summary>
    /// Returns true if the stack is empty, false otherwise.
    /// </summary>
    /// <returns></returns>
    public bool Empty() {
        return queue1.Count == 0;
    }
}
