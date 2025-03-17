namespace leetcode_076;

class Program
{
    /// <summary>
    /// 76. Minimum Window Substring
    /// https://leetcode.com/problems/minimum-window-substring/description/
    /// 76. 最小覆蓋子串
    /// https://leetcode.cn/problems/minimum-window-substring/description/
    /// </summary>
    /// <param name="args"></param> 
    static void Main(string[] args)
    {
        // 測試案例 1: 一般情況
        string s1 = "ADOBECODEBANC";
        string t1 = "ABC";
        Console.WriteLine("測試案例 1 (一般情況):");
        Console.WriteLine($"輸入: s = {s1}, t = {t1}");
        Console.WriteLine($"輸出: {MinWindow(s1, t1)}\n");

        // 測試案例 2: 目標字串包含重複字符
        string s2 = "ADOBECODEBANCBA";
        string t2 = "AABC";
        Console.WriteLine("測試案例 2 (重複字符):");
        Console.WriteLine($"輸入: s = {s2}, t = {t2}");
        Console.WriteLine($"輸出: {MinWindow(s2, t2)}\n");

        // 測試案例 3: 找不到符合的子串
        string s3 = "ADOBECODEBANC";
        string t3 = "XYZ";
        Console.WriteLine("測試案例 3 (無解情況):");
        Console.WriteLine($"輸入: s = {s3}, t = {t3}");
        Console.WriteLine($"輸出: {MinWindow(s3, t3)}\n");

        // 測試案例 4: 完全相同的字串
        string s4 = "ABC";
        string t4 = "ABC";
        Console.WriteLine("測試案例 4 (完全相同):");
        Console.WriteLine($"輸入: s = {s4}, t = {t4}");
        Console.WriteLine($"輸出: {MinWindow(s4, t4)}\n");

        // 測試案例 5: 單字符情況
        string s5 = "A";
        string t5 = "A";
        Console.WriteLine("測試案例 5 (單字符):");
        Console.WriteLine($"輸入: s = {s5}, t = {t5}");
        Console.WriteLine($"輸出: {MinWindow(s5, t5)}");
    }

    /// <summary>
    /// 解題思路：使用滑動窗口（Sliding Window）技術
    /// 1. 使用兩個指針 left 和 right 形成一個窗口
    /// 2. 右指針不斷向右移動擴大窗口，直到窗口包含所有 t 中的字符
    /// 3. 當找到一個可行解後，左指針向右移動縮小窗口，尋找最優解
    /// 4. 在這個過程中不斷更新最小窗口的位置
    /// 時間複雜度：O(n)，其中 n 是字符串 s 的長度
    /// 空間複雜度：O(k)，其中 k 是字符集大小，本題中 k=128
    /// </summary>
    /// <param name="S">源字符串</param>
    /// <param name="t">目標字符串</param>
    /// <returns>包含所有目標字符的最小子串</returns>
    public static string MinWindow(string S, string t)
    {
        char[] s = S.ToCharArray();
        int m = s.Length;
        int ansLeft = -1;           // 最小窗口的左邊界
        int ansRight = m;           // 最小窗口的右邊界
        int[] cntS = new int[128];  // 記錄窗口中每個字符的出現次數
        int[] cntT = new int[128];  // 記錄目標字符串中每個字符的出現次數

        // 統計目標字符串中每個字符的出現次數
        foreach(char c in t.ToCharArray())
        {
            cntT[c]++;
        }

        int left = 0;  // 窗口左邊界
        for(int right = 0; right < m; right++)  
        {
            // 擴大窗口：將右邊界的字符納入統計
            // 加入新字符
            cntS[s[right]]++;  

            // 當前窗口包含所有目標字符時，嘗試縮小窗口
            // 檢查是否包含所有目標字符
            while(isCovered(cntS, cntT))  
            {
                // 更新最小窗口的位置
                if(right - left < ansRight - ansLeft)  
                {
                    ansLeft = left;
                    ansRight = right;
                }

                // 縮小窗口：將左邊界的字符移出統計
                cntS[s[left]]--;
                left++;
            }
        }
        // 如果沒找到可行解，返回空字符串；否則返回最小窗口子串
        return ansLeft < 0 ? "" : S.Substring(ansLeft, ansRight - ansLeft + 1);
    }

    /// <summary>
    /// 檢查當前窗口是否包含目標字符串的所有字符
    /// 通過比較當前窗口中每個字符的出現次數是否大於等於目標字符串中對應字符的出現次數
    /// 
    /// 這個函數在滑動窗口算法中被反複調用，用於：
    /// 1. 判斷當前窗口是否可以開始收縮
    /// 2. 確保窗口收縮過程中維持所需的所有字符
    /// 
    /// 只檢查 ASCII 範圍內有出現在目標字串的字符
    /// 避免檢查不必要的字符範圍 
    /// </summary>
    /// <param name="cntS">當前窗口中字符的出現次數數組</param>
    /// <param name="cntT">目標字符串中字符的出現次數數組</param>
    /// <returns>true 表示當前窗口涵蓋所有目標字符，false 則表示未涵蓋</returns>
    private static bool isCovered(int[] cntS, int[] cntT)
    {
        // 只檢查目標字串中出現的字符
        for (int i = 0; i < 128; i++)
        {
            // 如果 cntT[i] > 0，表示這個字符是我們需要匹配的目標字符, 這樣可以跳過不需要關注的字符，提高效率
            // 如果 cntS[i] < cntT[i]，表示當前窗口中缺少足夠的字符 i
            if (cntT[i] > 0 && cntS[i] < cntT[i])
            {
                return false;
            }
        }
        return true;
    }
}
