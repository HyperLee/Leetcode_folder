namespace leetcode_1051
{
    internal class Program
    {
        /// <summary>
        /// 1051. Height Checker
        /// https://leetcode.com/problems/height-checker/description/?envType=daily-question&envId=2024-06-10
        /// 1051. 高度检查器
        /// https://leetcode.cn/problems/height-checker/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Environment.ExitCode = RunSamples() ? 0 : 1;
        }

        /// <summary>
        /// 執行固定測試案例，逐一用排序法與計數排序法驗證答案及輸入保留性。
        /// 此方法不接受輸入；輸出每個案例的預期值、實際值與通過狀態，
        /// 並回傳所有解法檢查是否全部通過。
        /// </summary>
        /// <returns>全部檢查通過時為 <see langword="true"/>，否則為 <see langword="false"/>。</returns>
        private static bool RunSamples()
        {
            (string Name, int[] Heights, int Expected)[] cases =
            [
                ("官方範例一", [1, 1, 4, 2, 1, 3], 3),
                ("官方範例二", [5, 1, 2, 3, 4], 5),
                ("官方範例三：已排序", [1, 2, 3, 4, 5], 0),
                ("單一最小值", [1], 0),
                ("重複值亂序", [2, 2, 1, 1], 4),
                ("值域上下界", [100, 1, 100, 1], 2),
                ("防禦性空陣列", [], 0)
            ];

            int passedChecks = 0;
            foreach ((string name, int[] heights, int expected) in cases)
            {
                passedChecks += RunTestCase(name, heights, expected);
            }

            int totalChecks = cases.Length * 2;
            Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");
            return passedChecks == totalChecks;
        }

        /// <summary>
        /// 執行一組高度案例，讓兩種解法使用獨立副本，避免彼此影響。
        /// 輸入包含案例名稱、高度陣列與手算預期值；輸出比較資訊，
        /// 並回傳答案正確且沒有改動輸入的解法數量。
        /// </summary>
        /// <param name="name">顯示於主控台的案例名稱。</param>
        /// <param name="heights">待驗證的高度陣列；正式題目要求元素介於 1 到 100。</param>
        /// <param name="expected">預期的不符合位置數量。</param>
        /// <returns>通過的解法數量，範圍為 0 到 2。</returns>
        private static int RunTestCase(string name, int[] heights, int expected)
        {
            int[] original = [.. heights];
            int[] sortingInput = [.. heights];
            int[] countingInput = [.. heights];

            int sortingActual = HeightChecker(sortingInput);
            int countingActual = HeightChecker2(countingInput);
            bool sortingInputPreserved = sortingInput.SequenceEqual(original);
            bool countingInputPreserved = countingInput.SequenceEqual(original);
            bool sortingPassed = sortingActual == expected && sortingInputPreserved;
            bool countingPassed = countingActual == expected && countingInputPreserved;

            Console.WriteLine($"Case: {name}");
            Console.WriteLine($"Input: {FormatArray(heights)}");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine(
                $"HeightChecker: {sortingActual} | Input preserved: {(sortingInputPreserved ? "PASS" : "FAIL")} | Result: {(sortingPassed ? "PASS" : "FAIL")}");
            Console.WriteLine(
                $"HeightChecker2: {countingActual} | Input preserved: {(countingInputPreserved ? "PASS" : "FAIL")} | Result: {(countingPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return (sortingPassed ? 1 : 0) + (countingPassed ? 1 : 0);
        }

        /// <summary>
        /// 將整數陣列轉為適合主控台與 README 顯示的固定格式。
        /// 輸入必須是非 <see langword="null"/> 陣列；輸出以方括號包住元素，
        /// 空陣列會輸出 <c>[]</c>。
        /// </summary>
        /// <param name="values">要格式化的整數陣列。</param>
        /// <returns>以逗號和空格分隔元素的陣列字串。</returns>
        private static string FormatArray(int[] values)
        {
            return $"[{string.Join(", ", values)}]";
        }

        /// <summary>
        /// 計算目前高度順序與非遞減預期順序不同的位置數量。
        /// 解法先複製輸入並排序副本，再逐一比較相同索引；
        /// 輸入須為非 <see langword="null"/> 且高度介於 1 到 100 的陣列，輸出為不相符的索引數量。
        /// </summary>
        /// <param name="heights">學生目前排列的高度陣列。</param>
        /// <returns>目前順序與非遞減順序不同的位置數量。</returns>
        public static int HeightChecker(int[] heights)
        {
            int mismatchCount = 0;
            int[] expected = [.. heights];

            // 排序副本可取得預期隊伍，同時保留呼叫端傳入的原始順序。
            Array.Sort(expected);

            for (int i = 0; i < heights.Length; i++)
            {
                if (heights[i] != expected[i])
                {
                    mismatchCount++;
                }
            }

            return mismatchCount;
        }

        /// <summary>
        /// 使用高度頻率計算目前順序與非遞減預期順序不同的位置數量。
        /// 解法依題目 1 到 100 的值域統計每種高度，再按高度由小到大展開預期順序；
        /// 輸入須為非 <see langword="null"/> 且高度介於 1 到 100 的陣列，輸出為不相符的索引數量。
        /// </summary>
        /// <param name="heights">學生目前排列的高度陣列。</param>
        /// <returns>目前順序與非遞減順序不同的位置數量。</returns>
        public static int HeightChecker2(int[] heights)
        {
            int[] frequencies = new int[101];
            foreach (int height in heights)
            {
                frequencies[height]++;
            }

            int mismatchCount = 0;
            int expectedHeight = 1;
            foreach (int actualHeight in heights)
            {
                // 跳過已耗盡的高度，下一個仍有數量的桶就是目前索引的預期高度。
                while (frequencies[expectedHeight] == 0)
                {
                    expectedHeight++;
                }

                if (actualHeight != expectedHeight)
                {
                    mismatchCount++;
                }

                frequencies[expectedHeight]--;
            }

            return mismatchCount;
        }
    }
}