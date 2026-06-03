# LeetCode 114 - Flatten Binary Tree to Linked List

![.NET 10](https://img.shields.io/badge/.NET-10-512BD4)
![C#](https://img.shields.io/badge/language-C%23-239120)
![Problem 114](https://img.shields.io/badge/LeetCode-114-orange)

以 C# 實作 LeetCode 114「將二元樹展開為鏈結串列」的練習專案。專案目前提供一個可直接執行的 console 範例入口，會建立測試樹、執行展平、驗證結果，並把每一組案例的輸入與輸出印到主控台。

> [!NOTE]
> 目前程式碼實作的是「前序走訪 + 節點列表重接」解法。README 同時整理另外兩種常見思路，方便比較設計差異，但這兩種替代解法尚未寫進專案。

## 快速連結

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一前序走訪--節點列表已實作](#解法一前序走訪--節點列表已實作)
- [解法二反向前序dfs未實作](#解法二反向前序-dfs未實作)
- [解法三原地指標重接未實作](#解法三原地指標重接未實作)
- [執行方式](#執行方式)
- [範例輸出](#範例輸出)

## 題目說明

給定一棵二元樹的根節點 `root`，要把整棵樹原地展平成單向鏈結串列形式：

- `left` 指標必須全部變成 `null`
- `right` 指標要依序串起所有節點
- 節點順序必須和原始樹的前序走訪一致，也就是 `root -> left -> right`

以題目常見範例 `[1,2,5,3,4,null,6]` 來看：

```text
原始樹
        1
      /   \
     2     5
    / \     \
   3   4     6

展平後
1
 \
  2
   \
    3
     \
      4
       \
        5
         \
          6
```

## 限制條件

根據題目原始限制：

- 樹中節點數量介於 `0` 到 `2000`
- `-100 <= Node.val <= 100`

這些限制代表：

- 可以接受遞迴解法，但極端情況下呼叫深度會接近節點數量
- 節點值範圍不影響演算法核心，真正重要的是指標如何重接
- 題目要求的是原地修改，而不是建立一棵新的樹

## 解題概念與出發點

這題的本質不是「重新排序數值」，而是「重新安排指標」。  
因為最後鏈結串列順序必須和前序走訪一致，所以最自然的切入點是：

1. 先搞清楚前序走訪的目標順序
2. 再把節點依照這個順序重新串成只走 `right` 的鏈

也就是說，解題真正要回答的問題是：

- 怎麼取得前序順序
- 怎麼在不遺失原本子樹的情況下完成重接
- 怎麼保證每個節點的 `left` 最後都變成 `null`

## 解法一：前序走訪 + 節點列表（已實作）

### 設計說明

這個做法分成兩段：

1. 用前序走訪把節點收集進 `List<TreeNode>`
2. 依照清單順序，把前一個節點的 `right` 指向下一個節點，並把 `left` 設成 `null`

為什麼這樣做可行：

- 題目要的最終順序，剛好就是前序走訪順序
- 一旦順序已經被放進陣列或清單，後續重接就只是線性處理
- 邏輯直觀，最適合教學、驗證與寫出可讀性高的示範程式

專案中的 `Program.Flatten` 就採用這個策略，並搭配 `Program.Preorder` 先蒐集節點順序。

### 演算法步驟

1. 若 `root` 是 `null`，直接結束
2. 建立 `List<TreeNode>` 作為前序結果容器
3. 以前序順序加入節點：先根節點，再左子樹，再右子樹
4. 從清單第 2 個節點開始走訪
5. 令 `previous.left = null`
6. 令 `previous.right = current`
7. 最後把尾節點的 `left` 與 `right` 都設為 `null`

### 範例演示流程

輸入：`[1, 2, 5, 3, 4, null, 6]`

第一階段，前序走訪收集順序：

```text
visit 1 -> list = [1]
visit 2 -> list = [1, 2]
visit 3 -> list = [1, 2, 3]
visit 4 -> list = [1, 2, 3, 4]
visit 5 -> list = [1, 2, 3, 4, 5]
visit 6 -> list = [1, 2, 3, 4, 5, 6]
```

第二階段，依序重接：

```text
1.right = 2, 1.left = null
2.right = 3, 2.left = null
3.right = 4, 3.left = null
4.right = 5, 4.left = null
5.right = 6, 5.left = null
6.right = null, 6.left = null
```

最後得到：

```text
[1, 2, 3, 4, 5, 6]
```

### 複雜度

- 時間複雜度：`O(n)`
- 額外空間複雜度：`O(n)`

### 優點

- 思路最直接
- 容易驗證與除錯
- 非常適合搭配教學與主控台示範輸出

### 缺點

- 額外使用一個節點清單
- 不算最節省空間的做法

## 解法二：反向前序 DFS（未實作）

### 設計說明

這個解法不需要先收集整份清單，而是使用一個外部指標 `prev`，改用「右、左、根」的反向前序順序處理：

1. 先處理右子樹
2. 再處理左子樹
3. 最後處理目前節點

當回到目前節點時：

- `current.right = prev`
- `current.left = null`
- `prev = current`

這樣做的核心理由是：  
因為我們想要把「目前節點後面要接誰」先準備好，所以就從答案尾端往前串。

### 演算法步驟

1. 宣告一個外部變數 `prev = null`
2. 遞迴先走右子樹，再走左子樹
3. 回到目前節點時，把 `right` 指向 `prev`
4. 把 `left` 設成 `null`
5. 更新 `prev = current`

### 範例演示流程

輸入：`[1, 2, 5, 3, 4, null, 6]`

反向處理順序會接近：

```text
6 -> 5 -> 4 -> 3 -> 2 -> 1
```

串接過程：

```text
prev = null
處理 6: 6.right = null, prev = 6
處理 5: 5.right = 6, prev = 5
處理 4: 4.right = 5, prev = 4
處理 3: 3.right = 4, prev = 3
處理 2: 2.right = 3, prev = 2
處理 1: 1.right = 2, prev = 1
```

最後同樣得到：

```text
[1, 2, 3, 4, 5, 6]
```

### 複雜度

- 時間複雜度：`O(n)`
- 額外空間複雜度：`O(h)`

其中 `h` 是樹高，主要來自遞迴呼叫堆疊。

### 優點

- 不需要額外 `List<TreeNode>`
- 程式碼通常比顯式清單版本更精煉

### 缺點

- 需要理解「反向」處理順序
- 對初學者來說，可讀性通常不如清單版本直觀
- 極端深樹下仍有遞迴深度風險

## 解法三：原地指標重接（未實作）

### 設計說明

這個做法完全從指標操作出發，不先存整份前序結果。  
對每個目前節點 `current`：

1. 如果 `current.left` 不為 `null`，找到左子樹最右邊的節點 `predecessor`
2. 把 `current` 原本的右子樹接到 `predecessor.right`
3. 把 `current.left` 移到 `current.right`
4. 再把 `current.left = null`
5. 讓 `current = current.right`，繼續往下走

這個思路的重點是：  
前序展平後，左子樹整段應該出現在原本右子樹之前，所以可以把整個左子樹搬到右邊，再把原右子樹接到左子樹尾端。

### 演算法步驟

1. 令 `current = root`
2. 當 `current != null` 時重複：
3. 若 `current.left == null`，直接走到 `current.right`
4. 若 `current.left != null`，找到左子樹最右節點
5. 把原本 `current.right` 接到該最右節點
6. 把 `current.left` 搬到 `current.right`
7. 清空 `current.left`
8. 繼續處理新的 `current.right`

### 範例演示流程

輸入：`[1, 2, 5, 3, 4, null, 6]`

第一輪，`current = 1`：

```text
左子樹是 2
左子樹最右節點是 4
把原本右子樹 5 接到 4.right
把 2 搬到 1.right
把 1.left 清空
```

此時右鏈骨架變成：

```text
1 -> 2
2 的子樹仍保留 3、4，且 4.right 已接上 5
```

第二輪，`current = 2`：

```text
左子樹是 3
左子樹最右節點是 3
把原本右子樹 4 接到 3.right
把 3 搬到 2.right
把 2.left 清空
```

繼續往右推進後，最終得到：

```text
[1, 2, 3, 4, 5, 6]
```

### 複雜度

- 時間複雜度：`O(n)`
- 額外空間複雜度：`O(1)`，不含輸入樹本身

### 優點

- 額外空間最省
- 完全原地處理

### 缺點

- 指標操作最繞
- 最容易在重接順序上犯錯
- 若是教學或面試白板題，說明成本通常最高

## 專案目前實作內容

目前 `leetcode_114/Program.cs` 包含：

- `TreeNode`：題目使用的二元樹節點結構
- `Flatten`：已實作的前序走訪 + 清單重接解法
- `Preorder`：遞迴收集前序順序
- `BuildTreeFromLevelOrder`：把 level-order 測資轉成實際樹結構
- `CollectRightChainValues`：收集展平後的輸出鏈
- `ValidateFlattenedResult`：驗證順序與 `left == null`
- `Main`：內建 4 組可執行案例

## 執行方式

### 建置

```powershell
dotnet build leetcode_114/leetcode_114.csproj
```

預期結果：建置成功，且不應有 warning 或 error。

### 執行範例

```powershell
dotnet run --project leetcode_114/leetcode_114.csproj
```

此命令會：

- 建立 4 組內建案例
- 對每一組執行 `Flatten`
- 驗證輸出順序是否正確
- 驗證所有 `left` 指標是否都已清空
- 將輸入與輸出印到主控台

### 檢查空白與換行

```powershell
git diff --check
```

預期結果：沒有任何輸出。

## 範例輸出

目前 `dotnet run --project leetcode_114/leetcode_114.csproj` 的實際輸出如下：

```text
[balanced] input=[1, 2, 5, 3, 4, null, 6] output=[1, 2, 3, 4, 5, 6]
[empty] input=[] output=[]
[single] input=[0] output=[0]
[left-skewed] input=[1, 2, null, 3, null, 4] output=[1, 2, 3, 4]
```

如果其中任何案例不符合預期，程式會直接丟出例外，讓問題在執行階段立即暴露。

## 專案結構

```text
.
├─ docs/
│  └─ readme-template.md
├─ leetcode_114/
│  ├─ Program.cs
│  └─ leetcode_114.csproj
└─ README.md
```

## 為什麼這個專案採用清單版解法

這份專案的重點不只是「解出題目」，也包含：

- 讓 `Main` 可以直接當成可執行示範
- 讓 README 能清楚對照程式行為
- 讓每個 helper method 都能用註解說清楚責任與輸入輸出

在這種前提下，`前序走訪 + List` 雖然不是最省空間的版本，卻是最容易閱讀、驗證、教學與維護的版本，因此被選為目前實作。
