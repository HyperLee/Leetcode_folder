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
}