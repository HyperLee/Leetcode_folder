namespace leetcode_014;

class Program
{
    /// <summary>
    /// 14. Longest Common Prefix
    /// https://leetcode.com/problems/longest-common-prefix/description/
    /// 14. 最長共同前綴
    /// https://leetcode.cn/problems/longest-common-prefix/description/
    /// 
    /// English:
    /// Write a function to find the longest common prefix string amongst an array of strings.
    /// If there is no common prefix, return an empty string "".
    ///
    /// 繁體中文：
    /// 撰寫一個函式，用來找出字串陣列中最長的共同前綴字串。
    /// 如果沒有共同前綴，請回傳空字串 ""。
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 解法一
    /// 
    /// 先找出輸入 strs 陣列中, 最短的字串
    /// 利用 最短字串長度 與 最短字串
    /// 去跟 "其他組字串" 來做文字比對
    /// 找出 相同的 文字出來
    /// 找到相同就加入 共同前綴 
    /// </summary>
    /// <param name="strs"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="strs"></param>
    /// <returns></returns>
    public string LongestCommonPrefix(string[] strs)
    {
        // 求出最短字串長度
        int shortlength = int.MaxValue;
        // 最短字串
        string shortstring = "";
        string res = "";

        // 找出最短字串以及長度
        for(int i = 0; i < strs.Length; i++)
        {
            if(strs[i].Length < shortlength)
            {
                // 最短字串長度
                shortlength = strs[i].Length;
                // 最短字串
                shortstring = strs[i];
            }
        }

        // 遍歷  "最短的字串" 和陣列中 "其他字串" 相比較, 比較次數上限為 "最短字串的長度"
        for(int i = 0; i < shortlength; i++)
        {
            // j: strs 陣列中, 第 j 個字串
            for(int j = 0; j < strs.Length; j++)
            {
                if(shortstring[i] != strs[j][i])
                {
                    // 不存在回傳空字串
                    return res;
                }
            }

            // 找到共同前綴字, 加入
            res += shortstring[i];
        }
        return res;
    }

    /// <summary>
    /// Data Structures and Algorithms 常見的「縱向掃描（Vertical Scanning）」解法。
    /// ex:
    /// ​flower
    /// flow
    /// flight
    /// 从左到右，竖着看，第一列全是 f，第二列全是 l，第三列就不全一样了，所以「最长公共前缀」是 fl。
    /// 
    /// 具体算法如下：
    /// 1. 从左到右遍历 strs 的每一列。
    /// ​2. 设当前遍历到第 j 列，从上到下遍历这一列的字母。
    /// 3. 设当前遍历到第 i 行，即 strs[i][j]。如果 j 等于 strs[i] 的长度，或者 strs[i][j]=strs[0][j]，说明这一列的字母缺失或者不全一
    /// 样，那么最长公共前缀的长度等于 j，返回 strs[0] 的长为 j 的前缀。
    /// 4. 如果没有中途返回，说明所有字符串都有一个等于 strs[0] 的前缀，那么最长公共前缀就是 strs[0]。
    /// </summary>
    /// <param name="strs"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="strs"></param>
    /// <returns></returns>
    public string LongestCommonPrefix(string[] strs)
    {
        string s0 = strs[0];
        // 從左至右
        for(int j = 0; j < s0.Length; j++)
        {
            char c = s0[j];

            // 從上到下檢查每個字串
            foreach(string s in strs)
            {
                // 如果字串長度不夠, 或目前字元不同
                if(j == s.Length || s[j] != c)
                {
                    // 回傳 0 ~ j - 1 的共同前綴
                    return s0.Substring(0, j);
                }
            }
        }
        // 全部都相同
        return s0;
    }
}
