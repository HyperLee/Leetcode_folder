namespace leetcode_1137;

class Program
{
    /// <summary>
    /// 1137. N-th Tribonacci Number
    /// https://leetcode.com/problems/n-th-tribonacci-number/description/
    /// 1137. 第 N 個泰波那契數
    /// https://leetcode.cn/problems/n-th-tribonacci-number/description/
    /// 
    /// 泰波那契數列 Tn 定義如下：
    /// T0 = 0，T1 = 1，T2 = 1，且對於 n >= 0，有 Tn+3 = Tn + Tn+1 + Tn+2。
    /// 給定 n，回傳 Tn 的值。 這是題目描述（繁體中文翻譯）
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        var program = new Program();
        
        // 測試資料
        int[] testCases = [0, 1, 2, 3, 4, 5, 10, 25];
        
        Console.WriteLine("=== 泰波那契數列測試 ===");
        Console.WriteLine("T(n) = T(n-1) + T(n-2) + T(n-3)");
        Console.WriteLine("T(0) = 0, T(1) = 1, T(2) = 1");
        Console.WriteLine();
        
        foreach (var n in testCases)
        {
            int result = program.Tribonacci(n);
            Console.WriteLine($"Tribonacci({n}) = {result}");
        }
    }

    /// <summary>
    /// 計算第 N 個泰波那契數（Tribonacci Number）。
    /// 
    /// <para><b>解題思路：迭代實作動態規劃</b></para>
    /// <para>
    /// 題目已直接給出狀態轉移方程：Tn+3 = Tn + Tn+1 + Tn+2，
    /// 本質上是一道模擬題。
    /// </para>
    /// 
    /// <para><b>演算法說明：</b></para>
    /// <para>
    /// 使用三個變數 (a, b, c) 分別記錄連續三個泰波那契數，
    /// 從前往後迭代計算一遍即可。
    /// </para>
    /// 
    /// <para><b>時間複雜度：</b> O(n) - 只需遍歷一次</para>
    /// <para><b>空間複雜度：</b> O(1) - 只使用常數個變數</para>
    /// </summary>
    /// <param name="n">要計算的泰波那契數索引，n >= 0</param>
    /// <returns>第 n 個泰波那契數 T(n)</returns>
    /// <example>
    /// <code>
    /// var program = new Program();
    /// int result = program.Tribonacci(4); // 回傳 4 (序列: 0, 1, 1, 2, 4)
    /// </code>
    /// </example>
    public int Tribonacci(int n)
    {
        // 基底情況：T(0) = 0
        if (n == 0)
        {
            return 0;
        }

        // 基底情況：T(1) = T(2) = 1
        if (n == 1 || n == 2)
        {
            return 1;
        }

        // 初始化三個變數代表連續的泰波那契數
        // a = T(0) = 0, b = T(1) = 1, c = T(2) = 1
        int a = 0, b = 1, c = 1, d = 0;
        
        // 迭代計算 T(3) 到 T(n)
        // 狀態轉移方程：d = T(i) = T(i-3) + T(i-2) + T(i-1) = a + b + c
        for (int i = 3; i <= n; i++)
        {
            d = a + b + c;  // 計算新的泰波那契數
            a = b;          // 滑動視窗：a 移動到 b 的位置
            b = c;          // 滑動視窗：b 移動到 c 的位置
            c = d;          // 滑動視窗：c 移動到新計算的 d
        }
        
        return d;
    }
}
