# LeetCode 3789｜滿足採購需求的最低成本

使用 C# 貪心法，在常數時間內算出滿足兩類需求的最低成本。專案以 .NET 10 主控台執行測資。

## 題目說明

給定三種物品的單價，以及兩類需求的最低數量。每種物品可購買任意非負整數件，目標是以最低總成本讓兩類需求都達標。

| 物品 | 單價 | 第 1 類貢獻 | 第 2 類貢獻 |
| --- | ---: | ---: | ---: |
| 第 1 類 | `cost1` | 1 | 0 |
| 第 2 類 | `cost2` | 0 | 1 |
| 第 3 類 | `costBoth` | 1 | 1 |

`need1` 與 `need2` 是達標門檻。若其中一類已達標，仍可購買第 3 類物品來滿足另一類，即使會讓已達標的一類超額。

### 限制條件

- `1 <= cost1, cost2, costBoth <= 10^6`
- `0 <= need1, need2 <= 10^9`
- 輸入為五個整數，回傳最低總成本。C# 使用 `long` 儲存結果。

### 官方範例

| 輸入 `(cost1, cost2, costBoth, need1, need2)` | 最低成本 | 購買方式 |
| --- | ---: | --- |
| `(3, 2, 1, 3, 2)` | 3 | 買 3 件第 3 類，兩類各得到 3 單位。 |
| `(5, 4, 15, 2, 3)` | 22 | 買 2 件第 1 類與 3 件第 2 類。 |
| `(5, 4, 15, 0, 0)` | 0 | 買 0 件。 |

[LeetCode 官方題目](https://leetcode.com/problems/minimum-cost-to-acquire-required-items/description/)

## 貪心解法 `MinimumCost`

### 從共同需求出發

令 `pairedCount = min(need1, need2)`。每一對共同需求需要兩類各 1 單位，可以各買 1 件單類物品，或買 1 件第 3 類物品。配對完成後，兩類最多只有一類還有需求，每個剩餘單位可在該類單品與第 3 類物品中選價格較低的。

程式先比較 `cost1 + cost2` 與 `costBoth`。若分開購買較便宜或同價，由於兩種單類物品的價格都為正，任一單類物品的單價也小於 `costBoth`。因此共同需求和剩餘需求都能用單類物品處理，直接計算 `cost1 * need1 + cost2 * need2`。

若 `costBoth` 較低，先買 `pairedCount` 件第 3 類物品並扣除兩邊的需求。剩下第 1 類就用 `min(cost1, costBoth)` 計價，剩下第 2 類就用 `min(cost2, costBoth)` 計價。兩邊都達標時，共同需求的成本就是答案。

### 為什麼能得到最低成本

假設買了 `x` 件第 3 類物品。固定 `x` 後，每類只需要用單類物品買到剛好補足缺口，因為再多買會增加成本。此時的最低成本為

```text
cost(x) = x * costBoth
        + max(need1 - x, 0) * cost1
        + max(need2 - x, 0) * cost2
```

- 當 `x < pairedCount`，多買 1 件第 3 類會同時少買兩種單類物品各 1 件，成本變化固定為 `costBoth - (cost1 + cost2)`。因此共同需求的每一對都可採用較便宜的方式。
- 當 `pairedCount <= x < max(need1, need2)`，再買 1 件第 3 類只會少買仍有缺口的單類物品 1 件，成本變化為 `costBoth - 該類單價`。此區間內比較的是這兩種物品的單價。
- 當 `x >= max(need1, need2)`，兩類都已達標，再買只會增加成本。

兩個區間的成本變化都固定。若第 3 類連一對單類物品都換得更貴，進入單邊區間後也不可能省錢。若替代一對單類物品可以省錢，就先買到配對上限，再由單邊價格決定要不要繼續買第 3 類。

### ASCII 流程圖

`n1`、`n2` 是當前需求，`c1`、`c2`、`cb` 是三種物品的單價。

```text
Start
  |
  v
c1 + c2 <= cb?
  +-- yes --> Return n1*c1 + n2*c2
  |
  +-- no ---> p = min(n1, n2)
              total = p*cb
              n1 = n1 - p, n2 = n2 - p
                |
                v
              n1 > 0?
                +-- yes --> Return total + n1*min(c1, cb)
                |
                +-- no ---> n2 > 0?
                             +-- yes --> Return total + n2*min(c2, cb)
                             |
                             +-- no ---> Return total
```

### 範例演示

**官方範例 1，共同購買後仍用第 3 類補足。** 輸入 `(3, 2, 1, 3, 2)`，因為 `3 + 2 > 1`，先買 `min(3, 2) = 2` 件第 3 類。剩餘第 1 類的單位成本為 `min(3, 1) = 1`，所以再買 1 件第 3 類，第 2 類會超額 1 單位。

```text
Remaining (3, 2) | cost = 0
  -> buy 2 x type 3 -> remaining (1, 0) | cost = 0 + 2*1 = 2
  -> buy 1 x type 3 -> remaining (0, 0) | cost = 2 + 1*1 = 3
  -> return 3        (received 3 units of each type)
```

**官方範例 2，分開購買。** 輸入 `(5, 4, 15, 2, 3)`，因為 `5 + 4 <= 15`，直接以各類單價乘上需求量。下圖把這個算式拆成兩筆採購。

```text
Remaining (2, 3) | cost = 0
  -> buy 2 x type 1 -> remaining (0, 3) | cost = 0 + 2*5 = 10
  -> buy 3 x type 2 -> remaining (0, 0) | cost = 10 + 3*4 = 22
  -> return 22
```

**第 2 類剩餘需求以單類物品補足。** 輸入 `(6, 3, 5, 1, 4)`，因為 `6 + 3 > 5`，先買 1 件第 3 類。配對後只剩第 2 類，單類物品的價格 3 低於第 3 類的價格 5。

```text
Remaining (1, 4) | cost = 0
  -> buy 1 x type 3 -> remaining (0, 3) | cost = 0 + 1*5 = 5
  -> buy 3 x type 2 -> remaining (0, 0) | cost = 5 + 3*3 = 14
  -> return 14
```

### 複雜度與數值範圍

- 時間複雜度為 `O(1)`，因為只做固定次數的比較與算術運算。
- 額外空間複雜度為 `O(1)`，因為只保存固定數量的變數。
- 單價乘上需求量可達 `10^6 * 10^9 = 10^15`，超過 `int` 的上限。程式把 `1L` 放在乘法最前面，讓第一步就用 `long` 計算。若乘完才轉型，先前的 `int` 乘法可能已經溢位。

## 建置與執行

安裝 .NET 10 SDK，從包含此 README 的目錄執行下列指令。

```sh
dotnet build leetcode_3789/leetcode_3789.csproj
dotnet run --project leetcode_3789/leetcode_3789.csproj --no-build
```

`Main` 會逐筆比較實際與預期成本，並輸出通過筆數。全部通過的結束碼為 0，任一案例失敗的結束碼為 1。

```text
[PASS] 官方範例 1：第 3 類超額供應 | 預期 3 | 實際 3
[PASS] 官方範例 2：分開購買 | 預期 22 | 實際 22
[PASS] 官方範例 3：兩邊需求皆為零 | 預期 0 | 實際 0
[PASS] 第 2 類剩餘需求用單類物品補足 | 預期 14 | 實際 14
[PASS] 第 1 類需求為零，第 3 類仍較便宜 | 預期 6 | 實際 6
[PASS] 共同需求的兩種買法同價 | 預期 12 | 實際 12
[PASS] 兩邊需求相等，配對後無剩餘 | 預期 12 | 實際 12
[PASS] 成本與需求達上限 | 預期 1000000000000000 | 實際 1000000000000000
通過 8/8 筆測試
```

## 專案結構

- [Program.cs](leetcode_3789/Program.cs) 包含原始題目註解、測資與貪心解法。
- [leetcode_3789.csproj](leetcode_3789/leetcode_3789.csproj) 定義以 `net10.0` 為目標的主控台專案。
- [readme-template.md](docs/readme-template.md) 是 README 的撰寫範本。
