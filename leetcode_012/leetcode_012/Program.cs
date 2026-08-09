using System.Text;

namespace leetcode_012;

class Program
{
    /// <summary>
    /// 12. Integer to Roman
    /// https://leetcode.com/problems/integer-to-roman/description/
    /// <para>
    /// Seven different symbols represent Roman numerals with the following values:
    /// Symbol: I, V, X, L, C, D, M
    /// Value: 1, 5, 10, 50, 100, 500, 1000
    ///
    /// Roman numerals are formed by appending the conversions of decimal place values from highest to lowest. Converting a decimal place value into a Roman numeral has the following rules:
    /// - If the value does not start with 4 or 9, select the symbol of the maximal value that can be subtracted from the input, append that symbol to the result, subtract its value, and convert the remainder to a Roman numeral.
    /// - If the value starts with 4 or 9, use the subtractive form representing one symbol subtracted from the following symbol. For example, 4 is one (I) less than five (V), written IV, and 9 is one (I) less than ten (X), written IX. Only these subtractive forms are used: 4 (IV), 9 (IX), 40 (XL), 90 (XC), 400 (CD), and 900 (CM).
    /// - Only powers of 10 (I, X, C, M) can be appended consecutively at most three times to represent multiples of 10. You cannot append 5 (V), 50 (L), or 500 (D) multiple times. If a symbol would need to be appended four times, use the subtractive form.
    ///
    /// Given an integer, convert it to a Roman numeral.
    ///
    /// Example 1:
    /// Input: num = 3749
    /// Output: "MMMDCCXLIX"
    /// Explanation:
    /// 3000 = MMM as 1000 (M) + 1000 (M) + 1000 (M)
    /// 700 = DCC as 500 (D) + 100 (C) + 100 (C)
    /// 40 = XL as 10 (X) less than 50 (L)
    /// 9 = IX as 1 (I) less than 10 (X)
    /// Note: 49 is not 1 (I) less than 50 (L) because the conversion is based on decimal places.
    ///
    /// Example 2:
    /// Input: num = 58
    /// Output: "LVIII"
    /// Explanation:
    /// 50 = L
    /// 8 = VIII
    ///
    /// Example 3:
    /// Input: num = 1994
    /// Output: "MCMXCIV"
    /// Explanation:
    /// 1000 = M
    /// 900 = CM
    /// 90 = XC
    /// 4 = IV
    ///
    /// Constraints:
    /// - 1 &lt;= num &lt;= 3999
    /// </para>
    /// <para>
    /// 12. 整數轉羅馬數字
    /// https://leetcode.cn/problems/integer-to-roman/description/
    ///
    /// 七個不同的符號代表羅馬數字，其值如下：
    /// 符號：I、V、X、L、C、D、M
    /// 數值：1、5、10、50、100、500、1000
    ///
    /// 羅馬數字是由最高位到最低位，依序附加各十進位位值的轉換結果而成。將一個十進位位值轉換成羅馬數字時遵循下列規則：
    /// - 若數值不是以 4 或 9 開頭，選擇可從輸入值中減去的最大數值符號，將該符號附加到結果，減去它的值，再將餘數轉換成羅馬數字。
    /// - 若數值以 4 或 9 開頭，使用減法形式，表示從後一個符號中減去前一個符號。例如，4 比 5 (V) 少 1 (I)，寫作 IV；9 比 10 (X) 少 1 (I)，寫作 IX。只使用下列減法形式：4 (IV)、9 (IX)、40 (XL)、90 (XC)、400 (CD) 和 900 (CM)。
    /// - 只有 10 的次方所對應的符號（I、X、C、M）可以連續附加，且至多三次，以表示 10 的倍數。5 (V)、50 (L) 或 500 (D) 不可重複附加。若需要將一個符號附加四次，應改用減法形式。
    ///
    /// 給定一個整數，請將它轉換成羅馬數字。
    ///
    /// 範例 1：
    /// 輸入：num = 3749
    /// 輸出："MMMDCCXLIX"
    /// 解釋：
    /// 3000 = MMM，因為 1000 (M) + 1000 (M) + 1000 (M)
    /// 700 = DCC，因為 500 (D) + 100 (C) + 100 (C)
    /// 40 = XL，因為比 50 (L) 少 10 (X)
    /// 9 = IX，因為比 10 (X) 少 1 (I)
    /// 注意：49 不會表示為比 50 (L) 少 1 (I)，因為轉換是以十進位位值為基礎。
    ///
    /// 範例 2：
    /// 輸入：num = 58
    /// 輸出："LVIII"
    /// 解釋：
    /// 50 = L
    /// 8 = VIII
    ///
    /// 範例 3：
    /// 輸入：num = 1994
    /// 輸出："MCMXCIV"
    /// 解釋：
    /// 1000 = M
    /// 900 = CM
    /// 90 = XC
    /// 4 = IV
    ///
    /// 限制條件：
    /// - 1 &lt;= num &lt;= 3999
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("=== LeetCode 12: 整數轉羅馬數字 測試 ===");
        
        Program program = new Program();
        
        // 測試案例
        int[] testCases = { 3, 4, 9, 58, 1994, 3749 };
        string[] expectedResults = { "III", "IV", "IX", "LVIII", "MCMXCIV", "MMMDCCXLIX" };
        
        Console.WriteLine("\n測試結果:");
        for (int i = 0; i < testCases.Length; i++)
        {
            int num = testCases[i];
            string result = program.IntToRoman(num);
            string expected = expectedResults[i];
            bool isCorrect = result == expected;
            
            Console.WriteLine($"輸入: {num,4} => 輸出: {result,-10} | 預期: {expected,-10} | {(isCorrect ? "✓ 正確" : "✗ 錯誤")}");
        }
        
        Console.WriteLine("\n=== 測試完成 ===");
    }

    /// <summary>
    /// 羅馬數字轉換映射表 - 貪婪演算法的核心資料結構
    /// 
    /// 【設計理念】
    /// 這個 Tuple 陣列包含了所有需要的「阿拉伯數字-羅馬符號」對應關係，
    /// 特別預先處理了羅馬數字的減法組合（如 IV=4, IX=9, XL=40, XC=90, CD=400, CM=900），
    /// 避免在演算法執行時進行複雜的邏輯判斷。
    /// 
    /// 【排序策略】
    /// 陣列按數值大到小排序（1000 → 1），這是貪婪演算法成功的關鍵：
    /// - 確保優先使用較大的羅馬符號，符合羅馬數字的標準書寫慣例
    /// - 自動處理特殊的減法組合，如 900(CM) 在 500(D) 之前，400(CD) 在 100(C) 之前
    /// - 避免產生錯誤的組合，如 DCCCC（錯誤）vs CM（正確）
    /// 
    /// 【完整性保證】
    /// 包含了 1-3999 範圍內所有可能需要的羅馬數字組合：
    /// - 基本符號：M(1000), D(500), C(100), L(50), X(10), V(5), I(1)
    /// - 減法組合：CM(900), CD(400), XC(90), XL(40), IX(9), IV(4)
    /// 
    /// 【技術實作】
    /// 使用 Tuple<int, string> 建立強型別的數值-符號對應，
    /// 提供型別安全和良好的可讀性，方便後續的查表操作。
    /// 
    /// 【演算法支援】
    /// 這個設計使得轉換演算法變得極其簡單：
    /// 只需從頭到尾遍歷陣列，對每個數值盡可能多地使用對應符號即可。
    /// </summary>
    static readonly Tuple<int, string>[] valueSymbols = 
    {
        new Tuple<int, string>(1000, "M"),
        new Tuple<int, string>(900, "CM"),
        new Tuple<int, string>(500, "D"),
        new Tuple<int, string>(400, "CD"),
        new Tuple<int, string>(100, "C"),
        new Tuple<int, string>(90, "XC"),
        new Tuple<int, string>(50, "L"),
        new Tuple<int, string>(40, "XL"),
        new Tuple<int, string>(10, "X"),
        new Tuple<int, string>(9, "IX"),
        new Tuple<int, string>(5, "V"),
        new Tuple<int, string>(4, "IV"),
        new Tuple<int, string>(1, "I")
    };

    /// <summary>
    /// LeetCode 12: 整數轉羅馬數字 - 解題說明
    /// 
    /// 【解題思路】
    /// 這道題的核心思想是「貪婪演算法」：
    /// 1. 從最大的羅馬數字符號開始，盡可能多地使用它們
    /// 2. 當無法使用當前符號時，移動到下一個較小的符號
    /// 3. 重複此過程直到數字變為 0
    /// 
    /// 【關鍵洞察】
    /// - 羅馬數字的特殊組合（如 IV=4, IX=9, XL=40 等）需要預先處理
    /// - 使用 Tuple 陣列建立「數值-符號」對應表，按數值大到小排序
    /// - 這樣可以確保優先使用較大的符號，符合羅馬數字的書寫規則
    /// 
    /// 【演算法步驟】
    /// 1. 建立包含所有可能數值和對應羅馬符號的映射表
    /// 2. 從最大數值開始遍歷映射表
    /// 3. 對每個數值，計算它在輸入數字中能使用多少次
    /// 4. 累加對應的羅馬符號到結果字串
    /// 5. 從輸入數字中減去已處理的部分
    /// 6. 繼續處理剩餘數字，直到變為 0
    /// 
    /// 【時間複雜度】O(1) - 因為羅馬數字符號數量固定
    /// 【空間複雜度】O(1) - 使用固定大小的映射表
    /// 
    /// ref:
    /// https://leetcode.cn/problems/integer-to-roman/solutions/774611/zheng-shu-zhuan-luo-ma-shu-zi-by-leetcod-75rs/
    /// https://leetcode.cn/problems/integer-to-roman/solutions/87905/tan-xin-ha-xi-biao-tu-jie-by-ml-zimingmeng/
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public string IntToRoman(int num)
    {
        // 使用 StringBuilder 提高字串拼接效率
        StringBuilder sb = new StringBuilder();

        // 遍歷預定義的數值-符號對應表，按數值從大到小的順序
        // 這確保了我們優先使用較大的羅馬數字符號
        foreach (Tuple<int, string> tuple in valueSymbols)
        {
            int value = tuple.Item1;    // 取得當前數值（如 1000, 900, 500...）
            string symbol = tuple.Item2; // 取得對應的羅馬符號（如 "M", "CM", "D"...）

            // 使用貪婪策略：盡可能多地使用當前符號
            // 計算當前數值在 num 中能使用多少次
            while(num >= value)
            {
                // 從 num 中減去當前數值
                num -= value;
                
                // 將對應的羅馬符號添加到結果字串
                sb.Append(symbol);
                
                // 例如：num=58, value=50, symbol="L"
                // 第一次：num=58-50=8, sb="L"
                // 下次迴圈：num=8 < 50，退出 while，處理下一個符號
            }

            // 最佳化：如果 num 已經變為 0，提前結束遍歷
            if(num == 0)
            {
                break;
            }
        }

        // 回傳完整的羅馬數字字串
        return sb.ToString();        
    }
}
