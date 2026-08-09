namespace leetcode_383
{
    internal class Program
    {
        /// <summary>
        /// 表示一筆符合題目輸入契約的固定案例，包含案例名稱、勒索信、
        /// 雜誌內容與手動推導的預期結果。
        /// </summary>
        /// <param name="Name">顯示於主控台的案例名稱。</param>
        /// <param name="RansomNote">只包含小寫英文字母且長度至少為 1 的勒索信。</param>
        /// <param name="Magazine">只包含小寫英文字母且長度至少為 1 的雜誌內容。</param>
        /// <param name="Expected">勒索信能由雜誌字元構成時為 <see langword="true"/>。</param>
        private readonly record struct SampleCase(
            string Name,
            string RansomNote,
            string Magazine,
            bool Expected);

        /// <summary>
        /// 383. Ransom Note
        /// https://leetcode.com/problems/ransom-note/description/
        /// <para>
        /// Given two strings ransomNote and magazine, return true if ransomNote can be constructed by using the letters from magazine and false otherwise.
        ///
        /// Each letter in magazine can only be used once in ransomNote.
        ///
        /// Example 1:
        /// Input: ransomNote = "a", magazine = "b"
        /// Output: false
        ///
        /// Example 2:
        /// Input: ransomNote = "aa", magazine = "ab"
        /// Output: false
        ///
        /// Example 3:
        /// Input: ransomNote = "aa", magazine = "aab"
        /// Output: true
        ///
        /// Constraints:
        /// - 1 &lt;= ransomNote.length, magazine.length &lt;= 10^5
        /// - ransomNote and magazine consist of lowercase English letters.
        /// </para>
        /// <para>
        /// 383. 贖金信
        /// https://leetcode.cn/problems/ransom-note/description/
        ///
        /// 給定兩個字串 ransomNote 與 magazine，若 ransomNote 能使用 magazine 中的字母構成則回傳 true，否則回傳 false。
        ///
        /// magazine 中的每個字母在 ransomNote 中只能使用一次。
        ///
        /// 範例 1：
        /// 輸入：ransomNote = "a", magazine = "b"
        /// 輸出：false
        ///
        /// 範例 2：
        /// 輸入：ransomNote = "aa", magazine = "ab"
        /// 輸出：false
        ///
        /// 範例 3：
        /// 輸入：ransomNote = "aa", magazine = "aab"
        /// 輸出：true
        ///
        /// 限制條件：
        /// - 1 &lt;= ransomNote.length, magazine.length &lt;= 10^5
        /// - ransomNote 與 magazine 只由小寫英文字母組成。
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 執行七組符合題目小寫英文字母契約的固定案例，分別驗證 List
        /// 搜尋移除法與固定 26 格字母計數法，並輸出每項結果及通過總數。
        /// 此方法不需要外部輸入，也不回傳資料。
        /// </summary>
        private static void RunSamples()
        {
            SampleCase[] sampleCases =
            {
                new("官方範例 1：找不到字母", "a", "b", false),
                new("官方範例 2：重複字母不足", "aa", "ab", false),
                new("官方範例 3：重複字母足夠", "aa", "aab", true),
                new("最小長度且內容相同", "a", "a", true),
                new("雜誌長度不足", "ab", "a", false),
                new("字母順序不同", "abc", "cba", true),
                new("雜誌包含多餘字元", "ab", "adcb", true)
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
        /// 對單一合法案例執行兩種字元消耗解法，將各自的布林結果與預期值比較，
        /// 並輸出穩定的 PASS 或 FAIL 訊息。
        /// </summary>
        /// <param name="caseNumber">從 1 開始顯示的案例編號。</param>
        /// <param name="sampleCase">包含兩個輸入字串及預期布林結果的案例。</param>
        /// <returns>兩種解法中通過預期結果比對的項目數，範圍為 0 到 2。</returns>
        private static int RunSample(int caseNumber, SampleCase sampleCase)
        {
            (string Name, Func<string, string, bool> Solution)[] solutions =
            {
                ("List 搜尋移除", CanConstruct),
                ("固定 26 格計數", CanConstruct2)
            };

            int passedChecks = 0;

            Console.WriteLine($"案例 {caseNumber}：{sampleCase.Name}");
            Console.WriteLine(
                $"  輸入：ransomNote = \"{sampleCase.RansomNote}\", magazine = \"{sampleCase.Magazine}\"");
            Console.WriteLine($"  預期：{sampleCase.Expected}");

            foreach ((string name, Func<string, string, bool> solution) in solutions)
            {
                bool actual = solution(sampleCase.RansomNote, sampleCase.Magazine);
                bool passed = actual == sampleCase.Expected;
                passedChecks += Convert.ToInt32(passed);

                Console.WriteLine($"  {name}：{actual} => {(passed ? "PASS" : "FAIL")}");
            }

            Console.WriteLine();
            return passedChecks;
        }

        /// <summary>
        /// https://www.delftstack.com/zh-tw/howto/csharp/how-to-remove-item-from-list-in-csharp/
        /// https://www.796t.com/content/1550172424.html
        /// C# 使用 RemoveAt() 方法從 List 中刪除元素
        /// RemoveAt() 方法根據該元素的索引號從 List 中刪除該元素。我們已經知道 C# 中的索引以 0 開頭。
        /// 因此，選擇索引號時要小心。此方法的正確語法如下：
        /// 
        /// indexof() ：在字串中從前向後定位字元和字串；所有的返回值都是指在字串的絕對位置，如為空則為- 1
        /// 
        /// https://ithelp.ithome.com.tw/articles/10221926
        /// 
        /// 劫匪信小 雜誌 大
        /// 小字串要存在於大字串裡面
        /// 大字串可以有多餘的單字
        /// 
        /// 每一個英文字母只能用一次 比對到 就去除
        /// 只要劫匪信能比對出來存在於 雜誌中 即可
        /// 使用contain會出錯, 條件有說 magazine 中的每个字符只能在 ransomNote 中使用一次。
        /// 
        /// 解法
        /// 1. 若 magazine 上的字元不夠在 ransomNote 上使用，return false
        /// 2. 將 magazine 及 ransomNote 轉換成 List<char>，這樣就可以使用 IndexOf(s) 及 
        ///    RemoveAt(index)
        /// 3. 判斷 magazine 裡有沒有 ransomNote 要的字元 
        ///    若有的話，就剪貼上去 (magazines.RemoveAt(index))
        ///    若 沒有 的話就 return false，因為不夠用啦～
        /// </summary>
        /// <param name="ransomNote">只包含小寫英文字母且長度至少為 1 的勒索信。</param>
        /// <param name="magazine">只包含小寫英文字母且長度至少為 1 的雜誌內容。</param>
        /// <returns>每個勒索信字元都能各自消耗一個雜誌字元時為 <see langword="true"/>；否則為 <see langword="false"/>。</returns>
        /// <remarks>
        /// 令 r 與 m 分別為兩個字串的長度；最壞時間複雜度為 O(r × m)，
        /// 輔助空間複雜度為 O(r + m)。
        /// </remarks>
        public static bool CanConstruct(string ransomNote, string magazine)
        {
            // 每個雜誌字元最多使用一次，字元總數不足時不可能完成勒索信。
            if (magazine.Length < ransomNote.Length)
            {
                return false;
            }

            List<char> ransomNotes = ransomNote.ToCharArray().ToList();
            List<char> magazines = magazine.ToCharArray().ToList();

            foreach (char letter in ransomNotes)
            {
                int index = magazines.IndexOf(letter);

                if (index >= 0)
                {
                    // 移除已使用的實體字元，讓後續相同字母不能重複使用它。
                    magazines.RemoveAt(index);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 使用固定 26 格整數陣列判斷勒索信能否由雜誌字元構成。
        /// 先統計雜誌中每個小寫英文字母的可用次數，再逐字消耗勒索信需求；
        /// 任一字母數量不足時回傳 <see langword="false"/>，全部需求都滿足時回傳
        /// <see langword="true"/>。時間複雜度為 O(r + m)，輔助空間為 O(1)。
        /// </summary>
        /// <param name="ransomNote">只包含小寫英文字母且長度至少為 1 的勒索信。</param>
        /// <param name="magazine">只包含小寫英文字母且長度至少為 1 的雜誌內容。</param>
        /// <returns>每個勒索信字元都有足夠的雜誌字元可供消耗時為 <see langword="true"/>；否則為 <see langword="false"/>。</returns>
        public static bool CanConstruct2(string ransomNote, string magazine)
        {
            // 每個雜誌字元最多使用一次，字元總數不足時不必建立頻率表。
            if (magazine.Length < ransomNote.Length)
            {
                return false;
            }

            int[] letterCounts = new int[26];

            foreach (char letter in magazine)
            {
                letterCounts[letter - 'a']++;
            }

            foreach (char letter in ransomNote)
            {
                int index = letter - 'a';

                // 計數為零代表這個字母已用完，或雜誌從未提供該字母。
                if (letterCounts[index] == 0)
                {
                    return false;
                }

                letterCounts[index]--;
            }

            return true;
        }
    }
}
