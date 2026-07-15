# LeetCode 872：Leaf-Similar Trees／葉子相似的樹

[英文題目](https://leetcode.com/problems/leaf-similar-trees/) ·
[中文題目](https://leetcode.cn/problems/leaf-similar-trees/)

這個 .NET 10 主控台專案實作 LeetCode 872：依由左至右的順序比較兩棵二元樹的
葉節點值序列。公開方法 `LeafSimilar` 只負責計算；`Main` 集中所有 acceptance
harness 輸出。

## 題目限制

- 每棵樹的節點數量介於 `1` 與 `200` 之間。
- 每個節點值皆滿足 `0 <= Node.val <= 200`。

實作只處理 LeetCode 的有效輸入契約，因此兩個根節點皆為非 `null`。

## 解法：深度優先搜尋葉序列

`LeafSimilar` 分別建立兩個 `List<int>`，再呼叫 `CollectLeaves` 收集兩棵樹的葉序列，
最後以 `SequenceEqual` 比較值、順序與數量是否完全一致。

DFS 維持兩個關鍵不變量：

1. 只有左右子節點都為 `null` 的節點才會加入葉序列。
2. 每個非葉節點固定先遞迴左子樹，再遞迴右子樹，因此加入順序就是全樹由左至右的
   葉節點順序。

## 複雜度

令兩棵樹分別有 `n`、`m` 個節點，葉節點數為 `l1`、`l2`，高度為 `h1`、`h2`：

- 時間複雜度：`O(n + m)`，每個節點各造訪一次，另比較兩個葉序列。
- 葉序列空間：`O(l1 + l2)`，分別保存兩棵樹的葉節點值。
- 遞迴空間：`O(h1 + h2)`，兩次 DFS 的呼叫堆疊上限合計由兩棵樹高度決定。

## 逐步走查

以第一組官方案例為例：

```plaintext
root1 依左至右遇到葉節點：6 -> 7 -> 4 -> 9 -> 8
root2 依左至右遇到葉節點：6 -> 7 -> 4 -> 9 -> 8
兩個序列的值、順序與數量完全一致
結果：true
```

第二組官方案例的葉序列分別為 `[2,3]` 與 `[3,2]`；即使值集合相同，順序不同仍
回傳 `false`。

## 可執行驗證

專案不建立正式測試專案；`Main` 的確定性驗收程式是目前的驗證機制。任一失敗都
會設定 `Environment.ExitCode = 1`。

| # | 案例 | 預期 | 驗證重點 |
|---:|---|---:|---|
| 1 | 官方相同葉序列範例 | `true` | 不同樹形產生相同完整葉序列 |
| 2 | 官方不同葉順序範例 | `false` | 值相同但順序不同 |
| 3 | 兩棵相同單節點樹 | `true` | 根節點本身即為葉節點 |
| 4 | 兩棵不同單節點樹 | `false` | 最小樹形但葉值不同 |
| 5 | 不同內部結構、相同 `[6,7,4]` | `true` | 只比較葉序列，不比較樹形 |
| 6 | `[1,2]` 與 `[2,1]` | `false` | 葉節點順序不可忽略 |
| 7 | `[5,5,8]` 與 `[5,8,8]` | `false` | 重複值的出現次數不同 |
| 8 | 兩者皆為 `[0,200]` | `true` | 節點值上下界 |
| 9 | 兩條 200 節點右鏈，末葉皆為 `200` | `true` | 節點數上限與遞迴深度 |

## 建置與執行

請把外層 `leetcode_872/` 當成工作目錄：

```bash
dotnet build leetcode_872/leetcode_872.csproj --nologo
dotnet run --no-build --project leetcode_872/leetcode_872.csproj
```

## 最新驗證輸出

以下是執行第二個命令的完整輸出，也是本 README 唯一的 `text` fence：

```text
LeetCode 872 acceptance harness

Case 1: Official example: same leaf sequence
Input: root1 = [3,5,1,6,2,9,8,null,null,7,4], root2 = [3,5,1,6,7,4,2,null,null,null,null,null,null,9,8]
PASS | Expected: true | Actual: true

Case 2: Official example: different leaf order
Input: root1 = [1,2,3], root2 = [1,3,2]
PASS | Expected: false | Actual: false

Case 3: Same single node
Input: root1 = [7], root2 = [7]
PASS | Expected: true | Actual: true

Case 4: Different single nodes
Input: root1 = [0], root2 = [200]
PASS | Expected: false | Actual: false

Case 5: Different structure with the same leaves
Input: leaf sequences = [6,7,4] and [6,7,4]
PASS | Expected: true | Actual: true

Case 6: Same values in a different order
Input: leaf sequences = [1,2] and [2,1]
PASS | Expected: false | Actual: false

Case 7: Repeated leaf multiplicity differs
Input: leaf sequences = [5,5,8] and [5,8,8]
PASS | Expected: false | Actual: false

Case 8: Boundary leaf values
Input: leaf sequences = [0,200] and [0,200]
PASS | Expected: true | Actual: true

Case 9: Maximum-depth right chains
Input: two 200-node right chains; internal values differ; final leaf = 200
PASS | Expected: true | Actual: true

Summary: 9/9 checks passed.
```

## 專案結構

```plaintext
leetcode_872/
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
│       │   └── 2026-07-15-leetcode-872-net10-migration.md
│       └── specs/
│           └── 2026-07-15-leetcode-872-net10-migration-design.md
└── leetcode_872/
    ├── Program.cs
    └── leetcode_872.csproj
```
