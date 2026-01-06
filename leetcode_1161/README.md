# LeetCode 1161 - 最大層內元素和

[![LeetCode](https://img.shields.io/badge/LeetCode-1161-FFA116?style=flat-square&logo=leetcode)](https://leetcode.com/problems/maximum-level-sum-of-a-binary-tree/)
[![Difficulty](https://img.shields.io/badge/Difficulty-Medium-orange?style=flat-square)]()
[![C#](https://img.shields.io/badge/C%23-14.0-239120?style=flat-square&logo=csharp)]()

> 給定一個二元樹的根節點，根節點所在層為 1，子節點所在層為 2，以此類推。回傳使該層節點值總和最大的最小層級 x。

## 題目描述

給定一個二元樹的根節點 `root`，其根節點所在層級為 1，子節點所在層級為 2，依此類推。

請回傳使該層節點值總和最大的**最小層級 x**。

### 限制條件

- 樹中的節點數量範圍為 `[1, 10^4]`
- `-10^5 <= Node.val <= 10^5`

### 範例

**範例 1:**

```
輸入: root = [1,7,0,7,-8,null,null]
樹狀結構:
      1
     / \
    7   0
   / \
  7  -8

層級總和:
- 第 1 層: 1
- 第 2 層: 7 + 0 = 7
- 第 3 層: 7 + (-8) = -1

輸出: 2
說明: 第 2 層的節點值總和最大為 7
```

**範例 2:**

```
輸入: root = [989,null,10250,98693,-89388,null,null,null,-32127]
樹狀結構:
       989
         \
        10250
        /   \
    98693  -89388
              \
            -32127

層級總和:
- 第 1 層: 989
- 第 2 層: 10250
- 第 3 層: 98693 + (-89388) = 9305
- 第 4 層: -32127

輸出: 2
說明: 第 2 層的節點值總和最大為 10250
```

## 解題思路

### 核心概念

這道題目的關鍵在於：
1. **計算每一層的節點值總和**
2. **找出總和最大的層級**
3. **如果有多層總和相同，選擇層級較小的**

### 解法：深度優先搜索 (DFS)

#### 為什麼選擇 DFS？

相較於廣度優先搜索 (BFS)，使用 DFS 配合動態陣列有以下優勢：
- **空間效率更高**：只需維護一個動態陣列記錄每層總和，不需要額外的佇列
- **程式碼簡潔**：遞迴實作更直觀易懂
- **性能優秀**：避免了 BFS 中頻繁的佇列操作

#### 演算法步驟

1. **使用動態陣列維護層級總和**
   - 使用 `List<int>` 作為動態陣列 `sum`
   - `sum[i]` 儲存第 i 層（從 0 開始）的節點值總和

2. **DFS 遞迴遍歷**
   - 從根節點開始，層級從 0 開始計數
   - 對於每個節點：
     - 如果 `level == sum.Count`：表示第一次訪問該層，將節點值加入陣列末尾
     - 否則：將節點值累加到對應層級的總和
   - 遞迴處理左子樹和右子樹，層級加 1

3. **找出最大總和的層級**
   - 遍歷 `sum` 陣列
   - 記錄總和最大的層級索引
   - 由於從左到右遍歷，自動滿足「層級最小」的條件
   - 最後將索引加 1（因為題目層級從 1 開始）

### 複雜度分析

- **時間複雜度**: O(n)
  - n 是樹中的節點數量
  - 每個節點訪問一次

- **空間複雜度**: O(h)
  - h 是樹的高度
  - 主要是遞迴呼叫堆疊的深度
  - 最壞情況下（樹退化成鍊表）為 O(n)

## 詳細範例演示

讓我們用範例 1 詳細說明演算法執行過程：

```
樹狀結構:
      1
     / \
    7   0
   / \
  7  -8
```

### 執行流程

| 步驟 | 訪問節點 | 層級 | 動作 | sum 陣列狀態 |
|------|----------|------|------|--------------|
| 1 | 1 | 0 | 第一次訪問第 0 層，加入陣列 | `[1]` |
| 2 | 7 | 1 | 第一次訪問第 1 層，加入陣列 | `[1, 7]` |
| 3 | 7 | 2 | 第一次訪問第 2 層，加入陣列 | `[1, 7, 7]` |
| 4 | -8 | 2 | 第 2 層已存在，累加到總和 | `[1, 7, -1]` |
| 5 | 0 | 1 | 第 1 層已存在，累加到總和 | `[1, 7, -1]` |

**找出最大總和**：
- 遍歷 sum 陣列：`[1, 7, -1]`
- 最大值是 7，索引為 1
- 回傳 1 + 1 = **2**

### DFS 遞迴樹

```
DFS(1, 0)
├─ DFS(7, 1)
│  ├─ DFS(7, 2)
│  └─ DFS(-8, 2)
└─ DFS(0, 1)
```

**遍歷順序**（前序遍歷）：1 → 7 → 7 → -8 → 0

## 程式碼實作

主要函式：

```csharp
public static int MaxLevelSum(TreeNode root)
{
    DFS(root, 0);
    int res = 0;
    
    for(int i = 0; i < sum.Count; i++)
    {
        if(sum[i] > sum[res])
        {
            res = i;
        }
    }
    
    return res + 1;
}
```

DFS 遞迴函式：

```csharp
private static void DFS(TreeNode node, int level)
{
    if(level == sum.Count)
    {
        sum.Add(node.val);
    }
    else
    {
        sum[level] += node.val;
    }

    if(node.left != null)
    {
        DFS(node.left, level + 1);
    }

    if(node.right != null)
    {
        DFS(node.right, level + 1);
    }
}
```

## 執行與測試

### 建構專案

```bash
dotnet build
```

### 執行程式

```bash
dotnet run --project leetcode_1161
```

### 預期輸出

```
測試案例 1 結果: 2
測試案例 2 結果: 3
```

## 關鍵要點

- ✅ 使用動態陣列避免事先計算樹的深度
- ✅ DFS 前序遍歷確保按層級順序處理
- ✅ 由左到右遍歷找最大值，自動滿足「最小層級」要求
- ✅ 層級從 0 開始計數，最後回傳時加 1

## 相關題目

- [LeetCode 102 - Binary Tree Level Order Traversal](https://leetcode.com/problems/binary-tree-level-order-traversal/)
- [LeetCode 637 - Average of Levels in Binary Tree](https://leetcode.com/problems/average-of-levels-in-binary-tree/)
- [LeetCode 513 - Find Bottom Left Tree Value](https://leetcode.com/problems/find-bottom-left-tree-value/)

## 參考資料

- [LeetCode 1161 - Maximum Level Sum of a Binary Tree](https://leetcode.com/problems/maximum-level-sum-of-a-binary-tree/)
- [LeetCode CN 1161 - 最大层内元素和](https://leetcode.cn/problems/maximum-level-sum-of-a-binary-tree/)
