namespace leetcode_045;

class Program
{
    /// <summary>
    /// 45. Jump Game II
    /// https://leetcode.com/problems/jump-game-ii/description/
    ///
    /// You are given a 0-indexed array of integers nums of length n. You are initially positioned at index 0.
    ///
    /// Each element nums[i] represents the maximum length of a forward jump from index i. In other words,
    /// if you are at index i, you can jump to any index (i + j) where:
    ///
    /// 0 <= j <= nums[i] and
    /// i + j < n
    ///
    /// Return the minimum number of jumps to reach index n - 1. The test cases are generated such that
    /// you can reach index n - 1.
    ///
    /// 45. 跳躍遊戲 II
    /// https://leetcode.cn/problems/jump-game-ii/description/
    ///
    /// 給定一個長度為 n 的 0 索引整數陣列 nums。你一開始位於索引 0。
    ///
    /// 每個元素 nums[i] 代表從索引 i 向前跳躍的最大長度。換句話說，如果你位於索引 i，
    /// 可以跳到任意索引 (i + j)，其中：
    ///
    /// 0 <= j <= nums[i] 且
    /// i + j < n
    ///
    /// 回傳到達索引 n - 1 所需的最少跳躍次數。測試案例保證可以到達索引 n - 1。
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    static void Main(string[] args)
    {
        Program solution = new();
        (int[] Nums, int Expected)[] testCases =
        [
            ([2, 3, 1, 1, 4], 2),
            ([2, 3, 0, 1, 4], 2),
            ([0], 0),
            ([1, 1, 1, 1], 3),
        ];

        foreach ((int[] nums, int expected) in testCases)
        {
            int reverseGreedy = solution.Jump(nums);
            int forwardGreedy = solution.Jump2(nums);
            string status = reverseGreedy == expected && forwardGreedy == expected ? "PASS" : "FAIL";

            Console.WriteLine(
                $"{status} nums=[{string.Join(",", nums)}], expected={expected}, " +
                $"Jump={reverseGreedy}, Jump2={forwardGreedy}");
        }
    }

    /// <summary>
    /// 使用反向貪婪法計算抵達最後一格的最少跳躍次數。
    /// 解題概念是從終點往前找第一個可以跳到目前目標位置的索引，
    /// 再把該索引設為新的目標，直到回推到索引 0。
    /// 輸入需符合題目條件：非空陣列、元素為非負整數，且保證可抵達最後一格。
    /// 輸出為從索引 0 到索引 nums.Length - 1 的最少跳躍次數。
    ///
    /// 方法一: 貪婪算法，反向查找出發位置。
    /// https://leetcode.cn/problems/jump-game-ii/solution/xiang-xi-tong-su-de-si-lu-fen-xi-duo-jie-fa-by-10/
    ///
    /// 結尾位置固定為已知
    /// 所以我們只要求出 出發位置即可
    /// 反向推導
    /// 結尾位置前的 跳躍位置
    /// 如果有好幾個位置都能跳躍到結尾
    /// 那就考慮 距離結尾最遠的那個位置
    /// 也就是能一次跳最遠的距離優先選擇
    ///
    /// 反向推導, 所以跳躍位置會越來往前靠(縮短)
    ///
    /// 反向的是 position 位置
    /// i 是從起始位置往 position 找
    /// 找到能跳到 position 位置的 i
    /// 也就是找到能跳最遠的
    /// </summary>
    /// <param name="nums">每個位置可向前跳躍的最大距離。</param>
    /// <returns>抵達最後一個索引所需的最少跳躍次數。</returns>
    public int Jump(int[] nums)
    {
        int position = nums.Length - 1;
        int steps = 0;

        // 每輪鎖定目前目標 position，尋找最左側可抵達它的位置，代表前一步可站得最靠前。
        while (position > 0)
        {
            for (int i = 0; i < position; i++)
            {
                if (i + nums[i] >= position)
                {
                    position = i;
                    steps++;
                    break;
                }
            }
        }

        return steps;
    }

    /// <summary>
    /// 使用正向貪婪法計算抵達最後一格的最少跳躍次數。
    /// 解題概念是把每次跳躍能涵蓋的索引視為一層區間，掃描該區間時持續更新下一層可抵達的最遠位置。
    /// 輸入需符合題目條件：非空陣列、元素為非負整數，且保證可抵達最後一格。
    /// 輸出為從索引 0 到索引 nums.Length - 1 的最少跳躍次數。
    ///
    /// 方法二：正向查找可到達的最大位置。
    /// end 表示當前能跳躍的邊界。
    ///
    /// 如果我们「贪心」地进行正向查找，每次找到可到达的最远位置，就可以在线性时间内得到最少的跳跃次数。
    /// 例如，对于数组 [2,3,1,2,4,2,3]，初始位置是下标 0，从下标 0 出发，最远可到达下标 2。下标 0 可到达的位置中，下标 1 的值
    /// 是 3，从下标 1 出发可以达到更远的位置，因此第一步到达下标 1。
    /// 从下标 1 出发，最远可到达下标 4。下标 1 可到达的位置中，下标 4 的值是 4 ，从下标 4 出发可以达到更远的位置，因此第二步到
    /// 达下标 4。
    /// 在具体的实现中，我们维护当前能够到达的最大下标位置，记为边界。我们从左到右遍历数组，到达边界时，更新边界并将跳跃次数
    /// 增加 1。
    /// 在遍历数组时，我们不访问最后一个元素，这是因为在访问最后一个元素之前，我们的边界一定大于等于最后一个位置，否则就无法
    /// 跳到最后一个位置了。如果访问最后一个元素，在边界正好为最后一个位置的情况下，我们会增加一次「不必要的跳跃次数」，因此
    /// 我们不必访问最后一个元素。
    /// </summary>
    /// <param name="nums">每個位置可向前跳躍的最大距離。</param>
    /// <returns>抵達最後一個索引所需的最少跳躍次數。</returns>
    public int Jump2(int[] nums)
    {
        int res = 0;
        int end = 0;
        int maxPosition = 0;

        for (int i = 0; i < nums.Length - 1; i++)
        {
            maxPosition = Math.Max(maxPosition, i + nums[i]);

            // 掃到目前跳躍層的邊界時，必須多跳一次並切換到下一層可抵達的最遠邊界。
            if (i == end)
            {
                end = maxPosition;
                res++;
            }
        }
        return res;
    }
}
