namespace leetcode_371;

class Program
{
    /// <summary>
    /// 371. Sum of Two Integers
    /// https://leetcode.com/problems/sum-of-two-integers/description/?envType=problem-list-v2&envId=oizxjoit
    /// 371. 两整数之和
    /// https://leetcode.cn/problems/sum-of-two-integers/description/
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試加法
        int a = 1, b = 2;
        Console.WriteLine($"加法測試:");
        Console.WriteLine($"a = {a}, b = {b} => GetSum(a, b) = {GetSum(a, b)}");
        a = 2; b = 3;
        Console.WriteLine($"a = {a}, b = {b} => GetSum(a, b) = {GetSum(a, b)}");

        // 測試減法
        Console.WriteLine($"\n減法測試:");
        a = 5; b = 3;
        Console.WriteLine($"a = {a}, b = {b} => GetDiff(a, b) = {GetDiff(a, b)}"); // 應該輸出 2
        a = 10; b = 7;
        Console.WriteLine($"a = {a}, b = {b} => GetDiff(a, b) = {GetDiff(a, b)}"); // 應該輸出 3
        a = 3; b = 2;
        Console.WriteLine($"a = {a}, b = {b} => GetDiff(a, b) = {GetDiff(a, b)}"); // 應該輸出 1
    }

    /// <summary>
    /// 原理說明（位元加法）：
	/// 加法可以拆成兩個部分：
	/// 1. 不進位的加法：用 XOR（^）來做。
	/// 2. 進位值的處理：用 AND（&）再左移一位（<< 1）。
	/// 然後重複這兩個步驟，直到沒有進位為止。
    /// 
    /// 條件	作用
    /// b != 0	還有進位，繼續處理
    /// a == 0	沒有影響，因為它只是加總結果的一部分
    /// 
    /// 判斷 b != 0 是因為 只要還有進位，就不能停。
    /// 判斷 a != 0 是多餘的，因為 a 本來就可能是 0、或變來變去，重點是 b。
    /// a 是「目前的加總結果」，它會一直更新。
    /// 我們 不需要它等於 0 才停，因為 a 有可能一開始就是非零，而且我們的目的就是要得到 a + b 的結果。
    /// 最後當 b == 0，代表沒有新的進位了，此時的 a 就是我們要的結果！
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int GetSum(int a, int b) 
    {
        // 1. 计算 a 和 b 的按位与，得到进位 carry
        // 2. 计算 a 和 b 的按位异或，得到不带进位的和 sum
        // 3. 将 carry 左移一位，准备加到 sum 上
        // 4. 重复步骤 1-3，直到 carry 为 0
        // 此方法使用位元運算來實現兩數相加，不使用 + 運算符
        // 例如: 計算 3 + 2
        // 3 的二進位是 0011
        // 2 的二進位是 0010
        
        while (b != 0) 
        {
            // Step 1: 計算進位 (carry)
            // 使用位元AND運算 (&) 找出哪些位置需要進位
            // 例如: 0011 & 0010 = 0010 (表示第1位需要進位)
            int carry = a & b;

            // Step 2: 計算不帶進位的和
            // 使用位元XOR運算 (^) 計算單純的相加結果
            // 例如: 0011 ^ 0010 = 0001
            a = a ^ b;

            // Step 3: 進位處理
            // 將進位值左移一位 (<<1)，因為進位要加到左邊一位
            // 例如: 0010 << 1 = 0100
            b = carry << 1;

            // Step 4: 重複此過程
            // 現在 a = 0001, b = 0100
            // 繼續循環直到沒有進位 (b = 0)
        }
        
        // 最終 a 包含了完整的加法結果
        return a;
    }

    /// <summary>
    /// 減法實現原理：
    /// 1. 減法可以轉換為加上一個數的補數
    /// 2. 例如：5-3 = 5+(-3)，所以我們需要計算 -3
    /// 3. 在電腦中，負數是用二補數表示：
    ///    - 先對數字取反(NOT操作)
    ///    - 然後加1
    /// 4. 最後用之前的加法函數計算結果
    /// </summary>
    /// <param name="a">被減數</param>
    /// <param name="b">減數</param>
    /// <returns>差值</returns>
    public static int GetDiff(int a, int b)
    {
        // Step 1: 將減法轉換為加法
        // 計算 b 的負數 (-b)
        // 在二進位中，負數是用二補數表示
        // 例如：要計算 5-3：
        // 3 的二進位：0011
        // 1. 取反：1100
        // 2. 加1：1101 (這就是-3的二補數表示)
        
        // 使用 ~ 運算符取反，再加1
        return GetSum(a, GetSum(~b, 1));
    }
}
