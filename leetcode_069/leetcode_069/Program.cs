namespace leetcode_069;

class Program
{
    /// <summary>
    /// 69. Sqrt(x)
    ///
    /// English:
    /// Given a non-negative integer x, return the square root of x rounded down to the nearest integer.
    /// The returned integer should be non-negative as well.
    /// You must not use any built-in exponent function or operator.
    /// For example, do not use pow(x, 0.5) in C++ or x ** 0.5 in Python.
    ///
    /// 繁體中文：
    /// 給定一個非負整數 x，回傳 x 的平方根，並無條件捨去到最接近的整數。
    /// 回傳的整數也必須是非負數。
    /// 你不能使用任何內建的指數函式或運算子。
    /// 例如，請勿在 C++ 使用 pow(x, 0.5)，也不要在 Python 使用 x ** 0.5。
    ///
    /// 此主要進入點提供可直接執行的範例測試資料，輸入多組非負整數並輸出
    /// <see cref="MySqrt"/> 的計算結果、預期答案與 PASS/FAIL 狀態。
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
