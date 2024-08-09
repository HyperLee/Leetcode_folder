namespace leetcode_840
{
    internal class Program
    {
        /// <summary>
        /// 840. Magic Squares In Grid
        /// https://leetcode.com/problems/magic-squares-in-grid/description/?envType=daily-question&envId=2024-08-09
        /// 
        /// 840. 矩阵中的幻方
        /// https://leetcode.cn/problems/magic-squares-in-grid/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[][] input = new int[][]
            {
                 new int[]{ 4,3,8,4 },
                 new int[]{ 9,5,1,9 },
                 new int[]{ 2,7,6,2}
            };

            Console.WriteLine(NumMagicSquaresInside(input));
            Console.ReadKey();
        }


        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/magic-squares-in-grid/solutions/107797/ju-zhen-zhong-de-huan-fang-by-leetcode-2/
        /// https://leetcode.cn/problems/magic-squares-in-grid/solutions/199750/fei-bao-li-nu-li-xie-chu-you-ya-de-dai-ma-shuang-b/
        /// https://leetcode.cn/problems/magic-squares-in-grid/solutions/2690676/840-ju-zhen-zhong-de-huan-fang-by-storms-jmlx/
        /// 
        /// 提示:
        /// 1. 3 * 3 矩陣的 中心點 數值為 5
        /// 2. 填充的數字為 1 ~ 9
        /// 3. 對角線總和相同
        /// 4. 每一行/列以及對角, 加總後總和為 15
        /// 5. 四角全為偶數, 中間皆為奇數
        /// 6. 共八種型態
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static int NumMagicSquaresInside(int[][] grid)
        {
            int res = 0;
            int row = grid.Length, col = grid[0].Length;
            for(int i = 1; i < row - 1; i++)
            {
                for(int j = 1; j < col - 1; j++)
                {
                    // (i, j)為子矩陣的中心點
                    if(IsMagicSquare(grid, i, j))
                    {
                        res++;
                    }
                }
            }

            return res;
        }


        /// <summary>
        /// 遍歷輸入的 grid 中 所有 3 * 3 大小的矩陣
        /// 判斷是不是 Magic Squares
        /// 
        /// Magic Squares: 3 * 3 大小
        /// 
        /// 对于 row×col 的矩阵，其中的任意 3×3 的子矩阵的中心位置的行下标和列下标的取值范围分别是 [1,row−2] 和 [1,col−2]。
        /// 
        /// 輸入的 centerRow 與 centerCol 為這次子矩陣的中心點
        /// 因為是 3 * 3, 已該位置為中心點
        /// 已中心點為基準, 上下左右增減擴展找出 3 * 3 矩陣
        /// 
        /// -1, 0, 1 => 共移動三次
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="centerRow"></param>
        /// <param name="centerCol"></param>
        /// <returns></returns>
        public static bool IsMagicSquare(int[][] grid, int centerRow, int centerCol)
        {
            if (grid[centerRow][centerCol] != 5)
            {
                // 中心點要為 5
                return false;
            }

            // 紀錄出現過數字
            ISet<int> set = new HashSet<int>();
            for(int i = -1; i <= 1; i++)
            {
                for(int j = -1; j <= 1; j++)
                {
                    int num = grid[centerRow + i][centerCol + j];
                    if(num < 1 || num > 9 || !set.Add(num))
                    {
                        // 數字範圍 1 ~ 9 必須在這範圍內, 不能超出
                        // 且僅能出現一次, 超出就錯誤
                        return false;
                    }
                }
            }

            for(int i = -1; i <= 1; i++)
            {
                int rowsum = 0, colsum = 0;
                for(int j = -1; j <= 1; j++)
                {
                    rowsum += grid[centerRow + i][centerCol + j];
                    colsum += grid[centerRow + j][centerCol + i];
                }

                if(rowsum != 15 || colsum != 15)
                {
                    // 每一 行/列 總和都要是 15
                    return false;
                }
            }

            int diagonalsum1 = 0, diagonalsum2 = 0;
            for(int i = -1, j = 1; i <= 1; i++, j--)
            {
                // 兩條斜對角總和都要是 15
                // diagonalsum1: 左上 -> 右下
                // diagonalsum2: 右上 -> 左下
                diagonalsum1 += grid[centerRow + i][centerCol + i];
                diagonalsum2 += grid[centerRow + i][centerCol + j];
            }

            return diagonalsum1 == 15 && diagonalsum2 == 15;
        }

    }
}
