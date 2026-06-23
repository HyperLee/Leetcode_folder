# LeetCode 222 - Count Complete Tree Nodes

這個專案是 LeetCode 222「**Count Complete Tree Nodes**」的 .NET 10 主控台解題示範。  
重點不只是在程式算出答案，還包含：

- 可直接執行的 `Main` sample harness
- 三種節點計數解法的對照
- 完整二元樹最佳解為什麼能低於 `O(n)` 的說明
- 與實際 `dotnet run` 結果逐字對齊的驗證輸出

## 題目說明

給定一棵 **完整二元樹**（complete binary tree）的根節點，請回傳樹中的節點總數。

題目的關鍵不只是「數節點」，而是：

- 輸入保證是完整二元樹
- 目標是設計 **低於 `O(n)`** 的演算法

這表示如果只是把每個節點都拜訪一次，雖然答案正確，但還沒有完全用上題目給的結構特性。

## 限制條件

根據題目設定，可以抓住幾個最重要的限制：

- 樹中節點數量上限為 `5 * 10^4`
- 輸入一定是一棵完整二元樹
- 最後一層節點只會出現在最左側連續區域，不會中間缺洞

其中最重要的是第二、三點。因為完整二元樹的形狀非常規律，所以我們能夠只遞迴到「可能不完整」的那一側，而不是每個節點都走一次。

## 完整二元樹特性與解題出發點

完整二元樹有兩個很值得利用的觀察：

1. 除了最後一層之外，其餘每一層都滿。
2. 最後一層的節點一定從左到右連續出現。

這會帶來一個非常有用的推論：

- 如果某個節點的左子樹與右子樹，沿著最左邊一路往下走的深度相同，表示左子樹一定是滿二元樹。
- 如果兩邊深度不同，表示右子樹一定是滿二元樹，而且左子樹才是那個還可能「最後一層沒填滿」的區域。

所以最佳解不是把整棵樹完整展開，而是：

- 先用高度判斷哪一側可以直接套公式
- 只對另一側做遞迴

這就是本題能低於 `O(n)` 的核心原因。

## 專案中的三種解法

| 方法 | 函式 | 核心概念 | 時間複雜度 | 空間複雜度 |
| --- | --- | --- | --- | --- |
| 最佳解 | `CountNodes` | 利用完整二元樹高度判斷滿子樹 | `O(log^2 n)` | `O(log n)` |
| 對照解一 | `CountNodesRecursive` | DFS 遞迴逐節點累加 | `O(n)` | `O(h)` |
| 對照解二 | `CountNodesBreadthFirst` | BFS 層序走訪逐節點累加 | `O(n)` | `O(n)` |

---

## 解法一：`CountNodes` 最佳解

### 設計思路

這個解法專門利用完整二元樹的結構特性。

對任一節點 `root`：

- 量出 `root.Left` 的最左深度
- 量出 `root.Right` 的最左深度

接著分兩種情況：

### 情況 A：左右最左深度相同

如果：

- `leftDepth == rightDepth`

表示左子樹一定是滿二元樹。

原因是：

- 右子樹既然可以達到和左子樹一樣的最左深度
- 代表左子樹那一層一定早就填滿了

此時可以直接算出左子樹節點數：

- 左子樹節點數 = `2^leftDepth - 1`
- 再加上根節點 `1`

因此總數可寫成：

```text
1 + (2^leftDepth - 1) + CountNodes(root.Right)
```

也就是：

```text
2^leftDepth + CountNodes(root.Right)
```

### 情況 B：左右最左深度不同

如果：

- `leftDepth > rightDepth`

表示右子樹一定是滿二元樹，而左子樹是那個還可能不完整的區域。

這時候就改成：

```text
1 + CountNodes(root.Left) + (2^rightDepth - 1)
```

### 為什麼是 `O(log^2 n)`？

每一層遞迴都要做兩次最左深度量測，而每次量測最多走 `O(log n)` 層。  
遞迴本身最多也只會往下走 `O(log n)` 層。

所以總時間為：

```text
O(log n) * O(log n) = O(log^2 n)
```

### 範例推演：`[1, 2, 3, 4, 5, 6]`

這個 sample 也出現在 `Main` 的 Case 3。

樹形如下：

```text
        1
      /   \
     2     3
    / \   /
   4   5 6
```

第一輪：

- `root = 1`
- `leftDepth = 2`，因為 `2 -> 4`
- `rightDepth = 2`，因為 `3 -> 6`
- 深度相同，所以左子樹一定是滿二元樹

直接得到：

- 左子樹節點數 = `2^2 - 1 = 3`
- 加上根節點後，已確定 `4` 個節點
- 剩下只要遞迴數右子樹 `3`

第二輪（子樹根節點為 `3`）：

- `leftDepth = 1`，因為 `6`
- `rightDepth = 0`，因為右邊為空
- 深度不同，所以右子樹是滿二元樹（其實是空滿樹）

得到：

- 右子樹節點數 = `2^0 - 1 = 0`
- 這一輪是 `1 + CountNodes(6) + 0`

第三輪（子樹根節點為 `6`）：

- 左右都空，回傳 `1`

合併：

- `CountNodes(3) = 1 + 1 + 0 = 2`
- `CountNodes(1) = 1 + 3 + 2 = 6`

---

## 解法二：`CountNodesRecursive` 遞迴 DFS

### 設計思路

這個版本完全不利用完整二元樹特性，只把問題拆成：

- 自己算 `1`
- 左子樹節點數
- 右子樹節點數

遞迴公式：

```text
Count(root) = 1 + Count(root.Left) + Count(root.Right)
```

Base case：

- `root == null` 時回傳 `0`

### 優點

- 寫法最直觀
- 公式最容易記
- 即使輸入不是完整二元樹，仍然正確

### 缺點

- 題目希望低於 `O(n)`，這個版本做不到
- 每個節點都一定會被拜訪一次

### 範例推演：`[1]`

這個 sample 也出現在 `Main` 的 Case 2。

流程非常單純：

1. `root = 1`，不是空節點
2. 回傳 `1 + Count(null) + Count(null)`
3. 兩個子樹都回傳 `0`
4. 結果為 `1`

這個例子很小，但剛好能清楚看出遞迴公式的最基本型態。

---

## 解法三：`CountNodesBreadthFirst` 迭代 BFS

### 設計思路

這個版本用 queue 做層序遍歷：

1. 根節點先入隊
2. 每次出隊一個節點，就把計數加一
3. 如果左子節點存在，入隊
4. 如果右子節點存在，入隊
5. queue 清空後，累加值就是答案

### 優點

- 很適合拿來對照「一層一層數」的思考方式
- 沒有遞迴深度問題
- 對任意二元樹也成立

### 缺點

- 一樣需要把每個節點都拜訪一次
- queue 在最寬那一層可能同時放很多節點

### 範例推演：`[1, 2, 3, 4, 5, 6, 7, 8, 9, 10]`

這個 sample 也出現在 `Main` 的 Case 5。

可以把 queue 的變化簡化成下面的節奏：

1. 初始 queue：`[1]`，count = `0`
2. 取出 `1`，count = `1`，放入 `2, 3`
3. 取出 `2`，count = `2`，放入 `4, 5`
4. 取出 `3`，count = `3`，放入 `6, 7`
5. 取出 `4`，count = `4`，放入 `8, 9`
6. 取出 `5`，count = `5`，放入 `10`
7. 依序取出 `6, 7, 8, 9, 10`
8. queue 清空後，count = `10`

這類做法的優勢是思路穩定、容易手推，但從題目要求來看，仍屬於線性解。

## 為什麼專案同時保留三種解法？

因為這題最值得學的不是只有「寫出最佳解」，而是理解：

- 線性解法為什麼正確
- 線性解法為什麼不夠好
- 完整二元樹的哪個特性讓我們能進一步優化

把三種解法放在一起有兩個好處：

1. 可以確認最佳解和直觀解法得到相同答案。
2. 可以更清楚比較「一般二元樹數節點」與「完整二元樹專屬優化」之間的差別。

## 建置與執行

請在 repo 根目錄執行：

```bash
dotnet build leetcode_222/leetcode_222.csproj
dotnet run --project leetcode_222/leetcode_222.csproj
```

> [!NOTE]
> 目前沒有獨立的 `*.Tests` 測試專案，因此這個 repo 的驗證方式以 `dotnet build`、`dotnet run` 與 sample 對照為主。

## 實際執行輸出

以下輸出直接來自：

```bash
dotnet run --project leetcode_222/leetcode_222.csproj
```

```text
LeetCode 222 - Count Complete Tree Nodes
========================================

Case 1
Input (level-order): []
Expected nodes: 0
Optimized CountNodes: 0
Recursive CountNodesRecursive: 0
Breadth-first CountNodesBreadthFirst: 0
Result: PASS

Case 2
Input (level-order): [1]
Expected nodes: 1
Optimized CountNodes: 1
Recursive CountNodesRecursive: 1
Breadth-first CountNodesBreadthFirst: 1
Result: PASS

Case 3
Input (level-order): [1, 2, 3, 4, 5, 6]
Expected nodes: 6
Optimized CountNodes: 6
Recursive CountNodesRecursive: 6
Breadth-first CountNodesBreadthFirst: 6
Result: PASS

Case 4
Input (level-order): [1, 2, 3, 4, 5, 6, 7]
Expected nodes: 7
Optimized CountNodes: 7
Recursive CountNodesRecursive: 7
Breadth-first CountNodesBreadthFirst: 7
Result: PASS

Case 5
Input (level-order): [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
Expected nodes: 10
Optimized CountNodes: 10
Recursive CountNodesRecursive: 10
Breadth-first CountNodesBreadthFirst: 10
Result: PASS

Summary: 5/5 sample(s) passed.
```

## 專案檔案重點

- `leetcode_222/Program.cs`：題目說明、三種解法、建樹 helper、可執行 sample harness
- `docs/readme-template.md`：README 初始模板參考
- `README.md`：本文件，整理題意、解法、複雜度與實際執行結果

## 小結

如果只要求「正確」，DFS 與 BFS 都能解。  
但這題真正想考的是：**你有沒有利用 complete binary tree 的結構，把問題縮小到只遞迴不完整的那一側。**

因此這個專案把：

- 正確但線性的兩種基礎寫法
- 符合題目進階要求的 `O(log^2 n)` 最佳解

放在同一個可執行範例裡，方便直接比較與複習。
