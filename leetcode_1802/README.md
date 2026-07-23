# LeetCode 1802 - Maximum Value at a Given Index in a Bounded Array（有界陣列中指定索引處的最大值）

- [LeetCode English](https://leetcode.com/problems/maximum-value-at-a-given-index-in-a-bounded-array/)
- [LeetCode 繁體中文](https://leetcode.cn/problems/maximum-value-at-a-given-index-in-a-bounded-array/)

## 題目說明

給定正整數 `n`、`index` 與 `maxSum`，要建立一個長度為 `n` 的正整數陣列。相鄰元素
差的絕對值不得超過 1，所有元素總和不得超過 `maxSum`；目標是讓 `nums[index]`
盡可能大，並回傳該最大值。

## 限制條件與 API

- `1 <= n <= maxSum <= 10^9`
- `0 <= index < n`

```csharp
public static int MaxValue(int n, int index, int maxSum);
```

## 設計、不變量與取捨

若固定 `nums[index] = peak`，要讓總和最小，左右兩側都應從 `peak - 1` 開始每格下降
1；降到正整數下限 1 後，其餘位置都填 1。這個最小山形總和若仍超過 `maxSum`，
`peak` 必定不可行；若不超過預算，剩餘預算可加到其他位置而不影響峰值的可行性。

可行性對 `peak` 具有單調性，因此使用上界二分搜尋：

- 候選峰值可行時，把左界提高到候選值。
- 候選峰值不可行時，把右界降到候選值減 1。
- 中點採上取整，確保只剩兩個候選時仍會向右推進。

單側總和不建立實際陣列，而是分成兩種等差級數：

1. 側邊長度不足以降到 1：直接計算 `peak - 1` 到 `peak - length` 的總和。
2. 已降到 1：計算 `peak - 1` 到 1，再為剩餘位置補 1。

所有乘法與總和使用 `long`，避免限制上限下的 32 位元溢位。

- 時間複雜度：`O(log maxSum)`。
- 結果空間：`O(1)`。
- 輔助空間：`O(1)`。

## 走查

以 `n = 4`、`index = 2`、`maxSum = 6` 為例：

```plaintext
peak = 2：最小山形可為 [1,1,2,1]，總和 5 <= 6，因此可行。
peak = 3：最小山形為 [1,2,3,2]，總和 8 > 6，因此不可行。
最大可行峰值為 2。
```

## Acceptance harness

| Case | 輸入 | Expected | 驗證重點 |
| --- | --- | ---: | --- |
| Official example 1 | `(4,2,6)` | 2 | 第一個官方範例。 |
| Official example 2 | `(6,1,10)` | 3 | 第二個官方範例與不對稱側長。 |
| Minimum valid input | `(1,0,1)` | 1 | 最小有效輸入。 |
| Single element maximum budget | `(1,0,10^9)` | `10^9` | 單元素與最大預算。 |
| Peak at left boundary | `(4,0,7)` | 3 | 峰值位於左端。 |
| Peak at right boundary | `(4,3,7)` | 3 | 峰值位於右端及對稱性。 |
| Both sides reach one | `(5,2,10)` | 3 | 兩側降到 1 後補平。 |
| Tight adjacent boundary | `(2,0,3)` | 2 | 兩元素與緊密預算。 |
| Large arithmetic-series sum | `(3,1,10^9)` | 333333334 | 大數乘法與總和不得溢位。 |
| Maximum length minimum budget | `(10^9,500000000,10^9)` | 1 | 最大長度不可建立實際陣列。 |

## 建置與執行

以下命令的工作目錄是 `leetcode_1802/` 題目根目錄：

```bash
dotnet build leetcode_1802/leetcode_1802.csproj --nologo
dotnet run --no-build --project leetcode_1802/leetcode_1802.csproj
```

本題沒有正式測試專案；`Main` 中的 deterministic acceptance harness 是可重複執行的
驗證入口。以下為 fresh run 的完整輸出：

```text
Case: Official example 1
Input: n=4, index=2, maxSum=6
Expected: 2
Actual: 2
Result: PASS

Case: Official example 2
Input: n=6, index=1, maxSum=10
Expected: 3
Actual: 3
Result: PASS

Case: Minimum valid input
Input: n=1, index=0, maxSum=1
Expected: 1
Actual: 1
Result: PASS

Case: Single element maximum budget
Input: n=1, index=0, maxSum=1000000000
Expected: 1000000000
Actual: 1000000000
Result: PASS

Case: Peak at left boundary
Input: n=4, index=0, maxSum=7
Expected: 3
Actual: 3
Result: PASS

Case: Peak at right boundary
Input: n=4, index=3, maxSum=7
Expected: 3
Actual: 3
Result: PASS

Case: Both sides reach one
Input: n=5, index=2, maxSum=10
Expected: 3
Actual: 3
Result: PASS

Case: Tight adjacent boundary
Input: n=2, index=0, maxSum=3
Expected: 2
Actual: 2
Result: PASS

Case: Large arithmetic-series sum
Input: n=3, index=1, maxSum=1000000000
Expected: 333333334
Actual: 333333334
Result: PASS

Case: Maximum length minimum budget
Input: n=1000000000, index=500000000, maxSum=1000000000
Expected: 1
Actual: 1
Result: PASS

Summary: 10/10 checks passed.
```

## 最終專案結構

```plaintext
leetcode_1802/
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
└── leetcode_1802/
    ├── Program.cs
    └── leetcode_1802.csproj
```
