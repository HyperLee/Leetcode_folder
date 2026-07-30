# LeetCode 230：二元搜尋樹中第 K 小的元素

這是一個以 .NET 10 主控台程式實作的教學範例。專案保留兩種中序遍歷解法：
遞迴深度優先搜尋，以及使用顯式堆疊的疊代版本。`Main` 內建固定案例，
可直接比較兩種解法的預期結果與實際結果。

## 快速導覽

- [題目說明](#題目說明)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：遞迴中序遍歷](#解法一遞迴中序遍歷)
- [解法二：疊代中序遍歷](#解法二疊代中序遍歷)
- [兩種解法比較](#兩種解法比較)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)

## 題目說明

給定一棵二元搜尋樹（Binary Search Tree，BST）的根節點 `root`，以及整數
`k`，回傳樹中第 `k` 小的節點值。順位從 1 開始計算。

題目連結：

- [LeetCode 230 - Kth Smallest Element in a BST](https://leetcode.com/problems/kth-smallest-element-in-a-bst/description/)
- [力扣 230 - 二叉搜索樹中第 K 小的元素](https://leetcode.cn/problems/kth-smallest-element-in-a-bst/description/)

### 官方範例

```text
輸入：root = [3,1,4,null,2], k = 1
輸出：1

輸入：root = [5,3,6,2,4,null,null,1], k = 3
輸出：3
```

### 限制條件

- 樹中的節點數為 `n`。
- `1 <= k <= n <= 10^4`
- `0 <= Node.val <= 10^4`
- `root` 是合法的二元搜尋樹。

> [!NOTE]
> LeetCode 保證樹不為空且 `k` 是有效順位。本專案另外定義防禦性行為：
> 空樹、`k <= 0` 或 `k` 大於節點數時回傳 `-1`。

## 解題概念與出發點

BST 對每個節點都維持以下關係：

- 左子樹的值小於目前節點。
- 右子樹的值大於目前節點。

因此，按照「左子樹 → 目前節點 → 右子樹」執行中序遍歷，節點值會以遞增
順序出現。題目不需要重新排序所有節點，只要在中序遍歷過程中記錄目前走到
第幾個節點；當順位到達 `k` 時，該節點就是答案。

例如 BST `[5,3,6,2,4,null,null,1]` 的中序順序為：

```text
1 → 2 → 3 → 4 → 5 → 6
```

所以 `k = 3` 時答案為 `3`。

## 解法一：遞迴中序遍歷

### 設計說明

公開方法 `KthSmallest` 先檢查空樹與非正數 `k`，再建立只屬於本次呼叫的
`remaining` 計數器。私有方法 `TryFindKthSmallest` 負責遞迴：

1. 遇到空子樹時回傳 `false`，表示沒有找到目標。
2. 先搜尋左子樹；若左側已找到答案，就立即向上回傳。
3. 左子樹處理完後才將 `remaining` 減 1，這與遞增的中序順位一致。
4. `remaining == 0` 時輸出目前節點值並回傳 `true`。
5. 尚未找到時才搜尋右子樹。

計數器是區域狀態，而不是儲存在 `Program` 物件的欄位，因此連續呼叫、
空樹或無效順位都不會讀到上一次執行留下的結果。布林回傳值也讓答案找到後
可以一路提早結束，不必繼續走訪其餘節點。

### 範例演示

以 `root = [5,3,6,2,4,null,null,1]`、`k = 3` 為例：

| 中序造訪節點 | `remaining` 變化 | 判斷 |
| --- | ---: | --- |
| `1` | `3 → 2` | 尚未到第 3 小 |
| `2` | `2 → 1` | 尚未到第 3 小 |
| `3` | `1 → 0` | 找到答案 `3` |

找到 `3` 後，遞迴呼叫會持續回傳 `true`，不再走訪 `4`、`5`、`6`。

### 複雜度

- 時間：有效順位下為 `O(h + k)`；若 `k` 無效而必須走完整棵樹，最壞為
  `O(n)`。
- 額外空間：`O(h)`，來自遞迴呼叫堆疊。

其中 `h` 是樹高，`n` 是節點數。平衡樹的 `h` 約為 `log n`；偏斜樹的
`h` 最壞可達 `n`。

## 解法二：疊代中序遍歷

### 設計說明

`KthSmallestIterative` 使用 `Stack<TreeNode>` 明確保存尚未處理的祖先節點：

1. 從目前節點一路向左，把沿途節點壓入堆疊。
2. 無法再往左時彈出堆疊頂端；這等同遞迴版從左子樹回到目前節點。
3. 每彈出一個節點就將 `k` 減 1；`k == 0` 時立即回傳節點值。
4. 接著轉往目前節點的右子樹，再重複壓入其左側路徑。
5. 堆疊清空且沒有目前節點時仍未找到，代表順位無效，回傳 `-1`。

這個版本不使用系統遞迴堆疊，控制流程與記憶體使用都直接呈現在程式中。
它仍然保存高度最多為 `h` 的節點，因此漸進空間複雜度與遞迴版相同。

### 範例演示

以 `root = [3,1,4,null,2]`、`k = 1` 為例：

1. 從 `3` 向左走到 `1`，堆疊依序成為 `[3, 1]`。
2. `1` 沒有左子節點，彈出 `1`。
3. `k` 從 `1` 減為 `0`，因此直接回傳 `1`。
4. 因為答案已找到，節點 `2`、`3`、`4` 不需要再走訪。

### 複雜度

- 時間：有效順位下為 `O(h + k)`；必須走完整棵樹時最壞為 `O(n)`。
- 額外空間：`O(h)`，來自顯式堆疊。

## 兩種解法比較

| 比較項目 | 遞迴中序遍歷 | 疊代中序遍歷 |
| --- | --- | --- |
| 主要 API | `KthSmallest` | `KthSmallestIterative` |
| 狀態管理 | 區域計數器與呼叫堆疊 | 區域計數器與顯式堆疊 |
| 可讀性 | 接近中序遍歷定義，較精簡 | 控制流程較明確，但程式較長 |
| 提早終止 | 以布林結果向上傳遞 | 找到時直接 `return` |
| 深樹風險 | 偏斜樹可能消耗大量呼叫堆疊 | 不依賴遞迴呼叫，但仍需 `O(h)` 堆疊 |
| 有效輸入時間 | `O(h + k)` | `O(h + k)` |
| 最壞時間 | `O(n)` | `O(n)` |
| 額外空間 | `O(h)` | `O(h)` |

## 可執行驗證案例

`Main` 使用同一組固定資料分別呼叫兩種解法。每筆案例包含手動推導的預期值，
每種解法各算一項檢查，共 7 筆案例、14 項檢查。

| 案例 | 輸入重點 | 預期 |
| --- | --- | ---: |
| 1 | 官方範例一，查找最小值 | `1` |
| 2 | 官方範例二，查找第三小值 | `3` |
| 3 | 單節點與節點值下界 | `0` |
| 4 | 右偏斜樹與最後順位 | `4` |
| 5 | 空樹 | `-1` |
| 6 | `k = 0` | `-1` |
| 7 | `k` 大於節點數 | `-1` |

## 建置與執行

需求：

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

從本 repository 根目錄執行：

```bash
dotnet restore leetcode_230/leetcode_230.csproj
dotnet build leetcode_230/leetcode_230.csproj --no-restore --nologo
dotnet run --project leetcode_230/leetcode_230.csproj --no-build
```

目前沒有獨立的自動化測試專案；驗收方式是成功建置，再執行 `Main` 內的固定
Expected/Actual 驗證案例。

## 實際執行結果

以下內容來自上述 `dotnet run` 命令：

```text
Kth Smallest Element in a BST sample verification

Case 1: LeetCode 範例 1：查找最小值
Input: root = [3, 1, 4, null, 2], k = 1
Expected: 1
Recursive Actual: 1 (PASS)
Iterative Actual: 1 (PASS)

Case 2: LeetCode 範例 2：查找第三小值
Input: root = [5, 3, 6, 2, 4, null, null, 1], k = 3
Expected: 3
Recursive Actual: 3 (PASS)
Iterative Actual: 3 (PASS)

Case 3: 單一節點：節點值為限制下界
Input: root = [0], k = 1
Expected: 0
Recursive Actual: 0 (PASS)
Iterative Actual: 0 (PASS)

Case 4: 右偏斜樹：查找最後順位
Input: root = [1, null, 2, null, 3, null, 4], k = 4
Expected: 4
Recursive Actual: 4 (PASS)
Iterative Actual: 4 (PASS)

Case 5: 空樹：沒有可回傳的節點
Input: root = [], k = 1
Expected: -1
Recursive Actual: -1 (PASS)
Iterative Actual: -1 (PASS)

Case 6: 無效順位：k 為 0
Input: root = [2, 1, 3], k = 0
Expected: -1
Recursive Actual: -1 (PASS)
Iterative Actual: -1 (PASS)

Case 7: 無效順位：k 大於節點數
Input: root = [2, 1, 3], k = 4
Expected: -1
Recursive Actual: -1 (PASS)
Iterative Actual: -1 (PASS)

Summary: 14/14 checks passed.
```

## 專案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_230.sln
└── leetcode_230/
    ├── leetcode_230.csproj
    └── Program.cs
```
