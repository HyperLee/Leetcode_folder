namespace leetcode_074;

class Program
{
    /// <summary>
    /// 74. Search a 2D Matrix
    /// https://leetcode.com/problems/search-a-2d-matrix/description/
    ///
    /// You are given an m x n integer matrix matrix with the following two properties:
    /// Each row is sorted in non-decreasing order.
    /// The first integer of each row is greater than the last integer of the previous row.
    /// Given an integer target, return true if target is in matrix or false otherwise.
    /// You must write a solution in O(log(m * n)) time complexity.
    ///
    /// 74. 搜尋二維矩陣
    /// https://leetcode.cn/problems/search-a-2d-matrix/description/
    ///
    /// 給定一個 m x n 的整數矩陣 matrix，且此矩陣具有以下兩個特性：
    /// 每一列都依照非遞減順序排序。
    /// 每一列的第一個整數都大於前一列的最後一個整數。
    /// 給定一個整數 target，如果 target 存在於 matrix 中則回傳 true，否則回傳 false。
    /// 你必須撰寫一個時間複雜度為 O(log(m * n)) 的解法。
    ///
    /// 此主要進入點提供可直接執行的範例測試資料，輸入多組符合題目排序規則的矩陣
    /// 與 target，並輸出 <see cref="SearchMatrix"/>、<see cref="SearchMatrix2"/> 的
    /// 實際結果、預期答案與 PASS/FAIL 狀態。
    /// </summary>
    /// <param name="args">Command-line arguments; unused.</param>
    static void Main(string[] args)
    {
        (string Name, int[][] Matrix, int Target, bool Expected)[] testCases =
        [
            (
                "target exists in first row",
                [
                    [1, 3, 5, 7],
                    [10, 11, 16, 20],
                    [23, 30, 34, 60],
                ],
                3,
                true
            ),
            (
                "target is missing between values",
                [
                    [1, 3, 5, 7],
                    [10, 11, 16, 20],
                    [23, 30, 34, 60],
                ],
                13,
                false
            ),
            (
                "single cell target exists",
                [
                    [1],
                ],
                1,
                true
            ),
            (
                "target below matrix minimum",
                [
                    [1],
                ],
                0,
                false
            ),
            (
                "target exists at last cell",
                [
                    [1, 3, 5, 7],
                    [10, 11, 16, 20],
                    [23, 30, 34, 60],
                ],
                60,
                true
            ),
        ];

        Program solution = new Program();

        foreach ((string name, int[][] matrix, int target, bool expected) in testCases)
        {
            bool bruteForceActual = solution.SearchMatrix(matrix, target);
            string bruteForceStatus = bruteForceActual == expected ? "PASS" : "FAIL";
            Console.WriteLine(
                $"SearchMatrix | {name} | target = {target} | expected = {expected} | actual = {bruteForceActual} | {bruteForceStatus}");

            bool binarySearchActual = solution.SearchMatrix2(matrix, target);
            string binarySearchStatus = binarySearchActual == expected ? "PASS" : "FAIL";
            Console.WriteLine(
                $"SearchMatrix2 | {name} | target = {target} | expected = {expected} | actual = {binarySearchActual} | {binarySearchStatus}");
        }
    }

    /// <summary>
    /// 使用暴力搜尋判斷 <paramref name="target"/> 是否存在於二維矩陣中。
    /// 解題概念是逐列、逐欄掃描每個元素，找到目標值立即回傳 true；若完整掃描後
    /// 仍未命中則回傳 false。輸入條件為非空矩陣，且每列至少包含一個元素；輸出結果
    /// 表示 target 是否存在。此解法時間複雜度為 O(m * n)，空間複雜度為 O(1)。
    /// </summary>
    /// <param name="matrix">符合題目限制的非空整數矩陣。</param>
    /// <param name="target">要搜尋的目標整數。</param>
    /// <returns>若 target 存在於矩陣中則回傳 true，否則回傳 false。</returns>
    public bool SearchMatrix(int[][] matrix, int target)
    {
        int m = matrix.Length;
        int n = matrix[0].Length;

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                // 暴力法不利用排序特性，逐一檢查每個格子是否為目標值。
                if (matrix[i][j] == target)
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// 解法二: 二分法查找
    /// 由于矩阵的每一行是递增的，且每行的第一个数大于前一行的最后一个数，如果把矩阵每一行拼在一起，我们可以得到一个递增数组。
    ///
    /// https://leetcode.cn/problems/search-a-2d-matrix/solution/by-stormsunshine-gay8/
    /// https://leetcode.cn/problems/search-a-2d-matrix/solutions/2783931/liang-chong-fang-fa-er-fen-cha-zhao-pai-39d74/
    /// 有趣,很有趣
    /// 原來二分法也可以應用在陣列上
    /// 以前都覺得只能用在 string 上找前後
    ///
    /// m 行 n 列的矩阵, m 與 n 類似高與寬 意思
    /// 大小是遞增 => 由左至右 由上至下
    /// 每一行的開頭, 會比前一行的尾數還要大
    ///
    /// 猜測是因為上述說明所以才用 n 來計算
    /// 把陣列攤平就是 m * n
    /// row = mid / n 與 column = mid % n 取法 是重點
    ///
    /// 第二個聯結有比較詳細說明
    /// 把輸入的陣列給攤平
    /// a = [1,3,5,7,10,11,16,20,23,30,34,60]
    /// 并不需要真的拼成一个长为 m * n 的数组 a，而是将 a[i] 转换成矩阵中的行号和列号。
    /// 例如示例 1，i = 9 对应的 a[i] = 30，
    /// 由于矩阵有 n = 4 列，
    /// a[i] 在 i / n = 2 行
    ///         i mod n = 1 列
    ///
    /// 由於攤平了所以用 n 來定位出在哪個位置
    ///
    /// 此方法將 m x n 矩陣視為長度 m * n 的遞增陣列，但不額外建立新陣列；每次
    /// 透過 mid / n 取得列索引、mid % n 取得欄索引，再依比較結果縮小搜尋範圍。
    /// 輸入條件為符合題目排序規則的非空矩陣；輸出結果表示 target 是否存在。
    /// 此解法時間複雜度為 O(log(m * n))，空間複雜度為 O(1)。
    /// </summary>
    /// <param name="matrix">符合題目限制的非空整數矩陣。</param>
    /// <param name="target">要搜尋的目標整數。</param>
    /// <returns>若 target 存在於矩陣中則回傳 true，否則回傳 false。</returns>
    public bool SearchMatrix2(int[][] matrix, int target)
    {
        // 幾組陣列; 高; 行(上下)
        int m = matrix.Length;
        // 每個陣列有多少個; 寬; 列(左右)
        int n = matrix[0].Length;
        int left = 0;
        int right = m * n - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            // 將攤平後的一維索引轉回矩陣座標，不需要真的建立新陣列。
            int row = mid / n;
            int column = mid % n;

            if (matrix[row][column] == target)
            {
                return true;
            }
            else if (matrix[row][column] > target)
            {
                // 目前值太大，target 只可能在攤平順序的左半邊。
                right = mid - 1;
            }
            else
            {
                // 目前值太小，target 只可能在攤平順序的右半邊。
                left = mid + 1;
            }
        }
        return false;
    }
}
