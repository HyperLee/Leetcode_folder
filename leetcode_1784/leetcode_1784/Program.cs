namespace leetcode_1784;

class Program
{
    /// <summary>
    /// 1784. Check if Binary String Has at Most One Segment of Ones
    /// Given a binary string s without leading zeros, return true if s contains at most one contiguous segment of ones. Otherwise, return false.
    ///
    /// 1784. 檢查二進位字串是否至多包含一個連續的 1 段
    /// 給定一個不含前導零的二進位字串 s，如果 s 至多包含一個連續的 1 段，回傳 true；否則回傳 false。
    ///
    /// English and Traditional Chinese versions added as requested.
    ///
    /// https://leetcode.com/problems/check-if-binary-string-has-at-most-one-segment-of-ones/description/?envType=daily-question&envId=2026-03-06
    /// https://leetcode.cn/problems/check-if-binary-string-has-at-most-one-segment-of-ones/description/?envType=daily-question&envId=2026-03-06
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 迴圈遍歷
    /// 
    /// 1. 遇到第一個 '1' 時開始「進入 1 段」狀態。
    /// 2. 如果之後看到 0，就把狀態改成「已離開 1 段」。
    /// 3. 一旦狀態是「已離開 1 段」卻又遇到 '1'，就代表有第二段，直接回傳 false。 
    /// 4. 走完整串沒有觸發上述條件，回傳 true。 
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public bool CheckOnesSegment(string s)
    {
        bool inSegment = false;
        bool leftSegment = false;
        foreach (char c in s) 
        {
            if (c == '1') 
            {
                if (leftSegment) 
                {
                    return false;      // 第二段 1
                }
                inSegment = true;
            } 
            else 
            { 
                // c == '0'
                if (inSegment) 
                {
                    leftSegment = true; // 退出第一段
                }
                inSegment = false;
            }
        }
        return true;
    }

    /// <summary>
    /// 方法2: 寻找 01 串
    /// 题目给定一个长度为 n 的二进制字符串 s，并满足该字符串不含前导零。
    /// 现在我们需要判断字符串中是否只包含零个或一个由连续 1 组成的字段。首先我们依次分析这两种情况：
    /// - 字符串 s 中包含零个由连续 1 组成的字段，那么整个串的表示为 00⋯00。
    /// - 字符串 s 中只包含一个由连续 1 组成的字段，因为已知字符串 s 不包含前导零，所以整个串的表示为 1⋯100⋯00。
    /// 那么可以看到两种情况中都不包含 01 串。且不包含的 01 串的一个二进制字符串也有且仅有上面两种情况。所以我们可以通过原字
    /// 符串中是否有 01 串来判断字符串中是否只包含零个或一个由连续 1 组成的字段。如果有 01 串则说明该情况不满足，否则即满足该
    /// 情况条件。
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public bool CheckOnesSegment2(string s)
    {
        return !s.Contains("01");
    }
}
