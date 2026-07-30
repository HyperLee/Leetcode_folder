# LeetCode 621 — Task Scheduler

![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4)
![C%23](https://img.shields.io/badge/C%23-console-239120)

使用 C# 與固定大小頻率陣列，計算完成所有任務所需的最少單位時間。本專案包含可直接執行的範例驗證，適合用來理解最高頻任務如何決定冷卻排程的下界。

## 快速導覽

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法：最高頻率排程框架](#解法最高頻率排程框架)
- [範例演示流程](#範例演示流程)
- [建置與執行](#建置與執行)

## 題目說明

給定一組以大寫英文字母表示的任務 `tasks`，以及非負整數冷卻時間 `n`：

- 每個任務需要 1 個單位時間完成。
- 任務可以重新排列成任意執行順序。
- 兩個相同任務之間至少要有 `n` 個單位時間；這些時間可以執行其他任務，也可以閒置。

目標是求出完成所有任務所需的最少單位時間。

例如 `tasks = [A, A, A, B, B, B]`、`n = 2` 時，可安排為：

```text
A B idle A B idle A B
```

總共需要 `8` 個單位時間。

## 限制條件

- `1 <= tasks.Length <= 10^4`
- `tasks[i]` 為大寫英文字母 `A` 到 `Z`
- `0 <= n <= 100`

`LeastInterval` 依題目條件處理合法輸入，沒有額外加入題目範圍外的輸入驗證。

## 解題概念與出發點

如果直接模擬每一個時間點，就需要反覆選擇目前可執行的任務並管理冷卻狀態。這題只要求最短時間，不要求輸出實際排程，因此可以改從「答案至少有多大」出發。

有兩個不可忽略的下界：

1. **冷卻框架下界**：出現次數最多的任務必須被冷卻間隔隔開。
2. **任務總數下界**：每個任務至少占用一個單位時間，所以答案不可能小於 `tasks.Length`。

最後取這兩個下界的較大值，就能同時涵蓋需要插入閒置時間與其他任務足以填滿間隔的情況。

## 解法：最高頻率排程框架

目前專案提供一種 `O(N)` 的公式解法。

### 1. 統計每種任務的頻率

任務只可能是 `A` 到 `Z`，因此使用長度固定為 26 的 `int[] counts`：

```text
索引 0 代表 A，索引 1 代表 B，……，索引 25 代表 Z。
```

掃描 `tasks` 時，同步記錄所有任務中的最高頻率 `maxCount`。這樣不需要先完成計數後再從原始任務中尋找最大值。

### 2. 計算並列最高頻率的任務種類數

最高頻率可能不只屬於一種任務。以 `AAABBB` 為例：

- `A` 出現 3 次
- `B` 出現 3 次
- `maxCount = 3`
- `maxFrequencyTaskCount = 2`

最後一輪必須同時容納所有並列最高頻率的任務，因此不能只加上 `1`。

### 3. 建立冷卻排程框架

在最後一次執行最高頻任務以前，共有 `maxCount - 1` 輪。每輪包含一個最高頻任務位置，以及其後必須保留的 `n` 個間隔，所以每輪長度為 `n + 1`。

最後一輪不需要再等待冷卻，只需放入所有並列最高頻率的任務：

```text
scheduleFrameLength =
    (n + 1) * (maxCount - 1) + maxFrequencyTaskCount
```

### 4. 與任務總數比較

當任務種類很多時，其他任務會填滿原本預留的冷卻位置，甚至讓公式算出的框架短於任務本身。每個任務仍然都必須執行，因此答案為：

```text
Math.Max(scheduleFrameLength, tasks.Length)
```

### 正確性直覺

- 最高頻任務的前 `maxCount - 1` 次出現之後都必須留下至少 `n` 格，因此冷卻框架不可能再縮短。
- 最後一輪要容納每一種並列最高頻率的任務，所以必須加上 `maxFrequencyTaskCount`。
- 若其他任務超過框架中的空位，它們會延長排程；`tasks.Length` 正好提供這個下界。
- 其他任務若沒有超過空位，就能被填入框架而不增加長度。

因此兩個下界的最大值就是最少所需時間。

### 複雜度

- 時間複雜度：`O(N)`，其中 `N` 為任務數量；另一次掃描固定 26 格陣列視為常數時間。
- 額外空間複雜度：`O(1)`，因為頻率陣列大小固定為 26。

## 範例演示流程

### 範例一：冷卻框架主導

輸入：

```text
tasks = [A, A, A, B, B, B], n = 2
```

1. 頻率為 `A = 3`、`B = 3`。
2. `maxCount = 3`。
3. 並列最高頻率的任務有 `A`、`B`，所以 `maxFrequencyTaskCount = 2`。
4. 框架長度為 `(2 + 1) * (3 - 1) + 2 = 8`。
5. 任務總數為 `6`。
6. 答案為 `max(8, 6) = 8`。

逐輪觀察：

```text
第 1 輪：A B idle
第 2 輪：A B idle
最後一輪：A B
```

此時冷卻框架較長，必須保留兩個閒置時間。

### 範例二：任務總數主導

輸入：

```text
tasks = [A, B, C, D, E, A, B, C, D, E], n = 1
```

1. `A` 到 `E` 都出現 2 次。
2. `maxCount = 2`。
3. `maxFrequencyTaskCount = 5`。
4. 框架長度為 `(1 + 1) * (2 - 1) + 5 = 7`。
5. 任務總數為 `10`。
6. 答案為 `max(7, 10) = 10`。

可以直接安排：

```text
A B C D E A B C D E
```

每個相同任務之間都有足夠的其他任務，所以不需要閒置時間；此時任務總數成為真正的答案下界。

## 建置與執行

需要安裝支援 `net10.0` 的 .NET SDK。請從此目錄執行：

```bash
dotnet build leetcode_621/leetcode_621.csproj --nologo
dotnet run --project leetcode_621/leetcode_621.csproj --no-build
```

專案目前沒有獨立的自動化測試專案；`Main` 內的固定案例會比對預期值與實際值。任何案例失敗時，程式會設定非零結束碼。

實際執行輸出：

```text
案例 1：官方冷卻案例
Input: tasks = [A, A, A, B, B, B], n = 2
Expected: 8
Actual: 8
Result: PASS

案例 2：任務種類足以填滿間隔
Input: tasks = [A, B, C, D, E, A, B, C, D, E], n = 1
Expected: 10
Actual: 10
Result: PASS

案例 3：單一任務重複
Input: tasks = [A, A, A, A], n = 2
Expected: 10
Actual: 10
Result: PASS

案例 4：單一任務搭配大冷卻值
Input: tasks = [A], n = 100
Expected: 1
Actual: 1
Result: PASS

案例 5：無冷卻時間
Input: tasks = [A, A, B, B], n = 0
Expected: 4
Actual: 4
Result: PASS

案例 6：多個最高頻任務剛好填滿排程
Input: tasks = [A, A, A, B, B, B, C, C], n = 2
Expected: 8
Actual: 8
Result: PASS

總結：6/6 筆測試通過
```

## 專案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_621.sln
└── leetcode_621/
    ├── leetcode_621.csproj
    └── Program.cs
```
