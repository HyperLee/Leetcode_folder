namespace leetcode_678
{
    internal class Program
    {
        /// <summary>
        /// 保存一組可執行驗證資料；輸入須符合題目限制，預期值表示是否能形成有效括號字串。
        /// </summary>
        /// <param name="Name">案例名稱。</param>
        /// <param name="Input">只包含左括號、右括號與星號的非空字串。</param>
        /// <param name="Expected">輸入是否存在一種有效的星號替換方式。</param>
        private sealed record SampleCase(string Name, string Input, bool Expected);

        /// <summary>
        /// 678. Valid Parenthesis String
        /// https://leetcode.com/problems/valid-parenthesis-string/description/?envType=daily-question&envId=2024-04-07
        /// 678. 有效的括號字符串
        /// https://leetcode.cn/problems/valid-parenthesis-string/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RunSamples();
        }

        /// <summary>
        /// 執行十組固定案例，分別呼叫雙堆疊、貪心與動態規劃解法，
        /// 並輸出每種解法的預期值、實際值及是否通過。
        /// 案例輸入皆符合題目限制；方法無輸入參數，若任一檢查失敗則設定非零結束碼。
        /// </summary>
        private static void RunSamples()
        {
            SampleCase[] sampleCases =
            [
                new("基本有效括號", "()", true),
                new("星號視為空字串", "(*)", true),
                new("星號視為左括號", "(*))", true),
                new("單一星號", "*", true),
                new("缺少右括號", "(", false),
                new("缺少左括號", ")", false),
                new("星號補足右括號", "((*)", true),
                new("結尾仍有左括號", "(*(", false),
                new("錯誤前綴無法補救", "())*", false),
                new("長度上限的全星號", new string('*', 100), true)
            ];

            (string Name, Func<string, bool> Solve)[] solutions =
            [
                ("雙堆疊", CheckValidString),
                ("貪心", CheckValidString2),
                ("動態規劃", CheckValidString3)
            ];

            int passedChecks = 0;
            int totalChecks = sampleCases.Length * solutions.Length;

            for (int caseIndex = 0; caseIndex < sampleCases.Length; caseIndex++)
            {
                SampleCase sampleCase = sampleCases[caseIndex];
                Console.WriteLine($"Case {caseIndex + 1}: {sampleCase.Name}");
                Console.WriteLine($"  input:    \"{sampleCase.Input}\"");
                Console.WriteLine($"  expected: {sampleCase.Expected}");

                foreach ((string solutionName, Func<string, bool> solve) in solutions)
                {
                    bool actual = solve(sampleCase.Input);
                    bool passed = actual == sampleCase.Expected;
                    passedChecks += passed ? 1 : 0;
                    Console.WriteLine(
                        $"  {solutionName}: {actual} ({(passed ? "PASS" : "FAIL")})");
                }

                Console.WriteLine();
            }

            Console.WriteLine($"Summary: {passedChecks}/{totalChecks} checks passed.");
            if (passedChecks != totalChecks)
            {
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// 使用兩個索引堆疊判斷字串能否形成有效括號字串。
        /// 左括號與星號分開保存；遇到右括號時優先配對左括號，最後再以位置較後的星號
        /// 補足剩餘左括號，確保配對順序合法。
        /// 輸入須為長度 1 到 100 且只包含 `(`、`)`、`*` 的字串；
        /// 若存在一種星號替換方式可使括號有效則回傳 <see langword="true"/>，否則回傳
        /// <see langword="false"/>。時間複雜度為 O(n)，輔助空間複雜度為 O(n)。
        /// </summary>
        /// <param name="s">符合題目限制、只包含括號與星號的非空字串。</param>
        /// <returns>是否存在一種星號替換方式可形成有效括號字串。</returns>
        public static bool CheckValidString(string s)
        {
            Stack<int> leftParentheses = new();
            Stack<int> wildcards = new();

            for (int index = 0; index < s.Length; index++)
            {
                char character = s[index];
                if (character == '(')
                {
                    leftParentheses.Push(index);
                }
                else if (character == '*')
                {
                    wildcards.Push(index);
                }
                else
                {
                    // 優先消耗真正的左括號，把彈性較高的星號留給後續位置。
                    if (leftParentheses.Count > 0)
                    {
                        leftParentheses.Pop();
                    }
                    else if (wildcards.Count > 0)
                    {
                        wildcards.Pop();
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            // 剩餘星號只能當右括號；其索引必須位於要配對的左括號之後。
            while (leftParentheses.Count > 0 && wildcards.Count > 0)
            {
                if (leftParentheses.Pop() > wildcards.Pop())
                {
                    return false;
                }
            }

            return leftParentheses.Count == 0;
        }

        /// <summary>
        /// 使用貪心上下界判斷字串能否形成有效括號字串。
        /// 掃描每個前綴時，同時維護「最少」與「最多」可能尚未配對的左括號數；
        /// 星號可讓範圍向下、維持或向上擴張，因此不必枚舉所有替換組合。
        /// 輸入須為長度 1 到 100 且只包含 `(`、`)`、`*` 的字串；
        /// 若掃描結束後零仍在可能範圍內則回傳 <see langword="true"/>。
        /// 時間複雜度為 O(n)，輔助空間複雜度為 O(1)。
        /// </summary>
        /// <param name="s">符合題目限制、只包含括號與星號的非空字串。</param>
        /// <returns>是否存在一種星號替換方式可形成有效括號字串。</returns>
        public static bool CheckValidString2(string s)
        {
            int minimumOpen = 0;
            int maximumOpen = 0;

            foreach (char character in s)
            {
                if (character == '(')
                {
                    minimumOpen++;
                    maximumOpen++;
                }
                else if (character == ')')
                {
                    minimumOpen--;
                    maximumOpen--;
                }
                else
                {
                    minimumOpen--;
                    maximumOpen++;
                }

                // 最大可能值仍為負，代表這個前綴的右括號無論如何都過多。
                if (maximumOpen < 0)
                {
                    return false;
                }

                // 未配對左括號數不可能小於零；負下界表示可選擇把星號視為空字串。
                minimumOpen = Math.Max(minimumOpen, 0);
            }

            return minimumOpen == 0;
        }

        /// <summary>
        /// 使用動態規劃判斷字串能否形成有效括號字串。
        /// 狀態 `reachable[i, open]` 表示處理前 i 個字元後，是否可能留下 open 個未配對左括號；
        /// 星號分別依空字串、左括號與右括號進行三種轉移，完整保留所有可達選擇。
        /// 輸入須為長度 1 到 100 且只包含 `(`、`)`、`*` 的字串；
        /// 若處理完整個字串後零個未配對左括號可達則回傳 <see langword="true"/>。
        /// 時間複雜度為 O(n²)，輔助空間複雜度為 O(n²)。
        /// </summary>
        /// <param name="s">符合題目限制、只包含括號與星號的非空字串。</param>
        /// <returns>是否存在一種星號替換方式可形成有效括號字串。</returns>
        public static bool CheckValidString3(string s)
        {
            int length = s.Length;
            bool[,] reachable = new bool[length + 1, length + 1];
            reachable[0, 0] = true;

            for (int processedLength = 0; processedLength < length; processedLength++)
            {
                char character = s[processedLength];

                for (int openCount = 0; openCount <= length; openCount++)
                {
                    if (!reachable[processedLength, openCount])
                    {
                        continue;
                    }

                    if (character == '(' || character == '*')
                    {
                        reachable[processedLength + 1, openCount + 1] = true;
                    }

                    if (character == '*')
                    {
                        // 星號視為空字串時，未配對左括號數保持不變。
                        reachable[processedLength + 1, openCount] = true;
                    }

                    if ((character == ')' || character == '*') && openCount > 0)
                    {
                        reachable[processedLength + 1, openCount - 1] = true;
                    }
                }
            }

            return reachable[length, 0];
        }
    }
}