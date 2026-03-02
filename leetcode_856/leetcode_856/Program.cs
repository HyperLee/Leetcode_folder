namespace leetcode_856;

class Program
{
    /// <summary>
    /// 856. Score of Parentheses
    /// https://leetcode.com/problems/score-of-parentheses/description/
    /// 
    /// Given a balanced parentheses string s, return the score of the string.
    /// 
    /// The score of a balanced parentheses string is based on the following rule:
    ///
    /// "()" has score 1.
    /// AB has score A + B, where A and B are balanced parentheses strings.
    /// (A) has score 2 * A, where A is a balanced parentheses string.
    ///
    /// 給定一個平衡的括號字串 s，返回該字串的分數。
    /// 
    /// 平衡括號字串的分數基於以下規則：
    ///
    /// "()" 的分數為 1。
    /// AB 的分數為 A + B，其中 A 和 B 是平衡的括號字串。
    /// (A) 的分數為 2 * A，其中 A 是平衡的括號字串。
    ///
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new Program();

        // 測試案例：輸入字串 / 預期分數
        (string input, int expected)[] testCases =
        [
            ("()",         1),
            ("(())",       2),
            ("()()",       2),
            ("(()(()))",   6),
        ];

        Console.WriteLine("=== LeetCode 856: Score of Parentheses ===\n");

        foreach (var (input, expected) in testCases)
        {
            int r1 = solution.ScoreOfParentheses(input);
            int r2 = solution.ScoreOfParentheses2(input);
            int r3 = solution.ScoreOfParentheses3(input);

            string pass1 = r1 == expected ? "PASS" : "FAIL";
            string pass2 = r2 == expected ? "PASS" : "FAIL";
            string pass3 = r3 == expected ? "PASS" : "FAIL";

            Console.WriteLine($"Input: \"{input}\" | Expected: {expected}");
            Console.WriteLine($"  方法一 (位元計算): {r1} [{pass1}]");
            Console.WriteLine($"  方法二 (堆疊):     {r2} [{pass2}]");
            Console.WriteLine($"  方法三 (分治):     {r3} [{pass3}]");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 方法一：位元切換計算最終分數和
    /// 
    /// 解題出發點：
    ///   整個括號字串的分數，內層的 "()" 會被外層乘以 2 倒推。
    ///   因此最內層在第 k 層括號內，其對應分數就是 2^k。
    ///   所以只要在每次看到最内層 "()" 時，引用當前深度算出 2^cnt，全部加總即為答案。
    /// 
    /// 解题步驟：
    ///   1. 維護一個括號層數計數器 cnt，遇左+1、遇右-1。
    ///   2. 每當遇到 ')' 且前一個字元為 '('(即最内層)，
    ///      計算 1 &lt;&lt; cnt 並加进 score。
    ///   3. 返回 score。
    /// 
    /// 時間複雜度：O(n) —— 單次遍訪字串。
    /// 空間複雜度：O(1) —— 僅需常數個變數。
    /// 
    /// <example>
    /// <code>
    /// // ScoreOfParentheses("(()(()))") == 6
    /// // cnt 變化： 1 2 1 2 3 2 1 0
    /// // 在 i=2 cnt=1： score += 1&lt;&lt;1 = 2
    /// // 在 i=5 cnt=2： score += 1&lt;&lt;2 = 4  =&gt; 總分 6
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="s">已平衡的括號字串</param>
    /// <returns>括號字串的分數</returns>
    public int ScoreOfParentheses(string s)
    {
        // score：累計最終分數；cnt：目前所在的括號深度（巢狀層數）
        int score = 0, cnt = 0;

        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '(')
            {
                // 遇到左括號，進入更深一層
                cnt++;
            }
            else
            {
                // 遇到右括號，離開一層
                cnt--;

                // 若前一個字元也是左括號，代表這是一個最內層的 "()"，
                // 其對應分數為 2^cnt （即目前深度的 2 的冪次）。
                // 利用位元左移 (1 << cnt) 取代 Math.Pow(2, cnt)，效率更高。
                if (s[i - 1] == '(')
                {
                    score += 1 << cnt;
                }
            }
        }

        return score;
    }

    /// <summary>
    /// 方法二：堆疊模擬括號層級
    /// 
    /// 解題出發點：
    ///   把字串看作一個空串後接上 s。用堆疊記録「期待累積」的分數：
    ///   遇到 '(' 開啟新層次，堆疊壓入 0；
    ///   遇到 ')' 關閉一層次，彈出 v(內層累積)：
    ///     - v == 0 代表內層為空串，产生分數 1
    ///     - v &gt; 0 則產生分數 2*v
    ///   再彈出 w(外層已累積)，將計算結果嚇回堆疊。
    /// 
    /// 解题步驟：
    ///   1. 堆疊初始化，壓入 0。
    ///   2. 遍歷每個字元：'(' 壓入 0；')' 彈出 v 和 w，
    ///      壓入 w + max(2*v, 1)。
    ///   3. 遍歷完成後返回堆疊頂端元素。
    /// 
    /// 時間複雜度：O(n)。
    /// 空間複雜度：O(n) —— 堆疊最多儲存 n/2 個元素。
    /// 
    /// <example>
    /// <code>
    /// // ScoreOfParentheses2("()()") == 2
    /// // stack: [0] -&gt; [0,0] -&gt; [1] -&gt; [1,0] -&gt; [2]
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="s">已平衡的括號字串</param>
    /// <returns>括號字串的分數</returns>
    public int ScoreOfParentheses2(string s)
    {
        Stack<int> stack = new Stack<int>();
        // 初始壓入 0，代表整個運算式「之前」的空字串分數
        stack.Push(0);

        foreach (char c in s)
        {
            if (c == '(')
            {
                // 遇到左括號：開啟新的計分框架，壓入 0 等待內層累積
                stack.Push(0);
            }
            else
            {
                // 遇到右括號：彈出內層分數 v（括號內部的平衡字串分數）
                int v = stack.Pop();
                // 彈出外層分數 w（此層括號之前已累積的分數）
                int w = stack.Pop();
                // 若 v == 0 代表內部是空串，即 "()"，得分 1；
                // 否則得分為 2 * v，再加回外層已累積的分數 w
                stack.Push(w + Math.Max(2 * v, 1));
            }
        }

        // 堆疊頂端即為整個字串的最終分數
        return stack.Peek();
    }

    /// <summary>
    /// 方法三：分治（Divide and Conquer）遞迴拆分
    /// 
    /// 解題出發點：
    ///   平衡括號字串必属兩種形式之一：
    ///     - A + B：可拆分為兩個子平衡字串。
    ///     - (A)：整個字串被最外層括號包住。
    ///   利用括號平衡計數 (bal) 找到最短平衡前綴長度 len，
    ///   就能判斷是哪種形式，然後遞迴分解。
    /// 
    /// 解题步驟：
    ///   1. 若 s.Length == 2：必為 "()"，回傳 1。
    ///   2. 遇左 bal+1、遇右 bal-1，從左到右找到最第一個 bal==0 的位置 len。
    ///   3. 若 len == n：字串形式為 (A)，返回 2 * Solve(s[1..n-2])。
    ///   4. 否則形式為 A+B，返回 Solve(s[0..len]) + Solve(s[len..])。
    /// 
    /// 時間複雜度：O(n^2) —— 最差情況每層結構不平均。
    /// 空間複雜度：O(n) —— 遞迴即橸深度。
    /// 
    /// <example>
    /// <code>
    /// // ScoreOfParentheses3("(()(()))") == 6
    /// // bal 對應整個字串，len==8==n，形式 (A)
    /// //   =&gt; 2 * Solve("()(())")
    /// //      len=2, 形式 A+B =&gt; Solve("()") + Solve("(())")
    /// //      = 1 + 2 = 3  =&gt; 2 * 3 = 6
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="s">已平衡的括號字串</param>
    /// <returns>括號字串的分數</returns>
    public int ScoreOfParentheses3(string s)
    {
        // 遞迴基底：長度為 2 只可能是 "()"，分數固定為 1
        if (s.Length == 2)
        {
            return 1;
        }

        // bal：括號平衡計數（左+1 / 右-1）
        // n：字串長度；len：第一個完整平衡前綴的長度
        int bal = 0, n = s.Length, len = 0;

        for (int i = 0; i < n; i++)
        {
            bal += (s[i] == '(' ? 1 : -1);

            // 當 bal 首次回到 0，找到第一個完整的平衡括號前綴
            if (bal == 0)
            {
                len = i + 1;
                break;
            }
        }

        if (len == n)
        {
            // 整個字串是一個被最外層括號包住的形式 (A)，
            // 去掉最外層括號後遞迴計算內部 A 的分數，再乘以 2
            return 2 * ScoreOfParentheses3(s.Substring(1, n - 2));
        }
        else
        {
            // 字串可拆分為 A + B 兩段，分別遞迴後加總
            return ScoreOfParentheses3(s.Substring(0, len)) + ScoreOfParentheses3(s.Substring(len));
        }
    }
}
