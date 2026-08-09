namespace leetcode_069;

class Program
{
    /// <summary>
    /// 69. Sqrt(x)
    /// https://leetcode.com/problems/sqrtx/description/
    /// <para>
    /// Given a non-negative integer x, return the square root of x rounded down to the nearest integer. The returned integer must also be non-negative.
    ///
    /// You must not use any built-in exponent function or operator.
    /// - For example, do not use pow(x, 0.5) in C++ or x ** 0.5 in Python.
    ///
    /// Example 1:
    /// Input: x = 4
    /// Output: 2
    /// Explanation: The square root of 4 is 2, so return 2.
    ///
    /// Example 2:
    /// Input: x = 8
    /// Output: 2
    /// Explanation: The square root of 8 is 2.82842..., and after rounding down to the nearest integer, 2 is returned.
    ///
    /// Constraints:
    /// - 0 &lt;= x &lt;= 2^31 - 1
    /// </para>
    /// <para>
    /// 69. x 的平方根
    /// https://leetcode.cn/problems/sqrtx/description/
    ///
    /// 給定一個非負整數 x，請回傳 x 的平方根無條件捨去後的整數。回傳的整數也必須是非負數。
    ///
    /// 你不得使用任何內建的指數函式或運算子。
    /// - 例如，不得在 C++ 使用 pow(x, 0.5)，也不得在 Python 使用 x ** 0.5。
    ///
    /// 範例 1：
    /// 輸入：x = 4
    /// 輸出：2
    /// 解釋：4 的平方根是 2，因此回傳 2。
    ///
    /// 範例 2：
    /// 輸入：x = 8
    /// 輸出：2
    /// 解釋：8 的平方根是 2.82842...，無條件捨去後回傳 2。
    ///
    /// 限制條件：
    /// - 0 &lt;= x &lt;= 2^31 - 1
    /// </para>
    /// </summary>
    /// <param name="args">Command-line arguments; unused.</param>
    static void Main(string[] args)
    {
        (int Input, int Expected)[] testCases =
        [
            (0, 0),
            (1, 1),
            (4, 2),
            (8, 2),
            (15, 3),
            (2147395599, 46339),
            (int.MaxValue, 46340),
        ];

        Program solution = new Program();

        foreach ((int input, int expected) in testCases)
        {
            int actual = solution.MySqrt(input);
            string status = actual == expected ? "PASS" : "FAIL";

            Console.WriteLine($"MySqrt({input}) = {actual}; expected = {expected}; {status}");
        }
    }

    /// <summary>
    /// 使用二分搜尋計算非負整數 <paramref name="x"/> 的整數平方根。
    /// 解題概念是尋找最大的整數 k，使得 k * k 小於或等於 x；若 mid 的平方
    /// 不超過 x，mid 可能是答案並嘗試往右半邊找更大的值，否則往左半邊縮小範圍。
    /// 輸入條件為 0 &lt;= x &lt;= <see cref="int.MaxValue"/>，輸出結果為無條件捨去後的平方根整數。
    /// </summary>
    /// <param name="x">要計算整數平方根的非負整數。</param>
    /// <returns>小於或等於真實平方根的最大整數。</returns>
    public int MySqrt(int x)
    {
        int left = 0;
        int right = x;
        int answer = 0;

        while (left <= right)
        {
            // 避免 left + right 在大範圍輸入時產生整數溢位。
            int mid = left + (right - left) / 2;
            long square = (long)mid * mid;

            if (square <= x)
            {
                // mid 已符合條件，記錄目前最佳解並嘗試尋找更大的整數平方根。
                answer = mid;
                left = mid + 1;
            }
            else
            {
                // mid 的平方過大，答案只能落在左半邊。
                right = mid - 1;
            }
        }

        return answer;
    }
}
