# LeetCode 865 - 具有所有最深節點的最小子樹

> **LeetCode 865. Smallest Subtree with all the Deepest Nodes**

本專案提供 LeetCode 第 865 題的 C# 解法實作。

## 題目描述

給定一個二叉樹的根節點 `root`，節點的深度定義為從根節點到該節點的最短距離。

請返回一個**最小的子樹**，使該子樹包含原樹中所有的最深節點。

- 若某節點在整棵樹中的深度為最大值，則稱該節點為**最深節點**
- 節點的子樹指由該節點及其所有子孫節點所構成的樹

### 範例

**範例 1:**

```
輸入: root = [3,5,1,6,2,0,8,null,null,7,4]
輸出: [2,7,4]
解釋:
        3
       / \
      5   1
     / \ / \
    6  2 0  8
      / \
     7   4

最深的節點為 7 和 4，深度都是 3
包含這兩個節點的最小子樹是以節點 2 為根的子樹
```

**範例 2:**

```
輸入: root = [1]
輸出: [1]
解釋: 根節點就是樹中唯一的節點，它也是最深的節點
```

**範例 3:**

```
輸入: root = [0,1,3,null,2]
輸出: [2]
解釋:
      0
     / \
    1   3
     \
      2

最深的節點是 2，深度為 2
包含節點 2 的最小子樹就是節點 2 本身
```

### 限制條件

- 樹中節點的數量在 `[1, 500]` 範圍內
- `0 <= Node.val <= 500`
- 每個節點的值都是唯一的

## 解題思路

### 核心概念

這道題的關鍵在於理解**最近公共祖先（LCA, Lowest Common Ancestor）**的概念：

1. **最深節點**：深度最大的所有葉節點
2. **最小子樹**：包含所有最深節點的最近公共祖先節點

### 解題出發點

- 使用**遞迴**的方式進行**深度優先搜尋（DFS）**
- 對於每個節點，我們需要知道兩個資訊：
  1. 該子樹的最大深度
  2. 包含所有最深節點的 LCA 節點

### 演算法邏輯

遞迴函式 `f(root)` 返回一個 `Tuple<TreeNode, int>`：
- `Item1`：包含所有最深節點的 LCA 節點
- `Item2`：當前子樹的最大深度

對於每個節點，分三種情況處理：

1. **左子樹更深**（`left.Item2 > right.Item2`）
   - 所有最深節點都在左子樹中
   - 返回左子樹的 LCA 和深度 + 1

2. **右子樹更深**（`left.Item2 < right.Item2`）
   - 所有最深節點都在右子樹中
   - 返回右子樹的 LCA 和深度 + 1

3. **深度相同**（`left.Item2 == right.Item2`）
   - 左右子樹都有最深節點
   - **當前節點就是 LCA**
   - 返回當前節點和深度 + 1

## 解法實作

```csharp
public TreeNode SubtreeWithAllDeepest(TreeNode root)
{
    return f(root).Item1;
}

private Tuple<TreeNode, int> dfs(TreeNode root)
{
    // 基礎情況：空節點的深度為 0
    if(root == null)
    {
        return new Tuple<TreeNode, int>(root, 0);
    }

    // 遞迴處理左右子樹
    Tuple<TreeNode, int> left = dfs(root.left);
    Tuple<TreeNode, int> right = dfs(root.right);

    // 情況 1：左子樹更深
    if(left.Item2 > right.Item2)
    {
        return new Tuple<TreeNode, int>(left.Item1, left.Item2 + 1);
    }

    // 情況 2：右子樹更深
    if(left.Item2 < right.Item2)
    {
        return new Tuple<TreeNode, int>(right.Item1, right.Item2 + 1);
    }

    // 情況 3：深度相同，當前節點為 LCA
    return new Tuple<TreeNode, int>(root, left.Item2 + 1);
}
```

### 複雜度分析

- **時間複雜度**：O(n)
  - 每個節點訪問一次，n 為樹中的節點總數
  
- **空間複雜度**：O(h)
  - 遞迴呼叫堆疊的深度，h 為樹的高度
  - 最壞情況（鏈狀樹）：O(n)
  - 最佳情況（平衡樹）：O(log n)

## 演示範例

讓我們用範例 1 詳細演示執行流程：

```
        3
       / \
      5   1
     / \ / \
    6  2 0  8
      / \
     7   4
```

### 執行流程

1. **處理葉節點**（深度 3）：
   - `f(7)` → `(7, 1)`
   - `f(4)` → `(4, 1)`
   - `f(6)` → `(6, 1)`
   - `f(0)` → `(0, 1)`
   - `f(8)` → `(8, 1)`

2. **處理節點 2**（深度 2）：
   - `left = f(7)` → `(7, 1)`
   - `right = f(4)` → `(4, 1)`
   - 深度相同 → 返回 `(2, 2)` ✓

3. **處理節點 5**（深度 1）：
   - `left = f(6)` → `(6, 1)`
   - `right = f(2)` → `(2, 2)`
   - 右子樹更深 → 返回 `(2, 3)`

4. **處理節點 1**（深度 1）：
   - `left = f(0)` → `(0, 1)`
   - `right = f(8)` → `(8, 1)`
   - 深度相同 → 返回 `(1, 2)`

5. **處理根節點 3**（深度 0）：
   - `left = f(5)` → `(2, 3)`
   - `right = f(1)` → `(1, 2)`
   - 左子樹更深 → 返回 `(2, 4)`

**最終結果**：節點 2

## C# Tuple 使用說明

### 什麼是 Tuple？

`Tuple` 是 C# 中的一種資料結構，用於**將多個值組合成一個物件**，而無需定義專門的類別。

### Tuple 的特點

- **不可變性**：一旦建立，Tuple 的值不能更改
- **輕量級**：適合臨時組合資料，不需要建立完整的類別
- **類型安全**：每個元素都有明確的類型

### 建立 Tuple

```csharp
// 方法 1：使用建構函式
var tuple1 = new Tuple<string, int>("Alice", 25);

// 方法 2：使用 Tuple.Create 方法（型別推斷）
var tuple2 = Tuple.Create("Bob", 30);

// 方法 3：C# 7.0+ 的值 Tuple（推薦）
var tuple3 = ("Charlie", 35);
var tuple4 = (Name: "David", Age: 40); // 具名元組
```

### 存取 Tuple 元素

```csharp
// 傳統 Tuple（使用 Item1, Item2...）
var person = new Tuple<string, int>("Alice", 25);
Console.WriteLine(person.Item1); // "Alice"
Console.WriteLine(person.Item2); // 25

// 值 Tuple（使用具名屬性或 Item）
var personValue = (Name: "Bob", Age: 30);
Console.WriteLine(personValue.Name); // "Bob"
Console.WriteLine(personValue.Age);  // 30
```

### 本題中的應用

在本題解法中，我們使用 `Tuple<TreeNode, int>` 來同時返回：
- **Item1**：TreeNode - 包含所有最深節點的 LCA 節點
- **Item2**：int - 當前子樹的深度

```csharp
// 建立並返回 Tuple
return new Tuple<TreeNode, int>(root, depth);

// 存取 Tuple 的值
Tuple<TreeNode, int> result = f(root);
TreeNode lcaNode = result.Item1;  // 取得 LCA 節點
int depth = result.Item2;          // 取得深度
```

### 現代寫法建議

如果使用 C# 7.0+，可以改用**值 Tuple**，程式碼會更簡潔：

```csharp
// 使用值 Tuple
private (TreeNode lca, int depth) dfs(TreeNode root)
{
    if(root == null)
    {
        return (null, 0);
    }

    var left = dfs(root.left);
    var right = dfs(root.right);

    if(left.depth > right.depth)
    {
        return (left.lca, left.depth + 1);
    }

    if(left.depth < right.depth)
    {
        return (right.lca, right.depth + 1);
    }

    return (root, left.depth + 1);
}
```

## 執行專案

### 建置專案

```bash
dotnet build
```

### 執行程式

```bash
dotnet run
```

### 預期輸出

```
測試案例 1 結果: 2
測試案例 2 結果: 1
測試案例 3 結果: 2
```

## 相關連結

- [LeetCode 865 題目（英文）](https://leetcode.com/problems/smallest-subtree-with-all-the-deepest-nodes/)
- [LeetCode 865 題目（中文）](https://leetcode.cn/problems/smallest-subtree-with-all-the-deepest-nodes/)

## 技術標籤

`二叉樹` `深度優先搜尋` `遞迴` `最近公共祖先` `Tuple`
