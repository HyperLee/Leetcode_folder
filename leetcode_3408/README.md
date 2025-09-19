# LeetCode 3408: Design Task Manager

> **高效任務管理器實現** - 使用優先佇列與雜湊表的完美結合

一個基於 C# 實現的高效任務管理系統，解決 LeetCode 3408 問題。該實現採用**優先佇列 + 雜湊表 + 懶刪除機制**的策略，提供了最佳的時間複雜度表現。

## 問題描述

設計一個任務管理器，支援以下操作：

- **TaskManager(tasks)**: 初始化任務管理器
- **add(userId, taskId, priority)**: 添加新任務
- **edit(taskId, newPriority)**: 編輯任務優先級
- **rmv(taskId)**: 刪除任務
- **execTop()**: 執行最高優先級任務並返回對應的使用者ID

### 排序規則

1. **優先級高的任務先執行**（降序）
2. **相同優先級下，任務ID大的先執行**（降序）

## 核心演算法解析

### 🎯 解題思路

這個問題的核心挑戰在於如何高效地：
1. 維護任務的優先級排序
2. 支援動態的任務優先級修改
3. 處理任務的刪除操作

### 🏗️ 資料結構設計

```csharp
public class TaskManager
{
    // 雜湊表：快速查詢任務資訊 O(1)
    private readonly Dictionary<int, int[]> taskInfo; // taskId -> [userId, priority]
    
    // 優先佇列：維護任務排序 O(log n)
    private readonly PriorityQueue<int[], int[]> heap; // [priority, taskId]
}
```

#### 1. 雜湊表 (Dictionary)
- **用途**: 儲存每個任務的最新資訊（使用者ID和優先級）
- **優勢**: O(1) 時間複雜度的查詢和更新
- **格式**: `taskId -> [userId, priority]`

#### 2. 優先佇列 (PriorityQueue)
- **用途**: 維護任務的排序，快速獲取最高優先級任務
- **排序規則**: 
  - 優先級降序（高優先級先執行）
  - 相同優先級下任務ID降序
- **格式**: `[priority, taskId]`

### 🔄 懶刪除機制

這是本解法的精髓所在！

> **為什麼需要懶刪除？**
> 
> 傳統的堆結構不支援直接刪除或修改中間元素，強行操作會導致 O(n) 的時間複雜度。
> 懶刪除通過延遲清理無效記錄，將平均時間複雜度降到最優。

#### 機制原理

1. **編輯操作**: 不修改堆中的舊記錄，直接添加新記錄
2. **刪除操作**: 只從雜湊表中移除，堆中記錄保留
3. **執行操作**: 檢查記錄有效性，跳過無效記錄

```csharp
// 懶刪除的核心邏輯
public int ExecTop()
{
    while (heap.Count > 0)
    {
        var task = heap.Dequeue();
        int priority = task[0];
        int taskId = task[1];
        
        // 驗證記錄有效性
        if (taskInfo.TryGetValue(taskId, out var info) && info[1] == priority)
        {
            // 有效記錄：執行並清理
            int userId = info[0];
            taskInfo.Remove(taskId);
            return userId;
        }
        // 無效記錄：跳過，繼續處理下一個
    }
    return -1;
}
```

## 複雜度分析

| 操作 | 時間複雜度 | 空間複雜度 | 說明 |
|------|------------|------------|------|
| **建構函式** | O(n log n) | O(n) | 初始化 n 個任務到堆中 |
| **Add** | O(log n) | O(1) | 插入堆的標準複雜度 |
| **Edit** | O(log n) | O(1) | 等同於添加新記錄 |
| **Remove** | O(1) | O(1) | 只從雜湊表刪除 |
| **ExecTop** | O(log n) 平均<br/>O(n log n) 最壞 | O(1) | 懶刪除可能需要清理多個無效記錄 |

> **注意**: `ExecTop` 的最壞情況出現在大量無效記錄堆積時，但在實際應用中平均性能優秀。

## 核心優勢

### ✅ 高效性能
- 大部分操作達到 O(log n) 時間複雜度
- 懶刪除避免了昂貴的堆重構操作

### ✅ 記憶體友善
- 雜湊表提供精確的任務狀態追蹤
- 避免了複雜的堆維護邏輯

### ✅ 邏輯清晰
- 責任分離：雜湊表管狀態，堆管排序
- 懶刪除機制易於理解和實現

## 使用範例

```csharp
// 初始化任務管理器
var tasks = new List<IList<int>>
{
    new List<int> { 1, 101, 10 }, // 使用者1, 任務101, 優先級10
    new List<int> { 2, 102, 20 }, // 使用者2, 任務102, 優先級20
    new List<int> { 3, 103, 15 }  // 使用者3, 任務103, 優先級15
};

var taskManager = new TaskManager(tasks);

// 執行最高優先級任務
Console.WriteLine(taskManager.ExecTop()); // 輸出: 2 (優先級20最高)
Console.WriteLine(taskManager.ExecTop()); // 輸出: 3 (優先級15次高)

// 添加新任務
taskManager.Add(4, 104, 25);

// 編輯任務優先級
taskManager.Edit(101, 30);

// 刪除任務
taskManager.Rmv(103);

Console.WriteLine(taskManager.ExecTop()); // 輸出: 1 (編輯後優先級30最高)
```

## 測試驗證

專案包含完整的測試用例，涵蓋：

- **基本功能測試**: 驗證核心 CRUD 操作
- **邊界條件測試**: 空佇列、單一元素等情況
- **排序規則測試**: 優先級和任務ID的排序邏輯
- **懶刪除測試**: 驗證無效記錄的正確處理

運行測試：

```bash
dotnet run
```

## 技術實現細節

### 優先佇列自定義比較器

```csharp
heap = new PriorityQueue<int[], int[]>(
    Comparer<int[]>.Create((a, b) =>
    {
        // 首先比較優先級（降序）
        if (a[0] == b[0])
        {
            // 優先級相同時，比較任務ID（降序）
            return b[1].CompareTo(a[1]);
        }
        return b[0].CompareTo(a[0]);
    })
);
```

### 懶刪除的一致性保證

通過在 `ExecTop` 中驗證 `taskInfo.TryGetValue(taskId, out var info) && info[1] == priority`，
確保：
1. 任務仍然存在（未被刪除）
2. 優先級匹配（未被修改）

## 相關連結

- [LeetCode 原題 (英文)](https://leetcode.com/problems/design-task-manager/description/?envType=daily-question&envId=2025-09-18)
- [LeetCode 原題 (中文)](https://leetcode.cn/problems/design-task-manager/description/)

## 開發環境

- **.NET 8.0**: 最新的 .NET 平台支援
- **C# 12**: 使用現代 C# 語法特性
- **Visual Studio Code**: 跨平台開發環境
