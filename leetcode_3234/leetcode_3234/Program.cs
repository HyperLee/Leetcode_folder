namespace leetcode_3234;

class Program
{
    /// <summary>
    /// 3234. Count the Number of Substrings With Dominant Ones
    /// https://leetcode.com/problems/count-the-number-of-substrings-with-dominant-ones/description/
    /// <para>
    /// You are given a binary string s.
    ///
    /// Return the number of substrings with dominant ones.
    ///
    /// A string has dominant ones if its number of ones is at least the square of its number of zeros.
    ///
    /// Example 1:
    /// Input: s = "00011"
    /// Output: 5
    /// Explanation: The dominant substrings are s[3..3] = "1" with 0 zeros and 1 one; s[4..4] = "1" with 0 zeros and 1 one; s[2..3] = "01" with 1 zero and 1 one; s[3..4] = "11" with 0 zeros and 2 ones; and s[2..4] = "011" with 1 zero and 2 ones.
    ///
    /// Example 2:
    /// Input: s = "101101"
    /// Output: 16
    /// Explanation: There are 21 substrings total. The 5 non-dominant substrings are s[1..1] = "0" with 1 zero and 0 ones; s[4..4] = "0" with 1 zero and 0 ones; s[1..4] = "0110" with 2 zeros and 2 ones; s[0..4] = "10110" with 2 zeros and 3 ones; and s[1..5] = "01101" with 2 zeros and 3 ones. Therefore, 16 substrings have dominant ones.
    ///
    /// Constraints:
    /// - 1 &lt;= s.length &lt;= 4 * 10^4
    /// - s consists only of '0' and '1'.
    /// </para>
    /// <para>
    /// 3234. 計算 1 佔優勢的子字串數量
    /// https://leetcode.cn/problems/count-the-number-of-substrings-with-dominant-ones/description/
    ///
    /// 給定一個二進位字串 s。
    ///
    /// 回傳 1 佔優勢的子字串數量。
    ///
    /// 若字串中的 1 數量至少等於 0 數量的平方，則稱該字串的 1 佔優勢。
    ///
    /// 範例 1：
    /// 輸入：s = "00011"
    /// 輸出：5
    /// 解釋：符合條件的子字串為 s[3..3] = "1"，含 0 個零與 1 個一；s[4..4] = "1"，含 0 個零與 1 個一；s[2..3] = "01"，含 1 個零與 1 個一；s[3..4] = "11"，含 0 個零與 2 個一；s[2..4] = "011"，含 1 個零與 2 個一。
    ///
    /// 範例 2：
    /// 輸入：s = "101101"
    /// 輸出：16
    /// 解釋：總共有 21 個子字串。5 個不符合條件的子字串為 s[1..1] = "0"，含 1 個零與 0 個一；s[4..4] = "0"，含 1 個零與 0 個一；s[1..4] = "0110"，含 2 個零與 2 個一；s[0..4] = "10110"，含 2 個零與 3 個一；s[1..5] = "01101"，含 2 個零與 3 個一。因此有 16 個一佔優勢的子字串。
    ///
    /// 限制條件：
    /// - 1 &lt;= s.length &lt;= 4 * 10^4
    /// - s 只由 '0' 與 '1' 組成。
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new Program();
        
        Console.WriteLine("=== 方法一：枚舉法（使用 pre 陣列） ===");
        Console.WriteLine();
        
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
        
        Console.WriteLine("========================================");
        Console.WriteLine();
        Console.WriteLine("=== 方法二：優化的枚舉法（記錄 0 的下標） ===");
        Console.WriteLine();
        
        // 測試案例 1 - 方法二
        int result1_m2 = solution.NumberOfSubstrings_Method2(test1);
        Console.WriteLine($"輸入: {test1}");
        Console.WriteLine($"輸出: {result1_m2}");
        Console.WriteLine($"預期: 5");
        Console.WriteLine();
        
        // 測試案例 2 - 方法二
        int result2_m2 = solution.NumberOfSubstrings_Method2(test2);
        Console.WriteLine($"輸入: {test2}");
        Console.WriteLine($"輸出: {result2_m2}");
        Console.WriteLine($"預期: 16");
        Console.WriteLine();
        
        // 測試案例 3 - 方法二
        int result3_m2 = solution.NumberOfSubstrings_Method2(test3);
        Console.WriteLine($"輸入: {test3}");
        Console.WriteLine($"輸出: {result3_m2}");
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

    /// <summary>
    /// 方法二：優化的枚舉法（記錄 0 的下標）
    /// 
    /// 解題思路：
    /// 1. 觀察：1 顯著子字串最多包含 √n 個 '0'（因為 cnt0² ≤ cnt1 ≤ n）
    /// 2. 使用陣列 pos0 記錄所有 '0' 的下標，pos0[0] = -1 作為哨兵
    /// 3. 枚舉右端點 r，分別計算恰好包含 0, 1, 2, ..., √n 個 '0' 的子字串數量
    /// 4. 對於恰好包含 cnt0 個 '0' 的子字串：
    ///    - 設左右兩個 '0' 的位置為 p 和 q
    ///    - 計算最短子字串 [q,r] 中 '1' 的個數 cnt1
    ///    - 合法左端點範圍：[p+1, q - max(cnt0² - cnt1, 0)]
    /// 5. 單獨計算不含 '0' 的子字串（每次遇到 '1' 時累加）
    /// 
    /// 優化點：
    /// - 直接記錄 '0' 的位置，避免重複查找
    /// - 倒序遍歷 pos0，直接計算 cnt0 = size - i
    /// - 提前終止條件：cnt0² > total1（目前累積的 '1' 總數）
    /// 
    /// 時間複雜度：O(n√n)，其中 n 是字串長度
    /// 空間複雜度：O(n)，用於儲存 pos0 陣列
    /// </summary>
    /// <param name="s">輸入的二進位字串</param>
    /// <returns>返回「1 顯著」的子字串數量</returns>
    public int NumberOfSubstrings_Method2(string s)
    {
        int n = s.Length;
        
        // pos0[i] 記錄第 i 個 '0' 的下標
        // pos0[0] = -1 作為哨兵，方便處理 cnt0 達到最大時的計數
        int[] pos0 = new int[n + 1];
        pos0[0] = -1;
        
        int size = 1; // pos0 陣列的有效長度（已記錄的 '0' 的個數 + 1）
        int total1 = 0; // [0, r] 區間中 '1' 的累積個數
        int ans = 0;

        // 枚舉右端點 r
        for(int r = 0; r < n; r++)
        {
            if(s[r] == '0')
            {
                // 記錄 '0' 的下標
                pos0[size++] = r;
            }
            else
            {
                // 累加 '1' 的個數
                total1++;
                
                // 單獨計算不含 '0' 的子字串個數
                // 右端點為 r，左端點可以是 [pos0[size-1]+1, r]，共 r - pos0[size-1] 個
                ans += r - pos0[size - 1];
            }

            // 倒序遍歷 pos0，枚舉子字串中 '0' 的個數
            // cnt0 = size - i（當前已記錄的 '0' 的個數 - 索引 = 實際包含的 '0' 的個數）
            for(int i = size - 1; i > 0 && (size - i) * (size - i) <= total1; i--)
            {
                // p: 更左邊的 '0' 的位置（左端點的最小值 = p + 1）
                int p = pos0[i - 1];
                
                // q: 當前枚舉的 '0' 的位置（最短子字串的左端點）
                int q = pos0[i];
                
                // cnt0: 子字串中 '0' 的個數
                int cnt0 = size - i;
                
                // cnt1: [q, r] 區間中 '1' 的個數
                // 計算方式：區間長度 (r - q + 1) - '0' 的個數 (cnt0)
                int cnt1 = r - q + 1 - cnt0;
                
                // 計算合法的左端點個數
                // 左端點最小值：p + 1
                // 左端點最大值：q - max(cnt0² - cnt1, 0)
                // 合法左端點個數：max(左端點最大值 - p, 0)
                ans += Math.Max(q - Math.Max(cnt0 * cnt0 - cnt1, 0) - p, 0);
            }
        }

        return ans;
    }

}
