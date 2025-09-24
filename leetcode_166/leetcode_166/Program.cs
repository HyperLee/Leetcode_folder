using System.Text;

namespace leetcode_166;

class Program
{
    /// <summary>
    /// 166. Fraction to Recurring Decimal
    /// https://leetcode.com/problems/fraction-to-recurring-decimal/description/?envType=daily-question&envId=2025-09-24
    ///
    /// Given two integers representing the numerator and denominator of a fraction, return the fraction in string format.
    /// If the fractional part is repeating, enclose the repeating part in parentheses.
    /// If multiple answers are possible, return any of them.
    /// It is guaranteed that the length of the answer string is less than 10^4 for all the given inputs.
    ///
    /// 166. 分數到小數
    /// https://leetcode.cn/problems/fraction-to-recurring-decimal/description/?envType=daily-question&envId=2025-09-24
    ///
    /// 給定兩個整數，分別表示一個分數的分子和分母，請將該分數以字串形式返回。
    /// 如果小數部分有循環重複，請將重複的部分用括號括起來。
    /// 如果存在多種可能的答案，返回任意一個即可。
    /// 保證對於所有給定的輸入，答案字串的長度小於 10^4。
    ///
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program program = new Program();
        
        // 測試案例
        Console.WriteLine("=== LeetCode 166: Fraction to Recurring Decimal ===\n");
        
        // 測試案例 1: 有限小數
        Console.WriteLine("測試案例 1: 9/8");
        Console.WriteLine($"結果: {program.FractionToDecimal(9, 8)}");
        Console.WriteLine("預期: 1.125\n");
        
        // 測試案例 2: 無限循環小數
        Console.WriteLine("測試案例 2: 3/14");
        Console.WriteLine($"結果: {program.FractionToDecimal(3, 14)}");
        Console.WriteLine("預期: 0.2(142857)\n");
        
        // 測試案例 3: 整數
        Console.WriteLine("測試案例 3: 4/2");
        Console.WriteLine($"結果: {program.FractionToDecimal(4, 2)}");
        Console.WriteLine("預期: 2\n");
        
        // 測試案例 4: 負數
        Console.WriteLine("測試案例 4: -1/2");
        Console.WriteLine($"結果: {program.FractionToDecimal(-1, 2)}");
        Console.WriteLine("預期: -0.5\n");
        
        // 測試案例 5: 經典循環小數
        Console.WriteLine("測試案例 5: 1/3");
        Console.WriteLine($"結果: {program.FractionToDecimal(1, 3)}");
        Console.WriteLine("預期: 0.(3)\n");
    }

    /// <summary>
    /// 使用長除法計算分數的小數表示
    /// 
    /// 解題思路：
    /// 1. 處理符號：如果分子分母符號不同，結果為負數
    /// 2. 計算整數部分：quotient = numerator / denominator
    /// 3. 計算初始餘數：remainder = numerator % denominator
    /// 4. 如果餘數為0，直接返回整數部分
    /// 5. 使用長除法計算小數部分：
    ///    - 餘數乘以10，計算下一位商
    ///    - 用哈希表記錄每個餘數第一次出現的位置
    ///    - 如果餘數重複出現，說明進入循環節
    ///    - 將循環部分用括號括起來
    /// 
    /// 範例：
    /// - 9/8 = 1.125 (有限小數)
    /// - 3/14 = 0.2(142857) (無限循環小數)
    /// 
    /// quotient（商）
    /// remainder（餘數）
    /// </summary>
    /// <param name="numerator">分子</param>
    /// <param name="denominator">分母</param>
    /// <returns>分數的字串表示，循環部分用括號括起來</returns>
    public string FractionToDecimal(int numerator, int denominator)
    {
        // 使用 long 避免 int 溢出問題
        long longNumerator = numerator;
        long longDenominator = denominator;
        
        // 判斷結果符號：分子分母符號不同時為負數
        string sign = longNumerator * longDenominator < 0 ? "-" : "";
        
        // 保證後續計算過程不產生負數
        longNumerator = Math.Abs(longNumerator);
        longDenominator = Math.Abs(longDenominator);

        // 計算整數部分和初始餘數
        long quotient = longNumerator / longDenominator;
        long remainder = longNumerator % longDenominator;
        
        // 如果沒有餘數，直接返回整數部分
        if (remainder == 0)
        {
            return sign + quotient.ToString();
        }

        // 建構小數結果：符號 + 整數部分 + 小數點
        StringBuilder result = new StringBuilder();
        result.Append(sign);
        result.Append(quotient);
        result.Append(".");
        
        // 用哈希表記錄餘數對應的小數位置，檢測循環節
        Dictionary<long, int> remainderToPosition = new Dictionary<long, int>();
        
        // 長除法計算小數部分
        while (remainder > 0)
        {
            // 餘數乘以10，準備計算下一位小數
            remainder *= 10;
            quotient = remainder / longDenominator;
            remainder %= longDenominator;
            
            // 將當前位數字加入結果
            result.Append(quotient);
            
            // 檢查是否進入循環節
            if (remainderToPosition.ContainsKey(remainder))
            {
                // 找到循環節開始位置
                int cycleStartPosition = remainderToPosition[remainder];
                // 將循環部分用括號括起來
                return result.ToString().Substring(0, cycleStartPosition) + 
                       "(" + result.ToString().Substring(cycleStartPosition) + ")";
            }
            
            // 記錄當前餘數對應的位置
            remainderToPosition[remainder] = result.Length;
        }
        
        // 有限小數，直接返回結果
        return result.ToString();
    }
}
