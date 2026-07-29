namespace leetcode_015
{
    internal class Program
    {
        /// <summary>
        /// 15. 3Sum
        /// https://leetcode.com/problems/3sum/
        /// 
        /// 15. 三数之和
        /// https://leetcode.cn/problems/3sum/
        /// 
        /// 給定一個整數陣列 nums，返回所有滿足以下條件的三元組 [nums[i], nums[j], nums[k]]：
        ///  i != j, i != k, and j != k, and nums[i] + nums[j] + nums[k] == 0.
        /// 請注意，解集合中不得包含重複的三元組。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 執行六組固定測資，逐一驗證三種 3Sum 解法的答案集合與輸入陣列保留契約。
        /// </summary>
        /// <remarks>
        /// 每種解法都使用獨立的輸入複本。每筆測資包含一項答案檢查與一項輸入保留檢查，
        /// 因此六組測資、三種解法合計執行 36 項驗證。
        /// </remarks>
        private static void RunSamples()
        {
            List<SampleCase> samples =
            [
                new SampleCase(
                    "官方範例 1 - 兩組不重複答案",
                    [-1, 0, 1, 2, -1, -4],
                    [[-1, -1, 2], [-1, 0, 1]]),
                new SampleCase(
                    "官方範例 2 - 沒有總和為零的三元組",
                    [0, 1, 1],
                    []),
                new SampleCase(
                    "官方範例 3 - 三個零只能形成一組答案",
                    [0, 0, 0],
                    [[0, 0, 0]]),
                new SampleCase(
                    "重複值 - 相同三元組只保留一次",
                    [-2, 0, 0, 2, 2],
                    [[-2, 0, 2]]),
                new SampleCase(
                    "全為正數 - 不可能得到零",
                    [1, 2, 3, 4],
                    []),
                new SampleCase(
                    "題目數值邊界 - 正負十萬互相抵消",
                    [-100000, 0, 100000],
                    [[-100000, 0, 100000]])
            ];

            List<SolutionCase> solutions =
            [
                new SolutionCase("解法一：排序 + 雙指針", ThreeSum),
                new SolutionCase("解法二：三層迴圈暴力枚舉", ThreeSum2),
                new SolutionCase("解法三：HashSet + Two Sum", ThreeSum3)
            ];

            int passedChecks = 0;
            int totalChecks = samples.Count * solutions.Count * 2;

            Console.WriteLine("LeetCode 15 - 3Sum");
            Console.WriteLine("驗證三種解法的答案與輸入陣列保留契約");
            Console.WriteLine();

            for (int sampleIndex = 0; sampleIndex < samples.Count; sampleIndex++)
            {
                SampleCase sample = samples[sampleIndex];

                Console.WriteLine($"案例 {sampleIndex + 1}：{sample.Description}");
                Console.WriteLine($"輸入：{FormatArray(sample.Input)}");
                Console.WriteLine($"預期：{FormatTriplets(sample.Expected)}");

                foreach (SolutionCase solution in solutions)
                {
                    int[] input = [.. sample.Input];
                    int[] originalInput = [.. input];
                    IList<IList<int>> actual = solution.Solve(input);
                    bool resultPassed = HaveSameTriplets(sample.Expected, actual);
                    bool inputPreserved = originalInput.SequenceEqual(input);

                    passedChecks += resultPassed ? 1 : 0;
                    passedChecks += inputPreserved ? 1 : 0;

                    Console.WriteLine($"  {solution.Name}");
                    Console.WriteLine($"    實際：{FormatTriplets(actual)}");
                    Console.WriteLine($"    答案檢查：{FormatCheck(resultPassed)}");
                    Console.WriteLine($"    輸入保留：{FormatCheck(inputPreserved)}");
                }

                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");

            if (passedChecks != totalChecks)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 比較預期與實際三元組集合；每組內部及集合本身的順序都不影響比較結果。
        /// </summary>
        /// <param name="expected">手動定義的預期三元組。</param>
        /// <param name="actual">演算法實際回傳的三元組。</param>
        /// <returns>兩者包含完全相同且數量一致的三元組時回傳 <c>true</c>；否則回傳 <c>false</c>。</returns>
        private static bool HaveSameTriplets(
            IEnumerable<IEnumerable<int>> expected,
            IEnumerable<IEnumerable<int>> actual)
        {
            string[] normalizedExpected = CanonicalizeForComparison(expected);
            string[] normalizedActual = CanonicalizeForComparison(actual);
            return normalizedExpected.SequenceEqual(normalizedActual);
        }

        /// <summary>
        /// 將三元組轉成僅供測試比較使用的排序字串，避免題目允許的輸出順序差異影響驗證。
        /// </summary>
        /// <param name="triplets">要正規化的三元組集合。</param>
        /// <returns>組內排序且集合依字典序排序的字串陣列。</returns>
        private static string[] CanonicalizeForComparison(IEnumerable<IEnumerable<int>> triplets)
        {
            return triplets
                .Select(triplet => string.Join(",", triplet.OrderBy(value => value)))
                .OrderBy(triplet => triplet, StringComparer.Ordinal)
                .ToArray();
        }

        /// <summary>
        /// 將整數陣列格式化為接近 LeetCode 題目範例的顯示形式。
        /// </summary>
        /// <param name="values">要顯示的整數陣列。</param>
        /// <returns>例如 <c>[-1,0,1]</c> 的格式化字串。</returns>
        private static string FormatArray(IEnumerable<int> values)
        {
            return $"[{string.Join(",", values)}]";
        }

        /// <summary>
        /// 將三元組集合格式化為接近 LeetCode 題目範例的巢狀陣列形式。
        /// </summary>
        /// <param name="triplets">要顯示的三元組集合。</param>
        /// <returns>例如 <c>[[-1,-1,2],[-1,0,1]]</c> 的格式化字串。</returns>
        private static string FormatTriplets(IEnumerable<IEnumerable<int>> triplets)
        {
            return $"[{string.Join(",", triplets.Select(FormatArray))}]";
        }

        /// <summary>
        /// 將布林檢查結果轉為主控台使用的 PASS 或 FAIL。
        /// </summary>
        /// <param name="passed">檢查是否通過。</param>
        /// <returns>通過時回傳 <c>PASS</c>，否則回傳 <c>FAIL</c>。</returns>
        private static string FormatCheck(bool passed)
        {
            return passed ? "PASS" : "FAIL";
        }

        /// <summary>
        /// 表示一筆可執行測資，包含案例說明、輸入陣列與手動推導的預期三元組。
        /// </summary>
        /// <param name="Description">案例涵蓋的情境。</param>
        /// <param name="Input">傳入解法的原始整數陣列。</param>
        /// <param name="Expected">預期得到的不重複三元組。</param>
        private sealed record SampleCase(string Description, int[] Input, int[][] Expected);

        /// <summary>
        /// 表示一種可執行的 3Sum 解法及其主控台顯示名稱。
        /// </summary>
        /// <param name="Name">解法名稱與核心概念。</param>
        /// <param name="Solve">接收整數陣列並回傳三元組集合的函式。</param>
        private sealed record SolutionCase(
            string Name,
            Func<int[], IList<IList<int>>> Solve);


        /// <summary>
        /// 使用排序與雙指針找出所有總和為零且不重複的三元組。
        /// </summary>
        /// <remarks>
        /// 先複製並排序輸入，接著固定第一個數，再從其右側以左右指針尋找互補的兩個數。
        /// 總和過小時右移左指針，總和過大時左移右指針，並跳過重複值。
        /// 輸入需符合題目條件：長度介於 3 到 3000，各元素介於 -100000 到 100000。
        /// 此方法不會修改 <paramref name="nums"/>。
        /// </remarks>
        /// <param name="nums">要搜尋的整數陣列；同一個索引在一組答案中只能使用一次。</param>
        /// <returns>所有總和為零的不重複三元組，組內與集合皆依遞增字典序排列。</returns>
        public static IList<IList<int>> ThreeSum(int[] nums)
        {
            int[] sortedNums = [.. nums];
            Array.Sort(sortedNums);
            HashSet<(int First, int Second, int Third)> triplets = [];

            for (int first = 0; first < sortedNums.Length - 2; first++)
            {
                if (sortedNums[first] > 0)
                {
                    break;
                }

                if (first > 0 && sortedNums[first] == sortedNums[first - 1])
                {
                    continue;
                }

                int second = first + 1;
                int third = sortedNums.Length - 1;

                while (second < third)
                {
                    int sum = sortedNums[first] + sortedNums[second] + sortedNums[third];

                    if (sum == 0)
                    {
                        AddNormalizedTriplet(
                            triplets,
                            sortedNums[first],
                            sortedNums[second],
                            sortedNums[third]);

                        int secondValue = sortedNums[second];
                        int thirdValue = sortedNums[third];

                        // 找到答案後同時收縮區間，並跨過相同值以免重複枚舉同一組答案。
                        second++;
                        third--;

                        while (second < third && sortedNums[second] == secondValue)
                        {
                            second++;
                        }

                        while (second < third && sortedNums[third] == thirdValue)
                        {
                            third--;
                        }
                    }
                    else if (sum < 0)
                    {
                        // 陣列已排序，總和過小時右移 second 才能取得更大的數。
                        second++;
                    }
                    else
                    {
                        // 總和過大時左移 third，縮小目前使用的最大值。
                        third--;
                    }
                }
            }

            return ToOrderedResult(triplets);
        }

        /// <summary>
        /// 使用三層迴圈枚舉所有不同索引組合，找出總和為零且不重複的三元組。
        /// </summary>
        /// <remarks>
        /// 依序枚舉滿足 <c>first &lt; second &lt; third</c> 的索引，因此同一位置不會重複使用。
        /// 命中的三元組會先排序再放入 HashSet 去重。輸入需符合題目條件：長度介於 3 到 3000，
        /// 各元素介於 -100000 到 100000。此方法不會修改 <paramref name="nums"/>。
        /// </remarks>
        /// <param name="nums">要完整枚舉不同三索引組合的整數陣列。</param>
        /// <returns>所有總和為零的不重複三元組，組內與集合皆依遞增字典序排列。</returns>
        public static IList<IList<int>> ThreeSum2(int[] nums)
        {
            HashSet<(int First, int Second, int Third)> triplets = [];

            for (int first = 0; first < nums.Length - 2; first++)
            {
                for (int second = first + 1; second < nums.Length - 1; second++)
                {
                    for (int third = second + 1; third < nums.Length; third++)
                    {
                        if (nums[first] + nums[second] + nums[third] == 0)
                        {
                            AddNormalizedTriplet(
                                triplets,
                                nums[first],
                                nums[second],
                                nums[third]);
                        }
                    }
                }
            }

            return ToOrderedResult(triplets);
        }

        /// <summary>
        /// 固定第一個數並使用 HashSet 解 Two Sum，找出所有總和為零且不重複的三元組。
        /// </summary>
        /// <remarks>
        /// 對每個第一索引建立新的 seen 集合。掃描後續元素時，檢查使三數總和為零的補數
        /// 是否已經出現；若存在便記錄答案。輸入需符合題目條件：長度介於 3 到 3000，
        /// 各元素介於 -100000 到 100000。此方法不會修改 <paramref name="nums"/>。
        /// </remarks>
        /// <param name="nums">要以固定第一個數加上 HashSet 補數搜尋的整數陣列。</param>
        /// <returns>所有總和為零的不重複三元組，組內與集合皆依遞增字典序排列。</returns>
        public static IList<IList<int>> ThreeSum3(int[] nums)
        {
            HashSet<(int First, int Second, int Third)> triplets = [];

            for (int first = 0; first < nums.Length - 2; first++)
            {
                HashSet<int> seen = [];

                for (int second = first + 1; second < nums.Length; second++)
                {
                    int complement = -nums[first] - nums[second];

                    // complement 必須先在目前 first 右側出現，才能確保三個索引彼此不同。
                    if (seen.Contains(complement))
                    {
                        AddNormalizedTriplet(
                            triplets,
                            nums[first],
                            nums[second],
                            complement);
                    }

                    seen.Add(nums[second]);
                }
            }

            return ToOrderedResult(triplets);
        }

        /// <summary>
        /// 將三個整數排序為唯一標準形式後加入三元組集合。
        /// </summary>
        /// <param name="triplets">儲存不重複三元組的集合。</param>
        /// <param name="first">三元組的第一個候選值。</param>
        /// <param name="second">三元組的第二個候選值。</param>
        /// <param name="third">三元組的第三個候選值。</param>
        private static void AddNormalizedTriplet(
            HashSet<(int First, int Second, int Third)> triplets,
            int first,
            int second,
            int third)
        {
            // 三次條件交換足以將三個值排列成 first <= second <= third。
            if (first > second)
            {
                (first, second) = (second, first);
            }

            if (second > third)
            {
                (second, third) = (third, second);
            }

            if (first > second)
            {
                (first, second) = (second, first);
            }

            triplets.Add((first, second, third));
        }

        /// <summary>
        /// 將正規化三元組依字典序轉為題目要求的巢狀 IList 回傳型別。
        /// </summary>
        /// <param name="triplets">已正規化且不重複的三元組集合。</param>
        /// <returns>組內遞增，且依第一、第二、第三個值排序的三元組清單。</returns>
        private static IList<IList<int>> ToOrderedResult(
            IEnumerable<(int First, int Second, int Third)> triplets)
        {
            return triplets
                .OrderBy(triplet => triplet.First)
                .ThenBy(triplet => triplet.Second)
                .ThenBy(triplet => triplet.Third)
                .Select(triplet => (IList<int>)new List<int>
                {
                    triplet.First,
                    triplet.Second,
                    triplet.Third
                })
                .ToList();

        }
    }
}
