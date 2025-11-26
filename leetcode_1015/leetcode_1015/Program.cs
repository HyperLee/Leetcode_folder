namespace leetcode_1015;

class Program
{
    /// <summary>
    /// 1015. Smallest Integer Divisible by K
    /// https://leetcode.com/problems/smallest-integer-divisible-by-k/description/?envType=daily-question&envId=2025-11-25
    /// 1015. 可被 K 整除的最小整数
    /// https://leetcode.cn/problems/smallest-integer-divisible-by-k/description/?envType=daily-question&envId=2025-11-25
    ///
    /// 題目（中文）：
    /// 給定一個正整數 k，找出最小正整數 n，使得 n 可以被 k 整除，並且 n 的十進位數字僅包含 1（例如 1, 11, 111, ...）。
    /// 回傳 n 的長度（也就是二進位數字 1 的個數）。如果不存在這樣的 n，則回傳 -1。
    /// 注意：n 可能超過 64-bit 整數範圍，因此應使用餘數 (mod) 技術來處理。
    ///
    /// 解題思路（簡述）：
    /// 1. 若 k 可被 2 或 5 整除，則不可能有只由 1 構成的整數能被 k 整除，直接回傳 -1。
    /// 2. 否則，使用餘數遞推：從 1 開始，紀錄 r = 1 % k，長度 len = 1。
    ///    若 r == 0，回傳 len。否則更新 r = (r * 10 + 1) % k，len++，直到找到 r == 0，或嘗試 k 次後仍沒找到，則回傳 -1。
    ///    （最多 k 次即可保證找出答案或證明無解，因為餘數只會出現 0..k-1 的值。）
    /// 
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 範例測試：
        var tests = new Dictionary<int, int>()
        {
            { 1, 1 },    // 1
            { 2, -1 },   // 無解，因為 2 整除
            { 3, 3 },    // 111
            { 7, 6 },    // 111111
            { 13, 6 },   // 111111 % 13 == 0
            { 9901, 12 } // 一個較大的測試
        };

        foreach (var kv in tests)
        {
            var k = kv.Key;
            var expected = kv.Value;
            var result = SmallestRepunitDivByK(k);
            Console.WriteLine($"k = {k}, result = {result}, expected = {expected}");
        }
    }

    /// <summary>
    /// 返回僅由數字 1 組成且可被 k 整除的最短正整數的長度。
    /// 若無解則返回 -1。
    /// 
    /// <para>
    /// <b>解題思路：</b>
    /// </para>
    /// <para>
    /// 此題要找最小的「Repunit」（全部由數字 1 組成的正整數，如 1, 11, 111, 1111, ...），
    /// 使其可被給定的正整數 k 整除。
    /// </para>
    /// 
    /// <para>
    /// <b>關鍵觀察：</b>
    /// </para>
    /// <list type="number">
    ///   <item>
    ///     <description>
    ///       若 k 可被 2 或 5 整除，則不存在這樣的 Repunit。
    ///       原因：任何 Repunit 的個位數皆為 1，無法被 2 或 5 整除。
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Repunit 可遞推表示：R(n) = R(n-1) * 10 + 1。
    ///       例如 R(1)=1, R(2)=11=1*10+1, R(3)=111=11*10+1, ...
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       為避免大數溢位，使用餘數運算：remainder(n) = (remainder(n-1) * 10 + 1) % k。
    ///       當 remainder 為 0 時，代表 R(n) 可被 k 整除。
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       由鴿籠原理，餘數只會出現 0 到 k-1 共 k 種可能，
    ///       因此最多嘗試 k 次即可確定是否有解。
    ///     </description>
    ///   </item>
    /// </list>
    /// 
    /// <para>
    /// <b>演算法流程：</b>
    /// </para>
    /// <list type="number">
    ///   <item><description>若 k ≤ 0，回傳 -1（無效輸入）。</description></item>
    ///   <item><description>若 k % 2 == 0 或 k % 5 == 0，回傳 -1（無解）。</description></item>
    ///   <item><description>初始化 remainder = 1 % k，length = 1。</description></item>
    ///   <item><description>當 remainder ≠ 0 且 length ≤ k 時，更新 remainder = (remainder * 10 + 1) % k，length++。</description></item>
    ///   <item><description>若 remainder == 0，回傳 length；否則回傳 -1。</description></item>
    /// </list>
    /// 
    /// <example>
    /// <code>
    ///  範例：k = 3
    ///  R(1) = 1, 1 % 3 = 1 (不整除)
    ///  R(2) = 11, 11 % 3 = 2 (不整除)
    ///  R(3) = 111, 111 % 3 = 0 (整除!) → 回傳 3
    /// int result = SmallestRepunitDivByK(3); // 回傳 3
    /// </code>
    /// </example>
    /// 
    /// <para>時間複雜度：O(k)</para>
    /// <para>空間複雜度：O(1)</para>
    /// </summary>
    /// <param name="k">目標除數（正整數）。</param>
    /// <returns>最短 Repunit 的長度（即 1 的個數），若無解則回傳 -1。</returns>
    static int SmallestRepunitDivByK(int k)
    {
        // 無效輸入檢查：k 必須為正整數
        if (k <= 0) return -1;

        // 關鍵剪枝：若 k 可被 2 或 5 整除，則不存在僅由 1 組成且可被 k 整除的數
        // 原因：任何 Repunit (1, 11, 111, ...) 的個位數皆為 1，無法被 2 或 5 整除
        if (k % 2 == 0 || k % 5 == 0) return -1;

        // remainder 表示當前 Repunit 除以 k 的餘數
        // 初始值為 1 % k，代表 R(1) = 1 的餘數
        int remainder = 1 % k;

        // length 表示當前 Repunit 的長度（即 1 的個數）
        int length = 1;

        // 迴圈尋找可被 k 整除的最短 Repunit
        // 終止條件：找到餘數為 0，或嘗試超過 k 次（鴿籠原理保證）
        while (remainder != 0 && length <= k)
        {
            // 遞推公式：R(n) = R(n-1) * 10 + 1
            // 對應餘數運算：remainder(n) = (remainder(n-1) * 10 + 1) % k
            // 這樣可以避免大數溢位問題
            remainder = (remainder * 10 + 1) % k;
            length++;
        }

        // 若餘數為 0，代表找到可被 k 整除的 Repunit，回傳其長度
        // 否則回傳 -1 表示無解
        return remainder == 0 ? length : -1;
    }
}
