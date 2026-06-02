# LeetCode 111 - Minimum Depth of Binary Tree

這個資料夾是 LeetCode 111「Minimum Depth of Binary Tree」的 C# console 專案。程式在 `Main` 內提供可直接執行的範例資料，並實作兩種深度優先搜尋解法：

- `MinDepth`：一般遞迴 DFS。
- `MinDepth2`：DFS 加上目前最佳深度剪枝。

題目來源：

- [LeetCode 111 - Minimum Depth of Binary Tree](https://leetcode.com/problems/minimum-depth-of-binary-tree/description/)
- [LeetCode Wiki 111](https://leetcode.doocs.org/en/lc/111/)

## 題目說明

給定一棵二元樹，找出它的最小深度。

最小深度是從根節點往下到最近葉節點，最短路徑上經過的節點數。葉節點是沒有任何子節點的節點。

範例：

```text
Input: root = [3,9,20,null,null,15,7]
Output: 2
```

```text
Input: root = [2,null,3,null,4,null,5,null,6]
Output: 5
```

## 限制條件

- 節點數量範圍：`[0, 10^5]`
- 節點值範圍：`[-1000, 1000]`

## 解題概念與出發點

這題最容易出錯的地方是：`null` 子節點不能被當成葉節點。

例如 `root = [2, null, 3]` 時，節點 `2` 的左子節點雖然是 `null`，但它不是一條有效的根到葉節點路徑。真正的葉節點是 `3`，所以最小深度是 `2`，不是 `1`。

因此解題時要遵守兩個核心判斷：

- 空樹深度是 `0`。
- 只有「左右子節點都不存在」的實際節點才是葉節點。

## 解法一：遞迴 DFS

`MinDepth(TreeNode? root)` 使用深度優先搜尋，自根節點往下遞迴尋找最近葉節點。

設計流程：

1. 如果 `root` 是 `null`，代表空樹，回傳 `0`。
2. 如果 `root.left == null && root.right == null`，代表目前節點是葉節點，回傳 `1`。
3. 初始化 `minDepth = int.MaxValue`。
4. 只有左子樹存在時，才遞迴計算左子樹深度並更新最小值。
5. 只有右子樹存在時，才遞迴計算右子樹深度並更新最小值。
6. 回傳 `minDepth + 1`，把目前節點本身計入深度。

時間複雜度是 `O(n)`，其中 `n` 是節點數。最壞情況需要走訪所有節點。空間複雜度是 `O(h)`，其中 `h` 是樹高，來自遞迴呼叫堆疊。

### 解法一範例演示

以 `root = [3, 9, 20, null, null, 15, 7]` 為例：

1. 從根節點 `3` 開始，左右子樹都存在。
2. 往左到 `9`，它沒有左右子節點，是葉節點，深度為 `1`。
3. 回到 `3`，左子樹候選深度是 `1`。
4. 往右到 `20`，它不是葉節點，繼續檢查 `15` 與 `7`。
5. `15` 與 `7` 都是葉節點，對 `20` 來說最小子樹深度是 `1`，所以 `20` 的深度是 `2`。
6. 回到 `3`，左右候選深度分別是 `1` 與 `2`，取較小的 `1`，再加上根節點本身，答案是 `2`。

以 `root = [2, null, 3, null, 4, null, 5, null, 6]` 為例：

1. 節點 `2` 的左子節點是 `null`，不能直接把它當作最短路徑。
2. 只能往右走到 `3`。
3. `3`、`4`、`5` 都只有右子節點，必須沿著唯一存在的子樹繼續遞迴。
4. 到達 `6` 時才遇到葉節點。
5. 路徑為 `2 -> 3 -> 4 -> 5 -> 6`，共 `5` 個節點，答案是 `5`。

## 解法二：DFS 加最佳深度剪枝

`MinDepth2(TreeNode? root)` 仍然使用 DFS，但額外維護 `_bestDepth`，記錄目前已找到的最短葉節點深度。

設計流程：

1. 如果 `root` 是 `null`，回傳 `0`。
2. 每次呼叫 `MinDepth2` 時先把 `_bestDepth` 重設為 `int.MaxValue`，避免同一個物件重複呼叫時沿用舊答案。
3. `Dfs(node, depth)` 進入節點時先計算 `currentDepth = depth + 1`。
4. 如果 `currentDepth >= _bestDepth`，代表這條路徑不可能產生更短答案，直接停止搜尋。
5. 如果目前節點是葉節點，使用 `currentDepth` 更新 `_bestDepth`。
6. 如果不是葉節點，繼續搜尋左右子樹。

時間複雜度最壞仍是 `O(n)`；如果很早找到淺層葉節點，剪枝可以減少部分不必要的遞迴。空間複雜度仍是 `O(h)`。

### 解法二範例演示

以 `root = [3, 9, 20, null, null, 15, 7]` 為例：

1. `_bestDepth` 初始為 `int.MaxValue`。
2. DFS 走到左葉節點 `9`，目前深度是 `2`，更新 `_bestDepth = 2`。
3. 回到右子樹 `20` 時，目前深度也是 `2`。
4. 因為 `currentDepth >= _bestDepth`，繼續往下只會更深，不可能比 `2` 更短，所以右子樹可以停止搜尋。
5. 最終答案是 `_bestDepth = 2`。

以 `root = [2, null, 3, null, 4, null, 5, null, 6]` 為例：

1. `_bestDepth` 初始為 `int.MaxValue`。
2. 每一層都還沒遇到葉節點，所以不能剪枝。
3. DFS 只能沿著右子樹走到 `6`。
4. 到達葉節點 `6` 時，深度是 `5`，更新 `_bestDepth = 5`。
5. 最終答案是 `5`。

## 專案結構

```text
leetcode_111/
├─ leetcode_111/
│  ├─ Program.cs
│  └─ leetcode_111.csproj
├─ leetcode_111.Tests/
│  ├─ Program.cs
│  └─ leetcode_111.Tests.csproj
├─ docs/
│  └─ readme-template.md
└─ README.md
```

## 建置、測試與執行

此專案目標框架是 `.NET 10`，需要安裝可建置 `net10.0` 的 .NET SDK。

建置主專案：

```powershell
dotnet build leetcode_111/leetcode_111.csproj
```

執行測試 runner：

```powershell
dotnet run --project leetcode_111.Tests/leetcode_111.Tests.csproj
```

預期輸出：

```text
PASS MinDepth_ReturnsZero_WhenRootIsNull
PASS Solutions_ReturnExpectedDepth_ForBalancedSample
PASS Solutions_ReturnExpectedDepth_ForRightSkewedTree
PASS MinDepth2_DoesNotReusePreviousBestDepth_BetweenCalls
```

執行主程式範例：

```powershell
dotnet run --project leetcode_111/leetcode_111.csproj
```

實際範例輸出：

```text
LeetCode 111 - Minimum Depth of Binary Tree

空樹 root = []
  MinDepth  => 0 (expected 0) PASS
  MinDepth2 => 0 (expected 0) PASS
單一節點 root = [1]
  MinDepth  => 1 (expected 1) PASS
  MinDepth2 => 1 (expected 1) PASS
範例一 root = [3, 9, 20, null, null, 15, 7]
  MinDepth  => 2 (expected 2) PASS
  MinDepth2 => 2 (expected 2) PASS
範例二 root = [2, null, 3, null, 4, null, 5, null, 6]
  MinDepth  => 5 (expected 5) PASS
  MinDepth2 => 5 (expected 5) PASS
```
