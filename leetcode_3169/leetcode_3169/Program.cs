namespace leetcode_3169;

class Program
{
    /// <summary>
    /// 3169. Count Days Without Meetings
    /// https://leetcode.com/problems/count-days-without-meetings/description/
    /// 3169. 無需開會的工作日
    /// https://leetcode.cn/problems/count-days-without-meetings/description/?envType=daily-question&envId=2025-07-11
    /// 
    /// 題目描述：
    /// 給定一個正整數 days，表示員工可工作的總天數（從第 1 天開始）。還給定一個大小為 n 的二維陣列 meetings，其中 meetings[i] = [start_i, end_i] 代表第 i 個會議的開始和結束天（包含端點）。
    /// 請返回員工可工作但沒有排定會議的天數。
    /// 注意：會議可能重疊。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料：10 天，會議分別為 [2,4] 與 [5,7]
        int days = 10;
        int[][] meetings = new int[][]
        {
            new int[] {2, 4},
            new int[] {5, 7}
        };
        var program = new Program();
        int result = program.CountDays(days, meetings);
        Console.WriteLine($"可工作但無會議的天數: {result}"); // 預期輸出 4

        int result2 = program.CountDaysByMarking(days, meetings);
        Console.WriteLine($"(布林陣列標記法)可工作但無會議的天數: {result2}"); // 預期輸出 4
    }


    /// <summary>
    /// 計算員工可工作但沒有排定會議的天數。
    /// 解題說明：
    /// 1. 先將所有會議依起始天數排序。
    /// 2. 依序合併重疊的會議區間，並累計有會議的天數。
    /// 3. 最後用總天數扣除有會議的天數即為答案。
    /// </summary>
    /// <param name="days">員工可工作的總天數</param>
    /// <param name="meetings">會議區間陣列，每個元素為 [start, end]</param>
    /// <returns>可工作但無會議的天數</returns>
    public int CountDays(int days, int[][] meetings)
    {
        // 依會議起始天排序，方便合併重疊區間
        Array.Sort(meetings, (a, b) => a[0].CompareTo(b[0]));

        int l = 1; // 當前合併區間的起始天
        int r = 0; // 當前合併區間的結束天
        foreach (var m in meetings)
        {
            // 若新會議起始天大於目前合併區間結束天，表示有空檔
            if (m[0] > r)
            {
                // 扣除有會議的天數
                days -= (r - l + 1);
                l = m[0]; // 更新合併區間起始天
            }
            // 合併重疊區間，更新結束天
            r = Math.Max(r, m[1]);
        }
        // 最後一段有會議的天數也要扣除
        days -= (r - l + 1);
        // 回傳剩餘可工作但無會議的天數
        return days;
    }

    /// <summary>
    /// 使用布林陣列標記法計算可工作但無會議的天數。
    /// 解題說明：
    /// 1. 建立一個長度為 days+1 的布林陣列（index 1~days），初始皆為 false。
    /// 2. 遍歷每個會議區間，將對應天數標記為 true（表示該天有會議）。
    /// 3. 最後統計陣列中為 false 的天數，即為可工作但無會議的天數。
    /// 適合 days 範圍不大時，程式碼簡單易懂。
    /// </summary>
    /// <param name="days">員工可工作的總天數</param>
    /// <param name="meetings">會議區間陣列，每個元素為 [start, end]</param>
    /// <returns>可工作但無會議的天數</returns>
    public int CountDaysByMarking(int days, int[][] meetings)
    {
        // 建立布林陣列，index 1~days
        var hasMeeting = new bool[days + 1];

        // 標記所有有會議的天數
        foreach (var m in meetings)
        {
            int start = m[0];
            int end = m[1];
            // 將區間內每一天標記為 true
            for (int d = start; d <= end; d++)
            {
                hasMeeting[d] = true;
            }
        }

        // 統計未被標記（無會議）的天數
        int count = 0;
        for (int d = 1; d <= days; d++)
        {
            if (!hasMeeting[d])
            {
                count++;
            }
        }
        return count;
    }
}
