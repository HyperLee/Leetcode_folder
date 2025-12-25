using System.Text;

namespace leetcode_012;

class Program
{
    /// <summary>
    /// 12. Integer to Roman
    /// https://leetcode.com/problems/integer-to-roman/description/
    /// 
    /// 繁體中文：12. 整數轉羅馬數字
    /// 題目描述：
    /// 七個不同的符號代表羅馬數字，對應數值如下：
    /// I - 1, V - 5, X - 10, L - 50, C - 100, D - 500, M - 1000
    /// 羅馬數字由高位到低位依序附加各位的羅馬表示，轉換規則如下：
    /// - 若數值不以 4 或 9 開頭，選擇可被減去的最大符號，附加該符號並減去其值，對餘數重複轉換（I、X、C、M 最多連續三次；V、L、D 不可重複）。
    /// - 若數值以 4 或 9 開頭，使用減法記法：4 -> IV、9 -> IX、40 -> XL、90 -> XC、400 -> CD、900 -> CM。
    /// 給定一個整數，將其轉換為相對應的羅馬數字。
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
