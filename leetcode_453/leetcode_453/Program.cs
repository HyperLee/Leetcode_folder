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
    /// 使用八組內建案例驗證 LeetCode 453 的三種解法，共進行 24 次預期結果比對。
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
            (new[] { -1_000_000_000, 1_000_000_000 }, 2_000_000_000),
            (new[] { 5, 6, 1 }, 9),
            (new[] { -3, -1, -2 }, 3),
            (Enumerable.Repeat(1_000_000_000, 100_000).ToArray(), 0),
            (Enumerable.Repeat(-1_000_000_000, 100_000).ToArray(), 0)
        };
        (string Name, Func<int[], int> Solve)[] solutions =
        {
            (nameof(MinMoves), solution.MinMoves),
            (nameof(MinMoves2), solution.MinMoves2),
            (nameof(MinMoves3), solution.MinMoves3)
        };

        foreach ((int[] nums, int expected) in testCases)
        {
            string input = nums.Length <= 10
                ? $"[{string.Join(", ", nums)}]"
                : $"[length={nums.Length}, first={nums[0]}, last={nums[^1]}]";

            foreach ((string methodName, Func<int[], int> solve) in solutions)
            {
                int actual = solve(nums);
                if (actual != expected)
                {
                    throw new InvalidOperationException(
                        $"{methodName}: expected {expected} moves for {input}, but got {actual}.");
                }

                Console.WriteLine(
                    $"[PASS] method={methodName}, nums={input}, expected={expected}, actual={actual}");
            }
        }

        Console.WriteLine($"All {testCases.Length * solutions.Length} checks passed.");
    }

    /// <summary>
    /// 解法一：先以 LINQ Min 取得最小值，再加總差額，回傳所有元素相等所需的最少操作次數。
    /// 每次增加 n - 1 個元素，忽略共同增量後等價於降低一個元素，因此以原陣列最小值為目標。
    /// nums 長度介於 1 至 10^5、元素介於 -10^9 至 10^9，且答案符合 32 位元有號整數範圍。
    /// 不修改輸入陣列，時間複雜度為 O(n)，額外空間複雜度為 O(1)。
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
            // 差額皆非負，部分累加不超過題目保證可用 int 表示的最終答案。
            res += num - minNum;
        }

        return res;
    }

    /// <summary>
    /// 解法二：單次走訪同時計算總和與最小值，利用 sum - n * minNum 求得最少操作次數。
    /// 將各元素與最小值的差額總和展開，即為元素總和減去 n 個最小值。
    /// nums 長度介於 1 至 10^5、元素介於 -10^9 至 10^9，且答案符合 32 位元有號整數範圍；
    /// 總和與乘積仍可能超過 int 範圍，使用 long 避免中間計算溢位。
    /// 不修改輸入陣列，時間複雜度為 O(n)，額外空間複雜度為 O(1)。
    /// </summary>
    /// <param name="nums">符合題目限制的非空整數陣列。</param>
    /// <returns>使所有元素相等所需的最少操作次數。</returns>
    public int MinMoves2(int[] nums)
    {
        int minNum = nums[0];
        long sum = 0;

        foreach (int num in nums)
        {
            // 每個元素都必須累加；這行不依賴 minNum，與下方最小值更新的順序可以交換。
            sum += num;

            if (num < minNum)
            {
                minNum = num;
            }
        }

        // 必須在走訪結束後使用最終最小值，且先轉為 long 再相乘，避免 int 乘法溢位。
        return (int)(sum - ((long)minNum * nums.Length));
    }

    /// <summary>
    /// 解法三：以兩次 foreach 先找出最小值，再加總差額，回傳所有元素相等所需的最少操作次數。
    /// 沿用解法一的等價減少觀點，以 Math.Min 比較元素，取代 LINQ Min 的整體最小值搜尋。
    /// nums 長度介於 1 至 10^5、元素介於 -10^9 至 10^9，且答案符合 32 位元有號整數範圍。
    /// 不修改輸入陣列，時間複雜度為 O(n)，額外空間複雜度為 O(1)。
    /// </summary>
    /// <param name="nums">符合題目限制的非空整數陣列。</param>
    /// <returns>使所有元素相等所需的最少操作次數。</returns>
    public int MinMoves3(int[] nums)
    {
        // 題目保證非空；使用實際元素初始化，避免以 0 初始化時誤判全正數陣列的最小值。
        int minNum = nums[0];
        int res = 0;

        foreach (int x in nums)
        {
            minNum = Math.Min(minNum, x);
        }

        // 必須先確定完整陣列的最小值，才可讓所有差額使用同一基準；否則較小值晚出現時會少算。
        foreach (int x in nums)
        {
            // 與解法一相同，非負差額的累加不會超過最終答案，因此可安全使用 int。
            res += x - minNum;
        }

        return res;
    }
}
