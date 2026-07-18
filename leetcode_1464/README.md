# 1464. Maximum Product of Two Elements in an Array／陣列中兩個元素的最大乘積

- [LeetCode English](https://leetcode.com/problems/maximum-product-of-two-elements-in-an-array/)
- [LeetCode 繁體中文](https://leetcode.cn/problems/maximum-product-of-two-elements-in-an-array/)

## 題意

給定整數陣列 `nums`，選兩個不同元素 `nums[i]` 和 `nums[j]`，找出
`(nums[i] - 1) * (nums[j] - 1)` 的最大值。因為每個數都是正整數，原始數值最大的兩個元素
也必然使減一後的乘積最大。

## 限制條件

- `2 <= nums.Length <= 500`
- `1 <= nums[i] <= 1000`

## 核心不變量與陷阱

掃描到任一位置後，`largest` 與 `secondLargest` 分別是目前已掃描值中的最大與次大值。新值若
大於 `largest`，舊 `largest` 必須下移到 `secondLargest`；否則新值若大於 `secondLargest`，
只更新次大值。

常見錯誤是只記錄最大值而漏掉「最大值先抵達」時的次大值，或採用 `Array.Sort` 導致輸入被修改。
本實作不排序、也不修改 `nums`；harness 每一項都會比對呼叫前後完整陣列。

## 採用解法：單趟維護前兩大值

`MaxProduct(int[] nums)` 以一次 `foreach` 追蹤最大與次大數字，最後計算兩者各減一後的乘積。
不需要額外集合，也不需要排序；API 本身沒有 console side effect。

| 指標 | 複雜度 |
| --- | --- |
| 時間 | `O(n)` |
| 結果空間 | `O(1)` |
| 輔助空間 | `O(1)` |

### 逐步走查

對 `[10, 2, 5, 2]`：

```plaintext
讀到 10：largest = 10，secondLargest = 0
讀到 2：largest = 10，secondLargest = 2
讀到 5：largest = 10，secondLargest = 5
讀到 2：兩個值維持不變
答案：(10 - 1) * (5 - 1) = 36
```

## Acceptance Harness

`Main` 有八個確定性案例；每個 PASS 同時要求回傳值正確與輸入陣列完全未變。

| # | 案例 | 預期 |
| --- | --- | ---: |
| 1 | Official example 1：`[3, 4, 5, 2]` | 12 |
| 2 | Official example 2／重複最大值：`[1, 5, 4, 5]` | 16 |
| 3 | Official example 3／最小長度：`[3, 7]` | 12 |
| 4 | 最小值：`[1, 1]` | 0 |
| 5 | 最大值：`[1000, 1000]` | 998001 |
| 6 | 最大值先到／次大值回歸：`[10, 2, 5, 2]` | 36 |
| 7 | 一般未排序回歸：`[4, 9, 2, 8, 3]` | 56 |
| 8 | 長度 500：`1..498, 1000, 999` | 997002 |

## 已驗證命令

以下命令從 repository root 執行：

```bash
jq empty leetcode_1464/.vscode/launch.json leetcode_1464/.vscode/tasks.json
dotnet build leetcode_1464/leetcode_1464/leetcode_1464.csproj --nologo
dotnet run --no-build --project leetcode_1464/leetcode_1464/leetcode_1464.csproj
```

若直接開啟 `leetcode_1464/` 作為 VS Code workspace，使用相對巢狀路徑：

```bash
dotnet build leetcode_1464/leetcode_1464.csproj --nologo
dotnet run --no-build --project leetcode_1464/leetcode_1464.csproj
```

以下是 fresh run 的完整輸出：

```text
Case: Official example 1
Input: [3, 4, 5, 2]
Expected: 12
Actual: 12
Result: PASS

Case: Official example 2 / duplicate maximum
Input: [1, 5, 4, 5]
Expected: 16
Actual: 16
Result: PASS

Case: Official example 3 / minimum length
Input: [3, 7]
Expected: 12
Actual: 12
Result: PASS

Case: Minimum values
Input: [1, 1]
Expected: 0
Actual: 0
Result: PASS

Case: Maximum values
Input: [1000, 1000]
Expected: 998001
Actual: 998001
Result: PASS

Case: Largest arrives first / second-largest regression
Input: [10, 2, 5, 2]
Expected: 36
Actual: 36
Result: PASS

Case: Unsorted general regression
Input: [4, 9, 2, 8, 3]
Expected: 56
Actual: 56
Result: PASS

Case: Maximum-length case
Input: [length 500; values 1..498, 1000, 999]
Expected: 997002
Actual: 997002
Result: PASS

Summary: 8/8 checks passed.
```

## 最終結構

```plaintext
leetcode_1464/
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
│       ├── plans/2026-07-18-leetcode-1464-net10-migration.md
│       └── specs/2026-07-18-leetcode-1464-net10-migration-design.md
└── leetcode_1464/
    ├── Program.cs
    └── leetcode_1464.csproj
```
