namespace leetcode_1509
{
    internal class Program
    {
        /// <summary>
        /// 1509. Minimum Difference Between Largest and Smallest Value in Three Moves
        /// https://leetcode.com/problems/minimum-difference-between-largest-and-smallest-value-in-three-moves/description/
        /// 1509. 三次操作后最大值与最小值的最小差
        /// https://leetcode.cn/problems/minimum-difference-between-largest-and-smallest-value-in-three-moves/description/
        ///
        /// You are given an integer array nums.
        /// In one move, you can choose one element of nums and change it to any value.
        /// Return the minimum difference between the largest and smallest value of nums after performing at most three moves.
        ///
        /// 給定一個整數陣列 nums。
        /// 在一次操作（move）中，你可以選擇 nums 中的一個元素，並將它修改為任意值。
        /// 請回傳最多進行三次操作後，陣列中最大值與最小值之間的最小差值。
        /// </summary>
        /// <remarks>
        /// 程式進入點會以五組固定案例驗證兩種解法，逐一輸出實際結果與通過狀態，最後彙整通過數量。
        /// 輸入案例涵蓋最小長度、長度四邊界、典型案例、重複值與數值範圍邊界。
        /// </remarks>
        /// <param name="args">命令列參數；本範例不使用。</param>
        private static void Main(string[] args)
        {
            int passedChecks = 0;
            const int totalChecks = 10;

            Console.WriteLine("LeetCode 1509 - 三次操作後最大值與最小值的最小差");
            Console.WriteLine();

            passedChecks += RunTestCase(1, "最小長度", [7], 0);
            passedChecks += RunTestCase(2, "長度 4 邊界", [5, 3, 2, 4], 0);
            passedChecks += RunTestCase(3, "官方典型案例", [1, 5, 0, 10, 14], 1);
            passedChecks += RunTestCase(4, "重複值", [6, 6, 0, 1, 1, 4, 6], 2);
            passedChecks += RunTestCase(
                5,
                "數值邊界",
                [1_000_000_000, -500_000_000, 0, -1_000_000_000, 500_000_000],
                500_000_000);

            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 通過");
        }

        /// <summary>
        /// 執行一組固定案例，分別呼叫完整排序與維護四個極值的解法，並輸出預期值、實際值與 PASS/FAIL。
        /// 每個解法都取得原始陣列的複本，避免排序解法修改輸入後影響另一個解法；輸入須符合題目限制，
        /// 即長度介於 1 到 100,000，且每個元素介於 -1,000,000,000 到 1,000,000,000。
        /// </summary>
        /// <param name="caseNumber">從 1 開始顯示的案例編號。</param>
        /// <param name="name">案例用途或涵蓋情境的名稱。</param>
        /// <param name="nums">要交給兩種解法驗證的原始整數陣列。</param>
        /// <param name="expected">此案例的預期最小差值。</param>
        /// <returns>此案例通過的解法數量，範圍為 0 到 2。</returns>
        private static int RunTestCase(int caseNumber, string name, int[] nums, int expected)
        {
            int sortingResult = MinDifference((int[])nums.Clone());
            Program solution = new Program();
            int extremaResult = solution.MinDifference2((int[])nums.Clone());
            bool sortingPassed = sortingResult == expected;
            bool extremaPassed = extremaResult == expected;

            Console.WriteLine($"案例 {caseNumber}：{name}");
            Console.WriteLine($"輸入：[{string.Join(", ", nums)}]");
            Console.WriteLine($"預期：{expected}");
            Console.WriteLine(
                $"MinDifference（完整排序）：實際 {sortingResult} -> {(sortingPassed ? "PASS" : "FAIL")}");
            Console.WriteLine(
                $"MinDifference2（維護四個極值）：實際 {extremaResult} -> {(extremaPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();

            return (sortingPassed ? 1 : 0) + (extremaPassed ? 1 : 0);
        }

        /// <summary>
        /// 使用完整排序計算最多修改三個元素後的最小極差。當陣列長度不超過 4 時，最多三次修改即可讓所有值相同；
        /// 否則先將陣列由小到大排序，再列舉「修改三個最大值」到「修改三個最小值」的四種極端值組合。
        /// 輸入陣列長度須介於 1 到 100,000，每個元素須介於 -1,000,000,000 到 1,000,000,000；
        /// 此方法會直接排序並改變輸入陣列的元素順序。
        /// </summary>
        /// <param name="nums">符合題目限制的整數陣列；呼叫後元素會依遞增順序排列。</param>
        /// <returns>最多修改三個元素後，可得到的最大值與最小值之最小差值。</returns>
        /// <remarks>
        /// 參考資料：
        /// https://leetcode.cn/problems/minimum-difference-between-largest-and-smallest-value-in-three-moves/solutions/336428/san-ci-cao-zuo-hou-zui-da-zhi-yu-zui-xiao-zhi-de-2/
        /// https://leetcode.cn/problems/minimum-difference-between-largest-and-smallest-value-in-three-moves/solutions/326880/minimum-difference-by-ikaruga/
        /// https://leetcode.cn/problems/minimum-difference-between-largest-and-smallest-value-in-three-moves/solutions/1530726/by-stormsunshine-pqnh/
        /// https://leetcode.cn/problems/minimum-difference-between-largest-and-smallest-value-in-three-moves/solutions/824266/c-kan-diao-zhu-mu-lang-ma-feng-tian-ping-lf0m/
        /// </remarks>
        public static int MinDifference(int[] nums)
        {
            int n = nums.Length;

            if (n <= 4)
            {
                // 最多修改三個元素，因此長度不超過四時可把所有元素調整成相同值。
                return 0;
            }

            // 修改最大值或最小值等價於排除排序後的極端值，因此只需比較四種剩餘區間。
            Array.Sort(nums);
            int result = int.MaxValue;

            for (int i = 0; i < 4; i++)
            {
                result = Math.Min(result, nums[n - 4 + i] - nums[i]);
            }

            return result;
        }

        /// <summary>
        /// 以一次掃描維護四個最大值與四個最小值，計算最多修改三個元素後的最小極差。
        /// 最大值陣列維持遞減、最小值陣列維持遞增，再配對四種「從兩端合計排除三個元素」的情況；
        /// 輸入陣列長度須介於 1 到 100,000，每個元素須介於 -1,000,000,000 到 1,000,000,000，
        /// 且此方法不會修改輸入陣列。
        /// </summary>
        /// <param name="nums">符合題目限制且保持原順序不變的整數陣列。</param>
        /// <returns>最多修改三個元素後，可得到的最大值與最小值之最小差值。</returns>
        public int MinDifference2(int[] nums)
        {
            int n = nums.Length;

            if (n <= 4)
            {
                // 最多修改三個元素，因此長度不超過四時可把所有元素調整成相同值。
                return 0;
            }

            int[] maxValues = new int[4];
            int[] minValues = new int[4];

            Array.Fill(maxValues, -1_000_000_000);
            Array.Fill(minValues, 1_000_000_000);

            foreach (int value in nums)
            {
                // 固定長度為四，插入後維持最大值陣列由大到小排列。
                int insertIndex = 0;

                while (insertIndex < 4 && maxValues[insertIndex] > value)
                {
                    insertIndex++;
                }

                if (insertIndex < 4)
                {
                    for (int j = 3; j > insertIndex; j--)
                    {
                        maxValues[j] = maxValues[j - 1];
                    }

                    maxValues[insertIndex] = value;
                }

                // 固定長度為四，插入後維持最小值陣列由小到大排列。
                insertIndex = 0;

                while (insertIndex < 4 && minValues[insertIndex] < value)
                {
                    insertIndex++;
                }

                if (insertIndex < 4)
                {
                    for (int j = 3; j > insertIndex; j--)
                    {
                        minValues[j] = minValues[j - 1];
                    }

                    minValues[insertIndex] = value;
                }
            }

            int result = int.MaxValue;

            // 配對最大與最小的四個候選邊界，涵蓋兩端合計排除三個元素的全部組合。
            for (int i = 0; i < 4; i++)
            {
                result = Math.Min(result, maxValues[i] - minValues[3 - i]);
            }

            return result;
        }
    }
}
