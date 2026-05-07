namespace leetcode_3660;

class Program
{
    /// <summary>
    /// 3660. Jump Game IX
    /// https://leetcode.com/problems/jump-game-ix/description/?envType=daily-question&envId=2026-05-07
    /// 3660. 跳跃游戏 IX
    /// https://leetcode.cn/problems/jump-game-ix/description/?envType=daily-question&envId=2026-05-07
    ///
    /// English:
    /// You are given an integer array nums.
    /// From any index i, you can jump to another index j under the following rules:
    /// Jump to index j where j &gt; i is allowed only if nums[j] &lt; nums[i].
    /// Jump to index j where j &lt; i is allowed only if nums[j] &gt; nums[i].
    /// For each index i, find the maximum value in nums that can be reached by following
    /// any sequence of valid jumps starting at i.
    /// Return an array ans where ans[i] is the maximum value reachable starting from index i.
    ///
    /// 繁體中文:
    /// 給定一個整數陣列 nums。
    /// 對於任一索引 i，你可以依照下列規則跳到另一個索引 j：
    /// 若 j > i，則只有在 nums[j] < nums[i] 時，才可以跳到索引 j。
    /// 若 j < i，則只有在 nums[j] > nums[i] 時，才可以跳到索引 j。
    /// 對於每個索引 i，請找出從 i 出發，經由任意合法跳躍序列後，所能到達的 nums 中最大值。
    /// 回傳一個陣列 ans，其中 ans[i] 表示從索引 i 出發所能到達的最大值。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solver = new Program();

        int[][] testCases =
        {
            new int[] { 4, 2, 5, 1, 3 },
            new int[] { 1, 2, 3, 4 },
            new int[] { 4, 3, 2, 1 },
            new int[] { 2, 1, 4, 3, 5 },
            new int[] { 2, 2, 1, 3 },
            new int[] { 5, 1, 4, 2, 3 },
            new int[] { 3, 1, 3, 2 },
            new int[] { 7 },
            Array.Empty<int>(),
        };

        foreach (int[] nums in testCases)
        {
            int[] answer = solver.MaxValue(nums);
            int[] answer2 = solver.MaxValue2(nums);
            Console.WriteLine($"nums = [{string.Join(", ", nums)}]");
            Console.WriteLine($"ans1 = [{string.Join(", ", answer)}]");
            Console.WriteLine($"ans2 = [{string.Join(", ", answer2)}]");
            Console.WriteLine($"same = {answer.SequenceEqual(answer2)}");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 計算每個索引出發時，依照 Jump Game IX 規則所能到達的最大值。
    /// 此方法不直接模擬跳躍，而是利用可達性把陣列切成多個連續區間。
    /// 若切點 i 滿足左側最大值小於等於右側最小值，代表不會存在跨越此切點的逆序對，
    /// 也就不會有跨區間的合法跳躍，因此同一區間內的所有索引答案都相同。
    /// 時間複雜度為 O(n)，額外空間複雜度為 O(n)。
    /// </summary>
    /// <param name="nums">輸入的整數陣列。</param>
    /// <returns>
    /// 回傳答案陣列，其中每個元素代表對應索引出發時可到達的最大值。
    /// </returns>
    public int[] MaxValue(int[] nums)
    {
        int length = nums.Length;

        if (length == 0)
        {
            return Array.Empty<int>();
        }

        int[] suffixMin = new int[length];
        suffixMin[length - 1] = nums[length - 1];

        // 若左側最大值 <= 右側最小值，代表沒有任何逆序對會跨過這個切點。
        for (int i = length - 2; i >= 0; i--)
        {
            suffixMin[i] = Math.Min(nums[i], suffixMin[i + 1]);
        }

        int[] answer = new int[length];
        int blockStart = 0;
        int currentBlockMax = nums[0];

        for (int i = 0; i < length; i++)
        {
            // currentBlockMax 表示目前這個可達區間內，已掃描部分的最大值。
            currentBlockMax = Math.Max(currentBlockMax, nums[i]);

            if (i == length - 1 || currentBlockMax <= suffixMin[i + 1])
            {
                // 同一可達區間內的索引彼此可互達，因此整段答案都等於這段最大值。
                Array.Fill(answer, currentBlockMax, blockStart, i - blockStart + 1);

                if (i + 1 < length)
                {
                    blockStart = i + 1;
                    currentBlockMax = nums[blockStart];
                }
            }
        }

        return answer;
    }

    /// <summary>
    /// 解法二：區間分治。
    /// 先預處理每個前綴區間 [0..i] 的最大值與所在位置。遞迴時，取目前前綴 [0..r]
    /// 的最大值作為 pivot，將 [0..r] 分成左側未處理區間與右側區間 [pivotIndex..r]。
    /// rightMax 表示前一個右側可達區間的目標最大值，也就是能繼承到的答案；
    /// rightMin 表示在該 rightMax 對應區間中，可以轉移到 rightMax 的元素最小值。
    /// 若目前前綴最大值 pMax 大於 rightMin，代表目前區間可以透過 rightMin
    /// 中轉到前一個右側區間，因此答案繼承 rightMax；否則目前區間無法跨過去，
    /// 答案就是本區間的前綴最大值 pMax。
    /// 每次處理完 [pivotIndex..r] 後，更新從 pivotIndex 往右的後綴最小值，
    /// 再遞迴處理 [0..pivotIndex - 1]。
    /// 時間複雜度為 O(n)，額外空間複雜度為 O(n)。
    /// </summary>
    /// <param name="nums">輸入的整數陣列。</param>
    /// <returns>
    /// 回傳答案陣列，其中每個元素代表對應索引出發時可到達的最大值。
    /// </returns>
    public int[] MaxValue2(int[] nums)
    {
        int n = nums.Length;

        if (n == 0)
        {
            return Array.Empty<int>();
        }

        int[] ans = new int[n];

        // prevMax[i] 記錄 nums[0..i] 的最大值，以及該最大值第一次出現的位置。
        (int Value, int Index)[] prevMax = new (int, int)[n];

        (int Value, int Index) prev = (int.MinValue, -1);
        for (int i = 0; i < n; i++)
        {
            if (nums[i] > prev.Value)
            {
                prev = (nums[i], i);
            }
            prevMax[i] = prev;
        }

        void Process(int r, int rightMin, int rightMax)
        {
            (int pMax, int pivotIndex) = prevMax[r];

            // 若 pMax <= rightMin，代表目前區間無法轉移到已處理的右側區間，
            // 答案只能是目前前綴最大值；否則可繼承右側區間的答案 rightMax。
            int currMax = pMax <= rightMin ? pMax : rightMax;

            // rightMin 本質上會被更新為 nums[pivotIndex..n-1] 的後綴最小值。
            // 若舊區間不可達，舊的 rightMin 一定不小於 pMax，仍會被本段最小值取代。
            int nextRightMin = Math.Min(pMax, rightMin);
            for (int i = pivotIndex; i <= r; i++)
            {
                ans[i] = currMax;
                nextRightMin = Math.Min(nextRightMin, nums[i]);
            }

            if (pivotIndex == 0)
            {
                return;
            }

            Process(pivotIndex - 1, nextRightMin, currMax);
        }

        Process(n - 1, int.MaxValue, 0);
        return ans;
    }
}
