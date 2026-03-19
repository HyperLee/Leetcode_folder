namespace leetcode_3212;

class Program
{
    /// <summary>
    /// 3212. Count Submatrices With Equal Frequency of X and Y
    /// https://leetcode.com/problems/count-submatrices-with-equal-frequency-of-x-and-y/description/?envType=daily-question&envId=2026-03-19
    /// 3212. 統計 X 和 Y 頻數相等的子矩陣數量
    /// https://leetcode.cn/problems/count-submatrices-with-equal-frequency-of-x-and-y/description/?envType=daily-question&envId=2026-03-19
    ///
    /// [EN]
    /// Given a 2D character matrix grid, where grid[i][j] is either 'X', 'Y', or '.', 
    /// return the number of submatrices that contain:
    ///   - grid[0][0]
    ///   - an equal frequency of 'X' and 'Y'.
    ///   - at least one 'X'.
    ///
    /// [繁體中文]
    /// 給定一個二維字元矩陣 grid，其中 grid[i][j] 為 'X'、'Y' 或 '.' 其中之一，
    /// 回傳滿足以下條件的子矩陣數量：
    ///   - 包含 grid[0][0]
    ///   - 'X' 與 'Y' 的出現頻數相等
    ///   - 至少包含一個 'X'
    ///
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試範例 1:
        // grid = [["X","Y","."],["Y",".","."]]
        // 預期輸出: 3
        // 說明: 以 (0,0)、(0,1)、(1,1) 為右下角的子矩陣各含相等數量的 X 與 Y
        char[][] grid1 =
        [
            ['X', 'Y', '.'],
            ['Y', '.', '.']
        ];
        Console.WriteLine($"測試 1: {solution.NumberOfSubmatrices(grid1)}"); // 預期: 3

        // 測試範例 2:
        // grid = [["X","X"],["X","Y"]]
        // 預期輸出: 0
        // 說明: 沒有任何以 grid[0][0] 為左上角的子矩陣能滿足 X 與 Y 頻數相等
        char[][] grid2 =
        [
            ['X', 'X'],
            ['X', 'Y']
        ];
        Console.WriteLine($"測試 2: {solution.NumberOfSubmatrices(grid2)}"); // 預期: 0

        // 測試範例 3:
        // grid = [[".","."],[".","."]]
        // 預期輸出: 0
        // 說明: 沒有任何 X，不符合「至少包含一個 X」的條件
        char[][] grid3 =
        [
            ['.', '.'],
            ['.', '.']
        ];
        Console.WriteLine($"測試 3: {solution.NumberOfSubmatrices(grid3)}"); // 預期: 0
    }

    /// <summary>
    /// 方法一：二維前綴和
    ///
    /// 解題思路：
    /// 因為子矩陣必須包含 grid[0][0]（即左上角固定），所以只需列舉右下角 (i, j) 即可確定子矩陣。
    /// 利用二維前綴和同時追蹤兩個維度的資訊：
    ///   - sum[i,j,0]：以 (i-1,j-1) 為右下角的子矩陣中 X 的數量 減去 Y 的數量（差值）。
    ///                  遇到 'X' 則 +1，遇到 'Y' 則 -1，遇到 '.' 不變。
    ///                  當差值為 0 時，表示 X 與 Y 的頻數相等。
    ///   - sum[i,j,1]：以 (i-1,j-1) 為右下角的子矩陣中是否至少包含一個 'X'（1 表示是，0 表示否）。
    ///
    /// 時間複雜度：O(n × m)
    /// 空間複雜度：O(n × m)
    /// </summary>
    /// <param name="grid">二維字元矩陣，元素為 'X'、'Y' 或 '.'</param>
    /// <returns>滿足條件的子矩陣數量</returns>
    public int NumberOfSubmatrices(char[][] grid)
    {
        int n = grid.Length;
        int m = grid[0].Length;
        int res = 0;

        // sum[i,j,0] = 以 (i-1,j-1) 為右下角之子矩陣的 (X數量 - Y數量)
        // sum[i,j,1] = 以 (i-1,j-1) 為右下角之子矩陣是否包含至少一個 X
        int[,,] sum = new int[n + 1, m + 1, 2];

        for(int i = 0; i < n; i++)
        {
            for(int j = 0; j < m; j++)
            {
                if(grid[i][j] == 'X')
                {
                    // 二維前綴和公式：上方 + 左方 - 左上方（容斥）+ 當前貢獻
                    sum[i + 1, j + 1, 0] = sum[i + 1, j, 0] + sum[i, j + 1, 0] - sum[i, j, 0] + 1;
                    // 當前格為 X，子矩陣必定包含 X
                    sum[i + 1, j + 1, 1] = 1;
                }
                else if(grid[i][j] == 'Y')
                {
                    // 遇到 Y，差值 -1
                    sum[i + 1, j + 1, 0] = sum[i + 1, j, 0] + sum[i, j + 1, 0] - sum[i, j, 0] - 1;
                    // 是否包含 X 取決於上方或左方的子矩陣是否已包含 X
                    sum[i + 1, j + 1, 1] = (sum[i + 1, j, 1] | sum[i, j + 1, 1]) == 1 ? 1 : 0;
                }
                else
                {
                    // '.' 不影響差值
                    sum[i + 1, j + 1, 0] = sum[i + 1, j, 0] + sum[i, j + 1, 0] - sum[i, j, 0];
                    // 是否包含 X 取決於上方或左方的子矩陣
                    sum[i + 1, j + 1, 1] = (sum[i + 1, j, 1] | sum[i, j + 1, 1]) == 1 ? 1 : 0;
                }

                // 差值為 0 且至少包含一個 X → 符合條件
                if(sum[i + 1, j + 1, 0] == 0 && sum[i + 1, j + 1, 1] == 1)
                {
                    res++;
                }
            }
        }
        return res;
    }
}
