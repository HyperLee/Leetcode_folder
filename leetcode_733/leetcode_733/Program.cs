using System.Reflection.Metadata.Ecma335;

namespace leetcode_733
{
    internal class Program
    {
        /// <summary>
        /// 733. Flood Fill
        /// https://leetcode.com/problems/flood-fill/description/
        /// 
        /// 733. 图像渲染
        /// https://leetcode.cn/problems/flood-fill/description/
        /// 
        /// 問題描述
        /// 1. 輸入參數：
        /// image 是一個 m x n 的二維整數陣列，表示影像，其中 image[i][j] 代表像素的值。
        /// sr 和 sc 是起始像素的行和列座標（image[sr][sc] 表示起始像素）。
        /// color 是目標顏色，即起始像素及符合條件的像素將被更新為此顏色。
        /// 
        /// 2. 任務:
        /// 從起始像素 image[sr][sc] 開始，將其以及所有與其相連、具有相同顏色的像素改為 color。
        /// 僅考慮直接相鄰（水平或垂直方向）的像素。
        /// 
        /// 
        /// 渲染填充（Flood Fill）的步驟如下：
        /// 1. 從起始像素開始，將其顏色更改為指定的目標顏色。
        /// 2. 對每個與起始像素直接相鄰的像素（即與起始像素共享一個邊的像素，水平或垂直方向）執行相同的操作，前提是該像素的顏色與起始像素的原始顏色相同。
        /// 3. 持續重複此過程，檢查已更新像素的鄰近像素，若它們的顏色與起始像素的原始顏色相符，則更改其顏色。
        /// 4. 當已無任何與起始像素原始顏色相符且相鄰的像素需要更新時，該過程結束。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[][] input = new int[][]
            {
                 new int[]{ 1, 1, 1 },
                 new int[]{ 1, 1, 0 },
                 new int[]{ 1, 0, 1 }
            };

            int sr = 1, sc = 1, color = 2;

            var res = FloodFill(input, sr, sc, color);

            Console.Write("[");
            foreach(var item in res)
            {
                Console.Write("[");
                foreach (var value in item)
                {
                    Console.Write(value.ToString() + ", ");
                }
                Console.Write("], ");
            }
            Console.WriteLine("]");
        }

        static int[] dx = { 1, 0, 0, -1};
        static int[] dy = { 0, 1, -1, 0};

        /// <summary>
        /// ref: 
        /// https://leetcode.cn/problems/flood-fill/solutions/375836/tu-xiang-xuan-ran-by-leetcode-solution/
        /// https://leetcode.cn/problems/flood-fill/solutions/1861210/by-stormsunshine-l9r8/
        /// 
        /// 深度優先解法 DFS
        /// </summary>
        /// <param name="image">輸入二維陣列</param>
        /// <param name="sr">row</param>
        /// <param name="sc">column</param>
        /// <param name="color">填充顏色</param>
        /// <returns></returns>
        public static int[][] FloodFill(int[][] image, int sr, int sc, int color)
        {
            // 取得起始像素的原始顏色
            int currColor = image[sr][sc];
            // 如果起始像素的顏色已經是目標顏色，則直接返回原影像
            if (currColor != color)
            {
                // 從起始像素開始執行遞迴
                DFS(image, sr, sc, currColor, color);
            }

            return image;
        }


        /// <summary>
        /// DFS 深度優先
        /// 
        /// 想像一個 3 * 3 的陣列, 正中間點為 (0, 0)
        /// 利用遞迴處理上下左右四個方向
        /// Dfs(x + 1, y); // 下
        /// Dfs(x - 1, y); // 上
        /// Dfs(x, y + 1); // 右
        /// Dfs(x, y - 1); // 左
        /// </summary>
        /// <param name="image">二維陣列</param>
        /// <param name="x">x 軸 / row</param>
        /// <param name="y">y 軸 / column</param>
        /// <param name="currColor">現在顏色</param>
        /// <param name="color">要填充顏色</param>
        public static void DFS(int[][] image, int x, int y, int currColor, int color)
        {
            // 該像素的顏色與起始像素的原始顏色相同。
            if (image[x][y] == currColor)
            {
                // 將當前像素更新為目標顏色
                image[x][y] = color;
                // 上下左右相鄰四方向
                for(int i = 0; i < 4; i++)
                {
                    int mx = x + dx[i];
                    int my = y + dy[i];
                    // 檢查是否越界或像素顏色是否與原始顏色不同
                    if (mx >= 0 && mx < image.Length && my >= 0 && my < image[0].Length)
                    {
                        // 繼續往下搜尋其他相鄰像素位置
                        DFS(image, mx, my, currColor, color);
                    }
                }
            }
        }
    }
}
