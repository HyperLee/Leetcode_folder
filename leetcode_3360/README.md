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

## 解法三　固定門檻判斷 `CanAliceWinByRange`

沿用解法二的累計取石數 `S(k)`。每當初始石頭數 `n` 達到下一個回合的門檻，就能多完成一回合，勝負也會翻轉。第 8 回合累計需要 52 顆，超過題目上限，因此只需考慮前 7 回合。

| 已完成回合數 | 累計取石數 | 初始石頭數範圍 | Alice 獲勝 |
| ---: | ---: | --- | --- |
| 0 | 0 | 1–9 | `false` |
| 1 | 10 | 10–18 | `true` |
| 2 | 19 | 19–26 | `false` |
| 3 | 27 | 27–33 | `true` |
| 4 | 34 | 34–39 | `false` |
| 5 | 40 | 40–44 | `true` |
| 6 | 45 | 45–48 | `false` |
| 7 | 49 | 49–50 | `true` |

程式以 C# 關係模式判斷 `n` 是否落在四個奇數回合的區間之一。

### 範例演示

`n = 18` 落在 `10–18`，只能完成第 1 回合，Alice 獲勝。`n = 19` 剛好達到第 2 回合的門檻，落在 `19–26`，Alice 輸掉遊戲。

### 複雜度

本題共有四個勝利區間，只需固定次數的整數比較，時間與額外空間皆為 `O(1)`。如果取石規則或輸入上限改變，需要重新確認區間。

## 解法四　二分搜尋回合數 `CanAliceWinByBinarySearch`

累計取石數 `S(k) = k(21 − k) / 2` 隨回合數增加，因此對固定的 `n`，`S(k) ≤ n` 在可完成的回合範圍內成立，超出後就不成立。搜尋 `0…7` 中最後一個成立的回合數，就能得知勝負。

1. 將搜尋範圍設為 `0…7`。第 0 回合不必取石，一定可完成。
2. 取靠右的中點。若 `S(k) ≤ n`，把下界移到中點。若 `S(k) > n`，把上界移到中點減一。
3. 上下界相等時，得到最多可完成的回合數。回合數為奇數時 Alice 獲勝。

### 範例演示

當 `n = 45` 時，程式用 `middle = low + (high - low + 1) / 2` 計算中點。若累計所需石頭數 `S(k) ≤ 45`，第 `k` 回合就能完成，剩餘石頭剛好用完也算。

| 輪次 | 搜尋範圍 | 中點回合 | 累計取石數 | 判斷與更新 | 更新後的範圍 |
| ---: | --- | ---: | ---: | --- | --- |
| 1 | `[0, 7]` | 4 | 34 | 可完成，`low = 4` | `[4, 7]` |
| 2 | `[4, 7]` | 6 | 45 | 剛好可完成，`low = 6` | `[6, 7]` |
| 3 | `[6, 7]` | 7 | 49 | 無法完成，`high = 7 - 1` | `[6, 6]` |

上下界都等於 6，搜尋結束。最後完成取石的是 Bob，接著 Alice 無法取石，因此回傳 `false`。

### 中點公式為什麼要加 `1`？

C# 的整數除法會捨去小數。當只剩相鄰的兩個候選值 `[6, 7]`，加 `1` 會讓中點取右邊的 `7`。如果右邊可完成，`low = middle` 會提高下界。如果右邊無法完成，`high = middle - 1` 會降低上界。兩種情況都會縮小搜尋範圍。

| 中點公式 | `[6, 7]` 時的計算 | 中點 |
| --- | --- | ---: |
| 加 `1` | `6 + (7 - 6 + 1) / 2 = 6 + 2 / 2` | 7 |
| 不加 `1` | `6 + (7 - 6) / 2 = 6 + 1 / 2` | 6 |

拿掉 `+ 1` 並沿用原本的更新方式時，搜尋會在**第 4 輪**卡住。

| 輪次 | 搜尋範圍 | 中點回合 | 累計取石數 | 更新後的範圍 |
| ---: | --- | ---: | ---: | --- |
| 1 | `[0, 7]` | 3 | 27 | `[3, 7]` |
| 2 | `[3, 7]` | 5 | 40 | `[5, 7]` |
| 3 | `[5, 7]` | 6 | 45 | `[6, 7]` |
| 4 | `[6, 7]` | 6 | 45 | `[6, 7]` |

第 4 輪選到的中點已經是下界。因為 `S(6) = 45`，程式再次執行 `low = 6`，上下界都沒有移動。`low < high` 仍然成立，後續每一輪都會重複同樣的判斷。

### 複雜度

本題最多進行 3 次比較，時間與額外空間皆為 `O(1)`。若首回合取 `m` 顆，可在 `0…m` 之間搜尋，以 `S(k) = k(2m + 1 − k) / 2` 計算門檻，時間為 `O(log(m + 1))`，額外空間為 `O(1)`。

## 建置與執行範例

安裝 .NET 11 SDK，從本 README 所在目錄依序執行下列命令。

```powershell
dotnet build leetcode_3360/leetcode_3360.csproj
dotnet run --project leetcode_3360/leetcode_3360.csproj --no-build
```

建置結果為 0 個警告與 0 個錯誤。範例程式先以 17 個固定案例驗證四種解法的結果，再逐一比對 50 個合法輸入。兩項驗證皆通過時結束碼為 `0`，任一項失敗時為 `1`。

```text
n=1 Expected=False Simulation=False Formula=False Range=False BinarySearch=False PASS
n=9 Expected=False Simulation=False Formula=False Range=False BinarySearch=False PASS
n=10 Expected=True Simulation=True Formula=True Range=True BinarySearch=True PASS
n=12 Expected=True Simulation=True Formula=True Range=True BinarySearch=True PASS
n=18 Expected=True Simulation=True Formula=True Range=True BinarySearch=True PASS
n=19 Expected=False Simulation=False Formula=False Range=False BinarySearch=False PASS
n=26 Expected=False Simulation=False Formula=False Range=False BinarySearch=False PASS
n=27 Expected=True Simulation=True Formula=True Range=True BinarySearch=True PASS
n=33 Expected=True Simulation=True Formula=True Range=True BinarySearch=True PASS
n=34 Expected=False Simulation=False Formula=False Range=False BinarySearch=False PASS
n=39 Expected=False Simulation=False Formula=False Range=False BinarySearch=False PASS
n=40 Expected=True Simulation=True Formula=True Range=True BinarySearch=True PASS
n=44 Expected=True Simulation=True Formula=True Range=True BinarySearch=True PASS
n=45 Expected=False Simulation=False Formula=False Range=False BinarySearch=False PASS
n=48 Expected=False Simulation=False Formula=False Range=False BinarySearch=False PASS
n=49 Expected=True Simulation=True Formula=True Range=True BinarySearch=True PASS
n=50 Expected=True Simulation=True Formula=True Range=True BinarySearch=True PASS
Passed 17/17 cases.
All four methods agree for 50/50 valid inputs.
```

## 題目限制變更時的影響

若只放寬石頭數 `n` 的上限，首回合仍取 10 顆，之後每回合少取 1 顆，四種實作會在不同的輸入遇到原先的限制。以下以每回合至少取 1 顆為前提，因此最多能完成 10 回合，累計最多取 55 顆。

| 解法 | 現有實作在上限放寬後的表現 | 調整方式 |
| --- | --- | --- |
| 逐回合模擬 | `n = 51…54` 仍可正確執行。`n = 55` 時，完成第 10 回合後剩 0 顆，現有迴圈還會把取 0 顆算成一回合，接著開始扣除負數。 | 在迴圈條件加上 `stonesToRemove > 0`。 |
| 等差級數公式 | `n = 51…55` 仍能算出正確的回合數。`n = 56` 時，根號內的 `441 - 8 * n` 首次變成負數，`Math.Sqrt` 會得到 `NaN`。 | 算式中的石頭數使用 `Math.Min(n, 55)`，因為 55 顆已足夠完成全部 10 回合。 |
| 固定門檻判斷 | `n = 51` 就會答錯。第 8 回合累計需要 52 顆，51 顆仍是 7 回合，Alice 應該獲勝。現有最後一段勝利區間只到 50。 | 依新的輸入範圍重新列出勝利區間。 |
| 二分搜尋回合數 | `n = 51` 仍可正確執行。`n = 52` 已可完成第 8 回合，但 `high = 7` 使程式最多只能找到第 7 回合。 | 將搜尋範圍改為 `0…10`，就能涵蓋所有可完成的回合。 |

若首回合的取石數也改成 `m`，二分搜尋可改用範圍 `0…m`，並用 `S(k) = k(2m + 1 − k) / 2` 計算累計門檻。固定門檻判斷則要依新規則重新計算勝利區間。

## 解法比較

表中的 `k` 是實際完成的回合數，`m` 是一般化時首回合需要取的石頭數。原題的回合數有固定上限，四種做法在原題中皆為常數時間。

| 解法 | 核心做法 | 時間 | 額外空間 | 適用情境 |
| --- | --- | --- | --- | --- |
| 逐回合模擬 | 依序扣除每回合需要的石頭 | `O(k + 1)`，本題迴圈最多執行 7 次 | `O(1)` | 想直接依遊戲規則推演，或觀察每回合的剩餘石頭 |
| 等差級數公式 | 解二次方程，將較小根向下取整 | `O(1)` | `O(1)` | 累計門檻可由公式反解，想練習將取石規則轉成數學式 |
| 固定門檻判斷 | 直接比對四個 Alice 獲勝的區間 | `O(1)` | `O(1)` | 規則與輸入範圍固定，門檻可事先列出 |
| 二分搜尋回合數 | 找出最後一個符合 `S(k) ≤ n` 的回合 | 本題 `O(1)`，一般化為 `O(log(m + 1))` | `O(1)` | 累計門檻單調且容易計算，想用整數搜尋最大可行回合數 |
