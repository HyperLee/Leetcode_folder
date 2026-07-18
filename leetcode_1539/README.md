# 1539. Kth Missing Positive Number／第 k 個缺失的正整數

給定嚴格遞增的正整數陣列，從 1 開始逐一檢查每個候選值，找出不在陣列中的第 `k` 個
正整數。本專案保留原有的枚舉教學思路，公開 API 是不輸出且不修改輸入的純函式。

- [LeetCode English](https://leetcode.com/problems/kth-missing-positive-number/)
- [LeetCode 中文](https://leetcode.cn/problems/kth-missing-positive-number/)

## 題目說明與限制條件

給定嚴格遞增的正整數陣列 `arr` 與正整數 `k`，回傳陣列中缺失的第 `k` 個正整數。

- `1 <= arr.Length <= 1,000`
- `1 <= arr[i] <= 1,000`
- `1 <= k <= 1,000`
- `arr[i] < arr[j]`，其中 `i < j`
- `FindKthPositive` 只處理題目保證的有效輸入，未額外定義無效輸入行為

## 演算法、不變量與取捨

`FindKthPositive` 使用三個狀態：`current` 是目前檢查的正整數，`arrayIndex` 指向尚未
匹配的第一個陣列元素，`missingCount` 記錄已找到的缺失正整數數量。

每輪若 `current == arr[arrayIndex]`，代表該值存在，只推進陣列索引；否則它就是下一個
缺失值，增加 `missingCount`。當缺失數量到達 `k`，回傳剛檢查完的 `current`。

核心不變量是：進入每輪迴圈時，小於 `current` 的正整數都已完成分類，而
`arrayIndex` 永遠指向第一個尚未處理的陣列元素。因此陣列與候選值都只向前移動，不會
漏算或重複計算缺失值。

| 指標 | 複雜度 |
| --- | --- |
| 時間 | `O(n + k)`；第 `k` 個缺失值最多在 `n + k` 之內 |
| 結果空間 | `O(1)`；只回傳一個整數 |
| 輔助空間 | `O(1)`；僅使用索引、候選值與計數器 |

另一種作法可用 `arr[i] - i - 1` 二分搜尋缺失數量，將時間降為 `O(log n)`；本題保留
逐一枚舉，是因為它直接呈現「存在則前進索引、缺失則累計」的原始教學脈絡，且在題目
上限內最多檢查 2,000 個候選值。

## 走查

以 `arr = [2, 3, 4, 7, 11]`、`k = 5` 為例：

| `current` | 是否存在 | 缺失序號 |
| ---: | --- | ---: |
| 1 | 否 | 1 |
| 2、3、4 | 是 | 仍為 1 |
| 5 | 否 | 2 |
| 6 | 否 | 3 |
| 7 | 是 | 仍為 3 |
| 8 | 否 | 4 |
| 9 | 否 | 5 |

因此第 5 個缺失正整數是 `9`。

## Acceptance Harness

`Main` 提供九個確定性案例。每案分別檢查答案及呼叫後陣列是否逐元素不變，共十八項
檢查；任何失敗都會將 process exit code 設為 1。最大長度案例採緊湊格式顯示，避免輸出
1,000 個數值。

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

Summary: 18/18 checks passed.
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
│       ├── plans/2026-07-18-leetcode-1539-net10-migration.md
│       └── specs/2026-07-18-leetcode-1539-net10-migration-design.md
└── leetcode_1539/
    ├── Program.cs
    └── leetcode_1539.csproj
```
