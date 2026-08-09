# LeetCode 3148：矩陣中的最大得分

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![Language](https://img.shields.io/badge/Language-C%23-239120)

這是一個以 .NET 10 Console App 撰寫的教學專案，示範如何利用「前綴最小值」解出 LeetCode 3148，並比較二維 DP 與一維滾動 DP 的空間取捨。程式入口內建五組固定案例，會同時驗證兩種解法並輸出 Expected、Actual 與 PASS/FAIL。

## 快速導覽

- [題目說明](#題目說明)
- [核心觀察與出發點](#核心觀察與出發點)
- [解法一：二維前綴最小值-dp](#解法一二維前綴最小值-dp)
- [解法二：一維滾動-dp](#解法二一維滾動-dp)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)

## 題目說明

給定一個由正整數組成的 `m x n` 矩陣 `grid`。可以從任意儲存格開始，每一步移動到目前位置正下方或正右方的任意儲存格，兩格不必相鄰。

若起點值為 `c1`、終點值為 `c2`，該次移動得分為 `c2 - c1`。必須至少移動一次，目標是求所有合法移動序列能得到的最大總分。

題目連結：[3148. Maximum Difference Score in a Grid](https://leetcode.com/problems/maximum-difference-score-in-a-grid/description/)

### 限制條件

- `m == grid.Length`
- `n == grid[i].Count`
- `2 <= m, n <= 1000`
- `4 <= m * n <= 100000`
- `1 <= grid[i][j] <= 100000`

這些限制表示 O(mn) 的完整掃描可行，但枚舉所有起點與終點的 O((mn)²) 方法不可行。

## 核心觀察與出發點

假設一條路徑經過的值依序為 `c1, c2, ..., ck`，總分為：

```text
(c2 - c1) + (c3 - c2) + ... + (ck - c(k-1))
= ck - c1
```

所有中間項都會互相消去。因此，不必保存完整路徑；把每個格子 `(i, j)` 視為終點時，只要知道其合法前驅區域中最小的值，就能計算：

```text
以 (i, j) 為終點的最佳分數
= grid[i][j] - 合法前驅區域的最小值
```

逐列、由左到右掃描時，所有合法前驅都可由「上方前綴」與「左方前綴」涵蓋。狀態轉移為：

```text
predecessorMinimum = min(上方的前綴最小值, 左方的前綴最小值)
currentMinimum = min(grid[i][j], predecessorMinimum)
```

左上角沒有前驅，不能把原地不動當作一次移動，因此只更新前綴最小值，不更新答案。這也確保全遞減矩陣仍會得到正確的負數答案，而不是錯誤的 `0`。

## 解法一：二維前綴最小值 DP

### 設計

`MaxScore` 建立 `(m + 1) x (n + 1)` 的 `prefixMinimum`，額外的第 0 列與第 0 欄填入 `int.MaxValue` 作為邊界。

對原矩陣的 `(i, j)`：

1. 從 `prefixMinimum[i][j + 1]` 取得上方狀態。
2. 從 `prefixMinimum[i + 1][j]` 取得左方狀態。
3. 取兩者較小值作為合法起點的最小值。
4. 若不是 `(0, 0)`，以 `grid[i][j] - predecessorMinimum` 更新答案。
5. 把目前值也納入前綴，寫入 `prefixMinimum[i + 1][j + 1]`。

此方法保留所有格子的 DP 狀態，容易直接觀察與除錯，也不會修改 `grid`。

### 範例演示

使用矩陣：

```text
[[1, 5],
 [2, 4]]
```

| 終點 | 目前值 | 上方最小值 | 左方最小值 | 合法前驅最小值 | 候選分數 | 寫入的前綴最小值 |
| --- | ---: | ---: | ---: | ---: | ---: | ---: |
| `(0, 0)` | 1 | ∞ | ∞ | ∞ | 不計算 | 1 |
| `(0, 1)` | 5 | ∞ | 1 | 1 | `5 - 1 = 4` | 1 |
| `(1, 0)` | 2 | 1 | ∞ | 1 | `2 - 1 = 1` | 1 |
| `(1, 1)` | 4 | 1 | 1 | 1 | `4 - 1 = 3` | 1 |

最大候選分數為 `4`，對應從值 `1` 向右移動到值 `5`。

### 複雜度

- 時間複雜度：O(mn)
- 空間複雜度：O(mn)

## 解法二：一維滾動 DP

### 設計

`MaxScore2` 觀察到計算目前格子時，只需要上一列同欄的狀態與目前列左側的狀態，因此將二維表壓縮為：

- `columnMinimum[j]`：更新前代表上方的前綴最小值，更新後代表目前格子的前綴最小值。
- `leftMinimum`：目前列左側格子的前綴最小值；每一列開始時重設為 `int.MaxValue`。

更新順序很重要：必須先讀取 `columnMinimum[j]` 的舊值，計算完成後才能覆寫，否則會遺失上一列狀態。此方法同樣只讀取 `grid`，不修改輸入。

### 範例演示

仍使用 `[[1, 5], [2, 4]]`，初始 `columnMinimum = [∞, ∞]`：

| 終點 | 更新前欄陣列 | 左方最小值 | 合法前驅最小值 | 候選分數 | 更新後欄陣列 |
| --- | --- | ---: | ---: | ---: | --- |
| `(0, 0)` | `[∞, ∞]` | ∞ | ∞ | 不計算 | `[1, ∞]` |
| `(0, 1)` | `[1, ∞]` | 1 | 1 | `5 - 1 = 4` | `[1, 1]` |
| `(1, 0)` | `[1, 1]` | ∞ | 1 | `2 - 1 = 1` | `[1, 1]` |
| `(1, 1)` | `[1, 1]` | 1 | 1 | `4 - 1 = 3` | `[1, 1]` |

得到與二維 DP 相同的最大分數 `4`，但只需要與欄數成正比的額外空間。

### 複雜度

- 時間複雜度：O(mn)
- 空間複雜度：O(n)

## 解法比較

| 方法 | 時間 | 額外空間 | 是否修改輸入 | 適合情境 |
| --- | --- | --- | --- | --- |
| `MaxScore` 二維 DP | O(mn) | O(mn) | 否 | 初次理解狀態、需要完整 DP 表除錯 |
| `MaxScore2` 一維滾動 DP | O(mn) | O(n) | 否 | 資料量較大、希望降低記憶體使用量 |

兩種方法的正確性來自同一個不變量：處理 `(i, j)` 前，上方與左方狀態已分別保存其涵蓋區域的最小值；兩者取最小值後，恰好涵蓋所有能到達 `(i, j)` 的合法起點。把目前值納入後，該不變量會繼續對下一個格子成立。

## 測試案例

`Main` 內建五組案例，每組會驗證兩個解法，共十項檢查：

1. 官方一般案例：驗證多步移動與非相鄰移動，預期 `9`。
2. 官方全遞減案例：驗證答案可能為負數，預期 `-1`。
3. 最小 `2 x 2` 遞增矩陣：驗證跨多步差值，預期 `3`。
4. 重複值矩陣：驗證最大分數可以是 `0`。
5. 最佳終點不是右下角：避免誤以為路徑必須走到右下角，預期 `9`。

任一檢查失敗時，程式會設定非零結束碼，方便命令列或 CI 判斷失敗。

## 建置與執行

需求：安裝 .NET 10 SDK。

從儲存庫根目錄執行：

```bash
dotnet restore leetcode_3148/leetcode_3148.csproj
dotnet build leetcode_3148/leetcode_3148.csproj --nologo
dotnet run --no-build --project leetcode_3148/leetcode_3148.csproj
```

格式驗證：

```bash
dotnet format leetcode_3148/leetcode_3148.csproj --verify-no-changes --no-restore
git diff --check
```

專案目前沒有獨立的自動化測試專案；`Main` 的固定測試入口就是可重複執行的驗收方式。

## 實際執行結果

以下內容來自 `dotnet run --no-build --project leetcode_3148/leetcode_3148.csproj` 的實際輸出：

<!-- RUN-OUTPUT-START -->
```text
Case: Official example 1
Input: [[9, 5, 7, 3], [8, 9, 6, 1], [6, 7, 14, 3], [2, 5, 3, 1]]
Expected: 9
  MaxScore: Actual = 9, Result = PASS
  MaxScore2: Actual = 9, Result = PASS

Case: Official example 2 - decreasing grid
Input: [[4, 3, 2], [3, 2, 1]]
Expected: -1
  MaxScore: Actual = -1, Result = PASS
  MaxScore2: Actual = -1, Result = PASS

Case: Minimum 2 x 2 increasing grid
Input: [[1, 2], [3, 4]]
Expected: 3
  MaxScore: Actual = 3, Result = PASS
  MaxScore2: Actual = 3, Result = PASS

Case: Duplicate values
Input: [[5, 5], [5, 5]]
Expected: 0
  MaxScore: Actual = 0, Result = PASS
  MaxScore2: Actual = 0, Result = PASS

Case: Best endpoint is not bottom-right
Input: [[1, 10, 2], [3, 4, 5]]
Expected: 9
  MaxScore: Actual = 9, Result = PASS
  MaxScore2: Actual = 9, Result = PASS

Summary: 10/10 checks passed
```
<!-- RUN-OUTPUT-END -->

## 專案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_3148/
│   ├── leetcode_3148.csproj
│   └── Program.cs
└── leetcode_3148.sln
```
