# 1502. Can Make Arithmetic Progression From Sequence／能否形成等差數列

以排序後的相鄰公差驗證等差數列，同時保留呼叫端輸入陣列不變。

- [LeetCode English](https://leetcode.com/problems/can-make-arithmetic-progression-from-sequence/)
- [LeetCode 中文](https://leetcode.cn/problems/can-make-arithmetic-progression-from-sequence/)

## 題目說明

給定整數陣列 `arr`，可任意重新排列元素。若能重新排列成等差數列則回傳 `true`，
否則回傳 `false`。等差數列中每一對相鄰元素的差必須相同。

## 限制條件

- `2 <= arr.Length <= 1000`
- `-1000000 <= arr[i] <= 1000000`
- 題目只要求 LeetCode 定義的有效輸入；公開 API 不額外定義無效輸入行為

## 解法與核心不變量

`CanMakeArithmeticProgression(int[] arr)` 先建立 `arr` 的複本，再只排序這個複本。
排序後，第一對元素的差是唯一候選公差；只要任一後續相鄰差不同，便不可能藉由其他
排列形成等差數列。反之，全部相鄰差相同即為等差數列。

容易出錯的地方是直接呼叫 `Array.Sort(arr)`：雖然能取得正確布林值，卻會改寫呼叫端
輸入。本實作只排序複本，harness 也會逐案比對原始內容，確保 API 沒有 mutation。

| 指標 | 複雜度 |
| --- | --- |
| 時間 | `O(n log n)`，排序主導成本 |
| 輔助空間 | `O(n)`，輸入複本 |
| 結果空間 | `O(1)`，只回傳一個布林值 |

## 逐步範例

以 `arr = [3, 5, 1]` 為例：

```plaintext
複製並排序後： [1, 3, 5]
第一個公差：   3 - 1 = 2
下一個相鄰差： 5 - 3 = 2，與候選公差相同
答案：true
```

## Acceptance Harness

`Main` 提供十個確定性案例；每個 PASS 同時要求回傳值正確及輸入陣列完全未變。

| # | 案例 | 輸入 | 預期 |
| --- | --- | --- | --- |
| 1 | 官網範例 1 | `[3, 5, 1]` | `true` |
| 2 | 官網範例 2 | `[1, 2, 4]` | `false` |
| 3 | 最小長度 | `[7, 3]` | `true` |
| 4 | 零公差 | `[5, 5, 5, 5]` | `true` |
| 5 | 負數 | `[-3, -1, -2]` | `true` |
| 6 | 跨越零 | `[-10, 10, 0]` | `true` |
| 7 | 尾端破壞公差 | `[1, 3, 5, 8]` | `false` |
| 8 | 重複值破壞公差 | `[1, 2, 2, 3]` | `false` |
| 9 | 數值邊界 | `[-1000000, 0, 1000000]` | `true` |
| 10 | 最大長度 | 遞減整數 `999..0`（1000 個值） | `true` |

本專案沒有正式測試專案；`Main` 是可重複執行的驗收機制。任何案例失敗時，程式會將
process exit code 設為 1。

## 建置與執行

從 repository 根目錄執行：

```bash
dotnet build leetcode_1502/leetcode_1502/leetcode_1502.csproj --nologo
dotnet run --no-build --project leetcode_1502/leetcode_1502/leetcode_1502.csproj
```

若直接以 `leetcode_1502/` 作為 workspace，從題目根目錄執行：

```bash
dotnet build leetcode_1502/leetcode_1502.csproj --nologo
dotnet run --no-build --project leetcode_1502/leetcode_1502.csproj
```

以下為 fresh run 的完整輸出：

```text
Case: Official example 1
Input: [3, 5, 1]
Expected: True
Actual: True
Result: PASS

Case: Official example 2
Input: [1, 2, 4]
Expected: False
Actual: False
Result: PASS

Case: Minimum length
Input: [7, 3]
Expected: True
Actual: True
Result: PASS

Case: Zero difference
Input: [5, 5, 5, 5]
Expected: True
Actual: True
Result: PASS

Case: Negative values
Input: [-3, -1, -2]
Expected: True
Actual: True
Result: PASS

Case: Across zero
Input: [-10, 10, 0]
Expected: True
Actual: True
Result: PASS

Case: Tail breaks progression
Input: [1, 3, 5, 8]
Expected: False
Actual: False
Result: PASS

Case: Duplicate breaks progression
Input: [1, 2, 2, 3]
Expected: False
Actual: False
Result: PASS

Case: Value boundaries
Input: [-1000000, 0, 1000000]
Expected: True
Actual: True
Result: PASS

Case: Maximum length
Input: [999..0] (1000 values)
Expected: True
Actual: True
Result: PASS

Summary: 10/10 checks passed.
```

## 專案結構

```plaintext
leetcode_1502/
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
│       ├── plans/2026-07-18-leetcode-1502-net10-migration.md
│       └── specs/2026-07-18-leetcode-1502-net10-migration-design.md
└── leetcode_1502/
    ├── Program.cs
    └── leetcode_1502.csproj
```
