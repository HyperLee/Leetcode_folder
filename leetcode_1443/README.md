# 🍎 LeetCode 1443 - 收集樹上所有蘋果的最少時間

[![C#](https://img.shields.io/badge/C%23-14-512BD4?style=flat-square&logo=csharp&logoColor=white)](https://docs.microsoft.com/dotnet/csharp/)
[![.NET](https://img.shields.io/badge/.NET-10-512BD4?style=flat-square&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![LeetCode](https://img.shields.io/badge/LeetCode-Medium-FFA116?style=flat-square&logo=leetcode&logoColor=white)](https://leetcode.com/problems/minimum-time-to-collect-all-apples-in-a-tree/)

## 📋 題目描述

給定一棵由 `n` 個節點（編號 `0` 到 `n-1`）組成的**無向樹**，部分節點上有蘋果。每走過一條邊需花費 **1 秒**。從節點 `0` 出發並最終回到節點 `0`，求收集樹上**所有蘋果**所需的**最短時間**（秒）。

### 輸入

- `n`：樹的節點數量
- `edges`：樹的邊，`edges[i] = [ai, bi]` 表示 `ai` 與 `bi` 之間存在一條邊
- `hasApple`：布林陣列，`hasApple[i] = true` 表示節點 `i` 有蘋果

### 輸出

收集所有蘋果所需的最少時間（秒）

## 💡 解題思路

### 核心觀察

1. **避免重複路徑**：收集蘋果時，同一條邊最多在兩個方向上各走一次
2. **最短路徑原則**：對於每個蘋果，應該走從根節點直接到達該蘋果的路徑
3. **邊數轉換時間**：將需要經過的邊數乘以 2 即為最少時間（去程 + 回程）

### 演算法步驟

```
1. 建立鄰接表 → 從邊列表轉換為圖結構
2. DFS 建立父節點關係 → 確定樹的結構
3. 回溯計算時間 → 從每個蘋果節點向根節點回溯
```

### 關鍵技巧

使用 `visited` 陣列避免重複計算已經走過的路徑：

- 初始時只有根節點 `0` 被標記為已訪問
- 對於每個有蘋果的節點，從該節點向根節點移動
- 遇到已訪問的節點時停止，避免重複計算

## 📊 複雜度分析

| 複雜度 | 數值 | 說明 |
|--------|------|------|
| 時間複雜度 | O(n) | 每個節點最多被訪問兩次（DFS + 回溯） |
| 空間複雜度 | O(n) | 鄰接表、父節點陣列、訪問標記 |

## 🎯 範例演示

### 範例 1

```
輸入：
n = 7
edges = [[0,1], [0,2], [1,4], [1,5], [2,3], [2,6]]
hasApple = [false, false, true, false, true, true, false]

樹結構：
        0 (無蘋果)
       / \
      1   2 (有蘋果)
     / \   \
    4   5   3
  (有) (有) (無)
```

**執行流程：**

1. **處理節點 2 的蘋果**（`hasApple[2] = true`）
   - 回溯路徑：`2 → 0`
   - 經過邊：`2-0`
   - 時間增加：2 秒
   - 標記節點 2 為已訪問

2. **處理節點 4 的蘋果**（`hasApple[4] = true`）
   - 回溯路徑：`4 → 1 → 0`
   - 經過邊：`4-1`, `1-0`
   - 時間增加：4 秒
   - 標記節點 4, 1 為已訪問

3. **處理節點 5 的蘋果**（`hasApple[5] = true`）
   - 回溯路徑：`5 → 1`（節點 1 已訪問，停止）
   - 經過邊：`5-1`
   - 時間增加：2 秒
   - 標記節點 5 為已訪問

**輸出：** `2 + 4 + 2 = 8` 秒

### 視覺化路徑

```
收集路徑（需要經過的邊，用 * 標記）：

        0
       /*\*
      1   2
     /*\
    4   5

經過的邊：0-1, 1-4, 1-5, 0-2
每條邊走 2 次（去 + 回）= 8 秒
```

## 🚀 快速開始

### 前置需求

- [.NET 10 SDK](https://dotnet.microsoft.com/download) 或更高版本

### 執行程式

```bash
# 複製專案
git clone https://github.com/HyperLee/Leetcode_folder.git

# 進入專案目錄
cd Leetcode_folder/leetcode_1443

# 建構並執行
dotnet run --project leetcode_1443/leetcode_1443.csproj
```

### 預期輸出

```
測試範例 1: 8
測試範例 2: 6
測試範例 3: 0
```

## 📁 專案結構

```
leetcode_1443/
├── leetcode_1443.sln          # 方案檔
├── README.md                  # 本文件
└── leetcode_1443/
    ├── leetcode_1443.csproj   # 專案檔
    └── Program.cs             # 主程式與解題實作
```

## 🔗 相關連結

- [LeetCode 題目連結（英文）](https://leetcode.com/problems/minimum-time-to-collect-all-apples-in-a-tree/)
- [LeetCode 題目連結（中文）](https://leetcode.cn/problems/minimum-time-to-collect-all-apples-in-a-tree/)

## 📚 相關知識

- **深度優先搜尋 (DFS)**：用於遍歷樹結構並建立父子關係
- **樹的遍歷**：無向圖轉換為有根樹的技巧
- **回溯法**：從葉節點向根節點回溯計算路徑
