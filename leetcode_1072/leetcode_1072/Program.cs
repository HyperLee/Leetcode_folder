namespace leetcode_1072
{
    internal class Program
    {
        /// <summary>
        /// <para>
        /// 1072. Flip Columns For Maximum Number of Equal Rows
        /// https://leetcode.com/problems/flip-columns-for-maximum-number-of-equal-rows/description/
        ///
        /// You are given an m x n binary matrix matrix.
        /// You can choose any number of columns in the matrix and flip every cell in that column (i.e., change
        /// the value of the cell from 0 to 1 or vice versa).
        /// Return the maximum number of rows that have all values equal after some number of flips.
        ///
        /// Example 1:
        /// Input: matrix = [[0,1],[1,1]]
        /// Output: 1
        /// Explanation: After flipping no values, 1 row has all values equal.
        ///
        /// Example 2:
        /// Input: matrix = [[0,1],[1,0]]
        /// Output: 2
        /// Explanation: After flipping values in the first column, both rows have equal values.
        ///
        /// Example 3:
        /// Input: matrix = [[0,0,0],[0,0,1],[1,1,0]]
        /// Output: 2
        /// Explanation: After flipping values in the first two columns, the last two rows have equal values.
        ///
        /// Constraints:
        /// m == matrix.length
        /// n == matrix[i].length
        /// 1 &lt;= m, n &lt;= 300
        /// matrix[i][j] is either 0 or 1.
        /// </para>
        /// <para>
        /// 1072. 翻轉欄後相等列的最大數量
        /// https://leetcode.cn/problems/flip-columns-for-maximum-number-of-equal-rows/description/
        ///
        /// 給定一個 m x n 的二元矩陣 matrix。
        /// 你可以選擇矩陣中的任意數量欄，並翻轉該欄中的每個儲存格（也就是將儲存格的值從 0
        /// 改為 1，或從 1 改為 0）。
        /// 請回傳經過若干次翻轉後，所有值都相等的列之最大數量。
        ///
        /// 範例 1：
        /// 輸入：matrix = [[0,1],[1,1]]
        /// 輸出：1
        /// 解釋：不翻轉任何值時，有 1 列的所有值都相等。
        ///
        /// 範例 2：
        /// 輸入：matrix = [[0,1],[1,0]]
        /// 輸出：2
        /// 解釋：翻轉第一欄中的值後，兩列的值都各自相等。
        ///
        /// 範例 3：
        /// 輸入：matrix = [[0,0,0],[0,0,1],[1,1,0]]
        /// 輸出：2
        /// 解釋：翻轉前兩欄中的值後，最後兩列的值都各自相等。
        ///
        /// 限制條件：
        /// m == matrix.length
        /// n == matrix[i].length
        /// 1 &lt;= m, n &lt;= 300
        /// matrix[i][j] 只能是 0 或 1。
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Environment.ExitCode = RunSamples() ? 0 : 1;
        }

        /// <summary>
        /// 執行固定矩陣案例，逐一驗證三種「翻轉欄後最多等值列」解法。
        /// 此方法不接受輸入；輸出每個案例的矩陣、預期值、實際值與通過狀態，
        /// 並回傳所有答案與輸入不變檢查是否全部通過。
        /// </summary>
        /// <returns>全部檢查通過時為 <see langword="true"/>，否則為 <see langword="false"/>。</returns>
        private static bool RunSamples()
        {
            (string Name, int[][] Matrix, int Expected, string Display)[] cases =
            [
                ("官方範例一", [[0, 1], [1, 1]], 1, "[[0,1],[1,1]]"),
                ("官方範例二", [[0, 1], [1, 0]], 2, "[[0,1],[1,0]]"),
                ("官方範例三", [[0, 0, 0], [0, 0, 1], [1, 1, 0]], 2, "[[0,0,0],[0,0,1],[1,1,0]]"),
                ("最小合法輸入", [[0]], 1, "[[0]]"),
                ("單欄混合值", [[0], [1], [0], [1]], 4, "[[0],[1],[0],[1]]"),
                ("重複、互補與干擾列", [[0, 1, 0], [1, 0, 1], [0, 1, 0], [1, 1, 0]], 3, "[[0,1,0],[1,0,1],[0,1,0],[1,1,0]]"),
                ("尺寸上界", CreateUpperBoundMatrix(), 300, "300 x 300 產生矩陣")
            ];

            int passedChecks = 0;
            foreach ((string name, int[][] matrix, int expected, string display) in cases)
            {
                passedChecks += RunTestCase(name, matrix, expected, display);
            }

            int totalChecks = cases.Length * 3;
            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過。");
            return passedChecks == totalChecks;
        }

        /// <summary>
        /// 執行一組二元矩陣案例並比較三種解法。
        /// 輸入包含案例名稱、符合題目限制的非空矩陣、預期答案與顯示文字；
        /// 每種解法使用獨立深層複本，輸出答案與輸入不變檢查的綜合結果。
        /// </summary>
        /// <param name="name">顯示於主控台的案例名稱。</param>
        /// <param name="matrix">由 0 與 1 組成的非空矩陣。</param>
        /// <param name="expected">翻轉若干欄後，列內元素全相等的最大列數。</param>
        /// <param name="display">適合輸出至主控台的矩陣描述。</param>
        /// <returns>通過答案與輸入不變檢查的解法數量，範圍為 0 到 3。</returns>
        private static int RunTestCase(string name, int[][] matrix, int expected, string display)
        {
            int[][] input1 = CloneMatrix(matrix);
            int[][] input2 = CloneMatrix(matrix);
            int[][] input3 = CloneMatrix(matrix);

            int actual1 = MaxEqualRowsAfterFlips(input1);
            int actual2 = MaxEqualRowsAfterFlips2(input2);
            int actual3 = MaxEqualRowsAfterFlips3(input3);
            bool passed1 = actual1 == expected && HaveSameValues(matrix, input1);
            bool passed2 = actual2 == expected && HaveSameValues(matrix, input2);
            bool passed3 = actual3 == expected && HaveSameValues(matrix, input3);

            Console.WriteLine($"案例：{name}");
            Console.WriteLine($"輸入：matrix = {display}");
            Console.WriteLine($"預期：{expected}");
            Console.WriteLine($"解法一實際：{actual1} => {(passed1 ? "PASS" : "FAIL")}");
            Console.WriteLine($"解法二實際：{actual2} => {(passed2 ? "PASS" : "FAIL")}");
            Console.WriteLine($"解法三實際：{actual3} => {(passed3 ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return (passed1 ? 1 : 0) + (passed2 ? 1 : 0) + (passed3 ? 1 : 0);
        }

        /// <summary>
        /// 建立符合題目尺寸上界的 300 x 300 二元矩陣。
        /// 偶數列使用固定週期，奇數列使用其逐位互補，因此所有列皆屬於同一等價類；
        /// 輸出可用來驗證三種解法在最大合法尺寸下都回傳 300。
        /// </summary>
        /// <returns>包含 300 列、每列 300 個二元值的矩陣。</returns>
        private static int[][] CreateUpperBoundMatrix()
        {
            const int size = 300;
            int[][] matrix = new int[size][];

            for (int row = 0; row < size; row++)
            {
                matrix[row] = new int[size];
                for (int column = 0; column < size; column++)
                {
                    int baseValue = column % 3 == 0 ? 1 : 0;
                    matrix[row][column] = row % 2 == 0 ? baseValue : 1 - baseValue;
                }
            }

            return matrix;
        }

        /// <summary>
        /// 深層複製不規則整數矩陣，使每種解法取得互不共用列陣列的輸入。
        /// 輸入須為非空且每列皆非空的矩陣；輸出內容相同但可獨立修改的矩陣。
        /// </summary>
        /// <param name="matrix">要複製的非空矩陣。</param>
        /// <returns>與輸入具有相同數值的新矩陣。</returns>
        private static int[][] CloneMatrix(int[][] matrix)
        {
            int[][] clone = new int[matrix.Length][];
            for (int row = 0; row < matrix.Length; row++)
            {
                clone[row] = (int[])matrix[row].Clone();
            }

            return clone;
        }

        /// <summary>
        /// 比較兩個不規則整數矩陣的尺寸與所有元素是否完全相同。
        /// 輸入須為已初始化的矩陣；輸出用於確認解法執行後仍保留原始輸入內容。
        /// </summary>
        /// <param name="first">比較基準矩陣。</param>
        /// <param name="second">要與基準比較的矩陣。</param>
        /// <returns>矩陣尺寸與所有對應元素皆相同時為 <see langword="true"/>。</returns>
        private static bool HaveSameValues(int[][] first, int[][] second)
        {
            if (first.Length != second.Length)
            {
                return false;
            }

            for (int row = 0; row < first.Length; row++)
            {
                if (!first[row].AsSpan().SequenceEqual(second[row]))
                {
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// 使用「XOR 正規化加字串雜湊」計算可同時變成列內全相等的最大列數。
        /// 每列逐位與首元素 XOR，使原列與其互補列產生相同模式，再以字典統計各模式的出現次數；
        /// 輸入須為符合題目限制、每列等長且只包含 0 與 1 的非空矩陣。
        /// </summary>
        /// <param name="matrix">要分析的非空二元矩陣。</param>
        /// <returns>翻轉任意欄後，列內所有值皆相等的最大列數。</returns>
        /// <remarks>
        /// 時間複雜度為 O(m * n)，額外空間複雜度為 O(m * n)，且不修改輸入矩陣。
        /// 參考：https://leetcode.cn/problems/flip-columns-for-maximum-number-of-equal-rows/solutions/2270101/ni-xiang-si-wei-pythonjavacgo-by-endless-915k/
        /// </remarks>
        public static int MaxEqualRowsAfterFlips(int[][] matrix)
        {
            Dictionary<string, int> patternCounts = new Dictionary<string, int>();
            int maximumRows = 0;

            foreach (int[] row in matrix)
            {
                char[] normalizedPattern = new char[row.Length];
                for (int column = 0; column < row.Length; column++)
                {
                    // 與首位 XOR 後首位固定為 0，互補列也會得到完全相同的模式。
                    normalizedPattern[column] = (char)('0' + (row[column] ^ row[0]));
                }

                string pattern = new string(normalizedPattern);
                int count = patternCounts.GetValueOrDefault(pattern) + 1;
                patternCounts[pattern] = count;
                maximumRows = Math.Max(maximumRows, count);
            }

            return maximumRows;
        }

        /// <summary>
        /// 使用「逐列等同或互補比較」計算可同時變成列內全相等的最大列數。
        /// 解法依序將每列當作基準，計算其他列是否在每個位置都與基準維持相同 XOR 關係；
        /// 輸入須為符合題目限制、每列等長且只包含 0 與 1 的非空矩陣。
        /// </summary>
        /// <param name="matrix">要分析的非空二元矩陣。</param>
        /// <returns>翻轉任意欄後，列內所有值皆相等的最大列數。</returns>
        /// <remarks>時間複雜度為 O(m² * n)，額外空間複雜度為 O(1)，且不修改輸入矩陣。</remarks>
        public static int MaxEqualRowsAfterFlips2(int[][] matrix)
        {
            int maximumRows = 0;

            for (int baseRow = 0; baseRow < matrix.Length; baseRow++)
            {
                int equivalentRows = 0;

                for (int candidateRow = 0; candidateRow < matrix.Length; candidateRow++)
                {
                    int expectedXor = matrix[baseRow][0] ^ matrix[candidateRow][0];
                    bool isEquivalent = true;

                    for (int column = 1; column < matrix[baseRow].Length; column++)
                    {
                        // XOR 關係一旦改變，兩列就既非完全相同，也非逐位互補。
                        if ((matrix[baseRow][column] ^ matrix[candidateRow][column]) != expectedXor)
                        {
                            isEquivalent = false;
                            break;
                        }
                    }

                    if (isEquivalent)
                    {
                        equivalentRows++;
                    }
                }

                maximumRows = Math.Max(maximumRows, equivalentRows);
            }

            return maximumRows;
        }

        /// <summary>
        /// 使用「XOR 正規化加二元 Trie」計算可同時變成列內全相等的最大列數。
        /// 每列正規化後依序沿 0 或 1 分支插入 Trie，並在完整模式的終端節點累計出現次數；
        /// 輸入須為符合題目限制、每列等長且只包含 0 與 1 的非空矩陣。
        /// </summary>
        /// <param name="matrix">要分析的非空二元矩陣。</param>
        /// <returns>翻轉任意欄後，列內所有值皆相等的最大列數。</returns>
        /// <remarks>時間複雜度為 O(m * n)，額外空間複雜度為 O(m * n)，且不修改輸入矩陣。</remarks>
        public static int MaxEqualRowsAfterFlips3(int[][] matrix)
        {
            TrieNode root = new TrieNode();
            int maximumRows = 0;

            foreach (int[] row in matrix)
            {
                TrieNode node = root;

                for (int column = 0; column < row.Length; column++)
                {
                    int normalizedBit = row[column] ^ row[0];
                    node = normalizedBit == 0
                        ? (node.Zero ??= new TrieNode())
                        : (node.One ??= new TrieNode());
                }

                // 只有走完整列模式後才計數，避免前綴相同被誤認為同一列模式。
                node.Count++;
                maximumRows = Math.Max(maximumRows, node.Count);
            }

            return maximumRows;
        }

        /// <summary>
        /// 表示正規化二元列模式 Trie 的節點，分別保存 0、1 子節點與完整模式出現次數。
        /// </summary>
        private sealed class TrieNode
        {
            public TrieNode? Zero { get; set; }

            public TrieNode? One { get; set; }

            public int Count { get; set; }
        }
    }
}