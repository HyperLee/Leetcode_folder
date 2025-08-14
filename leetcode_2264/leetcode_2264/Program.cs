namespace leetcode_2264;

class Program
{
    /// <summary>
    /// 2264. Largest 3-Same-Digit Number in String
    /// 題目：給定字串 num 代表一個大整數，若一個整數有下列條件則稱為 good：
    /// - 它是 num 的長度為 3 的子字串
    /// - 它只包含單一數字
    /// 回傳最大的 good 整數（字串形式），若不存在回傳空字串 ""。
    /// 注意：子字串允許前導零。
    ///
    /// 解法說明：線性掃描字串，檢查每個長度為 3 的子字串是否三個字元相同，
    /// 若相同則與目前記錄的最大值比較更新。時間複雜度 O(n)，空間 O(1)。
    /// </summary>
    /// <param name="args">命令列參數（未使用）</param>
    static void Main(string[] args)
    {
        // 範例測試
        var tests = new[]
        {
            (input: "6777133339", expected: "777"),
            (input: "2300019", expected: "000"),
            (input: "42352338", expected: ""),
            (input: "000", expected: "000"),
            (input: "11122111", expected: "111")
        };

        foreach (var (input, expected) in tests)
        {
            var res1 = LargestGoodInteger(input);
            var res2 = LargestGoodInteger2(input);
            bool equal = res1 == res2;
            Console.WriteLine($"input={input} => LargestGoodInteger={res1}, LargestGoodInteger2={res2}, equal={equal} (expected={expected})");
        }
    }

    /// <summary>
    /// 返回字串中最大的長度為 3 且三個字元相同的子字串，若無則回傳空字串。
    ///
    /// 解題說明：
    /// - 我們需要檢查所有長度為 3 的子字串，判斷該子字串是否由同一個數字構成（例如 "777" 或 "000"）。
    /// - 因為數字字元是 '0' 到 '9'，可以直接以字元比較大小來決定哪個三位數最大（字元的 ASCII 值在數字範圍內與數值大小一致）。
    /// - 採用線性掃描：對每個索引 i（0..n-3），比較 num[i], num[i+1], num[i+2] 是否相等；若相等，更新當前最大字元。
    /// - 最終若找到最大字元 x，回傳由該字元重複三次的字串（例如 new string(x, 3)），否則回傳空字串。
    ///
    /// 時間複雜度：O(n)，只需一次線性掃描。
    /// 空間複雜度：O(1)，僅使用常數額外空間（記錄目前最大字元）。
    /// </summary>
    /// <param name="num">輸入字串，代表一個大整數（允許前導零）</param>
    /// <returns>最大的 good 整數（字串）或空字串</returns>
    static string LargestGoodInteger(string num)
    {
        if (string.IsNullOrEmpty(num) || num.Length < 3)
            return string.Empty;

        // 使用 char 來比較，並追蹤最大的字元（'0'..'9'）
        char maxDigit = '\0';

        for (int i = 0; i + 2 < num.Length; i++)
        {
            // 取出三個相鄰字元
            char a = num[i];
            char b = num[i + 1];
            char c = num[i + 2];

            // 若三者相等，代表這是一個 good 整數（例如 "222"）
            if (a == b && b == c)
            {
                // 以字元大小比較決定較大的數字。
                // 例如 '7' > '6'，代表 "777" > "666"。
                if (a > maxDigit)
                    maxDigit = a;
            }
        }

        return maxDigit == '\0' ? string.Empty : new string(maxDigit, 3);
    }

    /// <summary>
    /// 使用枚舉（遍歷）方式找出字串中最大的「長度為 3 且三個字元相同」的子字串（good 整數）。
    ///
    /// 解題說明：
    /// - 枚舉所有長度為 3 的子字串 num[i..i+2]（從左到右）。
    /// - 對每個子字串，檢查三個字元是否相等；若相等，該子字串為一個 good 整數（例如 "777"、"000"）。
    /// - 將該子字串轉為整數比較並更新目前紀錄的最大值。因為題目允許前導零，結果需以三位數字字串回傳（例如 "000"）。
    ///
    /// 時間複雜度：O(n)，只需一次線性掃描所有長度為 3 的子字串。
    /// 空間複雜度：O(1)，僅使用常數額外空間來儲存目前最大值。
    /// </summary>
    /// <param name="num">輸入的數字字串（允許前導零）。</param>
    /// <returns>若存在 good 整數則回傳其三位字串表示；否則回傳空字串。</returns>
    public static string LargestGoodInteger2(string num)
    {
        // 輸入檢查：若字串為 null 或長度小於 3，直接回傳空字串（無法形成長度為 3 的子字串）。
        if (string.IsNullOrEmpty(num) || num.Length < 3)
            return string.Empty;

        int n = num.Length;
        // 使用 -1 表示尚未找到任何 good 子字串；若找到則以數字形式記錄其整數值（例如 "777" 會記為 777）。
        int res = -1;

        for (int i = 0; i < n - 2; i++)
        {
            // 若三個相鄰字元相同，則該子字串為 good 整數
            if (num[i] == num[i + 1] && num[i + 1] == num[i + 2])
            {
                // 將長度為 3 的子字串轉為整數並與目前最大值比較更新。
                // 注意：對於像 "000" 的情形，int.Parse 會得到 0，因此後面要特別處理回傳三位格式。
                res = Math.Max(res, int.Parse(num.Substring(i, 3)));
            }
        }

        // 若找到的最大值為 -1，代表沒有任何 good 子字串
        if (res == -1)
        {
            return string.Empty;
        }

        // 若 res == 0，代表最大 good 為 "000"，直接回傳三位字串
        if (res == 0)
        {
            return "000";
        }

        // 將整數格式化為三位字串（前導零補齊），例如 7 -> "007"（但此題中不會出現單一數字轉成的情形，仍保留作為健全處理）。
        return res.ToString("D3");
    }
}
