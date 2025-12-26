# LeetCode 61. Rotate List (旋轉鏈結串列)

[![LeetCode](https://img.shields.io/badge/LeetCode-61-orange?style=flat-square&logo=leetcode)](https://leetcode.com/problems/rotate-list/)
[![Difficulty](https://img.shields.io/badge/Difficulty-Medium-yellow?style=flat-square)](https://leetcode.com/problems/rotate-list/)
[![Language](https://img.shields.io/badge/Language-C%23-blue?style=flat-square&logo=csharp)](https://docs.microsoft.com/dotnet/csharp/)
[![.NET](https://img.shields.io/badge/.NET-10.0-512bd4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)

## 題目描述

給定一個鏈結串列的頭節點 `head`，將鏈結串列向右旋轉 `k` 個位置。

### 範例

**範例 1:**
```
輸入: head = [1,2,3,4,5], k = 2
輸出: [4,5,1,2,3]
```

```
原始:     1 -> 2 -> 3 -> 4 -> 5
旋轉 1 次: 5 -> 1 -> 2 -> 3 -> 4
旋轉 2 次: 4 -> 5 -> 1 -> 2 -> 3
```

**範例 2:**
```
輸入: head = [0,1,2], k = 4
輸出: [2,0,1]
```

```
原始:     0 -> 1 -> 2
旋轉 1 次: 2 -> 0 -> 1
旋轉 2 次: 1 -> 2 -> 0
旋轉 3 次: 0 -> 1 -> 2  (回到原始)
旋轉 4 次: 2 -> 0 -> 1  (等同旋轉 1 次)
```

### 限制條件

- 鏈結串列中節點的數目範圍是 `[0, 500]`
- `-100 <= Node.val <= 100`
- `0 <= k <= 2 * 10^9`

---

## 解題思路

### 關鍵觀察

1. **旋轉的本質**: 向右旋轉 `k` 個位置，等於將鏈結串列最後 `k` 個節點移動到最前面。

2. **週期性**: 如果 `k` 大於鏈結串列長度 `n`，實際上只需要旋轉 `k % n` 次，因為旋轉 `n` 次後會回到原位。

3. **新的頭節點**: 旋轉後的新頭節點是原本的倒數第 `k` 個節點。

### 解題策略：環形鏈結串列法

核心想法是將鏈結串列首尾相連形成環，然後在適當位置斷開：

```
步驟 1: 計算長度並找到尾節點
步驟 2: 將尾節點連接到頭節點，形成環
步驟 3: 找到新的斷開位置
步驟 4: 在該位置斷開環，得到結果
```

---

## 演算法詳解

### 步驟分解

以 `head = [1,2,3,4,5]`, `k = 2` 為例：

#### 步驟 1: 計算鏈結串列長度

```
1 -> 2 -> 3 -> 4 -> 5 -> null
                    ↑
                   tail
length = 5
```

#### 步驟 2: 形成環形鏈結串列

將尾節點的 `next` 指向頭節點：

```
┌─────────────────────────┐
↓                         │
1 -> 2 -> 3 -> 4 -> 5 ────┘
```

#### 步驟 3: 計算新的斷開位置

- 向右旋轉 `k = 2` 位
- 新的尾節點位置 = `length - k % length - 1 = 5 - 2 - 1 = 2`
- 從頭開始走 2 步，到達節點 `3`

```
┌─────────────────────────┐
↓                         │
1 -> 2 -> 3 -> 4 -> 5 ────┘
          ↑
       newTail (索引 2)
```

#### 步驟 4: 斷開環並設定新頭節點

- 新頭節點 = `newTail.next` = 節點 `4`
- 將 `newTail.next` 設為 `null`

```
4 -> 5 -> 1 -> 2 -> 3 -> null
```

---

## 程式碼

```csharp
public ListNode? RotateRight(ListNode? head, int k)
{
    // 邊界條件：空鏈結串列或只有一個節點時，直接返回
    if (head is null || head.next is null)
    {
        return head;
    }

    // 步驟 1: 計算鏈結串列長度，並找到尾節點
    ListNode tail = head;
    int length = 1;
    while (tail.next is not null)
    {
        tail = tail.next;
        length++;
    }

    // 步驟 2: 將尾節點連接到頭節點，形成環形鏈結串列
    tail.next = head;

    // 步驟 3: 計算新的斷開位置
    int stepsToNewTail = length - k % length - 1;

    // 步驟 4: 找到新的尾節點位置
    ListNode newTail = head;
    while (stepsToNewTail > 0)
    {
        newTail = newTail.next!;
        stepsToNewTail--;
    }

    // 步驟 5: 斷開環，設定新的頭節點
    ListNode newHead = newTail.next!;
    newTail.next = null;

    return newHead;
}
```

---

## 複雜度分析

| 複雜度 | 值 | 說明 |
|--------|-----|------|
| **時間複雜度** | O(n) | 遍歷鏈結串列兩次：一次計算長度，一次找到新尾節點 |
| **空間複雜度** | O(1) | 只使用常數個指標變數 |

---

## 執行範例

### 輸入
```
head = [1, 2, 3, 4, 5], k = 2
```

### 執行過程

| 步驟 | 操作 | 狀態 |
|------|------|------|
| 初始 | - | `1 -> 2 -> 3 -> 4 -> 5` |
| 1 | 計算長度 | `length = 5`, `tail = 5` |
| 2 | 形成環 | `5.next = 1` (環形) |
| 3 | 計算步數 | `stepsToNewTail = 5 - 2 - 1 = 2` |
| 4 | 找新尾節點 | 從 1 走 2 步 → `newTail = 3` |
| 5 | 斷開環 | `newHead = 4`, `3.next = null` |

### 輸出
```
[4, 5, 1, 2, 3]
```

---

## 邊界情況處理

| 情況 | 輸入 | 輸出 | 說明 |
|------|------|------|------|
| 空鏈結串列 | `head = null, k = 1` | `null` | 直接返回 |
| 單節點 | `head = [1], k = 99` | `[1]` | 無論旋轉幾次都一樣 |
| k = 0 | `head = [1,2,3], k = 0` | `[1,2,3]` | 不旋轉 |
| k = 鏈結串列長度 | `head = [1,2,3], k = 3` | `[1,2,3]` | 旋轉一圈回原位 |
| k > 鏈結串列長度 | `head = [1,2,3], k = 4` | `[3,1,2]` | 等同 k % 3 = 1 |

---

## 執行程式

```bash
cd leetcode_061
dotnet run
```

---

## 相關題目

- [LeetCode 189. Rotate Array](https://leetcode.com/problems/rotate-array/) - 陣列旋轉
- [LeetCode 25. Reverse Nodes in k-Group](https://leetcode.com/problems/reverse-nodes-in-k-group/) - k 個一組翻轉
- [LeetCode 92. Reverse Linked List II](https://leetcode.com/problems/reverse-linked-list-ii/) - 區間翻轉

---

## 參考資源

- [LeetCode 61. Rotate List](https://leetcode.com/problems/rotate-list/)
- [LeetCode 61. 旋轉鏈表 (中文)](https://leetcode.cn/problems/rotate-list/)
