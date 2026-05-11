namespace leetcode_344;

class Program
{
    /// <summary>
    /// 344. Reverse String
    /// https://leetcode.com/problems/reverse-string/description/?envType=daily-question&envId=2024-06-02
    /// 344. 反轉字串
    /// https://leetcode.cn/problems/reverse-string/description/
    /// 
    /// English:
    /// Write a function that reverses a string. The input string is given as an array of characters s.
    /// You must do this by modifying the input array in-place with O(1) extra memory.
    /// 
    /// 繁體中文：
    /// 撰寫一個函式來反轉字串。輸入字串會以字元陣列 s 的形式給定。
    /// 你必須原地修改輸入陣列，並且只使用 O(1) 的額外記憶體。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new Program();

        RunSample(solution, ['h', 'e', 'l', 'l', 'o']);
        RunSample(solution, ['H', 'a', 'n', 'n', 'a', 'h']);
        RunSample(solution, []);
    }

    /// <summary>
    /// 執行單一反轉字串範例。
    /// 用途是保留原始輸入顯示，再呼叫解法函式輸出反轉後結果。
    /// 輸入條件為非 null 的解法物件與字元陣列；輸出結果會寫入主控台。
    /// </summary>
    /// <param name="solution">包含反轉字串解法的物件。</param>
    /// <param name="input">要展示的測試字元陣列。</param>
    private static void RunSample(Program solution, char[] input)
    {
        char[] twoPointerSample = (char[])input.Clone();
        char[] singlePointerSample = (char[])input.Clone();

        solution.ReverseString(twoPointerSample);
        solution.ReverseStringSinglePointer(singlePointerSample);

        Console.WriteLine($"Input:      {FormatChars(input)}");
        Console.WriteLine($"Solution 1: {FormatChars(twoPointerSample)}");
        Console.WriteLine($"Solution 2: {FormatChars(singlePointerSample)}");
        Console.WriteLine();
    }

    /// <summary>
    /// 將字元陣列轉成範例輸出格式。
    /// 解題展示時使用此函式把每個字元包成單引號並以逗號分隔。
    /// 輸入條件為非 null 的字元陣列；輸出結果是可讀的陣列字串。
    /// </summary>
    /// <param name="chars">要格式化的字元陣列。</param>
    /// <returns>格式化後的字元陣列字串。</returns>
    private static string FormatChars(char[] chars)
    {
        return $"[{string.Join(", ", chars.Select(ch => $"'{ch}'"))}]";
    }

    /// <summary>
    /// 使用雙指針原地反轉字元陣列。
    /// 解題概念是讓左指針從開頭、右指針從結尾往中間靠攏，沿途交換兩端字元。
    /// 輸入條件為非 null 的字元陣列；方法不回傳值，完成後輸入陣列本身即為反轉結果。
    /// </summary>
    /// <param name="s">要原地反轉的字元陣列。</param>
    public void ReverseString(char[] s)
    {
        int right = s.Length - 1;
        for (int left = 0; left < right; left++, right--)
        {
            // 每次交換目前左右邊界的字元，逐步縮小尚未處理的區間。
            char ch = s[left];
            s[left] = s[right];
            s[right] = ch;
        }
    }

    /// <summary>
    /// 使用單指針原地反轉字元陣列。
    /// 解題概念是用 i 表示左側索引，再透過 n - 1 - i 推導出右側對稱索引。
    /// 只需要處理前半段字元，因為每次交換都會同時完成一組左右對稱位置。
    /// </summary>
    /// <param name="s">要原地反轉的字元陣列。</param>
    public void ReverseStringSinglePointer(char[] s)
    {
        int n = s.Length;
        for (int i = 0; i < n / 2; i++)
        {
            char ch = s[i];
            s[i] = s[n - 1 - i];
            s[n - 1 - i] = ch;
        }
    }
}
