namespace leetcode_3557;

class Program
{
    /// <summary>
    /// 3577. Count the Number of Computer Unlocking Permutations
    /// https://leetcode.com/problems/count-the-number-of-computer-unlocking-permutations/description/?envType=daily-question&envId=2025-12-10
    /// 3577. 统计计算机解锁顺序排列数
    /// https://leetcode.cn/problems/count-the-number-of-computer-unlocking-permutations/description/?envType=daily-question&envId=2025-12-10
    /// 給定一個長度為 n 的整數陣列 complexity。
    ///
    /// 房間裡有 n 台已鎖定的電腦，標號從 0 到 n - 1，且每台電腦各有唯一的密碼。編號為 i 的電腦之密碼複雜度為 complexity[i]。
    ///
    /// 標號為 0 的電腦的密碼已經解密，作為根節點。其他所有電腦必須使用它或其他先前已解鎖的電腦來解鎖，依照以下規則：
    ///
    /// - 你可以使用電腦 j 的密碼來解密電腦 i 的密碼，當且僅當 j < i 且 complexity[j] < complexity[i]（也就是 j 必須小於 i，且複雜度更低）。
    /// - 要解鎖電腦 i，你必須已經解鎖某台 j，滿足 j < i 且 complexity[j] < complexity[i]。
    ///
    /// 求有多少種排列 [0, 1, 2, ..., n - 1] 表示有效的解鎖順序，且從只有電腦 0 為已解鎖開始。
    ///
    /// 由於答案可能很大，請將結果對 10^9 + 7 取模。
    ///
    /// 注意：編號為 0 的電腦的密碼是已解密的，而不是排列中第一個位置的電腦。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試案例 1：complexity = [1, 2, 3]
        // 電腦 1, 2 的複雜度都 > 電腦 0，可按任意順序解鎖
        // 期望結果：(3-1)! = 2! = 2
        int[] test1 = [1, 2, 3];
        Console.WriteLine($"測試 1: complexity = [{string.Join(", ", test1)}]");
        Console.WriteLine($"結果: {solution.CountPermutations(test1)}"); // 預期: 2
        Console.WriteLine();

        // 測試案例 2：complexity = [3, 1, 2]
        // 電腦 1 的複雜度 (1) <= 電腦 0 的複雜度 (3)，無法解鎖
        // 期望結果：0
        int[] test2 = [3, 1, 2];
        Console.WriteLine($"測試 2: complexity = [{string.Join(", ", test2)}]");
        Console.WriteLine($"結果: {solution.CountPermutations(test2)}"); // 預期: 0
        Console.WriteLine();

        // 測試案例 3：complexity = [1, 2, 3, 4, 5]
        // 所有電腦複雜度都 > 電腦 0
        // 期望結果：(5-1)! = 4! = 24
        int[] test3 = [1, 2, 3, 4, 5];
        Console.WriteLine($"測試 3: complexity = [{string.Join(", ", test3)}]");
        Console.WriteLine($"結果: {solution.CountPermutations(test3)}"); // 預期: 24
        Console.WriteLine();

        // 測試案例 4：complexity = [2, 1]
        // 電腦 1 的複雜度 (1) <= 電腦 0 的複雜度 (2)，無法解鎖
        // 期望結果：0
        int[] test4 = [2, 1];
        Console.WriteLine($"測試 4: complexity = [{string.Join(", ", test4)}]");
        Console.WriteLine($"結果: {solution.CountPermutations(test4)}"); // 預期: 0
        Console.WriteLine();

        // 測試案例 5：complexity = [1, 5, 3, 4, 2]
        // 所有電腦複雜度 (5, 3, 4, 2) 都 > 電腦 0 的複雜度 (1)
        // 期望結果：(5-1)! = 4! = 24
        int[] test5 = [1, 5, 3, 4, 2];
        Console.WriteLine($"測試 5: complexity = [{string.Join(", ", test5)}]");
        Console.WriteLine($"結果: {solution.CountPermutations(test5)}"); // 預期: 24
    }

    /// <summary>
    /// 計算有效的電腦解鎖排列數量。
    /// 
    /// <para><b>解題思路：</b></para>
    /// <para>
    /// 根據題目條件：用電腦 j 解鎖電腦 i 的前提是 j &lt; i 且 complexity[j] &lt; complexity[i]。
    /// </para>
    /// 
    /// <para><b>關鍵觀察：</b></para>
    /// <para>
    /// 1. 一開始只有電腦 0 是解鎖的。<br/>
    /// 2. 第一輪：被電腦 0 解鎖的電腦（集合 A），其密碼複雜度必須比 complexity[0] 大。<br/>
    /// 3. 第二輪：被集合 A 解鎖的電腦（集合 B），其密碼複雜度更大，所以也比 complexity[0] 大。<br/>
    /// 4. 依此類推，所有被解鎖的電腦的密碼複雜度都必須比 complexity[0] 大。
    /// </para>
    /// 
    /// <para><b>定理：</b></para>
    /// <para>
    /// 當且僅當電腦 0 右邊的所有電腦的密碼複雜度都比 complexity[0] 大，才能解鎖所有電腦。
    /// </para>
    /// 
    /// <para><b>充分性：</b></para>
    /// <para>
    /// 如果電腦 0 右邊所有電腦的複雜度都比 complexity[0] 大，僅用電腦 0 便可解鎖所有電腦。
    /// </para>
    /// 
    /// <para><b>必要性：</b></para>
    /// <para>
    /// 若存在電腦 i（i &gt; 0）滿足 complexity[i] ≤ complexity[0]，則無法解鎖電腦 i。
    /// 因為要解鎖它需要在左邊找更小複雜度的電腦，最終會追溯到電腦 0 解鎖的電腦，
    /// 而這些電腦的複雜度必須 &gt; complexity[0]，產生矛盾。
    /// </para>
    /// 
    /// <para><b>結論：</b></para>
    /// <para>
    /// 若條件滿足，可以按任意順序解鎖 n-1 台電腦，方案數為 (n-1)!。
    /// </para>
    /// </summary>
    /// <param name="complexity">電腦密碼複雜度陣列</param>
    /// <returns>有效解鎖排列數量（對 10^9 + 7 取模）</returns>
    /// <example>
    /// <code>
    /// 範例 1：complexity = [1, 2, 3]
    /// 電腦 1, 2 的複雜度都大於電腦 0，可解鎖所有電腦
    /// 排列數 = (3-1)! = 2! = 2
    /// 
    /// 範例 2：complexity = [3, 1, 2]
    /// 電腦 1 的複雜度 (1) ≤ 電腦 0 的複雜度 (3)，無法解鎖
    /// 排列數 = 0
    /// 
    /// 注意題目只要求計算出排序組數, 不需要列出所有排列。所以階層算法足夠
    /// </code>
    /// </example>
    public int CountPermutations(int[] complexity)
    {
        int n = complexity.Length;
        long res = 1;
        const int MOD = 1_000_000_007;

        // 遍歷電腦 0 右邊的所有電腦
        for (int i = 1; i < n; i++)
        {
            // 如果任何電腦的複雜度 <= 電腦 0 的複雜度，則無法解鎖所有電腦
            if (complexity[i] <= complexity[0])
            {
                return 0;
            }

            // 計算 (n-1)! = 1 * 2 * 3 * ... * (n-1)
            // 每次迭代乘以 i（i 從 1 到 n-1）
            res = res * i % MOD;
        }

        return (int)res;
    }
}
