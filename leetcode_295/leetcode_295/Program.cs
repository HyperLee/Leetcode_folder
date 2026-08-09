namespace leetcode_295;

class Program
{
    /// <summary>
    /// 295. Find Median from Data Stream
    /// https://leetcode.com/problems/find-median-from-data-stream/description/
    /// <para>
    /// The median is the middle value in an ordered integer list. If the list has an even size, there is no single middle value, and the median is the mean of the two middle values. For arr = [2,3,4], the median is 3. For arr = [2,3], the median is (2 + 3) / 2 = 2.5.
    ///
    /// Implement the MedianFinder class:
    /// - MedianFinder() initializes the MedianFinder object.
    /// - void addNum(int num) adds integer num from the data stream to the data structure.
    /// - double findMedian() returns the median of all elements so far. Answers within 10^-5 of the actual answer are accepted.
    ///
    /// Example 1:
    /// Input: ["MedianFinder","addNum","addNum","findMedian","addNum","findMedian"], [[],[1],[2],[],[3],[]]
    /// Output: [null,null,null,1.5,null,2.0]
    /// Explanation: Create MedianFinder. Add 1 and 2; findMedian() returns 1.5, namely (1 + 2) / 2. Add 3; findMedian() returns 2.0.
    ///
    /// Constraints:
    /// - -10^5 &lt;= num &lt;= 10^5
    /// - There is at least one element in the data structure before findMedian is called.
    /// - At most 5 * 10^4 calls are made to addNum and findMedian.
    ///
    /// Follow-up:
    /// - If all stream integers are in [0, 100], how would you optimize the solution?
    /// - If 99% of all stream integers are in [0, 100], how would you optimize the solution?
    /// </para>
    /// <para>
    /// 295. 資料流的中位數
    /// https://leetcode.cn/problems/find-median-from-data-stream/description/
    ///
    /// 中位數是已排序整數串列的中間值。若串列長度為偶數，便沒有單一中間值，中位數是中間兩數的平均值。對 arr = [2,3,4]，中位數是 3；對 arr = [2,3]，中位數是 (2 + 3) / 2 = 2.5。
    ///
    /// 實作 MedianFinder 類別：
    /// - MedianFinder() 初始化 MedianFinder 物件。
    /// - void addNum(int num) 將資料流中的整數 num 加入資料結構。
    /// - double findMedian() 回傳目前所有元素的中位數。與實際答案相差不超過 10^-5 的答案均會被接受。
    ///
    /// 範例 1：
    /// 輸入：["MedianFinder","addNum","addNum","findMedian","addNum","findMedian"], [[],[1],[2],[],[3],[]]
    /// 輸出：[null,null,null,1.5,null,2.0]
    /// 解釋：建立 MedianFinder，加入 1 與 2；findMedian() 回傳 1.5，也就是 (1 + 2) / 2。加入 3 後，findMedian() 回傳 2.0。
    ///
    /// 限制條件：
    /// - -10^5 &lt;= num &lt;= 10^5
    /// - 呼叫 findMedian 前，資料結構中至少有一個元素。
    /// - addNum 與 findMedian 最多共被呼叫 5 * 10^4 次。
    ///
    /// 進階：
    /// - 若資料流中的所有整數都在 [0, 100] 範圍內，你會如何最佳化解法？
    /// - 若資料流中 99% 的整數都在 [0, 100] 範圍內，你會如何最佳化解法？
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var sampleCases = new (string Name, int[] Numbers, double[] ExpectedMedians)[]
        {
            ("官方範例", [1, 2, 3], [1.0, 1.5, 2.0]),
            ("遞減資料", [5, 4, 3, 2, 1], [5.0, 4.5, 4.0, 3.5, 3.0]),
            ("重複值", [2, 2, 2, 2], [2.0, 2.0, 2.0, 2.0]),
            ("負數", [-5, -1, -3], [-5.0, -3.0, -3.0]),
            ("正負混合", [-10, 0, 10, 20], [-10.0, -5.0, 0.0, 5.0]),
            ("題目上下界", [-100000, 100000], [-100000.0, 0.0])
        };

        int passedChecks = 0;
        int totalChecks = 0;

        Console.WriteLine("LeetCode 295: Find Median from Data Stream");
        Console.WriteLine("==========================================");

        for (int caseIndex = 0; caseIndex < sampleCases.Length; caseIndex++)
        {
            var sample = sampleCases[caseIndex];
            var medianFinder = new MedianFinder();

            Console.WriteLine($"\n案例 {caseIndex + 1}：{sample.Name}");
            Console.WriteLine($"資料流：[{string.Join(", ", sample.Numbers)}]");

            for (int stepIndex = 0; stepIndex < sample.Numbers.Length; stepIndex++)
            {
                int number = sample.Numbers[stepIndex];
                double expected = sample.ExpectedMedians[stepIndex];

                medianFinder.AddNum(number);
                double actual = medianFinder.FindMedian();
                bool passed = Math.Abs(actual - expected) < 1e-9;

                totalChecks++;
                if (passed)
                {
                    passedChecks++;
                }

                string expectedText = expected.ToString(
                    "0.0####",
                    System.Globalization.CultureInfo.InvariantCulture);
                string actualText = actual.ToString(
                    "0.0####",
                    System.Globalization.CultureInfo.InvariantCulture);

                Console.WriteLine(
                    $"步驟 {stepIndex + 1}：加入 {number} | Expected: {expectedText} | Actual: {actualText} | {(passed ? "PASS" : "FAIL")}");
            }
        }

        Console.WriteLine($"\n總結：{passedChecks}/{totalChecks} 項驗證通過");
    }
}

/// <summary>
/// 以兩個優先佇列維護資料流的中位數。
/// 最大堆保存較小的一半，最小堆保存較大的一半，讓加入數字的成本為
/// <c>O(log n)</c>，查詢中位數的成本為 <c>O(1)</c>。
/// 輸入數字須符合題目限制 <c>-10^5 &lt;= num &lt;= 10^5</c>。
/// </summary>
public class MedianFinder
{
    /// <summary>
    /// 保存資料流較小的一半，堆頂是這一半中的最大值。
    /// </summary>
    private PriorityQueue<int, int> maxHeap;

    /// <summary>
    /// 保存資料流較大的一半，堆頂是這一半中的最小值。
    /// </summary>
    private PriorityQueue<int, int> minHeap;

    /// <summary>
    /// 建立不含任何資料的中位數查詢器。
    /// 最大堆以反向優先序保存較小的一半，最小堆使用預設優先序保存較大的一半。
    /// 建立完成後必須先呼叫 <see cref="AddNum(int)"/>，才能呼叫
    /// <see cref="FindMedian"/>。
    /// </summary>
    public MedianFinder()
    {
        maxHeap = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b - a));
        minHeap = new PriorityQueue<int, int>();
    }

    /// <summary>
    /// 將一個整數加入資料流，並透過堆頂搬移維持兩個不變量：
    /// 最大堆的數量等於最小堆或多一個，且最大堆頂不大於最小堆頂。
    /// 每次加入的時間複雜度為 <c>O(log n)</c>。
    /// </summary>
    /// <param name="num">要加入的整數，範圍為 <c>-10^5</c> 到 <c>10^5</c>。</param>
    public void AddNum(int num)
    {
        if (maxHeap.Count == minHeap.Count)
        {
            // 先經過最小堆篩選，再把較小值移入最大堆，讓奇數筆資料時左側多一個元素。
            minHeap.Enqueue(num, num);
            int minValue = minHeap.Dequeue();
            maxHeap.Enqueue(minValue, minValue);
        }
        else
        {
            // 先經過最大堆篩選，再把較大值移入最小堆，使兩堆恢復相同大小。
            maxHeap.Enqueue(num, num);
            int maxValue = maxHeap.Dequeue();
            minHeap.Enqueue(maxValue, maxValue);
        }
    }

    /// <summary>
    /// 回傳目前所有已加入數字的中位數。
    /// 奇數筆資料取最大堆頂；偶數筆資料取兩個堆頂的平均值，時間複雜度為
    /// <c>O(1)</c>。
    /// </summary>
    /// <returns>目前資料流的中位數。</returns>
    /// <remarks>呼叫前至少必須成功呼叫一次 <see cref="AddNum(int)"/>。</remarks>
    public double FindMedian()
    {
        if (maxHeap.Count > minHeap.Count)
        {
            return maxHeap.Peek();
        }

        return (maxHeap.Peek() + minHeap.Peek()) / 2.0;
    }
}
