# LeetCode 110 - 平衡二元樹 (Balanced Binary Tree)

C# 實作 LeetCode 110 題的兩種解法,包含詳細註解與測試案例。

## 題目描述

給定一個二元樹,判斷它是否為**高度平衡二元樹**。

**平衡二元樹的定義:**
- 一個二元樹每個節點的左右兩個子樹的高度差的絕對值不超過 1
- 左右子樹本身也必須是平衡二元樹

**題目來源:**
- [LeetCode 110 - English](https://leetcode.com/problems/balanced-binary-tree/description/?envType=daily-question&envId=2026-02-08)
- [LeetCode 110 - 中文](https://leetcode.cn/problems/balanced-binary-tree/description/?envType=daily-question&envId=2026-02-08)

### 範例

**範例 1:**
```
輸入: root = [3,9,20,null,null,15,7]
       3
      / \
     9  20
       /  \
      15   7
輸出: true
```

**範例 2:**
```
輸入: root = [1,2,2,3,3,null,null,4,4]
          1
         / \
        2   2
       / \
      3   3
     / \
    4   4
輸出: false
```

**範例 3:**
```
輸入: root = []
輸出: true
```

## 解題概念與出發點

### 核心概念

1. **高度 (Height)**: 從節點到葉子節點的最長路徑長度
2. **平衡條件**: 對於樹中的每個節點,左右子樹的高度差 ≤ 1
3. **遞迴性質**: 如果一棵樹是平衡的,那麼它的所有子樹也必須是平衡的

### 解題思路

判斷平衡二元樹主要有兩種思路:

1. **自頂向下 (Top-Down)**: 從根節點開始,檢查每個節點的左右子樹高度差,然後遞迴檢查子樹
2. **自底向上 (Bottom-Up)**: 從葉子節點開始,在計算高度的同時判斷是否平衡,發現不平衡立即返回

第二種方法更優,因為它避免了重複計算。

## 解法詳解

### 解法一:自頂向下的遞迴 (Top-Down Approach)

#### 演算法思路

1. 對於當前節點,計算左右子樹的高度
2. 檢查高度差是否 ≤ 1
3. 遞迴檢查左右子樹是否也平衡
4. 只有當三個條件都滿足時,才返回 `true`

#### 程式碼實現

```csharp
public bool IsBalanced(TreeNode root)
{
    // 基本情況:空樹視為平衡
    if(root is null)
    {
        return true;
    }
    else
    {
        // 三個條件必須同時滿足:
        // 1. 當前節點的左右子樹高度差 <= 1
        // 2. 左子樹本身是平衡的
        // 3. 右子樹本身是平衡的
        return Math.Abs(height(root.left) - height(root.right)) <= 1 
            && IsBalanced(root.left) 
            && IsBalanced(root.right);
    }
}

public int height(TreeNode root)
{
    // 空樹高度為 0
    if(root is null)
    {
        return 0;
    }
    else
    {
        // 遞迴計算左右子樹高度,取最大值後 +1(加上當前節點這一層)
        return Math.Max(height(root.left), height(root.right)) + 1;
    }
}
```

#### 複雜度分析

- **時間複雜度**: O(n²)
  - 對於每個節點 (n 個節點),都需要計算其高度
  - 計算高度需要遍歷子樹,最壞情況下需要 O(n) 時間
  - 特別是在完全二元樹的情況下,會有大量重複計算

- **空間複雜度**: O(n)
  - 遞迴呼叫堆疊的深度
  - 最壞情況下為樹的高度 n (鏈狀樹)

#### 優缺點

**優點:**
- 思路直觀,易於理解
- 程式碼簡潔

**缺點:**
- 存在大量重複計算
- 即使已經發現某個子樹不平衡,仍會繼續檢查其他節點
- 時間複雜度較高

### 解法二:自底向上的遞迴 (Bottom-Up Approach) ⭐ 推薦

#### 演算法思路

1. 採用**後序遍歷** (左 → 右 → 根) 的方式
2. 先遞迴處理左右子樹
3. 在計算高度的同時判斷是否平衡
4. 使用 `-1` 作為特殊標記表示樹不平衡
5. 一旦發現不平衡,立即向上傳遞 `-1`,終止後續計算

#### 程式碼實現

```csharp
public bool IsBalanced2(TreeNode root)
{
    // 如果 height2 返回 -1,表示樹不平衡
    // 否則返回的是樹的高度,表示樹平衡
    return height2(root) != -1;
}

public int height2(TreeNode root)
{
    // 基本情況:空子樹高度為 0(且空樹是平衡的)
    if(root is null)
    {
        return 0;
    }

    // 遞迴計算左子樹高度
    int leftH = height2(root.left);
    
    // 如果左子樹不平衡,提前終止,直接返回 -1
    if(leftH == -1)
    {
        return -1;
    }

    // 遞迴計算右子樹高度
    int rightH = height2(root.right);
    
    // 檢查兩個條件:
    // 1. 右子樹本身是否平衡
    // 2. 當前節點的左右子樹高度差是否 > 1
    if(rightH == -1 || Math.Abs(leftH - rightH) > 1)
    {
        return -1;
    }
    
    // 當前子樹平衡,返回其高度
    return Math.Max(leftH, rightH) + 1;
}
```

#### 複雜度分析

- **時間複雜度**: O(n)
  - 每個節點只訪問一次
  - 在一次遍歷中同時完成高度計算和平衡判斷
  - 提前終止機制避免了不必要的計算

- **空間複雜度**: O(n)
  - 遞迴呼叫堆疊的深度
  - 最壞情況下為樹的高度 n (鏈狀樹)

#### 優缺點

**優點:**
- **避免重複計算**: 每個節點的高度只計算一次
- **提前終止**: 一旦發現不平衡,立即返回,不繼續檢查
- **時間複雜度優**: O(n) 優於解法一的 O(n²)
- **一石二鳥**: 同時完成高度計算和平衡判斷

**缺點:**
- 相對解法一稍微複雜一些
- 需要理解 `-1` 特殊標記的含義

## 演算流程演示

### 範例:判斷以下樹是否平衡

```
       3
      / \
     9  20
       /  \
      15   7
```

#### 解法一執行流程

```
步驟 1: 檢查節點 3
  - 計算 height(9) = 1
  - 計算 height(20):
    - 計算 height(15) = 1
    - 計算 height(7) = 1
    - height(20) = max(1, 1) + 1 = 2
  - |1 - 2| = 1 ≤ 1 ✓
  - 檢查 IsBalanced(9) = true
  - 檢查 IsBalanced(20):
    - 重新計算 height(15) = 1  ← 重複計算
    - 重新計算 height(7) = 1   ← 重複計算
    - |1 - 1| = 0 ≤ 1 ✓
    - IsBalanced(15) = true
    - IsBalanced(7) = true
    - 返回 true
  - 返回 true

結果: true (但有重複計算)
```

#### 解法二執行流程

```
步驟 1: height2(3) 開始
  |
  ├─ height2(9)
  |    └─ 左右子樹都是 null
  |    └─ 返回 0 (空樹) + 1 = 1
  |
  └─ height2(20)
       |
       ├─ height2(15)
       |    └─ 左右子樹都是 null
       |    └─ 返回 1
       |
       └─ height2(7)
            └─ 左右子樹都是 null
            └─ 返回 1
       
       leftH = 1, rightH = 1
       |1 - 1| = 0 ≤ 1 ✓
       返回 max(1, 1) + 1 = 2

步驟 2: 回到 height2(3)
  leftH = 1, rightH = 2
  |1 - 2| = 1 ≤ 1 ✓
  返回 max(1, 2) + 1 = 3

步驟 3: IsBalanced2(root)
  height2(root) = 3 ≠ -1
  返回 true

結果: true (每個節點只計算一次)
```

### 不平衡樹的演示

```
       1
      / \
     2   2
    / \
   3   3
  / \
 4   4
```

#### 解法二執行流程 (提前終止)

```
步驟 1: height2(1) 開始
  |
  ├─ height2(左子樹 2)
  |    |
  |    ├─ height2(左孫 3)
  |    |    |
  |    |    ├─ height2(4) = 1
  |    |    └─ height2(4) = 1
  |    |    └─ 返回 2
  |    |
  |    ├─ height2(右孫 3) = 1
  |    |
  |    └─ leftH = 2, rightH = 1
  |         |2 - 1| = 1 ≤ 1 ✓
  |         返回 3
  |
  └─ height2(右子樹 2) = 1

步驟 2: 計算節點 1
  leftH = 3, rightH = 1
  |3 - 1| = 2 > 1 ✗
  返回 -1  ← 發現不平衡

步驟 3: IsBalanced2(root)
  height2(root) = -1
  返回 false

結果: false (一發現不平衡就終止)
```

## 執行專案

### 環境需求

- .NET 10.0 或更高版本
- VS Code 或 Visual Studio

### 建構與執行

```bash
# 建構專案
dotnet build

# 執行程式
dotnet run
```

### 預期輸出

```
測試案例 1: [3,9,20,null,null,15,7]
解法一結果: True
解法二結果: True

測試案例 2: [1,2,2,3,3,null,null,4,4]
解法一結果: False
解法二結果: False

測試案例 3: 空樹
解法一結果: True
解法二結果: True

測試案例 4: 單一節點 [1]
解法一結果: True
解法二結果: True
```

## 學習重點

### 關鍵概念

1. **遞迴樹的處理**: 理解如何用遞迴處理樹的問題
2. **後序遍歷**: 自底向上的處理方式
3. **優化策略**: 如何避免重複計算
4. **提前終止**: 發現不符合條件時立即返回

### 延伸思考

> **為什麼平衡二元樹很重要?**
>
> 平衡二元樹 (如 AVL 樹、紅黑樹) 能保證樹的高度為 O(log n),從而確保搜尋、插入、刪除操作的時間複雜度都是 O(log n)。如果樹不平衡 (最壞情況退化成鏈表),這些操作的時間複雜度會退化到 O(n)。

### 相關題目

- [LeetCode 104 - Maximum Depth of Binary Tree](https://leetcode.com/problems/maximum-depth-of-binary-tree/) (計算樹的高度)
- [LeetCode 111 - Minimum Depth of Binary Tree](https://leetcode.com/problems/minimum-depth-of-binary-tree/)
- [LeetCode 543 - Diameter of Binary Tree](https://leetcode.com/problems/diameter-of-binary-tree/)

## 技術細節

### C# 特性使用

- **Pattern Matching**: `if(root is null)` 簡潔的 null 檢查
- **XML Documentation**: 完整的 `<summary>` 文件註解
- **Math.Abs / Math.Max**: 使用內建數學函式
- **String Interpolation**: `$"解法一結果: {result}"` 格式化輸出

### 程式碼風格

- 遵循 C# 命名規範 (PascalCase for methods)
- 清晰的註解說明演算法邏輯
- 適當的空行分隔邏輯區塊
- 具描述性的變數名稱

## 兩種解法比較總結

| 特性 | 解法一 (Top-Down) | 解法二 (Bottom-Up) ⭐ |
|-----|------------------|---------------------|
| 時間複雜度 | O(n²) | **O(n)** |
| 空間複雜度 | O(n) | O(n) |
| 重複計算 | 有 | **無** |
| 提前終止 | 否 | **是** |
| 程式碼複雜度 | 簡單 | 中等 |
| 推薦程度 | ⭐⭐⭐ | **⭐⭐⭐⭐⭐** |

**建議**: 在實際應用中,**解法二**是更優的選擇,因為它有更好的時間複雜度和提前終止機制。

---

**作者**: HyperLee  
**日期**: 2026-02-08  
**LeetCode**: [110. Balanced Binary Tree](https://leetcode.com/problems/balanced-binary-tree/)
