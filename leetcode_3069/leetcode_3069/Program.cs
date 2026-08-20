namespace leetcode_3069;

class Program
{
    /// <summary>
    /// 3069. Distribute Elements Into Two Arrays I
    /// https://leetcode.com/problems/distribute-elements-into-two-arrays-i/description
    /// 3069. 將元素分配到兩個陣列中 I
    /// https://leetcode.cn/problems/distribute-elements-into-two-arrays-i/description
    ///
    /// English:
    /// You are given a 1-indexed array of distinct integers nums of length n.
    ///
    /// You need to distribute all the elements of nums between two arrays arr1 and arr2 using n operations.
    /// In the first operation, append nums[1] to arr1. In the second operation, append nums[2] to arr2.
    /// Afterwards, in the ith operation:
    ///
    /// - If the last element of arr1 is greater than the last element of arr2, append nums[i] to arr1.
    /// - Otherwise, append nums[i] to arr2.
    ///
    /// The array result is formed by concatenating the arrays arr1 and arr2. For example, if
    /// arr1 == [1,2,3] and arr2 == [4,5,6], then result = [1,2,3,4,5,6].
    ///
    /// Return the array result.
    ///
    /// 繁體中文：
    /// 給定一個長度為 n、從 1 開始索引，且由互不相同整數組成的陣列 nums。
    ///
    /// 你需要使用 n 次操作，將 nums 中的所有元素分配至兩個陣列 arr1 與 arr2。
    /// 在第一次操作中，將 nums[1] 加入 arr1 的末尾。在第二次操作中，將 nums[2] 加入 arr2 的末尾。
    /// 之後，在第 i 次操作中：
    ///
    /// - 若 arr1 的最後一個元素大於 arr2 的最後一個元素，則將 nums[i] 加入 arr1 的末尾。
    /// - 否則，將 nums[i] 加入 arr2 的末尾。
    ///
    /// 將 arr1 與 arr2 串接後形成陣列 result。例如，若 arr1 == [1,2,3] 且 arr2 == [4,5,6]，
    /// 則 result = [1,2,3,4,5,6]。
    ///
    /// 回傳陣列 result。
    /// </summary>
    /// <remarks>
    /// 建立固定測試資料，依序執行兩種解法並輸出 Input、Expected、Actual 與 PASS/FAIL；
    /// 若任一驗證失敗，程式會以非零結束碼結束。
    /// </remarks>
    /// <param name="args">命令列參數（未使用）。</param>
    static void Main(string[] args)
    {
        Program solver = new Program();
        (string Name, int[] Input, int[] Expected)[] testCases = new[]
        {
            ("官方範例 1", new int[] { 2, 1, 3 }, new int[] { 2, 3, 1 }),
            ("官方範例 2", new int[] { 5, 4, 3, 8 }, new int[] { 5, 3, 4, 8 }),
            ("最小長度", new int[] { 1, 2 }, new int[] { 1, 2 }),
            ("多次切換分配方向", new int[] { 10, 20, 30, 5, 6 }, new int[] { 10, 6, 20, 30, 5 }),
            ("arr2 連續追加與反轉順序", new int[] { 1, 100, 90, 80, 70 }, new int[] { 1, 100, 90, 80, 70 })
        };

        int passedChecks = 0;
        foreach ((string Name, int[] Input, int[] Expected) testCase in testCases)
        {
            passedChecks += RunCase(solver, testCase.Name, testCase.Input, testCase.Expected);
        }

        int totalChecks = testCases.Length * 2;
        Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
        Environment.ExitCode = passedChecks == totalChecks ? 0 : 1;
    }

    /// <summary>
    /// 將一組符合題目限制的輸入分別交給兩種解法，逐一比對預期陣列並輸出驗證結果。
    /// 每種解法都取得獨立的輸入副本，回傳值為本案例通過的解法數量。
    /// </summary>
    /// <param name="solver">提供兩種解法的 <see cref="Program"/> 實例。</param>
    /// <param name="caseName">顯示於驗證輸出的案例名稱。</param>
    /// <param name="input">長度至少為 2 且元素互不相同的輸入陣列。</param>
    /// <param name="expected">兩種解法都應回傳的預期陣列。</param>
    /// <returns>本案例通過的檢查數，範圍為 0 至 2。</returns>
    private static int RunCase(Program solver, string caseName, int[] input, int[] expected)
    {
        int[] actual1 = solver.ResultArray(input.ToArray());
        int[] actual2 = solver.ResultArray2(input.ToArray());

        bool method1Passed = actual1.SequenceEqual(expected);
        bool method2Passed = actual2.SequenceEqual(expected);
        int passedChecks = (method1Passed ? 1 : 0) + (method2Passed ? 1 : 0);
        string status = passedChecks == 2 ? "PASS" : "FAIL";

        Console.WriteLine(
            $"[{caseName}] Input: {FormatArray(input)} | Expected: {FormatArray(expected)} | " +
            $"Actual: M1={FormatArray(actual1)}, M2={FormatArray(actual2)} | {status}");

        return passedChecks;
    }

    /// <summary>
    /// 將整數陣列格式化為易於比對的方括號字串；輸入可為空陣列，輸出不改變原陣列。
    /// </summary>
    /// <param name="values">要顯示的整數陣列。</param>
    /// <returns>以逗號與空格分隔元素的字串，例如 <c>[2, 3, 1]</c>。</returns>
    private static string FormatArray(int[] values)
    {
        return $"[{string.Join(", ", values)}]";
    }

    /// <summary>
    /// 使用兩個 List 直接模擬題目的 arr1 與 arr2 分配規則，再依序串接兩個陣列。
    /// 輸入必須包含至少兩個互不相同的整數；方法不修改輸入，並回傳完成分配後的新陣列。
    /// </summary>
    /// <param name="nums">長度為 2 至 50、元素值為 1 至 100 且互不相同的整數陣列。</param>
    /// <returns>先放置 arr1、再放置 arr2 的分配結果。</returns>
    public int[] ResultArray(int[] nums)
    {
        int length = nums.Length;
        List<int> firstArray = new List<int>();
        List<int> secondArray = new List<int>();

        // 前兩個元素分別固定成為 arr1 與 arr2 的起點。
        firstArray.Add(nums[0]);
        secondArray.Add(nums[1]);

        for (int i = 2; i < length; i++)
        {
            // 後續元素只需比較兩個陣列目前的尾端，完全依照題目規則追加。
            if (firstArray[firstArray.Count - 1] > secondArray[secondArray.Count - 1])
            {
                firstArray.Add(nums[i]);
            }
            else
            {
                secondArray.Add(nums[i]);
            }
        }

        firstArray.AddRange(secondArray);
        return firstArray.ToArray();
    }

    /// <summary>
    /// 使用單一結果陣列與雙指標模擬分配：arr1 從左向右寫入，arr2 從右向左寫入，
    /// 最後反轉 arr2 區段以恢復追加順序。輸入必須包含至少兩個互不相同的整數；
    /// 方法不修改輸入，並回傳完成分配後的新陣列。
    /// </summary>
    /// <param name="nums">長度為 2 至 50、元素值為 1 至 100 且互不相同的整數陣列。</param>
    /// <returns>先放置 arr1、再放置 arr2 的分配結果。</returns>
    public int[] ResultArray2(int[] nums)
    {
        int length = nums.Length;
        int[] result = new int[length];

        // 左區段保存正向的 arr1；右區段暫時反向保存 arr2。
        result[0] = nums[0];
        result[length - 1] = nums[1];
        int firstTailIndex = 0;
        int secondTailIndex = length - 1;

        for (int i = 2; i < length; i++)
        {
            if (result[firstTailIndex] > result[secondTailIndex])
            {
                result[++firstTailIndex] = nums[i];
            }
            else
            {
                result[--secondTailIndex] = nums[i];
            }
        }

        // arr2 是由右向左寫入，反轉該區段後即可接在 arr1 後方。
        Array.Reverse(result, secondTailIndex, length - secondTailIndex);
        return result;
    }
}