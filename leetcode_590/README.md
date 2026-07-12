# 590. N-ary Tree Postorder Traversal / N 元樹的後序遍歷

## 題目

給定一棵 N 元樹的根節點，回傳所有節點值的後序走訪結果。後序走訪會先按照
`children` 陣列的原始順序完成每一棵子樹，最後才加入目前節點。

- 英文題目：[N-ary Tree Postorder Traversal](https://leetcode.com/problems/n-ary-tree-postorder-traversal/)
- 中文題目：[N 叉樹的後序遍歷](https://leetcode.cn/problems/n-ary-tree-postorder-traversal/)

英文題意是：Given the root of an n-ary tree, return the postorder traversal of
its nodes' values. 本專案的 `Node` 使用 `children` 表示任意數量的子節點，與
只有 `left`、`right` 的 binary tree 不同。

### 限制條件

- 節點總數介於 `0` 與 `10^4` 之間。
- `0 <= Node.val <= 10^4`。
- N 元樹高度不超過 `1000`。

## 解題思路

`Postorder(Node? root)` 建立結果集合後呼叫 `PostorderVisit`。helper 遇到空節點
立即返回；對非空節點則先依 `children` 的原始順序遞迴所有子樹，再把目前節點的
`val` 加入結果。這個「子樹先完成、根節點最後加入」就是後序遍歷的核心不變量。

### 不變量

進入一個節點時，該節點的值不會在任何 child subtree 完成前加入結果。因此每個
子樹的後序序列會維持完整且連續，兄弟節點也會保持 `children` 的輸入順序。

### 複雜度

- 時間：`O(n)`，每個節點只走訪一次。
- 結果空間：`O(n)`，需要保存所有節點值。
- 輔助空間：`O(h)`，來自遞迴呼叫堆疊；`h` 是樹高。

## 逐步範例

官方範例 1 的根節點是 `1`，children 依序為 `[3, 2, 4]`；節點 `3` 的 children
是 `[5, 6]`。走訪順序如下：

```plaintext
先走 3 的子樹：5 → 6 → 3
再走根節點的其他子樹：2 → 4
最後加入根節點：1
結果：[5, 6, 3, 2, 4, 1]
```

不能先排序或反轉 `children`，否則會改變題目定義的後序順序。

## Acceptance Harness

| 案例 | 驗證重點 |
| --- | --- |
| Official example 1 | 根節點最後加入與巢狀 children 順序 |
| Official example 2 | 多層、多分支 N 元樹 |
| Minimum: null root | 最小有效空樹 |
| Minimum: single leaf | leaf 節點沒有 child 時的結果 |
| Children keep input order | 不排序、不反轉兄弟節點 |
| Repeated invocations | 不共享跨呼叫的結果狀態 |
| Height upper-bound spot check | 1000 層 chain 的遞迴邊界 |
| Traversal invariants | 節點數、根節點最後與完整序列 |

本專案沒有正式測試專案；上述 8 個 acceptance checks 由 `Main` 執行。每個案例
都會列印 Input、Expected、Actual 與 PASS/FAIL，任何失敗都會設定非零 exit code。

## 建置與執行

從 `leetcode_590/` 題目資料夾執行：

```bash
dotnet build leetcode_590/leetcode_590.csproj --nologo
dotnet run --no-build --project leetcode_590/leetcode_590.csproj
```

最新驗證輸出：

```text
Case: Official example 1
Input: [1,null,3,2,4,null,5,6]
Expected: [5, 6, 3, 2, 4, 1]
Actual: [5, 6, 3, 2, 4, 1]
PASS
Case: Official example 2
Input: [1,null,2,3,4,5,null,null,6,7,null,8,null,9,10,null,null,11,null,12,null,13,null,null,14]
Expected: [2, 6, 14, 11, 7, 3, 12, 8, 4, 13, 9, 10, 5, 1]
Actual: [2, 6, 14, 11, 7, 3, 12, 8, 4, 13, 9, 10, 5, 1]
PASS
Case: Minimum: null root
Input: root = null
Expected: []
Actual: []
PASS
Case: Minimum: single leaf
Input: root = [42]
Expected: [42]
Actual: [42]
PASS
Case: Children keep input order
Input: root 10 -> children 30, 20, 40; 30 -> child 5
Expected: [5, 30, 20, 40, 10]
Actual: [5, 30, 20, 40, 10]
PASS
Case: Repeated invocations
Input: first root [1,2], then root [9,8]
Expected: [2, 1], then [8, 9]
Actual: [2, 1], then [8, 9]
PASS
Case: Height upper-bound spot check
Input: 1000-node chain with values 0..999
Expected: count=1000, first=999, last=0
Actual: count=1000, first=999, last=0
PASS
Case: Traversal invariants
Input: root 1 -> children 2 -> 4 and 3 -> 5
Expected: count=5, root-last=1, sequence=[4, 2, 5, 3, 1]
Actual: count=5, root-last=1, sequence=[4, 2, 5, 3, 1]
PASS
Summary: 8/8 checks passed.
```

## 專案結構

```plaintext
leetcode_590/
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
└── leetcode_590/
    ├── Program.cs
    └── leetcode_590.csproj
```
