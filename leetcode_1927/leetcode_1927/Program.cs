using System.Globalization;

namespace leetcode_1927;

class Program
{
    /// <summary>
    /// 1927. Sum Game
    /// https://leetcode.com/problems/sum-game/description
    /// 1927. 求和遊戲
    /// https://leetcode.cn/problems/sum-game/description
    ///
    /// English original:
    /// Alice and Bob take turns playing a game, with Alice starting first.
    /// You are given a string num of even length consisting of digits and '?' characters.
    /// On each turn, a player will do the following if there is still at least one '?' in num:
    /// Choose an index i where num[i] == '?'.
    /// Replace num[i] with any digit between '0' and '9'.
    /// The game ends when there are no more '?' characters in num.
    /// For Bob to win, the sum of the digits in the first half of num must be equal to the sum of the digits in the second half. For Alice to win, the sums must not be equal.
    /// For example, if the game ended with num = "243801", then Bob wins because 2+4+3 = 8+0+1. If the game ended with num = "243803", then Alice wins because 2+4+3 != 8+0+3.
    /// Assuming Alice and Bob play optimally, return true if Alice will win and false if Bob will win.
    ///
    /// 繁體中文翻譯：
    /// Alice 和 Bob 輪流進行遊戲，由 Alice 先開始。
    /// 給定一個偶數長度的字串 num，其中由數字與 '?' 字元組成。
    /// 如果 num 中仍至少有一個 '?'，每回合玩家會執行以下操作：
    /// 選擇一個滿足 num[i] == '?' 的索引 i。
    /// 將 num[i] 替換為 '0' 到 '9' 之間的任一數字。
    /// 當 num 中不再有 '?' 字元時，遊戲結束。
    /// 若要讓 Bob 獲勝，num 前半部的數字總和必須等於後半部的數字總和；Alice 獲勝的條件則是兩個總和不相等。
    /// 例如，若遊戲結束時 num = "243801"，則 Bob 獲勝，因為 2+4+3 = 8+0+1。若 num = "243803"，則 Alice 獲勝，因為 2+4+3 != 8+0+3。
    /// 假設 Alice 與 Bob 都採取最佳策略，若 Alice 會獲勝則回傳 true；若 Bob 會獲勝則回傳 false。
    /// </summary>
    /// <remarks>
    /// 直接執行程式時，這個入口會使用固定案例呼叫 <see cref="SumGame(string)"/>，
    /// 並列印每個案例的預期與實際結果。
    /// </remarks>
    /// <param name="args">Command-line arguments.</param>
    static void Main(string[] args)
    {
        (string Name, string Input, bool Expected)[] testCases =
        {
            ("Official_5023", "5023", false),
            ("Official_25Question", "25??", true),
            ("Official_Question3295Questions", "?3295???", false),
            ("OddQuestionCount", "?123", true),
            ("KnownUnequalSums", "1234", true),
            ("EvenQuestions_SameKnownSums", "1?1?", false),
            ("AllQuestions", "????????", false),
            ("QuestionsOnLeft", "??00", true),
            ("QuestionsOnRight", "00??", true)
        };

        Program solution = new Program();
        int passedCount = 0;

        Console.WriteLine("LeetCode 1927 - Sum Game");

        foreach ((string Name, string Input, bool Expected) testCase in testCases)
        {
            bool actual = solution.SumGame(testCase.Input);
            bool passed = actual == testCase.Expected;
            string status = passed ? "PASS" : "FAIL";

            Console.WriteLine(
                $"{testCase.Name}: Input=\"{testCase.Input}\", Expected={testCase.Expected}, Actual={actual}, {status}");

            if (passed)
            {
                passedCount++;
            }
        }

        Console.WriteLine($"Summary: {passedCount}/{testCases.Length} PASS");
        Environment.ExitCode = passedCount == testCases.Length ? 0 : 1;
    }

    /// <summary>
    /// 方法一：猜想 + 数学归纳法验证
    /// 判斷 Sum Game 中 Alice 是否能保證獲勝。
    ///
    /// 遊戲規則：
    /// 字串 <paramref name="num"/> 被分成左右兩半，其中部分字元為 '?'。
    /// Alice 與 Bob 輪流將一個 '?' 替換成 0~9 的任意數字，Alice 先手。
    ///
    /// 當所有 '?' 都被替換後：
    /// - 如果左右兩半的數字總和不同，Alice 獲勝。
    /// - 如果左右兩半的數字總和相同，Bob 獲勝。
    ///
    /// 解題核心：
    ///
    /// 設：
    /// - n0：左半部已知數字的總和。
    /// - n1：右半部已知數字的總和。
    /// - q0：左半部 '?' 的數量。
    /// - q1：右半部 '?' 的數量。
    ///
    /// 1. 如果 q0 + q1 為奇數：
    ///    Alice 一定獲勝。
    ///
    ///    因為 Alice 是先手，所以最後一個 '?' 一定由 Alice 操作。
    ///    最多只有一個數字可以讓左右兩邊總和相等，
    ///    Alice 只要選擇其他數字即可讓兩邊總和不同。
    ///
    /// 2. 如果 q0 + q1 為偶數：
    ///    Bob 獲勝的充要條件為：
    ///
    ///        n0 - n1 = 9 * (q1 - q0) / 2
    ///
    ///    原因是 Alice 與 Bob 每兩次操作可以視為一組：
    ///
    ///    - 如果兩個 '?' 分別位於左右兩側，
    ///      Bob 可以選擇與 Alice 相同的數字，使左右增加量互相抵消。
    ///
    ///    - 如果兩個 '?' 位於同一側，
    ///      Bob 可以選擇 9 - d，使兩次操作加入的總和固定為 9。
    ///
    ///    因此，多出來的同側 '?' 每兩個會產生固定的 9 點差距。
    ///    如果初始數字和的差距剛好可以抵消這些 '?' 所造成的差距，
    ///    Bob 就能保證最後左右兩側總和相同。
    ///
    /// 等價判斷：
    ///
    ///     BobWin =
    ///         (q0 + q1) % 2 == 0
    ///         &amp;&amp;
    ///         n0 - n1 == 9 * (q1 - q0) / 2;
    ///
    /// 因此 Alice 的勝利條件就是上述 Bob 勝利條件的相反結果。
    ///
    /// 時間複雜度：O(n)，只需要遍歷字串一次。
    /// 空間複雜度：O(1)。
    /// </summary>
    /// <param name="num">
    /// 由數字 '0'~'9' 與 '?' 組成的字串，長度為偶數。
    /// </param>
    /// <returns>
    /// 如果 Alice 可以保證最終左右兩半的數字總和不同，回傳 <see langword="true"/>；
    /// 如果 Bob 可以保證左右兩半總和相同，回傳 <see langword="false"/>。
    /// </returns>
    public bool SumGame(string num)
    {
        int n = num.Length;

        // 只記錄兩半的已知總和與問號數量，不需要模擬所有替換順序。
        var left = Get(num.Substring(0, n / 2));
        var right = Get(num.Substring(n / 2, n / 2));

        int n0 = left.Item1;
        int q0 = left.Item2;

        int n1 = right.Item1;
        int q1 = right.Item2;

        // 問號總數為奇數，Alice 一定獲勝。
        if ((q0 + q1) % 2 == 1)
        {
            return true;
        }

        // Bob 獲勝條件：
        // n0 - n1 = 9 * (q1 - q0) / 2
        //
        // 兩邊同乘 2，避免整數除法：
        // 2 * (n0 - n1) = 9 * (q1 - q0)
        return 2 * (n0 - n1) != 9 * (q1 - q0);
    }

    /// <summary>
    /// 分析一段數字字串，計算已知數字的總和與問號數量。
    /// </summary>
    /// <param name="s">由數字與 '?' 組成的字串片段。</param>
    /// <returns>
    /// 回傳 tuple；Item1 是已知數字總和，Item2 是問號數量。
    /// </returns>
    private (int, int) Get(string s)
    {
        int nn = 0;
        int qq = 0;

        foreach (char ch in s)
        {
            if (ch == '?')
            {
                qq++;
            }
            else
            {
                nn += ch - '0';
            }
        }

        return (nn, qq);
    }
}