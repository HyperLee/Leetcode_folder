namespace leetcode_977;

class Program
{
    /// <summary>
    /// 977. Squares of a Sorted Array
    /// https://leetcode.com/problems/squares-of-a-sorted-array/description/
    /// 977. 有序数组的平方
    /// https://leetcode.cn/problems/squares-of-a-sorted-array/description/
    ///
    /// English:
    /// Given an integer array nums sorted in non-decreasing order, return an array of the squares
    /// of each number sorted in non-decreasing order.
    ///
    /// 繁體中文：
    /// 給定一個以非遞減順序排列的整數陣列 nums，請回傳一個由每個數字的平方組成，
    /// 並同樣以非遞減順序排列的陣列。
    ///
    /// 執行入口：以五組固定案例執行三種解法，比對預期結果並輸出 PASS/FAIL 摘要。
    /// </summary>
    /// <param name="args">命令列參數；本範例不使用。</param>
    static void Main(string[] args)
    {
        var testCases = new (string Name, int[] Input, int[] Expected)[]
        {
            ("Official example 1", [-4, -1, 0, 3, 10], [0, 1, 9, 16, 100]),
            ("Official example 2", [-7, -3, 2, 3, 11], [4, 9, 9, 49, 121]),
            ("All negative", [-5, -3, -2, -1], [1, 4, 9, 25]),
            ("All non-negative", [0, 2, 3, 8], [0, 4, 9, 64]),
            ("Single zero", [0], [0])
        };

        var solver = new Program();
        var solutions = new (string Name, Func<int[], int[]> Solve)[]
        {
            (nameof(SortedSquares), solver.SortedSquares),
            (nameof(SortedSquares2), solver.SortedSquares2),
            (nameof(SortedSquares3), solver.SortedSquares3)
        };

        int passed = 0;
        int total = testCases.Length * solutions.Length;

        Console.WriteLine("LeetCode 977 - Squares of a Sorted Array");
        Console.WriteLine();

        foreach (var testCase in testCases)
        {
            Console.WriteLine($"{testCase.Name}: [{string.Join(", ", testCase.Input)}]");
            Console.WriteLine($"Expected: [{string.Join(", ", testCase.Expected)}]");

            foreach (var solution in solutions)
            {
                if (RunCase(solution.Name, solution.Solve, testCase.Input, testCase.Expected))
                {
                    passed++;
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine($"Summary: {passed}/{total} passed");
    }

    /// <summary>
    /// 執行單一解法與案例。輸入陣列會先複製，避免不同解法互相影響；方法會比對預期結果、
    /// 輸出 PASS/FAIL 與實際陣列，並回傳此次檢查是否通過。
    /// </summary>
    /// <param name="solutionName">顯示於輸出中的解法名稱。</param>
    /// <param name="solution">接收非遞減排序整數陣列並回傳非遞減平方陣列的解法。</param>
    /// <param name="input">符合題目限制且已按非遞減順序排序的測試輸入。</param>
    /// <param name="expected">此測試輸入對應的預期平方排序結果。</param>
    /// <returns>實際結果與預期結果完全相同時為 <see langword="true"/>，否則為 <see langword="false"/>。</returns>
    private static bool RunCase(
        string solutionName,
        Func<int[], int[]> solution,
        int[] input,
        int[] expected)
    {
        int[] actual = solution((int[])input.Clone());
        bool passed = actual.SequenceEqual(expected);

        Console.WriteLine(
            $"  {solutionName}: {(passed ? "PASS" : "FAIL")} -> [{string.Join(", ", actual)}]");

        return passed;
    }

    /// <summary>
    /// 方法一：直接排序。逐一計算輸入元素的平方，再使用內建排序得到非遞減結果。
    /// 輸入必須符合題目限制並已按非遞減順序排序；本解法不依賴原始排序特性。
    /// </summary>
    /// <param name="nums">已按非遞減順序排序的整數陣列。</param>
    /// <returns>每個元素平方後按非遞減順序排列的新陣列。</returns>
    public int[] SortedSquares(int[] nums)
    {
        int[] input = new int[nums.Length];

        // 先把正負號消除成平方值，再交由排序處理原本被打亂的大小順序。
        for (int i = 0; i < nums.Length; i++)
        {
            input[i] = nums[i] * nums[i];
        }

        Array.Sort(input);
        return input;
    }

    /// <summary>
    /// 方法二：分界雙指標合併。先找出負數與非負數的分界，將陣列視為兩段已排序的平方值：
    /// 負數區從分界往左移動時，平方值會逐漸變大；非負數區從分界往右移動時，平方值也會逐漸變大。
    /// 接著比較分界兩側目前的平方值，每次將較小者放入結果陣列，直到兩側元素全部合併完成。
    /// 此解法運用原陣列的排序特性，不需要再次進行整體排序，時間複雜度為 O(n)，空間複雜度為 O(n)。
    /// </summary>
    /// <param name="nums">已按非遞減順序排序的整數陣列。</param>
    /// <returns>每個元素平方後按非遞減順序排列的新陣列。</returns>
    public int[] SortedSquares2(int[] nums)
    {
        int n = nums.Length;
        int negative = -1;

        // 找出最後一個負數，將平方後的遞減區與遞增區分成兩段。
        for (int scanIndex = 0; scanIndex < n; scanIndex++)
        {
            if (nums[scanIndex] < 0)
            {
                negative = scanIndex;
            }
            else
            {
                break;
            }
        }

        int[] ans = new int[n];
        int index = 0;
        int i = negative;
        int j = negative + 1;

        // 從分界線兩側挑選較小的平方值，依序合併成遞增結果。
        while (i >= 0 || j < n)
        {
            if (i < 0)
            {
                ans[index] = nums[j] * nums[j];
                j++;
            }
            else if (j == n)
            {
                ans[index] = nums[i] * nums[i];
                i--;
            }
            else if (nums[i] * nums[i] < nums[j] * nums[j])
            {
                ans[index] = nums[i] * nums[i];
                i--;
            }
            else
            {
                ans[index] = nums[j] * nums[j];
                j++;
            }
            index++;
        }
        return ans;
    }

    /// <summary>
    /// 方法三：左右端雙指標。已排序陣列的最大平方必定位於左右端之一，因此每次比較兩端平方，
    /// 將較大值由結果陣列尾端往前填入。輸入必須符合題目限制並已按非遞減順序排序。
    /// </summary>
    /// <param name="nums">已按非遞減順序排序的整數陣列。</param>
    /// <returns>每個元素平方後按非遞減順序排列的新陣列。</returns>
    public int[] SortedSquares3(int[] nums)
    {
        int[] result = new int[nums.Length];
        int left = 0;
        int right = nums.Length - 1;

        // 最大平方只可能來自目前左右端，從尾端填值可直接建立遞增結果。
        for (int writeIndex = nums.Length - 1; writeIndex >= 0; writeIndex--)
        {
            int leftSquare = nums[left] * nums[left];
            int rightSquare = nums[right] * nums[right];

            if (leftSquare > rightSquare)
            {
                result[writeIndex] = leftSquare;
                left++;
            }
            else
            {
                result[writeIndex] = rightSquare;
                right--;
            }
        }

        return result;
    }
}
