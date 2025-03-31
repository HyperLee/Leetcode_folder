namespace leetcode_252;

public class Solution {
    /// <summary>
    /// 1.先處理邊界條件：如果沒有會議或只有一個會議，可以直接返回 true
    /// 2.將會議時間區間根據開始時間進行排序
    /// 3.遍歷排序後的會議時間區間，檢查相鄰的會議時間是否重疊
    /// </summary>
    /// <param name="intervals"></param>
    /// <returns></returns>
    public bool CanAttendMeetings(int[][] intervals) {
        // 如果沒有會議或只有一個會議，可以參加所有會議
        if (intervals == null || intervals.Length <= 1) 
        {
            return true;
        }
        
        // 根據會議開始時間排序
        Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));
        
        // 檢查相鄰會議是否重疊
        for (int i = 1; i < intervals.Length; i++) {
            // 如果前一個會議的結束時間大於後一個會議的開始時間
            // 表示會議時間重疊，無法參加所有會議
            if (intervals[i - 1][1] > intervals[i][0]) {
                return false;
            }
        }
        
        // 沒有重疊的情況，可以參加所有會議
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
        Solution solution = new Solution();
        
        // 測試案例 1: [[0,30],[5,10],[15,20]] => false
        int[][] test1 = new int[][] {
            new int[] {0, 30},
            new int[] {5, 10},
            new int[] {15, 20}
        };
        Console.WriteLine($"Test case 1: {solution.CanAttendMeetings(test1)}"); // 應該輸出 false
        
        // 測試案例 2: [[7,10],[2,4]] => true
        int[][] test2 = new int[][] {
            new int[] {7, 10},
            new int[] {2, 4}
        };
        Console.WriteLine($"Test case 2: {solution.CanAttendMeetings(test2)}"); // 應該輸出 true
    }
}
