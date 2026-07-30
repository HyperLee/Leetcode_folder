namespace leetcode_084;

public class Solution
{
    /// <summary>
    /// 計算直方圖可形成的最大矩形面積。此解法分別由左至右、由右至左維護
    /// 單調遞增索引棧，找出每根柱子兩側第一根嚴格較矮的柱子，再以該柱高
    /// 乘上可延伸寬度。輸入須為題目限制內的非負柱高陣列，回傳最大矩形面積。
    /// 時間複雜度為 O(n)，額外空間複雜度為 O(n)。
    /// </summary>
    /// <param name="heights">表示直方圖高度的整數陣列</param>
    /// <returns>最大矩形面積</returns>
    public int LargestRectangleArea(int[] heights)
    {
        int n = heights.Length;
        int[] left = new int[n];
        int[] right = new int[n];
        Stack<int> monoStack = new Stack<int>();

        for (int i = 0; i < n; i++)
        {
            // 移除不夠矮的柱子，留下的棧頂才是左側第一個嚴格較矮位置。
            while (monoStack.Count > 0 && heights[monoStack.Peek()] >= heights[i])
            {
                monoStack.Pop();
            }

            left[i] = monoStack.Count == 0 ? -1 : monoStack.Peek();
            monoStack.Push(i);
        }

        monoStack.Clear();
        for (int i = n - 1; i >= 0; i--)
        {
            while (monoStack.Count > 0 && heights[monoStack.Peek()] >= heights[i])
            {
                monoStack.Pop();
            }

            // n 是右側邊界外的哨兵，與左側的 -1 共同簡化寬度公式。
            right[i] = monoStack.Count == 0 ? n : monoStack.Peek();
            monoStack.Push(i);
        }

        int maximumArea = 0;
        for (int i = 0; i < n; i++)
        {
            int width = right[i] - left[i] - 1;
            maximumArea = Math.Max(maximumArea, width * heights[i]);
        }

        return maximumArea;
    }

    /// <summary>
    /// 計算直方圖可形成的最大矩形面積。此解法只由左至右掃描一次，使用
    /// 單調遞增索引棧保存尚未確定右邊界的柱子；遇到較矮柱子時，彈出柱子並
    /// 立即計算其最大寬度。輸入須為題目限制內的非負柱高陣列，回傳最大矩形
    /// 面積，且不會修改輸入。時間複雜度為 O(n)，額外空間複雜度為 O(n)。
    /// </summary>
    /// <param name="heights">表示直方圖高度的整數陣列。</param>
    /// <returns>直方圖中可形成的最大矩形面積。</returns>
    public int LargestRectangleArea2(int[] heights)
    {
        Stack<int> monoStack = new Stack<int>();
        int maximumArea = 0;

        for (int i = 0; i <= heights.Length; i++)
        {
            // 掃描到陣列尾端時使用虛擬高度 0，迫使所有待處理柱子出棧。
            int currentHeight = i == heights.Length ? 0 : heights[i];

            while (monoStack.Count > 0 && heights[monoStack.Peek()] >= currentHeight)
            {
                int height = heights[monoStack.Pop()];
                int leftBoundary = monoStack.Count == 0 ? -1 : monoStack.Peek();
                int width = i - leftBoundary - 1;

                maximumArea = Math.Max(maximumArea, height * width);
            }

            // 虛擬哨兵不屬於輸入，因此只負責清棧，不儲存其索引。
            if (i < heights.Length)
            {
                monoStack.Push(i);
            }
        }

        return maximumArea;
    }
}

class Program
{
    /// <summary>
    /// 84. Largest Rectangle in Histogram
    /// https://leetcode.com/problems/largest-rectangle-in-histogram/description/
    /// 84. 柱状图中最大的矩形
    /// https://leetcode.cn/problems/largest-rectangle-in-histogram/description/ 
    /// 
    /// LeetCode 84. 柱狀圖中最大的矩形
    /// 題目描述：
    /// 給定 n 個非負整數，用來表示柱狀圖中各個柱子的高度。
    /// 每個柱子的寬度為 1，請計算在該柱狀圖中能夠勾勒出的矩形的最大面積。
    /// 解題思路：
    /// 1. 使用單調棧（Monotonic Stack）解法
    /// 2. 為什麼選擇單調棧？
    ///    - 時間複雜度為 O(n)，比暴力解法 O(n^2) 更優
    ///    - 能有效找出每個柱子左右兩側第一個較矮的柱子
    ///    - 空間複雜度為 O(n)，用於存儲左右邊界
    ///
    /// 此進入點會使用固定測試資料執行兩種單調棧解法，
    /// 並輸出每項檢查的預期值、實際值及通過狀態。
    /// </summary>
    /// <param name="args">命令列參數；本範例不使用此參數。</param>
    static void Main(string[] args)
    {
        Solution solution = new Solution();

        (string Name, int[] Heights, int Expected)[] testCases =
        [
            ("官方範例 1", [2, 1, 5, 6, 2, 3], 10),
            ("官方範例 2", [2, 4], 4),
            ("高度為零", [0], 0),
            ("單一柱子", [1], 1),
            ("嚴格遞增", [1, 2, 3, 4, 5], 9),
            ("嚴格遞減", [5, 4, 3, 2, 1], 9),
            ("重複高度", [2, 2, 2, 2], 8),
            ("中央低谷", [2, 1, 2], 3)
        ];

        int passedChecks = 0;
        foreach ((string name, int[] heights, int expected) in testCases)
        {
            passedChecks += RunTestCase(solution, name, heights, expected);
        }

        int totalChecks = testCases.Length * 2;
        Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");

        if (passedChecks != totalChecks)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 執行單一固定案例，分別呼叫雙向邊界與單次掃描解法，
    /// 比較每種解法的實際結果與預期結果，並輸出 PASS 或 FAIL。
    /// </summary>
    /// <param name="solution">提供兩種最大矩形面積解法的物件。</param>
    /// <param name="name">顯示於主控台的案例名稱。</param>
    /// <param name="heights">符合題目限制的非負柱高陣列。</param>
    /// <param name="expected">此案例預期的最大矩形面積。</param>
    /// <returns>兩種解法中通過檢查的數量，範圍為 0 到 2。</returns>
    private static int RunTestCase(Solution solution, string name, int[] heights, int expected)
    {
        (string Name, Func<int[], int> Solve)[] solutions =
        [
            ("解法一：雙向邊界", solution.LargestRectangleArea),
            ("解法二：單次掃描", solution.LargestRectangleArea2)
        ];

        Console.WriteLine($"案例：{name}");
        Console.WriteLine($"輸入：[{string.Join(", ", heights)}]");

        int passedChecks = 0;
        foreach ((string solutionName, Func<int[], int> solve) in solutions)
        {
            int actual = solve(heights);
            bool passed = actual == expected;
            passedChecks += passed ? 1 : 0;

            Console.WriteLine(
                $"{solutionName} | Expected: {expected}, Actual: {actual} | {(passed ? "PASS" : "FAIL")}");
        }

        Console.WriteLine();
        return passedChecks;
    }
}
