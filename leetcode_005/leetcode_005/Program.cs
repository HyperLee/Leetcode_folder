namespace leetcode_005;

class Program
{
    /// <summary>
    /// 5. Longest Palindromic Substring
    /// https://leetcode.com/problems/longest-palindromic-substring/description/
    /// 5. 最长回文子串
    /// https://leetcode.cn/problems/longest-palindromic-substring/description/
    /// 
    /// English:
    /// Given a string s, return the longest palindromic substring in s.
    ///
    /// Traditional Chinese:
    /// 給定一個字串 s，回傳 s 中最長的回文子字串。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    /// <summary>
    /// 方法一: 暴力法
    /// 使用 3 层循环 来依次对所有子串进行检查，将最长的子串最为最终结果返回。
    /// 下面代码中，我们检查 i 到 j 的子串是否是回文串，如果是且长度大于当前结果 result 的长度，
    /// 就将 result 更新为 i 到 j 的子串。
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public string LongestPalindrome(string s)
    {
        string res = "";
        int n = s.Length;

        for(int i = 0; i < n; i++)
        {
            for(int j = 0; j < n; j++)
            {
                //檢查s[i]到s[j]是否是回文, 如果是且長度大於result長度就更新他
                int p = i;
                int q = j;
                bool isPalindromic = true;

                while(p < q)
                {
                    if(s[p++] != s[q--])
                    {
                        isPalindromic = false;
                        break;
                    }
                }

                if(isPalindromic)
                {
                    int len = j - i + 1;
                    if(len > res.Length)
                    {
                        res = s.Substring(i, len);
                    }
                }
            }
        }
        return res;
    }

    /// <summary>
    /// 中心扩展法
    /// 思路在于遍历找到各个可以作为中点的单个字母或两个相同的字母，
    /// 然后利用 ExpendCenter 方法来获取最长的结果并和最终结果比较，取最长。
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public string LongestPalindrome2(string s)
    {
        if(s.Length <= 1)
        {
            return s;
        }

        string res = s.Substring(0, 1);
        for(int i = 0; i < s.Length; i++)
        {
            string temp1 = ExpendCenter(s, i, i);
            string temp2 = ExpendCenter(s, i, i + 1);
            
            if(res.Length < temp1.Length)
            {
                res = temp1;
            }

            if(res.Length < temp2.Length)
            {
                res = temp2;
            }
        }
        return res;
    }

    /// <summary>
    /// 左右擴展
    /// 找出左右是否相同
    /// </summary>
    /// <param name="s"></param>
    /// <param name="l"></param>
    /// <param name="r"></param>
    /// <returns></returns>
    public string ExpendCenter(string s, int l, int r)
    {
        while(l >= 0 && r < s.Length && s[l] == s[r])
        {
            l--;
            r++;
        }
        return s.Substring(l + 1, r - l - 1);
    }

    /// <summary>
    /// 方法三 : 中心扩展法
    /// 此方法比方法二 效率還要好
    /// 但是計算方法 必較麻煩
    /// 要看連結說明
    /// 
    /// 因為 mid 取浮點數, 所以 end 要開兩倍 n 儲存 2(n - 1)
    /// 因為要處理輸入 s 長度為偶數問題, mid 才會用浮點數來處理
    /// 
    /// Math.Floor: 向下取整數  向負無限取整
    /// Value: 7.03, Floor: 7
    /// Value: 7.64, Floor: 7
    /// Value: 0.12, Floor: 0
    /// Value: -0.12, Floor: -1
    /// Value: -7.1, Floor: -8
    /// Value: -7.6, Floor: -8
    /// 
    /// Math.Ceiling: 向上取整數 向正無限取整
    /// Value: 7.03, Ceiling: 8
    /// Value: 7.64, Ceiling: 8
    /// Value: 0.12, Ceiling: 1
    /// Value: -0.12, Ceiling: 0
    /// Value: -7.1, Ceiling: -7
    /// Value: -7.6, Ceiling: -7
    /// 
    /// 中心拓展法: 
    /// 簡單說就是 遍歷每個 i
    /// 然後假設 i 為中心點接著左右擴展開來比對
    /// 當左右擴展一格也是相同, 就接續擴展
    /// 當遇到不同時候就跳出
    /// 此時計算長度, 擷取文字下來
    /// 用左邊界為起始點, 長度為 右邊界扣除左邊界
    /// 
    /// 左右擴展都是用 + 1 為單位去擴展
    /// 逐個比對是否相同
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public string LongestPalindrome3(string s)
    {
        string res = "";
        int n = s.Length;
        int end = 2 * n - 1;

        for(int i = 0; i < end; i++)
        {
            double mid = i / 2.0;
            // 左邊界
            int p = (int)(Math.Floor(mid));
            // 右邊界
            int q = (int)(Math.Ceiling(mid));

            // 遍歷 s
            while(p >= 0 && q < n)
            {
                string s_p = s[p].ToString();
                string s_q = s[q].ToString();

                if(s[p] != s[q])
                {
                    // 不同就跳出
                    break;
                }

                // 已 mid 為中心將左右邊界同時擴大
                p--;
                q++;
            }

            // 取長度
            int len = q - p - 1;
            if(len >= res.Length)
            {
                // 更新
                res = s.Substring(p + 1, len);
            }
        }
        return res;
    }
}
