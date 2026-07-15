# LeetCode 746：Min Cost Climbing Stairs／使用最小花費爬樓梯

這是一個以 C# 撰寫的 .NET 10 主控台專案。它保留兩種由相同狀態轉移衍生的
動態規劃解法：完整 DP 陣列與只保留前兩個狀態的空間優化版本。

- [英文題目：746. Min Cost Climbing Stairs](https://leetcode.com/problems/min-cost-climbing-stairs/)
- [中文題目：746. 使用最小花費爬樓梯](https://leetcode.cn/problems/min-cost-climbing-stairs/)

## 題目說明

`cost[i]` 表示踏上索引 `i` 階梯所需支付的費用。支付後可以向上爬一階或兩階，
而起點可以選擇索引 `0` 或索引 `1`。目標是越過最後一階、抵達索引
`cost.Length` 所代表的樓梯頂端，並回傳最低總花費。

## 限制條件

- `2 <= cost.Length <= 1000`
- `0 <= cost[i] <= 999`
- 每次只能向上爬一階或兩階。
- 可以從索引 `0` 或索引 `1` 開始。

實作只處理題目定義的有效輸入，不另外定義無效輸入的例外行為。

## 核心不變量與狀態轉移

把樓梯頂端視為索引 `n = cost.Length` 的虛擬位置，並令 `dp[i]` 為抵達位置
`i` 的最低總花費。因為可以直接從索引 `0` 或 `1` 開始，所以：

```csharp
dp[0] = 0;
dp[1] = 0;
```

抵達 `i` 前只可能位於 `i - 1` 或 `i - 2`，而離開該階時必須支付對應費用：

```csharp
dp[i] = Math.Min(
    dp[i - 1] + cost[i - 1],
    dp[i - 2] + cost[i - 2]);
```

容易出錯之處是把費用誤算在「抵達」階梯時，或忘記頂端本身沒有費用。兩個公開
API 都不會修改 `cost`，也不會寫入主控台。

## 兩種解法與取捨

### `MinCostClimbingStairs`

以長度 `n + 1` 的陣列保存所有狀態，能直接觀察每一階的最低累積花費，適合說明
完整 DP 推導。

- 時間複雜度：`O(n)`
- 結果空間：`O(1)`，只回傳一個整數
- 輔助空間：`O(n)`

### `MinCostClimbingStairs2`

每次狀態轉移只使用前兩個結果，因此以 `twoStepsBefore` 與 `oneStepBefore`
取代整個陣列。

- 時間複雜度：`O(n)`
- 結果空間：`O(1)`
- 輔助空間：`O(1)`

## `[10, 15, 20]` 逐步走查

| 位置 `i` | 從 `i - 1` 前來 | 從 `i - 2` 前來 | `dp[i]` |
| ---: | ---: | ---: | ---: |
| `0` | — | — | `0` |
| `1` | — | — | `0` |
| `2` | `0 + 15 = 15` | `0 + 10 = 10` | `10` |
| `3`（頂端） | `10 + 20 = 30` | `0 + 15 = 15` | `15` |

因此最低花費為：

```plaintext
15
```

## 可執行驗證案例

`Main` 共有 8 組案例。每組分別檢查兩種解法的結果與各自輸入未被修改，共 32 項
檢查：

| 案例 | 輸入 | 預期 | 驗證重點 |
| --- | --- | ---: | --- |
| 1 | `[10, 15, 20]` | `15` | 官方範例，從索引 1 開始 |
| 2 | `[1,100,1,1,1,100,1,1,100,1]` | `6` | 官方長範例與多次狀態轉移 |
| 3 | `[0, 0]` | `0` | 最小長度與零費用 |
| 4 | `[999, 0]` | `0` | 從索引 1 開始較佳 |
| 5 | `[1, 100, 1]` | `2` | 必須支付最後一階費用的路徑 |
| 6 | `[10, 1, 1, 10, 1]` | `3` | 多階累積回歸案例 |
| 7 | `[0, 0, 0, 0]` | `0` | 多階零費用 |
| 8 | 1,000 個 `999` | `499500` | 長度與費用上限 spot check |

任一檢查失敗時，程式會將 `Environment.ExitCode` 設為 `1`。本題沒有獨立 test
project；console acceptance harness 是目前的驗證機制。

## 建置與執行

請從此 README 所在的外層 `leetcode_746` 目錄執行：

```bash
dotnet build leetcode_746/leetcode_746.csproj --nologo
dotnet run --no-build --project leetcode_746/leetcode_746.csproj
```

以下是完成建置後執行第二個命令的完整輸出：

```text
LeetCode 746 acceptance harness

Case 1: Official example
Input: [10, 15, 20]
PASS | DP array result | Expected: 15 | Actual: 15
PASS | DP array input preserved | Expected: True | Actual: True
PASS | Rolling-state result | Expected: 15 | Actual: 15
PASS | Rolling-state input preserved | Expected: True | Actual: True

Case 2: Official longer example
Input: [1, 100, 1, 1, 1, 100, 1, 1, 100, 1]
PASS | DP array result | Expected: 6 | Actual: 6
PASS | DP array input preserved | Expected: True | Actual: True
PASS | Rolling-state result | Expected: 6 | Actual: 6
PASS | Rolling-state input preserved | Expected: True | Actual: True

Case 3: Minimum length with zero costs
Input: [0, 0]
PASS | DP array result | Expected: 0 | Actual: 0
PASS | DP array input preserved | Expected: True | Actual: True
PASS | Rolling-state result | Expected: 0 | Actual: 0
PASS | Rolling-state input preserved | Expected: True | Actual: True

Case 4: Starting at index 1 is optimal
Input: [999, 0]
PASS | DP array result | Expected: 0 | Actual: 0
PASS | DP array input preserved | Expected: True | Actual: True
PASS | Rolling-state result | Expected: 0 | Actual: 0
PASS | Rolling-state input preserved | Expected: True | Actual: True

Case 5: Paying the final stair is optimal
Input: [1, 100, 1]
PASS | DP array result | Expected: 2 | Actual: 2
PASS | DP array input preserved | Expected: True | Actual: True
PASS | Rolling-state result | Expected: 2 | Actual: 2
PASS | Rolling-state input preserved | Expected: True | Actual: True

Case 6: Multi-step regression
Input: [10, 1, 1, 10, 1]
PASS | DP array result | Expected: 3 | Actual: 3
PASS | DP array input preserved | Expected: True | Actual: True
PASS | Rolling-state result | Expected: 3 | Actual: 3
PASS | Rolling-state input preserved | Expected: True | Actual: True

Case 7: All zero costs
Input: [0, 0, 0, 0]
PASS | DP array result | Expected: 0 | Actual: 0
PASS | DP array input preserved | Expected: True | Actual: True
PASS | Rolling-state result | Expected: 0 | Actual: 0
PASS | Rolling-state input preserved | Expected: True | Actual: True

Case 8: Upper-bound spot check
Input: 1,000 steps (all 999)
PASS | DP array result | Expected: 499500 | Actual: 499500
PASS | DP array input preserved | Expected: True | Actual: True
PASS | Rolling-state result | Expected: 499500 | Actual: 499500
PASS | Rolling-state input preserved | Expected: True | Actual: True

Summary: 32/32 checks passed.
```

## 專案結構

```plaintext
.
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── docs/
│   ├── readme-template.md
│   └── superpowers/
│       ├── plans/
│       └── specs/
├── leetcode_746/
│   ├── Program.cs
│   └── leetcode_746.csproj
├── AGENTS.md
└── README.md
```
