namespace leetcode_1886;

class Program
{
    /// <summary>
    /// 1886. Determine Whether Matrix Can Be Obtained By Rotation
    /// https://leetcode.com/problems/determine-whether-matrix-can-be-obtained-by-rotation/description/?envType=daily-question&envId=2026-03-22
    /// 1886. 判斷矩陣經旋轉後是否一致
    /// https://leetcode.cn/problems/determine-whether-matrix-can-be-obtained-by-rotation/description/?envType=daily-question&envId=2026-03-22
    ///
    /// Given two n x n binary matrices mat and target, return true if it is possible to make mat equal to target
    /// by rotating mat in 90-degree increments, or false otherwise.
    ///
    /// 給定兩個 n x n 的二進位矩陣 mat 與 target，若可以透過將 mat 以 90 度為單位旋轉，
    /// 使其與 target 相等，則回傳 true，否則回傳 false。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // Example 1: mat 旋轉 90 度後等於 target => true
        int[][] mat1 = [[0, 1], [1, 0]];
        int[][] target1 = [[1, 0], [0, 1]];
        Console.WriteLine($"Example 1: {solution.FindRotation(mat1, target1)}"); // true

        // Example 2: mat 無論旋轉幾次都無法等於 target => false
        int[][] mat2 = [[0, 1], [1, 1]];
        int[][] target2 = [[1, 0], [0, 1]];
        Console.WriteLine($"Example 2: {solution.FindRotation(mat2, target2)}"); // false

        // Example 3: mat 本身已經等於 target (旋轉 0 度) => true
        int[][] mat3 = [[0, 0, 0], [0, 1, 0], [1, 1, 1]];
        int[][] target3 = [[1, 1, 1], [0, 1, 0], [0, 0, 0]];
        Console.WriteLine($"Example 3: {solution.FindRotation(mat3, target3)}"); // true
    }

    /// <summary>
    /// 模擬旋轉操作：將矩陣順時針旋轉 90 度，最多旋轉 4 次（0°、90°、180°、270°），
    /// 每次旋轉後與 target 比較，若一致則回傳 true。
    /// 旋轉 4 次後回到原始狀態，因此最多只需比較 4 次。
    /// 原地旋轉使用四點循環交換法，參考 LeetCode 48. Rotate Image。
    /// 時間複雜度：O(n^2)，空間複雜度：O(1)。
    /// </summary>
    /// <param name="mat">待旋轉的 n x n 二進位矩陣</param>
    /// <param name="target">目標 n x n 二進位矩陣</param>
    /// <returns>若 mat 經旋轉後可與 target 一致，回傳 true；否則回傳 false</returns>
    public bool FindRotation(int[][] mat, int[][] target)
    {
        int n = mat.Length;

        // 最多旋轉 4 次（90° x 4 = 360° 回到原位）
        for(int k = 0; k < 4; k++)
        {
            // 原地順時針旋轉 90 度：只需遍歷左上角 1/4 區域
            for(int i = 0; i < n / 2; i++)
            {
                for(int j = 0; j < (n + 1) / 2; j++)
                {
                    // 四點循環交換：(i,j) <- (n-1-j,i) <- (n-1-i,n-1-j) <- (j,n-1-i)
                    int temp = mat[i][j];
                    mat[i][j] = mat[n-1-j][i];
                    mat[n-1-j][i] = mat[n-1-i][n-1-j];
                    mat[n-1-i][n-1-j] = mat[j][n-1-i];
                    mat[j][n-1-i] = temp;
                }
            }

            // 旋轉完成後，比較是否與 target 一致
            if(IsEqual(mat, target))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 逐元素比較兩個 n x n 矩陣是否完全相等。
    /// 遍歷所有位置 (i, j)，只要有任一元素不同即回傳 false。
    /// 時間複雜度：O(n^2)，空間複雜度：O(1)。
    /// </summary>
    /// <param name="mat">第一個 n x n 矩陣</param>
    /// <param name="target">第二個 n x n 矩陣</param>
    /// <returns>若兩矩陣完全相等回傳 true，否則回傳 false</returns>
    private bool IsEqual(int[][] mat, int[][] target)
    {
        int n = mat.Length;

        // 逐列逐行比較每個元素
        for(int i = 0; i < n; i++)
        {
            for(int j = 0; j < n; j++)
            {
                // 只要有一個元素不同，兩矩陣即不相等
                if(mat[i][j] != target[i][j])
                {
                    return false;
                }
            }
        }

        return true;
    }
}
