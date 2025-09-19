namespace leetcode_3408;

class Program
{
    /// <summary>
    /// 3408. Design Task Manager
    /// https://leetcode.com/problems/design-task-manager/description/?envType=daily-question&envId=2025-09-18
    /// 3408. 设计任务管理器
    /// https://leetcode.cn/problems/design-task-manager/description/
    /// 
    /// 題目描述：
    /// 有一個任務管理系統允許使用者管理他們的任務，每個任務都有關聯的優先級。
    /// 系統應該高效處理新增、修改、執行和刪除任務的操作。
    /// 
    /// 實現 TaskManager 類別：
    /// 1. TaskManager(vector<vector<int>>& tasks) 使用使用者-任務-優先級三元組清單初始化任務管理器。
    ///    輸入清單中的每個元素形式為 [userId, taskId, priority]，將具有給定優先級的任務添加到指定使用者。
    /// 
    /// 2. void add(int userId, int taskId, int priority) 將具有指定 taskId 和優先級的任務添加到 userId 使用者。
    ///    保證 taskId 在系統中不存在。
    /// 
    /// 3. void edit(int taskId, int newPriority) 將現有 taskId 的優先級更新為 newPriority。
    ///    保證 taskId 在系統中存在。
    /// 
    /// 4. void rmv(int taskId) 從系統中刪除由 taskId 識別的任務。
    ///    保證 taskId 在系統中存在。
    /// 
    /// 5. int execTop() 執行所有使用者中具有最高優先級的任務。
    ///    如果有多個具有相同最高優先級的任務，執行具有最高 taskId 的任務。
    ///    執行後，taskId 從系統中刪除。返回與執行任務關聯的 userId。
    ///    如果沒有可用任務，返回 -1。
    /// 
    /// 注意：使用者可能被分配多個任務。
    /// 
    /// 約束條件：
    /// - 1 <= tasks.length <= 10^5
    /// - 0 <= userId <= 10^5
    /// - 0 <= taskId <= 10^5
    /// - 0 <= priority <= 10^9
    /// - 0 <= newPriority <= 10^9
    /// - add, edit, rmv, execTop 方法總共最多被呼叫 2 * 10^5 次
    /// - 輸入保證 taskId 是有效的
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("=== LeetCode 3408: Design Task Manager 測試 ===\n");

        // 測試案例 1：基本功能測試
        Console.WriteLine("測試案例 1：基本功能測試");
        var tasks1 = new List<IList<int>>
        {
            new List<int> { 1, 101, 10 }, // [userId=1, taskId=101, priority=10]
            new List<int> { 2, 102, 20 }, // [userId=2, taskId=102, priority=20]
            new List<int> { 3, 103, 15 }  // [userId=3, taskId=103, priority=15]
        };

        var taskManager1 = new TaskManager(tasks1);
        
        Console.WriteLine($"執行最高優先級任務: 使用者ID = {taskManager1.ExecTop()}"); // 預期: 2 (priority=20)
        Console.WriteLine($"執行最高優先級任務: 使用者ID = {taskManager1.ExecTop()}"); // 預期: 3 (priority=15)
        Console.WriteLine($"執行最高優先級任務: 使用者ID = {taskManager1.ExecTop()}"); // 預期: 1 (priority=10)
        Console.WriteLine($"執行最高優先級任務: 使用者ID = {taskManager1.ExecTop()}"); // 預期: -1 (無任務)

        Console.WriteLine("\n" + new string('-', 50) + "\n");

        // 測試案例 2：完整操作測試
        Console.WriteLine("測試案例 2：完整操作測試 (Add, Edit, Remove)");
        var tasks2 = new List<IList<int>>
        {
            new List<int> { 1, 201, 5 },  // [userId=1, taskId=201, priority=5]
            new List<int> { 2, 202, 8 }   // [userId=2, taskId=202, priority=8]
        };

        var taskManager2 = new TaskManager(tasks2);
        
        // 添加新任務
        taskManager2.Add(3, 203, 12);
        Console.WriteLine("添加任務: userId=3, taskId=203, priority=12");
        
        // 編輯任務優先級
        taskManager2.Edit(201, 15);
        Console.WriteLine("編輯任務: taskId=201, newPriority=15");
        
        // 刪除任務
        taskManager2.Rmv(202);
        Console.WriteLine("刪除任務: taskId=202");
        
        Console.WriteLine($"執行最高優先級任務: 使用者ID = {taskManager2.ExecTop()}"); // 預期: 1 (priority=15)
        Console.WriteLine($"執行最高優先級任務: 使用者ID = {taskManager2.ExecTop()}"); // 預期: 3 (priority=12)
        Console.WriteLine($"執行最高優先級任務: 使用者ID = {taskManager2.ExecTop()}"); // 預期: -1 (任務202已刪除)

        Console.WriteLine("\n" + new string('-', 50) + "\n");

        // 測試案例 3：相同優先級測試（taskId 排序）
        Console.WriteLine("測試案例 3：相同優先級測試 (taskId 較大者優先)");
        var tasks3 = new List<IList<int>>
        {
            new List<int> { 1, 301, 10 }, // [userId=1, taskId=301, priority=10]
            new List<int> { 2, 305, 10 }, // [userId=2, taskId=305, priority=10]
            new List<int> { 3, 303, 10 }  // [userId=3, taskId=303, priority=10]
        };

        var taskManager3 = new TaskManager(tasks3);
        
        Console.WriteLine($"執行最高優先級任務: 使用者ID = {taskManager3.ExecTop()}"); // 預期: 2 (taskId=305最大)
        Console.WriteLine($"執行最高優先級任務: 使用者ID = {taskManager3.ExecTop()}"); // 預期: 3 (taskId=303次大)
        Console.WriteLine($"執行最高優先級任務: 使用者ID = {taskManager3.ExecTop()}"); // 預期: 1 (taskId=301最小)

        Console.WriteLine("\n" + new string('-', 50) + "\n");

        // 測試案例 4：懶刪除機制測試
        Console.WriteLine("測試案例 4：懶刪除機制測試");
        var tasks4 = new List<IList<int>>
        {
            new List<int> { 1, 401, 5 }
        };

        var taskManager4 = new TaskManager(tasks4);
        
        // 多次編輯同一任務
        taskManager4.Edit(401, 8);
        taskManager4.Edit(401, 12);
        taskManager4.Edit(401, 6);
        Console.WriteLine("多次編輯任務 401: 5 -> 8 -> 12 -> 6");
        
        Console.WriteLine($"執行最高優先級任務: 使用者ID = {taskManager4.ExecTop()}"); // 預期: 1 (最終priority=6)

        Console.WriteLine("\n=== 測試完成 ===");
    }
}

/// <summary>
/// 任務管理器類別
/// 使用優先佇列 + 雜湊表實現高效的任務管理
/// 
/// 解題思路：
/// 1. 使用雜湊表 taskInfo 存储每個任務的最新資訊（優先級和使用者ID）
/// 2. 使用最大堆 heap 來高效獲取最高優先級的任務
/// 3. 通過懶刪除機制處理任務的編輯和刪除操作
/// 4. 在執行任務時檢查任務的有效性，確保資料一致性
/// </summary>
public class TaskManager
{
    // 雜湊表：儲存任務ID到[使用者ID, 優先級]的對應關係
    private readonly Dictionary<int, int[]> taskInfo; // taskId -> [userId, priority]

    // 優先佇列：以優先級為主要排序依據，任務ID為次要排序依據的最大堆
    // priorityQueue 元素格式：[priority, taskId]
    private readonly PriorityQueue<int[], int[]> heap;

    /// <summary>
    /// 建構函式：初始化任務管理器
    /// 時間複雜度：O(n log n)，其中 n 為初始任務數量
    /// 空間複雜度：O(n)
    /// </summary>
    /// <param name="tasks">初始任務清單，每個任務包含 [userId, taskId, priority]</param>
    public TaskManager(IList<IList<int>> tasks)
    {
        // 初始化雜湊表和優先佇列
        taskInfo = new Dictionary<int, int[]>();
        
        // 建立最大堆：優先級高的任務優先，相同優先級下任務ID大的優先
        heap = new PriorityQueue<int[], int[]>(
            Comparer<int[]>.Create((a, b) =>
            {
                // 首先比較優先級（降序）
                if (a[0] != b[0])
                {
                    // 優先級不同時，比較優先級（降序）
                    return b[0].CompareTo(a[0]);
                }
                // 優先級相同時，比較任務ID（降序）
                return b[1].CompareTo(a[1]);
            })
        );

        // 將所有初始任務加入系統
        foreach (var task in tasks)
        { 
            int userId = task[0];
            int taskId = task[1];
            int priority = task[2];
            
            // 儲存任務資訊到雜湊表
            taskInfo[taskId] = new int[] { userId, priority };
            
            // 將任務加入優先佇列，格式：[優先級, 任務ID]
            heap.Enqueue(new int[] { priority, taskId }, new int[] { priority, taskId });
        }
    }

    /// <summary>
    /// 添加新任務到管理器
    /// 時間複雜度：O(log n)，插入堆的時間複雜度
    /// 空間複雜度：O(1)
    /// 
    /// 注意：題目保證 taskId 不存在於系統中，因此不需要重複檢查
    /// </summary>
    /// <param name="userId">任務所屬的使用者ID</param>
    /// <param name="taskId">任務的唯一標識符</param>
    /// <param name="priority">任務的優先級</param>
    public void Add(int userId, int taskId, int priority)
    {
        // 題目保證 taskId 不存在，直接將任務資訊儲存到雜湊表中
        taskInfo[taskId] = new int[] { userId, priority };
        
        // 將任務加入優先佇列，確保能夠按優先級排序
        heap.Enqueue(new int[] { priority, taskId }, new int[] { priority, taskId });
    }

    /// <summary>
    /// 編輯現有任務的優先級
    /// 使用懶刪除機制：不直接修改堆中的元素，而是添加新記錄
    /// 時間複雜度：O(log n)，插入堆的時間複雜度
    /// 空間複雜度：O(1)
    /// 
    /// 注意：題目保證 taskId 存在，因此不需要額外驗證
    /// </summary>
    /// <param name="taskId">要編輯的任務ID</param>
    /// <param name="newPriority">新的優先級</param>
    public void Edit(int taskId, int newPriority)
    {
        // 題目保證 taskId 存在，直接更新雜湊表中的優先級資訊
        int userId = taskInfo[taskId][0]; // 保持使用者ID不變
        taskInfo[taskId] = new int[] { userId, newPriority };
        
        // 向堆中添加新的記錄（舊記錄通過懶刪除處理）
        heap.Enqueue(new int[] { newPriority, taskId }, new int[] { newPriority, taskId });
    }

    /// <summary>
    /// 刪除指定任務
    /// 使用懶刪除：只從雜湊表中移除，堆中的記錄將在後續操作中被跳過
    /// 時間複雜度：O(1)
    /// 空間複雜度：O(1)
    /// 
    /// 注意：題目保證 taskId 存在於系統中，因此直接執行刪除操作
    /// </summary>
    /// <param name="taskId">要刪除的任務ID</param>
    public void Rmv(int taskId)
    {
        // 題目保證 taskId 存在，直接從雜湊表中移除任務
        // 堆中對應的記錄會在 ExecTop 方法中被識別為無效並跳過
        taskInfo.Remove(taskId);
    }

    /// <summary>
    /// 執行最高優先級的任務
    /// 使用懶刪除機制處理已刪除或已編輯的任務記錄
    /// 時間複雜度：O(log n) 平均情況，O(n log n) 最壞情況（當大量無效記錄需要清理時）
    /// 空間複雜度：O(1)
    /// </summary>
    /// <returns>執行任務的使用者ID，如果沒有可執行任務則返回 -1</returns>
    public int ExecTop()
    {
        // 持續從堆頂取出任務，直到找到有效任務或堆空
        while (heap.Count > 0)
        {
            // 取出優先級最高的任務
            var task = heap.Dequeue();
            int priority = task[0];
            int taskId = task[1];
            
            // 檢查任務是否仍然有效（存在於雜湊表中且優先級匹配）
            if (taskInfo.TryGetValue(taskId, out var info) && info[1] == priority)
            {
                // 任務有效，取得使用者ID並從系統中移除該任務
                int userId = info[0];
                taskInfo.Remove(taskId);
                return userId;
            }
            
            // 如果任務無效（已被刪除或優先級已更改），繼續處理下一個任務
            // 這就是懶刪除機制的體現
        }
        
        // 沒有可執行的任務
        return -1;
    }
}
