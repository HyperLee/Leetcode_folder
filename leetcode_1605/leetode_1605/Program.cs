namespace leetode_1605
{
    internal class Program
    {
        /// <summary>
        /// 1605. Find Valid Matrix Given Row and Column Sums
        /// https://leetcode.com/problems/find-valid-matrix-given-row-and-column-sums/description/?envType=daily-question&envId=2024-07-20
        /// 1605. 给定行和列的和求可行矩阵
        /// https://leetcode.cn/problems/find-valid-matrix-given-row-and-column-sums/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] rowsum = {3, 8 };
            int[] columnsum = {4, 7 };

            var res = RestoreMatrix(rowsum, columnsum);

            // 輸出 陣列資料
            for (int i = 0; i < res.Length; i++)
            {
                System.Console.Write("Element({0}): ", i);

                for (int j = 0; j < res[i].Length; j++)
                {
                    System.Console.Write("{0}{1}", res[i][j], j == (res[i].Length - 1) ? "" : " ");

                }
                System.Console.WriteLine();
            }

            Console.ReadKey();

        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/find-valid-matrix-given-row-and-column-sums/solutions/2166773/mei-you-si-lu-yi-ge-dong-hua-miao-dong-f-eezj/
        /// https://leetcode.cn/problems/find-valid-matrix-given-row-and-column-sums/solutions/2165784/gei-ding-xing-he-lie-de-he-qiu-ke-xing-j-u8dj/
        /// https://leetcode.cn/problems/find-valid-matrix-given-row-and-column-sums/solutions/2167065/python3javacgotypescript-yi-ti-yi-jie-ta-qtx7/
        /// https://leetcode.cn/problems/find-valid-matrix-given-row-and-column-sums/solutions/2019569/by-stormsunshine-ktsx/
        /// 
        /// 詳細說明請參考第一連結裡面
        /// 圖片說明
        /// 比較好理解
        /// 
        /// 輸入是給列, 行總和
        /// 所以要生成陣列填入出各i, j 位置數值
        /// 個位置最大數值取 min(rowsum, columnsum)
        /// 比較合適
        /// 取出來之後要扣除 取出來的數值
        /// 持續更新
        /// 
        /// 題目說明有: sum(rowSum) == sum(colSum)  
        /// </summary>
        /// <param name="rowSum">是二维矩阵中第 i 行元素的和</param>
        /// <param name="colSum">是第 j 列元素的和</param>
        /// <returns></returns>
        public static int[][] RestoreMatrix(int[] rowSum, int[] colSum)
        {
            int m = rowSum.Length;
            int n = colSum.Length;
            int[][] mat = new int[m][];

            for(int i = 0; i < m; i++)
            {
                mat[i] = new int[n];
            }

            for(int i = 0; i < m; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    // 列行 取出最小值出來
                    mat[i][j] = Math.Min(rowSum[i], colSum[j]);
                    // 更新資料, 上面取出數值塞入, 原始總和就要減少.
                    rowSum[i] -= mat[i][j];
                    colSum[j] -= mat[i][j];
                }
            }

            return mat;

        }
    }
}
