namespace leetcode_2976;

class Program
{
    /// <summary>
    /// 2976. Minimum Cost to Convert String I
    /// https://leetcode.com/problems/minimum-cost-to-convert-string-i/description/
    /// <para>
    /// You are given two 0-indexed strings source and target, both of length n and consisting of lowercase English letters. You are also given two 0-indexed character arrays original and changed, and an integer array cost, where cost[i] is the cost of changing original[i] to changed[i].
    ///
    /// You start with source. In one operation, you may pick a character x and change it to y at cost z if there is an index j such that cost[j] == z, original[j] == x, and changed[j] == y.
    ///
    /// Return the minimum cost to convert source to target using any number of operations. If it is impossible, return -1.
    ///
    /// Note that there may be indices i and j such that original[j] == original[i] and changed[j] == changed[i].
    ///
    /// Example 1:
    /// Input: source = "abcd", target = "acbe", original = ["a","b","c","c","e","d"], changed = ["b","c","b","e","b","e"], cost = [2,5,5,1,2,20]
    /// Output: 28
    /// Explanation: Change index 1 from 'b' to 'c' for 5. Change index 2 from 'c' to 'e' for 1, then from 'e' to 'b' for 2. Change index 3 from 'd' to 'e' for 20. The total is 5 + 1 + 2 + 20 = 28, which is minimal.
    ///
    /// Example 2:
    /// Input: source = "aaaa", target = "bbbb", original = ["a","c"], changed = ["c","b"], cost = [1,2]
    /// Output: 12
    /// Explanation: Changing 'a' to 'c' costs 1, then changing 'c' to 'b' costs 2, for 1 + 2 = 3 per character. Converting all four occurrences costs 3 * 4 = 12.
    ///
    /// Example 3:
    /// Input: source = "abcd", target = "abce", original = ["a"], changed = ["e"], cost = [10000]
    /// Output: -1
    /// Explanation: Conversion is impossible because the value at index 3 cannot be changed from 'd' to 'e'.
    ///
    /// Constraints:
    /// - 1 &lt;= source.length == target.length &lt;= 10^5
    /// - source and target consist of lowercase English letters.
    /// - 1 &lt;= cost.length == original.length == changed.length &lt;= 2000
    /// - original[i] and changed[i] are lowercase English letters.
    /// - 1 &lt;= cost[i] &lt;= 10^6
    /// - original[i] != changed[i]
    /// </para>
    /// <para>
    /// 2976. 轉換字串的最小成本 I
    /// https://leetcode.cn/problems/minimum-cost-to-convert-string-i/description/
    ///
    /// 給定兩個 0-indexed 字串 source 與 target，兩者長度皆為 n，且只含小寫英文字母。另給定兩個 0-indexed 字元陣列 original、changed 與整數陣列 cost，其中 cost[i] 是將 original[i] 改為 changed[i] 的成本。
    ///
    /// 你從 source 開始。一次操作中，若存在索引 j，使 cost[j] == z、original[j] == x 且 changed[j] == y，便可選擇字元 x 並以成本 z 將它改為 y。
    ///
    /// 回傳使用任意次操作將 source 轉換為 target 的最小成本；若無法轉換，回傳 -1。
    ///
    /// 注意，可能存在索引 i 和 j，使 original[j] == original[i] 且 changed[j] == changed[i]。
    ///
    /// 範例 1：
    /// 輸入：source = "abcd", target = "acbe", original = ["a","b","c","c","e","d"], changed = ["b","c","b","e","b","e"], cost = [2,5,5,1,2,20]
    /// 輸出：28
    /// 解釋：將索引 1 的 'b' 以成本 5 改為 'c'；將索引 2 的 'c' 以成本 1 改為 'e'，再以成本 2 將 'e' 改為 'b'；將索引 3 的 'd' 以成本 20 改為 'e'。總成本為 5 + 1 + 2 + 20 = 28，且這是最小值。
    ///
    /// 範例 2：
    /// 輸入：source = "aaaa", target = "bbbb", original = ["a","c"], changed = ["c","b"], cost = [1,2]
    /// 輸出：12
    /// 解釋：將 'a' 改為 'c' 的成本為 1，再將 'c' 改為 'b' 的成本為 2，每個字元共需 1 + 2 = 3。轉換全部四個字元需 3 * 4 = 12。
    ///
    /// 範例 3：
    /// 輸入：source = "abcd", target = "abce", original = ["a"], changed = ["e"], cost = [10000]
    /// 輸出：-1
    /// 解釋：無法轉換，因為索引 3 的值不能從 'd' 改為 'e'。
    ///
    /// 限制條件：
    /// - 1 &lt;= source.length == target.length &lt;= 10^5
    /// - source 與 target 只含小寫英文字母。
    /// - 1 &lt;= cost.length == original.length == changed.length &lt;= 2000
    /// - original[i] 與 changed[i] 是小寫英文字母。
    /// - 1 &lt;= cost[i] &lt;= 10^6
    /// - original[i] != changed[i]
    /// </para>
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var solution = new Program();

        // 測試案例 1: 基本範例
        string source1 = "abcd";
        string target1 = "acbe";
        char[] original1 = { 'a', 'b', 'c', 'c', 'e', 'd' };
        char[] changed1 = { 'b', 'c', 'b', 'e', 'b', 'e' };
        int[] cost1 = { 2, 5, 5, 1, 2, 20 };
        long result1 = solution.MinimumCost(source1, target1, original1, changed1, cost1);
        Console.WriteLine($"測試案例 1: {result1} (預期: 28)");

        // 測試案例 2: 無法轉換
        string source2 = "aaaa";
        string target2 = "bbbb";
        char[] original2 = { 'a', 'c' };
        char[] changed2 = { 'c', 'b' };
        int[] cost2 = { 1, 2 };
        long result2 = solution.MinimumCost(source2, target2, original2, changed2, cost2);
        Console.WriteLine($"測試案例 2: {result2} (預期: 12)");

        // 測試案例 3: 已經相同
        string source3 = "abcd";
        string target3 = "abcd";
        char[] original3 = { 'a' };
        char[] changed3 = { 'b' };
        int[] cost3 = { 1 };
        long result3 = solution.MinimumCost(source3, target3, original3, changed3, cost3);
        Console.WriteLine($"測試案例 3: {result3} (預期: 0)");
    }

    /// <summary>
    /// 使用 Floyd-Warshall 演算法計算字串轉換的最小成本
    /// 
    /// 解題思路：
    /// 1. 問題本質：將 source 中的每個字元逐一轉換成 target 對應位置的字元
    /// 2. 關鍵挑戰：同一字元可能有多種轉換路徑（直接或間接），需找出最小成本
    /// 3. 解決方案：將字元轉換視為圖論中的「最短路徑」問題
    /// 
    /// Floyd-Warshall 演算法：
    /// - 建立 26x26 的距離矩陣 dis[i][j]，代表字母 i 轉換成字母 j 的最小成本
    /// - 初始化所有距離為無限大（int.MaxValue / 2，避免加法溢位）
    /// - 填入題目給定的直接轉換成本
    /// - 透過中繼點 k，更新所有字母對之間的最短路徑：
    ///   如果 dis[i][j] > dis[i][k] + dis[k][j]，則更新為較小值
    /// - 最後將 source 的每個字元轉換成 target 的成本累加
    /// 
    /// 時間複雜度：O(26³ + n)，其中 n 是字串長度
    /// 空間複雜度：O(26²)
    /// </summary>
    /// <param name="source">來源字串</param>
    /// <param name="target">目標字串</param>
    /// <param name="original">可轉換的原始字元陣列</param>
    /// <param name="changed">對應的目標字元陣列</param>
    /// <param name="cost">每次轉換的成本陣列</param>
    /// <returns>最小轉換成本，若無法轉換則回傳 -1</returns>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="original"></param>
    /// <param name="changed"></param>
    /// <param name="cost"></param>
    /// <returns></returns>
    public long MinimumCost(string source, string target, char[] original, char[] changed, int[] cost)
    {
        // 步驟 1: 建立 26x26 的距離矩陣（代表 26 個英文小寫字母）
        int[][] dis = new int[26][];
        
        // 步驟 2: 初始化距離矩陣
        for(int i = 0; i < 26; i++)
        {
            dis[i] = new int[26];
            // 填入初始值為無限大，表示尚未找到任何轉換路徑
            // 使用 int.MaxValue / 2 避免後續相加時發生算術溢位
            Array.Fill(dis[i], int.MaxValue / 2);
        }

        // 步驟 3: 填入題目給定的直接轉換成本
        // 將 original[i] 轉換成 changed[i] 的成本為 cost[i]
        for(int i = 0; i < cost.Length; i++)
        {
            int x = original[i] - 'a';  // 將字元轉換為索引 0-25
            int y = changed[i] - 'a';
            // 若同一轉換有多個成本選項，取最小值
            dis[x][y] = Math.Min(dis[x][y], cost[i]);
        }

        // 步驟 4: Floyd-Warshall 演算法核心 - 計算所有字母對之間的最短路徑
        // k 是中繼點，嘗試透過 k 來更新 i 到 j 的最短距離
        // 若 dis[i][j] > dis[i][k] + dis[k][j]，表示經由 k 的路徑更短
        for(int k = 0; k < 26; k++)
        {
            // 字母轉換成自己的成本為 0
            dis[k][k] = 0;
            
            // 嘗試用 k 作為中繼點，更新所有 i 到 j 的距離
            for(int i = 0; i < 26; i++)
            {
                for(int j = 0; j < 26; j++)
                {
                    // 更新最短路徑：比較「直接路徑」與「經由 k 的路徑」
                    dis[i][j] = Math.Min(dis[i][j], dis[i][k] + dis[k][j]);
                }
            }
        }

        // 步驟 5: 計算將 source 轉換成 target 的總成本
        // 已經建立好所有字母對之間的最小轉換成本矩陣
        long res = 0;
        for(int i = 0; i < source.Length; i++)
        {
            // 查詢將 source[i] 轉換成 target[i] 的最小成本
            int d = dis[source[i] - 'a'][target[i] - 'a'];
            
            // 若成本仍為無限大，表示無法完成轉換
            if(d >= int.MaxValue / 2)
            {
                // 無法將 source[i] 轉換成 target[i]，回傳 -1
                return -1;
            }

            // 累加轉換成本
            res += d;         
        }
        return res;
    }
}
