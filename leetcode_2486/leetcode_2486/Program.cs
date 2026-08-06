namespace leetcode_2486
{
    internal class Program
    {
        /// <summary>
        /// 2486. Append Characters to String to Make Subsequence
        /// https://leetcode.com/problems/append-characters-to-string-to-make-subsequence/?envType=daily-question&envId=2024-06-03
        /// 2486. 追加字符以获得子序列
        /// https://leetcode.cn/problems/append-characters-to-string-to-make-subsequence/description/
        /// </summary>
        /// <summary>
        /// 執行六組固定案例，分別驗證線性雙指標與位置索引二分搜尋兩種解法。
        /// 每個案例都會比較預期值與實際值；全部通過時回傳 0，否則回傳 1。
        /// </summary>
        /// <param name="args">保留 console entry point 的參數位置；本測試不需要命令列輸入。</param>
        static int Main(string[] args)
        {
            SampleCase[] samples =
            [
                new("官方案例 1", "coaching", "coding", 4),
                new("官方案例 2", "abcde", "a", 0),
                new("官方案例 3", "z", "abcde", 5),
                new("非連續完整匹配", "abcde", "ace", 0),
                new("重複字元部分匹配", "aabb", "aaab", 2),
                new("完全沒有可匹配首字元", "xyz", "abc", 3)
            ];

            int passedChecks = 0;
            int totalChecks = samples.Length * 2;

            foreach (SampleCase sample in samples)
            {
                Console.WriteLine($"案例：{sample.Name}");
                Console.WriteLine($"  s = \"{sample.S}\", t = \"{sample.T}\", Expected = {sample.Expected}");

                int actual = AppendCharacters(sample.S, sample.T);
                bool passed = actual == sample.Expected;
                passedChecks += passed ? 1 : 0;
                Console.WriteLine($"  AppendCharacters: Actual = {actual} => {(passed ? "PASS" : "FAIL")}");

                int actual2 = AppendCharacters2(sample.S, sample.T);
                bool passed2 = actual2 == sample.Expected;
                passedChecks += passed2 ? 1 : 0;
                Console.WriteLine($"  AppendCharacters2: Actual = {actual2} => {(passed2 ? "PASS" : "FAIL")}");
            }

            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
            return passedChecks == totalChecks ? 0 : 1;
        }

        /// <summary>
        /// Main 使用的固定驗證案例，保存輸入字串與兩種解法共用的預期結果。
        /// </summary>
        private sealed record SampleCase(string Name, string S, string T, int Expected);


        /// <summary>
        /// https://leetcode.cn/problems/append-characters-to-string-to-make-subsequence/solutions/1993448/tan-xin-pi-pei-by-endlesscheng-d6eq/
        /// https://leetcode.cn/problems/append-characters-to-string-to-make-subsequence/solutions/2602537/2486-zhui-jia-zi-fu-yi-huo-de-zi-xu-lie-ghzhg/
        /// 
        /// sIndex 與 tIndex
        /// 分別為s與t的下標位置
        /// 初始時候都為0
        /// 
        /// 當s[sIndex] == t[tIndex], 此時 sIndex 與 tIndex 都向右移動
        /// 因為兩個字母相同
        /// 
        /// 反之當 s[sIndex] != t[tIndex] 此時 sIndex向右移動
        /// 
        /// 遍歷整個輸入字串s長度, 
        /// 
        /// 最後結果計算為 輸入字串t 
        /// 總長度扣除兩者相同長度 
        /// n - tIndex
        /// 不相同者字母即為需要加入s中的長度
        /// </summary>
        /// <remarks>
        /// 找到的匹配內容一定是 t 的前綴；未匹配的 t 後綴就是必須附加到 s 尾端的字元數。
        /// 本解法只使用兩個索引，因此時間複雜度為 O(m + n)，額外空間複雜度為 O(1)。
        /// </remarks>
        /// <param name="s">原始字串，只包含小寫英文字母。</param>
        /// <param name="t">要成為 s 子序列的目標字串，只包含小寫英文字母。</param>
        /// <returns>讓 t 成為 s 子序列所需附加的最少字元數。</returns>
        public static int AppendCharacters(string s, string t)
        {
            int m = s.Length, n = t.Length;
            int sIndex = 0, tIndex = 0;

            while (sIndex < m && tIndex < n)
            {
                if (s[sIndex] == t[tIndex])
                {
                    // 匹配後消耗 t 的一個前綴字元；sIndex 每次迴圈都會前進。
                    tIndex++;
                }

                // 不匹配時只能跳過 s 的目前字元，不能跳過 t 的目標字元。
                sIndex++;
            }

            return n - tIndex;
        }

        /// <summary>
        /// 使用字元位置索引與 lower-bound 二分搜尋，找出 t 的最長前綴是否能依序出現在 s 中。
        /// </summary>
        /// <remarks>
        /// 先為 s 的每個小寫字元保存所有出現位置，再對 t 的每個字元尋找不小於目前搜尋游標的第一個位置。
        /// 這個方法的建表與查詢時間複雜度為 O(m + n log m)，額外空間複雜度為 O(m)。
        /// </remarks>
        /// <param name="s">原始字串，只包含小寫英文字母。</param>
        /// <param name="t">要成為 s 子序列的目標字串，只包含小寫英文字母。</param>
        /// <returns>讓 t 成為 s 子序列所需附加的最少字元數。</returns>
        public static int AppendCharacters2(string s, string t)
        {
            List<int>[] positions = BuildCharacterPositions(s);
            int nextSearchIndex = 0;
            int tIndex = 0;

            while (tIndex < t.Length)
            {
                List<int> candidatePositions = positions[t[tIndex] - 'a'];
                int positionIndex = LowerBound(candidatePositions, nextSearchIndex);

                if (positionIndex == candidatePositions.Count)
                {
                    // t 的前綴已無法在 s 中延伸，剩餘字元必須全部附加。
                    break;
                }

                // 只接受游標之後的索引，確保選出的字元順序仍是子序列順序。
                nextSearchIndex = candidatePositions[positionIndex] + 1;
                tIndex++;
            }

            return t.Length - tIndex;
        }

        /// <summary>
        /// 建立 26 組遞增位置清單，記錄 s 中每個小寫英文字元出現的索引。
        /// </summary>
        /// <param name="s">要建立索引的原始字串，只包含小寫英文字母。</param>
        /// <returns>以字母編號 0 到 25 對應的字元位置清單陣列。</returns>
        private static List<int>[] BuildCharacterPositions(string s)
        {
            List<int>[] positions = new List<int>[26];
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = [];
            }

            for (int i = 0; i < s.Length; i++)
            {
                positions[s[i] - 'a'].Add(i);
            }

            return positions;
        }

        /// <summary>
        /// 在已排序的位置清單中，找出第一個大於或等於 target 的索引。
        /// </summary>
        /// <param name="positions">按照字元出現順序排列的位置清單。</param>
        /// <param name="target">可接受位置的下限。</param>
        /// <returns>第一個不小於 target 的清單索引；若不存在則回傳 positions.Count。</returns>
        private static int LowerBound(IReadOnlyList<int> positions, int target)
        {
            int left = 0;
            int right = positions.Count;

            while (left < right)
            {
                int middle = left + (right - left) / 2;

                // [0, left) 小於 target，[right, Count) 大於或等於 target。
                if (positions[middle] < target)
                {
                    left = middle + 1;
                }
                else
                {
                    right = middle;
                }
            }

            return left;
        }
    }
}