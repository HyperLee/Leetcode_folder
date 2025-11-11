using System.Net.Http.Headers;

namespace leetcode_474;

class Program
{
    /// <summary>
    /// 474. Ones and Zeroes
    /// https://leetcode.com/problems/ones-and-zeroes/description/?envType=daily-question&envId=2025-11-11
    /// 474. 一和零
    /// https://leetcode.cn/problems/ones-and-zeroes/description/?envType=daily-question&envId=2025-11-11
    /// 
    /// 給定一個二進位字串陣列 strs 和兩個整數 m 和 n。
    /// 返回 strs 中最大子集的大小，使得子集中最多有 m 個 0 和 n 個 1。
    /// 如果集合 x 是集合 y 的子集，則 x 中的所有元素也是 y 中的元素。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Program solution = new Program();

        // 測試案例 1
        // 輸入：strs = ["10","0001","111001","1","0"], m = 5, n = 3
        // 輸出：4
        // 解釋：最多有 5 個 0 和 3 個 1 的最大子集是 {"10","0001","1","0"}，因此答案是 4
        string[] strs1 = ["10", "0001", "111001", "1", "0"];
        int m1 = 5, n1 = 3;
        int result1 = solution.FindMaxForm(strs1, m1, n1);
        Console.WriteLine($"測試案例 1:");
        Console.WriteLine($"輸入：strs = [{string.Join(", ", strs1.Select(s => $"\"{s}\""))}], m = {m1}, n = {n1}");
        Console.WriteLine($"輸出：{result1}");
        Console.WriteLine($"預期：4");
        Console.WriteLine();

        // 測試案例 2
        // 輸入：strs = ["10","0","1"], m = 1, n = 1
        // 輸出：2
        // 解釋：最大子集是 {"0", "1"}，因此答案是 2
        string[] strs2 = ["10", "0", "1"];
        int m2 = 1, n2 = 1;
        int result2 = solution.FindMaxForm(strs2, m2, n2);
        Console.WriteLine($"測試案例 2:");
        Console.WriteLine($"輸入：strs = [{string.Join(", ", strs2.Select(s => $"\"{s}\""))}], m = {m2}, n = {n2}");
        Console.WriteLine($"輸出：{result2}");
        Console.WriteLine($"預期：2");
        Console.WriteLine();

        // 測試案例 3
        // 輸入：strs = ["10","0001","111001","1","0"], m = 3, n = 4
        // 輸出：3
        string[] strs3 = ["10", "0001", "111001", "1", "0"];
        int m3 = 3, n3 = 4;
        int result3 = solution.FindMaxForm(strs3, m3, n3);
        Console.WriteLine($"測試案例 3:");
        Console.WriteLine($"輸入：strs = [{string.Join(", ", strs3.Select(s => $"\"{s}\""))}], m = {m3}, n = {n3}");
        Console.WriteLine($"輸出：{result3}");
        Console.WriteLine($"預期：3");
        Console.WriteLine();

        // ===== 測試優化版本 =====
        Console.WriteLine("===== 使用滾動陣列優化版本測試 =====");
        Console.WriteLine();

        // 優化版本測試案例 1
        int resultOpt1 = solution.FindMaxFormOptimized(strs1, m1, n1);
        Console.WriteLine($"優化測試案例 1:");
        Console.WriteLine($"輸入：strs = [{string.Join(", ", strs1.Select(s => $"\"{s}\""))}], m = {m1}, n = {n1}");
        Console.WriteLine($"輸出：{resultOpt1}");
        Console.WriteLine($"預期：4");
        Console.WriteLine($"結果：{(resultOpt1 == result1 ? "✓ 與原版本一致" : "✗ 結果不一致")}");
        Console.WriteLine();

        // 優化版本測試案例 2
        int resultOpt2 = solution.FindMaxFormOptimized(strs2, m2, n2);
        Console.WriteLine($"優化測試案例 2:");
        Console.WriteLine($"輸入：strs = [{string.Join(", ", strs2.Select(s => $"\"{s}\""))}], m = {m2}, n = {n2}");
        Console.WriteLine($"輸出：{resultOpt2}");
        Console.WriteLine($"預期：2");
        Console.WriteLine($"結果：{(resultOpt2 == result2 ? "✓ 與原版本一致" : "✗ 結果不一致")}");
        Console.WriteLine();

        // 優化版本測試案例 3
        int resultOpt3 = solution.FindMaxFormOptimized(strs3, m3, n3);
        Console.WriteLine($"優化測試案例 3:");
        Console.WriteLine($"輸入：strs = [{string.Join(", ", strs3.Select(s => $"\"{s}\""))}], m = {m3}, n = {n3}");
        Console.WriteLine($"輸出：{resultOpt3}");
        Console.WriteLine($"預期：3");
        Console.WriteLine($"結果：{(resultOpt3 == result3 ? "✓ 與原版本一致" : "✗ 結果不一致")}");
    }


    /// <summary>
    /// 使用動態規劃求解 0/1 背包問題的變體
    /// 
    /// 解題思路：
    /// 1. 定義三維 DP 陣列：dp[i,j,k] 表示在前 i 個字串中，使用 j 個 0 和 k 個 1 的情況下最多可以得到的字串數量
    /// 2. 狀態轉移方程：
    ///    - 如果 j < zeros 或 k < ones：dp[i,j,k] = dp[i-1,j,k]（無法選擇當前字串）
    ///    - 如果 j >= zeros 且 k >= ones：dp[i,j,k] = max(dp[i-1,j,k], dp[i-1,j-zeros,k-ones] + 1)
    /// 3. 時間複雜度：O(lmn)，其中 l 是字串陣列長度
    /// 4. 空間複雜度：O(lmn)
    /// </summary>
    /// <param name="strs">二進位字串陣列</param>
    /// <param name="m">最多可使用的 0 的數量</param>
    /// <param name="n">最多可使用的 1 的數量</param>
    /// <returns>符合條件的最大子集大小</returns>
    public int FindMaxForm(string[] strs, int m, int n)
    {
        int length = strs.Length;
        
        // 建立三維 DP 陣列：dp[i,j,k] 表示前 i 個字串使用 j 個 0 和 k 個 1 時的最大字串數量
        int[,,] dp = new int[length + 1, m + 1, n + 1];
        
        // 遍歷每個字串
        for (int i = 1; i <= length; i++)
        {
            // 計算當前字串中 0 和 1 的數量
            int[] zerosOnes = GetZerosOnes(strs[i - 1]);
            int zeros = zerosOnes[0];  // 當前字串的 0 的數量
            int ones = zerosOnes[1];   // 當前字串的 1 的數量
            
            // 遍歷所有可能的 0 的容量
            for (int j = 0; j <= m; j++)
            {
                // 遍歷所有可能的 1 的容量
                for (int k = 0; k <= n; k++)
                {
                    // 預設不選擇當前字串
                    dp[i, j, k] = dp[i - 1, j, k];
                    
                    // 如果容量足夠，考慮選擇當前字串
                    if (j >= zeros && k >= ones)
                    {
                        // 選擇當前字串：前 i-1 個字串用 j-zeros 個 0 和 k-ones 個 1 的最大數量 + 1
                        dp[i, j, k] = Math.Max(dp[i, j, k], dp[i - 1, j - zeros, k - ones] + 1);
                    }
                }
            }
        }
        
        // 返回使用所有字串、m 個 0 和 n 個 1 的最大字串數量
        return dp[length, m, n];
    }

    /// <summary>
    /// 計算字串中 0 和 1 的數量
    /// 
    /// 解題思路：
    /// 1. 建立一個長度為 2 的陣列，索引 0 存放 '0' 的數量，索引 1 存放 '1' 的數量
    /// 2. 遍歷字串，使用字元與 '0' 的 ASCII 差值作為索引，統計數量
    /// 3. 時間複雜度：O(len)，其中 len 是字串長度
    /// 4. 空間複雜度：O(1)
    /// </summary>
    /// <param name="str">二進位字串（僅包含 '0' 和 '1'）</param>
    /// <returns>長度為 2 的陣列，[0] 存放 0 的數量，[1] 存放 1 的數量</returns>
    public int[] GetZerosOnes(string str)
    {
        // zerosOnes[0] 存放 '0' 的數量，zerosOnes[1] 存放 '1' 的數量
        int[] zerosOnes = new int[2];
        int length = str.Length;
        
        // 遍歷字串中的每個字元
        for (int i = 0; i < length; i++)
        {
            // str[i] - '0' 的結果：'0' -> 0, '1' -> 1
            // 直接使用計算結果作為陣列索引，統計對應字元的數量
            zerosOnes[str[i] - '0']++;
        }
        
        return zerosOnes;
    }

    /// <summary>
    /// 使用滾動陣列優化的動態規劃解法
    /// 
    /// 解題思路：
    /// 1. 使用二維 DP 陣列代替三維陣列，省略字串索引維度
    /// 2. 從後往前遍歷，確保每次使用的是上一輪的狀態值
    /// 3. 狀態轉移：dp[j,k] = max(dp[j,k], dp[j-zeros,k-ones] + 1)
    /// 4. 時間複雜度：O(lmn)，其中 l 是字串陣列長度
    /// 5. 空間複雜度：O(mn)，大幅降低空間使用
    /// 
    /// 優化原理：
    /// - 每次計算 dp[i] 只依賴 dp[i-1]，不需要保存所有歷史狀態
    /// - 從後往前更新可避免覆蓋本輪需要使用的上一輪數據
    /// </summary>
    /// <param name="strs">二進位字串陣列</param>
    /// <param name="m">最多可使用的 0 的數量</param>
    /// <param name="n">最多可使用的 1 的數量</param>
    /// <returns>符合條件的最大子集大小</returns>
    public int FindMaxFormOptimized(string[] strs, int m, int n)
    {
        // 建立二維 DP 陣列，省略字串索引維度
        int[,] dp = new int[m + 1, n + 1];
        
        // 遍歷每個字串
        foreach (string str in strs)
        {
            // 計算當前字串中 0 和 1 的數量
            int[] zerosOnes = GetZerosOnes(str);
            int zeros = zerosOnes[0];
            int ones = zerosOnes[1];
            
            // 從後往前更新，避免覆蓋未使用的狀態
            // 這是滾動陣列的核心技巧：確保 dp[j-zeros, k-ones] 是上一輪的值
            for (int j = m; j >= zeros; j--)
            {
                for (int k = n; k >= ones; k--)
                {
                    // 選擇當前字串：上一輪的 dp[j-zeros, k-ones] + 1
                    // 不選擇當前字串：保持 dp[j, k] 不變
                    dp[j, k] = Math.Max(dp[j, k], dp[j - zeros, k - ones] + 1);
                }
            }
        }
        
        // 返回最終結果
        return dp[m, n];
    }
}
