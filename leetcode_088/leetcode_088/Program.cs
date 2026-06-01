namespace leetcode_088;

class Program
{
    /// <summary>
    /// 88. Merge Sorted Array
    /// https://leetcode.com/problems/merge-sorted-array/description/
    /// 88. 合并两个有序数组
    /// https://leetcode.cn/problems/merge-sorted-array/description/
    /// 
    /// English:
    /// You are given two integer arrays nums1 and nums2, sorted in non-decreasing order, and two integers
    /// m and n, representing the number of elements in nums1 and nums2 respectively. Merge nums1 and nums2
    /// into a single array sorted in non-decreasing order. The final sorted array should not be returned by
    /// the function, but instead be stored inside the array nums1. To accommodate this, nums1 has a length
    /// of m + n, where the first m elements denote the elements that should be merged, and the last n
    /// elements are set to 0 and should be ignored. nums2 has a length of n.
    /// 
    /// 繁體中文:
    /// 給定兩個以非遞減順序排序的整數陣列 nums1 和 nums2，以及兩個整數 m 和 n，分別代表 nums1 和
    /// nums2 中有效元素的數量。請將 nums1 和 nums2 合併成一個以非遞減順序排序的陣列。最終排序後的
    /// 陣列不應由函式回傳，而是必須儲存在 nums1 內。為了容納合併後的結果，nums1 的長度為 m + n，
    /// 其中前 m 個元素代表需要合併的元素，最後 n 個元素設為 0 且應忽略。nums2 的長度為 n。
    /// </summary>
    /// <remarks>
    /// 主要進入點會執行多組範例資料，驗證三種解法是否都能將 nums2 原地合併到 nums1 並輸出 PASS/FAIL。
    /// </remarks>
    /// <param name="args">Command-line arguments.</param>
    static void Main(string[] args)
    {
        Program solution = new Program();

        RunSolutionCases("Merge", solution.Merge);
        RunSolutionCases("Merge2", solution.Merge2);
        RunSolutionCases("Merge3", solution.Merge3);

        Console.WriteLine("All examples passed.");
    }

    /// <summary>
    /// 使用同一批案例驗證指定解法，確保不同合併策略在一般情境、空 nums2、空有效 nums1、
    /// 以及 nums2 需要插入前方時都能得到相同輸出。
    /// 輸入為解法名稱與合併委派；輸出為各案例的 PASS/FAIL 主控台訊息。
    /// </summary>
    /// <param name="solutionName">要顯示在輸出中的解法名稱。</param>
    /// <param name="mergeAction">要驗證的合併方法。</param>
    static void RunSolutionCases(string solutionName, Action<int[], int, int[], int> mergeAction)
    {
        RunCase($"{solutionName} - Example 1", mergeAction, [1, 2, 3, 0, 0, 0], 3, [2, 5, 6], 3, [1, 2, 2, 3, 5, 6]);
        RunCase($"{solutionName} - Example 2", mergeAction, [1], 1, [], 0, [1]);
        RunCase($"{solutionName} - Example 3", mergeAction, [0], 0, [1], 1, [1]);
        RunCase($"{solutionName} - Left tail move", mergeAction, [2, 0], 1, [1], 1, [1, 2]);
    }

    /// <summary>
    /// 執行單一合併案例，將 nums1 複製後交給指定解法，並比對合併結果是否符合預期。
    /// 解題概念是讓 Main 可以用相同輸入條件驗證不同合併策略；輸入需符合 nums1.Length == m + n、
    /// nums2.Length == n，輸出結果會寫到主控台，若結果錯誤則擲出例外中止執行。
    /// </summary>
    /// <param name="name">案例名稱。</param>
    /// <param name="mergeAction">要驗證的合併方法。</param>
    /// <param name="nums1">包含 m 個有效元素與 n 個預留空間的第一個非遞減陣列。</param>
    /// <param name="m">nums1 中需要參與合併的有效元素數量。</param>
    /// <param name="nums2">包含 n 個有效元素的第二個非遞減陣列。</param>
    /// <param name="n">nums2 中需要參與合併的有效元素數量。</param>
    /// <param name="expected">預期的合併後非遞減陣列。</param>
    static void RunCase(
        string name,
        Action<int[], int, int[], int> mergeAction,
        int[] nums1,
        int m,
        int[] nums2,
        int n,
        int[] expected)
    {
        int[] actual = (int[])nums1.Clone();

        mergeAction(actual, m, nums2, n);

        bool passed = actual.SequenceEqual(expected);
        Console.WriteLine($"{name}: {(passed ? "PASS" : "FAIL")} -> [{FormatArray(actual)}]");

        if (!passed)
        {
            throw new InvalidOperationException($"{name} expected [{FormatArray(expected)}], got [{FormatArray(actual)}].");
        }
    }

    /// <summary>
    /// 將整數陣列格式化為逗號分隔字串，供範例輸出與錯誤訊息使用。
    /// 輸入可為空陣列；輸出為不含中括號的元素清單字串。
    /// </summary>
    /// <param name="values">要格式化的整數陣列。</param>
    /// <returns>以逗號與空白分隔的陣列內容。</returns>
    static string FormatArray(int[] values)
    {
        return string.Join(", ", values);
    }

    /// <summary>
    /// 方法一：逆向雙指針。
    /// 用途是將 nums2 原地合併到 nums1；解題概念是從兩個有效區間尾端比較較大值，
    /// 放入 nums1 最尾端的預留位置，避免覆蓋 nums1 尚未比較的有效元素。
    /// 輸入需符合 nums1.Length == m + n、nums2.Length == n，且兩陣列有效區間皆為非遞減排序。
    /// 方法不回傳值，輸出結果直接寫回 nums1。
    /// </summary>
    /// <param name="nums1">第一個非遞減陣列，前 m 個元素有效，後 n 個位置用來存放合併結果。</param>
    /// <param name="m">nums1 中有效元素的數量。</param>
    /// <param name="nums2">第二個非遞減陣列，長度為 n。</param>
    /// <param name="n">nums2 中有效元素的數量。</param>
    public void Merge(int[] nums1, int m, int[] nums2, int n)
    {
        int p1 = m - 1;
        int p2 = n - 1;
        int tail = m + n - 1;
        int cur;

        // 從尾端放入目前最大值，才能使用 nums1 的預留空間而不需要額外暫存陣列。
        while (p1 >= 0 || p2 >= 0)
        {
            if (p1 == -1)
            {
                cur = nums2[p2--];
            }
            else if (p2 == -1)
            {
                cur = nums1[p1--];
            }
            else if (nums1[p1] > nums2[p2])
            {
                cur = nums1[p1--];
            }
            else
            {
                cur = nums2[p2--];
            }

            nums1[tail--] = cur;
        }
    }

    /// <summary>
    /// 方法二：正向雙指針。
    /// 用途是將 nums2 合併到 nums1；解題概念是從兩個有效區間開頭取較小值放入暫存陣列，
    /// 全部合併完成後再複製回 nums1。
    /// 輸入需符合 nums1.Length == m + n、nums2.Length == n，且兩陣列有效區間皆為非遞減排序。
    /// 方法不回傳值，輸出結果直接寫回 nums1。
    /// </summary>
    /// <param name="nums1">第一個非遞減陣列，前 m 個元素有效，後 n 個位置用來存放合併結果。</param>
    /// <param name="m">nums1 中有效元素的數量。</param>
    /// <param name="nums2">第二個非遞減陣列，長度為 n。</param>
    /// <param name="n">nums2 中有效元素的數量。</param>
    public void Merge2(int[] nums1, int m, int[] nums2, int n)
    {
        int p1 = 0;
        int p2 = 0;
        int[] sorted = new int[m + n];
        int cur;

        // 正向合併會覆蓋 nums1 的原始有效元素，因此先寫到暫存陣列再複製回 nums1。
        while (p1 < m || p2 < n)
        {
            if (p1 == m)
            {
                cur = nums2[p2++];
            }
            else if (p2 == n)
            {
                cur = nums1[p1++];
            }
            else if (nums1[p1] <= nums2[p2])
            {
                cur = nums1[p1++];
            }
            else
            {
                cur = nums2[p2++];
            }

            sorted[p1 + p2 - 1] = cur;
        }

        for (int i = 0; i < m + n; i++)
        {
            nums1[i] = sorted[i];
        }
    }

    /// <summary>
    /// 方法三：直接合併後排序。
    /// 用途是將 nums2 追加到 nums1 的預留位置後排序整個 nums1；解題概念最直覺，
    /// 先讓 nums1 包含所有元素，再交給標準排序完成非遞減排列。
    /// 輸入需符合 nums1.Length == m + n、nums2.Length == n，且兩陣列有效區間皆為非遞減排序。
    /// 方法不回傳值，輸出結果直接寫回 nums1。
    /// </summary>
    /// <param name="nums1">第一個非遞減陣列，前 m 個元素有效，後 n 個位置用來存放合併結果。</param>
    /// <param name="m">nums1 中有效元素的數量。</param>
    /// <param name="nums2">第二個非遞減陣列，長度為 n。</param>
    /// <param name="n">nums2 中有效元素的數量。</param>
    public void Merge3(int[] nums1, int m, int[] nums2, int n)
    {
        for (int i = 0; i < n; i++)
        {
            nums1[m + i] = nums2[i];
        }

        // 追加後直接排序，犧牲排序成本換取最簡單的實作。
        Array.Sort(nums1);
    }
}
