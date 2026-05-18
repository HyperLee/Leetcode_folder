# LeetCode 55. Jump Game

這個專案使用 C# / .NET console app 解 LeetCode 55「Jump Game」。題目要求判斷從陣列第一個位置出發，依照每格提供的最大跳躍長度，是否能抵達最後一個位置。

## 題目說明

給定一個整數陣列 `nums`：

- 初始位置在索引 `0`。
- `nums[i]` 代表站在索引 `i` 時，最多可以往右跳幾格。
- 如果能抵達最後一個索引，回傳 `true`；否則回傳 `false`。

範例：

```text
Input: nums = [2,3,1,1,4]
Output: true
原因：從索引 0 跳到索引 1，再從索引 1 跳到最後一格。

Input: nums = [3,2,1,0,4]
Output: false
原因：無論怎麼跳都會卡在索引 3，無法跨到索引 4。
```

## 限制條件

- `1 <= nums.length <= 10^4`
- `0 <= nums[i] <= 10^5`

專案中的 `CanJump2` 對空陣列額外回傳 `false`，這是保護性處理；LeetCode 原題輸入不會出現空陣列。

## 解題概念與出發點

本題不需要真的列舉每一條跳躍路徑。只要知道「目前為止能到達的最遠索引」即可。

核心觀察：

- 若目前索引 `i` 在最遠可達範圍內，代表可以站上這一格。
- 站上索引 `i` 後，新的可達範圍可能延伸到 `i + nums[i]`。
- 如果最遠可達範圍已經碰到或超過最後一格，就能回傳 `true`。
- 如果掃描到某個索引 `i` 時，`i` 已經大於目前最遠可達索引，代表中間有斷點，後面再大的跳躍長度也用不到。

因此兩種解法都採用正向貪心，時間複雜度是 `O(n)`，空間複雜度是 `O(1)`。

## 解法一：`CanJump`

`CanJump` 使用 `rightMost` 記錄目前能抵達的最遠索引。

設計流程：

1. 初始化 `rightMost = 0`，表示一開始只能保證站在索引 `0`。
2. 從左到右掃描每個索引 `i`。
3. 只有當 `i <= rightMost` 時，索引 `i` 才是可達位置，可以用它更新最遠距離。
4. 更新方式為 `rightMost = Math.Max(rightMost, i + nums[i])`。
5. 當 `rightMost >= nums.Length - 1`，代表終點已在可達範圍內，直接回傳 `true`。
6. 若迴圈結束仍無法覆蓋終點，回傳 `false`。

### 範例演示：`[2,3,1,1,4]`

| 索引 `i` | `nums[i]` | 掃描前 `rightMost` | 是否可達 | 掃描後 `rightMost` |
| --- | ---: | ---: | --- | ---: |
| 0 | 2 | 0 | 是 | 2 |
| 1 | 3 | 2 | 是 | 4 |

當 `rightMost` 更新為 `4` 時，已經到達最後索引，所以回傳 `true`。

### 範例演示：`[3,2,1,0,4]`

| 索引 `i` | `nums[i]` | 掃描前 `rightMost` | 是否可達 | 掃描後 `rightMost` |
| --- | ---: | ---: | --- | ---: |
| 0 | 3 | 0 | 是 | 3 |
| 1 | 2 | 3 | 是 | 3 |
| 2 | 1 | 3 | 是 | 3 |
| 3 | 0 | 3 | 是 | 3 |
| 4 | 4 | 3 | 否 | 3 |

索引 `4` 大於 `rightMost`，表示無法抵達最後一格，回傳 `false`。

## 解法二：`CanJump2`

`CanJump2` 是同一個貪心概念的另一種寫法，使用 `max` 表示目前所有可達位置能延伸出的最遠索引。

設計流程：

1. 若輸入陣列長度為 `0`，直接回傳 `false`。
2. 初始化 `max = 0`。
3. 從左到右掃描每個索引 `i`。
4. 若 `i > max`，代表索引 `i` 已不可達，直接回傳 `false`。
5. 若索引 `i` 可達，更新 `max = Math.Max(max, i + nums[i])`。
6. 若 `max >= nums.Length - 1`，代表終點已可達，直接回傳 `true`。
7. 掃描結束仍未失敗時，回傳 `true`。

### 範例演示：`[2,3,1,1,4]`

| 索引 `i` | `nums[i]` | 掃描前 `max` | 判斷 | 掃描後 `max` |
| --- | ---: | ---: | --- | ---: |
| 0 | 2 | 0 | `0 <= 0`，可達 | 2 |
| 1 | 3 | 2 | `1 <= 2`，可達 | 4 |

`max` 更新為 `4` 後覆蓋最後索引，因此回傳 `true`。

### 範例演示：`[3,2,1,0,4]`

| 索引 `i` | `nums[i]` | 掃描前 `max` | 判斷 | 掃描後 `max` |
| --- | ---: | ---: | --- | ---: |
| 0 | 3 | 0 | `0 <= 0`，可達 | 3 |
| 1 | 2 | 3 | `1 <= 3`，可達 | 3 |
| 2 | 1 | 3 | `2 <= 3`，可達 | 3 |
| 3 | 0 | 3 | `3 <= 3`，可達 | 3 |
| 4 | 4 | 3 | `4 > 3`，不可達 | 3 |

掃描到索引 `4` 時已超出 `max`，所以回傳 `false`。

## 執行方式

需求：

- .NET SDK 10.0 或更新版本

建置：

```powershell
dotnet build leetcode_055\leetcode_055.csproj
```

執行範例：

```powershell
dotnet run --project leetcode_055\leetcode_055.csproj
```

範例輸出摘要：

```text
LeetCode 55. Jump Game

nums = [2, 3, 1, 1, 4], expected = True
  CanJump : True (PASS)
  CanJump2: True (PASS)

nums = [3, 2, 1, 0, 4], expected = False
  CanJump : False (PASS)
  CanJump2: False (PASS)
```

## 專案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_055/
    ├── Program.cs
    └── leetcode_055.csproj
```
