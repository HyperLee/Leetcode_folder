# 1508. Range Sum of Sorted Subarray Sums／子陣列和排序後的區間總和

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)

給定正整數陣列，列出所有非空連續子陣列的總和、排序後，再加總指定排名區間。
本專案保留直觀的列舉排序解法，並提供二分搜尋搭配滑動視窗的進階解法作為比較。

- [LeetCode English](https://leetcode.com/problems/range-sum-of-sorted-subarray-sums/)
- [LeetCode 中文](https://leetcode.cn/problems/range-sum-of-sorted-subarray-sums/)

## 題目說明

`nums` 包含 `n` 個正整數。對每一個非空連續子陣列計算元素總和，可得到
`n * (n + 1) / 2` 個數字。將這些數字依非遞減順序排序後，回傳第 `left` 個到第
`right` 個數字的總和；`left` 與 `right` 都從 1 起算且包含兩端。

答案可能很大，需對 `1_000_000_007` 取模。

## 限制條件

- `n == nums.Length`
- `1 <= nums.Length <= 1000`
- `1 <= nums[i] <= 100`
- `1 <= left <= right <= n * (n + 1) / 2`
- 兩個公開 API 都只處理 LeetCode 定義的有效輸入，不額外定義無效輸入行為
- 兩個公開 API 都不修改 `nums`

## 解題概念與出發點

題目要求的是「排序後的子陣列和」排名區間，不是原陣列索引區間。因此最直接的想法是
完整產生所有子陣列和後排序。這個方法容易理解，也正是 `RangeSum` 的設計。

若不希望配置 `O(n²)` 的陣列，可以把區間答案改寫為前綴排名總和：

```plaintext
answer(left, right) = firstK(right) - firstK(left - 1)
```

接著利用元素全為正整數的條件，以二分搜尋找出第 `k` 小子陣列和的數值門檻；每次測試
門檻時，再用滑動視窗同時計算符合門檻的子陣列數量與總和。這就是 `RangeSum2`。

## 解法一：`RangeSum` 列舉並排序

### 設計

1. 子陣列總數是 `n * (n + 1) / 2`，先配置同樣長度的 `sums`。
2. 固定左端點 `i`，讓右端點 `j` 從 `i` 移到陣列尾端。
3. 右端點每前進一步，只需把 `nums[j]` 加入目前總和，不必重新掃描子陣列。
4. 排序 `sums`。
5. 將 1-based 的 `[left, right]` 轉成 0-based 索引後逐項加總並取模。

此方法只排序新建立的 `sums`，不會排序或改寫 `nums`。

### 複雜度

令 `m = n * (n + 1) / 2`：

| 指標 | 複雜度 |
| --- | --- |
| 時間 | `O(n² + m log m)`，可簡寫為 `O(n² log n)` |
| 輔助空間 | `O(m)`，保存全部子陣列和 |
| 結果空間 | `O(1)`，只回傳一個整數 |

### 範例演示

以 `nums = [1, 2, 3, 4]`、`left = 1`、`right = 5` 為例：

| 固定左端點 | 依序產生的子陣列和 |
| --- | --- |
| 0 | `1, 3, 6, 10` |
| 1 | `2, 5, 9` |
| 2 | `3, 7` |
| 3 | `4` |

排序後為 `[1, 2, 3, 3, 4, 5, 6, 7, 9, 10]`。前五項總和是
`1 + 2 + 3 + 3 + 4 = 13`。

## 解法二：`RangeSum2` 二分搜尋與滑動視窗

### 1. 將排名區間改成兩個前綴問題

`SumOfFirstK(k)` 表示排序後最小的前 `k` 個子陣列和之總和，因此：

```plaintext
RangeSum2(left, right) = SumOfFirstK(right) - SumOfFirstK(left - 1)
```

當 `k = 0` 時直接回傳 0，讓 `left = 1` 不需要額外分支。

### 2. 二分搜尋第 k 小數值門檻

子陣列和的最小可能值至少為 1，最大值不超過整個陣列總和。實作以 `[0, sum(nums)]`
作為搜尋範圍，找出最小門檻 `limit`，使得「總和不大於 `limit` 的子陣列數量」至少為
`k`。門檻越大，符合條件的數量只會增加，所以具有二分搜尋所需的單調性。

### 3. 一次滑動視窗同時計數與加總

`CountAndSumAtMost(limit)` 掃描每個右端點，維護：

- `windowSum`：目前合法視窗的元素總和。
- `endingSums`：目前所有以右端點結束、且起點位於合法視窗內的子陣列和總和。
- `count`：目前找到的合法子陣列數量。
- `total`：所有合法子陣列和的總和。

加入 `nums[right]` 時，既有的每一個結尾子陣列都多出這個值，並新增只包含右端點的
子陣列，因此更新式為：

```plaintext
endingSums += nums[right] * (right - windowLeft + 1)
```

若 `windowSum > limit`，便持續右移左界。移除舊左界前，`windowSum` 正好是從該左界
延伸到目前右端點的子陣列和，所以也要從 `endingSums` 扣除。因為所有元素皆為正數，
右移左界一定會讓總和下降，整次統計只需 `O(n)`。

### 4. 修正門檻上的重複值

最小可行門檻可能一次涵蓋多個相同子陣列和。若門檻統計得到 `count > k`，多出的項目
一定都等於 `limit`，所以前 `k` 小總和為：

```plaintext
thresholdTotal - (thresholdCount - k) * limit
```

例如 `nums = [1, 1, 1]` 的排序結果是 `[1, 1, 1, 2, 2, 3]`：

- `SumOfFirstK(5)` 的門檻是 2，`count = 5`、`total = 7`，結果為 7。
- `SumOfFirstK(1)` 的門檻是 1，`count = 3`、`total = 3`，修正後為
  `3 - (3 - 1) * 1 = 1`。
- 因此排名 `2..5` 的答案是 `7 - 1 = 6`。

### 完整範例演示

對 `[1, 2, 3, 4]` 求前五小總和時，二分搜尋依序縮小門檻：

| 門檻 | 不大於門檻的數量 | 決策 |
| ---: | ---: | --- |
| 5 | 6 | 數量足夠，縮小上界 |
| 2 | 2 | 數量不足，提高下界 |
| 4 | 5 | 數量足夠，門檻收斂為 4 |
| 3 | 4 | 數量不足，確認最小門檻是 4 |

門檻 4 涵蓋 `[1, 2, 3, 3, 4]`，數量正好是 5、總和是 13，因此
`SumOfFirstK(5) = 13`。

### 複雜度

令 `S = sum(nums)`：

| 指標 | 複雜度 |
| --- | --- |
| 時間 | `O(n log S)`；區間答案需執行兩次前綴排名計算，漸進複雜度不變 |
| 輔助空間 | `O(1)` |
| 結果空間 | `O(1)` |

所有計數與未取模總和都使用 `long`，避免最大限制案例的中間值溢位。

## 解法比較

| 特性 | `RangeSum` | `RangeSum2` |
| --- | --- | --- |
| 核心概念 | 列出全部子陣列和後排序 | 二分數值門檻並用滑動視窗統計 |
| 易讀性 | 最直接，容易對照題意 | 推導較多，需理解排名前綴與門檻修正 |
| 時間 | `O(n² log n)` | `O(n log S)` |
| 輔助空間 | `O(n²)` | `O(1)` |
| 是否修改輸入 | 否 | 否 |

## Acceptance Harness

專案沒有獨立測試專案；`Main` 是可重複執行的驗收機制。每個案例會用兩份獨立陣列
執行兩個公開 API，只有兩個答案都符合預期且輸入內容都保持不變時才會 PASS。任一案例
失敗時，程式會將 process exit code 設為 1。

| # | 案例 | 輸入與排名區間 | 預期 |
| --- | --- | --- | ---: |
| 1 | 官方案例 1 | `[1, 2, 3, 4]`, `1..5` | 13 |
| 2 | 官方案例 2 | `[1, 2, 3, 4]`, `3..4` | 6 |
| 3 | 官方案例 3 | `[1, 2, 3, 4]`, `1..10` | 50 |
| 4 | 最小輸入 | `[7]`, `1..1` | 7 |
| 5 | 重複子陣列和 | `[1, 1, 1]`, `2..5` | 6 |
| 6 | 中段排名範圍 | `[2, 1, 3]`, `2..5` | 12 |
| 7 | 最大限制與 modulo | 1000 個 `100`, `1..500500` | 716699888 |

## 建置與執行

從 `leetcode_1508` repository 根目錄執行：

```bash
dotnet restore leetcode_1508/leetcode_1508.csproj
dotnet build leetcode_1508/leetcode_1508.csproj --no-restore --nologo
dotnet run --no-build --project leetcode_1508/leetcode_1508.csproj
```

以下為 fresh run 的完整輸出：

```text
Case: Official example 1
Input: nums = [1, 2, 3, 4]
Range: left = 1, right = 5
Expected: 13
RangeSum: 13
RangeSum2: 13
Input preserved: True
Result: PASS

Case: Official example 2
Input: nums = [1, 2, 3, 4]
Range: left = 3, right = 4
Expected: 6
RangeSum: 6
RangeSum2: 6
Input preserved: True
Result: PASS

Case: Official example 3
Input: nums = [1, 2, 3, 4]
Range: left = 1, right = 10
Expected: 50
RangeSum: 50
RangeSum2: 50
Input preserved: True
Result: PASS

Case: Minimum input
Input: nums = [7]
Range: left = 1, right = 1
Expected: 7
RangeSum: 7
RangeSum2: 7
Input preserved: True
Result: PASS

Case: Duplicate subarray sums
Input: nums = [1, 1, 1]
Range: left = 2, right = 5
Expected: 6
RangeSum: 6
RangeSum2: 6
Input preserved: True
Result: PASS

Case: Middle rank range
Input: nums = [2, 1, 3]
Range: left = 2, right = 5
Expected: 12
RangeSum: 12
RangeSum2: 12
Input preserved: True
Result: PASS

Case: Maximum constraints and modulo
Input: nums = [100 x 1000]
Range: left = 1, right = 500500
Expected: 716699888
RangeSum: 716699888
RangeSum2: 716699888
Input preserved: True
Result: PASS

Summary: 7/7 checks passed.
```

## 專案結構

```plaintext
leetcode_1508/
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── AGENTS.md
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_1508.sln
└── leetcode_1508/
    ├── Program.cs
    └── leetcode_1508.csproj
```