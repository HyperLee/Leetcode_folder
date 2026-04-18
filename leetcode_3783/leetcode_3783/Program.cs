namespace leetcode_3783;

class Program
{
    /// <summary>
    /// 3783. Mirror Distance of an Integer
    /// https://leetcode.com/problems/mirror-distance-of-an-integer/description/?envType=daily-question&envId=2026-04-18
    /// 3783. 整數的鏡像距離
    /// https://leetcode.cn/problems/mirror-distance-of-an-integer/description/?envType=daily-question&envId=2026-04-18
    ///
    /// [EN]
    /// You are given an integer n.
    /// Define its mirror distance as: abs(n - reverse(n)),
    /// where reverse(n) is the integer formed by reversing the digits of n.
    /// Return an integer denoting the mirror distance of n.
    /// abs(x) denotes the absolute value of x.
    ///
    /// [繁體中文]
    /// 給定一個整數 n。
    /// 定義其鏡像距離為：abs(n - reverse(n))，
    /// 其中 reverse(n) 表示將 n 的數字反轉後所形成的整數。
    /// 回傳表示 n 的鏡像距離的整數。
    /// abs(x) 表示 x 的絕對值。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public int MirrorDistance(int n)
    {
        return Math.Abs(n - ReverseNum(n));
    }

    /// <summary>
    /// 將非負整數 num 的十進位數字反轉，自動省略前導零。
    /// <para>
    /// 演算法：每次取 num 末位數字（num % 10）附加到 reversed 末位，
    /// 再將 num 右移一位（num /= 10），重複直到 num 歸零。
    /// </para>
    /// <example>
    /// <code>
    /// ReverseNum(120) → 21
    /// ReverseNum(123) → 321
    /// ReverseNum(1)   → 1
    /// </code>
    /// </example>
    /// <para>時間複雜度：O(D)，D 為 num 的位數。</para>
    /// <para>空間複雜度：O(1)。</para>
    /// </summary>
    /// <param name="num">欲反轉的非負整數。</param>
    /// <returns>反轉後的整數（省略前導零）。</returns>
    private int ReverseNum(int num)
    {
        int reversed = 0;
        while (num > 0)
        {
            reversed = reversed * 10 + num % 10; // 將末位數字附加到 reversed 末位
            num /= 10;                            // 移除 num 的末位數字
        }

        return reversed;
    }
}
