# LeetCode 1046 — Last Stone Weight（最後一塊石頭的重量）

> 使用 **Max-Heap（最大優先佇列）** 模擬石頭碰撞遊戲，高效求解最後剩餘石頭重量。

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-14-239120?logo=csharp)](https://learn.microsoft.com/dotnet/csharp/)
[![LeetCode](https://img.shields.io/badge/LeetCode-1046-FFA116?logo=leetcode)](https://leetcode.com/problems/last-stone-weight/)

---

## 題目說明

> [!NOTE]
> 原題連結：[LeetCode 1046 - Last Stone Weight](https://leetcode.com/problems/last-stone-weight/description/)（[中文版](https://leetcode.cn/problems/last-stone-weight/description/)）

給定一個整數陣列 `stones`，其中 `stones[i]` 代表第 `i` 顆石頭的重量。

每個回合，從石頭堆中選出**最重的兩顆石頭** `x` 和 `y`（`x ≤ y`）互相砸碎：

- 若 `x == y`：兩顆石頭均被摧毀。
- 若 `x < y`：重量 `x` 的石頭被摧毀，重量 `y` 的石頭剩餘重量變為 `y - x`。

重複上述過程，直到石頭堆中剩餘不超過一顆石頭。

**回傳最後剩餘石頭的重量；若無石頭剩下，回傳 `0`。**

---

## 解題概念與出發點

核心問題是：**如何每次高效地取得目前最重的兩顆石頭？**

若每次都對陣列做排序，時間複雜度為 $O(n^2 \log n)$，效率不佳。

更好的選擇是使用 **優先佇列（Priority Queue）**，其底層為 **二元堆積（Binary Heap）**：
- 每次取出最大值的代價為 $O(\log n)$
- 插入新元素的代價為 $O(\log n)$

因此整體時間複雜度降至 $O(n \log n)$。

---

## 解法詳解

### 演算法步驟

1. **初始化**：將 `stones` 中所有元素加入優先佇列，以**負值**作為優先度，讓 .NET 預設的 Min-Heap 模擬 Max-Heap 的行為（即最重的石頭優先出列）。

2. **模擬碰撞**：只要佇列中有 **≥ 2 顆石頭**，就持續執行：
   - 先取出優先度最高的為 `y`（最重）
   - 再取出次高的為 `x`（次重）
   - 此時 `x ≤ y`
   - 若 `x == y`：不放回任何石頭（兩顆均摧毀）
   - 若 `x < y`：將 `y - x` 放回佇列繼續參與

3. **回傳結果**：
   - 佇列為空 → 回傳 `0`
   - 佇列剩一顆 → 取出並回傳其重量

### 程式碼

```csharp
public int LastStoneWeight(int[] stones)
{
    // 使用負值優先度將 Min-Heap 模擬為 Max-Heap
    PriorityQueue<int, int> pq = new PriorityQueue<int, int>();

    foreach (int stone in stones)
    {
        pq.Enqueue(stone, -stone);
    }

    while (pq.Count >= 2)
    {
        int y = pq.Dequeue(); // 最重的石頭
        int x = pq.Dequeue(); // 次重的石頭（x <= y）
        if (x < y)
        {
            pq.Enqueue(y - x, x - y); // 差值放回佇列
        }
        // x == y 時，兩顆均摧毀，不放回
    }

    return pq.Count == 0 ? 0 : pq.Dequeue();
}
```

### 複雜度分析

| 指標 | 複雜度 |
|------|--------|
| 時間複雜度 | $O(n \log n)$ |
| 空間複雜度 | $O(n)$ |

> [!NOTE]
> 每次 `Enqueue` / `Dequeue` 操作皆為 $O(\log n)$，最多執行 $n$ 輪碰撞，故整體為 $O(n \log n)$。

---

## 演示流程範例

以 `stones = [2, 7, 4, 1, 8, 1]` 為例，逐步說明：

| 回合 | 佇列內容（降序） | 取出 y | 取出 x | 結果 | 放回 |
|------|-----------------|--------|--------|------|------|
| 初始 | [8, 7, 4, 2, 1, 1] | — | — | — | — |
| 1 | [8, 7, 4, 2, 1, 1] | 8 | 7 | 8-7=1 | 1 |
| 2 | [4, 2, 1, 1, 1] | 4 | 2 | 4-2=2 | 2 |
| 3 | [2, 1, 1, 1] | 2 | 1 | 2-1=1 | 1 |
| 4 | [1, 1, 1] | 1 | 1 | 1-1=0 | 不放回 |
| 5 | [1] | — | — | — | — |

佇列剩 `[1]`，回傳 `1`。✓

---

## 測試案例

```csharp
var solution = new Program();

solution.LastStoneWeight([2, 7, 4, 1, 8, 1]); // => 1
solution.LastStoneWeight([1]);                 // => 1
solution.LastStoneWeight([3, 3]);              // => 0（兩顆重量相同，互相摧毀）
solution.LastStoneWeight([10, 4, 2, 10]);      // => 2（10 與 10 互相摧毀，4 與 2 碰撞剩 2）
```

---

## 補充：PriorityQueue 介紹

`PriorityQueue<TElement, TPriority>` 是 .NET 6 起內建的優先佇列實作，底層基於 **四元堆積（4-ary Min-Heap）**。

### 重要特性

- **預設為 Min-Heap**：優先度數值越小，越先出列。
- **泛型設計**：`TElement` 為儲存的元素，`TPriority` 為決定出列順序的優先度。
- **不保證同優先度的出列順序**（非穩定）。

### 常用 API

| 方法 | 說明 |
|------|------|
| `Enqueue(element, priority)` | 加入元素與其優先度 |
| `Dequeue()` | 取出並移除優先度最小的元素 |
| `Peek()` | 查看優先度最小的元素（不移除） |
| `Count` | 目前佇列中的元素數量 |

### 模擬 Max-Heap 的技巧

由於 .NET 的 `PriorityQueue` 預設為 Min-Heap，若需要 Max-Heap 效果，常見做法是**將優先度取負值**：

```csharp
// 加入元素時，以負值作為優先度
pq.Enqueue(value, -value);

// 取出的會是原始最大值
int max = pq.Dequeue();
```

這樣在 Min-Heap 的機制下，原本值越大的元素，其負值越小，因此會優先出列，達到 Max-Heap 的效果。

### 時間複雜度

| 操作 | 複雜度 |
|------|--------|
| `Enqueue` | $O(\log n)$ |
| `Dequeue` | $O(\log n)$ |
| `Peek` | $O(1)$ |
| 建立含 n 元素的堆積 | $O(n)$ |

> [!TIP]
> 若需要自訂比較器，可在建構子傳入 `IComparer<TPriority>`，例如 `new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b - a))` 直接實現 Max-Heap。
