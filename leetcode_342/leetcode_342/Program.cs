namespace leetcode_342;

class Program
{
    /// <summary>
    /// 342. Power of Four
    /// https://leetcode.com/problems/power-of-four/description/?envType=daily-question&envId=2025-08-15
    /// 342. 4的幂
    /// https://leetcode.cn/problems/power-of-four/description/?envType=daily-question&envId=2025-08-15
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 題目：給定一個整數 n，若存在整數 x 使得 n == 4^x，則回傳 true，否則回傳 false。
        // 範例：n = 16 -> true (4^2)，n = 5 -> false
        Console.WriteLine("342. 4的冪：給定整數 n，若存在整數 x 使得 n == 4^x，回傳 true 否則 false。");

        // 執行內建的簡單測試集合，檢查三種實作的一致性與正確性
        RunAllTests();
    }

    /// <summary>
    /// 簡易推理
    /// n & (n - 1) 之後會為 0
    /// 若 n 為 4 的冪，則可以表示為 4 的某個整數次方，我們可以發現 (n - 1) 被 3 整除
    /// 因為 4 mod 3 == 1
    /// 且 n > 0
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    /// <summary>
    /// 方法一：使用 n>0、n&(n-1)==0（2 的冪）與 (n-1) % 3 == 0 的數學性質
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static bool IsPowerOfFour(int n)
    {
        return n > 0 && (n & (n - 1)) == 0 && (n - 1) % 3 == 0;
    }

    /// <summary>
    /// 這個方法好懂 但是會超時, 不推薦
    /// 負數和零不是 4 的冪
    /// 1 是任何數的冪
    /// 若 n % 4 != 0 則肯定不是 4 的冪
    /// 使用迴圈逐步乘 4 檢查是否等於目標值
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    /// <summary>
    /// 方法二：直觀版，逐步乘 4 檢查（教育用，不推薦於大數）
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static bool IsPowerOfFour2(int n)
    {
        if (n < 0)
        {
            return false;
        }

        if (n == 1)
        {
            return true;
        }

        if (n % 4 != 0)
        {
            return false;
        }

        for (int i = 4; i <= n; i *= 4)
        {
            if (i == n)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 解題說明：
    /// 若 n 為 4 的冪，則 n 的二進位表示中有且僅有一個 1，且該 1 必定出現在從最低位 (第 0 位) 起的偶數位。
    /// 例如：n = 16 -> (10000)2，唯一的 1 在第 4 位（偶數），因此 16 = 4^2。
    /// 我們可以用位元運算快速檢查：
    /// 1) n > 0：負數與 0 不可能是 4 的冪。
    /// 2) (n & (n - 1)) == 0：檢查 n 是否只有一個 1（是否為 2 的冪）。
    /// 3) (n & 0xAAAAAAAA) == 0：使用遮罩 0xAAAAAAAA（二進位為 1010...），其奇數位為 1；若與運算結果為 0，表示 n 的 1 不在奇數位（即在偶數位）。
    /// 綜合上述三個條件即能判斷 n 是否為 4 的冪。
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    /// <summary>
    /// 方法三：位元遮罩法，檢查為 2 的冪且唯一的 1 在偶數位
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static bool IsPowerOfFour3(int n)
    {
        // 條件 1: n 必須大於 0，因為負數與 0 都不是任何正整數的次方
        // 條件 2: (n & (n - 1)) == 0 用來判斷 n 是否為 2 的冪（也就是二進位表示只有一個 1）
        // 條 3: 使用遮罩 0xAAAAAAAA（1010... 二進位），遮罩在所有奇數位上為 1。
        //       如果 n 在奇數位上有 1，則 n & 0xAAAAAAAA != 0；若結果為 0，代表 n 的 1 在偶數位上。
        // 綜合三個條件即可判斷 n 是否為 4 的冪。
        return n > 0 && (n & (n - 1)) == 0 && (n & 0xAAAAAAAA) == 0;
    }

    /// <summary>
    /// 執行一組測資，並對三個方法輸出 PASS/FAIL
    /// </summary>
    public static void RunAllTests()
    {
        var tests = new (int n, bool expected)[]
        {
            (1, true),
            (4, true),
            (16, true),
            (5, false),
            (0, false),
            (-4, false),
            (2, false),
            (64, true),
            (1024, true),
        };

        var methods = new (string name, Func<int, bool> fn)[]
        {
            ("IsPowerOfFour", IsPowerOfFour),
            ("IsPowerOfFour2", IsPowerOfFour2),
            ("IsPowerOfFour3", IsPowerOfFour3),
        };

        Console.WriteLine();
        Console.WriteLine("Running tests for three implementations...");

        foreach (var (n, expected) in tests)
        {
            Console.WriteLine($"\nTest n = {n}, expected = {expected}");
            foreach (var (name, fn) in methods)
            {
                bool actual;
                try
                {
                    actual = fn(n);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  {name}: EXCEPTION -> {ex.Message}");
                    continue;
                }

                var result = actual == expected ? "PASS" : "FAIL";
                Console.WriteLine($"  {name}: {actual} -> {result}");
            }
        }

        Console.WriteLine();
        Console.WriteLine("Tests completed.");
    }
}
