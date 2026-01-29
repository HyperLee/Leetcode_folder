namespace leetcode_2976;

class Program
{
    /// <summary>
    /// 2976. Minimum Cost to Convert String I
    /// https://leetcode.com/problems/minimum-cost-to-convert-string-i/description/?envType=daily-question&envId=2026-01-29
    /// https://leetcode.cn/problems/minimum-cost-to-convert-string-i/description/?envType=daily-question&envId=2026-01-29
    ///
    /// English:
    /// You are given two 0-indexed strings source and target, both of length n and consisting of lowercase English letters. You are also given two 0-indexed character arrays original and changed, and an integer array cost, where cost[i] represents the cost of changing the character original[i] to the character changed[i].
    ///
    /// You start with the string source. In one operation, you can pick a character x from the string and change it to the character y at a cost of z if there exists any index j such that cost[j] == z, original[j] == x, and changed[j] == y.
    ///
    /// Return the minimum cost to convert the string source to the string target using any number of operations. If it is impossible to convert source to target, return -1.
    ///
    /// Note that there may exist indices i, j such that original[j] == original[i] and changed[j] == changed[i].
    ///
    /// 中文（繁體）:
    /// 給定兩個 0 為起始索引的字串 source 與 target，長度皆為 n，且只包含小寫英文字母。另有兩個 0 為起始索引字元陣列 original 與 changed，以及整數陣列 cost，其中 cost[i] 表示將字元 original[i] 變為 changed[i] 的成本。
    ///
    /// 從字串 source 出發。在一次操作中，你可以選取字串中的字元 x，將其改為字元 y，成本為 z，當且僅當存在某個索引 j 使得 cost[j] == z、original[j] == x、且 changed[j] == y。
    ///
    /// 回傳將 source 轉換成 target 的最小總成本；若無法轉換則回傳 -1。
    ///
    /// 注意：可能存在不同索引 i, j 但 original[j] == original[i] 且 changed[j] == changed[i] 的情況。
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
