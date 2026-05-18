namespace leetcode_066;

class Program
{
    /// <summary>
    /// 66. Plus One
    /// https://leetcode.com/problems/plus-one/description/
    /// 66. 加一
    /// https://leetcode.cn/problems/plus-one/description/
    /// 
    /// English:
    /// You are given a large integer represented as an integer array digits, where each digits[i] is the ith
    /// digit of the integer. The digits are ordered from most significant to least significant in left-to-right
    /// order. The large integer does not contain any leading 0's.
    ///
    /// Increment the large integer by one and return the resulting array of digits.
    ///
    /// Traditional Chinese:
    /// 給定一個以整數陣列 digits 表示的大整數，其中每個 digits[i] 是該整數的第 i 個數字。這些數字依照
    /// 從最高位到最低位的順序由左至右排列。這個大整數不包含任何前導 0。
    ///
    /// 將這個大整數加一，並回傳結果的數字陣列。
    /// </summary>
    /// <remarks>
    /// 主要進入點會執行多組範例資料，確認兩種 Plus One 解法都能得到預期結果。
    /// </remarks>
    /// <param name="args">命令列參數；此範例程式未使用。</param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        int[][] testCases =
        {
            new int[] { 1, 2, 3 },
            new int[] { 4, 3, 2, 1 },
            new int[] { 9 },
            new int[] { 9, 9, 9 },
            new int[] { 1, 2, 9, 9 },
        };
        int[][] expectedResults =
        {
            new int[] { 1, 2, 4 },
            new int[] { 4, 3, 2, 2 },
            new int[] { 1, 0 },
            new int[] { 1, 0, 0, 0 },
            new int[] { 1, 3, 0, 0 },
        };

        Console.WriteLine("LeetCode 66 - Plus One");
        Console.WriteLine();

        for (int i = 0; i < testCases.Length; i++)
        {
            int[] input = testCases[i];
            int[] expected = expectedResults[i];
            int[] plusOneResult = solution.PlusOne((int[])input.Clone());
            int[] plusOne2Result = solution.PlusOne2((int[])input.Clone());

            Console.WriteLine($"Case {i + 1}");
            Console.WriteLine($"Input:    {FormatDigits(input)}");
            Console.WriteLine($"Expected: {FormatDigits(expected)}");
            Console.WriteLine($"PlusOne:  {FormatDigits(plusOneResult)} {FormatStatus(plusOneResult, expected)}");
            Console.WriteLine($"PlusOne2: {FormatDigits(plusOne2Result)} {FormatStatus(plusOne2Result, expected)}");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 解法一：找出最長的後綴 9，從最低位往前尋找第一個不為 9 的數字。
    /// 解題概念是只處理會受加一影響的尾端位數：若找到非 9，該位加一並將右側所有 9 清為 0；
    /// 若全部位數都是 9，則建立長度加一的新陣列並將最高位設為 1。
    /// </summary>
    /// <param name="digits">以最高位到最低位排序的數字陣列；長度至少為 1，每個元素介於 0 到 9，且不含前導 0。</param>
    /// <returns>加一後的數字陣列；多數情況會原地修改並回傳輸入陣列，全部為 9 時會回傳新陣列。</returns>
    public int[] PlusOne(int[] digits)
    {
        int n = digits.Length;
        for (int i = n - 1; i >= 0; i--)
        {
            if (digits[i] != 9)
            {
                digits[i]++;

                // 右側原本都是 9，加一進位後會全部變成 0。
                for (int j = i + 1; j < n; j++)
                {
                    digits[j] = 0;
                }

                return digits;
            }
        }

        // 所有位數皆為 9 時，最高位會新增 1，其他位數預設為 0。
        int[] res = new int[n + 1];
        res[0] = 1;
        return res;
    }

    /// <summary>
    /// 解法二：模擬直式加法，從最低位開始逐位加一並處理進位。
    /// 解題概念是目前位數加一後只保留個位數；若結果不是 0，代表沒有進位可直接回傳；
    /// 若一路進位到最高位之外，則建立長度加一的新陣列並將最高位設為 1。
    /// </summary>
    /// <param name="digits">以最高位到最低位排序的數字陣列；長度至少為 1，每個元素介於 0 到 9，且不含前導 0。</param>
    /// <returns>加一後的數字陣列；多數情況會原地修改並回傳輸入陣列，全部為 9 時會回傳新陣列。</returns>
    public int[] PlusOne2(int[] digits)
    {
        // 正常數學計算由低位往高位，遇到不需進位的位數即可停止。
        for (int i = digits.Length - 1; i >= 0; i--)
        {
            digits[i]++;
            digits[i] %= 10;

            if (digits[i] != 0)
            {
                return digits;
            }
        }

        // 例如 99 或 999，最後仍有進位，需要多補一個最高位 1。
        digits = new int[digits.Length + 1];
        digits[0] = 1;

        return digits;
    }

    /// <summary>
    /// 將數字陣列格式化為範例輸出用的字串。
    /// </summary>
    /// <param name="digits">要顯示的數字陣列。</param>
    /// <returns>以中括號與逗號呈現的陣列字串。</returns>
    private static string FormatDigits(int[] digits)
    {
        return $"[{string.Join(", ", digits)}]";
    }

    /// <summary>
    /// 比對實際輸出與預期輸出，產生範例測試狀態。
    /// </summary>
    /// <param name="actual">解法實際回傳的數字陣列。</param>
    /// <param name="expected">範例預期得到的數字陣列。</param>
    /// <returns>兩個陣列完全相同時回傳 PASS，否則回傳 FAIL。</returns>
    private static string FormatStatus(int[] actual, int[] expected)
    {
        return AreSameDigits(actual, expected) ? "PASS" : "FAIL";
    }

    /// <summary>
    /// 逐位比較兩個數字陣列是否相同。
    /// </summary>
    /// <param name="actual">解法實際回傳的數字陣列。</param>
    /// <param name="expected">範例預期得到的數字陣列。</param>
    /// <returns>長度與每個位置的數字都相同時回傳 true，否則回傳 false。</returns>
    private static bool AreSameDigits(int[] actual, int[] expected)
    {
        if (actual.Length != expected.Length)
        {
            return false;
        }

        for (int i = 0; i < actual.Length; i++)
        {
            if (actual[i] != expected[i])
            {
                return false;
            }
        }

        return true;
    }
}
