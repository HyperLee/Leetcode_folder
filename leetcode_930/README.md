# LeetCode 930：和相同的二元子陣列

這是一個以 C# / .NET 10 撰寫的主控台範例，示範如何計算二元陣列中「總和恰好等於 `goal`」的非空連續子陣列數量。

- 題目：[930. Binary Subarrays With Sum](https://leetcode.com/problems/binary-subarrays-with-sum/description/)
- 中文題目：[930. 和相同的二元子陣列](https://leetcode.cn/problems/binary-subarrays-with-sum/description/)

## 題目說明

給定一個只包含 `0` 與 `1` 的陣列 `nums`，以及整數 `goal`，請回傳總和等於 `goal` 的非空連續子陣列數量。

「子陣列」必須是原陣列中一段連續的元素，不能跳著選。

例如：

```text
nums = [1, 0, 1, 0, 1], goal = 2
```

符合條件的子陣列共有 4 個，因此答案為 `4`。

## 限制條件

- `1 <= nums.Length <= 30,000`
- `nums[i]` 只能是 `0` 或 `1`
- `0 <= goal <= nums.Length`

## 解題概念與出發點

最直接的想法是枚舉每一個起點與終點，再計算每段子陣列的總和。即使利用累加避免重算，仍需要檢查約 `n²` 個區間；當陣列長度達到 30,000 時並不合適。

本專案保留兩種 `O(n)` 解法：

1. **前綴和＋雜湊表**：把「區間總和」改寫成兩個前綴和的差，快速查詢以前出現過多少個符合的切點。
2. **雙左界滑動視窗**：利用陣列元素皆為非負數的特性，同時維護「總和不超過 goal」與「總和小於 goal」的兩個視窗邊界。

## 解法比較

| 解法 | 方法 | 時間複雜度 | 空間複雜度 | 主要特性 |
|---|---|---:|---:|---|
| `NumSubarraysWithSum` | 前綴和＋雜湊表 | `O(n)` | `O(n)` | 概念通用，透過前綴和出現次數計數 |
| `NumSubarraysWithSum2` | 雙左界滑動視窗 | `O(n)` | `O(1)` | 不需額外集合，但依賴元素非負 |

## 解法一：前綴和＋雜湊表

### 設計思路

令 `P[j]` 表示從陣列開頭累加到目前位置的前綴和。若某段子陣列的總和為 `goal`，則它的左右前綴和滿足：

```text
P[j] - P[i] = goal
```

移項後可得：

```text
P[i] = P[j] - goal
```

因此，每取得一個新的目前前綴和 `sum`，只要查詢雜湊表中曾出現多少次 `sum - goal`，就知道有多少個以目前位置為右端點的有效子陣列。

程式在加入目前元素之前，先把當時的 `sum` 記錄到雜湊表。這代表雜湊表只包含目前位置以前的切點，也自然包含「從陣列索引 0 開始」所需的初始前綴和 `0`。

### 範例流程

輸入：`nums = [1, 0, 1, 0, 1]`、`goal = 2`

| 索引 | 目前元素 | 加入元素後的 `sum` | 查詢 `sum - goal` | 命中次數 `val` | 累積答案 |
|---:|---:|---:|---:|---:|---:|
| 0 | 1 | 1 | -1 | 0 | 0 |
| 1 | 0 | 1 | -1 | 0 | 0 |
| 2 | 1 | 2 | 0 | 1 | 1 |
| 3 | 0 | 2 | 0 | 1 | 2 |
| 4 | 1 | 3 | 1 | 2 | 4 |

最後得到 `4`。最後一列會一次增加 2，是因為前綴和 `1` 曾出現兩次，兩個切點都能形成總和為 2 的不同子陣列。

### 為什麼正確

每個非空連續子陣列都能由「右端前綴和減去左端前綴和」唯一表示。雜湊表保存所有已經走過的左端前綴和及其出現次數，所以查到幾次 `sum - goal`，就恰好有幾個有效起點；把每個右端點的數量相加即為完整答案。

## 解法二：雙左界滑動視窗

### 設計思路

因為 `nums` 只包含 `0` 與 `1`，當左界向右移時，視窗總和只會減少或維持不變。可以對同一個右界維護兩個左界：

- `left1`：縮小視窗直到 `sum1 <= goal`。
- `left2`：縮小視窗直到 `sum2 < goal`。

此時，所有位於 `[left1, left2)` 的起點都會讓子陣列總和恰好等於 `goal`，因此目前右界新增的答案數量為：

```text
left2 - left1
```

### 範例流程

輸入：`nums = [1, 0, 1, 0, 1]`、`goal = 2`

下表中的狀態是兩個視窗完成縮減後的結果。

| `right` | 元素 | `left1` | `sum1`（<= goal） | `left2` | `sum2`（< goal） | 本輪新增 | 累積答案 |
|---:|---:|---:|---:|---:|---:|---:|---:|
| 0 | 1 | 0 | 1 | 0 | 1 | 0 | 0 |
| 1 | 0 | 0 | 1 | 0 | 1 | 0 | 0 |
| 2 | 1 | 0 | 2 | 1 | 1 | 1 | 1 |
| 3 | 0 | 0 | 2 | 1 | 1 | 1 | 2 |
| 4 | 1 | 1 | 2 | 3 | 1 | 2 | 4 |

以最後一列為例，`left1 = 1`、`left2 = 3`，所以起點 1 與 2 都能形成總和為 2 的子陣列，本輪新增 `3 - 1 = 2` 個答案。

### `goal = 0` 為什麼仍然有效

當 `goal = 0` 時，第一個視窗會移除所有使總和大於 0 的元素；第二個視窗則會持續縮小到總和小於 0。因為二元陣列不可能得到負的視窗總和，`left2` 會越過目前右界。兩個左界的距離正好代表目前結尾之前可選擇多少個連續零作為起點。

例如五個零 `[0, 0, 0, 0, 0]` 每輪分別新增 `1、2、3、4、5` 個子陣列，總數為 `15`。

### 為什麼正確

對固定右界而言，非負元素讓視窗總和隨左界右移而單調不增。`left1` 排除總和大於 `goal` 的起點，`left2` 再排除總和等於 `goal` 的起點；因此兩者之間留下的起點全部且只會產生總和等於 `goal` 的子陣列。

## 可執行案例

`Main` 會讓兩種解法共同執行下列案例，並各自比對預期值：

| 案例 | `nums` | `goal` | 預期答案 | 驗證重點 |
|---:|---|---:|---:|---|
| 1 | `[1, 0, 1, 0, 1]` | 2 | 4 | 一般情境 |
| 2 | `[0, 0, 0, 0, 0]` | 0 | 15 | 全零與 `goal = 0` |
| 3 | `[1]` | 0 | 0 | 沒有符合的子陣列 |
| 4 | `[0, 1, 0, 1, 0]` | 1 | 8 | 重複前綴和與多個起點 |

只要任一結果不符預期，該列會顯示 `FAIL`，最終狀態會是 `Overall: FAIL`，程式結束碼也會設為 1。

## 專案結構

```text
leetcode_930/
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_930/
    ├── leetcode_930.csproj
    └── Program.cs
```

## 建置與執行

需求：已安裝 [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)。

在本專案根目錄執行：

```powershell
dotnet build .\leetcode_930\leetcode_930.csproj
dotnet run --no-build --project .\leetcode_930\leetcode_930.csproj
```

## 執行結果

```text
LeetCode 930 - Binary Subarrays With Sum

Case 1: nums = [1, 0, 1, 0, 1], goal = 2, expected = 4
  NumSubarraysWithSum  (Prefix Sum + Hash Map): 4 [PASS]
  NumSubarraysWithSum2 (Sliding Window)        : 4 [PASS]

Case 2: nums = [0, 0, 0, 0, 0], goal = 0, expected = 15
  NumSubarraysWithSum  (Prefix Sum + Hash Map): 15 [PASS]
  NumSubarraysWithSum2 (Sliding Window)        : 15 [PASS]

Case 3: nums = [1], goal = 0, expected = 0
  NumSubarraysWithSum  (Prefix Sum + Hash Map): 0 [PASS]
  NumSubarraysWithSum2 (Sliding Window)        : 0 [PASS]

Case 4: nums = [0, 1, 0, 1, 0], goal = 1, expected = 8
  NumSubarraysWithSum  (Prefix Sum + Hash Map): 8 [PASS]
  NumSubarraysWithSum2 (Sliding Window)        : 8 [PASS]

Overall: PASS
```
