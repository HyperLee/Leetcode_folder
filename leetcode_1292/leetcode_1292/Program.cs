namespace leetcode_1292;

/// <summary>
/// LeetCode 1292. Maximum Side Length of a Square with Sum Less than or Equal to Threshold
/// 
/// 題目連結：
/// - https://leetcode.com/problems/maximum-side-length-of-a-square-with-sum-less-than-or-equal-to-threshold/
/// - https://leetcode.cn/problems/maximum-side-length-of-a-square-with-sum-less-than-or-equal-to-threshold/
/// 
/// 題目描述：
/// 給定一個 m x n 的矩陣 mat 與一個整數 threshold，
/// 回傳元素和小於或等於 threshold 的正方形之最大邊長；若不存在此類正方形，則回傳 0。
/// 
/// 解題思路：
/// 1. 使用二維前綴和預處理，使任意子矩陣的元素和可以在 O(1) 時間內計算。
/// 2. 暴力枚舉每個位置作為正方形左上角，嘗試擴展邊長。
/// 3. 關鍵優化：從當前已知的最大邊長 ans+1 開始枚舉，而非從 1 開始。
///    因為邊長 ≤ ans 的正方形不會讓答案變得更大，沒有枚舉的意義。
/// 
/// 時間複雜度：O(m × n)，每個格子最多被訪問常數次
/// 空間複雜度：O(m × n)，用於儲存前綴和陣列
/// </summary>
class Program
{
    /// <summary>
    /// 程式進入點，包含測試案例的執行。
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        var program = new Program();

        // 測試案例 1：預期輸出 2
        // 解釋：正方形 [[1,1],[1,1]] 的元素和為 4，小於等於 threshold=4
        int[][] mat1 =
        [
            [1, 1, 3, 2, 4, 3, 2],
            [1, 1, 3, 2, 4, 3, 2],
            [1, 1, 3, 2, 4, 3, 2]
        ];
        int threshold1 = 4;
        int result1 = program.MaxSideLength(mat1, threshold1);
        Console.WriteLine($"測試案例 1: mat = [[1,1,3,2,4,3,2],...], threshold = {threshold1}");
        Console.WriteLine($"結果: {result1}, 預期: 2");
        Console.WriteLine();

        // 測試案例 2：預期輸出 2
        int[][] mat2 =
        [
            [2, 2, 2, 2, 2],
            [2, 2, 2, 2, 2],
            [2, 2, 2, 2, 2],
            [2, 2, 2, 2, 2],
            [2, 2, 2, 2, 2]
        ];
        int threshold2 = 1;
        int result2 = program.MaxSideLength(mat2, threshold2);
        Console.WriteLine($"測試案例 2: 5x5 全為 2 的矩陣, threshold = {threshold2}");
        Console.WriteLine($"結果: {result2}, 預期: 0 (沒有元素 ≤ 1)");
        Console.WriteLine();

        // 測試案例 3：預期輸出 3
        int[][] mat3 =
        [
            [1, 1, 1, 1],
            [1, 0, 0, 0],
            [1, 0, 0, 0],
            [1, 0, 0, 0]
        ];
        int threshold3 = 6;
        int result3 = program.MaxSideLength(mat3, threshold3);
        Console.WriteLine($"測試案例 3: 含有 0 的 4x4 矩陣, threshold = {threshold3}");
        Console.WriteLine($"結果: {result3}, 預期: 3");
        Console.WriteLine();

        // 測試案例 4：單一元素
        int[][] mat4 = [[5]];
        int threshold4 = 5;
        int result4 = program.MaxSideLength(mat4, threshold4);
        Console.WriteLine($"測試案例 4: 單一元素 [[5]], threshold = {threshold4}");
        Console.WriteLine($"結果: {result4}, 預期: 1");
    }

    /// <summary>
    /// 計算元素和小於或等於 threshold 的正方形之最大邊長。
    /// 
    /// 演算法說明：
    /// 1. 先建立二維前綴和陣列 sum，其中 sum[i+1][j+1] 代表 mat[0..i][0..j] 的元素總和。
    /// 2. 枚舉每個位置 (i, j) 作為正方形的左上角。
    /// 3. 關鍵優化：從 res+1 開始嘗試擴展邊長（而非從 1 開始），
    ///    因為比 res 小的邊長不會更新答案，沒有枚舉的意義。
    /// 4. 若正方形不超出邊界且元素和 ≤ threshold，則持續擴展。
    /// </summary>
    /// <param name="mat">輸入的 m x n 整數矩陣</param>
    /// <param name="threshold">正方形元素和的上限閾值</param>
    /// <returns>滿足條件的最大正方形邊長，若不存在則回傳 0</returns>
    /// <example>
    /// <code>
    /// int[][] mat = [[1,1,3,2,4,3,2],[1,1,3,2,4,3,2],[1,1,3,2,4,3,2]];
    /// int result = MaxSideLength(mat, 4); // 回傳 2
    /// </code>
    /// </example>
    public int MaxSideLength(int[][] mat, int threshold)
    {
        int m = mat.Length;
        int n = mat[0].Length;

        // 建立二維前綴和陣列，大小為 (m+1) x (n+1)
        // sum[i+1][j+1] 儲存 mat[0..i][0..j] 的元素總和
        // 多一行一列是為了避免邊界檢查，簡化程式碼
        int[][] sum = new int[m + 1][];
        for (int i = 0; i <= m; i++)
        {
            sum[i] = new int[n + 1];
        }

        // 計算二維前綴和
        // 公式：sum[i+1][j+1] = sum[i+1][j] + sum[i][j+1] - sum[i][j] + mat[i][j]
        // 利用容斥原理：左邊區塊 + 上方區塊 - 重疊區塊 + 當前元素
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                sum[i + 1][j + 1] = sum[i + 1][j] + sum[i][j + 1] - sum[i][j] + mat[i][j];
            }
        }

        // res 記錄當前找到的最大邊長
        int res = 0;

        // 枚舉每個位置 (i, j) 作為正方形左上角
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                // 關鍵優化：從 res+1 開始嘗試（而非從 1 開始）
                // 因為邊長 ≤ res 的正方形不會讓答案變得更大
                // 這使得時間複雜度從 O(mn × min(m,n)) 優化為 O(mn)
                while (i + res < m && j + res < n && Query(sum, i, j, i + res, j + res) <= threshold)
                {
                    res++;
                }
            }
        }

        return res;
    }

    /// <summary>
    /// 使用前綴和陣列，在 O(1) 時間內查詢子矩陣 mat[r1..r2][c1..c2] 的元素總和。
    /// 
    /// 原理說明（容斥原理）：
    /// 要計算區域 (r1,c1) 到 (r2,c2) 的元素和，利用前綴和的性質：
    /// - sum[r2+1][c2+1]：從 (0,0) 到 (r2,c2) 的總和
    /// - sum[r2+1][c1]：  需減去左側多餘區域
    /// - sum[r1][c2+1]：  需減去上方多餘區域  
    /// - sum[r1][c1]：    因左側和上方重疊減了兩次，需加回來
    /// 
    /// 視覺化示意：
    /// <code>
    /// +-------+-------+
    /// |   A   |   B   |  sum[r1][c2+1] = A + B
    /// +-------+-------+
    /// |   C   |   D   |  sum[r2+1][c1] = A + C
    /// +-------+-------+  sum[r2+1][c2+1] = A + B + C + D
    ///                    sum[r1][c1] = A
    ///                    D = 全部 - 左側 - 上方 + 重疊
    /// </code>
    /// </summary>
    /// <param name="sum">預處理的二維前綴和陣列</param>
    /// <param name="r1">子矩陣左上角的列索引</param>
    /// <param name="c1">子矩陣左上角的行索引</param>
    /// <param name="r2">子矩陣右下角的列索引</param>
    /// <param name="c2">子矩陣右下角的行索引</param>
    /// <returns>子矩陣 mat[r1..r2][c1..c2] 的元素總和</returns>
    private int Query(int[][] sum, int r1, int c1, int r2, int c2)
    {
        // 容斥原理：全部區域 - 左側區域 - 上方區域 + 重疊區域（因被減了兩次）
        return sum[r2 + 1][c2 + 1] - sum[r2 + 1][c1] - sum[r1][c2 + 1] + sum[r1][c1];
    }
}
