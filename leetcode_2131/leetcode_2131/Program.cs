// filepath: /Users/qiuzili/Leetcode/Leetcode_folder/leetcode_2131/leetcode_2131/Program.cs
using System;
using System.Collections.Generic;

namespace leetcode_2131;

class Program
{
    /// <summary>
    /// 2131. Longest Palindrome by Concatenating Two Letter Words
    /// https://leetcode.com/problems/longest-palindrome-by-concatenating-two-letter-words/description/?envType=daily-question&envId=2025-05-25
    /// 2131. 连接两字母单词得到的最长回文串
    /// https://leetcode.cn/problems/longest-palindrome-by-concatenating-two-letter-words/description/?envType=daily-question&envId=2025-05-25
    /// 
    /// 給定一個字串陣列 words。words 中的每個元素由兩個小寫英文字母組成。
    /// 
    /// 通過從 words 中選擇一些元素並以任意順序將它們連接起來，建立最長的回文字串。每個元素最多只能選擇一次。
    /// 
    /// 返回你能建立的最長回文字串的長度。如果不可能建立任何回文字串，則返回 0。
    /// 
    /// 回文字串是指正向和反向讀取都相同的字串。
    /// </summary>
    /// <param name="args"></param>
    /// <summary>
    /// 主程式進入點，用於測試三種不同解法的效能與正確性
    /// </summary>
    /// <param name="args">命令列引數</param>
    static void Main(string[] args)
    {
        // 測試資料
        string[][] testCases = new string[][] {
            // 測試案例1：基本配對 + 一個回文單詞
            new string[] {"lc", "cl", "gg"},  // "lc" + "cl" + "gg"，預期結果：6
            
            // 測試案例2：多種配對組合
            new string[] {"ab", "ty", "yt", "lc", "cl", "ab"},  // "ab" + "ty" + "yt" + "lc" + "cl" + "ab"，預期結果：8
            
            // 測試案例3：純回文單詞
            new string[] {"cc", "ll", "xx"},  // 只選擇一個，預期結果：2
            
            // 測試案例4：複雜情況，多對單詞和多個回文單詞
            new string[] {"dd", "aa", "bb", "dd", "aa", "dd", "bb", "dd", "aa", "cc", "bb", "cc", "dd", "cc"},  // 預期結果：22
            
            // 測試案例5：混合情況
            new string[] {"ab", "ba", "cc", "aa", "bb", "dd", "aa", "bb", "ee", "ee"}  // 預期結果：18
        };
        
        // 預期結果
        int[] expected = new int[] { 6, 8, 2, 22, 18 };
        
        // 測試並顯示結果
        Console.WriteLine("測試結果：");
        Console.WriteLine("----------------------------");
        Console.WriteLine("| 測試資料                | 解法1 | 解法2 | 解法3 | 預期結果 | 結果 |");
        Console.WriteLine("----------------------------");
        
        for (int i = 0; i < testCases.Length; i++)
        {
            // 取得此測試資料的字串表示
            string testCaseStr = string.Join(", ", testCases[i]);
            if (testCaseStr.Length > 20) 
            {
                testCaseStr = testCaseStr.Substring(0, 17) + "...";
            }
            
            // 使用三種不同的解法
            int result1 = LongestPalindrome(testCases[i]);
            int result2 = LongestPalindrome2(testCases[i]);
            int result3 = LongestPalindrome3(testCases[i]);
            
            // 驗證結果是否一致
            bool isValid = (result1 == expected[i] && result2 == expected[i] && result3 == expected[i]);
            
            // 顯示結果
            Console.WriteLine($"| {testCaseStr,-20} | {result1,-5} | {result2,-5} | {result3,-5} | {expected[i],-10} | {(isValid ? "通過" : "失敗")} |");
        }
        
        Console.WriteLine("----------------------------");
        Console.WriteLine("測試完成！");
    }

    /// <summary>
    /// 通過連接兩字母單詞建立最長回文字串 (字典+單字特性比較法)
    /// 解題思路：使用字典統計每個單詞出現頻率，根據單詞特性將其分為兩類處理：
    /// 1. 回文單詞（如 "aa"）：可以貢獻偶數個放在回文兩側，若有剩餘的奇數個可以放一個在中間
    /// 2. 非回文單詞（如 "ab"）：需要與其反轉形式（如 "ba"）配對使用
    /// 時間複雜度：O(n)，空間複雜度：O(n)，其中 n 是單詞數量
    /// </summary>
    /// <param name="words">包含兩個小寫字母的字串陣列</param>
    /// <returns>最長回文字串的長度</returns>
    public static int LongestPalindrome(string[] words)
    {
        // 使用字典儲存每個單詞的出現次數
        Dictionary<string, int> count = new Dictionary<string, int>();
        int answer = 0;

        // 建立所有字串的頻率字典
        foreach (string word in words)
        {
            if (!count.ContainsKey(word))
            {
                // 如果字典中不存在，則初始化頻率為 1
                count[word] = 1;
            }
            else
            {
                // 如果已經存在，則增加頻率 
                count[word]++;
            }
        }
        
        // 用來標記是否有可以放在中間的單詞
        bool central = false;
        
        foreach (var entry in count) 
        {
            string word = entry.Key;
            int frequency = entry.Value;

            // 處理 "aa", "bb" 等形式的回文單詞（首尾字母相同）
            if (word[0] == word[1])
            {
                // 將偶數個回文單詞放在回文的兩側，每對貢獻 4 個字元
                answer += (frequency / 2) * 4;

                // 如果有奇數個回文單詞，可以將其中一個放在中間
                if (frequency % 2 == 1)
                {
                    central = true; // 標記可以放一個在中間
                }
            }
            // 處理 "ab" 和 "ba" 等需要配對的單詞（首尾字母不同）
            else if (word[0] < word[1])
            {
                // 構造反轉單詞，如 "ab" -> "ba"
                string reversed = "" + word[1] + word[0];

                // 如果反轉單詞存在，就可以形成配對
                // 這邊判斷是否存在，也就是說最小會是一組
                // 不會出現 0 組問題
                if (count.ContainsKey(reversed))
                {
                    // 取較小頻率作為配對數，每對貢獻 4 個字元（2 個單詞各 2 個字元）
                    // word 與 reversed 的頻率取最小值，確保每對只計算一次
                    answer += Math.Min(frequency, count[reversed]) * 4;
                }
            }
        }

        // 如果有一個可以放在中間的回文單詞，增加 2 個字元長度
        if (central)
        { 
            answer += 2;
        }
            
        return answer;
    }


    /// <summary>
    /// 通過連接兩字母單詞建立最長回文字串 (二維陣列實作法)
    /// 解題思路：使用二維陣列儲存每種字母對的出現頻率，根據字母在字母表中的位置作為索引。
    /// 對於回文單詞（如 "aa"），對應陣列的對角線元素；對於非回文單詞（如 "ab"），需要與其反轉形式配對。
    /// 優點是使用二維陣列可以節省空間（僅需 26x26 的空間）並加速查找。
    /// 時間複雜度：O(n + 26²)，空間複雜度：O(26²)，其中 n 是單詞數量
    /// 
    /// ref:
    /// https://leetcode.cn/problems/longest-palindrome-by-concatenating-two-letter-words/solutions/1199641/gou-zao-tan-xin-fen-lei-tao-lun-by-endle-dqr8/?envType=daily-question&envId=2025-05-25
    /// </summary>
    /// <param name="words">包含兩個小寫字母的字串陣列</param>
    /// <returns>最長回文字串的長度</returns>
    public static int LongestPalindrome2(string[] words)
    {
        // 建立二維陣列儲存字母對出現頻率，索引為字母在字母表中的位置 (a=0, b=1, ..., z=25)
        int[][] cnt = new int[26][];
        for (int i = 0; i < 26; i++)
        {
            cnt[i] = new int[26];
        }

        // 統計每個單詞的字母對出現頻率，如 "ab" 會增加 cnt[0][1]
        foreach (string w in words)
        {
            cnt[w[0] - 'a'][w[1] - 'a']++;
        }

        int ans = 0;    // 用於統計可用於回文的單詞配對數量
        int odd = 0;    // 用位元運算標記是否有可放在中間的單詞
        
        for (int i = 0; i < 26; i++)
        {
            // 處理對角線元素，即形如 "aa", "bb" 等的回文單詞
            // 假如輸入陣列中 "aa" 出現了 5 次，則 cnt[0][0] = 5
            int c = cnt[i][i];

            // 將偶數個回文單詞加入結果
            // 確保結果是偶數，也可以寫成 c & ~1
            ans += c - c % 2;

            // 如果有奇數個回文單詞，標記 odd 為 1
            // 使用位元運算，如果任一個 c%2 為 1，odd 就變為 1
            odd |= c % 2; 
            
            // 處理非對角線元素，即形如 "ab" 和 "ba" 的配對單詞
            for (int j = i + 1; j < 26; j++)
            {
                // 取 cnt[i][j] 和 cnt[j][i] 的最小值作為配對數，如 "ab" 和 "ba" 配對
                // 每對 word 貢獻 2 個單詞（4 個字元）
                // 這邊計算共有幾組資料，最後結果才計算多少字元長度
                // 迴文一左一右所以是 * 2
                ans += Math.Min(cnt[i][j], cnt[j][i]) * 2;
            }
        }
        
        // ans 是單詞數量，每個單詞有 2 個字元
        // odd 如果為 1，表示可以多放一個單詞在中間，增加 2 個字元
        return (ans + odd) * 2; // 上面統計的是字串數量，乘以 2 就是長度
    }


    /// <summary>
    /// 通過連接兩字母單詞建立最長回文字串 (字典及字串比較法)
    /// 解題思路：使用字典統計頻率，並使用字串比較來避免重複計算配對單詞。
    /// 對於回文單詞（如 "aa"），計算能形成偶數對的數量；對於非回文單詞，
    /// 使用字串比較確保每對配對單詞只計算一次。
    /// 這種方法使用 string.Compare 來決定計算順序，確保每對只計算一次。
    /// 時間複雜度：O(n)，空間複雜度：O(n)，其中 n 是單詞數量
    /// 
    /// ref:
    /// https://leetcode.cn/problems/longest-palindrome-by-concatenating-two-letter-words/solutions/1202034/lian-jie-liang-zi-mu-dan-ci-de-dao-de-zu-vs99/?envType=daily-question&envId=2025-05-25
    /// </summary>
    /// <param name="words">包含兩個小寫字母的字串陣列</param>
    /// <returns>最長回文字串的長度</returns>
    public static int LongestPalindrome3(string[] words)
    {
        // 使用字典儲存每個單詞的出現頻率
        Dictionary<string, int> freq = new Dictionary<string, int>();
        foreach (string word in words)
        {
            // 使用 GetValueOrDefault 簡化字典操作，如果 key 不存在則返回預設值 0
            freq[word] = freq.GetValueOrDefault(word, 0) + 1;
        }

        int res = 0;        // 記錄回文字串長度（字元數量）
        bool mid = false;   // 標記是否可以有一個單詞放在中間
        
        foreach (var entry in freq)
        {
            string word = entry.Key;
            int cnt = entry.Value;
            
            // 建立反轉單詞，如 "ab" -> "ba"
            string rev = "" + word[1] + word[0];
            
            // 如果單詞本身就是回文（如 "aa", "bb"）
            if (word == rev)
            {
                // 如果出現次數為奇數，可以放一個在中間
                if (cnt % 2 == 1)
                {
                    mid = true; // 如果有奇數個回文單詞，可以放在中間
                }
                // 偶數次數的回文單詞可以放在兩側，每對貢獻 4 個字元
                //cnt/2 計算對數，*2 確保是偶數，再 *2 表示每對貢獻 4 個字元
                res += 2 * (cnt / 2 * 2);
            }
            // 對於需要配對的單詞，使用字串比較確保每一對只計算一次
            // 只有當 word 字典序大於 rev 時才計算，避免重複
            else if (string.Compare(word, rev) > 0)
            {
                // 找出 word 和 rev 中較少的出現次數，每對貢獻 4 個字元
                res += 4 * Math.Min(cnt, freq.GetValueOrDefault(rev, 0));
            }
            // 如果 word 字典序小於 rev，等到處理到 rev 時再計算
        }

        // 如果有可以放在中間的回文單詞，增加 2 個字元長度
        if (mid)
        {
            res += 2; // 如果有奇數個回文單詞，可以額外加 2
        }
        return res;
    }
}
