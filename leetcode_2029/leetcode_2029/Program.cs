using System.ComponentModel;

namespace leetcode_2029;

class Program
{
    /// <summary>
    /// 2029. Stone Game IX
    /// https://leetcode.com/problems/stone-game-ix/description/
    /// 2029. 石子游戏 IX
    /// https://leetcode.cn/problems/stone-game-ix/description/
    ///
    /// English:
    /// Alice and Bob continue their games with stones. There is a row of n stones, and each stone has an associated value.
    /// You are given an integer array stones, where stones[i] is the value of the ith stone.
    ///
    /// Alice and Bob take turns, with Alice starting first. On each turn, the player may remove any stone from stones.
    /// The player who removes a stone loses if the sum of the values of all removed stones is divisible by 3.
    /// Bob will win automatically if there are no remaining stones (even if it is Alice's turn).
    ///
    /// Assuming both players play optimally, return true if Alice wins and false if Bob wins.
    ///
    /// 繁體中文：
    /// Alice 和 Bob 繼續進行石子遊戲。有一排 n 顆石子，每顆石子都有一個對應的數值。
    /// 給你一個整數陣列 stones，其中 stones[i] 是第 i 顆石子的數值。
    ///
    /// Alice 和 Bob 輪流行動，由 Alice 先手。每一回合，玩家可以從 stones 中移除任意一顆石子。
    /// 如果所有已移除石子的數值總和可以被 3 整除，移除該石子的玩家就輸了。
    /// 如果沒有剩餘石子，Bob 會自動獲勝（即使此時輪到 Alice）。
    ///
    /// 假設兩位玩家都採取最佳策略，如果 Alice 獲勝則回傳 true；如果 Bob 獲勝則回傳 false。
    /// </summary>
    /// <param name="args">命令列參數；本程式目前未使用。</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stones"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="stones"></param>
    /// <returns></returns>
    public bool StoneGameIX(int[] stones)
    {
        int cnt0 = 0;
        int cnt1 = 0;
        int cnt2 = 0;
        foreach(int val in stones)
        {
            int type = val % 3;
            if(type == 0)
            {
                cnt0++;
            }
            else if(type == 1)
            {
                cnt1++;
            }
            else
            {
                cnt2++;
            }
        }

        if(cnt0 % 2 == 0)
        {
            return cnt1 >= 1 && cnt2 >= 1;
        }

        return cnt1 - cnt2 > 2 || cnt2 - cnt1 > 2;
    }

    /// <summary>
    /// 计算最大回合数
    /// </summary>
    /// <param name="stones"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="stones"></param>
    /// <returns></returns>
    public bool StoneGameIX2(int[] stones)
    {
        int[] cnt = new int[3];

        foreach(int x in stones)
        {
            cnt[x % 3]++;
        }

        int n = stones.Length;

        // 小技巧：
        // 交換 cnt[1] 和 cnt[2] 再呼叫 Check，
        // 相當於 Alice 第一回合移除了餘數為 2 的石頭
        return Check(n, (int[])cnt.Clone()) ||
               Check(n, new int[] { cnt[0], cnt[2], cnt[1] });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="n"></param>
    /// <param name="cnt"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="n"></param>
    /// <param name="cnt"></param>
    /// <returns></returns>
    private bool Check(int n, int[] cnt)
    {
        // Alice 第一回合必須先拿一顆餘數為 1 的石頭
        if(cnt[1] == 0)
        {
            return false;
        }

        cnt[1]--;

        // 第一回合 Alice 移除餘數 1
        // 後面兩人交替移除餘數 1 和 2
        // 中途可以插入 cnt[0] 顆餘數為 0 的石頭
        int rounds = 1 + Math.Min(cnt[1], cnt[2] * 2 + cnt[0]);

        if(cnt[1] > cnt[2])
        {
            // 還可以再移除一顆餘數為 1 的石頭
            rounds++;
        }
        return rounds < n && rounds % 2 > 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stones"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="stones"></param>
    /// <returns></returns>
    public bool StoneGameIX3(int[] stones)
    {
        int[] cnt = new int[3];
        foreach(int x in stones)
        {
            cnt[x % 3]++;
        }

        if(cnt[0] % 2 == 0)
        {
            return cnt[1] > 0 && cnt[2] > 0;
        }
        return Math.Abs(cnt[1] - cnt[2]) > 2;
    }
}