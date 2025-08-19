using System;

namespace leetcode_2348;

/// <summary>
/// 2348. Number of Zero-Filled Subarrays
/// https://leetcode.com/problems/number-of-zero-filled-subarrays/description/?envType=daily-question&envId=2025-08-19
/// 2348. 全0 子數組的數目
///
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Zero-Filled Subarray 測試範例：");

        var program = new Program();

        var tests = new int[][]
        {
            new int[] { 1, 0, 0, 0, 2, 0, 0 }, // 範例 -> 6
            new int[] { },                      // 空陣列 -> 0
            new int[] { 0, 0, 0 },              // 全0 -> 6
            new int[] { 1, 2, 3 }               // 無0 -> 0
        };

        for (int i = 0; i < tests.Length; i++)
        {
            var input = tests[i];
            var repr = input.Length == 0 ? "" : string.Join(",", input);
            var ans = program.ZeroFilledSubarray(input);
            Console.WriteLine($"Test {i + 1}: [{repr}] => {ans}");
        }

        // 示範 null 輸入會拋出 ArgumentNullException
        try
        {
            program.ZeroFilledSubarray(null!);
        }
        catch (ArgumentNullException)
        {
            Console.WriteLine("Test null: 已觸發 ArgumentNullException（預期行為）");
        }
    }

    /// <summary>
    /// 計算陣列中所有只包含 0 的子陣列（連續且非空）的總數。
    ///
    /// 解題說明:
    /// - 採用單次掃描的貪婪策略，維護當前連續 0 的長度 count。
    /// - 當遇到 0 時，count 增加 1；此時以該 位置為結尾的全0 子陣列數量剛好等於 count，將其加到總和 res。
    ///   例：對於連續 k 個 0，其貢獻為 1 + 2 + ... + k = k*(k+1)/2，但在驅動過程中累加 count 可即時得到相同結果。
    /// - 當遇到非0 時，將 count 重置為 0。
    ///
    /// 複雜度：時間 O(n)，額外記憶體 O(1)
    /// 邊界：若輸入為 null，會拋出 ArgumentNullException；若陣列為空則回傳 0。
    /// 
    /// 每個 count 都是一個 subarrays 
    /// 利用 res 把全部 count 都加總起來 就是題目所需
    /// 每多一個連續相鄰的 0 都可以跟上一個 subarray 組合成新的 subarray
    ///
    /// 連續 0   subarrays 個數
    /// 1   -   1
    /// 2   -   3   (pre + cur  = 1 + 2)
    /// 3   -   6   (pre + cur  = 3 + 3)
    /// 4   -   10  (pre + cur  = 6 + 4)
    /// 5   -   15  (pre + cur  = 10 + 5)
    /// 6   -   21  (pre + cur  = 15 + 6)
    /// 7   -   28  (pre + cur  = 21 + 7)
    /// 
    /// ref:
    /// https://leetcode.cn/problems/number-of-zero-filled-subarrays/solutions/1693068/java-by-kayleh-s3xx/
    /// https://leetcode.cn/problems/number-of-zero-filled-subarrays/solutions/1696737/c-by-zardily-zy84/
    /// </summary>
    /// <param name="nums">輸入整數陣列；不可為 null。</param>
    /// <returns>所有全0 子陣列的總數（long，避免溢位）。</returns>
    public long ZeroFilledSubarray(int[] nums)
    {
        if (nums is null)
        {
            throw new ArgumentNullException(nameof(nums));
        }

        long res = 0; // 最終答案：全 0 子陣列總數
        long count = 0; // 當前連續 0 的長度

        // 單次掃描陣列
        foreach (var num in nums)
        {
            if (num == 0)
            {
                // 每多一個 0，就會新增 count 個以該位置為結尾的全 0 子陣列
                count++;
                res += count;
            }
            else
            {
                // 遇到非0，重置連續 0 的計數
                count = 0;
            }
        }

        return res;
    }
}
