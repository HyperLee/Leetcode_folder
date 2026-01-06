namespace leetcode_1975;

class Program
{
    /// <summary>
    /// 1975. Maximum Matrix Sum
    /// https://leetcode.com/problems/maximum-matrix-sum/description/?envType=daily-question&envId=2026-01-05
    /// 1975. 最大方陣和
    /// https://leetcode.cn/problems/maximum-matrix-sum/description/?envType=daily-question&envId=2026-01-05
    /// 
    /// Problem:
    /// You are given an n x n integer matrix. You can do the following operation any number of times:
    /// - Choose any two adjacent elements of the matrix and multiply each of them by -1.
    /// Two elements are considered adjacent if and only if they share a border.
    /// Your goal is to maximize the summation of the matrix's elements. Return the maximum sum of the matrix's elements using the operation mentioned above.
    /// 
    /// 繁體中文（題目描述）:
    /// 給定一個 n x n 的整數矩陣。你可以任意次數執行下列操作：
    /// - 選擇矩陣中任意兩個相鄰元素（僅在共用邊界時視為相鄰），並將它們各自乘以 -1。
    /// 目標是使矩陣元素的總和最大化。請回傳在允許上述操作下，矩陣元素能達到的最大總和。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program program = new Program();

        // 測試案例 1: 偶數個負數
        int[][] matrix1 = new int[][]
        {
            new int[] { 1, -1 },
            new int[] { -1, 1 }
        };
        Console.WriteLine($"測試案例 1: {program.MaxMatrixSum(matrix1)}"); // 預期輸出: 4

        // 測試案例 2: 奇數個負數
        int[][] matrix2 = new int[][]
        {
            new int[] { 1, 2, 3 },
            new int[] { -1, -2, -3 },
            new int[] { 1, 2, 3 }
        };
        Console.WriteLine($"測試案例 2: {program.MaxMatrixSum(matrix2)}"); // 預期輸出: 16

        // 測試案例 3: 包含 0
        int[][] matrix3 = new int[][]
        {
            new int[] { -1, 0, -1 },
            new int[] { -2, 1, 3 },
            new int[] { 3, 2, 2 }
        };
        Console.WriteLine($"測試案例 3: {program.MaxMatrixSum(matrix3)}"); // 預期輸出: 15
    }

    /// <summary>
    /// 解題思路：
    /// 雖然題目規定我們只能操作相鄰的元素，但我們可以通過多次操作，把任意兩個元素都乘以 -1。
    /// 
    /// 關鍵觀察：
    /// 1. 每次操作恰好改變兩個數的正負號
    /// 2. 多次操作恰好改變偶數個數的正負號
    /// 
    /// 分類討論：
    /// - 如果 matrix 有偶數個負數，那麼可以把所有數都變成非負數
    /// - 如果 matrix 有奇數個負數且沒有 0，那麼最終必然剩下一個負數。貪心地，選擇 matrix 中的絕對值最小的數，給它加上負號
    /// - 如果 matrix 有奇數個負數且有 0，那麼可以把一個 0 和最終剩下的一個負數操作一次，從而把所有數都變成非負數
    /// 
    /// 實作細節：
    /// 無需特判是否有 0。如果有 0，那麼程式碼中的 mn=0，對 total 無影響。
    /// </summary>
    /// <param name="matrix">n x n 的整數矩陣</param>
    /// <returns>能達到的最大總和</returns>
    public long MaxMatrixSum(int[][] matrix)
    {
        long total = 0;      // 所有元素絕對值的總和
        int negCnt = 0;      // 負數的個數
        int mn = int.MaxValue; // 絕對值最小的元素
        int n = matrix.Length;

        // 遍歷矩陣，計算絕對值總和、負數個數和絕對值最小值
        for(int i = 0; i < n; i++)
        {
            for(int j = 0; j < n; j++)
            {
                int val = matrix[i][j];
                if(val < 0)
                {
                    negCnt++;  // 記錄負數個數
                    val = -val; // 轉為絕對值
                }
                mn = Math.Min(mn, val);  // 追蹤絕對值最小的元素
                total += val;            // 累加絕對值
            }
        }

        // 如果負數個數是偶數，可以將所有數變為正數
        if(negCnt % 2 == 0)
        {
            return total;
        }
        else
        {
            // 如果負數個數是奇數，最終必有一個負數，選擇絕對值最小的數變負
            return total - 2L * mn;
        }
    }
}
