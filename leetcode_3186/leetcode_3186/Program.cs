namespace leetcode_3186;

class Program
{
    /// <summary>
    /// 3186. Maximum Total Damage With Spell Casting
    /// https://leetcode.com/problems/maximum-total-damage-with-spell-casting/description/?envType=daily-question&envId=2025-10-11
    /// https://leetcode.cn/problems/maximum-total-damage-with-spell-casting/description/?envType=daily-question&envId=2025-10-11
    ///
    /// Problem (English):
    /// A magician has various spells.
    /// You are given an array power, where each element represents the damage of a spell. Multiple spells can have the same damage value.
    /// It is a known fact that if a magician decides to cast a spell with a damage of power[i],
    /// they cannot cast any spell with a damage of power[i] - 2, power[i] - 1, power[i] + 1, or power[i] + 2.
    /// Each spell can be cast only once.
    /// Return the maximum possible total damage that a magician can cast.
    ///
    /// 題目描述 (中文):
    /// 一位魔法師擁有多種法術。
    /// 給定陣列 power，陣列中每個元素代表一個法術的傷害值。可能有多個法術具有相同的傷害值。
    /// 若魔法師選擇施放傷害為 power[i] 的法術，則他不能施放任何傷害為 power[i] - 2、power[i] - 1、power[i] + 1 或 power[i] + 2 的法術。
    /// 每個法術只能施放一次。
    /// 請回傳魔法師能施放的最大總傷害。
    ///
    /// </summary>
    /// <param name="args">命令列引數（執行時不需輸入名稱）</param>
    static void Main(string[] args)
    {
        // 測試案例
        var testCases = new[]
        {
            new int[] { 1, 1, 3, 4 },        // Expected: 6 (1+1+4 or 3+3)
            new int[] { 7, 1, 6, 6 },        // Expected: 13 (7+6)
            new int[] { 1, 2, 3, 4, 5 },     // Expected: 7 (2+5)
            new int[] { 5, 9, 4, 6 },        // Expected: 15 (9+6)
        };

        var solution = new Solution();
        
        for (int i = 0; i < testCases.Length; i++)
        {
            long result1 = solution.MaximumTotalDamage_DP(testCases[i]);
            long result2 = solution.MaximumTotalDamage_Optimized(testCases[i]);
            
            Console.WriteLine($"Test Case {i + 1}: [{string.Join(", ", testCases[i])}]");
            Console.WriteLine($"  DP Solution: {result1}");
            Console.WriteLine($"  Optimized Solution: {result2}");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// LeetCode 標準介面
    /// </summary>
    /// <param name="power">法術傷害陣列</param>
    /// <returns>最大總傷害</returns>
    public long MaximumTotalDamage(int[] power)
    {
        return new Solution().MaximumTotalDamage_DP(power);
    }
}

public class Solution
{
    /// <summary>
    /// 解法1：動態規劃 + 排序（使用輔助函式尋找不衝突位置）
    /// 
    /// 解題思路：
    /// 1. 將問題轉換為「選擇不衝突的傷害值」來最大化總傷害
    /// 2. 相同傷害值可以全部選擇，因此先統計每個傷害值的總和
    /// 3. 按傷害值排序後，使用動態規劃決定每個傷害值是否選擇
    /// 4. 對於每個傷害值 i，有兩種選擇：
    ///    - 不選：dp[i] = dp[i-1]
    ///    - 選：dp[i] = dp[lastValid] + currentDamage（lastValid 是最後一個不衝突的位置）
    /// 
    /// 關鍵點：
    /// - 衝突定義：如果選擇傷害值 x，則 [x-2, x-1, x+1, x+2] 都不能選
    /// - 排序後，只需考慮左側的衝突（x-2 和 x-1）
    /// - 使用輔助函式 FindLastValidIndex 找到最後一個不衝突的位置
    /// 
    /// 時間複雜度：O(n log n) - 排序 O(n log n) + DP O(n * k)，其中 k 是尋找不衝突位置的平均成本
    /// 空間複雜度：O(n) - Dictionary + 排序陣列 + DP 陣列
    /// </summary>
    /// <param name="power">法術傷害值陣列</param>
    /// <returns>最大總傷害值</returns>
    public long MaximumTotalDamage_DP(int[] power)
    {
        // 邊界情況：空陣列
        if (power.Length == 0) return 0;
        
        // 步驟1：統計每個傷害值的總和
        // 例如：[1, 1, 3, 4] -> {1: 2, 3: 3, 4: 4}
        // 原因：相同傷害值的法術可以全部施放（它們不會互相衝突）
        var damageCount = new Dictionary<int, long>();
        foreach (int p in power)
        {
            damageCount[p] = damageCount.GetValueOrDefault(p, 0) + p;
        }
        
        // 步驟2：將傷害值按從小到大排序
        // 排序後可以確保處理當前傷害值時，所有較小的傷害值已經處理完畢
        var sortedDamages = damageCount.Keys.OrderBy(x => x).ToArray();
        int n = sortedDamages.Length;
        
        // 步驟3：動態規劃
        // dp[i] 表示考慮前 i+1 個傷害值時，能獲得的最大總傷害
        long[] dp = new long[n];
        
        // 初始化：只有第一個傷害值時，直接選擇它
        dp[0] = damageCount[sortedDamages[0]];
        
        // 從第二個傷害值開始遍歷
        for (int i = 1; i < n; i++)
        {
            // 選項1：不選擇當前傷害值
            // 最大傷害等於前一個狀態的最大傷害
            dp[i] = dp[i - 1];
            
            // 選項2：選擇當前傷害值
            // 需要找到最後一個不與當前傷害值衝突的位置
            int lastValidIndex = FindLastValidIndex(sortedDamages, i);
            long currentDamage = damageCount[sortedDamages[i]];
            
            // 如果沒有不衝突的位置（所有之前的傷害值都衝突）
            if (lastValidIndex == -1)
            {
                // 只能選擇當前傷害值，不能加上之前的任何傷害
                dp[i] = Math.Max(dp[i], currentDamage);
            }
            else
            {
                // 可以選擇當前傷害值 + 最後一個不衝突位置的最大傷害
                dp[i] = Math.Max(dp[i], dp[lastValidIndex] + currentDamage);
            }
        }
        
        // 返回考慮所有傷害值後的最大總傷害
        return dp[n - 1];
    }
    
    /// <summary>
    /// 輔助函式：找到最後一個不與 index 位置衝突的索引
    /// 
    /// 衝突條件：如果選擇 sortedDamages[index]，則不能選擇與它差距在 [-2, 2] 範圍內的傷害值
    /// 
    /// 演算法：從 index-1 向前遍歷，找到第一個滿足 (currentDamage - sortedDamages[i]) > 2 的位置
    /// 
    /// 範例：sortedDamages = [1, 3, 4, 6, 9], index = 3 (值為 6)
    /// - i=2 (值4): 6-4=2 ≤ 2，衝突，繼續
    /// - i=1 (值3): 6-3=3 > 2，不衝突，返回 1
    /// 
    /// 時間複雜度：O(k)，其中 k 是需要檢查的位置數量（最壞情況 O(n)）
    /// </summary>
    /// <param name="sortedDamages">排序後的傷害值陣列</param>
    /// <param name="index">當前考慮的索引位置</param>
    /// <returns>最後一個不衝突的索引，如果所有位置都衝突則返回 -1</returns>
    private int FindLastValidIndex(int[] sortedDamages, int index)
    {
        int currentDamage = sortedDamages[index];
        
        // 從 index-1 開始向前遍歷
        for (int i = index - 1; i >= 0; i--)
        {
            // 如果差距大於 2，表示不衝突
            // 因為陣列已排序，後面的位置差距會更大，可以直接返回
            if (currentDamage - sortedDamages[i] > 2)
            {
                return i;
            }
        }
        
        // 所有之前的位置都衝突
        return -1;
    }
    
    /// <summary>
    /// 解法2：優化版動態規劃（內嵌式尋找不衝突位置）
    /// 
    /// 解題思路：
    /// 與解法1相同的核心邏輯，但實作方式更簡潔直觀
    /// 1. 統計每個傷害值的總和（相同傷害值可全部選擇）
    /// 2. 按傷害值排序，轉換為 (damage, totalDamage) 的陣列
    /// 3. 使用動態規劃，直接在迴圈內尋找不衝突的位置
    /// 
    /// 與解法1的差異：
    /// - 解法1：使用獨立的 FindLastValidIndex 函式
    /// - 解法2：在 DP 迴圈內直接使用 while 迴圈尋找
    /// 
    /// 優勢：
    /// - 程式碼更緊湊，減少函式呼叫開銷
    /// - 邏輯更直觀，容易理解
    /// - 使用匿名型別讓資料結構更清晰
    /// 
    /// 動態轉移方程：
    /// dp[i] = max(dp[i-1], dp[j] + damages[i].Total)
    /// 其中 j 是最後一個滿足 damages[i].Damage - damages[j].Damage > 2 的位置
    /// 
    /// 時間複雜度：O(n log n) - 排序 O(n log n) + DP O(n * k)
    /// 空間複雜度：O(n) - Dictionary + damages 陣列 + DP 陣列
    /// </summary>
    /// <param name="power">法術傷害值陣列</param>
    /// <returns>最大總傷害值</returns>
    public long MaximumTotalDamage_Optimized(int[] power)
    {
        // 邊界情況：空陣列
        if (power.Length == 0) return 0;
        
        // 步驟1：統計每種傷害值的總傷害
        // 使用 Dictionary 來聚合相同傷害值
        // 例如：[1, 1, 3, 4] -> {1: 2, 3: 3, 4: 4}
        var damageMap = new Dictionary<int, long>();
        foreach (int p in power)
        {
            damageMap[p] = damageMap.GetValueOrDefault(p, 0) + p;
        }
        
        // 步驟2：轉換為 (damage, totalDamage) 並排序
        // 使用匿名型別讓資料結構更清晰易讀
        // OrderBy 保證後續處理時，所有較小的傷害值已被處理
        var damages = damageMap.Select(kv => new { Damage = kv.Key, Total = kv.Value })
                                .OrderBy(x => x.Damage)
                                .ToArray();
        
        int n = damages.Length;
        
        // 特殊情況：只有一種傷害值，直接返回
        if (n == 1) return damages[0].Total;
        
        // 步驟3：動態規劃
        // dp[i] 表示考慮前 i+1 個傷害值時的最大總傷害
        long[] dp = new long[n];
        
        // 初始化：第一個傷害值必然選擇
        dp[0] = damages[0].Total;
        
        // 從第二個傷害值開始遍歷
        for (int i = 1; i < n; i++)
        {
            // 選項1：不選擇當前傷害值
            // 繼承前一個狀態的最大傷害
            dp[i] = dp[i - 1];
            
            // 選項2：選擇當前傷害值
            // 需要找到最後一個不與當前傷害值衝突的位置 j
            // 衝突條件：damages[i].Damage - damages[j].Damage <= 2
            int j = i - 1;
            
            // 向前遍歷，跳過所有衝突的傷害值
            // 因為陣列已排序，只要找到第一個不衝突的位置即可停止
            while (j >= 0 && damages[i].Damage - damages[j].Damage <= 2)
            {
                j--;
            }
            
            // 計算選擇當前傷害值時的總傷害
            // 如果 j >= 0，加上 dp[j]（之前不衝突位置的最大傷害）
            // 如果 j < 0，表示所有之前的位置都衝突，只取當前傷害值
            long chooseCurrentDamage = (j >= 0 ? dp[j] : 0) + damages[i].Total;
            
            // 取「不選」和「選」兩種情況的最大值
            dp[i] = Math.Max(dp[i], chooseCurrentDamage);
        }
        
        // 返回考慮所有傷害值後的最大總傷害
        return dp[n - 1];
    }
}
