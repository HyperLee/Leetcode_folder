# LeetCode 144. Binary Tree Preorder Traversal

[![LeetCode](https://img.shields.io/badge/LeetCode-144-FFA116?style=flat-square&logo=leetcode)](https://leetcode.com/problems/binary-tree-preorder-traversal/)
[![Difficulty](https://img.shields.io/badge/Difficulty-Easy-5CB85C?style=flat-square)](https://leetcode.com/problems/binary-tree-preorder-traversal/)
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)

使用 C# 實作 LeetCode 第 144 題：二元樹的前序遍歷 (Binary Tree Preorder Traversal)

## 題目描述

給定一個二元樹的根節點 `root`，回傳其節點值的**前序遍歷**結果。

### 範例

**範例 1:**

```
輸入: root = [1, null, 2, 3]
輸出: [1, 2, 3]
```

```
    1
     \
      2
     /
    3
```

**範例 2:**

```
輸入: root = []
輸出: []
```

**範例 3:**

```
輸入: root = [1]
輸出: [1]
```

### 限制條件

- 節點數量範圍：`[0, 100]`
- `-100 <= Node.val <= 100`

---

## 解題概念與出發點

### 什麼是前序遍歷？

前序遍歷 (Preorder Traversal) 是二元樹遍歷的一種方式，其訪問順序為：

```
根節點 (Root) → 左子樹 (Left) → 右子樹 (Right)
```

簡記為：**根 → 左 → 右**

### 解題思路

本題的核心在於理解並實作前序遍歷的順序。我們採用**深度優先搜尋 (DFS)** 的遞迴方式來解決：

1. **終止條件**：若當前節點為 `null`，直接返回
2. **處理根節點**：將當前節點的值加入結果清單
3. **遞迴左子樹**：對左子節點進行前序遍歷
4. **遞迴右子樹**：對右子節點進行前序遍歷

---

## 解法詳解

### DFS 遞迴解法

```csharp
public IList<int> PreorderTraversal(TreeNode? root)
{
    List<int> list = new List<int>();
    DFS(root, list);
    return list;
}

private static void DFS(TreeNode? node, List<int> list)
{
    // 終止條件：若節點為空，結束此次遞迴
    if (node is null)
    {
        return;
    }

    // 步驟 1：訪問根節點
    list.Add(node.val);
    
    // 步驟 2：遞迴遍歷左子樹
    DFS(node.left, list);
    
    // 步驟 3：遞迴遍歷右子樹
    DFS(node.right, list);
}
```

### 複雜度分析

| 項目 | 複雜度 | 說明 |
|------|--------|------|
| **時間複雜度** | O(n) | 每個節點恰好被訪問一次 |
| **空間複雜度** | O(h) | h 為樹的高度，遞迴呼叫堆疊的空間消耗 |

> [!NOTE]
> 空間複雜度在最壞情況下（傾斜樹）為 O(n)，最佳情況下（平衡樹）為 O(log n)

---

## 範例演示流程

以二元樹 `[1, 2, 3, 4, 5]` 為例：

```
        1
       / \
      2   3
     / \
    4   5
```

### 執行步驟追蹤

| 步驟 | 當前節點 | 動作 | 結果清單 |
|------|----------|------|----------|
| 1 | 1 | 訪問根節點 | [1] |
| 2 | 2 | 遞迴左子樹，訪問節點 2 | [1, 2] |
| 3 | 4 | 遞迴左子樹，訪問節點 4 | [1, 2, 4] |
| 4 | null | 節點 4 的左子樹為空，返回 | [1, 2, 4] |
| 5 | null | 節點 4 的右子樹為空，返回 | [1, 2, 4] |
| 6 | 5 | 遞迴右子樹，訪問節點 5 | [1, 2, 4, 5] |
| 7 | null | 節點 5 的左子樹為空，返回 | [1, 2, 4, 5] |
| 8 | null | 節點 5 的右子樹為空，返回 | [1, 2, 4, 5] |
| 9 | 3 | 遞迴右子樹，訪問節點 3 | [1, 2, 4, 5, 3] |
| 10 | null | 節點 3 的左子樹為空，返回 | [1, 2, 4, 5, 3] |
| 11 | null | 節點 3 的右子樹為空，返回 | [1, 2, 4, 5, 3] |

**最終輸出**：`[1, 2, 4, 5, 3]`

### 遞迴呼叫圖示

```
PreorderTraversal(1)
├── Add(1) → [1]
├── DFS(2)
│   ├── Add(2) → [1, 2]
│   ├── DFS(4)
│   │   ├── Add(4) → [1, 2, 4]
│   │   ├── DFS(null) → return
│   │   └── DFS(null) → return
│   └── DFS(5)
│       ├── Add(5) → [1, 2, 4, 5]
│       ├── DFS(null) → return
│       └── DFS(null) → return
└── DFS(3)
    ├── Add(3) → [1, 2, 4, 5, 3]
    ├── DFS(null) → return
    └── DFS(null) → return
```

---

## Binary Tree Traversal 三種方法介紹

二元樹的遍歷主要有三種深度優先 (DFS) 的方式，差別在於**訪問根節點的時機**：

### 1. 前序遍歷 (Preorder Traversal)

**順序**：根 → 左 → 右

```csharp
void Preorder(TreeNode? node)
{
    if (node is null) return;
    
    Visit(node);           // 先訪問根
    Preorder(node.left);   // 再遍歷左子樹
    Preorder(node.right);  // 最後遍歷右子樹
}
```

**特點**：
- 第一個訪問的一定是根節點
- 適合用於**複製**二元樹、**序列化**樹結構
- 常用於前綴表達式 (Prefix Expression)

**範例**：

```
        1
       / \
      2   3
     / \
    4   5

前序遍歷結果: [1, 2, 4, 5, 3]
```

### 2. 中序遍歷 (Inorder Traversal)

**順序**：左 → 根 → 右

```csharp
void Inorder(TreeNode? node)
{
    if (node is null) return;
    
    Inorder(node.left);    // 先遍歷左子樹
    Visit(node);           // 再訪問根
    Inorder(node.right);   // 最後遍歷右子樹
}
```

**特點**：
- 對於**二元搜尋樹 (BST)**，中序遍歷會得到**升序排列**的結果
- 適合用於 BST 的排序輸出
- 常用於中綴表達式 (Infix Expression)

**範例**：

```
        1
       / \
      2   3
     / \
    4   5

中序遍歷結果: [4, 2, 5, 1, 3]
```

### 3. 後序遍歷 (Postorder Traversal)

**順序**：左 → 右 → 根

```csharp
void Postorder(TreeNode? node)
{
    if (node is null) return;
    
    Postorder(node.left);   // 先遍歷左子樹
    Postorder(node.right);  // 再遍歷右子樹
    Visit(node);            // 最後訪問根
}
```

**特點**：
- 最後一個訪問的一定是根節點
- 適合用於**刪除**二元樹（先刪子節點，再刪父節點）
- 適合計算目錄大小、評估表達式樹
- 常用於後綴表達式 (Postfix Expression)

**範例**：

```
        1
       / \
      2   3
     / \
    4   5

後序遍歷結果: [4, 5, 2, 3, 1]
```

### 三種遍歷方式比較

| 遍歷方式 | 順序 | 特點 | 應用場景 |
|----------|------|------|----------|
| **前序** | 根→左→右 | 根節點最先訪問 | 複製樹、序列化、前綴表達式 |
| **中序** | 左→根→右 | BST 會得到升序結果 | BST 排序輸出、中綴表達式 |
| **後序** | 左→右→根 | 根節點最後訪問 | 刪除樹、計算大小、後綴表達式 |

> [!TIP]
> 記憶口訣：「前中後」指的是**根節點**在遍歷順序中的位置
> - **前**序：根在**前**（根→左→右）
> - **中**序：根在**中**（左→根→右）
> - **後**序：根在**後**（左→右→根）

---

## 執行專案

```bash
# 建構專案
dotnet build

# 執行程式
dotnet run --project leetcode_144
```

## 相關題目

- [LeetCode 94. Binary Tree Inorder Traversal](https://leetcode.com/problems/binary-tree-inorder-traversal/) - 中序遍歷
- [LeetCode 145. Binary Tree Postorder Traversal](https://leetcode.com/problems/binary-tree-postorder-traversal/) - 後序遍歷
- [LeetCode 102. Binary Tree Level Order Traversal](https://leetcode.com/problems/binary-tree-level-order-traversal/) - 層序遍歷 (BFS)
