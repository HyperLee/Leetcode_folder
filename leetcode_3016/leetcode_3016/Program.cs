namespace leetcode_3016
{
    internal class Program
    {
        /// <summary>
        /// 3016. Minimum Number of Pushes to Type Word II
        /// https://leetcode.com/problems/minimum-number-of-pushes-to-type-word-ii/description/
        /// <para>
        /// You are given a string word containing lowercase English letters.
        ///
        /// Telephone keypad keys are mapped to distinct collections of lowercase letters. For example, if key 2 maps to ["a","b","c"], one push types "a", two pushes type "b", and three pushes type "c".
        ///
        /// You may remap keys 2 through 9 to distinct collections containing any number of letters, but every letter must map to exactly one key. Find the minimum number of key pushes needed to type word.
        ///
        /// Return the minimum number of pushes needed after remapping the keys.
        ///
        /// Keypad image: https://assets.leetcode.com/uploads/2023/12/26/keypaddesc.png
        /// Note that keys 1, *, #, and 0 do not map to letters.
        ///
        /// Example 1:
        /// Image: https://assets.leetcode.com/uploads/2023/12/26/keypadv1e1.png
        /// Input: word = "abcde"
        /// Output: 5
        /// Explanation: Map "a", "b", "c", "d", and "e" to the first position on keys 2, 3, 4, 5, and 6 respectively. The total is 1 + 1 + 1 + 1 + 1 = 5, and no mapping has a lower cost.
        ///
        /// Example 2:
        /// Image: https://assets.leetcode.com/uploads/2024/08/20/edited.png
        /// Input: word = "xyzxyzxyzxyz"
        /// Output: 12
        /// Explanation: Map "x", "y", and "z" to the first position on keys 2, 3, and 4. The total is 1 * 4 + 1 * 4 + 1 * 4 = 12, which is minimal. Key 9 need not map to a letter; every letter must be mapped, but not every key must be used.
        ///
        /// Example 3:
        /// Image: https://assets.leetcode.com/uploads/2023/12/27/keypadv2.png
        /// Input: word = "aabbccddeeffgghhiiiiii"
        /// Output: 24
        /// Explanation: Map "a" through "g" to the first position on keys 2 through 8, map "i" to the first position on key 9, and map "h" to the second position on key 9. The total is 1 * 2 + 1 * 2 + 1 * 2 + 1 * 2 + 1 * 2 + 1 * 2 + 1 * 2 + 2 * 2 + 6 * 1 = 24, which is minimal.
        ///
        /// Constraints:
        /// - 1 &lt;= word.length &lt;= 10^5
        /// - word consists of lowercase English letters.
        /// </para>
        /// <para>
        /// 3016. 輸入單字所需的最少按鍵次數 II
        /// https://leetcode.cn/problems/minimum-number-of-pushes-to-type-word-ii/description/
        ///
        /// 給定一個只含小寫英文字母的字串 word。
        ///
        /// 電話鍵盤的按鍵會分別映射到不同的小寫字母集合。例如，若按鍵 2 映射到 ["a","b","c"]，按一次可輸入 "a"，按兩次可輸入 "b"，按三次可輸入 "c"。
        ///
        /// 你可以將按鍵 2 到 9 重新映射到互不重複的字母集合，每個集合可含任意數量的字母，但每個字母必須恰好映射到一個按鍵。請找出輸入 word 所需的最少按鍵次數。
        ///
        /// 回傳重新映射按鍵後，輸入 word 所需的最少按鍵次數。
        ///
        /// 鍵盤圖片：https://assets.leetcode.com/uploads/2023/12/26/keypaddesc.png
        /// 請注意，按鍵 1、*、# 和 0 不映射到任何字母。
        ///
        /// 範例 1：
        /// 圖片：https://assets.leetcode.com/uploads/2023/12/26/keypadv1e1.png
        /// 輸入：word = "abcde"
        /// 輸出：5
        /// 解釋：將 "a"、"b"、"c"、"d"、"e" 分別映射到按鍵 2、3、4、5、6 的第一個位置。總成本為 1 + 1 + 1 + 1 + 1 = 5，且沒有成本更低的映射。
        ///
        /// 範例 2：
        /// 圖片：https://assets.leetcode.com/uploads/2024/08/20/edited.png
        /// 輸入：word = "xyzxyzxyzxyz"
        /// 輸出：12
        /// 解釋：將 "x"、"y"、"z" 分別映射到按鍵 2、3、4 的第一個位置。總成本為 1 * 4 + 1 * 4 + 1 * 4 = 12，且為最小值。按鍵 9 不一定要映射字母；所有字母都必須映射，但不必使用每個按鍵。
        ///
        /// 範例 3：
        /// 圖片：https://assets.leetcode.com/uploads/2023/12/27/keypadv2.png
        /// 輸入：word = "aabbccddeeffgghhiiiiii"
        /// 輸出：24
        /// 解釋：將 "a" 到 "g" 分別映射到按鍵 2 到 8 的第一個位置，將 "i" 映射到按鍵 9 的第一個位置，並將 "h" 映射到按鍵 9 的第二個位置。總成本為 1 * 2 + 1 * 2 + 1 * 2 + 1 * 2 + 1 * 2 + 1 * 2 + 1 * 2 + 2 * 2 + 6 * 1 = 24，且為最小值。
        ///
        /// 限制條件：
        /// - 1 &lt;= word.length &lt;= 10^5
        /// - word 只含小寫英文字母。
        /// </para>
        /// </summary>
        /// <remarks>
        /// 執行固定案例，比較三種最少按鍵次數解法；若任一結果不符預期，程序會以非零結束碼結束。
        /// </remarks>
        /// <param name="args">命令列參數；本範例不使用此參數。</param>
        static void Main(string[] args)
        {
            int failedChecks = RunSamples();
            Environment.ExitCode = failedChecks == 0 ? 0 : 1;
        }

        /// <summary>
        /// 執行七組符合題目限制的固定案例，逐一驗證三種解法並回傳失敗項目數。
        /// 測試涵蓋官方範例、最小長度、按鍵成本分層，以及最大輸入長度。
        /// </summary>
        /// <returns>三種解法合計未通過的驗證數；全部正確時為 0。</returns>
        private static int RunSamples()
        {
            (string Name, string Word, int Expected)[] cases =
            [
                ("官方案例 1", "abcde", 5),
                ("官方案例 2", "xyzxyzxyzxyz", 12),
                ("官方案例 3", "aabbccddeeffgghhiiiiii", 24),
                ("最小長度", "a", 1),
                ("跨入第二按鍵層", "abcdefghi", 10),
                ("26 個不同字母", "abcdefghijklmnopqrstuvwxyz", 56),
                ("最大長度重複字母", new string('a', 100_000), 100_000)
            ];

            int passedChecks = 0;
            int totalChecks = cases.Length * 3;

            Console.WriteLine("LeetCode 3016 - Minimum Number of Pushes to Type Word II");
            Console.WriteLine("三種解法對照驗證");
            Console.WriteLine();

            for (int i = 0; i < cases.Length; i++)
            {
                (string name, string word, int expected) = cases[i];
                passedChecks += RunCase(i + 1, name, word, expected);
            }

            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項測試通過");
            return totalChecks - passedChecks;
        }

        /// <summary>
        /// 將單一測試案例交給三種解法，輸出預期值、實際值與驗證結果。
        /// 輸入字串須符合題目限制；回傳值可供總結所有解法的通過數。
        /// </summary>
        /// <param name="caseNumber">從 1 開始的案例編號。</param>
        /// <param name="caseName">案例用途的簡短名稱。</param>
        /// <param name="word">由小寫英文字母組成、長度介於 1 到 100000 的字串。</param>
        /// <param name="expected">此案例的最少按鍵次數。</param>
        /// <returns>此案例通過的解法數，範圍為 0 到 3。</returns>
        private static int RunCase(int caseNumber, string caseName, string word, int expected)
        {
            (string Name, Func<string, int> Solver)[] solutions =
            [
                (nameof(MinimumPushes), MinimumPushes),
                (nameof(MinimumPushesByFrequencyBuckets), MinimumPushesByFrequencyBuckets),
                (nameof(MinimumPushesByRepeatedSelection), MinimumPushesByRepeatedSelection)
            ];

            int passedChecks = 0;

            Console.WriteLine($"案例 {caseNumber}：{caseName}");
            Console.WriteLine($"輸入：word = \"{FormatWord(word)}\" (length = {word.Length})");

            foreach ((string name, Func<string, int> solver) in solutions)
            {
                int actual = solver(word);
                bool passed = actual == expected;
                passedChecks += passed ? 1 : 0;
                Console.WriteLine($"{name}: Expected = {expected}, Actual = {actual} => {(passed ? "PASS" : "FAIL")}");
            }

            Console.WriteLine();
            return passedChecks;
        }

        /// <summary>
        /// 將測試字串整理成適合主控台顯示的形式；短字串完整保留，長字串僅顯示前後各十二字元。
        /// 輸入必須是符合題目限制的非空字串，輸出只用於顯示，不會改變原字串。
        /// </summary>
        /// <param name="word">要顯示的測試字串。</param>
        /// <returns>長度不超過 24 時回傳原字串，否則回傳含省略號的縮寫。</returns>
        private static string FormatWord(string word)
        {
            const int visibleCharacterCount = 12;

            if (word.Length <= visibleCharacterCount * 2)
            {
                return word;
            }

            return $"{word[..visibleCharacterCount]}...{word[^visibleCharacterCount..]}";
        }


        /// <summary>
        /// 計算重新映射按鍵後輸入 <paramref name="word"/> 所需的最少按鍵次數。
        /// 解法先統計 26 個小寫字母的出現頻率並排序，再讓最高頻的字母依序使用成本最低的按鍵位置。
        /// 輸入須為長度 1 到 100000、只含小寫英文字母的字串；輸出為最少按鍵次數且不改變輸入。
        ///
        /// ref:
        /// https://leetcode.cn/problems/minimum-number-of-pushes-to-type-word-ii/solutions/2613399/tan-xin-jian-ji-xie-fa-pythonjavacgo-by-5l4je/
        /// https://leetcode.cn/problems/minimum-number-of-pushes-to-type-word-ii/solutions/2613661/3016-shu-ru-dan-ci-xu-yao-de-zui-shao-an-n2z2/
        /// 排序不等式
        /// https://zh.wikipedia.org/zh-tw/%E6%8E%92%E5%BA%8F%E4%B8%8D%E7%AD%89%E5%BC%8F
        /// 
        /// 注意 題目有說:
        /// 现在允许你将编号为 2 到 9 的按键重新映射到 不同 字母集合。每个按键可以映射到 任意数量 的字母，但每个字母 必须 恰好 映射到 一个 按键上。你需要找到输入字符串 word 所需的 最少 按键次数。
        /// => 也就是 
        /// 有 8 個字母只需要按下一次按鍵, 第二組 8 個字母需要按下兩次按鍵, 第三組 8 個字母需要按下三次按鍵, 第四組字母要按下四次按鍵
        /// 電話鍵盤共 12 個按鍵, 但是 注意 1，*，# 和 0 不能放入字母
        /// 所以 12 - 4 = 8 個按鍵可以放入字母而已
        /// 
        /// 為了達到題目要求, 最少按鍵次數
        /// 所以 出現頻率(次數)高的 數字 要分配在第一組, 只需要按下一次按鍵的即可
        /// 不然給他分配到第二, 三組會造成按鍵次數暴增
        /// 
        /// 頻率(次數)高 -> 按鍵次數少
        /// 反之
        /// 頻率(次數)低 -> 按鍵次數高
        /// 
        /// 出現的頻率(次數) * 按鍵次數 => 題目所求
        /// </summary>
        /// <param name="word">由小寫英文字母組成、長度介於 1 到 100000 的字串。</param>
        /// <returns>在最佳按鍵映射下輸入整個字串所需的最少按鍵次數。</returns>
        public static int MinimumPushes(string word)
        {
            int[] frequencies = new int[26];
            foreach (char c in word)
            {
                frequencies[c - 'a']++;
            }

            Array.Sort(frequencies);

            int minimumPushes = 0;
            for (int rank = 0; rank < frequencies.Length; rank++)
            {
                // 每八個字母進入下一個按鍵成本層：第 1～8 名按一次，第 9～16 名按兩次，依此類推。
                int pushCost = (rank / 8) + 1;
                minimumPushes += frequencies[frequencies.Length - 1 - rank] * pushCost;
            }

            return minimumPushes;
        }

        /// <summary>
        /// 使用頻率桶計算重新映射按鍵後輸入 <paramref name="word"/> 的最少按鍵次數。
        /// 解法記錄每一種出現次數對應多少個字母，再由高頻桶往低頻桶配置成本由低到高的按鍵位置。
        /// 輸入須為長度 1 到 100000、只含小寫英文字母的字串；輸出為最少按鍵次數且不改變輸入。
        /// </summary>
        /// <param name="word">由小寫英文字母組成、長度介於 1 到 100000 的字串。</param>
        /// <returns>在最佳按鍵映射下輸入整個字串所需的最少按鍵次數。</returns>
        public static int MinimumPushesByFrequencyBuckets(string word)
        {
            int[] frequencies = new int[26];
            foreach (char c in word)
            {
                frequencies[c - 'a']++;
            }

            int[] frequencyBuckets = new int[word.Length + 1];
            foreach (int frequency in frequencies)
            {
                if (frequency > 0)
                {
                    frequencyBuckets[frequency]++;
                }
            }

            int rank = 0;
            int minimumPushes = 0;

            // 從最高頻率往下展開桶，效果等同依頻率遞減排序，但不需要比較元素。
            for (int frequency = frequencyBuckets.Length - 1; frequency >= 1; frequency--)
            {
                for (int letterCount = 0; letterCount < frequencyBuckets[frequency]; letterCount++)
                {
                    int pushCost = (rank / 8) + 1;
                    minimumPushes += frequency * pushCost;
                    rank++;
                }
            }

            return minimumPushes;
        }

        /// <summary>
        /// 使用重複選取最高頻字母的方式，計算重新映射按鍵後輸入 <paramref name="word"/> 的最少按鍵次數。
        /// 每一輪直接掃描 26 個頻率並挑出尚未配置的最大值，依排名配置按鍵成本，作為不使用排序的直觀基準。
        /// 輸入須為長度 1 到 100000、只含小寫英文字母的字串；輸出為最少按鍵次數且不改變輸入。
        /// </summary>
        /// <param name="word">由小寫英文字母組成、長度介於 1 到 100000 的字串。</param>
        /// <returns>在最佳按鍵映射下輸入整個字串所需的最少按鍵次數。</returns>
        public static int MinimumPushesByRepeatedSelection(string word)
        {
            int[] frequencies = new int[26];
            foreach (char c in word)
            {
                frequencies[c - 'a']++;
            }

            int minimumPushes = 0;

            for (int rank = 0; rank < frequencies.Length; rank++)
            {
                int highestFrequency = 0;
                int selectedIndex = -1;

                // 每輪挑出一個最高頻字母；將它清零後，下一輪自然會選到下一名。
                for (int i = 0; i < frequencies.Length; i++)
                {
                    if (frequencies[i] > highestFrequency)
                    {
                        highestFrequency = frequencies[i];
                        selectedIndex = i;
                    }
                }

                if (selectedIndex == -1)
                {
                    break;
                }

                int pushCost = (rank / 8) + 1;
                minimumPushes += highestFrequency * pushCost;
                frequencies[selectedIndex] = 0;
            }

            return minimumPushes;
        }

    }
}