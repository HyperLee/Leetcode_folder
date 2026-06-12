namespace leetcode_187;

class Program
{
    /// <summary>
    /// 187. Repeated DNA Sequences
    /// https://leetcode.com/problems/repeated-dna-sequences/description/
    /// 187. 重複的 DNA 序列
    /// https://leetcode.cn/problems/repeated-dna-sequences/description/
    ///
    /// The DNA sequence is composed of a series of nucleotides abbreviated as 'A', 'C', 'G', and 'T'.
    ///
    /// For example, "ACGAATTCCG" is a DNA sequence.
    /// When studying DNA, it is useful to identify repeated sequences within the DNA.
    ///
    /// Given a string s that represents a DNA sequence, return all the 10-letter-long sequences
    /// (substrings) that occur more than once in a DNA molecule. You may return the answer in any order.
    ///
    /// DNA 序列是由一連串以 'A'、'C'、'G' 與 'T' 縮寫表示的核苷酸所組成。
    ///
    /// 例如，"ACGAATTCCG" 是一個 DNA 序列。
    /// 在研究 DNA 時，找出 DNA 中重複出現的序列是很有幫助的。
    ///
    /// 給定一個代表 DNA 序列的字串 s，請回傳所有在 DNA 分子中出現超過一次、
    /// 長度為 10 的序列（子字串）。你可以以任意順序回傳答案。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    const int Length = 10;

    /// <summary>
    /// 解法一: Dictionary 統計
    /// 我们可以用一个Dictionary统计 s 所有长度为 10 的子串的出现次数，返回所有出现次数超过 10 的子串。
    /// 代码实现时，可以一边遍历子串一边记录答案，为了不重复记录答案，我们只统计当前出现次数为 2 的子串。
    /// 統計相同長度 出現次數 
    /// 當 為2 時候判斷為 重覆
    /// 加入至解答中
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public IList<string> FindRepeatedDnaSequences(string s)
    {
        IList<string> result = new List<string>();
        Dictionary<string, int> dic = new Dictionary<string, int>();
        int n = s.Length;

        for(int i = 0; i <= n - Length; i++)
        {
            string sub = s.Substring(i, Length);

            if(!dic.ContainsKey(sub))
            {
                dic.Add(sub, 1);
            }
            else
            {
                dic[sub]++;
            }

            // 同樣字串出現第二次就加入
            if(dic[sub] == 2)
            {
                result.Add(sub);
            }
        }
        return result;
    }

    Dictionary<char, int> bin = new Dictionary<char, int>{ {'A', 0}, {'C', 1}, {'G', 2}, {'T', 3}};

    /// <summary>
    /// 解法二: 哈希表 + 滑动窗口 + 位运算
    /// 由于 s 中只含有 4 种字符，我们可以将每个字符用 2 个比特表示，即：
    /// A 表示为二进制 00；
    /// C 表示为二进制 01；
    /// G 表示为二进制 10；
    /// T 表示为二进制 11。
    /// 
    /// 如此一来，一个长为 10 的字符串就可以用 20 个比特表示，而一个 int 整数有 32 个比特，足够容纳该字符串，因此我们可以将 s
    /// 的每个长为 10 的子串用一个 int 整数表示（只用低 20 位）。
    /// 
    /// 注意到上述字符串到整数的映射是一一映射，每个整数都对应着一个唯一的字符串，因此我们可以将方法一中的哈希表改为存储每个
    /// 长为 10 的子串的整数表示。
    /// 
    /// 如果我们对每个长为 10 的子串都单独计算其整数表示，那么时间复杂度仍然和方法一一样为 O(NL)。为了优化时间复杂度，我们
    /// 可以用一个大小固定为 10 的滑动窗口来计算子串的整数表示。设当前滑动窗口对应的整数表示为 x，当我们要计算下一个子串时，
    /// 就将滑动窗口向右移动一位，此时会有一个新的字符进入窗口，以及窗口最左边的字符离开窗口，这些操作对应的位运算，按计算顺
    /// 序表示如下：
    /// 滑动窗口向右移动一位：x = x << 2，由于每个字符用 2 个比特表示，所以要左移 2 位；
    /// 一个新的字符 ch 进入窗口：x = x | bin[ch]，这里 bin[ch] 为字符 ch 的对应二进制；
    /// 窗口最左边的字符离开窗口：x = x & ((1 << 20) - 1)，由于我们只考虑 x 的低 20 位比特，需要将其余位置零，即与上 (1 << 20) - 1。
    /// 将这三步合并，就可以用 O(1) 的时间计算出下一个子串的整数表示，即 x = ((x << 2) | bin[ch]) & ((1 << 20) - 1)。
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public IList<string> FindRepeatedDnaSequences2(string s)
    {
        IList<string> res = new List<string>();
        int n = s.Length;
        if(n <= Length)
        {
            return res;
        }

        int x = 0;
        for(int i = 0; i < Length - 1; i++)
        {
            x = (x << 2) | bin[s[i]];
        }

        Dictionary<int, int> cnt = new Dictionary<int, int>();
        for(int i = 0; i <= n - Length; i++)
        {
            x = ((x << 2) | bin[s[i + Length - 1]]) & ((1 << (Length * 2)) - 1);
            if(!cnt.ContainsKey(x))
            {
                cnt.Add(x, 1);
            }
            else
            {
                cnt[x]++;
            }

            if(cnt[x] == 2)
            {
                res.Add(s.Substring(i, Length));
            }
        }
        return res;
    }
}
