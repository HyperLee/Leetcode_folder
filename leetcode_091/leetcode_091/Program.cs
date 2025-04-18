namespace leetcode_091;

class Program
{
    /// <summary>
    /// 91. Decode Ways
    /// https://leetcode.com/problems/decode-ways/description/?envType=problem-list-v2&envId=oizxjoit
    /// 91. 解码方法
    /// https://leetcode.cn/problems/decode-ways/description/
    /// 
    /// 題目描述：
    /// 一條包含字母 A-Z 的訊息可以按照以下規則進行編碼：
    /// 'A' -> "1"
    /// 'B' -> "2"
    /// ...
    /// 'Z' -> "26"
    /// 
    /// 要解碼一個已編碼的訊息，所有數字必須被映射回字母（可能有多種方法）。
    /// 例如，"11106" 可以被映射為：
    /// "AAJF"，對應 (1 1 10 6)
    /// "KJF"，對應 (11 10 6)
    /// 
    /// 給定一個數字字串 s，請計算有多少種解碼方法。
    /// 
    /// 解題出發點：
    /// 1. 我們使用動態規劃的方法解決此問題，因為需要記錄並利用先前計算的結果
    /// 2. 針對每個位置的數字，考慮兩種可能性：單獨解碼或與前一個數字組合解碼
    /// 3. 需要處理特殊情況如 "0" 不能單獨解碼，而 "10" 和 "20" 只能作為整體解碼
    /// 4. 使用 1-based 索引來簡化邊界條件和狀態轉移的處理(index 從 1 開始)
    /// 
    /// 注意:雙位數開頭不能是 0
    /// 例如: 06 -> 錯誤，因為 0 不能單獨解碼
    /// 所以區分兩種案例
    /// 1.個位數 (1-9) 可以單獨解碼
    /// 2.雙位數 (10-26) 可以組合解碼
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program program = new Program();
        
        // 測試用例
        string[] testCases = {
            "12",       // 可以解碼為 "AB"(1 2) 或 "L"(12)，應返回 2
            "226",      // 可以解碼為 "BZ"(2 26)、"VF"(22 6) 或 "BBF"(2 2 6)，應返回 3
            "06",       // 無法解碼，應返回 0
            "10",       // 只能解碼為 "J"，應返回 1
            "2101",     // 可以解碼為 "BAA"(2 10 1) 或 "UA"(21 0 1)，但 0 不合法，應返回 1
            "123123"    // 較複雜的測試用例
        };
        
        // 預期結果
        int[] expected = { 2, 3, 0, 1, 1, 9 };
        
        Console.WriteLine("LeetCode 91. Decode Ways 測試結果：");
        Console.WriteLine("--------------------------------");
        
        for (int i = 0; i < testCases.Length; i++)
        {
            int result = program.NumDecodings(testCases[i]);
            bool isPassed = result == expected[i];
            
            Console.WriteLine($"測試用例: \"{testCases[i]}\"");
            Console.WriteLine($"預期結果: {expected[i]}");
            Console.WriteLine($"實際結果: {result}");
            Console.WriteLine($"測試 {(isPassed ? "通過" : "失敗")}");
            Console.WriteLine("--------------------------------");
        }
    }


    /// <summary>
    /// 解碼方法 (Decode Ways) 的動態規劃解法
    /// 
    /// 解題概念：
    /// 1. 使用動態規劃 (Dynamic Programming) 解決此問題
    /// 2. 定義 dp[i] 為字串 s 前 i 個字元的解碼方法數量
    /// 3. 考慮兩種可能的解碼方式：單個數字 (1-9) 或兩個數字 (10-26)
    /// 
    /// 解題思路：
    /// - 若當前數字 s[i] 可以單獨解碼 (1-9)，則 dp[i] 可以繼承 dp[i-1] 的解碼方法數
    /// - 若當前數字與前一個數字組合 s[i-1:i] 可以解碼 (10-26)，則 dp[i] 可以加上 dp[i-2] 的解碼方法數
    /// - 若兩種情況都不滿足，則 dp[i] = 0，表示無法解碼
    /// 
    /// 同时，由于在大部分语言中，字符串的下标是从 0 而不是 1 开始的，因此在代码的编写过程中，
    /// 我们需要将所有字符串的下标减去 1，与使用的语言保持一致。
    /// </summary>
    /// <param name="s">包含數字的字串，表示需要解碼的訊息</param>
    /// <returns>可能的解碼方法總數</returns>
    public int NumDecodings(string s) 
    {
        int n = s.Length;
        s = " " + s; // 將索引轉為 1-based，方便後續計算
        int[] dp = new int[n + 1]; // dp[i]表示s[1..i]的解碼方法數
        dp[0] = 1; // 空字串的解碼方法數為1（基礎情況）
        char[] sChar = s.ToCharArray();

        for(int i = 1; i <= n; i++)
        {
            int a = sChar[i] - '0'; // 當前數字的值
            int b = (sChar[i - 1] - '0') * 10 + (sChar[i] - '0'); // 當前數字與前一個數字組合的值

            // 情況1: 當前數字可以單獨解碼（1-9）
            if(1 <= a && a <= 9) // 確保當前字元不是 '0'
            {
                dp[i] = dp[i - 1]; // 繼承前一個狀態的解碼方法數
            }

            // 情況2: 當前數字與前一個數字的組合可以解碼（10-26）
            if(10 <= b && b <= 26) // 確保組合在有效範圍內
            {
                dp[i] += dp[i - 2]; // 加上前兩個狀態的解碼方法數
            }

            // 隱含情況: 如果兩種情況都不滿足，dp[i] 會保持為 0，表示無法解碼
        }
        return dp[n]; // 返回整個字串的解碼方法數
    }
}
