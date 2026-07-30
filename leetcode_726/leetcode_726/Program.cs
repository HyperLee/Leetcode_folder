using System.Text;

namespace leetcode_726
{
    internal class Program
    {
        /// <summary>
        /// 726. Number of Atoms
        /// https://leetcode.com/problems/number-of-atoms/description/?envType=daily-question&envId=2024-07-14
        /// 726. 原子的数量
        /// https://leetcode.cn/problems/number-of-atoms/description/
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
