namespace leetcode_027;

class Program
{
    /// <summary>
    /// 27. Remove Element
    /// https://leetcode.com/problems/remove-element/description/
    /// 27. 移除元素
    /// https://leetcode.cn/problems/remove-element/description/
    ///
    /// English:
    /// Given an integer array nums and an integer val, remove all occurrences of val in nums in-place.
    /// The order of the elements may be changed. Then return the number of elements in nums which are
    /// not equal to val.
    ///
    /// Consider the number of elements in nums which are not equal to val be k, to get accepted,
    /// you need to do the following things:
    /// 1. Change the array nums such that the first k elements of nums contain the elements which are
    /// not equal to val. The remaining elements of nums are not important as well as the size of nums.
    /// 2. Return k.
    ///
    /// 繁體中文:
    /// 給定一個整數陣列 nums 與一個整數 val，請你原地移除 nums 中所有等於 val 的元素。
    /// 元素的順序可以改變。接著回傳 nums 中不等於 val 的元素數量。
    ///
    /// 設 nums 中不等於 val 的元素數量為 k，若要通過題目，你需要完成以下事項：
    /// 1. 調整陣列 nums，使前 k 個元素皆為不等於 val 的元素。其餘元素與陣列長度不重要。
    /// 2. 回傳 k。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        RunDemos();
    }

    /// <summary>
    /// 執行題目範例與邊界案例，示範如何呼叫 RemoveElement。
    /// 解題概念是以雙指標原地覆寫陣列，觀察前 k 個有效元素是否符合預期。
    /// 輸入條件為內部已定義的測試資料，不依賴命令列參數。
    /// 輸出結果為每個案例的原始陣列、目標值、回傳長度與處理後有效區段。
    /// </summary>
    private static void RunDemos()
    {
        Program solver = new Program();

        Console.WriteLine("LeetCode 27 - Remove Element");
        Console.WriteLine(new string('-', 50));

        RunDemoCase(solver, "題目範例 1", new[] { 3, 2, 2, 3 }, 3);
        RunDemoCase(solver, "題目範例 2", new[] { 0, 1, 2, 2, 3, 0, 4, 2 }, 2);
        RunDemoCase(solver, "邊界案例：空陣列", Array.Empty<int>(), 1);
        RunDemoCase(solver, "邊界案例：全部移除", new[] { 1, 1, 1, 1 }, 1);
        RunDemoCase(solver, "邊界案例：完全保留", new[] { 4, 5, 6 }, 3);
    }

    /// <summary>
    /// 執行單一測試案例並輸出觀察資訊，方便比對回傳的 k 與陣列前段內容。
    /// 解題概念是先複製輸入陣列，再呼叫 RemoveElement 進行原地覆寫，避免不同案例彼此污染。
    /// 輸入條件為非 null 的整數陣列、欲移除的目標值，以及僅供顯示的案例名稱。
    /// 輸出結果為主控台上的案例摘要，不額外回傳資料。
    /// </summary>
    private static void RunDemoCase(Program solver, string caseName, int[] nums, int val)
    {
        int[] workingNums = (int[])nums.Clone();
        int k = solver.RemoveElement(workingNums, val);

        Console.WriteLine($"[{caseName}]");
        Console.WriteLine($"輸入陣列: {FormatArray(nums)}, val = {val}");
        Console.WriteLine($"移除後長度 k = {k}");
        Console.WriteLine($"前 k 個有效元素: {FormatArray(workingNums[..k])}");
        Console.WriteLine($"處理後完整陣列: {FormatArray(workingNums)}");
        Console.WriteLine();
    }

    /// <summary>
    /// 將整數陣列格式化為易讀字串，供主控台輸出與 README 範例一致。
    /// 解題概念是將結果統一包成 [a, b, c] 形式，方便比對有效區段與原地覆寫後的內容。
    /// 輸入條件為任意長度的整數陣列，包含空陣列。
    /// 輸出結果為可直接顯示的字串表示法。
    /// </summary>
    private static string FormatArray(int[] nums)
    {
        return $"[{string.Join(", ", nums)}]";
    }

    /// <summary>
    /// 原地移除陣列中所有等於 val 的元素，並回傳保留下來的有效元素數量 k。
    /// 解題概念採用雙指標：right 掃描每個元素，left 維護下一個可寫入的有效位置，遇到非 val 元素就前移覆寫。
    /// 輸入條件為非 null 的整數陣列 nums 與要移除的目標值 val；題目允許改寫原陣列且不要求保留尾端內容。
    /// 輸出結果為前 k 個元素皆不等於 val 的新長度 k，nums 的其餘位置視為未定義區域。
    /// </summary>
    /// <param name="nums">要原地移除元素的整數陣列。</param>
    /// <param name="val">要從陣列中移除的目標值。</param>
    /// <returns>移除後保留下來的有效元素數量 k。</returns>
    public int RemoveElement(int[] nums, int val)
    {
        int n = nums.Length;
        if (n == 0)
        {
            return 0;
        }

        int left = 0;
        for (int right = 0; right < n; right++)
        {
            // left 永遠指向下一個可寫入非 val 元素的位置。
            // 只保留不等於 val 的元素，並把有效元素壓縮到陣列前段。
            if (nums[right] != val)
            {
                nums[left] = nums[right];
                left++;
            }
        }

        return left;
    }
}
