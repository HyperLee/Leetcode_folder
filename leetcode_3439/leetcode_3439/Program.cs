namespace leetcode_3439;

class Program
{
    /// <summary>
    /// 3439. Reschedule Meetings for Maximum Free Time I
    /// https://leetcode.com/problems/reschedule-meetings-for-maximum-free-time-i/description/?envType=daily-question&envId=2025-07-09
    ///
    /// 3439. 重新安排會議得到最多空餘時間 I
    /// https://leetcode.cn/problems/reschedule-meetings-for-maximum-free-time-i/description/?envType=daily-question&envId=2025-07-09
    ///
    /// 題目描述：
    /// 給定一個整數 eventTime，表示一個事件的持續時間，事件發生於 t = 0 到 t = eventTime。
    /// 還給定兩個整數陣列 startTime 和 endTime，長度皆為 n，分別表示 n 個不重疊會議的開始與結束時間，第 i 個會議發生於 [startTime[i], endTime[i]]。
    /// 你最多可以重新安排 k 個會議的開始時間（會議長度不變），以最大化事件期間內最長的連續空閒時間。
    /// 所有會議的相對順序必須保持不變，且會議之間不得重疊。
    /// 會議不能被安排到事件之外的時間。
    /// 請返回重新安排會議後，能獲得的最大連續空閒時間。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料：示例 1
        int eventTime = 5;
        int k = 1;
        int[] startTime = { 1, 3 };
        int[] endTime = { 2, 4 };
        var program = new Program();
        int result = program.MaxFreeTime(eventTime, k, startTime, endTime);
        Console.WriteLine($"最大連續空閒時間: {result}"); // 預期輸出: 2
    }


    /// <summary>
    /// 計算重新安排最多 k 個會議後，能獲得的最大連續空閒時間。
    ///
    /// 解題說明：
    /// 將 n 個會議分割出 n+1 個空閒時間段，最優策略是合併連續 k+1 個空閒時間段，
    /// 也就是將 k 個會議盡量移動，使 k+1 個空閒區間合併成一段，取得最大長度。
    /// 以滑動視窗計算所有連續 k+1 個空閒區間的總長度，取最大值即為答案。
    /// </summary>
    /// <param name="eventTime">事件總時長</param>
    /// <param name="k">最多可重新安排的會議數</param>
    /// <param name="startTime">會議開始時間陣列</param>
    /// <param name="endTime">會議結束時間陣列</param>
    /// <returns>最大連續空閒時間</returns>
    public int MaxFreeTime(int eventTime, int k, int[] startTime, int[] endTime)
    {
        int ans = 0;
        int s = 0;
        int n = startTime.Length;
        // 使用滑動視窗，合併連續 k+1 個空閒區間
        for (int i = 0; i <= n; i++)
        {
            s += GetGap(i, eventTime, startTime, endTime);
            if (i < k)
            {
                continue;
            }
            ans = Math.Max(ans, s);
            s -= GetGap(i - k, eventTime, startTime, endTime);
        }
        return ans;
    }

    /// <summary>
    /// 計算第 i 個空閒區間的長度。
    ///
    /// 解題說明：
    /// 空閒區間分為三種：
    /// 1. 最左側：從 0 到第一個會議開始。
    /// 2. 最右側：從最後一個會議結束到 eventTime。
    /// 3. 中間：兩個相鄰會議之間。
    /// </summary>
    /// <param name="i">空閒區間索引</param>
    /// <param name="eventTime">事件總時長</param>
    /// <param name="startTime">會議開始時間陣列</param>
    /// <param name="endTime">會議結束時間陣列</param>
    /// <returns>空閒區間長度</returns>
    private int GetGap(int i, int eventTime, int[] startTime, int[] endTime)
    {
        int n = startTime.Length;
        if (i == 0)
        {
            return startTime[0]; // 最左側空閒區間
        }
        if (i == n)
        {
            return eventTime - endTime[n - 1]; // 最右側空閒區間
        }
        return startTime[i] - endTime[i - 1]; // 中間空閒區間
    }
}
