namespace leetcode_462;

class Program
{
    /// <summary>
    /// 462. Minimum Moves to Equal Array Elements II
    /// https://leetcode.com/problems/minimum-moves-to-equal-array-elements-ii/description/
    /// 462. 最小操作次数使数组元素相等 II
    /// https://leetcode.cn/problems/minimum-moves-to-equal-array-elements-ii/description/
    /// English (original):
    /// Given an integer array nums of size n, return the minimum number of moves required to make all array elements equal.
    /// In one move, you can increment or decrement an element of the array by 1.
    /// Test cases are designed so that the answer will fit in a 32-bit integer.
    /// 繁體中文：
    /// 給定一個大小為 n 的整數陣列 nums，請回傳使陣列中所有元素相等所需的最少移動次數。
    /// 每次移動可以將陣列中的一個元素遞增或遞減 1。
    /// 測試案例保證答案可放入 32 位元整數。
    /// <para>本主控台入口使用內建案例比較兩種解法；args 不需提供輸入資料，成功時輸出結果，不符時拋出例外。</para>
    /// </summary>
    /// <param name="args">命令列引數；範例資料已內建，因此不會使用此參數。</param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        (int[] Nums, int Expected)[] testCases =
        {
            (new[] { 1, 2, 3 }, 2),
            (new[] { 1, 10, 2, 9 }, 16),
            (new[] { 7, 7, 7 }, 0),
            (new[] { -1000000000 }, 0),
            (new[] { -7, -3, -1 }, 6)
        };

        foreach ((int[] nums, int expected) in testCases)
        {
            int sortResult = solution.MinMoves2((int[])nums.Clone());
            int quickSelectResult = solution.MinMoves2QuickSelect((int[])nums.Clone());

            if (sortResult != expected || quickSelectResult != expected)
            {
                throw new InvalidOperationException(
                    $"Input [{string.Join(", ", nums)}]: expected {expected}, " +
                    $"MinMoves2 returned {sortResult}, " +
                    $"MinMoves2QuickSelect returned {quickSelectResult}.");
            }

            Console.WriteLine(
                $"Input: [{string.Join(", ", nums)}] | Expected: {expected} | " +
                $"MinMoves2: {sortResult} | MinMoves2QuickSelect: {quickSelectResult}");
        }

        Console.WriteLine($"All {testCases.Length} examples passed.");
    }

    /// <summary>
    /// 排序 nums 後取中位數，並加總每個元素與中位數的距離，求得最少操作次數。輸入需符合題目限制：陣列長度為 1 至 100,000，元素值介於 −10^9 至 10^9；回傳值可用 32 位元整數表示。
    /// </summary>
    /// <param name="nums">輸入整數陣列；方法會原地排序並改變元素順序。</param>
    /// <returns>使所有元素相等所需的最少操作次數。</returns>
    public int MinMoves2(int[] nums)
    {
        Array.Sort(nums);
        int n = nums.Length;

        // 中位數會使絕對差總和最小；偶數長度時取右側中位數也能得到最小值。
        int median = nums[n / 2];
        int totalMoves = 0;
        for (int i = 0; i < n; i++)
        {
            totalMoves += Math.Abs(nums[i] - median);
        }

        return totalMoves;
    }

    /// <summary>
    /// 使用三向分割的 Quickselect 找到中位數，再加總每個元素與中位數的距離，求得最少操作次數。輸入需符合題目限制：陣列長度為 1 至 100,000，元素值介於 −10^9 至 10^9；回傳值可用 32 位元整數表示。
    /// </summary>
    /// <param name="nums">輸入整數陣列；方法會原地分割並改變元素順序。</param>
    /// <returns>使所有元素相等所需的最少操作次數。</returns>
    public int MinMoves2QuickSelect(int[] nums)
    {
        int medianIndex = nums.Length / 2;
        int left = 0;
        int right = nums.Length - 1;

        while (true)
        {
            int pivot = nums[left + (right - left) / 2];
            int lower = left;
            int current = left;
            int upper = right;

            // 三向分割將值分成小於、等於、大於 pivot 的區段；重複值會一次歸入等值區。
            while (current <= upper)
            {
                if (nums[current] < pivot)
                {
                    (nums[lower], nums[current]) = (nums[current], nums[lower]);
                    lower++;
                    current++;
                }
                else if (nums[current] > pivot)
                {
                    (nums[current], nums[upper]) = (nums[upper], nums[current]);
                    upper--;
                }
                else
                {
                    current++;
                }
            }

            // 中位數索引只會落在三個區段之一；只保留包含目標索引的區段繼續搜尋。
            if (medianIndex < lower)
            {
                right = lower - 1;
            }
            else if (medianIndex > upper)
            {
                left = upper + 1;
            }
            else
            {
                break;
            }
        }

        int median = nums[medianIndex];
        int totalMoves = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            totalMoves += Math.Abs(nums[i] - median);
        }

        return totalMoves;
    }
}
