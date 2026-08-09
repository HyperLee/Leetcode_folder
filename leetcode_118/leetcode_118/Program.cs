namespace leetcode_118;

class Program
{
    /// <summary>
    /// <para>
    /// 118. Pascal's Triangle
    /// https://leetcode.com/problems/pascals-triangle/description/
    ///
    /// Given an integer numRows, return the first numRows of Pascal's triangle.
    /// In Pascal's triangle, each number is the sum of the two numbers directly above it as shown:
    /// Illustration: https://upload.wikimedia.org/wikipedia/commons/0/0d/PascalTriangleAnimated2.gif
    ///
    /// Example 1:
    /// Input: numRows = 5
    /// Output: [[1],[1,1],[1,2,1],[1,3,3,1],[1,4,6,4,1]]
    ///
    /// Example 2:
    /// Input: numRows = 1
    /// Output: [[1]]
    ///
    /// Constraints:
    /// 1 &lt;= numRows &lt;= 30
    /// </para>
    /// <para>
    /// 118. 帕斯卡三角形
    /// https://leetcode.cn/problems/pascals-triangle/description/
    ///
    /// 給定整數 numRows，請回傳帕斯卡三角形的前 numRows 列。
    /// 在帕斯卡三角形中，每個數字都是其正上方兩個數字的總和，如圖所示：
    /// 示意圖：https://upload.wikimedia.org/wikipedia/commons/0/0d/PascalTriangleAnimated2.gif
    ///
    /// 範例 1：
    /// 輸入：numRows = 5
    /// 輸出：[[1],[1,1],[1,2,1],[1,3,3,1],[1,4,6,4,1]]
    ///
    /// 範例 2：
    /// 輸入：numRows = 1
    /// 輸出：[[1]]
    ///
    /// 限制條件：
    /// 1 &lt;= numRows &lt;= 30
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料：產生前 5 行楊輝三角
        int numRows = 5;
        var triangle = new Program().Generate(numRows);
        Console.WriteLine($"Pascal's Triangle 前 {numRows} 行：");
        foreach (var row in triangle)
        {
            Console.WriteLine(string.Join(", ", row));
        }
    }


    /// <summary>
    /// 產生楊輝三角的前 numRows 行。
    /// 每一行的第一個和最後一個元素皆為 1，其他元素為左上方與正上方元素之和。
    /// <example>
    /// <code>
    /// Generate(5) 會回傳：
    /// [
    ///   [1],
    ///   [1,1],
    ///   [1,2,1],
    ///   [1,3,3,1],
    ///   [1,4,6,4,1]
    /// ]
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="numRows">要產生的行數</param>
    /// <returns>楊輝三角的前 numRows 行</returns>
    public IList<IList<int>> Generate(int numRows)
    {
        // 建立結果儲存空間
        var triangle = new List<IList<int>>(numRows);

        if (numRows <= 0)
        {
            // 邊界檢查：若行數小於等於 0，回傳空集合
            return triangle;
        }

        // 第一行固定為 [1]
        triangle.Add(new List<int> { 1 });

        for (int i = 1; i < numRows; i++)
        {
            // 每多層要多一個元素空間
            var row = new List<int>(i + 1);

            // 每行第一個元素
            row.Add(1);

            // 去掉頭尾
            for (int j = 1; j < i; j++)
            {
                // 其餘元素為左上方與正上方元素之和
                row.Add(triangle[i - 1][j - 1] + triangle[i - 1][j]);
            }

            // 每行最後一個元素
            row.Add(1);
            triangle.Add(row);
        }
        
        return triangle;
    }
}
