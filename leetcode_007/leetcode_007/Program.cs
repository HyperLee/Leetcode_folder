namespace leetcode_007;

class Program
{
    /// <summary>
    /// 7. Reverse Integer
    /// https://leetcode.com/problems/reverse-integer/description/
    /// <para>
    /// Given a signed 32-bit integer x, return x with its digits reversed. If reversing x causes the value to go outside the signed 32-bit integer range [-2^31, 2^31 - 1], then return 0.
    ///
    /// Assume the environment does not allow you to store 64-bit integers (signed or unsigned).
    ///
    /// Example 1:
    /// Input: x = 123
    /// Output: 321
    ///
    /// Example 2:
    /// Input: x = -123
    /// Output: -321
    ///
    /// Example 3:
    /// Input: x = 120
    /// Output: 21
    ///
    /// Constraints:
    /// - -2^31 &lt;= x &lt;= 2^31 - 1
    /// </para>
    /// <para>
    /// 7. 整數反轉
    /// https://leetcode.cn/problems/reverse-integer/description/
    ///
    /// 給定一個有符號 32 位元整數 x，請將 x 的數字反轉後回傳。如果反轉 x 會使數值超出有符號 32 位元整數範圍 [-2^31, 2^31 - 1]，則回傳 0。
    ///
    /// 假設執行環境不允許儲存 64 位元整數（有符號或無符號）。
    ///
    /// 範例 1：
    /// 輸入：x = 123
    /// 輸出：321
    ///
    /// 範例 2：
    /// 輸入：x = -123
    /// 輸出：-321
    ///
    /// 範例 3：
    /// 輸入：x = 120
    /// 輸出：21
    ///
    /// 限制條件：
    /// - -2^31 &lt;= x &lt;= 2^31 - 1
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料
        int[] testCases = {123, -123, 120, 0, 1534236469, -2147483648};
        foreach (int x in testCases)
        {
            int result = Reverse(x);
            Console.WriteLine($"Reverse({x}) = {result}");
        }
    }


    /// <summary>
    /// 解題說明：
    /// 此函式將給定的有符號 32 位元整數 x 進行數字反轉，若反轉後超出 32 位元整數範圍則回傳 0。
    /// 透過數學運算（不使用輔助堆疊或陣列）逐位彈出與推入數字，並於每次運算時檢查是否溢位。
    /// 
    /// 時間複雜度：O(log₁₀x)，x 的位數決定迴圈次數。
    /// 空間複雜度：O(1)，僅使用常數額外變數。
    /// 
    /// ref:
    /// https://leetcode.cn/problems/reverse-integer/solution/zheng-shu-fan-zhuan-by-leetcode-solution-bccn/
    /// 
    /// 要在沒有輔助堆疊或陣列的幫助下「彈出」和「推入」數字，可以使用如下數學方法：
    /// 彈出 x 的末尾數字 digit
    /// int digit = x % 10;
    /// x /= 10;
    /// 
    /// 將數字 digit 推入 rev 末尾
    /// rev = rev * 10 + digit;
    /// 
    /// 溢位檢查說明可以查看 md 檔案
    /// rev 的計算方式會先乘以 10，然後加上 digit
    /// 也就是說檢查溢位的時候需要考慮到乘以 10 的影響。
    /// 當 rev 乘以 10 後，若超過 int.MaxValue/ 10 或小於 int.MinValue/ 10，則表示下一步加上 digit 會導致溢位。
    /// </summary>
    /// <param name="x">要反轉的整數</param>
    /// <returns>反轉後的整數，若溢位則回傳 0</returns>
    public static int Reverse(int x)
    {
        // 初始化反轉後的數字 rev
        int rev = 0;
        // 當 x 不等於 0 時，持續處理每一位數字
        while (x != 0)
        {
            // 檢查 rev 是否即將溢位，若會溢位則直接回傳 0
            if (rev < int.MinValue / 10 || rev > int.MaxValue / 10)
            {
                return 0;
            }

            // 取得 x 的末尾數字 digit
            int digit = x % 10;
            // 去掉 x 的末尾數字
            x /= 10;

            // 將 digit 加到 rev 的末尾，完成一位數字的反轉
            rev = rev * 10 + digit;
        }
        // 回傳反轉後的結果
        return rev;
    }
}
