namespace leetcode_075
{
    internal class Program
    {
        /// <summary>
        /// 75. Sort Colors
        /// https://leetcode.com/problems/sort-colors/?envType=daily-question&envId=2024-06-12
        /// 75. 颜色分类
        /// https://leetcode.cn/problems/sort-colors/description/
        /// 
        /// 排序一個只包含 0、1 和 2 的整數數組。這是一個經典的問題，稱為荷蘭國旗問題。
        /// 目標是就地排序數組，使所有的 0 排在最前面，接著是所有的 1，最後是所有的 2。
        /// 
        /// 題目給定一個只包含 0（紅色）、1（白色）、2（藍色）三種數字的陣列 nums，要求 就地（in-place） 進行排序，使得 0 在最前，1 在中間，2 在最後。
        /// 
        /// 不能使用API呼叫排序
        /// </summary>
        /// <remarks>
        /// 主要進入點會以六組固定資料依序驗證五種排序解法，並統一輸出預期結果、
        /// 實際結果與 PASS/FAIL。命令列參數不參與測試。
        /// </remarks>
        /// <param name="args">命令列參數；本範例不使用。</param>
        static void Main(string[] args)
        {
            (string Name, int[] Input, int[] Expected)[] testCases =
            {
                ("官方範例 1", new[] { 2, 0, 2, 1, 1, 0 }, new[] { 0, 0, 1, 1, 2, 2 }),
                ("官方範例 2", new[] { 2, 0, 1 }, new[] { 0, 1, 2 }),
                ("單一元素", new[] { 1 }, new[] { 1 }),
                ("已排序", new[] { 0, 0, 1, 1, 2, 2 }, new[] { 0, 0, 1, 1, 2, 2 }),
                ("反向排列", new[] { 2, 2, 1, 1, 0, 0 }, new[] { 0, 0, 1, 1, 2, 2 }),
                ("全部相同", new[] { 2, 2, 2 }, new[] { 2, 2, 2 })
            };

            (string Name, Action<int[]> Sort)[] solutions =
            {
                ("SortColors / BubbleSortAlgorithm", SortColors),
                ("SortColors2", SortColors2),
                ("CountingSortAlgorithm", CountingSortAlgorithm),
                ("SortColors3", SortColors3),
                ("SortColors4", SortColors4)
            };

            int passed = 0;
            int total = 0;

            foreach ((string solutionName, Action<int[]> sort) in solutions)
            {
                Console.WriteLine($"[{solutionName}]");

                foreach ((string caseName, int[] input, int[] expected) in testCases)
                {
                    total++;
                    if (RunTestCase(caseName, input, expected, sort))
                    {
                        passed++;
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine($"Overall: {passed}/{total} passed.");
        }

        /// <summary>
        /// 執行一筆排序驗證。方法會複製輸入陣列後呼叫指定解法，以免原地排序影響其他測試，
        /// 再比較實際與預期陣列並輸出測試結果。輸入必須符合題目條件：長度為 1 到 300，
        /// 且每個元素只能是 0、1 或 2；回傳值表示實際結果是否完全符合預期。
        /// </summary>
        /// <param name="caseName">顯示於主控台的案例名稱。</param>
        /// <param name="input">尚未排序的合法測試資料；方法不會修改此陣列。</param>
        /// <param name="expected">預期的非遞減排序結果。</param>
        /// <param name="sort">接受整數陣列並進行原地排序的解法。</param>
        /// <returns>實際排序結果與預期結果相同時為 <see langword="true"/>，否則為 <see langword="false"/>。</returns>
        private static bool RunTestCase(
            string caseName,
            int[] input,
            int[] expected,
            Action<int[]> sort)
        {
            int[] actual = (int[])input.Clone();
            sort(actual);

            bool passed = actual.SequenceEqual(expected);
            Console.WriteLine(
                $"{caseName} | Input: {FormatArray(input)} | Expected: {FormatArray(expected)} | " +
                $"Actual: {FormatArray(actual)} | {(passed ? "PASS" : "FAIL")}");

            return passed;
        }

        /// <summary>
        /// 將整數陣列格式化為 README 與測試輸出使用的緊湊表示法。
        /// 輸入可為任何非 <see langword="null"/> 的整數陣列；輸出格式為方括號包住、
        /// 以逗號分隔的元素，例如 <c>[2,0,1]</c>，且不會修改原陣列。
        /// </summary>
        /// <param name="nums">要格式化的整數陣列。</param>
        /// <returns>陣列的緊湊字串表示。</returns>
        private static string FormatArray(int[] nums)
        {
            return $"[{string.Join(",", nums)}]";
        }

        /// <summary>
        /// 使用泡沫排序將顏色陣列原地排成 0、1、2 的順序，並委派給
        /// <see cref="BubbleSortAlgorithm(int[])"/> 完成相鄰元素比較與交換。
        /// 輸入長度必須為 1 到 300，元素只能是 0、1 或 2；完成後原陣列會按非遞減順序排列。
        /// </summary>
        /// <param name="nums">要原地排序的顏色陣列。</param>
        public static void SortColors(int[] nums)
        {
            BubbleSortAlgorithm(nums);
        }

        /// <summary>
        /// 使用泡沫排序反覆比較相鄰元素，若前者較大便交換，讓每一輪尚未排序區間中的最大值
        /// 移到右端。輸入長度必須為 1 到 300，元素只能是 0、1 或 2；
        /// 完成後原陣列會按非遞減順序排列。
        /// </summary>
        /// <param name="arr">要原地排序的顏色陣列。</param>
        public static void BubbleSortAlgorithm(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                // 每完成一輪，右側便多一個已定位的最大值，因此下一輪可縮短比較範圍。
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// 使用針對 0、1、2 最佳化的計數排序。先以固定三格陣列統計每種顏色的數量，
        /// 再依顏色順序覆寫原陣列。輸入長度必須為 1 到 300，元素只能是 0、1 或 2；
        /// 完成後原陣列會按非遞減順序排列。
        /// </summary>
        /// <param name="nums">要原地排序的顏色陣列。</param>
        public static void SortColors2(int[] nums)
        {
            int[] counts = new int[3];
            foreach (int num in nums)
            {
                counts[num]++;
            }

            // 依序消耗各顏色的計數，直接把 0、1、2 寫回原陣列。
            int n = nums.Length;
            for (int i = 0, j = 0; i < n; i++)
            {
                while (counts[j] == 0)
                {
                    j++;
                }

                nums[i] = j;
                counts[j]--;
            }
        }

        /// <summary>
        /// 使用一般化的穩定計數排序。方法會找出最大值、建立各值的出現次數與累積位置，
        /// 從右向左放入輸出陣列後再複製回原陣列。輸入長度必須為 1 到 300，
        /// 元素只能是 0、1 或 2；完成後原陣列會按非遞減順序排列。
        /// </summary>
        /// <param name="arr">要原地呈現排序結果的顏色陣列。</param>
        public static void CountingSortAlgorithm(int[] arr)
        {
            int n = arr.Length;
            int[] output = new int[n];

            int max = arr[0];
            for (int i = 1; i < n; i++)
            {
                if (arr[i] > max)
                    max = arr[i];
            }

            int[] count = new int[max + 1];

            for (int i = 0; i < n; ++i)
            {
                ++count[arr[i]];
            }

            // 累積計數代表每個值在輸出陣列中的右邊界位置。
            for (int i = 1; i <= max; ++i)
            {
                count[i] += count[i - 1];
            }

            // 從右向左放置可保留相同值的原始相對順序，使計數排序維持穩定。
            for (int i = n - 1; i >= 0; i--)
            {
                output[count[arr[i]] - 1] = arr[i];
                --count[arr[i]];
            }

            for (int i = 0; i < n; ++i)
            {
                arr[i] = output[i];
            }
        }

        /// <summary>
        /// 使用左右雙指標搭配單次掃描：<c>p0</c> 指向下一個 0 的位置，
        /// <c>p2</c> 指向下一個 2 的位置，掃描後讓 1 自然留在中間。
        /// 輸入長度必須為 1 到 300，元素只能是 0、1 或 2；
        /// 完成後原陣列會按非遞減順序排列。
        /// </summary>
        /// <param name="nums">要原地排序的顏色陣列。</param>
        public static void SortColors3(int[] nums)
        {
            int n = nums.Length;
            int p0 = 0;
            int p2 = n - 1;

            for (int i = 0; i < n; i++)
            {
                // 與右側交換回來的值尚未分類，因此固定 i 並持續檢查，直到它不再是 2。
                while (i <= p2 && nums[i] == 2)
                {
                    int temp = nums[i];
                    nums[i] = nums[p2];
                    nums[p2] = temp;
                    p2--;
                }

                // 此時若為 0，便放到左側已分類區間的下一格；其餘的 1 留在中間。
                if (nums[i] == 0)
                {
                    int temp = nums[i];
                    nums[i] = nums[p0];
                    nums[p0] = temp;
                    p0++;
                }
            }
        }

        /// <summary>
        /// 使用荷蘭國旗三指標法將陣列分成三個區域：<c>low</c> 左側全為 0、
        /// <c>high</c> 右側全為 2，<c>mid</c> 掃描尚未分類的元素。
        /// 輸入長度必須為 1 到 300，元素只能是 0、1 或 2；
        /// 完成後原陣列會按非遞減順序排列。
        /// </summary>
        /// <param name="nums">要排序的整數陣列。</param>
        public static void SortColors4(int[] nums)
        {
            int low = 0;
            int high = nums.Length - 1;
            int mid = 0;
            int temp = 0;

            while (mid <= high)
            {
                switch (nums[mid])
                {
                    case 0:
                        {
                            temp = nums[low];
                            nums[low] = nums[mid];
                            nums[mid] = temp;
                            low++;
                            mid++;
                            break;
                        }
                    case 1:
                        {
                            mid++;
                            break;
                        }
                    case 2:
                        {
                            // 將 mid 位置的元素與 high 位置的元素交換
                            temp = nums[mid];
                            nums[mid] = nums[high];
                            nums[high] = temp;
                            high--;

                            // 右側換回來的值仍未分類，所以 mid 必須留在原位再次判斷。
                            break;
                        }
                }
            }
        }
    }
}