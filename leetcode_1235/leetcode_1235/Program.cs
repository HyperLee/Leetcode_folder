namespace leetcode_1235;

class Program
{
    /// <summary>
    /// 1235. Maximum Profit in Job Scheduling
    /// https://leetcode.com/problems/maximum-profit-in-job-scheduling/description/
    /// 1235. 规划兼职工作
    /// https://leetcode.cn/problems/maximum-profit-in-job-scheduling/description/
    /// 
    /// 題目說明：
    /// 有 n 份兼職工作，每份工作都有開始時間、結束時間和報酬。
    /// 給定三個數組 startTime、endTime 和 profit，其中：
    /// - startTime[i] 是第 i 份工作的開始時間
    /// - endTime[i] 是第 i 份工作的結束時間
    /// - profit[i] 是第 i 份工作的報酬
    /// 
    /// 要求：找出收益最大的工作計畫，工作時間不能重疊
    /// </summary>
    static void Main(string[] args)
    {
        // 測試案例 1
        int[] startTime1 = new int[] { 1, 2, 3, 3 };
        int[] endTime1 = new int[] { 3, 4, 5, 6 };
        int[] profit1 = new int[] { 50, 10, 40, 70 };
        Console.WriteLine("測試案例 1:");
        Console.WriteLine($"預期結果: 120");
        Console.WriteLine($"實際結果: {JobScheduling(startTime1, endTime1, profit1)}");
        Console.WriteLine();

        // 測試案例 2
        int[] startTime2 = new int[] { 1, 2, 3, 4, 6 };
        int[] endTime2 = new int[] { 3, 5, 10, 6, 9 };
        int[] profit2 = new int[] { 20, 20, 100, 70, 60 };
        Console.WriteLine("測試案例 2:");
        Console.WriteLine($"預期結果: 150");
        Console.WriteLine($"實際結果: {JobScheduling(startTime2, endTime2, profit2)}");
        Console.WriteLine();

        // 測試案例 3
        int[] startTime3 = new int[] { 1, 1, 1 };
        int[] endTime3 = new int[] { 2, 3, 4 };
        int[] profit3 = new int[] { 5, 6, 4 };
        Console.WriteLine("測試案例 3:");
        Console.WriteLine($"預期結果: 6");
        Console.WriteLine($"實際結果: {JobScheduling(startTime3, endTime3, profit3)}");
    }

    /// <summary>
    /// ref:動態規劃 ＋二分法查找
    /// https://leetcode.cn/problems/maximum-profit-in-job-scheduling/solutions/1910416/gui-hua-jian-zhi-gong-zuo-by-leetcode-so-gu0e/
    /// https://leetcode.cn/problems/maximum-profit-in-job-scheduling/solutions/1913089/dong-tai-gui-hua-er-fen-cha-zhao-you-hua-zkcg/
    /// https://leetcode.cn/problems/maximum-profit-in-job-scheduling/solutions/1913143/by-ac_oier-rgup/
    /// 使用動態規劃解決工作規劃問題
    /// 解題思路：
    /// 1. 將工作按結束時間排序，方便我們進行後續的動態規劃
    /// 2. 使用 dp[i] 表示前 i 個工作能獲得的最大利潤
    /// 3. 對每個工作，我們可以選擇做或不做：
    ///    - 不做：保持前一個狀態的利潤 dp[i-1]
    ///    - 做：當前工作的利潤 + 不衝突工作的最大利潤
    /// 4. 使用二分查找找到不衝突的工作
    /// 5. 返回 dp[n] 即為最大利潤
    /// 
    /// dp[i] = Math.Max(dp[i - 1], dp[k] + jobs[i - 1][2]);
    /// dp[i - 1]: 不做當前工作的最大利潤, 直接繼承前一個狀態的最大利潤
    /// dp[k] + jobs[i - 1][2]: 做當前工作的最大利潤
    /// dp[k]: 前 k 個工作的最大利潤（k 是最後一個不衝突的工作）
    /// jobs[i - 1][2]: 當前工作的利潤（陣列中的第三個元素是 profit）
    /// </summary>
    /// <param name="startTime">每個工作的開始時間陣列</param>
    /// <param name="endTime">每個工作的結束時間陣列</param>
    /// <param name="profit">每個工作的利潤陣列</param>
    /// <returns>能獲得的最大利潤</returns>
    public static int JobScheduling(int[] startTime, int[] endTime, int[] profit)
    {
        int n = startTime.Length;
        // 步驟1：整合工作資訊到二維陣列中
        int[][] jobs = new int[n][];
        for(int i = 0; i < n; i++)
        {
            jobs[i] = new int[]{startTime[i], endTime[i], profit[i]};
        }

        // 步驟2：按結束時間對工作進行排序; 升序
        Array.Sort(jobs, (a, b) => a[1] - b[1]); // 使用減法
        // 同上述寫法.
        //Array.Sort(jobs, (a, b) => a[1].CompareTo(b[1])); // 使用 CompareTo

        // 步驟3：動態規劃求解
        // dp[0] 代表不選擇任何工作時的利潤（即為 0; 這是動態規劃的基礎情況（base case）
        int[] dp = new int[n + 1];

        // jobs[i-1] 對應到原始工作的索引
        // dp[i] 對應到考慮前 i 個工作的最大利潤
        for(int i = 1; i <= n; i++)
        {
            // 找到當前工作不衝突的最近工作
            int k = BinarySearch(jobs, i - 1, jobs[i - 1][0]);
            // 選擇不做當前工作與做當前工作，兩者取最大值
            dp[i] = Math.Max(dp[i - 1], dp[k] + jobs[i - 1][2]);
        }
        
        return dp[n];
    }

    /// <summary>
    /// 對於當前工作 i
    /// 尋找結束時間小於等於當前工作開始時間的最後一個工作
    /// ->確保選擇的工作時間不會重疊
    /// ->只有結束時間小於等於當前工作開始時間的工作才能被選擇
    /// 白話文就是要找出不衝突的工作,
    /// 並且這個工作的結束時間要小於等於當前工作的開始時間
    /// 
    /// 二分查找最後一個結束時間小於等於目標時間的工作
    /// 解題思路：
    /// 1. 在已排序的工作陣列中尋找最後一個符合條件的位置
    /// 2. 利用二分查找優化搜尋效率
    /// 3. 回傳的索引值代表最後一個可以與目標工作相容的工作位置
    /// </summary>
    /// <param name="jobs">已按結束時間排序的工作陣列</param>
    /// <param name="right">搜尋範圍的右邊界</param>
    /// <param name="target">目標開始時間</param>
    /// <returns>最後一個相容工作的索引</returns>
    public static int BinarySearch(int[][] jobs, int right, int target) 
    {
        int left = 0;
        while(left < right)
        {
            // 避免溢位, 常見二分法寫法
            int mid = left + (right - left) / 2;
            if(jobs[mid][1] <= target)  // 如果中間位置的工作結束時間小於等於目標時間
            {
                // 可能還有更後面的相容工作, 向右尋找(往右可能可以找到更適合的工作)
                left = mid + 1;         
            }
            else
            {
                // 需要在前半部分尋找, 向左尋找(發生衝突, 需要找到不衝突的工作)
                right = mid;            
            }
        }
        return left;
    }
}
