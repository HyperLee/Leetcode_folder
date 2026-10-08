namespace leetcode_1021;

class Program
{
    /// <summary>
    /// 1021. Remove Outermost Parentheses
    /// https://leetcode.com/problems/remove-outermost-parentheses/description/
    /// 1021. 删除最外层的括号
    /// https://leetcode.cn/problems/remove-outermost-parentheses/description/
    ///
    /// English:
    /// A valid parentheses string is either empty "", "(" + A + ")", or A + B,
    /// where A and B are valid parentheses strings, and + represents string concatenation.
    /// For example, "", "()", "(())()", and "(()(()))" are all valid parentheses strings.
    /// A valid parentheses string s is primitive if it is nonempty and cannot be split into
    /// s = A + B, where A and B are both nonempty valid parentheses strings.
    /// Given a valid parentheses string s, its primitive decomposition is
    /// s = P1 + P2 + ... + Pk, where every Pi is primitive.
    /// Return s after removing the outermost parentheses of every primitive string in its
    /// primitive decomposition.
    ///
    /// 繁體中文：
    /// 有效的括號字串符合以下任一條件：空字串 ""、"(" + A + ")"，或 A + B；
    /// 其中 A 與 B 都是有效的括號字串，而 + 表示字串串接。
    /// 例如，""、"()"、"(())()" 與 "(()(()))" 都是有效的括號字串。
    /// 有效括號字串 s 若非空，且無法拆分為 s = A + B（其中 A 與 B 都是非空的
    /// 有效括號字串），則稱為原始字串（primitive）。
    /// 給定有效括號字串 s，其原始分解為 s = P1 + P2 + ... + Pk，其中每個 Pi 都是
    /// 原始字串。回傳移除原始分解中每個原始字串最外層括號後的 s。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}