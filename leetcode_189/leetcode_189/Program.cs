namespace leetcode_189;

class Program
{
    private sealed record SampleCase(string Name, int[] Input, int K, int[] Expected);

    /// <summary>
    /// 189. Rotate Array
    /// https://leetcode.com/problems/rotate-array/description/?envType=study-plan-v2&envId=top-interview-150
    /// 189. 轮转数组
    /// https://leetcode.cn/problems/rotate-array/description/
    ///
    /// Problem (English):
    /// Given an integer array nums, rotate the array to the right by k steps, where k is non-negative.
    ///
    /// 題目描述（繁體中文）:
    /// 給定一個整數陣列 nums，將陣列向右輪轉 k 個步驟，其中 k 為非負整數。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program program = new Program();
        program.RunSamples();
    }

    /// <summary>
    /// 執行固定測資，驗證翻轉法與額外陣列法都能正確完成陣列右旋。
    /// 輸入案例皆符合題目條件，輸出為主控台報表，列出預期結果、實際結果與通過狀態。
    /// </summary>
    private void RunSamples()
    {
        SampleCase[] sampleCases =
        [
            new SampleCase("Basic rotation", [1, 2, 3, 4, 5, 6, 7], 3, [5, 6, 7, 1, 2, 3, 4]),
            new SampleCase("Mixed signs", [-1, -100, 3, 99], 2, [3, 99, -1, -100]),
            new SampleCase("Zero rotation", [1, 2, 3, 4], 0, [1, 2, 3, 4]),
            new SampleCase("K larger than length", [10, 20, 30, 40, 50], 7, [40, 50, 10, 20, 30]),
            new SampleCase("Single element", [42], 15, [42])
        ];

        int passedCases = 0;
        int passedChecks = 0;

        Console.WriteLine("LeetCode 189 - Rotate Array Samples");
        Console.WriteLine("===================================");

        for (int i = 0; i < sampleCases.Length; i++)
        {
            int samplePassedChecks = ExecuteSample(i + 1, sampleCases[i]);
            passedChecks += samplePassedChecks;

            if (samplePassedChecks == 2)
            {
                passedCases++;
            }
        }

        Console.WriteLine($"Summary: {passedCases}/{sampleCases.Length} sample cases passed.");
        Console.WriteLine($"Checks:  {passedChecks}/{sampleCases.Length * 2} solution checks passed.");
    }

    /// <summary>
    /// 使用三次反轉在原陣列中完成右旋，符合題目要求的 in-place 解法。
    /// 輸入必須是非空整數陣列與非負整數 k；輸出會直接修改 nums，使其成為右移 k 步後的結果。
    /// 時間複雜度為 O(n)，額外空間複雜度為 O(1)。
    /// </summary>
    /// <param name="nums">要被原地右旋的整數陣列。</param>
    /// <param name="k">向右輪轉的步數，允許大於陣列長度。</param>
    public void Rotate(int[] nums, int k)
    {
        if (nums.Length <= 1)
        {
            return;
        }

        // 先將位移量正規化到陣列長度內，右移 n 的倍數次等同不移動。
        int distance = k % nums.Length;
        if (distance == 0)
        {
            return;
        }

        // 全陣列反轉後，原本尾端要移到前面的元素會先集中到前段。
        ReverseRange(nums, 0, nums.Length - 1);

        // 再把前 distance 個元素反轉回原本相對順序，得到正確的新前段。
        ReverseRange(nums, 0, distance - 1);

        // 最後把剩餘元素反轉回原本相對順序，完成整體右旋。
        ReverseRange(nums, distance, nums.Length - 1);
    }

    /// <summary>
    /// 就地反轉陣列中指定閉區間的元素，作為翻轉法的基礎操作。
    /// 輸入需保證 start 與 end 都位於陣列範圍內，且 start 不大於 end；輸出會直接修改 nums 指定區間的順序。
    /// </summary>
    /// <param name="nums">要被修改的整數陣列。</param>
    /// <param name="start">反轉區間的起始索引。</param>
    /// <param name="end">反轉區間的結束索引。</param>
    private void ReverseRange(int[] nums, int start, int end)
    {
        while (start < end)
        {
            int temp = nums[start];
            nums[start] = nums[end];
            nums[end] = temp;

            start++;
            end--;
        }
    }

    /// <summary>
    /// 使用額外陣列記錄每個元素右移後的新位置，再將結果複製回原陣列。
    /// 輸入必須是非空整數陣列與非負整數 k；輸出會直接修改 nums，使其成為右移 k 步後的結果。
    /// 時間複雜度為 O(n)，額外空間複雜度為 O(n)。
    /// </summary>
    /// <param name="nums">要被原地覆寫成右旋結果的整數陣列。</param>
    /// <param name="k">向右輪轉的步數，允許大於陣列長度。</param>
    public void Rotate2(int[] nums, int k)
    {
        if (nums.Length <= 1)
        {
            return;
        }

        int n = nums.Length;
        int distance = k % n;
        if (distance == 0)
        {
            return;
        }

        int[] newArr = new int[n];
        for (int i = 0; i < n; i++)
        {
            // 元素從索引 i 右移 distance 步後，會落到 (i + distance) % n 的位置。
            newArr[(i + distance) % n] = nums[i];
        }

        Array.Copy(newArr, 0, nums, 0, n);
    }

    /// <summary>
    /// 執行單一測資，列出兩種解法的實際輸出與驗證結果。
    /// 輸入需提供題目合法案例與預期答案；輸出為通過的解法數量，用於最後統計。
    /// </summary>
    /// <param name="caseNumber">顯示於報表中的測資編號。</param>
    /// <param name="sampleCase">包含輸入、位移量與預期答案的測資資料。</param>
    /// <returns>本測資中通過驗證的解法數量。</returns>
    private int ExecuteSample(int caseNumber, SampleCase sampleCase)
    {
        // 兩種解法都會原地修改陣列，因此必須各自使用輸入副本避免互相干擾。
        int[] rotateInput = (int[])sampleCase.Input.Clone();
        int[] rotate2Input = (int[])sampleCase.Input.Clone();

        Rotate(rotateInput, sampleCase.K);
        Rotate2(rotate2Input, sampleCase.K);

        bool rotatePassed = ArraysEqual(rotateInput, sampleCase.Expected);
        bool rotate2Passed = ArraysEqual(rotate2Input, sampleCase.Expected);

        Console.WriteLine();
        Console.WriteLine($"Case {caseNumber}: {sampleCase.Name}");
        Console.WriteLine($"Input:    {FormatArray(sampleCase.Input)}");
        Console.WriteLine($"k:        {sampleCase.K}");
        Console.WriteLine($"Expected: {FormatArray(sampleCase.Expected)}");
        Console.WriteLine($"Rotate:   {FormatArray(rotateInput)} ({(rotatePassed ? "PASS" : "FAIL")})");
        Console.WriteLine($"Rotate2:  {FormatArray(rotate2Input)} ({(rotate2Passed ? "PASS" : "FAIL")})");

        int passedChecks = 0;
        if (rotatePassed)
        {
            passedChecks++;
        }

        if (rotate2Passed)
        {
            passedChecks++;
        }

        return passedChecks;
    }

    /// <summary>
    /// 將整數陣列格式化成易於閱讀的字串，供範例輸出與 README 對照。
    /// 輸入為任意整數陣列；輸出為以中括號包裹、逗號分隔的字串表示法。
    /// </summary>
    /// <param name="nums">要格式化的整數陣列。</param>
    /// <returns>例如 [1, 2, 3] 的字串表示。</returns>
    private string FormatArray(int[] nums)
    {
        return $"[{string.Join(", ", nums)}]";
    }

    /// <summary>
    /// 比較兩個整數陣列的長度與每個位置的值是否完全一致。
    /// 輸入為兩個已建立的整數陣列；輸出為布林值，true 代表內容完全相同。
    /// </summary>
    /// <param name="left">第一個待比較的整數陣列。</param>
    /// <param name="right">第二個待比較的整數陣列。</param>
    /// <returns>若兩個陣列完全一致則回傳 true，否則回傳 false。</returns>
    private bool ArraysEqual(int[] left, int[] right)
    {
        if (left.Length != right.Length)
        {
            return false;
        }

        for (int i = 0; i < left.Length; i++)
        {
            if (left[i] != right[i])
            {
                return false;
            }
        }

        return true;
    }
}
