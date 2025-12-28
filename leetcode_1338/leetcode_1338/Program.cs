namespace leetcode_1338;

class Program
{
    /// <summary>
    /// 1338. Reduce Array Size to The Half
    /// https://leetcode.com/problems/reduce-array-size-to-the-half/description/
    /// 1338. 數組大小減半
    /// https://leetcode.cn/problems/reduce-array-size-to-the-half/description/
    /// 
    /// 題目描述：給定一個整數陣列 `arr`。你可以選擇一個整數集合，並從陣列中移除該集合中所有整數的所有出現。
    /// 返回該集合的最小大小，使得陣列中至少一半的整數被移除。
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試範例 1: arr = [3,3,3,3,5,5,5,2,2,7]
        // 預期輸出: 2 (移除 3 和 5 的所有出現，共 7 個元素 >= 10/2 = 5)
        int[] arr1 = [3, 3, 3, 3, 5, 5, 5, 2, 2, 7];
        Console.WriteLine($"測試 1: arr = [{string.Join(", ", arr1)}]");
        Console.WriteLine($"結果: {solution.MinSetSize(arr1)}, 預期: 2");
        Console.WriteLine();

        // 測試範例 2: arr = [7,7,7,7,7,7]
        // 預期輸出: 1 (只需移除 7，共 6 個元素 >= 6/2 = 3)
        int[] arr2 = [7, 7, 7, 7, 7, 7];
        Console.WriteLine($"測試 2: arr = [{string.Join(", ", arr2)}]");
        Console.WriteLine($"結果: {solution.MinSetSize(arr2)}, 預期: 1");
        Console.WriteLine();

        // 測試範例 3: arr = [1,2,3,4,5,6,7,8,9,10]
        // 預期輸出: 5 (每個元素出現 1 次，需要移除 5 個不同的元素)
        int[] arr3 = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        Console.WriteLine($"測試 3: arr = [{string.Join(", ", arr3)}]");
        Console.WriteLine($"結果: {solution.MinSetSize(arr3)}, 預期: 5");
    }

    /// <summary>
    /// 解決 LeetCode 1338: 數組大小減半問題
    /// 
    /// <para>
    /// <b>解題思路：貪婪演算法 (Greedy Algorithm)</b>
    /// </para>
    /// 
    /// <para>
    /// 核心概念：要使用最少的整數集合來移除至少一半的陣列元素，
    /// 我們應該優先選擇出現次數最多的元素，這樣每選一個整數就能移除最多的元素。
    /// </para>
    /// 
    /// <para>
    /// <b>演算法步驟：</b>
    /// <list type="number">
    ///   <item>統計每個元素的出現次數（使用 Dictionary）</item>
    ///   <item>將出現次數由大到小排序</item>
    ///   <item>從出現次數最多的元素開始，依序累加移除的元素數量</item>
    ///   <item>當累計移除數量達到陣列長度的一半時，返回已選擇的整數個數</item>
    /// </list>
    /// </para>
    /// 
    /// <para>
    /// <b>時間複雜度：</b> O(n log n)，其中 n 為陣列長度，排序為主要開銷
    /// </para>
    /// <para>
    /// <b>空間複雜度：</b> O(n)，用於儲存頻率計數的 Dictionary
    /// </para>
    /// </summary>
    /// <param name="arr">輸入的整數陣列</param>
    /// <returns>最小的整數集合大小，使得移除該集合中所有整數後，陣列至少減少一半</returns>
    /// <example>
    /// <code>
    /// var solution = new Program();
    /// int[] arr = [3, 3, 3, 3, 5, 5, 5, 2, 2, 7];
    /// int result = solution.MinSetSize(arr); // 返回 2
    ///  選擇 {3, 5}，移除 4 + 3 = 7 個元素，7 >= 10/2 = 5
    /// </code>
    /// </example>
    public int MinSetSize(int[] arr)
    {
        // 步驟 1: 建立頻率計數表
        // 使用 Dictionary 統計每個元素出現的次數
        Dictionary<int, int> countMap = new Dictionary<int, int>();

        for (int i = 0; i < arr.Length; i++)
        {
            // 如果元素已存在於字典中，次數 +1；否則初始化為 1
            if (countMap.ContainsKey(arr[i]))
            {
                countMap[arr[i]]++;
            }
            else
            {
                countMap[arr[i]] = 1;
            }
        }

        // 步驟 2: 按出現次數由大到小排序
        // 貪婪策略：優先選擇出現次數最多的元素，可以用最少的選擇移除最多的元素
        var countMapOrdered = countMap.OrderByDescending(kv => kv.Value);

        // 步驟 3: 貪婪選擇，累計移除元素直到達到目標
        int removedCount = 0;  // 已移除的元素總數
        int setSize = 0;       // 已選擇的整數集合大小
        int targetCount = arr.Length / 2;  // 目標：至少移除陣列一半的元素

        foreach (var kv in countMapOrdered)
        {
            removedCount += kv.Value;  // 累加當前元素的出現次數
            setSize++;                  // 集合大小 +1

            // 當移除的元素數量達到或超過目標時，即可停止
            if (removedCount >= targetCount)
            {
                break;
            }
        }

        return setSize;
    }
}
