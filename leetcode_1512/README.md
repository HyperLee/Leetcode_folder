# 1512. Number of Good Pairs／好數對的數目

計算所有索引 `i < j` 且 `nums[i] == nums[j]` 的配對。本專案保留一個清楚的
雙迴圈解法，以及一個利用出現次數的線性時間解法，兩者都是不改動輸入的純函式。

- [LeetCode English](https://leetcode.com/problems/number-of-good-pairs/)
- [LeetCode 中文](https://leetcode.cn/problems/number-of-good-pairs/)

## 題目說明

給定整數陣列 `nums`，一組索引 `(i, j)` 在 `i < j` 且兩個位置的值相同時，是一組
好數對。回傳好數對總數。

## 限制條件

- `1 <= nums.Length <= 100`
- `1 <= nums[i] <= 100`
- 公開 API 只處理 LeetCode 定義的有效輸入，不額外定義無效輸入行為

## 解法與取捨

### 1. `NumIdenticalPairs`：雙迴圈枚舉

固定左端索引 `i` 後，讓 `j` 從 `i + 1` 走到結尾；這保證每一對索引只被檢查一次。
當兩值相等便累加答案。它最直接地對應題目定義，適合用來理解配對本身。

| 指標 | 複雜度 |
| --- | --- |
| 時間 | `O(n^2)` |
| 結果空間 | `O(1)`，只回傳一個整數 |
| 輔助空間 | `O(1)` |

### 2. `NumIdenticalPairs2`：字典累積先前出現次數

字典記錄每個值在目前索引之前出現了幾次。讀到值 `x` 時，若 `x` 已出現 `c` 次，
目前索引會立刻與那 `c` 個位置各形成一組新配對；因此先將 `c` 加入答案，再將記錄
更新為 `c + 1`。相較雙迴圈，這是較適合較長輸入的線性掃描。

| 指標 | 複雜度 |
| --- | --- |
| 時間 | `O(n)` |
| 結果空間 | `O(1)`，只回傳一個整數 |
| 輔助空間 | `O(k)`，`k` 是不同值的數量 |

兩種方法均不輸出資料，也不寫入 `nums`；harness 會分別提供獨立陣列並檢查呼叫後內容
完全相同。

## 逐步範例

以 `nums = [1, 2, 1, 2, 1]` 為例，字典法的累積過程如下：

| 讀入值 | 讀入前的出現次數 | 新增配對 | 累計答案 | 更新後次數 |
| --- | ---: | ---: | ---: | ---: |
| `1` | 0 | 0 | 0 | 1 |
| `2` | 0 | 0 | 0 | 1 |
| `1` | 1 | 1 | 1 | 2 |
| `2` | 1 | 1 | 2 | 2 |
| `1` | 2 | 2 | 4 | 3 |

最終答案為 `4`。這也涵蓋非相鄰的重複值，避免只比較相鄰元素的錯誤實作。

## Acceptance Harness

`Main` 提供九個確定性案例。每案建立兩份獨立輸入，只有兩個方法的實際結果都符合
預期、且兩份輸入都逐元素保持不變時才會 PASS。專案沒有正式測試專案；此 executable
harness 是可重複執行的驗收機制。任何失敗都會將 process exit code 設為 1。

| # | 案例 | 輸入 | 預期 |
| --- | --- | --- | ---: |
| 1 | 官網範例 1 | `[1, 2, 3, 1, 1, 3]` | 4 |
| 2 | 官網範例 2 | `[1, 1, 1, 1]` | 6 |
| 3 | 官網範例 3 | `[1, 2, 3]` | 0 |
| 4 | 最小輸入 | `[1]` | 0 |
| 5 | 兩元素配對 | `[5, 5]` | 1 |
| 6 | 非相鄰重複 | `[1, 2, 1, 2, 1]` | 4 |
| 7 | 值邊界 | `[1, 100, 1, 100]` | 2 |
| 8 | 混合頻率 | `[1, 1, 2, 2, 2, 3, 3, 3, 3]` | 10 |
| 9 | 最大長度 | 100 個值為 `7` 的元素 | 4950 |

## 建置與執行

從 repository 根目錄執行：

```bash
dotnet build leetcode_1512/leetcode_1512/leetcode_1512.csproj --nologo
dotnet run --no-build --project leetcode_1512/leetcode_1512/leetcode_1512.csproj
```

若直接以 `leetcode_1512/` 作為 workspace，從題目根目錄執行：

```bash
dotnet build leetcode_1512/leetcode_1512.csproj --nologo
dotnet run --no-build --project leetcode_1512/leetcode_1512.csproj
```

以下為 fresh run 的完整輸出：

```text
Case: Official example 1
Input: [1, 2, 3, 1, 1, 3]
Expected: 4
NumIdenticalPairs: 4
NumIdenticalPairs2: 4
Input preserved: True
Result: PASS

Case: Official example 2
Input: [1, 1, 1, 1]
Expected: 6
NumIdenticalPairs: 6
NumIdenticalPairs2: 6
Input preserved: True
Result: PASS

Case: Official example 3
Input: [1, 2, 3]
Expected: 0
NumIdenticalPairs: 0
NumIdenticalPairs2: 0
Input preserved: True
Result: PASS

Case: Minimum input
Input: [1]
Expected: 0
NumIdenticalPairs: 0
NumIdenticalPairs2: 0
Input preserved: True
Result: PASS

Case: Two-element pair
Input: [5, 5]
Expected: 1
NumIdenticalPairs: 1
NumIdenticalPairs2: 1
Input preserved: True
Result: PASS

Case: Non-adjacent duplicates
Input: [1, 2, 1, 2, 1]
Expected: 4
NumIdenticalPairs: 4
NumIdenticalPairs2: 4
Input preserved: True
Result: PASS

Case: Value boundaries
Input: [1, 100, 1, 100]
Expected: 2
NumIdenticalPairs: 2
NumIdenticalPairs2: 2
Input preserved: True
Result: PASS

Case: Mixed frequencies
Input: [1, 1, 2, 2, 2, 3, 3, 3, 3]
Expected: 10
NumIdenticalPairs: 10
NumIdenticalPairs2: 10
Input preserved: True
Result: PASS

Case: Maximum length
Input: [7 × 100]
Expected: 4950
NumIdenticalPairs: 4950
NumIdenticalPairs2: 4950
Input preserved: True
Result: PASS

Summary: 9/9 checks passed.
```

## 專案結構

```plaintext
leetcode_1512/
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── AGENTS.md
├── README.md
├── docs/
│   ├── readme-template.md
│   └── superpowers/
│       ├── plans/2026-07-18-leetcode-1512-net10-migration.md
│       └── specs/2026-07-18-leetcode-1512-net10-migration-design.md
└── leetcode_1512/
    ├── Program.cs
    └── leetcode_1512.csproj
```
