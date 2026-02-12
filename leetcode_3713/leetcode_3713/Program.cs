using System.ComponentModel;

namespace leetcode_3713;

class Program
{
    /// <summary>
    /// 3713. Longest Balanced Substring I
    /// https://leetcode.com/problems/longest-balanced-substring-i/description/?envType=daily-question&envId=2026-02-12
    /// 3713. 最长的平衡子串 I
    /// https://leetcode.cn/problems/longest-balanced-substring-i/description/?envType=daily-question&envId=2026-02-12
    /// 
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program program = new Program();
        
        // 測試案例 1: "cabbacc" - 預期輸出: 4
        string test1 = "cabbacc";
        int result1 = program.LongestBalanced(test1);
        Console.WriteLine($"測試 1: 輸入 = \"{test1}\", 輸出 = {result1}");
        
        // 測試案例 2: "aabbcc" - 預期輸出: 6
        string test2 = "aabbcc";
        int result2 = program.LongestBalanced(test2);
        Console.WriteLine($"測試 2: 輸入 = \"{test2}\", 輸出 = {result2}");
        
        // 測試案例 3: "aaaa" - 預期輸出: 4
        string test3 = "aaaa";
        int result3 = program.LongestBalanced(test3);
        Console.WriteLine($"測試 3: 輸入 = \"{test3}\", 輸出 = {result3}");
        
        // 測試案例 4: "abc" - 預期輸出: 1
        string test4 = "abc";
        int result4 = program.LongestBalanced(test4);
        Console.WriteLine($"測試 4: 輸入 = \"{test4}\", 輸出 = {result4}");
    }

    /// <summary>
    /// LeetCode 3713 - 尋找最長的平衡子字串
    /// 
    /// 題目說明：
    /// 給定一個僅由小寫英文字母組成的字串 s，找出最長的平衡子字串的長度。
    /// 平衡子字串的定義：子字串中所有出現的字符的出現次數都相同。
    /// 
    /// 解題思路：
    /// 使用枚舉法，逐一檢查所有可能的子字串。
    /// 1. 外層迴圈枚舉子字串的左端點 i (起始位置)。
    /// 2. 內層迴圈枚舉子字串的右端點 j (結束位置)，範圍從 i 到 n-1。
    /// 3. 在枚舉右端點的過程中，使用陣列 cnt 統計每種字符的出現次數。
    /// 4. 每次擴展右端點後，檢查當前子字串是否平衡：
    ///    - 遍歷 cnt 陣列，確認所有已出現字符的出現次數是否都相同。
    /// 5. 若當前子字串平衡，則更新最長平衡子字串的長度。
    /// 
    /// 時間複雜度：O(n^2 * 26) = O(n^2)，其中 n 為字串長度。
    /// 空間複雜度：O(26) = O(1)，只使用固定大小的陣列。
    /// </summary>
    /// <param name="s">輸入的字串，僅包含小寫英文字母</param>
    /// <returns>最長平衡子字串的長度</returns>
    public int LongestBalanced(string s)
    {
        int n = s.Length;
        int res = 0; // 記錄最長平衡子字串的長度
        int[] cnt = new int[26]; // 統計 26 個小寫字母的出現次數

        // 枚舉所有可能的子字串左端點
        for(int i = 0; i < n; i++)
        {
            // 每次開始新的左端點時，重置字符計數陣列
            Array.Fill(cnt, 0);
            
            // 枚舉所有可能的子字串右端點
            for(int j = i; j < n; j++)
            {
                bool flag = true; // 標記當前子字串是否平衡
                int c = s[j] - 'a'; // 將字符轉換為陣列索引 (0-25)
                cnt[c]++; // 增加當前字符的出現次數

                // 檢查所有已出現的字符，其出現次數是否都相同
                foreach(int x in cnt)
                {
                    // 若某字符已出現 (x > 0) 且其出現次數不等於剛加入的字符出現次數
                    // 則當前子字串不平衡
                    if(x > 0 && x != cnt[c])
                    {
                        flag = false;
                        break;
                    }
                }

                // 若當前子字串平衡，更新最長長度
                if(flag)
                {
                    res = Math.Max(res, j - i + 1);
                }
            }
        }
        return res;
    }
}
