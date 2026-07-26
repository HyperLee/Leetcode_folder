# LeetCode 2265 - Count Nodes Equal to Average of Subtree

## 統計值等於子樹平均值的節點數

- [English problem](https://leetcode.com/problems/count-nodes-equal-to-average-of-subtree/)
- [中文題目](https://leetcode.cn/problems/count-nodes-equal-to-average-of-subtree/)

給定一棵二元樹，對每個節點取其完整子樹（含自己）的所有節點值，以整數除法求平均；若該平均值等於目前節點值，就把此節點計入答案。

## 限制條件

- 樹的節點數 `n` 為 `1 <= n <= 1000`。
- 每個節點值為 `0 <= Node.val <= 1000`。
- 平均值採題目定義的整數除法，因此小數部分直接捨去。

## 核心不變量與解法

`Traverse(node)` 的回傳值固定表示該節點完整子樹的 `(Sum, Count, Matches)`：

- 空子節點回傳 `(0, 0, 0)`。
- 非空節點先以後序取得左右子樹的彙總，再把目前值加入 `Sum`、把自己加入 `Count`。
- 此時 `Sum / Count` 正是目前節點子樹的整數平均；`Matches` 則是左右匹配數加上目前節點的判斷結果。

舊版程式把答案放在全域 `ans`，重複呼叫同一方法會累積上一次的結果，且無法保證可重入。本版讓每次走訪都將匹配數隨 tuple 回傳，因此 `AverageOfSubtree` 是不修改樹、無 Console 輸出、無跨呼叫狀態的純函式。

相較於每個節點重新掃描其整棵子樹的作法，本解法只走訪每個節點一次。用遞迴後序而非額外字典可直接把子樹資訊交給父節點；代價是呼叫堆疊深度會隨樹高變化。

| 項目 | 複雜度 |
| --- | --- |
| 時間 | `O(n)` |
| 結果空間 | `O(1)` |
| 輔助空間 | `O(h)` 遞迴呼叫堆疊，`h` 為樹高 |

## 逐步走查

以官方樹 `[4,8,5,0,1,null,6]` 為例：

1. 葉節點 `0`、`1`、`6` 的子樹平均各自等於本身，先得到 3 個匹配。
2. 節點 `8` 的子樹總和是 `9`、節點數是 `3`，整數平均是 `3`，不匹配。
3. 節點 `5` 的子樹總和是 `11`、節點數是 `2`，整數平均是 `5`，匹配。
4. 根節點 `4` 的子樹總和是 `24`、節點數是 `6`，整數平均是 `4`，匹配；總計為 `5`。

## Acceptance harness

`Main` 是唯一 Console I/O 邊界，以下九項皆以手算 literal expected value 驗證：

| # | 案例 | 預期 | 驗證目的 |
| --- | --- | --- | --- |
| 1 | `[4,8,5,0,1,null,6]` | `5` | 官方範例與一般分支 |
| 2 | `[1]` | `1` | 最小有效樹 |
| 3 | `[2,1,4]` | `3` | 根節點等於截斷平均 |
| 4 | `[9,1,1]` | `2` | 根節點不等於平均 |
| 5 | `[0,0,0]` | `3` | 零值邊界 |
| 6 | `[3,null,1,null,0]` | `1` | 不對稱右斜子樹 |
| 7 | 同一官方樹呼叫兩次 | `(5, 5)` | 防止舊版全域狀態累積 |
| 8 | `[2,1]` 呼叫前後節點快照 | `1; True` | 同時驗證 `3 / 2` 截斷為 `1`，以及值與左右物件參考均維持不變 |
| 9 | 1000 個零值右斜節點 | `1000` | 題目上限 spot check |

## 建置與執行

已從 repository 根目錄實際驗證：

```bash
dotnet build leetcode_2265/leetcode_2265/leetcode_2265.csproj --nologo
dotnet run --no-build --project leetcode_2265/leetcode_2265/leetcode_2265.csproj
```

若直接開啟題目根目錄 `leetcode_2265/`，使用：

```bash
dotnet build leetcode_2265/leetcode_2265.csproj --nologo
dotnet run --no-build --project leetcode_2265/leetcode_2265.csproj
```

以下為 fresh run 的完整輸出：

```text
Case: Official example; Input: [4,8,5,0,1,null,6]
Expected: 5
Actual: 5
PASS
Case: Single node; Input: [1]
Expected: 1
Actual: 1
PASS
Case: Root equals truncated average; Input: [2,1,4]
Expected: 3
Actual: 3
PASS
Case: Root does not equal average; Input: [9,1,1]
Expected: 2
Actual: 2
PASS
Case: All zeroes; Input: [0,0,0]
Expected: 3
Actual: 3
PASS
Case: Right-skewed mixed values; Input: [3,null,1,null,0]
Expected: 1
Actual: 1
PASS
Case: Repeated call on same official tree; Input: same [4,8,5,0,1,null,6] instance
Expected: (5, 5)
Actual: (5, 5)
PASS
Case: Truncating average and tree topology preservation; Input: snapshot [2,1]
Expected: 1; True
Actual: 1; True
PASS
Case: Right-skewed limit spot check; Input: 1000 zero-valued nodes
Expected: 1000
Actual: 1000
PASS
Summary: 9/9 checks passed.
```

## 舊版檔案整理

已逐檔移除舊式 `leetcode_2265.sln`、`App.config` 與 `Properties/AssemblyInfo.cs`。SDK-style `net10.0` 專案由 `leetcode_2265.csproj` 集中管理組件資訊與建置設定，因此不保留這些 .NET Framework 產物。

## 專案結構

```plaintext
leetcode_2265/
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
└── leetcode_2265/
    ├── Program.cs
    └── leetcode_2265.csproj
```
