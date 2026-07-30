# LeetCode 236：二元樹的最近公共祖先

![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4)
![C#](https://img.shields.io/badge/C%23-Console-239120)

這個專案以 C# 實作 LeetCode 236「Lowest Common Ancestor of a Binary Tree」，使用遞迴深度優先搜尋找出二元樹中兩個指定節點的最近公共祖先，並提供五筆可直接執行的固定案例。

## 題目說明

給定一棵二元樹及樹中的兩個不同節點 `p`、`q`，找出它們的最近公共祖先（Lowest Common Ancestor，LCA）。

最近公共祖先是同時為 `p`、`q` 祖先且深度最大的節點。依照題目定義，一個節點可以是自己的祖先，因此：

- 若 `p`、`q` 分別位於某節點的左右子樹，該分流節點就是答案。
- 若 `p` 本身是 `q` 的祖先，最近公共祖先就是 `p`；反之亦然。

題目連結：

- [LeetCode 236 - Lowest Common Ancestor of a Binary Tree](https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-tree/description/)
- [力扣 236 - 二叉樹的最近公共祖先](https://leetcode.cn/problems/lowest-common-ancestor-of-a-binary-tree/description/)

## 限制條件

- 樹中的節點數量介於 `2` 到 `10^5`。
- `-10^9 <= Node.val <= 10^9`。
- 所有 `Node.val` 皆不相同。
- `p != q`。
- `p` 與 `q` 都存在於給定的二元樹中。

本實作遵循上述輸入契約。公開方法接收非空的 `root`、`p`、`q`，並回傳非空的最近公共祖先；左右空子樹則以 `TreeNode?` 表達。

## 解題概念與出發點

### 從「目前子樹找到了什麼」思考

直接從根節點列出 `p`、`q` 的完整路徑再比較，當然可以得到答案，但需要額外保存路徑。這個解法改用後序形式的遞迴 DFS，讓每個子樹只向父節點回報一個狀態：

- 回傳 `null`：目前子樹沒有找到 `p` 或 `q`。
- 回傳 `p` 或 `q`：目前子樹找到其中一個目標。
- 回傳其他節點：該節點已經是兩條目標路徑的分流點，也就是最近公共祖先。

父節點只需要合併左右子樹的回報，不必另外保存從根到目標的路徑。

### 遞迴終止條件

```text
root == null
```

空子樹不含任何目標，回傳 `null`。

```text
root == p 或 root == q
```

目前節點已是其中一個目標，直接回傳該節點。這也涵蓋「一個目標是另一個目標的祖先」的情況，因為節點可以是自己的祖先。

程式使用 `ReferenceEquals` 比較節點參考，而不是只比較 `val`。題目的 API 給定的是樹中的節點物件；即使題目同時保證值唯一，依物件身分比較仍更準確地表達這個契約。

### 合併左右子樹結果

遞迴完成左右子樹後，依下表決定目前節點要向上回傳什麼：

| 左子樹結果 | 右子樹結果 | 代表意義 | 回傳 |
| --- | --- | --- | --- |
| 非 `null` | 非 `null` | `p`、`q` 的路徑在目前節點分流 | 目前節點 |
| 非 `null` | `null` | 目標或已找到的 LCA 位於左側 | 左側結果 |
| `null` | 非 `null` | 目標或已找到的 LCA 位於右側 | 右側結果 |
| `null` | `null` | 目前子樹沒有任何目標 | `null` |

## 解法一：遞迴深度優先搜尋

主要入口為：

```csharp
public static TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
```

公開方法保留 LeetCode 常見的非 nullable 介面，再交由可處理空子樹的私有 helper 執行遞迴。

### 設計流程

1. 從 `root` 開始搜尋。
2. 若目前節點為 `null`、`p` 或 `q`，直接回傳目前節點。
3. 遞迴搜尋左子樹並保存 `left`。
4. 遞迴搜尋右子樹並保存 `right`。
5. 若 `left` 與 `right` 都不是 `null`，表示兩個目標分別從目前節點的左右方向被找到，回傳目前節點。
6. 否則回傳 `left ?? right`，將唯一找到的目標或較深處已確認的 LCA 向上傳遞。
7. 題目保證 `p`、`q` 都存在，因此公開方法最終會取得唯一的非空答案。

### 正確性說明

對任一子樹，其遞迴結果符合以下不變量：沒有目標時回傳 `null`；只含一個目標時回傳該目標；同時含有兩個目標時回傳它們在該子樹內的最近公共祖先。

- 空子樹與目標節點的終止條件直接符合不變量。
- 若左右子樹都回傳非空，兩個目標分處兩側，當前根節點是第一個同時涵蓋兩者的節點。
- 若只有一側非空，兩個目標不可能在另一側分流，應繼續傳遞非空結果。

遞迴由最深層開始合併，因此第一個同時收到左右非空結果的節點深度最大；它正是最近公共祖先。若其中一個目標本身是祖先，終止條件會回傳該目標，並一路向上傳遞。

### 複雜度分析

令 `n` 為節點總數，`h` 為樹高：

- 時間複雜度：`O(n)`。最壞情況下每個節點造訪一次。
- 額外空間複雜度：`O(h)`。空間來自遞迴呼叫堆疊；平衡樹為 `O(log n)`，完全偏斜時最壞為 `O(n)`。

> [!NOTE]
> 題目允許最多 `10^5` 個節點。遞迴解法簡潔且適合說明核心觀念，但極端偏斜且非常深的樹可能受到執行環境呼叫堆疊大小限制。

## 範例演示流程

### 演示一：目標分處左右子樹

```text
root = [3, 5, 1, 6, 2, 0, 8, null, null, 7, 4]
p = 5
q = 1
```

1. 左子樹根節點 `5` 就是 `p`，左側遞迴回傳節點 `5`。
2. 右子樹根節點 `1` 就是 `q`，右側遞迴回傳節點 `1`。
3. 根節點 `3` 同時收到左右非空結果。
4. `3` 是兩條路徑第一次匯合的位置，因此回傳 `3`。

### 演示二：一個目標是另一個目標的祖先

```text
root = [3, 5, 1, 6, 2, 0, 8, null, null, 7, 4]
p = 5
q = 4
```

1. 搜尋到節點 `5` 時已命中 `p`，直接回傳 `5`。
2. 根節點 `3` 的左側結果為 `5`，右側沒有任何目標。
3. 根節點將唯一的非空結果 `5` 向上傳遞。
4. 因為節點可以是自己的祖先，答案為 `5`。

題目保證 `q = 4` 存在於節點 `5` 的子樹中，因此命中 `p` 時不需要繼續向下搜尋來重新驗證輸入契約。

### 演示三：最近公共祖先位於較深子樹

```text
root = [3, 5, 1, 6, 2, 0, 8, null, null, 7, 4]
p = 6
q = 4
```

1. 在節點 `5` 的左子樹找到 `6`。
2. 在節點 `5` 的右子樹中，沿著節點 `2` 找到 `4`。
3. 節點 `5` 同時收到左右非空結果，因此先確認 LCA 為 `5`。
4. 回到根節點 `3` 時，左側結果為 `5`、右側為 `null`，繼續傳遞 `5`。
5. 最終答案仍是較深的節點 `5`，不會被根節點 `3` 取代。

## 可執行案例

`Main` 會依序執行下列五筆資料：

| 案例 | 驗證情境 | 預期答案 |
| --- | --- | --- |
| 1 | `p`、`q` 分處根節點左右子樹 | `3` |
| 2 | `p` 本身就是 `q` 的祖先 | `5` |
| 3 | 兩節點的最小合法樹 | `1` |
| 4 | 最近公共祖先位於左側較深子樹 | `5` |
| 5 | 兩個目標同在右子樹 | `1` |

測試工具從層序陣列建立新樹，再依唯一節點值取得樹中的實際 `p`、`q` 物件。若根資料無效或找不到目標節點，程式會立即拋出例外，避免把錯誤測資誤報為演算法的 FAIL。

## 專案結構

```text
.
├── docs/
│   └── readme-template.md
├── leetcode_236/
│   ├── Program.cs
│   └── leetcode_236.csproj
├── leetcode_236.sln
└── README.md
```

## 建置與執行

請從此 repository 根目錄執行：

```bash
dotnet restore leetcode_236/leetcode_236.csproj
dotnet build leetcode_236/leetcode_236.csproj --nologo
dotnet run --project leetcode_236/leetcode_236.csproj
```

目前沒有獨立的 xUnit、NUnit 或 MSTest 專案；驗證方式是確認專案可成功建置，並由 `Main` 的固定案例比較預期與實際結果。

## 實際執行輸出

以下內容來自：

```bash
dotnet run --project leetcode_236/leetcode_236.csproj
```

```text
Lowest Common Ancestor of a Binary Tree sample verification

Case 1: 官方案例 1：目標分別位於根節點左右子樹
Input: root = [3, 5, 1, 6, 2, 0, 8, null, null, 7, 4], p = 5, q = 1
Expected: 3
Actual: 3
Result: PASS

Case 2: 官方案例 2：其中一個目標本身就是最近公共祖先
Input: root = [3, 5, 1, 6, 2, 0, 8, null, null, 7, 4], p = 5, q = 4
Expected: 5
Actual: 5
Result: PASS

Case 3: 官方案例 3：最小合法樹且根節點就是目標
Input: root = [1, 2], p = 1, q = 2
Expected: 1
Actual: 1
Result: PASS

Case 4: 左側深層分流：最近公共祖先位於左子樹
Input: root = [3, 5, 1, 6, 2, 0, 8, null, null, 7, 4], p = 6, q = 4
Expected: 5
Actual: 5
Result: PASS

Case 5: 右側子樹分流：兩個目標同在右子樹
Input: root = [3, 5, 1, 6, 2, 0, 8, null, null, 7, 4], p = 0, q = 8
Expected: 1
Actual: 1
Result: PASS

Summary: 5/5 checks passed.
```

## 最終檢查

完成修改後，執行：

```bash
git diff --check
```

命令沒有輸出即表示目前差異未發現多餘空白或換行錯誤。
