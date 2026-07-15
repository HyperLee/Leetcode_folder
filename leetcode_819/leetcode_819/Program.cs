using System.Text;

namespace leetcode_819;

internal static class Program
{
    private static int s_checks;
    private static int s_passed;

    /// <summary>
    /// 819. Most Common Word
    /// https://leetcode.com/problems/most-common-word/
    /// 819. 最常見的單字
    /// https://leetcode.cn/problems/most-common-word/
    /// Given a paragraph and a list of banned words, return the most frequent word that is not banned; matching is case-insensitive and the answer is unique.
    /// 給定一段文字與禁用單字清單，回傳出現次數最多且未被禁用的單字；比對不分大小寫，且答案唯一。
    /// </summary>
    private static void Main()
    {
        string upperBoundParagraph = string.Concat(Enumerable.Repeat("a ", 334))
            + string.Concat(Enumerable.Repeat("b ", 165))
            + "a.";

        if (upperBoundParagraph.Length != 1000)
        {
            throw new InvalidOperationException("上限案例的 paragraph 必須恰好包含 1000 個字元。");
        }

        (string Name, string Paragraph, string[] Banned, string Expected, string InputDescription)[] cases =
        [
            (
                "Official example",
                "Bob hit a ball, the hit BALL flew far after it was hit.",
                ["hit"],
                "ball",
                "paragraph = \"Bob hit a ball, the hit BALL flew far after it was hit.\", banned = [\"hit\"]"),
            ("Official minimum example", "a.", [], "a", "paragraph = \"a.\", banned = []"),
            (
                "Case normalization",
                "Tea tea coffee COFFEE coffee.",
                ["COFFEE"],
                "tea",
                "paragraph = \"Tea tea coffee COFFEE coffee.\", banned = [\"COFFEE\"]"),
            (
                "Punctuation boundaries",
                "alpha!beta?alpha'gamma,alpha;beta.gamma.",
                ["gamma"],
                "alpha",
                "paragraph = \"alpha!beta?alpha'gamma,alpha;beta.gamma.\", banned = [\"gamma\"]"),
            (
                "Exclude raw highest frequency",
                "red red red blue blue green.",
                ["red"],
                "blue",
                "paragraph = \"red red red blue blue green.\", banned = [\"red\"]"),
            (
                "Final word flush",
                "edge middle middle edge edge",
                [],
                "edge",
                "paragraph = \"edge middle middle edge edge\", banned = []"),
            (
                "Exactly 1000 characters",
                upperBoundParagraph,
                [],
                "a",
                "paragraph = 1000 characters (334 x \"a \", 165 x \"b \", then \"a.\"), banned = []")
        ];

        Console.WriteLine("LeetCode 819 acceptance harness");
        Console.WriteLine();

        for (int i = 0; i < cases.Length; i++)
        {
            (string name, string paragraph, string[] banned, string expected, string inputDescription) = cases[i];
            string actual = MostCommonWord(paragraph, banned);
            bool passed = string.Equals(expected, actual, StringComparison.Ordinal);

            RecordCheck(passed);
            Console.WriteLine($"Case {i + 1}: {name}");
            Console.WriteLine($"Input: {inputDescription}");
            Console.WriteLine($"{(passed ? "PASS" : "FAIL")} | Expected: {expected} | Actual: {actual}");
            Console.WriteLine();
        }

        Console.WriteLine($"Summary: {s_passed}/{s_checks} checks passed.");

        if (s_passed != s_checks)
        {
            Environment.ExitCode = 1;
        }
    }

    /// <summary>
    /// 以單次字元掃描統計未禁用單字的出現次數；paragraph、banned 與唯一答案皆遵循 LeetCode 有效輸入契約，並回傳正規化為小寫的最高頻合法單字。
    /// </summary>
    public static string MostCommonWord(string paragraph, string[] banned)
    {
        // 禁用清單先統一為 invariant 小寫，讓後續每次查詢都能以 O(1) 平均時間完成。
        HashSet<string> bannedWords = new(
            banned.Select(static word => word.ToLowerInvariant()),
            StringComparer.Ordinal);
        Dictionary<string, int> frequencies = new(StringComparer.Ordinal);
        StringBuilder currentWord = new();
        string answer = string.Empty;
        int maximumCount = 0;

        // i == paragraph.Length 是虛擬邊界，確保沒有尾端標點的最後一個單字仍會被結算。
        for (int i = 0; i <= paragraph.Length; i++)
        {
            if (i < paragraph.Length && char.IsLetter(paragraph[i]))
            {
                currentWord.Append(char.ToLowerInvariant(paragraph[i]));
                continue;
            }

            if (currentWord.Length == 0)
            {
                continue;
            }

            string word = currentWord.ToString();
            currentWord.Clear();

            if (bannedWords.Contains(word))
            {
                continue;
            }

            frequencies.TryGetValue(word, out int count);
            count++;
            frequencies[word] = count;

            // 只有嚴格超越目前最大值時才更新答案，避免額外排序頻率字典。
            if (count > maximumCount)
            {
                maximumCount = count;
                answer = word;
            }
        }

        return answer;
    }

    /// <summary>
    /// 接收單一案例的驗證結果；遞增總檢查數，並在案例通過時同步遞增通過數。
    /// </summary>
    private static void RecordCheck(bool passed)
    {
        s_checks++;

        if (passed)
        {
            s_passed++;
        }
    }
}
