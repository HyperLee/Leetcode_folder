# LeetCode 3100：換水問題 II（Water Bottles II）

本專案使用 .NET 10 console application 示範 LeetCode 3100「換水問題 II」的兩種解法，並透過 `Main` 中的固定案例比較預期結果與實際結果。

- 英文題目：[Water Bottles II](https://leetcode.com/problems/water-bottles-ii/description/)
- 中文題目：[換水問題 II](https://leetcode.cn/problems/water-bottles-ii/description/)
- Target framework：`net10.0`

## 題目說明

給定兩個整數 `numBottles` 與 `numExchange`：

- `numBottles` 是一開始擁有的滿水瓶數量。
- 喝掉滿水瓶後，該水瓶會變成空瓶。
- 可以用目前的 `numExchange` 個空瓶換一個滿水瓶。
- 每完成一次兌換，下一次所需的空瓶數會增加一。

同一個 `numExchange` 值只能兌換一瓶，不能在門檻增加前一次換取多瓶。例如 `numBottles = 3`、`numExchange = 1` 時，第一次只能用一個空瓶換一瓶水，不能一次把三個空瓶都用相同門檻換成三瓶水。

請計算依照以上規則最多能喝多少瓶水。

## 限制條件

依照[官方題目限制](https://leetcode.com/problems/water-bottles-ii/description/)：

- `1 <= numBottles <= 100`
- `1 <= numExchange <= 100`
- 題目保證輸入符合限制，因此兩種方法都不另外處理零或負數。

## 解題概念與出發點

最初的 `numBottles` 瓶水一定都可以喝掉，因此初始狀態同時是：

```text
已喝瓶數 = numBottles
空瓶數   = numBottles
```

假設目前交換門檻為 `exchange`，一次完整操作會：

1. 消耗 `exchange` 個空瓶換一瓶水。
2. 喝掉新水，使總飲用數增加一。
3. 新水瓶又變成一個空瓶。
4. 將下一次交換門檻增加一。

所以空瓶的「淨減少量」是 `exchange - 1`。這個狀態變化可以直接逐次模擬，也可以把多次交換的淨消耗寫成等差級數，再利用需求隨交換次數單調增加的特性進行二分搜尋。

## 解法一：逐次模擬交換

對應方法：`MaxBottlesDrunk`

### 設計說明

1. 先喝完所有初始滿瓶，將總飲用數與空瓶數都設為 `numBottles`。
2. 當空瓶數不少於目前的 `numExchange` 時，完成一次兌換。
3. 總飲用數增加一。
4. 扣除本次交換造成的空瓶淨消耗 `numExchange - 1`。
5. 將 `numExchange` 增加一，繼續判斷下一次兌換。
6. 空瓶不足時停止，回傳總飲用數。

這個方法直接呈現題目中的每次操作，狀態容易追蹤，也不需要額外資料結構。

### 範例演示

以 `numBottles = 10`、`numExchange = 3` 為例：

| 階段 | 交換前空瓶 | 本次門檻 | 交換並喝完後空瓶 | 累計喝水 |
| --- | ---: | ---: | ---: | ---: |
| 初始喝完 | 10 | 3 | 10 | 10 |
| 第 1 次兌換 | 10 | 3 | `10 - 3 + 1 = 8` | 11 |
| 第 2 次兌換 | 8 | 4 | `8 - 4 + 1 = 5` | 12 |
| 第 3 次兌換 | 5 | 5 | `5 - 5 + 1 = 1` | 13 |

此時下一次門檻為 6，但只剩一個空瓶，因此答案是 `13`。

### 複雜度

- 時間複雜度：`O(k)`，其中 `k` 是實際兌換次數。
- 額外空間複雜度：`O(1)`。

## 解法二：等差級數與整數二分搜尋

對應方法：`MaxBottlesDrunk2`

### 累積需求公式

設第一次交換門檻為 `e = numExchange`。各次交換完成後造成的空瓶淨消耗依序是：

```text
e - 1, e, e + 1, ..., e + k - 2
```

完成 `k` 次兌換後，這個等差級數的總淨消耗為：

```text
k * (2 * (e - 1) + k - 1) / 2
```

要真的完成第 `k` 次兌換，喝完最後換來的水後仍會留下該瓶的一個空瓶，因此當 `k > 0` 時，最少初始空瓶需求為：

```text
required(k) = k * (2 * (e - 1) + k - 1) / 2 + 1
```

`required(k)` 會隨 `k` 單調增加，所以所有可行的交換次數會形成從 0 開始的連續區間。程式在 `0..numBottles` 中二分搜尋最大可行的 `k`，最後回傳 `numBottles + k`。公式使用 `long` 計算，避免中間乘法溢位。

### 範例演示

同樣以 `numBottles = 10`、`numExchange = 3` 為例，二分搜尋過程如下：

| 搜尋範圍 | 候選 `k` | `required(k)` | 判斷 | 新範圍 |
| --- | ---: | ---: | --- | --- |
| `[0, 10]` | 5 | `5 * (4 + 4) / 2 + 1 = 21` | 需求大於 10 | `[0, 4]` |
| `[0, 4]` | 2 | `2 * (4 + 1) / 2 + 1 = 6` | 可行 | `[2, 4]` |
| `[2, 4]` | 3 | `3 * (4 + 2) / 2 + 1 = 10` | 可行 | `[3, 4]` |
| `[3, 4]` | 4 | `4 * (4 + 3) / 2 + 1 = 15` | 不可行 | `[3, 3]` |

最大可行兌換次數是 `k = 3`，因此最多可喝：

```text
numBottles + k = 10 + 3 = 13
```

### 複雜度

- 時間複雜度：`O(log numBottles)`。
- 額外空間複雜度：`O(1)`。

## 兩種解法比較

| 方法 | 核心概念 | 時間複雜度 | 額外空間 | 教學重點 |
| --- | --- | --- | --- | --- |
| `MaxBottlesDrunk` | 逐次更新空瓶、總飲用數與門檻 | `O(k)` | `O(1)` | 最貼近題目操作，容易追蹤每一步 |
| `MaxBottlesDrunk2` | 等差級數需求與整數二分搜尋 | `O(log numBottles)` | `O(1)` | 將重複操作轉成單調可行性問題 |

兩個方法都不改變呼叫端傳入的值，也不產生主控台輸出；所有展示與驗證輸出都集中在 `Main` 的 harness。

## Main 測試 harness

`Main` 使用 10 組固定案例，讓兩種解法各執行一次，共進行 20 項驗證：

| 案例 | `numBottles` | `numExchange` | 預期結果 | 覆蓋情境 |
| --- | ---: | ---: | ---: | --- |
| 官方範例一 | 13 | 6 | 15 | 官方一般案例 |
| 官方範例二 | 10 | 3 | 13 | 多次交換且門檻增加 |
| 最小值且立即兌換 | 1 | 1 | 2 | 兩個輸入都是最小值 |
| 交換門檻從一開始 | 3 | 1 | 5 | 同一門檻不能一次換多瓶 |
| 空瓶不足無法兌換 | 1 | 2 | 1 | 初始即無法交換 |
| 空瓶剛好足夠兌換 | 5 | 5 | 6 | 等號邊界可交換一次 |
| 一般情況 | 9 | 3 | 11 | 交換兩次後停止 |
| 較小交換門檻 | 10 | 2 | 13 | 初始門檻較低 |
| 最大瓶數與最小門檻 | 100 | 1 | 114 | `numBottles` 上限與最低門檻 |
| 最大瓶數與最大門檻 | 100 | 100 | 101 | 兩個輸入都是上限 |

每項驗證會顯示預期值、實際值與 `PASS`／`FAIL`。全部通過時程式回傳結束碼 `0`；任一失敗時回傳 `1`。

## 執行方式

請在本專案根目錄執行：

```bash
dotnet restore leetcode_3100/leetcode_3100.csproj
dotnet build leetcode_3100/leetcode_3100.csproj --nologo --no-restore
dotnet run --no-build --project leetcode_3100/leetcode_3100.csproj
```

本專案沒有獨立測試專案，因此以固定案例 harness 作為可重複執行的驗證。格式與差異檢查可使用：

```bash
dotnet format leetcode_3100/leetcode_3100.csproj --verify-no-changes --no-restore
git diff --check
```

## 範例執行結果

以下內容取自 `dotnet run --no-build --project leetcode_3100/leetcode_3100.csproj` 的實際輸出：

<!-- RUN-OUTPUT-START -->
```text
案例：官方範例一
numBottles = 13, numExchange = 6
預期 = 15
MaxBottlesDrunk    實際 = 15 => PASS
MaxBottlesDrunk2   實際 = 15 => PASS

案例：官方範例二
numBottles = 10, numExchange = 3
預期 = 13
MaxBottlesDrunk    實際 = 13 => PASS
MaxBottlesDrunk2   實際 = 13 => PASS

案例：最小值且立即兌換
numBottles = 1, numExchange = 1
預期 = 2
MaxBottlesDrunk    實際 = 2 => PASS
MaxBottlesDrunk2   實際 = 2 => PASS

案例：交換門檻從一開始
numBottles = 3, numExchange = 1
預期 = 5
MaxBottlesDrunk    實際 = 5 => PASS
MaxBottlesDrunk2   實際 = 5 => PASS

案例：空瓶不足無法兌換
numBottles = 1, numExchange = 2
預期 = 1
MaxBottlesDrunk    實際 = 1 => PASS
MaxBottlesDrunk2   實際 = 1 => PASS

案例：空瓶剛好足夠兌換
numBottles = 5, numExchange = 5
預期 = 6
MaxBottlesDrunk    實際 = 6 => PASS
MaxBottlesDrunk2   實際 = 6 => PASS

案例：一般情況
numBottles = 9, numExchange = 3
預期 = 11
MaxBottlesDrunk    實際 = 11 => PASS
MaxBottlesDrunk2   實際 = 11 => PASS

案例：較小交換門檻
numBottles = 10, numExchange = 2
預期 = 13
MaxBottlesDrunk    實際 = 13 => PASS
MaxBottlesDrunk2   實際 = 13 => PASS

案例：最大瓶數與最小門檻
numBottles = 100, numExchange = 1
預期 = 114
MaxBottlesDrunk    實際 = 114 => PASS
MaxBottlesDrunk2   實際 = 114 => PASS

案例：最大瓶數與最大門檻
numBottles = 100, numExchange = 100
預期 = 101
MaxBottlesDrunk    實際 = 101 => PASS
MaxBottlesDrunk2   實際 = 101 => PASS

總結：20/20 項驗證通過
```
<!-- RUN-OUTPUT-END -->

## 專案結構

```text
leetcode_3100/
├── leetcode_3100/
│   ├── Program.cs
│   └── leetcode_3100.csproj
├── leetcode_3100.sln
└── README.md
```
