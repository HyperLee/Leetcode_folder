namespace leetcode_1399;

class Program
{
    private const int MaximumDigitSum = 36;

    /// <summary>
    /// <para>
    /// 1399. Count Largest Group
    /// https://leetcode.com/problems/count-largest-group/description/
    ///
    /// You are given an integer n.
    ///
    /// We need to group the numbers from 1 to n according to the sum of their digits. For example, the numbers 14 and 5 belong
    /// to the same group, whereas 13 and 3 belong to different groups.
    ///
    /// Return the number of groups that have the largest size, i.e. the maximum number of elements.
    ///
    /// Example 1:
    /// Input: n = 13
    /// Output: 4
    /// Explanation: There are 9 groups in total, grouped according to the digit sums of numbers from 1 to 13:
    /// [1,10], [2,11], [3,12], [4,13], [5], [6], [7], [8], [9].
    /// There are 4 groups with the largest size.
    ///
    /// Example 2:
    /// Input: n = 2
    /// Output: 2
    /// Explanation: There are 2 groups [1], [2] of size 1.
    ///
    /// Constraints:
    /// - 1 &lt;= n &lt;= 10^4
    /// </para>
    /// <para>
    /// 1399. 統計最大群組的數量
    /// https://leetcode.cn/problems/count-largest-group/description/
    ///
    /// 給定一個整數 n。
    ///
    /// 必須依照各位數字的總和，將從 1 到 n 的數字分組。例如，數字 14 與 5 屬於同一群組，
    /// 而 13 與 3 屬於不同群組。
    ///
    /// 回傳大小最大的群組數量，也就是具有最多元素的群組數量。
    ///
    /// 範例 1：
    /// 輸入：n = 13
    /// 輸出：4
    /// 解釋：總共有 9 個群組，從 1 到 13 的數字依各位數字總和分組如下：
    /// [1,10]、[2,11]、[3,12]、[4,13]、[5]、[6]、[7]、[8]、[9]。
    /// 有 4 個大小最大的群組。
    ///
    /// 範例 2：
    /// 輸入：n = 2
    /// 輸出：2
    /// 解釋：有 2 個群組 [1]、[2]，大小均為 1。
    ///
    /// 限制條件：
    /// - 1 &lt;= n &lt;= 10^4
    /// </para>
    /// </summary>
    /// <remarks>
    /// 程式進入點會將邊界與代表性輸入分別交給三種解法，
    /// 並核對預期結果與實際結果。全部檢查通過時結束碼為 0，否則為 1。
    /// </remarks>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        (string Name, int Input, int Expected)[] testCases =
        [
            ("最小邊界", 1, 1),
            ("官方範例二", 2, 2),
            ("官方範例一", 13, 4),
            ("進位前的一般案例", 19, 9),
            ("多個最大群組", 24, 5),
            ("三位數邊界", 999, 2),
            ("最大邊界", 10000, 1),
        ];

        (string Name, Func<int, int> Solve)[] solutions =
        [
            (nameof(CountLargestGroup), CountLargestGroup),
            (nameof(CountLargestGroup2), CountLargestGroup2),
            (nameof(CountLargestGroup3), CountLargestGroup3),
        ];

        int passedChecks = 0;
        int totalChecks = testCases.Length * solutions.Length;

        Console.WriteLine("LeetCode 1399 - Count Largest Group");
        Console.WriteLine();

        foreach ((string name, int input, int expected) in testCases)
        {
            Console.WriteLine($"案例：{name}");
            Console.WriteLine($"Input: n = {input}");

            foreach ((string solutionName, Func<int, int> solve) in solutions)
            {
                int actual = solve(input);
                bool passed = actual == expected;
                passedChecks += passed ? 1 : 0;

                Console.WriteLine(
                    $"{solutionName} | Expected: {expected} | Actual: {actual} | {(passed ? "PASS" : "FAIL")}");
            }

            Console.WriteLine();
        }

        Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");
        Environment.ExitCode = passedChecks == totalChecks ? 0 : 1;
    }

    /// <summary>
    /// 使用字典統計 1 到 <paramref name="n"/> 中每種各位數和的出現次數，
    /// 適用於 1 到 10000 的輸入，並回傳成員數量最多的群組有幾個。
    /// </summary>
    /// <param name="n">分組範圍的上限，必須介於 1 與 10000 之間。</param>
    /// <returns>成員數量達到全域最大值的群組數量。</returns>
    /// <remarks>時間複雜度為 O(n log n)；字典鍵數受最大各位數和限制，空間複雜度為 O(log n)。</remarks>
    public static int CountLargestGroup(int n)
    {
        Dictionary<int, int> groupSizes = [];

        for (int number = 1; number <= n; number++)
        {
            int digitSum = GetDigitSum(number);
            groupSizes[digitSum] = groupSizes.GetValueOrDefault(digitSum) + 1;
        }

        int maxSize = groupSizes.Values.Max();
        return groupSizes.Values.Count(size => size == maxSize);
    }

    /// <summary>
    /// 使用固定大小的計數陣列，統計 1 到 <paramref name="n"/> 中每種各位數和的群組大小。
    /// 適用於 1 到 10000 的輸入，回傳並列最大群組的數量。
    /// </summary>
    /// <param name="n">分組範圍的上限，必須介於 1 與 10000 之間。</param>
    /// <returns>成員數量達到全域最大值的群組數量。</returns>
    /// <remarks>時間複雜度為 O(n log n)；計數桶大小固定，空間複雜度為 O(1)。</remarks>
    public static int CountLargestGroup2(int n)
    {
        int[] groupSizes = new int[MaximumDigitSum + 1];

        for (int number = 1; number <= n; number++)
        {
            // 在題目範圍內，各位數和只會落在 1 到 36。
            groupSizes[GetDigitSum(number)]++;
        }

        int maxSize = groupSizes.Max();
        return groupSizes.Count(size => size == maxSize);
    }

    /// <summary>
    /// 以前綴數字的結果遞推每個數的各位數和，並在分組時同步追蹤最大群組。
    /// 適用於 1 到 10000 的輸入，回傳並列最大群組的數量。
    /// </summary>
    /// <param name="n">分組範圍的上限，必須介於 1 與 10000 之間。</param>
    /// <returns>成員數量達到全域最大值的群組數量。</returns>
    /// <remarks>時間複雜度為 O(n)；儲存每個數的各位數和，空間複雜度為 O(n)。</remarks>
    public static int CountLargestGroup3(int n)
    {
        int[] digitSums = new int[n + 1];
        int[] groupSizes = new int[MaximumDigitSum + 1];
        int maxSize = 0;
        int largestGroupCount = 0;

        for (int number = 1; number <= n; number++)
        {
            // 移除個位數後的數已經計算過，因此只需加上目前個位數。
            digitSums[number] = digitSums[number / 10] + (number % 10);
            int currentSize = ++groupSizes[digitSums[number]];

            // 新最大值會重置群組數；追平時才增加並列群組。
            if (currentSize > maxSize)
            {
                maxSize = currentSize;
                largestGroupCount = 1;
            }
            else if (currentSize == maxSize)
            {
                largestGroupCount++;
            }
        }

        return largestGroupCount;
    }


    /// <summary>
    /// 重複取出非負整數的個位數並移除該位，回傳所有位數的總和。
    /// </summary>
    /// <param name="number">要加總各位數的非負整數。</param>
    /// <returns><paramref name="number"/> 每一位數相加後的結果。</returns>
    private static int GetDigitSum(int number)
    {
        int sum = 0;
        while (number > 0)
        {
            sum += number % 10;
            number /= 10;
        }

        return sum;
    }
}