# 1721. Swapping Nodes in a Linked List／交換鏈結串列中的節點

- [LeetCode English](https://leetcode.com/problems/swapping-nodes-in-a-linked-list/)
- [LeetCode 繁體中文](https://leetcode.cn/problems/swapping-nodes-in-a-linked-list/)

## 題意

給定單向鏈結串列的頭節點 `head` 與整數 `k`，交換正數第 `k` 個節點和倒數第 `k` 個
節點的值，並回傳原鏈結串列的頭節點。串列採 1-based 索引。

## 限制條件

- 串列節點數量為 `n`。
- `1 <= k <= n <= 100000`。
- `0 <= Node.val <= 100`。

## 核心不變量與陷阱

題目要求交換的是兩個節點的值，不是節點物件本身。兩種實作都必須回傳原 `head`，所有
`next` 關係也必須保持不變。若正數與倒數第 `k` 個位置相同，與自己交換後串列不變。

常見錯誤包括把倒數位置算成 `n - k` 而少一、實際重排節點導致 topology 改變，或為了找
倒數位置而建立陣列，產生不必要的 `O(n)` 輔助空間。本題只接受有效輸入，因此不新增 null
或 `k` 越界的自訂行為。

## 解法一：先計算長度

`SwapNodes` 先走訪一次取得長度 `n`，再分別前進到第 `k` 個與第 `n - k + 1` 個節點，
最後交換兩個值。這個版本的索引關係直接、容易走查，但需要多次由 head 開始走訪。

## 解法二：固定距離雙指標

`SwapNodes2` 先讓 `front` 到達正數第 `k` 個節點，再從 head 啟動 `back`。當 `front` 的
領先指標前進至尾端時，`back` 恰好位於倒數第 `k` 個節點。這個版本不必先知道串列長度，
但必須正確維持兩個指標的距離。

| 解法 | 時間複雜度 | 結果空間 | 輔助空間 |
| --- | --- | --- | --- |
| 先計算長度 | `O(n)` | `O(1)` | `O(1)` |
| 固定距離雙指標 | `O(n)` | `O(1)` | `O(1)` |

### 逐步走查

以 `head = [1,2,3,4,5]`、`k = 2` 為例：

```plaintext
正數第 2 個節點值為 2。
倒數第 2 個節點等同正數第 4 個節點，值為 4。
交換兩個節點的值；節點物件與 next 關係不變。
結果為 [1,4,3,2,5]。
```

## Acceptance Harness

`Main` 有八個確定性案例。每個案例為兩種公開解法建立獨立串列，並同時要求輸出值正確、
回傳原 head、節點物件順序不變及 topology 完整。

| # | 案例 | 驗證重點 |
| --- | --- | --- |
| 1 | Official example 1 | 一般奇數長度交換。 |
| 2 | Official example 2 | 重複值與較長串列。 |
| 3 | Single node | 最小輸入與同一節點交換。 |
| 4 | Two nodes | 最小的實際交換。 |
| 5 | Odd-length middle node | 正數與倒數位置為同一中點。 |
| 6 | Even-length adjacent middle nodes | 中央相鄰節點交換。 |
| 7 | Value boundaries / k equals length | 節點值上下界與 `k = n`。 |
| 8 | Maximum length / adjacent middle nodes | `n = 100000` 的線性時間 spot check。 |

## 已驗證命令

以下命令從 repository root 執行：

```bash
jq empty leetcode_1721/.vscode/launch.json leetcode_1721/.vscode/tasks.json
dotnet build leetcode_1721/leetcode_1721/leetcode_1721.csproj --nologo
dotnet run --no-build --project leetcode_1721/leetcode_1721/leetcode_1721.csproj
```

若直接開啟 `leetcode_1721/` 作為 VS Code workspace，使用相對巢狀路徑：

```bash
dotnet build leetcode_1721/leetcode_1721.csproj --nologo
dotnet run --no-build --project leetcode_1721/leetcode_1721.csproj
```

以下是 fresh run 的完整輸出：

```text
Case: Official example 1
Input: head=[1, 2, 3, 4, 5], k=2
Expected: [1, 4, 3, 2, 5]
Length scan actual: [1, 4, 3, 2, 5]
Length scan returned original head: YES
Length scan topology preserved: YES
Two pointers actual: [1, 4, 3, 2, 5]
Two pointers returned original head: YES
Two pointers topology preserved: YES
Result: PASS

Case: Official example 2
Input: head=[7, 9, 6, 6, 7, 8, 3, 0, 9, 5], k=5
Expected: [7, 9, 6, 6, 8, 7, 3, 0, 9, 5]
Length scan actual: [7, 9, 6, 6, 8, 7, 3, 0, 9, 5]
Length scan returned original head: YES
Length scan topology preserved: YES
Two pointers actual: [7, 9, 6, 6, 8, 7, 3, 0, 9, 5]
Two pointers returned original head: YES
Two pointers topology preserved: YES
Result: PASS

Case: Single node
Input: head=[1], k=1
Expected: [1]
Length scan actual: [1]
Length scan returned original head: YES
Length scan topology preserved: YES
Two pointers actual: [1]
Two pointers returned original head: YES
Two pointers topology preserved: YES
Result: PASS

Case: Two nodes
Input: head=[1, 2], k=1
Expected: [2, 1]
Length scan actual: [2, 1]
Length scan returned original head: YES
Length scan topology preserved: YES
Two pointers actual: [2, 1]
Two pointers returned original head: YES
Two pointers topology preserved: YES
Result: PASS

Case: Odd-length middle node
Input: head=[1, 2, 3], k=2
Expected: [1, 2, 3]
Length scan actual: [1, 2, 3]
Length scan returned original head: YES
Length scan topology preserved: YES
Two pointers actual: [1, 2, 3]
Two pointers returned original head: YES
Two pointers topology preserved: YES
Result: PASS

Case: Even-length adjacent middle nodes
Input: head=[1, 2, 3, 4], k=2
Expected: [1, 3, 2, 4]
Length scan actual: [1, 3, 2, 4]
Length scan returned original head: YES
Length scan topology preserved: YES
Two pointers actual: [1, 3, 2, 4]
Two pointers returned original head: YES
Two pointers topology preserved: YES
Result: PASS

Case: Value boundaries / k equals length
Input: head=[0, 25, 50, 75, 100], k=5
Expected: [100, 25, 50, 75, 0]
Length scan actual: [100, 25, 50, 75, 0]
Length scan returned original head: YES
Length scan topology preserved: YES
Two pointers actual: [100, 25, 50, 75, 0]
Two pointers returned original head: YES
Two pointers topology preserved: YES
Result: PASS

Case: Maximum length / adjacent middle nodes
Input: head=[0, 1, 2, 3, 4, ..., 5, 6, 7, 8, 9] (length 100000), k=50000
Expected: [0, 1, 2, 3, 4, ..., 5, 6, 7, 8, 9] (length 100000)
Length scan actual: [0, 1, 2, 3, 4, ..., 5, 6, 7, 8, 9] (length 100000)
Length scan returned original head: YES
Length scan topology preserved: YES
Two pointers actual: [0, 1, 2, 3, 4, ..., 5, 6, 7, 8, 9] (length 100000)
Two pointers returned original head: YES
Two pointers topology preserved: YES
Result: PASS

Summary: 8/8 checks passed.
```

## 最終結構

```plaintext
leetcode_1721/
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
└── leetcode_1721/
    ├── Program.cs
    └── leetcode_1721.csproj
```
