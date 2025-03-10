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
        // 測試案例 1: 3x7 網格
        Console.WriteLine("測試案例 1 (3x7):");
        Console.WriteLine($"Expected: 28, Output: {UniquePaths(3, 7)}");
        
        // 測試案例 2: 3x2 網格
        Console.WriteLine("\n測試案例 2 (3x2):");
        Console.WriteLine($"Expected: 3, Output: {UniquePaths(3, 2)}");
        
        // 測試案例 3: 7x3 網格
        Console.WriteLine("\n測試案例 3 (7x3):");
        Console.WriteLine($"Expected: 28, Output: {UniquePaths(7, 3)}");
        
        // 測試案例 4: 邊界情況 - 3x1 網格
        Console.WriteLine("\n測試案例 4 (3x1):");
        Console.WriteLine($"Expected: 1, Output: {UniquePaths(3, 1)}");
        
        // 測試案例 5: 較大的網格 - 10x10
        Console.WriteLine("\n測試案例 5 (10x10):");
        Console.WriteLine($"Expected: 48620, Output: {UniquePaths(10, 10)}");
    }


    /// <summary>
    /// 遞迴方法，計算從(i,j)到起點(0,0)的不同路徑數
    /// 不用 memo 也可以, 但是加入之後可以避免重複計算. 節省時間
    ///  
    /// 起始位置為左上角(0,0)，終點位置為右下角(m-1,n-1)
    /// 從終點位置開始，向上和向左搜尋可能的路徑
    /// 終點往回推出各別的路徑數
    /// </summary>
    /// <param name="i">當前位置的行座標</param>
    /// <param name="j">當前位置的列座標</param>
    /// <param name="memo">記憶化數組，用於存儲已計算的結果</param>
    /// <returns>不同路徑的總數</returns>
    private static int dfs(int i, int j, int[][] memo) 
    {
        // 檢查是否超出邊界
        if(i < 0 || j < 0) 
        {
            return 0;
        }

        // 當在最上方或最左方時，只有一種路徑可選
        if (i == 0 || j == 0) 
        {
            return 1;
        }

        // 如果該位置已經計算過，直接返回記憶化的結果
        if (memo[i][j] != 0) 
        {
            return memo[i][j];
        }

        // 當前位置的路徑數 = 上方位置的路徑數 + 左方位置的路徑數
        memo[i][j] = dfs(i - 1, j, memo) + dfs(i, j - 1, memo);
        
        return memo[i][j];
    }


    /// <summary>
    /// 計算從起點到終點的不同路徑數
    /// 
    /// ref:
    /// https://leetcode.cn/problems/unique-paths/solutions/514311/bu-tong-lu-jing-by-leetcode-solution-hzjf/
    /// https://leetcode.cn/problems/unique-paths/solutions/3062432/liang-chong-fang-fa-dong-tai-gui-hua-zu-o5k32/
    /// https://leetcode.cn/problems/unique-paths/solutions/2201991/62-bu-tong-lu-jing-by-stormsunshine-b8ji/
    /// 可以參考第一個連結,官方解答有影片 比較好理解邊界處理
    /// </summary>
    /// <param name="m">網格的行數</param>
    /// <param name="n">網格的列數</param>
    /// <returns>從左上角到右下角的不同路徑總數</returns>
    public static int UniquePaths(int m, int n)
    {
        // 初始化記憶化數組
        int[][] memo = new int[m][];

        // 初始化二維陣列
        for (int i = 0; i < m; i++)
        {
            memo[i] = new int[n];
        }

        // 從終點開始計算到起點的路徑數
        return dfs(m - 1, n - 1, memo);
    }
}
