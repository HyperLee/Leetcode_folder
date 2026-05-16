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
    /// 解題方式：反向貪婪。
    /// 從最後一個索引開始，把它視為目前必須抵達的目標位置 <c>position</c>。
    /// 每一輪由左往右掃描 <c>[0, position)</c>，尋找最左側且可以跳到
    /// <c>position</c> 的索引，將該索引改為新的目標位置，並累計一次跳躍。
    /// 目標位置會持續往左推回，直到回到索引 0；累計次數即為最少跳躍次數。
    /// 時間複雜度為 O(n^2)，空間複雜度為 O(1)。
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
    /// 解題方式：正向貪婪。
    /// 將目前一次跳躍可涵蓋的索引視為區間，<c>end</c> 表示當前區間的右邊界。
    /// 從左到右掃描時，持續用 <c>i + nums[i]</c> 更新下一次跳躍可抵達的最遠位置
    /// <c>maxPosition</c>。當掃描索引 <c>i</c> 到達 <c>end</c> 時，代表目前區間已掃完，
    /// 必須增加一次跳躍，並把邊界推進到 <c>maxPosition</c>。
    /// 迴圈只掃描到倒數第二個索引，避免在已抵達終點時多計算一次跳躍。
    /// 時間複雜度為 O(n)，空間複雜度為 O(1)。
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
