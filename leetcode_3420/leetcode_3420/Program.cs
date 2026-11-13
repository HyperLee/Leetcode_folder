namespace leetcode_3420;

class Program
{
    /// <summary>
    /// 3420. Count Non-Decreasing Subarrays After K Operations
    /// https://leetcode.com/problems/count-non-decreasing-subarrays-after-k-operations/description/
    /// <para>
    /// You are given an array <c>nums</c> of <c>n</c> integers and an integer <c>k</c>.
    ///
    /// For each subarray of <c>nums</c>, you can apply up to <c>k</c> operations on it. In each operation, you increment any element of the subarray by 1.
    ///
    /// Note that each subarray is considered independently, meaning changes made to one subarray do not persist to another.
    /// Return the number of subarrays that you can make non-decreasing after performing at most <c>k</c> operations.
    ///
    /// An array is said to be non-decreasing if each element is greater than than or equal to its previous element, if it exists.
    ///
    /// /// Example 1:
    /// Input: nums = [6,3,1,2,4,4], k = 7
    /// Output: 17
    ///
    /// Explanation:
    /// Out of all 21 possible subarrays of <c>nums</c>, only the subarrays [6, 3, 1], [6, 3, 1, 2], [6, 3, 1, 2, 4] and [6, 3, 1, 2, 4, 4] cannot be made non-decreasing after applying up to <c>k</c> = 7 operations. Thus, the number of non-decreasing subarrays is 21 - 4 = 17.
    ///
    /// Example 2:
    /// Input: nums = [6,3,1,3,6], k = 4
    /// Output: 12
    ///
    /// Explanation:
    /// The subarray [3, 1, 3, 6] along with all subarrays of <c>nums</c> with three or fewer elements, except [6, 3, 1], can be made non-decreasing after <c>k</c> operations. There are 5 subarrays of a single element, 4 subarrays of two elements, and 2 subarrays of three elements except [6, 3, 1], so there are 1 + 5 + 4 + 2 = 12 subarrays that can be made non-decreasing.
    ///
    /// Constraints:
    /// - 1 &lt;= nums.length &lt;= 10^5
    /// - 1 &lt;= nums[i] &lt;= 10^9
    /// - 1 &lt;= k &lt;= 10^9
    /// </para>
    /// <para>
    /// 3420. 統計 K 次操作以內得到非遞減子陣列的數目
    /// https://leetcode.cn/problems/count-non-decreasing-subarrays-after-k-operations/description/
    ///
    /// 給定一個包含 <c>n</c> 個整數的陣列 <c>nums</c>，以及一個整數 <c>k</c>。
    ///
    /// 對於 <c>nums</c> 的每個子陣列，你最多可以對該子陣列執行 <c>k</c> 次操作。每次操作中，將子陣列中的任意一個元素加 1。
    ///
    /// 請注意，每個子陣列都會獨立計算，也就是說，對某個子陣列所做的變更不會延續到其他子陣列。
    /// 請回傳最多執行 <c>k</c> 次操作後，可以調整為非遞減的子陣列數量。
    ///
    /// 若陣列中每個元素（若有前一個元素）都大於或等於前一個元素，則稱此陣列為非遞減。
    ///
    /// 範例 1：
    /// 輸入：nums = [6,3,1,2,4,4], k = 7
    /// 輸出：17
    ///
    /// 解釋：
    /// 在 <c>nums</c> 所有 21 個可能的子陣列中，只有 [6, 3, 1]、[6, 3, 1, 2]、[6, 3, 1, 2, 4] 和 [6, 3, 1, 2, 4, 4] 無法在最多執行 <c>k</c> = 7 次操作後變成非遞減。因此，非遞減子陣列的數量為 21 - 4 = 17。
    ///
    /// 範例 2：
    /// 輸入：nums = [6,3,1,3,6], k = 4
    /// 輸出：12
    ///
    /// 解釋：
    /// 子陣列 [3, 1, 3, 6]，以及 <c>nums</c> 中所有長度不超過 3 的子陣列（[6, 3, 1] 除外），都可以在 <c>k</c> 次操作內變成非遞減。長度為 1 的子陣列有 5 個，長度為 2 的有 4 個，長度為 3 的有 2 個（不包含 [6, 3, 1]），因此可變成非遞減的子陣列共有 1 + 5 + 4 + 2 = 12 個。
    ///
    /// 限制條件：
    /// - 1 &lt;= nums.length &lt;= 10^5
    /// - 1 &lt;= nums[i] &lt;= 10^9
    /// - 1 &lt;= k &lt;= 10^9
    /// </para>
    /// </summary>
    /// <param name="args">命令列參數；此範例不使用。</param>
    /// <remarks>
    /// 執行兩種解法的 13 筆固定案例，以及固定種子的 1,000 筆小陣列暴力交叉驗證。
    /// 每筆固定案例顯示預期值與實際值；答案不符或輸入被修改時拋出例外，讓程序以非零狀態結束。
    /// </remarks>
    static void Main(string[] args)
    {
        var solver = new Program();
        var largeEqual = Enumerable.Repeat(1, 100_000).ToArray();
        var largePlateau = Enumerable.Repeat(1, 100_000).ToArray();
        largePlateau[0] = 1_000_000_000;

        var testCases = new (string Name, int[] Nums, int K, long Expected)[]
        {
            ("Example 1", new[] { 6, 3, 1, 2, 4, 4 }, 7, 17),
            ("Example 2", new[] { 6, 3, 1, 3, 6 }, 4, 12),
            ("Single element", new[] { 1 }, 1, 1),
            ("Increasing", new[] { 1, 2, 3, 4 }, 1, 10),
            ("All equal", new[] { 2, 2, 2, 2 }, 1, 10),
            ("Decreasing", new[] { 5, 4, 3, 2, 1 }, 3, 12),
            ("Exact budget", new[] { 3, 1, 2 }, 3, 6),
            ("Below required budget", new[] { 3, 1, 2 }, 2, 5),
            ("Gap exceeds budget", new[] { 5, 1 }, 3, 2),
            ("Repeated plateaus", new[] { 4, 4, 1, 1, 5 }, 3, 11),
            ("Large values", new[] { 1_000_000_000, 1, 1 }, 1_000_000_000, 5),
            ("Large equal array", largeEqual, 1, 5_000_050_000L),
            ("Large plateau cost", largePlateau, 1_000_000_000, 4_999_950_002L)
        };

        Console.WriteLine("LeetCode 3420: fixed test cases");
        foreach (var testCase in testCases)
        {
            VerifyCase(solver, testCase.Name, testCase.Nums, testCase.K, testCase.Expected);
        }

        const int seed = 3420;
        const int randomCaseCount = 1_000;
        var random = new Random(seed);
        for (int caseIndex = 0; caseIndex < randomCaseCount; caseIndex++)
        {
            var nums = new int[random.Next(1, 13)];
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = random.Next(1, 11);
            }

            int k = random.Next(1, 51);
            long expected = CountNonDecreasingSubarraysBruteForce(nums, k);
            VerifyCase(solver, $"Random case {caseIndex + 1}", nums, k, expected, printResult: false);
        }

        Console.WriteLine($"[PASS] Random cross-check: {randomCaseCount} cases (seed={seed}).");
        Console.WriteLine($"All tests passed: {testCases.Length} fixed cases + {randomCaseCount} random cases.");
    }

    /// <summary>
    /// 驗證兩種解法在題目合法輸入下皆回傳指定答案，且不修改輸入陣列。
    /// 比較失敗時拋出例外並附上可重現的資料；通過時可選擇輸出測試結果。
    /// </summary>
    /// <param name="solver">提供兩種正式解法的執行個體。</param>
    /// <param name="name">測試名稱，供輸出與失敗診斷使用。</param>
    /// <param name="nums">符合題目限制的非空陣列。</param>
    /// <param name="k">最多可用的加一操作次數，介於 1 與 10^9 之間。</param>
    /// <param name="expected">獨立推導或由暴力參考解法計算的預期答案。</param>
    /// <param name="printResult">是否輸出這筆案例的預期值、實際值與通過狀態。</param>
    private static void VerifyCase(Program solver, string name, int[] nums, int k, long expected, bool printResult = true)
    {
        var original = (int[])nums.Clone();
        long result1 = solver.CountNonDecreasingSubarrays(nums, k);
        if (result1 != expected || !nums.SequenceEqual(original))
        {
            throw new InvalidOperationException(
                $"{name}: method1 failed; nums=[{string.Join(",", original)}], k={k}, " +
                $"expected={expected}, actual={result1}, inputUnchanged={nums.SequenceEqual(original)}.");
        }

        long result2 = solver.CountNonDecreasingSubarrays2(nums, k);
        if (result2 != expected || !nums.SequenceEqual(original))
        {
            throw new InvalidOperationException(
                $"{name}: method2 failed; nums=[{string.Join(",", original)}], k={k}, " +
                $"expected={expected}, actual={result2}, inputUnchanged={nums.SequenceEqual(original)}.");
        }

        if (printResult)
        {
            Console.WriteLine($"[PASS] {name}: expected={expected}, method1={result1}, method2={result2}");
        }
    }

    /// <summary>
    /// 以 O(n²) 暴力枚舉驗證小型合法輸入；固定起點後，用前綴最大值計算每個終點的最低成本。
    /// 成本超過 k 即停止延伸，回傳可以在預算內調整為非遞減的子陣列總數，不修改輸入。
    /// </summary>
    /// <param name="nums">非空正整數陣列；僅在小陣列交叉驗證時使用。</param>
    /// <param name="k">最多可用的加一操作次數，介於 1 與 10^9 之間。</param>
    /// <returns>最低操作成本不超過 k 的子陣列數量。</returns>
    private static long CountNonDecreasingSubarraysBruteForce(int[] nums, int k)
    {
        long answer = 0;
        for (int left = 0; left < nums.Length; left++)
        {
            int prefixMax = nums[left];
            long operations = 0;
            for (int right = left; right < nums.Length; right++)
            {
                prefixMax = Math.Max(prefixMax, nums[right]);
                operations += (long)prefixMax - nums[right];
                // 固定起點時，延長子陣列只會增加或維持成本，超支後不會恢復合法。
                if (operations > k)
                {
                    break;
                }

                answer++;
            }
        }

        return answer;
    }

    /// <summary>
    /// 方法二：由右往左擴展滑動視窗，用單調雙端佇列維護前綴最大值形成的平台。
    /// 合併較低平台以更新最低操作成本，超支時移除右端，累加每個起點的合法終點數量。
    /// 接受題目限制內的非空正整數陣列及正整數預算；時間 O(n)、額外空間 O(n)，不修改輸入。
    /// </summary>
    /// <param name="nums">長度介於 1 與 10^5，且各元素介於 1 與 10^9 的陣列。</param>
    /// <param name="k">最多可用的加一操作次數，介於 1 與 10^9 之間。</param>
    /// <returns>最多執行 k 次操作後可以成為非遞減的子陣列數量，以 long 避免答案溢位。</returns>
    public long CountNonDecreasingSubarrays2(int[] nums, int k)
    {
        int n = nums.Length;
        var deque = new int[n];
        int head = 0;
        int tail = 0;
        int right = n - 1;
        long operations = 0;
        long answer = 0;

        for (int left = n - 1; left >= 0; left--)
        {
            // deque[head..tail) 的索引由右至左遞減，高度不遞增。
            // 尾端是最左邊的平台；新左端點較高時，這些平台都必須被補到新高度。
            while (head < tail && nums[deque[tail - 1]] < nums[left])
            {
                int index = deque[--tail];
                int segmentRight = head < tail ? deque[tail - 1] : right + 1;
                // 原平台涵蓋 [index, segmentRight)，先轉 long 再乘，避免大面積提升溢位。
                operations += (long)(nums[left] - nums[index]) * (segmentRight - index);
            }

            deque[tail++] = left;

            while (operations > k)
            {
                // 佇列前端是右端點所屬的平台；移除 nums[right] 只需扣掉它的提升量。
                operations -= (long)nums[deque[head]] - nums[right];
                if (deque[head] == right)
                {
                    head++;
                }

                right--;
            }

            // 固定 left 時，縮短右端不會增加成本，所以 left..right 的每個終點都合法。
            answer += right - left + 1;
        }

        return answer;
    }

    /// <summary>
    /// 方法一：由左往右擴展滑動視窗，用優先佇列取得視窗最大值，計算加入右端點的提升成本。
    /// 單調堆疊建立覆蓋關係與下一個大於等於的邊界，讓移除左端點時可以按區段扣除成本。
    /// 接受題目限制內的非空正整數陣列及正整數預算，回傳成本不超過 k 的子陣列總數。
    /// 時間 O(n log n)、額外空間 O(n)，不修改輸入陣列。
    /// </summary>
    /// <param name="nums">長度介於 1 與 10^5，且各元素介於 1 與 10^9 的陣列。</param>
    /// <param name="k">最多可用的加一操作次數，介於 1 與 10^9 之間。</param>
    /// <returns>最多執行 k 次操作後可以成為非遞減的子陣列數量，以 long 避免答案溢位。</returns>
    public long CountNonDecreasingSubarrays(int[] nums, int k)
    {
        int n = nums.Length;
        long ans = 0;
        long operations = 0;
        // PriorityQueue 是最小堆，使用負值優先權，讓堆頂成為視窗最大值。
        var q = new PriorityQueue<(int val, int idx), int>();
        // cnt[p] 記錄左側最近且嚴格較大元素為 p 的索引；移除 p 時，由它們接手區段高度。
        var cnt = new List<int>[n];
        for (int i = 0; i < n; i++)
        {
            cnt[i] = new List<int>();
        }

        // pos[i] 是已掃描範圍中，i 右側第一個大於等於 nums[i] 的索引；尚未遇到時以 n 表示。
        var pos = new int[n];
        Array.Fill(pos, n);
        // 堆疊中的值嚴格遞減，用來建立 cnt 與 pos；它與目前視窗左界無關。
        var st = new Stack<int>();

        for (int left = 0, right = 0; right < n; right++)
        {
            while (st.Count > 0 && nums[right] >= nums[st.Peek()])
            {
                pos[st.Peek()] = right;
                st.Pop();
            }

            // 移除不大於新元素的索引後，堆疊頂端就是它左側最近的嚴格較大元素。
            if (st.Count > 0)
            {
                cnt[st.Peek()].Add(right);
            }

            st.Push(right);

            q.Enqueue((nums[right], right), -nums[right]);
            // 舊視窗的提升結果不變；新元素只需補到新視窗最大值。
            operations += Math.Max(0, q.Peek().val - nums[right]);

            while (operations > k)
            {
                foreach (int i in cnt[left])
                {
                    // 移除 left 後，[i, min(pos[i], right + 1)) 的目標高度
                    // 由 nums[left] 降為 nums[i]，因此扣除「高度差 × 區段寬度」。
                    operations -= (long)(nums[left] - nums[i]) * (Math.Min(pos[i], right + 1) - i);
                }

                left++;
            }

            // 縮窗使用覆蓋關係扣成本，不需堆頂；此時再清掉過期堆頂，供下一輪擴窗使用。
            // 其餘過期項目可留在堆內，等它們成為堆頂時才移除。
            while (q.Count > 0 && q.Peek().idx < left)
            {
                q.Dequeue();
            }

            // left 是這個 right 的最早合法起點；再向右移起點也不會增加最低成本。
            ans += right - left + 1;
        }

        return ans;
    }
}
