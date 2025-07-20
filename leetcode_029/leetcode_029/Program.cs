namespace leetcode_029;

class Program
{
    /// <summary>
    /// 29. Divide Two Integers
    /// https://leetcode.com/problems/divide-two-integers/description/
    /// 29. 两数相除
    /// https://leetcode.cn/problems/divide-two-integers/description/
    ///
    /// 題目描述：
    /// 給定兩個整數 dividend 和 divisor，不使用乘法、除法和 mod 運算子，實現兩數相除。
    /// 整數除法需向零截斷（即捨去小數部分）。
    /// 返回相除後的商，若商超過 32 位元整數範圍，則回傳對應邊界值。
    ///
    /// 解題說明：
    /// 1. 為避免溢位，全程將 dividend 與 divisor 轉為負數處理（負數範圍比正數大）。
    /// 2. 先判斷特殊溢位情況（如 int.MinValue / -1）。
    /// 3. 判斷最終結果正負號，計算時皆以負數進行。
    /// 4. 透過倍增法（每次將除數與商倍增），高效逼近被除數，避免 long 型別。
    /// 5. 若倍增過程超過 int.MinValue 一半（-1073741824），則停止倍增以防溢位。
    ///
    /// This is the problem description and solution explanation in Chinese.
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 範例測試
        int dividend = 10;
        int divisor = 3;
        int result = Divide(dividend, divisor);
        Console.WriteLine($"{dividend} / {divisor} = {result}");
    }

    /// <summary>
    /// 將兩個整數相除，不使用乘法、除法和 mod 運算子。
    /// 全程以負數進行運算，避免溢位。
    /// </summary>
    /// <param name="dividend">被除數</param>
    /// <param name="divisor">除數</param>
    /// <returns>商，已處理 32 位元整數溢位</returns>
    public static int Divide(int dividend, int divisor)
    {
        // 32 位元整數邊界
        const int MIN = int.MinValue;
        const int MAX = int.MaxValue;
        const int LIMIT = -1073741824; // MIN 的一半，防止倍增溢位

        // 特殊溢位情況：MIN / -1 會超出 int 範圍
        if (dividend == MIN && divisor == -1)
        {
            return MAX;
        }

        // 判斷最終結果正負號
        bool negative = (dividend > 0 && divisor < 0) || (dividend < 0 && divisor > 0);

        // 轉為負數以避免溢位（負數範圍比正數大）
        int a = dividend > 0 ? -dividend : dividend;
        int b = divisor > 0 ? -divisor : divisor;
        int ans = 0;
        // 以倍增法逼近被除數
        while (a <= b)
        {
            int c = b, d = -1;
            // 倍增過程，防止溢位（c >= LIMIT），且 c >= a - c 保證不超過 a
            while (c >= LIMIT && d >= LIMIT && c >= a - c)
            {
                c += c; // 除數倍增
                d += d; // 商倍增
            }
            a -= c; // 減去當前倍增後的除數
            ans += d; // 累加對應倍增後的商
        }
        // 根據正負號回傳結果
        return negative ? ans : -ans;
    }
}
