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
    /// <remarks>
    /// 執行固定操作序列，對照雙佇列與單佇列輪轉兩種 stack 模擬策略的結果是否一致。
    /// </remarks>
    /// <param name="args">目前未使用的命令列參數。</param>
    static void Main(string[] args)
    {
        RunSamples();
    }

    /// <summary>
    /// 執行固定測資，逐案驗證兩種以 queue 模擬 stack 的解法是否都符合 LIFO 行為。
    /// </summary>
    /// <remarks>
    /// 測資涵蓋官方示例、多次 push 後連續 pop、單元素生命週期，以及清空後再次使用等情境。
    /// </remarks>
    private static void RunSamples()
    {
        SampleCase[] samples =
        [
            new(
                "官方示例：先推入 1、2，再依序 top / pop / empty",
                [
                    new(StackOperationKind.Push, 1),
                    new(StackOperationKind.Push, 2),
                    new(StackOperationKind.Top),
                    new(StackOperationKind.Pop),
                    new(StackOperationKind.Empty)
                ],
                ["null", "null", "2", "2", "False"]),
            new(
                "多次推入後驗證後進先出",
                [
                    new(StackOperationKind.Push, 10),
                    new(StackOperationKind.Push, 20),
                    new(StackOperationKind.Push, 30),
                    new(StackOperationKind.Pop),
                    new(StackOperationKind.Top),
                    new(StackOperationKind.Empty)
                ],
                ["null", "null", "null", "30", "20", "False"]),
            new(
                "單一元素的完整生命週期",
                [
                    new(StackOperationKind.Push, 42),
                    new(StackOperationKind.Top),
                    new(StackOperationKind.Pop),
                    new(StackOperationKind.Empty)
                ],
                ["null", "42", "42", "True"]),
            new(
                "清空後再次使用 stack",
                [
                    new(StackOperationKind.Push, 7),
                    new(StackOperationKind.Pop),
                    new(StackOperationKind.Empty),
                    new(StackOperationKind.Push, 9),
                    new(StackOperationKind.Top),
                    new(StackOperationKind.Pop),
                    new(StackOperationKind.Empty)
                ],
                ["null", "7", "True", "null", "9", "9", "True"])
        ];

        StackFactory[] factories =
        [
            new("方法一（雙佇列重排）", () => new MyStack()),
            new("方法二（單佇列輪轉）", () => new MyStackSingleQueue())
        ];

        int passedChecks = 0;

        Console.WriteLine("LeetCode 225 - Implement Stack using Queues");
        Console.WriteLine("==================================================");

        for (int index = 0; index < samples.Length; index++)
        {
            SampleCase sample = samples[index];
            Console.WriteLine($"[{index + 1}] {sample.Name}");
            Console.WriteLine($"操作序列：{DescribeOperationSequence(sample.Operations)}");
            Console.WriteLine($"預期輸出：{FormatOutputs(sample.ExpectedOutputs)}");

            foreach (StackFactory factory in factories)
            {
                string[] actualOutputs = ExecuteOperations(factory.Create(), sample.Operations);
                bool passed = actualOutputs.SequenceEqual(sample.ExpectedOutputs);

                passedChecks += passed ? 1 : 0;

                Console.WriteLine($"{factory.Name}：{FormatOutputs(actualOutputs)} ({(passed ? "PASS" : "FAIL")})");
            }

            Console.WriteLine();
        }

        Console.WriteLine($"總結：{passedChecks}/{samples.Length * factories.Length} 項驗證通過");
    }

    /// <summary>
    /// 依序執行一組 stack 操作，將每一步的可觀察輸出轉成字串陣列，便於與預期結果比較。
    /// </summary>
    /// <param name="stack">要驗證的 stack 實作；輸入需符合題目保證，避免對空 stack 執行 pop 或 top。</param>
    /// <param name="operations">按順序執行的操作清單。</param>
    /// <returns>每一步操作對應的輸出字串；push 以 <c>null</c> 表示無回傳值。</returns>
    private static string[] ExecuteOperations(IStackSolution stack, StackOperation[] operations)
    {
        List<string> outputs = new List<string>(operations.Length);

        foreach (StackOperation operation in operations)
        {
            switch (operation.Kind)
            {
                case StackOperationKind.Push:
                    stack.Push(operation.Value);
                    outputs.Add("null");
                    break;
                case StackOperationKind.Pop:
                    outputs.Add(stack.Pop().ToString());
                    break;
                case StackOperationKind.Top:
                    outputs.Add(stack.Top().ToString());
                    break;
                case StackOperationKind.Empty:
                    outputs.Add(stack.Empty().ToString());
                    break;
                default:
                    throw new InvalidOperationException($"Unsupported operation kind: {operation.Kind}");
            }
        }

        return [.. outputs];
    }

    /// <summary>
    /// 將操作清單格式化成易讀字串，方便在主控台與 README 展示每個案例的執行流程。
    /// </summary>
    /// <param name="operations">要描述的操作清單。</param>
    /// <returns>以箭頭串接的操作序列字串。</returns>
    private static string DescribeOperationSequence(StackOperation[] operations)
    {
        return string.Join(" -> ", operations.Select(static operation => operation.ToDisplayText()));
    }

    /// <summary>
    /// 將輸出值陣列格式化成中括號表示法，讓不同實作的結果可以直接對照。
    /// </summary>
    /// <param name="outputs">欲格式化的輸出值陣列。</param>
    /// <returns>以中括號包住、逗號分隔的輸出字串。</returns>
    private static string FormatOutputs(string[] outputs)
    {
        return $"[{string.Join(", ", outputs)}]";
    }

    /// <summary>
    /// 表示單一測資，包含說明、操作序列與對應的預期輸出。
    /// </summary>
    /// <param name="Name">測資名稱。</param>
    /// <param name="Operations">要依序執行的 stack 操作。</param>
    /// <param name="ExpectedOutputs">每一步操作對應的預期輸出。</param>
    private sealed record SampleCase(string Name, StackOperation[] Operations, string[] ExpectedOutputs);

    /// <summary>
    /// 描述要執行的 stack 操作，以及 push 時要放入的值。
    /// </summary>
    /// <param name="Kind">操作種類。</param>
    /// <param name="Value">只有 push 操作會使用的輸入值。</param>
    private sealed record StackOperation(StackOperationKind Kind, int Value = 0)
    {
        /// <summary>
        /// 將操作轉成主控台與 README 會使用的展示文字。
        /// </summary>
        /// <returns>操作的可讀字串表示。</returns>
        public string ToDisplayText()
        {
            return Kind switch
            {
                StackOperationKind.Push => $"push({Value})",
                StackOperationKind.Pop => "pop()",
                StackOperationKind.Top => "top()",
                StackOperationKind.Empty => "empty()",
                _ => throw new InvalidOperationException($"Unsupported operation kind: {Kind}")
            };
        }
    }

    /// <summary>
    /// 列舉 sample harness 會使用的 stack 操作種類。
    /// </summary>
    private enum StackOperationKind
    {
        Push,
        Pop,
        Top,
        Empty
    }

    /// <summary>
    /// 封裝測資輸出時使用的實作名稱與建構方式。
    /// </summary>
    /// <param name="Name">顯示在主控台與 README 的方法名稱。</param>
    /// <param name="Create">建立新 stack 實例的工廠方法。</param>
    private sealed record StackFactory(string Name, Func<IStackSolution> Create);
}

/// <summary>
/// 提供 sample harness 共用的 stack 操作介面，方便以同一套案例驗證不同實作。
/// </summary>
internal interface IStackSolution
{
    /// <summary>
    /// 將一個整數推入 stack 頂端。
    /// </summary>
    /// <param name="x">要推入的整數；依題目條件保證為合法輸入。</param>
    void Push(int x);

    /// <summary>
    /// 移除並回傳目前 stack 頂端元素。
    /// </summary>
    /// <returns>原本位於 stack 頂端的整數。</returns>
    int Pop();

    /// <summary>
    /// 查看目前 stack 頂端元素，但不移除它。
    /// </summary>
    /// <returns>目前位於 stack 頂端的整數。</returns>
    int Top();

    /// <summary>
    /// 判斷 stack 是否為空。
    /// </summary>
    /// <returns>若 stack 沒有任何元素則回傳 <see langword="true"/>；否則回傳 <see langword="false"/>。</returns>
    bool Empty();
}

/// <summary>
/// 使用兩個 queue 模擬 stack，讓每次 push 後都把最新元素調整到前端，之後的 top 與 pop 就能直接在前端完成。
/// </summary>
/// <remarks>
/// 輸入需符合題目保證：不會對空 stack 執行 <see cref="Pop"/> 或 <see cref="Top"/>；輸出為標準 stack 語意。
/// </remarks>
public class MyStack : IStackSolution
{
    private Queue<int> queue1;
    private Queue<int> queue2;

    /// <summary>
    /// 建立一個空的雙佇列 stack。
    /// </summary>
    public MyStack()
    {
        queue1 = new Queue<int>();
        queue2 = new Queue<int>();
    }

    /// <summary>
    /// 將元素推入 stack 頂端；作法是先放入輔助 queue，再把舊資料全部接到後面，讓新元素成為新的 front。
    /// </summary>
    /// <param name="x">要推入 stack 的整數。</param>
    public void Push(int x)
    {
        queue2.Enqueue(x);

        // 將既有元素全部搬到新元素後方，front 就會對應到最新推入的 stack top。
        while (queue1.Count > 0)
        {
            queue2.Enqueue(queue1.Dequeue());
        }

        Queue<int> temp = queue1;
        queue1 = queue2;
        queue2 = temp;
    }

    /// <summary>
    /// 移除並回傳 stack 頂端元素；因為 push 時已重排完成，所以直接從 queue front 取出即可。
    /// </summary>
    /// <returns>目前 stack 頂端的整數。</returns>
    public int Pop()
    {
        return queue1.Dequeue();
    }

    /// <summary>
    /// 回傳 stack 頂端元素而不移除；由於 queue front 永遠維持 stack top，因此可直接查看 front。
    /// </summary>
    /// <returns>目前 stack 頂端的整數。</returns>
    public int Top()
    {
        return queue1.Peek();
    }

    /// <summary>
    /// 判斷 stack 是否為空；只要主 queue 沒有元素，就表示沒有任何 stack 內容。
    /// </summary>
    /// <returns>若 stack 為空則回傳 <see langword="true"/>；否則回傳 <see langword="false"/>。</returns>
    public bool Empty()
    {
        return queue1.Count == 0;
    }
}

/// <summary>
/// 使用單一 queue 模擬 stack；每次 push 後將先前元素輪轉到尾端，讓最新元素移到 front，維持 LIFO 取用順序。
/// </summary>
/// <remarks>
/// 輸入需符合題目保證：不會對空 stack 執行 <see cref="Pop"/> 或 <see cref="Top"/>；輸出為標準 stack 語意。
/// </remarks>
public class MyStackSingleQueue : IStackSolution
{
    private Queue<int> queue;

    /// <summary>
    /// 建立一個空的單佇列 stack。
    /// </summary>
    public MyStackSingleQueue()
    {
        queue = new Queue<int>();
    }

    /// <summary>
    /// 將元素推入 stack 頂端；先把新元素放到 queue 尾端，再將舊元素依序輪轉到後方，讓新元素移到 front。
    /// </summary>
    /// <param name="x">要推入 stack 的整數。</param>
    public void Push(int x)
    {
        queue.Enqueue(x);

        // 輪轉 push 前已存在的元素，讓剛加入的新元素成為 queue front，也就是 stack top。
        for (int rotationCount = queue.Count - 1; rotationCount > 0; rotationCount--)
        {
            queue.Enqueue(queue.Dequeue());
        }
    }

    /// <summary>
    /// 移除並回傳 stack 頂端元素；因為 front 永遠對應到最新元素，所以可直接 dequeue。
    /// </summary>
    /// <returns>目前 stack 頂端的整數。</returns>
    public int Pop()
    {
        return queue.Dequeue();
    }

    /// <summary>
    /// 回傳 stack 頂端元素而不移除；front 即為最後推入的元素。
    /// </summary>
    /// <returns>目前 stack 頂端的整數。</returns>
    public int Top()
    {
        return queue.Peek();
    }

    /// <summary>
    /// 判斷 stack 是否為空；當 queue 沒有元素時，表示 stack 內也沒有任何資料。
    /// </summary>
    /// <returns>若 stack 為空則回傳 <see langword="true"/>；否則回傳 <see langword="false"/>。</returns>
    public bool Empty()
    {
        return queue.Count == 0;
    }
}
