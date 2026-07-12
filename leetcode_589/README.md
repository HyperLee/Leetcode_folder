# 589. N-ary Tree Preorder Traversal

## 題目

給定一棵 N 元樹的根節點，回傳所有節點值的前序走訪結果。前序走訪會先
處理目前節點，再按照 `children` 陣列的原始順序遞迴處理每一棵子樹。

- 英文題目：[N-ary Tree Preorder Traversal](https://leetcode.com/problems/n-ary-tree-preorder-traversal/)
- 中文題目：[N 叉樹的前序遍歷](https://leetcode.cn/problems/n-ary-tree-preorder-traversal/)

### 限制條件

- 節點總數介於 `0` 與 `10^4` 之間。
- `0 <= Node.val <= 10^4`。
- 樹高不超過 `1000`。

## 解題思路

`Preorder(Node? root)` 建立結果集合後呼叫遞迴 helper。每次進入節點時，先
將 `node.val` 放入結果，再依序呼叫每個 child；遇到空樹時直接返回空集合。

### 不變量

進入 `PreorderVisit` 時，該節點的值一定會在所有子樹值之前加入結果。只要
children 的迭代順序不被排序或反轉，結果就會維持題目要求的前序順序。

### 複雜度

- 時間：`O(n)`，每個節點只走訪一次。
- 結果空間：`O(n)`，需要保存所有節點值。
- 輔助空間：`O(h)`，來自遞迴呼叫堆疊；`h` 是樹高。

## 逐步範例

對官方範例 1：root `1` 的 children 是 `[3, 2, 4]`，而 `3` 的 children
是 `[5, 6]`：

```plaintext
1 → 3 → 5 → 6 → 2 → 4
```

因此結果為 `[1, 3, 5, 6, 2, 4]`。注意不能先排序 children，否則會改變
題目定義的走訪順序。

## Acceptance Harness

| 案例 | 驗證重點 |
| --- | --- |
| Official example 1 | 根節點優先與巢狀 children 順序 |
| Official example 2 | 多層、多分支遞迴 |
| Minimum: null root | 最小有效空樹 |
| Minimum: single leaf | leaf 的空 children 集合 |
| Children keep input order | 不排序、不反轉 children |
| Repeated invocations | 不共享跨呼叫狀態 |
| Height upper-bound spot check | 1000 層遞迴深度 |
| Traversal invariants | 節點數、root-first 與完整序列 |

本專案沒有正式測試專案；上述 acceptance harness 由 `Main` 執行，所有
案例都會列印 Input、Expected、Actual 與 PASS/FAIL。

## 建置與執行

從 `leetcode_589/` 題目資料夾執行：

```bash
dotnet build leetcode_589/leetcode_589.csproj --nologo
dotnet run --no-build --project leetcode_589/leetcode_589.csproj
```

最新驗證輸出：

```text
Case: Official example 1
Input: [1,null,3,2,4,null,5,6]
Expected: [1, 3, 5, 6, 2, 4]
Actual: [1, 3, 5, 6, 2, 4]
PASS
Case: Official example 2
Input: [1,null,2,3,4,5,null,null,6,7,null,8,null,9,10,null,null,11,null,12,null,13,null,null,14]
Expected: [1, 2, 3, 6, 7, 11, 14, 4, 8, 12, 5, 9, 13, 10]
Actual: [1, 2, 3, 6, 7, 11, 14, 4, 8, 12, 5, 9, 13, 10]
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
Expected: [10, 30, 5, 20, 40]
Actual: [10, 30, 5, 20, 40]
PASS
Case: Repeated invocations
Input: first root [1,2], then root [9,8]
Expected: [1, 2], then [9, 8]
Actual: [1, 2], then [9, 8]
PASS
Case: Height upper-bound spot check
Input: 1000-node chain with values 0..999
Expected: count=1000, first=0, last=999
Actual: count=1000, first=0, last=999
PASS
Case: Traversal invariants
Input: root 1 -> children 2 -> 4 and 3 -> 5
Expected: count=5, first=1, sequence=[1, 2, 4, 3, 5]
Actual: count=5, first=1, sequence=[1, 2, 4, 3, 5]
PASS
Summary: 8/8 checks passed.
```

## 專案結構

```plaintext
leetcode_589/
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
└── leetcode_589/
    ├── Program.cs
    └── leetcode_589.csproj
```
