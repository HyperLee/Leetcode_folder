namespace leetcode_3408;

class Program
{
    /// <summary>
    /// 3408. Design Task Manager
    /// https://leetcode.com/problems/design-task-manager/description/?envType=daily-question&envId=2025-09-18
    /// 3408. 设计任务管理器
    /// https://leetcode.cn/problems/design-task-manager/description/
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
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
                if (a[0] == b[0])
                {
                    // 優先級相同時，比較任務ID（降序）
                    return b[1].CompareTo(a[1]);
                }
                return b[0].CompareTo(a[0]); // 優先級降序排列
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
    /// </summary>
    /// <param name="userId">任務所屬的使用者ID</param>
    /// <param name="taskId">任務的唯一標識符</param>
    /// <param name="priority">任務的優先級</param>
    public void Add(int userId, int taskId, int priority)
    {
        // 將任務資訊儲存到雜湊表中
        taskInfo[taskId] = new int[] { userId, priority };
        
        // 將任務加入優先佇列，確保能夠按優先級排序
        heap.Enqueue(new int[] { priority, taskId }, new int[] { priority, taskId });
    }

    /// <summary>
    /// 編輯現有任務的優先級
    /// 使用懶刪除機制：不直接修改堆中的元素，而是添加新記錄
    /// 時間複雜度：O(log n)，插入堆的時間複雜度
    /// 空間複雜度：O(1)
    /// </summary>
    /// <param name="taskId">要編輯的任務ID</param>
    /// <param name="newPriority">新的優先級</param>
    public void Edit(int taskId, int newPriority)
    {
        // 檢查任務是否存在
        if (taskInfo.ContainsKey(taskId))
        {
            // 更新雜湊表中的優先級資訊
            int userId = taskInfo[taskId][0]; // 保持使用者ID不變
            taskInfo[taskId] = new int[] { userId, newPriority };
            
            // 向堆中添加新的記錄（舊記錄通過懶刪除處理）
            heap.Enqueue(new int[] { newPriority, taskId }, new int[] { newPriority, taskId });
        }
    }

    /// <summary>
    /// 刪除指定任務
    /// 使用懶刪除：只從雜湊表中移除，堆中的記錄將在後續操作中被跳過
    /// 時間複雜度：O(1)
    /// 空間複雜度：O(1)
    /// </summary>
    /// <param name="taskId">要刪除的任務ID</param>
    public void Rmv(int taskId)
    {
        // 直接從雜湊表中移除任務
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
