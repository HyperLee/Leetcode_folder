# LeetCode 2037：使每位學生都有座位的最少移動次數

[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/download/dotnet/10.0)

本專案是 LeetCode 2037 的 .NET 10 console 教學範例。程式保留排序貪婪解法，並加入利用位置範圍限制的計數雙指標解法；`Main` 內建六組固定案例，可直接比較兩種解法的答案。

## 目錄

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：排序後依序配對](#解法一排序後依序配對)
- [解法二：計數與雙指標](#解法二計數與雙指標)
- [兩種解法比較](#兩種解法比較)
- [可執行測試資料](#可執行測試資料)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)
- [專案結構](#專案結構)

## 題目說明

官方題目：

- [LeetCode 英文題目](https://leetcode.com/problems/minimum-number-of-moves-to-seat-everyone/description/)
- [LeetCode 中文題目](https://leetcode.cn/problems/minimum-number-of-moves-to-seat-everyone/description/)

房間內有 `n` 個座位與 `n` 位學生：

- `seats[i]` 表示第 `i` 個座位的位置。
- `students[j]` 表示第 `j` 位學生目前的位置。
- 一次移動可以讓一位學生的位置增加或減少 `1`。

目標是讓每位學生各自坐到一個座位，且不能有兩位學生使用同一個座位。請回傳完成安排所需的最少移動次數。

初始狀態可能有多個座位位於同一位置，也可能有多位學生站在同一位置。這些重複值代表不同的座位或學生，配對時仍須逐一計算。

例如：

```text
seats   = [3, 1, 5]
students = [2, 7, 4]
```

可以把位置 `2` 的學生移到 `1`、位置 `4` 的學生移到 `3`、位置 `7` 的學生移到 `5`，總移動次數為 `1 + 1 + 2 = 4`。

## 限制條件

| 條件 | 官方範圍 |
| --- | --- |
| 陣列長度 | `n == seats.Length == students.Length` |
| 座位與學生數量 | `1 <= n <= 100` |
| 座位位置 | `1 <= seats[i] <= 100` |
| 學生位置 | `1 <= students[j] <= 100` |

公開方法依照 LeetCode 呼叫契約處理合法輸入，不額外定義空陣列、長度不同或位置超出範圍時的行為。

## 解題概念與出發點

### 1. 移動成本就是位置距離

學生從位置 `x` 移到座位位置 `y`，每次只能加一或減一，因此成本必定是：

```text
|x - y|
```

整體問題就是：如何在所有一對一配對方式中，讓絕對距離總和最小。

### 2. 最佳配對不需要交叉

假設兩位學生的位置為 `a <= b`，兩個座位的位置為 `x <= y`。依照順序配對的成本是：

```text
|a - x| + |b - y|
```

交叉配對的成本則是：

```text
|a - y| + |b - x|
```

在一維數線上，把較小的學生位置配給較小的座位位置，不會比交叉配對更差。若某個候選答案存在交叉配對，可以交換那兩個配對而不增加總成本；持續消除交叉後，就得到「由小到大依序配對」的最佳解。

### 3. 重複位置仍是獨立項目

`seats = [2,2,6,6]` 表示位置 `2` 與 `6` 各有兩個不同座位。排序法會保留四個陣列元素；計數法則把它們記為 `count[2] = 2`、`count[6] = 2`，兩者都不會遺失重複資料。

### 4. 測試資料必須隔離

第一種解法會原地排序 `seats` 與 `students`，第二種解法只讀取輸入。測試入口在呼叫每個解法前都建立獨立陣列副本，避免第一種解法的排序結果洩漏到第二種解法。

## 解法一：排序後依序配對

### API

```csharp
public static int MinMovesToSeat(int[] seats, int[] students)
```

### 設計說明

分別排序座位與學生位置，讓兩個陣列都由小到大排列，再把相同索引的元素配成一組：

```text
sort(seats)
sort(students)

answer = sum(|seats[i] - students[i]|)
```

這個方法直接落實「最佳配對不交叉」的性質。排序完成後，第 `i` 小的學生應配給第 `i` 小的座位；逐項累加距離即可。

### 範例演示流程

輸入：

```text
seats   = [3, 1, 5]
students = [2, 7, 4]
```

排序後：

```text
seats   = [1, 3, 5]
students = [2, 4, 7]
```

| 索引 | 座位 | 學生 | 本次距離 | 累積距離 |
| ---: | ---: | ---: | ---: | ---: |
| 0 | 1 | 2 | `|1-2| = 1` | 1 |
| 1 | 3 | 4 | `|3-4| = 1` | 2 |
| 2 | 5 | 7 | `|5-7| = 2` | 4 |

因此答案為 `4`。

### 正確性說明

排序後若不依相同索引配對，就至少存在一組交叉：較左邊的學生被分配到較右邊的座位，而較右邊的學生被分配到較左邊的座位。交換這兩個目的地不會增加一維絕對距離總和。反覆交換後，所有配對都按照位置順序排列，而成本不增加，因此相同索引配對必定能得到最小總成本。

### 複雜度與輸入契約

- 時間複雜度：`O(n log n)`，主要成本來自兩次排序。
- 額外空間：取決於執行環境的排序實作。
- 修改輸入：是；`seats` 與 `students` 都會被原地排序。
- 優點：概念直接，程式碼短，適用於位置範圍很大的情況。

## 解法二：計數與雙指標

### API

```csharp
public static int MinMovesToSeat2(int[] seats, int[] students)
```

### 設計說明

題目保證所有位置都介於 `1` 到 `100`，因此不必真的排序每個元素。建立兩個長度為 `101` 的次數表：

```text
seatCounts[position]    = 該位置的座位數量
studentCounts[position] = 該位置的學生數量
```

接著使用 `seatPosition` 與 `studentPosition`，分別指向目前仍有數量的最小座位位置與最小學生位置。兩邊可一次配對的數量是：

```text
matchedCount = min(seatCounts[seatPosition], studentCounts[studentPosition])
```

這批配對增加的成本為：

```text
matchedCount * |seatPosition - studentPosition|
```

扣除已配對數量後，數量歸零的一側繼續向右尋找下一個位置。這等價於把計數表展開成排序陣列後逐項配對，但不用改動輸入。

### 範例演示流程

仍以 `seats = [3,1,5]`、`students = [2,7,4]` 為例，非零計數如下：

```text
座位：1×1、3×1、5×1
學生：2×1、4×1、7×1
```

| 步驟 | 座位位置 | 學生位置 | 配對數量 | 本次成本 | 累積成本 |
| ---: | ---: | ---: | ---: | ---: | ---: |
| 1 | 1 | 2 | 1 | `1 × |1-2| = 1` | 1 |
| 2 | 3 | 4 | 1 | `1 × |3-4| = 1` | 2 |
| 3 | 5 | 7 | 1 | `1 × |5-7| = 2` | 4 |

若某個位置有多個座位或學生，`matchedCount` 可以一次消耗多個相同項目，不必逐一展開。

### 正確性說明

雙指標每輪取出尚未配對的最小學生位置與最小座位位置，正好模擬排序後的下一組相同索引元素。批次配對只是把距離相同的多組配對合併計算，不會改變任何配對關係。由於排序後依序配對已證明為最佳解，計數雙指標得到的總成本也必定最小。

### 複雜度與輸入契約

令 `k` 為可能的位置數量，本題 `k = 100`：

- 時間複雜度：`O(n + k)`，先統計 `n` 個元素，再掃描位置範圍。
- 額外空間：`O(k)`，使用兩個位置次數表。
- 修改輸入：否。
- 優點：利用小型固定值域避免比較排序，也能自然批次處理重複位置。

## 兩種解法比較

| 比較項目 | `MinMovesToSeat` | `MinMovesToSeat2` |
| --- | --- | --- |
| 核心方法 | 排序後逐項配對 | 位置計數後以雙指標批次配對 |
| 時間複雜度 | `O(n log n)` | `O(n + k)`，本題 `k = 100` |
| 額外空間 | 依排序實作而定 | `O(k)` |
| 修改輸入 | 是 | 否 |
| 是否依賴小型值域 | 否 | 是 |
| 教學重點 | 一維最小成本配對、交換論證 | 計數排序觀點、批次消耗重複值 |

若只考慮本題限制，計數法具有線性時間；若位置範圍很大或沒有明確上限，排序法更通用且簡潔。

## 可執行測試資料

`Main` 執行六組固定案例，每組呼叫兩種解法，因此共有十二項答案檢查。任一檢查失敗時，程式會設定非零結束代碼。

| 案例 | `seats` | `students` | 預期 | 涵蓋重點 |
| --- | --- | --- | ---: | --- |
| 官方範例一 | `[3,1,5]` | `[2,7,4]` | 4 | 一般未排序輸入 |
| 官方範例二 | `[4,1,5,9]` | `[1,3,2,6]` | 7 | 四組不同距離 |
| 官方重複位置範例 | `[2,2,6,6]` | `[1,3,2,6]` | 4 | 重複座位與零距離配對 |
| 最小輸入 | `[1]` | `[1]` | 0 | `n = 1`、無須移動 |
| 已配對但順序不同 | `[2,1,2]` | `[2,2,1]` | 0 | 順序不影響位置多重集合 |
| 重複值與位置上下界 | `[1,1,1]` | `[100,100,100]` | 297 | 邊界位置、批次配對與最大單步距離 |

## 建置與執行

請在本 repository 根目錄執行：

```bash
dotnet restore leetcode_2037/leetcode_2037.csproj
dotnet build leetcode_2037/leetcode_2037.csproj --no-restore --nologo
dotnet run --no-build --project leetcode_2037/leetcode_2037.csproj
```

本 repository 目前沒有獨立的自動化測試專案；`Main` 的十二項固定檢查就是可重複執行的驗收 harness。成功時最後一行應為：

```text
總結：12/12 項測試通過
```

## 實際執行結果

以下內容來自修改後實際執行 `dotnet run --no-build --project leetcode_2037/leetcode_2037.csproj` 的輸出：

```text
案例：1. 官方範例一
Input：seats = [3, 1, 5], students = [2, 7, 4]
解法一：MinMovesToSeat（排序貪婪）
Expected：4
Actual：4
Result：PASS
解法二：MinMovesToSeat2（計數雙指標）
Expected：4
Actual：4
Result：PASS

案例：2. 官方範例二
Input：seats = [4, 1, 5, 9], students = [1, 3, 2, 6]
解法一：MinMovesToSeat（排序貪婪）
Expected：7
Actual：7
Result：PASS
解法二：MinMovesToSeat2（計數雙指標）
Expected：7
Actual：7
Result：PASS

案例：3. 官方重複位置範例
Input：seats = [2, 2, 6, 6], students = [1, 3, 2, 6]
解法一：MinMovesToSeat（排序貪婪）
Expected：4
Actual：4
Result：PASS
解法二：MinMovesToSeat2（計數雙指標）
Expected：4
Actual：4
Result：PASS

案例：4. 最小輸入
Input：seats = [1], students = [1]
解法一：MinMovesToSeat（排序貪婪）
Expected：0
Actual：0
Result：PASS
解法二：MinMovesToSeat2（計數雙指標）
Expected：0
Actual：0
Result：PASS

案例：5. 已配對但順序不同
Input：seats = [2, 1, 2], students = [2, 2, 1]
解法一：MinMovesToSeat（排序貪婪）
Expected：0
Actual：0
Result：PASS
解法二：MinMovesToSeat2（計數雙指標）
Expected：0
Actual：0
Result：PASS

案例：6. 重複值與位置上下界
Input：seats = [1, 1, 1], students = [100, 100, 100]
解法一：MinMovesToSeat（排序貪婪）
Expected：297
Actual：297
Result：PASS
解法二：MinMovesToSeat2（計數雙指標）
Expected：297
Actual：297
Result：PASS

總結：12/12 項測試通過
```

## 專案結構

```text
leetcode_2037/
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_2037.sln
└── leetcode_2037/
    ├── leetcode_2037.csproj
    └── Program.cs
```

- `Program.cs`：題目解法、XML 文件與可執行案例 harness。
- `leetcode_2037.csproj`：目標框架為 `net10.0` 的 console 專案設定。
- `docs/readme-template.md`：首次建立 README 時使用的內容與驗證準則。
