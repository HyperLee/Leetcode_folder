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
        Console.WriteLine("Hello, World!");
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
    /// 空間複雜度：O(n)，用於儲存狀態陣列和佇列
    /// 
    /// ref:
    /// https://leetcode.cn/problems/maximum-candies-you-can-get-from-boxes/solutions/101813/ni-neng-cong-he-zi-li-huo-de-de-zui-da-tang-guo-2/?envType=daily-question&envId=2025-06-03
    /// 
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
        bool[] canOpen = new bool[n];    // 記錄每個盒子是否可以開啟（有鑰匙或本身就是開啟的）
        bool[] hasBox = new bool[n];     // 記錄是否擁有該盒子
        bool[] used = new bool[n];       // 記錄該盒子是否已經被使用過

        // 初始化 canOpen 陣列：根據初始狀態設定哪些盒子可以開啟
        for (int i = 0; i < n; i++)
        {
            canOpen[i] = (status[i] == 1); // status[i] == 1 表示盒子 i 初始就是開啟的
        }

        // 使用佇列進行廣度優先搜尋
        Queue<int> queue = new Queue<int>();
        int res = 0; // 累計獲得的糖果總數

        // 處理初始擁有的盒子
        foreach (int box in initialBoxes)
        {
            hasBox[box] = true; // 標記擁有這個盒子

            // 如果這個盒子可以開啟且尚未使用
            if (canOpen[box])
            {
                queue.Enqueue(box);     // 加入佇列等待處理
                used[box] = true;       // 標記為已使用
                res += candies[box];    // 累加糖果數量
            }
        }

        // BFS 主迴圈：持續處理佇列中的盒子
        while (queue.Count > 0)
        {
            int box = queue.Dequeue(); // 取出一個盒子進行處理

            // 處理當前盒子中的所有鑰匙
            foreach (int key in keys[box])
            {
                canOpen[key] = true; // 獲得鑰匙後，對應的盒子就可以開啟了

                // 檢查：如果有這個盒子且尚未使用，就可以開啟它
                if (!used[key] && hasBox[key])
                {
                    queue.Enqueue(key);     // 加入佇列等待處理
                    used[key] = true;       // 標記為已使用
                    res += candies[key];    // 累加糖果數量
                }
            }

            // 處理當前盒子中包含的其他盒子
            foreach (int boxx in containedBoxes[box])
            {
                hasBox[boxx] = true; // 標記現在擁有這個新盒子

                // 檢查：如果可以開啟這個新盒子且尚未使用
                if (!used[boxx] && canOpen[boxx])
                {
                    queue.Enqueue(boxx);    // 加入佇列等待處理
                    used[boxx] = true;      // 標記為已使用
                    res += candies[boxx];   // 累加糖果數量
                }
            }
        }

        return res; // 返回總糖果數量
    }
}
