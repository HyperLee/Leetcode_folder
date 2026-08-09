using System.Text;

namespace leetcode_726
{
    internal class Program
    {
        /// <summary>
        /// 726. Number of Atoms
        /// https://leetcode.com/problems/number-of-atoms/description/
        /// <para>
        /// Given a string formula representing a chemical formula, return the count of each atom.
        ///
        /// An atomic element always starts with an uppercase character, followed by zero or more lowercase letters representing its name.
        ///
        /// One or more digits representing that element's count may follow if the count is greater than 1. If the count is 1, no digits follow.
        /// - For example, "H2O" and "H2O2" are possible, but "H1O2" is impossible.
        ///
        /// Two formulas concatenated together produce another formula.
        /// - For example, "H2O2He3Mg4" is also a formula.
        ///
        /// A formula placed in parentheses, followed by an optional count, is also a formula.
        /// - For example, "(H2O2)" and "(H2O2)3" are formulas.
        ///
        /// Return the count of all elements as a string in this form: the first name in sorted order, followed by its count if greater than 1; then the second name in sorted order, followed by its count if greater than 1; and so on.
        ///
        /// The test cases are generated so that all values in the output fit in a 32-bit integer.
        ///
        /// Example 1:
        /// Input: formula = "H2O"
        /// Output: "H2O"
        /// Explanation: The element counts are {'H': 2, 'O': 1}.
        ///
        /// Example 2:
        /// Input: formula = "Mg(OH)2"
        /// Output: "H2MgO2"
        /// Explanation: The element counts are {'H': 2, 'Mg': 1, 'O': 2}.
        ///
        /// Example 3:
        /// Input: formula = "K4(ON(SO3)2)2"
        /// Output: "K4N2O14S4"
        /// Explanation: The element counts are {'K': 4, 'N': 2, 'O': 14, 'S': 4}.
        ///
        /// Constraints:
        /// - 1 &lt;= formula.length &lt;= 1000
        /// - formula consists of English letters, digits, '(', and ')'.
        /// - formula is always valid.
        /// </para>
        /// <para>
        /// 726. 原子的數量
        /// https://leetcode.cn/problems/number-of-atoms/description/
        ///
        /// 給定表示化學式的字串 formula，回傳每種原子的數量。
        ///
        /// 原子元素名稱一定以大寫字母開頭，後接零個或多個小寫字母。
        ///
        /// 若該元素數量大於 1，後面可以接一個或多個表示數量的數字；若數量為 1，則不接數字。
        /// - 例如，"H2O" 與 "H2O2" 是可能的形式，但 "H1O2" 不可能。
        ///
        /// 兩個化學式串接在一起會形成另一個化學式。
        /// - 例如，"H2O2He3Mg4" 也是化學式。
        ///
        /// 放在括號中的化學式，後面可選擇性加上數量，也是一個化學式。
        /// - 例如，"(H2O2)" 與 "(H2O2)3" 都是化學式。
        ///
        /// 依下列形式將所有元素的數量回傳為字串：先放按字母順序排列的第一個名稱，若其數量大於 1 則接上數量；再放第二個名稱與其大於 1 時的數量，依此類推。
        ///
        /// 測試案例保證輸出中的所有數值都在 32 位元整數範圍內。
        ///
        /// 範例 1：
        /// 輸入：formula = "H2O"
        /// 輸出："H2O"
        /// 解釋：各元素數量為 {'H': 2, 'O': 1}。
        ///
        /// 範例 2：
        /// 輸入：formula = "Mg(OH)2"
        /// 輸出："H2MgO2"
        /// 解釋：各元素數量為 {'H': 2, 'Mg': 1, 'O': 2}。
        ///
        /// 範例 3：
        /// 輸入：formula = "K4(ON(SO3)2)2"
        /// 輸出："K4N2O14S4"
        /// 解釋：各元素數量為 {'K': 4, 'N': 2, 'O': 14, 'S': 4}。
        ///
        /// 限制條件：
        /// - 1 &lt;= formula.length &lt;= 1000
        /// - formula 由英文字母、數字、'(' 與 ')' 組成。
        /// - formula 永遠有效。
        /// </para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            SampleResult[] results = RunSamples();
            int passedCases = 0;

            for (int index = 0; index < results.Length; index++)
            {
                SampleResult result = results[index];

                if (result.Passed)
                {
                    passedCases++;
                }

                Console.WriteLine($"案例 {index + 1}：{result.Name}");
                Console.WriteLine($"公式：{result.Formula}");
                Console.WriteLine($"預期：{result.Expected}");
                Console.WriteLine($"實際：{result.Actual} => {(result.Passed ? "PASS" : "FAIL")}");
                Console.WriteLine();
            }

            Console.WriteLine($"總結：{passedCases}/{results.Length} 筆測試通過");
        }

        /// <summary>
        /// 執行固定的化學式解析案例，逐一呼叫堆疊解法並建立預期值與實際值的比對結果。
        /// 輸入案例皆為符合題目文法的有效化學式，涵蓋基本元素、括號、巢狀括號、
        /// 省略倍率與多位數倍率；方法本身不進行主控台輸出。
        /// </summary>
        /// <returns>依案例宣告順序排列的驗證結果陣列。</returns>
        private static SampleResult[] RunSamples()
        {
            SampleCase[] samples =
            [
                new("官方範例一：基本元素與數量", "H2O", "H2O"),
                new("官方範例二：括號群組", "Mg(OH)2", "H2MgO2"),
                new("官方範例三：巢狀括號", "K4(ON(SO3)2)2", "K4N2O14S4"),
                new("多字母元素與多位數數量", "Be32", "Be32"),
                new("多個化學式片段串接", "H2O2He3Mg4", "H2He3Mg4O2"),
                new("括號後省略倍率", "(OH)", "HO"),
                new("多層括號與不同倍率", "((H)2(O)3)4", "H8O12")
            ];

            SampleResult[] results = new SampleResult[samples.Length];

            for (int index = 0; index < samples.Length; index++)
            {
                SampleCase sample = samples[index];
                string actual = CountOfAtoms(sample.Formula);
                results[index] = new SampleResult(
                    sample.Name,
                    sample.Formula,
                    sample.Expected,
                    actual);
            }

            return results;
        }

        /// <summary>
        /// 使用「堆疊搭配每層元素計數字典」解析化學式。
        /// 遇到左括號時建立新的計數層，遇到右括號時將該層乘上後續倍率並合併回外層；
        /// 一般元素則直接累加至目前層。輸入必須是符合題目文法的非空有效化學式。
        /// </summary>
        /// <param name="formula">只包含英文字母、數字與括號，且文法有效的化學式。</param>
        /// <returns>依元素名稱字典序排列，並省略數量 1 的標準化原子計數字串。</returns>
        public static string CountOfAtoms(string formula)
        {
            int i = 0;
            int n = formula.Length;

            Stack<Dictionary<string, int>> stack = new();
            stack.Push(new Dictionary<string, int>());

            while (i < n)
            {
                char ch = formula[i];
                if (ch == '(')
                {
                    i++;

                    // 每個左括號代表新的作用域，先獨立累計該層的元素數量。
                    stack.Push(new Dictionary<string, int>());
                }
                else if (ch == ')')
                {
                    i++;

                    int num = 0;
                    while (i < n && char.IsNumber(formula[i]))
                    {
                        num = num * 10 + formula[i++] - '0';
                    }

                    if (num == 0)
                    {
                        // 右括號後未指定倍率時，依題目規則視為 1。
                        num = 1;
                    }

                    Dictionary<string, int> groupCounts = stack.Pop();
                    Dictionary<string, int> outerCounts = stack.Peek();

                    // 右括號結束目前層：先套用括號倍率，再累加回上一層。
                    foreach (KeyValuePair<string, int> pair in groupCounts)
                    {
                        string atom = pair.Key;
                        int multipliedCount = pair.Value * num;

                        if (outerCounts.ContainsKey(atom))
                        {
                            outerCounts[atom] += multipliedCount;
                        }
                        else
                        {
                            outerCounts.Add(atom, multipliedCount);
                        }
                    }
                }
                else
                {
                    StringBuilder atomBuilder = new();
                    atomBuilder.Append(formula[i++]);

                    // 元素名稱以大寫字母起始，後面可接零個或多個小寫字母。
                    while (i < n && char.IsLower(formula[i]))
                    {
                        atomBuilder.Append(formula[i++]);
                    }

                    int num = 0;
                    while (i < n && char.IsNumber(formula[i]))
                    {
                        num = num * 10 + formula[i++] - '0';
                    }

                    if (num == 0)
                    {
                        num = 1;
                    }

                    string atom = atomBuilder.ToString();
                    Dictionary<string, int> currentCounts = stack.Peek();

                    if (currentCounts.ContainsKey(atom))
                    {
                        currentCounts[atom] += num;
                    }
                    else
                    {
                        currentCounts.Add(atom, num);
                    }
                }
            }

            Dictionary<string, int> dictionary = stack.Pop();
            List<KeyValuePair<string, int>> pairs = new List<KeyValuePair<string, int>>(dictionary);

            // 題目要求依元素名稱的字典序輸出，再接上大於 1 的數量。
            pairs.Sort((p1, p2) => p1.Key.CompareTo(p2.Key));
            StringBuilder resultBuilder = new();

            foreach (KeyValuePair<string, int> pair in pairs)
            {
                string atom = pair.Key;
                int count = pair.Value;
                resultBuilder.Append(atom);

                if (count > 1)
                {
                    resultBuilder.Append(count);
                }
            }

            return resultBuilder.ToString();
        }

        /// <summary>
        /// 從指定索引讀取一個元素名稱：先接受一個大寫字母，再連續收集後續小寫字母。
        /// 輸入索引必須指向有效化學式中的元素起始位置，方法只回傳名稱，不回傳更新後索引。
        /// </summary>
        /// <param name="i">元素名稱的起始索引。</param>
        /// <param name="n">允許讀取的化學式長度上限。</param>
        /// <param name="formula">符合題目文法的化學式。</param>
        /// <returns>由一個大寫字母與其後零個或多個小寫字母組成的元素名稱。</returns>
        public static string ParseAtom(int i, int n, string formula)
        {
            StringBuilder atomBuilder = new();
            atomBuilder.Append(formula[i++]);

            while (i < n && char.IsLower(formula[i]))
            {
                atomBuilder.Append(formula[i++]);
            }

            return atomBuilder.ToString();
        }

        /// <summary>
        /// 從指定索引讀取連續數字並轉換為元素或括號群組的倍率。
        /// 輸入索引應指向可能的數量位置；若起點已是最後位置或不是數字，則依題目規則回傳預設倍率 1。
        /// 此方法只回傳倍率，不回傳解析完成後的索引。
        /// </summary>
        /// <param name="i">可能出現倍率的起始索引。</param>
        /// <param name="n">允許讀取的化學式長度上限。</param>
        /// <param name="formula">符合題目文法的化學式。</param>
        /// <returns>解析到的正整數倍率；沒有可解析數字時回傳 1。</returns>
        public static int ParseNum(int i, int n, string formula)
        {
            if (i == n - 1 || !char.IsNumber(formula[i]))
            {
                return 1;
            }

            int num = 0;
            while (i < n && char.IsNumber(formula[i]))
            {
                num = num * 10 + formula[i++] - '0';
            }

            return num;
        }

        private sealed record SampleCase(string Name, string Formula, string Expected);

        private sealed record SampleResult(
            string Name,
            string Formula,
            string Expected,
            string Actual)
        {
            public bool Passed => Expected == Actual;
        }
    }
}
