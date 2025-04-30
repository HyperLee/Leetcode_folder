namespace leetcode_073;

class Program
{
    /// <summary>
    /// 73. Set Matrix Zeroes
    /// https://leetcode.com/problems/set-matrix-zeroes/description/?envType=problem-list-v2&envId=oizxjoit
    /// 73. 矩阵置零
    /// https://leetcode.cn/problems/set-matrix-zeroes/description/
    /// 
    /// 題目描述：
    /// 給定一個 m x n 的矩陣，如果一個元素為 0，則將其所在的整行和整列設為 0。
    /// 要求使用原地算法，空間複雜度為 O(1)。
    /// 
    /// 解題提示與想法：
    /// 1. 使用第一行和第一列作為標記區域，記錄哪些行和列需要設為 0。
    /// 2. 使用兩個布林變數分別記錄第一行和第一列是否需要設為 0，避免覆蓋標記資訊。
    /// 3. 先處理內部區域（從第二行和第二列開始），最後再處理第一行和第一列。
    /// 4. 此方法的時間複雜度為 O(m*n)，空間複雜度為 O(1)。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        
        // 測試案例 1
        int[][] matrix1_1 = new int[][] {
            new int[] {1, 1, 1},
            new int[] {1, 0, 1},
            new int[] {1, 1, 1}
        };
        int[][] matrix1_2 = new int[][] {
            new int[] {1, 1, 1},
            new int[] {1, 0, 1},
            new int[] {1, 1, 1}
        };
        Console.WriteLine("測試案例 1:");
        Console.WriteLine("原始矩陣:");
        PrintMatrix(matrix1_1);
        
        Console.WriteLine("\n使用 SetZeroes 的結果:");
        solution.SetZeroes(matrix1_1);
        PrintMatrix(matrix1_1);

        Console.WriteLine("\n使用 SetZeroes2 的結果:");
        solution.SetZeroes2(matrix1_2);
        PrintMatrix(matrix1_2);
        
        // 測試案例 2
        int[][] matrix2_1 = new int[][] {
            new int[] {0, 1, 2, 0},
            new int[] {3, 4, 5, 2},
            new int[] {1, 3, 1, 5}
        };
        int[][] matrix2_2 = new int[][] {
            new int[] {0, 1, 2, 0},
            new int[] {3, 4, 5, 2},
            new int[] {1, 3, 1, 5}
        };
        Console.WriteLine("\n測試案例 2:");
        Console.WriteLine("原始矩陣:");
        PrintMatrix(matrix2_1);

        Console.WriteLine("\n使用 SetZeroes 的結果:");
        solution.SetZeroes(matrix2_1);
        PrintMatrix(matrix2_1);

        Console.WriteLine("\n使用 SetZeroes2 的結果:");
        solution.SetZeroes2(matrix2_2);
        PrintMatrix(matrix2_2);
    }

    // 輔助函數：列印矩陣
    private static void PrintMatrix(int[][] matrix)
    {
        foreach (var row in matrix)
        {
            Console.WriteLine(string.Join(" ", row));
        }
    }


    /// <summary>
    /// 解題思路：
    /// 1. 使用第一行和第一列作為標記，減少空間複雜度到 O (1)
    /// 2. 使用兩個布林變數記錄第一行和第一列是否原本包含 0
    /// 3. 使用第一行和第一列來標記其他行列是否需要設為 0
    /// 4. 最後再處理第一行和第一列
    /// 
    /// 時間複雜度：O (m*n)，其中 m 和 n 分別是矩陣的行數和列數
    /// 空間複雜度：O (1)
    /// 
    /// 1. 檢查第一行：O (n)
    /// 2. 檢查第一列：O (m)
    /// 3. 標記其餘位置：O (m*n)
    /// 4. 根據標記設零：O (m*n)
    /// 5. 處理第一行：O (n)
    /// 6. 處理第一列：O (m)
    /// 
    /// 第一行與第一列處理 matrix 最外層判斷(最外層就直接是 0 的行列)
    /// 第二行與第二列處理 matrix 內部的判斷(內部有 0 的行列)
    /// 例如 main 測試資料範例一與範例二
    /// </summary>
    /// <param name="matrix">輸入的二維整數陣列</param>
    public void SetZeroes(int[][] matrix)
    {
        int rows = matrix.Length;
        int cols = matrix[0].Length;

        // 標記第一行和第一列是否原本包含 0
        bool firstRowZero = false;
        bool firstColZero = false;

        // 檢查第一行是否有 0
        // 注意迴圈大小
        for (int j = 0; j < cols; j++)
        {
            if (matrix[0][j] == 0)
            {
                firstRowZero = true;
                break;
            }
        }

        // 檢查第一列是否有 0
        // 注意迴圈大小
        for (int i = 0; i < rows; i++)
        {
            if (matrix[i][0] == 0)
            {
                firstColZero = true;
                break;
            }
        }

        // 使用第二行和第二列來標記其餘位置的 0
        for (int i = 1; i < rows; i++)
        {
            for (int j = 1; j < cols; j++)
            {
                if (matrix[i][j] == 0)
                {
                    matrix[i][0] = 0; // 標記該行需要設為 0
                    matrix[0][j] = 0; // 標記該列需要設為 0
                }
            }
        }

        // 根據第二行和第二列的標記，將對應的行和列設為 0
        for (int i = 1; i < rows; i++)
        {
            for (int j = 1; j < cols; j++)
            {
                if (matrix[i][0] == 0 || matrix[0][j] == 0)
                {
                    matrix[i][j] = 0;
                }
            }
        }

        // 如果第一行原本有 0，將整個第一行設為 0
        if (firstRowZero)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[0][j] = 0;
            }
        }

        // 如果第一列原本有 0，將整個第一列設為 0
        if (firstColZero)
        {
            for (int i = 0; i < rows; i++)
            {
                matrix[i][0] = 0;
            }
        }
    }


    /// <summary>
    /// 解題思路：
    /// 1. 使用兩個布林陣列分別記錄需要設為 0 的行和列
    /// 2. 第一次遍歷找出所有 0 的位置，並在對應的布林陣列中標記
    /// 3. 第二次遍歷根據標記將對應的行和列設為 0
    /// 
    /// 時間複雜度：O (m*n)，其中 m 和 n 分別是矩陣的行數和列數
    /// 空間複雜度：O (m+n)，需要額外的空間來存儲行和列的標記
    /// 
    /// 這方法比較好理解，但空間複雜度較高。
    /// 這是第二種解法，使用額外的空間來存儲行和列的標記
    /// </summary>
    /// <param name="matrix">輸入的二維整數陣列</param>
    public void SetZeroes2(int[][] matrix)
    {
        int m = matrix.Length;
        int n = matrix[0].Length;
        bool[] row = new bool[m]; // 記錄需要設為 0 的行
        bool[] col = new bool[n]; // 記錄需要設為 0 的列

        // 第一次遍歷：標記包含 0 的行和列
        for(int i = 0; i < m; i++)
        {
            for(int j = 0; j < n; j++)
            {
                if (matrix[i][j] == 0)
                {
                    row[i] = true; // 標記該行需要設為 0
                    col[j] = true; // 標記該列需要設為 0
                }
            }
        }

        // 第二次遍歷：根據標記設置 0
        for(int i = 0; i < m; i++)
        {
            for(int j = 0; j < n; j++)
            {
                if (row[i] || col[j])
                {
                    matrix[i][j] = 0;
                }
            }
        }
    }
}
