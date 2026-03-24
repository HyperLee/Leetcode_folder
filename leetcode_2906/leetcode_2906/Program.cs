using System.Runtime.CompilerServices;

namespace leetcode_2906;

class Program
{
    /// <summary>
    /// 2906. Construct Product Matrix
    /// https://leetcode.com/problems/construct-product-matrix/description/?envType=daily-question&envId=2026-03-24
    /// 2906. 构造乘积矩阵
    /// https://leetcode.cn/problems/construct-product-matrix/description/?envType=daily-question&envId=2026-03-24
    ///
    /// [EN]
    /// Given a 0-indexed 2D integer matrix grid of size n * m, we define a 0-indexed 2D matrix p of size n * m
    /// as the product matrix of grid if the following condition is met:
    /// Each element p[i][j] is calculated as the product of all elements in grid except for the element grid[i][j].
    /// This product is then taken modulo 12345.
    /// Return the product matrix of grid.
    ///
    /// [繁中]
    /// 給定一個大小為 n * m 的 0 索引二維整數矩陣 grid，
    /// 若一個大小同為 n * m 的 0 索引二維矩陣 p 滿足以下條件，則稱其為 grid 的乘積矩陣：
    /// 每個元素 p[i][j] 的值為 grid 中除了 grid[i][j] 以外所有元素的乘積，
    /// 並對該乘積取模 12345。
    /// 回傳 grid 的乘積矩陣。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試範例 1: grid = [[1,2],[3,4]]
        // 預期輸出: [[24,12],[8,6]] mod 12345 = [[24,12],[8,6]]
        int[][] grid1 = new int[][] { new int[] { 1, 2 }, new int[] { 3, 4 } };
        int[][] result1 = solution.ConstructProductMatrix(grid1);
        Console.WriteLine("測試範例 1:");
        PrintMatrix(result1);
        // 預期: [[24, 12], [8, 6]]

        // 測試範例 2: grid = [[12345],[2],[1]]
        // grid[0][0]=12345, grid[1][0]=2, grid[2][0]=1
        // p[0][0] = (2*1) % 12345 = 2
        // p[1][0] = (12345*1) % 12345 = 0
        // p[2][0] = (12345*2) % 12345 = 0
        // 預期輸出: [[2],[0],[0]]
        int[][] grid2 = new int[][] { new int[] { 12345 }, new int[] { 2 }, new int[] { 1 } };
        int[][] result2 = solution.ConstructProductMatrix(grid2);
        Console.WriteLine("測試範例 2:");
        PrintMatrix(result2);
        // 預期: [[2], [0], [0]]

        // 測試範例 3: 較大矩陣
        int[][] grid3 = new int[][] { new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 }, new int[] { 7, 8, 9 } };
        int[][] result3 = solution.ConstructProductMatrix(grid3);
        Console.WriteLine("測試範例 3:");
        PrintMatrix(result3);
    }

    static void PrintMatrix(int[][] matrix)
    {
        Console.Write("[");
        for (int i = 0; i < matrix.Length; i++)
        {
            Console.Write("[");
            Console.Write(string.Join(", ", matrix[i]));
            Console.Write("]");
            if (i < matrix.Length - 1) Console.Write(", ");
        }
        Console.WriteLine("]");
    }

    /// <summary>
    /// 解題思路：前綴積 × 後綴積（類似 LeetCode 238 的二維推廣）
    ///
    /// 核心觀念：
    /// 將二維矩陣視為展開後的一維陣列，對每個元素 p[i][j]，
    /// 其值 = 該元素「之前所有元素的乘積（前綴積）」×「之後所有元素的乘積（後綴積）」，再取模 12345。
    ///
    /// 演算法步驟：
    /// 1. 倒序遍歷矩陣，計算每個位置的後綴積並存入結果矩陣 p。
    /// 2. 順序遍歷矩陣，計算前綴積，並將 p[i][j] 乘上前綴積得到最終答案。
    ///
    /// 時間複雜度：O(n * m)，僅需兩次遍歷。
    /// 空間複雜度：O(1)（不計輸出矩陣 p），只需常數額外空間。
    /// </summary>
    /// <param name="grid">輸入的二維整數矩陣</param>
    /// <returns>乘積矩陣 p，其中 p[i][j] = grid 中除 grid[i][j] 外所有元素的乘積 mod 12345</returns>
    public int[][] ConstructProductMatrix(int[][] grid)
    {
        const int MOD = 12345;
        int n = grid.Length;
        int m = grid[0].Length;

        // 初始化結果矩陣 p
        int[][] p = new int[n][];
        for(int i = 0; i < n; i++)
        {
            p[i] = new int[m];
        }

        // 第一輪：倒序遍歷，計算後綴積
        // suffixProduct 記錄從目前位置的下一個元素到最後一個元素的累積乘積
        long suffixProduct = 1;
        for(int i = n - 1; i >= 0; i--)
        {
            for(int j = m - 1; j >= 0; j--)
            {
                // 先將後綴積存入 p[i][j]（此時還不含前綴積）
                p[i][j] = (int)suffixProduct;
                // 將當前元素乘入後綴積，供前面的元素使用
                suffixProduct = (suffixProduct * grid[i][j]) % MOD;
            }
        }

        // 第二輪：順序遍歷，計算前綴積並與已存的後綴積相乘
        // prefixProduct 記錄從第一個元素到目前位置的前一個元素的累積乘積
        long prefixProduct = 1;
        for(int i = 0; i < n; i++)
        {
            for(int j = 0; j < m; j++)
            {
                // p[i][j] = 後綴積 × 前綴積，即為除自身以外所有元素的乘積
                p[i][j] = (int)((long)p[i][j] * prefixProduct % MOD);
                // 將當前元素乘入前綴積，供後面的元素使用
                prefixProduct = (prefixProduct * grid[i][j]) % MOD;
            }
        }

        return p;
    }
}
