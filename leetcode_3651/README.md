# LeetCode 3651: Minimum Cost Path with Teleportations

[![LeetCode](https://img.shields.io/badge/LeetCode-3651-orange?style=flat-square)](https://leetcode.com/problems/minimum-cost-path-with-teleportations/)
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-14-239120?style=flat-square)](https://docs.microsoft.com/en-us/dotnet/csharp/)

使用動態規劃解決「帶傳送的最小成本路徑」問題的 C# 實作。

## 題目描述

給定一個 `m x n` 的整數陣列 `grid` 和一個整數 `k`。你從左上角格子 `(0, 0)` 出發，目標是抵達右下角格子 `(m - 1, n - 1)`。

### 移動規則

可用的移動類型有兩種：

| 移動類型 | 描述 | 花費 |
| --------- | ------ | ------ |
| **一般移動** | 從格子 `(i, j)` 向右或向下移動到 `(i, j + 1)` 或 `(i + 1, j)` | 目的格子的值 |
| **傳送** | 從任意格子 `(i, j)` 傳送到任何滿足 `grid[x][y] <= grid[i][j]` 的格子 `(x, y)` | 0（最多 `k` 次） |

**目標：** 回傳從 `(0, 0)` 到達 `(m - 1, n - 1)` 的最小總花費。

## 解題思路

### 核心概念：動態規劃

使用 `costs[t][i][j]` 表示恰好使用 `t` 次傳送，從 `(i,j)` 移動到 `(m−1,n−1)` 的最小移動總成本。

考慮從 `(i,j)` 首次移動的兩種情況：

#### 1. 不使用傳送（一般移動）

可以從 `(i,j)` 移動到 `(i+1,j)` 或 `(i,j+1)`，轉移方程為：

```text
costs[t][i][j] = min(costs[t][i+1][j] + grid[i+1][j], costs[t][i][j+1] + grid[i][j+1])
```

#### 2. 使用傳送

可以傳送到所有 `(x,y)` 且 `grid[x][y] ≤ grid[i][j]`，轉移方程為：

```text
costs[t][i][j] = min(costs[t−1][x][y])  其中 grid[x][y] ≤ grid[i][j]
```

### 優化技巧

1. **空間優化**：由於 `costs[t]` 只依賴 `costs[t−1]`，可以省略 `t` 這一維度，直接用二維陣列 `costs[i][j]`
2. **傳送計算優化**：使用 `points` 存放所有單元格座標，並按 `grid` 值升序排序。遍歷 `points`，用雙指標記錄值相同的區間 `[j,i]`，並維護已遍歷單元格在 `costs[t−1]` 的最小值 `minCost`

## 演算法流程圖解

以 `grid = [[3,1],[5,2]]`, `k = 2` 為例：

### 初始狀態

```text
grid:           座標:
┌───┬───┐      ┌───────┬───────┐
│ 3 │ 1 │      │ (0,0) │ (0,1) │
├───┼───┤      ├───────┼───────┤
│ 5 │ 2 │      │ (1,0) │ (1,1) │
└───┴───┘      └───────┴───────┘
```

### Step 1: 按 grid 值排序座標

```text
排序後的 points（按 grid 值升序）:
值: 1 → (0,1)
值: 2 → (1,1)  ← 終點
值: 3 → (0,0)  ← 起點
值: 5 → (1,0)
```

### Step 2: 迭代處理（t = 0, 1, 2）

**初始 costs（全為 ∞）：**

```text
costs:
┌───┬───┐
│ ∞ │ ∞ │
├───┼───┤
│ ∞ │ ∞ │
└───┴───┘
```

**t = 0（不使用傳送）- 傳送處理後：**

```text
costs（傳送後）:
┌───┬───┐
│ ∞ │ ∞ │
├───┼───┤
│ ∞ │ ∞ │
└───┴───┘
```

**t = 0 - 一般移動後（從右下往左上）：**

```text
costs:
┌───┬───┐
│ 4 │ 2 │  ← (0,0): min(5+grid[1][0], 2+grid[0][1]) = min(7, 3) = 3... 
├───┼───┤     實際: 從(0,1)→(1,1)需要grid[1][1]=2
│ 2 │ 0 │  ← 終點(1,1)=0, (1,0)=grid[1][1]=2
└───┴───┘
```

**t = 1（使用 1 次傳送）- 傳送處理後：**

```text
遍歷排序後的 points:
- 值=1, (0,1): minCost = min(∞, 2) = 2
- 值=2, (1,1): minCost = min(2, 0) = 0
- 值=3, (0,0): minCost = min(0, 4) = 0, 更新 costs[0,0] = 0
- 值=5, (1,0): minCost = min(0, 2) = 0, 更新 costs[1,0] = 0
```

**最終結果：** `costs[0,0] = 0`

> [!TIP]
> 透過傳送，可以從高值格子 `(0,0)` 直接傳送到低值格子，再利用一般移動到達終點，大幅降低成本。

## 複雜度分析

| 複雜度 | 分析 |
| ------- | ------ |
| **時間複雜度** | $O(k \cdot m \cdot n)$，其中 k 為傳送次數，m×n 為格子數量 |
| **空間複雜度** | $O(m \cdot n)$，用於儲存 costs 陣列和排序後的座標 |

## 快速開始

### 環境需求

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download) 或更高版本

### 建構與執行

```bash
# 複製專案
cd leetcode_3651

# 建構專案
dotnet build

# 執行測試
dotnet run
```

### 預期輸出

```text
測試 1: MinCost(grid1, 1) = 2
測試 2: MinCost(grid2, 2) = 0
測試 3: MinCost(grid3, 0) = 21
測試 4: MinCost(grid4, 1) = ...
```

## 專案結構

```text
leetcode_3651/
├── leetcode_3651.sln        # 解決方案檔
├── README.md                # 說明文件
└── leetcode_3651/
    ├── leetcode_3651.csproj # 專案檔
    └── Program.cs           # 主程式（包含解題程式碼）
```

## 相關連結

- [LeetCode 題目頁面（英文）](https://leetcode.com/problems/minimum-cost-path-with-teleportations/)
- [LeetCode 題目頁面（中文）](https://leetcode.cn/problems/minimum-cost-path-with-teleportations/)
