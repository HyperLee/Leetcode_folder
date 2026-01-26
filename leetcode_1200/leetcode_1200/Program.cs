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

    /// <summary>
    /// 解法二：計數排序 (Counting Sort) — 適用於數值範圍有限的情況。
    /// 
    /// <para><b>解題思路：</b></para>
    /// <para>
    /// 1. 找出陣列的最小值與最大值，建立一個布林陣列標記哪些數字存在。
    /// 2. 遍歷計數陣列，找出相鄰存在的數字，計算其差值。
    /// 3. 與排序解法類似，維護最小差值並收集結果。
    /// </para>
    /// 
    /// <para><b>時間複雜度：</b>O(n + k)，其中 n 為陣列長度，k 為數值範圍 (max - min)。</para>
    /// <para><b>空間複雜度：</b>O(k)，計數陣列所需空間。</para>
    /// <para><b>適用場景：</b>當數值範圍 k 較小時（如 k ≤ 2×10^6），效率優於排序解法。</para>
    /// </summary>
    /// <param name="arr">由互不相同整數組成的陣列。</param>
    /// <returns>所有具有最小絕對差的元素對清單，按升序排列。</returns>
    public IList<IList<int>> MinimumAbsDifference_CountingSort(int[] arr)
    {
        // 步驟 1：找出最小值和最大值
        int min = arr.Min();
        int max = arr.Max();
        int range = max - min + 1;

        // 步驟 2：建立計數陣列（用 bool 即可，因為元素互不相同）
        bool[] exists = new bool[range];
        foreach (int num in arr)
        {
            exists[num - min] = true;
        }

        IList<IList<int>> result = new List<IList<int>>();
        int minDiff = int.MaxValue;
        int prev = -1;

        // 步驟 3：遍歷計數陣列找出相鄰存在的數字
        for (int i = 0; i < range; i++)
        {
            if (!exists[i])
            {
                continue;
            }

            if (prev != -1)
            {
                int delta = i - prev;

                if (delta < minDiff)
                {
                    minDiff = delta;
                    result.Clear();
                    result.Add(new List<int> { prev + min, i + min });
                }
                else if (delta == minDiff)
                {
                    result.Add(new List<int> { prev + min, i + min });
                }
            }

            prev = i;
        }

        return result;
    }

    /// <summary>
    /// 解法三：兩次遍歷優化版本 — 程式碼更清晰簡潔。
    /// 
    /// <para><b>解題思路：</b></para>
    /// <para>
    /// 1. 第一次遍歷：排序後只找最小差值。
    /// 2. 第二次遍歷：收集所有等於最小差值的元素對。
    /// </para>
    /// 
    /// <para><b>時間複雜度：</b>O(n log n)，排序為主要開銷。</para>
    /// <para><b>空間複雜度：</b>O(log n)，排序所需的堆疊空間。</para>
    /// <para><b>優點：</b>程式碼更簡潔，避免 Clear() 操作。</para>
    /// </summary>
    /// <param name="arr">由互不相同整數組成的陣列。</param>
    /// <returns>所有具有最小絕對差的元素對清單，按升序排列。</returns>
    public IList<IList<int>> MinimumAbsDifference_TwoPass(int[] arr)
    {
        Array.Sort(arr);

        // 第一次遍歷：只找最小差值
        int minDiff = int.MaxValue;
        for (int i = 0; i < arr.Length - 1; i++)
        {
            minDiff = Math.Min(minDiff, arr[i + 1] - arr[i]);
        }

        // 第二次遍歷：收集所有等於最小差值的元素對
        IList<IList<int>> result = new List<IList<int>>();
        for (int i = 0; i < arr.Length - 1; i++)
        {
            if (arr[i + 1] - arr[i] == minDiff)
            {
                result.Add(new List<int> { arr[i], arr[i + 1] });
            }
        }

        return result;
    }

    /// <summary>
    /// 解法四：陣列優化版本 — 預先分配記憶體，減少動態分配。
    /// 
    /// <para><b>解題思路：</b></para>
    /// <para>
    /// 1. 第一次遍歷：找最小差值並計算符合條件的元素對數量。
    /// 2. 預先分配正確大小的結果清單，避免動態擴容。
    /// 3. 第二次遍歷：收集結果。
    /// </para>
    /// 
    /// <para><b>時間複雜度：</b>O(n log n)，排序為主要開銷。</para>
    /// <para><b>空間複雜度：</b>O(log n)，排序所需的堆疊空間。</para>
    /// <para><b>優點：</b>減少記憶體分配次數，使用陣列取代 List 作為元素對。</para>
    /// </summary>
    /// <param name="arr">由互不相同整數組成的陣列。</param>
    /// <returns>所有具有最小絕對差的元素對清單，按升序排列。</returns>
    public IList<IList<int>> MinimumAbsDifference_ArrayOptimized(int[] arr)
    {
        Array.Sort(arr);

        int minDiff = int.MaxValue;
        int count = 0;

        // 第一次遍歷：找最小差值並計數
        for (int i = 0; i < arr.Length - 1; i++)
        {
            int delta = arr[i + 1] - arr[i];

            if (delta < minDiff)
            {
                minDiff = delta;
                count = 1;
            }
            else if (delta == minDiff)
            {
                count++;
            }
        }

        // 預先分配正確大小的結果
        IList<IList<int>> result = new List<IList<int>>(count);
        for (int i = 0; i < arr.Length - 1; i++)
        {
            if (arr[i + 1] - arr[i] == minDiff)
            {
                result.Add(new int[] { arr[i], arr[i + 1] });
            }
        }

        return result;
    }
}
