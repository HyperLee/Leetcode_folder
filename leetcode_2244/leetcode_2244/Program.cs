namespace leetcode_2244;

class Program
{
    /// <summary>
    /// 2244. Minimum Rounds to Complete All Tasks
    /// https://leetcode.com/problems/minimum-rounds-to-complete-all-tasks/description/
    /// 2244. 完成所有任务需要的最少轮数
    /// https://leetcode.cn/problems/minimum-rounds-to-complete-all-tasks/description/
    /// 
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var program = new Program();

        // 測試案例 1: tasks = [2,2,3,3,2,4,4,4,4,4]
        // 難度 2 出現 3 次, 難度 3 出現 2 次, 難度 4 出現 5 次
        // 難度 2: 3 = 3×1 → 1 輪
        // 難度 3: 2 = 2×1 → 1 輪  
        // 難度 4: 5 = 3+2 → 2 輪
        // 總計: 1+1+2 = 4 輪
        int[] tasks1 = [2, 2, 3, 3, 2, 4, 4, 4, 4, 4];
        Console.WriteLine($"測試 1: tasks = [2,2,3,3,2,4,4,4,4,4]");
        Console.WriteLine($"MinimumRounds 結果: {program.MinimumRounds(tasks1)}"); // 預期: 4
        Console.WriteLine($"MinimumRounds2 結果: {program.MinimumRounds2(tasks1)}"); // 預期: 4
        Console.WriteLine();

        // 測試案例 2: tasks = [2,3,3]
        // 難度 2 只出現 1 次，無法完成（每輪至少要處理 2 或 3 個同難度任務）
        int[] tasks2 = [2, 3, 3];
        Console.WriteLine($"測試 2: tasks = [2,3,3]");
        Console.WriteLine($"MinimumRounds 結果: {program.MinimumRounds(tasks2)}"); // 預期: -1
        Console.WriteLine($"MinimumRounds2 結果: {program.MinimumRounds2(tasks2)}"); // 預期: -1
        Console.WriteLine();

        // 測試案例 3: tasks = [5,5,5,5,5,5]
        // 難度 5 出現 6 次: 6 = 3×2 → 2 輪
        int[] tasks3 = [5, 5, 5, 5, 5, 5];
        Console.WriteLine($"測試 3: tasks = [5,5,5,5,5,5]");
        Console.WriteLine($"MinimumRounds 結果: {program.MinimumRounds(tasks3)}"); // 預期: 2
        Console.WriteLine($"MinimumRounds2 結果: {program.MinimumRounds2(tasks3)}"); // 預期: 2
    }

    /// <summary>
    /// 使用 LINQ GroupBy 解法計算完成所有任務所需的最少輪數。
    /// 
    /// <para><b>解題思路：</b></para>
    /// <para>1. 每輪只能完成 2 或 3 個相同難度的任務</para>
    /// <para>2. 統計每個難度等級的任務數量</para>
    /// <para>3. 對於每個難度，計算最少需要幾輪：</para>
    /// <para>   - 若數量為 1，無法完成，回傳 -1</para>
    /// <para>   - 否則使用貪心策略：盡可能多用 3，剩餘用 2 補足</para>
    /// <para>   - 數學公式：⌈count / 3⌉ = (count + 2) / 3 = count/3 + (count%3 != 0 ? 1 : 0)</para>
    /// 
    /// <para><b>時間複雜度：</b>O(n)，其中 n 為任務數量</para>
    /// <para><b>空間複雜度：</b>O(k)，其中 k 為不同難度等級的數量</para>
    /// </summary>
    /// <param name="tasks">任務難度陣列，tasks[i] 表示第 i 個任務的難度等級</param>
    /// <returns>完成所有任務所需的最少輪數；若無法完成則回傳 -1</returns>
    /// <example>
    /// <code>
    /// var program = new Program();
    /// int[] tasks = [2, 2, 3, 3, 2, 4, 4, 4, 4, 4];
    /// int result = program.MinimumRounds(tasks); // 回傳 4
    /// </code>
    /// </example>
    public int MinimumRounds(int[] tasks)
    {
        // 使用 LINQ 將任務依難度分組，並建立「難度 → 數量」的字典
        var map = tasks.GroupBy(t => t).ToDictionary(g => g.Key, g => g.Count());

        var res = 0;

        // 遍歷每個難度等級的任務數量
        foreach (var kvp in map)
        {
            // 若某難度只有 1 個任務，無法完成（每輪至少處理 2 個）
            if (kvp.Value == 1)
            {
                return -1;
            }

            // 貪心策略：優先使用 3 個一組，計算需要幾輪
            // count / 3 得到可以完成幾輪「3 個任務」
            res += kvp.Value / 3;

            // 若有餘數（1 或 2），還需要額外 1 輪
            // 餘數 1：向前借一輪 3，變成 (3-1)+(1+1) = 2+2 → 2 輪，但這裡已計入
            // 餘數 2：直接 1 輪處理 2 個
            if (kvp.Value % 3 != 0)
            {
                res++;
            }
        }

        return res;
    }

    /// <summary>
    /// 使用傳統 Dictionary 迴圈解法計算完成所有任務所需的最少輪數。
    /// 
    /// <para><b>解題思路：</b></para>
    /// <para>1. 每輪只能完成 2 或 3 個相同難度的任務</para>
    /// <para>2. 使用 Dictionary 手動統計每個難度等級的任務數量</para>
    /// <para>3. 對於每個難度，使用與 MinimumRounds 相同的貪心策略計算輪數</para>
    /// 
    /// <para><b>與 MinimumRounds 的差異：</b></para>
    /// <para>- 此方法使用傳統 foreach 迴圈建立字典，程式碼較冗長但執行效率略高</para>
    /// <para>- MinimumRounds 使用 LINQ，程式碼簡潔但有額外的函式呼叫開銷</para>
    /// 
    /// <para><b>時間複雜度：</b>O(n)，其中 n 為任務數量</para>
    /// <para><b>空間複雜度：</b>O(k)，其中 k 為不同難度等級的數量</para>
    /// </summary>
    /// <param name="tasks">任務難度陣列，tasks[i] 表示第 i 個任務的難度等級</param>
    /// <returns>完成所有任務所需的最少輪數；若無法完成則回傳 -1</returns>
    /// <example>
    /// <code>
    /// var program = new Program();
    /// int[] tasks = [2, 2, 3, 3, 2, 4, 4, 4, 4, 4];
    /// int result = program.MinimumRounds2(tasks); // 回傳 4
    /// </code>
    /// </example>
    public int MinimumRounds2(int[] tasks)
    {
        // 建立字典儲存「難度等級 → 出現次數」的對應關係
        IDictionary<int, int> map = new Dictionary<int, int>();

        // 手動遍歷陣列統計每個難度的出現次數
        foreach (int task in tasks)
        {
            // 若該難度尚未存在於字典，初始化為 1
            if (!map.ContainsKey(task))
            {
                map[task] = 1;
            }
            else
            {
                // 該難度已存在，數量加 1
                map[task]++;
            }
        }

        int res = 0;

        // 只需遍歷數量值，不需要鍵（難度等級）
        foreach (int v in map.Values)
        {
            // 若某難度只有 1 個任務，無法完成
            if (v == 1)
            {
                return -1;
            }

            // 計算該難度需要的輪數：⌈v / 3⌉
            res += v / 3;
            if (v % 3 != 0)
            {
                res++;
            }
        }

        return res;
    }
}
