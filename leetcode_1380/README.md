# LeetCode 1380 — 矩陣中的幸運數

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![C%23](https://img.shields.io/badge/C%23-console-239120)

這個專案使用 C# 與 .NET 10 實作 [LeetCode 1380：Lucky Numbers in a Matrix](https://leetcode.com/problems/lucky-numbers-in-a-matrix/)，並以可直接執行的 console harness 比較逐格驗證、預先計算列欄極值，以及 maximin/minimax 鞍點判斷三種解法。

## 目錄

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：逐格驗證](#解法一逐格驗證)
- [解法二：預先計算列欄極值](#解法二預先計算列欄極值)
- [解法三：極值鞍點法](#解法三極值鞍點法)
- [解法比較](#解法比較)
- [測試案例](#測試案例)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)

## 題目說明

給定一個 `m x n`、所有元素互異的整數矩陣。若某個元素同時符合下列條件，它就是「幸運數」：

1. 它是所在列的最小值。
2. 它是所在欄的最大值。

回傳矩陣中的所有幸運數，順序不限。

例如：

```text
matrix = [[3, 7, 8],
          [9, 11, 13],
          [15, 16, 17]]
```

第三列的最小值是 `15`，第一欄的最大值也是 `15`，因此答案為 `[15]`。

由於所有元素互異，一個矩陣至多只有一個幸運數；但題目不保證一定存在。測試案例 7 就涵蓋了沒有幸運數的情況。

## 限制條件

- `m == matrix.length`
- `n == matrix[i].length`
- `1 <= m, n <= 50`
- `1 <= matrix[i][j] <= 10^5`
- 矩陣中的所有元素互異。
- 三種公開解法只讀取 `matrix`，不會修改輸入。

限制保證矩陣至少包含一個元素，因此公開 API 依題目契約處理非空矩形矩陣，不額外定義空矩陣或不規則矩陣的行為。

## 解題概念與出發點

判斷位置 `(row, column)` 是否為幸運數，必須同時知道兩件事：

```text
matrix[row][column] == 該列最小值
matrix[row][column] == 該欄最大值
```

三種解法的差別，在於如何取得並組合這兩類資訊：

- `LuckyNumbers` 對每個候選元素現場掃描其列與欄。
- `LuckyNumbers2` 先保存所有列最小值和欄最大值，再回頭比對。
- `LuckyNumbers3` 不保存每一列與每一欄的極值，只比較兩個全域極值，利用鞍點性質判定答案。

## 解法一：逐格驗證

`LuckyNumbers` 最直接地翻譯題意：逐一枚舉矩陣中的每個元素，先確認它是否為所在列的最小值，再確認它是否為所在欄的最大值。

### 設計流程

1. 以 `(row, column)` 枚舉每一個候選元素。
2. 掃描同一列；只要找到更小的值，候選就不可能是幸運數。
3. 對已通過列檢查的候選掃描同一欄。
4. 若欄中也沒有更大的值，將候選加入結果。

列檢查失敗後立刻 `continue`，可以避免對明知不合格的元素繼續掃描欄。這不改變最壞時間複雜度，但能減少實際比較次數。

### 範例演示

考慮第一個官方範例中的 `15`：

```text
所在列：[15, 16, 17] -> 15 是最小值
所在欄：[3, 9, 15]   -> 15 是最大值
```

兩個條件都成立，因此加入答案。相對地，`16` 的同列存在更小的 `15`，列檢查便會立即淘汰它，不需要再掃描第二欄。

### 複雜度

- 時間複雜度：O(mn(m+n))。最多對 `mn` 個候選各掃描 `n` 個同列元素與 `m` 個同欄元素。
- 額外空間複雜度：O(1)，不計回傳結果。

## 解法二：預先計算列欄極值

`LuckyNumbers2` 先把重複查詢的資訊保存起來。第一次掃描同時計算：

- `rowMinimums[row]`：每一列的最小值。
- `columnMaximums[column]`：每一欄的最大值。

第二次掃描只需用 O(1) 時間查表，判斷目前元素是否同時等於對應的兩個極值。

### 設計流程

1. 將所有列最小值初始化為 `int.MaxValue`。
2. 將所有欄最大值初始化為 `int.MinValue`。
3. 掃描每個元素，同時更新它所屬列的最小值和所屬欄的最大值。
4. 再掃描一次矩陣，收集同時符合兩個極值的元素。

### 範例演示

對矩陣：

```text
[[1, 10, 4, 2],
 [9, 3, 8, 7],
 [15, 16, 17, 12]]
```

預先計算得到：

```text
每列最小值：[1, 3, 12]
每欄最大值：[15, 16, 17, 12]
```

只有位置 `(2, 3)` 的 `12` 同時等於 `rowMinimums[2]` 和 `columnMaximums[3]`，所以答案是 `[12]`。

### 複雜度

- 時間複雜度：O(mn)。兩次完整掃描仍屬於線性矩陣工作量。
- 額外空間複雜度：O(m+n)，用來保存列最小值與欄最大值。

## 解法三：極值鞍點法

`LuckyNumbers3` 使用矩陣鞍點的 maximin/minimax 性質：

```text
maximumOfRowMinimums = max(每一列的最小值)
minimumOfColumnMaximums = min(每一欄的最大值)
```

對任何矩陣都成立：

```text
maximumOfRowMinimums <= minimumOfColumnMaximums
```

若兩者相等，選出最大列最小值的那一列，以及最小欄最大值的那一欄；兩者交會位置不可能小於前者，也不可能大於後者，因此必定正好等於共同極值。它就是列最小、欄最大的幸運數。

反過來，如果矩陣存在幸運數，這兩個全域極值也必須被夾在該幸運數的同一個值上，所以兩者必定相等。因此：

```text
兩者相等 -> 回傳該值
兩者不相等 -> 回傳空集合
```

### 範例演示：存在幸運數

以官方範例一為例：

```text
每列最小值：[3, 9, 15]   -> 最大值為 15
每欄最大值：[15, 16, 17] -> 最小值為 15
```

兩個極值相等，因此 `15` 是幸運數。

### 範例演示：不存在幸運數

考慮測試案例：

```text
[[10, 20],
 [30, 5]]
```

計算結果為：

```text
每列最小值：[10, 5]  -> 最大值為 10
每欄最大值：[30, 20] -> 最小值為 20
```

`10 != 20`，表示矩陣沒有同時滿足列最小與欄最大的交會值，因此回傳 `[]`。

### 複雜度

- 時間複雜度：O(mn)。所有列與所有欄各掃描一次。
- 額外空間複雜度：O(1)，只保留目前極值，不建立列或欄陣列。

## 解法比較

| 解法 | 公開方法 | 時間複雜度 | 額外空間 | 教學重點 |
| --- | --- | --- | --- | --- |
| 逐格驗證 | `LuckyNumbers` | O(mn(m+n)) | O(1) | 直接翻譯題意，容易理解與驗證 |
| 預先計算列欄極值 | `LuckyNumbers2` | O(mn) | O(m+n) | 用額外空間消除重複掃描 |
| 極值鞍點法 | `LuckyNumbers3` | O(mn) | O(1) | 利用 maximin/minimax 等價條件壓縮狀態 |

若以實務可讀性為優先，第二種解法最直觀地兼顧線性時間與清楚資料流；第三種空間最省，但理解和維護前需要知道鞍點性質。第一種則保留最貼近原始題意的基準版本，適合逐步教學。

## 測試案例

`Main` 內建 8 組固定案例，每組分別檢查三種解法，共 24 項驗證：

| 案例 | 驗證重點 | 預期結果 |
| --- | --- | --- |
| 1 | 官方範例一 | `[15]` |
| 2 | 官方範例二 | `[12]` |
| 3 | 官方範例三 | `[7]` |
| 4 | 1×1 單一元素 | `[42]` |
| 5 | 單列矩陣 | `[1]` |
| 6 | 單欄矩陣 | `[9]` |
| 7 | 合法但沒有幸運數 | `[]` |
| 8 | 50×50 尺寸上界、數值 1 到 2500 | `[2451]` |

每種解法都取得獨立的矩陣副本。若任何 Actual 與 Expected 不同，該列會顯示 `FAIL`，程式也會設定非零結束碼，方便終端機或自動化流程辨識失敗。

## 建置與執行

從此 repository 根目錄執行：

```bash
dotnet restore leetcode_1380/leetcode_1380.csproj
dotnet build leetcode_1380/leetcode_1380.csproj --nologo
dotnet run --project leetcode_1380/leetcode_1380.csproj --no-build
```

專案目前沒有獨立的自動化測試專案；console harness 的 24 項 Expected/Actual 比對就是行為驗收入口。

## 實際執行結果

以下內容來自上述 `dotnet run` 命令的實際輸出：

```text
Case 1: 官方範例一
Matrix: [[3, 7, 8], [9, 11, 13], [15, 16, 17]]
Expected: [15]
LuckyNumbers Actual: [15] => PASS
LuckyNumbers2 Actual: [15] => PASS
LuckyNumbers3 Actual: [15] => PASS

Case 2: 官方範例二
Matrix: [[1, 10, 4, 2], [9, 3, 8, 7], [15, 16, 17, 12]]
Expected: [12]
LuckyNumbers Actual: [12] => PASS
LuckyNumbers2 Actual: [12] => PASS
LuckyNumbers3 Actual: [12] => PASS

Case 3: 官方範例三
Matrix: [[7, 8], [1, 2]]
Expected: [7]
LuckyNumbers Actual: [7] => PASS
LuckyNumbers2 Actual: [7] => PASS
LuckyNumbers3 Actual: [7] => PASS

Case 4: 單一元素
Matrix: [[42]]
Expected: [42]
LuckyNumbers Actual: [42] => PASS
LuckyNumbers2 Actual: [42] => PASS
LuckyNumbers3 Actual: [42] => PASS

Case 5: 單列矩陣
Matrix: [[9, 1, 5]]
Expected: [1]
LuckyNumbers Actual: [1] => PASS
LuckyNumbers2 Actual: [1] => PASS
LuckyNumbers3 Actual: [1] => PASS

Case 6: 單欄矩陣
Matrix: [[3], [9], [1]]
Expected: [9]
LuckyNumbers Actual: [9] => PASS
LuckyNumbers2 Actual: [9] => PASS
LuckyNumbers3 Actual: [9] => PASS

Case 7: 沒有幸運數的矩陣
Matrix: [[10, 20], [30, 5]]
Expected: []
LuckyNumbers Actual: [] => PASS
LuckyNumbers2 Actual: [] => PASS
LuckyNumbers3 Actual: [] => PASS

Case 8: 50 x 50 上界矩陣（數值 1 到 2500）
Matrix: 50 x 50 generated matrix
Expected: [2451]
LuckyNumbers Actual: [2451] => PASS
LuckyNumbers2 Actual: [2451] => PASS
LuckyNumbers3 Actual: [2451] => PASS

Summary: 24/24 checks passed.
```
