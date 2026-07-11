# LeetCode 445：Add Two Numbers II（兩數相加 II）

- [LeetCode 英文題目](https://leetcode.com/problems/add-two-numbers-ii/)
- [LeetCode 中文題目](https://leetcode.cn/problems/add-two-numbers-ii/)

## 題目說明

兩個非空單向鏈結串列分別代表兩個非負整數。每個節點只存放一位十進位數字，且串列頭端是最高位；請回傳兩數相加後的鏈結串列，結果也必須保持最高位在前。

本題與 LeetCode 2 的差異在於數字順序沒有反轉，因此不能直接從串列頭端逐位相加。進階要求是不反轉輸入串列，本專案也保證不改動任何輸入節點。

## 限制條件

- 每個鏈結串列的節點數介於 `1` 到 `100`。
- `0 <= Node.val <= 9`。
- 除了數字 `0` 本身外，輸入不含前導零。
- `l1` 與 `l2` 都是題目保證的非空串列。

## Stack 解法與核心不變量

先由串列頭端走訪，將兩個數字的每一位壓入各自的 `Stack<int>`。Stack 頂端便是尚未處理的最低位，之後可以像一般直式加法一樣彈出、加上進位，卻不必改變原串列的 `next` 指標。

每次算出的位數都是結果中目前最右側的位數，所以新節點必須前插至 `result` 頭端。此不變量保證經過一次由低到高的處理後，輸出仍是題目要求的「最高位在前」。

`AddTwoNumbers` 只讀取輸入並建立新節點；所有 `Console` 輸出都集中在 `Main` 的 acceptance harness。

## `7243 + 564` 逐步走查

| 處理順序（最低位起） | 加總與進位 | 前插後的結果 |
| --- | --- | --- |
| `3 + 4` | `7`，進位 `0` | `[7]` |
| `4 + 6` | `10`，寫入 `0`、進位 `1` | `[0,7]` |
| `2 + 5 + 1` | `8`，進位 `0` | `[8,0,7]` |
| `7 + 0` | `7`，進位 `0` | `[7,8,0,7]` |

兩個原始輸入 `[7,2,4,3]` 與 `[5,6,4]` 在整個過程中都沒有被重新連接或反轉。

## 複雜度

令兩個串列長度分別為 `m` 與 `n`。

| 項目 | 複雜度 | 原因 |
| --- | --- | --- |
| 時間 | `O(m + n)` | 每個輸入節點只走訪、壓入與彈出一次。 |
| 輔助空間 | `O(m + n)` | 兩個 Stack 保存所有輸入位數。 |
| 結果空間 | `O(max(m, n) + 1)` | 新結果至多比最長輸入多一個進位節點。 |

## Acceptance harness 案例

| # | 案例 | 驗證重點 |
| --- | --- | --- |
| 1 | `[7,2,4,3] + [5,6,4]` | 官方不等長範例。 |
| 2 | `[2,4,3] + [5,6,4]` | 官方等長範例。 |
| 3 | `[0] + [0]` | 官方最小有效輸入。 |
| 4 | `[5,6,4] + [7,2,4,3]` | 較短串列位於 `l1` 的回歸案例。 |
| 5 | `[9,9,9,9,9,9,9] + [9,9,9,9]` | 連鎖進位與新增最高位。 |
| 6 | 100 個 `9` 加上 `[1]` | 題目上限的完整位數驗證與摘要輸出。 |

每一項都同時比對完整結果與兩個輸入串列，確認解法沒有為了計算而修改它們。

## 建置與執行

以下命令已在 repository 根目錄驗證：

```bash
dotnet build leetcode_445/leetcode_445/leetcode_445.csproj --nologo
dotnet run --no-build --project leetcode_445/leetcode_445/leetcode_445.csproj
```

## 實際執行輸出

```text
LeetCode 445 acceptance harness

Case 1: Official example: l1 is longer
Input: l1 = [7,2,4,3], l2 = [5,6,4]
Expected: result = [7,8,0,7]; inputs unchanged
Actual: result = [7,8,0,7]; l1 after = [7,2,4,3]; l2 after = [5,6,4]
PASS

Case 2: Official example: equal lengths
Input: l1 = [2,4,3], l2 = [5,6,4]
Expected: result = [8,0,7]; inputs unchanged
Actual: result = [8,0,7]; l1 after = [2,4,3]; l2 after = [5,6,4]
PASS

Case 3: Official example: zero
Input: l1 = [0], l2 = [0]
Expected: result = [0]; inputs unchanged
Actual: result = [0]; l1 after = [0]; l2 after = [0]
PASS

Case 4: Regression: l1 is shorter
Input: l1 = [5,6,4], l2 = [7,2,4,3]
Expected: result = [7,8,0,7]; inputs unchanged
Actual: result = [7,8,0,7]; l1 after = [5,6,4]; l2 after = [7,2,4,3]
PASS

Case 5: Cascading carry
Input: l1 = [9,9,9,9,9,9,9], l2 = [9,9,9,9]
Expected: result = [1,0,0,0,9,9,9,8]; inputs unchanged
Actual: result = [1,0,0,0,9,9,9,8]; l1 after = [9,9,9,9,9,9,9]; l2 after = [9,9,9,9]
PASS

Case 6: Upper-bound spot check: 100 digits
Input: l1 = [9,9,9,...,9,9,9] (100 nodes), l2 = [1]
Expected: result = [1,0,0,...,0,0,0] (101 nodes); inputs unchanged
Actual: result = [1,0,0,...,0,0,0] (101 nodes); l1 after = [9,9,9,...,9,9,9] (100 nodes); l2 after = [1]
PASS

Summary: 6/6 checks passed.
```

## 專案結構

```plaintext
leetcode_445/
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
└── leetcode_445/
    ├── Program.cs
    └── leetcode_445.csproj
```
