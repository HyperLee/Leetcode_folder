namespace leetcode_2914;

class Program
{
    /// <summary>
    /// 2914. Minimum Number of Changes to Make Binary String Beautiful
    /// https://leetcode.com/problems/minimum-number-of-changes-to-make-binary-string-beautiful/description/
    /// 2914. 使二進位字串變美麗的最少修改次數
    /// https://leetcode.cn/problems/minimum-number-of-changes-to-make-binary-string-beautiful/description/
    ///
    /// <para><b>English</b></para>
    /// <para>You are given a <b>0-indexed</b> binary string <c>s</c> having an even length.</para>
    /// <para>A string is <b>beautiful</b> if it's possible to partition it into one or more substrings such that:</para>
    /// <list type="bullet">
    /// <item><description>Each substring has an <b>even length</b>.</description></item>
    /// <item><description>Each substring contains <b>only</b> <c>1</c>'s or <b>only</b> <c>0</c>'s.</description></item>
    /// </list>
    /// <para>You can change any character in <c>s</c> to <c>0</c> or <c>1</c>.</para>
    /// <para>Return the <b>minimum</b> number of changes required to make the string <c>s</c> beautiful.</para>
    ///
    /// <para><b>繁體中文</b></para>
    /// <para>給定一個從索引 <c>0</c> 開始、長度為偶數的二進位字串 <c>s</c>。</para>
    /// <para>如果能將一個字串分割成一個或多個符合下列條件的子字串，這個字串就是<b>美麗的</b>。</para>
    /// <list type="bullet">
    /// <item><description>每個子字串的長度都是<b>偶數</b>。</description></item>
    /// <item><description>每個子字串都<b>只包含</b> <c>1</c>，或<b>只包含</b> <c>0</c>。</description></item>
    /// </list>
    /// <para>你可以將 <c>s</c> 中的任意字元改為 <c>0</c> 或 <c>1</c>。</para>
    /// <para>請回傳使字串 <c>s</c> 成為美麗字串所需的<b>最少修改次數</b>。</para>
    /// </summary>
    /// <param name="args"></param>
    /// <remarks>
    /// 執行內建的二進位字串範例，核對最少修改次數。無需傳入命令列引數。
    /// 逐筆輸出實際值、預期值和通過狀態。任一範例不符時，程序以非零狀態碼結束。
    /// </remarks>
    static void Main(string[] args)
    {
        (string Input, int Expected)[] examples =
        [
            ("1001", 2),
            ("10", 1),
            ("0000", 0),
            ("1100", 0),
            ("00100111", 2)
        ];

        Program solution = new Program();

        foreach ((string input, int expected) in examples)
        {
            int actual = solution.MinChanges(input);
            bool passed = actual == expected;
            string status = passed ? "PASS" : "FAIL";
            Console.WriteLine($"s = \"{input}\" | actual = {actual} | expected = {expected} | {status}");

            if (!passed)
            {
                Environment.ExitCode = 1;
            }
        }
    }

    /// <summary>
    /// 計算將偶數長度的二進位字串變成美麗字串所需的最少修改次數。
    /// 以貪心法從索引 0 起每兩個字元配成一組，每組字元不同時累計一次修改。
    /// </summary>
    /// <param name="s">長度介於 2 與 100,000 之間且為偶數，只包含 <c>0</c> 和 <c>1</c> 的字串。</param>
    /// <returns>使所有固定配對內的字元相同所需的最少修改次數。</returns>
    public int MinChanges(string s)
    {
        int changes = 0;

        // 每段長度是偶數，因此分段邊界都在偶數索引，固定配對不會跨段。
        for (int i = 0; i < s.Length; i += 2)
        {
            if (s[i] != s[i + 1])
            {
                changes++;
            }
        }

        return changes;
    }
}
