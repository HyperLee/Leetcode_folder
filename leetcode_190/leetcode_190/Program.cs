namespace leetcode_190;

class Program
{
    /// <summary>
    /// 190. Reverse Bits
    /// <para>
    /// LeetCode 190. 顛倒二進位位元
    /// 給定一個 32 位元有號整數，將其二進位位元全部顛倒。
    /// </para>
    /// <para>
    /// 題目連結：
    /// https://leetcode.com/problems/reverse-bits/description/?envType=daily-question&amp;envId=2026-02-16
    /// https://leetcode.cn/problems/reverse-bits/description/?envType=daily-question&amp;envId=2026-02-16
    /// </para>
    /// <example>
    /// 範例 1：
    /// <code>
    /// 輸入：n = 00000010100101000001111010011100
    /// 輸出：964176192 (00111001011110000010100101000000)
    /// </code>
    /// 範例 2：
    /// <code>
    /// 輸入：n = 11111111111111111111111111111101
    /// 輸出：-1073741825 (10111111111111111111111111111111)
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        var program = new Program();

        // 測試資料 1：n = 00000010100101000001111010011100 (43261596)
        // 預期輸出：    00111001011110000010100101000000 (964176192)
        int test1 = 43261596;
        Console.WriteLine($"測試 1 輸入: {test1}");
        Console.WriteLine($"  二進位:  {Convert.ToString(test1, 2).PadLeft(32, '0')}");
        int result1 = program.ReverseBits(test1);
        Console.WriteLine($"  ReverseBits  結果: {result1} => {Convert.ToString(result1, 2).PadLeft(32, '0')}");
        int result1B = program.ReverseBits2(43261596);
        Console.WriteLine($"  ReverseBits2 結果: {result1B} => {Convert.ToString(result1B, 2).PadLeft(32, '0')}");
        int result1C = program.ReverseBits3(43261596);
        Console.WriteLine($"  ReverseBits3 結果: {result1C} => {Convert.ToString(result1C, 2).PadLeft(32, '0')}");
        Console.WriteLine();

        // 測試資料 2：n = 11111111111111111111111111111101 (-3 以 int 表示)
        // 預期輸出：    10111111111111111111111111111111 (-1073741825)
        int test2 = -3;
        Console.WriteLine($"測試 2 輸入: {test2}");
        Console.WriteLine($"  二進位:  {Convert.ToString(test2, 2).PadLeft(32, '0')}");
        int result2 = program.ReverseBits(test2);
        Console.WriteLine($"  ReverseBits  結果: {result2} => {Convert.ToString(result2, 2).PadLeft(32, '0')}");
        int result2B = program.ReverseBits2(-3);
        Console.WriteLine($"  ReverseBits2 結果: {result2B} => {Convert.ToString(result2B, 2).PadLeft(32, '0')}");
        int result2C = program.ReverseBits3(-3);
        Console.WriteLine($"  ReverseBits3 結果: {result2C} => {Convert.ToString(result2C, 2).PadLeft(32, '0')}");
        Console.WriteLine();

        // 測試資料 3：n = 00000000000000000000000000000001 (1)
        // 預期輸出：    10000000000000000000000000000000 (-2147483648 以 int 表示)
        int test3 = 1;
        Console.WriteLine($"測試 3 輸入: {test3}");
        Console.WriteLine($"  二進位:  {Convert.ToString(test3, 2).PadLeft(32, '0')}");
        int result3 = program.ReverseBits(test3);
        Console.WriteLine($"  ReverseBits  結果: {result3} => {Convert.ToString(result3, 2).PadLeft(32, '0')}");
        int result3B = program.ReverseBits2(1);
        Console.WriteLine($"  ReverseBits2 結果: {result3B} => {Convert.ToString(result3B, 2).PadLeft(32, '0')}");
        int result3C = program.ReverseBits3(1);
        Console.WriteLine($"  ReverseBits3 結果: {result3C} => {Convert.ToString(result3C, 2).PadLeft(32, '0')}");
        Console.WriteLine();

        // 測試資料 4：邊界情況 n = 0
        // 預期輸出：0
        int test4 = 0;
        Console.WriteLine($"測試 4 輸入: {test4}");
        Console.WriteLine($"  二進位:  {Convert.ToString(test4, 2).PadLeft(32, '0')}");
        int result4 = program.ReverseBits(test4);
        Console.WriteLine($"  ReverseBits  結果: {result4}");
        int result4B = program.ReverseBits2(0);
        Console.WriteLine($"  ReverseBits2 結果: {result4B}");
        int result4C = program.ReverseBits3(0);
        Console.WriteLine($"  ReverseBits3 結果: {result4C}");
    }

    /// <summary>
    /// 解法一：逐位元取出並反轉（迴圈 + 位元運算）
    /// <para>
    /// 【解題概念】
    /// 類似十進位中「取餘數 → 組合 → 整除」的思路，只不過這裡是在二進位上操作。
    /// 每次從原數 n 的最低位元取出一個位元，放到結果 result 的最低位元，
    /// 然後將 result 左移騰出空間，對 n 右移以處理下一個位元。
    /// 重複 32 次後，原數的最低位元就跑到了結果的最高位元，完成顛倒。
    /// </para>
    /// <para>
    /// 【核心思想】
    /// 逐位元取出原數的二進位位元，並將其移到目標數的對應位置。
    /// </para>
    /// <para>
    /// 【程式邏輯說明】
    /// 1. <c>result &lt;&lt;= 1</c>：將結果左移一位，預留空間給新位元。
    /// 2. <c>result |= (n &amp; 1)</c>：取得 n 的最低位元並將其加入 result。
    /// 3. <c>n &gt;&gt;= 1</c>：將原始數字右移一位，以檢查下一個位元。
    /// 4. 重複 32 次：因為我們處理的是 32 位元整數。
    /// </para>
    /// <para>
    /// 【時間複雜度】O(32) = O(1)，固定迴圈 32 次。
    /// 【空間複雜度】O(1)，只使用常數額外空間。
    /// </para>
    /// <example>
    /// <code>
    /// 原始二進位:     00000000000000000000000000000001 (1)
    /// 顛倒後的二進位: 10000000000000000000000000000000 (-2147483648)
    /// 
    /// 流程演示 (以 n = 1 為例，僅展示前 2 次與最後 1 次迴圈)：
    /// i=0: result=0 左移→0, n&amp;1=1, result|=1→1, n右移→0
    /// i=1: result=1 左移→2, n&amp;1=0, result|=0→2, n右移→0
    /// ...
    /// i=31: result 左移後最高位元為 1 → 10000000000000000000000000000000
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="n">要顛倒位元的 32 位元整數</param>
    /// <returns>顛倒後的整數值</returns>
    public int ReverseBits(int n)
    {
        int result = 0;

        // 題目輸入是 32 位元，所以迴圈固定跑 32 次
        for (int i = 0; i < 32; i++)
        {
            // Step 1: 將 result 左移一位，為下一個新位元騰出最低位空間
            result <<= 1;

            // Step 2: 用 (n & 1) 取出 n 目前的最低位元（0 或 1），
            //         再用 |= 將該位元放入 result 的最低位
            result |= (n & 1);

            // Step 3: 將 n 右移一位，讓下一個位元移到最低位，以便下次迭代取出
            n >>= 1;
        }

        return result;
    }

    /// <summary>
    /// 解法二：逐位元顛倒 — 直接定位目標位置（官方解法一）
    /// <para>
    /// 【解題概念】
    /// 與解法一同樣是逐位元處理，但不同之處在於：
    /// 解法一是從低位到高位「堆疊式」地將位元推入 result，
    /// 而本解法直接計算每個位元在結果中的「目標位置」，使用左移將位元放到正確位置。
    /// 第 i 次迭代處理的是原數的第 i 位元（從最低位算起），
    /// 它在結果中的位置是第 (31 - i) 位元。
    /// </para>
    /// <para>
    /// 【最佳化】
    /// 當 n 變為 0 時提前結束迴圈，因為剩餘位元全為 0，無需再處理。
    /// </para>
    /// <para>
    /// 【時間複雜度】O(32) = O(1)，最多迴圈 32 次，可能提前結束。
    /// 【空間複雜度】O(1)，只使用常數額外空間。
    /// </para>
    /// <para>
    /// 參考：https://leetcode.cn/problems/reverse-bits/solutions/685436/dian-dao-er-jin-zhi-wei-by-leetcode-solu-yhxz/
    /// </para>
    /// <example>
    /// <code>
    /// 以 n = 43261596 (00000010100101000001111010011100) 為例：
    /// i=0:  n&amp;1=0, 不影響 result, n 右移
    /// i=1:  n&amp;1=0, 不影響 result, n 右移
    /// i=2:  n&amp;1=1, result |= 1 &lt;&lt; (31-2) = 1 &lt;&lt; 29, n 右移
    /// ...持續處理，直到 n == 0 時提前結束
    /// 最終結果：964176192 (00111001011110000010100101000000)
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="n">要顛倒位元的 32 位元整數</param>
    /// <returns>顛倒後的整數值</returns>
    public int ReverseBits2(int n)
    {
        int result = 0;

        // 迴圈最多 32 次；當 n 為 0 時提前結束（剩餘位元皆為 0）
        for (int i = 0; i < 32 && n != 0; i++)
        {
            // (n & 1) 取出 n 的最低位元，
            // << (31 - i) 將該位元直接移到結果中的目標位置（第 31-i 位），
            // |= 將其合併到 result
            result |= (n & 1) << (31 - i);

            // 將 n 右移一位，讓下一個位元移到最低位
            n >>= 1;
        }

        return result;
    }

    /// <summary>位元遮罩：交換相鄰 1 位元 (奇偶位元交換)</summary>
    private const int M1 = 0x55555555; // 01010101010101010101010101010101

    /// <summary>位元遮罩：交換相鄰 2 位元組</summary>
    private const int M2 = 0x33333333; // 00110011001100110011001100110011

    /// <summary>位元遮罩：交換相鄰 4 位元組（半位元組）</summary>
    private const int M4 = 0x0f0f0f0f; // 00001111000011110000111100001111

    /// <summary>位元遮罩：交換相鄰 8 位元組（位元組）</summary>
    private const int M8 = 0x00ff00ff; // 00000000111111110000000011111111

    /// <summary>
    /// 解法三：位元運算分治法（Divide and Conquer）— 官方解法二
    /// <para>
    /// 【解題概念】
    /// 若要翻轉一個 32 位元的二進位串，可以使用分治的思想：
    /// 先交換相鄰的 1 位元 → 再交換相鄰的 2 位元 → 4 位元 → 8 位元 → 16 位元。
    /// 共五步即可完成整個 32 位元的顛倒。
    /// </para>
    /// <para>
    /// 【為什麼可以這樣做？】
    /// 這類似於「翻轉陣列」的分治法：先在最小粒度交換，逐層擴大，
    /// 直到最後交換整個左右半部。每一步都是平行處理所有相同粒度的對，
    /// 利用位元遮罩 (mask) 一次性完成。
    /// </para>
    /// <para>
    /// 【五步操作說明】
    /// Step 1: M1 = 0x55555555 — 交換所有相鄰的 1 位元（奇偶位元互換）
    /// Step 2: M2 = 0x33333333 — 以 2 位元為一組，交換相鄰的兩組
    /// Step 3: M4 = 0x0f0f0f0f — 以 4 位元為一組，交換相鄰的兩組
    /// Step 4: M8 = 0x00ff00ff — 以 8 位元為一組，交換相鄰的兩組
    /// Step 5: 直接交換高低 16 位元
    /// </para>
    /// <para>
    /// 【時間複雜度】O(1)，固定五次位元運算。
    /// 【空間複雜度】O(1)，只使用常數額外空間。
    /// </para>
    /// <para>
    /// 參考：https://leetcode.cn/problems/reverse-bits/solutions/685436/dian-dao-er-jin-zhi-wei-by-leetcode-solu-yhxz/
    /// </para>
    /// <example>
    /// <code>
    /// 以 8 位元簡化演示 (abcdefgh)：
    /// Step 1 交換相鄰 1 位元: abcdefgh → badcfehg
    /// Step 2 交換相鄰 2 位元: badcfehg → dcbahgfe
    /// Step 3 交換相鄰 4 位元: dcbahgfe → hgfedcba  ✓ 完成顛倒
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="n">要顛倒位元的 32 位元整數</param>
    /// <returns>顛倒後的整數值</returns>
    public int ReverseBits3(int n)
    {
        int result = n;

        // Step 1: 交換所有相鄰的 1 位元（奇數位與偶數位互換）
        // 將偶數位元右移 1 位，奇數位元左移 1 位，再合併
        result = ((int)((uint)result >> 1) & M1) | ((result & M1) << 1);

        // Step 2: 以 2 位元為一組，交換相鄰的兩組
        result = ((int)((uint)result >> 2) & M2) | ((result & M2) << 2);

        // Step 3: 以 4 位元為一組，交換相鄰的兩組（半位元組交換）
        result = ((int)((uint)result >> 4) & M4) | ((result & M4) << 4);

        // Step 4: 以 8 位元為一組，交換相鄰的兩組（位元組交換）
        result = ((int)((uint)result >> 8) & M8) | ((result & M8) << 8);

        // Step 5: 交換高 16 位元與低 16 位元
        return (int)((uint)result >> 16) | (result << 16);
    }
}
