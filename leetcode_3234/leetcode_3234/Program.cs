namespace leetcode_3234;

class Program
{
    /// <summary>
    /// 3234. Count the Number of Substrings With Dominant Ones
    /// https://leetcode.com/problems/count-the-number-of-substrings-with-dominant-ones/description/?envType=daily-question&envId=2025-11-15
    /// 3234. 统计 1 显著的字符串的数量
    /// https://leetcode.cn/problems/count-the-number-of-substrings-with-dominant-ones/description/?envType=daily-question&envId=2025-11-15
    ///
    /// 题目描述：给定一个二进位字串 s，返回具有“1 顯著”的子字串數量。
    /// 子字串若滿足 '1' 的數量 >= ('0' 的數量)^2，則稱其為“1 顯著”的子字串。
    ///
    /// 例如：s = "001101"，請統計所有子字串中符合上述條件的個數。
    ///
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        
        // 測試案例 1
        string test1 = "00011";
        int result1 = solution.NumberOfSubstrings(test1);
        Console.WriteLine($"輸入: {test1}");
        Console.WriteLine($"輸出: {result1}");
        Console.WriteLine($"預期: 5");
        Console.WriteLine();
        
        // 測試案例 2
        string test2 = "101101";
        int result2 = solution.NumberOfSubstrings(test2);
        Console.WriteLine($"輸入: {test2}");
        Console.WriteLine($"輸出: {result2}");
        Console.WriteLine($"預期: 16");
        Console.WriteLine();
        
        // 測試案例 3
        string test3 = "001101";
        int result3 = solution.NumberOfSubstrings(test3);
        Console.WriteLine($"輸入: {test3}");
        Console.WriteLine($"輸出: {result3}");
        Console.WriteLine();
    }


    /// <summary>
    /// 方法一：枚舉法
    /// 
    /// 解題思路：
    /// 1. 不直接枚舉所有子字串，而是枚舉子字串中 '0' 出現的次數（範圍：0 到 √n）
    /// 2. 對於每個右邊界 i，枚舉包含的 '0' 個數 cnt0
    /// 3. 使用 pre[] 陣列記錄每個位置之前最近的 '0' 的位置
    /// 4. 計算在第 cnt0 個和第 cnt0+1 個 '0' 之間有多少個合法的左邊界
    /// 5. 合法條件：'1' 的數量 >= ('0' 的數量)^2
    /// 
    /// 時間複雜度：O(n√n)，其中 n 是字串長度
    /// 空間複雜度：O(n)，用於儲存 pre 陣列
    /// </summary>
    /// <param name="s">輸入的二進位字串</param>
    /// <returns>返回「1 顯著」的子字串數量</returns>
    public int NumberOfSubstrings(string s)
    {
        int n = s.Length;
        
        // pre[i] 記錄位置 i 之前最近的一個 '0' 出現的位置
        // pre[0] = -1 作為哨兵，方便處理字串最左側的連續 '1'
        int[] pre = new int[n + 1];
        pre[0] = -1;
        
        // 建構 pre 陣列：記錄每個位置之前最近的 '0' 的位置
        for(int i = 0; i < n; i++)
        {
            if(i == 0 || (i > 0 && s[i - 1] == '0'))
            {
                // 如果是第一個位置或前一個字元是 '0'，則當前位置就是最近的 '0'
                pre[i + 1] = i;
            }
            else
            {
                // 否則繼承前一個位置的值
                pre[i + 1] = pre[i];
            }
        }

        int res = 0;
        
        // 枚舉右邊界 i（從 1 到 n）
        for(int i = 1; i <= n; i++)
        {
            // 計算以位置 i 為右端點時，當前位置是否為 '0'
            int cnt0 = s[i - 1] == '0' ? 1 : 0;
            int j = i;
            
            // 枚舉包含的 '0' 的個數（從 1 到 √n）
            // 當 cnt0^2 > n 時停止，因為此時不可能有足夠的 '1' 來滿足條件
            while(j > 0 && cnt0 * cnt0 <= n)
            {
                // cnt1：從位置 j 到位置 i 之間 '1' 的個數
                // 計算方式：總長度 (i - pre[j]) 減去 '0' 的個數 (cnt0)
                int cnt1 = (i - pre[j]) - cnt0;
                
                // 判斷是否滿足「1 顯著」條件：cnt1 >= cnt0^2
                if(cnt0 * cnt0 <= cnt1)
                {
                    // 計算合法的左邊界數量
                    // j - pre[j]：第 cnt0 個和第 cnt0+1 個 '0' 之間的距離（區間長度限制）
                    // cnt1 - cnt0 * cnt0 + 1：在保證 '1' 數量足夠的前提下，可以向左延伸的位置數（條件限制）
                    // 取兩者的最小值，因為必須同時滿足區間長度和條件限制
                    res += Math.Min(j - pre[j], cnt1 - cnt0 * cnt0 + 1);
                }
                
                // 移動到前一個 '0' 的位置，繼續枚舉
                j = pre[j];
                cnt0++;
            }
        }
        
        return res;
    }

}
