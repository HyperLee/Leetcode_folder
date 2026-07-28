using System.Collections;

namespace leetcode_003
{
    internal class Program
    {
        /// <summary>
        /// 3. Longest Substring Without Repeating Characters
        /// https://leetcode.com/problems/longest-substring-without-repeating-characters/
        /// 3. 無重複字元的最長子字串
        /// https://leetcode.cn/problems/longest-substring-without-repeating-characters/
        ///
        /// English:
        /// Given a string s, find the length of the longest substring without duplicate characters.
        ///
        /// 繁體中文：
        /// 給定一個字串 s，找出其中不含重複字元的最長子字串之長度。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            (string Input, int Expected)[] cases =
            [
                ("", 0),
                ("a", 1),
                ("abcabcbb", 3),
                ("bbbbb", 1),
                ("pwwkew", 3),
                ("dvdf", 3),
                ("a b!a", 4)
            ];

            int passed = 0;
            for (int index = 0; index < cases.Length; index++)
            {
                passed += RunCase(index + 1, cases[index].Input, cases[index].Expected);
            }

            int total = cases.Length * 4;
            Console.WriteLine($"總結：{passed}/{total} 項驗證通過");
        }

        /// <summary>
        /// 執行單一測試案例，分別呼叫四種滑動視窗解法，並輸出每種解法的實際結果與驗證狀態。
        /// </summary>
        /// <param name="caseNumber">從 1 開始顯示的案例編號。</param>
        /// <param name="input">符合題目限制、由 ASCII 字元組成的非 null 字串。</param>
        /// <param name="expected">不含重複字元之最長子字串的預期長度。</param>
        /// <returns>本案例通過驗證的解法數量，範圍為 0 到 4。</returns>
        private static int RunCase(int caseNumber, string input, int expected)
        {
            Console.WriteLine($"案例 {caseNumber}：s = {FormatInput(input)}，預期 = {expected}");

            int passed = 0;
            passed += PrintResult("解法一（BitArray）", LengthOfLongestSubstring(input), expected);
            passed += PrintResult("解法二（List<char>）", LengthOfLongestSubstring2(input), expected);
            passed += PrintResult("解法三（int[]）", LengthOfLongestSubstring3(input), expected);
            passed += PrintResult("解法四（bool[]）", LengthOfLongestSubstring4(input), expected);
            Console.WriteLine();

            return passed;
        }

        /// <summary>
        /// 比較單一解法的實際值與預期值，輸出 PASS 或 FAIL，並將結果轉為可累計的通過數。
        /// </summary>
        /// <param name="solutionName">顯示於主控台的解法名稱。</param>
        /// <param name="actual">解法計算出的最長子字串長度。</param>
        /// <param name="expected">案例的預期長度。</param>
        /// <returns>實際值符合預期時回傳 1，否則回傳 0。</returns>
        private static int PrintResult(string solutionName, int actual, int expected)
        {
            bool isPassed = actual == expected;
            Console.WriteLine($"  {solutionName}：{actual} {(isPassed ? "PASS" : "FAIL")}");
            return isPassed ? 1 : 0;
        }

        /// <summary>
        /// 將測試輸入包在雙引號中，讓空字串、空白與一般 ASCII 字元在主控台輸出中清楚可辨。
        /// </summary>
        /// <param name="input">要顯示的非 null 測試字串。</param>
        /// <returns>以雙引號包住的輸入字串。</returns>
        private static string FormatInput(string input)
        {
            return $"\"{input}\"";
        }

        /// <summary>
        /// 使用 <see cref="BitArray"/> 記錄目前滑動視窗中的 ASCII 字元；遇到重複字元時，
        /// 從左側移除舊字元並跨過前一次出現位置，以求得不含重複字元的最長子字串。
        /// </summary>
        /// <param name="s">由 ASCII 字元組成的非 null 字串，可為空字串。</param>
        /// <returns>不含重複字元之最長連續子字串的長度。</returns>
        public static int LengthOfLongestSubstring(string s)
        {
            int max = 0;
            BitArray map = new BitArray(256, false);
            int l = 0, r = 0;
            int n = s.Length;

            while (r < n)
            {
                if (map[s[r]])
                {
                    max = Math.Max(max, r - l);

                    // 保留右側重複字元，並縮短左界直到跨過它上次出現的位置。
                    while (s[l] != s[r])
                    {
                        map[s[l]] = false;
                        l++;
                    }

                    l++;
                    r++;
                }
                else
                {
                    map[s[r]] = true;
                    r++;
                }
            }

            max = Math.Max(max, r - l);
            return max;
        }

        /// <summary>
        /// 使用 <see cref="List{T}"/> 保存目前滑動視窗中的字元；右側字元重複時逐步移除左側字元，
        /// 直到視窗重新符合字元皆不重複的條件。線性搜尋使最壞時間複雜度為 O(n²)。
        /// </summary>
        /// <param name="s">由 ASCII 字元組成的非 null 字串，可為空字串。</param>
        /// <returns>不含重複字元之最長連續子字串的長度。</returns>
        public static int LengthOfLongestSubstring2(string s)
        {
            if (s.Length == 0)
            {
                return 0;
            }

            List<char> letter = new List<char>();
            int left = 0, right = 0;
            int length = s.Length;
            int count = 0, max = 0;

            while (right < length)
            {
                if (!letter.Contains(s[right]))
                {
                    letter.Add(s[right]);
                    right++;
                    count++;
                }
                else
                {
                    // 右指針先停留，從左側縮窗，直到重複字元被移出後再繼續擴張。
                    letter.Remove(s[left]);
                    left++;
                    count--;
                }

                max = Math.Max(max, count);
            }

            return max;
        }

        /// <summary>
        /// 使用長度為 128 的整數陣列計算目前滑動視窗中各 ASCII 字元的出現次數；
        /// 當新字元計數超過 1 時持續縮短左界，讓視窗恢復為無重複狀態。
        /// </summary>
        /// <param name="s">由 ASCII 字元組成的非 null 字串，可為空字串。</param>
        /// <returns>不含重複字元之最長連續子字串的長度。</returns>
        public static int LengthOfLongestSubstring3(string s)
        {
            char[] chars = s.ToCharArray();
            int n = chars.Length;
            int ans = 0;
            int left = 0;

            int[] count = new int[128];

            for (int right = 0; right < n; right++)
            {
                char c = chars[right];
                count[c]++;

                // 只要新加入的字元仍重複，就持續移除左界字元以恢復視窗不變量。
                while (count[c] > 1)
                {
                    count[chars[left]]--;
                    left++;
                }

                ans = Math.Max(ans, right - left + 1);
            }

            return ans;
        }

        /// <summary>
        /// 使用長度為 128 的布林陣列模擬 ASCII 字元集合；加入右側字元前若發現重複，
        /// 就從左側逐一清除存在標記，直到該字元能安全加入目前視窗。
        /// </summary>
        /// <param name="s">由 ASCII 字元組成的非 null 字串，可為空字串。</param>
        /// <returns>不含重複字元之最長連續子字串的長度。</returns>
        public static int LengthOfLongestSubstring4(string s)
        {
            char[] chars = s.ToCharArray();
            int n = chars.Length;
            int ans = 0;
            int left = 0;
            bool[] exists = new bool[128];

            for (int right = 0; right < n; right++)
            {
                // 重複字元尚在視窗中時，縮短左界並同步清除離開視窗的存在標記。
                while (exists[chars[right]])
                {
                    exists[chars[left]] = false;
                    left++;
                }

                exists[chars[right]] = true;
                ans = Math.Max(ans, right - left + 1);
            }

            return ans;
        }
    }
}