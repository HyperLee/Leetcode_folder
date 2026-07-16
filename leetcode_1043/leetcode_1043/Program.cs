namespace leetcode_1043;

class Program
{
    /// <summary>
    /// 1043. Partition Array for Maximum Sum
    /// https://leetcode.com/problems/partition-array-for-maximum-sum/description/
    /// 1043. 分隔数组以得到最大和
    /// https://leetcode.cn/problems/partition-array-for-maximum-sum/description/
    ///
    /// English:
    /// Given an integer array arr, partition the array into (contiguous) subarrays of length at most k.
    /// After partitioning, each subarray has their values changed to become the maximum value of that subarray.
    ///
    /// Return the largest sum of the given array after partitioning. Test cases are generated so that the answer
    /// fits in a 32-bit integer.
    ///
    /// 繁體中文：
    /// 給定一個整數陣列 arr，請將此陣列分割成長度最多為 k 的（連續）子陣列。
    /// 分割完成後，每個子陣列中的所有值都會變更為該子陣列的最大值。
    ///
    /// 請回傳分割後陣列所能得到的最大總和。測試案例會確保答案可用 32 位元整數表示。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        RunTestCases();
    }

    /// <summary>
    /// 執行固定且符合題目限制的測試案例，分別呼叫 Bottom-up 與 Top-down 解法，
    /// 並將每個實際結果與預期最大總和比較後輸出 PASS 或 FAIL。
    /// 輸入資料包含官方範例、k = 1 與 k 等於陣列長度；輸出為各案例結果及總通過數。
    /// </summary>
    private static void RunTestCases()
    {
        (string Name, int[] Arr, int K, int Expected)[] testCases =
        [
            ("Official example 1", [1, 15, 7, 9, 2, 5, 10], 3, 84),
            ("Official example 2", [1, 4, 1, 5, 7, 3, 6, 1, 9, 9, 3], 4, 83),
            ("Official example 3", [1], 1, 1),
            ("k equals 1", [1, 2, 3], 1, 6),
            ("k equals array length", [1, 2, 3], 3, 9)
        ];

        Program solution = new();
        int passedChecks = 0;
        int totalChecks = testCases.Length * 2;

        Console.WriteLine("LeetCode 1043 - Partition Array for Maximum Sum");
        Console.WriteLine(new string('=', 49));

        for (int index = 0; index < testCases.Length; index++)
        {
            (string name, int[] arr, int k, int expected) = testCases[index];
            int bottomUpResult = solution.MaxSumAfterPartitioning(arr, k);
            int topDownResult = solution.MaxSumAfterPartitioningTopDown(arr, k);
            bool bottomUpPassed = bottomUpResult == expected;
            bool topDownPassed = topDownResult == expected;

            passedChecks += bottomUpPassed ? 1 : 0;
            passedChecks += topDownPassed ? 1 : 0;

            Console.WriteLine();
            Console.WriteLine($"Case {index + 1}: {name}");
            Console.WriteLine($"Input: arr = [{string.Join(", ", arr)}], k = {k}");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"Bottom-up DP: {bottomUpResult} ({(bottomUpPassed ? "PASS" : "FAIL")})");
            Console.WriteLine($"Top-down DP: {topDownResult} ({(topDownPassed ? "PASS" : "FAIL")})");
        }

        Console.WriteLine();
        Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");
    }

    /// <summary>
    /// 使用 Bottom-up 動態規劃計算分割後的最大總和。
    /// <c>dp[i]</c> 表示前 <c>i</c> 個元素可取得的最佳結果；對每個前綴枚舉最後一段的長度，
    /// 再以「前一個最佳結果＋最後一段最大值乘以段長」更新答案。
    /// 輸入需符合題目限制：陣列長度為 1 到 500、元素為 0 到 10^9，且 <paramref name="k"/> 介於 1 與陣列長度之間。
    /// </summary>
    /// <param name="arr">要分割的非負整數陣列。</param>
    /// <param name="k">每個連續子陣列允許的最大長度。</param>
    /// <returns>所有合法分割方式中，替換各段元素後可得到的最大總和。</returns>
    public int MaxSumAfterPartitioning(int[] arr, int k)
    {
        int[] dp = new int[arr.Length + 1];

        for (int prefixLength = 1; prefixLength <= arr.Length; prefixLength++)
        {
            int maxValue = 0;
            int maxPartitionLength = Math.Min(k, prefixLength);

            // 枚舉最後一段的長度，同時維護該段最大值，避免重複掃描子陣列。
            for (int partitionLength = 1; partitionLength <= maxPartitionLength; partitionLength++)
            {
                maxValue = Math.Max(maxValue, arr[prefixLength - partitionLength]);
                int candidate = dp[prefixLength - partitionLength] + maxValue * partitionLength;
                dp[prefixLength] = Math.Max(dp[prefixLength], candidate);
            }
        }

        return dp[arr.Length];
    }

    /// <summary>
    /// 使用 Top-down 動態規劃與記憶化遞迴計算分割後的最大總和。
    /// 每個狀態代表從指定索引開始的最佳結果；尚未計算的狀態以 -1 標記，計算後存入 memo 避免重複展開。
    /// 輸入需符合題目限制：陣列長度為 1 到 500、元素為 0 到 10^9，且 <paramref name="k"/> 介於 1 與陣列長度之間。
    /// </summary>
    /// <param name="arr">要分割的非負整數陣列。</param>
    /// <param name="k">每個連續子陣列允許的最大長度。</param>
    /// <returns>所有合法分割方式中，替換各段元素後可得到的最大總和。</returns>
    public int MaxSumAfterPartitioningTopDown(int[] arr, int k)
    {
        int[] memo = new int[arr.Length];
        Array.Fill(memo, -1);

        return CalculateMaxSumFromIndex(arr, k, 0, memo);
    }

    /// <summary>
    /// 計算從 <paramref name="startIndex"/> 到陣列結尾可取得的最大總和。
    /// 此方法枚舉下一段的所有合法終點、維護該段最大值，並將每個起點的結果存入 <paramref name="memo"/>。
    /// 起點可等於陣列長度，代表已完成所有分割並回傳 0；其餘輸入沿用公開方法的題目限制。
    /// </summary>
    /// <param name="arr">要分割的非負整數陣列。</param>
    /// <param name="k">每個連續子陣列允許的最大長度。</param>
    /// <param name="startIndex">目前尚未處理區間的起始索引。</param>
    /// <param name="memo">依起始索引保存最佳結果的記憶化陣列，-1 表示尚未計算。</param>
    /// <returns>從指定起點到陣列結尾的最大總和。</returns>
    private int CalculateMaxSumFromIndex(int[] arr, int k, int startIndex, int[] memo)
    {
        if (startIndex == arr.Length)
        {
            return 0;
        }

        if (memo[startIndex] != -1)
        {
            // 相同起點的最佳結果已求得，直接重用以避免重複遞迴。
            return memo[startIndex];
        }

        int bestSum = 0;
        int maxValue = 0;
        int lastIndex = Math.Min(arr.Length, startIndex + k);

        for (int endIndex = startIndex; endIndex < lastIndex; endIndex++)
        {
            maxValue = Math.Max(maxValue, arr[endIndex]);
            int partitionLength = endIndex - startIndex + 1;
            int candidate = maxValue * partitionLength
                + CalculateMaxSumFromIndex(arr, k, endIndex + 1, memo);
            bestSum = Math.Max(bestSum, candidate);
        }

        memo[startIndex] = bestSum;
        return bestSum;
    }
}
