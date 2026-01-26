namespace leetcode_1200;

class Program
{
    /// <summary>
    /// 1200. Minimum Absolute Difference
    /// https://leetcode.com/problems/minimum-absolute-difference/description/?envType=daily-question&envId=2026-01-26
    /// 1200. 最小絕對差
    /// https://leetcode.cn/problems/minimum-absolute-difference/description/?envType=daily-question&envId=2026-01-26
    /// 
    /// 繁體中文：
    /// 給定一個由互不相同的整數所組成的陣列 arr，找出所有具有最小絕對差的元素對。
    /// 回傳一個以對為單位、以升冪排序的清單。每個對 [a, b] 必須滿足：
    /// - a 與 b 來自 arr
    /// - a < b
    /// - b - a 等於陣列中任兩元素的最小絕對差
    ///
    /// English:
    /// Given an array of distinct integers arr, find all pairs of elements with the minimum absolute difference
    /// of any two elements. Return a list of pairs in ascending order (with respect to pairs), each pair [a, b]
    /// follows:
    /// - a, b are from arr
    /// - a < b
    /// - b - a equals to the minimum absolute difference of any two elements in arr
    /// </summary>
    /// <param name="args">Command-line arguments (unused).</param>
    static void Main(string[] args)
    {
        Program program = new();

        // 測試案例 1: arr = [4,2,1,3] => 輸出: [[1,2],[2,3],[3,4]]
        int[] arr1 = [4, 2, 1, 3];
        IList<IList<int>> result1 = program.MinimumAbsDifference(arr1);
        Console.WriteLine("測試案例 1:");
        Console.WriteLine($"輸入: [{string.Join(", ", arr1)}]");
        PrintResult(result1);

        // 測試案例 2: arr = [1,3,6,10,15] => 輸出: [[1,3]]
        int[] arr2 = [1, 3, 6, 10, 15];
        IList<IList<int>> result2 = program.MinimumAbsDifference(arr2);
        Console.WriteLine("\n測試案例 2:");
        Console.WriteLine($"輸入: [{string.Join(", ", arr2)}]");
        PrintResult(result2);

        // 測試案例 3: arr = [3,8,-10,23,19,-4,-14,27] => 輸出: [[-14,-10],[19,23],[23,27]]
        int[] arr3 = [3, 8, -10, 23, 19, -4, -14, 27];
        IList<IList<int>> result3 = program.MinimumAbsDifference(arr3);
        Console.WriteLine("\n測試案例 3:");
        Console.WriteLine($"輸入: [{string.Join(", ", arr3)}]");
        PrintResult(result3);
    }

    /// <summary>
    /// 輔助方法：列印結果清單。
    /// </summary>
    /// <param name="result">包含元素對的結果清單。</param>
    private static void PrintResult(IList<IList<int>> result)
    {
        Console.Write("輸出: [");
        for (int i = 0; i < result.Count; i++)
        {
            Console.Write($"[{result[i][0]},{result[i][1]}]");
            if (i < result.Count - 1)
            {
                Console.Write(",");
            }
        }
        Console.WriteLine("]");
    }

    /// <summary>
    /// 找出陣列中所有具有最小絕對差的元素對。
    /// 
    /// <para><b>解題思路：排序 + 一次遍歷</b></para>
    /// <para>
    /// 1. 首先對陣列進行升序排序。排序後，具有「最小絕對差」的元素對
    ///    只可能由相鄰的兩個元素構成（因為非相鄰元素的差一定更大）。
    /// </para>
    /// <para>
    /// 2. 遍歷排序後的陣列，計算每對相鄰元素的差值 δ = arr[i+1] - arr[i]：
    ///    - 若 δ 小於 當前最小差 minDiff，則更新 minDiff，清空結果並加入新的元素對
    ///    - 若 δ == minDiff，則直接將該元素對加入結果
    /// </para>
    /// 
    /// <para><b>時間複雜度：</b>O(n log n)，其中 n 為陣列長度（排序為主要開銷）。</para>
    /// <para><b>空間複雜度：</b>O(log n)，排序所需的堆疊空間。</para>
    /// </summary>
    /// <param name="arr">由互不相同整數組成的陣列。</param>
    /// <returns>所有具有最小絕對差的元素對清單，按升序排列。</returns>
    /// <example>
    /// <code>
    ///  範例 1:
    /// int[] arr1 = [4, 2, 1, 3];
    ///  排序後: [1, 2, 3, 4]
    ///  相鄰差值皆為 1，故輸出: [[1,2], [2,3], [3,4]]
    /// 
    ///  範例 2:
    /// int[] arr2 = [1, 3, 6, 10, 15];
    ///  排序後: [1, 3, 6, 10, 15]
    ///  相鄰差值: 2, 3, 4, 5，最小為 2，故輸出: [[1,3]]
    /// </code>
    /// </example>
    public IList<IList<int>> MinimumAbsDifference(int[] arr)
    {
        int n = arr.Length;

        // 步驟 1：升序排序，使得最小絕對差只會出現在相鄰元素之間
        Array.Sort(arr);

        // 記錄目前遇到的最小差值
        int minDiff = int.MaxValue;

        // 儲存所有具有最小絕對差的元素對
        IList<IList<int>> result = new List<IList<int>>();

        // 步驟 2：一次遍歷，檢查每對相鄰元素
        for (int i = 0; i < n - 1; i++)
        {
            // 計算相鄰元素的差值
            int delta = arr[i + 1] - arr[i];

            if (delta < minDiff)
            {
                // 發現更小的差值，更新 minDiff 並清空之前的結果
                minDiff = delta;
                result.Clear();

                // 建立新的元素對並加入結果
                IList<int> pair = new List<int>
                {
                    arr[i],
                    arr[i + 1]
                };
                result.Add(pair);
            }
            else if (delta == minDiff)
            {
                // 差值相同，直接將元素對加入結果
                IList<int> pair = new List<int>
                {
                    arr[i],
                    arr[i + 1]
                };
                result.Add(pair);
            }
        }

        return result;
    }
}
