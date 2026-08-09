namespace leetcode_1432;

class Program
{
    /// <summary>
    /// <para>
    /// 1432. Max Difference You Can Get From Changing an Integer
    /// https://leetcode.com/problems/max-difference-you-can-get-from-changing-an-integer/description/
    ///
    /// You are given an integer num. You will apply the following steps to num two separate times:
    /// - Pick a digit x (0 &lt;= x &lt;= 9).
    /// - Pick another digit y (0 &lt;= y &lt;= 9). Note y can be equal to x.
    /// - Replace all the occurrences of x in the decimal representation of num by y.
    ///
    /// Let a and b be the two results from applying the operation to num independently.
    /// Return the max difference between a and b.
    /// Note that neither a nor b may have any leading zeros, and must not be 0.
    ///
    /// Example 1:
    /// Input: num = 555
    /// Output: 888
    /// Explanation: The first time pick x = 5 and y = 9 and store the new integer in a.
    /// The second time pick x = 5 and y = 1 and store the new integer in b.
    /// We have now a = 999 and b = 111 and max difference = 888.
    ///
    /// Example 2:
    /// Input: num = 9
    /// Output: 8
    /// Explanation: The first time pick x = 9 and y = 9 and store the new integer in a.
    /// The second time pick x = 9 and y = 1 and store the new integer in b.
    /// We have now a = 9 and b = 1 and max difference = 8.
    ///
    /// Constraints:
    /// - 1 &lt;= num &lt;= 10^8
    /// </para>
    /// <para>
    /// 1432. 改變一個整數能得到的最大差值
    /// https://leetcode.cn/problems/max-difference-you-can-get-from-changing-an-integer/description/
    ///
    /// 給定一個整數 num。你將對 num 分別執行兩次下列步驟：
    /// - 選擇一個數字 x（0 &lt;= x &lt;= 9）。
    /// - 選擇另一個數字 y（0 &lt;= y &lt;= 9）。請注意，y 可以等於 x。
    /// - 將 num 的十進位表示中所有出現的 x 替換為 y。
    ///
    /// 令 a 與 b 分別為獨立對 num 執行此操作後得到的兩個結果。
    /// 回傳 a 與 b 之間的最大差值。
    /// 請注意，a 與 b 都不得有前導零，也不得為 0。
    ///
    /// 範例 1：
    /// 輸入：num = 555
    /// 輸出：888
    /// 解釋：第一次選擇 x = 5、y = 9，並將新整數存入 a。
    /// 第二次選擇 x = 5、y = 1，並將新整數存入 b。
    /// 此時 a = 999、b = 111，最大差值 = 888。
    ///
    /// 範例 2：
    /// 輸入：num = 9
    /// 輸出：8
    /// 解釋：第一次選擇 x = 9、y = 9，並將新整數存入 a。
    /// 第二次選擇 x = 9、y = 1，並將新整數存入 b。
    /// 此時 a = 9、b = 1，最大差值 = 8。
    ///
    /// 限制條件：
    /// - 1 &lt;= num &lt;= 10^8
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main (string [] args)
    {
        // 測試資料
        int[] testCases = {555, 9, 123456, 10000, 9288, 110105, 9000};
        var prog = new Program();
        foreach (var num in testCases)
        {
            int diff1 = prog.MaxDiff(num);
            int diff2 = prog.MaxDiff2(num);
            Console.WriteLine($"num = {num}, MaxDiff = {diff1}, MaxDiff2 = {diff2}");
        }
    }

    /// <summary>
    ///ref:https://leetcode.cn/problems/max-difference-you-can-get-from-changing-an-integer/solutions/514358/gai-bian-yi-ge-zheng-shu-neng-de-dao-de-0byhw/?envType=daily-question&envId=2025-06-15
    /// 
    /// 枚舉所有可能的數字替換，並計算最大差值。
    /// 解題說明:
    /// 本題需找出將 num 中某個數字全部替換成另一個數字後，能得到的最大與最小值，
    /// 並回傳兩者的差。透過枚舉所有 x, y 組合，並檢查替換後的數字是否合法（無前導零），
    /// 最後取最大與最小值的差即為答案。
    /// </summary>
    /// <param name="num"> 輸入的整數 </param>
    /// <returns > 最大差值 </returns>
    public int MaxDiff (int num)
    {
        //change 函式: 將 num 中所有 x 替換為 y，回傳替換後的字串
        Func<int, int, string> change = (x, y) =>
        {
            var numStr = num.ToString (); // 將整數轉為字串
            // 逐位檢查，若等於 x 則替換為 y，否則保留原數字
            return new string (numStr.Select (digit => (digit - '0') == x ? (char)('0' + y) : digit).ToArray ());
        };

        int minNum = num; // 初始化最小值為原始數字
        int maxNum = num; // 初始化最大值為原始數字

        // 枚舉所有 x, y 組合 (0~9)
        for (int x = 0; x < 10; ++x)
        {
            for (int y = 0; y < 10; ++y)
            {
                string res = change (x, y); // 執行替換
                // 檢查是否有前導零，若有則跳過
                if (res [0] != '0')
                {
                    int res_i = int.Parse (res); // 轉回整數
                    minNum = Math.Min (minNum, res_i); // 更新最小值
                    maxNum = Math.Max (maxNum, res_i); // 更新最大值
                }
            }
        }
        // 回傳最大與最小值的差
        return maxNum - minNum;
    }

    /// <summary>
    /// MaxDiff2: 使用貪心法求最大差值
    /// 解題說明：
    /// 1. 最大值：將 num 中第一個不是 '9' 的數字全部替換為 '9'，其餘不變，這樣能讓數字最大。
    /// 2. 最小值：
    ///   - 若首位不是 '1'，則將首位全部替換為 '1'（避免前導零，且盡量小）。
    ///   - 否則，將第一個不是 '0' 且不是首位的數字全部替換為 '0'，其餘不變。
    /// 3. 回傳最大值與最小值的差。
    /// 此方法利用數字特性，直接找出最有利於極大化與極小化的替換策略，效率高於暴力枚舉。
    /// </summary>
    /// <param name="num"> 輸入的整數 </param>
    /// <returns > 最大差值 </returns>
    public int MaxDiff2 (int num)
    {
        // 將字串 s 中所有 x 替換為 y
        void Replace (ref string s, char x, char y)
        {
            s = s.Replace (x, y);
        }

        string minNum = num.ToString (); // 最小值字串
        string maxNum = num.ToString (); // 最大值字串

        // 最大值策略：將第一個不是 '9' 的數字全部替換為 '9'
        foreach (char digit in maxNum)
        {
            if (digit != '9')
            {
                Replace (ref maxNum, digit, '9');
                break; // 只需替換一次
            }
        }

        // 最小值策略
        for (int i = 0; i < minNum.Length; i++)
        {
            char digit = minNum [i];
            // 檢查首位數字
            if (i == 0)
            {
                // 首位不是 '1'，則全部替換為 '1'
                if (digit != '1')
                {
                    Replace (ref minNum, digit, '1');
                    break;
                }
            }
            else
            {
                // 非首位且不是 '0' 且不是首位數字，全部替換為 '0'
                if (digit != '0' && digit != minNum [0])
                {
                    Replace (ref minNum, digit, '0');
                    break;
                }
            }
        }
        // 回傳最大值與最小值的差
        return int.Parse (maxNum) - int.Parse (minNum);
    }
}
