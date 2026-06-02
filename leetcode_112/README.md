# LeetCode 112 - Path Sum

這個專案是 LeetCode 112「Path Sum」的 C# console 實作。程式包含一個可直接執行的範例入口點，展示二元樹路徑總和的主要情境與邊界案例。

## 題目說明

給定一棵二元樹的根節點 `root` 和整數 `targetSum`，判斷是否存在一條「從根節點到葉節點」的路徑，使得路徑上所有節點值加總後等於 `targetSum`。

葉節點是沒有左子節點也沒有右子節點的節點。只要路徑停在非葉節點，即使目前加總已經等於 `targetSum`，也不能算作有效答案。

## 限制條件

- 節點數量範圍為 `0` 到 `5000`。
- 節點值範圍為 `-1000` 到 `1000`。
- `targetSum` 範圍為 `-1000` 到 `1000`。
- `root` 可以是 `null`，代表空樹。
- 有效路徑必須從根節點開始，並且結束於葉節點。

## 解題概念與出發點

這題的核心不是尋找任意一段路徑，而是確認是否存在完整的 root-to-leaf path。因此解法需要同時追蹤兩件事：

1. 目前走到哪一個節點。
2. 走到這個節點後，距離目標總和還剩多少。

從根節點往下走時，每經過一個節點，就把該節點值從 `targetSum` 中扣掉。當走到葉節點時，若剩餘目標值剛好等於葉節點值，代表這條從根到葉的路徑總和符合需求。

這個思路自然符合深度優先搜尋 DFS：

- 空節點沒有路徑，回傳 `false`。
- 葉節點是唯一可以判定路徑是否成功的位置。
- 非葉節點將「扣掉目前節點值後的剩餘目標」交給左右子樹。
- 左子樹或右子樹任一邊找到答案，就可以回傳 `true`。

## 解法一：遞迴 DFS 扣減目標值

### 設計說明

目前專案實作的主要方法是：

```csharp
public bool HasPathSum(TreeNode? root, int targetSum)
```

輸入條件：

- `root` 是目前要檢查的子樹根節點，可以是 `null`。
- `targetSum` 是目前這條路徑還需要湊出的剩餘總和。

輸出結果：

- 若存在至少一條從目前節點走到葉節點的路徑，且路徑總和等於目標值，回傳 `true`。
- 若不存在符合條件的 root-to-leaf path，回傳 `false`。

遞迴流程：

1. 如果 `root == null`，代表這個方向沒有節點，也不可能形成路徑，回傳 `false`。
2. 如果目前節點沒有左子節點也沒有右子節點，代表它是葉節點；此時判斷 `targetSum == root.val`。
3. 如果目前節點不是葉節點，先扣掉目前節點值，改成尋找 `targetSum - root.val`。
4. 分別遞迴檢查左子樹與右子樹，只要其中一邊符合條件就回傳 `true`。

這樣設計可以避免常見錯誤：路徑中途的前綴和等於目標值時，不能提前回傳 `true`，因為題目要求必須走到葉節點。

### 正確性說明

對任一節點 `root` 而言，`HasPathSum(root, targetSum)` 的語意是：「從這個節點出發，是否存在一條到葉節點的路徑，使路徑總和等於 `targetSum`。」

- 當 `root` 是空節點時，沒有任何路徑存在，因此結果必定是 `false`。
- 當 `root` 是葉節點時，路徑只剩目前節點本身；只要 `root.val` 等於 `targetSum`，這條路徑就成立。
- 當 `root` 不是葉節點時，任何有效路徑都必須經過目前節點後再往左或往右走，因此子問題會變成：左右子樹是否能湊出 `targetSum - root.val`。

上述三種情況完整涵蓋所有可能節點狀態，因此遞迴判斷可以得到正確答案。

### 複雜度

- 時間複雜度：`O(n)`，最壞情況需要拜訪每一個節點。
- 空間複雜度：`O(h)`，`h` 是樹高，來自遞迴呼叫堆疊；最壞情況退化成鏈狀樹時為 `O(n)`。

## 範例演示流程

### 範例 1：標準範例 target 22

範例樹：

```text
        5
       / \
      4   8
     /   / \
    11  13  4
   /  \      \
  7    2      1
```

目標值是 `22`。其中一條有效路徑是：

```text
5 -> 4 -> 11 -> 2
```

扣減流程：

```text
22 - 5 = 17
17 - 4 = 13
13 - 11 = 2
葉節點 2 == 剩餘目標 2
```

結果為 `true`。

### 範例 2：中途值符合但不是葉節點

使用同一棵範例樹，目標值是 `5`。根節點值本身等於 `5`，但根節點不是葉節點，因此不能直接視為有效路徑。繼續往下搜尋後，沒有任何 root-to-leaf path 的總和等於 `5`。

結果為 `false`。

### 範例 3：空樹

`root` 是 `null`，沒有任何根到葉節點路徑。

結果為 `false`。

### 範例 4：單一節點

樹只有一個節點 `1`，它同時是根節點與葉節點。目標值也是 `1`。

結果為 `true`。

### 範例 5：負數路徑

範例樹：

```text
-2
  \
  -3
```

目標值是 `-5`，路徑 `-2 -> -3` 的總和為 `-5`。

結果為 `true`。

### 範例 6：前綴和符合但未到葉節點

範例樹：

```text
1
 \
  2
   \
    3
```

目標值是 `3`。路徑前段 `1 -> 2` 的加總是 `3`，但節點 `2` 不是葉節點；完整 root-to-leaf path 是 `1 -> 2 -> 3`，總和為 `6`。

結果為 `false`。

## 專案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_112/
    ├── Program.cs
    └── leetcode_112.csproj
```

## 建置與執行

請從此 README 所在的專案目錄執行命令。

建置專案：

```bash
dotnet build leetcode_112/leetcode_112.csproj
```

執行範例：

```bash
dotnet run --no-build --project leetcode_112/leetcode_112.csproj
```

預期輸出：

```text
LeetCode 112 Path Sum
Example 1 - sample target 22: expected=True, actual=True, pass=True
Example 2 - sample target 5: expected=False, actual=False, pass=True
Example 3 - empty tree target 0: expected=False, actual=False, pass=True
Example 4 - single node target 1: expected=True, actual=True, pass=True
Example 5 - negative path target -5: expected=True, actual=True, pass=True
Example 6 - prefix-only target 3: expected=False, actual=False, pass=True
```

目前沒有獨立的測試專案；範例執行輸出即為此 console project 的手動驗證資料。
