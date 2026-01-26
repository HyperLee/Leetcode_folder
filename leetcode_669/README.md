# 669. Trim a Binary Search Tree

[![LeetCode](https://img.shields.io/badge/LeetCode-669-orange?style=flat-square&logo=leetcode)](https://leetcode.com/problems/trim-a-binary-search-tree/)
[![Difficulty](https://img.shields.io/badge/Difficulty-Medium-yellow?style=flat-square)](https://leetcode.com/problems/trim-a-binary-search-tree/)
[![Language](https://img.shields.io/badge/Language-C%23-239120?style=flat-square&logo=csharp)](https://dotnet.microsoft.com/)

這是 LeetCode 第 669 題「Trim a Binary Search Tree（修剪二叉搜尋樹）」的 C# 解題實作。

## 題目描述

給定一個二叉搜尋樹（BST）的根節點 `root`，以及下界 `low` 與上界 `high`，將樹進行修剪，使得所有節點值都位於 `[low, high]` 範圍內。

修剪後**不應改變**剩餘節點之間的相對結構（即某節點的任一子孫仍須保持為該節點的子孫）。已證明答案是唯一的。

回傳修剪後的 BST 的根節點，注意根節點可能會因給定範圍而改變。

### 範例

**範例 1：**

```
輸入: root = [1,0,2], low = 1, high = 2
輸出: [1,null,2]
```

```
    1                1
   / \      =>        \
  0   2                2
```

**範例 2：**

```
輸入: root = [3,0,4,null,2,null,null,1], low = 1, high = 3
輸出: [3,2,null,1]
```

```
      3                 3
     / \               /
    0   4      =>     2
     \               /
      2             1
     /
    1
```

### 限制條件

- 樹中節點數量範圍為 `[1, 10⁴]`
- `0 <= Node.val <= 10⁴`
- 樹中每個節點的值都是**唯一**的
- `root` 是一個有效的二叉搜尋樹
- `0 <= low <= high <= 10⁴`

## 解題概念與思路

### 核心觀念：利用 BST 特性

二叉搜尋樹（Binary Search Tree）具有一個重要特性：

> **左子樹所有節點值 < 根節點值 < 右子樹所有節點值**

這個特性讓我們可以在修剪時做出明智的決策，而不需要遍歷整棵樹。

### 遞迴策略

根據當前節點值與範圍 `[low, high]` 的關係，分為三種情況：

| 情況 | 條件 | 處理方式 |
|------|------|----------|
| 1 | `node.val < low` | 當前節點及其**左子樹**全部不符合條件，往**右子樹**尋找 |
| 2 | `node.val > high` | 當前節點及其**右子樹**全部不符合條件，往**左子樹**尋找 |
| 3 | `low <= node.val <= high` | 保留當前節點，遞迴修剪左右子樹 |

### 為什麼這樣做有效？

以情況 1 為例（`node.val < low`）：
- 由於 BST 的特性，左子樹所有節點值 < 當前節點值 < low
- 因此左子樹**所有節點**都小於 low，全部不符合條件
- 只需要往右子樹尋找，可能存在符合條件的節點

## 演算法流程演示

以範例 2 為例：`root = [3,0,4,null,2,null,null,1], low = 1, high = 3`

```
初始狀態:
      3
     / \
    0   4
     \
      2
     /
    1

步驟 1: 處理根節點 3
  - 3 在 [1,3] 範圍內 ✓
  - 保留節點 3，遞迴處理左右子樹

步驟 2: 處理節點 0（3 的左子節點）
  - 0 < low(1) ✗
  - 捨棄節點 0 和其左子樹
  - 轉向處理右子樹（節點 2）

步驟 3: 處理節點 2
  - 2 在 [1,3] 範圍內 ✓
  - 保留節點 2，遞迴處理其子樹

步驟 4: 處理節點 1
  - 1 在 [1,3] 範圍內 ✓
  - 保留節點 1

步驟 5: 處理節點 4（3 的右子節點）
  - 4 > high(3) ✗
  - 捨棄節點 4 和其右子樹
  - 轉向處理左子樹（為 null）

最終結果:
      3
     /
    2
   /
  1
```

## 複雜度分析

| 項目 | 複雜度 | 說明 |
|------|--------|------|
| 時間複雜度 | O(n) | 最壞情況需遍歷所有節點 |
| 空間複雜度 | O(h) | h 為樹的高度，遞迴呼叫堆疊深度 |

> [!NOTE]
> 對於平衡的 BST，h = O(log n)；對於退化成鏈表的 BST，h = O(n)

## 執行方式

### 前置需求

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download) 或更新版本

### 建構與執行

```bash
# 進入專案目錄
cd leetcode_669

# 建構專案
dotnet build

# 執行程式
dotnet run
```

### 預期輸出

```
範例 1:
[1, 2]

範例 2:
[3, 2, 1]

範例 3:
[5, 3, 2, 4, 1]
```

## 專案結構

```
leetcode_669/
├── leetcode_669.slnx          # 解決方案檔
├── README.md                   # 本說明文件
└── leetcode_669/
    ├── leetcode_669.csproj    # 專案檔
    └── Program.cs             # 主程式與解題實作
```

## 相關題目

- [LeetCode 98 - Validate Binary Search Tree](https://leetcode.com/problems/validate-binary-search-tree/)
- [LeetCode 700 - Search in a Binary Search Tree](https://leetcode.com/problems/search-in-a-binary-search-tree/)
- [LeetCode 450 - Delete Node in a BST](https://leetcode.com/problems/delete-node-in-a-bst/)

## 參考資料

- [LeetCode 題目連結（英文）](https://leetcode.com/problems/trim-a-binary-search-tree/)
- [LeetCode 題目連結（中文）](https://leetcode.cn/problems/trim-a-binary-search-tree/)
