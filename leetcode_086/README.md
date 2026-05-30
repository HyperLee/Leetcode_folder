# LeetCode 86 - Partition List

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![Language](https://img.shields.io/badge/Language-C%23-239120)
![Topic](https://img.shields.io/badge/Topic-Linked%20List-blue)

這個專案使用 C#/.NET 實作 LeetCode 86「Partition List」。目標是在不改變兩個分區內相對順序的前提下，將小於 `x` 的節點移到大於或等於 `x` 的節點之前。

## 題目說明

給定一個鏈結串列的頭節點 `head` 與整數 `x`，請重新排列鏈結串列，使所有節點值小於 `x` 的節點都出現在節點值大於或等於 `x` 的節點之前。

重要條件是「穩定分隔」：小於 `x` 的節點彼此之間要維持原本順序，大於或等於 `x` 的節點彼此之間也要維持原本順序。

## 限制條件

- 鏈結串列節點數量範圍：`0 <= node count <= 200`
- 節點值範圍：`-100 <= Node.val <= 100`
- 分隔值範圍：`-200 <= x <= 200`
- 空鏈結串列是合法輸入，輸出也應為空鏈結串列。

## 解題概念與出發點

這題不是排序問題，而是穩定分區問題。若直接交換節點值或把節點插到前方，很容易破壞原本的相對順序。

比較穩定的做法是建立兩條暫存鏈結串列：

- `small`：依照走訪順序收集所有 `< x` 的節點。
- `large`：依照走訪順序收集所有 `>= x` 的節點。

走訪完成後，把 `small` 的尾端接到 `large` 的頭端，就能得到符合題意的結果。

## 解法一：雙鏈結串列分流法

程式中的 `Partition` 使用兩個 dummy head 節點處理分區。dummy head 不代表實際資料，只是讓「空分區」與「第一個節點」的接法一致，避免額外處理頭節點為空的特殊情況。

設計流程：

1. 建立 `smallHead` 與 `largeHead` 兩個 dummy head。
2. 使用 `small` 與 `large` 作為兩條鏈結串列目前的尾端指標。
3. 從原始 `head` 開始逐一走訪節點。
4. 若目前節點值 `< x`，就接到 `small` 尾端。
5. 若目前節點值 `>= x`，就接到 `large` 尾端。
6. 每次接上節點前先保存原本的 `next`，接上後將目前節點的 `next` 斷開，避免重用原節點時留下舊連結。
7. 走訪完成後，將 `small` 尾端接到 `largeHead.next`。
8. 回傳 `smallHead.next`，略過 dummy head。

這個方法只改變節點連結方向，不建立新的資料節點作為答案，因此符合鏈結串列題目常見的原地重排精神。

### 複雜度

- 時間複雜度：`O(n)`，每個節點只走訪一次。
- 額外空間複雜度：`O(1)`，只使用固定數量的指標與 dummy 節點；範例輸出 helper 的陣列不屬於核心演算法。

## 範例演示流程

### 範例 1

輸入：

```text
head = [1, 4, 3, 2, 5, 2], x = 3
```

分流過程：

| 走訪節點 | 判斷 | small 分區 | large 分區 |
| --- | --- | --- | --- |
| `1` | `1 < 3` | `[1]` | `[]` |
| `4` | `4 >= 3` | `[1]` | `[4]` |
| `3` | `3 >= 3` | `[1]` | `[4, 3]` |
| `2` | `2 < 3` | `[1, 2]` | `[4, 3]` |
| `5` | `5 >= 3` | `[1, 2]` | `[4, 3, 5]` |
| `2` | `2 < 3` | `[1, 2, 2]` | `[4, 3, 5]` |

最後串接：

```text
[1, 2, 2] + [4, 3, 5] = [1, 2, 2, 4, 3, 5]
```

### 範例 2

輸入：

```text
head = [2, 1], x = 2
```

- `2 >= 2`，進入 `large` 分區。
- `1 < 2`，進入 `small` 分區。
- 串接後結果為 `[1, 2]`。

### 範例 3

輸入：

```text
head = [], x = 1
```

空鏈結串列不會進入走訪迴圈，`smallHead.next` 仍為 `null`，輸出為 `[]`。

## 專案結構

```text
.
├── README.md
├── docs
│   └── readme-template.md
└── leetcode_086
    ├── Program.cs
    └── leetcode_086.csproj
```

## 建置與執行

確認環境：

```bash
dotnet --version
```

本專案驗證時使用：

```text
10.0.100
```

建置專案：

```bash
dotnet build leetcode_086/leetcode_086.csproj
```

執行範例：

```bash
dotnet run --project leetcode_086/leetcode_086.csproj
```

預期輸出：

```text
LeetCode 86 - Partition List

Example 1: PASS
  Input: head = [1, 4, 3, 2, 5, 2], x = 3
  Expected: [1, 2, 2, 4, 3, 5]
  Actual:   [1, 2, 2, 4, 3, 5]

Example 2: PASS
  Input: head = [2, 1], x = 2
  Expected: [1, 2]
  Actual:   [1, 2]

Example 3: PASS
  Input: head = [], x = 1
  Expected: []
  Actual:   []
```

> [!NOTE]
> 在目前 macOS/.NET 環境執行 `dotnet` 指令時，可能會先出現 `CSSM_ModuleLoad(): One or more parameters passed to a function were not valid.`。這是環境訊息，不影響建置成功或範例輸出結果。
