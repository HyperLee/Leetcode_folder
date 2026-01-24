namespace leetcode_647;

class Program
{
    /// <summary>
    /// 647. Palindromic Substrings
    /// https://leetcode.com/problems/palindromic-substrings/description/
    /// 647. 回文子串
    /// https://leetcode.cn/problems/palindromic-substrings/description/
    /// 
    /// Given a string s, return the number of palindromic substrings in it.
    /// 
    /// A string is a palindrome when it reads the same backward as forward.
    /// 
    /// A substring is a contiguous sequence of characters within the string.
    /// 
    /// 繁體中文翻譯（對照）：
    /// 給定一個字串 s，回傳其中回文子字串的數量。
    /// 一個字串若正讀與反讀相同，稱為回文。
    /// 子字串是字串中連續的字元序列。
    /// 
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        var program = new Program();

        // 測試範例 1: "abc" -> 回文子字串有 "a", "b", "c" 共 3 個
        string test1 = "abc";
        Console.WriteLine($"Input: \"{test1}\" -> Output: {program.CountSubstrings(test1)}"); // 預期輸出: 3

        // 測試範例 2: "aaa" -> 回文子字串有 "a", "a", "a", "aa", "aa", "aaa" 共 6 個
        string test2 = "aaa";
        Console.WriteLine($"Input: \"{test2}\" -> Output: {program.CountSubstrings(test2)}"); // 預期輸出: 6

        // 測試範例 3: "aba" -> 回文子字串有 "a", "b", "a", "aba" 共 4 個
        string test3 = "aba";
        Console.WriteLine($"Input: \"{test3}\" -> Output: {program.CountSubstrings(test3)}"); // 預期輸出: 4

        // 測試範例 4: 空字串
        string test4 = "";
        Console.WriteLine($"Input: \"{test4}\" -> Output: {program.CountSubstrings(test4)}"); // 預期輸出: 0

        // 測試範例 5: 較長字串 "abba"
        string test5 = "abba";
        Console.WriteLine($"Input: \"{test5}\" -> Output: {program.CountSubstrings(test5)}"); // 預期輸出: 6 (a, b, b, a, bb, abba)
    }

    /// <summary>
    /// 計算字串中所有回文子字串的數量。
    /// 
    /// 解題思路：中心擴展法 (Expand Around Center)
    /// - 回文字串具有對稱性，可以從中心向兩側擴展來判斷
    /// - 回文中心有兩種情況：
    ///   1. 奇數長度回文：中心為單一字元（如 "aba" 中心為 'b'）
    ///   2. 偶數長度回文：中心為兩個相鄰字元（如 "abba" 中心為 "bb"）
    /// - 遍歷每個位置作為可能的中心，分別向兩側擴展計數
    /// 
    /// 時間複雜度：O(n²)，其中 n 為字串長度
    /// 空間複雜度：O(1)，只使用常數額外空間
    /// </summary>
    /// <param name="s">輸入字串</param>
    /// <returns>回文子字串的總數量</returns>
    public int CountSubstrings(string s)
    {
        int count = 0;

        // 處理邊界情況：空字串或 null
        if (s is null || s.Length == 0)
        {
            return count;
        }

        // 遍歷每個字元作為回文中心
        for (int i = 0; i < s.Length; i++)
        {
            // CheckForPalindrome(i, i, s): 以 s[i] 為中心，尋找奇數長度的回文
            // CheckForPalindrome(i, i + 1, s): 以 s[i] 和 s[i+1] 為中心，尋找偶數長度的回文
            count += CheckForPalindrome(i, i, s) + CheckForPalindrome(i, i + 1, s);
        }

        return count;
    }

    /// <summary>
    /// 從指定的中心位置向兩側擴展，計算以該位置為中心的回文子字串數量。
    /// 
    /// 核心邏輯：
    /// - 從 start 和 end 位置開始，若 s[start] == s[end]，則找到一個回文
    /// - 繼續向外擴展（start--，end++），直到邊界或字元不相等
    /// - 每次成功匹配都代表找到一個新的回文子字串
    /// 
    /// 範例說明：
    /// - 對於 "aba"，從中心 'b'（start=1, end=1）擴展：
    ///   - 第一次：s[1]='b' == s[1]='b'，count=1（回文 "b"）
    ///   - 第二次：s[0]='a' == s[2]='a'，count=2（回文 "aba"）
    ///   - 第三次：start=-1 超出邊界，停止
    /// </summary>
    /// <param name="start">擴展的左起始索引</param>
    /// <param name="end">擴展的右起始索引</param>
    /// <param name="s">輸入字串</param>
    /// <returns>以指定中心擴展找到的回文子字串數量</returns>
    private int CheckForPalindrome(int start, int end, string s)
    {
        int count = 0;

        // 持續擴展條件：
        // 1. start >= 0：左指標未超出左邊界
        // 2. end < s.Length：右指標未超出右邊界
        // 3. s[start] == s[end]：左右字元相等（符合回文特性）
        while (start >= 0 && end < s.Length && s[start] == s[end])
        {
            // 找到一個回文子字串，計數加一
            count++;

            // 向外擴展：左指標左移，右指標右移
            start--;
            end++;
        }

        return count;
    }
}
