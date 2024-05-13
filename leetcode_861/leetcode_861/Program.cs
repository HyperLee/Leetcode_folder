namespace leetcode_861
{
    internal class Program
    {
        /// <summary>
        /// 861. Score After Flipping Matrix
        /// https://leetcode.com/problems/score-after-flipping-matrix/description/?envType=daily-question&envId=2024-05-13
        /// 861. 翻转矩阵后的得分
        /// https://leetcode.cn/problems/score-after-flipping-matrix/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[][] input = new int[][]
            {
                 new int[]{ 0, 0, 1, 1 },
                 new int[]{ 1, 0, 1, 0 },
                 new int[]{ 1, 1, 0, 0 }
            };

            Console.WriteLine(MatrixScore(input));
            Console.ReadKey();

        }


        /// <summary>
        /// https://leetcode.cn/problems/score-after-flipping-matrix/solutions/511825/fan-zhuan-ju-zhen-hou-de-de-fen-by-leetc-cxma/
        /// https://leetcode.cn/problems/score-after-flipping-matrix/solutions/2015659/by-stormsunshine-2qgs/
        /// https://leetcode.cn/problems/score-after-flipping-matrix/solutions/512319/c-tu-jie-zhe-ge-yue-shi-tan-xin-yue-by-t-nhyw/
        /// 
        /// 1.把開頭最高位的貢獻值(總和)先計算出來
        /// 2.計算開頭以外的位置,從第一列開始計算每行每列數值
        /// 3.開頭為1的, 不翻轉
        /// 4.開頭為0的, 需要翻轉數值
        /// 
        ///   
        /// 行與列的二進位數值拆開計算
        /// 行只有計算開頭
        /// 列是開頭以外
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static int MatrixScore(int[][] grid)
        {
            // 行
            int m = grid.Length;
            // 列
            int n = grid[0].Length;
            // high bit 行貢獻值 m * 2 ^ n - 1  
            int ret = m * (1 << (n - 1));

            // 列從1開始.
            for(int j = 1; j < n; j++)
            {
                int nOnes = 0;
                for(int i = 0; i < m; i++)
                {
                    // 第 i 行第 0 列為 1  (每行開頭為1, 不翻轉. 開頭為0直接翻轉)
                    if (grid[i][0] == 1)
                    {
                        // 統計 列裡面 1的數量有多少
                        nOnes += grid[i][j];
                    }
                    else // 第 i 行第 0 列為 0
                    {
                        // high bit不為1的列, 把數值翻轉, 讓他變大
                        // 反轉意思, 0變1, 1變0
                        nOnes += (1 - grid[i][j]);
                    }
                }

                //  k 是列翻转后的 1 的数量
                int k = Math.Max(nOnes, m - nOnes);
                // 列貢獻值  k * 2 ^ n - j - 1
                ret += k * (1 << (n - j - 1));
            }

            return ret;
        }
    }
}
