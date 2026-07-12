# LeetCode 501：二元搜尋樹中的眾數（Find Mode in Binary Search Tree）

這是一個 C# .NET 10 主控台專案。`FindMode(TreeNode? root)` 以迭代中序走訪找出
可含重複值的二元搜尋樹之所有眾數；`Main` 則集中輸出八項確定性的 acceptance
harness 結果。

- [English problem: 501. Find Mode in Binary Search Tree](https://leetcode.com/problems/find-mode-in-binary-search-tree/)
- [中文題目：501. 二叉搜索树中的众数](https://leetcode.cn/problems/find-mode-in-binary-search-tree/)

## 題目說明 / Problem statement

English: Given the root of a binary search tree that may contain duplicates, return every
mode—the value or values occurring most frequently. If several values tie for the highest
frequency, all of them are valid results.

中文：給定一棵可能含重複值的二元搜尋樹根節點，回傳出現次數最高的所有值；若有多個
值並列最高頻率，就一併回傳它們。BST 的題目契約是左子樹值不大於目前節點、右子樹值
不小於目前節點，且兩側子樹也都符合相同規則。

## 官方限制條件

- 樹的節點數量介於 `1` 與 `10^4`（含）之間。
- `-10^5 <= Node.val <= 10^5`。

本實作遵守題目定義的有效 BST 輸入；沒有另外為 `null` 根節點或違反 BST 排序的資料
設計 LeetCode 外的錯誤處理規則。

## 連續區段不變量

對 BST 做中序走訪時，讀到的數值是非遞減序列，所以相同值必定連續。例如走訪到目前
節點之後，程式維持下列不變量：`modes` 恰好保存目前已走訪節點中，出現次數等於
`maxFrequency` 的所有值。

每讀到一個值時：

1. 它與前一值相同，就增加目前連續次數；否則從 `1` 重新開始。
2. 若連續次數超過最大頻率，清空舊的 `modes` 並放入目前值。
3. 若兩者相等，保留平手，將目前值附加到 `modes`。

這避免了以字典保存每個值的全域計數；演算法直接利用 BST 的排序結構，而非對整棵樹
額外建立一份頻率表。

## 為何使用迭代 Stack

遞迴中序走訪寫法較短，但本題節點數上限為 `10^4`，右傾或左傾樹可能讓遞迴呼叫堆疊
過深。此專案改用顯式 `Stack<TreeNode>`：可維持相同的中序順序與連續區段不變量，並
把深度管理放在可控的堆積結構中。所有計數狀態都是 `FindMode` 的區域變數，因此同一
程序連續呼叫也不會殘留前一棵樹的結果。

## `[1,null,2,2]` 走查

層序輸入 `[1,null,2,2]` 表示根節點 `1` 的右子節點是 `2`，而該 `2` 的左子節點也是
`2`。中序序列為 `1, 2, 2`：

| 讀到的值 | 目前連續次數 | `maxFrequency` | `modes` | 動作 |
| ---: | ---: | ---: | --- | --- |
| `1` | 1 | 1 | `[1]` | 首次成為最高頻率 |
| `2` | 1 | 1 | `[1,2]` | 與最高頻率平手，保留兩者 |
| `2` | 2 | 2 | `[2]` | 新最高頻率，替換舊結果 |

因此結果是 `[2]`。

## 複雜度

- 時間複雜度：`O(n)`，每個節點恰好進出 stack 一次。
- 結果空間：`O(k)`，其中 `k` 是回傳的眾數個數。
- 輔助空間：`O(h)`，其中 `h` 是樹高，來自迭代走訪的 stack；不計回傳的眾數陣列。

## 可執行驗證案例

沒有另建正式測試專案。`Main` 執行下列八項固定案例，逐案輸出輸入、預期值、實際值與
PASS/FAIL；任何失敗都會設定非零 exit code。

| 案例 | 輸入摘要 | 預期結果 | 驗證目的 |
| --- | --- | --- | --- |
| Official example 1 | `[1,null,2,2]` | `[2]` | 官方重複值範例與基本連續區段 |
| Official example 2 / minimal tree | `[0]` | `[0]` | 最小有效樹 |
| Distinct constraint bounds | 層序 `[0,-100000,100000]`；中序 `[-100000,0,100000]` | `[-100000,0,100000]` | 官方值域兩端且全部平手 |
| Tied modes | `[2,1,2,1]` | `[1,2]` | 多個眾數必須一併保留 |
| Right subtree supplies mode | `[2,1,2]` | `[2]` | 右子樹提供最多出現次數 |
| Repeated negative value | `[-1,-1,0]` | `[-1]` | 負值重複區段 |
| Repeated invocations | `first [1,1,2], then [2,1,2]` | `[1], then [2]` | 兩次 API 呼叫不共享狀態 |
| Maximum-height spot check | `10000-node right chain of 7` | `[7]` | 高度上限下的迭代走訪 |

## 建置與執行

直接開啟本題外層 `leetcode_501` 目錄作為 workspace，並從該目錄執行：

```bash
dotnet build leetcode_501/leetcode_501.csproj --nologo
dotnet run --no-build --project leetcode_501/leetcode_501.csproj
```

第二個命令的 `--no-build` 假設前一個建置已成功。以下內容是依本次已建置產物新鮮執行
後取得的完整輸出：

```text
Case: Official example 1
Input: [1,null,2,2]
Expected: [2]
Actual: [2]
PASS
Case: Official example 2 / minimal tree
Input: [0]
Expected: [0]
Actual: [0]
PASS
Case: Distinct constraint bounds
Input: level-order [0,-100000,100000]; in-order [-100000,0,100000]
Expected: [-100000,0,100000]
Actual: [-100000,0,100000]
PASS
Case: Tied modes
Input: [2,1,2,1]
Expected: [1,2]
Actual: [1,2]
PASS
Case: Right subtree supplies mode
Input: [2,1,2]
Expected: [2]
Actual: [2]
PASS
Case: Repeated negative value
Input: [-1,-1,0]
Expected: [-1]
Actual: [-1]
PASS
Case: Repeated invocations
Input: first [1,1,2], then [2,1,2]
Expected: [1], then [2]
Actual: [1], then [2]
PASS
Case: Maximum-height spot check
Input: 10000-node right chain of 7
Expected: [7]
Actual: [7]
PASS
Summary: 8/8 checks passed.
```

## 專案結構

```plaintext
.
├── .editorconfig                  # C# 與結構化檔案格式規範
├── .gitattributes                 # 文字與二進位檔案屬性
├── .gitignore                     # .NET／IDE 產生檔案排除規則
├── .vscode/
│   ├── launch.json                # 直接偵錯 net10.0 輸出
│   └── tasks.json                 # 預設建置工作
├── docs/
│   ├── readme-template.md         # 初次建立 README 的範本
│   └── superpowers/               # 本次遷移的設計與執行計畫
├── leetcode_501/
│   ├── Program.cs                 # 解法與八項 acceptance harness
│   └── leetcode_501.csproj        # .NET 10 SDK 專案設定
├── AGENTS.md                      # 本題協作指南
└── README.md                      # 題目、解法與驗證紀錄
```
