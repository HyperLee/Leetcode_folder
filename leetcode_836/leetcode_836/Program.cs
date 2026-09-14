namespace leetcode_836;

class Program
{
    /// <summary>
    /// 836. Rectangle Overlap
    /// https://leetcode.com/problems/rectangle-overlap/description/
    /// 836. 矩形重疊
    /// https://leetcode.cn/problems/rectangle-overlap/description/
    ///
    /// An axis-aligned rectangle is represented as a list [x1, y1, x2, y2], where (x1, y1) is the coordinate of its bottom-left corner, and (x2, y2) is the coordinate of its top-right corner.
    /// Its top and bottom edges are parallel to the X-axis, and its left and right edges are parallel to the Y-axis.
    ///
    /// Two rectangles overlap if the area of their intersection is positive. To be clear, two rectangles that only touch at the corner or edges do not overlap.
    ///
    /// Given two axis-aligned rectangles rec1 and rec2, return true if they overlap, otherwise return false.
    ///
    /// 軸對齊矩形以列表 [x1, y1, x2, y2] 表示，其中 (x1, y1) 是矩形左下角的座標，而 (x2, y2) 是矩形右上角的座標。
    /// 矩形的上邊與下邊平行於 X 軸，左邊與右邊平行於 Y 軸。
    ///
    /// 如果兩個矩形的交集面積為正，則兩個矩形重疊。明確來說，只在角落或邊緣接觸的兩個矩形不算重疊。
    ///
    /// 給定兩個軸對齊矩形 rec1 與 rec2，如果它們重疊則回傳 true，否則回傳 false。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 以固定案例同時驗證兩種解法，讓 README 的執行結果可以重現與核對。
        Program solution = new();
        TestCase[] testCases =
        [
            new("官方範例一：部分重疊", [0, 0, 2, 2], [1, 1, 3, 3], true),
            new("官方範例二：邊緣接觸", [0, 0, 1, 1], [1, 0, 2, 1], false),
            new("官方範例三：角落接觸", [0, 0, 1, 1], [2, 2, 3, 3], false),
            new("包含關係", [-1, -1, 4, 4], [0, 0, 2, 2], true),
            new("rec1 位於 rec2 右側", [1, 0, 2, 1], [0, 0, 1, 1], false),
            new("rec1 位於 rec2 下方", [0, 0, 2, 2], [0, 2, 2, 4], false),
            new("rec1 位於 rec2 上方", [0, 2, 2, 4], [0, 0, 2, 2], false),
            new("負座標部分重疊", [-4, -3, 1, 2], [-2, -1, 3, 4], true),
            new("完全相同矩形", [-2, -2, 3, 3], [-2, -2, 3, 3], true),
            new("座標邊界部分重疊", [-1000000000, -1000000000, 0, 0], [-1, -1, 1000000000, 1000000000], true)
        ];

        int passedChecks = 0;
        foreach (TestCase testCase in testCases)
        {
            (bool methodOneActual, bool methodTwoActual) = RunTestCase(solution, testCase);
            bool methodOnePassed = methodOneActual == testCase.Expected;
            bool methodTwoPassed = methodTwoActual == testCase.Expected;

            Console.WriteLine($"Case: {testCase.Name}");
            Console.WriteLine($"Input: rec1 = [{string.Join(", ", testCase.Rec1)}], rec2 = [{string.Join(", ", testCase.Rec2)}]");
            Console.WriteLine($"Expected: {testCase.Expected}");
            Console.WriteLine($"Method 1 Actual: {methodOneActual} - {(methodOnePassed ? "PASS" : "FAIL")}");
            Console.WriteLine($"Method 2 Actual: {methodTwoActual} - {(methodTwoPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            passedChecks += (methodOnePassed ? 1 : 0) + (methodTwoPassed ? 1 : 0);
        }

        int totalChecks = testCases.Length * 2;
        Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");
        if (passedChecks != totalChecks)
        {
            Environment.ExitCode = 1;
        }
    }

    private sealed record TestCase(string Name, int[] Rec1, int[] Rec2, bool Expected);

    /// <summary>
    /// 以同一組輸入依序呼叫兩種矩形重疊解法，回傳兩者的實際結果供測試入口比較。
    /// 輸入案例包含兩個四元素座標陣列與預期布林值；此函式不輸出主控台，也不修改輸入陣列。
    /// </summary>
    /// <param name="solution">要執行的矩形重疊解法物件。</param>
    /// <param name="testCase">包含兩個矩形座標與預期結果的固定測試案例。</param>
    /// <returns>依序代表方法一與方法二實際結果的具名 tuple。</returns>
    private static (bool MethodOne, bool MethodTwo) RunTestCase(Program solution, TestCase testCase)
    {
        bool methodOne = solution.IsRectangleOverlap(testCase.Rec1, testCase.Rec2);
        bool methodTwo = solution.IsRectangleOverlap2(testCase.Rec1, testCase.Rec2);
        return (methodOne, methodTwo);
    }

    /// <summary>
    /// 方法一：以相對位置判斷兩個軸對齊矩形是否存在正面積重疊。
    /// 輸入為兩個符合題目契約的四元素座標陣列 [x1, y1, x2, y2]；先排除退化矩形，再檢查四種完全分離情況。
    /// 只要兩個矩形不符合任何分離條件，就回傳 true；否則回傳 false。
    /// </summary>
    /// <param name="rec1">第一個軸對齊矩形的左下角與右上角座標，格式為 [x1, y1, x2, y2]。</param>
    /// <param name="rec2">第二個軸對齊矩形的左下角與右上角座標，格式為 [x1, y1, x2, y2]。</param>
    /// <returns>若兩個矩形的交集面積大於 0，回傳 true；只接觸邊或角落時回傳 false。</returns>
    public bool IsRectangleOverlap(int[] rec1, int[] rec2)
    {
        if (rec1[0] == rec1[2] || rec1[1] == rec1[3] || rec2[0] == rec2[2] || rec2[1] == rec2[3])
        {
            // 題目保證矩形面積非零；此防禦判斷讓退化矩形不會被誤判為重疊。
            return false;
        }

        // 只要在任一軸上完全分離，就不可能形成正面積交集；等號也涵蓋邊或角落接觸。
        return !(rec1[2] <= rec2[0] || // rec1 完全在 rec2 左側
                 rec1[3] <= rec2[1] || // rec1 完全在 rec2 下方
                 rec1[0] >= rec2[2] || // rec1 完全在 rec2 右側
                 rec1[1] >= rec2[3]);  // rec1 完全在 rec2 上方
    }

    /// <summary>
    /// 方法二：將兩個矩形分別投影到 x 軸與 y 軸，檢查兩個方向是否都保留正長度的交集。
    /// 輸入為兩個符合題目契約的四元素座標陣列 [x1, y1, x2, y2]；只有水平與垂直投影都嚴格相交時，才回傳 true。
    /// 使用嚴格大於比較，因此只接觸邊或角落的情況會回傳 false。
    /// </summary>
    /// <param name="rec1">第一個軸對齊矩形的左下角與右上角座標，格式為 [x1, y1, x2, y2]。</param>
    /// <param name="rec2">第二個軸對齊矩形的左下角與右上角座標，格式為 [x1, y1, x2, y2]。</param>
    /// <returns>若 x 軸與 y 軸投影都具有正長度交集，回傳 true；否則回傳 false。</returns>
    public bool IsRectangleOverlap2(int[] rec1, int[] rec2)
    {
        // 交集的右端必須大於左端；相等代表只有邊界接觸，沒有正面積。
        bool hasHorizontalOverlap = Math.Min(rec1[2], rec2[2]) > Math.Max(rec1[0], rec2[0]);
        bool hasVerticalOverlap = Math.Min(rec1[3], rec2[3]) > Math.Max(rec1[1], rec2[1]);

        return hasHorizontalOverlap && hasVerticalOverlap;
    }
}