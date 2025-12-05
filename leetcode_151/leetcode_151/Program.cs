using System.Text;
using System.Text.RegularExpressions;

namespace leetcode_151;

class Program
{
    /// <summary>
    /// 151. Reverse Words in a String
    /// https://leetcode.com/problems/reverse-words-in-a-string/description/?envType=study-plan-v2&envId=leetcode-75
    /// 151. 反轉字串中的單詞
    /// https://leetcode.cn/problems/reverse-words-in-a-string/description/
    /// 
    /// 
    /// </summary>
    /// <param name="args"></param>
    /// <summary>
    /// 給定一個輸入字串 s，請反轉字串中的單詞順序。
    /// 「單詞」由連續的非空格字元組成。s 中的單詞至少會被一個空格分隔。
    /// 回傳一個字串，該字串為按反序排列的單詞，並以單一空格連接。
    /// 注意 s 可能包含前導或尾隨空格，或兩個單詞之間有多個空格。回傳的字串中單詞間只應保留單一空格，且不要包含額外的空格。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public string ReverseWords(string s)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == ' ')
            {
                // 去除空白
                continue;
            }

            // 字串起始位置
            int start = i;
            while (i < s.Length && s[i] != ' ')
            {
                // 字串結束位置; 計算該字串長度
                i++;
            }

            // 加入字串: 結束位置 - 起始位置
            if (sb.Length == 0)
            {
                // sb 為空, 就加入
                sb.Append(s.Substring(start, i - start));
            }
            else
            {
                // 新字串插入位置 0, 原先已加入者會往後, 最後加上一個空白區隔
                sb.Insert(0, s.Substring(start, i - start) + ' ');
            }
        }

        return sb.ToString();        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public string ReverseWords_2(string s)
    {
        s = s.Trim();
        String[] split = s.Split();
        // 正規畫 去除 兩個以上空白
        Regex replaceSpace = new Regex(@"\s{2,}", RegexOptions.IgnoreCase);

        s = replaceSpace.Replace(s, "").Trim();

        StringBuilder sb = new StringBuilder();
        //從後面取
        for (int i = split.Length - 1; i >= 0; i--)
        {
            if (sb.Length > 0)
            {
                //if(i)
                //前面正則已經把空白去掉了,這邊依照題目要求補上
                sb.Append(" ");
            }
            sb.Append(split[i]);
        }

        string aa = "";
        aa = sb.ToString();

        aa = replaceSpace.Replace(aa, " ").Trim();

        return aa;        
    }
}
