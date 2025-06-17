using System;

namespace leetcode_3405;

class Program
{
    /// <summary>
    /// 3405. Count the Number of Arrays with K Matching Adjacent Elements
    /// https://leetcode.com/problems/count-the-number-of-arrays-with-k-matching-adjacent-elements/description/?envType=daily-question&envId=2025-06-17
    /// 3405. 统计恰好有 K 个相等相邻元素的数组数目
    /// https://leetcode.cn/problems/count-the-number-of-arrays-with-k-matching-adjacent-elements/description/?envType=daily-question&envId=2025-06-17
    /// 
    /// 題目描述：
    /// 給定三個整數 n, m, k。大小為 n 的好數組 arr 定義如下：
    ///- arr 中的每個元素都在包含範圍 [1, m] 內。
    ///- 恰好有 k 個索引 i（其中 1 <= i < n）滿足條件 arr [i - 1] == arr [i]。
    /// 回傳可以形成的好數組的數量。
    /// 由於答案可能非常大，請回傳答案模 10^9 + 7 的結果。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program program = new Program();

        // 測試案例 1: n=3, m=2, k=1
        // 期望結果: 4
        // 說明: 可能的數組 [1,1,2], [1,2,2], [2,1,1], [2,2,1]
        Console.WriteLine("測試案例 1:");
        Console.WriteLine($"n=3, m=2, k=1");
        int result1 = program.CountGoodArrays(3, 2, 1);
        Console.WriteLine($"結果: {result1}");
        Console.WriteLine($"期望: 4");
        Console.WriteLine();

        // 測試案例 2: n=4, m=2, k=2
        // 期望結果: 6
        Console.WriteLine("測試案例 2:");
        Console.WriteLine($"n=4, m=2, k=2");
        int result2 = program.CountGoodArrays(4, 2, 2);
        Console.WriteLine($"結果: {result2}");
        Console.WriteLine($"期望: 6");
        Console.WriteLine();

        // 測試案例 3: n=5, m=2, k=0
        // 期望結果: 2
        // 說明: 所有相鄰元素都不同，只有 [1,2,1,2,1] 和 [2,1,2,1,2]
        Console.WriteLine("測試案例 3:");
        Console.WriteLine($"n=5, m=2, k=0");
        int result3 = program.CountGoodArrays(5, 2, 0);
        Console.WriteLine($"結果: {result3}");
        Console.WriteLine($"期望: 2");
        Console.WriteLine();

        // 測試案例 4: n=1, m=10, k=0
        // 期望結果: 10
        // 說明: 長度為 1 的數組沒有相鄰元素，所以 k 必須為 0，有 m 種選擇
        Console.WriteLine("測試案例 4:");
        Console.WriteLine($"n=1, m=10, k=0");
        int result4 = program.CountGoodArrays(1, 10, 0);
        Console.WriteLine($"結果: {result4}");
        Console.WriteLine($"期望: 10");
        Console.WriteLine();

        // 測試案例 5: n=2, m=3, k=1
        // 期望結果: 3
        // 說明: 兩個相同元素的數組，有 3 種選擇 [1,1], [2,2], [3,3]
        Console.WriteLine("測試案例 5:");
        Console.WriteLine($"n=2, m=3, k=1");
        int result5 = program.CountGoodArrays(2, 3, 1);
        Console.WriteLine($"結果: {result5}");
        Console.WriteLine($"期望: 3");
        Console.WriteLine();

        Console.WriteLine("所有測試完成！");

        // 額外測試案例 - 邊界情況
        Console.WriteLine("\n=== 額外邊界測試 ===");

        // 測試案例 6: n=3, m=1, k=2 (所有元素都相同)
        Console.WriteLine("測試案例 6 (所有元素相同):");
        Console.WriteLine($"n=3, m=1, k=2");
        int result6 = program.CountGoodArrays(3, 1, 2);
        Console.WriteLine($"結果: {result6}");
        Console.WriteLine($"期望: 1");
        Console.WriteLine();

        // 測試案例 7: k 比可能的最大值還大 (應該為 0)
        Console.WriteLine("測試案例 7 (k 超出範圍):");
        Console.WriteLine($"n=3, m=2, k=3");
        int result7 = program.CountGoodArrays(3, 2, 3);
        Console.WriteLine($"結果: {result7}");
        Console.WriteLine($"期望: 0");
        Console.WriteLine();
    }

    // 模數常數：10^9 + 7，用於取模運算防止整數溢出
    const int MOD = 1_000_000_007;
    // 預處理數組的最大長度
    const int MX = 100000;
    // 階乘數組：fact [i] = i!
    static readonly long[] fact = new long[MX];
    // 逆階乘數組：invFact [i] = (i!)^(-1) mod MOD
    static readonly long[] invFact = new long[MX];

    /// <summary>
    /// 快速冪算法：計算 x^n % MOD
    /// 用於計算大指數的冪運算，時間複雜度 O (log n)
    /// </summary>
    /// <param name="x"> 底數 </param>
    /// <param name="n"> 指數 </param>
    /// <returns>x^n % MOD 的結果 </returns>
    long qpow(long x, int n)
    {
        long res = 1; // 初始化結果為 1
        while (n > 0)
        {
            // 如果 n 是奇數，將當前的 x 乘入結果
            if ((n & 1) == 1)
            {
                res = res * x % MOD;
            }
            //x 平方，n 右移一位（等價於除以 2）
            x = x * x % MOD;
            n >>= 1;
        }
        return res;
    }

    /// <summary>
    /// 初始化階乘數組和逆階乘數組
    /// 用於快速計算組合數 C (n,m)
    /// 使用費馬小定理計算模逆元：a^(p-1) ≡ 1 (mod p)，所以 a^(p-2) ≡ a^(-1) (mod p)
    /// </summary>
    void init()
    {
        // 如果已經初始化過，直接返回
        if (fact[0] != 0)
        {
            return;
        }

        // 計算階乘數組：fact [i] = i!
        fact[0] = 1;
        for (int i = 1; i < MX; i++)
        {
            fact[i] = fact[i - 1] * i % MOD;
        }

        // 計算最大階乘的逆元，然後線性求出所有逆階乘
        // 使用費馬小定理：(n!)^(-1) ≡ (n!)^(MOD-2) (mod MOD)
        invFact[MX - 1] = qpow(fact[MX - 1], MOD - 2);

        // 線性求逆元：(i-1)!^(-1) = i!^(-1) * i
        for (int i = MX - 1; i > 0; i--)
        {
            invFact[i - 1] = invFact[i] * i % MOD;
        }
    }

    /// <summary>
    /// 計算組合數 C (n,m) = n! / (m! * (n-m)!)
    /// 使用預計算的階乘和逆階乘數組，時間複雜度 O (1)
    /// </summary>
    /// <param name="n"> 總數 </param>
    /// <param name="m"> 選擇數 </param>
    /// <returns>C (n,m) % MOD 的結果 </returns>
    long comb(int n, int m)
    {
        // 檢查邊界條件
        if (m < 0 || m > n) return 0;

        // C (n,m) = n! * (m!)^(-1) * ((n-m)!)^(-1)
        return fact[n] * invFact[m] % MOD * invFact[n - m] % MOD;
    }

    /// <summary>
    /// 方法一：組合數學解法
    /// 解題流程：
    /// 1. 長度為 n 的數組有 n-1 對相鄰元素，其中 k 對相同，n-1-k 對不同
    /// 2. 將 n-1-k 對不同的相鄰元素視為隔板，將數組分為 n-k 段相同元素的子數組
    /// 3. 計算方案數：
    ///    - 選擇隔板位置：C (n-1, k) 種方案
    ///    - 第一段可以選任意值：m 種選擇
    ///    - 後續每段與前一段不同：每段有 m-1 種選擇，共 n-k-1 段
    /// 4. 總方案數：m × C (n-1, k) × (m-1)^(n-k-1)
    /// 
    /// 時間複雜度：O (MX + log (n-k-1))，其中 MX 為預處理數組大小
    /// 空間複雜度：O (MX)
    /// </summary>
    /// <param name="n"> 數組長度 </param>
    /// <param name="m"> 元素取值範圍 [1, m]</param>
    /// <param name="k"> 相鄰相同元素對的數量 </param>
    /// <returns > 滿足條件的數組數量，結果模 10^9 + 7</returns>
    public int CountGoodArrays(int n, int m, int k)
    {
        init(); // 初始化階乘和逆階乘數組

        // 邊界檢查：k 不能超過 n-1 或小於 0
        if (k > n - 1 || k < 0)
        {
            return 0;
        }

        // 特殊情況：只有一個元素時
        if (n == 1)
        {
            return k == 0 ? m : 0;
        }

        // 套用公式：m × C (n-1, k) × (m-1)^(n-k-1)
        // 注意：n-k-1 是除了第一段外的其他段數量
        return (int)((comb(n - 1, k) * m % MOD * qpow(m - 1, n - k - 1)) % MOD);
    }
}
