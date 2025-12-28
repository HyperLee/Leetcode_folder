namespace leetcode_1332;

class Program
{
    /// <summary>
    /// 1332. Remove Palindromic Subsequences
    /// https://leetcode.com/problems/remove-palindromic-subsequences/description/
    /// 
    /// 繁體中文題目描述：
    /// 給定一個僅由字母 'a' 和 'b' 組成的字串 s。一次操作可以從 s 中刪除一個回文子序列。
    /// 回傳使給定字串變為空字串所需的最小操作次數。
    /// 子序列是由刪除原字串中部分字元（不需要連續）且保持相對順序所得到的字串。
    /// 回文字串是正讀與反讀相同的字串。
    /// 
    /// 簡體中文翻譯：
    /// 1332. 删除回文子序列
    /// https://leetcode.cn/problems/remove-palindromic-subsequences/description/
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試案例 1: 回文字串 "ababa"
        string test1 = "ababa";
        Console.WriteLine($"輸入: \"{test1}\"");
        Console.WriteLine($"輸出: {solution.RemovePalindromeSub(test1)}");
        Console.WriteLine($"預期: 1 (字串本身是回文，一次刪除即可)");
        Console.WriteLine();

        // 測試案例 2: 非回文字串 "abb"
        string test2 = "abb";
        Console.WriteLine($"輸入: \"{test2}\"");
        Console.WriteLine($"輸出: {solution.RemovePalindromeSub(test2)}");
        Console.WriteLine($"預期: 2 (先刪除所有 'a'，再刪除所有 'b')");
        Console.WriteLine();

        // 測試案例 3: 非回文字串 "baabb"
        string test3 = "baabb";
        Console.WriteLine($"輸入: \"{test3}\"");
        Console.WriteLine($"輸出: {solution.RemovePalindromeSub(test3)}");
        Console.WriteLine($"預期: 2 (非回文字串)");
        Console.WriteLine();

        // 測試案例 4: 單一字元 "a"
        string test4 = "a";
        Console.WriteLine($"輸入: \"{test4}\"");
        Console.WriteLine($"輸出: {solution.RemovePalindromeSub(test4)}");
        Console.WriteLine($"預期: 1 (單一字元必為回文)");
        Console.WriteLine();

        // 測試案例 5: 全部相同字元 "aaaa"
        string test5 = "aaaa";
        Console.WriteLine($"輸入: \"{test5}\"");
        Console.WriteLine($"輸出: {solution.RemovePalindromeSub(test5)}");
        Console.WriteLine($"預期: 1 (全部相同字元必為回文)");
    }

    /// <summary>
    /// 方法一：直接判斷
    /// 
    /// 解題思路：
    /// 由於字串本身只含有字母 'a' 和 'b' 兩種字元，題目要求每次刪除回文子序列（不一定連續）使得字串最終為空。
    /// 
    /// 關鍵觀察：
    /// 因為只包含兩種不同的字元，相同的字元組成的子序列一定是回文子序列。
    /// 例如：所有的 'a' 組成的子序列 "aaa..." 必定是回文。
    /// 因此最多只需要刪除 2 次即可刪除所有的字元。
    /// 
    /// 刪除判斷：
    /// 1. 如果該字串本身為回文串，此時只需刪除 1 次即可，刪除次數為 1。
    /// 2. 如果該字串本身不是回文串，此時只需刪除 2 次即可：
    ///    - 先刪除所有的 'a'（所有 'a' 組成回文子序列）
    ///    - 再刪除所有的 'b'（所有 'b' 組成回文子序列）
    ///    刪除次數為 2。
    /// 
    /// 時間複雜度：O(n)，其中 n 為字串長度，只需遍歷一半字串判斷是否為回文。
    /// 空間複雜度：O(1)，只使用常數額外空間。
    /// </summary>
    /// <param name="s">僅由 'a' 和 'b' 組成的字串</param>
    /// <returns>使字串變為空所需的最小刪除回文子序列次數</returns>
    public int RemovePalindromeSub(string s)
    {
        int n = s.Length;

        // 使用雙指標從兩端向中間檢查是否為回文
        // 只需檢查前半部分，與後半部分對應位置比較
        for (int i = 0; i < n / 2; i++)
        {
            // 如果對稱位置的字元不相等，則不是回文
            // 非回文字串需要 2 次刪除：先刪所有 'a'，再刪所有 'b'
            if (s[i] != s[n - 1 - i])
            {
                return 2;
            }
        }

        // 字串本身是回文，只需 1 次刪除即可清空
        return 1;
    }
}
