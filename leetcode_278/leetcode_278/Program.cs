namespace leetcode_278
{
    internal class Program
    {
        private static int simulatedFirstBadVersion;

        /// <summary>
        /// 278. First Bad Version
        /// https://leetcode.cn/problems/first-bad-version/description/
        /// 
        /// 278. 第一个错误的版本
        /// https://leetcode.cn/problems/first-bad-version/description/
        /// </summary>
        /// <remarks>
        /// 主程式會執行固定測資，驗證二分搜尋能在第一版、最後一版、中間版本與整數上限等情境找到第一個錯誤版本。
        /// </remarks>
        /// <param name="args">命令列參數；本範例程式不需要使用。</param>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 執行六筆固定案例，逐筆設定本機錯誤版本邊界並比對預期與實際結果。
        /// </summary>
        /// <remarks>
        /// 測資涵蓋官方範例、單一版本、第一版即錯、最後一版才錯、一般中段與 <see cref="int.MaxValue"/> 上界。
        /// 每筆案例都符合 <c>1 &lt;= bad &lt;= n</c>，並輸出 PASS/FAIL 與最終通過數。
        /// </remarks>
        private static void RunSamples()
        {
            SampleCase[] samples =
            [
                new SampleCase("官方範例 - 第 4 版開始錯誤", 5, 4, 4),
                new SampleCase("單一版本 - 唯一版本即為錯誤版本", 1, 1, 1),
                new SampleCase("第一版即錯 - 所有版本皆錯誤", 10, 1, 1),
                new SampleCase("最後一版才錯 - 前面版本皆正確", 10, 10, 10),
                new SampleCase("一般中段 - 第 5 版開始錯誤", 8, 5, 5),
                new SampleCase("整數上限 - 驗證中點計算不溢位", int.MaxValue, int.MaxValue, int.MaxValue)
            ];

            int passedCount = 0;

            Console.WriteLine("LeetCode 278 - First Bad Version");
            Console.WriteLine("解法：在單調的版本區間中使用二分搜尋定位第一個錯誤版本");
            Console.WriteLine();

            for (int i = 0; i < samples.Length; i++)
            {
                SampleCase sample = samples[i];
                simulatedFirstBadVersion = sample.Bad;

                int actual = FirstBadVersion(sample.N);
                bool passed = actual == sample.Expected;

                if (passed)
                {
                    passedCount++;
                }

                Console.WriteLine($"案例 {i + 1}：{sample.Description}");
                Console.WriteLine($"n：{sample.N}");
                Console.WriteLine($"bad：{sample.Bad}");
                Console.WriteLine($"預期：{sample.Expected}");
                Console.WriteLine($"實際：{actual} => {(passed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passedCount}/{samples.Length} 筆測試通過");
        }

        /// <summary>
        /// 使用二分搜尋，在版本範圍 <c>[1, n]</c> 中找出第一個錯誤版本。
        /// </summary>
        /// <remarks>
        /// 錯誤狀態具有單調性：第一個錯誤版本之前皆為正確版本，從該版本起皆為錯誤版本。
        /// 搜尋期間維持答案位於閉區間 <c>[left, right]</c>；每次透過 <see cref="IsBadVersion"/> 將範圍縮小一半。
        /// </remarks>
        /// <param name="n">最後一個版本號；依題目保證介於 1 與 <see cref="int.MaxValue"/> 之間，且範圍內至少有一個錯誤版本。</param>
        /// <returns>範圍 <c>[1, n]</c> 中第一個錯誤版本的版本號。</returns>
        public static int FirstBadVersion(int n)
        {
            int left = 1;
            int right = n;

            // 閉區間 [left, right] 始終包含第一個錯誤版本，直到兩個邊界收斂。
            while (left < right)
            {
                int mid = left + (right - left) / 2;

                if (IsBadVersion(mid))
                {
                    // mid 可能正是第一個錯誤版本，因此保留 mid 並縮小右邊界。
                    right = mid;
                }
                else
                {
                    // mid 為正確版本，可排除 [left, mid]，答案只可能在右側。
                    left = mid + 1;
                }
            }

            return left;
        }

        /// <summary>
        /// 模擬 LeetCode 提供的版本檢查 API，判斷指定版本是否位於錯誤版本區間。
        /// </summary>
        /// <remarks>
        /// <see cref="RunSamples"/> 會在每筆案例執行前設定錯誤起點；實際提交至 LeetCode 時，平台會提供真正的 <c>isBadVersion</c> API。
        /// </remarks>
        /// <param name="version">要檢查的版本號；案例保證介於 1 與目前的版本總數之間。</param>
        /// <returns>若版本號大於或等於目前設定的第一個錯誤版本則回傳 <c>true</c>；否則回傳 <c>false</c>。</returns>
        public static bool IsBadVersion(int version)
        {
            return version >= simulatedFirstBadVersion;
        }

        /// <summary>
        /// 表示一筆可執行案例，包含案例目的、版本上限、錯誤起點與預期答案。
        /// </summary>
        /// <param name="Description">案例涵蓋的情境與驗證目的。</param>
        /// <param name="N">案例中的最後一個版本號。</param>
        /// <param name="Bad">案例中第一個錯誤版本，範圍為 <c>[1, N]</c>。</param>
        /// <param name="Expected">預期由 <see cref="FirstBadVersion"/> 找到的版本號。</param>
        private sealed record SampleCase(string Description, int N, int Bad, int Expected);
    }
}
