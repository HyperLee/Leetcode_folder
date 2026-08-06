# LeetCode 1701：平均等待時間（Average Waiting Time）

[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/download/dotnet/10.0)

本專案是 LeetCode 1701 的 .NET 10 console 教學範例。程式以「完成時間模擬」、「等待積壓遞推」與「前綴公式」三種 O(n) 觀點計算平均等待時間，並在 `Main` 內建六組可重複執行的案例，實際驗證答案與輸入不變性。

## 目錄

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：完成時間模擬](#解法一完成時間模擬)
- [解法二：等待積壓遞推](#解法二等待積壓遞推)
- [解法三：前綴製作時間與最大偏移](#解法三前綴製作時間與最大偏移)
- [三種解法比較](#三種解法比較)
- [可執行測試資料](#可執行測試資料)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)
- [專案結構](#專案結構)

## 題目說明

官方題目：

- [LeetCode 英文題目](https://leetcode.com/problems/average-waiting-time/)
- [LeetCode 中文題目](https://leetcode.cn/problems/average-waiting-time/description/)

餐廳只有一位廚師。輸入 `customers[i] = [arrival_i, time_i]` 表示第 `i` 位客人的到達時間，以及完成該客人餐點所需的製作時間。

服務規則如下：

1. 客人按照輸入順序接受服務，不能插隊。
2. 廚師一次只能製作一份餐點。
3. 若客人到達時廚師空閒，立即開始製作。
4. 若客人到達時廚師仍忙碌，必須等前面的餐點完成。
5. 客人的等待時間包含餐點製作時間，即「取得餐點的時間減去到達時間」。

最後回傳所有客人等待時間的平均值。答案與正確值誤差在 `10^-5` 內即可接受。

以 `customers = [[1,2],[2,5],[4,3]]` 為例：

| 客人 | 到達 | 製作時間 | 開始製作 | 完成 | 等待時間 |
| --- | ---: | ---: | ---: | ---: | ---: |
| 1 | 1 | 2 | 1 | 3 | `3 - 1 = 2` |
| 2 | 2 | 5 | 3 | 8 | `8 - 2 = 6` |
| 3 | 4 | 3 | 8 | 11 | `11 - 4 = 7` |

因此平均等待時間為：

```text
(2 + 6 + 7) / 3 = 5
```

## 限制條件

| 條件 | 官方範圍 |
| --- | --- |
| 客人數量 | `1 <= customers.Length <= 100000` |
| 每筆資料長度 | `customers[i].Length == 2` |
| 到達時間 | `1 <= arrival_i <= 10000` |
| 製作時間 | `1 <= time_i <= 10000` |
| 排序條件 | `arrival_i <= arrival_(i+1)` |

題目保證輸入非空、每筆資料合法且到達時間已排序，因此三個公開 API 不額外定義空陣列、錯誤列長度或未排序輸入的例外行為。

## 解題概念與出發點

### 1. 真正的開始時間

對每位客人而言，能開始製作的時間同時受兩件事限制：

- 客人必須已經到達。
- 廚師必須完成前一份餐點。

因此：

```text
startTime = max(arrival, previousFinishTime)
finishTime = startTime + preparationTime
waitingTime = finishTime - arrival
```

### 2. 空閒與積壓是同一個狀態的兩面

若前一份餐點早於目前客人到達前完成，廚師有空閒時間，新的等待只剩目前餐點的製作時間。若尚未完成，剩餘工作會形成積壓，目前餐點必須排在積壓之後。

```text
newPending = max(0, oldPending - arrivalGap) + preparationTime
```

更新後的 `newPending` 正好是目前客人從到達到取餐的完整等待時間。

### 3. 累積值使用 long

單一完成時間在官方限制內仍可放入 `int`，但十萬位客人的等待時間總和可能遠大於 `int.MaxValue`。三種解法都用 `long` 保存完成時間、前綴和與總等待時間，最後除以客人數量時才轉為 `double`。

### 4. 輸入不變契約

三個公開方法只讀取 `customers`，不會排序、覆寫或替換其中的資料。測試入口仍為每個方法建立獨立的深層複本，執行後再逐項比較，讓這項契約可以被實際驗證。

## 解法一：完成時間模擬

### API

```csharp
public static double AverageWaitingTime(int[][] customers)
```

### 設計說明

這是最直接對照題意的貪婪模擬。`finishTime` 始終代表廚師完成目前已排入訂單的時間：

```text
finishTime = max(finishTime, arrival) + preparationTime
totalWaitingTime += finishTime - arrival
```

若 `finishTime < arrival`，代表廚師先前已經空閒，從客人的到達時間重新開始；否則直接接續既有工作。

### 範例演示流程

輸入 `[[1,2],[2,5],[4,3]]`：

| 客人 | 舊 `finishTime` | 到達 | 製作 | 新 `finishTime` | 本次等待 | 累積等待 |
| --- | ---: | ---: | ---: | ---: | ---: | ---: |
| `[1,2]` | 0 | 1 | 2 | `max(0,1)+2 = 3` | 2 | 2 |
| `[2,5]` | 3 | 2 | 5 | `max(3,2)+5 = 8` | 6 | 8 |
| `[4,3]` | 8 | 4 | 3 | `max(8,4)+3 = 11` | 7 | 15 |

最後 `15 / 3 = 5`。

### 正確性說明

處理每位客人前，`finishTime` 是所有前序訂單的完成時間。客人與廚師兩者都就緒後才能開始，因此兩者時間的最大值必定是真正開始時間；再加製作時間便得到唯一合法的完成時間。逐筆累加 `finishTime - arrival`，最後即得到所有客人的平均等待時間。

### 複雜度與輸入契約

- 時間複雜度：`O(n)`。
- 回傳值空間：`O(1)`。
- 額外空間：`O(1)`。
- 修改輸入：否。

## 解法二：等待積壓遞推

### API

```csharp
public static double AverageWaitingTime2(int[][] customers)
```

### 設計說明

這個版本不直接保存絕對完成時間，而是保存 `pendingTime`：在目前觀察時間點，廚師仍需要多少時間才能完成已排定工作。

相鄰兩位客人的到達間隔 `elapsed` 期間，廚師會持續消化工作：

```text
pendingTime = max(0, pendingTime - elapsed)
```

積壓不能低於零；若間隔足夠長，多出的時間只是廚師空閒。接著排入目前訂單：

```text
pendingTime += preparationTime
```

此時 `pendingTime` 就是目前客人的等待時間。

### 範例演示流程

輸入 `[[1,2],[2,5],[4,3]]`，初始 `previousArrival = 1`、`pendingTime = 0`：

| 客人 | 到達間隔 | 消耗後積壓 | 加入製作時間 | 本次等待 | 累積等待 |
| --- | ---: | ---: | ---: | ---: | ---: |
| `[1,2]` | `1-1 = 0` | 0 | `0+2 = 2` | 2 | 2 |
| `[2,5]` | `2-1 = 1` | `max(0,2-1) = 1` | `1+5 = 6` | 6 | 8 |
| `[4,3]` | `4-2 = 2` | `max(0,6-2) = 4` | `4+3 = 7` | 7 | 15 |

最後 `15 / 3 = 5`。

### 正確性說明

處理客人前，舊 `pendingTime` 代表前一位客人到達後排入的全部工作。兩次到達之間經過的時間必然會等量減少尚未完成的工作，但不能產生負工作量。加入目前餐點後，更新值正是目前客人必須等候的既有工作加自身製作時間，所以每輪累加值皆正確。

### 複雜度與輸入契約

- 時間複雜度：`O(n)`。
- 回傳值空間：`O(1)`。
- 額外空間：`O(1)`。
- 修改輸入：否。

## 解法三：前綴製作時間與最大偏移

### API

```csharp
public static double AverageWaitingTime3(int[][] customers)
```

### 設計出發點

把前 `i` 位客人的製作時間總和記為 `preparationPrefix_i`。如果從第 `j` 位客人的到達時間重新開工，處理到第 `i` 位時的完成時間可寫成：

```text
arrival_j + (preparationPrefix_i - preparationPrefix_(j-1))
= preparationPrefix_i + (arrival_j - preparationPrefix_(j-1))
```

第 `i` 位客人真正的完成時間，必須考慮所有可能造成延後的起點 `j`，所以只要維護：

```text
maximumStartOffset = max(arrival_j - preparationPrefix_(j-1))
finishTime = preparationPrefix_i + maximumStartOffset
```

這將逐筆模擬改寫成前綴和與前綴最大值，但仍只需走訪一次。

### 範例演示流程

輸入 `[[1,2],[2,5],[4,3]]`：

| 客人 | 舊製作前綴 | 新偏移候選 | 最大偏移 | 新製作前綴 | 完成時間 | 等待 |
| --- | ---: | ---: | ---: | ---: | ---: | ---: |
| `[1,2]` | 0 | `1-0 = 1` | 1 | 2 | `2+1 = 3` | 2 |
| `[2,5]` | 2 | `2-2 = 0` | 1 | 7 | `7+1 = 8` | 6 |
| `[4,3]` | 7 | `4-7 = -3` | 1 | 10 | `10+1 = 11` | 7 |

等待時間總和仍為 15，平均為 5。

### 正確性說明

任何一段連續忙碌區間都從某位客人到達時開始，完成目前客人所需的時間等於該起點的到達時間加上區間內所有製作時間。公式將每個可能起點拆成固定的目前製作前綴與起點偏移；取偏移的前綴最大值，就等價於選出對目前完成時間限制最強的起點，因此所得完成時間與實際服務流程相同。

### 複雜度與輸入契約

- 時間複雜度：`O(n)`。
- 回傳值空間：`O(1)`。
- 額外空間：`O(1)`。
- 修改輸入：否。

## 三種解法比較

| 比較項目 | `AverageWaitingTime` | `AverageWaitingTime2` | `AverageWaitingTime3` |
| --- | --- | --- | --- |
| 核心狀態 | 絕對完成時間 | 相對等待積壓 | 製作前綴和、最大起始偏移 |
| 思考角度 | 直接模擬服務流程 | 觀察兩次到達之間的工作消耗 | 將完成時間代數化 |
| 時間複雜度 | `O(n)` | `O(n)` | `O(n)` |
| 額外空間 | `O(1)` | `O(1)` | `O(1)` |
| 修改輸入 | 否 | 否 | 否 |
| 適合用途 | 最直觀、實務首選 | 理解空閒與積壓 | 練習前綴公式推導 |

三種方法的漸進複雜度相同。若只需要提交一道題目，完成時間模擬最簡潔；後兩種保留作為不同狀態建模方式的教學與交叉驗證。

## 可執行測試資料

`Main` 執行六組固定案例，每組呼叫三種解法。每次呼叫驗證兩項契約：

1. `Actual` 與 `Expected` 的絕對誤差不超過 `1e-9`。
2. 呼叫後的巢狀輸入與原始內容完全相同。

因此總共有 `6 × 3 × 2 = 36` 項檢查。任一項失敗時，程式會設定非零結束代碼。

| 案例 | 輸入摘要 | 預期平均 | 涵蓋重點 |
| --- | --- | ---: | --- |
| 官方範例一 | `[[1,2],[2,5],[4,3]]` | 5 | 廚師持續忙碌 |
| 官方範例二 | `[[5,2],[5,4],[10,3],[20,1]]` | 3.25 | 同時到達、重新空閒 |
| 最小輸入 | `[[1,1]]` | 1 | `n = 1` |
| 全程無積壓 | `[[1,2],[10,3],[20,1]]` | 2 | 每次積壓歸零 |
| 同時到達 | `[[5,2],[5,1],[5,3]]` | `11/3` | 零到達間隔、循環小數 |
| 官方最大規模 | 十萬筆 `[1,10000]` | 500005000 | 效能與大型累積值 |

大型案例的完整輸入不適合出現在終端與 README，因此格式化工具只顯示資料筆數、第一筆及最後一筆。

## 建置與執行

請在本 repository 根目錄執行：

```bash
dotnet restore leetcode_1701/leetcode_1701.csproj
dotnet build leetcode_1701/leetcode_1701.csproj --no-restore --nologo
dotnet run --no-build --project leetcode_1701/leetcode_1701.csproj
```

成功時最後一行應為：

```text
總結：36/36 項測試通過
```

## 實際執行結果

以下內容來自本專案修改後的實際 `dotnet run --no-build` 執行：

```text

案例：1. 官方範例一（廚師持續忙碌）
Input：customers = [[1, 2], [2, 5], [4, 3]]
解法一：AverageWaitingTime（完成時間模擬）
Expected：5
Actual：5
Error：0
Output：PASS
Input unchanged：PASS
解法二：AverageWaitingTime2（等待積壓遞推）
Expected：5
Actual：5
Error：0
Output：PASS
Input unchanged：PASS
解法三：AverageWaitingTime3（前綴公式）
Expected：5
Actual：5
Error：0
Output：PASS
Input unchanged：PASS

案例：2. 官方範例二（同時到達與空閒）
Input：customers = [[5, 2], [5, 4], [10, 3], [20, 1]]
解法一：AverageWaitingTime（完成時間模擬）
Expected：3.25
Actual：3.25
Error：0
Output：PASS
Input unchanged：PASS
解法二：AverageWaitingTime2（等待積壓遞推）
Expected：3.25
Actual：3.25
Error：0
Output：PASS
Input unchanged：PASS
解法三：AverageWaitingTime3（前綴公式）
Expected：3.25
Actual：3.25
Error：0
Output：PASS
Input unchanged：PASS

案例：3. 最小輸入
Input：customers = [[1, 1]]
解法一：AverageWaitingTime（完成時間模擬）
Expected：1
Actual：1
Error：0
Output：PASS
Input unchanged：PASS
解法二：AverageWaitingTime2（等待積壓遞推）
Expected：1
Actual：1
Error：0
Output：PASS
Input unchanged：PASS
解法三：AverageWaitingTime3（前綴公式）
Expected：1
Actual：1
Error：0
Output：PASS
Input unchanged：PASS

案例：4. 每位客人到達前廚師皆已空閒
Input：customers = [[1, 2], [10, 3], [20, 1]]
解法一：AverageWaitingTime（完成時間模擬）
Expected：2
Actual：2
Error：0
Output：PASS
Input unchanged：PASS
解法二：AverageWaitingTime2（等待積壓遞推）
Expected：2
Actual：2
Error：0
Output：PASS
Input unchanged：PASS
解法三：AverageWaitingTime3（前綴公式）
Expected：2
Actual：2
Error：0
Output：PASS
Input unchanged：PASS

案例：5. 多位客人同時到達
Input：customers = [[5, 2], [5, 1], [5, 3]]
解法一：AverageWaitingTime（完成時間模擬）
Expected：3.6666666667
Actual：3.6666666667
Error：0
Output：PASS
Input unchanged：PASS
解法二：AverageWaitingTime2（等待積壓遞推）
Expected：3.6666666667
Actual：3.6666666667
Error：0
Output：PASS
Input unchanged：PASS
解法三：AverageWaitingTime3（前綴公式）
Expected：3.6666666667
Actual：3.6666666667
Error：0
Output：PASS
Input unchanged：PASS

案例：6. 官方最大客人數與製作時間
Input：customers = 100000 筆；first = [1, 10000]；last = [1, 10000]
解法一：AverageWaitingTime（完成時間模擬）
Expected：500005000
Actual：500005000
Error：0
Output：PASS
Input unchanged：PASS
解法二：AverageWaitingTime2（等待積壓遞推）
Expected：500005000
Actual：500005000
Error：0
Output：PASS
Input unchanged：PASS
解法三：AverageWaitingTime3（前綴公式）
Expected：500005000
Actual：500005000
Error：0
Output：PASS
Input unchanged：PASS

總結：36/36 項測試通過
```

## 專案結構

```text
leetcode_1701/
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_1701.sln
└── leetcode_1701/
    ├── leetcode_1701.csproj
    └── Program.cs
```

- `Program.cs`：三種解法、XML 教學文件與可執行驗證入口。
- `leetcode_1701.csproj`：目標框架為 `net10.0` 的 console project。
- `docs/readme-template.md`：初次建立 README 時使用的專案範本。