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
    /// <remarks>
    /// 執行三組固定範例，並用相同輸入驗證三種解法的操作序列。
    /// </remarks>
    /// <param name="args">Command-line arguments; this demo does not require input.</param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        int passed = 0;

        passed += RunExample(solution, 1, [1, 3], 3, ["Push", "Push", "Pop", "Push"]);
        passed += RunExample(solution, 2, [1, 2, 3], 3, ["Push", "Push", "Push"]);
        passed += RunExample(solution, 3, [1, 2], 4, ["Push", "Push"]);

        Console.WriteLine($"{passed}/9 passed.");
    }

    /// <summary>
    /// 解法一：直接計算相鄰目標值之間的缺口。
    /// 
    /// 由於 target 嚴格遞增，前一個目標值與當前目標值之間的數字都必須先
    /// Push 再 Pop，當前目標值則只需 Push。
    /// 
    /// 輸入條件：target 不可為空、必須嚴格遞增，且每個值都介於 1 與
    /// n 之間。輸出是按順序建立目標陣列所需的 Push/Pop 字串序列。
    /// </summary>
    /// <param name="target">要建立的非空、嚴格遞增目標陣列。</param>
    /// <param name="n">資料流的上限；資料流包含 1 到 <paramref name="n"/>。</param>
    /// <returns>建立 <paramref name="target"/> 所需的堆疊操作序列。</returns>
    public IList<string> BuildArray(int[] target, int n)
    {
        IList<string> res = new List<string>();
        int prev = 0;
        foreach(int number in target)
        {
            // number - prev - 1 就是兩個相鄰目標值之間必須捨棄的數字數量。
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
    /// 解法二：只枚舉到最後一個目標值，並用指標追蹤下一個需要的數字。
    /// 
    /// 每從資料流取出一個數字都先加入 Push；若它等於指標指向的目標值就移動指標，
    /// 否則再加入 Pop 捨棄該數字。
    /// 
    /// 輸入條件：target 不可為空、必須嚴格遞增，且每個值都介於 1 與
    /// n 之間。輸出是讀到最後一個目標值時所產生的堆疊操作序列。
    /// </summary>
    /// <param name="target">要建立的非空、嚴格遞增目標陣列。</param>
    /// <param name="n">資料流的上限；資料流包含 1 到 <paramref name="n"/>。</param>
    /// <returns>建立 <paramref name="target"/> 所需的堆疊操作序列。</returns>
    public IList<string> BuildArray2(int[] target, int n)
    {
        List<string> ans = new List<string>();
        int mx = target[target.Length - 1];
        int i = 0;
        for(int x = 1; x <= mx; x++)
        {
            // 題目規定讀取數字後必須先 Push，不是目標值時才能緊接著 Pop。
            ans.Add("Push");

            if(x == target[i])
            {
                i++;
            }
            else
            {
                ans.Add("Pop");
            }
        }
        return ans;
    }

    /// <summary>
    /// 解法三：完整模擬 1 到 n 的資料流，同時追蹤目標陣列位置。
    /// 
    /// 每個讀取值都先加入 Push；若不等於當前目標值就加入 Pop，否則移向下一個目標值。
    /// 當資料流結束或所有目標值都已匹配時停止。
    /// 
    /// 輸入條件：target 不可為空、必須嚴格遞增，且每個值都介於 1 與
    /// n 之間。輸出是直接模擬資料流後所產生的堆疊操作序列。
    /// </summary>
    /// <param name="target">要建立的非空、嚴格遞增目標陣列。</param>
    /// <param name="n">資料流的上限；資料流包含 1 到 <paramref name="n"/>。</param>
    /// <returns>建立 <paramref name="target"/> 所需的堆疊操作序列。</returns>
    public IList<string> BuildArray3(int[] target, int n)
    {
        IList<string> ans = new List<string>();
        int m = target.Length;

        // j == m 代表目標已完成，此時必須停止讀取後續數字。
        for(int i = 1, j = 0; i <= n && j < m; i++)
        {
            ans.Add("Push");

            if(target[j] != i)
            {
                ans.Add("Pop");
            }
            else
            {
                j++;
            }
        }
        return ans;
    }

    /// <summary>
    /// 以同一組非空、嚴格遞增的目標陣列執行三種解法，比對預期操作序列並輸出驗證結果。
    /// </summary>
    /// <param name="solution">提供三種解法的 <see cref="Program"/> 實例。</param>
    /// <param name="caseNumber">顯示在主控台的範例編號。</param>
    /// <param name="target">要建立的目標陣列。</param>
    /// <param name="n">資料流的上限。</param>
    /// <param name="expected">此範例預期的堆疊操作序列。</param>
    /// <returns>通過預期結果比對的解法數量，範圍為 0 到 3。</returns>
    private static int RunExample(Program solution, int caseNumber, int[] target, int n, string[] expected)
    {
        (string Name, Func<int[], int, IList<string>> Execute)[] methods =
        [
            (nameof(BuildArray), solution.BuildArray),
            (nameof(BuildArray2), solution.BuildArray2),
            (nameof(BuildArray3), solution.BuildArray3)
        ];

        Console.WriteLine($"Case {caseNumber}: target = [{string.Join(", ", target)}], n = {n}");

        int passed = 0;
        foreach((string name, Func<int[], int, IList<string>> execute) in methods)
        {
            IList<string> actual = execute(target, n);
            bool isPassed = actual.SequenceEqual(expected);

            Console.WriteLine($"  {name}: [{string.Join(", ", actual)}] | {(isPassed ? "PASS" : "FAIL")}");
            if(isPassed)
            {
                passed++;
            }
        }

        Console.WriteLine();
        return passed;
    }
}
