# 🌳 LeetCode 94 - Binary Tree Inorder Traversal

二元樹的中序走訪

[![LeetCode](https://img.shields.io/badge/LeetCode-94-orange?style=flat-square)](https://leetcode.com/problems/binary-tree-inorder-traversal/)
[![Difficulty](https://img.shields.io/badge/Difficulty-Easy-brightgreen?style=flat-square)](https://leetcode.com/problems/binary-tree-inorder-traversal/)
[![Language](https://img.shields.io/badge/Language-C%23-blue?style=flat-square)](https://docs.microsoft.com/en-us/dotnet/csharp/)

## 📋 題目描述

給定一個二元樹的根節點 `root`，返回其節點值的**中序走訪**結果。

### 範例

**範例 1:**
```
輸入: root = [1,null,2,3]
    1
     \
      2
     /
    3

輸出: [1,3,2]
```

**範例 2:**
```
輸入: root = [1,2,3,4,5,null,8,null,null,6,7,9]
        1
       / \
      2   3
     / \   \
    4   5   8
       / \ /
      6  7 9

輸出: [4,2,6,5,7,1,3,9,8]
```

**範例 3:**
```
輸入: root = []
輸出: []
```

**範例 4:**
```
輸入: root = [1]
輸出: [1]
```

### 限制條件

- 樹中節點數量範圍 `[0, 100]`
- `-100 <= Node.val <= 100`

---

## 💡 解題概念與想法

### 什麼是中序走訪 (Inorder Traversal)？

中序走訪是二元樹走訪的三種深度優先搜尋（DFS）方式之一，走訪順序為：

```
左子樹 → 根節點 → 右子樹
```

| 走訪方式 | 順序 | 英文 |
|---------|------|------|
| 前序走訪 | 根 → 左 → 右 | Preorder |
| **中序走訪** | **左 → 根 → 右** | **Inorder** |
| 後序走訪 | 左 → 右 → 根 | Postorder |

> [!TIP]
> 對於**二元搜尋樹 (BST)**，中序走訪會按照節點值的**升序**輸出，這是一個非常重要的特性！

### 核心思路

使用**遞迴**方式實作中序走訪：

1. 如果當前節點為空，直接返回（遞迴終止條件）
2. 先遞迴走訪**左子樹**
3. 訪問**當前節點**（將值加入結果列表）
4. 再遞迴走訪**右子樹**

---

## 🔧 解題方法

### 方法：遞迴 (Recursion)

```csharp
public IList<int> InorderTraversal(TreeNode? root)
{
    List<int> res = new List<int>();
    Inorder(root, res);
    return res;
}

public static void Inorder(TreeNode? root, List<int> res)
{
    // 遞迴終止條件：若節點為空，直接返回
    if (root is null)
    {
        return;
    }

    // Step 1: 遞迴走訪左子樹
    Inorder(root.left, res);

    // Step 2: 訪問當前節點
    res.Add(root.val);

    // Step 3: 遞迴走訪右子樹
    Inorder(root.right, res);
}
```

### 複雜度分析

| 複雜度 | 數值 | 說明 |
|--------|------|------|
| 時間複雜度 | O(n) | n 為節點數量，每個節點恰好被訪問一次 |
| 空間複雜度 | O(n) | 最壞情況下遞迴堆疊深度為 n（樹為鏈狀） |

---

## 📝 演示流程

以 `root = [1,null,2,3]` 為例：

```
樹結構:
    1
     \
      2
     /
    3
```

### 執行步驟

```
呼叫 Inorder(1)
├── Step 1: Inorder(null)         → 左子樹為空，返回
├── Step 2: res.Add(1)            → res = [1]
└── Step 3: Inorder(2)            → 走訪右子樹
            ├── Step 1: Inorder(3)
            │           ├── Inorder(null) → 返回
            │           ├── res.Add(3)    → res = [1, 3]
            │           └── Inorder(null) → 返回
            ├── Step 2: res.Add(2)        → res = [1, 3, 2]
            └── Step 3: Inorder(null)     → 返回

最終結果: [1, 3, 2]
```

### 更複雜的範例

以 `root = [1,2,3,4,5,null,8,null,null,6,7,9]` 為例：

```
樹結構:
        1
       / \
      2   3
     / \   \
    4   5   8
       / \ /
      6  7 9
```

執行流程：

| 步驟 | 操作 | res 狀態 |
|------|------|----------|
| 1 | 走到最左節點 4 | [] |
| 2 | 訪問節點 4 | [4] |
| 3 | 回到節點 2，訪問 | [4, 2] |
| 4 | 走到節點 5 的左子樹 6 | [4, 2] |
| 5 | 訪問節點 6 | [4, 2, 6] |
| 6 | 訪問節點 5 | [4, 2, 6, 5] |
| 7 | 訪問節點 7 | [4, 2, 6, 5, 7] |
| 8 | 回到根節點 1，訪問 | [4, 2, 6, 5, 7, 1] |
| 9 | 訪問節點 3 | [4, 2, 6, 5, 7, 1, 3] |
| 10 | 走到節點 8 的左子樹 9 | [4, 2, 6, 5, 7, 1, 3] |
| 11 | 訪問節點 9 | [4, 2, 6, 5, 7, 1, 3, 9] |
| 12 | 訪問節點 8 | [4, 2, 6, 5, 7, 1, 3, 9, 8] |

**最終結果**: `[4, 2, 6, 5, 7, 1, 3, 9, 8]`

---

## 🚀 執行程式

### 環境需求

- [.NET 10.0](https://dotnet.microsoft.com/download) 或更高版本

### 執行步驟

```bash
# 進入專案目錄
cd leetcode_094

# 建構專案
dotnet build

# 執行程式
dotnet run
```

### 預期輸出

```
範例 1: [1, 3, 2]
範例 2: [4, 2, 6, 5, 7, 1, 3, 9, 8]
範例 3 (空樹): []
範例 4 (單節點): [1]
```

---

## 📚 相關題目

| 題號 | 題目 | 難度 |
|------|------|------|
| 144 | [Binary Tree Preorder Traversal](https://leetcode.com/problems/binary-tree-preorder-traversal/) | Easy |
| 145 | [Binary Tree Postorder Traversal](https://leetcode.com/problems/binary-tree-postorder-traversal/) | Easy |
| 102 | [Binary Tree Level Order Traversal](https://leetcode.com/problems/binary-tree-level-order-traversal/) | Medium |
| 173 | [Binary Search Tree Iterator](https://leetcode.com/problems/binary-search-tree-iterator/) | Medium |

---

## 📖 參考資料

- [LeetCode 94. Binary Tree Inorder Traversal](https://leetcode.com/problems/binary-tree-inorder-traversal/)
- [LeetCode 94. 二叉树的中序遍历](https://leetcode.cn/problems/binary-tree-inorder-traversal/)
