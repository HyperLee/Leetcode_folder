namespace leetcode_2515;

class Program
{
    /// <summary>
    /// <para>
    /// 2515. Shortest Distance to Target String in a Circular Array
    /// https://leetcode.com/problems/shortest-distance-to-target-string-in-a-circular-array/description/
    ///
    /// You are given a 0-indexed circular string array words and string target. The array's end connects to its beginning: the next element after words[i] is words[(i + 1) % n], and the previous element is words[(i - 1 + n) % n], where n is words.length. Starting at startIndex, each step moves to the next or previous word. Return the shortest distance to target, or -1 if target does not occur in words.
    ///
    /// Example 1:
    /// Input: words = ["hello","i","am","leetcode","hello"], target = "hello", startIndex = 1
    /// Output: 1
    /// Explanation: From index 1, index 4 can be reached by moving 3 steps right or 2 left, while index 0 can be reached by moving 4 right or 1 left. The shortest distance is 1.
    ///
    /// Example 2:
    /// Input: words = ["a","b","leetcode"], target = "leetcode", startIndex = 0
    /// Output: 1
    /// Explanation: From index 0, index 2 is reached by moving 2 steps right or 1 left. The shortest distance is 1.
    ///
    /// Example 3:
    /// Input: words = ["i","eat","leetcode"], target = "ate", startIndex = 0
    /// Output: -1
    /// Explanation: "ate" does not occur in words, so return -1.
    ///
    /// Constraints:
    /// - 1 &lt;= words.length &lt;= 100
    /// - 1 &lt;= words[i].length &lt;= 100
    /// - words[i] and target consist only of lowercase English letters.
    /// - 0 &lt;= startIndex &lt; words.length
    /// </para>
    /// <para>
    /// 2515. 到目標字串的最短距離
    /// https://leetcode.cn/problems/shortest-distance-to-target-string-in-a-circular-array/description/
    ///
    /// 給定 0 索引環狀字串陣列 words 與字串 target。陣列尾端與開頭相連：words[i] 的下一個元素是 words[(i + 1) % n]，前一個元素是 words[(i - 1 + n) % n]，其中 n 為 words.length。從 startIndex 開始，每一步可移到下一個或前一個單字。回傳到達 target 的最短距離；若 words 中沒有 target，則回傳 -1。
    ///
    /// 範例 1：
    /// 輸入：words = ["hello","i","am","leetcode","hello"], target = "hello", startIndex = 1
    /// 輸出：1
    /// 說明：從索引 1 出發，可向右 3 步或向左 2 步到達索引 4，也可向右 4 步或向左 1 步到達索引 0。最短距離為 1。
    ///
    /// 範例 2：
    /// 輸入：words = ["a","b","leetcode"], target = "leetcode", startIndex = 0
    /// 輸出：1
    /// 說明：從索引 0 出發，可向右 2 步或向左 1 步到達索引 2。最短距離為 1。
    ///
    /// 範例 3：
    /// 輸入：words = ["i","eat","leetcode"], target = "ate", startIndex = 0
    /// 輸出：-1
    /// 說明：words 中沒有 "ate"，因此回傳 -1。
    ///
    /// 限制條件：
    /// - 1 &lt;= words.length &lt;= 100
    /// - 1 &lt;= words[i].length &lt;= 100
    /// - words[i] 與 target 僅由小寫英文字母組成。
    /// - 0 &lt;= startIndex &lt; words.length
    /// </para>
    /// </summary>
    /// <param name="args">命令列參數。</param>
    static void Main(string[] args)
    {
        Program solver = new();

        var testCases = new (string[] Words, string Target, int StartIndex, int Expected)[]
        {
            (["hello", "i", "am", "leetcode", "hello"], "hello", 1, 1),
            (["a", "b", "leetcode"], "leetcode", 0, 1),
            (["start", "middle", "target", "tail"], "start", 0, 0),
            (["a", "target", "c", "d", "e"], "target", 4, 2),
            (["x", "y", "z"], "hello", 1, -1)
        };

        Console.WriteLine("LeetCode 2515 - Shortest Distance to Target String in a Circular Array");
        Console.WriteLine("Compare formula scan and expanding-from-start solutions:");

        for (int index = 0; index < testCases.Length; index++)
        {
            var testCase = testCases[index];
            int actualByFormula = solver.ClosestTarget(testCase.Words, testCase.Target, testCase.StartIndex);
            int actualByExpanding = solver.ClosestTargetByExpandingFromStartIndex(testCase.Words, testCase.Target, testCase.StartIndex);
            bool passed = actualByFormula == testCase.Expected && actualByExpanding == testCase.Expected;

            Console.WriteLine(
                $"Case {index + 1}: words = [{string.Join(", ", testCase.Words)}], target = {testCase.Target}, startIndex = {testCase.StartIndex}, expected = {testCase.Expected}, formula = {actualByFormula}, expand = {actualByExpanding}, pass = {passed}");
        }
    }

    /// <summary>
    /// 2515. Shortest Distance to Target String in a Circular Array。
    /// 這個解法從左到右遍歷整個陣列，當找到 target 時，同時計算不跨越首尾的直接距離與跨越首尾的環狀距離，並持續更新最小答案。
    /// 核心觀察是從 startIndex 到索引 i 的最短步數，一定等於 min(|i - startIndex|, n - |i - startIndex|)。
    /// 因此只需要一次線性掃描，就能找出所有 target 位置中的最短距離。
    /// </summary>
    /// <param name="words">環狀字串陣列。</param>
    /// <param name="target">要搜尋的目標字串。</param>
    /// <param name="startIndex">起始索引。</param>
    /// <returns>從 startIndex 走到任一 target 的最短距離；若不存在則回傳 -1。</returns>
    /// <example>
    /// <code>
    /// int distance = new Program().ClosestTarget(["hello", "i", "am", "leetcode", "hello"], "hello", 1);
    /// Console.WriteLine(distance); // 1
    /// </code>
    /// </example>
    public int ClosestTarget(string[] words, string target, int startIndex)
    {
        int n = words.Length;
        int minDistance = int.MaxValue;

        for (int i = 0; i < n; i++)
        {
            if (words[i] != target)
            {
                continue;
            }

            // 不跨越陣列首尾時，直接走到目標位置的距離。
            int directDistance = Math.Abs(i - startIndex);

            // 環狀陣列可以反方向繞一圈，因此也要比較跨越首尾的距離。
            int wrappedDistance = n - directDistance;

            minDistance = Math.Min(minDistance, Math.Min(directDistance, wrappedDistance));
        }

        return minDistance == int.MaxValue ? -1 : minDistance;
    }

    /// <summary>
    /// 2515. Shortest Distance to Target String in a Circular Array。
    /// 這個解法直接從 startIndex 出發，依照距離 0、1、2... 逐層向左右兩側擴散。
    /// 對於每一個候選距離，都檢查 startIndex 往左與往右走相同步數後的位置是否命中 target。
    /// 因為我們是依照可能答案由小到大枚舉，所以第一次找到 target 的距離一定就是最短距離。
    /// 配合環狀陣列的特性，任何超出範圍的索引都可以用模運算轉回 [0, n - 1] 的合法範圍。
    /// </summary>
    /// <param name="words">環狀字串陣列。</param>
    /// <param name="target">要搜尋的目標字串。</param>
    /// <param name="startIndex">起始索引。</param>
    /// <returns>從 startIndex 往左右兩側擴散時，遇到 target 的最短距離；若不存在則回傳 -1。</returns>
    /// <example>
    /// <code>
    /// int distance = new Program().ClosestTargetByExpandingFromStartIndex(["a", "b", "leetcode"], "leetcode", 0);
    /// Console.WriteLine(distance); // 1
    /// </code>
    /// </example>
    public int ClosestTargetByExpandingFromStartIndex(string[] words, string target, int startIndex)
    {
        int n = words.Length;
        int maxDistance = n / 2;

        for (int distance = 0; distance <= maxDistance; distance++)
        {
            // 左移可能先算出負值，因此先加上 n，再用 % n 折回合法索引。
            int leftIndex = (startIndex - distance + n) % n;

            // 右移只會超過上界，不會變成負值，直接 % n 即可折回合法索引。
            int rightIndex = (startIndex + distance) % n;

            // 由於距離是從小到大枚舉，第一次命中就是最短距離。
            if (words[leftIndex] == target || words[rightIndex] == target)
            {
                return distance;
            }
        }

        return -1;
    }
}
