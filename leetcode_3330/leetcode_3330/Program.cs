namespace leetcode_3330;

class Program
{
    /// <summary>
    /// 3330. Find the Original Typed String I
    /// https://leetcode.com/problems/find-the-original-typed-string-i/description/?envType=daily-question&envId=2025-07-01
    /// 3330. 找到初始輸入字串 I
    /// https://leetcode.cn/problems/find-the-original-typed-string-i/description/?envType=daily-question&envId=2025-07-01
    /// 
    /// 題目描述：
    /// Alice 嘗試在電腦上輸入一個特定字串，但她有時會因為手殘，某個按鍵按太久，導致某個字元被輸入多次。
    /// 雖然 Alice 很專心，但她知道自己最多只會出現一次這種情況。
    /// 給定一個字串 word，代表 Alice 螢幕上最終顯示的內容。
    /// 請回傳 Alice 可能原本想輸入的原始字串總數。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var program = new Program();
        string[] testCases = { "aabb", "abc", "aabbaa", "a", "aa", "abcc" };
        foreach (var word in testCases)
        {
            int result = program.PossibleStringCount(word);
            Console.WriteLine($"word: {word}, 可能原始字串總數: {result}");
        }
    }

    /// <summary>
    /// 計算 Alice 可能原本想輸入的原始字串總數
    /// </summary>
    /// <param name="word"></param>
    /// <returns></returns>
    public int PossibleStringCount(string word)
    {
        int n = word.Length;
        int res = 1;
        for (int i = 1; i < n; i++)
        {
            if (word[i] == word[i - 1])
            {
                res++;
            }
        }

        return res;
    }
}
