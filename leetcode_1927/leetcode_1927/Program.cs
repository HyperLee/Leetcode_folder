using System.Globalization;

namespace leetcode_1927;

class Program
{
    /// <summary>
    /// 1927. Sum Game
    /// https://leetcode.com/problems/sum-game/description
    /// 1927. 求和遊戲
    /// https://leetcode.cn/problems/sum-game/description/
    ///
    /// <para>English original:</para>
    /// <para>Alice and Bob take turns playing a game, with Alice starting first.</para>
    /// <para>You are given a string <c>num</c> of even length consisting of digits and '?' characters.</para>
    /// <para>On each turn, a player will do the following if there is still at least one '?' in <c>num</c>:</para>
    /// <para>Choose an index <c>i</c> where <c>num[i] == '?'</c>.</para>
    /// <para>Replace <c>num[i]</c> with any digit between '0' and '9'.</para>
    /// <para>The game ends when there are no more '?' characters in <c>num</c>.</para>
    /// <para>For Bob to win, the sum of the digits in the first half of <c>num</c> must be equal to the sum of the digits in the second half. For Alice to win, the sums must not be equal.</para>
    /// <para>For example, if the game ended with <c>num = "243801"</c>, then Bob wins because 2+4+3 = 8+0+1. If the game ended with <c>num = "243803"</c>, then Alice wins because 2+4+3 != 8+0+3.</para>
    /// <para>Assuming Alice and Bob play optimally, return <c>true</c> if Alice will win and <c>false</c> if Bob will win.</para>
    ///
    /// <para>繁體中文翻譯：</para>
    /// <para>Alice 和 Bob 輪流進行遊戲，由 Alice 先開始。</para>
    /// <para>給定一個偶數長度的字串 <c>num</c>，其中由數字與 '?' 字元組成。</para>
    /// <para>如果 <c>num</c> 中仍至少有一個 '?'，每回合玩家會執行以下操作：</para>
    /// <para>選擇一個滿足 <c>num[i] == '?'</c> 的索引 <c>i</c>。</para>
    /// <para>將 <c>num[i]</c> 替換為 '0' 到 '9' 之間的任一數字。</para>
    /// <para>當 <c>num</c> 中不再有 '?' 字元時，遊戲結束。</para>
    /// <para>若要讓 Bob 獲勝，<c>num</c> 前半部的數字總和必須等於後半部的數字總和；Alice 獲勝的條件則是兩個總和不相等。</para>
    /// <para>例如，若遊戲結束時 <c>num = "243801"</c>，則 Bob 獲勝，因為 2+4+3 = 8+0+1。若 <c>num = "243803"</c>，則 Alice 獲勝，因為 2+4+3 != 8+0+3。</para>
    /// <para>假設 Alice 與 Bob 都採取最佳策略，若 Alice 會獲勝則回傳 <c>true</c>；若 Bob 會獲勝則回傳 <c>false</c>。</para>
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
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
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
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