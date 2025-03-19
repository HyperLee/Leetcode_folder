namespace leetcode_295;

class Program
{
    /// <summary>
    /// 295. Find Median from Data Stream
    /// https://leetcode.com/problems/find-median-from-data-stream/description/
    /// 295. 数据流的中位数
    /// https://leetcode.cn/problems/find-median-from-data-stream/description/
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 建立測試案例
        Console.WriteLine("LeetCode 295: Find Median from Data Stream 測試");
        Console.WriteLine("==========================================");

        MedianFinder medianFinder = new MedianFinder();
        
        // 測試案例 1
        Console.WriteLine("\n測試案例 1:");
        medianFinder.AddNum(1);
        Console.WriteLine($"加入 1 後的中位數: {medianFinder.FindMedian()}");  // 預期輸出: 1.0
        
        // 測試案例 2
        Console.WriteLine("\n測試案例 2:");
        medianFinder.AddNum(2);
        Console.WriteLine($"加入 2 後的中位數: {medianFinder.FindMedian()}");  // 預期輸出: 1.5
        
        // 測試案例 3
        Console.WriteLine("\n測試案例 3:");
        medianFinder.AddNum(3);
        Console.WriteLine($"加入 3 後的中位數: {medianFinder.FindMedian()}");  // 預期輸出: 2.0

        // 額外測試案例
        Console.WriteLine("\n額外測試案例:");
        MedianFinder mf = new MedianFinder();
        int[] numbers = { 4, 2, 7, 1, 3, 6 };
        
        foreach (int num in numbers)
        {
            mf.AddNum(num);
            Console.WriteLine($"加入 {num} 後的中位數: {mf.FindMedian()}");
        }

        Console.WriteLine("\n測試完成!");
    }
}

/// <summary>
/// ref: 建議參考鏈結說明
/// https://leetcode.cn/problems/find-median-from-data-stream/solutions/3015873/ru-he-zi-ran-yin-ru-da-xiao-dui-jian-ji-4v22k/
/// https://leetcode.cn/problems/find-median-from-data-stream/solutions/2361972/295-shu-ju-liu-de-zhong-wei-shu-dui-qing-gmdo/
/// https://leetcode.cn/problems/find-median-from-data-stream/solutions/961062/shu-ju-liu-de-zhong-wei-shu-by-leetcode-ktkst/ 
/// LeetCode 295: 數據流中的中位數
/// 解題思路：
/// 1. 使用兩個優先佇列（堆）來維護數據流：
///    - maxHeap：存放較小的一半數字，使用最大堆
///    - minHeap：存放較大的一半數字，使用最小堆
/// 2. 平衡策略：
///    - 維持 maxHeap 和 minHeap 的大小差不超過 1
///    - maxHeap 的所有元素都小於 minHeap 的所有元素
/// 3. 中位數計算：
///    - 如果元素總數為奇數：中位數為 maxHeap 的頂部元素
///    - 如果元素總數為偶數：中位數為兩個堆頂部元素的平均值
/// 時間複雜度：
/// - AddNum: O(log n)，其中 n 為數據流中的元素數量
/// - FindMedian: O(1)
/// 空間複雜度：O(n)，需要存儲所有數據
/// </summary>
public class MedianFinder
{
    /// <summary>
    /// maxHeap 用於存放較小的一半數字
    /// 使用自定義比較器實現最大堆
    /// 堆頂元素是較小一半數字中的最大值
    /// </summary>
    private PriorityQueue<int, int> maxHeap;
    
    /// <summary>
    /// minHeap 用於存放較大的一半數字
    /// 使用默認的最小堆實現
    /// 堆頂元素是較大一半數字中的最小值
    /// </summary>
    private PriorityQueue<int, int> minHeap;

    /// <summary>
    /// 初始化 MedianFinder
    /// 設置兩個優先佇列，其中 maxHeap 使用自定義比較器來實現最大堆
    /// 要注意, 一開始就有經過排序了. 且兩者排序方式不同
    /// ex:輸入數字為 [1, 2, 3, 4, 5, 6]
    /// maxHeap（最大堆）：存放較小的一半數字，使用自定義比較器 (b - a) 實現降序排序
    /// [3, 2, 1] → 3 為堆頂元素
    /// minHeap（最小堆）：存放較大的一半數字，使用默認的升序排序
    /// [4, 5, 6] → 4 為堆頂
    /// </summary>
    public MedianFinder()
    {
        // 初始化最大堆，使用自定義比較器 (b - a) 實現降序排序
        maxHeap = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b - a));
        // 初始化最小堆，使用默認的升序排序
        minHeap = new PriorityQueue<int, int>();
    }

    /// <summary>
    /// 將新數字加入數據流中，並維護兩個堆的平衡狀態
    /// 實現策略：
    /// 1. 當兩個堆大小相等時：
    ///    - 先將數字加入 minHeap（最小堆）
    ///    - 取出 minHeap 最小的數字
    ///    - 將該數字放入 maxHeap（最大堆）
    ///    - 這樣確保 maxHeap 總是包含較小的那一半數字
    /// 2. 當 maxHeap 比 minHeap 大時（差值為1）：
    ///    - 先將數字加入 maxHeap
    ///    - 取出 maxHeap 最大的數字
    ///    - 將該數字放入 minHeap
    ///    - 這樣可以重新平衡兩個堆的大小
    /// </summary>
    /// <param name="num">要加入數據流的新數字</param>
    public void AddNum(int num)
    {
        // 步驟 1: 根據兩個堆的大小關係決定新數字的去向
        if (maxHeap.Count == minHeap.Count)
        {
            // 當兩堆大小相等時：
            // 1. 先將數字加入 minHeap
            // 2. 將 minHeap 的最小值移到 maxHeap
            // 這確保了 maxHeap 始終包含較小的一半數字
            minHeap.Enqueue(num, num);
            int minValue = minHeap.Dequeue();
            maxHeap.Enqueue(minValue, minValue);
        }
        else
        {
            // 當 maxHeap 比 minHeap 大時：
            // 1. 先將數字加入 maxHeap
            // 2. 將 maxHeap 的最大值移到 minHeap
            // 這樣可以維持兩個堆的平衡
            maxHeap.Enqueue(num, num);
            int maxValue = maxHeap.Dequeue();
            minHeap.Enqueue(maxValue, maxValue);
        }
    }

    /// <summary>
    /// 計算當前數據流的中位數
    /// </summary>
    /// <returns>
    /// 如果元素總數為奇數，返回 maxHeap 的頂部元素
    /// 如果元素總數為偶數，返回兩個堆頂部元素的平均值
    /// </returns>
    public double FindMedian()
    {
        // 如果 maxHeap 大於 minHeap，說明總數為奇數
        if (maxHeap.Count > minHeap.Count)
        {
            return maxHeap.Peek();
        }
        // 如果兩個堆大小相等，返回兩個堆頂元素的平均值
        return (maxHeap.Peek() + minHeap.Peek()) / 2.0;
    }
}

