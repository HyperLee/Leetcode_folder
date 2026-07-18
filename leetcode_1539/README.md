# 1539. Kth Missing Positive Number／第 k 個缺失的正整數

給定嚴格遞增的正整數陣列，找出不在陣列中的第 `k` 個正整數。本專案保留直觀的逐一
枚舉，並新增效率更高的二分搜尋；兩個公開 API 都不輸出且不修改輸入。

- [LeetCode English](https://leetcode.com/problems/kth-missing-positive-number/)
- [LeetCode 中文](https://leetcode.cn/problems/kth-missing-positive-number/)

## 題目說明與限制條件

給定嚴格遞增的正整數陣列 `arr` 與正整數 `k`，回傳陣列中缺失的第 `k` 個正整數。

- `1 <= arr.Length <= 1,000`
- `1 <= arr[i] <= 1,000`
- `1 <= k <= 1,000`
- `arr[i] < arr[j]`，其中 `i < j`
- `FindKthPositive` 與 `FindKthPositive2` 只處理題目保證的有效輸入

## 解法一：逐一枚舉

`FindKthPositive` 使用 `current`、`arrayIndex` 與 `missingCount`。若 `current` 等於目前
陣列元素，就只推進陣列索引；否則將它計為下一個缺失值。當缺失數量到達 `k`，回傳剛
檢查完的正整數。

不變量是：進入每輪迴圈時，小於 `current` 的正整數都已完成分類，而 `arrayIndex` 永遠
指向第一個尚未處理的陣列元素。因此候選值與索引都只向前移動，不會漏算或重複計算。

以 `arr = [2, 3, 4, 7, 11]`、`k = 5` 為例，缺失值依序為 `1、5、6、8、9`，所以答案
是 `9`。

## 解法二：二分搜尋

`FindKthPositive2` 不逐一枚舉答案，而是在陣列索引上尋找缺失數量的分界。對索引 `i`
而言，`arr[i]` 前理應出現 `i + 1` 個正整數，因此截至該位置的缺失數量是：

```plaintext
missing(i) = arr[i] - (i + 1) = arr[i] - i - 1
```

使用半開區間 `[left, right)` 尋找第一個 `missing(i) >= k` 的索引。若中點缺失數量仍
小於 `k`，答案一定在右側；否則中點仍可能是第一個符合位置，因此保留它並收縮右界。
搜尋結束時，`left` 代表答案前存在於陣列中的元素數量，故答案為 `left + k`。

### 二分走查

以 `arr = [2, 3, 4, 7, 11]`、`k = 5` 為例：

| 區間 | `middle` | `arr[middle] - middle - 1` | 更新 |
| --- | ---: | ---: | --- |
| `[0, 5)` | 2 | `4 - 2 - 1 = 1` | `1 < 5`，令 `left = 3` |
| `[3, 5)` | 4 | `11 - 4 - 1 = 6` | `6 >= 5`，令 `right = 4` |
| `[3, 4)` | 3 | `7 - 3 - 1 = 3` | `3 < 5`，令 `left = 4` |

最後 `left = right = 4`，所以答案為 `left + k = 4 + 5 = 9`。

## 複雜度與取捨

| 方法 | 時間 | 結果空間 | 輔助空間 | 特點 |
| --- | --- | --- | --- | --- |
| `FindKthPositive` | `O(n + k)` | `O(1)` | `O(1)` | 流程直觀，直接列舉缺失值 |
| `FindKthPositive2` | `O(log n)` | `O(1)` | `O(1)` | 利用缺失數量單調性，較有效率 |

枚舉法適合說明題意與指標推進；二分法則利用 `missing(i)` 隨索引單調不減的性質，避免
逐一檢查正整數。兩者都只讀取 `arr`。

## Acceptance Harness

`Main` 提供九個確定性案例，對兩個解法各使用獨立輸入 clone。每次呼叫分別檢查答案與
輸入保存，因此共有 `9 × 2 × 2 = 36` 項檢查；任何失敗都會將 process exit code 設為
1。最大長度案例採緊湊格式顯示。

| # | 案例 | 輸入 | `k` | 預期 |
| --- | --- | --- | ---: | ---: |
| 1 | 官方範例 1 | `[2, 3, 4, 7, 11]` | 5 | 9 |
| 2 | 官方範例 2 | `[1, 2, 3, 4]` | 2 | 6 |
| 3 | 最小值 | `[1]` | 1 | 2 |
| 4 | 第一個元素前缺失 | `[2]` | 1 | 1 |
| 5 | 陣列內缺失 | `[2, 3, 4, 7, 11]` | 3 | 6 |
| 6 | 陣列後缺失 | `[1, 2, 3]` | 5 | 8 |
| 7 | 緊密前綴與最大 `k` | `[1, 2, 3, 4, 5]` | 1,000 | 1,005 |
| 8 | 最大首元素與 `k` | `[1,000]` | 1,000 | 1,001 |
| 9 | 最大長度與 `k` | `1..1,000` | 1,000 | 2,000 |

## 建置與執行

從 repository 根目錄執行：

```bash
dotnet build leetcode_1539/leetcode_1539/leetcode_1539.csproj --nologo
dotnet run --no-build --project leetcode_1539/leetcode_1539/leetcode_1539.csproj
```

若直接以 `leetcode_1539/` 作為 workspace，從題目根目錄執行：

```bash
dotnet build leetcode_1539/leetcode_1539.csproj --nologo
dotnet run --no-build --project leetcode_1539/leetcode_1539.csproj
```

以下為 fresh run 的完整輸出：

```text
Case: Official example 1
Solution: FindKthPositive
Input: [2, 3, 4, 7, 11]
k: 5
Check: kth missing positive
Expected: 9
Actual: 9
Result: PASS
Check: input preserved
Expected: True
Actual: True
Result: PASS

Case: Official example 1
Solution: FindKthPositive2
Input: [2, 3, 4, 7, 11]
k: 5
Check: kth missing positive
Expected: 9
Actual: 9
Result: PASS
Check: input preserved
Expected: True
Actual: True
Result: PASS

Case: Official example 2
Solution: FindKthPositive
Input: [1, 2, 3, 4]
k: 2
Check: kth missing positive
Expected: 6
Actual: 6
Result: PASS
Check: input preserved
Expected: True
Actual: True
Result: PASS

Case: Official example 2
Solution: FindKthPositive2
Input: [1, 2, 3, 4]
k: 2
Check: kth missing positive
Expected: 6
Actual: 6
Result: PASS
Check: input preserved
Expected: True
Actual: True
Result: PASS

Case: Minimum values
Solution: FindKthPositive
Input: [1]
k: 1
Check: kth missing positive
Expected: 2
Actual: 2
Result: PASS
Check: input preserved
Expected: True
Actual: True
Result: PASS

Case: Minimum values
Solution: FindKthPositive2
Input: [1]
k: 1
Check: kth missing positive
Expected: 2
Actual: 2
Result: PASS
Check: input preserved
Expected: True
Actual: True
Result: PASS

Case: Missing before first element
Solution: FindKthPositive
Input: [2]
k: 1
Check: kth missing positive
Expected: 1
Actual: 1
Result: PASS
Check: input preserved
Expected: True
Actual: True
Result: PASS

Case: Missing before first element
Solution: FindKthPositive2
Input: [2]
k: 1
Check: kth missing positive
Expected: 1
Actual: 1
Result: PASS
Check: input preserved
Expected: True
Actual: True
Result: PASS

Case: Missing inside array
Solution: FindKthPositive
Input: [2, 3, 4, 7, 11]
k: 3
Check: kth missing positive
Expected: 6
Actual: 6
Result: PASS
Check: input preserved
Expected: True
Actual: True
Result: PASS

Case: Missing inside array
Solution: FindKthPositive2
Input: [2, 3, 4, 7, 11]
k: 3
Check: kth missing positive
Expected: 6
Actual: 6
Result: PASS
Check: input preserved
Expected: True
Actual: True
Result: PASS

Case: Missing after last element
Solution: FindKthPositive
Input: [1, 2, 3]
k: 5
Check: kth missing positive
Expected: 8
Actual: 8
Result: PASS
Check: input preserved
Expected: True
Actual: True
Result: PASS

Case: Missing after last element
Solution: FindKthPositive2
Input: [1, 2, 3]
k: 5
Check: kth missing positive
Expected: 8
Actual: 8
Result: PASS
Check: input preserved
Expected: True
Actual: True
Result: PASS

Case: Maximum k after dense prefix
Solution: FindKthPositive
Input: [1, 2, 3, 4, 5]
k: 1000
Check: kth missing positive
Expected: 1005
Actual: 1005
Result: PASS
Check: input preserved
Expected: True
Actual: True
Result: PASS

Case: Maximum k after dense prefix
Solution: FindKthPositive2
Input: [1, 2, 3, 4, 5]
k: 1000
Check: kth missing positive
Expected: 1005
Actual: 1005
Result: PASS
Check: input preserved
Expected: True
Actual: True
Result: PASS

Case: Maximum first value and k
Solution: FindKthPositive
Input: [1000]
k: 1000
Check: kth missing positive
Expected: 1001
Actual: 1001
Result: PASS
Check: input preserved
Expected: True
Actual: True
Result: PASS

Case: Maximum first value and k
Solution: FindKthPositive2
Input: [1000]
k: 1000
Check: kth missing positive
Expected: 1001
Actual: 1001
Result: PASS
Check: input preserved
Expected: True
Actual: True
Result: PASS

Case: Maximum length and k
Solution: FindKthPositive
Input: [1, 2, 3, ..., 998, 999, 1000] (Length: 1000)
k: 1000
Check: kth missing positive
Expected: 2000
Actual: 2000
Result: PASS
Check: input preserved
Expected: True
Actual: True
Result: PASS

Case: Maximum length and k
Solution: FindKthPositive2
Input: [1, 2, 3, ..., 998, 999, 1000] (Length: 1000)
k: 1000
Check: kth missing positive
Expected: 2000
Actual: 2000
Result: PASS
Check: input preserved
Expected: True
Actual: True
Result: PASS

Summary: 36/36 checks passed.
```

## 專案結構

```plaintext
leetcode_1539/
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
│       ├── plans/
│       │   ├── 2026-07-18-leetcode-1539-binary-search.md
│       │   └── 2026-07-18-leetcode-1539-net10-migration.md
│       └── specs/
│           ├── 2026-07-18-leetcode-1539-binary-search-design.md
│           └── 2026-07-18-leetcode-1539-net10-migration-design.md
└── leetcode_1539/
    ├── Program.cs
    └── leetcode_1539.csproj
```
