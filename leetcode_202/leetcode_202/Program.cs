namespace leetcode_202;

class Program
{
    /// <summary>
    /// 202. Happy Number
    /// https://leetcode.com/problems/happy-number/description/
    /// 202. 快樂數
    /// https://leetcode.cn/problems/happy-number/description/
    ///
    /// Write an algorithm to determine if a number n is happy.
    /// A happy number is a number defined by the following process:
    /// Starting with any positive integer, replace the number by the sum of the squares of its digits.
    /// Repeat the process until the number equals 1 (where it will stay), or it loops endlessly in a cycle which does not include 1.
    /// Those numbers for which this process ends in 1 are happy.
    /// Return true if n is a happy number, and false if not.
    ///
    /// 請撰寫一個演算法來判斷整數 n 是否為快樂數。
    /// 快樂數的定義如下：
    /// 從任意正整數開始，將該數替換為其各位數字平方和。
    /// 重複此過程，直到數字變成 1（之後會持續停留在 1），
    /// 或陷入一個不包含 1 的無限循環。
    /// 如果此過程最後會得到 1，則此數為快樂數。
    /// 若 n 是快樂數則回傳 true，否則回傳 false。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        (int Value, bool Expected)[] samples =
        [
            (1, true),
            (7, true),
            (19, true),
            (2, false),
            (20, false)
        ];

        foreach ((int value, bool expected) in samples)
        {
            bool hashSetResult = IsHappy(value);
            bool floydResult = IsHappy2(value);
            bool match = hashSetResult == expected && floydResult == expected;

            Console.WriteLine(
                $"n={value} | HashSet={hashSetResult} | Floyd={floydResult} | Expected={expected} | Match={match}");
        }
    }

    /// <summary>
    /// 判斷整數是否為快樂數，並以雜湊集合記錄每次轉換後的值來偵測循環。
    /// 解題概念是反覆把目前數字轉成「各位數平方和」，若過程中先得到 1 就是快樂數；
    /// 若某個中間值重複出現，代表已進入不含 1 的循環，因此可直接判定為非快樂數。
    /// 輸入預期為正整數；若傳入 0 或負數，方法會採用防禦式處理並回傳 false。
    /// </summary>
    /// <param name="n">要判斷的整數，題目語意下應為正整數。</param>
    /// <returns>若 n 最終可轉換為 1 則回傳 true，否則回傳 false。</returns>
    public static bool IsHappy(int n)
    {
        if (n <= 0)
        {
            return false;
        }

        HashSet<int> seen = [];
        int current = n;

        // 若某個中間值再次出現，就代表後續會重複相同轉換流程，無法走到 1。
        while (current != 1 && !seen.Contains(current))
        {
            seen.Add(current);
            current = SumOfSquaredDigits(current);
        }

        return current == 1;
    }

    /// <summary>
    /// 計算一個非負整數每一位數字的平方和，作為快樂數轉換規則的核心步驟。
    /// 解題時會透過取餘數與整除的方式，自右而左拆出每個位數並累加平方值。
    /// 輸入預期為非負整數；若 value 為 0，結果會回傳 0。
    /// </summary>
    /// <param name="value">要拆解並計算平方和的整數。</param>
    /// <returns>value 各位數字平方後的總和。</returns>
    private static int SumOfSquaredDigits(int value)
    {
        int sum = 0;

        while (value > 0)
        {
            int digit = value % 10;

            // 透過「取個位數 -> 平方 -> 加總 -> 去掉個位數」逐步完成題目要求的轉換。
            sum += digit * digit;
            value /= 10;
        }

        return sum;
    }

    /// <summary>
    /// 判斷整數是否為快樂數，並以 Floyd 快慢指標法把數字轉換序列視為鏈結串列中的循環偵測問題。
    /// 慢指標每次前進一步，快指標每次前進兩步；若快指標先到 1，代表序列收斂到快樂數，
    /// 若兩者在非 1 的位置相遇，則表示序列已落入循環，應回傳 false。
    /// 輸入預期為正整數；若傳入 0 或負數，方法會採用防禦式處理並回傳 false。
    /// </summary>
    /// <param name="n">要判斷的整數，題目語意下應為正整數。</param>
    /// <returns>若 n 最終可轉換為 1 則回傳 true，否則回傳 false。</returns>
    public static bool IsHappy2(int n)
    {
        if (n <= 0)
        {
            return false;
        }

        int slowRunner = n;
        int fastRunner = n;

        // 將數字轉換流程視為鏈結串列走訪；若存在循環，快慢指標終會在循環內相遇。
        do
        {
            slowRunner = SumOfSquaredDigits(slowRunner);
            fastRunner = SumOfSquaredDigits(SumOfSquaredDigits(fastRunner));
        }
        while (fastRunner != 1 && slowRunner != fastRunner);

        return fastRunner == 1;
    }
}
