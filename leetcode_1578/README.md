# LeetCode 1578 — Minimum Time to Make Rope Colorful

這個專案包含一個針對 LeetCode 題目 1578 的簡單 C# 範例實作，並附上解題說明、時間/空間複雜度分析以及範例步驟演示。

## 題目描述

Alice 有 n 個氣球排成一條繩子。給定一個 0-indexed 字串 `colors`，`colors[i]` 表示第 `i` 個氣球的顏色。
Alice 希望繩子是彩色的，不希望有兩個相鄰的氣球顏色相同，因此她請 Bob 幫忙。Bob 可以移除一些氣球使繩子變得彩色。
給定一個 0-indexed 整數陣列 `neededTime`，`neededTime[i]` 表示 Bob 移除第 `i` 個氣球所需的時間（秒）。請回傳 Bob 使繩子變彩色所需的最少時間。

## 解法概述（貪心）

觀察：對於每個連續同色段（例如 "aaa"），最終只能保留一個氣球，其餘都需要移除。要最小化總時間，應保留該段中移除耗時最大的那個氣球，移除其他較小耗時的氣球。

因此整體答案等於 `neededTime` 的總和，減去每個連續同色段的最大值之和。實作上可以一次掃描字串，同時累加耗時和追蹤當前段的最大耗時，當段結束時扣掉該段最大值。

時間複雜度：O(n)

空間複雜度：O(1)

## 範例與詳細步驟

範例 1
- colors = "aab"
- neededTime = [1, 2, 3]

步驟：
- 分段: "aa" (indices 0..1), "b" (index 2)
- 第 1 段總和 = 1 + 2 = 3，最大值 = 2，需移除 = 3 - 2 = 1
- 第 2 段只有一個氣球，不需移除
- 答案 = 1

範例 2
- colors = "abbba"
- neededTime = [1, 3, 2, 4, 1]

步驟：
- 分段: "a","bbb","a"
- 中間段 "bbb" 的總和 = 3 + 2 + 4 = 9，最大值 = 4，需移除 = 9 - 4 = 5
- 答案 = 5

## 專案內容

- `leetcode_1578/leetcode_1578/Program.cs`：包含題目實作 `MinCost` 方法與簡單的 `Main` 範例執行。
- `.vscode/launch.json`、`.vscode/tasks.json`：VS Code 偵錯與建置設定，按下「開始偵錯」即可自動 build 並執行，不需手動輸入參數。

## 建置與執行

確保你有安裝 .NET SDK（版本 8.0 或相容版本），在專案根目錄執行：

```bash
dotnet build leetcode_1578/leetcode_1578.csproj
dotnet run --project leetcode_1578/leetcode_1578.csproj
```

或在 VS Code 中直接按下「開始偵錯」。

## 小記

此解法適用於單一檔案/單一執行場景。如需擴充為函式庫或加入單元測試，可額外建立測試專案並將 `MinCost` 提取為公用類別方法。
