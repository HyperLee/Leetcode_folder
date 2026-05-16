# LeetCode 45. Jump Game II

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)

這個專案使用 C#/.NET 10 實作 LeetCode 45「Jump Game II」。目標是在保證可抵達最後一格的前提下，計算從索引 `0` 到索引 `n - 1` 所需的最少跳躍次數。

## 題目說明

給定一個 0-indexed 整數陣列 `nums`，一開始位於索引 `0`。每個 `nums[i]` 代表從索引 `i` 最多可以向前跳幾格，也就是可以跳到 `i + j`，其中：

- `0 <= j <= nums[i]`
- `i + j < nums.Length`

請回傳抵達最後一個索引所需的最少跳躍次數。題目保證最後一格一定可達。

## 限制條件

根據題目限制：

- `1 <= nums.length <= 10^4`
- `0 <= nums[i] <= 1000`
- 保證可以抵達 `nums[n - 1]`

參考來源：[LeetCode Wiki - 45. Jump Game II](https://leetcode.doocs.org/en/lc/45/)

## 解題概念

這題的核心是貪婪選擇。每次跳躍不需要真的枚舉所有路徑，而是找出「下一步可以讓後續涵蓋範圍最大」的位置。專案保留兩種寫法：

- `Jump`: 反向貪婪，從終點往前回推可抵達目前目標的最左索引。
- `Jump2`: 正向貪婪，從起點往右掃描目前跳躍區間，並更新下一層可抵達的最遠邊界。

## 解法設計

### 解法一：反向貪婪 `Jump`

從最後一個索引開始，尋找最左側且能跳到目前目標 `position` 的索引 `i`。找到後，把 `position` 更新為 `i`，代表最少路徑中的前一步可以站在這裡。重複回推到索引 `0`，更新次數就是最少跳躍次數。

- 時間複雜度：`O(n^2)`
- 空間複雜度：`O(1)`

範例 `nums = [2,3,1,1,4]`：

| 回合 | 目前目標 | 最左可抵達目標的位置 | 跳躍次數 |
| --- | --- | --- | --- |
| 1 | index `4` | index `1`，因為 `1 + nums[1] = 4` | 1 |
| 2 | index `1` | index `0`，因為 `0 + nums[0] = 2` | 2 |

結果為 `2`。

### 解法二：正向貪婪 `Jump2`

把每次跳躍可涵蓋的索引視為一層。掃描目前層時，用 `maxPosition` 記錄下一層能抵達的最遠位置；當索引走到目前層邊界 `end`，代表必須跳一次，並把邊界更新為 `maxPosition`。

- 時間複雜度：`O(n)`
- 空間複雜度：`O(1)`

範例 `nums = [2,3,1,1,4]`：

| 掃描位置 | 當前值 | 下一層最遠位置 | 是否到達邊界 | 跳躍次數 |
| --- | --- | --- | --- | --- |
| index `0` | `2` | `2` | 是，邊界更新為 `2` | 1 |
| index `1` | `3` | `4` | 否 | 1 |
| index `2` | `1` | `4` | 是，邊界更新為 `4` | 2 |

結果為 `2`。

## 執行方式

建置專案：

```bash
dotnet build leetcode_045/leetcode_045.csproj
```

執行範例資料：

```bash
dotnet run --project leetcode_045/leetcode_045.csproj
```

程式輸出會包含每組範例的 `PASS`/`FAIL` 狀態：

```text
PASS nums=[2,3,1,1,4], expected=2, Jump=2, Jump2=2
PASS nums=[2,3,0,1,4], expected=2, Jump=2, Jump2=2
PASS nums=[0], expected=0, Jump=0, Jump2=0
PASS nums=[1,1,1,1], expected=3, Jump=3, Jump2=3
```

> [!NOTE]
> 目前沒有獨立測試專案；範例驗證資料放在 `Main` 進入點中。若本機 .NET SDK 先輸出平台相關警告，請以程式列出的 `PASS` 結果為準。

檢查差異是否有多餘空白：

```bash
git diff --check
```

## 專案結構

```text
.
├── README.md
├── docs
│   └── readme-template.md
└── leetcode_045
    ├── Program.cs
    └── leetcode_045.csproj
```
