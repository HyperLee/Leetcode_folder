namespace leetcode_279;

class Program
{
    /// <summary>
    /// 279. Perfect Squares
    /// https://leetcode.com/problems/perfect-squares/
    ///
    /// 題目描述（繁體中文）：這是題目描述
    /// 給定一個整數 n，回傳組成 n 所需的最少完全平方數數量。
    /// 完全平方數是某個整數的平方，例如 1、4、9、16；3 和 11 則不是。
    ///
    /// 279. 完全平方數
    /// https://leetcode.cn/problems/perfect-squares/description/
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 動態規劃
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public int NumSquares(int n)
    {
        int[] f = new int[n + 1];
        for(int i = 1; i <= n; i++)
        {
            int min = int.MaxValue;
            for(int j = 1; j * j <= i; j++)
            {
                min = Math.Min(min, f[i - j * j]);
            }
            f[i] = min + 1;
        }
        return f[n];
    }
}
