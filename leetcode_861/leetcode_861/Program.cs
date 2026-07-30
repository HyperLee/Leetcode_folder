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
            SampleResult[] results = RunSamples();
            int passedCases = 0;

            for (int index = 0; index < results.Length; index++)
            {
                SampleResult result = results[index];

                if (result.Passed)
                {
                    passedCases++;
                }

                Console.WriteLine($"案例 {index + 1}：{result.Sample.Name}");
                Console.WriteLine($"輸入：{FormatGrid(result.Sample.Grid)}");
                Console.WriteLine($"預期：{result.Sample.Expected}");
                Console.WriteLine($"實際：{result.Actual} => {(result.Passed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passedCases}/{results.Length} 筆測試通過");
        }

        /// <summary>
        /// 執行固定的矩陣計分案例，逐一呼叫貪心解法並建立預期值與實際值的比對結果。
        /// 輸入皆為符合題目限制的非空二元矩陣，涵蓋官方範例、單列、單欄、
        /// 全零、全一與欄位中 0、1 數量平手等情境；方法本身不進行主控台輸出。
        /// </summary>
        /// <returns>依案例宣告順序排列的驗證結果陣列。</returns>
        private static SampleResult[] RunSamples()
        {
            SampleCase[] samples =
            [
                new(
                    "官方範例一：3 × 4 混合矩陣",
                    [
                        [0, 0, 1, 1],
                        [1, 0, 1, 0],
                        [1, 1, 0, 0]
                    ],
                    39),
                new("官方範例二：單一元素", [[0]], 1),
                new("單列且最高位為 0", [[0, 1, 0, 1]], 15),
                new("單欄混合矩陣", [[0], [1], [0]], 3),
                new("全零矩陣", [[0, 0, 0], [0, 0, 0]], 14),
                new("全一矩陣", [[1, 1, 1], [1, 1, 1]], 14),
                new("非最高位欄的 0、1 數量平手", [[1, 0, 0], [1, 1, 1]], 11)
            ];

            SampleResult[] results = new SampleResult[samples.Length];

            for (int index = 0; index < samples.Length; index++)
            {
                SampleCase sample = samples[index];
                results[index] = new SampleResult(sample, MatrixScore(sample.Grid));
            }

            return results;
        }

        /// <summary>
        /// 將二維整數陣列轉成容易閱讀的矩陣字串，供主控台案例展示使用。
        /// 輸入必須是非空且每列皆存在的矩陣；此方法只讀取資料，不會修改矩陣。
        /// </summary>
        /// <param name="grid">要格式化的二維整數陣列。</param>
        /// <returns>格式為 <c>[[a, b], [c, d]]</c> 的矩陣字串。</returns>
        private static string FormatGrid(int[][] grid)
        {
            string[] rows = new string[grid.Length];

            for (int row = 0; row < grid.Length; row++)
            {
                rows[row] = $"[{string.Join(", ", grid[row])}]";
            }

            return $"[{string.Join(", ", rows)}]";
        }

        /// <summary>
        /// 使用不實際翻轉矩陣的貪心策略，計算任意翻轉列與欄後可取得的最大分數。
        /// 先讓每列最高位在邏輯上成為 1，再針對其餘每欄選擇能保留較多 1 的方向；
        /// 輸入必須是非空、每列等長且只包含 0 與 1 的二元矩陣，方法不會修改輸入。
        /// </summary>
        /// <param name="grid">符合題目限制的二元矩陣。</param>
        /// <returns>把每列視為二進位數後，所有列可達到的最大總分。</returns>
        /// <remarks>
        /// 參考：
        /// https://leetcode.cn/problems/score-after-flipping-matrix/solutions/511825/fan-zhuan-ju-zhen-hou-de-de-fen-by-leetc-cxma/
        /// https://leetcode.cn/problems/score-after-flipping-matrix/solutions/2015659/by-stormsunshine-2qgs/
        /// https://leetcode.cn/problems/score-after-flipping-matrix/solutions/512319/c-tu-jie-zhe-ge-yue-shi-tan-xin-yue-by-t-nhyw/
        /// </remarks>
        public static int MatrixScore(int[][] grid)
        {
            int m = grid.Length;
            int n = grid[0].Length;

            // 最高位的權重大於右側所有位元的總和，因此每列最高位一定要視為 1。
            int ret = m * (1 << (n - 1));

            for (int j = 1; j < n; j++)
            {
                int nOnes = 0;

                for (int i = 0; i < m; i++)
                {
                    // 依最高位判斷該列是否需要虛擬翻轉，不直接修改原始矩陣。
                    if (grid[i][0] == 1)
                    {
                        nOnes += grid[i][j];
                    }
                    else
                    {
                        nOnes += 1 - grid[i][j];
                    }
                }

                // 每個非最高位欄都能獨立翻轉，選擇 1 較多的一側取得最大貢獻。
                int k = Math.Max(nOnes, m - nOnes);
                ret += k * (1 << (n - j - 1));
            }

            return ret;
        }

        private sealed record SampleCase(string Name, int[][] Grid, int Expected);

        private sealed record SampleResult(SampleCase Sample, int Actual)
        {
            public bool Passed => Sample.Expected == Actual;
        }
    }
}
