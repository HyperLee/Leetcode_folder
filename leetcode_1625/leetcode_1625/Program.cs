namespace leetcode_1625;

class Program
{
    /// <summary>
    /// 1625. Lexicographically Smallest String After Applying Operations
    /// https://leetcode.com/problems/lexicographically-smallest-string-after-applying-operations/description/?envType=daily-question&envId=2025-10-19
    /// 1625. 執行操作後字典序最小的字串
    /// https://leetcode.cn/problems/lexicographically-smallest-string-after-applying-operations/description/?envType=daily-question&envId=2025-10-19
    /// 
    /// 給定一個由 0 到 9 的數字組成的偶數長度的字串 s，以及兩個整數 a 和 b。
    /// 您可以對 s 應用以下兩個操作中的任意一個任意次數，並以任意順序：
    /// 將 a 加到 s 的所有奇數索引（索引從 0 開始）上。超過 9 的數字循環回到 0。
    /// 例如，如果 s = "3456" 且 a = 5，s 變為 "3951"。
    /// 將 s 向右旋轉 b 個位置。例如，如果 s = "3456" 且 b = 1，s 變為 "6345"。
    /// 返回通過對 s 應用上述操作任意次數所能獲得的字典序最小的字串。
    /// 一個字串 a 在字典序上小於字串 b（長度相同）如果在 a 和 b 第一次不同的位置，
    /// 字串 a 在該位置的字母在字母表中比字串 b 對應的字母出現得更早。
    /// 例如，"0158" 在字典序上小於 "0190"，因為它們在第三個字母處第一次不同，且 '5' 在 '9' 之前。
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
