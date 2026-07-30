using System.Collections.Generic;

namespace leetcode_242
{
    internal class Program
    {
        private readonly record struct SampleCase(
            string Name,
            string S,
            string T,
            bool Expected);

        /// <summary>
        /// 242. Valid Anagram
        /// https://leetcode.com/problems/valid-anagram/
        /// 242. 有效的字母異位詞
        /// https://leetcode.cn/problems/valid-anagram/
        /// 
        /// 比對兩輸入字串是否相同
        /// 1.出現字母
        /// 2.每次字母出現的頻率(次數)
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 執行六組符合題目小寫英文字母契約的固定案例，分別驗證固定陣列與
        /// Dictionary 兩種計數解法，並輸出每項結果及通過總數。
        /// </summary>
        private static void RunSamples()
        {
            SampleCase[] sampleCases =
            {
                new("官方範例 1：可重新排列", "anagram", "nagaram", true),
                new("官方範例 2：字母不同", "rat", "car", false),
                new("重複字母頻率不同", "aacc", "ccac", false),
                new("字串長度不同", "ab", "a", false),
                new("最小長度且內容相同", "a", "a", true),
                new(
                    "完整小寫字母反向排列",
                    "abcdefghijklmnopqrstuvwxyz",
                    "zyxwvutsrqponmlkjihgfedcba",
                    true)
            };

            int passedChecks = 0;

            for (int index = 0; index < sampleCases.Length; index++)
            {
                passedChecks += RunSample(index + 1, sampleCases[index]);
            }

            int totalChecks = sampleCases.Length * 2;
            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項演算法驗證通過");
        }

        /// <summary>
        /// 對單一案例執行兩種字母頻率計數方法，將實際布林結果與預期值比較，
        /// 並輸出穩定的 PASS 或 FAIL 訊息。
        /// </summary>
        /// <param name="caseNumber">從 1 開始顯示的案例編號。</param>
        /// <param name="sampleCase">包含案例名稱、兩個輸入字串與預期結果的測試資料。</param>
        /// <returns>兩種解法中通過預期結果比對的項目數，範圍為 0 到 2。</returns>
        private static int RunSample(int caseNumber, SampleCase sampleCase)
        {
            (string Name, Func<string, string, bool> Solution)[] solutions =
            {
                ("固定陣列計數", IsAnagram),
                ("Dictionary 計數", IsAnagram2)
            };

            int passedChecks = 0;

            Console.WriteLine($"案例 {caseNumber}：{sampleCase.Name}");
            Console.WriteLine($"  輸入：s = \"{sampleCase.S}\", t = \"{sampleCase.T}\"");
            Console.WriteLine($"  預期：{sampleCase.Expected}");

            foreach ((string name, Func<string, string, bool> solution) in solutions)
            {
                bool actual = solution(sampleCase.S, sampleCase.T);
                bool passed = actual == sampleCase.Expected;
                passedChecks += Convert.ToInt32(passed);

                Console.WriteLine($"  {name}：{actual} => {(passed ? "PASS" : "FAIL")}");
            }

            Console.WriteLine();
            return passedChecks;
        }

        /// <summary>
        /// 使用固定 26 格陣列判斷兩個小寫英文字串是否為字母異位詞。
        /// 同一輪對第一個字串的字母計數加一、對第二個字串減一；
        /// 長度相同且所有頻率差最後皆為零時回傳 <see langword="true"/>。
        /// 時間複雜度為 O(n)，輔助空間複雜度為 O(1)。
        /// </summary>
        /// <param name="s">第一個非空字串，內容只包含小寫英文字母。</param>
        /// <param name="t">第二個非空字串，內容只包含小寫英文字母。</param>
        /// <returns>兩字串包含完全相同的字母及出現次數時為 <see langword="true"/>；否則為 <see langword="false"/>。</returns>
        public static bool IsAnagram(string s, string t)
        {
            // 字母異位詞的字元總數必須相同，長度不同時不必再建立計數。
            if (s.Length != t.Length)
            {
                return false;
            }

            int[] charCount = new int[26];

            // 同一索引保存 s 與 t 的頻率差，完全抵銷後應回到零。
            for (int i = 0; i < s.Length; i++)
            {
                charCount[s[i] - 'a']++;
                charCount[t[i] - 'a']--;
            }

            for (int i = 0; i < charCount.Length; i++)
            {
                // 任一頻率差未歸零，代表至少一個字母的出現次數不同。
                if (charCount[i] != 0)
                {
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// 使用 Dictionary 記錄第一個小寫英文字串的字母頻率，再由第二個字串
        /// 逐字抵銷；長度相同、沒有缺少的字母且所有計數皆歸零時回傳
        /// <see langword="true"/>。時間複雜度為 O(n)，輔助空間複雜度為 O(k)，
        /// 其中 k 是不同字母數且在題目限制下最多為 26。
        /// </summary>
        /// <param name="s">第一個非空字串，內容只包含小寫英文字母。</param>
        /// <param name="t">第二個非空字串，內容只包含小寫英文字母。</param>
        /// <returns>兩字串包含完全相同的字母及出現次數時為 <see langword="true"/>；否則為 <see langword="false"/>。</returns>
        public static bool IsAnagram2(string s, string t)
        {
            // 字母異位詞的字元總數必須相同。
            if (s.Length != t.Length)
            {
                return false;
            }

            Dictionary<char, int> dic = new Dictionary<char, int>();

            // 先建立 s 的完整頻率表，再讓 t 的每個字母逐一抵銷。
            foreach (char c in s)
            {
                if (dic.ContainsKey(c))
                {
                    dic[c]++;
                }
                else
                {
                    dic.Add(c, 1);
                }
            }

            foreach (char c in t)
            {
                if (dic.ContainsKey(c))
                {
                    dic[c]--;
                }
                else
                {
                    // t 出現 s 未曾記錄的字母時，不可能完全抵銷。
                    return false;
                }
            }

            foreach (var item in dic)
            {
                // 任一計數未歸零，代表該字母在兩字串中的出現次數不同。
                if (item.Value != 0)
                {
                    return false;
                }
            }

            return true;
        }
    }

}
