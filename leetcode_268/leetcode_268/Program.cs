namespace leetcode_268;

class Program
{
    /// <summary>
    /// 268. Missing Number
    /// https://leetcode.com/problems/missing-number/description/
    /// 268. 丢失的数字
    /// https://leetcode.cn/problems/missing-number/description/
    /// Given an array nums containing n distinct numbers in the range [0, n],
    /// return the only number in the range that is missing from the array.
    ///
    /// 給定一個陣列 nums，其中包含範圍 [0, n] 內 n 個互不相同的數字，
    /// 請回傳此範圍中唯一缺少的那個數字。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solver = new Program();

        Console.WriteLine("LeetCode 268 - Missing Number");
        Console.WriteLine("================================");
        Console.WriteLine();

        RunSampleCase(solver, 1, new int[] { 3, 0, 1 }, 2);
        RunSampleCase(solver, 2, new int[] { 0, 1 }, 2);
        RunSampleCase(solver, 3, new int[] { 9, 6, 4, 2, 3, 5, 7, 0, 1 }, 8);
        RunSampleCase(solver, 4, new int[] { 0 }, 1);
        RunSampleCase(solver, 5, new int[] { 1 }, 0);
    }

    /// <summary>
    /// 使用排序解法找出缺少的數字。
    /// 先將輸入陣列原地排序，再從索引 0 開始逐一比對「索引值」與「排序後的數字」，
    /// 第一個不相等的位置就是缺少的數字。輸入必須是範圍 [0, n] 內互不重複的 n 個整數，
    /// 輸出為唯一缺失的那個值。時間複雜度為 O(n log n)，空間複雜度依排序實作而定。
    /// </summary>
    /// <param name="nums">包含 n 個互不重複整數的輸入陣列，數值範圍為 [0, n]。</param>
    /// <returns>範圍 [0, n] 中唯一沒有出現在陣列裡的數字。</returns>
    public int MissingNumber(int[] nums)
    {
        Array.Sort(nums);

        // 排序後，第一個「數值 != 索引」的位置，就是缺少的數字。
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] != i)
            {
                return i;
            }
        }

        // 如果 0 到 n - 1 都對得上，代表缺少的是尾端的 n。
        return nums.Length;
    }

    /// <summary>
    /// 使用 HashSet 解法找出缺少的數字。
    /// 先把輸入陣列中的所有值放入雜湊集合，再從 0 掃描到 n，
    /// 第一個不在集合中的值就是答案。輸入必須是範圍 [0, n] 內互不重複的 n 個整數，
    /// 輸出為唯一缺失的那個值。時間複雜度為 O(n)，空間複雜度為 O(n)。
    /// </summary>
    /// <param name="nums">包含 n 個互不重複整數的輸入陣列，數值範圍為 [0, n]。</param>
    /// <returns>範圍 [0, n] 中唯一沒有出現在陣列裡的數字。</returns>
    public int MissingNumber2(int[] nums)
    {
        ISet<int> numSet = new HashSet<int>(nums);
        int n = nums.Length;
        int missingNumber = -1;

        // 依序檢查完整區間 [0, n]，哪個數字不在集合裡，哪個就是缺失值。
        for (int i = 0; i <= n; i++)
        {
            if (!numSet.Contains(i))
            {
                missingNumber = i;
                break;
            }
        }

        return missingNumber;
    }

    /// <summary>
    /// 使用位元 XOR 解法找出缺少的數字。
    /// 將陣列中的所有值與完整區間 [0, n] 的所有整數全部做 XOR，
    /// 成對出現的數字會互相抵消，最後留下來的值就是缺少的數字。輸入必須是範圍 [0, n] 內互不重複的 n 個整數，
    /// 輸出為唯一缺失的那個值。時間複雜度為 O(n)，空間複雜度為 O(1)。
    /// </summary>
    /// <param name="nums">包含 n 個互不重複整數的輸入陣列，數值範圍為 [0, n]。</param>
    /// <returns>範圍 [0, n] 中唯一沒有出現在陣列裡的數字。</returns>
    public int MissingNumber3(int[] nums)
    {
        int xor = 0;
        int n = nums.Length;

        for (int i = 0; i < n; i++)
        {
            xor ^= nums[i];
        }

        // 陣列值與完整範圍值全部 XOR 後，重複出現的數字會兩兩抵消。
        for (int i = 0; i <= n; i++)
        {
            xor ^= i;
        }

        return xor;
    }

    /// <summary>
    /// 執行單筆範例資料，輸出三種解法的結果，並檢查是否符合預期答案。
    /// </summary>
    /// <param name="solver">提供三種解法的程式實例。</param>
    /// <param name="caseNumber">範例案例編號。</param>
    /// <param name="nums">要驗證的輸入陣列。</param>
    /// <param name="expected">此案例預期缺少的數字。</param>
    private static void RunSampleCase(Program solver, int caseNumber, int[] nums, int expected)
    {
        // 排序解法會改動陣列內容，因此三種解法都使用複本，避免彼此互相影響。
        int sortingResult = solver.MissingNumber((int[])nums.Clone());
        int hashSetResult = solver.MissingNumber2((int[])nums.Clone());
        int xorResult = solver.MissingNumber3((int[])nums.Clone());

        Console.WriteLine($"Case {caseNumber}");
        Console.WriteLine($"Input: [{string.Join(", ", nums)}]");
        Console.WriteLine($"Expected: {expected}");
        Console.WriteLine($"MissingNumber  (Sort)   : {sortingResult} {(sortingResult == expected ? "PASS" : "FAIL")}");
        Console.WriteLine($"MissingNumber2 (HashSet): {hashSetResult} {(hashSetResult == expected ? "PASS" : "FAIL")}");
        Console.WriteLine($"MissingNumber3 (XOR)    : {xorResult} {(xorResult == expected ? "PASS" : "FAIL")}");
        Console.WriteLine();
    }
}
