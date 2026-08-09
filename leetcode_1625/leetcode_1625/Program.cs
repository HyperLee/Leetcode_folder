namespace leetcode_1625;

class Program
{
    /// <summary>
    /// <para>
    /// 1625. Lexicographically Smallest String After Applying Operations
    /// https://leetcode.com/problems/lexicographically-smallest-string-after-applying-operations/description/
    ///
    /// You are given an even-length string s consisting of digits 0 through 9, and integers a and b. Apply either operation
    /// any number of times in any order:
    /// - Add a to all odd indices (0-indexed); digits past 9 cycle to 0. For s = "3456", a = 5 gives "3951".
    /// - Rotate s right by b positions. For s = "3456", b = 1 gives "6345".
    /// Return the lexicographically smallest obtainable string. For equal-length strings, a string is smaller if at the first
    /// differing position its digit comes earlier; for example, "0158" is smaller than "0190" because '5' precedes '9'.
    ///
    /// Example 1:
    /// Input: s = "5525", a = 9, b = 2
    /// Output: "2050"
    /// Explanation: Start "5525"; Rotate "2555"; Add "2454"; Add "2353"; Rotate "5323"; Add "5222"; Add "5121";
    /// Rotate "2151"; Add "2050". No smaller string is obtainable.
    ///
    /// Example 2:
    /// Input: s = "74", a = 5, b = 1
    /// Output: "24"
    /// Explanation: Start "74"; Rotate "47"; Add "42"; Rotate "24". No smaller string is obtainable.
    ///
    /// Example 3:
    /// Input: s = "0011", a = 4, b = 2
    /// Output: "0011"
    /// Explanation: No sequence of operations produces a lexicographically smaller string.
    ///
    /// Constraints:
    /// - 2 &lt;= s.length &lt;= 100
    /// - s.length is even.
    /// - s consists only of digits 0 through 9.
    /// - 1 &lt;= a &lt;= 9
    /// - 1 &lt;= b &lt;= s.length - 1
    /// </para>
    /// <para>
    /// 1625. 執行操作後字典序最小的字串
    /// https://leetcode.cn/problems/lexicographically-smallest-string-after-applying-operations/description/
    ///
    /// 給定由數字 0 到 9 組成的偶數長度字串 s，以及整數 a 與 b。可以任意順序、任意次數執行：
    /// - 將 a 加到所有奇數索引（從 0 開始）的數字；超過 9 會循環回 0。例如 s = "3456"、a = 5 得到 "3951"。
    /// - 將 s 向右旋轉 b 個位置。例如 s = "3456"、b = 1 得到 "6345"。
    /// 回傳可得到的字典序最小字串。對長度相同的字串，在第一個不同位置上數字較前者較小；例如 "0158"
    /// 小於 "0190"，因為 '5' 位於 '9' 之前。
    ///
    /// 範例 1：
    /// 輸入：s = "5525"，a = 9，b = 2
    /// 輸出："2050"
    /// 解釋：起始 "5525"；旋轉 "2555"；加法 "2454"；加法 "2353"；旋轉 "5323"；加法 "5222"；
    /// 加法 "5121"；旋轉 "2151"；加法 "2050"。無法得到更小的字串。
    ///
    /// 範例 2：
    /// 輸入：s = "74"，a = 5，b = 1
    /// 輸出："24"
    /// 解釋：起始 "74"；旋轉 "47"；加法 "42"；旋轉 "24"。無法得到更小的字串。
    ///
    /// 範例 3：
    /// 輸入：s = "0011"，a = 4，b = 2
    /// 輸出："0011"
    /// 解釋：不存在能產生字典序更小字串的操作序列。
    ///
    /// 限制條件：
    /// - 2 &lt;= s.length &lt;= 100
    /// - s.length 為偶數。
    /// - s 只包含數字 0 到 9。
    /// - 1 &lt;= a &lt;= 9
    /// - 1 &lt;= b &lt;= s.length - 1
    /// </para>
    /// </summary>
    static void Main(string[] args)
    {
        Program program = new Program();
        
        // 測試案例 1
        string s1 = "5525";
        int a1 = 9, b1 = 2;
        string result1 = program.FindLexSmallestString(s1, a1, b1);
        Console.WriteLine($"測試案例 1: s=\"{s1}\", a={a1}, b={b1}");
        Console.WriteLine($"預期輸出: \"2050\", 實際輸出: \"{result1}\"");
        Console.WriteLine($"結果: {(result1 == "2050" ? "通過" : "失敗")}");
        Console.WriteLine();
        
        // 測試案例 2
        string s2 = "74";
        int a2 = 5, b2 = 1;
        string result2 = program.FindLexSmallestString(s2, a2, b2);
        Console.WriteLine($"測試案例 2: s=\"{s2}\", a={a2}, b={b2}");
        Console.WriteLine($"預期輸出: \"24\", 實際輸出: \"{result2}\"");
        Console.WriteLine($"結果: {(result2 == "24" ? "通過" : "失敗")}");
        Console.WriteLine();
        
        // 測試案例 3
        string s3 = "0011";
        int a3 = 4, b3 = 2;
        string result3 = program.FindLexSmallestString(s3, a3, b3);
        Console.WriteLine($"測試案例 3: s=\"{s3}\", a={a3}, b={b3}");
        Console.WriteLine($"預期輸出: \"0011\", 實際輸出: \"{result3}\"");
        Console.WriteLine($"結果: {(result3 == "0011" ? "通過" : "失敗")}");
        Console.WriteLine();
        
        // 額外測試案例
        string s4 = "1234";
        int a4 = 3, b4 = 1;
        string result4 = program.FindLexSmallestString(s4, a4, b4);
        Console.WriteLine($"額外測試案例: s=\"{s4}\", a={a4}, b={b4}");
        Console.WriteLine($"輸出: \"{result4}\"");
    }

    /// <summary>
    /// 找出執行操作後字典序最小的字串
    /// 
    /// 解題思路：
    /// 1. 題目提供兩種操作：
    ///    - 累加操作：將奇數位的數字加上 a（超過 9 則回到 0）
    ///    - 輪轉操作：將字串向右輪轉 b 位
    /// 
    /// 2. 關鍵觀察：
    ///    - 如果 b 是偶數，無論輪轉多少次，只能對奇數位進行累加操作
    ///    - 如果 b 是奇數，可以對奇數位和偶數位都進行累加操作（且可以做不同次數）
    ///    - 累加操作和輪轉操作是獨立的
    /// 
    /// 3. 解法：枚舉法
    ///    - 先枚舉輪轉次數（最多 n 次就會循環）
    ///    - 對每個輪轉結果，枚舉累加次數（最多 10 次就會循環，因為數字範圍是 0-9）
    ///    - 比較所有可能的結果，找出字典序最小的字串
    /// 
    /// 時間複雜度：O(n² × 10 × 10) = O(n²)
    /// 空間複雜度：O(n)
    /// </summary>
    /// <param name="s">由 0-9 組成的偶數長度字串</param>
    /// <param name="a">累加的數值</param>
    /// <param name="b">輪轉的位數</param>
    /// <returns>字典序最小的字串</returns>
    public string FindLexSmallestString(string s, int a, int b)
    {
        int n = s.Length;
        bool[] vis = new bool[n]; // 記錄每個輪轉位置是否已訪問過
        string res = s; // 儲存目前找到的最小字串
        s = s + s; // 將字串複製一份接在後面，方便擷取輪轉後的字串

        // 枚舉輪轉操作：從位置 0 開始，每次右移 b 位，直到遇到已訪問過的位置
        for (int i = 0; !vis[i]; i = (i + b) % n)
        {
            vis[i] = true;
            
            // 枚舉對奇數位做累加操作的次數（0-9 次）
            for (int j = 0; j < 10; j++)
            {
                // 如果 b 是偶數，則無法對偶數位做累加操作（kLimit = 0）
                // 如果 b 是奇數，則可以對偶數位做累加操作（kLimit = 9）
                int kLimit = b % 2 == 0 ? 0 : 9;
                
                // 枚舉對偶數位做累加操作的次數
                for (int k = 0; k <= kLimit; k++)
                {
                    // 取得輪轉後的字串
                    char[] t = s.Substring(i, n).ToCharArray();
                    
                    // 對奇數位（索引 1, 3, 5, ...）進行累加操作
                    for (int p = 1; p < n; p += 2)
                    {
                        t[p] = (char)('0' + (t[p] - '0' + j * a) % 10);
                    }

                    // 對偶數位（索引 0, 2, 4, ...）進行累加操作
                    for (int p = 0; p < n; p += 2)
                    {
                        t[p] = (char)('0' + (t[p] - '0' + k * a) % 10);
                    }

                    // 將字元陣列轉回字串並與目前最小值比較
                    string tStr = new string(t);
                    if (tStr.CompareTo(res) < 0)
                    {
                        res = tStr;
                    }
                }
            }
        }
        return res;
    }
}
