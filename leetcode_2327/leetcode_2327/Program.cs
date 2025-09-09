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
        // 簡單範例測試，改為建立 Program 物件後呼叫實例方法
        int n = 6, delay = 2, forget = 4;
        var program = new Program();
        int result = program.PeopleAwareOfSecret(n, delay, forget);
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
                int shareFromDay = day - delay; // 這一天學到的人開始分享
                int shareIdx = (shareFromDay - 1) % forget;
                shareableSum = (shareableSum + buf[shareIdx]) % MOD;
            }

            // 同時如果有人在今天忘記，且他們先前已經在 shareableSum 中，需要從 shareableSum 中移除
            if (day - forget >= 1)
            {
                int forgotDay = day - forget;
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
}
