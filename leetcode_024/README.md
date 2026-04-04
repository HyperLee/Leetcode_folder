# LeetCode 24 — Swap Nodes in Pairs

[![Build](https://img.shields.io/badge/build-passing-brightgreen?style=flat-square)](leetcode_024/leetcode_024.csproj)
[![Language](https://img.shields.io/badge/C%23-14-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![.NET](https://img.shields.io/badge/.NET-10-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![Difficulty](https://img.shields.io/badge/Difficulty-Medium-orange?style=flat-square)](https://leetcode.com/problems/swap-nodes-in-pairs/)

> 給定一個鏈表，兩兩交換相鄰節點並回傳頭節點。  
> **限制：** 不得修改節點的值，只能改變節點本身的連接。

題目連結：[LeetCode 24 (EN)](https://leetcode.com/problems/swap-nodes-in-pairs/description/) ・ [力扣 24 (CN)](https://leetcode.cn/problems/swap-nodes-in-pairs/description/)

---

## 題目說明

給定一個鏈表的頭節點 `head`，將鏈表中**每兩個相鄰的節點**交換位置，並返回交換後的鏈表頭節點。

**限制條件：**

- 鏈表節點數範圍：`0 <= n <= 100`
- 節點值範圍：`0 <= Node.val <= 100`
- **只能改變節點的連接，不可修改節點的值。**

### 範例輸入 / 輸出

| 輸入 | 輸出 |
|------|------|
| `[1, 2, 3, 4]` | `[2, 1, 4, 3]` |
| `[]` | `[]` |
| `[1]` | `[1]` |
| `[1, 2, 3]` | `[2, 1, 3]` |

---

## 解題概念與出發點

### 核心觀察

交換「每兩個相鄰節點」這個操作，具有**遞迴子結構**：

> 若想交換整條鏈表的節點對，只需先交換**最前面兩個節點**，  
> 再把**剩餘鏈表**的交換結果，接到第一個節點的後面即可。

這正是遞迴（Recursion）的經典使用場景：
- **大問題** = 交換 N 個節點的鏈表
- **小問題** = 交換最前面兩個節點，剩下的交給遞迴

### 終止條件（Base Case）

- `head == null`：鏈表為空，直接返回。
- `head.next == null`：只剩一個節點（奇數尾端），無法配對，直接返回。

---

## 解法說明

### 演算法（遞迴）

```
SwapPairs(head):
    if head 或 head.next 為 null → return head

    newHead = head.next          // 第二個節點成為新的頭

    head.next = SwapPairs(newHead.next)   // 遞迴處理後半段，接到第一個節點
    newHead.next = head                   // 第二個節點指向第一個，完成交換

    return newHead
```

### 複雜度分析

| 面向 | 複雜度 | 說明 |
|------|--------|------|
| 時間 | O(n)   | 每個節點恰好被拜訪一次 |
| 空間 | O(n)   | 遞迴呼叫堆疊深度為 n/2 |

---

## 流程演示

以 `[1 → 2 → 3 → 4]` 為例，說明每層遞迴的狀態。

### 第 1 層：處理節點 1 和 2

```
呼叫前：  head(1) → newHead(2) → [3 → 4]

步驟：
  newHead = 2
  head.next = SwapPairs([3 → 4])   ← 交給下一層遞迴
  newHead.next = head(1)

回傳前（等待遞迴結果）：
  2 → 1 → ???
```

### 第 2 層：處理節點 3 和 4

```
呼叫前：  head(3) → newHead(4) → null

步驟：
  newHead = 4
  head.next = SwapPairs(null) → null
  newHead.next = head(3)

回傳：4 → 3 → null
```

### 回溯合併

```
第 2 層回傳：  4 → 3
第 1 層收到後：2 → 1 → 4 → 3

最終結果：[2 → 1 → 4 → 3]
```

### 奇數節點 `[1 → 2 → 3]` 的情形

```
第 1 層：head=1, newHead=2, SwapPairs([3])
  第 2 層：head=3, head.next==null → 直接回傳 3
第 1 層收到 3 後：
  head(1).next = 3
  newHead(2).next = 1

回傳：2 → 1 → 3
```

> [!NOTE]
> 奇數鏈表的最後一個節點**無法配對**，因此維持在原有位置不動。

---

## 快速開始

### 環境需求

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### 執行程式

```bash
# 建構專案
dotnet build leetcode_024/leetcode_024.csproj

# 執行並查看測試輸出
dotnet run --project leetcode_024/leetcode_024.csproj
```

### 預期輸出

```
Test 1 Input:  [1 -> 2 -> 3 -> 4]
Test 1 Output: [2 -> 1 -> 4 -> 3]

Test 2 Input:  []
Test 2 Output: []

Test 3 Input:  [1]
Test 3 Output: [1]

Test 4 Input:  [1 -> 2 -> 3]
Test 4 Output: [2 -> 1 -> 3]
```
