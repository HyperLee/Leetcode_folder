using System.Text;

namespace leetcode_1957;

class Program
{
    /// <summary>
    /// 1957. Delete Characters to Make Fancy String
    /// https://leetcode.com/problems/delete-characters-to-make-fancy-string/description/?envType=daily-question&envId=2025-07-21
    /// 1957. 删除字符使字符串变好
    /// https://leetcode.cn/problems/delete-characters-to-make-fancy-string/description/?envType=daily-question&envId=2025-07-21
    /// 
    /// 題目描述：
    /// 給定一個字串 s，請刪除最少數量的字元，使得刪除後的字串不會有三個連續的字元相同。
    /// 返回刪除後的最終字串。可以證明答案一定唯一。
    /// </summary>
    /// <param name="args"></param> <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // 測試資料：驗證 MakeFancyString 功能
        var program = new Program();
        string[] testCases = { "aaabaaaa", "aab", "abcdddeeeeaabbbcd" };
        foreach (var test in testCases)
        {
            string result = program.MakeFancyString(test);
            Console.WriteLine($"輸入: {test}，結果: {result}");
        }
    }


    /// <summary>
    /// 刪除字元使字串變好：
    /// 給定一個字串 s，需刪除最少數量的字元，使刪除後的字串不會有三個連續相同的字元。
    /// 解題思路：
    /// 逐字遍歷原字串，將每個字元加入 StringBuilder，
    /// 若發現最後三個字元相同，則移除最後一個，確保不會有三連相同字元。
    /// 時間複雜度 O(n)，空間複雜度 O(n)。
    /// <example>
    /// <code>
    /// MakeFancyString("aaabaaaa") // 回傳 "aabaa"
    /// MakeFancyString("aab") // 回傳 "aab"
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="s">原始字串</param>
    /// <returns>刪除後的最終字串</returns>
    public string MakeFancyString(string s)
    {
        // 使用 StringBuilder 來高效組合字元
        StringBuilder sb = new StringBuilder();
        int n = s.Length;
        for (int i = 0; i < n; i++)
        {
            // 將目前字元加入 sb
            sb.Append(s[i]);
            // 檢查 sb 最後三個字元是否相同
            if (i >= 2 && sb[sb.Length - 1] == sb[sb.Length - 2] && sb[sb.Length - 2] == sb[sb.Length - 3])
            {
                // 若三連相同，移除最後一個字元，避免三連重複
                sb.Length--;
            }
        }
        // 回傳處理後的字串
        return sb.ToString();
    }
}
