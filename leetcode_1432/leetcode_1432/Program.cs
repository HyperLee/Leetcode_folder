namespace leetcode_1432;

class Program
{
    /// <summary>
    /// 1432. Max Difference You Can Get From Changing an Integer
    /// https://leetcode.com/problems/max-difference-you-can-get-from-changing-an-integer/description/?envType=daily-question&envId=2025-06-15
    /// 1432. 改變一個整數能得到的最大差值
    /// https://leetcode.cn/problems/max-difference-you-can-get-from-changing-an-integer/description/?envType=daily-question&envId=2025-06-15
    /// 題目描述:
    /// 給定一個整數 num，對 num 進行兩次如下操作：
    /// 1. 選擇一個數字 x (0 <= x <= 9)。
    /// 2. 選擇另一個數字 y (0 <= y <= 9)，y 可以等於 x。
    /// 3. 將 num 中所有出現的 x 替換為 y。
    /// 兩次操作分別得到 a 和 b，返回 a 和 b 的最大差值。
    /// 注意：a 和 b 不能有前導零，且不能為 0。
    /// 
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
