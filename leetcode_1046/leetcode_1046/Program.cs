namespace leetcode_1046;

class Program
{
    /// <summary>
    /// 1046. Last Stone Weight
    /// https://leetcode.com/problems/last-stone-weight/description/
    /// <para>
    /// You are given an array of integers stones where stones[i] is the weight of the ith stone.
    /// We are playing a game with the stones. On each turn, we choose the heaviest two stones and smash them together.
    /// Suppose the heaviest two stones have weights x and y with x &lt;= y. The result of this smash is:
    /// - If x == y, both stones are destroyed.
    /// - If x != y, the stone of weight x is destroyed, and the stone of weight y has new weight y - x.
    /// At the end of the game, there is at most one stone left.
    /// Return the weight of the last remaining stone. If there are no stones left, return 0.
    /// </para>
    /// <para>
    /// 1046. 最後一塊石頭的重量
    /// https://leetcode.cn/problems/last-stone-weight/description/
    /// </para>
    /// <para>
    /// 給你一個整數陣列 stones，其中 stones[i] 是第 i 顆石頭的重量。
    /// 我們正在用石頭玩一個遊戲。每個回合，我們選擇最重的兩顆石頭並將它們互相砸碎。
    /// 假設最重的兩顆石頭重量分別為 x 和 y，且 x &lt;= y。砸碎的結果如下：
    /// - 如果 x == y，兩顆石頭都被摧毀。
    /// - 如果 x != y，重量為 x 的石頭被摧毀，重量為 y 的石頭新重量為 y - x。
    /// 遊戲結束時，最多只剩一顆石頭。
    /// 回傳最後剩餘石頭的重量。如果沒有石頭剩下，則回傳 0。
    /// </para>
    /// </summary>
    /// <param name="args">命令列引數</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
