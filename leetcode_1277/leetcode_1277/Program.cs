using System;

namespace leetcode_1277;

class Program
{
    /// <summary>
    /// 1277. Count Square Submatrices with All Ones
    /// https://leetcode.com/problems/count-square-submatrices-with-all-ones/description/?envType=daily-question&envId=2025-08-20
    ///
    /// Problem (English): Given an m * n matrix of ones and zeros, return how many square submatrices have all ones.
    /// 題目（中文翻譯）：給定一個 m * n 的矩陣，由 0 和 1 組成，請返回其中全為 1 的正方形子矩陣數量。
    ///
    /// 解法：使用動態規劃，對於每個值為 1 的位置 (i,j)，記錄以 (i,j) 為右下角的最大正方形邊長 dp[i,j]，
    /// dp[i,j] = 1 + min(dp[i-1,j], dp[i,j-1], dp[i-1,j-1])（邊界為第一列或第一行時為 1），累加所有 dp 值即為答案。
    ///
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 範例 1
        int[][] mat1 = new int[][]
        {
            new int[] {0,1,1,1},
            new int[] {1,1,1,1},
            new int[] {0,1,1,1}
        };

        // 範例 2
        int[][] mat2 = new int[][]
        {
            new int[] {1,0,1, 0},
            new int[] {1,1,0, 0},
            new int[] {1,1,0, 0}
        };

        Console.WriteLine("題目（中文）：給定一個 m * n 的矩陣，由 0 和 1 組成，請返回其中全為 1 的正方形子矩陣數量。");
        Console.WriteLine("範例 1 輸出（應為 15）： " + CountSquares(mat1));
        Console.WriteLine("範例 2 輸出（應為 7）： " + CountSquares(mat2));
    }

    /// <summary>
    /// 計算矩陣中全為 1 的正方形子矩陣數量
    /// 輸入：int[][] matrix（不為 null）
    /// 輸出：int，表示符合條件的正方形子矩陣總數
    /// 
    /// 本題目重點是使用動態規劃來計算每個位置 (i,j) 為右下角的最大正方形邊長。
    /// 所以如何找出轉移方程式就是最重要的, 建議看連結的圖示說明比較好理解
    /// 
    /// ref:
    /// https://leetcode.cn/problems/count-square-submatrices-with-all-ones/solutions/3751608/tu-jie-dong-tai-gui-hua-jian-ji-xie-fa-p-1kiy/?envType=daily-question&envId=2025-08-20
    /// https://leetcode.cn/problems/count-square-submatrices-with-all-ones/solutions/101706/tong-ji-quan-wei-1-de-zheng-fang-xing-zi-ju-zhen-2/?envType=daily-question&envId=2025-08-20
    /// </summary>
    /// <param name="matrix"></param>
    /// <returns></returns>
    public static int CountSquares(int[][] matrix)
    {
        // 檢查輸入（若為 null 或沒有列，直接回傳 0）
        if (matrix == null || matrix.Length == 0) return 0;

        // m: 列數（rows），n: 欄數（columns）
        int m = matrix.Length;
        int n = matrix[0].Length;

        // dp[i,j] 表示以 (i,j) 為右下角的、且全為 1 的最大正方形邊長
        // 使用二維陣列以便直接存取已計算的鄰近狀態
        int[,] dp = new int[m, n];

        // result 累加所有位置作為右下角所能形成的正方形數量
        //（例如 dp[i,j] = 3 則表示有 3 個正方形：1x1, 2x2, 3x3）
        int result = 0;

        // 走訪每一個格子，計算 dp 值
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                // 只有當當前格子值為 1 時才可能形成以它為右下角的正方形
                if (matrix[i][j] == 1)
                {
                    // 如果在第一列或第一行，最大邊長只能是 1（只能自己本身）
                    if (i == 0 || j == 0)
                    {
                        dp[i, j] = 1;
                    }
                    else
                    {
                        // 轉移方程（核心）：
                        // 當 matrix[i][j] == 1 時，會受到上 (i-1,j)、左 (i,j-1)、左上 (i-1,j-1)
                        // 三個方向的限制。最大可擴展的邊長為三者最小值再加 1。
                        // dp[i,j] = 1 + min(dp[i-1,j], dp[i,j-1], dp[i-1,j-1])
                        dp[i, j] = 1 + Math.Min(dp[i - 1, j], Math.Min(dp[i, j - 1], dp[i - 1, j - 1]));
                    }

                    // 累加：以 (i,j) 為右下角的所有可能正方形數量
                    result += dp[i, j];
                }
                else
                {
                    // 若當前為 0，則不可能是任何全 1 正方形的右下角
                    dp[i, j] = 0;
                }
            }
        }

        // 時間複雜度：O(m * n)，因為每個格子只處理一次
        // 空間複雜度：O(m * n)（可優化為 O(n) 的一維 DP，但此處保留二維陣列以利說明）
        return result;
    }
}
