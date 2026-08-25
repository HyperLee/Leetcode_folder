# LeetCode 3718：缺失的最小倍數

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![Language](https://img.shields.io/badge/Language-C%23-239120)

這是一個可直接執行的 .NET 10 Console 專案，示範三種方式解決 LeetCode 3718「Smallest Missing Multiple of K」。程式不需要輸入資料，執行後會用兩個官方範例與一個上界案例，同時驗證三種解法並輸出 PASS/FAIL。

## 快速導覽

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [三種解法比較](#三種解法比較)
- [解法一：HashSet 與加法枚舉](#解法一hashset-與加法枚舉)
- [解法二：固定大小布林陣列](#解法二固定大小布林陣列)
- [解法三：HashSet 與乘數枚舉](#解法三hashset-與乘數枚舉)
- [完整範例演示](#完整範例演示)
- [建置與執行](#建置與執行)

## 題目說明

給定整數陣列 `nums` 與正整數 `k`，請找出沒有出現在 `nums` 中的最小正整數 `k` 倍數。

正整數 `k` 倍數依序為：

```text
k, 2k, 3k, 4k, ...
```

我們要從這個序列中找出第一個不在 `nums` 裡的數字。

題目連結：

- [LeetCode 英文題目](https://leetcode.com/problems/smallest-missing-multiple-of-k/description/)
- [力扣中文題目](https://leetcode.cn/problems/smallest-missing-multiple-of-k/description/)

### 官方範例 1

```text
nums = [8, 2, 3, 4, 6]
k = 2
```

`2` 的正倍數為 `2, 4, 6, 8, 10, ...`。其中 `2`、`4`、`6`、`8` 都存在於陣列，第一個不存在的倍數是 `10`，所以答案為 `10`。

### 官方範例 2

```text
nums = [1, 4, 7, 10, 15]
k = 5
```

`5` 的正倍數為 `5, 10, 15, 20, ...`。第一個候選值 `5` 就沒有出現在陣列，因此答案為 `5`；不需要因為 `10` 和 `15` 存在而繼續往後搜尋。

## 限制條件

- `1 <= nums.length <= 100`
- `1 <= nums[i] <= 100`
- `1 <= k <= 100`

這些限制對解法二非常重要：因為 `nums[i]` 最大只會是 `100`，所以可以用長度 `101` 的布林陣列，直接用數值當索引。

## 解題概念與出發點

### 1. 不必尋找所有缺少的整數

題目只關心 `k` 的正倍數。即使數字 `1`、`3` 或 `7` 不在陣列，只要它不是 `k` 的倍數，就不會影響答案。

### 2. 候選值天然具有由小到大的順序

從 `k` 開始，每次增加 `k`，便會依序得到所有正倍數：

```text
k -> 2k -> 3k -> 4k -> ...
```

只要按順序檢查，遇到第一個不存在的候選值就可以立刻回傳。因為所有更小的正倍數都已經確認存在，所以這個候選值必然是「最小」缺失倍數。

### 3. 核心差異在於「如何判斷數字是否存在」

三種解法的搜尋順序相同，差別主要是資料結構與候選值的表示方式：

- 解法一用 `HashSet<int>` 判斷候選倍數是否存在，候選值以加法遞增。
- 解法二用 `bool[101]` 直接標記每個數字是否存在。
- 解法三同樣使用 `HashSet<int>`，但以 `k * multiplier` 計算候選倍數。

設：

- `n` 為 `nums` 的長度。
- `m` 為從 `k` 開始到答案為止，實際檢查的倍數數量。

因為 `nums[i] <= 100`，答案最多只需要越過 `100`，所以 `m` 的規模很小；即使如此，以下仍用一般化方式分析複雜度。

## 三種解法比較

| 解法 | 成員判斷方式 | 候選值產生方式 | 時間複雜度 | 額外空間 | 特點 |
| --- | --- | --- | --- | --- | --- |
| `MissingMultiple` | `HashSet<int>` | `multiple += k` | 平均 `O(n + m)` | `O(n)` | 直觀、容易套用到較大數值範圍 |
| `MissingMultiple2` | `bool[101]` | `multiple += k` | `O(n + m)` | `O(101)`，視為 `O(1)` | 利用題目上界，結構最簡單且沒有雜湊成本 |
| `MissingMultiple3` | `HashSet<int>` | `k * multiplier` | 平均 `O(n + m)` | `O(n)` | 明確表達目前檢查第幾個倍數 |

三種方法都不會修改輸入陣列。範例 runner 仍會為每個方法建立陣列複本，使各方法的測試彼此隔離，未來即使新增會排序或修改陣列的解法，也不會污染下一個方法的輸入。

## 解法一：HashSet 與加法枚舉

對應方法：`MissingMultiple(int[] nums, int k)`

### 設計步驟

1. 將 `nums` 的所有元素放入 `HashSet<int> seen`。
2. 將第一個候選值設為 `multiple = k`。
3. 如果 `seen` 包含 `multiple`，表示這個倍數尚未缺失，執行 `multiple += k`。
4. 重複檢查，直到找到第一個不在 `seen` 中的候選值。
5. 回傳該候選值。

核心程式概念如下：

```csharp
HashSet<int> seen = new HashSet<int>(nums);
int multiple = k;

while (seen.Contains(multiple))
{
    multiple += k;
}

return multiple;
```

### 為什麼正確

迴圈開始時，`multiple` 一定是正整數 `k` 的倍數。只有在目前候選值存在於 `nums` 時，程式才會前進到下一個更大的倍數。

當迴圈停止時：

- 目前的 `multiple` 不在 `nums` 中。
- 所有比它小、且屬於正整數 `k` 倍數的候選值都曾被檢查，並確認存在於 `nums`。

因此目前的 `multiple` 正是最小缺失正倍數。

### 複雜度

- 建立 HashSet：平均 `O(n)`。
- 檢查 `m` 個候選倍數：平均 `O(m)`。
- 總時間複雜度：平均 `O(n + m)`。
- 額外空間複雜度：`O(n)`。

### 範例演示：官方範例 1

`nums = [8, 2, 3, 4, 6]`，`k = 2`。

建立集合：

```text
seen = {2, 3, 4, 6, 8}
```

| 檢查順序 | `multiple` | 是否在 `seen` | 動作 |
| ---: | ---: | --- | --- |
| 1 | 2 | 是 | 加上 2，下一個候選值為 4 |
| 2 | 4 | 是 | 加上 2，下一個候選值為 6 |
| 3 | 6 | 是 | 加上 2，下一個候選值為 8 |
| 4 | 8 | 是 | 加上 2，下一個候選值為 10 |
| 5 | 10 | 否 | 停止並回傳 10 |

## 解法二：固定大小布林陣列

對應方法：`MissingMultiple2(int[] nums, int k)`

### 設計步驟

1. 建立 `bool[] exists = new bool[101]`。
2. 逐一讀取 `nums`，將 `exists[num]` 設為 `true`。
3. 從 `multiple = k` 開始檢查。
4. 當 `multiple <= 100` 且 `exists[multiple]` 為 `true` 時，將候選值增加 `k`。
5. 遇到未標記的候選值，或候選值已超過 `100` 時，直接回傳。

核心程式概念如下：

```csharp
bool[] exists = new bool[101];

foreach (int num in nums)
{
    exists[num] = true;
}

int multiple = k;

while (multiple <= 100 && exists[multiple])
{
    multiple += k;
}

return multiple;
```

### 為什麼需要 `multiple <= 100`

布林陣列的有效索引為 `0` 到 `100`。若候選值是 `101`，直接存取 `exists[101]` 會超出陣列範圍。

但題目已保證所有 `nums[i] <= 100`，因此候選值只要超過 `100`，就能確定它不可能出現在 `nums` 中。條件：

```csharp
multiple <= 100 && exists[multiple]
```

會先判斷上界；C# 的 `&&` 具有短路求值特性，當 `multiple <= 100` 為 `false` 時，不會再讀取 `exists[multiple]`。

### 為什麼正確

標記完成後，對所有合法輸入值 `x`，`exists[x]` 為 `true` 等價於 `x` 出現在 `nums` 中。

迴圈只會略過已出現的正倍數。若候選值未被標記，它就是第一個缺失倍數；若候選值超過 `100`，依題目上界也必然缺失。因為候選值由小到大檢查，所以回傳值一定最小。

### 複雜度

- 建立標記：`O(n)`。
- 檢查候選倍數：`O(m)`。
- 總時間複雜度：`O(n + m)`。
- 額外空間固定為 101 個布林值，因此是 `O(1)`。

### 範例演示：官方範例 2

`nums = [1, 4, 7, 10, 15]`，`k = 5`。

標記後的重要索引如下：

| 索引 | `exists[index]` | 說明 |
| ---: | --- | --- |
| 1 | `true` | 1 存在 |
| 4 | `true` | 4 存在 |
| 5 | `false` | 5 不存在 |
| 10 | `true` | 10 存在 |
| 15 | `true` | 15 存在 |

第一個候選值就是 `5`。雖然後面的 `10`、`15` 都存在，但題目要求最小缺失倍數，因此在 `exists[5] == false` 時立即回傳 `5`。

## 解法三：HashSet 與乘數枚舉

對應方法：`MissingMultiple3(int[] nums, int k)`

### 設計步驟

1. 將 `nums` 放入 `HashSet<int> seen`。
2. 將 `multiplier` 設為 `1`，代表先檢查第 1 個正倍數。
3. 以 `k * multiplier` 計算目前候選值。
4. 若候選值存在於 `seen`，將 `multiplier` 加 1。
5. 遇到第一個不存在的乘積時，回傳 `k * multiplier`。

核心程式概念如下：

```csharp
HashSet<int> seen = new HashSet<int>(nums);
int multiplier = 1;

while (seen.Contains(k * multiplier))
{
    multiplier++;
}

return k * multiplier;
```

### 與解法一的差異

解法一直接保存目前候選值，每次執行 `multiple += k`；解法三保存「第幾倍」，每次需要候選值時再計算 `k * multiplier`。

兩者檢查的數列完全相同：

```text
解法一：k, k + k, k + k + k, ...
解法三：k × 1, k × 2, k × 3, ...
```

因此解法三不是不同量級的演算法，而是另一種表達相同搜尋流程的方式。它的優點是 `multiplier` 能清楚表示目前正在檢查第幾個倍數。

### 為什麼正確

當 `multiplier = t` 時，候選值恰好是第 `t` 個正整數 `k` 倍數。乘數從 1 開始逐次增加，因此候選值嚴格由小到大。

迴圈只會略過存在於集合中的乘積，停止時的乘積不存在，而且所有更小的正倍數都已確認存在，所以回傳值是最小缺失正倍數。

### 複雜度

- 建立 HashSet：平均 `O(n)`。
- 檢查 `m` 個乘積：平均 `O(m)`。
- 總時間複雜度：平均 `O(n + m)`。
- 額外空間複雜度：`O(n)`。

### 範例演示：官方範例 1

| `multiplier` | `k * multiplier` | 是否在 `seen` | 動作 |
| ---: | ---: | --- | --- |
| 1 | 2 | 是 | `multiplier` 增加為 2 |
| 2 | 4 | 是 | `multiplier` 增加為 3 |
| 3 | 6 | 是 | `multiplier` 增加為 4 |
| 4 | 8 | 是 | `multiplier` 增加為 5 |
| 5 | 10 | 否 | 回傳 `2 * 5 = 10` |

## 完整範例演示

### 邊界案例：答案超過元素上限

範例 runner 使用：

```text
nums = [1, 2, 3, ..., 100]
k = 1
expected = 101
```

因為 `k = 1`，所有正整數都是 `k` 的倍數。`1` 到 `100` 全部存在，所以下一個候選值 `101` 才是最小缺失倍數。

三種方法的處理方式：

- 解法一的 HashSet 不包含 `101`，因此回傳 `101`。
- 解法二檢查到 `multiple = 101` 時，`multiple <= 100` 為 `false`，不會存取不存在的 `exists[101]`，直接回傳 `101`。
- 解法三在 `multiplier = 101` 時計算 `1 * 101`，集合不包含該值，因此回傳 `101`。

這個案例除了驗證答案能超過 `100`，也驗證了解法二的陣列上界判斷。

### Runner 驗證流程

每一組案例都依序呼叫：

1. `MissingMultiple`
2. `MissingMultiple2`
3. `MissingMultiple3`

每個實際結果都與人工指定的 `expected` 比較。三組案例乘以三種方法，總共有九次驗證；全部符合時，最後會輸出：

```text
總結：9/9 通過，0 個失敗。
```

## 建置與執行

### 環境需求

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

### 建置

在本 README 所在的專案根目錄執行：

```powershell
dotnet build .\leetcode_3718\leetcode_3718.csproj
```

### 執行

```powershell
dotnet run --project .\leetcode_3718\leetcode_3718.csproj
```

程式不需要命令列參數，也不需要互動輸入。

### 實際執行結果

以下內容來自本專案的實際 `dotnet run` 輸出：

```text
=== 3718. Smallest Missing Multiple of K ===
官方範例 1：nums = [8, 2, 3, 4, 6], k = 2, expected = 10
  MissingMultiple: actual = 10, PASS
  MissingMultiple2: actual = 10, PASS
  MissingMultiple3: actual = 10, PASS

官方範例 2：nums = [1, 4, 7, 10, 15], k = 5, expected = 5
  MissingMultiple: actual = 5, PASS
  MissingMultiple2: actual = 5, PASS
  MissingMultiple3: actual = 5, PASS

邊界案例：nums = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100], k = 1, expected = 101
  MissingMultiple: actual = 101, PASS
  MissingMultiple2: actual = 101, PASS
  MissingMultiple3: actual = 101, PASS

總結：9/9 通過，0 個失敗。
```

## 專案結構

```text
leetcode_3718/
├── .vscode/                  # VS Code 建置與直接執行設定
├── docs/
│   └── readme-template.md    # README 結構參考
├── leetcode_3718/
│   ├── leetcode_3718.csproj  # .NET 10 Console 專案
│   └── Program.cs            # 固定案例、三種解法與驗證器
└── README.md                 # 題目與解法教學文件
```
