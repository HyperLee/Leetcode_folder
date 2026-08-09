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
    /// <summary>
    /// 3440. Reschedule Meetings for Maximum Free Time II
    /// https://leetcode.com/problems/reschedule-meetings-for-maximum-free-time-ii/description/
    /// <para>
    /// You are given eventTime, the duration of an event, and arrays startTime and endTime of length n. They describe n non-overlapping meetings during [0, eventTime], with meeting i occupying [startTime[i], endTime[i]].
    ///
    /// You may reschedule at most one meeting by moving its start time while preserving its duration and keeping meetings non-overlapping, to maximize the longest continuous free-time interval.
    ///
    /// Return the maximum possible free time. Meetings must remain inside the event and non-overlapping. In this version, their relative order may change after rescheduling one meeting.
    ///
    /// Example 1:
    /// Input: eventTime = 5, startTime = [1,3], endTime = [2,5]
    /// Output: 2
    /// Explanation: Move [1,2] to [2,3], leaving [0,2] free.
    ///
    /// Example 2:
    /// Input: eventTime = 10, startTime = [0,7,9], endTime = [1,8,10]
    /// Output: 7
    /// Explanation: Move [0,1] to [8,9], leaving [0,7] free.
    ///
    /// Example 3:
    /// Input: eventTime = 10, startTime = [0,3,7,9], endTime = [1,4,8,10]
    /// Output: 6
    /// Explanation: Move [3,4] to [8,9], leaving [1,7] free.
    ///
    /// Example 4:
    /// Input: eventTime = 5, startTime = [0,1,2,3,4], endTime = [1,2,3,4,5]
    /// Output: 0
    /// Explanation: No event time is free.
    ///
    /// Constraints:
    /// - 1 &lt;= eventTime &lt;= 10^9
    /// - n == startTime.length == endTime.length
    /// - 2 &lt;= n &lt;= 10^5
    /// - 0 &lt;= startTime[i] &lt; endTime[i] &lt;= eventTime
    /// - endTime[i] &lt;= startTime[i + 1] for i in [0, n - 2]
    /// </para>
    /// <para>
    /// 3440. 重新安排會議以取得最大空閒時間 II
    /// https://leetcode.cn/problems/reschedule-meetings-for-maximum-free-time-ii/description/
    ///
    /// 給定活動持續時間 eventTime，以及長度為 n 的陣列 startTime 與 endTime。它們描述 [0, eventTime] 內 n 場互不重疊的會議，第 i 場位於 [startTime[i], endTime[i]]。
    ///
    /// 你最多可以重新安排一場會議：移動其開始時間但維持相同持續時間，且所有會議仍不重疊，以最大化最長連續空閒時段。
    ///
    /// 回傳可能的最大空閒時間。會議不得移到活動外，且必須保持不重疊。在本版本中，重新安排一場會議後，會議相對順序可以改變。
    ///
    /// 範例 1：
    /// 輸入：eventTime = 5, startTime = [1,3], endTime = [2,5]
    /// 輸出：2
    /// 解釋：將 [1,2] 移到 [2,3]，使 [0,2] 沒有會議。
    ///
    /// 範例 2：
    /// 輸入：eventTime = 10, startTime = [0,7,9], endTime = [1,8,10]
    /// 輸出：7
    /// 解釋：將 [0,1] 移到 [8,9]，使 [0,7] 沒有會議。
    ///
    /// 範例 3：
    /// 輸入：eventTime = 10, startTime = [0,3,7,9], endTime = [1,4,8,10]
    /// 輸出：6
    /// 解釋：將 [3,4] 移到 [8,9]，使 [1,7] 沒有會議。
    ///
    /// 範例 4：
    /// 輸入：eventTime = 5, startTime = [0,1,2,3,4], endTime = [1,2,3,4,5]
    /// 輸出：0
    /// 解釋：活動期間沒有未被會議占用的時間。
    ///
    /// 限制條件：
    /// - 1 &lt;= eventTime &lt;= 10^9
    /// - n == startTime.length == endTime.length
    /// - 2 &lt;= n &lt;= 10^5
    /// - 0 &lt;= startTime[i] &lt; endTime[i] &lt;= eventTime
    /// - 對 [0, n - 2] 中的 i，endTime[i] &lt;= startTime[i + 1]
    /// </para>
    /// </summary>
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
