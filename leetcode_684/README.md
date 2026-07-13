# 684. Redundant Connection（冗餘連線）

[LeetCode 題目](https://leetcode.com/problems/redundant-connection/) · [力扣中文題目](https://leetcode.cn/problems/redundant-connection/)

給定一張由樹加上一條額外無向邊所形成的圖，找出依輸入順序第一條使圖出現環的邊。本專案以 .NET 10 主控台程式實作 Union-Find，並保留造成環之邊在輸入中的端點方向。

## 題目說明

核心 API 是 `FindRedundantConnection(int[][] edges)`。它依序讀取每條無向邊；若兩端節點在加入此邊之前已屬於同一個連通元件，這條邊便是答案。方法不輸出主控台、不排序也不修改 `edges`、不改寫任何內層陣列，因此可作為純粹的資料轉換 API 使用。

雖然連通性判斷把邊視為無向邊，回傳值直接使用原始輸入邊。因此例如輸入 `[4,1]` 造成環時，回傳仍是 `[4,1]`，不會為了表示無向邊而改成 `[1,4]`。

## 限制條件

官方題目保證邊數為 `n`，節點編號位於 `1..n`，且圖原本是恰有一條額外邊的連通無向圖。一般限制為 `3 <= n <= 1000`，每條邊有兩個端點。

本專案的 acceptance harness 另加入保留輸入方向與 `100000` 節點 spot check，作為實作契約與效率的補強驗證；它不會改變公開 API 的語意。

## 核心不變量

- `parent[root] == root` 的節點是其連通元件的代表元。
- 只有代表元的 `componentSize` 有意義，記錄整個元件的大小。
- `Find` 回傳代表元並壓縮沿途節點；`Union` 永遠把較小集合的根接到較大集合的根。
- `Union` 回傳 `false` 時，兩端原本已連通，當前原始邊即為冗餘邊。

## 演算法設計

先建立索引 `1..n` 的 parent 與 component-size 陣列。每讀到一條 `[u,v]`：

1. 以路徑壓縮找出 `u` 和 `v` 的代表元。
2. 若代表元相同，`u` 到 `v` 已有既存路徑；回傳此原始 `[u,v]`。
3. 否則以 union-by-size 合併兩個元件，繼續處理下一條邊。

## 逐步範例

以下輸入在讀到最後一條邊時才形成環：

```plaintext
[[1,2], [1,3], [2,3]]
```

| 步驟 | 邊 | 合併前代表元 | 結果 |
| --- | --- | --- | --- |
| 1 | `[1,2]` | `1`、`2` | 合併為同一元件。 |
| 2 | `[1,3]` | `1`、`3` | 合併為同一元件。 |
| 3 | `[2,3]` | `1`、`1` | 已連通，回傳原始邊 `[2,3]`。 |

## 複雜度

- 時間：`O(n alpha(n))`，其中 `alpha` 是反阿克曼函數；在路徑壓縮與 union-by-size 下，每次操作近乎常數時間。
- 額外空間：`O(n)`，來自 `parent` 與 `componentSize` 陣列。

## Acceptance harness

本題沒有正式測試專案。取而代之的是 `Main` 中八個固定、可重複執行的 acceptance checks。每一項都印出輸入、預期值、實際值與 PASS/FAIL；只要有一項失敗，程式會設定非零結束碼。

| # | 案例 | 輸入 | 預期輸出 | 驗證目的 |
| --- | --- | --- | --- | --- |
| 1 | Official example 1 | `[[1,2], [1,3], [2,3]]` | `[2,3]` | 官方基本環。 |
| 2 | Official example 2 | `[[1,2], [2,3], [3,4], [1,4], [1,5]]` | `[1,4]` | 官方較長樹前綴。 |
| 3 | Minimum reordered cycle | `[[3,1], [1,2], [2,3]]` | `[2,3]` | 最小環與輸入順序。 |
| 4 | Long tree prefix | `[[1,2], [2,3], [3,4], [4,5], [2,5]]` | `[2,5]` | 長鏈尾端接回既有元件。 |
| 5 | Preserve answer direction | `[[2,1], [3,2], [4,3], [4,1]]` | `[4,1]` | 答案端點方向必須保留。 |
| 6 | Branches meet in a cycle | `[[1,2], [1,3], [2,4], [3,5], [4,5]]` | `[4,5]` | 分支在尾端合成環。 |
| 7 | Forest becomes one cycle | `[[1,2], [3,4], [2,3], [1,4]]` | `[1,4]` | 暫時分離的元件最後成環。 |
| 8 | 100000-node spot check | `chain 1..100000 plus [1,100000]` | `[1,100000]` | 大型鏈與最後回邊。 |

## 建置與執行

請在 `leetcode_684/` 題目根目錄使用巢狀專案路徑：

```plaintext
dotnet build leetcode_684/leetcode_684.csproj --nologo
dotnet run --no-build --project leetcode_684/leetcode_684.csproj
```

在 VS Code 直接開啟 `leetcode_684/` 時，選擇 `Debug leetcode_684`；設定會先建置後啟動 `.NET 10` 主控台程式。

## 實際輸出

下列內容來自新鮮執行 `dotnet run --no-build --project leetcode_684/leetcode_684.csproj` 的完整逐字輸出：

```text
Case: Official example 1
Input: [[1,2], [1,3], [2,3]]
Expected: [2,3]
Actual: [2,3]
Result: PASS

Case: Official example 2
Input: [[1,2], [2,3], [3,4], [1,4], [1,5]]
Expected: [1,4]
Actual: [1,4]
Result: PASS

Case: Minimum reordered cycle
Input: [[3,1], [1,2], [2,3]]
Expected: [2,3]
Actual: [2,3]
Result: PASS

Case: Long tree prefix
Input: [[1,2], [2,3], [3,4], [4,5], [2,5]]
Expected: [2,5]
Actual: [2,5]
Result: PASS

Case: Preserve answer direction
Input: [[2,1], [3,2], [4,3], [4,1]]
Expected: [4,1]
Actual: [4,1]
Result: PASS

Case: Branches meet in a cycle
Input: [[1,2], [1,3], [2,4], [3,5], [4,5]]
Expected: [4,5]
Actual: [4,5]
Result: PASS

Case: Forest becomes one cycle
Input: [[1,2], [3,4], [2,3], [1,4]]
Expected: [1,4]
Actual: [1,4]
Result: PASS

Case: 100000-node spot check
Input: chain 1..100000 plus [1,100000]
Expected: [1,100000]
Actual: [1,100000]
Result: PASS

Summary: 8/8 checks passed.
```

## 專案結構

```plaintext
leetcode_684/
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
│       │   └── 2026-07-13-leetcode-684-net10-migration.md
│       └── specs/
│           └── 2026-07-13-leetcode-684-net10-migration-design.md
└── leetcode_684/
    ├── leetcode_684.csproj
    └── Program.cs
```

舊式 `leetcode_684.sln`、`App.config` 與 `Properties/AssemblyInfo.cs` 已在 .NET 10 遷移中移除。
