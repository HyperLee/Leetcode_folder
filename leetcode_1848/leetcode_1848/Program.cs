namespace leetcode_1848;

/// <summary>
/// 1848. Minimum Distance to the Target Element
/// https://leetcode.com/problems/minimum-distance-to-the-target-element/description/?envType=daily-question&envId=2026-04-13
/// 給定整數陣列 nums、目標值 target 與起點 start，找出一個滿足 nums[i] == target 的索引 i，
/// 並讓 abs(i - start) 最小，最後回傳這個最小距離。
/// </summary>
internal class Program
{
    /// <summary>
    /// 使用固定測試資料展示 GetMinDistance 的輸出結果。
    /// </summary>
    /// <param name="args">命令列參數，目前未使用。</param>
    private static void Main(string[] args)
    {
        Program solver = new();

        // 準備題目範例與額外案例，方便直接從主控台驗證演算法結果。
        var testCases = new[]
        {
            (Name: "範例 1", Nums: new[] { 1, 2, 3, 4, 5 }, Target: 5, Start: 3, Expected: 1),
            (Name: "範例 2", Nums: new[] { 1 }, Target: 1, Start: 0, Expected: 0),
            (Name: "自訂案例", Nums: new[] { 5, 1, 2, 3, 5, 4, 5 }, Target: 5, Start: 5, Expected: 1),
        };

        foreach (var testCase in testCases)
        {
            int actual = solver.GetMinDistance(testCase.Nums, testCase.Target, testCase.Start);

            Console.WriteLine($"{testCase.Name}");
            Console.WriteLine($"nums = [{string.Join(", ", testCase.Nums)}], target = {testCase.Target}, start = {testCase.Start}");
            Console.WriteLine($"expected = {testCase.Expected}, actual = {actual}");
            Console.WriteLine(actual == testCase.Expected ? "PASS" : "FAIL");
            Console.WriteLine(new string('-', 50));
        }
    }

    /// <summary>
    /// 使用線性掃描解題：逐一檢查陣列中的每個位置，遇到 target 時就計算它與 start 的絕對距離，
    /// 並持續更新目前最小值。由於題目保證 target 一定存在，因此完整掃描一次後得到的最小距離就是答案。
    /// </summary>
    /// <param name="nums">輸入整數陣列。</param>
    /// <param name="target">要尋找的目標值。</param>
    /// <param name="start">計算距離的起始索引。</param>
    /// <returns>target 與 start 之間的最小絕對距離。</returns>
    /// <remarks>
    /// 解題出發點在於：答案只取決於所有 target 出現位置與 start 的距離最小值，
    /// 不需要排序、額外儲存結構或雙向搜尋，直接掃描整個陣列即可。
    /// </remarks>
    /// <example>
    /// 當 nums = [1, 2, 3, 4, 5]、target = 5、start = 3 時，
    /// 唯一符合條件的位置是索引 4，因此答案為 abs(4 - 3) = 1。
    /// </example>
    public int GetMinDistance(int[] nums, int target, int start)
    {
        int minDistance = int.MaxValue;

        for (int index = 0; index < nums.Length; index++)
        {
            // 只有遇到 target 時，這個位置才可能成為答案候選。
            if (nums[index] != target)
            {
                continue;
            }

            // 比較目前答案與新距離，保留較小值。
            minDistance = Math.Min(minDistance, Math.Abs(index - start));
        }

        return minDistance;
    }
}
