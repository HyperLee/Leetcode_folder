namespace leetcode_1298;

class Program
{
    /// <summary>
    /// 1298. Maximum Candies You Can Get from Boxes
    /// https://leetcode.com/problems/maximum-candies-you-can-get-from-boxes/description/?envType=daily-question&envId=2025-06-03
    /// 1298. 你能从盒子里获得的最大糖果数
    /// https://leetcode.cn/problems/maximum-candies-you-can-get-from-boxes/description/?envType=daily-question&envId=2025-06-03
    /// 
    /// 題目描述：
    /// 你有 n 個盒子，標記從 0 到 n-1。給你四個陣列：status、candies、keys 和 containedBoxes，其中：
    /// - status[i] 為 1 表示第 i 個盒子是開著的，為 0 表示第 i 個盒子是關著的
    /// - candies[i] 是第 i 個盒子中的糖果數量
    /// - keys[i] 是你開啟第 i 個盒子後可以開啟的其他盒子標籤列表
    /// - containedBoxes[i] 是你在第 i 個盒子內找到的其他盒子列表
    /// 你會得到一個整數陣列 initialBoxes，包含你最初擁有的盒子標籤。
    /// 你可以拿取任何開著的盒子中的所有糖果，使用其中的鑰匙開啟新盒子，也可以使用你在其中找到的盒子。
    /// 返回遵循上述規則後你能獲得的最大糖果數量。
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("LeetCode 1298: Maximum Candies You Can Get from Boxes 測試");
        Console.WriteLine("=========================================================");
        
        var solution = new Program();
        
        // 測試案例 1
        Console.WriteLine("\n測試案例 1:");
        int[] status1 = {1, 0, 1, 0};
        int[] candies1 = {7, 5, 4, 9};
        int[][] keys1 = {new int[]{}, new int[]{}, new int[]{1}, new int[]{}};
        int[][] containedBoxes1 = {new int[]{1, 2}, new int[]{3}, new int[]{}, new int[]{}};
        int[] initialBoxes1 = {0};
        
        int result1_bfs = solution.MaxCandies(status1, candies1, keys1, containedBoxes1, initialBoxes1);
        int result1_dfs = solution.MaxCandies2(status1, candies1, keys1, containedBoxes1, initialBoxes1);
        
        Console.WriteLine($"BFS 解法結果: {result1_bfs}");
        Console.WriteLine($"DFS 解法結果: {result1_dfs}");
        Console.WriteLine($"預期結果: 16");
        Console.WriteLine($"BFS 正確: {result1_bfs == 16}");
        Console.WriteLine($"DFS 正確: {result1_dfs == 16}");
        
        // 測試案例 2
        Console.WriteLine("\n測試案例 2:");
        int[] status2 = {1, 0, 0, 0, 0, 0};
        int[] candies2 = {1, 1, 1, 1, 1, 1};
        int[][] keys2 = {new int[]{1, 2, 3, 4, 5}, new int[]{}, new int[]{}, new int[]{}, new int[]{}, new int[]{}};
        int[][] containedBoxes2 = {new int[]{}, new int[]{}, new int[]{}, new int[]{}, new int[]{}, new int[]{}};
        int[] initialBoxes2 = {0};
        
        int result2_bfs = solution.MaxCandies(status2, candies2, keys2, containedBoxes2, initialBoxes2);
        int result2_dfs = solution.MaxCandies2(status2, candies2, keys2, containedBoxes2, initialBoxes2);
        
        Console.WriteLine($"BFS 解法結果: {result2_bfs}");
        Console.WriteLine($"DFS 解法結果: {result2_dfs}");
        Console.WriteLine($"預期結果: 6");
        Console.WriteLine($"BFS 正確: {result2_bfs == 6}");
        Console.WriteLine($"DFS 正確: {result2_dfs == 6}");
        
        Console.WriteLine("\n=========================================================");
        Console.WriteLine("測試完成！兩種解法都已實作完成。");
    }

    /// <summary>
    /// 解題思路：使用廣度優先搜尋 (BFS) 來解決此問題
    /// 
    /// 核心概念：
    /// 1. 追蹤三個狀態：能否開啟盒子 (canOpen)、是否擁有盒子 (hasBox)、是否已使用盒子 (used)
    /// 2. 只有當「擁有盒子」且「能開啟盒子」且「尚未使用」時，才能取得糖果
    /// 3. 使用佇列進行 BFS，每次處理一個可開啟的盒子
    /// 4. 開啟盒子後獲得：糖果、新鑰匙、新盒子
    /// 
    /// 演算法步驟：
    /// 1. 初始化三個布林陣列來追蹤盒子狀態
    /// 2. 處理初始擁有的盒子，將可開啟的加入佇列
    /// 3. BFS 遍歷：對每個盒子處理其鑰匙和包含的盒子
    /// 4. 累計所有能開啟盒子的糖果數量
    ///
    /// 時間複雜度：O(n + k)，其中 n 是盒子數量，k 是所有鑰匙和包含盒子的總數
    /// 空間複雜度：O(n)，用於儲存狀態陣列和 BFS 佇列
    /// 
    /// ref:
    /// https://leetcode.cn/problems/maximum-candies-you-can-get-from-boxes/solutions/101813/ni-neng-cong-he-zi-li-huo-de-de-zui-da-tang-guo-2/?envType=daily-question&envId=2025-06-03
    /// </summary>
    /// <param name="status">盒子狀態陣列，1表示開啟，0表示關閉</param>
    /// <param name="candies">每個盒子中的糖果數量</param>
    /// <param name="keys">每個盒子中包含的鑰匙列表</param>
    /// <param name="containedBoxes">每個盒子中包含的其他盒子列表</param>
    /// <param name="initialBoxes">初始擁有的盒子列表</param>
    /// <returns>能獲得的最大糖果數量</returns>
    public int MaxCandies(int[] status, int[] candies, int[][] keys, int[][] containedBoxes, int[] initialBoxes)
    {
        int n = status.Length; // 盒子總數
        
        // 建立三個狀態追蹤陣列
        bool[] canOpen = new bool[n];        // 記錄每個盒子是否可以開啟（有鑰匙或本身就是開啟的）
        bool[] hasBox = new bool[n];         // 記錄是否擁有該盒子
        bool[] used = new bool[n];           // 記錄該盒子是否已經被使用過

        // 初始化可開啟狀態：根據 status 陣列設定初始的可開啟狀態
        for (int i = 0; i < n; i++)
        {
            canOpen[i] = (status[i] == 1);   // 開啟的盒子可以直接使用
        }

        // 使用佇列進行 BFS
        Queue<int> queue = new Queue<int>();
        int res = 0; // 累計糖果總數

        // 處理初始擁有的盒子
        // 階段說明：這是「預處理階段」，不是完整處理
        // - 目的 1：立即收集糖果收益
        // - 目的 2：準備後續的連鎖反應處理
        // - 重點：盒子內的鑰匙 (keys) 和其他盒子 (containedBoxes) 要在 BFS 主迴圈中才會處理
        foreach (int box in initialBoxes)
        {
            hasBox[box] = true;              // 標記為擁有此盒子
            if (canOpen[box] && !used[box])  // 檢查：可開啟 且 未處理過
            {
                // 關鍵：即使取了糖果，盒子仍需加入佇列處理「內容」
                queue.Enqueue(box);          // 加入佇列，準備處理 keys[box] 和 containedBoxes[box]
                used[box] = true;            // 防重複：標記已使用
                res += candies[box];         // 即時收益：累加糖果數量
            }
        }

        // BFS 遍歷處理所有可開啟的盒子
        // 兩個 迴圈：一個處理鑰匙，另一個處理盒子
        // 兩個迴圈順序可以戶換，不影響結果
        while (queue.Count > 0)
        {
            int box = queue.Dequeue();       // 取出當前處理的盒子

            // 處理該盒子中的鑰匙
            // 鑰匙迴圈：影響 canOpen 狀態
            foreach (int key in keys[box])
            {
                canOpen[key] = true;         // 獲得新鑰匙，更新可開啟狀態

                // 檢查是否有對應的盒子且尚未使用
                if (hasBox[key] && !used[key])
                {
                    queue.Enqueue(key);      // 加入佇列等待處理
                    used[key] = true;        // 標記為已使用
                    res += candies[key];     // 累加糖果數量
                }
            }

            // 處理該盒子中包含的其他盒子
            // 盒子迴圈：影響 hasBox 狀態  
            foreach (int boxx in containedBoxes[box])
            {
                hasBox[boxx] = true;         // 獲得新盒子，更新擁有狀態

                // 檢查是否可以開啟且尚未使用
                if (canOpen[boxx] && !used[boxx])
                {
                    queue.Enqueue(boxx);     // 加入佇列等待處理
                    used[boxx] = true;       // 標記為已使用
                    res += candies[boxx];    // 累加糖果數量
                }
            }
        }

        return res; // 返回總糖果數量
    }   


    /// <summary>
    /// 解法2：使用深度優先搜尋 (DFS) 來解決此問題
    /// 
    /// 核心概念：
    /// 1. 使用遞迴的方式處理每個盒子
    /// 2. 將開著的盒子視為已有鑰匙
    /// 3. 遞迴處理：先找到鑰匙再開盒子，或先找到盒子再使用鑰匙
    /// 4. 使用 hasBox[x] = false 避免重複訪問
    /// 5. 使用 ref 參數傳遞累計值，避免全域變數
    /// 
    /// 時間複雜度：O(n + k)，其中 n 是盒子數量，k 是所有鑰匙和包含盒子的總數
    /// 空間複雜度：O(n)，用於儲存狀態陣列和遞迴堆疊
    /// 
    /// ref:
    /// https://leetcode.cn/problems/maximum-candies-you-can-get-from-boxes/solutions/3683782/dfs-xie-fa-pythonjavacgo-by-endlesscheng-g6wx/?envType=daily-question&envId=2025-06-03
    /// </summary>
    /// <param name="status">盒子狀態陣列，1表示開啟，0表示關閉</param>
    /// <param name="candies">每個盒子中的糖果數量</param>
    /// <param name="keys">每個盒子中包含的鑰匙列表</param>
    /// <param name="containedBoxes">每個盒子中包含的其他盒子列表</param>
    /// <param name="initialBoxes">初始擁有的盒子列表</param>
    /// <returns>能獲得的最大糖果數量</returns>
    public int MaxCandies2(int[] status, int[] candies, int[][] keys, int[][] containedBoxes, int[] initialBoxes)
    {
        int ans = 0; // 使用區域變數取代全域變數
        int[] hasKey = (int[])status.Clone(); // 把開著的盒子當作有鑰匙
        bool[] hasBox = new bool[status.Length]; // 記錄是否擁有該盒子

        // 標記初始擁有的盒子
        foreach (int x in initialBoxes)
        {
            hasBox[x] = true;
        }

        // 處理初始擁有且可以開啟的盒子
        foreach (int x in initialBoxes)
        {
            if (hasBox[x] && hasKey[x] == 1) // 注意 DFS 中會修改 hasBox
            {
                Dfs(x, candies, keys, containedBoxes, hasKey, hasBox, ref ans);
            }
        }

        return ans;
    }

    /// <summary>
    /// 深度優先搜尋遞迴函式
    /// </summary>
    /// <param name="x">當前處理的盒子編號</param>
    /// <param name="candies">糖果陣列</param>
    /// <param name="keys">鑰匙陣列</param>
    /// <param name="containedBoxes">包含盒子陣列</param>
    /// <param name="hasKey">是否有鑰匙陣列</param>
    /// <param name="hasBox">是否擁有盒子陣列</param>
    /// <param name="ans">累計糖果數量（使用 ref 參數）</param>
    private void Dfs(int x, int[] candies, int[][] keys, int[][] containedBoxes, int[] hasKey, bool[] hasBox, ref int ans)
    {
        ans += candies[x]; // 累加糖果數量
        hasBox[x] = false; // 避免找到鑰匙後重新訪問開著的盒子

        // 找到鑰匙，打開盒子（說明我們先找到盒子，然後找到鑰匙）
        foreach (int y in keys[x])
        {
            hasKey[y] = 1; // 獲得鑰匙
            if (hasBox[y]) // 如果擁有該盒子，立即處理
            {
                Dfs(y, candies, keys, containedBoxes, hasKey, hasBox, ref ans);
            }
        }

        // 找到盒子，使用鑰匙（說明我們先找到鑰匙，然後找到盒子）
        foreach (int y in containedBoxes[x])
        {
            hasBox[y] = true; // 標記擁有新盒子
            if (hasKey[y] == 1) // 如果有鑰匙，立即處理
            {
                Dfs(y, candies, keys, containedBoxes, hasKey, hasBox, ref ans);
            }
        }
    }
}
