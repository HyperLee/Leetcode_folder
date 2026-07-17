namespace leetcode_1441;

class Program
{
    /// <summary>
    /// 1441. Build an Array With Stack Operations
    /// English:
    /// You are given an integer array target and an integer n.
    /// You have an empty stack with the two following operations:
    /// <list type="bullet">
    /// Push: pushes an integer to the top of the stack.
    /// Pop: removes the integer on the top of the stack.
    /// 
    /// You also have a stream of the integers in the range [1, n].
    /// Use the two stack operations to make the numbers in the stack (from the bottom to the top) equal to target. You should follow these rules:
    /// 
    /// If the stream of the integers is not empty, pick the next integer from the stream and push it to the top of the stack.
    /// If the stack is not empty, pop the integer at the top of the stack.
    /// If, at any moment, the elements in the stack (from the bottom to the top) are equal to target, do not read new integers from the stream and do not do more operations on the stack.
    /// 
    /// Return the stack operations needed to build target following the mentioned rules. If there are multiple valid answers, return any of them.
    /// 繁體中文：
    /// 給定一個整數陣列 target 和一個整數 n。
    /// 你有一個空堆疊，並支援以下兩種操作：
    /// <list type="bullet">
    /// 「Push」：將一個整數推入堆疊頂端。
    /// 「Pop」：移除堆疊頂端的整數。
    /// 
    /// 你還有一個由 [1, n] 範圍內整數組成的資料流。
    /// 請使用這兩種堆疊操作，讓堆疊中的數字（由底部到頂部）與 target 完全相同。如果有多組有效答案，回傳任一組即可。
    /// 請遵守以下規則：
    /// 
    /// 當資料流不為空時，從資料流取出下一個整數，並將它推入堆疊頂端。
    /// 當堆疊不為空時，移除堆疊頂端的整數。
    /// 只要堆疊中的元素（由底部到頂部）在任何時刻等於 target，就不要再從資料流讀取新整數，也不要再對堆疊進行任何操作。
    /// 
    /// 請依照上述規則，回傳建立 target 所需的堆疊操作。
    /// </summary>
    /// <param name="args">Command-line arguments; this demo does not require input.</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 解法一:模擬
    /// 操作的对象是 1 到 n 按顺序排列的数字，每次操作一个数字时，如果它在 target 中，则只需要将它 Push 入栈即可。如果不在 
    /// target 中，可以先将其 Push 入栈，紧接着 Pop 出栈。因为 target 中数字是严格递增的，因此只要遍历 target，在 target 中每两
    /// 个连续的数字 prev 和 number 中插入 number−prev−1 个 Push 和 Pop，再多加一个 Push 来插入当前数字即可。
    /// 
    /// 
    /// 效率較好
    /// 關鍵就是要得出
    /// 因为 target 中数字是严格递增的，因此只要遍历
    /// target，在 target 中每两个连续的数字 prev 和 number 中插入 number − prev − 1
    /// 个 Push 和 Pop，再多加一个 Push 来插入当前数字即可。
    /// 
    /// 如果想不出來, 只能用方法2
    /// </summary>
    /// <param name="target"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public IList<string> BuildArray(int[] target, int n)
    {
        IList<string> res = new List<string>();
        int prev = 0;
        foreach(int number in target)
        {
            // for迴圈條件是這解法關鍵
            for(int i = 0; i < number - prev - 1; i++)
            {
                res.Add("Push");
                res.Add("Pop");
            }
            res.Add("Push");
            prev = number;
        }
        return res;
    }

    /// <summary>
    /// 解法二: 模擬
    /// 看示例 1，n=3 意味着我们会依次读取 1,2,3 这三个数。
    /// 1 在 target 中，入栈。
    /// 2 不在 target 中，先入栈，再出栈。注意一定要入栈，这是题目要求。
    /// 3 在 target 中，入栈。现在栈等于 target。
    /// 
    /// 怎么判断当前读取的数是否在 target 中？
    /// 1. 设 target 的最后一个数为 mx。初始化指针 i=0，指向 target 的第一个数。
    /// 2. 枚举读取的数为 x=1,2,…,mx。
    /// 3. 先把 x 入栈。
    /// 4. 如果 x = target[i]，那么 x 是我们要的数，把 i 加一，指向 target 的下一个数。
    /// 5. 否则 x < target[i]，那么 x 不是我们要的数，把 x 出栈。
    /// 由于 target 是严格递增的，所以 x≤target[i] 始终成立。
    /// </summary>
    /// <param name="target"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public IList<string> BuildArray2(int[] target, int n)
    {
        List<string> ans = new List<string>();
        int mx = target[target.Length - 1];
        int i = 0;
        for(int x = 1; x <= mx; x++)
        {
            // 先把 x 入棧(題目要求)
            ans.Add("Push");

            // x 是我們要的數
            if(x == target[i])
            {
                i++;
            }
            else
            {
                // x 不是我們要的數, 出棧
                ans.Add("Pop");
            }
        }
        return ans;
    }

    /// <summary>
    /// 方法三:模擬
    /// 根据题意进行模拟即可：每次我们将当前处理到 i 压入栈中（往答案添加一个 Push），然后判断当前处理到的 i 是否最新的
    /// 栈顶元素 target[j] 是否相同，若不相同则丢弃元素（往答案添加一个 Pop），若存在则将指针 j 后移，直到构建出目标答案。
    /// 
    /// 當n 與target相符合 就 push
    /// 否則只能pop 出去
    /// 
    /// 比較直覺 易懂
    /// 
    /// [1, n] => 嚴格遞增 連續數字
    /// 所以遇到 不要的就pop出去
    /// </summary>
    /// <param name="target"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public IList<string> BuildArray3(int[] target, int n)
    {
        IList<string> ans = new List<string>();
        int m = target.Length;

        // i:[1, n], j = target長度
        for(int i = 1, j = 0; i <= n && j < m; i++)
        {
            ans.Add("Push");

            if(target[j] != i)
            {
                ans.Add("Pop");
            }
            else
            {
                // 相同就繼續往下
                j++;
            }
        }
        return ans;
    }
}
