namespace leetcode_054
{
    internal class Program
    {
        /// <summary>
        /// 54. Spiral Matrix
        /// https://leetcode.com/problems/spiral-matrix/description/
        /// 54. 螺旋矩阵
        /// https://leetcode.cn/problems/spiral-matrix/description/ 
        /// </summary>
        /// <param name="args"></param> <summary>
        static void Main(string[] args)
        {
            int[][] matrix = new int[][] {
                new int[] {1, 2, 3},
                new int[] {4, 5, 6},
                new int[] {7, 8, 9}
            };
            
            IList<int> result = SpiralOrder(matrix);
            Console.WriteLine("res: " + string.Join(", ", result));
        }

        /// <summary>
        /// {0,1}: 向右移動（行不變，列+1）
        /// {1,0}: 向下移動（行+1，列不變）
        /// {0,-1}: 向左移動（行不變，列-1）
        /// {-1,0}: 向上移動（行-1，列不變）
		/// 想像成[y, x], 比較好理解
		/// y: 矩陣的列數（寬度）
		/// x: 矩陣的行數（高度）
        /// </summary>
        /// <value></value>
        private static readonly int[,] DIRS = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } }; // 右下左上

        /// <summary>
        /// ref:
        /// https://leetcode.cn/problems/spiral-matrix/solutions/275393/luo-xuan-ju-zhen-by-leetcode-solution/
        /// https://leetcode.cn/problems/spiral-matrix/solutions/2966229/liang-chong-fang-fa-jian-ji-gao-xiao-pyt-4wzk/
        /// https://leetcode.cn/problems/spiral-matrix/solutions/1984171/by-stormsunshine-cq4c/ 
        /// 
        /// Returns the elements of the matrix in spiral order.
        /// 解題概念：直接依據提目描述方式,螺旋走矩陣
        /// 使用方向數組 DIRS 來表示四個方向（右、下、左、上），
        /// 並用 di 來追蹤當前方向。每次走一步後，檢查下一步是否出界或已訪問過，
        /// 如果是，則右轉 90°。重複這個過程直到遍歷完整個矩陣。
        /// 
        /// 根據題意，我們從左上角( 0 ,0 )出發，依照「右下左上」(螺旋, 順時鐘)的順序前進：
        /// 首先向右走，如果到達矩陣邊界，則向右轉9 0 度，前進方向變為向下。
        /// 然後向下走，如果到達矩陣邊界，則向右轉9 0 度，前進方向變為向左。
        /// 然後向左走，如果到達矩陣邊界，則向右轉9 0 度 ，前進方向變為向上。
        /// 每次走到底(邊界位置)就轉向，直到所有元素都被訪問過。
        /// 遇到標記過的元素或者邊界時，就轉向。
        /// 
        /// nextRow < 0: 檢查是否會超出矩陣的上邊界
        /// nextRow >= m: 檢查是否會超出矩陣的下邊界
        /// nextCol < 0: 檢查是否會超出矩陣的左邊界
        /// nextCol >= n: 檢查是否會超出矩陣的右邊界
        /// matrix[nextRow][nextCol] == int.MaxValue: 檢查下一個位置是否已被訪問過
        /// 我們使用 int.MaxValue 作為已訪問的標記
        /// </summary>
        /// <param name="matrix">A 2D array of integers.</param>
        /// <returns>A list of integers in spiral order.</returns>
        public static IList<int> SpiralOrder(int[][] matrix)
        {
            // 步驟1: 初始化基本變量
            int m = matrix.Length; // 矩陣的行數（高度）
            if (m == 0) 
            {
                return new List<int>(); // 處理空矩陣的邊界情況
            }
            int n = matrix[0].Length; // 矩陣的列數（寬度）
            
            // 步驟2: 創建結果集
            // 由於我們知道最終結果的大小，所以預先分配空間以提高效率
            List<int> res = new List<int>(m * n);
            
            // 步驟3: 初始化遍歷位置和方向
            int i = 0;  // 當前行的位置
            int j = 0;  // 當前列的位置
            int di = 0; // 當前方向索引（0:右, 1:下, 2:左, 3:上）
            
            // 步驟4: 開始螺旋遍歷
            for (int k = 0; k < m * n; k++) 
            { 
                // 步驟4.1: 收集當前元素
                res.Add(matrix[i][j]); // 加入當前位置的元素到結果集
                matrix[i][j] = int.MaxValue; // 標記已訪問（避免重複訪問）
                
                // 步驟4.2: 計算下一步位置
                int nextRow = i + DIRS[di, 0]; // 計算下一步的行索引
                int nextCol = j + DIRS[di, 1]; // 計算下一步的列索引
                
                // 步驟4.3: 檢查是否需要改變方向
                // 如果下一步會出界或已訪問過，則需要轉向
                if (nextRow < 0 || nextRow >= m || nextCol < 0 || nextCol >= n || matrix[nextRow][nextCol] == int.MaxValue) 
                {
                    di = (di + 1) % 4; // 順時針轉向（右->下->左->上）
                }
                
                // 步驟4.4: 移動到下一個位置
                i += DIRS[di, 0]; // 根據當前方向更新行位置
                j += DIRS[di, 1]; // 根據當前方向更新列位置
            }
            
            // 步驟5: 返回最終結果
            return res;
        }

    }
}