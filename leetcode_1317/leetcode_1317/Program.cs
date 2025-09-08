namespace leetcode_1317;

class Program
{
    /// <summary>
    /// 1317. Convert Integer to the Sum of Two No-Zero Integers
    /// https://leetcode.com/problems/convert-integer-to-the-sum-of-two-no-zero-integers/description/?envType=daily-question&envId=2025-09-08
    /// 1317. 将整数转换为两个无零整数的和
    /// https://leetcode.cn/problems/convert-integer-to-the-sum-of-two-no-zero-integers/description/?envType=daily-question&envId=2025-09-08
    /// 
    /// 題目描述（繁體中文）：
    /// 無零整數 (No-Zero integer) 是指其十進位表示中不包含任何數字 0 的正整數。
    /// 給定一個整數 n，回傳一個包含兩個整數 [a, b] 的陣列，其中：
    /// - a 與 b 均為無零整數。
    /// - a + b = n
    /// 題目的測資保證至少存在一組有效解。若存在多種有效解，回傳任一組即可。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 執行內建測試資料
        RunTests();
    }

    /// <summary>
    /// 執行一些範例測資並印出 GetNoZeroIntegers 的回傳結果
    /// </summary>
    static void RunTests()
    {
        // ...existing code...
        var program = new Program();
        int[] tests = new int[] {2, 11, 100, 1010, 109};
        foreach (var n in tests)
        {
            var res = program.GetNoZeroIntegers(n);
            if (res.Length == 2)
            {
                Console.WriteLine($"n={n} -> [{res[0]}, {res[1]}]");
            }
            else
            {
                Console.WriteLine($"n={n} -> No result");
            }
        }
    }

    /// <summary>
    /// <summary>
    /// 找到兩個無零整數 a 與 b 使 a + b == n。
    /// 以暴力法從 a = 1 開始嘗試，直到找到第一組同時不包含數字 '0' 的配對。
    /// 回傳長度為 2 的陣列 [a, b]（若找到），否則回傳空陣列。
    /// </summary>
    /// <param name="n">目標整數（正整數）。</param>
    /// <returns>
    /// 長度為 2 的陣列 [a, b]，代表找到的無零整數配對；若無解則回傳空陣列。
    /// </returns>
    /// <remarks>
    /// - 輸入保證至少有一組解（題目保證）。
    /// - 演算法：簡單暴力掃描 a 從 1 到 n-1，檢查 a 與 b 的十進位表示是否包含 '0'。
    /// - 時間複雜度：O(n * d)，其中 d 為轉字串判斷時的位數（通常 d = O(log n)）。
    /// - 空間複雜度：O(1)（回傳固定大小陣列）。
    /// </remarks>
    public int[] GetNoZeroIntegers(int n)
    {
        for (int a = 1; a < n; a++)
        {
            int b = n - a;
            // 將整數轉為字串並檢查是否包含 '0'
            // 這裡使用 ToString() 再 Contains('0') 為最直接的實作，易讀且對此題足夠。
            if (!a.ToString().Contains('0') && !b.ToString().Contains('0'))
            {
                // 找到第一個符合條件的配對，依題目可以回傳任一組解
                return new int[] { a, b };
            }
        }
        // 理論上題目保證有解，此行為防守式回傳（若輸入不符合題目假設時仍可安全回傳）
        return Array.Empty<int>();
    }
}
