namespace leetcode_438;

class Program
{
    /// <summary>
    /// 438. Find All Anagrams in a String
    /// https://leetcode.com/problems/find-all-anagrams-in-a-string/description/
    /// 438. 找出字串中所有字母異位詞
    /// https://leetcode.cn/problems/find-all-anagrams-in-a-string/description/
    /// 
    /// </summary>
    /// <param name="args"></param> 
    static void Main(string[] args)
    {
        // 測試案例1: 基本案例
        Console.WriteLine("測試案例1 - 基本案例:");
        TestFindAnagrams("cbaebabacd", "abc");

        // 測試案例2: 連續重複的情況
        Console.WriteLine("\n測試案例2 - 連續重複的情況:");
        TestFindAnagrams("abab", "ab");

        // 測試案例3: 空字串情況
        Console.WriteLine("\n測試案例3 - 空字串情況:");
        TestFindAnagrams("", "abc");

        // 測試案例4: 目標字串比來源字串長
        Console.WriteLine("\n測試案例4 - 目標字串較長:");
        TestFindAnagrams("ab", "abc");

        // 測試案例5: 所有字母相同的情況
        Console.WriteLine("\n測試案例5 - 相同字母:");
        TestFindAnagrams("aaaaaaa", "aa");
    }

    private static void TestFindAnagrams(string s, string p)
    {
        Console.WriteLine($"來源字串: {s}");
        Console.WriteLine($"目標字串: {p}");
        var result = FindAnagrams(s, p);
        Console.WriteLine($"找到的異位詞起始索引: {string.Join(", ", result)}");
    }

    /// <summary>
    /// 尋找字串中所有字母異位詞的起始索引
    /// 
    /// 解題概念：
    /// 1. 使用滑動視窗（Sliding Window）配合計數器追蹤字母頻率
    /// 2. 只需一個計數器陣列，初始記錄目標字串 p 的字母頻率
    /// 3. 透過左右指針控制視窗大小，動態調整計數器
    /// 4. 當計數器某字母小於0時，表示當前視窗包含過多該字母
    /// 
    /// 時間複雜度：O(n)，只需遍歷一次字串
    /// 空間複雜度：O(1)，使用固定大小的計數器陣列(26個字母)
    /// </summary>
    public static IList<int> FindAnagrams(string s, string p)
    {
        var result = new List<int>();
        // 步驟1: 基礎檢查，確保輸入有效
        if (string.IsNullOrEmpty(s) || s.Length < p.Length) 
        {
            return result;
        }

        // 步驟2: 初始化計數器陣列
        var count = new int[26];
        // 記錄目標字串p中每個字母需要的數量
        foreach (char c in p)
        {
            count[c - 'a']++;
        }

        // 步驟3: 使用雙指針技巧實現滑動視窗
        int left = 0;
        // 拓展右視窗
        for (int right = 0; right < s.Length; right++)
        {
            // 步驟4: 處理右指針，將新字母加入視窗
            int currentChar = s[right] - 'a';
            count[currentChar]--;  // 消耗一個當前字母的配額

            // 步驟5: 調整左指針，處理超出配額的情況; 收縮視窗
            while (count[currentChar] < 0)  // 當前字母出現次數超過需求;表示當前視窗中某個字母的出現次數超過了目標字串中的需求
            {
                count[s[left] - 'a']++;  // 恢復左側字母的配額, 維護正確的字母頻率統計
                left++;  // 縮小視窗
            }

            // 步驟6: 檢查是否找到有效的異位詞; 檢查結果
            // 當視窗大小正好等於目標字串長度時，表示找到一個異位詞
            if (right - left + 1 == p.Length)
            {
                // 記錄異位詞的起始位置
                // 在滑動視窗算法中，left 代表當前視窗的起始位置，而 right 代表結束位置。我們要記錄的是異位詞的起始位置，所以需要加入 left。
                // 題目要求找出所有異位詞的「起始」索引; left 指針永遠指向當前視窗的起始位置
                result.Add(left);  
            }
        }

        return result;
    }
}
