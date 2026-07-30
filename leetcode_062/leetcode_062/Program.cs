namespace leetcode_062;

class Program
{
    /// <summary>
    /// 62. Unique Paths
    /// https://leetcode.com/problems/unique-paths/description/
    /// 62. 不同路徑
    /// https://leetcode.cn/problems/unique-paths/description/
    /// 解題思路:
    /// 1. 使用動態規劃(Dynamic Programming)的方式，通過記憶化搜索(Memoization)來解決
    /// 2. 從終點(m-1,n-1)開始，向上和向左搜尋可能的路徑
    /// 3. 基本情況:
    ///    - 當i或j小於0時，表示超出邊界，回傳0
    ///    - 當i或j等於0時，表示在邊緣，只有一種路徑，回傳1
    /// 4. 使用memo二維陣列來儲存已計算過的結果，避免重複計算
    /// 時間複雜度: O(m*n)
    /// 空間複雜度: O(m*n)
    /// </summary>
    /// <param name="args"></param> 
    static void Main(string[] args)
    {
        (int M, int N, int Expected)[] testCases =
        [
            (3, 7, 28),
            (3, 2, 3),
            (7, 3, 28),
            (3, 1, 1),
            (1, 1, 1),
            (10, 10, 48620)
        ];

        int passedCases = 0;

        for (int index = 0; index < testCases.Length; index++)
        {
            (int m, int n, int expected) = testCases[index];

            if (RunTestCase(index + 1, m, n, expected))
            {
                passedCases++;
            }
        }

        Console.WriteLine($"Result: {passedCases}/{testCases.Length} passed.");
    }

    /// <summary>
    /// 執行一組固定測試案例，比對記憶化搜尋的實際結果與預期結果，
    /// 並輸出案例編號、網格尺寸、預期值、實際值及 PASS/FAIL。
    /// </summary>
    /// <param name="caseNumber">從 1 開始顯示的測試案例編號。</param>
    /// <param name="m">網格列數，符合題目限制 1 到 100。</param>
    /// <param name="n">網格欄數，符合題目限制 1 到 100。</param>
    /// <param name="expected">此網格從左上角走到右下角的預期路徑數。</param>
    /// <returns>實際結果與預期結果相同時回傳 <see langword="true"/>，否則回傳 <see langword="false"/>。</returns>
    private static bool RunTestCase(int caseNumber, int m, int n, int expected)
    {
        int actual = UniquePaths(m, n);
        bool passed = actual == expected;
        string status = passed ? "PASS" : "FAIL";

        Console.WriteLine(
            $"{status} | Case {caseNumber} | m = {m}, n = {n} | Expected: {expected} | Actual: {actual}");

        return passed;
    }

    /// <summary>
    /// 以遞迴與記憶化搜尋計算從左上角 <c>(0, 0)</c> 到指定座標 <c>(i, j)</c>
    /// 的不同路徑數。每個狀態只計算一次，再由上方與左方狀態的結果相加取得答案。
    /// </summary>
    /// <param name="i">目標位置的列索引；遞迴時可能減為負數以表示越界。</param>
    /// <param name="j">目標位置的欄索引；遞迴時可能減為負數以表示越界。</param>
    /// <param name="memo">尺寸為 <c>m × n</c> 的記憶化陣列，儲存已計算狀態的路徑數。</param>
    /// <returns>從 <c>(0, 0)</c> 到 <c>(i, j)</c> 的不同路徑總數；越界時回傳 0。</returns>
    private static int dfs(int i, int j, int[][] memo)
    {
        // 越過上方或左方邊界代表這個方向無法形成合法路徑。
        if (i < 0 || j < 0)
        {
            return 0;
        }

        // 首列只能一路向右，首欄只能一路向下，因此都只有一條路徑。
        if (i == 0 || j == 0)
        {
            return 1;
        }

        // 合法狀態的答案至少為 1，所以 0 可以安全表示「尚未計算」。
        if (memo[i][j] != 0)
        {
            return memo[i][j];
        }

        // 走到目前位置的最後一步，只可能來自上方或左方。
        memo[i][j] = dfs(i - 1, j, memo) + dfs(i, j - 1, memo);

        return memo[i][j];
    }

    /// <summary>
    /// 計算機器人在 <c>m × n</c> 網格中，只能向右或向下移動時，
    /// 從左上角到右下角的不同路徑總數。方法由終點反向遞迴至上方與左方狀態，
    /// 並以二維記憶化陣列避免重複計算。
    /// </summary>
    /// <param name="m">網格列數，題目保證介於 1 到 100。</param>
    /// <param name="n">網格欄數，題目保證介於 1 到 100。</param>
    /// <returns>從左上角到右下角的不同路徑總數，題目保證結果不超過 <see cref="int.MaxValue"/>。</returns>
    public static int UniquePaths(int m, int n)
    {
        int[][] memo = new int[m][];

        for (int i = 0; i < m; i++)
        {
            memo[i] = new int[n];
        }

        // 從終點反推到起點，memo 會保存沿途所有已解出的子問題。
        return dfs(m - 1, n - 1, memo);
    }
}