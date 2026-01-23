# 617. Merge Two Binary Trees

[![LeetCode](https://img.shields.io/badge/LeetCode-617-orange?style=flat-square)](https://leetcode.com/problems/merge-two-binary-trees/)
[![Difficulty](https://img.shields.io/badge/Difficulty-Easy-green?style=flat-square)](https://leetcode.com/problems/merge-two-binary-trees/)
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square)](https://dotnet.microsoft.com/)

合併兩棵二元樹的 LeetCode 解題實作。

## 題目描述

給定兩棵二元樹 `root1` 與 `root2`。假設把其中一棵覆蓋在另一棵之上，部分節點會重疊，其他則不會。請將兩棵樹合併為一棵新的二元樹。

**合併規則：**
- 當兩節點重疊時，合併節點的值為兩節點值的**總和**
- 當只有一個節點存在時，使用該**非 null** 的節點作為合併後的節點

> [!NOTE]
> 合併過程需從兩棵樹的**根節點**開始。

### 範例

**範例 1：**

```
輸入: root1 = [1,3,2,5], root2 = [2,1,3,null,4,null,7]

Tree 1:       1              Tree 2:       2
             / \                          / \
            3   2                        1   3
           /                              \   \
          5                                4   7

輸出: [3,4,5,5,4,null,7]

合併後:       3
             / \
            4   5
           / \   \
          5   4   7
```

**範例 2：**

```
輸入: root1 = [1], root2 = [1,2]
輸出: [2,2]
```

### 限制條件

- 兩棵樹的節點數量範圍為 `[0, 2000]`
- 節點值範圍為 `-10⁴ <= Node.val <= 10⁴`

## 解題思路

### 核心概念

這道題目的關鍵在於理解**遞迴遍歷**的特性。我們需要同時遍歷兩棵樹的對應節點位置，並根據節點是否存在來決定合併方式。

### 解題出發點

1. **分治思想**：將大問題拆解為小問題 — 合併兩棵樹等於合併根節點 + 合併左子樹 + 合併右子樹
2. **邊界條件處理**：當其中一棵樹的節點為空時，直接使用另一棵樹的節點
3. **遞迴終止**：當兩個節點都為空時，返回 null

### 演算法步驟

```
1. 若 root1 與 root2 皆為 null → 返回 null
2. 若 root1 為 null → 返回 root2（包含其所有子樹）
3. 若 root2 為 null → 返回 root1（包含其所有子樹）
4. 兩節點皆存在時：
   a. 建立新節點，值 = root1.val + root2.val
   b. 遞迴合併左子樹
   c. 遞迴合併右子樹
5. 返回合併後的新節點
```

## 解法實作

```csharp
public TreeNode MergeTrees(TreeNode root1, TreeNode root2)
{
    // 終止條件：若兩個節點都為 null，不需要合併，直接返回 null
    if (root1 is null && root2 is null)
    {
        return null;
    }

    // 若 root1 為空，直接返回 root2（包含其所有子樹）
    if (root1 is null && root2 is not null)
    {
        return root2;
    }

    // 若 root2 為空，直接返回 root1（包含其所有子樹）
    if (root1 is not null && root2 is null)
    {
        return root1;
    }

    // 兩個節點都存在時，建立新節點，值為兩節點值的總和
    TreeNode mergedNode = new TreeNode(root1.val + root2.val);

    // 遞迴合併左子樹
    mergedNode.left = MergeTrees(root1.left, root2.left);

    // 遞迴合併右子樹
    mergedNode.right = MergeTrees(root1.right, root2.right);

    return mergedNode;
}
```

## 複雜度分析

| 複雜度 | 分析 |
|--------|------|
| **時間複雜度** | O(min(m, n))，其中 m 和 n 分別為兩棵樹的節點數。只需遍歷重疊的部分。 |
| **空間複雜度** | O(min(h₁, h₂))，其中 h₁ 和 h₂ 分別為兩棵樹的高度。遞迴呼叫堆疊的深度取決於較矮的樹。 |

## 範例演示流程

以範例 1 為例，詳細說明合併過程：

```
初始狀態:
Tree1:  1          Tree2:  2
       / \                / \
      3   2              1   3
     /                    \   \
    5                      4   7

步驟 1: 合併根節點 (1, 2)
├─ 兩節點皆存在 → 建立新節點 val = 1 + 2 = 3
├─ 遞迴處理左子樹 MergeTrees(3, 1)
└─ 遞迴處理右子樹 MergeTrees(2, 3)

步驟 2: 合併節點 (3, 1) — 左子樹
├─ 兩節點皆存在 → 建立新節點 val = 3 + 1 = 4
├─ 遞迴處理左子樹 MergeTrees(5, null)
└─ 遞迴處理右子樹 MergeTrees(null, 4)

步驟 3: 合併節點 (5, null)
└─ root2 為 null → 直接返回 root1 (節點 5)

步驟 4: 合併節點 (null, 4)
└─ root1 為 null → 直接返回 root2 (節點 4)

步驟 5: 合併節點 (2, 3) — 右子樹
├─ 兩節點皆存在 → 建立新節點 val = 2 + 3 = 5
├─ 遞迴處理左子樹 MergeTrees(null, null) → 返回 null
└─ 遞迴處理右子樹 MergeTrees(null, 7)

步驟 6: 合併節點 (null, 7)
└─ root1 為 null → 直接返回 root2 (節點 7)

最終結果:
        3
       / \
      4   5
     / \   \
    5   4   7
```

## 快速開始

### 環境需求

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### 執行方式

```bash
# 進入專案目錄
cd leetcode_617

# 建構專案
dotnet build

# 執行程式
dotnet run
```

### 預期輸出

```
617. Merge Two Binary Trees
==============================

範例 1:
合併後根節點值: 3
合併後左子節點值: 4
合併後右子節點值: 5

範例 2: (其中一棵樹為空)
合併後根節點值: 1

範例 3: (兩棵樹都為空)
合併後結果: null
```

## 相關題目

- [100. Same Tree](https://leetcode.com/problems/same-tree/)
- [101. Symmetric Tree](https://leetcode.com/problems/symmetric-tree/)
- [104. Maximum Depth of Binary Tree](https://leetcode.com/problems/maximum-depth-of-binary-tree/)

## 參考資料

- [LeetCode 617 題目連結](https://leetcode.com/problems/merge-two-binary-trees/)
- [LeetCode 617 中文版](https://leetcode.cn/problems/merge-two-binary-trees/)
