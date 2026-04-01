using System.Text;

namespace leetcode_316;

class Program
{
    /// <summary>
    /// 316. Remove Duplicate Letters
    /// https://leetcode.com/problems/remove-duplicate-letters/description/
    /// 316. 去除重复字母
    /// https://leetcode.cn/problems/remove-duplicate-letters/description/
    /// 
    /// Given a string s, remove duplicate letters so that every letter appears once and only once. 
    /// You must make sure your result is the smallest in lexicographical order among all possible results.
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solver = new Program();
        (string Input, string Expected)[] testCases =
        [
            ("bcabc", "abc"),
            ("cbacdcbc", "acdb"),
            ("cdadabcc", "adbc"),
            ("abacb", "abc"),
            (string.Empty, string.Empty)
        ];

        Console.WriteLine("LeetCode 316 - Remove Duplicate Letters");
        Console.WriteLine(new string('=', 40));

        // 用固定測資同時驗證兩個版本是否都能得到預期答案。
        foreach ((string input, string expected) in testCases)
        {
            string firstResult = solver.RemoveDuplicateLetters(input);
            string secondResult = solver.RemoveDuplicateLetters2(input);
            bool isPassed = firstResult == expected && secondResult == expected;

            Console.WriteLine($"Input    : \"{input}\"");
            Console.WriteLine($"Expected : \"{expected}\"");
            Console.WriteLine($"Method 1 : \"{firstResult}\"");
            Console.WriteLine($"Method 2 : \"{secondResult}\"");
            Console.WriteLine($"Status   : {(isPassed ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 使用貪心策略搭配單調堆疊，移除重複字母後回傳字典序最小的合法結果。
    /// 這題的關鍵在於不能打亂原字串中字元的相對順序，因此當前字元若比結果尾端更小，
    /// 只有在尾端字元未來還會再出現時，才可以安全地把尾端字元移除，替較小字元讓位。
    /// </summary>
    /// <param name="s">只包含小寫英文字母的輸入字串。</param>
    /// <returns>每個字母只出現一次且字典序最小的結果字串。</returns>
    /// <remarks>
    /// 解題流程如下：
    /// 1. 先統計每個字元在後續還剩多少次可用。
    /// 2. 逐字掃描字串，若字元已經放進答案則直接略過。
    /// 3. 若答案尾端字元比目前字元大，且尾端字元後面還會再出現，就先把尾端移除。
    /// 4. 將目前字元加入答案，最後得到符合相對順序限制的最小字典序答案。
    /// 時間複雜度為 O(n)，空間複雜度為 O(1)，因為只使用固定大小的 26 字元計數陣列。
    /// </remarks>
    /// <example>
    /// <code>
    /// RemoveDuplicateLetters("cbacdcbc") // 回傳 "acdb"
    /// </code>
    /// </example>
    public string RemoveDuplicateLetters(string s)
    {
        ArgumentNullException.ThrowIfNull(s);

        if (s.Length == 0)
        {
            return string.Empty;
        }

        // remainingCount[x] 表示字元 x 在後續尚未處理區段還剩幾次。
        int[] remainingCount = new int[26];
        for (int i = 0; i < s.Length; i++)
        {
            remainingCount[s[i] - 'a']++;
        }

        // resultStack 模擬單調堆疊；inResult 用來避免重複加入同一字元。
        StringBuilder resultStack = new StringBuilder(s.Length);
        HashSet<char> inResult = new HashSet<char>();

        for (int i = 0; i < s.Length; i++)
        {
            char current = s[i];
            remainingCount[current - 'a']--;

            if (inResult.Contains(current))
            {
                continue;
            }

            // 若尾端字元較大且之後還會出現，先移除它以換取更小的字典序。
            while (resultStack.Length > 0
                && resultStack[^1] > current
                && remainingCount[resultStack[^1] - 'a'] > 0)
            {
                char lastChar = resultStack[^1];
                inResult.Remove(lastChar);
                resultStack.Remove(resultStack.Length - 1, 1);
            }

            resultStack.Append(current);
            inResult.Add(current);
        }

        return resultStack.ToString();
    }

    /// <summary>
    /// 使用明確的 Stack 與 HashSet 實作相同的單調堆疊貪心解法。
    /// 這個版本將「答案內容」與「字元是否已入堆」分開維護，方便觀察推入與彈出的決策過程。
    /// </summary>
    /// <param name="s">只包含小寫英文字母的輸入字串。</param>
    /// <returns>每個字母只出現一次且字典序最小的結果字串。</returns>
    /// <remarks>
    /// 核心概念與第一個方法一致，只是資料結構改成顯式堆疊：
    /// 1. 先記錄每個字元剩餘出現次數。
    /// 2. 掃描目前字元時，若尚未入堆，就檢查堆頂是否可以安全彈出。
    /// 3. 只要堆頂字元比目前字元大，且後面還會再出現，就先彈出。
    /// 4. 最後把堆疊內容反轉，即可得到答案。
    /// 時間複雜度為 O(n)，空間複雜度為 O(1)；這裡使用固定大小的 256 字元計數陣列。
    /// </remarks>
    /// <example>
    /// <code>
    /// RemoveDuplicateLetters2("bcabc") // 回傳 "abc"
    /// </code>
    /// </example>
    public string RemoveDuplicateLetters2(string s)
    {
        ArgumentNullException.ThrowIfNull(s);

        if (s.Length == 0)
        {
            return string.Empty;
        }

        int[] remainingCount = new int[256];

        foreach (char current in s)
        {
            remainingCount[current]++;
        }

        HashSet<char> inStack = new HashSet<char>();
        Stack<char> stack = new Stack<char>();

        foreach (char current in s)
        {
            remainingCount[current]--;

            if (inStack.Contains(current))
            {
                continue;
            }

            // 堆頂字元若更大且未來還會再出現，就可以安全移除。
            while (stack.Count > 0)
            {
                char previousChar = stack.Peek();

                if (remainingCount[previousChar] > 0 && previousChar > current)
                {
                    char popped = stack.Pop();
                    inStack.Remove(popped);
                }
                else
                {
                    break;
                }
            }

            inStack.Add(current);
            stack.Push(current);
        }

        List<char> result = new List<char>(stack.Count);

        // 堆疊由左到右是反向順序，因此取出後要再反轉一次。
        while (stack.Count > 0)
        {
            result.Add(stack.Pop());
        }

        result.Reverse();

        return new string(result.ToArray());
    }
}
