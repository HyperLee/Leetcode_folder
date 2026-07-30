namespace leetcode_75;

class Program
{
    /// <summary>
    /// LeetCode 75. 颜色分類（Sort Colors）
    /// 題目描述：
    /// 給定一個包含 0、1 和 2 的整數陣列 nums，請你原地對陣列進行排序，使得相同顏色的元素相鄰，並按照 0、1、2 的順序排列。
    /// 你必須在不使用內建 sort 函式的情況下完成這個問題。
    /// 
    /// 解題提示：
    /// 1. 可以使用氣泡排序、計數排序或雙指針（荷蘭國旗問題）等方法。
    /// 2. 本範例採用氣泡排序，雖然效率較低，但實作簡單。
    /// 3. 若需最佳化，建議參考一次遍歷的雙指針法。
    /// 
    /// 題目連結：
    /// https://leetcode.com/problems/sort-colors/description/?envType=daily-question&envId=2025-05-17
    /// https://leetcode.cn/problems/sort-colors/description/?envType=daily-question&envId=2025-05-17
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program program = new Program();
        (string Name, int[] Input, int[] Expected)[] testCases =
        {
            ("官方範例一", new[] { 2, 0, 2, 1, 1, 0 }, new[] { 0, 0, 1, 1, 2, 2 }),
            ("官方範例二", new[] { 2, 0, 1 }, new[] { 0, 1, 2 }),
            ("最小長度邊界", new[] { 1 }, new[] { 1 }),
            ("全部重複值", new[] { 2, 2, 2, 2 }, new[] { 2, 2, 2, 2 })
        };

        int passed = 0;
        foreach ((string name, int[] input, int[] expected) in testCases)
        {
            passed += RunTestCase(program, name, input, expected);
        }

        const int solutionCount = 3;
        int total = testCases.Length * solutionCount;
        Console.WriteLine($"{passed}/{total} passed.");
    }

    /// <summary>
    /// 執行一組固定案例，分別驗證三種原地排序解法。
    /// 每次呼叫解法前都會複製輸入陣列，避免前一種解法的修改影響後續結果。
    /// 輸入陣列須至少包含一個元素，且每個元素只能是 0、1 或 2；
    /// 回傳值是本案例通過驗證的解法數量，範圍為 0 到 3。
    /// </summary>
    /// <param name="program">提供三種排序方法的 <see cref="Program"/> 執行個體。</param>
    /// <param name="caseName">顯示於主控台的案例名稱。</param>
    /// <param name="input">符合題目限制、尚未排序的輸入陣列。</param>
    /// <param name="expected">輸入陣列依 0、1、2 排列後的預期結果。</param>
    /// <returns>本案例中實際結果與預期結果相同的解法數量。</returns>
    private static int RunTestCase(Program program, string caseName, int[] input, int[] expected)
    {
        (string Name, Action<int[]> Sort)[] solutions =
        {
            (nameof(SortColors), program.SortColors),
            (nameof(SortColors2), program.SortColors2),
            (nameof(SortColors3), program.SortColors3)
        };

        Console.WriteLine($"案例：{caseName}");
        Console.WriteLine($"輸入：[{string.Join(", ", input)}]");
        Console.WriteLine($"預期：[{string.Join(", ", expected)}]");

        int passed = 0;
        foreach ((string name, Action<int[]> sort) in solutions)
        {
            int[] actual = (int[])input.Clone();
            sort(actual);

            bool isCorrect = actual.SequenceEqual(expected);
            if (isCorrect)
            {
                passed++;
            }

            Console.WriteLine(
                $"{name}: [{string.Join(", ", actual)}] => {(isCorrect ? "PASS" : "FAIL")}");
        }

        Console.WriteLine();
        return passed;
    }

    /// <summary>
    /// 使用氣泡排序將顏色陣列原地排列為 0、1、2。
    /// 每一輪比較相鄰元素並交換逆序配對，使尚未排序區間的最大值移到尾端；
    /// 若某輪沒有交換則提前結束。輸入陣列須至少包含一個元素，且只能包含 0、1、2。
    /// 執行完成後，原陣列會由小到大排列。最差時間複雜度為 O(n²)，額外空間為 O(1)。
    /// </summary>
    /// <param name="nums">待排序的整數陣列，僅包含 0、1、2</param>
    public void SortColors(int[] nums)
    {
        int n = nums.Length;
        for (int i = 0; i < n - 1; i++)
        {
            bool swap = false;

            for (int j = 0; j < n - i - 1; j++)
            {
                if (nums[j] > nums[j + 1])
                {
                    int temp = nums[j];
                    nums[j] = nums[j + 1];
                    nums[j + 1] = temp;
                    swap = true;
                }
            }

            // 本輪沒有逆序配對，代表整個陣列已經排序完成。
            if (!swap)
            {
                break;
            }
        }
    }

    /// <summary>
    /// 使用固定大小的計數陣列將顏色陣列原地排列為 0、1、2。
    /// 第一階段統計三種數值的出現次數，第二階段依計數覆寫原陣列。
    /// 輸入陣列須至少包含一個元素，且只能包含 0、1、2；
    /// 執行完成後，原陣列會由小到大排列。時間複雜度為 O(n)，額外空間為 O(1)。
    /// </summary>
    /// <param name="nums">待排序的整數陣列，僅包含 0、1、2。</param>
    public void SortColors2(int[] nums)
    {
        int[] count = new int[3];

        // 數值本身就是固定三格計數陣列的索引。
        for (int i = 0; i < nums.Length; i++)
        {
            count[nums[i]]++;
        }

        // 依照 0、1、2 的計數順序覆寫，直接得到排序結果。
        int index = 0;
        for (int i = 0; i < count.Length; i++)
        {
            for (int j = 0; j < count[i]; j++)
            {
                nums[index++] = i;
            }
        }
    }

    /// <summary>
    /// 使用 p0、p1 與目前索引進行一次掃描，將顏色陣列原地排列為 0、1、2。
    /// 掃描期間維持已處理區間依序分成 0、1、2 三段；遇到 0 時擴張前兩段，
    /// 遇到 1 時只擴張 1 的區段，遇到 2 時保留在後段。
    /// 輸入陣列須至少包含一個元素，且只能包含 0、1、2；
    /// 執行完成後，原陣列會由小到大排列。時間複雜度為 O(n)，額外空間為 O(1)。
    /// </summary>
    /// <param name="nums">待排序的整數陣列，僅包含 0、1、2。</param>
    public void SortColors3(int[] nums)
    {
        int n = nums.Length;
        int p0 = 0;
        int p1 = 0;

        for (int i = 0; i < n; i++)
        {
            // 進入本輪時：[0, p0) 是 0，[p0, p1) 是 1，[p1, i) 是 2。
            if (nums[i] == 0)
            {
                int temp = nums[i];
                nums[i] = nums[p0];
                nums[p0] = temp;

                // p0 後方已有 1 時，第一次交換會把它移到 i，須再交換到 p1。
                if (p0 < p1)
                {
                    temp = nums[i];
                    nums[i] = nums[p1];
                    nums[p1] = temp;
                }
                p0++;
                p1++;
            }
            else if (nums[i] == 1)
            {
                // 將 1 放到 p1 位置
                int temp = nums[i];
                nums[i] = nums[p1];
                nums[p1] = temp;
                p1++;
            }
        }
    }
}
