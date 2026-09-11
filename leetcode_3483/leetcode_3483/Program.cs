namespace leetcode_3483;

class Program
{
    /// <summary>
    /// 3483. Unique 3-Digit Even Numbers
    /// https://leetcode.com/problems/unique-3-digit-even-numbers/description/
    /// 3483. 不同三位偶数的数目
    /// https://leetcode.cn/problems/unique-3-digit-even-numbers/description/
    ///
    /// English:
    /// You are given an array of digits called digits. Your task is to determine the number of distinct three-digit even numbers that can be formed using these digits.
    ///
    /// Note: Each copy of a digit can only be used once per number, and there may not be leading zeros.
    ///
    /// Traditional Chinese（繁體中文）：
    /// 給定一個名為 digits 的數字陣列。你的任務是判斷使用這些數字可以組成多少個互不相同的三位數偶數。
    ///
    /// 注意：每個數字的每個副本在同一個數字中只能使用一次，且數字的開頭不能是 0。
    /// </summary>
    /// <param name="args"></param>
    /// <returns>所有固定案例都通過時回傳 0，否則回傳非零值。</returns>
    /// <remarks>
    /// 程式不讀取互動輸入，而是執行固定案例，並讓三種解法分別計算後與預期結果比較。
    /// </remarks>
    static int Main(string[] args)
    {
        (string Name, int[] Digits, int Expected)[] testCases =
        [
            ("官方案例 1", [1, 2, 3, 4], 12),
            ("官方案例 2：重複數字", [0, 2, 2], 2),
            ("官方案例 3：全部相同", [6, 6, 6], 1),
            ("官方案例 4：沒有偶數尾數", [1, 3, 5], 0),
            ("前導零與不同尾數", [0, 1, 2], 3),
            ("重複零與前導零", [0, 0, 1], 1),
            ("所有數字各一次", [0, 1, 2, 3, 4, 5, 6, 7, 8, 9], 328),
            ("多組重複數字", [1, 2, 2, 8, 8], 14)
        ];

        Program solver = new();
        int passed = 0;

        foreach ((string name, int[] digits, int expected) in testCases)
        {
            if (RunTestCase(solver, name, digits, expected))
            {
                passed++;
            }
        }

        int failed = testCases.Length - passed;
        Console.WriteLine($"總結：{passed}/{testCases.Length} 通過，{failed} 個失敗。");
        return failed == 0 ? 0 : 1;
    }

    /// <summary>
    /// 執行一組固定案例，並比較三種解法的結果是否都符合預期。
    /// </summary>
    /// <param name="solver">用來執行三種解法的同一個解題實例。</param>
    /// <param name="caseName">案例名稱。</param>
    /// <param name="digits">只包含 0 到 9 的輸入數字陣列，長度介於 3 到 10。</param>
    /// <param name="expected">此案例應得到的不同三位偶數數量。</param>
    /// <returns>三種解法都得到預期結果時回傳 true，否則回傳 false。</returns>
    private static bool RunTestCase(Program solver, string caseName, int[] digits, int expected)
    {
        // 每個方法使用自己的輸入複本，讓測試不受其他方法的排序或資料處理影響。
        int result1 = solver.TotalNumbers(digits.ToArray());
        int result2 = solver.TotalNumbers2(digits.ToArray());
        int result3 = solver.TotalNumbers3(digits.ToArray());
        bool passed = result1 == expected && result2 == expected && result3 == expected;

        string input = $"[{string.Join(", ", digits)}]";
        string status = passed ? "PASS" : "FAIL";
        Console.WriteLine(
            $"[{caseName}] digits = {input}, expected = {expected}, " +
            $"TotalNumbers = {result1}, TotalNumbers2 = {result2}, " +
            $"TotalNumbers3 = {result3} => {status}");

        return passed;
    }

    /// <summary>
    /// 使用三重枚舉選出百位、十位與個位，計算符合條件的不同三位偶數數量。
    /// 輸入必須是長度 3 到 10、元素介於 0 到 9 的數字陣列；輸出為可形成的不同數字數量。
    /// </summary>
    /// <param name="digits">可重複且每個元素介於 0 到 9 的數字陣列。</param>
    /// <returns>符合不使用重複索引、無前導零且為偶數等條件的不同三位數數量。</returns>
    public int TotalNumbers(int[] digits)
    {
        int n = digits.Length;
        bool[] seen = new bool[1000];
        int answer = 0;

        for (int i = 0; i < n; i++)
        {
            if (digits[i] == 0)
            {
                continue; // 百位不能是 0。
            }

            for (int j = 0; j < n; j++)
            {
                if (j == i)
                {
                    continue; // 同一個索引不能重複使用。
                }

                for (int k = 0; k < n; k++)
                {
                    if (k == i || k == j || digits[k] % 2 != 0)
                    {
                        continue; // 個位必須是偶數，且三個索引必須互異。
                    }

                    int number = digits[i] * 100 + digits[j] * 10 + digits[k];
                    if (!seen[number])
                    {
                        seen[number] = true;
                        answer++;
                    }
                }
            }
        }

        return answer;
    }

    /// <summary>
    /// 先枚舉偶數個位數，再以頻率統計與組合數學計算百位和十位的填法。
    /// 輸入必須是長度 3 到 10、元素介於 0 到 9 的數字陣列；輸出為可形成的不同三位偶數數量。
    /// </summary>
    /// <param name="digits">可重複且每個元素介於 0 到 9 的數字陣列。</param>
    /// <returns>符合題目條件的不同三位偶數數量。</returns>
    public int TotalNumbers2(int[] digits)
    {
        int[] count = new int[10];
        foreach (int digit in digits)
        {
            count[digit]++;
        }

        int kinds = 0;
        int nonZeros = 0;
        int singles = 0;
        for (int digit = 0; digit < 10; digit++)
        {
            if (count[digit] == 0)
            {
                continue;
            }

            kinds++;
            if (digit > 0)
            {
                nonZeros++;
                if (count[digit] == 1)
                {
                    singles++;
                }
            }
        }

        int answer = 0;
        for (int lastDigit = 0; lastDigit < 10; lastDigit += 2)
        {
            int copies = count[lastDigit];
            if (copies == 0)
            {
                continue;
            }

            // 個位消耗一份後，計算十位與百位仍可使用的數字種類。
            int tensKinds = kinds - (copies == 1 ? 1 : 0);
            int hundredsKinds = nonZeros - (lastDigit > 0 && copies == 1 ? 1 : 0);

            // 若非零數字只剩一份，不能同時放在百位與十位。
            int remainingSingles = singles;
            if (lastDigit > 0)
            {
                if (copies == 1)
                {
                    remainingSingles--;
                }
                else if (copies == 2)
                {
                    remainingSingles++;
                }
            }

            answer += tensKinds * hundredsKinds - remainingSingles;
        }

        return answer;
    }

    /// <summary>
    /// 透過排序、DFS 與回溯列舉三位數排列，並在同一層跳過重複數字。
    /// 輸入必須是長度 3 到 10、元素介於 0 到 9 的數字陣列；輸出為可形成的不同三位偶數數量。
    /// </summary>
    /// <param name="digits">可重複且每個元素介於 0 到 9 的數字陣列。</param>
    /// <returns>符合題目條件的不同三位偶數數量。</returns>
    public int TotalNumbers3(int[] digits)
    {
        // 排序複本後，同一層可以利用相鄰相等值跳過重複分支。
        int[] sortedDigits = digits.ToArray();
        Array.Sort(sortedDigits);
        bool[] used = new bool[sortedDigits.Length];
        return Dfs(sortedDigits, used, 3, 0);
    }

    /// <summary>
    /// 建立剩餘長度的數字排列，回傳其中符合三位偶數條件的葉節點數量。
    /// </summary>
    /// <param name="digits">已排序的輸入數字陣列。</param>
    /// <param name="used">記錄每個索引是否已在目前路徑使用。</param>
    /// <param name="remaining">目前還需要選出的位數。</param>
    /// <param name="number">目前遞迴路徑組成的部分數字。</param>
    /// <returns>目前分支可形成的合法三位偶數數量。</returns>
    private int Dfs(int[] digits, bool[] used, int remaining, int number)
    {
        if (remaining == 0)
        {
            // 三位數不能以 0 開頭，且最後一位必須是偶數。
            return number >= 100 && number <= 999 && number % 2 == 0 ? 1 : 0;
        }

        int answer = 0;
        for (int i = 0; i < digits.Length; i++)
        {
            if (used[i])
            {
                continue;
            }

            // 同一層的相同數字只保留第一個未使用索引，避免重複排列。
            if (i > 0 && digits[i] == digits[i - 1] && !used[i - 1])
            {
                continue;
            }

            used[i] = true;
            answer += Dfs(digits, used, remaining - 1, number * 10 + digits[i]);
            used[i] = false; // 回溯，讓下一個分支重新使用此索引。
        }

        return answer;
    }
}