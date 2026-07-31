namespace leetcode_1002
{
    internal class Program
    {
        /// <summary>
        /// 1002. Find Common Characters
        /// https://leetcode.com/problems/find-common-characters/description/?envType=daily-question&envId=2024-06-05
        /// 1002. 查找共用字符
        /// https://leetcode.cn/problems/find-common-characters/description/
        /// </summary>
        /// <remarks>
        /// 不需要命令列參數；主程式會以六組合法案例驗證三種解法，
        /// 答案比較忽略排列順序，但保留每個共同字元的重複次數。
        /// </remarks>
        /// <param name="args">未使用的命令列參數。</param>
        static void Main(string[] args)
        {
            SampleCase[] cases =
            [
                new("官方範例一", ["bella", "label", "roller"], ["e", "l", "l"]),
                new("官方範例二", ["cool", "lock", "cook"], ["c", "o"]),
                new("最小合法輸入", ["a"], ["a"]),
                new("單一字串保留重複字元", ["aabb"], ["a", "a", "b", "b"]),
                new("沒有共同字元", ["abc", "def"], []),
                new("以最少出現次數為準", ["aaab", "aab", "aaa"], ["a", "a"])
            ];

            Solver[] solvers =
            [
                new("固定長度計數陣列", CommonChars),
                new("Dictionary 頻率交集", CommonCharsWithDictionary),
                new("候選清單逐字移除", CommonCharsWithCandidateList)
            ];

            int passedChecks = 0;

            Console.WriteLine("LeetCode 1002：查找共用字元");
            Console.WriteLine();

            for (int i = 0; i < cases.Length; i++)
            {
                passedChecks += RunTestCase(i + 1, cases[i], solvers);
            }

            int totalChecks = cases.Length * solvers.Length;
            Console.WriteLine($"總結：{passedChecks}/{totalChecks} 項驗證通過");
        }

        /// <summary>
        /// 執行單一固定案例的所有解法，並輸出輸入、預期值、各解法結果及 PASS/FAIL。
        /// 輸入須為符合題目限制的案例與至少一個解法；答案以字元多重集合比較，
        /// 因此忽略排列順序但保留重複次數，最後回傳本案例通過的解法數量。
        /// </summary>
        /// <param name="caseNumber">從 1 開始、供 console 顯示的案例編號。</param>
        /// <param name="sample">包含案例名稱、合法輸入與手算預期值的固定案例。</param>
        /// <param name="solvers">要執行的解法名稱及函式。</param>
        /// <returns>此案例中結果正確的解法數量。</returns>
        private static int RunTestCase(int caseNumber, SampleCase sample, Solver[] solvers)
        {
            int passedChecks = 0;

            Console.WriteLine($"案例 {caseNumber}：{sample.Name}");
            Console.WriteLine($"輸入：words = {FormatWords(sample.Words)}");
            Console.WriteLine($"預期：{FormatCharacters(sample.Expected)}");

            foreach (Solver solver in solvers)
            {
                IList<string> actual = solver.Solve(sample.Words);
                bool passed = HaveSameCharacters(sample.Expected, actual);

                if (passed)
                {
                    passedChecks++;
                }

                Console.WriteLine($"解法：{solver.Name}");
                Console.WriteLine($"實際：{FormatCharacters(actual)}");
                Console.WriteLine($"結果：{(passed ? "PASS" : "FAIL")}");
            }

            Console.WriteLine();
            return passedChecks;
        }

        /// <summary>
        /// 比較兩個字元集合是否包含相同的字元及相同的重複次數。
        /// 輸入可採任意排列順序；方法分別排序副本後進行序列比較，
        /// 不修改任一輸入，並回傳兩者是否為相同的字元多重集合。
        /// </summary>
        /// <param name="expected">手算的預期共同字元。</param>
        /// <param name="actual">演算法實際回傳的共同字元。</param>
        /// <returns>字元內容及每個字元的出現次數都相同時回傳 true，否則回傳 false。</returns>
        private static bool HaveSameCharacters(IList<string> expected, IList<string> actual)
        {
            return expected.OrderBy(character => character)
                .SequenceEqual(actual.OrderBy(character => character));
        }

        /// <summary>
        /// 將字串陣列轉換成適合 console 與 README 閱讀的格式。
        /// 輸入為合法單字陣列；回傳例如 <c>["bella", "label", "roller"]</c> 的字串，
        /// 並保留原始單字順序。
        /// </summary>
        /// <param name="words">要格式化的單字陣列。</param>
        /// <returns>以雙引號、逗號及方括號組成的顯示字串。</returns>
        private static string FormatWords(string[] words)
        {
            return $"[{string.Join(", ", words.Select(word => $"\"{word}\""))}]";
        }

        /// <summary>
        /// 將共同字元集合排序後轉換成穩定的顯示格式。
        /// 輸入可為空集合且可採任意順序；回傳例如 <c>["e", "l", "l"]</c> 的字串，
        /// 不修改原集合，讓不同解法與 README 使用一致的輸出。
        /// </summary>
        /// <param name="characters">要格式化的共同字元集合。</param>
        /// <returns>依字母排序並以雙引號、逗號及方括號組成的顯示字串。</returns>
        private static string FormatCharacters(IEnumerable<string> characters)
        {
            return $"[{string.Join(", ", characters.OrderBy(character => character)
                .Select(character => $"\"{character}\""))}]";
        }

        /// <summary>
        /// 使用固定長度為 26 的計數陣列找出所有字串的共同字元。
        /// 輸入須包含 1 到 100 個非空字串，且每個字串只含小寫英文字母；
        /// 方法統計每個字母在各字串中的出現次數並保留全域最小值，
        /// 最後依最小次數回傳共同字元，包含應保留的重複項目。
        /// </summary>
        /// <remarks>
        /// 時間複雜度為 O(T + 26n)，空間複雜度為 O(26)，其中 T 是所有字串的
        /// 字元總數、n 是字串數量。方法不修改輸入陣列。
        /// 參考：
        /// https://leetcode.cn/problems/find-common-characters/solutions/445468/cha-zhao-chang-yong-zi-fu-by-leetcode-solution/
        /// https://leetcode.cn/problems/find-common-characters/solutions/445914/1002-cha-zhao-chang-yong-zi-fu-ha-xi-fa-jing-dian-/
        /// https://leetcode.cn/problems/find-common-characters/solutions/1458604/by-stormsunshine-83l2/
        /// </remarks>
        /// <param name="words">符合題目限制、只含小寫英文字母的字串陣列。</param>
        /// <returns>所有字串都具有的字元多重集合；每個字元重複最少出現次數。</returns>
        public static IList<string> CommonChars(string[] words)
        {
            int[] minimumFrequencies = new int[26];
            Array.Fill(minimumFrequencies, int.MaxValue);

            foreach (string word in words)
            {
                int[] currentFrequencies = new int[26];

                foreach (char character in word)
                {
                    currentFrequencies[character - 'a']++;
                }

                for (int i = 0; i < minimumFrequencies.Length; i++)
                {
                    // 共同字元只能保留到目前為止每個字串都具備的最少次數。
                    minimumFrequencies[i] = Math.Min(
                        minimumFrequencies[i],
                        currentFrequencies[i]);
                }
            }

            IList<string> letters = new List<string>();

            for (int i = 0; i < minimumFrequencies.Length; i++)
            {
                string letter = ((char)('a' + i)).ToString();

                for (int count = 0; count < minimumFrequencies[i]; count++)
                {
                    letters.Add(letter);
                }
            }

            return letters;
        }

        /// <summary>
        /// 使用 Dictionary 字元頻率表逐步計算所有字串的共同字元。
        /// 輸入須包含 1 到 100 個非空字串，且每個字串只含小寫英文字母；
        /// 方法先統計第一個字串，再把每個候選字元的次數更新為與後續字串的最小值，
        /// 並移除後續字串未出現的字元，最後回傳保留重複次數的共同字元集合。
        /// </summary>
        /// <remarks>
        /// 平均時間複雜度為 O(T + un)，空間複雜度為 O(u)，其中 T 是所有字元總數、
        /// n 是字串數量、u 是任一輸入字串的最大不同字元數。方法不修改輸入陣列。
        /// </remarks>
        /// <param name="words">符合題目限制、只含小寫英文字母的字串陣列。</param>
        /// <returns>所有字串都具有的字元多重集合；回傳順序不影響答案正確性。</returns>
        public static IList<string> CommonCharsWithDictionary(string[] words)
        {
            Dictionary<char, int> minimumFrequencies = CountCharacters(words[0]);

            for (int wordIndex = 1; wordIndex < words.Length; wordIndex++)
            {
                Dictionary<char, int> currentFrequencies = CountCharacters(words[wordIndex]);

                foreach (char character in minimumFrequencies.Keys.ToArray())
                {
                    // 缺少的字元不可能再成為共同字元；存在時則只保留較小頻率。
                    if (!currentFrequencies.TryGetValue(character, out int currentCount))
                    {
                        minimumFrequencies.Remove(character);
                    }
                    else
                    {
                        minimumFrequencies[character] = Math.Min(
                            minimumFrequencies[character],
                            currentCount);
                    }
                }
            }

            IList<string> commonCharacters = new List<string>();

            foreach ((char character, int count) in minimumFrequencies)
            {
                for (int occurrence = 0; occurrence < count; occurrence++)
                {
                    commonCharacters.Add(character.ToString());
                }
            }

            return commonCharacters;
        }

        /// <summary>
        /// 使用候選字元清單逐字串配對並移除不共同的字元。
        /// 輸入須包含 1 到 100 個非空字串，且每個字串只含小寫英文字母；
        /// 方法將第一個字串視為候選多重集合，之後每個字串只能配對其中尚未使用的字元，
        /// 最後把仍存活的候選字元轉成字串並回傳。
        /// </summary>
        /// <remarks>
        /// 若單字最大長度為 L、字串數量為 n，最差時間複雜度為 O(nL²)，
        /// 額外空間複雜度為 O(L)。方法不修改輸入陣列。
        /// </remarks>
        /// <param name="words">符合題目限制、只含小寫英文字母的字串陣列。</param>
        /// <returns>所有字串都具有的字元多重集合，排列順序沿用第一個字串的候選順序。</returns>
        public static IList<string> CommonCharsWithCandidateList(string[] words)
        {
            List<char> candidates = [.. words[0]];

            for (int wordIndex = 1; wordIndex < words.Length; wordIndex++)
            {
                List<char> availableCharacters = [.. words[wordIndex]];

                for (int candidateIndex = candidates.Count - 1; candidateIndex >= 0; candidateIndex--)
                {
                    int matchIndex = availableCharacters.IndexOf(candidates[candidateIndex]);

                    // 每次成功配對也要移除可用字元，才能正確限制重複字元的數量。
                    if (matchIndex >= 0)
                    {
                        availableCharacters.RemoveAt(matchIndex);
                    }
                    else
                    {
                        candidates.RemoveAt(candidateIndex);
                    }
                }
            }

            return candidates.Select(character => character.ToString()).ToList();
        }

        /// <summary>
        /// 統計單一字串內每個字元的出現次數。
        /// 輸入須為只含小寫英文字母的非空字串；回傳以字元為鍵、出現次數為值的
        /// Dictionary，供 Dictionary 頻率交集解法重複使用。
        /// </summary>
        /// <param name="word">要統計且符合題目限制的字串。</param>
        /// <returns>字串中每個不同字元及其出現次數。</returns>
        private static Dictionary<char, int> CountCharacters(string word)
        {
            Dictionary<char, int> frequencies = new();

            foreach (char character in word)
            {
                frequencies[character] = frequencies.GetValueOrDefault(character) + 1;
            }

            return frequencies;
        }

        /// <summary>
        /// 表示一筆固定測試案例。
        /// 輸入包含顯示名稱、符合題目限制的字串陣列與手算預期結果；
        /// 供主程式重複驗證所有解法，不包含額外的執行邏輯。
        /// </summary>
        /// <param name="Name">案例顯示名稱。</param>
        /// <param name="Words">符合題目限制的輸入字串陣列。</param>
        /// <param name="Expected">預期的共同字元多重集合。</param>
        private sealed record SampleCase(string Name, string[] Words, string[] Expected);

        /// <summary>
        /// 將解法顯示名稱與共同字元函式配對。
        /// 函式須接受合法字串陣列並回傳共同字元多重集合；
        /// 主程式透過此資料結構以相同案例執行每一種解法。
        /// </summary>
        /// <param name="Name">解法的繁體中文顯示名稱。</param>
        /// <param name="Solve">接受字串陣列並回傳共同字元集合的函式。</param>
        private sealed record Solver(string Name, Func<string[], IList<string>> Solve);
    }
}