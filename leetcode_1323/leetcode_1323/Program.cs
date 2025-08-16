namespace leetcode_1323;

class Program
{
    /// <summary>
    /// 1323. Maximum 69 Number
    /// https://leetcode.com/problems/maximum-69-number/description/?envType=daily-question&envId=2025-08-16
    /// 1323. 6 和 9 组成的最大数字
    /// https://leetcode.cn/problems/maximum-69-number/description/?envType=daily-question&envId=2025-08-16
    ///
    /// 題目中文說明：給定一個只包含數字 6 和 9 的正整數 num，最多可以將一個數字由 6 變為 9 或由 9 變為 6，
    /// 返回可取得的最大數字。
    ///
    /// 範例：輸入 9669，將第一個 6 變為 9 得到 9969，為最大可能值，回傳 9969。
    ///
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料
        int[] tests = new int[] {9669, 9996, 9999, 6666, 6, 9};

        var p = new Program();
        foreach (var t in tests)
        {
            int result = p.Maximum69Number(t);
            Console.WriteLine($"輸入: {t} -> 最大值: {result}");
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
}
