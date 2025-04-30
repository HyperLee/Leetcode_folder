namespace leetcode_048;

class Program
{
    /// <summary>
    /// 48. Rotate Image
    /// https://leetcode.com/problems/rotate-image/description/?envType=problem-list-v2&envId=oizxjoit
    /// 48. 旋轉影像
    /// https://leetcode.cn/problems/rotate-image/description/
    /// 給定一個 n x n 的二維矩陣 matrix 代表一個圖像，將圖像順時針旋轉 90 度。
    /// 你必須在原地旋轉圖像，這意味著你需要直接修改輸入的二維矩陣。請不要使用額外的矩陣來旋轉圖像。
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new Program();

        // 測試案例 1: 3x3 矩陣
        int[][] matrix1 = new int[][] {
            new int[] {1, 2, 3},
            new int[] {4, 5, 6},
            new int[] {7, 8, 9}
        };
        Console.WriteLine("測試案例 1 - 3x3 矩陣：");
        Console.WriteLine("原始矩陣：");
        PrintMatrix(matrix1);
        solution.Rotate(matrix1);
        Console.WriteLine("使用 Rotate 旋轉後：");
        PrintMatrix(matrix1);

        // 重新創建矩陣用於測試優化版本
        matrix1 = new int[][] {
            new int[] {1, 2, 3},
            new int[] {4, 5, 6},
            new int[] {7, 8, 9}
        };
        Console.WriteLine("\n使用 RotateOptimized 測試：");
        Console.WriteLine("原始矩陣：");
        PrintMatrix(matrix1);
        solution.RotateOptimized(matrix1);
        Console.WriteLine("使用 RotateOptimized 旋轉後：");
        PrintMatrix(matrix1);

        // 測試案例 2: 4x4 矩陣
        int[][] matrix2 = new int[][] {
            new int[] {5, 1, 9, 11},
            new int[] {2, 4, 8, 10},
            new int[] {13, 3, 6, 7},
            new int[] {15, 14, 12, 16}
        };
        Console.WriteLine("\n測試案例 2 - 4x4 矩陣：");
        Console.WriteLine("原始矩陣：");
        PrintMatrix(matrix2);
        solution.Rotate(matrix2);
        Console.WriteLine("使用 Rotate 旋轉後：");
        PrintMatrix(matrix2);

        // 重新創建矩陣用於測試優化版本
        matrix2 = new int[][] {
            new int[] {5, 1, 9, 11},
            new int[] {2, 4, 8, 10},
            new int[] {13, 3, 6, 7},
            new int[] {15, 14, 12, 16}
        };
        Console.WriteLine("\n使用 RotateOptimized 測試：");
        Console.WriteLine("原始矩陣：");
        PrintMatrix(matrix2);
        solution.RotateOptimized(matrix2);
        Console.WriteLine("使用 RotateOptimized 旋轉後：");
        PrintMatrix(matrix2);
    }

    // 輔助方法：列印矩陣
    private static void PrintMatrix(int[][] matrix)
    {
        foreach (var row in matrix)
        {
            Console.WriteLine(string.Join("\t", row));
        }
    }

    /// <summary>
    /// 將 n x n 的二維矩陣順時針旋轉 90 度
    /// </summary>
    /// <param name="matrix">要旋轉的二維矩陣，大小為 n x n</param>
    /// <remarks>
    /// 實現方法：
    /// 1. 首先進行上下翻轉：將第 i 行與第 (n-1-i) 行交換
    /// 2. 然後進行對角線翻轉：將位置 (i,j) 與位置 (j,i) 交換
    /// 
    /// 時間複雜度：O(n²)，其中 n 是矩陣的邊長
    /// 空間複雜度：O(1)，原地修改矩陣
    /// 
    /// 此方法比較好理解，就兩步驟而已
    /// 先上下交換，再來就對角交換結束
    /// </remarks>
    public void Rotate(int[][] matrix)
    {
        // 獲取矩陣的邊長
        int n = matrix.Length;

        // 步驟1: 上下翻轉矩陣
        // 只需要遍歷上半部分（n/2行），與下半部分對應行交換
        // 第一行與最後一行互換, 第二行與倒數第二行互換, 以此類推
        // 中間行不需要處理，因為它在上下翻轉後會保持不變
        for (int i = 0; i < n / 2; i++)
        {
            // 遍歷當前行的每一列
            for (int j = 0; j < n; j++)
            {
                // 使用臨時變數存儲上方元素
                int temp = matrix[i][j];
                // 將下方元素移到上方
                matrix[i][j] = matrix[n - 1 - i][j];
                // 將原上方元素移到下方
                matrix[n - 1 - i][j] = temp;
            }
        }

        // 步驟2: 對角線翻轉（以左上到右下的對角線為軸）
        // 只需要遍歷對角線右上方的元素
        for (int i = 0; i < n; i++)
        {
            // j 從 i+1 開始，避免重複交換和對角線上的自我交換
            for (int j = i + 1; j < n; j++)
            {
                // 交換 (i,j) 和 (j,i) 的元素
                int temp = matrix[i][j];
                matrix[i][j] = matrix[j][i];
                matrix[j][i] = temp;
            }
        }
    }

    /// <summary>
    /// 優化版本：將矩陣順時針旋轉 90 度
    /// 此版本直接在一次遍歷中完成四個位置的旋轉，減少了額外的翻轉操作
    /// 
    /// 時間複雜度：O(n²)，其中 n 是矩陣的邊長
    /// 空間複雜度：O(1)，原地修改矩陣
    /// 
    /// 這方法比較抽象, 要腦內幻想各位置
    /// 第一次是最外圍四個角落位置交換, 
    /// 第二次是第二圈四個角落位置交換,要稍微轉一下角度幻想位置
    /// 進行四個位置的循環交換，順時針方向：
    /// 左上 <- 左下 <- 右下 <- 右上 <- 左上
    /// </summary>
    /// <param name="matrix">要旋轉的二維矩陣，大小為 n x n</param>
    public void RotateOptimized(int[][] matrix) 
    {
        int n = matrix.Length;
        
        // 只需要遍歷矩陣的四分之一區域
        // (n+1)/2 確保在 n 為奇數時也能正確處理中心點
        for (int i = 0; i < (n + 1) / 2; i++) 
        {
            // n/2 確保我們只處理需要的列
            // 對於偶數大小的矩陣，這剛好是一半
            // 對於奇數大小的矩陣，這會避免重複處理中心點
            for (int j = 0; j < n / 2; j++) 
            {
                // 暫存第一個位置的值
                // 位置 (i,j) 位於左上角區域
                int temp = matrix[i][j];
                
                // 進行四個位置的循環交換，順時針方向：
                // 左上 <- 左下 <- 右下 <- 右上 <- 左上
                
                // 1. 左上位置 (i,j) 接收來自左下位置 (n-1-j,i) 的值
                matrix[i][j] = matrix[n - 1 - j][i];
                
                // 2. 左下位置 (n-1-j,i) 接收來自右下位置 (n-1-i,n-1-j) 的值
                matrix[n - 1 - j][i] = matrix[n - 1 - i][n - 1 - j];
                
                // 3. 右下位置 (n-1-i,n-1-j) 接收來自右上位置 (j,n-1-i) 的值
                matrix[n - 1 - i][n - 1 - j] = matrix[j][n - 1 - i];
                
                // 4. 右上位置 (j,n-1-i) 接收最初暫存的左上角的值
                matrix[j][n - 1 - i] = temp;
            }
        }
    }
}
