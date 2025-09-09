namespace leetcode_2327;

class Program
{
    /// <summary>
    /// 2327. Number of People Aware of a Secret
    /// https://leetcode.com/problems/number-of-people-aware-of-a-secret/description/?envType=daily-question&envId=2025-09-09
    /// 2327. 知道秘密的人數
    /// https://leetcode.cn/problems/number-of-people-aware-of-a-secret/description/?envType=daily-question&envId=2025-09-09
    ///
    /// 中文題目翻譯：
    /// 第 1 天，有一個人發現了一個秘密。
    /// 給定整數 delay，表示每個人在發現秘密 delay 天後開始，每天會把秘密分享給一個新的人。
    /// 給定整數 forget，表示每個人在發現秘密 forget 天後就會忘記秘密。人在忘記當天以及之後的任何一天都不能分享秘密。
    /// 給定整數 n，請返回在第 n 天結束時還知道秘密的人數。由於答案可能很大，請對 10^9 + 7 取模後返回。
    ///
    /// 範例：
    /// 輸入: n = 6, delay = 2, forget = 4
    /// 輸出: 5
    ///
    /// 參考（英文/中文）如上。
    ///
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 簡單範例測試
        int n = 6, delay = 2, forget = 4;
        int result = PeopleAware(n, delay, forget);
        Console.WriteLine($"People aware on day {n}: {result}");
    }

    /// <summary>
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
    static int PeopleAware(int n, int delay, int forget)
    {
        const int MOD = 1_000_000_007;
        // arr[i] = 在 day i 那天新知道秘密的人數（1-based day）。我們只需要保留最後 forget 天。
        long[] window = new long[forget];
        // 第 1 天，有 1 人知道秘密
        window[0] = 1;
        long totalAware = 1; // 在目前天數仍記得秘密的人數總和

        for (int day = 2; day <= n; day++)
        {
            // index for the new day in circular buffer
            int idx = (day - 1) % forget;

            // people who forget today are window[idx]
            totalAware = (totalAware - window[idx] + MOD) % MOD;

            // compute new people who learn the secret today:
            // 他們來自於那些已經知道且進入分享期的人：也就是 totalAware 相減掉尚未進入分享期的人
            long newLearners = 0;
            if (day - delay >= 1)
            {
                // sum of people who learned on days [1 .. day-delay] but still haven't forgotten
                // 這等價於總體仍記得的人 totalAware 加上即將被移除的 window[idx]，
                // 再減去那些尚未到分享期的人 (在最近 delay-1 天內新知道的人)。
                // 更簡潔的做法：計算分享者為在 buffer 中那些 index 對應到 day-shareDay >= delay
                int startShareDay = Math.Max(1, day - forget + 1);
                int shareThresholdDay = day - delay;

                // sum over days t in [startShareDay .. shareThresholdDay] of window[(t-1)%forget]
                // 直接遍歷小於 forget 的範圍
                long sum = 0;
                int from = startShareDay;
                int to = shareThresholdDay;
                for (int t = from; t <= to; t++)
                {
                    sum = (sum + window[(t - 1) % forget]) % MOD;
                }
                newLearners = sum;
            }

            // place new learners into buffer slot idx
            window[idx] = newLearners % MOD;
            totalAware = (totalAware + window[idx]) % MOD;
        }

        return (int)(totalAware % MOD);
    }
}
