namespace leetcode_1582;

class Program
{
    /// <summary>
    /// 1582. Special Positions in a Binary Matrix
    /// https://leetcode.com/problems/special-positions-in-a-binary-matrix/description/?envType=daily-question&envId=2026-03-04
    /// 
    /// Given an m x n binary matrix mat, return the number of special positions in mat.
    ///
    /// A position (i, j) is called special if mat[i][j] == 1 and all other elements
    /// in row i and column j are 0 (rows and columns are 0-indexed).
    ///
    /// 1582. 二進位矩陣中的特殊位置
    /// https://leetcode.cn/problems/special-positions-in-a-binary-matrix/description/?envType=daily-question&envId=2026-03-04
    ///
    /// 給定一個 m x n 的二進位矩陣 mat，回傳 mat 中特殊位置的數量。
    ///
    /// 當 mat[i][j] == 1 且同一列 i 和同一行 j 上的所有其他元素皆為 0
    /// 時，位置 (i, j) 被稱為特殊位置 (列與行皆從 0 開始計數)。
    ///
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new Program();

        // 測試案例 1
        // 輸入：
        //   1 0 0
        //   0 0 1
        //   1 0 0
        // 預期輸出：1（只有 (0,0) 所在列和行的其他元素均為 0）
        int[][] mat1 = new int[][]
        {
            new int[] { 1, 0, 0 },
            new int[] { 0, 0, 1 },
            new int[] { 1, 0, 0 }
        };
        Console.WriteLine($"測試案例 1 輸出：{solution.NumSpecial(mat1)}"); // 預期：1

        // 測試案例 2
        // 輸入：
        //   1 0 0
        //   0 1 0
        //   0 0 1
        // 預期輸出：3（主對角線三個位置均為特殊位置）
        int[][] mat2 = new int[][]
        {
            new int[] { 1, 0, 0 },
            new int[] { 0, 1, 0 },
            new int[] { 0, 0, 1 }
        };
        Console.WriteLine($"測試案例 2 輸出：{solution.NumSpecial(mat2)}"); // 預期：3
    }

    /// <summary>
    /// 解法：模擬 — 預先計算列與行的總和
    ///
    /// 核心思路：
    ///   因為矩陣中每個元素只能是 0 或 1，
    ///   「特殊位置」的充要條件等價於：
    ///     1. mat[i][j] == 1
    ///     2. 第 i 列的總和 == 1（表示該列只有 (i,j) 這一個 1）
    ///     3. 第 j 行的總和 == 1（表示該行只有 (i,j) 這一個 1）
    ///
    /// 演算法步驟：
    ///   Step 1 — 預處理：遍歷整個矩陣，
    ///            分別累加每一列的總和存入 rowssum[]，
    ///            以及每一行的總和存入 colsum[]。
    ///            時間複雜度：O(m×n)
    ///
    ///   Step 2 — 計數：再次遍歷矩陣，
    ///            若 mat[i][j]==1 且 rowssum[i]==1 且 colsum[j]==1，
    ///            則 (i,j) 是特殊位置，結果 +1。
    ///            時間複雜度：O(m×n)
    ///
    /// 整體時間複雜度：O(m×n)，空間複雜度：O(m+n)
    ///
    /// 參考資料：
    ///   https://sweetkikibaby.pixnet.net/blog/post/191310453
    ///   https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/builtin-types/arrays#jagged-arrays
    /// </summary>
    /// <param name="mat">m×n 的二進位矩陣，元素只含 0 或 1</param>
    /// <returns>矩陣中特殊位置的總數</returns>
    /// <example>
    /// <code>
    /// mat = [[1,0,0],[0,0,1],[1,0,0]] => 1
    /// mat = [[1,0,0],[0,1,0],[0,0,1]] => 3
    /// </code>
    /// </example>
    public int NumSpecial(int[][] mat)
    {
        int m = mat.Length;       // 矩陣列數
        int n = mat[0].Length;    // 矩陣行數
        int[] rowssum = new int[m]; // rowssum[i]：第 i 列中所有元素的總和
        int[] colsum = new int[n];  // colsum[j]：第 j 行中所有元素的總和

        // Step 1：預先計算每列、每行的總和
        // 由於元素只有 0/1，總和即代表該列/行中 1 的個數
        for(int i = 0; i < m; i++)
        {
            for(int j = 0; j < n; j++)
            {
                rowssum[i] += mat[i][j];
                colsum[j] += mat[i][j];
            }
        }

        int res = 0;

        // Step 2：判斷每個位置是否為特殊位置
        // 條件：該格為 1，且所在列和行都只有這一個 1（總和均為 1）
        for(int i = 0; i < m; i++)
        {
            for(int j = 0; j < n; j++)
            {
                if(mat[i][j] == 1 && rowssum[i] == 1 && colsum[j] == 1)
                {
                    res++; // 找到一個特殊位置
                }
            }
        }

        return res;
    }
}
