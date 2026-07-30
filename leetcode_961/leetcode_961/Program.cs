namespace leetcode_961;

/// <summary>
/// 提供 LeetCode 961「在長度 2N 的陣列中找出重複 N 次的元素」的三種解法，
/// 並透過主程式執行固定案例，對照每種解法的預期結果與輸入保留狀態。
/// </summary>
class Program
{
    /// <summary>
    /// 程式進入點。
    /// 不需要命令列參數；會執行五組合法測資，分別驗證三種解法的回傳值，
    /// 並確認各方法不會修改輸入陣列。
    /// </summary>
    /// <param name="args">未使用的命令列參數。</param>
    private static void Main(string[] args)
    {
        RunSamples();
    }

    /// <summary>
    /// 解法一：使用雜湊集合記錄已出現的數字。
    /// 輸入必須符合題目條件：陣列長度為 2N、其中一個值出現 N 次，其餘 N 個值各出現一次。
    /// 第一個無法加入集合的數字就是重複 N 次的元素；方法不會修改輸入，找不到時回傳 -1。
    /// 時間複雜度為 O(N)，額外空間複雜度為 O(N)。
    /// </summary>
    /// <param name="nums">符合題目條件的整數陣列。</param>
    /// <returns>重複 N 次的元素；若輸入不符合題目保證則回傳 -1。</returns>
    public int RepeatedNTimes(int[] nums)
    {
        HashSet<int> seen = new HashSet<int>();

        foreach (int num in nums)
        {
            // HashSet.Add 在元素已存在時回傳 false，因此第二次遇到重複值即可結束。
            if (!seen.Add(num))
            {
                return num;
            }
        }

        return -1;
    }

    /// <summary>
    /// 解法二：利用 0 到 10000 的數值限制，以固定大小的頻率陣列記錄出現次數。
    /// 輸入必須符合題目條件，且每個元素位於 0 到 10000；第二次出現的值就是答案。
    /// 方法不會修改輸入，找不到時回傳 -1。
    /// 時間複雜度為 O(N)，額外空間為 O(U)，其中 U 固定為 10001。
    /// </summary>
    /// <param name="nums">符合題目條件且元素位於 0 到 10000 的整數陣列。</param>
    /// <returns>重複 N 次的元素；若輸入不符合題目保證則回傳 -1。</returns>
    public int RepeatedNTimes2(int[] nums)
    {
        int[] frequencies = new int[10001];

        foreach (int num in nums)
        {
            frequencies[num]++;

            // 其餘元素只出現一次，所以任何計數到達 2 的值必定是目標。
            if (frequencies[num] == 2)
            {
                return num;
            }
        }

        return -1;
    }

    /// <summary>
    /// 解法三：利用重複元素佔陣列一半的性質，檢查相距 1、2、3 的元素。
    /// 對符合題目條件且 N 至少為 2 的輸入，重複值必有兩次出現的距離不超過 3；
    /// 找到相等的一對即回傳該值。方法不會修改輸入，找不到時回傳 -1。
    /// 時間複雜度為 O(N)，額外空間複雜度為 O(1)。
    /// </summary>
    /// <param name="nums">符合題目條件的整數陣列。</param>
    /// <returns>重複 N 次的元素；若輸入不符合題目保證則回傳 -1。</returns>
    public int RepeatedNTimes3(int[] nums)
    {
        // 若每兩次重複值都至少相距 4，N 個重複值至少需要 4N - 3 個位置，
        // 但陣列只有 2N 個位置（N >= 2），因此必有一對的距離介於 1 到 3。
        for (int distance = 1; distance <= 3; distance++)
        {
            for (int i = distance; i < nums.Length; i++)
            {
                if (nums[i] == nums[i - distance])
                {
                    return nums[i];
                }
            }
        }

        return -1;
    }

    /// <summary>
    /// 建立五組固定案例並依序執行三種解法。
    /// 每個解法都使用獨立的輸入副本；輸出各案例的預期值、實際值、輸入是否保留與 PASS/FAIL，
    /// 最後輸出通過數，正常情況應為 15/15。
    /// </summary>
    private static void RunSamples()
    {
        SampleCase[] cases =
        [
            new SampleCase([1, 2, 3, 3], 3),
            new SampleCase([2, 1, 2, 5, 3, 2], 2),
            new SampleCase([5, 1, 5, 2, 5, 3, 5, 4], 5),
            new SampleCase([0, 1, 2, 0], 0),
            new SampleCase([10000, 1, 10000, 2], 10000)
        ];

        Program solver = new Program();
        (string Name, Func<int[], int> Solve)[] solutions =
        [
            ("解法一（HashSet）", solver.RepeatedNTimes),
            ("解法二（頻率陣列）", solver.RepeatedNTimes2),
            ("解法三（鄰距檢查）", solver.RepeatedNTimes3)
        ];

        int passedChecks = 0;
        int totalChecks = cases.Length * solutions.Length;

        Console.WriteLine("LeetCode 961：在長度 2N 的陣列中找出重複 N 次的元素");
        Console.WriteLine();

        for (int i = 0; i < cases.Length; i++)
        {
            Console.WriteLine($"案例 {i + 1}");
            Console.WriteLine($"輸入：{FormatArray(cases[i].Nums)}");
            Console.WriteLine($"預期：{cases[i].Expected}");

            foreach ((string name, Func<int[], int> solve) in solutions)
            {
                if (RunSolution(name, solve, cases[i]))
                {
                    passedChecks++;
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
    }

    /// <summary>
    /// 對單一案例執行指定解法。
    /// 輸入為解法名稱、接受整數陣列並回傳答案的函式，以及合法的測試案例；
    /// 方法會用獨立副本呼叫解法，同時驗證答案與輸入保留狀態，輸出結果並回傳是否通過。
    /// </summary>
    /// <param name="name">顯示於主控台的解法名稱。</param>
    /// <param name="solve">接受測試陣列並回傳重複元素的解法。</param>
    /// <param name="sample">包含原始輸入與預期答案的測試案例。</param>
    /// <returns>答案正確且輸入未被修改時回傳 true，否則回傳 false。</returns>
    private static bool RunSolution(
        string name,
        Func<int[], int> solve,
        SampleCase sample)
    {
        int[] input = [.. sample.Nums];
        int actual = solve(input);
        bool inputPreserved = input.SequenceEqual(sample.Nums);
        bool passed = actual == sample.Expected && inputPreserved;

        Console.WriteLine(
            $"  {name}：實際={actual}，輸入保留={(inputPreserved ? "是" : "否")} => {(passed ? "PASS" : "FAIL")}");

        return passed;
    }

    /// <summary>
    /// 將整數陣列轉換為便於閱讀的方括號格式。
    /// 輸入可為空陣列；輸出格式例如 <c>[1, 2, 3]</c>，且不會修改原陣列。
    /// </summary>
    /// <param name="nums">要格式化的整數陣列。</param>
    /// <returns>以逗號與空格分隔、外加方括號的字串。</returns>
    private static string FormatArray(int[] nums)
    {
        return $"[{string.Join(", ", nums)}]";
    }

    /// <summary>
    /// 表示一筆固定測試案例，包含符合題目條件的輸入陣列與預期重複元素。
    /// </summary>
    /// <param name="Nums">符合題目條件的整數陣列。</param>
    /// <param name="Expected">預期重複 N 次的元素。</param>
    private sealed record SampleCase(int[] Nums, int Expected);
}
