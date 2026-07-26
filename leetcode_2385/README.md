# LeetCode 2385 - Amount of Time for Binary Tree to Be Infected

## 感染二叉樹需要的總時間

- [English problem](https://leetcode.com/problems/amount-of-time-for-binary-tree-to-be-infected/)
- [中文題目](https://leetcode.cn/problems/amount-of-time-for-binary-tree-to-be-infected/)

給定一棵節點值互異的二元樹與起始值 `start`。感染在第 0 分鐘出現在該節點，之後每分鐘可從已感染節點擴散到它的父節點、左子節點與右子節點。回傳整棵樹都被感染所需的分鐘數。

## 限制條件

- 節點數 `n` 為 `1 <= n <= 100000`。
- `1 <= Node.val <= 100000`，且每個節點值都唯一。
- `start` 必定存在於樹中。

## 解法：parent map + 逐層 BFS

二元樹的節點只持有子節點參考，感染卻能向父節點擴散。第一階段以一次 BFS 建立
`Dictionary<TreeNode, TreeNode?>`，令每個節點可查到父節點，同時找出起始節點。這把每條父子邊轉成可雙向走訪的關係。

第二階段從 `start` 做 BFS：queue 和 visited 都先加入 start，並把 `minutes` 設為 `-1`。每次 while 迴圈只處理進入該層時 queue 的固定數量；這些節點代表同一分鐘新感染的前緣。完成一層後再加一分鐘，因此單節點的唯一一層會讓答案從 `-1` 變成 `0`。

`visited` 是必要的不變量：建立 parent map 後，parent-child 關係成為無向圖；若沒有它，感染會立即沿剛走過的邊來回傳播。整個方法不修改節點，也不使用跨呼叫的可變狀態，所以能安全重複呼叫並保留原樹拓撲。

公開 API：

```csharp
public static int AmountOfTime(TreeNode root, int start)
```

| 項目 | 複雜度 |
| --- | --- |
| 時間 | `O(n)` |
| 結果空間 | `O(1)` |
| 輔助空間 | `O(n)`，parent map、queue 與 visited 最多各含 `n` 個節點 |

## 逐步走查

官方樹 `[1,5,3,null,4,10,6,9,2]` 從 `start = 3` 開始：

```plaintext
第 0 分鐘：3
第 1 分鐘：1、10、6
第 2 分鐘：5
第 3 分鐘：4
第 4 分鐘：9、2
```

最遠節點在第 4 分鐘感染，答案為 `4`。parent map 讓 `3` 能在第一分鐘感染其父節點 `1`，也讓後續感染可跨越根節點抵達左子樹。

## Acceptance harness

`Main` 是唯一 Console I/O 邊界。九項檢查皆用手算 literal expected value；任何失敗都會設定 process exit code 為 `1`。

| # | 案例 | 預期 | 驗證目的 |
| ---: | --- | ---: | --- |
| 1 | 官方樹，`start=3` | `4` | 一般父子雙向擴散 |
| 2 | 單節點 | `0` | `minutes=-1` 的層級起算邊界 |
| 3 | 跨根葉節點 | `4` | 從左葉走到右子樹 |
| 4 | 同一樹從根開始 | `2` | 根節點起始的樹高 |
| 5 | 五節點斜樹中間 | `2` | 兩個方向的最大距離 |
| 6 | 同一官方樹連續 `start=(3,9)` | `(4, 5)` | 防止跨呼叫狀態殘留 |
| 7 | 官方樹呼叫前後快照 | `4; True` | 同時檢查結果與完整拓撲未變 |
| 8 | 100,000 節點右斜樹 | `99999` | 上限與非遞迴線性走訪 |
| 9 | 不對稱樹內部起點 | `3` | 父節點與不同深度分支 |

## 建置與執行

已從 repository 根目錄實際驗證：

```bash
dotnet build leetcode_2385/leetcode_2385/leetcode_2385.csproj --nologo
dotnet run --no-build --project leetcode_2385/leetcode_2385/leetcode_2385.csproj
```

若直接開啟題目根目錄 `leetcode_2385/`，使用：

```bash
dotnet build leetcode_2385/leetcode_2385.csproj --nologo
dotnet run --no-build --project leetcode_2385/leetcode_2385.csproj
```

以下是 fresh run 的完整輸出：

```text
Case: Official example; Input: [1,5,3,null,4,10,6,9,2], start=3
Expected: 4
Actual: 4
PASS
Case: Single node; Input: [1], start=1
Expected: 0
Actual: 0
PASS
Case: Cross-root leaf; Input: [1,2,3,4,5,null,6], start=4
Expected: 4
Actual: 4
PASS
Case: Same tree from root; Input: [1,2,3,4,5,null,6], start=1
Expected: 2
Actual: 2
PASS
Case: Five-node skew from middle; Input: [1,null,2,null,3,null,4,null,5], start=3
Expected: 2
Actual: 2
PASS
Case: Repeated official-tree calls; Input: same official tree, start=(3,9)
Expected: (4, 5)
Actual: (4, 5)
PASS
Case: Official-tree result and topology preservation; Input: snapshot official tree, start=3
Expected: 4; True
Actual: 4; True
PASS
Case: 100,000-node skew; Input: right-skewed [1..100000], start=1
Expected: 99999
Actual: 99999
PASS
Case: Asymmetric internal start; Input: [8,3,10,1,6,null,14], start=3
Expected: 3
Actual: 3
PASS
Summary: 9/9 checks passed.
```

## 舊版檔案整理

已逐檔移除舊式 `leetcode_2385.sln`、`App.config` 與 `Properties/AssemblyInfo.cs`。SDK-style `net10.0` 專案由 `leetcode_2385.csproj` 集中管理組件資訊與建置設定，因此不保留這些 .NET Framework 產物。

## 專案結構

```plaintext
leetcode_2385/
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
└── leetcode_2385/
    ├── Program.cs
    └── leetcode_2385.csproj
```
