# LeetCode 979：在二元樹中分配硬幣

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)

這是一個使用 C# 與 .NET 10 實作的主控台專案，解答
[LeetCode 979. Distribute Coins in Binary Tree](https://leetcode.com/problems/distribute-coins-in-binary-tree/)。
專案內建四組固定案例，不需要輸入資料即可直接執行並檢查答案。

## 題目說明

給定一棵包含 `n` 個節點的二元樹，每個節點的 `val` 表示該節點目前持有的硬幣數量，
而整棵樹的硬幣總數恰好也是 `n`。

每次操作可以在兩個相鄰節點之間移動一枚硬幣：

- 從父節點移到子節點。
- 從子節點移到父節點。

目標是讓每個節點最後都恰好持有一枚硬幣，並回傳所需的最少移動次數。

### 限制條件

- 樹的節點數為 `n`。
- `1 <= n <= 100`。
- `0 <= Node.val <= n`。
- 所有節點的 `Node.val` 總和等於 `n`。

## 解題概念與出發點

### 1. 不必模擬每一枚硬幣

直接決定「哪一枚硬幣要走到哪裡」會產生很多不必要的選擇。真正重要的是：

> 一棵子樹在替自己的每個節點各保留一枚硬幣後，還剩多少硬幣，或缺少多少硬幣？

把這個數值稱為子樹的 `balance`：

```text
balance = 子樹目前的硬幣總數 - 子樹的節點總數
```

- `balance > 0`：子樹有盈餘，必須把硬幣送往父節點。
- `balance < 0`：子樹有短缺，必須從父節點取得硬幣。
- `balance = 0`：子樹內部已平衡，不必再和父節點交換硬幣。

### 2. 為什麼要使用後序 DFS

父節點必須先知道左、右子樹各自的盈虧，才能決定有多少硬幣需要跨越兩條子邊。
因此走訪順序是：

```text
左子樹 → 右子樹 → 目前節點
```

這正是後序深度優先搜尋（post-order DFS）。

### 3. 為什麼移動次數是 `|balance|`

子樹只能透過「子樹根節點與父節點之間的那一條邊」和外界交換硬幣。

假設左子樹的 `balance = 2`，代表有兩枚盈餘硬幣必須跨過左子樹與目前節點之間的邊，
因此產生兩次移動。若 `balance = -2`，則必須由父節點跨過同一條邊補入兩枚，
同樣也是兩次移動。

所以每個節點處新增的移動次數為：

```text
|leftBalance| + |rightBalance|
```

而目前子樹要交給父節點的 balance 為：

```text
node.val + leftBalance + rightBalance - 1
```

最後的 `-1` 表示目前節點先替自己保留一枚硬幣。

## 解法一：後序 DFS 計算子樹盈虧

專案目前只有這一種解法。

### 設計流程

1. `DistributeCoins` 建立只屬於本次呼叫的 `moves`。
2. `CalculateBalance` 遞迴處理左子樹，取得 `leftBalance`。
3. 遞迴處理右子樹，取得 `rightBalance`。
4. 將 `|leftBalance| + |rightBalance|` 加入 `moves`。
5. 回傳目前子樹的 balance 給父節點。
6. DFS 完成後，`moves` 就是全樹所需的最少移動次數。

`moves` 是單次 `DistributeCoins` 呼叫內的區域狀態，因此連續執行多組案例時，
前一棵樹的答案不會污染下一棵樹。

### 核心程式

```csharp
public static int DistributeCoins(TreeNode root)
{
    int moves = 0;
    CalculateBalance(root, ref moves);
    return moves;
}

private static int CalculateBalance(TreeNode? node, ref int moves)
{
    if (node == null)
    {
        return 0;
    }

    int leftBalance = CalculateBalance(node.left, ref moves);
    int rightBalance = CalculateBalance(node.right, ref moves);

    moves += Math.Abs(leftBalance) + Math.Abs(rightBalance);

    return node.val + leftBalance + rightBalance - 1;
}
```

### 為什麼這會得到最少次數

對任一非根子樹而言，它與樹中其他部分只有一條連接邊。

- 若子樹盈餘 `k` 枚，這 `k` 枚一定都要跨過該邊離開。
- 若子樹短缺 `k` 枚，也一定要有 `k` 枚跨過該邊進入。

因此這條邊至少必須被使用 `|balance|` 次，沒有任何解法能更少。
後序 DFS 對每條父子邊恰好累加這個必要次數，所以得到的總和就是全域最小值。

## 範例演示

### 範例一：`[0,3,0]`

樹形如下：

```text
    0
   / \
  3   0
```

後序 DFS 的計算過程：

| 處理節點 | leftBalance | rightBalance | 本步新增 moves | 回傳 balance |
|---|---:|---:|---:|---:|
| 左節點 `3` | 0 | 0 | 0 | `3 - 1 = 2` |
| 右節點 `0` | 0 | 0 | 0 | `0 - 1 = -1` |
| 根節點 `0` | 2 | -1 | `2 + 1 = 3` | `0 + 2 - 1 - 1 = 0` |

實際搬運可以理解為：

1. 左節點把第一枚盈餘硬幣移到根節點。
2. 左節點把第二枚盈餘硬幣移到根節點。
3. 根節點把其中一枚硬幣移到右節點。

總共需要 `3` 次移動。

### 範例二：`[1,0,0,null,3]`

層序表示中的 `null` 代表缺少左節點，樹形如下：

```text
      1
     / \
    0   0
     \
      3
```

後序 DFS 的計算過程：

| 處理節點 | leftBalance | rightBalance | 累計前新增 moves | 回傳 balance |
|---|---:|---:|---:|---:|
| 節點 `3` | 0 | 0 | 0 | `3 - 1 = 2` |
| 左節點 `0` | 0 | 2 | `0 + 2 = 2` | `0 + 0 + 2 - 1 = 1` |
| 右節點 `0` | 0 | 0 | 0 | `0 - 1 = -1` |
| 根節點 `1` | 1 | -1 | `1 + 1 = 2` | `1 + 1 - 1 - 1 = 0` |

總移動次數為 `2 + 2 = 4`：

- 節點 `3` 的兩枚盈餘都要跨到它的父節點，共兩次。
- 左子樹剩下的一枚盈餘再跨到根節點，共一次。
- 根節點把這枚硬幣送到短缺的右節點，再一次。

## 複雜度分析

令 `n` 為節點數、`h` 為樹高：

- 時間複雜度：`O(n)`。每個節點只會被 DFS 處理一次。
- 空間複雜度：`O(h)`。空間來自遞迴呼叫堆疊。
  - 平衡樹約為 `O(log n)`。
  - 完全偏斜的樹最壞為 `O(n)`。

演算法沒有另外建立與節點數同級的集合或陣列。

## 內建測試案例

| 案例 | 輸入 | 預期結果 | 測試重點 |
|---|---|---:|---|
| 1 | `[3,0,0]` | 2 | 根節點將硬幣分給兩個子節點 |
| 2 | `[0,3,0]` | 3 | 硬幣先向上移，再向下移 |
| 3 | `[1]` | 0 | 單節點邊界，原本已平衡 |
| 4 | `[1,0,0,null,3]` | 4 | 硬幣跨越多層與不同子樹 |

## 建置與執行

請從此儲存庫根目錄執行：

```powershell
dotnet build leetcode_979/leetcode_979.csproj --nologo
dotnet run --project leetcode_979/leetcode_979.csproj
```

此專案目前沒有獨立的自動化測試專案；`Main` 內的固定案例 runner 是行為驗收工具。
如果任一案例失敗，程式會輸出 `Overall: FAIL` 並設定非零結束碼。

### 實際執行輸出

```text
Case 1: root = [3,0,0]
Expected: 2
Actual: 2
Result: PASS

Case 2: root = [0,3,0]
Expected: 3
Actual: 3
Result: PASS

Case 3: root = [1]
Expected: 0
Actual: 0
Result: PASS

Case 4: root = [1,0,0,null,3]
Expected: 4
Actual: 4
Result: PASS

Summary: 4/4 passed.
Overall: PASS
```

## 專案結構

```text
.
├── leetcode_979/
│   ├── leetcode_979.csproj
│   └── Program.cs
├── docs/
│   └── readme-template.md
├── AGENTS.md
├── README.md
└── leetcode_979.sln
```
