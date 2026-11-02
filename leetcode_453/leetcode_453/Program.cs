namespace leetcode_453;

/// <summary>
/// 453. Minimum Moves to Equal Array Elements
/// https://leetcode.com/problems/minimum-moves-to-equal-array-elements/description/
/// 453. 最小操作次数使数组元素相等
/// https://leetcode.cn/problems/minimum-moves-to-equal-array-elements/description/
/// English: Given an integer array nums of size n, return the minimum number of moves required to make all array elements equal.
/// In one move, you can increment n - 1 elements of the array by 1.
/// 繁體中文：給定一個大小為 n 的整數陣列 nums，請回傳使陣列中所有元素相等所需的最少操作次數。
/// 每次操作中，你可以將陣列中的 n - 1 個元素各增加 1。
/// </summary>
class Program
{
    /// <summary>
    /// 使用內建案例驗證 LeetCode 453 解法，並以預期結果比對最小值差額總和的計算結果。
    /// 此入口不讀取命令列輸入；案例全數通過時列印驗證結果，若有不符則拋出例外。
    /// </summary>
    /// <param name="args">未使用的命令列引數。</param>
    static void Main(string[] args)
    {
        Program solution = new();
        (int[] Nums, int Expected)[] testCases =
        {
            (new[] { 1, 2, 3 }, 3),
            (new[] { 1, 1, 1 }, 0),
            (new[] { 42 }, 0),
            (new[] { -1_000_000_000, 1_000_000_000 }, 2_000_000_000)
        };

        foreach ((int[] nums, int expected) in testCases)
        {
            int actual = solution.MinMoves(nums);
            if (actual != expected)
            {
                throw new InvalidOperationException(
                    $"Expected {expected} moves for [{string.Join(", ", nums)}], but got {actual}.");
            }

            Console.WriteLine(
                $"[PASS] nums=[{string.Join(", ", nums)}], expected={expected}, actual={actual}");
        }
    }

    /// <summary>
    /// 計算非空整數陣列 nums 變成所有元素相等所需的最少操作次數；nums 長度介於 1 至 10^5，
    /// 元素介於 -10^9 至 10^9，且答案符合 32 位元有號整數範圍。利用每次操作等價於降低一個元素，
    /// 將每個元素與陣列最小值的差額加總後回傳。
    /// </summary>
    /// <param name="nums">符合題目限制的非空整數陣列。</param>
    /// <returns>使所有元素相等所需的最少操作次數。</returns>
    public int MinMoves(int[] nums)
    {
        int minNum = nums.Min();
        int res = 0;

        // 對所有元素同加 1 只會讓整個陣列平移；忽略這個共同增量後，操作等價於將未被遞增的那個元素降低 1。
        // 因此將每個元素降到原陣列最小值，並加總所需的差額，即得到最少操作次數。
        foreach (var num in nums)
        {
            res += num - minNum;
        }

        return res;
    }
}
