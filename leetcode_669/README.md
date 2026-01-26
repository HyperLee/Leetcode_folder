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

這道題目提供了兩種解法：**遞迴法**和**迭代法**。兩者都充分利用了二叉搜尋樹（BST）的特性。

### 核心觀念：利用 BST 特性

二叉搜尋樹（Binary Search Tree）具有一個重要特性：

> **左子樹所有節點值 < 根節點值 < 右子樹所有節點值**

這個特性讓我們可以在修剪時做出明智的決策，而不需要遍歷整棵樹。

---

## 解法一：遞迴法

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

## 複雜度分析（遞迴法）

| 項目 | 複雜度 | 說明 |
|------|--------|------|
| 時間複雜度 | O(n) | 最壞情況需遍歷所有節點 |
| 空間複雜度 | O(h) | h 為樹的高度，遞迴呼叫堆疊深度 |

> [!NOTE]
> 對於平衡的 BST，h = O(log n)；對於退化成鏈表的 BST，h = O(n)

---

## 解法二：迭代法

### 迭代策略與出發點

遞迴法雖然簡潔，但會使用 O(h) 的堆疊空間。對於深度較大的樹，可能會導致堆疊溢位。迭代法則可以避免這個問題，只使用常數空間。

**核心思想：**

如果一個節點符合要求（值在 [low, high] 範圍內），那麼它的左右子樹應該如何修剪？

迭代法將修剪過程分為三個獨立階段：

1. **找到符合條件的根節點**
2. **修剪左子樹**
3. **修剪右子樹**

### 詳細解法說明

#### 階段 1：調整根節點

首先，我們需要確保根節點符合條件：

```csharp
while (root is not null && (root.val < low || root.val > high))
{
    if (root.val < low)
        root = root.right;  // 值太小，往右找
    else
        root = root.left;   // 值太大，往左找
}
```

- 如果根節點值 < low，根據 BST 特性，右子樹可能有符合條件的節點
- 如果根節點值 > high，根據 BST 特性，左子樹可能有符合條件的節點

#### 階段 2：修剪左子樹

從根節點開始，向左遍歷並修剪：

```csharp
for (TreeNode node = root; node.left is not null;)
{
    if (node.left.val < low)
        node.left = node.left.right;  // 用右子樹替換
    else
        node = node.left;             // 繼續向左
}
```

**關鍵邏輯：**

- 若左子節點值 < low：
  - 該左子節點及其**左子樹**都 < low（BST 特性）
  - 但其**右子樹**可能有符合條件的節點
  - 因此用 `node.left.right` 替換 `node.left`
  - **不移動 node**，因為需要重新檢查新的左子節點

- 若左子節點值 ≥ low：
  - 該左子節點符合條件，保留
  - 繼續向左遍歷（`node = node.left`）

#### 階段 3：修剪右子樹

從根節點開始，向右遍歷並修剪：

```csharp
for (TreeNode node = root; node.right is not null;)
{
    if (node.right.val > high)
        node.right = node.right.left;  // 用左子樹替換
    else
        node = node.right;             // 繼續向右
}
```

**關鍵邏輯：**

- 若右子節點值 > high：
  - 該右子節點及其**右子樹**都 > high（BST 特性）
  - 但其**左子樹**可能有符合條件的節點
  - 因此用 `node.right.left` 替換 `node.right`
  - **不移動 node**，因為需要重新檢查新的右子節點

- 若右子節點值 ≤ high：
  - 該右子節點符合條件，保留
  - 繼續向右遍歷（`node = node.right`）

### 迭代法流程演示

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

階段 1: 調整根節點
  - 檢查根節點 3：3 在 [1,3] 範圍內 ✓
  - 根節點符合條件，無需調整

當前狀態:
      3
     / \
    0   4
     \
      2
     /
    1

階段 2: 修剪左子樹
  步驟 2.1:
    - node = 3，檢查 node.left = 0
    - 0 < low(1) ✗
    - 將 node.left 替換為 node.left.right (節點 2)
    - node 仍為 3（不移動，重新檢查新的左子節點）

  當前狀態:
      3
     / \
    2   4
   /
  1

  步驟 2.2:
    - node = 3，檢查 node.left = 2
    - 2 在 [1,3] 範圍內 ✓
    - node = node.left（移動到節點 2）

  步驟 2.3:
    - node = 2，檢查 node.left = 1
    - 1 在 [1,3] 範圍內 ✓
    - node = node.left（移動到節點 1）

  步驟 2.4:
    - node = 1，node.left = null
    - 左子樹修剪完成

當前狀態:
      3
     / \
    2   4
   /
  1

階段 3: 修剪右子樹
  步驟 3.1:
    - node = 3，檢查 node.right = 4
    - 4 > high(3) ✗
    - 將 node.right 替換為 node.right.left (null)
    - node 仍為 3

  步驟 3.2:
    - node = 3，node.right = null
    - 右子樹修剪完成

最終結果:
      3
     /
    2
   /
  1
```

### 為什麼不移動 node 當子節點不符合條件？

這是迭代法的關鍵技巧：

```csharp
if (node.left.val < low)
    node.left = node.left.right;  // ← 替換後不移動 node
```

**原因：** 替換後的新左子節點（原本的 `node.left.right`）可能仍然不符合條件，需要再次檢查。

**重要觀念釐清：**

❌ **錯誤理解：** 每次替換後都從 root 重新開始往下找  
✅ **正確理解：** 替換後停留在**當前父節點**，檢查新的子節點

```
迴圈運作方式：

第 1 次迴圈: node 在位置 A
  → 檢查 node.left (舊的子節點)
  → 不符合條件，替換為 node.left.right
  → node 仍在位置 A (不移動)

第 2 次迴圈: node 仍在位置 A
  → 檢查 node.left (新的子節點)
  → 如果符合，才 node = node.left (向下移動)
  → 如果不符合，再次替換，node 仍在 A

只有子節點符合條件時，才會：
  → node = node.left (向下移動到子節點)
```

**詳細範例：**

```
假設 low = 5，觀察 node 的位置變化

初始狀態:
     10
    /
   3
    \
     4
      \
       6

第一次循環:
  - node = 10 (位置不變)
  - 檢查 node.left = 3
  - 3 < 5 ✗ → 替換 node.left = node.left.right (節點 4)
  - 重點：node 仍是 10，沒有移動！

     10  ← node 停留在這裡
    /
   4        ← 新的 node.left
    \
     6

第二次循環:
  - node = 10 (位置不變，仍在檢查同一個父節點)
  - 檢查 node.left = 4 (注意：這是新的左子節點)
  - 4 < 5 ✗ → 再次替換 node.left = node.left.right (節點 6)
  - 重點：node 還是 10，繼續停留！

     10  ← node 仍停留在這裡
    /
   6        ← 又是新的 node.left

第三次循環:
  - node = 10 (仍在原位)
  - 檢查 node.left = 6
  - 6 >= 5 ✓ → 符合條件！
  - 執行 node = node.left (向下移動)

      10
     /
    6  ← 現在 node 移動到這裡了

第四次循環:
  - node = 6
  - node.left = null → 迴圈結束

總結：
  → node 在位置 10 停留了 3 次循環
  → 每次檢查不同的 node.left (3 → 4 → 6)
  → 直到 node.left 符合條件，才向下移動
  → 並非每次都從 root 重新開始
```

### 複雜度分析（迭代法）

| 項目 | 複雜度 | 說明 |
|------|--------|------|
| 時間複雜度 | O(n) | 最壞情況需遍歷所有節點 |
| 空間複雜度 | O(1) | 只使用常數額外空間 |

---

## 兩種解法比較

| 比較項目 | 遞迴法 | 迭代法 |
|----------|--------|--------|
| **實作難度** | 簡單，直觀 | 中等，需理解三階段處理 |
| **程式碼長度** | 短（約 20 行） | 較長（約 40 行） |
| **時間複雜度** | O(n) | O(n) |
| **空間複雜度** | O(h)（遞迴堆疊） | O(1)（常數空間） |
| **最壞空間** | O(n)（退化樹） | O(1) |
| **可讀性** | 高，符合分治思想 | 中，需理解分階段邏輯 |
| **堆疊溢位風險** | 有（深度很大時） | 無 |
| **除錯難度** | 較難（遞迴追蹤） | 較易（線性流程） |

### 選擇建議

**使用遞迴法當：**
- 樹的深度不大（< 1000）
- 追求程式碼簡潔性
- 面試或競賽快速實作

**使用迭代法當：**
- 處理深度很大的樹
- 記憶體受限環境
- 需要避免堆疊溢位風險
- 追求極致空間效率

### 效能實測建議

對於 LeetCode 測試案例：
- 兩種方法在**執行時間**上差異不大
- 迭代法在**記憶體使用**上有優勢
- 遞迴法在**程式碼維護性**上更佳

> [!TIP]
> 實務中，除非明確遇到堆疊溢位問題，否則遞迴法的簡潔性通常更受青睞。但了解迭代法的實作原理，有助於深入理解演算法本質。

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
