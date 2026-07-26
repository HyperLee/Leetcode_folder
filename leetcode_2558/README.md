# LeetCode 2558：從禮物最多的堆中拿取禮物

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![LeetCode](https://img.shields.io/badge/LeetCode-2558-FFA116)](https://leetcode.com/problems/take-gifts-from-the-richest-pile/)

這是一個 .NET 10 主控台專案，示範三種解決 LeetCode 2558 的方式：
線性搜尋、每輪完整排序，以及以 `PriorityQueue` 模擬最大堆。程式內含固定案例，
可以直接建置並執行，不需要輸入任何資料。

## 快速導覽

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：線性搜尋最大值](#解法一線性搜尋最大值)
- [解法二：每輪完整排序](#解法二每輪完整排序)
- [解法三：PriorityQueue 最大堆](#解法三priorityqueue-最大堆)
- [三種解法比較](#三種解法比較)
- [建置與執行](#建置與執行)

## 題目說明

給定整數陣列 `gifts`，其中 `gifts[i]` 表示第 `i` 堆的禮物數量。
每一秒必須執行一次下列操作：

1. 選擇目前禮物數量最多的一堆。
2. 如果多堆並列最大，可以任選其中一堆。
3. 將選中堆的數量改成原數量平方根的向下取整值：

   ```text
   gifts[i] = floor(sqrt(gifts[i]))
   ```

經過 `k` 秒後，回傳所有禮物堆的數量總和。

題目連結：

- [LeetCode 英文版](https://leetcode.com/problems/take-gifts-from-the-richest-pile/description/)
- [力扣中文版](https://leetcode.cn/problems/take-gifts-from-the-richest-pile/description/)

### 官方範例一

```text
輸入：gifts = [25, 64, 9, 4, 100], k = 4
輸出：29
```

四次操作依序縮減 `100`、`64`、`25`、`10`，最後各堆可以表示為
`[5, 8, 9, 4, 3]`，總和為 `29`。

### 官方範例二

```text
輸入：gifts = [1, 1, 1, 1], k = 4
輸出：4
```

因為 `floor(sqrt(1)) = 1`，每次操作後都不會再減少，最後總和仍是 `4`。

## 限制條件

官方題目限制如下：

```text
1 <= gifts.length <= 10^3
1 <= gifts[i] <= 10^9
1 <= k <= 10^3
```

本專案另外加入 `k = 0` 的本機防退化案例。它不在官方輸入範圍內，
但可驗證「不執行任何操作時，應直接回傳原始總和」。

## 解題概念與出發點

### 1. 為什麼每次都處理最大堆

這不是需要另外尋找最佳策略的選擇題；題目已明確指定每秒必須選擇目前最大堆。
因此核心工作是忠實模擬 `k` 次操作，並思考如何有效率地反覆取得最大值。

所有解法都遵循同一個狀態轉移：

```text
目前最大值 x
    ↓
移除或定位 x
    ↓
計算 floor(sqrt(x))
    ↓
把縮減後的值放回集合
```

### 2. 平方根為什麼可以直接轉成 int

題目的 `gifts[i]` 都是正整數。對非負的 `double` 執行 `(int)` 轉型會截去
小數部分，因此：

```csharp
(int)Math.Sqrt(x)
```

與下列寫法結果相同：

```csharp
(int)Math.Floor(Math.Sqrt(x))
```

### 3. 三種解法真正不同的地方

三種方法的狀態轉移完全相同，差異只在「如何取得目前最大值」：

- `PickGifts` 每輪掃描整個陣列。
- `PickGifts2` 每輪重新排序整個陣列。
- `PickGifts3` 用最大堆持續維護最大值。

## 解法一：線性搜尋最大值

### 設計說明

`PickGifts` 每一輪分成三步：

1. `gifts.Max()` 掃描陣列，找出最大禮物數。
2. `Array.IndexOf` 再掃描一次，找到第一個最大值的索引。
3. 將該位置替換為平方根向下取整值。

如果最大值重複，`Array.IndexOf` 會選到第一個最大值。這符合題目「並列時任選」
的規定，不需要另外處理所有並列位置。

此方法會直接修改傳入的 `gifts`。測試 runner 呼叫它之前會使用
`gifts.ToArray()` 建立副本，避免影響其他解法。

### 演示流程

使用 `gifts = [25, 64, 9, 4, 100]`、`k = 4`：

| 秒數 | 掃描得到的最大值 | 選中索引 | 縮減結果 | 更新後陣列 |
| ---: | ---: | ---: | ---: | --- |
| 初始 | - | - | - | `[25, 64, 9, 4, 100]` |
| 1 | 100 | 4 | 10 | `[25, 64, 9, 4, 10]` |
| 2 | 64 | 1 | 8 | `[25, 8, 9, 4, 10]` |
| 3 | 25 | 0 | 5 | `[5, 8, 9, 4, 10]` |
| 4 | 10 | 4 | 3 | `[5, 8, 9, 4, 3]` |

最後總和：

```text
5 + 8 + 9 + 4 + 3 = 29
```

### 正確性說明

在每一輪開始時，`Max()` 會取得目前所有禮物堆中的最大值；
`Array.IndexOf` 會定位其中一個具有該值的合法禮物堆。方法只縮減這一堆，
而且縮減公式與題目相同。因此，每輪結束後的陣列狀態都等同題目規定的狀態。
重複 `k` 輪後，陣列即為第 `k` 秒的正確狀態，累加所有元素便得到正確答案。

### 複雜度

- 時間：`O(k × n + n)`，簡化為 `O(k × n)`。
- 額外空間：`O(1)`。
- 輸入副作用：會修改 `gifts` 的內容。

## 解法二：每輪完整排序

### 設計說明

`PickGifts2` 不另外搜尋索引，而是在每輪執行：

1. `Array.Sort(gifts)` 將陣列由小到大排序。
2. `Array.Reverse(gifts)` 將順序反轉成由大到小。
3. 此時索引 `0` 必為最大值，直接縮減 `gifts[0]`。

寫法直觀，最大值的位置也非常明確，但為了只取得一個最大值而排序全部元素，
付出的成本比線性掃描更高。它同樣會改動輸入陣列，包含元素順序與縮減後的內容。

### 演示流程

使用相同的 `gifts` 與 `k`。表格中的「排序後」是縮減前的狀態：

| 秒數 | 由大到小排序後 | 縮減最大值 | 本輪結束狀態 |
| ---: | --- | --- | --- |
| 1 | `[100, 64, 25, 9, 4]` | `100 → 10` | `[10, 64, 25, 9, 4]` |
| 2 | `[64, 25, 10, 9, 4]` | `64 → 8` | `[8, 25, 10, 9, 4]` |
| 3 | `[25, 10, 9, 8, 4]` | `25 → 5` | `[5, 10, 9, 8, 4]` |
| 4 | `[10, 9, 8, 5, 4]` | `10 → 3` | `[3, 9, 8, 5, 4]` |

最後陣列順序和解法一不同，但元素多重集合仍是 `{3, 4, 5, 8, 9}`，
所以總和同樣為 `29`。題目只要求總數，不要求保留原始順序。

### 正確性說明

每輪排序並反轉後，`gifts[0]` 一定是當前最大值。方法對它套用正確的平方根
縮減公式，其他元素保持相同；因此本輪狀態符合題意。此性質連續成立 `k` 輪，
最後累加陣列即可取得正確答案。

### 複雜度

- 時間：每輪排序為 `O(n log n)`、反轉為 `O(n)`，總計 `O(k × n log n)`。
- 額外空間：依 .NET 陣列排序實作，呼叫堆疊為 `O(log n)`。
- 輸入副作用：會修改 `gifts` 的順序與內容。

## 解法三：PriorityQueue 最大堆

### 設計說明

反覆取最大值正是最大堆擅長的工作。但 .NET 的
`PriorityQueue<TElement, TPriority>` 是最小優先權佇列：
數值最小的 `TPriority` 會先被 `Dequeue`。

本解法把禮物數 `gift` 當成元素，並使用 `-gift` 當成優先權：

```csharp
pq.Enqueue(gift, -gift);
```

例如禮物數 `100`、`64`、`25` 的優先權分別是 `-100`、`-64`、`-25`。
最小的優先權 `-100` 會先出隊，因此效果等同取出最大的禮物堆 `100`。

每輪流程如下：

1. `Dequeue()` 取出最大禮物堆。
2. 計算平方根向下取整值。
3. 以新值及其負優先權重新 `Enqueue()`。

全部操作完成後，再逐一取出堆內元素並累加。此方法只把輸入元素加入新的
優先佇列，不會修改原始 `gifts` 陣列。

### 演示流程

堆的內部陣列排列不是完整排序；下表為了便於閱讀，僅以由大到小的邏輯內容表示：

| 秒數 | 取出最大值 | 放回值 | 堆內邏輯內容 |
| ---: | ---: | ---: | --- |
| 初始 | - | - | `[100, 64, 25, 9, 4]` |
| 1 | 100 | 10 | `[64, 25, 10, 9, 4]` |
| 2 | 64 | 8 | `[25, 10, 9, 8, 4]` |
| 3 | 25 | 5 | `[10, 9, 8, 5, 4]` |
| 4 | 10 | 3 | `[9, 8, 5, 4, 3]` |

將堆內所有值出隊並相加：

```text
9 + 8 + 5 + 4 + 3 = 29
```

### 正確性說明

建立優先佇列後，每一堆禮物都存在堆中，且數量 `x` 對應優先權 `-x`。
因為較大的 `x` 會形成較小的 `-x`，每次 `Dequeue` 必定取出當前最大值。
方法依題意縮減該值後立刻放回，故堆仍完整表示下一秒開始前的所有禮物堆。
這個不變量維持 `k` 輪後，堆中的元素就是正確最終狀態，其總和即為答案。

### 複雜度

- 建堆：目前程式逐一 `Enqueue`，需要 `O(n log n)`。
- `k` 次更新：`O(k log n)`。
- 最後逐一出隊加總：`O(n log n)`。
- 總時間：`O((n + k) log n)`。
- 額外空間：`O(n)`。
- 輸入副作用：不修改 `gifts`。

## 三種解法比較

| 方法 | 取得最大值方式 | 時間複雜度 | 額外空間 | 修改輸入 | 特點 |
| --- | --- | --- | --- | --- | --- |
| `PickGifts` | 每輪線性掃描 | `O(k × n)` | `O(1)` | 是 | 實作簡單，無額外集合 |
| `PickGifts2` | 每輪完整排序 | `O(k × n log n)` | `O(log n)` | 是 | 最直觀，但做了超出需求的完整排序 |
| `PickGifts3` | 最大堆 | `O((n + k) log n)` | `O(n)` | 否 | 最符合反覆取得最大值的資料結構 |

在官方上限 `n, k <= 1000` 下三種方法都容易理解與執行；若資料規模增加，
最大堆通常更能避免每輪掃描或完整排序的重複成本。

## 可執行測試案例

`Main` 會執行五組固定案例，且每種解法都取得獨立的輸入副本：

| 案例 | `gifts` | `k` | 預期值 | 驗證目的 |
| --- | --- | ---: | ---: | --- |
| Official example 1 | `[25,64,9,4,100]` | 4 | 29 | 一般縮減流程 |
| Official example 2 | `[1,1,1,1]` | 4 | 4 | 平方根後保持不變 |
| Single pile | `[100]` | 1 | 10 | 單一禮物堆 |
| Repeated maximums | `[16,16]` | 2 | 8 | 並列最大值可任選 |
| Zero operations | `[9,4]` | 0 | 13 | 不執行操作 |

## 建置與執行

### 需求

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

### 建置

在本目錄 `leetcode_2558` 執行：

```powershell
dotnet build .\leetcode_2558\leetcode_2558.csproj
```

### 執行

```powershell
dotnet run --project .\leetcode_2558\leetcode_2558.csproj
```

也可以在 VS Code 按 `Ctrl+Shift+B` 建置，或直接按 `F5` 啟動偵錯。

### 實際執行輸出

```text
Official example 1: gifts = [25, 64, 9, 4, 100], k = 4, Expected = 29
  PickGifts:  Actual = 29 (PASS)
  PickGifts2: Actual = 29 (PASS)
  PickGifts3: Actual = 29 (PASS)

Official example 2: gifts = [1, 1, 1, 1], k = 4, Expected = 4
  PickGifts:  Actual = 4 (PASS)
  PickGifts2: Actual = 4 (PASS)
  PickGifts3: Actual = 4 (PASS)

Single pile: gifts = [100], k = 1, Expected = 10
  PickGifts:  Actual = 10 (PASS)
  PickGifts2: Actual = 10 (PASS)
  PickGifts3: Actual = 10 (PASS)

Repeated maximums: gifts = [16, 16], k = 2, Expected = 8
  PickGifts:  Actual = 8 (PASS)
  PickGifts2: Actual = 8 (PASS)
  PickGifts3: Actual = 8 (PASS)

Zero operations: gifts = [9, 4], k = 0, Expected = 13
  PickGifts:  Actual = 13 (PASS)
  PickGifts2: Actual = 13 (PASS)
  PickGifts3: Actual = 13 (PASS)
```

總計：`15 PASS / 0 FAIL`。

## 專案結構

```text
leetcode_2558/
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── docs/
│   └── readme-template.md
├── leetcode_2558/
│   ├── leetcode_2558.csproj
│   └── Program.cs
├── AGENTS.md
└── README.md
```

`Program.cs` 同時包含固定案例 runner 與三種演算法；此專案目前沒有獨立測試專案，
因此 `dotnet run` 的 PASS/FAIL 輸出就是可重複執行的行為驗證。
