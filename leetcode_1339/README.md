# LeetCode 1339: 分裂二叉樹的最大乘積

[![LeetCode](https://img.shields.io/badge/LeetCode-1339-FFA116?style=flat&logo=leetcode)](https://leetcode.com/problems/maximum-product-of-splitted-binary-tree/)
[![Difficulty](https://img.shields.io/badge/難度-Medium-orange)](https://leetcode.com/problems/maximum-product-of-splitted-binary-tree/)

## 題目描述

給定一個二叉樹的根節點 `root`，移除一條邊把二叉樹分成兩個子樹，使得兩個子樹節點值之和的乘積最大化。

返回兩個子樹節點和的最大乘積。由於答案可能很大，請將結果對 `10^9 + 7` 取餘。

> **注意**：應先最大化乘積再對 `10^9 + 7` 取餘，而不是在取餘後再最大化。

### 範例 1

**輸入：** `root = [1,2,3,4,5,6]`

```
       1
      / \
     2   3
    / \   \
   4   5   6
```

**註記：** 圖中「紅色標記」表示被移除的邊（此例為 `1-2`，把節點 `2` 與其子樹 `4,5` 分離）。

**輸出：** `110`

**說明：** 移除紅色的邊 `[1,2]`，樹會分裂成兩個子樹，節點值之和分別為 `11` 和 `10`。乘積為 `11 * 10 = 110`。

### 範例 2

**輸入：** `root = [1,null,2,3,4,null,null,5,6]`

```
    1
     \
      2
     / \
    3   4
   / \
  5   6
```

**輸出：** `90`

**說明：** 移除紅色的邊 `[2,3]`，樹會分裂成兩個子樹，節點值之和分別為 `15` 和 `6`。乘積為 `15 * 6 = 90`。

### 限制條件

- 樹中節點數量範圍：`[2, 5 * 10^4]`
- 每個節點值範圍：`[1, 10^4]`
- 保證至少有一條邊可以移除

## 解題思路

### 核心概念

這道題的關鍵在於理解：當我們移除一條邊後，二叉樹會被分成兩個子樹。假設：
- 整棵樹的節點值總和為 `sum`
- 其中一個子樹的節點值總和為 `subSum`
- 那麼另一個子樹的節點值總和就是 `sum - subSum`

我們要找到一個 `subSum`，使得 `subSum * (sum - subSum)` 最大。

### 數學優化

根據**均值不等式**原理，當兩個數之和固定時，這兩個數越接近，它們的乘積越大。因此：
- 當 `sum` 固定時
- `subSum` 越接近 `sum / 2`
- `subSum * (sum - subSum)` 的值就越大

這意味著我們不需要計算所有可能的乘積，只需要找到**最接近總和一半**的子樹和即可。

### 算法步驟

1. **第一次 DFS**：計算整棵樹的節點值總和 `sum`
2. **第二次 DFS**：
   - 計算每個節點為根的子樹的節點值總和 `cur`
   - 找出最接近 `sum / 2` 的子樹和，記錄為 `best`
3. **計算結果**：返回 `best * (sum - best) % (10^9 + 7)`

### 如何判斷「最接近 sum/2」

我們使用絕對值差來判斷：
- `|cur * 2 - sum|` 表示 `cur` 與 `sum / 2` 的距離（乘以 2 避免浮點運算）
- 如果 `|cur * 2 - sum| < |best * 2 - sum|`，說明 `cur` 更接近 `sum / 2`
- 更新 `best = cur`

### 為什麼需要兩次 DFS

- **第一次 DFS**：必須先知道總和 `sum`，才能在第二次遍歷時判斷每個子樹和是否接近 `sum / 2`
- **第二次 DFS**：通過後序遍歷（左右根）計算每個子樹的和，並找出最優解

## 複雜度分析

- **時間複雜度**：`O(n)`
  - 第一次 DFS 遍歷所有節點：`O(n)`
  - 第二次 DFS 遍歷所有節點：`O(n)`
  - 總計：`O(2n) = O(n)`

- **空間複雜度**：`O(h)`
  - `h` 為樹的高度
  - 遞迴調用堆疊的深度為樹的高度
  - 最壞情況（鏈狀樹）：`O(n)`
  - 平衡樹：`O(log n)`

## 演示範例

以範例 1 為例：`root = [1,2,3,4,5,6]`

```
       1
      / \
     2   3
    / \   \
   4   5   6
```

### 步驟 1：第一次 DFS 計算總和

遍歷順序：`1 → 2 → 4 → 5 → 3 → 6`

```
sum = 1 + 2 + 4 + 5 + 3 + 6 = 21
```

### 步驟 2：第二次 DFS 找最接近 sum/2 的子樹和

目標：找到最接近 `21 / 2 = 10.5` 的子樹和

遞迴計算每個子樹的和（後序遍歷）：

| 節點 | 子樹和計算 | 子樹和 | 距離 sum/2 | 更新 best? |
|------|------------|--------|-----------|------------|
| 4 | `0 + 0 + 4` | 4 | \|4×2-21\| = 13 | best = 4 |
| 5 | `0 + 0 + 5` | 5 | \|5×2-21\| = 11 | best = 5 |
| 2 | `4 + 5 + 2` | 11 | \|11×2-21\| = 1 | ✅ best = 11 |
| 6 | `0 + 0 + 6` | 6 | \|6×2-21\| = 9 | ❌ |
| 3 | `0 + 6 + 3` | 9 | \|9×2-21\| = 3 | ❌ |
| 1 | `11 + 9 + 1` | 21 | \|21×2-21\| = 21 | ❌ |

**最終 best = 11**（最接近 10.5）

### 步驟 3：計算最大乘積

```
子樹 1 的和：best = 11
子樹 2 的和：sum - best = 21 - 11 = 10
最大乘積：11 × 10 = 110
```

### 視覺化分割

當移除節點 1 和節點 2 之間的邊時：

```
分割前：              分割後：
       1                    1           2
      / \                    \         / \
     2   3        =>          3       4   5
    / \   \                    \
   4   5   6                    6

子樹 1 (根=1): 1+3+6 = 10
子樹 2 (根=2): 2+4+5 = 11
乘積: 10 × 11 = 110
```

## 實作細節

### 取模注意事項

由於題目要求對 `10^9 + 7` 取模，但必須**先計算最大乘積再取模**，因此：

```csharp
// ❌ 錯誤：先取模會導致較大的數取模後可能變小
return (best % MOD) * ((sum - best) % MOD) % MOD;

// ✅ 正確：使用 long 避免溢位，最後再取模
return (int)((long)best * (sum - best) % 1000000007);
```

### 程式碼結構

```csharp
private int sum = 0;   // 整棵樹的總和
private int best = 0;  // 最接近 sum/2 的子樹和

public int MaxProduct(TreeNode root)
{
    DFS(root);   // 第一次 DFS：計算總和
    DFS2(root);  // 第二次 DFS：找最接近 sum/2 的子樹和
    return (int)((long)best * (sum - best) % 1000000007);
}
```

## 執行方式

```bash
# 編譯專案
dotnet build

# 執行程式
dotnet run

# 執行測試（包含範例 1 和範例 2）
```

## 相關題目

- [LeetCode 124: Binary Tree Maximum Path Sum](https://leetcode.com/problems/binary-tree-maximum-path-sum/)
- [LeetCode 543: Diameter of Binary Tree](https://leetcode.com/problems/diameter-of-binary-tree/)
- [LeetCode 687: Longest Univalue Path](https://leetcode.com/problems/longest-univalue-path/)

## 參考資料

- [LeetCode 官方題目](https://leetcode.com/problems/maximum-product-of-splitted-binary-tree/)
- [LeetCode 中文題目](https://leetcode.cn/problems/maximum-product-of-splitted-binary-tree/)
