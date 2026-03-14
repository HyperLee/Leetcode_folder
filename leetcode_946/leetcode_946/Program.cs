namespace leetcode_946;

class Program
{
    /// <summary>
    /// https://leetcode.com/problems/validate-stack-sequences/description/
    /// 946. Validate Stack Sequences
    /// 946. 驗證堆疊序列
    /// https://leetcode.cn/problems/validate-stack-sequences/description/
    ///
    /// Given two integer arrays pushed and popped each with distinct values,
    /// return true if this could have been the result of a sequence of push and pop operations
    /// on an initially empty stack, or false otherwise.
    ///
    /// 給定兩個整數陣列 pushed 和 popped，其中每個陣列的元素各不相同，
    /// 若此序列可能是對一個初始為空的堆疊執行一系列 push 與 pop 操作的結果，
    /// 則回傳 true，否則回傳 false。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var program = new Program();

        // 測試範例 1：pushed = [1,2,3,4,5], popped = [4,5,3,2,1] → true
        int[] pushed1 = [1, 2, 3, 4, 5];
        int[] popped1 = [4, 5, 3, 2, 1];
        Console.WriteLine($"測試 1: pushed=[{string.Join(",", pushed1)}], popped=[{string.Join(",", popped1)}]");
        Console.WriteLine($"結果: {program.ValidateStackSequences(pushed1, popped1)}"); // true

        // 測試範例 2：pushed = [1,2,3,4,5], popped = [4,3,5,1,2] → false
        int[] pushed2 = [1, 2, 3, 4, 5];
        int[] popped2 = [4, 3, 5, 1, 2];
        Console.WriteLine($"測試 2: pushed=[{string.Join(",", pushed2)}], popped=[{string.Join(",", popped2)}]");
        Console.WriteLine($"結果: {program.ValidateStackSequences(pushed2, popped2)}"); // false

        // 測試範例 3：pushed = [1], popped = [1] → true
        int[] pushed3 = [1];
        int[] popped3 = [1];
        Console.WriteLine($"測試 3: pushed=[{string.Join(",", pushed3)}], popped=[{string.Join(",", popped3)}]");
        Console.WriteLine($"結果: {program.ValidateStackSequences(pushed3, popped3)}"); // true

        // 測試範例 4：pushed = [2,1,0], popped = [1,2,0] → true
        int[] pushed4 = [2, 1, 0];
        int[] popped4 = [1, 2, 0];
        Console.WriteLine($"測試 4: pushed=[{string.Join(",", pushed4)}], popped=[{string.Join(",", popped4)}]");
        Console.WriteLine($"結果: {program.ValidateStackSequences(pushed4, popped4)}"); // true
    }

    /// <summary>
    /// 方法一：堆疊模擬
    ///
    /// <para>
    /// 解題思路：
    /// 所有元素一定按照 pushed 的順序依次入堆疊，關鍵在於何時出堆疊。
    /// 由於堆疊中元素互不相同，若當前堆疊頂元素恰好等於 popped 中下一個待出堆疊的元素，
    /// 則必須立刻將其彈出——因為之後再 push 新元素會覆蓋堆疊頂，導致順序無法匹配。
    /// </para>
    ///
    /// <para>
    /// 演算法步驟：
    /// 1. 依序將 pushed 的每個元素推入堆疊。
    /// 2. 每次推入後，持續檢查堆疊頂是否與 popped 當前指標指向的元素相同；若相同則彈出，並將指標後移。
    /// 3. 遍歷結束後，若堆疊為空則代表序列合法，回傳 true；否則回傳 false。
    /// </para>
    ///
    /// <para>
    /// 時間複雜度：O(n)，其中 n 為陣列長度。每個元素最多入堆疊和出堆疊各一次。
    /// 空間複雜度：O(n)，堆疊最多同時存放 n 個元素。
    /// </para>
    ///
    /// <example>
    /// <code>
    /// pushed = [1,2,3,4,5], popped = [4,5,3,2,1] → true
    /// pushed = [1,2,3,4,5], popped = [4,3,5,1,2] → false
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="pushed">入堆疊順序陣列，元素互不相同</param>
    /// <param name="popped">出堆疊順序陣列，為 pushed 的一個排列</param>
    /// <returns>若 popped 是合法的出堆疊序列則回傳 true，否則回傳 false</returns>
    public bool ValidateStackSequences(int[] pushed, int[] popped)
    {
        // 長度不同，不可能是合法序列
        if (pushed.Length != popped.Length)
        {
            return false;
        }

        // 空陣列視為合法（沒有任何操作）
        if (pushed.Length == 0)
        {
            return true;
        }

        Stack<int> stack = new Stack<int>();
        int n = pushed.Length;
        int j = 0; // j 為 popped 陣列的指標，指向下一個期望被彈出的元素

        for (int i = 0; i < n; i++)
        {
            // 將 pushed[i] 依序推入堆疊
            stack.Push(pushed[i]);

            // 持續比較堆疊頂與 popped[j]，若相同就彈出
            while (stack.Count != 0 && stack.Peek() == popped[j])
            {
                stack.Pop();
                j++;
            }
        }

        // 若所有元素都已按照 popped 順序彈出，堆疊應為空
        return stack.Count == 0;
    }
}
