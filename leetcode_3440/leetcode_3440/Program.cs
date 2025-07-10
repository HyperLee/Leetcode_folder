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
        Console.WriteLine("Hello, World!");
    }
}
