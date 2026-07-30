# 235. Lowest Common Ancestor of a Binary Search Tree／二元搜尋樹的最近公共祖先

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![C%23](https://img.shields.io/badge/language-C%23-239120)

這是一個 C#／.NET 10 Console 專案，示範如何利用二元搜尋樹的排序特性，找出兩個指定節點的最近公共祖先。專案保留遞迴解法，並加入不使用遞迴呼叫堆疊的迭代解法；`Main` 內含可直接執行的 acceptance harness。

- [LeetCode English](https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-search-tree/description/)
- [LeetCode 中文](https://leetcode.cn/problems/lowest-common-ancestor-of-a-binary-search-tree/description/)

## 題目說明

給定一棵二元搜尋樹（Binary Search Tree，BST）的根節點 `root`，以及樹中的兩個不同節點 `p`、`q`，請回傳兩者的最近公共祖先。

最近公共祖先是同時包含 `p` 與 `q` 的最深節點；題目允許節點成為自己的祖先，因此若 `p` 是 `q` 的祖先，答案可以直接是 `p`。

例如：

```text
root = [6, 2, 8, 0, 4, 7, 9, null, null, 3, 5]

        6
      /   \
     2     8
    / \   / \
   0   4 7   9
      / \
     3   5
```

- `p = 2`、`q = 8` 時，兩個節點分居根節點兩側，答案是 `6`。
- `p = 2`、`q = 4` 時，`2` 本身就是 `4` 的祖先，答案是 `2`。

## 限制條件

- 樹的節點數量範圍為 `2 <= n <= 10^5`。
- 節點值範圍為 `-10^9 <= Node.val <= 10^9`。
- 所有 `Node.val` 都互不相同。
- `p != q`。
- `p` 與 `q` 都存在於給定的 BST 中。
- 本專案的兩個公開解法依照上述有效輸入契約運作，不另外定義空樹、重複值或目標節點不存在時的行為。

## 解題概念與出發點

一般二元樹必須搜尋多個分支，才能知道 `p` 與 `q` 分別位於何處；BST 則提供更強的排序資訊：

- 左子樹的所有值都小於目前節點值。
- 右子樹的所有值都大於目前節點值。

令目前節點值為 `current`：

1. 若 `p.val` 與 `q.val` 都小於 `current`，兩個目標都在左子樹，最近公共祖先也只能在左子樹。
2. 若 `p.val` 與 `q.val` 都大於 `current`，兩個目標都在右子樹，最近公共祖先也只能在右子樹。
3. 其餘情況代表兩個目標分居兩側，或目前節點正好等於其中一個目標；目前節點就是搜尋過程遇到的最低分岔點，也就是答案。

這個判斷與 `p`、`q` 的傳入順序無關，而且每一層只會選擇一個子樹，不需要走訪整棵樹。

## 解法比較

| 解法 | 公開方法 | 核心做法 | 時間複雜度 | 輔助空間 | 結果空間 |
| --- | --- | --- | --- | --- | --- |
| 遞迴 BST 剪枝 | `LowestCommonAncestor` | 依大小關係遞迴進入單一子樹 | `O(h)` | `O(h)` | `O(1)` |
| 迭代 BST 剪枝 | `LowestCommonAncestor2` | 以指標迴圈向單一子樹移動 | `O(h)` | `O(1)` | `O(1)` |

`h` 是樹高。平衡 BST 的 `h` 約為 `log n`；完全向單側傾斜時，`h` 最壞可達 `n`。

## 解法一：遞迴 BST 剪枝

### 設計說明

`LowestCommonAncestor` 把「目前節點所代表的子樹」視為一個較小的相同問題：

- 兩個目標都較小：以 `root.left` 作為新根節點遞迴。
- 兩個目標都較大：以 `root.right` 作為新根節點遞迴。
- 目標不再同向：目前節點就是最低分岔點，結束遞迴。

每次呼叫都排除一整側不可能包含答案的子樹，因此只沿著一條根到節點的路徑前進。題目保證 `p`、`q` 存在於樹中，所以當兩者同在某側時，對應子節點一定存在。

### 不變量

每次進入 `LowestCommonAncestor(root, p, q)` 時：

- `root` 所代表的子樹一定同時包含 `p` 與 `q`。
- 若兩個目標值都位於 `root.val` 的同一側，答案一定也位於該側。
- 第一個無法繼續同向縮小的節點，就是最近公共祖先。

### 範例演示流程

輸入 `p = 3`、`q = 5`：

| 遞迴層級 | 目前節點 | 比較結果 | 下一步 |
| ---: | ---: | --- | --- |
| 1 | 6 | `3 < 6` 且 `5 < 6` | 遞迴進入左子樹，根改為 `2` |
| 2 | 2 | `3 > 2` 且 `5 > 2` | 遞迴進入右子樹，根改為 `4` |
| 3 | 4 | `3 < 4`、`5 > 4` | 兩者分居兩側，回傳節點 `4` |

節點 `4` 同時是 `3` 與 `5` 的祖先，而且比 `6`、`2` 更深，因此是最近公共祖先。

### 複雜度

- 時間：`O(h)`，最多沿樹高走過一條路徑。
- 輔助空間：`O(h)`，每前進一層就多一層遞迴呼叫堆疊。
- 結果空間：`O(1)`，只回傳既有節點參考，不建立新的結果集合。

對高度接近 `n` 的極端傾斜樹，遞迴版本也會建立接近 `n` 層的呼叫堆疊；若執行環境的堆疊深度有限，可優先使用迭代版本。

## 解法二：迭代 BST 剪枝

### 設計說明

`LowestCommonAncestor2` 使用 `current` 保存目前節點，將遞迴的「呼叫下一層」改成直接更新指標：

1. `current` 從根節點開始。
2. 兩個目標都較小時，執行 `current = current.left`。
3. 兩個目標都較大時，執行 `current = current.right`。
4. 兩個目標不再同向時，回傳 `current`。

它與遞迴版本使用完全相同的剪枝判斷，但不需要保存每一層返回位置，因為找到分岔點後可以直接回傳，不必回頭組合任何結果。

### 不變量

每輪迴圈開始時：

- `current` 所代表的子樹同時包含 `p` 與 `q`。
- 已排除的另一側子樹不可能包含最近公共祖先。
- `current` 只會往下一層移動，迴圈最多執行 `h` 次。

### 範例演示流程

同樣輸入 `p = 3`、`q = 5`：

| 迴圈輪次 | `current` | 比較結果 | 更新 |
| ---: | ---: | --- | --- |
| 1 | 6 | 兩個目標都小於 `6` | `current = 2` |
| 2 | 2 | 兩個目標都大於 `2` | `current = 4` |
| 3 | 4 | `3` 在左側、`5` 在右側 | 回傳 `current`，答案為 `4` |

整個過程只保留一個 `current` 節點參考，不會隨樹高增加額外容器或呼叫堆疊。

### 複雜度

- 時間：`O(h)`，最多沿樹高走過一條路徑。
- 輔助空間：`O(1)`，只使用固定數量的節點參考與比較值。
- 結果空間：`O(1)`，直接回傳樹中既有節點。

## Acceptance Harness

目前沒有獨立的自動化測試專案；`Main` 是可重複執行的 acceptance harness。每個案例會：

1. 依指定順序建立一棵 BST。
2. 從樹中找出實際的 `p`、`q` 與預期答案節點。
3. 分別執行遞迴與迭代解法。
4. 使用 `ReferenceEquals` 確認回傳的是預期的同一個樹節點，而不只比較數值。
5. 任一檢查失敗時將 process exit code 設為 `1`。

| # | 案例 | `p` | `q` | 預期節點 |
| ---: | --- | ---: | ---: | ---: |
| 1 | 官方案例 1／根節點分岔 | 2 | 8 | 6 |
| 2 | 官方案例 2／祖先是 `p` 本身 | 2 | 4 | 2 |
| 3 | 官方案例 3／最小樹 | 2 | 1 | 2 |
| 4 | 左子樹深層節點 | 3 | 5 | 4 |
| 5 | 右子樹節點 | 7 | 9 | 8 |
| 6 | 反向傳入 `p`、`q` | 8 | 2 | 6 |
| 7 | 含負值的 BST | -5 | -3 | -4 |
| 8 | 完全向右傾斜的 BST | 8 | 10 | 8 |

兩個解法各驗證八個案例，因此共有 16 項檢查。

## 建置與執行

從本 repository 根目錄執行：

```bash
dotnet restore leetcode_235/leetcode_235.csproj
dotnet build leetcode_235/leetcode_235.csproj --no-restore --nologo
dotnet run --no-build --project leetcode_235/leetcode_235.csproj
```

Fresh run 的完整輸出：

```text
LeetCode 235 - Lowest Common Ancestor of a Binary Search Tree
====================================================================

[1] Official example 1 / split at root
Tree insertion order: [6, 2, 8, 0, 4, 7, 9, 3, 5]
p = 2, q = 8, expected node = 6
Recursive: 6 (PASS)
Iterative: 6 (PASS)

[2] Official example 2 / ancestor is p
Tree insertion order: [6, 2, 8, 0, 4, 7, 9, 3, 5]
p = 2, q = 4, expected node = 2
Recursive: 2 (PASS)
Iterative: 2 (PASS)

[3] Official example 3 / minimum tree
Tree insertion order: [2, 1]
p = 2, q = 1, expected node = 2
Recursive: 2 (PASS)
Iterative: 2 (PASS)

[4] Deep nodes in left subtree
Tree insertion order: [6, 2, 8, 0, 4, 7, 9, 3, 5]
p = 3, q = 5, expected node = 4
Recursive: 4 (PASS)
Iterative: 4 (PASS)

[5] Nodes in right subtree
Tree insertion order: [6, 2, 8, 0, 4, 7, 9, 3, 5]
p = 7, q = 9, expected node = 8
Recursive: 8 (PASS)
Iterative: 8 (PASS)

[6] Reversed p and q
Tree insertion order: [6, 2, 8, 0, 4, 7, 9, 3, 5]
p = 8, q = 2, expected node = 6
Recursive: 6 (PASS)
Iterative: 6 (PASS)

[7] Negative values
Tree insertion order: [-2, -4, 1, -5, -3, 0, 2]
p = -5, q = -3, expected node = -4
Recursive: -4 (PASS)
Iterative: -4 (PASS)

[8] Right-skewed tree
Tree insertion order: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
p = 8, q = 10, expected node = 8
Recursive: 8 (PASS)
Iterative: 8 (PASS)

Summary: 16/16 checks passed.
```

## 專案結構

```text
.
├── AGENTS.md
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_235.sln
└── leetcode_235/
    ├── Program.cs
    └── leetcode_235.csproj
```

## 驗證

修改後可使用下列指令確認格式與空白：

```bash
git diff --check
```

由於新建且尚未追蹤的檔案不會出現在一般 `git diff` 中，提交前也應另外確認 `README.md` 沒有行尾空白並保留正確的檔案結尾。
