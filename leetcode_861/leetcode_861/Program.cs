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
        /// 2.當開頭都為1之後,接下來計算 列的貢獻值(總和)
        /// 3.需要注意當列蘊含的 1 > 0 就不要翻轉了, 會導致 0 變多
        ///   統計 1多少個, 0 多少個
        ///   k為列翻轉後1的數量
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
                    if (grid[i][0] == 1)
                    {
                        // 統計 列裡面 1的數量有多少
                        nOnes += grid[i][j];
                    }
                    else
                    {
                        // 反轉意思, 0變1, 1變0
                        nOnes += (1 - grid[i][j]);
                    }
                }

                int k = Math.Max(nOnes, m - nOnes);
                // 列貢獻值  k * 2 ^ n - j - 1
                ret += k * (1 << (n - j - 1));
            }

            return ret;
        }
    }
}
