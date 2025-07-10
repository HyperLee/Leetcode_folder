namespace leetcode_3440;

/// <summary>
/// 3440. Reschedule Meetings for Maximum Free Time II
/// https://leetcode.com/problems/reschedule-meetings-for-maximum-free-time-ii/description/?envType=daily-question&envId=2025-07-10
/// 3440. 重新安排會議得到最多空餘時間 II
/// https://leetcode.cn/problems/reschedule-meetings-for-maximum-free-time-ii/description/?envType=daily-question&envId=2025-07-10
///
/// 題目描述：
/// 給定一個整數 eventTime 表示一個活動的持續時間，還有兩個長度為 n 的整數陣列 startTime 和 endTime，
/// 分別表示 n 個不重疊會議的開始和結束時間，這些會議發生在 t = 0 到 t = eventTime 之間。
/// 第 i 個會議的時間區間為 [startTime[i], endTime[i]]。
/// 你可以將最多一個會議的開始時間重新安排（保持其持續時間不變），
/// 並且會議之間仍然不能重疊，目標是最大化活動期間內最長的連續空閒時間。
/// 返回重新安排後，能得到的最大空閒時間。
/// 注意：會議不能被移動到活動時間之外，且會議之間不能重疊。
/// 此版本允許重新安排後會議的相對順序改變。
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        // 測試資料範例
        int eventTime = 20;
        int[] startTime = { 2, 6, 12 };
        int[] endTime = { 4, 10, 15 };

        var program = new Program();
        int maxFree = program.MaxFreeTime(eventTime, startTime, endTime);
        Console.WriteLine($"最大空閒時間: {maxFree}");
    }

    private int eventTime;
    private int[] startTime = Array.Empty<int>();
    private int[] endTime = Array.Empty<int>();

    /// <summary>
    /// 計算重新安排會議後，能得到的最大空閒時間。
    /// 
    /// 解題思路：
    /// 將每個會議視為一張桌子，空閒時間視為空位。目標是將一張桌子（會議）移動到另一個空位，
    /// 使得活動期間內的最大連續空閒時間最大化。為此，需考慮：
    /// 1. 計算所有 n+1 個空位的長度，找出前三大的空位（下標分別為 a, b, c）。
    /// 2. 枚舉每一張桌子，嘗試將其移動到不與其相鄰的最大空位（a、b、c之一）。
    /// 3. 若能移動（空位長度足夠），新的空位長度為桌子長度加上左右兩側空位長度；
    ///    否則只能合併左右空位。
    /// 4. 最終取所有情況下的最大空閒時間。
    /// 
    /// ref: https://leetcode.cn/problems/reschedule-meetings-for-maximum-free-time-ii/solutions/3061629/wei-hu-qian-san-da-de-kong-wei-mei-ju-fe-xm2f/?envType=daily-question&envId=2025-07-10
    /// 
    /// </summary>
    /// <param name="eventTime">活動總時長</param>
    /// <param name="startTime">每個會議的開始時間</param>
    /// <param name="endTime">每個會議的結束時間</param>
    /// <returns>最大空閒時間</returns>
    public int MaxFreeTime(int eventTime, int[] startTime, int[] endTime)
    {
        this.eventTime = eventTime;
        this.startTime = startTime;
        this.endTime = endTime;
        int n = startTime.Length;

        // a, b, c 分別為前三大空位的下標
        int a = 0;
        int b = -1;
        int c = -1;

        // 找出前三大空位的位置 idx
        for (int i = 1; i <= n; i++)
        {
            int size = Get(i);

            if (size > Get(a))
            {
                c = b;
                b = a;
                a = i;
            }
            else if (b < 0 || size > Get(b))
            {
                c = b;
                b = i;
            }
            else if (c < 0 || size > Get(c))
            {
                c = i;
            }
        }

        int res = 0;
        // 枚舉每一張桌子（會議），嘗試移動到最大空位
        for (int i = 0; i < n; i++)
        {
            int size = endTime[i] - startTime[i]; // 桌子長度（會議時長）

            if ((i != a && i + 1 != a && size <= Get(a)) ||
                (i != b && i + 1 != b && size <= Get(b)) ||
                size <= Get(c))
            {
                res = Math.Max(res, Get(i) + size + Get(i + 1));
            }
            else
            {
                res = Math.Max(res, Get(i) + Get(i + 1));
            }
        }

        return res;
    }

    /// <summary>
    /// 計算第 i 個空位的長度。
    /// 空位定義：
    /// - 最左側 i = 0：活動起點到第一個會議的空閒時間。
    /// - 最右側 i = n：最後一個會議結束到活動結束的空閒時間。
    /// - 中間：第 i-1 個會議結束到第 i 個會議開始的空閒時間。
    /// </summary>
    /// <param name="i">空位下標（0 ~ n）</param>
    /// <returns>空位長度</returns>
    private int Get(int i)
    {
        int n = startTime.Length;
        if (i == 0)
        {
            // 活動起點到第一個會議的空閒時間
            return startTime[0];
        }
        
        if (i == n)
        {
            // 最後一個會議結束到活動結束的空閒時間
            return eventTime - endTime[n - 1];
        }

        // 第 i-1 個會議結束到第 i 個會議開始的空閒時間
        return startTime[i] - endTime[i - 1];
    }
}
