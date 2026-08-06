# LeetCode 2471：逐層排序二叉樹所需的最少操作數目

本專案使用 .NET 10 主控台程式示範 LeetCode 2471，逐層讀取二元樹的節點值，並計算將每一層整理為嚴格遞增所需的最少交換次數。

題目連結：

- [LeetCode 2471 - Minimum Number of Operations to Sort a Binary Tree by Level](https://leetcode.com/problems/minimum-number-of-operations-to-sort-a-binary-tree-by-level/)
- [力扣 2471 - 逐层排序二叉树所需的最少操作数目](https://leetcode.cn/problems/minimum-number-of-operations-to-sort-a-binary-tree-by-level/)

## 題目說明

給定一棵二元樹的根節點 `root`。節點的層級是該節點到根節點路徑上的邊數；同一層的節點由左至右排列。

一次操作可以選擇同一層的任意兩個節點，交換它們保存的值。樹的結構與節點位置不會改變，只有節點值可以交換。

請回傳最少操作次數，使每一層由左至右的節點值都成為嚴格遞增排列。

例如，第二個官方範例的樹可以用 level-order 表示為：

```text
[1, 3, 2, 7, 6, 5, 4]
```

各層資料為：

```text
第 0 層：[1]
第 1 層：[3, 2]
第 2 層：[7, 6, 5, 4]
```

第二層需要 1 次交換，第三層需要 2 次交換，因此答案是 `3`。

## 限制條件

依題目條件：

- 節點數量介於 `1` 到 `10^5`。
- `1 <= Node.val <= 10^5`。
- 所有節點值都不重複。
- 根節點一定存在，因此兩種解法都以非空 `TreeNode root` 為輸入。

節點值唯一很重要，因為排序後可以用「值」唯一對應到它在該層的目標索引；若允許重複值，就需要額外追蹤相同值的個別位置。

## 解題概念與出發點

### 1. 逐層處理

每一次操作只能交換同一層的兩個值，因此不同層之間互不影響。可以先用 BFS 由上到下取得每一層的值，再獨立計算該層排序所需的交換次數，最後將所有層的結果相加。

專案中的 `GetLevelValues` 以 queue 保存待處理節點：

1. 先記錄目前 queue 的大小，這個大小就是目前層的節點數。
2. 取出這些節點並保存它們的值。
3. 將非空的左右子節點加入 queue，留待下一輪處理。
4. 產生目前層的整數陣列。

兩種解法共用這個逐層 BFS 流程，差異只在「如何計算一層的最少交換數」。

## 解法一：排序後逐點修正

API：`MinimumOperations(TreeNode root)`

### 設計流程

對每一層的 `values`：

1. 複製一份陣列並排序，得到 `sortedValues`。
2. 建立 `Dictionary<int, int>`，把每個值映射到它在 `sortedValues` 中應該出現的索引。
3. 從左到右檢查目前位置。如果 `values[i]` 還不是 `sortedValues[i]`，就查出目前值的目標索引。
4. 交換 `values[i]` 與目標索引的值，並將交換次數加一。
5. 重複檢查同一個 `i`，直到正確值被放到位置 `i`，再處理下一個位置。

這個方法修改的是 BFS 產生的暫存陣列，不會修改輸入的 `TreeNode` 或樹的結構。

### 為什麼能得到最少交換次數

當目前位置 `i` 的值錯誤時，該值在排序結果中有唯一的目標索引。把它直接交換到目標索引，可以讓至少一個值固定在正確的位置；對每個錯位群組持續進行這項修正，就等同於逐步拆解置換循環，每個長度為 `k` 的循環需要 `k - 1` 次交換。

### 範例

對第三層 `[7, 6, 5, 4]`，排序結果為 `[4, 5, 6, 7]`：

```text
目標索引：7 -> 3，6 -> 2，5 -> 1，4 -> 0

[7, 6, 5, 4]
交換索引 0 與 3
[4, 6, 5, 7]

交換索引 1 與 2
[4, 5, 6, 7]
```

這一層需要 2 次交換。

## 解法二：置換循環分解

API：`MinimumOperationsByCycles(TreeNode root)`

### 設計流程

對每一層的 `values`：

1. 複製並排序，得到 `sortedValues`。
2. 建立值到目標索引的 `Dictionary<int, int>`。
3. 使用 `visited` 記錄哪些索引已經被某個置換循環處理。
4. 若目前索引已在正確位置，或已被拜訪，就跳過。
5. 否則沿著「目前值的目標索引」前進，直到回到已拜訪的位置，計算循環長度 `k`。
6. 將 `k - 1` 加入答案。

### 為什麼是 `k - 1`

一個長度為 `k` 的置換循環至少有 `k - 1` 個位置不可能同時透過更少的任意交換完成修正；另一方面，可以固定其中一個位置，再用 `k - 2` 次交換逐一整理其他位置，因此剛好需要 `k - 1` 次交換。

以第三層 `[7, 6, 5, 4]` 為例，目標索引形成兩個循環：

```text
(0 -> 3 -> 0)  長度 2，需要 1 次交換
(1 -> 2 -> 1)  長度 2，需要 1 次交換
```

所以這一層需要 `1 + 1 = 2` 次交換。

### 與解法一的差異

解法一實際模擬「把錯誤值交換到目標位置」的過程；解法二先分析整個置換結構，再直接用循環長度計數。兩者的時間複雜度相同，但解法二更直接呈現最少交換數與置換循環之間的關係。

## 範例演示流程

使用官方第二個範例：

```text
輸入：[1, 3, 2, 7, 6, 5, 4]
```

### 解法一的逐點交換流程

| 層級 | 目前值 | 排序後 | 交換過程 | 次數 |
|---|---|---|---|---:|
| 0 | `[1]` | `[1]` | 不需交換 | 0 |
| 1 | `[3,2]` | `[2,3]` | 交換索引 `0`、`1` | 1 |
| 2 | `[7,6,5,4]` | `[4,5,6,7]` | `[7,6,5,4] -> [4,6,5,7] -> [4,5,6,7]` | 2 |

總交換次數為 `0 + 1 + 2 = 3`。

### 解法二的循環分解流程

| 層級 | 目標索引循環 | 計算 | 次數 |
|---|---|---|---:|
| 0 | 沒有錯位 | 不需處理 | 0 |
| 1 | `(0 -> 1 -> 0)` | `2 - 1` | 1 |
| 2 | `(0 -> 3 -> 0)`、`(1 -> 2 -> 1)` | `(2 - 1) + (2 - 1)` | 2 |

總交換次數同樣為 `3`。

## 複雜度分析

令 `n` 為樹的節點總數，`w` 為樹的最大寬度。單一層大小為 `k` 時，排序需要 `O(k log k)`；所有層加總後，最壞時間複雜度為 `O(n log n)`。

| 解法 | 時間複雜度 | 額外空間複雜度 | 主要資料結構 |
|---|---|---|---|
| 排序後逐點修正 | `O(n log n)` | `O(w)` | queue、暫存陣列、目標索引 dictionary |
| 置換循環分解 | `O(n log n)` | `O(w)` | queue、排序陣列、目標索引 dictionary、`visited` |

兩種方法都只保留目前 BFS 層及 queue 所需的資料，不需要複製整棵樹。

## 可執行測試案例

`Main` 會固定執行以下案例，並同時比較兩種解法：

| 案例 | Level-order 資料 | 預期結果 |
|---|---|---:|
| LeetCode Example 1 | `[1,4,3,7,6,8,5,null,null,null,null,9,null,10]` | 3 |
| LeetCode Example 2 | `[1,3,2,7,6,5,4]` | 3 |
| LeetCode Example 3 | `[1,2,3,4,5,6]` | 0 |
| Single node | `[1]` | 0 |
| One swap within a level | `[1,3,2]` | 1 |
| Right-skewed sparse tree | `[1,null,2,null,3]` | 0 |

## 範例執行結果

以下內容來自完成建置後的實際執行：

```text
=== Test Cases ===
Case: LeetCode Example 1
  Expected: 3
  Actual (MinimumOperations): 3
  Actual (MinimumOperationsByCycles): 3
  Result: PASS

Case: LeetCode Example 2
  Expected: 3
  Actual (MinimumOperations): 3
  Actual (MinimumOperationsByCycles): 3
  Result: PASS

Case: LeetCode Example 3
  Expected: 0
  Actual (MinimumOperations): 0
  Actual (MinimumOperationsByCycles): 0
  Result: PASS

Case: Single node
  Expected: 0
  Actual (MinimumOperations): 0
  Actual (MinimumOperationsByCycles): 0
  Result: PASS

Case: One swap within a level
  Expected: 1
  Actual (MinimumOperations): 1
  Actual (MinimumOperationsByCycles): 1
  Result: PASS

Case: Right-skewed sparse tree
  Expected: 0
  Actual (MinimumOperations): 0
  Actual (MinimumOperationsByCycles): 0
  Result: PASS

Summary: 6/6 cases passed.
```

若任一案例失敗，`Main` 會回傳非零結束狀態，方便在非互動式環境中辨識失敗。

## 執行方式

請在本專案根目錄 `/Users/qiuzili/Leetcode/Leetcode_folder/leetcode_2471` 執行：

```bash
dotnet restore leetcode_2471/leetcode_2471.csproj
dotnet build leetcode_2471/leetcode_2471.csproj --nologo
dotnet run --project leetcode_2471/leetcode_2471.csproj
```

格式與差異檢查：

```bash
dotnet format leetcode_2471/leetcode_2471.csproj --verify-no-changes --no-restore
git diff --check
```

本專案目前沒有獨立的 automated test project；固定測試案例由主控台入口執行，建置與執行結果作為驗收依據。

## 專案結構

```text
leetcode_2471/
├── leetcode_2471.sln
├── leetcode_2471/
│   ├── Program.cs
│   └── leetcode_2471.csproj
├── docs/
│   └── readme-template.md
└── README.md
```
