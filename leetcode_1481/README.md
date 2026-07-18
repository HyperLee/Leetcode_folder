# 1481. Least Number of Unique Integers after K Removals／移除 K 個元素後相異整數的最少數目

- [LeetCode English](https://leetcode.com/problems/least-number-of-unique-integers-after-k-removals/)
- [LeetCode 中文](https://leetcode.cn/problems/least-number-of-unique-integers-after-k-removals/)

## 題意

給定整數陣列 `arr` 與整數 `k`，恰好移除 `k` 個元素後，回傳可能留下的最少相異整數數量。
移除部分元素不會讓該數字消失；只有移除某個數字的所有出現次數，才會讓相異整數數量減一。

## 限制條件

- `1 <= arr.Length <= 100000`
- `1 <= arr[i] <= 1000000000`
- `0 <= k <= arr.Length`

## 核心不變量與舊實作陷阱

頻率群組從小到大完整移除時，每消除一個群組，留下的相異整數數量必定減一；因此，任何最佳解都
優先使用移除額度消除最低頻率群組。當下一個群組頻率大於剩餘 `k`，即使仍須恰好移除元素，也
只能從保留群組中部分移除，無法再降低相異整數數量。

舊實作曾在列舉 `sortedDict` 時移除其中項目，這會觸發集合已修改的執行階段例外。另一個一般性陷阱
是直接排序 `arr` 造成輸入被修改；本實作只排序獨立建立的頻率清單，且 harness 會在每個案例比對
API 呼叫前後的完整輸入陣列。部分移除也不能誤算成消除相異整數。

## 採用解法：頻率統計後由低至高完整移除

`FindLeastNumOfUniqueInts(int[] arr, int k)` 先以字典統計各數值的出現次數，再將頻率排序。
依序扣除可完整移除的最低頻率群組，直到下一組不再容納於剩餘 `k`；API 沒有 console side effect，
且不修改 `arr`。

| 指標 | 複雜度 |
| --- | --- |
| 時間 | `O(n + u log u)` |
| 輔助空間 | `O(u)` |
| 結果空間 | `O(1)` |

其中 `n` 是陣列長度、`u` 是相異整數數量。

### 逐步走查：`[4, 3, 1, 1, 3, 3, 2]`，`k = 3`

```plaintext
頻率：1 -> 2、2 -> 1、3 -> 3、4 -> 1
排序後頻率：[1, 1, 2, 3]，初始相異整數數量 = 4

移除頻率 1 的數字 2：k = 2，剩餘相異數量 = 3
移除頻率 1 的數字 4：k = 1，剩餘相異數量 = 2
下一組頻率為 2，大於 k = 1；部分移除不會消除該相異整數
答案：2
```

## Acceptance Harness

`Main` 有九個確定性案例；每個 PASS 同時要求答案正確及輸入陣列完全未變。

| # | 案例 | 預期 |
| --- | --- | ---: |
| 1 | 官方案例 1：`[5, 5, 4]`，`k = 1` | 1 |
| 2 | 官方案例 2：`[4, 3, 1, 1, 3, 3, 2]`，`k = 3` | 2 |
| 3 | 最小輸入／不移除：`[1]`，`k = 0` | 1 |
| 4 | 移除所有元素：`[1]`，`k = 1` | 0 |
| 5 | 零次移除：`[1, 2, 2, 3, 3, 3]`，`k = 0` | 3 |
| 6 | 無法完整移除下一頻率群組：`[1, 1, 2, 2, 2, 3, 3, 3]`，`k = 1` | 3 |
| 7 | 相同頻率：`[1, 1, 2, 2, 3, 3, 4]`，`k = 4` | 2 |
| 8 | 大數值：`[1000000000, 1000000000, 999999999, 123456789]`，`k = 2` | 1 |
| 9 | 最大長度：長度 100000，`k = 25000` | 2 |

## 已驗證命令

以下命令從 repository root 執行：

```bash
jq empty leetcode_1481/.vscode/launch.json leetcode_1481/.vscode/tasks.json
dotnet build leetcode_1481/leetcode_1481/leetcode_1481.csproj --nologo
dotnet run --no-build --project leetcode_1481/leetcode_1481/leetcode_1481.csproj
```

若直接開啟 `leetcode_1481/` 作為 VS Code workspace，從題目根目錄使用相對巢狀路徑：

```bash
dotnet build leetcode_1481/leetcode_1481.csproj --nologo
dotnet run --no-build --project leetcode_1481/leetcode_1481.csproj
```

以下是 fresh run 的完整輸出：

```text
Case: Official example 1
Input: arr = [5, 5, 4], k = 1
Expected: 1
Actual: 1
Result: PASS

Case: Official example 2
Input: arr = [4, 3, 1, 1, 3, 3, 2], k = 3
Expected: 2
Actual: 2
Result: PASS

Case: Minimum input / remove nothing
Input: arr = [1], k = 0
Expected: 1
Actual: 1
Result: PASS

Case: Remove every element
Input: arr = [1], k = 1
Expected: 0
Actual: 0
Result: PASS

Case: Zero removals
Input: arr = [1, 2, 2, 3, 3, 3], k = 0
Expected: 3
Actual: 3
Result: PASS

Case: Cannot remove the next frequency group
Input: arr = [1, 1, 2, 2, 2, 3, 3, 3], k = 1
Expected: 3
Actual: 3
Result: PASS

Case: Equal frequencies
Input: arr = [1, 1, 2, 2, 3, 3, 4], k = 4
Expected: 2
Actual: 2
Result: PASS

Case: Large values
Input: arr = [1000000000, 1000000000, 999999999, 123456789], k = 2
Expected: 1
Actual: 1
Result: PASS

Case: Maximum length
Input: arr = [length 100000; 1 x 50000, 2 x 25000, 3..25002], k = 25000
Expected: 2
Actual: 2
Result: PASS

Summary: 9/9 checks passed.
```

## 最終結構

```plaintext
leetcode_1481/
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
│       ├── plans/2026-07-18-leetcode-1481-net10-migration.md
│       └── specs/2026-07-18-leetcode-1481-net10-migration-design.md
└── leetcode_1481/
    ├── Program.cs
    └── leetcode_1481.csproj
```
