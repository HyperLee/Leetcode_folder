namespace leetcode_2540
{
    internal class Program
    {
        /// <summary>
        /// 2540. Minimum Common Value
        /// https://leetcode.com/problems/minimum-common-value/description/?envType=daily-question&envId=2024-03-09
        /// 2540. 最小公共值
        /// https://leetcode.cn/problems/minimum-common-value/
        /// </summary>
        /// <remarks>
        /// 以固定案例執行三種解法，逐一比較預期值與實際值；全部案例通過時回傳 0，否則回傳非零結束碼。
        /// </remarks>
        /// <param name="args"></param>
        /// <returns>所有驗證通過時回傳 0，任一驗證失敗時回傳 1。</returns>
        static int Main(string[] args)
        {
            return RunSamples();
        }

        /// <summary>
        /// 建立題目限制內的固定案例，執行三種解法並統計驗證結果。
        /// </summary>
        /// <returns>全部驗證通過時回傳 0，否則回傳 1。</returns>
        private static int RunSamples()
        {
            SampleCase[] samples =
            {
                new("官方範例一", new[] { 1, 2, 3 }, new[] { 2, 4 }, 2),
                new("官方範例二", new[] { 1, 2, 3, 6 }, new[] { 2, 3, 4, 5 }, 2),
                new("無交集", new[] { 1, 3, 5 }, new[] { 2, 4, 6 }, -1),
                new("單元素邊界", new[] { 7 }, new[] { 7 }, 7),
                new("重複值", new[] { 1, 2, 2, 4 }, new[] { 2, 2, 3 }, 2),
                new("最大值邊界", new[] { 1, 1_000_000_000 }, new[] { 1_000_000_000 }, 1_000_000_000)
            };

            int passedChecks = 0;
            foreach (SampleCase sample in samples)
            {
                passedChecks += RunCase(sample);
            }

            int totalChecks = samples.Length * 3;
            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
            return passedChecks == totalChecks ? 0 : 1;
        }

        /// <summary>
        /// 執行單一案例，輸出三種解法的實際結果並計算通過數量。
        /// </summary>
        /// <param name="sample">包含兩個已排序陣列與預期最小公共值的測試案例。</param>
        /// <returns>本案例通過的解法驗證數量，範圍為 0 到 3。</returns>
        private static int RunCase(SampleCase sample)
        {
            Console.WriteLine($"案例：{sample.Name}");
            Console.WriteLine($"nums1 = [{FormatArray(sample.Nums1)}]");
            Console.WriteLine($"nums2 = [{FormatArray(sample.Nums2)}]");
            Console.WriteLine($"預期 = {sample.Expected}");

            (string Name, int Actual)[] results =
            {
                ("GetCommon", GetCommon(sample.Nums1, sample.Nums2)),
                ("GetCommon2", GetCommon2(sample.Nums1, sample.Nums2)),
                ("GetCommon3", GetCommon3(sample.Nums1, sample.Nums2))
            };

            int passedChecks = 0;
            foreach ((string name, int actual) in results)
            {
                bool passed = actual == sample.Expected;
                if (passed)
                {
                    passedChecks++;
                }

                Console.WriteLine($"{name,-11} 實際 = {actual} => {(passed ? "PASS" : "FAIL")}");
            }

            Console.WriteLine();
            return passedChecks;
        }

        /// <summary>
        /// 將整數陣列格式化成適合主控台與 README 顯示的逗號分隔文字。
        /// </summary>
        /// <param name="values">要格式化的整數陣列。</param>
        /// <returns>不含外層方括號的陣列內容文字。</returns>
        private static string FormatArray(int[] values)
        {
            return string.Join(", ", values);
        }

        private sealed record SampleCase(string Name, int[] Nums1, int[] Nums2, int Expected);


        /// <summary>
        /// 使用 Dictionary 記錄 nums1 出現過的值，再依 nums2 的排序順序尋找第一個命中值。
        /// 題目輸入為兩個非遞減排序的非空整數陣列；若沒有公共值則回傳 -1。
        /// </summary>
        /// <param name="nums1">第一個非遞減排序的非空整數陣列。</param>
        /// <param name="nums2">第二個非遞減排序的非空整數陣列。</param>
        /// <returns>兩個陣列的最小公共值；沒有公共值時回傳 -1。</returns>
        /// <remarks>時間複雜度為 O(nums1.Length + nums2.Length)，額外空間複雜度為 O(nums1.Length)。</remarks>
        public static int GetCommon(int[] nums1, int[] nums2)
        {
            Dictionary<int, int> numbersInFirst = new Dictionary<int, int>();
            foreach (int num in nums1)
            {
                // Dictionary 的 value 不參與判斷，key 只用來表示 nums1 是否出現過此值。
                if (!numbersInFirst.ContainsKey(num))
                {
                    numbersInFirst.Add(num, 1);
                }
            }

            foreach (int num in nums2)
            {
                // nums2 已排序，第一個命中的值必定是所有公共值中最小的。
                if (numbersInFirst.ContainsKey(num))
                {
                    return num;
                }
            }

            return -1;
        }


        /// <summary>
        /// 使用 HashSet 記錄 nums1 的成員，再依 nums2 的排序順序尋找最小公共值。
        /// 題目輸入為兩個非遞減排序的非空整數陣列；若沒有公共值則回傳 -1。
        /// </summary>
        /// <param name="nums1">第一個非遞減排序的非空整數陣列。</param>
        /// <param name="nums2">第二個非遞減排序的非空整數陣列。</param>
        /// <returns>兩個陣列的最小公共值；沒有公共值時回傳 -1。</returns>
        /// <remarks>HashSet 直接表達成員資格，時間複雜度為 O(nums1.Length + nums2.Length)，額外空間複雜度為 O(nums1.Length)。</remarks>
        public static int GetCommon2(int[] nums1, int[] nums2)
        {
            HashSet<int> numbersInFirst = new HashSet<int>();
            foreach (int num in nums1)
            {
                numbersInFirst.Add(num);
            }

            foreach (int num in nums2)
            {
                // nums2 已排序，第一個命中的值就是最小公共值。
                if (numbersInFirst.Contains(num))
                {
                    return num;
                }
            }

            return -1;
        }

        /// <summary>
        /// 利用兩個陣列皆已排序的條件，以雙指標同步掃描並找出最小公共值。
        /// 輸入必須是兩個非遞減排序的非空整數陣列；若找不到公共值則回傳 -1。
        /// </summary>
        /// <param name="nums1">第一個非遞減排序的非空整數陣列。</param>
        /// <param name="nums2">第二個非遞減排序的非空整數陣列。</param>
        /// <returns>兩個陣列的最小公共值；沒有公共值時回傳 -1。</returns>
        /// <remarks>每個指標只會向前移動，時間複雜度為 O(nums1.Length + nums2.Length)，額外空間複雜度為 O(1)。</remarks>
        public static int GetCommon3(int[] nums1, int[] nums2)
        {
            int index1 = 0;
            int index2 = 0;

            while (index1 < nums1.Length && index2 < nums2.Length)
            {
                if (nums1[index1] == nums2[index2])
                {
                    return nums1[index1];
                }

                // 較小的值不可能與目前較大的值相等，只需移動較小值所在的指標。
                if (nums1[index1] < nums2[index2])
                {
                    index1++;
                }
                else
                {
                    index2++;
                }
            }

            return -1;
        }
    }
}