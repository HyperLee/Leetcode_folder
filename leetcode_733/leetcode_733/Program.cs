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
            RunSamples();
        }

        /// <summary>
        /// 執行五組固定案例，分別驗證 DFS 與 BFS 解法，並輸出預期結果、實際結果與通過統計。
        /// 所有案例均符合題目所定義的非空矩陣、合法起點與顏色範圍。
        /// </summary>
        private static void RunSamples()
        {
            SampleCase[] samples =
            {
                new(
                    "官方連通區案例",
                    [[1, 1, 1], [1, 1, 0], [1, 0, 1]],
                    1,
                    1,
                    2,
                    [[2, 2, 2], [2, 2, 0], [2, 0, 1]]),
                new(
                    "目標色等於原色",
                    [[0, 0, 0], [0, 0, 0]],
                    0,
                    0,
                    0,
                    [[0, 0, 0], [0, 0, 0]]),
                new(
                    "單一像素",
                    [[1]],
                    0,
                    0,
                    2,
                    [[2]]),
                new(
                    "邊界起點",
                    [[0, 0, 0], [0, 1, 1]],
                    0,
                    0,
                    3,
                    [[3, 3, 3], [3, 1, 1]]),
                new(
                    "同色但不連通",
                    [[1, 0, 1], [1, 0, 1], [0, 0, 1]],
                    0,
                    0,
                    2,
                    [[2, 0, 1], [2, 0, 1], [0, 0, 1]])
            };

            int passedChecks = 0;
            foreach (SampleCase sample in samples)
            {
                passedChecks += RunSample(sample);
            }

            int totalChecks = samples.Length * 2;
            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
        }

        /// <summary>
        /// 以彼此獨立的影像副本執行一組 DFS 與 BFS 驗證，避免原地填色造成解法間的資料污染。
        /// 輸入必須包含合法矩陣、起點、目標色與手動推導的預期矩陣；回傳通過的解法數，範圍為 0 到 2。
        /// </summary>
        /// <param name="sample">單一 Flood Fill 測試案例。</param>
        /// <returns>本案例通過驗證的解法數。</returns>
        private static int RunSample(SampleCase sample)
        {
            int[][] dfsResult = FloodFill(CloneImage(sample.Image), sample.StartRow, sample.StartColumn, sample.Color);
            int[][] bfsResult = FloodFill2(CloneImage(sample.Image), sample.StartRow, sample.StartColumn, sample.Color);
            bool dfsPassed = AreImagesEqual(sample.Expected, dfsResult);
            bool bfsPassed = AreImagesEqual(sample.Expected, bfsResult);

            Console.WriteLine($"案例：{sample.Name}");
            Console.WriteLine($"輸入：image = {FormatImage(sample.Image)}, sr = {sample.StartRow}, sc = {sample.StartColumn}, color = {sample.Color}");
            Console.WriteLine($"Expected：{FormatImage(sample.Expected)}");
            Console.WriteLine($"DFS Actual：{FormatImage(dfsResult)} => {(dfsPassed ? "PASS" : "FAIL")}");
            Console.WriteLine($"BFS Actual：{FormatImage(bfsResult)} => {(bfsPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return (dfsPassed ? 1 : 0) + (bfsPassed ? 1 : 0);
        }

        /// <summary>
        /// 深拷貝不規則二維整數陣列，使原地修改型演算法能在互不干擾的輸入上執行。
        /// 輸入為非 null 的二維陣列；回傳內容相同但各列皆為新陣列的副本。
        /// </summary>
        /// <param name="image">要複製的影像矩陣。</param>
        /// <returns>可獨立修改的影像副本。</returns>
        private static int[][] CloneImage(int[][] image)
        {
            int[][] copy = new int[image.Length][];
            for (int row = 0; row < image.Length; row++)
            {
                copy[row] = [.. image[row]];
            }

            return copy;
        }

        /// <summary>
        /// 逐列逐像素比較兩個影像矩陣。
        /// 輸入為非 null 的二維陣列；只有列數、各列長度與所有像素都相同時才回傳 true。
        /// </summary>
        /// <param name="expected">預期影像。</param>
        /// <param name="actual">實際影像。</param>
        /// <returns>兩個影像是否具有完全相同的結構與像素值。</returns>
        private static bool AreImagesEqual(int[][] expected, int[][] actual)
        {
            if (expected.Length != actual.Length)
            {
                return false;
            }

            for (int row = 0; row < expected.Length; row++)
            {
                if (expected[row].Length != actual[row].Length)
                {
                    return false;
                }

                for (int column = 0; column < expected[row].Length; column++)
                {
                    if (expected[row][column] != actual[row][column])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 將非 null 的二維整數陣列轉為緊湊且固定的顯示格式，供主程式與 README 對照。
        /// </summary>
        /// <param name="image">要格式化的影像矩陣。</param>
        /// <returns>格式如 [[1,1],[1,0]] 的矩陣字串。</returns>
        private static string FormatImage(int[][] image)
        {
            return $"[{string.Join(",", image.Select(row => $"[{string.Join(",", row)}]"))}]";
        }

        /// <summary>
        /// 描述一組 Flood Fill 驗證資料，包括名稱、輸入矩陣、起點、目標色與預期輸出。
        /// </summary>
        /// <param name="Name">案例名稱。</param>
        /// <param name="Image">原始影像矩陣。</param>
        /// <param name="StartRow">起點列索引。</param>
        /// <param name="StartColumn">起點欄索引。</param>
        /// <param name="Color">目標顏色。</param>
        /// <param name="Expected">預期填色結果。</param>
        private sealed record SampleCase(
            string Name,
            int[][] Image,
            int StartRow,
            int StartColumn,
            int Color,
            int[][] Expected);

        private static readonly int[] RowOffsets = { 1, 0, 0, -1 };
        private static readonly int[] ColumnOffsets = { 0, 1, -1, 0 };

        /// <summary>
        /// 使用深度優先搜尋從指定起點填滿四方向相連且與起點原色相同的像素。
        /// 輸入必須是非空矩陣，sr 與 sc 必須是合法座標；方法會原地修改 image 並回傳同一個矩陣。
        /// 若起點已是目標顏色，會直接回傳，避免同色遞迴無法留下已走訪標記。
        /// </summary>
        /// <param name="image">要執行填色的非空影像矩陣。</param>
        /// <param name="sr">起點列索引。</param>
        /// <param name="sc">起點欄索引。</param>
        /// <param name="color">要填入的目標顏色。</param>
        /// <returns>完成 Flood Fill 後的原影像矩陣。</returns>
        public static int[][] FloodFill(int[][] image, int sr, int sc, int color)
        {
            int currColor = image[sr][sc];
            if (currColor == color)
            {
                return image;
            }

            DFS(image, sr, sc, currColor, color);
            return image;
        }

        /// <summary>
        /// 使用廣度優先搜尋從指定起點填滿四方向相連且與起點原色相同的像素。
        /// 輸入必須是非空矩陣，sr 與 sc 必須是合法座標；方法會原地修改 image 並回傳同一個矩陣。
        /// </summary>
        /// <param name="image">要執行填色的非空影像矩陣。</param>
        /// <param name="sr">起點列索引。</param>
        /// <param name="sc">起點欄索引。</param>
        /// <param name="color">要填入的目標顏色。</param>
        /// <returns>完成 Flood Fill 後的原影像矩陣。</returns>
        public static int[][] FloodFill2(int[][] image, int sr, int sc, int color)
        {
            int currColor = image[sr][sc];
            // 同色時不需走訪，也避免「改色」無法作為已走訪標記。
            if (currColor == color)
            {
                return image;
            }

            Queue<(int Row, int Column)> queue = new();
            image[sr][sc] = color;
            queue.Enqueue((sr, sc));

            while (queue.Count > 0)
            {
                (int row, int column) = queue.Dequeue();
                for (int i = 0; i < 4; i++)
                {
                    int nextRow = row + RowOffsets[i];
                    int nextColumn = column + ColumnOffsets[i];
                    if (nextRow >= 0
                        && nextRow < image.Length
                        && nextColumn >= 0
                        && nextColumn < image[0].Length
                        && image[nextRow][nextColumn] == currColor)
                    {
                        // 入列時立即改色，讓顏色同時充當已走訪標記，避免同一像素重複入列。
                        image[nextRow][nextColumn] = color;
                        queue.Enqueue((nextRow, nextColumn));
                    }
                }
            }

            return image;
        }


        /// <summary>
        /// 以 DFS 遞迴處理單一像素及其上下左右鄰居。
        /// image 必須是非空矩陣，x 與 y 必須是合法座標，且 currColor 與 color 必須不同；
        /// 方法沒有回傳值，會將目前連通區中仍為 currColor 的像素原地改為 color。
        /// </summary>
        /// <param name="image">要執行填色的非空影像矩陣。</param>
        /// <param name="x">目前像素的列索引。</param>
        /// <param name="y">目前像素的欄索引。</param>
        /// <param name="currColor">起點的原始顏色。</param>
        /// <param name="color">要填入的目標顏色。</param>
        public static void DFS(int[][] image, int x, int y, int currColor, int color)
        {
            if (image[x][y] == currColor)
            {
                // 先改色再展開鄰居，使顏色同時充當已走訪標記。
                image[x][y] = color;
                for (int i = 0; i < 4; i++)
                {
                    int mx = x + RowOffsets[i];
                    int my = y + ColumnOffsets[i];
                    if (mx >= 0 && mx < image.Length && my >= 0 && my < image[0].Length)
                    {
                        // 只對合法的四方向鄰居遞迴；顏色條件由下一層入口判斷。
                        DFS(image, mx, my, currColor, color);
                    }
                }
            }
        }
    }
}