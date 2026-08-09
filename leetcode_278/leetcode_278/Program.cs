namespace leetcode_278
{
    internal class Program
    {
        private static int simulatedFirstBadVersion;

        /// <summary>
        /// 278. First Bad Version
        /// https://leetcode.com/problems/first-bad-version/description/
        /// <para>
        /// You are a product manager and currently leading a team to develop a new product. Unfortunately, the latest version of your product fails the quality check. Since each version is developed based on the previous version, all versions after a bad version are also bad.
        ///
        /// Suppose you have n versions [1, 2, ..., n] and you want to find the first bad one, which causes all following versions to be bad.
        ///
        /// You are given an API bool isBadVersion(version), which returns whether version is bad. Implement a function to find the first bad version. You should minimize the number of calls to the API.
        ///
        /// Example 1:
        /// Input: n = 5, bad = 4
        /// Output: 4
        /// Explanation: isBadVersion(3) returns false, isBadVersion(5) returns true, and isBadVersion(4) returns true. Therefore, 4 is the first bad version.
        ///
        /// Example 2:
        /// Input: n = 1, bad = 1
        /// Output: 1
        ///
        /// Constraints:
        /// - 1 &lt;= bad &lt;= n &lt;= 2^31 - 1
        /// </para>
        /// <para>
        /// 278. 第一個錯誤版本
        /// https://leetcode.cn/problems/first-bad-version/description/
        ///
        /// 你是一位產品經理，目前正帶領團隊開發新產品。不幸的是，產品的最新版本未通過品質檢查。由於每個版本都是基於前一個版本開發，因此錯誤版本之後的所有版本也都是錯誤的。
        ///
        /// 假設共有 n 個版本 [1, 2, ..., n]，你想找出第一個錯誤版本；它會導致後續所有版本都出錯。
        ///
        /// 題目提供 API bool isBadVersion(version)，用來回傳 version 是否為錯誤版本。請實作函式找出第一個錯誤版本，並盡量減少呼叫此 API 的次數。
        ///
        /// 範例 1：
        /// 輸入：n = 5, bad = 4
        /// 輸出：4
        /// 解釋：isBadVersion(3) 回傳 false，isBadVersion(5) 回傳 true，isBadVersion(4) 回傳 true。因此 4 是第一個錯誤版本。
        ///
        /// 範例 2：
        /// 輸入：n = 1, bad = 1
        /// 輸出：1
        ///
        /// 限制條件：
        /// - 1 &lt;= bad &lt;= n &lt;= 2^31 - 1
        /// </para>
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
