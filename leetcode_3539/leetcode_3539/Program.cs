using System.Numerics;

namespace leetcode_3539;

class Program
{
    /// <summary>
    /// 3539. Find Sum of Array Product of Magical Sequences
    /// https://leetcode.com/problems/find-sum-of-array-product-of-magical-sequences/description/?envType=daily-question&envId=2025-10-12
    /// 3539. 魔法序列的数组乘积之和
    /// https://leetcode.cn/problems/find-sum-of-array-product-of-magical-sequences/description/?envType=daily-question&envId=2025-10-12
    /// 
    /// 給定兩個整數 m 和 k，以及一個整數陣列 nums。
    /// 一個整數序列 seq 稱為魔法序列，如果：
    /// seq 的大小為 m。
    /// 0 <= seq[i] < nums.length
    /// 2^seq[0] + 2^seq[1] + ... + 2^seq[m - 1] 的二進位表示有 k 個設定位元。
    /// 此序列的陣列乘積定義為 prod(seq) = (nums[seq[0]] * nums[seq[1]] * ... * nums[seq[m - 1]])。
    /// 返回所有有效魔法序列的陣列乘積之和。
    /// 由於答案可能很大，返回它模 10^9 + 7。
    /// 設定位元是指二進位表示中值為 1 的位元。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("LeetCode 3539 - Find Sum of Array Product of Magical Sequences");
        Console.WriteLine("========================================================");
        
        // 測試案例 1
        TestMagicalSum(2, 1, new int[] { 1, 2, 3 }, "測試案例 1");
        
        // 測試案例 2
        TestMagicalSum(3, 2, new int[] { 1, 1, 1, 1 }, "測試案例 2");
        
        // 測試案例 3
        TestMagicalSum(1, 0, new int[] { 5, 6 }, "測試案例 3");
        
        // 測試案例 4
        TestMagicalSum(2, 2, new int[] { 2, 3, 4 }, "測試案例 4");
        
        // 測試案例 5
        TestMagicalSum(3, 1, new int[] { 1, 2 }, "測試案例 5");
        
        // 測試案例 6（失敗的測試案例）
        TestMagicalSum(3, 2, new int[] { 33 }, "測試案例 6 (預期: 35937)");
        
        Console.WriteLine("測試完成！");
    }
    
    /// <summary>
    /// 測試 MagicalSum 方法的輔助方法
    /// </summary>
    /// <param name="m">序列大小</param>
    /// <param name="k">設定位元數量</param>
    /// <param name="nums">整數陣列</param>
    /// <param name="testName">測試名稱</param>
    private static void TestMagicalSum(int m, int k, int[] nums, string testName)
    {
        Console.WriteLine($"\n{testName}:");
        Console.WriteLine($"  m = {m}, k = {k}");
        Console.WriteLine($"  nums = [{string.Join(", ", nums)}]");
        
        var program = new Program();
        int result = program.MagicalSum(m, k, nums);
        
        Console.WriteLine($"  結果: {result}");
    }

    /// <summary>
    /// 計算所有魔法序列的陣列乘積之和
    /// 
    /// 解題思路：動態規劃
    /// 令數組 nums 的長度為 n。根據題意，我們依次從 0 到 n-1 分別取若干個數（即 nums 的下標）組成序列 seq，
    /// 假設取值等於 t 的元素有 r_t 個，那麼有 Σ(t=0 to n-1) r_t = m
    /// 
    /// 這些序列排列數為：m! / ∏(t=0 to n-1) r_t!
    /// 這些序列對應的數組乘積和為：m! × ∏(t=0 to n-1) (nums[t]^r_t / r_t!)
    /// 
    /// 定義 f(i,j,p,q) 為：從 0 取到 i，總共取了 j 個數，mask 為 p，低 i 位的置位為 q 個
    /// 對應的所有取數方案的 ∏(t=0 to i) (nums[t]^r_t / r_t!) 的和
    /// 
    /// 遞推公式：f(i,j,p,q) × (nums[i+1]^r_(i+1) / r_(i+1)!) 
    ///          → f(i+1, j+r_(i+1), ⌊p/2⌋+r_(i+1), q+(p mod 2))
    /// 
    /// 初始值：f(0,j,j,0) = nums[0]^j / j!
    /// 
    /// 最終答案：Σ(所有 b_p + q = k) (m! × f(n-1, m, p, q))
    /// 其中 b_p 表示 p 的置位數
    /// </summary>
    /// <param name="m">序列大小</param>
    /// <param name="k">二進位表示中設定位元的數量</param>
    /// <param name="nums">整數陣列</param>
    /// <returns>所有魔法序列的陣列乘積之和 mod 10^9+7</returns>
    public int MagicalSum(int m, int k, int[] nums)
    {
        int n = nums.Length;
        const long mod = 1000000007;

        // 預計算階乘 fac[i] = i!
        long[] fac = new long[m + 1];
        fac[0] = 1;

        for (int i = 1; i <= m; i++)
        {
            fac[i] = fac[i - 1] * i % mod;
        }

        // 預計算階乘的逆元 ifac[i] = 1/i!
        // 使用費馬小定理：a^(-1) ≡ a^(p-2) (mod p)，其中 p 是質數
        // 方法：先計算 m! 的逆元，然後反向計算其他階乘的逆元
        long[] ifac = new long[m + 1];
        ifac[m] = QuickMul(fac[m], mod - 2, mod);  // ifac[m] = 1/m!
        
        // 反向計算：ifac[i-1] = ifac[i] * i
        // 因為 1/(i-1)! = (1/i!) * i
        for (int i = m; i >= 1; i--)
        {
            ifac[i - 1] = ifac[i] * i % mod;
        }

        // 預計算 nums 的冪次方 numsPower[i][j] = nums[i]^j
        // 這樣可以避免重複計算
        long[][] numsPower = new long[n][];
        for (int i = 0; i < n; i++)
        {
            numsPower[i] = new long[m + 1];
            numsPower[i][0] = 1;
            for (int j = 1; j <= m; j++)
            {
                numsPower[i][j] = numsPower[i][j - 1] * nums[i] % mod;
            }
        }

        // 動態規劃陣列 f[i][j][p][q]
        // i: 目前處理到 nums 的第 i 個元素
        // j: 已經取了 j 個數
        // p: 已取數字索引的 mask 值（除以 2 的累積）
        // q: 低位元的置位數累積
        long[][][][] f = new long[n][][][];
        for (int i = 0; i < n; i++)
        {
            f[i] = new long[m + 1][][];
            for (int j = 0; j <= m; j++)
            {
                f[i][j] = new long[m * 2 + 1][];
                for (int p = 0; p <= m * 2; p++)
                {
                    f[i][j][p] = new long[k + 1];
                }
            }
        }

        // 初始化：從 nums[0] 取 j 個數，mask 為 j，置位數為 0
        // f(0,j,j,0) = nums[0]^j / j!
        for (int j = 0; j <= m; j++)
        {
            f[0][j][j][0] = numsPower[0][j] * ifac[j] % mod;
        }

        // 動態規劃轉移
        // 從第 i 個元素轉移到第 i+1 個元素
        for (int i = 0; i + 1 < n; i++)
        {
            for (int j = 0; j <= m; j++)
            {
                for (int p = 0; p <= m * 2; p++)
                {
                    for (int q = 0; q <= k; q++)
                    {
                        // 計算新的置位數：當前 p 的最低位 + 已累積的置位數 q
                        int q2 = (p % 2) + q;
                        if (q2 > k)
                        {
                            break;
                        }

                        // 從 nums[i+1] 取 r 個數（r 從 0 到 m-j）
                        for (int r = 0; r + j <= m; r++)
                        {
                            // 新的 mask 值：p 右移一位（除以 2）後加上 r
                            int p2 = p / 2 + r;
                            // 狀態轉移：f[i][j][p][q] × (nums[i+1]^r / r!)
                            f[i + 1][j + r][p2][q2] += f[i][j][p][q] * numsPower[i + 1][r] % mod * ifac[r] % mod;
                            f[i + 1][j + r][p2][q2] %= mod;
                        }
                    }
                }
            }
        }

        // 計算最終答案
        // 枚舉所有可能的 p 和 q，滿足 BitOperations.PopCount(p) + q = k
        // 答案為 Σ(m! × f(n-1, m, p, q))
        long res = 0;
        for (int p = 0; p <= m * 2; p++) {
            for (int q = 0; q <= k; q++) {
                // 檢查 p 的置位數加上 q 是否等於 k
                if (BitOperations.PopCount((uint)p) + q == k) {
                    res = (res + f[n - 1][m][p][q] * fac[m] % mod) % mod;
                }
            }
        }
        return (int)res;
    }

    /// <summary>
    /// 快速冪取模運算（使用二進位分解法）
    /// 計算 x^y mod mod
    /// 
    /// 原理：將指數 y 用二進位表示，例如 y = 13 = 1101(2) = 8 + 4 + 1
    /// 則 x^13 = x^8 × x^4 × x^1
    /// 
    /// 時間複雜度：O(log y)
    /// </summary>
    /// <param name="x">底數</param>
    /// <param name="y">指數</param>
    /// <param name="mod">模數</param>
    /// <returns>x^y mod mod 的結果</returns>
    private long QuickMul(long x, long y, long mod)
    {
        long res = 1;
        long cur = x % mod;
        while (y > 0)
        {
            // 如果 y 的當前位是 1，將當前的 cur 乘入結果
            if ((y & 1) == 1)
            {
                res = res * cur % mod;
            }

            // y 右移一位（相當於除以 2）
            y >>= 1;
            // cur 平方（為下一位準備）
            cur = cur * cur % mod;
        }
        return res;
    }

}
