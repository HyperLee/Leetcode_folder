# LeetCode 700. Search in a Binary Search Tree

[![LeetCode](https://img.shields.io/badge/LeetCode-700-orange?style=flat-square&logo=leetcode)](https://leetcode.com/problems/search-in-a-binary-search-tree/)
[![Difficulty](https://img.shields.io/badge/Difficulty-Easy-green?style=flat-square)](https://leetcode.com/problems/search-in-a-binary-search-tree/)
[![Language](https://img.shields.io/badge/Language-C%23-blue?style=flat-square&logo=csharp)](https://dotnet.microsoft.com/)
[![.NET](https://img.shields.io/badge/.NET-10.0-purple?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)

二元搜尋樹 (Binary Search Tree, BST) 中的搜尋問題解法，使用 C# 實作。

## 題目描述

給定一個二元搜尋樹 (BST) 的根節點 `root` 與一個整數 `val`：

- 找到值等於 `val` 的節點
- 回傳以該節點為根的子樹
- 若不存在，回傳 `null`

### 範例

**範例 1：**

```
輸入: root = [4,2,7,1,3], val = 2
輸出: [2,1,3]
```

```
       4
      / \
     2   7
    / \
   1   3

搜尋 val = 2，回傳以節點 2 為根的子樹：

     2
    / \
   1   3
```

**範例 2：**

```
輸入: root = [4,2,7,1,3], val = 5
輸出: null
```

節點 5 不存在於樹中，因此回傳 `null`。

### 限制條件

- 樹中節點數量範圍為 `[1, 5000]`
- `1 <= Node.val <= 10^7`
- `root` 是一個二元搜尋樹
- `1 <= val <= 10^7`

---

## 解題思路

### 核心概念：二元搜尋樹的性質

二元搜尋樹 (BST) 具有以下特性：

| 性質 | 說明 |
|------|------|
| 左子樹 | 所有節點的值**均小於**根節點的值 |
| 右子樹 | 所有節點的值**均大於**根節點的值 |
| 遞迴性 | 左、右子樹也都是二元搜尋樹 |

這個性質讓我們可以在每次比較後**排除一半的搜尋空間**，類似於二分搜尋。

### 解法：遞迴搜尋

根據 BST 的性質，我們可以設計以下演算法：

```
1. 若 root 為空 → 回傳 null（搜尋結束，未找到）
2. 若 val == root.val → 回傳 root（找到目標）
3. 若 val < root.val → 遞迴搜尋左子樹
4. 若 val > root.val → 遞迴搜尋右子樹
```

> [!TIP]
> 利用 BST 的有序性質，每次遞迴都能排除一半的節點，大幅提升搜尋效率！

### 複雜度分析

| 複雜度 | 數值 | 說明 |
|--------|------|------|
| 時間複雜度 | O(h) | h 為樹的高度 |
| 空間複雜度 | O(h) | 遞迴呼叫堆疊的深度 |

> [!NOTE]
> - 最佳情況（平衡樹）：h = log(n)，複雜度為 O(log n)
> - 最差情況（退化為鏈結串列）：h = n，複雜度為 O(n)

---

## 演算法流程演示

以 `root = [4,2,7,1,3], val = 2` 為例：

```
步驟 1: 從根節點開始
       [4]  ← 目前節點
      /   \
     2     7
    / \
   1   3

   比較: val(2) < root.val(4)
   → 往左子樹搜尋

步驟 2: 進入左子樹
        4
      /   \
    [2]  ← 目前節點
    / \
   1   3

   比較: val(2) == root.val(2)
   → 找到目標！回傳此節點

結果: 回傳以節點 2 為根的子樹
     2
    / \
   1   3
```

### 搜尋不存在值的流程

以 `root = [4,2,7,1,3], val = 5` 為例：

```
步驟 1: root.val(4) < val(5) → 往右子樹
步驟 2: root.val(7) > val(5) → 往左子樹
步驟 3: 左子樹為空 → 回傳 null
```

---

## 程式碼

```csharp
public TreeNode SearchBST(TreeNode root, int val)
{
    // 基礎情況 1：節點為空，表示搜尋結束，未找到目標值
    if (root is null)
    {
        return null;
    }

    // 基礎情況 2：找到目標值，回傳當前節點（以此節點為根的子樹）
    if (root.val == val)
    {
        return root;
    }

    // 遞迴情況：根據 BST 性質決定往左或往右搜尋
    if (root.val < val)
    {
        // 目標值大於當前節點，往右子樹搜尋
        return SearchBST(root.right, val);
    }
    else
    {
        // 目標值小於當前節點，往左子樹搜尋
        return SearchBST(root.left, val);
    }
}
```

---

## 執行方式

### 環境需求

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download) 或更新版本

### 建構與執行

```bash
# 進入專案目錄
cd leetcode_700

# 建構專案
dotnet build

# 執行程式
dotnet run
```

### 預期輸出

```
=== LeetCode 700. Search in a Binary Search Tree ===

測試樹結構:
       4
      / \
     2   7
    / \
   1   3

測試 1: 搜尋 val = 2
結果: 找到節點，子樹根節點值 = 2

測試 2: 搜尋 val = 5
結果: 未找到

測試 3: 搜尋 val = 4
結果: 找到節點，子樹根節點值 = 4

測試 4: 搜尋 val = 7
結果: 找到節點，子樹根節點值 = 7
```

---

## 相關題目

- [701. Insert into a Binary Search Tree](https://leetcode.com/problems/insert-into-a-binary-search-tree/)
- [450. Delete Node in a BST](https://leetcode.com/problems/delete-node-in-a-bst/)
- [98. Validate Binary Search Tree](https://leetcode.com/problems/validate-binary-search-tree/)

## 參考資料

- [LeetCode 700 題目連結 (英文)](https://leetcode.com/problems/search-in-a-binary-search-tree/)
- [LeetCode 700 題目連結 (中文)](https://leetcode.cn/problems/search-in-a-binary-search-tree/)
