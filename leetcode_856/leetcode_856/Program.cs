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
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 方法一:计算最终分数和
    /// 我们可以对空间复杂度进行进一步的优化，并不需要使用栈去保留所有的中间情况，
    /// 可以只用一个变量 cnt 来记录当前在第几层括号之中，因为本题的括号累加值是有规律的，"()" 是1，
    /// 因为最中间的括号在0层括号内，2^0 = 1。"(())" 是2，因为最中间的括号在1层括号内，2^1 = 2。"((()))" 是4，
    /// 因为最中间的括号在2层括号内，2^2 = 4。因此类推，其实只需要统计出最中间那个括号外变有几个括号
    /// ，就可以直接算出整个多重包含的括号字符串的值，
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public int ScoreOfParentheses(string s)
    {
        int score = 0, cnt = 0;
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '(')
            {
                cnt++;
            }
            else
            {
                cnt--;
                if (s[i - 1] == '(')
                {
                    score += 1 << cnt;
                }
            }
        }
        return score;
    }

    /// <summary>
    /// 方法2:棧
    /// 我们把平衡字符串 s 看作是一个空字符串加上 s 本身，并且定义空字符串的分数为 0。使用栈 st 记录平衡字符串的分数，在开始之前要压入分数 0，表示空字符串的分数。
    /// 在遍历字符串 s 的过程中：
    /// 遇到左括号，那么我们需要计算该左括号内部的子平衡括号字符串 A 的分数，我们也要先压入分数 0，表示 A 前面的空字符串的分数。
    /// 遇到右括号，说明该右括号内部的子平衡括号字符串 A 的分数已经计算出来了，我们将它弹出栈，并保存到变量 v 中。如果 v=0，那么说明子平衡括号字符串 A 是空串，(A) 的分数为 1，否则 (A) 的分数为 2v，然后将 (A) 的分数加到栈顶元素上。
    /// 遍历结束后，栈顶元素保存的就是 s 的分数。
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public int ScoreOfParentheses2(string s)
    {
        Stack<int> stack = new Stack<int>();
        stack.Push(0); // The score of the current frame
        foreach (char c in s)
        {
            if (c == '(')
            {
                stack.Push(0);
            }
            else
            {
                int v = stack.Pop();
                int w = stack.Pop();
                stack.Push(w + Math.Max(2 * v, 1));
            }
        }
        return stack.Peek();
    }

    /// <summary>
    /// 方法三:分治
    /// 根据题意，一个平衡括号字符串 s 可以被分解为 A+B 或 (A) 的形式，因此我们可以对 s 进行分解，分而治之。
    /// 如何判断 s 应该分解为 A+B 或 (A) 的哪一种呢？我们将左括号记为 1，右括号记为 −1，
    /// 如果 s 的某个非空前缀对应的和 bal=0，那么这个前缀就是一个平衡括号字符串。如果该前缀长度等于 s 的长度，那么 s 可以分解为 (A) 的形式；
    /// 否则 s 可以分解为 A+B 的形式，其中 A 为该前缀。将 s 分解之后，我们递归地求解子问题，并且 s 的长度为 2 时，分数为 1。
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public int ScoreOfParentheses3(string s)
    {
        if (s.Length == 2) 
        {
            return 1;
        }
        int bal = 0, n = s.Length, len = 0;
        for (int i = 0; i < n; i++) 
        {
            bal += (s[i] == '(' ? 1 : -1);
            if (bal == 0) 
            {
                len = i + 1;
                break;
            }
        }
        if (len == n) 
        {
            return 2 * ScoreOfParentheses(s.Substring(1, n - 2));
        } 
        else 
        {
            return ScoreOfParentheses(s.Substring(0, len)) + ScoreOfParentheses(s.Substring(len));
        }

    }
}
