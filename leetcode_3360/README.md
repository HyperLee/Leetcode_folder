# LeetCode 3360. Stone Removal Game

## 題目說明

Alice 和 Bob 輪流從同一堆石頭中取石，Alice 先手，第一回合必須取走 10 顆。每個後續回合都必須比上一回合少取 1 顆。輪到的玩家如果沒有足夠的石頭完成該回合，就輸掉遊戲。

輸入是初始石頭數 `n`，限制為 `1 ≤ n ≤ 50`。Alice 獲勝時回傳 `true`，Bob 獲勝時回傳 `false`。[LeetCode 官方題目](https://leetcode.com/problems/stone-removal-game/description/)

| 官方範例 | 輸入 | 輸出 | 原因 |
| --- | ---: | --- | --- |
| 1 | 12 | `true` | Alice 取走 10 顆後剩 2 顆，Bob 無法取走 9 顆。 |
| 2 | 1 | `false` | Alice 開局就無法取走 10 顆。 |

## 解題出發點

每一回合需要的石頭數已由規則決定，依序為 `10, 9, 8, 7, …`。因此只要算出遊戲停下前一共**成功完成幾個回合**，就能判斷勝負。

- 完成奇數個回合時，最後成功取石的是 Alice。接著輪到 Bob，Bob 無法完成回合，所以回傳 `true`。
- 完成偶數個回合時，下一個無法取石的是 Alice，所以回傳 `false`。完成 0 個回合時也是如此。

## 解法一　逐回合模擬 `CanAliceWin`

將本回合需要的石頭數初始化為 10，已完成回合數初始化為 0。每回合開始時，比較剩餘石頭和該回合所需數量。

1. 剩餘石頭小於所需數量時停止，回合數維持原值。
2. 剩餘石頭大於或等於所需數量時，扣除該回合需要的石頭，將完成回合數加 1，並把下一回合所需數量減 1。
3. 停止後，以完成回合數的奇偶判斷 Alice 是否獲勝。

### 範例演示

| 初始石頭 | 回合 | 玩家 | 回合開始時剩餘 | 本回合需取 | 回合結果 | 已完成回合數 |
| ---: | ---: | --- | ---: | ---: | --- | ---: |
| 18 | 1 | Alice | 18 | 10 | 成功，剩 8 顆 | 1 |
| 18 | 2 | Bob | 8 | 9 | 石頭不足，Bob 輸 | 1 |
| 19 | 1 | Alice | 19 | 10 | 成功，剩 9 顆 | 1 |
| 19 | 2 | Bob | 9 | 9 | 成功，剩 0 顆 | 2 |
| 19 | 3 | Alice | 0 | 8 | 石頭不足，Alice 輸 | 2 |

`n = 18` 共完成 1 回合，回傳 `true`。`n = 19` 時，Bob 恰好能取完最後 9 顆，回合仍然成立。遊戲共完成 2 回合，回傳 `false`。

### 複雜度

每完成一個回合就執行一次迴圈，停止時再做一次條件判斷。若完成 `k` 個回合，耗時為 `O(k + 1)`，迴圈本體為 `O(k)`。題目範圍內最多完成 7 回合，因為完成第 7 回合累計需要 49 顆，第 8 回合則需要 52 顆。額外空間為 `O(1)`。

## 解法二　等差級數公式 `CanAliceWin2`

### 從回合數推回石頭數

第 `k` 回合需要取 `11 − k` 顆。以 `S(k)` 表示完成前 `k` 個回合需要的總數，令 `S(0) = 0`。當 `k ≥ 1` 時，可用「項數 × 首尾兩項平均」計算等差級數的和。

```text
S(k) = 10 + 9 + ⋯ + (11 − k)
     = k × (10 + 11 − k) / 2
     = k(21 − k) / 2
```

我們要找最大的整數 `k`，使 `S(k) ≤ n`。先用等式 `S(k) = n` 找界線。

```text
k(21 − k) / 2 = n
k² − 21k + 2n = 0

較小根 = (21 − √(441 − 8n)) / 2
較大根 = (21 + √(441 − 8n)) / 2
```

在 0 到 8 回合這一段，`S(k)` 隨著回合數增加，且 `S(8) = 52`。對每個合法的 `n`，水平線 `y = n` 都會與這一段的 `y = S(k)` 曲線相交，交點的回合數就是較小根。較大根落在曲線較後面的遞減段。

較小根介於 0 和 8 之間。將它向下取整，就得到滿足 `S(k) ≤ n` 的最大整數回合數。C# 將非負的浮點數轉成 `int` 時會截去小數部分，剛好完成這一步。最後同樣用回合數奇偶判斷勝負。

### 範例演示

| 初始石頭 | 代入根號 | 較小根 | 取整後的回合數 | 用累計取石數核對 | 結果 |
| ---: | --- | --- | ---: | --- | --- |
| 18 | `441 − 8 × 18 = 297` | `(21 − √297) / 2 ≈ 1.88` | 1 | `S(1) = 10 ≤ 18 < S(2) = 19` | 奇數，回傳 `true` |
| 19 | `441 − 8 × 19 = 289` | `(21 − 17) / 2 = 2` | 2 | `S(2) = 19 ≤ 19 < S(3) = 27` | 偶數，回傳 `false` |

`n = 19` 正好落在第 2 回合的門檻上，較小根恰為 2，所以第 2 回合計入結果。

### 複雜度

只需固定次數的算術、一次平方根與一次奇偶判斷，時間與額外空間皆為 `O(1)`。

## 建置與執行範例

安裝 .NET 11 SDK，從本 README 所在目錄依序執行下列命令。

```powershell
dotnet build leetcode_3360/leetcode_3360.csproj
dotnet run --project leetcode_3360/leetcode_3360.csproj --no-build
```

建置結果為 0 個警告與 0 個錯誤。範例程式會把兩種解法的回傳值各自與固定期望值比對。所有案例通過時，程式結束碼為 `0`，任一案例失敗時為 `1`。

```text
n=1 Expected=False Simulation=False Formula=False PASS
n=9 Expected=False Simulation=False Formula=False PASS
n=10 Expected=True Simulation=True Formula=True PASS
n=12 Expected=True Simulation=True Formula=True PASS
n=18 Expected=True Simulation=True Formula=True PASS
n=19 Expected=False Simulation=False Formula=False PASS
n=44 Expected=True Simulation=True Formula=True PASS
n=45 Expected=False Simulation=False Formula=False PASS
n=50 Expected=True Simulation=True Formula=True PASS
Passed 9/9 cases.
```
