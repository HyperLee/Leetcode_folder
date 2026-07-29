using System.Text;

namespace leetcode_017;

class Program
{
    // 電話鍵盤上 0 ~ 9 按鈕, 但是只有2 ~ 9才有蘊含英文字母
    public static string[] lettersArr = { "", "", "abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz" };
    public static string digits2 = string.Empty;
    public static IList<string> combinations = new List<string>();

    /// <summary>
    /// 17. Letter Combinations of a Phone Number
    /// https://leetcode.com/problems/letter-combinations-of-a-phone-number/description/
    /// 17. 电话号码的字母组合
    /// https://leetcode.cn/problems/letter-combinations-of-a-phone-number/
    ///
    /// 列舉 回朔 題目
    /// 類似 題目 046
    ///
    /// 電話號碼字母組合的特性
    /// 1. 順序性
    ///     輸入是 "23"，其中：
    ///     2 對應 "abc"
    ///     3 對應 "def"
    ///     組合必須按照輸入數字的順序來生成
    /// 2. 示例說明
    ///     輸入 "23" 時：
    ///     第一個位置只能是 2 對應的字母 (a,b,c)
    ///     第二個位置只能是 3 對應的字母 (d,e,f)
    /// 所以合法的組合是：
    /// ad, ae, af
    /// bd, be, bf
    /// cd, ce, cf
    ///
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        (string Name, string Digits, string[] Expected)[] testCases =
        {
            (
                "兩個數字組合",
                "23",
                new[] { "ad", "ae", "af", "bd", "be", "bf", "cd", "ce", "cf" }
            ),
            ("空字串", "", Array.Empty<string>()),
            ("單個數字", "2", new[] { "a", "b", "c" }),
            (
                "三個數字組合",
                "234",
                new[]
                {
                    "adg", "adh", "adi", "aeg", "aeh", "aei", "afg", "afh", "afi",
                    "bdg", "bdh", "bdi", "beg", "beh", "bei", "bfg", "bfh", "bfi",
                    "cdg", "cdh", "cdi", "ceg", "ceh", "cei", "cfg", "cfh", "cfi"
                }
            ),
            (
                "包含 7 和 9",
                "79",
                new[]
                {
                    "pw", "px", "py", "pz", "qw", "qx", "qy", "qz",
                    "rw", "rx", "ry", "rz", "sw", "sx", "sy", "sz"
                }
            )
        };

        int passedCases = 0;
        foreach ((string name, string digits, string[] expected) in testCases)
        {
            if (RunCase(name, digits, expected))
            {
                passedCases++;
            }
        }

        Console.WriteLine($"總結：{passedCases}/{testCases.Length} 筆測試通過");
    }

    /// <summary>
    /// 執行一筆可重現的範例，將輸入交給回溯解法，並依序比較所有預期與實際組合。
    /// 輸入數字必須符合題目限制；若組合內容與順序完全一致則回傳成功。
    /// </summary>
    /// <param name="name">顯示於主控台的測試案例名稱。</param>
    /// <param name="digits">由數字 2 至 9 組成的字串，亦可為空字串。</param>
    /// <param name="expected">依電話按鍵與輸入順序排列的預期組合。</param>
    /// <returns>預期結果與實際結果是否完全相同。</returns>
    private static bool RunCase(string name, string digits, string[] expected)
    {
        IList<string> actual = LetterCombinations(digits);
        bool passed = expected.SequenceEqual(actual);

        Console.WriteLine($"測試案例：{name}");
        Console.WriteLine($"輸入：\"{digits}\"");
        Console.WriteLine($"預期：{FormatCombinations(expected)}");
        Console.WriteLine($"實際：{FormatCombinations(actual)}");
        Console.WriteLine($"結果：{(passed ? "PASS" : "FAIL")}");
        Console.WriteLine();

        return passed;
    }

    /// <summary>
    /// 將字母組合集合格式化為包含雙引號的陣列表示法，方便在主控台核對空集合與各項內容。
    /// </summary>
    /// <param name="values">要顯示的字母組合集合。</param>
    /// <returns>例如 <c>["ad", "ae"]</c> 的可讀字串；空集合則回傳 <c>[]</c>。</returns>
    private static string FormatCombinations(IEnumerable<string> values)
    {
        return $"[{string.Join(", ", values.Select(value => $"\"{value}\""))}]";
    }

    /// <summary>
    /// 產生輸入數字可代表的所有字母組合。方法依序處理每個按鍵，
    /// 透過深度優先回溯列舉每一條完整路徑；空字串會回傳空集合。
    /// </summary>
    /// <param name="digits">長度為 0 至 4，且非空時僅包含數字 2 至 9 的字串。</param>
    /// <returns>依按鍵字母順序排列的所有可能組合。</returns>
    public static IList<string> LetterCombinations(string digits)
    {
        digits2 = digits;
        combinations = new List<string>();

        if (digits2.Length == 0)
        {
            return combinations;
        }

        Backtrack(0, new StringBuilder());
        return combinations;
    }

    /// <summary>
    /// 從指定索引繼續建立目前的候選字串。每層遞迴選擇當前按鍵的一個字母，
    /// 處理下一個按鍵後撤銷選擇；走到輸入尾端時將完整組合加入結果。
    /// </summary>
    /// <param name="index">目前要處理的輸入字元索引，範圍為 0 至輸入長度。</param>
    /// <param name="sb">保存目前遞迴路徑的可變字串，其長度與 <paramref name="index"/> 相同。</param>
    public static void Backtrack(int index, StringBuilder sb)
    {
        // 遞迴深度等於輸入長度，表示目前路徑已形成一個完整組合。
        if (index == digits2.Length)
        {
            combinations.Add(sb.ToString());
            return;
        }

        int digit = digits2[index] - '0';
        string letters = lettersArr[digit];

        foreach (char letter in letters)
        {
            // 選擇目前按鍵的一個字母，再遞迴處理下一個按鍵。
            sb.Append(letter);
            Backtrack(index + 1, sb);

            // 撤銷剛才的選擇，讓下一個字母沿用相同的前綴繼續搜尋。
            sb.Length--;
        }
    }
}
