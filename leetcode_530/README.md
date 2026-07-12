# 530. Minimum Absolute Difference in BST

## 題目名稱

- English: **Minimum Absolute Difference in BST**
- 繁體中文：**二叉搜尋樹的最小絕對差**
- [LeetCode 英文題目](https://leetcode.com/problems/minimum-absolute-difference-in-bst/description/)
- [力扣中文題目](https://leetcode.cn/problems/minimum-absolute-difference-in-bst/)

## 題目說明

給定一棵二叉搜尋樹的根節點，回傳樹中任意兩個不同節點值之間的最小絕對差。

本題限制如下：

- 節點數量介於 `2` 到 `10,000`。
- `0 <= Node.val <= 100,000`。
- 輸入符合二叉搜尋樹的結構。

## 解題核心

二叉搜尋樹的中序遍歷順序是由小到大，因此排序後任意兩個值的最小差，必定出現在
中序序列的相鄰元素之間。走訪每個節點時，只要保留前一個節點值，便能在線性時間內
更新目前的最小差距，不需要額外收集或排序所有節點。

本實作使用 `hasPrevious` 明確表示是否已經走訪第一個節點，避免使用 `-1` sentinel，
也避免以 static 欄位保存狀態。每次呼叫 `GetMinimumDifference` 都會建立獨立的區域狀態。

## 演算法走查

以官方範例 `[4,2,6,1,3]` 為例，中序遍歷得到 `[1,2,3,4,6]`：

```plaintext
相鄰差距：1、1、1、2
最小差距：1
```

遞迴流程是：先走訪左子樹，再比較目前節點與前一個中序節點，最後走訪右子樹。
因為 BST 的中序結果已經遞增，所以 `node.val - previousValue` 就是非負的相鄰差距。

## 複雜度

- 時間複雜度：`O(n)`，每個節點只走訪一次。
- 結果空間：`O(1)`，方法只回傳一個整數結果。
- 輔助空間：`O(h)`，`h` 是樹高，來自遞迴呼叫堆疊。
- 不使用儲存所有節點的陣列，也不需要額外排序。

## Acceptance Harness

`Main` 集中輸出所有驗證結果，每項檢查都顯示案例名稱、Expected、Actual 與
`PASS`/`FAIL`。最後一項使用 `0..9999` 建立 10,000 節點平衡 BST，只驗證結果，不輸出
整棵樹。

| 案例 | 目的 | Expected |
| --- | --- | ---: |
| 官方範例 1 | 標準多層 BST | `1` |
| 官方範例 2 | 含 `0` 與不平衡分支 | `1` |
| 最小有效輸入 | 兩個節點的最小輸入 | `1` |
| 最小差值來自中序相鄰節點 | 驗證不只比較直接父子節點 | `3` |
| 值域下界與上界 | 驗證 `0` 與 `100000` | `100000` |
| 高值相鄰 | 驗證上界附近的差距 | `1` |
| 多層不規則 BST | 驗證多層左右遞迴 | `5` |
| 10,000 節點上限 spot check | 驗證題目節點數上限 | `1` |

## 建置與執行

從題目根目錄 `leetcode_530/` 執行：

```bash
dotnet build leetcode_530/leetcode_530.csproj --nologo
dotnet run --no-build --project leetcode_530/leetcode_530.csproj
```

從 repository 根目錄執行完整驗證時，使用巢狀專案的明確路徑：

```bash
jq empty leetcode_530/.vscode/launch.json leetcode_530/.vscode/tasks.json
dotnet build leetcode_530/leetcode_530/leetcode_530.csproj --nologo
dotnet run --no-build --project leetcode_530/leetcode_530/leetcode_530.csproj
git diff --check -- leetcode_530
```

本題沒有正式測試專案；可執行 acceptance harness 是本題的驗證機制。

## 實際執行輸出

```text
Case: 官方範例 1
Expected: 1
Actual: 1
Result: PASS

Case: 官方範例 2
Expected: 1
Actual: 1
Result: PASS

Case: 最小有效輸入
Expected: 1
Actual: 1
Result: PASS

Case: 最小差值來自中序相鄰節點
Expected: 3
Actual: 3
Result: PASS

Case: 值域下界與上界
Expected: 100000
Actual: 100000
Result: PASS

Case: 高值相鄰
Expected: 1
Actual: 1
Result: PASS

Case: 多層不規則 BST
Expected: 5
Actual: 5
Result: PASS

Case: 10,000 節點上限 spot check
Expected: 1
Actual: 1
Result: PASS

Summary: 8/8 checks passed.
```

## 專案結構

```plaintext
leetcode_530/
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
│       └── specs/
└── leetcode_530/
    ├── Program.cs
    └── leetcode_530.csproj
```
