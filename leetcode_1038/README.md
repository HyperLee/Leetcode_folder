# LeetCode 1038 — Binary Search Tree to Greater Sum Tree

![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4)
![Language](https://img.shields.io/badge/language-C%23-239120)

本專案使用 C# 與反向中序走訪，將二元搜尋樹（Binary Search Tree，BST）
原地轉換為較大和樹（Greater Sum Tree，GST），並提供四組可直接執行、
會自行比對預期結果的範例。

- [題目說明](#題目說明)
- [解題概念與出發點](#解題概念與出發點)
- [解法一遞迴反向中序走訪](#解法一遞迴反向中序走訪)
- [範例演示](#範例演示)
- [建置與執行](#建置與執行)

## 題目說明

給定一棵二元搜尋樹的根節點 `root`，將每個節點的值改成：

> 節點原值 + 原始 BST 中所有大於該節點值的值之總和

轉換直接修改原本的節點，最後回傳同一個根節點。

以官方完整案例為例：

```text
輸入： [4,1,6,0,2,5,7,null,null,null,3,null,null,null,8]
輸出： [30,36,21,36,35,26,15,null,null,null,33,null,null,null,8]
```

### 二元搜尋樹的性質

對任一節點而言：

- 左子樹中的值都小於目前節點。
- 右子樹中的值都大於目前節點。
- 左、右子樹本身也都是二元搜尋樹。

### 限制條件

依 [LeetCode 1038 官方題面](https://leetcode.com/problems/binary-search-tree-to-greater-sum-tree/description/)：

- 節點數量介於 `1` 到 `100`。
- `0 <= Node.val <= 100`。
- 樹中所有節點值互不相同。

程式另外支援 `root == null`，方便示範空樹的防禦性處理；空樹不是本題正式測試資料的必要條件。

## 解題概念與出發點

### 從普通中序走訪開始

BST 的普通中序走訪順序是：

```text
左子樹 → 根節點 → 右子樹
```

因為左邊較小、右邊較大，所以得到的節點值會由小到大排列。但題目在更新某個
節點時，需要知道所有「比它大的值」，由小到大處理會使這些值尚未被走訪。

### 將順序反過來

把中序走訪反轉為：

```text
右子樹 → 根節點 → 左子樹
```

節點便會由大到小被處理。維護一個 `accumulatedSum`：

1. 先走訪右子樹，處理所有更大的節點。
2. 把目前節點原值加進 `accumulatedSum`。
3. 將目前節點改為這個累加值。
4. 再走訪左子樹，讓較小節點使用已累積的所有較大值。

當程式處理目前節點時，`accumulatedSum` 已包含所有比目前值更大的節點；
加入目前原值後，正好就是題目要求的新值。

## 解法一：遞迴反向中序走訪

### 設計說明

公開方法 `BstToGst` 在每次呼叫時建立自己的區域累加器，再由區域遞迴函式
`Traverse` 完成走訪。累加器不使用靜態欄位，因此：

- 不同測試案例不會共用前一次的累加結果。
- 同一個行程中可安全地連續轉換多棵樹。
- 遞迴邏輯仍直接對輸入樹原地更新，不需要建立第二棵樹。

核心流程可表示為：

```text
Traverse(node):
    如果 node 為 null，返回
    Traverse(node.right)
    accumulatedSum += node.val
    node.val = accumulatedSum
    Traverse(node.left)
```

### 正確性說明

反向中序會先完成右子樹，因此處理 `node` 前，比 `node.val` 大的所有值都已經
加入累加器。接著加入 `node` 的原值，累加器便等於「目前原值加上所有更大值」
的總和。把它寫回 `node.val` 即符合題意。最後走訪左子樹時，累加器也已包含
目前節點，可供所有更小的節點繼續使用。這個性質遞迴套用到整棵樹，因此每個
節點都會得到正確的新值。

### 複雜度

- 時間複雜度：`O(n)`，每個節點只處理一次。
- 空間複雜度：`O(h)`，`h` 是樹高，空間來自遞迴呼叫堆疊。
  - 平衡樹約為 `O(log n)`。
  - 完全偏斜的樹最差為 `O(n)`。

## 範例演示

### 官方完整 BST

原始層序資料：

```text
[4,1,6,0,2,5,7,null,null,null,3,null,null,null,8]
```

反向中序的實際處理順序為：

```text
8 → 7 → 6 → 5 → 4 → 3 → 2 → 1 → 0
```

| 處理順序 | 節點原值 | 加入前累加值 | 更新後節點值 |
|---:|---:|---:|---:|
| 1 | 8 | 0 | 8 |
| 2 | 7 | 8 | 15 |
| 3 | 6 | 15 | 21 |
| 4 | 5 | 21 | 26 |
| 5 | 4 | 26 | 30 |
| 6 | 3 | 30 | 33 |
| 7 | 2 | 33 | 35 |
| 8 | 1 | 35 | 36 |
| 9 | 0 | 36 | 36 |

更新完成後，以層序表示為：

```text
[30,36,21,36,35,26,15,null,null,null,33,null,null,null,8]
```

### 兩個節點

```text
輸入： [0,null,1]
走訪： 1 → 0
更新： 1 保持為 1；0 加上 1 後變為 1
輸出： [1,null,1]
```

### 單一節點

單一節點沒有更大的值，因此 `[1]` 轉換後仍是 `[1]`。

### 空樹

空樹沒有節點需要更新，輸入 `[]` 時直接回傳並輸出 `[]`。

## 專案結構

```text
leetcode_1038/
├── docs/
│   └── readme-template.md
├── leetcode_1038/
│   ├── leetcode_1038.csproj
│   └── Program.cs
├── leetcode_1038.sln
└── README.md
```

## 建置與執行

需求：

- .NET 10 SDK

從此 repository 根目錄依序執行：

```powershell
dotnet restore leetcode_1038/leetcode_1038.csproj
dotnet build leetcode_1038/leetcode_1038.csproj --nologo
dotnet run --project leetcode_1038/leetcode_1038.csproj
```

### 執行結果

```text
Case: 官方完整 BST
Expected: [30,36,21,36,35,26,15,null,null,null,33,null,null,null,8]
Actual:   [30,36,21,36,35,26,15,null,null,null,33,null,null,null,8]
Result: PASS

Case: 兩個節點
Expected: [1,null,1]
Actual:   [1,null,1]
Result: PASS

Case: 單一節點
Expected: [1]
Actual:   [1]
Result: PASS

Case: 空樹
Expected: []
Actual:   []
Result: PASS

Summary: 4/4 tests passed.
```

若任一案例失敗，主控台會顯示 `FAIL`，程式也會設定非零結束碼，方便命令列或
自動化流程判定執行結果。
