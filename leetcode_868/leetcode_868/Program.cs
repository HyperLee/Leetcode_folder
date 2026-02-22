namespace leetcode_868;

class Program
{
    /// <summary>
    /// 868. Binary Gap
    /// https://leetcode.com/problems/binary-gap/description/?envType=daily-question&envId=2026-02-22
    /// 868. 二進位間距
    /// https://leetcode.cn/problems/binary-gap/description/?envType=daily-question&envId=2026-02-22
    /// 
    /// 
    /// Description / 描述：
    /// Given a positive integer n, find and return the longest distance between any two adjacent 1's in the binary representation of n.
    /// If there are no two adjacent 1's, return 0.
    ///
    /// Two 1's are adjacent if there are only 0's separating them (possibly no 0's).
    /// The distance between two 1's is the absolute difference between their bit positions.
    /// For example, the two 1's in "1001" have a distance of 3.
    ///
    /// 給定一個正整數 n，找出並回傳其二進位表示中任意兩個相鄰的 1 之間的最長距離。
    /// 如果不存在兩個相鄰的 1，則回傳 0。
    ///
    /// 當兩個 1 之間只有 0（可能沒有 0）相隔時，視為相鄰。
    /// 兩個 1 之間的距離為它們位元位置的絕對差。
    /// 例如，在 "1001" 中的兩個 1 的距離為 3。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試資料 1：n = 22 (二進位: 10110) => 相鄰兩個 1 的最大間距 = 2
        // 位元分布: bit0=0, bit1=1, bit2=1, bit3=0, bit4=1
        // 相鄰 1 的位置對: (1,2) -> 距離 1; (2,4) -> 距離 2
        int n1 = 22;
        Console.WriteLine($"Input: {n1} (二進位: {Convert.ToString(n1, 2)})");
        Console.WriteLine($"Output: {solution.BinaryGap(n1)}");  // 預期: 2
        Console.WriteLine();

        // 測試資料 2：n = 5 (二進位: 101) => 相鄰兩個 1 的最大間距 = 2
        // 位元分布: bit0=1, bit1=0, bit2=1
        // 相鄰 1 的位置對: (0,2) -> 距離 2
        int n2 = 5;
        Console.WriteLine($"Input: {n2} (二進位: {Convert.ToString(n2, 2)})");
        Console.WriteLine($"Output: {solution.BinaryGap(n2)}");  // 預期: 2
        Console.WriteLine();

        // 測試資料 3：n = 6 (二進位: 110) => 相鄰兩個 1 的最大間距 = 1
        // 位元分布: bit0=0, bit1=1, bit2=1
        // 相鄰 1 的位置對: (1,2) -> 距離 1
        int n3 = 6;
        Console.WriteLine($"Input: {n3} (二進位: {Convert.ToString(n3, 2)})");
        Console.WriteLine($"Output: {solution.BinaryGap(n3)}");  // 預期: 1
        Console.WriteLine();

        // 測試資料 4：n = 8 (二進位: 1000) => 只有一個 1，無相鄰 1，回傳 0
        int n4 = 8;
        Console.WriteLine($"Input: {n4} (二進位: {Convert.ToString(n4, 2)})");
        Console.WriteLine($"Output: {solution.BinaryGap(n4)}");  // 預期: 0
    }


    /// <summary>
    /// 方法：位元運算（Bit Manipulation）
    ///
    /// 解題概念與出發點：
    ///   給定正整數 n 的二進位表示，找出任意兩個「相鄰的 1」之間的最大距離。
    ///   所謂「相鄰的 1」是指兩個 1 之間只有 0（或沒有 0）相隔。
    ///   距離定義為兩個 1 所在位元位置的差。
    ///
    /// 解法說明：
    ///   1. 從 n 的最低位元開始，逐一掃描每個位元（由低到高，第 0 位到最高位）。
    ///   2. 使用 n & 1 判斷當前最低位元是否為 1。
    ///   3. 用變數 last 記錄上一次出現 1 的位元索引（初始為 -1 表示尚未找到）。
    ///   4. 每當在第 i 位找到 1 且 last != -1（已有上一個 1），
    ///      計算距離 i - last，並更新最大距離 res。
    ///   5. 更新 last = i，再將 n 右移一位 n >>= 1，進入下一個位元。
    ///   6. 迴圈直到 n 為 0 為止（不需再處理更高位元）。
    ///
    /// 時間複雜度：O(log n)，n 最多有 log₂n 個位元需要掃描。
    /// 空間複雜度：O(1)，僅使用常數額外空間。
    ///
    /// <example>
    /// <code>
    /// n = 22，二進位為 10110
    /// bit: 4 3 2 1 0
    /// val: 1 0 1 1 0
    /// 出現 1 的位置: 1, 2, 4
    /// 相鄰距離: (2-1)=1, (4-2)=2 => 最大距離 = 2
    /// BinaryGap(22); // 回傳 2
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="n">正整數，將依其二進位表示進行分析。</param>
    /// <returns>任意兩個相鄰 1 之間的最長距離；若不存在兩個相鄰的 1 則回傳 0。</returns>
    public int BinaryGap(int n)
    {
        // last: 記錄上一個找到的 1 所在的位元索引；初始為 -1 表示尚未找到任何 1
        int last = -1;

        // res: 目前已知的最大相鄰 1 距離
        int res = 0;

        // 從第 0 位元開始掃描，直到 n 所有位元都處理完畢（n 變為 0）
        for (int i = 0; n != 0; i++)
        {
            // 使用位元與運算檢查當前最低位是否為 1
            if ((n & 1) == 1)
            {
                // 若之前已找到過 1，計算與上一個 1 的距離並更新最大值
                if (last != -1)
                {
                    res = Math.Max(res, i - last);
                }

                // 更新上一個 1 的位置為當前索引 i
                last = i;
            }

            // 將 n 右移一位，使下一次迴圈處理下一個位元
            n >>= 1;
        }

        return res;
    }
}
