namespace leetcode_350
{
    internal class Program
    {
        /// <summary>
        /// 350. Intersection of Two Arrays II
        /// https://leetcode.com/problems/intersection-of-two-arrays-ii/description/?envType=daily-question&envId=2024-07-02
        /// 350. 两个数组的交集 II
        /// https://leetcode.cn/problems/intersection-of-two-arrays-ii/description/
        /// 
        /// 本題目為 349. Intersection of Two Arrays
        /// 進階衍生題目
        /// 
        /// 本題目如果遇到重覆數字,需要輸出相同數量的數字
        /// 不能只輸出一個數字當作代表
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] maximumLengthNums1 = Enumerable.Repeat(1000, 1000).ToArray();
            int[] maximumLengthNums2 = Enumerable.Repeat(0, 999).Append(1000).ToArray();
            TestCase[] testCases =
            [
                new("Official example 1", "[1, 2, 2, 1]", "[2, 2]", [1, 2, 2, 1], [2, 2], [2, 2]),
                new("Official example 2", "[4, 9, 5]", "[9, 4, 9, 8, 4]", [4, 9, 5], [9, 4, 9, 8, 4], [4, 9]),
                new("No intersection", "[1, 2, 3]", "[4, 5, 6]", [1, 2, 3], [4, 5, 6], []),
                new("Asymmetric duplicate counts", "[1, 1, 1, 2]", "[1, 1, 2, 2]", [1, 1, 1, 2], [1, 1, 2, 2], [1, 1, 2]),
                new("Minimum lengths and value", "[0]", "[0]", [0], [0], [0]),
                new("Complete intersection in different order", "[0, 500, 1000]", "[1000, 500, 0]", [0, 500, 1000], [1000, 500, 0], [0, 500, 1000]),
                new("Second array is shorter", "[1, 2, 2, 3, 3]", "[2, 3]", [1, 2, 2, 3, 3], [2, 3], [2, 3]),
                new(
                    "Maximum lengths and value",
                    "[length 1000; all values are 1000]",
                    "[length 1000; 999 zeros followed by 1000]",
                    maximumLengthNums1,
                    maximumLengthNums2,
                    [1000])
            ];

            int passed = 0;
            foreach (TestCase testCase in testCases)
            {
                int[] intersectNums1 = [.. testCase.Nums1];
                int[] intersectNums2 = [.. testCase.Nums2];
                int[] intersect2Nums1 = [.. testCase.Nums1];
                int[] intersect2Nums2 = [.. testCase.Nums2];

                int[] intersectActual = Intersect(intersectNums1, intersectNums2);
                int[] intersect2Actual = Intersect2(intersect2Nums1, intersect2Nums2);
                bool inputsPreserved = intersectNums1.SequenceEqual(testCase.Nums1)
                    && intersectNums2.SequenceEqual(testCase.Nums2)
                    && intersect2Nums1.SequenceEqual(testCase.Nums1)
                    && intersect2Nums2.SequenceEqual(testCase.Nums2);
                bool isPassed = IsSameMultiset(intersectActual, testCase.Expected)
                    && IsSameMultiset(intersect2Actual, testCase.Expected)
                    && inputsPreserved;

                if (isPassed)
                {
                    passed++;
                }

                Console.WriteLine($"Case: {testCase.Name}");
                Console.WriteLine($"Nums1: {testCase.Nums1Display}");
                Console.WriteLine($"Nums2: {testCase.Nums2Display}");
                Console.WriteLine($"Expected: {FormatArray(testCase.Expected)}");
                Console.WriteLine($"Intersect: {FormatArray(intersectActual)}");
                Console.WriteLine($"Intersect2: {FormatArray(intersect2Actual)}");
                Console.WriteLine($"Inputs preserved: {inputsPreserved}");
                Console.WriteLine($"Result: {(isPassed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"Summary: {passed}/{testCases.Length} checks passed.");
            if (passed != testCases.Length)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 比較兩個整數陣列是否代表相同的多重集合。
        /// 方法會排序各自的副本，因此可忽略輸出順序並保留重複元素的數量，
        /// 且不會修改傳入陣列。
        /// </summary>
        /// <param name="actual">演算法實際回傳的非 <see langword="null"/> 陣列。</param>
        /// <param name="expected">測試案例預期的非 <see langword="null"/> 陣列。</param>
        /// <returns>元素與每個元素的出現次數完全相同時回傳 <see langword="true"/>。</returns>
        private static bool IsSameMultiset(int[] actual, int[] expected)
        {
            int[] sortedActual = [.. actual];
            int[] sortedExpected = [.. expected];
            Array.Sort(sortedActual);
            Array.Sort(sortedExpected);

            return sortedActual.SequenceEqual(sortedExpected);
        }

        /// <summary>
        /// 將整數陣列格式化為穩定且易讀的文字。
        /// 顯示前只排序副本，不改變演算法輸出或測試資料。
        /// </summary>
        /// <param name="numbers">要顯示的非 <see langword="null"/> 整數陣列。</param>
        /// <returns>以中括號包住、由小到大排列的逗號分隔字串。</returns>
        private static string FormatArray(int[] numbers)
        {
            int[] sortedNumbers = [.. numbers];
            Array.Sort(sortedNumbers);

            return $"[{string.Join(", ", sortedNumbers)}]";
        }

        /// <summary>
        /// 使用 Dictionary 次數表計算兩個整數陣列的交集。
        /// 方法先選擇較短陣列建立次數表，再掃描較長陣列並消耗可用次數，
        /// 因此每個共同元素會依兩側較少的出現次數加入結果。
        /// 適用於題目限制內的非 <see langword="null"/> 陣列，不修改輸入；
        /// 平均時間複雜度為 <c>O(n + m)</c>。
        /// </summary>
        /// <param name="nums1">長度介於 1 至 1000，元素介於 0 至 1000 的整數陣列。</param>
        /// <param name="nums2">長度介於 1 至 1000，元素介於 0 至 1000 的整數陣列。</param>
        /// <returns>包含兩個輸入共同元素的陣列；重複次數正確，元素順序不保證。</returns>
        public static int[] Intersect(int[] nums1, int[] nums2)
        {
            if (nums1.Length > nums2.Length)
            {
                return GetIntersection(nums2, nums1);
            }

            return GetIntersection(nums1, nums2);
        }

        /// <summary>
        /// 由較短陣列建立元素次數表，再掃描較長陣列產生交集。
        /// 找到共同元素後立即扣減剩餘次數，歸零時移除該鍵，
        /// 避免同一元素被加入超過短陣列所提供的數量。
        /// 輸入必須是符合題目限制的非 <see langword="null"/> 陣列，
        /// 且 <paramref name="shortNumbers"/> 的長度不得大於
        /// <paramref name="longNumbers"/>；方法不修改輸入。
        /// 平均時間複雜度為 <c>O(n + m)</c>，輔助空間為
        /// <c>O(min(n, m))</c>，結果空間為 <c>O(k)</c>。
        /// </summary>
        /// <param name="shortNumbers">用來建立次數表的較短整數陣列。</param>
        /// <param name="longNumbers">用來逐一查找共同元素的較長整數陣列。</param>
        /// <returns>依較長陣列掃描順序建立的交集陣列；每個值最多出現兩側次數的較小值。</returns>
        public static int[] GetIntersection(int[] shortNumbers, int[] longNumbers)
        {
            List<int> intersection = new List<int>();
            Dictionary<int, int> remainingCounts = new Dictionary<int, int>();

            // 只為較短陣列建立次數表，可將雜湊表空間控制在 O(min(n, m))。
            foreach (int number in shortNumbers)
            {
                if (remainingCounts.TryGetValue(number, out int count))
                {
                    remainingCounts[number] = count + 1;
                }
                else
                {
                    remainingCounts.Add(number, 1);
                }
            }

            foreach (int number in longNumbers)
            {
                if (!remainingCounts.TryGetValue(number, out int count))
                {
                    continue;
                }

                intersection.Add(number);

                // 歸零後移除鍵，後續相同值便不會超量加入結果。
                if (count == 1)
                {
                    remainingCounts.Remove(number);
                }
                else
                {
                    remainingCounts[number] = count - 1;
                }
            }

            return intersection.ToArray();
        }

        /// <summary>
        /// 使用排序副本與雙指標計算兩個整數陣列的交集。
        /// 兩個指標分別走訪排序後的陣列：值相等時加入結果並同時前進，
        /// 否則只前進值較小的一側，因此可正確保留共同元素的重複次數。
        /// 適用於題目限制內的非 <see langword="null"/> 陣列；
        /// 排序只作用於副本，不修改輸入。
        /// 時間複雜度為 <c>O(n log n + m log m)</c>，輔助空間為
        /// <c>O(n + m)</c>，結果空間為 <c>O(k)</c>。
        /// </summary>
        /// <param name="nums1">長度介於 1 至 1000，元素介於 0 至 1000 的整數陣列。</param>
        /// <param name="nums2">長度介於 1 至 1000，元素介於 0 至 1000 的整數陣列。</param>
        /// <returns>由小到大排列的交集陣列；每個值最多出現兩側次數的較小值。</returns>
        public static int[] Intersect2(int[] nums1, int[] nums2)
        {
            int[] sortedNums1 = [.. nums1];
            int[] sortedNums2 = [.. nums2];
            Array.Sort(sortedNums1);
            Array.Sort(sortedNums2);

            List<int> intersection = new List<int>();
            int nums1Index = 0;
            int nums2Index = 0;

            while (nums1Index < sortedNums1.Length && nums2Index < sortedNums2.Length)
            {
                if (sortedNums1[nums1Index] == sortedNums2[nums2Index])
                {
                    intersection.Add(sortedNums1[nums1Index]);
                    nums1Index++;
                    nums2Index++;
                }
                else if (sortedNums1[nums1Index] < sortedNums2[nums2Index])
                {
                    // 較小值不可能和另一側目前或後續值相交，只前進 nums1。
                    nums1Index++;
                }
                else
                {
                    // 同理，nums2 目前值較小時只前進 nums2。
                    nums2Index++;
                }
            }

            return intersection.ToArray();
        }

        /// <summary>
        /// 定義一組可重複執行的交集驗證資料。
        /// </summary>
        /// <param name="Name">案例名稱。</param>
        /// <param name="Nums1Display">第一個陣列的穩定顯示文字。</param>
        /// <param name="Nums2Display">第二個陣列的穩定顯示文字。</param>
        /// <param name="Nums1">第一個輸入陣列。</param>
        /// <param name="Nums2">第二個輸入陣列。</param>
        /// <param name="Expected">預期的交集多重集合。</param>
        private sealed record TestCase(
            string Name,
            string Nums1Display,
            string Nums2Display,
            int[] Nums1,
            int[] Nums2,
            int[] Expected);
    }
}