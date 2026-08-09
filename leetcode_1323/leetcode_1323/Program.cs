namespace leetcode_1323;

class Program
{
    /// <summary>
    /// <para>
    /// 1323. Maximum 69 Number
    /// https://leetcode.com/problems/maximum-69-number/description/
    ///
    /// You are given a positive integer num consisting only of digits 6 and 9.
    ///
    /// Return the maximum number you can get by changing at most one digit (6 becomes 9, and 9 becomes 6).
    ///
    /// Example 1:
    /// Input: num = 9669
    /// Output: 9969
    /// Explanation:
    /// Changing the first digit results in 6669.
    /// Changing the second digit results in 9969.
    /// Changing the third digit results in 9699.
    /// Changing the fourth digit results in 9666.
    /// The maximum number is 9969.
    ///
    /// Example 2:
    /// Input: num = 9996
    /// Output: 9999
    /// Explanation: Changing the last digit 6 to 9 results in the maximum number.
    ///
    /// Example 3:
    /// Input: num = 9999
    /// Output: 9999
    /// Explanation: It is better not to apply any change.
    ///
    /// Constraints:
    /// - 1 &lt;= num &lt;= 10^4
    /// - num consists of only 6 and 9 digits.
    /// </para>
    /// <para>
    /// 1323. 最大的 69 數字
    /// https://leetcode.cn/problems/maximum-69-number/description/
    ///
    /// 給定一個只由數字 6 和 9 組成的正整數 num。
    ///
    /// 最多變更一個數字（6 變成 9，9 變成 6），回傳所能得到的最大數字。
    ///
    /// 範例 1：
    /// 輸入：num = 9669
    /// 輸出：9969
    /// 解釋：
    /// 變更第一個數字會得到 6669。
    /// 變更第二個數字會得到 9969。
    /// 變更第三個數字會得到 9699。
    /// 變更第四個數字會得到 9666。
    /// 最大數字是 9969。
    ///
    /// 範例 2：
    /// 輸入：num = 9996
    /// 輸出：9999
    /// 解釋：將最後一個數字從 6 改成 9，會得到最大數字。
    ///
    /// 範例 3：
    /// 輸入：num = 9999
    /// 輸出：9999
    /// 解釋：不進行任何變更會比較好。
    ///
    /// 限制條件：
    /// - 1 &lt;= num &lt;= 10^4
    /// - num 僅由數字 6 和 9 組成。
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料
        int[] tests = new int[] { 9669, 9996, 9999, 6666, 6, 9 };

        var p = new Program();
        Console.WriteLine("--- 方法一（字串處理） ---");
        foreach (var t in tests)
        {
            int result = p.Maximum69Number(t);
            Console.WriteLine($"輸入: {t} -> 最大值: {result}");
        }

        Console.WriteLine("--- 方法二（數學運算） ---");
        foreach (var t in tests)
        {
            int result2 = p.Maximum69Number2(t);
            Console.WriteLine($"輸入: {t} -> 最大值: {result2}");
        }
    }

    /// <summary>
    /// 解題思路：
    /// 此題要求將一個只包含數字 6 和 9 的正整數 num，最多將一個 6 變為 9，取得最大可能值。
    /// 由於高位數字影響最大，採用貪心策略，將最左邊（數位最高）的 6 變為 9 即可。
    /// 若不存在 6，則原數即為最大值。
    /// 實作上，將 num 轉為字元陣列，遍歷並找到第一個 6 變為 9，最後再轉回整數。
    /// <example>
    /// <code>
    /// Maximum69Number(9669) // 回傳 9969
    /// Maximum69Number(9999) // 回傳 9999
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="num">只包含 6 和 9 的正整數</param>
    /// <returns>最大可能值</returns>
    public int Maximum69Number(int num)
    {
        // 將 num 轉為字元陣列，方便逐位操作
        char[] digits = num.ToString().ToCharArray();
        // 從高位到低位遍歷，找到第一個 6
        for (int i = 0; i < digits.Length; i++)
        {
            if (digits[i] == '6')
            {
                digits[i] = '9'; // 只需將第一個 6 變為 9
                break; // 只允許一次變更，立即跳出
            }
        }

        // 將修改後的字元陣列轉回整數並回傳
        return int.Parse(new string(digits));
    }


    /// <summary>
    /// <summary>
    /// 另一種解法：數學運算
    /// 解題說明：
    /// 不需將數字轉為字串，直接用數學方式遍歷每個數位。
    /// 從最低位（個位）開始，每次取 num % 10 得到當前位數，num /= 10 去掉最低位。
    /// 用 base 變數記錄目前位數（1, 10, 100...），每遇到 6 就更新 maxBase。
    /// 最終 maxBase 即最高位的 6 所在的 base。
    /// 回傳 num + maxBase * 3，即將最高位的 6 換成 9。
    /// 最後 * 3 是因為將 6 變為 9 相當於增加了 3 倍的 base 值。
    /// <example>
    /// <code>
    /// Maximum69Number2(9669) // 回傳 9969
    /// Maximum69Number2(9999) // 回傳 9999
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="num">只包含 6 和 9 的正整數</param>
    /// <returns>最大可能值</returns>
    public int Maximum69Number2(int num)
    {
        int maxBase = 0; // 記錄最高位的 6 的 base
        int baseValue = 1; // 當前位數（1, 10, 100...）
        for (int x = num; x > 0; x /= 10)
        {
            // 取最低位
            int digit = x % 10;
            // 若該位為 6，更新 maxBase
            if (digit == 6)
            {
                maxBase = baseValue;
            }
            // baseValue 進位
            baseValue *= 10;
        }
        // 若有 6，將最高位的 6 換成 9，num + maxBase * 3
        // 若沒有 6，maxBase 為 0，直接回傳原數
        return num + maxBase * 3;
    }

}
