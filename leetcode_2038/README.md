# LeetCode 2038 — Remove Colored Pieces if Both Neighbors are the Same Color

> 如果相鄰兩個顏色均相同則刪除當前顏色｜.NET 10 主控台專案

- [English problem](https://leetcode.com/problems/remove-colored-pieces-if-both-neighbors-are-the-same-color/)
- [中文題目](https://leetcode.cn/problems/remove-colored-pieces-if-both-neighbors-are-the-same-color/)

## 題目說明

一列色塊只包含 `A` 與 `B`。Alice 與 Bob 輪流移除色塊，Alice 先手：

- Alice 只能移除左右相鄰色塊也都是 `A` 的 `A`。
- Bob 只能移除左右相鄰色塊也都是 `B` 的 `B`。
- 兩端的色塊不可移除。
- 輪到某位玩家卻無法操作時，該玩家落敗。

題目限制：

- `1 <= colors.length <= 10^5`
- `colors[i]` 只會是 `A` 或 `B`。

## 核心不變量

長度為 `L` 的同色連續段必須保留左右兩端，因此能提供
`max(0, L - 2)` 次操作。移除某段內部色塊不會合併被另一種顏色隔開的連續段，也不會
改變對手的操作總數，所以不必模擬每一回合；只需分別統計所有 `A` 段與 `B` 段的可移除
次數。

```plaintext
colors = "AAAABBAAABBBB"

"AAAA"  提供 Alice 2 次操作
"BB"    提供 Bob   0 次操作
"AAA"   提供 Alice 1 次操作
"BBBB"  提供 Bob   2 次操作

Alice = 3，Bob = 2；Alice 先手且操作數較多，因此獲勝。
```

容易出錯的地方：

- 連續段長度剛好為 3 時已有一次操作，不可等到長度 4 才計數。
- 長度 4 的同色段有兩個可移除機會，不能只把整段當成一次。
- 不同位置的同色段必須加總，不能只保留最長段。
- Alice 先手；操作數平手時 Alice 會先無法行動，因此必須使用嚴格大於。
- 長度上限為 100,000，逐回合刪除字串會造成不必要的高成本。

## 單趟連續段計數

公開入口：

```csharp
public static bool WinnerOfGame(string colors)
```

掃描 `colors` 時維護目前顏色與連續長度。每當同色段長度到達 3 或以上，便為該顏色
增加一次操作；這等價於在段落結束時計算 `L - 2`，但不需要額外的第二次處理。

- 時間複雜度：`O(n)`。
- 結果空間：`O(1)`。
- 輔助空間：`O(1)`。
- 輸入不會被修改，且解法函式沒有 console side effect。

## Acceptance Harness

`Main` 執行十個確定性案例，每案直接驗證公開 API。任何失敗都會把 process exit code
設為 `1`；長度 100,000 的輸入只顯示頭尾各 16 個字元，實際比較仍使用完整字串。

| # | 輸入 | 預期 | 驗證目的 |
| ---: | --- | ---: | --- |
| 1 | `AAABABB` | `true` | 官方案例；Alice 有唯一操作 |
| 2 | `AA` | `false` | 官方案例；不足三個相鄰色塊 |
| 3 | `ABBBBBBBAAA` | `false` | 官方案例；Bob 操作數較多 |
| 4 | `A` | `false` | 最小有效輸入 |
| 5 | `AAAAAA` | `true` | Alice 單一長段可連續操作 |
| 6 | `BBBBBB` | `false` | Bob 單一長段優勢 |
| 7 | `AAABBB` | `false` | 操作數平手時 Alice 落敗 |
| 8 | `AAAABBAAABBBB` | `true` | 多個連續段必須分別加總 |
| 9 | `AAAABBB` | `true` | `A` 長段有兩次操作，擊破每段只計一次的錯誤 |
| 10 | 100,000 個 `A` | `true` | 題目上限的線性掃描與穩定輸出 |

## 建置與執行

已從 repository 根目錄驗證：

```bash
dotnet build leetcode_2038/leetcode_2038/leetcode_2038.csproj --nologo
dotnet run --no-build --project leetcode_2038/leetcode_2038/leetcode_2038.csproj
```

若直接開啟題目根目錄 `leetcode_2038/`，使用：

```bash
dotnet build leetcode_2038/leetcode_2038.csproj --nologo
dotnet run --no-build --project leetcode_2038/leetcode_2038.csproj
```

以下是 fresh run 的完整輸出：

```text
LeetCode 2038 Acceptance Harness
Case: Official example 1
Input: colors = "AAABABB"
PASS WinnerOfGame result | Expected: True | Actual: True

Case: Official example 2
Input: colors = "AA"
PASS WinnerOfGame result | Expected: False | Actual: False

Case: Official example 3
Input: colors = "ABBBBBBBAAA"
PASS WinnerOfGame result | Expected: False | Actual: False

Case: Minimum input
Input: colors = "A"
PASS WinnerOfGame result | Expected: False | Actual: False

Case: Alice-only long run
Input: colors = "AAAAAA"
PASS WinnerOfGame result | Expected: True | Actual: True

Case: Bob-only long run
Input: colors = "BBBBBB"
PASS WinnerOfGame result | Expected: False | Actual: False

Case: Equal move counts
Input: colors = "AAABBB"
PASS WinnerOfGame result | Expected: False | Actual: False

Case: Multiple runs aggregate
Input: colors = "AAAABBAAABBBB"
PASS WinnerOfGame result | Expected: True | Actual: True

Case: Overlapping removals regression
Input: colors = "AAAABBB"
PASS WinnerOfGame result | Expected: True | Actual: True

Case: Maximum-length input
Input: colors = "AAAAAAAAAAAAAAAA...AAAAAAAAAAAAAAAA" (length: 100000)
PASS WinnerOfGame result | Expected: True | Actual: True

Summary: 10/10 checks passed.
```

## 專案結構

```plaintext
leetcode_2038/
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
└── leetcode_2038/
    ├── Program.cs
    └── leetcode_2038.csproj
```
