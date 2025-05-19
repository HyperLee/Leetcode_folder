namespace leetcode_241;

class Program
{
    /// <summary>
    /// 241. Different Ways to Add Parentheses
    /// https://leetcode.com/problems/different-ways-to-add-parentheses/description/
    /// 241. 為運算表達式設計優先級
    /// https://leetcode.cn/problems/different-ways-to-add-parentheses/description/
    /// 
    /// 題目描述：
    /// 給你一個由數字和運算符組成的字串 expression，按不同優先級組合數字和運算符，
    /// 計算並返回所有可能組合的結果。你可以按任意順序返回答案。
    /// 
    /// 生成的測試用例滿足：
    /// - 運算符只有 '+'、'-' 和 '*'
    /// - 運算數只有整數
    /// - 運算數和運算符都能被空格字元分隔
    /// - 算式的結果在 32-bit 整數範圍內
    /// 
    /// 範例 1:
    /// 輸入: expression = "2-1-1"
    /// 輸出: [0, 2]
    /// 解釋：
    /// ((2-1)-1) = 0 
    /// (2-(1-1)) = 2
    /// 
    /// 範例 2:
    /// 輸入: expression = "2*3-4*5"
    /// 輸出: [-34, -14, -10, -10, 10]
    /// 解釋：
    /// (2*(3-(4*5))) = -34 
    /// ((2*3)-(4*5)) = -14 
    /// ((2*(3-4))*5) = -10 
    /// (2*((3-4)*5)) = -10 
    /// (((2*3)-4)*5) = 10
    /// </summary>
    /// <param name="args">命令列參數</param>
    static void Main(string[] args)
    {
        // 建立測試用例
        Program program = new Program();
        string[] testExpressions = new string[] {
            "2-1-1",       // 範例 1
            "2*3-4*5",     // 範例 2
            "1+2*3",       // 簡單測試用例
            "11+22*33"     // 多位數測試用例
        };

        // 顯示標題
        Console.WriteLine("LeetCode 241 - Different Ways to Add Parentheses");
        Console.WriteLine("=============================================");

        // 遍歷每個測試用例
        foreach (string expr in testExpressions)
        {
            Console.WriteLine($"\n測試表達式: {expr}");
            Console.WriteLine("------------------------------------------");

            // 使用分治法解決問題
            Console.WriteLine("分治法結果:");
            var divideConquerResults = program.DiffWaysToCompute(expr);
            PrintResults(divideConquerResults);

            // 使用 DFS 解決問題
            Console.WriteLine("\nDFS法結果:");
            var dfsResults = program.DiffWaysToCompute_DFS(expr);
            PrintResults(dfsResults);

            // 使用記憶化搜索解決問題
            Console.WriteLine("\n記憶化搜索結果:");
            var memoResults = program.DiffWaysToCompute_memo(expr);
            PrintResults(memoResults);

            // 使用動態規劃解決問題
            Console.WriteLine("\n動態規劃結果:");
            var dpResults = program.DiffWaysToCompute_DP(expr);
            PrintResults(dpResults);

            Console.WriteLine("\n=============================================");
        }
    }
    

    /// <summary>
    /// 輔助方法：列印結果集合
    /// 將計算結果排序後列印，便於結果比較和觀察
    /// </summary>
    /// <param name="results">計算結果集合</param>
    static void PrintResults(IList<int> results)
    {
        // 將結果按照升序排序，便於比較
        List<int> sortedResults = new List<int>(results);
        sortedResults.Sort();

        // 列印排序後的結果
        Console.Write("[");
        for (int i = 0; i < sortedResults.Count; i++)
        {
            Console.Write(sortedResults[i]);
            if (i < sortedResults.Count - 1)
                Console.Write(", ");
        }
        Console.WriteLine("]");
    }


    // 使用常數代表運算符，便於在計算過程中區分數字和運算符
    const int Add = -1;  // 加法運算符常數值 '+'
    const int Sub = -2;  // 減法運算符常數值 '-'
    const int Mul = -3;  // 乘法運算符常數值 '*'


    /// <summary>
    /// 解法一：記憶化搜索 (Memoization)
    /// 透過記憶化搜索避免重複計算子問題，提高算法效率
    /// 
    /// 1. 將表達式轉換為數字和運算符的序列，使用負數表示運算符
    /// 2. 使用二維陣列 dp[l][r] 緩存從 l 到 r 區間的所有可能計算結果
    /// 3. 通過 DFS 遞迴方法計算不同區間的結果，並重用已計算的子問題結果
    /// 4. 特別處理沒有運算符的情況，確保所有輸入都能正確處理
    /// 
    /// 時間複雜度：O(n^3)，其中 n 是表達式的長度
    /// 空間複雜度：O(n^2)，用於存儲子問題的結果
    /// 
    /// ref:
    /// https://leetcode.cn/problems/different-ways-to-add-parentheses/solutions/1634445/wei-yun-suan-biao-da-shi-she-ji-you-xian-lyw6/
    /// </summary>
    /// <param name="expression">待計算的算術表達式字串</param>
    /// <returns>所有可能的計算結果列表</returns>
    public IList<int> DiffWaysToCompute_memo(string expression)
    {
        // 將表達式轉換為數字和運算符序列
        IList<int> ops = new List<int>();

        for (int i = 0; i < expression.Length;)
        {
            // 如果當前字元是運算符
            if (!char.IsDigit(expression[i]))
            {
                if (expression[i] == '+')
                {
                    ops.Add(Add); // 加法運算符用 -1 表示
                }
                else if (expression[i] == '-')
                {
                    ops.Add(Sub); // 減法運算符用 -2 表示
                }
                else if (expression[i] == '*')
                {
                    ops.Add(Mul); // 乘法運算符用 -3 表示
                }
                i++;
            }
            else
            {
                // 解析多位數字
                int t = 0;
                while (i < expression.Length && char.IsDigit(expression[i]))
                {
                    t = t * 10 + (expression[i] - '0');
                    i++;
                }
                ops.Add(t); // 將解析到的數字加入序列
            }
        }

        // 初始化記憶化陣列，存儲子問題的解
        IList<int>[][] dp = new IList<int>[ops.Count][];
        for (int i = 0; i < ops.Count; i++)
        {
            dp[i] = new IList<int>[ops.Count];
        }
        // 初始化每個子問題的結果列表
        for (int i = 0; i < ops.Count; i++)
        {
            for (int j = 0; j < ops.Count; j++)
            {
                dp[i][j] = new List<int>();
            }
        }

        // 使用深度優先搜索解決問題
        return DFS(dp, 0, ops.Count - 1, ops);
    }


    /// <summary>
    /// 解法一的輔助函式：深度優先搜索
    /// 計算從 l 到 r 區間內所有可能的運算結果
    /// 
    /// 此函式是記憶化搜索的核心，通過遞迴方式處理每個子問題：
    /// 1. 如果子問題已被計算過，則直接返回結果
    /// 2. 如果區間只包含一個數字，將其加入結果
    /// 3. 遍歷區間中的每個運算符，將表達式分為左右兩部分
    /// 4. 計算左右部分的所有可能結果，然後根據運算符組合它們
    /// 5. 如果沒有找到運算符，確保能處理整個區間為單一數字的情況
    /// </summary>
    /// <param name="dp">記憶化數組，存儲已計算過的子問題結果</param>
    /// <param name="l">左邊界索引</param>
    /// <param name="r">右邊界索引</param>
    /// <param name="ops">運算符和數字序列</param>
    /// <returns>所有可能的計算結果列表</returns>
    public IList<int> DFS(IList<int>[][] dp, int l, int r, IList<int> ops)
    {
        // 若已計算過該子問題，直接返回結果
        if (dp[l][r].Count > 0)
        {
            return dp[l][r];
        }

        // 基本情況：只有一個數字
        if (l == r)
        {
            dp[l][r].Add(ops[l]);
            return dp[l][r];
        }

        // 遍歷區間內的每一個位置
        for (int i = l; i < r; i++)
        {
            // 只處理運算符位置（運算符以負數表示）
            if (ops[i] >= 0) continue;  // 跳過數字

            // 遞迴計算左右兩部分的結果
            IList<int> left = DFS(dp, l, i - 1, ops);    // 計算左側表達式的所有結果
            IList<int> right = DFS(dp, i + 1, r, ops);   // 計算右側表達式的所有結果

            // 合併左右兩部分的結果
            foreach (int lval in left)
            {
                foreach (int rval in right)
                {
                    int res = 0;
                    // 根據運算符進行相應運算
                    if (ops[i] == Add)
                    {
                        res = lval + rval;  // 加法
                    }
                    else if (ops[i] == Sub)
                    {
                        res = lval - rval;  // 減法
                    }
                    else
                    {
                        res = lval * rval;  // 乘法
                    }
                    dp[l][r].Add(res);  // 將結果添加到記憶化數組
                }
            }
        }

        // 如果沒有找到運算符，表示整個區間是一個數字，這種情況應該在基本情況處理
        // 但為了防止出現未處理的情況，這裡添加一個額外的檢查
        if (dp[l][r].Count == 0)
        {
            // 嘗試解析從 l 到 r 的整個數字
            int num = ops[l];
            dp[l][r].Add(num);
        }

        return dp[l][r];  // 返回計算結果
    }


    /// <summary>
    /// 解法二：動態規劃 (Dynamic Programming)
    /// 自底向上的動態規劃方法，避免遞迴調用的開銷
    /// 
    /// 1. 先將表達式轉換為數字和運算符的序列（同樣使用負數表示運算符）
    /// 2. dp[i][j] 表示從第 i 個位置到第 j 個位置的所有可能的結果
    /// 3. 填充基本情況：dp[i][i] 包含單個數字的結果
    /// 4. 按照子問題長度遞增，考慮所有可能的分割點k（必須是運算符）
    /// 5. 對每個分割點，合併左側 dp[i][k-1] 和右側 dp[k+1][j] 的結果
    /// 6. 時間複雜度 O(n^3)，空間複雜度 O(n^2)
    /// 
    /// 特別處理：只有負數值（運算符）才能作為分割點，正數（數字）被跳過
    /// 
    /// ref:
    /// https://leetcode.cn/problems/different-ways-to-add-parentheses/solutions/1634445/wei-yun-suan-biao-da-shi-she-ji-you-xian-lyw6/
    /// 
    /// https://leetcode.cn/problems/different-ways-to-add-parentheses/solutions/125144/xiang-xi-tong-su-de-si-lu-fen-xi-duo-jie-fa-by-5-5/
    /// </summary>
    /// <param name="expression">待計算的算術表達式字串</param>
    /// <returns>所有可能的計算結果列表</returns>
    public IList<int> DiffWaysToCompute_DP(string expression)
    {        // 將表達式轉換為數字和運算符序列
        IList<int> ops = new List<int>();

        // 解析表達式字串
        for (int i = 0; i < expression.Length;)
        {
            // 如果當前字元是運算符
            if (!char.IsDigit(expression[i]))
            {
                if (expression[i] == '+')
                {
                    ops.Add(Add);
                }
                else if (expression[i] == '-')
                {
                    ops.Add(Sub);
                }
                else if (expression[i] == '*')
                {
                    ops.Add(Mul);
                }
                i++;
            }
            else
            {
                // 解析多位數字
                int t = 0;
                while (i < expression.Length && char.IsDigit(expression[i]))
                {
                    t = t * 10 + (expression[i] - '0');
                    i++;
                }
                ops.Add(t);
            }
        }

        // 初始化動態規劃數組
        IList<int>[][] dp = new IList<int>[ops.Count][];
        for (int i = 0; i < ops.Count; i++)
        {
            dp[i] = new IList<int>[ops.Count];
        }
        for (int i = 0; i < ops.Count; i++)
        {
            for (int j = 0; j < ops.Count; j++)
            {
                dp[i][j] = new List<int>();
            }
        }
        // 填充單個數字的基本情況
        for (int i = 0; i < ops.Count; i++)
        {
            // 只處理數字（正數或零）
            if (ops[i] >= 0)
            {
                dp[i][i].Add(ops[i]);
            }
        }
        // 自底向上計算所有子問題
        for (int len = 2; len <= ops.Count; len++) // 子問題長度從2開始
        {
            for (int i = 0; i <= ops.Count - len; i++) // 起始位置
            {
                int j = i + len - 1;  // 終止位置
                for (int k = i; k < j; k++)  // 遍歷可能的中間位置
                {
                    // 只有運算符才能作為分割點
                    if (ops[k] >= 0) continue; // 跳過數字

                    foreach (int left in dp[i][k - 1])  // 左側表達式的所有結果
                    {
                        foreach (int right in dp[k + 1][j])  // 右側表達式的所有結果
                        {
                            int res = 0;
                            // 根據運算符進行相應運算
                            if (ops[k] == Add)
                            {
                                res = left + right;
                            }
                            else if (ops[k] == Sub)
                            {
                                res = left - right;
                            }
                            else
                            {
                                res = left * right;
                            }
                            dp[i][j].Add(res);
                        }
                    }
                }
            }
        }

        return dp[0][ops.Count - 1];  // 返回整個表達式的計算結果
    }


    /// <summary>
    /// 解法三：分治演算法 (Divide and Conquer)
    /// 最直觀的解法，無需顯式建立序列表示
    /// 
    /// 實作步驟：
    /// 1. 如果表達式只包含數字，則直接解析並返回
    /// 2. 遍歷表達式中的每個字元，找到運算符
    /// 3. 每找到一個運算符，就將表達式分割為左右兩部分
    /// 4. 遞迴計算左右兩部分的所有可能結果
    /// 5. 根據當前運算符，組合左右兩部分的所有結果
    /// 
    /// 特點：
    /// - 實作簡潔，邏輯直觀
    /// - 無需額外數據結構轉換表達式
    /// - 直接使用字串切割處理左右子表達式
    /// 
    /// 時間複雜度：理論上是 O(2^n)，但實際上由於子問題重疊情況較多，表現會好一些
    /// 空間複雜度：O(n)，主要是遞迴調用棧的開銷
    /// 
    /// ref:
    /// https://leetcode.cn/problems/different-ways-to-add-parentheses/solutions/44108/pythongolang-fen-zhi-suan-fa-by-jalan/
    /// </summary>
    /// <param name="expression">待計算的表達式</param>
    /// <returns>所有可能的計算結果</returns>
    public IList<int> DiffWaysToCompute(string expression)
    {
        // 如果只有數字，直接返回
        bool isDigitOnly = true;
        foreach (char c in expression)
        {
            if (!char.IsDigit(c))
            {
                isDigitOnly = false;
                break;
            }
        }

        if (isDigitOnly)
        {
            return new List<int> { int.Parse(expression) };
        }

        IList<int> res = new List<int>();
        for (int i = 0; i < expression.Length; i++)
        {
            char c = expression[i];
            if (c == '+' || c == '-' || c == '*')
            {
                // 1.分解：遇到運算符，計算左右兩側的結果集
                // 2.解決：DiffWaysToCompute 遞迴函式求出子問題的解
                IList<int> left = DiffWaysToCompute(expression.Substring(0, i));
                IList<int> right = DiffWaysToCompute(expression.Substring(i + 1));

                // 3.合併：根據運算符合併子問題的解
                foreach (int l in left)
                {
                    foreach (int r in right)
                    {
                        if (c == '+')
                        {
                            res.Add(l + r);
                        }
                        else if (c == '-')
                        {
                            res.Add(l - r);
                        }
                        else
                        {
                            res.Add(l * r);
                        }
                    }
                }
            }
        }

        return res;
    }


    /// <summary>
    /// 解法四：DFS演算法 (Depth-First Search)
    /// 使用字元陣列與遞迴DFS方法解決問題
    /// 
    /// 實作特點：
    /// 1. 將表達式轉換為字元陣列，避免重複字串操作
    /// 2. 使用專用 DFS 函式遞迴處理字元陣列中指定區間 [l,r]
    /// 3. 在區間中找出所有運算符，分別作為分割點
    /// 4. 通過遞迴計算每個分割點左右兩側的所有可能結果
    /// 5. 將左右結果根據運算符組合起來
    /// 6. 特別處理沒有運算符的情況，將整個數字作為結果
    /// 
    /// 優勢：
    /// - 直接處理字元級別，不需要預處理表達式
    /// - 能正確處理多位數字
    /// - 實作簡潔明瞭
    /// 
    /// 時間複雜度：與分治法相似，理論 O(2^n)，實際表現較好
    /// 空間複雜度：O(n)，用於遞迴調用棧
    /// 
    /// ref:
    /// https://leetcode.cn/problems/different-ways-to-add-parentheses/solutions/1637092/by-ac_oier-z07i/
    /// </summary>
    /// <param name="expression">待計算的表達式</param>
    /// <returns>所有可能的計算結果</returns>
    public IList<int> DiffWaysToCompute_DFS(string expression)
    {
        char[] cs = expression.ToCharArray();
        return DFS(cs, 0, cs.Length - 1);
    }


    /// <summary>
    /// DFS遞迴函式，計算字元陣列表達式從l到r區間的所有可能結果
    /// 
    /// 此函式主要步驟：
    /// 1. 遍歷當前區間 [l,r] 的每個字元
    /// 2. 找到運算符字元 ('+', '-', '*')
    /// 3. 以運算符為分割點，遞迴計算左右兩側表達式的所有結果
    /// 4. 根據運算符將左右結果組合成新結果
    /// 5. 若區間內沒有運算符，則解析整個區間為一個數字值
    /// 
    /// 注意：此函式能正確處理多位數字，將連續數字字元轉換為一個整數值
    /// </summary>
    /// <param name="cs">表達式的字元陣列</param>
    /// <param name="l">左邊界索引</param>
    /// <param name="r">右邊界索引</param>
    /// <returns>所有可能的計算結果列表</returns>
    private IList<int> DFS(char[] cs, int l, int r)
    {
        IList<int> ans = new List<int>();
        for (int i = l; i <= r; i++)
        {
            // 跳過數字字元，只處理運算符
            if (cs[i] >= '0' && cs[i] <= '9') continue;

            // 分別計算運算符左右兩側的所有可能結果
            IList<int> left = DFS(cs, l, i - 1);
            IList<int> right = DFS(cs, i + 1, r);

            // 根據運算符合併左右兩側的結果
            foreach (int a in left)
            {
                foreach (int b in right)
                {
                    int cur = 0;
                    if (cs[i] == '+') cur = a + b;       // 加法
                    else if (cs[i] == '-') cur = a - b;  // 減法
                    else cur = a * b;                    // 乘法
                    ans.Add(cur);                       // 將結果添加到答案列表
                }
            }
        }

        // 如果當前區間沒有運算符（即全是數字），則直接計算數值
        if (ans.Count == 0)
        {
            int cur = 0;
            for (int i = l; i <= r; i++)
                cur = cur * 10 + (cs[i] - '0');  // 將字元轉換為數字並計算多位數值
            ans.Add(cur);  // 將數字添加到結果列表
        }

        return ans;  // 返回所有可能的計算結果
    }
}
