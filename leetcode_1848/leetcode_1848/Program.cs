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
    /// 使用固定測試資料展示三種解法的輸出結果。
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
            int linearScanActual = solver.GetMinDistance(testCase.Nums, testCase.Target, testCase.Start);
            int expansionActual = solver.GetMinDistanceByExpandingFromStart(testCase.Nums, testCase.Target, testCase.Start);
            int binarySearchActual = solver.GetMinDistanceByBinarySearch(testCase.Nums, testCase.Target, testCase.Start);
            bool allPassed =
                linearScanActual == testCase.Expected &&
                expansionActual == testCase.Expected &&
                binarySearchActual == testCase.Expected;

            Console.WriteLine($"{testCase.Name}");
            Console.WriteLine($"nums = [{string.Join(", ", testCase.Nums)}], target = {testCase.Target}, start = {testCase.Start}");
            Console.WriteLine($"線性掃描 expected = {testCase.Expected}, actual = {linearScanActual}");
            Console.WriteLine($"雙向擴散 expected = {testCase.Expected}, actual = {expansionActual}");
            Console.WriteLine($"索引 + 二分搜尋 expected = {testCase.Expected}, actual = {binarySearchActual}");
            Console.WriteLine(allPassed ? "PASS" : "FAIL");
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
    /// 不需要排序或額外儲存結構，直接掃描整個陣列即可。
    /// 若只處理單次查詢，這也是最穩定且最直觀的做法。
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

    /// <summary>
    /// 使用以 start 為中心向左右兩側擴散的方式解題：先檢查距離 start 最近的位置，
    /// 再逐步把搜尋半徑往外擴大，第一個遇到的 target 就是最小距離答案。
    /// </summary>
    /// <param name="nums">輸入整數陣列。</param>
    /// <param name="target">要尋找的目標值。</param>
    /// <param name="start">計算距離的起始索引。</param>
    /// <returns>target 與 start 之間的最小絕對距離。</returns>
    /// <remarks>
    /// 這個做法把距離由小到大依序檢查，因此一旦命中 target 就可以立即回傳。
    /// 若答案剛好很靠近 start，通常會比完整掃描更早結束；但最壞情況仍然需要看完整個陣列。
    /// </remarks>
    /// <example>
    /// 當 nums = [5, 1, 2, 3, 5, 4, 5]、target = 5、start = 5 時，
    /// 先檢查索引 5，接著檢查距離為 1 的索引 4 與 6，於索引 4 找到 target，因此答案為 1。
    /// </example>
    public int GetMinDistanceByExpandingFromStart(int[] nums, int target, int start)
    {
        for (int distance = 0; distance < nums.Length; distance++)
        {
            int leftIndex = start - distance;

            // 每次先看左側，因為目前 distance 已經是尚未檢查過的最小距離。
            if (leftIndex >= 0 && nums[leftIndex] == target)
            {
                return distance;
            }

            int rightIndex = start + distance;

            // distance 為 0 時左右索引相同，避免重複檢查同一個位置。
            if (distance > 0 && rightIndex < nums.Length && nums[rightIndex] == target)
            {
                return distance;
            }
        }

        throw new InvalidOperationException($"{nameof(target)} must exist in {nameof(nums)}.");
    }

    /// <summary>
    /// 先建立每個數值出現位置的索引表，再只對 target 的索引列表做二分搜尋，
    /// 找出最接近 start 的候選位置。
    /// </summary>
    /// <param name="nums">輸入整數陣列。</param>
    /// <param name="target">要尋找的目標值。</param>
    /// <param name="start">計算距離的起始索引。</param>
    /// <returns>target 與 start 之間的最小絕對距離。</returns>
    /// <remarks>
    /// 這個做法的重點是把 target 所有出現位置保持為遞增索引列表，
    /// 然後利用二分搜尋只比較 start 左右最近的兩個候選位置。
    /// 若同一個 nums 需要被查詢很多次，將索引表預先建立並重複使用，效益會比單次查詢更明顯。
    /// </remarks>
    /// <example>
    /// 當 nums = [5, 1, 2, 3, 5, 4, 5]、target = 5、start = 5 時，
    /// target 的索引列表為 [0, 4, 6]，二分搜尋 start = 5 後只需比較索引 4 與 6，
    /// 兩者距離皆為 1，因此答案為 1。
    /// </example>
    public int GetMinDistanceByBinarySearch(int[] nums, int target, int start)
    {
        Dictionary<int, List<int>> indicesByValue = BuildIndicesByValue(nums);
        List<int> targetIndices = indicesByValue[target];
        int searchResult = targetIndices.BinarySearch(start);

        // start 若剛好命中 target 出現位置，最短距離立即是 0。
        if (searchResult >= 0)
        {
            return 0;
        }

        int insertionIndex = ~searchResult;
        int minDistance = int.MaxValue;

        // 比較插入點右邊最近的候選位置。
        if (insertionIndex < targetIndices.Count)
        {
            minDistance = Math.Min(minDistance, Math.Abs(targetIndices[insertionIndex] - start));
        }

        // 再比較插入點左邊最近的候選位置。
        if (insertionIndex > 0)
        {
            minDistance = Math.Min(minDistance, Math.Abs(targetIndices[insertionIndex - 1] - start));
        }

        return minDistance;
    }

    /// <summary>
    /// 建立每個數值對應的遞增索引列表，提供給二分搜尋版本使用。
    /// </summary>
    /// <param name="nums">輸入整數陣列。</param>
    /// <returns>以數值為鍵、出現索引列表為值的對照表。</returns>
    private static Dictionary<int, List<int>> BuildIndicesByValue(int[] nums)
    {
        Dictionary<int, List<int>> indicesByValue = [];

        for (int index = 0; index < nums.Length; index++)
        {
            if (!indicesByValue.TryGetValue(nums[index], out List<int>? indices))
            {
                indices = [];
                indicesByValue[nums[index]] = indices;
            }

            // 由左到右加入索引，可自然維持遞增順序，讓後續能直接做二分搜尋。
            indices.Add(index);
        }

        return indicesByValue;
    }
}
