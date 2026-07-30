namespace leetcode_252;

public class Solution
{
    /// <summary>
    /// 判斷是否能參加所有會議。此解法先依開始時間原地排序區間，
    /// 再檢查相鄰會議；若前一場的結束時間晚於下一場的開始時間，
    /// 便表示兩場會議重疊。排序會改變輸入區間的排列順序。
    /// </summary>
    /// <param name="intervals">
    /// 每個元素皆為 <c>[start, end]</c> 的會議區間；有效區間須滿足
    /// <c>start &lt; end</c>。傳入 null、空陣列或單一區間時皆視為沒有衝突。
    /// </param>
    /// <returns>所有會議皆不重疊時回傳 true，否則回傳 false。</returns>
    public bool CanAttendMeetings(int[][] intervals)
    {
        if (intervals == null || intervals.Length <= 1)
        {
            return true;
        }

        // 排序後若存在重疊，衝突必定會出現在某一對相鄰區間。
        Array.Sort(intervals, static (a, b) => a[0].CompareTo(b[0]));

        for (int i = 1; i < intervals.Length; i++)
        {
            if (intervals[i - 1][1] > intervals[i][0])
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 判斷是否能參加所有會議。此解法將所有開始時間與結束時間分別複製並排序，
    /// 再檢查第 i 場開始時間是否早於前一個已排序的結束時間。
    /// 因為只排序新建立的一維陣列，所以不會改變輸入區間。
    /// </summary>
    /// <param name="intervals">
    /// 每個元素皆為 <c>[start, end]</c> 的會議區間；有效區間須滿足
    /// <c>start &lt; end</c>。傳入 null、空陣列或單一區間時皆視為沒有衝突。
    /// </param>
    /// <returns>所有會議皆不重疊時回傳 true，否則回傳 false。</returns>
    public bool CanAttendMeetings2(int[][] intervals)
    {
        if (intervals == null || intervals.Length <= 1)
        {
            return true;
        }

        int[] starts = new int[intervals.Length];
        int[] ends = new int[intervals.Length];

        for (int i = 0; i < intervals.Length; i++)
        {
            starts[i] = intervals[i][0];
            ends[i] = intervals[i][1];
        }

        Array.Sort(starts);
        Array.Sort(ends);

        // 若下一個開始時間早於目前最早尚未銜接的結束時間，代表兩場同時進行。
        for (int i = 1; i < intervals.Length; i++)
        {
            if (starts[i] < ends[i - 1])
            {
                return false;
            }
        }

        return true;
    }
}

class Program
{
    /// <summary>
    /// 252. Meeting Rooms
    /// https://leetcode.com/problems/meeting-rooms/description/?envType=problem-list-v2&envId=oizxjoit
    /// 
    /// Given an array of meeting time intervals where intervals[i] = [starti, endi], 
    /// determine if a person could attend all meetings.
    /// 
    /// Example 1:
    /// Input: intervals = [[0,30],[5,10],[15,20]]
    /// Output: false
    /// 
    /// Example 2:
    /// Input: intervals = [[7,10],[2,4]]
    /// Output: true
    /// 
    /// 給定一個會議時間區間的陣列 intervals，其中 intervals[i] = [starti, endi]
    /// 判斷一個人是否可以參加所有會議。
    /// 換句話說，需要確認這些會議時間是否有重疊
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        RunSamples();
    }

    /// <summary>
    /// 建立六組固定案例並依序執行兩種解法，共完成十二項結果驗證，
    /// 最後輸出通過數量，作為此主控台專案的可重複執行驗收入口。
    /// </summary>
    private static void RunSamples()
    {
        SampleCase[] sampleCases =
        [
            new(
                "題目範例一：會議重疊",
                [[0, 30], [5, 10], [15, 20]],
                false),
            new(
                "題目範例二：未排序但不重疊",
                [[7, 10], [2, 4]],
                true),
            new(
                "沒有會議",
                [],
                true),
            new(
                "只有一場會議",
                [[5, 8]],
                true),
            new(
                "前一場結束時下一場剛好開始",
                [[1, 5], [5, 10]],
                true),
            new(
                "三場未排序且彼此不重疊",
                [[10, 12], [0, 3], [5, 8]],
                true)
        ];

        Solution solution = new Solution();
        int passedChecks = 0;

        for (int i = 0; i < sampleCases.Length; i++)
        {
            passedChecks += RunCase(solution, sampleCases[i], i + 1);
        }

        int totalChecks = sampleCases.Length * 2;
        Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
    }

    /// <summary>
    /// 執行單一案例，分別以獨立的輸入副本呼叫兩種解法，
    /// 比對預期與實際結果並輸出 PASS 或 FAIL。
    /// </summary>
    /// <param name="solution">提供兩種會議重疊判斷方法的解題物件。</param>
    /// <param name="sampleCase">包含案例名稱、會議區間與預期結果的測試資料。</param>
    /// <param name="caseNumber">從一開始顯示的案例編號。</param>
    /// <returns>此案例通過的檢查數量，範圍為零到二。</returns>
    private static int RunCase(Solution solution, SampleCase sampleCase, int caseNumber)
    {
        string inputText = FormatIntervals(sampleCase.Intervals);

        // 第一種解法會原地排序，因此兩種解法必須各自取得深複製的資料。
        bool firstResult = solution.CanAttendMeetings(CloneIntervals(sampleCase.Intervals));
        bool secondResult = solution.CanAttendMeetings2(CloneIntervals(sampleCase.Intervals));
        bool firstPassed = firstResult == sampleCase.Expected;
        bool secondPassed = secondResult == sampleCase.Expected;

        Console.WriteLine($"案例 {caseNumber}：{sampleCase.Name}");
        Console.WriteLine($"輸入：{inputText}");
        Console.WriteLine($"預期：{sampleCase.Expected}");
        Console.WriteLine($"解法一：{firstResult} => {(firstPassed ? "PASS" : "FAIL")}");
        Console.WriteLine($"解法二：{secondResult} => {(secondPassed ? "PASS" : "FAIL")}");
        Console.WriteLine();

        return (firstPassed ? 1 : 0) + (secondPassed ? 1 : 0);
    }

    /// <summary>
    /// 深複製二維會議區間，使會原地排序的解法不會改動共用案例資料。
    /// </summary>
    /// <param name="intervals">要複製的非 null 會議區間陣列。</param>
    /// <returns>外層與每個內層區間皆為新陣列的完整副本。</returns>
    private static int[][] CloneIntervals(int[][] intervals)
    {
        int[][] copy = new int[intervals.Length][];

        for (int i = 0; i < intervals.Length; i++)
        {
            copy[i] = (int[])intervals[i].Clone();
        }

        return copy;
    }

    /// <summary>
    /// 將會議區間轉成緊湊且固定的字串格式，方便主控台與 README 對照。
    /// </summary>
    /// <param name="intervals">每個元素皆含開始與結束時間的區間陣列。</param>
    /// <returns>例如 <c>[[0,30],[5,10]]</c>；空陣列則回傳 <c>[]</c>。</returns>
    private static string FormatIntervals(int[][] intervals)
    {
        string[] formattedIntervals = new string[intervals.Length];

        for (int i = 0; i < intervals.Length; i++)
        {
            formattedIntervals[i] = $"[{intervals[i][0]},{intervals[i][1]}]";
        }

        return $"[{string.Join(",", formattedIntervals)}]";
    }

    /// <summary>
    /// 表示一組可重複執行的會議室案例，保存顯示名稱、輸入區間與預期結果。
    /// </summary>
    /// <param name="Name">案例的繁體中文顯示名稱。</param>
    /// <param name="Intervals">要交給兩種解法驗證的會議區間。</param>
    /// <param name="Expected">是否能參加全部會議的預期答案。</param>
    private sealed record SampleCase(string Name, int[][] Intervals, bool Expected);
}
