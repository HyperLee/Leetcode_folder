# LeetCode 1971 - 尋找圖中是否存在路徑

[![LeetCode](https://img.shields.io/badge/LeetCode-1971-orange?style=flat-square&logo=leetcode)](https://leetcode.com/problems/find-if-path-exists-in-graph/)
[![Difficulty](https://img.shields.io/badge/Difficulty-Easy-green?style=flat-square)](https://leetcode.com/problems/find-if-path-exists-in-graph/)
[![.NET](https://img.shields.io/badge/.NET-10.0-512bd4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)

使用**深度優先搜索 (DFS)** 演算法解決 LeetCode 第 1971 題：判斷無向圖中兩點之間是否存在路徑。

## 題目描述

給定一個有 `n` 個頂點的**無向圖**，頂點編號從 `0` 到 `n - 1`。邊集合以二維陣列 `edges` 表示，每個 `edges[i] = [uᵢ, vᵢ]` 表示頂點 `uᵢ` 與 `vᵢ` 之間存在一條無向邊。

**限制條件：**
- 每對頂點之間最多一條邊
- 不存在自環（頂點連向自己）

**目標：** 判斷是否存在一條從 `source` 到 `destination` 的有效路徑。

### 範例

```
輸入: n = 6, edges = [[0,1],[1,2],[1,3],[3,4],[2,4],[4,5]], source = 0, destination = 5
輸出: true

圖形結構:
    0 --- 1 --- 2
          |     |
          3 --- 4 --- 5

存在路徑: 0 → 1 → 3 → 4 → 5
```

## 解題思路

### 核心概念：深度優先搜索 (DFS)

DFS 是一種圖遍歷演算法，其特點是沿著一條路徑**盡可能深入**探索，直到無法繼續前進時才回溯。

```
     從起點出發
         ↓
    ┌─────────────┐
    │ 當前 = 目標? │──是──→ 找到路徑！
    └─────────────┘
         │ 否
         ↓
    標記為已訪問
         ↓
    ┌─────────────┐
    │ 遍歷相鄰節點 │
    └─────────────┘
         ↓
    ┌─────────────┐
    │ 未訪問過？   │──否──→ 跳過
    └─────────────┘
         │ 是
         ↓
    遞迴搜索 (DFS)
```

### 演算法步驟

1. **建構鄰接表**：將邊集合轉換為鄰接表，方便快速查詢每個頂點的相鄰節點
2. **初始化訪問陣列**：建立 `visited` 陣列記錄每個節點的訪問狀態
3. **DFS 遞迴搜索**：
   - 若當前節點等於目標節點，返回 `true`
   - 標記當前節點為已訪問
   - 遍歷所有未訪問的相鄰節點，遞迴搜索
   - 若任一路徑可達目標，返回 `true`
   - 所有路徑都無法到達，返回 `false`

### 複雜度分析

| 複雜度 | 說明 |
|--------|------|
| **時間** | O(V + E)，V 為頂點數，E 為邊數 |
| **空間** | O(V + E)，用於鄰接表和訪問陣列 |

## 演示流程

以下使用具體範例說明 DFS 執行過程：

```
輸入: n = 4, edges = [[0,1],[1,2],[2,3]], source = 0, destination = 3

圖形: 0 --- 1 --- 2 --- 3

鄰接表:
  0: [1]
  1: [0, 2]
  2: [1, 3]
  3: [2]
```

### 執行追蹤

| 步驟 | 當前節點 | visited 狀態 | 動作 |
|:----:|:--------:|:------------:|------|
| 1 | 0 | `[T, F, F, F]` | 0 ≠ 3，標記已訪問，探索相鄰節點 [1] |
| 2 | 1 | `[T, T, F, F]` | 1 ≠ 3，標記已訪問，探索相鄰節點 [0, 2]，0 已訪問跳過 |
| 3 | 2 | `[T, T, T, F]` | 2 ≠ 3，標記已訪問，探索相鄰節點 [1, 3]，1 已訪問跳過 |
| 4 | 3 | `[T, T, T, T]` | **3 = 3，找到目標！返回 true** |

```
搜索路徑: 0 → 1 → 2 → 3 ✓
```

### 無路徑範例

```
輸入: n = 4, edges = [[0,1],[2,3]], source = 0, destination = 3

圖形: 0 --- 1    2 --- 3  (兩個不連通的子圖)

執行結果:
  從 0 出發 → 訪問 1 → 無其他相鄰節點 → 返回 false
```

## 快速開始

### 環境需求

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)

### 建構與執行

```bash
# 複製專案
git clone https://github.com/HyperLee/Leetcode_folder.git
cd Leetcode_folder/leetcode_1971

# 建構專案
dotnet build

# 執行測試
dotnet run --project leetcode_1971
```

### 預期輸出

```
測試 1: n=6, source=0, destination=5
結果: True (預期: True)

測試 2: n=4, source=0, destination=3
結果: False (預期: False)

測試 3: n=3, source=1, destination=1
結果: True (預期: True)

測試 4: n=1, source=0, destination=0
結果: True (預期: True)
```

## 程式碼結構

```
leetcode_1971/
├── leetcode_1971.sln          # Solution 檔案
├── leetcode_1971/
│   ├── leetcode_1971.csproj   # 專案檔案
│   └── Program.cs             # 主程式（包含解題邏輯）
└── README.md                  # 本文件
```

## 延伸學習

- **廣度優先搜索 (BFS)**：另一種圖遍歷方式，適合找最短路徑
- **Union-Find (並查集)**：高效判斷圖的連通性
- **圖的鄰接矩陣表示法**：另一種圖的資料結構

## 相關題目

| 題號 | 題目 | 難度 |
|:----:|------|:----:|
| 200 | [島嶼數量](https://leetcode.com/problems/number-of-islands/) | Medium |
| 547 | [省份數量](https://leetcode.com/problems/number-of-provinces/) | Medium |
| 797 | [所有可能的路徑](https://leetcode.com/problems/all-paths-from-source-to-target/) | Medium |
