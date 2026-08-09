namespace leetcode_2327;

class Program
{
    /// <summary>
    /// <para>
    /// 2327. Number of People Aware of a Secret
    /// https://leetcode.com/problems/number-of-people-aware-of-a-secret/description/
    ///
    /// On day 1, one person discovers a secret. Each person shares it with one new person every day beginning delay days after learning it. Each person forgets it forget days after learning it and cannot share on that day or later. Given n, return how many people know the secret at the end of day n, modulo 10^9 + 7.
    ///
    /// Example 1:
    /// Input: n = 6, delay = 2, forget = 4
    /// Output: 5
    /// Explanation: Day 1: A discovers the secret (1 person). Day 2: only A knows (1). Day 3: A tells B (2). Day 4: A tells C (3). Day 5: A forgets and B tells D (3). Day 6: B tells E and C tells F (5).
    ///
    /// Example 2:
    /// Input: n = 4, delay = 1, forget = 3
    /// Output: 6
    /// Explanation: Day 1: A discovers the secret (1 person). Day 2: A tells B (2). Day 3: A and B tell C and D (4). Day 4: A forgets; B, C, and D tell three new people (6).
    ///
    /// Constraints:
    /// - 2 &lt;= n &lt;= 1000
    /// - 1 &lt;= delay &lt; forget &lt;= n
    /// </para>
    /// <para>
    /// 2327. 知道秘密的人數
    /// https://leetcode.cn/problems/number-of-people-aware-of-a-secret/description/
    ///
    /// 第 1 天有一人發現秘密。每個人在得知秘密的 delay 天後開始每天分享給一位新人；在得知秘密的 forget 天後忘記，且當天及之後都不能分享。給定 n，回傳第 n 天結束時仍知道秘密的人數，答案對 10^9 + 7 取模。
    ///
    /// 範例 1：
    /// 輸入：n = 6, delay = 2, forget = 4
    /// 輸出：5
    /// 說明：第 1 天：A 發現秘密（1 人）。第 2 天：只有 A 知道（1）。第 3 天：A 告訴 B（2）。第 4 天：A 告訴 C（3）。第 5 天：A 忘記，B 告訴 D（3）。第 6 天：B 告訴 E，C 告訴 F（5）。
    ///
    /// 範例 2：
    /// 輸入：n = 4, delay = 1, forget = 3
    /// 輸出：6
    /// 說明：第 1 天：A 發現秘密（1 人）。第 2 天：A 告訴 B（2）。第 3 天：A、B 告訴 C、D（4）。第 4 天：A 忘記；B、C、D 告訴三位新人（6）。
    ///
    /// 限制條件：
    /// - 2 &lt;= n &lt;= 1000
    /// - 1 &lt;= delay &lt; forget &lt;= n
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 簡單範例測試，改為建立 Program 物件後呼叫實例方法
        int n = 6, delay = 2, forget = 4;
        var program = new Program();
        int result = program.PeopleAwareOfSecret(n, delay, forget);
        Console.WriteLine($"解法B: People aware on day {n}: {result}");

        int result2 = program.PeopleAwareOfSecret1(n, delay, forget);
        Console.WriteLine($"解法A: People aware on day {n}: {result2}");
    }

    /// <summary>
    /// 解法 B — O(n) 時間, O(forget) 空間（環形緩衝 + 滑動和）
    /// 
    /// 計算在第 n 天結束時知道秘密的人數，對 1_000_000_007 取模。
    /// 輸入: n, delay, forget
    /// 輸出: 知道秘密的人數 (int)
    ///
    /// 思路：使用動態規劃或模擬計數。對於每一天，追蹤當天新知道秘密的人數，
    /// 並維護一個環形陣列記錄過去 forget 天中每天新知道的人。每天產生的新知道
    /// 人數等於過去已經入分享期 (delay) 且尚未忘記的人會分享的總和。
    ///
    /// 時間複雜度: O(n * forget) 或 O(n) 使用滑動窗最佳化
    /// 空間複雜度: O(forget)
    /// </summary>
    public int PeopleAwareOfSecret(int n, int delay, int forget)
    {
        const int MOD = 1_000_000_007;
        // 使用 O(forget) 空間的環形緩衝與滑動和
        if (n == 0) return 0;
        long[] buf = new long[forget]; // buf[i] = 第 (day) 天新知道的人數，保留最近 forget 天
        // day 1
        buf[0] = 1;
        long totalRemembering = 1; // 當前仍記得秘密的總人數
        long shareableSum = 0; // 當天可以分享的人數（進入分享期且尚未忘記）

        // 初始化 shareableSum 在 day=1 時為 0（因為 delay >=1 通常），迴圈從 day=2
        for (int day = 2; day <= n; day++)
        {
            int idx = (day - 1) % forget; // 對應要覆寫的 slot，這個 slot 的值是 day-forget 的新知道值

            // 先移除今天忘記的人（如果有）
            totalRemembering = (totalRemembering - buf[idx] + MOD) % MOD;

            // 更新 shareableSum：當 day - delay >= 1 時，新增那些剛好到分享期的人
            if (day - delay >= 1)
            {
                int shareFromDay = day - delay; // 這一天學到的人開始分享;「剛好到達分享期的人（day - delay）」
                int shareIdx = (shareFromDay - 1) % forget;
                shareableSum = (shareableSum + buf[shareIdx]) % MOD;
            }

            // 同時如果有人在今天忘記，且他們先前已經在 shareableSum 中，需要從 shareableSum 中移除
            if (day - forget >= 1)
            {
                int forgotDay = day - forget; // 「剛好在今天忘記的人（day - forget）」
                int forgotIdx = (forgotDay - 1) % forget;
                shareableSum = (shareableSum - buf[forgotIdx] + MOD) % MOD;
            }

            // 今天新知道的人數就是當前可分享的人數
            long newLearners = shareableSum % MOD;

            // 放入 buffer（覆寫 day-forget 的 slot）
            buf[idx] = newLearners;
            totalRemembering = (totalRemembering + newLearners) % MOD;
        }

        return (int)totalRemembering;
    }


    /// <summary>
    /// 解法 A — O(n) 時間, O(n) 空間（前綴和）
    /// </summary>
    /// <param name="n"></param>
    /// <param name="delay"></param>
    /// <param name="forget"></param>
    /// <returns></returns>
    public int PeopleAwareOfSecret1(int n, int delay, int forget)
    {
        const int MOD = 1_000_000_007;
        // 使用 O(n) 的滑動窗與前綴和思路：
        // keep[i] 表示第 i 天新知道的人數（1-based day），只需長度 n+1 的陣列。
        // newLearners(day) = sum_{t=1..day-delay} keep[t] - sum_{t=1..day-forget} keep[t]
        // 我們可以透過 prefix sum 快速取得區間和。
        long[] keep = new long[n + 1];
        keep[1] = 1;
        long[] prefix = new long[n + 1];
        prefix[1] = 1;

        for (int day = 2; day <= n; day++)
        {
            // 分享者來自那些在 [1, day-delay] 天學到，且尚未在 day 忘記的人。
            if (day - delay >= 1)
            {
                int l = 1;
                int r = day - delay;
                long totalCanShare = (prefix[r] - prefix[l - 1] + MOD) % MOD;
                // 減去已經忘記的人：那些在 [1, day-forget] 的人已經忘記
                if (day - forget >= 1)
                {
                    int fr = day - forget;
                    totalCanShare = (totalCanShare - prefix[fr] + MOD) % MOD;
                }
                keep[day] = totalCanShare % MOD;
            }
            else
            {
                keep[day] = 0;
            }

            prefix[day] = (prefix[day - 1] + keep[day]) % MOD;
        }

        // 統計在第 n 天仍然記得的人：這些人是在天數區間 (n-forget+1 .. n) 內學到的
        long ans = 0;
        int start = Math.Max(1, n - forget + 1);
        for (int t = start; t <= n; t++) ans = (ans + keep[t]) % MOD;
        return (int)ans;
    }
}
