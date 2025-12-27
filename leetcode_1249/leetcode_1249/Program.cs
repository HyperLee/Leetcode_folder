using System.Text;

namespace leetcode_1249;

class Program
{
    /// <summary>
    /// 1249. Minimum Remove to Make Valid Parentheses
    /// https://leetcode.com/problems/minimum-remove-to-make-valid-parentheses/description/
    /// 1249. 移除无效的括号
    /// https://leetcode.cn/problems/minimum-remove-to-make-valid-parentheses/description/
    /// 繁體中文翻譯：
    /// 給定一個由 '('、')' 以及小寫英文字母組成的字串 s。
    /// 任務是最少移除若干括號（'(' 或 ')'，可位於任意位置），使得結果的括號字串為有效，並回傳任一個有效的字串。
    /// 形式上，有效的括號字串定義如下：
    /// - 空字串，或只包含小寫字母；或
    /// - 可以寫成 AB（A 與 B 為有效字串），或
    /// - 可以寫成 (A)，其中 A 為有效字串。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program program = new Program();

        // 測試案例 1: "lee(t(c)o)de)" -> "lee(t(c)o)de"
        string test1 = "lee(t(c)o)de)";
        Console.WriteLine($"輸入: \"{test1}\"");
        Console.WriteLine($"輸出: \"{program.MinRemoveToMakeValid(test1)}\"");
        Console.WriteLine();

        // 測試案例 2: "a)b(c)d" -> "ab(c)d"
        string test2 = "a)b(c)d";
        Console.WriteLine($"輸入: \"{test2}\"");
        Console.WriteLine($"輸出: \"{program.MinRemoveToMakeValid(test2)}\"");
        Console.WriteLine();

        // 測試案例 3: "))((" -> ""
        string test3 = "))((";
        Console.WriteLine($"輸入: \"{test3}\"");
        Console.WriteLine($"輸出: \"{program.MinRemoveToMakeValid(test3)}\"");
        Console.WriteLine();

        // 測試案例 4: "(a(b(c)d)" -> "a(b(c)d)" 或 "(a(bc)d)" 或其他有效結果
        string test4 = "(a(b(c)d)";
        Console.WriteLine($"輸入: \"{test4}\"");
        Console.WriteLine($"輸出: \"{program.MinRemoveToMakeValid(test4)}\"");
        Console.WriteLine();

        // 測試案例 5: 空字串
        string test5 = "";
        Console.WriteLine($"輸入: \"{test5}\"");
        Console.WriteLine($"輸出: \"{program.MinRemoveToMakeValid(test5)}\"");
    }

    /// <summary>
    /// 移除最少括號使字串有效（兩次掃描法）
    /// <para>
    /// <b>解題思路：</b><br/>
    /// 這是一個不使用 Stack 的兩次掃描解法，時間與空間複雜度皆為 O(n)。<br/>
    /// 核心概念是分兩階段處理：先移除無效的 ')'，再移除多餘的 '('。
    /// </para>
    /// <para>
    /// <b>演算法步驟：</b><br/>
    /// <b>第一階段（從左到右掃描）：</b>移除所有無效的 ')' 括號<br/>
    /// - 使用 balanceCount 追蹤未匹配的 '(' 數量<br/>
    /// - 遇到 '(' 時，openCount++ 和 balanceCount++<br/>
    /// - 遇到 ')' 時，若 balanceCount > 0 則配對成功（balanceCount--），否則跳過該 ')'<br/>
    /// <br/>
    /// <b>第二階段（從左到右掃描）：</b>移除多餘的 '(' 括號<br/>
    /// - 計算需要保留的 '(' 數量 = openCount - balanceCount<br/>
    /// - 從左到右掃描，只保留需要數量的 '('，超出的部分跳過<br/>
    /// </para>
    /// <para>
    /// <b>為何從左移除多餘 '(' 可行？</b><br/>
    /// 經過第一階段後，所有 ')' 都是有效的（都有對應的 '('）。<br/>
    /// 多餘的 '(' 一定在字串較右側（因為右側沒有足夠的 ')' 來匹配）。<br/>
    /// 因此從左保留需要數量的 '('，剩餘的自然就是多餘的。
    /// </para>
    /// <para>
    /// <b>時間複雜度：</b>O(n)，兩次線性掃描<br/>
    /// <b>空間複雜度：</b>O(n)，StringBuilder 儲存結果
    /// </para>
    /// </summary>
    /// <param name="s">輸入字串，包含 '('、')' 及小寫英文字母</param>
    /// <returns>移除最少括號後的有效字串</returns>
    /// <example>
    /// <code>
    ///  範例 1
    /// MinRemoveToMakeValid("lee(t(c)o)de)") // 回傳 "lee(t(c)o)de"
    /// 
    ///  範例 2  
    /// MinRemoveToMakeValid("a)b(c)d") // 回傳 "ab(c)d"
    /// 
    ///  範例 3
    /// MinRemoveToMakeValid("))((") // 回傳 ""
    /// </code>
    /// </example>
    /// <remarks>
    /// 參考來源：LeetCode 官方解法 - 方法三：改進的使用 StringBuilder 的兩步法<br/>
    /// https://leetcode-cn.com/problems/minimum-remove-to-make-valid-parentheses/solution/yi-chu-wu-xiao-gua-hao-by-leetcode/
    /// </remarks>
    public string MinRemoveToMakeValid(string s)
    {
        // ========== 第一階段：從左到右掃描，移除所有無效的 ')' ==========
        StringBuilder sb = new StringBuilder();
        int openCount = 0;      // 記錄遇到的 '(' 總數
        int balanceCount = 0;   // 記錄目前未匹配的 '(' 數量（用於判斷 ')' 是否有效）
        
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '(')
            {
                // 遇到左括號：總數 +1，未匹配數 +1
                openCount++;
                balanceCount++;
            }
            else if (s[i] == ')')
            {
                // 遇到右括號：檢查是否有對應的左括號
                if (balanceCount == 0)
                {
                    // 沒有未匹配的 '('，此 ')' 為無效，跳過不加入結果
                    continue;
                }
                // 有對應的 '('，配對成功，未匹配數 -1
                balanceCount--;
            }
            // 將有效字元加入結果（字母直接加入，有效括號加入）
            sb.Append(s[i]);
        }

        // ========== 第二階段：移除多餘的 '(' ==========
        string str = sb.ToString();
        sb = new StringBuilder();
        
        // 計算需要保留的 '(' 數量
        // balanceCount = 第一階段結束後仍未匹配的 '(' 數量（即多餘的 '('）
        // 需要保留的 '(' = 總 '(' 數 - 多餘的 '(' 數
        int balancedOpenCount = openCount - balanceCount;
        
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == '(')
            {
                if (balancedOpenCount <= 0)
                {
                    // 已經保留足夠數量的 '('，剩餘的都是多餘的，跳過
                    continue;
                }
                // 保留這個 '('，剩餘需保留數量 -1
                balancedOpenCount--;
            }
            sb.Append(str[i]);
        }
        
        return sb.ToString();
    }
}
