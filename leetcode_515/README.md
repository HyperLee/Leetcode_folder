# LeetCode 515：Find Largest Value in Each Tree Row／在每个树行中找最大值

此 .NET 10 主控台專案以深度優先搜尋（DFS）找出二叉樹每一層的最大值。公開
`LargestValues` API 不輸出主控台，`Main` 則負責執行十項可重複的 acceptance checks。

## 題目連結

- [English: 515. Find Largest Value in Each Tree Row](https://leetcode.com/problems/find-largest-value-in-each-tree-row/description/)
- [中文：515. 在每个树行中找最大值](https://leetcode.cn/problems/find-largest-value-in-each-tree-row/)

## 題意與限制

**English.** Given the root of a binary tree, return an array of the largest value in each row of the tree (0-indexed).

**中文。** 給定一棵二叉樹的根節點 `root`，請找出該二叉樹中每一層的最大值。

官方限制如下：

- 節點數量介於 `0` 到 `10^4`。
- `-2^31 <= Node.val <= 2^31 - 1`。
- 空樹的答案是空集合。

## 解法：深度優先搜尋

公開方法為：

```csharp
public static IList<int> LargestValues(TreeNode? root)
```

DFS 傳遞目前深度 `depth`，並讓結果清單的索引與深度一一對應：

1. 第一次抵達某個深度時，該節點是這一層的第一個候選值，因此加入結果清單。
2. 再次抵達相同深度時，以 `Math.Max` 比較並更新該層最大值。
3. 先走訪左子樹，再走訪右子樹；走訪順序不影響每層最大值。

核心不變量是：完成任一節點的處理後，`result[d]` 永遠等於目前已走訪深度
`d` 節點中的最大值。空樹在 DFS 前直接回傳空集合，不需要額外 sentinel，因此
`int.MinValue` 也能正確作為節點值。

### 複雜度

- 時間複雜度：`O(n)`，每個節點只走訪一次。
- 結果空間：`O(h)`，其中 `h` 是樹的高度層數。
- DFS call stack：`O(h)`；這是保留原始 DFS 教學主軸的取捨。
- 公開 API 不修改輸入樹的節點值或左右連結。

## 逐步範例

以 `root = [1,3,2,5,3,null,9]` 為例：

```plaintext
深度 0：先讀到 1，結果為 [1]
深度 1：讀到 3，結果為 [1, 3]
深度 2：讀到 5，結果為 [1, 3, 5]
深度 2：讀到 3，更新 max(5, 3)，結果仍為 [1, 3, 5]
回到右子樹深度 1：讀到 2，更新 max(3, 2)，結果仍為 [1, 3, 5]
深度 2：讀到 9，更新 max(5, 9)，結果為 [1, 3, 9]
```

## Acceptance harness 案例

| # | 案例 | 驗證重點 |
|---:|---|---|
| 1 | 官方範例 `[1,3,2,5,3,null,9]` | 基本 DFS 與逐層最大值 |
| 2 | 官方範例 `[1,2,3]` | 根節點與第二層 |
| 3 | `root = null` | 空樹回傳空集合 |
| 4 | 單節點 `int.MinValue` | 最小整數邊界 |
| 5 | 全負數與零 | 不依賴錯誤 sentinel |
| 6 | 左偏樹 | 深度索引逐層增加 |
| 7 | 同層多節點 | DFS 後續節點更新最大值 |
| 8 | `int.MinValue` / `int.MaxValue` | 整數上下界 |
| 9 | 固定節點參考與值 | 解法不修改輸入樹 |
| 10 | 10,000 節點完整樹 | 題目上限 spot check |

## 建置與執行

從題目根目錄 `/Users/qiuzili/Leetcode/Leetcode_folder/leetcode_515/` 執行：

```bash
dotnet build leetcode_515/leetcode_515.csproj --nologo
dotnet run --no-build --project leetcode_515/leetcode_515.csproj
```

從 repository 根目錄執行完整 transcript 驗證：

```bash
dotnet build leetcode_515/leetcode_515/leetcode_515.csproj --nologo
dotnet run --no-build --project leetcode_515/leetcode_515/leetcode_515.csproj
```

## 實際驗證輸出

以下內容由 fresh `dotnet run --no-build` 產生，README 中只有這一個 `text` fence：

```text
LeetCode 515 acceptance harness

Case 1: Official example 1
Input: root = [1,3,2,5,3,null,9]
Expected: [1, 3, 9]
Actual: [1, 3, 9]
Result: PASS

Case 2: Official example 2
Input: root = [1,2,3]
Expected: [1, 3]
Actual: [1, 3]
Result: PASS

Case 3: Empty tree
Input: root = null
Expected: []
Actual: []
Result: PASS

Case 4: Single int.MinValue node
Input: root = [-2147483648]
Expected: [-2147483648]
Actual: [-2147483648]
Result: PASS

Case 5: Negative values and zero
Input: root = [-10,-20,-5,null,null,-2147483648,0]
Expected: [-10, -5, 0]
Actual: [-10, -5, 0]
Result: PASS

Case 6: Left-skewed tree
Input: root = [1,2,null,3,null,4]
Expected: [1, 2, 3, 4]
Actual: [1, 2, 3, 4]
Result: PASS

Case 7: Same-level maximum update
Input: root = [1,2,3,4,10,8,7]
Expected: [1, 3, 10]
Actual: [1, 3, 10]
Result: PASS

Case 8: Integer boundaries
Input: root = [-2147483648,2147483647,-1,-2147483648,2147483647,null,0]
Expected: [-2147483648, 2147483647, 2147483647]
Actual: [-2147483648, 2147483647, 2147483647]
Result: PASS

Case 9: Read-only tree structure
Input: root = [5,1,9,null,6,12]
Expected: values = [5, 9, 12]; structure unchanged = True
Actual: values = [5, 9, 12]; structure unchanged = True
Result: PASS

Case 10: 10,000-node complete tree
Input: nodeCount = 10000; value = heap index
Expected: [0, 2, 6, 14, 30, 62, 126, 254, 510, 1022, 2046, 4094, 8190, 9999]
Actual: [0, 2, 6, 14, 30, 62, 126, 254, 510, 1022, 2046, 4094, 8190, 9999]
Result: PASS

Summary: 10/10 checks passed.
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
├── AGENTS.md
├── docs/
│   ├── readme-template.md
│   └── superpowers/
│       ├── plans/2026-07-12-leetcode-515-net10-migration.md
│       └── specs/2026-07-12-leetcode-515-net10-migration-design.md
├── leetcode_515/
│   ├── Program.cs
│   └── leetcode_515.csproj
└── README.md
```
