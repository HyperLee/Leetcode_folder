using System.Text;

namespace leetcode_022;

class Program
{
    /// <summary>
    /// 22. Generate Parentheses
    /// https://leetcode.com/problems/generate-parentheses/description/
    /// 22. 括号生成
    /// https://leetcode.cn/problems/generate-parentheses/description/
    /// <para>
    /// English: Given n pairs of parentheses, write a function to generate all combinations of well-formed parentheses.
    /// </para>
    /// <para>
    /// 繁體中文：給定 n 對括號，請撰寫一個函式，產生所有格式正確的括號組合。
    /// </para>
    /// </summary>
    /// <param name="args">命令列參數；本程式不使用。</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 解法一：暴力法
    /// 
    /// 我们可以生成所有 2＾2n个 ‘(’ 和 ‘)’ 字符构成的序列，然后我们检查每一个是否有效即可。
    /// 为了生成所有序列，我们可以使用递归。长度为 n 的序列就是在长度为 n−1 的序列前加一个 ‘(’ 或 ‘)’。
    /// 为了检查序列是否有效，我们遍历这个序列，并使用一个变量 balance 表示左括号的数量减去右括号的数量。如果在遍历过程中
    /// balance 的值小于零，或者结束时 balance 的值不为零，那么该序列就是无效的，否则它是有效的。
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public IList<string> GenerateParenthesis(int n)
    {
        List<string> combinations = new List<string>();
        generateAll(new char[2 * n], 0, combinations);
        return combinations;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="current"></param>
    /// <param name="pos"></param>
    /// <param name="result"></param>
    public void generateAll(char[] current, int pos, List<string> result)
    {
        if(pos == current.Length)
        {
            if(isValid(current))
            {
                result.Add(new string(current));
            }
        }
        else
        {
            current[pos] = '(';
            generateAll(current, pos + 1, result);
            current[pos] = ')';
            generateAll(current, pos + 1, result);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="current"></param>
    /// <returns></returns>
    public bool isValid(char[] current)
    {
        int balance = 0;
        foreach(char c in current)
        {
            if(c == '(')
            {
                balance++;
            }
            else
            {
                balance--;  
            }

            if(balance < 0)
            {
                return false;
            }
        }
        return balance == 0;
    }

    /// <summary>
    /// 解法二: 回朔法
    /// 方法一还有改进的余地：我们可以只在序列仍然保持有效时才添加 ‘(’ 或 ‘)’，而不是像 方法一 那样每次添加。我们可以通过跟踪到
    /// 目前为止放置的左括号和右括号的数目来做到这一点，
    /// 如果左括号数量不大于 n，我们可以放一个左括号。如果右括号数量小于左括号的数量，我们可以放一个右括号。
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public IList<string> GenerateParenthesis2(int n)
    {
        List<string> ans = new List<string>();
        backtrack(ans, new StringBuilder(), 0, 0, n);
        return ans;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ans"></param>
    /// <param name="cur"></param>
    /// <param name="open"></param>
    /// <param name="close"></param>
    /// <param name="max"></param>
    public void backtrack(List<string> ans, StringBuilder cur, int open, int close, int max)
    {
        if(cur.Length == max * 2)
        {
            ans.Add(cur.ToString());
            return;
        }

        if(open < max)
        {
            cur.Append('(');
            backtrack(ans, cur, open + 1, close, max);
            cur.Remove(cur.Length - 1, 1);
        }
        if(close < open)
        {
            cur.Append(')');
            backtrack(ans, cur, open, close + 1, max);
            cur.Remove(cur.Length - 1, 1);
        }
    }
}