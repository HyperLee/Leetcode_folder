namespace leetcode_3370;

class Program
{
    /// <summary>
    /// 3370. Smallest Number With All Set Bits
    /// https://leetcode.com/problems/smallest-number-with-all-set-bits/description/?envType=daily-question&envId=2025-10-29
    /// 3370. 僅含置位位的最小整數
    /// https://leetcode.cn/problems/smallest-number-with-all-set-bits/description/?envType=daily-question&envId=2025-10-29
    ///
    /// 題目描述（中文）：給定一個正整數 n，回傳最小的整數 x（x >= n），使得 x 的二進位表示中只含有置位（即所有位元均為 1）。
    /// 也就是尋找第一個不小於 n 的數，其二進位表示形如 1、11、111、1111...（全部為 1）。
    /// 例如：
    ///   - n = 5 (0b101)，答案為 7 (0b111)
    ///   - n = 8 (0b1000)，答案為 15 (0b1111)
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("=== LeetCode 3370: 僅含置位位的最小整數 ===\n");

        var solution = new Program();

        // 測試案例
        int[] testCases = { 5, 10, 3, 8, 1, 15, 31, 100 };

        foreach (int n in testCases)
        {
            int result1 = solution.SmallestNumber(n);
            int result2 = solution.SmallestNumber2(n);

            Console.WriteLine($"輸入: n = {n}");
            Console.WriteLine($"  二進位表示: {Convert.ToString(n, 2)}");
            Console.WriteLine($"  方法一(找規律)結果: {result1} (二進位: {Convert.ToString(result1, 2)})");
            Console.WriteLine($"  方法二(位元長度)結果: {result2} (二進位: {Convert.ToString(result2, 2)})");
            Console.WriteLine($"  結果是否一致: {(result1 == result2 ? "✓" : "✗")}");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 方法一:找規律
    /// 
    /// 思路與演算法:
    /// 枚舉僅包含置位位的整數序列為: 1, 3, 7, 15, 31, 63, ...
    /// 對應的二進位表示為: 0b1, 0b11, 0b111, 0b1111, 0b11111, 0b111111, ...
    /// 
    /// 規律發現:
    /// 這個數列滿足遞推關係: x_{k+1} = x_k * 2 + 1
    /// 在二進位運算上相當於在右側新增一個 1 位元
    /// 例如: 3(0b11) * 2 + 1 = 7(0b111)
    ///      7(0b111) * 2 + 1 = 15(0b1111)
    /// 
    /// 演算法步驟:
    /// 1. 初始化 x = 1(第一個僅含置位位的整數)
    /// 2. 重複執行 x = x * 2 + 1,不斷生成下一個僅含置位位的整數
    /// 3. 當 x >= n 時,x 即為所求的最小整數
    /// 
    /// 時間複雜度: O(log n),因為 x 以指數速度增長
    /// 空間複雜度: O(1)
    /// </summary>
    /// <param name="n">給定的正整數</param>
    /// <returns>不小於 n 且二進位表示全為 1 的最小整數</returns>
    public int SmallestNumber(int n)
    {
        // 初始化 x 為 1,這是第一個僅含置位位的整數(二進位: 0b1)
        int x = 1;

        // 持續生成僅含置位位的整數序列: 1, 3, 7, 15, 31, ...
        // 迴圈條件: x < n,表示當前的 x 還不夠大
        while (x < n)
        {
            // 遞推公式: x = x * 2 + 1
            // 等價於位元運算: x = (x << 1) | 1
            // 效果: 在二進位表示的右側新增一個 1
            // 例如: 3(0b11) -> 7(0b111), 7(0b111) -> 15(0b1111)
            x = x * 2 + 1;
        }

        // 當 x >= n 時,x 就是我們要找的答案
        return x;
    }

    /// <summary>
    /// 方法二:位元長度計算法
    /// 
    /// 題意:回傳 >= n 且二進位全為 1 的最小整數
    /// 
    /// 思路與演算法:
    /// 計算 n 的二進位長度 m,答案的二進位長度至少是 m
    /// 由於長為 m 的全為 1 的二進位數必定 >= n,因此答案的二進位長度就是 m
    /// 
    /// 數學公式:答案 = 2^m - 1
    /// 
    /// 原理解釋:
    /// - 1 << m 表示將 1 左移 m 位,得到 10...0(1 後面跟著 m 個 0)
    /// - (1 << m) - 1 表示減 1,得到 11...1(m 個 1)
    /// 
    /// 範例:
    /// - n = 5(0b101),二進位長度 m = 3
    ///   答案 = (1 << 3) - 1 = 8 - 1 = 7(0b111)
    /// - n = 8(0b1000),二進位長度 m = 4
    ///   答案 = (1 << 4) - 1 = 16 - 1 = 15(0b1111)
    /// 
    /// 時間複雜度: O(log n),計算二進位長度需要 O(log n)
    /// 空間複雜度: O(log n),Convert.ToString 需要建立字串
    /// </summary>
    /// <param name="n">給定的正整數</param>
    /// <returns>不小於 n 且二進位表示全為 1 的最小整數</returns>
    public int SmallestNumber2(int n)
    {
        // 計算 n 的二進位表示的長度
        // 例如: n = 5(0b101) -> bitLength = 3
        //      n = 8(0b1000) -> bitLength = 4
        int bitLength = Convert.ToString(n, 2).Length;

        // 使用公式 2^m - 1 計算答案
        // (1 << bitLength) 相當於 2^bitLength,即 10...0(1 後面跟著 bitLength 個 0)
        // 減 1 後得到 11...1(bitLength 個 1)
        return (1 << bitLength) - 1;
    }
}
