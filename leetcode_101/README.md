# LeetCode 101 - Symmetric Tree

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![Language](https://img.shields.io/badge/C%23-Console-239120)

這個專案是 LeetCode 101「Symmetric Tree / 對稱二元樹」的 C# console 解題紀錄。程式在 `Main` 中提供可直接執行的範例資料，並展示兩種判斷策略：遞迴 DFS 鏡像比較與迭代 Queue 鏡像比較。

## 題目說明

給定一棵二元樹的根節點 `root`，判斷這棵樹是否為自身的鏡像，也就是是否以根節點為中心左右對稱。

對稱二元樹的重點不是單純比較左子樹和右子樹是否「長得一樣」，而是要比較它們是否互為鏡像：

- 左子樹的左節點要對應右子樹的右節點。
- 左子樹的右節點要對應右子樹的左節點。
- 對應節點必須同時存在，且節點值必須相同。

參考連結：

- [LeetCode - Symmetric Tree](https://leetcode.com/problems/symmetric-tree/description/)
- [LeetCode Wiki - 101. Symmetric Tree](https://leetcode.doocs.org/en/lc/101/)

## 限制條件

- 樹中節點數量範圍為 `[1, 1000]`。
- 節點值範圍為 `-100 <= Node.val <= 100`。
- 本專案的實作也額外處理 `root == null`，並將空樹視為對稱，方便本機範例與一般二元樹定義使用。

## 解題概念與出發點

這題的核心觀察是「對稱」必須成對比較鏡像位置，而不是比較同方向位置。

以根節點 `root` 來看，真正需要比較的是 `root.left` 和 `root.right` 是否互為鏡像。若目前比較的兩個節點分別是 `left` 與 `right`，則必須同時滿足：

1. `left` 與 `right` 都是 `null`：這一組位置對稱。
2. 只有其中一邊是 `null`：結構不對稱。
3. 兩邊都存在但 `left.val != right.val`：數值不對稱。
4. 兩邊都存在且數值相同：繼續比較下一層鏡像位置。

下一層鏡像位置的比較方式是：

```text
left.left  <-> right.right
left.right <-> right.left
```

本專案提供兩種解法：

1. `IsSymmetric`：遞迴 DFS，直接用函式呼叫堆疊保存待比較的鏡像節點。
2. `IsSymmetricIterative`：迭代 Queue，用佇列保存待比較的鏡像節點 pair。

兩種解法的判斷邏輯相同，差異只在於「下一組待比較節點」由遞迴呼叫保存，或由明確的資料結構保存。

## 解法一：遞迴 DFS 鏡像比較

`IsSymmetric` 先處理根節點：

- `root == null` 時直接回傳 `true`。
- 否則呼叫 `IsSymmetricTree(root.left, root.right)`，開始比較左右子樹是否互為鏡像。

`IsSymmetricTree` 負責比較一組鏡像節點：

```text
IsSymmetricTree(left, right)
```

比較流程：

1. 若 `left == null && right == null`，代表這組鏡像位置都沒有節點，回傳 `true`。
2. 若 `left == null || right == null`，代表只有一邊有節點，回傳 `false`。
3. 若 `left.val != right.val`，代表鏡像位置的值不同，回傳 `false`。
4. 若目前節點通過檢查，遞迴比較外側與內側：

```text
IsSymmetricTree(left.left, right.right)
IsSymmetricTree(left.right, right.left)
```

只有外側與內側都對稱，整組子樹才對稱。

時間複雜度為 `O(n)`，其中 `n` 是節點數量，因為每個節點最多被比較一次。空間複雜度為 `O(h)`，其中 `h` 是樹高，來自遞迴呼叫堆疊；最壞情況退化成鍊狀樹時為 `O(n)`。

### 範例演示

輸入：

```text
root = [1,2,2,3,4,4,3]
```

樹狀結構：

```text
        1
      /   \
     2     2
    / \   / \
   3   4 4   3
```

遞迴流程：

```text
比較 root.left = 2 與 root.right = 2，數值相同
比較外側：left.left = 3 與 right.right = 3，數值相同
比較 3 的下一層：null 與 null，對稱
比較 3 的另一側：null 與 null，對稱
比較內側：left.right = 4 與 right.left = 4，數值相同
比較 4 的下一層：null 與 null，對稱
比較 4 的另一側：null 與 null，對稱
所有鏡像 pair 都通過，回傳 true
```

結果：

```text
true
```

反例：

```text
root = [1,2,2,null,3,null,3]
```

樹狀結構：

```text
        1
      /   \
     2     2
      \     \
       3     3
```

遞迴流程：

```text
比較 root.left = 2 與 root.right = 2，數值相同
比較外側：left.left = null 與 right.right = 3
只有一邊存在，結構不對稱，回傳 false
```

## 解法二：迭代 Queue 鏡像比較

`IsSymmetricIterative` 使用 `Queue<(TreeNode? Left, TreeNode? Right)>` 保存「應該互為鏡像」的節點 pair。

初始狀態：

```text
queue = [(root.left, root.right)]
```

每次從佇列取出一組 `(left, right)` 後執行同樣的鏡像檢查：

1. 兩邊都是 `null`：這組通過，繼續下一組。
2. 只有一邊是 `null`：回傳 `false`。
3. 兩邊值不同：回傳 `false`。
4. 兩邊值相同：把下一層鏡像 pair 加入佇列。

加入佇列的順序是：

```text
(left.left, right.right)
(left.right, right.left)
```

這個解法避免使用遞迴呼叫堆疊，適合用來回應題目的 follow-up：「能否同時用遞迴與迭代方式解決？」

時間複雜度同樣是 `O(n)`。空間複雜度為 `O(w)`，其中 `w` 是同一層最多待比較 pair 的數量；最壞情況可達 `O(n)`。

### 範例演示

輸入：

```text
root = [1,2,2,3,4,4,3]
```

佇列流程：

```text
queue = [(2, 2)]

取出 (2, 2)，數值相同
加入外側 pair (3, 3)
加入內側 pair (4, 4)
queue = [(3, 3), (4, 4)]

取出 (3, 3)，數值相同
加入 (null, null)
加入 (null, null)
queue = [(4, 4), (null, null), (null, null)]

取出 (4, 4)，數值相同
加入 (null, null)
加入 (null, null)
queue = [(null, null), (null, null), (null, null), (null, null)]

後續取出的 pair 都是 (null, null)，全部通過
queue 清空，回傳 true
```

反例：

```text
root = [1,2,2,null,3,null,3]
```

佇列流程：

```text
queue = [(2, 2)]

取出 (2, 2)，數值相同
加入外側 pair (null, 3)
加入內側 pair (3, null)
queue = [(null, 3), (3, null)]

取出 (null, 3)
只有一邊存在，結構不對稱，回傳 false
```

## 執行方式

建置專案：

```powershell
dotnet build .\leetcode_101\leetcode_101.csproj
```

執行範例：

```powershell
dotnet run --project .\leetcode_101\leetcode_101.csproj
```

目前範例輸出：

```text
Recursive DFS - Empty tree: PASS -> True
Recursive DFS - Single node: PASS -> True
Recursive DFS - Example 1: PASS -> True
Recursive DFS - Example 2: PASS -> False
Recursive DFS - Value mismatch: PASS -> False
Iterative Queue - Empty tree: PASS -> True
Iterative Queue - Single node: PASS -> True
Iterative Queue - Example 1: PASS -> True
Iterative Queue - Example 2: PASS -> False
Iterative Queue - Value mismatch: PASS -> False
All examples passed.
```

檢查 Git diff 是否有多餘空白：

```powershell
git diff --check
```

> [!NOTE]
> 目前專案沒有獨立測試專案，因此驗證以 `dotnet build` 與 `dotnet run` 中的 console 範例為主。

## 專案結構

```text
.
├── docs/
│   └── readme-template.md
├── leetcode_101/
│   ├── Program.cs
│   └── leetcode_101.csproj
└── README.md
```
