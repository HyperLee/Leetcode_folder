# LeetCode 2331：計算布林二元樹的值

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![Language](https://img.shields.io/badge/Language-C%23-239120)](https://learn.microsoft.com/dotnet/csharp/)

這是一個以 .NET 10 Console App 實作的教學專案，示範如何使用「遞迴深度優先搜尋」與「迭代後序走訪」計算布林完整二元樹。

## 快速導覽

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：遞迴深度優先搜尋](#解法一遞迴深度優先搜尋)
- [解法二：迭代後序走訪](#解法二迭代後序走訪)
- [兩種解法比較](#兩種解法比較)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)

## 題目說明

給定一棵布林完整二元樹，計算根節點所表示的布林結果。

節點值的意義如下：

| 節點種類 | `val` | 意義 |
| --- | ---: | --- |
| 葉節點 | `0` | `false` |
| 葉節點 | `1` | `true` |
| 非葉節點 | `2` | 左右子樹結果執行 OR |
| 非葉節點 | `3` | 左右子樹結果執行 AND |

完整二元樹中的每個節點只會有 0 或 2 個子節點。因此，只要目前節點不是葉節點，就能確定左右子節點都存在。

題目連結：

- [LeetCode](https://leetcode.com/problems/evaluate-boolean-binary-tree/)
- [LeetCode 中文站](https://leetcode.cn/problems/evaluate-boolean-binary-tree/)

## 限制條件

- 樹中的節點數介於 `1` 到 `1000`。
- `0 <= Node.val <= 3`。
- 每個節點有 0 或 2 個子節點。
- 葉節點的值只會是 `0` 或 `1`。
- 非葉節點的值只會是 `2` 或 `3`。
- 根節點一定存在，因此兩個公開解法都接受非空的 `TreeNode`。

## 解題概念與出發點

一個運算節點的結果依賴兩個資訊：

1. 左子樹的布林結果。
2. 右子樹的布林結果。

取得兩側結果後，才能根據目前節點的值套用 OR 或 AND。這種「先處理子節點，再處理父節點」的相依關係，正好對應二元樹的後序走訪。

本專案提供兩種實作：

- `EvaluateTree`：利用程式語言的函式呼叫堆疊，自然地遞迴到葉節點，再由下往上合併結果。
- `EvaluateTreeIterative`：自行維護堆疊與節點狀態，明確模擬後序走訪。

兩種解法都不會修改輸入樹，因此測試時可以安全地共用同一棵樹。

## 解法一：遞迴深度優先搜尋

### 設計說明

`EvaluateTree` 把每一棵子樹視為一個較小的相同問題：

1. 若節點沒有左右子節點，它是葉節點，直接回傳 `root.val == 1`。
2. 否則遞迴計算左右子樹。
3. `root.val == 2` 時回傳左右結果的 OR。
4. `root.val == 3` 時回傳左右結果的 AND。

C# 的 `||` 與 `&&` 具有短路特性：OR 的左側已為 `true`，或 AND 的左側已為 `false` 時，不必再遞迴計算右側。不過在最壞情況下仍可能走訪所有節點。

### 複雜度

- 時間複雜度：`O(n)`，最壞情況每個節點都處理一次。
- 空間複雜度：`O(h)`，`h` 是樹高，來自遞迴呼叫堆疊。

### 範例演示流程

#### 葉節點：`0`

1. 根節點沒有子節點，符合基底條件。
2. 判斷 `0 == 1`，結果為 `false`。

#### 單層運算：`false OR true`

```text
    OR(2)
    /   \
false  true
  (0)   (1)
```

1. 左葉節點 `0` 得到 `false`。
2. 右葉節點 `1` 得到 `true`。
3. 根節點值為 `2`，計算 `false || true`，得到 `true`。

#### 多層運算：`true OR (false AND true)`

```text
          OR(2)
         /     \
     true(1)   AND(3)
               /    \
          false(0) true(1)
```

1. 左子樹得到 `true`。
2. 從運算定義來看，右子樹為 `false AND true = false`。
3. 根節點得到 `true OR false = true`。
4. 實際遞迴版本可利用 OR 短路：左側已為 `true` 時便能直接決定根節點結果。

## 解法二：迭代後序走訪

### 為什麼需要展開狀態

普通堆疊第一次取出父節點時，左右子樹可能尚未計算完成。因此堆疊元素除了節點以外，還保存 `Expanded`：

- `Expanded == false`：第一次遇到節點，先安排它稍後再次出現，再把子節點放入堆疊。
- `Expanded == true`：第二次遇到節點，代表左右子樹結果已存在，可以合併。

節點結果保存於 `Dictionary<TreeNode, bool>`。字典以節點物件作為索引，使父節點能取得左右子節點已計算完成的布林值。

### 堆疊安排順序

遇到尚未展開的運算節點時，依序推入：

1. `(目前節點, true)`
2. `(右子節點, false)`
3. `(左子節點, false)`

堆疊是後進先出，因此實際處理順序為左子樹、右子樹、目前節點，形成後序走訪。

### 複雜度

- 時間複雜度：`O(n)`，每個運算節點最多進出堆疊兩次。
- 空間複雜度：`O(n)`，堆疊與結果字典在最壞情況下都可能保存與節點數同階的資料。

### 範例演示流程

#### 葉節點：`1`

1. 初始堆疊為 `[(1, false)]`。
2. 取出節點後發現沒有子節點，直接記錄 `values[1] = true`。
3. 堆疊清空，回傳根節點保存的 `true`。

#### 單層運算：`true AND false`

1. 第一次取出 AND，推入「已展開 AND」、右葉節點、左葉節點。
2. 左葉節點記錄 `true`。
3. 右葉節點記錄 `false`。
4. 第二次取出 AND，從字典取得兩側結果並計算 `true && false`。
5. 根節點記錄並回傳 `false`。

#### 多層運算：`true OR (false AND true)`

後序處理順序為：

```text
true -> false -> true -> AND -> OR
```

1. 三個葉節點依序把 `true`、`false`、`true` 寫入結果表。
2. AND 節點讀取 `false` 與 `true`，保存 `false`。
3. OR 節點讀取左側 `true` 與右側 `false`，保存 `true`。
4. 回傳根節點的結果 `true`。

迭代版本為了維持一致的後序狀態流程，會明確計算兩側子樹，不依賴語言的遞迴短路行為。

## 兩種解法比較

| 比較項目 | 遞迴 DFS | 迭代後序走訪 |
| --- | --- | --- |
| 公開方法 | `EvaluateTree` | `EvaluateTreeIterative` |
| 核心工具 | 函式呼叫堆疊 | `Stack` 與 `Dictionary` |
| 時間複雜度 | `O(n)` | `O(n)` |
| 額外空間 | `O(h)` | `O(n)` |
| 短路運算 | 可直接使用 `||`、`&&` | 目前版本完整計算左右子樹 |
| 優點 | 程式精簡，貼近樹的遞迴定義 | 流程明確，不受系統遞迴深度限制 |
| 取捨 | 極深樹可能累積呼叫堆疊 | 需要額外管理節點狀態與結果表 |

## 測試案例設計

`Main` 使用七個固定案例，同時驗證兩種公開解法，共 14 項檢查：

1. 單一 `false` 葉節點。
2. 單一 `true` 葉節點。
3. OR 的兩側皆為 `false`。
4. OR 其中一側為 `true`。
5. AND 其中一側為 `false`。
6. AND 兩側皆為 `true`。
7. 多層 OR 與 AND 混合樹。

每項結果都與人工推導的 `Expected` 比較。若任何檢查失敗，程式會設定非零結束碼，方便在終端機或 CI 環境辨識失敗。

## 專案結構

```text
leetcode_2331/
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_2331.sln
└── leetcode_2331/
    ├── leetcode_2331.csproj
    └── Program.cs
```

## 建置與執行

請從本專案根目錄執行：

```bash
dotnet restore leetcode_2331/leetcode_2331.csproj
dotnet build leetcode_2331/leetcode_2331.csproj --nologo
dotnet run --no-build --project leetcode_2331/leetcode_2331.csproj
```

本專案目前沒有獨立的自動化測試專案；`Main` 中的固定案例、建置結果與程序結束碼共同構成驗收測試。

## 實際執行結果

以下內容來自實際執行 `dotnet run --no-build --project leetcode_2331/leetcode_2331.csproj`：

```text
案例：單一 false 葉節點
Expected: False
EvaluateTree: False - PASS
EvaluateTreeIterative: False - PASS

案例：單一 true 葉節點
Expected: True
EvaluateTree: True - PASS
EvaluateTreeIterative: True - PASS

案例：OR：false OR false
Expected: False
EvaluateTree: False - PASS
EvaluateTreeIterative: False - PASS

案例：OR：false OR true
Expected: True
EvaluateTree: True - PASS
EvaluateTreeIterative: True - PASS

案例：AND：true AND false
Expected: False
EvaluateTree: False - PASS
EvaluateTreeIterative: False - PASS

案例：AND：true AND true
Expected: True
EvaluateTree: True - PASS
EvaluateTreeIterative: True - PASS

案例：混合：true OR (false AND true)
Expected: True
EvaluateTree: True - PASS
EvaluateTreeIterative: True - PASS

總結：14/14 項測試通過
```
