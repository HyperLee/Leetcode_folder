namespace leetcode_2515;

class Program
{
    /// <summary>
    /// 2515. Shortest Distance to Target String in a Circular Array
    ///
    /// English:
    /// You are given a 0-indexed circular string array words and a string target. A circular array means that the array's end connects to the array's beginning.
    /// Formally, the next element of words[i] is words[(i + 1) % n] and the previous element of words[i] is words[(i - 1 + n) % n], where n is the length of words.
    /// Starting from startIndex, you can move to either the next word or the previous word with 1 step at a time.
    /// Return the shortest distance needed to reach the string target. If the string target does not exist in words, return -1.
    ///
    /// 繁體中文：
    /// 給定一個 0-indexed 的環狀字串陣列 words 與一個字串 target。環狀陣列表示陣列的結尾會連接回陣列的開頭。
    /// 更正式地說，words[i] 的下一個元素是 words[(i + 1) % n]，前一個元素是 words[(i - 1 + n) % n]，其中 n 為 words 的長度。
    /// 從 startIndex 開始，你每一步都可以移動到下一個單字或前一個單字。
    /// 回傳到達字串 target 所需的最短距離。如果 words 中不存在 target，則回傳 -1。
    /// </summary>
    /// <param name="args">命令列參數。</param>
    static void Main(string[] args)
    {
        Program solver = new();

        var testCases = new (string[] Words, string Target, int StartIndex, int Expected)[]
        {
            (["hello", "i", "am", "leetcode", "hello"], "hello", 1, 1),
            (["a", "b", "leetcode"], "leetcode", 0, 1),
            (["x", "y", "z"], "hello", 1, -1)
        };

        Console.WriteLine("LeetCode 2515 - Shortest Distance to Target String in a Circular Array");

        for (int index = 0; index < testCases.Length; index++)
        {
            var testCase = testCases[index];
            int actual = solver.ClosestTarget(testCase.Words, testCase.Target, testCase.StartIndex);

            Console.WriteLine(
                $"Case {index + 1}: words = [{string.Join(", ", testCase.Words)}], target = {testCase.Target}, startIndex = {testCase.StartIndex}, expected = {testCase.Expected}, actual = {actual}");
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
}
