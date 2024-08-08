namespace leetcode_0885
{
    internal class Program
    {
        /// <summary>
        /// 885. Spiral Matrix III
        /// https://leetcode.com/problems/spiral-matrix-iii/description/?envType=daily-question&envId=2024-08-08
        /// 
        /// 885. 螺旋矩阵 III
        /// https://leetcode.cn/problems/spiral-matrix-iii/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int rows = 1, cols = 4, rstart = 0, cstart = 0;

            var res = SpiralMatrixIII2(rows, cols, rstart, cstart);

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
        /// https://leetcode.cn/problems/spiral-matrix-iii/solutions/3546/luo-xuan-ju-zhen-iii-by-leetcode/
        /// https://leetcode.cn/problems/spiral-matrix-iii/solutions/660264/dongge-de-jie-fa-si-lu-qing-xi-by-victor-gmmz/
        /// https://leetcode.cn/problems/spiral-matrix-iii/solutions/1984188/by-stormsunshine-m9yn/
        /// 
        /// 題目要求
        /// 順時鐘方向 走 螺旋方式
        /// 就會是:右, 下, 左, 上 這樣走
        /// 
        /// 此方法有可能會走超出範圍
        /// 不再網格內
        /// </summary>
        /// <param name="rows">行</param>
        /// <param name="cols">列</param>
        /// <param name="rStart">row 起始位置</param>
        /// <param name="cStart">col 起始位置</param>
        /// <returns></returns>
        public static int[][] SpiralMatrixIII(int rows, int cols, int rStart, int cStart)
        {
            // 右, 下, 左, 上
            int[][] dirs = { new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 0, -1 }, new int[] { -1, 0 } };
            // 輸入網格數量
            int total = rows * cols;
            int[][] order = new int[total][];
            int index = 0;
            // 移動編號, 改變移動方向就++
            int linecount = 1;
            int row = rStart, col = cStart;
            int dirindex = 0;

            while(index < total)
            {
                // dir 方向移動步數
                int step = (linecount + 1) / 2;

                for(int i = 0; i < step; i++)
                {
                    if(row >= 0 && row < rows && col >= 0 && col < cols)
                    {
                        // 位置處於網格內才會加入答案
                        order[index] = new int[] { row, col };
                        index++;
                    }

                    // 下一個節點
                    row += dirs[dirindex][0];
                    col += dirs[dirindex][1];
                }

                // 換 dir
                linecount++;
                // % 4 ? 四個方向?
                dirindex = (dirindex + 1) % 4;
            }

            return order;
        }


        /// <summary>
        /// 這方法 似乎比較好理解
        /// https://leetcode.cn/problems/spiral-matrix-iii/solutions/660264/dongge-de-jie-fa-si-lu-qing-xi-by-victor-gmmz/
        /// 
        /// 大原則就是 遍歷整個網格(Grid)
        /// 但是有可能會超出題目輸入的網格範圍
        /// 所以 res 結果, 我們只會把 範圍內的給加入而已
        /// 
        /// 螺旋狀的走路方向,遍歷整個網格
        /// 1. 先確定 四個邊界
        /// 2. 當一個方向走到底邊界時候, 舊調整方向
        /// 3. 根據方向更新下一個節點
        /// 4. 當節點在網格範圍內, 加到結果 res 中
        /// 
        /// 每次改變方向 dir++
        /// 
        /// Row 是橫的<上下增減數量>，Column 是直的<左右增減加數量>
        /// 
        /// rows = 1, cols = 4
        /// => 1234
        /// 
        /// </summary>
        /// <param name="rows">總共列(橫的)</param>
        /// <param name="cols">總共行(直的)</param>
        /// <param name="rStart">row 起始位置</param>
        /// <param name="cStart">col 起始位置</param>
        /// <returns></returns>
        public static int[][] SpiralMatrixIII2(int rows, int cols, int rStart, int cStart)
        {
            // 共 rows * cols 筆資料
            int[][] res = new int[rows * cols][];
            for(int i = 0; i < rows * cols; i++)
            {
                // 每筆都存放 {x, y} 位置
                res[i] = new int[2];
            }

            // 順時鐘螺旋移動方向: 右, 下, 左, 上
            int[][] around = { new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 0, -1 }, new int[] { -1, 0 } };
            
            // {x, y} 為當前位置, num 當前查找數字, dir 當前方向 (0 ~ 3, 四個方向)
            int x = rStart, y = cStart, num = 1, dir = 0;
            
            // 四個方向的邊界
            int left = cStart - 1, right = cStart + 1, upper = rStart - 1, bottom = rStart + 1;

            while(num <= rows * cols)
            {
                if(x >= 0 && x < rows && y >= 0 && y < cols)
                {
                    // {x, y}處於網格內, 加入 結果
                    res[num - 1] = new int[] { x, y };
                    num++;
                }

                if (dir == 0 && y == right)
                {
                    // 向右到右邊界

                    // 調轉方向 向下
                    dir += 1;
                    // 右邊界右移
                    right += 1;
                }
                else if (dir == 1 && x == bottom)
                {
                    // 向下到底邊界

                    dir += 1;
                    // 底邊界下移
                    bottom += 1;
                }
                else if (dir == 2 && y == left)
                {
                    // 向左到左邊界

                    dir += 1;
                    // 左邊界左移
                    left--;
                }
                else if(dir == 3 && x == upper)
                {
                    // 向上到上邊界

                    // 歸0, 下次重新累積
                    dir = 0;
                    // 上邊界上移
                    upper--;
                }

                // 下一個節點
                x += around[dir][0];
                y += around[dir][1];
            }

            return res;
        }
    }
}
