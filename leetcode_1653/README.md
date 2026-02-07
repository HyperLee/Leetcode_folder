# LeetCode 1653: Minimum Deletions to Make String Balanced

> 💡 一個關於字串平衡操作的中等難度演算法問題，提供兩種高效解法。

## 題目描述

給定一個只包含字元 `'a'` 和 `'b'` 的字串 `s`。你可以刪除 `s` 中任意數量的字元，使 `s` 成為**平衡字串**。

**平衡字串的定義：**
當不存在索引對 `(i, j)` 滿足以下條件時，字串 `s` 即為平衡：
- `i < j`
- `s[i] = 'b'`
- `s[j] = 'a'`

**簡單來說：**字串中不能出現 `'b'` 在 `'a'` 前面的情況。

**目標：**回傳使 `s` 成為平衡字串所需的最少刪除數。

### 範例

**範例 1:**
```
Input: s = "aababbab"
Output: 2
說明: 可以刪除索引 2 和 6 的兩個 'a'，得到 "ababbb"
```

**範例 2:**
```
Input: s = "bbaaaaabb"
Output: 2
說明: 可以刪除索引 0 和 1 的兩個 'b'，得到 "aaaaabb"
```

### 約束條件

- `1 <= s.length <= 10^5`
- `s[i]` 只能是 `'a'` 或 `'b'`

## 解題概念

### 平衡字串的三種形式

一個平衡的字串可以是以下三種形式之一：

1. **全部是 'a'**：將所有 `'b'` 刪除
2. **全部是 'b'**：將所有 `'a'` 刪除  
3. **混合模式**：`'a'` 在前，`'b'` 在後（如 `"aaa...bbb"`）

### 核心思路：分隔線概念

想像在字串中的任意位置劃一條**分隔線**：
```
s = "b a b a"
     ↓
    b | a b a
    ↑   ↑
  leftb righta
```

- **分隔線左邊**應該全是 `'a'`，所以左邊的 `'b'` 需要刪除
- **分隔線右邊**應該全是 `'b'`，所以右邊的 `'a'` 需要刪除
- **刪除次數** = `leftb + righta`

**關鍵觀察：**
- 只需要刪除 `'a'` 或 `'b'` 其中一種，不需要同時刪除
- 通過移動分隔線找到最小的刪除次數

## 解法詳解

### 解法一：分隔線掃描法 (Two-Pass)

#### 演算法步驟

1. **第一次遍歷**：計算字串中總共有多少個 `'a'`（初始化 `righta`）
2. **第二次遍歷**：在每個位置嘗試放置分隔線
   - 遇到 `'a'`：`righta--`（這個 'a' 移到分隔線左邊了，不需要刪除）
   - 遇到 `'b'`：`leftb++`（這個 'b' 在分隔線左邊，可能需要刪除）
   - 記錄最小的 `leftb + righta`

#### 複雜度分析

- **時間複雜度**：O(n) - 兩次遍歷
- **空間複雜度**：O(1) - 只使用常數空間

#### 程式碼實作

```csharp
public int MinimumDeletions(string s)
{
    int leftb = 0;   // 分隔線左邊的 'b' 數量
    int righta = 0;  // 分隔線右邊的 'a' 數量

    // 第一次遍歷：計算總 'a' 數
    foreach (char c in s)
    {
        if (c == 'a') righta++;
    }

    int minDeletions = leftb + righta;

    // 第二次遍歷：移動分隔線
    foreach (char c in s)
    {
        if (c == 'a')
            righta--;  // 'a' 移到左邊
        else
            leftb++;   // 'b' 在左邊

        minDeletions = Math.Min(minDeletions, leftb + righta);
    }

    return minDeletions;
}
```

### 解法二：貪心演算法 (Greedy One-Pass) ⭐ 推薦

#### 核心思想

當遇到「`'b'` 後面跟著 `'a'`」的不平衡情況時，有兩種選擇：
1. 刪除當前的 `'a'`
2. 刪除之前遇到的某個 `'b'`

**貪心策略**：選擇對後續影響最小的方案。

#### 演算法邏輯

- `leftb`：記錄目前遇到的 `'b'` 字元數量（可能需要刪除的 'b' 候選）
- `res`：記錄需要刪除的次數

遍歷字串時：
1. 遇到 `'b'`：`leftb++`（累積潛在需要刪除的 'b'）
2. 遇到 `'a'`：
   - 如果前面有 `'b'`（`leftb > 0`），表示出現不平衡
   - 刪除一個字元並將 `leftb--`（減少未來不平衡的可能）
   - `res++`（累計刪除次數）

#### 複雜度分析

- **時間複雜度**：O(n) - 一次遍歷
- **空間複雜度**：O(1) - 只使用常數空間

#### 程式碼實作

```csharp
public static int MinimumDeletions2(string s)
{
    int leftb = 0;  // 目前遇到的 'b' 數量
    int res = 0;    // 累計刪除次數

    foreach (char c in s) 
    {
        if (c == 'a')
        {
            // 遇到 'a' 且前面有 'b'，出現不平衡
            if (leftb > 0)
            {
                res++;      // 累計刪除次數
                leftb--;    // 減少一個 'b' 的計數
            }
        }
        else
        {
            leftb++;  // 累計 'b' 的數量
        }
    }

    return res;
}
```

## 範例演示

### 範例 1：`s = "aababbab"`

#### 使用解法一（分隔線掃描法）

```
初始狀態: righta = 4 (總共 4 個 'a')

遍歷過程：
位置 0 'a': righta=3, leftb=0 → minDel = min(4, 3) = 3
位置 1 'a': righta=2, leftb=0 → minDel = min(3, 2) = 2
位置 2 'b': righta=2, leftb=1 → minDel = min(2, 3) = 2
位置 3 'a': righta=1, leftb=1 → minDel = min(2, 2) = 2 ✓
位置 4 'b': righta=1, leftb=2 → minDel = min(2, 3) = 2
位置 5 'b': righta=1, leftb=3 → minDel = min(2, 4) = 2
位置 6 'a': righta=0, leftb=3 → minDel = min(2, 3) = 2
位置 7 'b': righta=0, leftb=4 → minDel = min(2, 4) = 2

結果: 2
```

#### 使用解法二（貪心演算法）

```
遍歷過程：
位置 0 'a': leftb=0 → 無操作
位置 1 'a': leftb=0 → 無操作
位置 2 'b': leftb=1
位置 3 'a': leftb=1>0 → res=1, leftb=0 (刪除一個)
位置 4 'b': leftb=1
位置 5 'b': leftb=2
位置 6 'a': leftb=2>0 → res=2, leftb=1 (刪除一個)
位置 7 'b': leftb=2

結果: 2
```

### 範例 2：`s = "bbaaaaabb"`

#### 使用解法二（貪心演算法）

```
遍歷過程：
位置 0 'b': leftb=1
位置 1 'b': leftb=2
位置 2 'a': leftb=2>0 → res=1, leftb=1 (刪除一個)
位置 3 'a': leftb=1>0 → res=2, leftb=0 (刪除一個)
位置 4 'a': leftb=0 → 無操作
位置 5 'a': leftb=0 → 無操作
位置 6 'a': leftb=0 → 無操作
位置 7 'b': leftb=1
位置 8 'b': leftb=2

結果: 2
```

## 執行專案

### 前置需求

- .NET 10.0 或更高版本

### 建構與執行

```bash
# 建構專案
dotnet build

# 執行程式
dotnet run
```

### 預期輸出

```
=== LeetCode 1653: Minimum Deletions to Make String Balanced ===

測試案例 1: "aababbab"
  MinimumDeletions:  2
  MinimumDeletions2: 2

測試案例 2: "bbaaaaabb"
  MinimumDeletions:  2
  MinimumDeletions2: 2

測試案例 3: "aaa"
  MinimumDeletions:  0
  MinimumDeletions2: 0

測試案例 4: "bbb"
  MinimumDeletions:  0
  MinimumDeletions2: 0

...
```

## 比較與總結

| 特性 | 解法一（分隔線掃描） | 解法二（貪心） |
|------|---------------------|----------------|
| 時間複雜度 | O(n) - 兩次遍歷 | O(n) - 一次遍歷 |
| 空間複雜度 | O(1) | O(1) |
| 程式碼簡潔度 | 中等 | 較簡潔 |
| 理解難度 | 較易理解 | 需理解貪心思想 |
| **推薦度** | ⭐⭐⭐ | ⭐⭐⭐⭐⭐ |

### 建議

- **學習階段**：先理解解法一的分隔線概念，建立直觀理解
- **實戰應用**：使用解法二，效率更高且程式碼更簡潔
- **面試情境**：可以先提出解法一，再優化到解法二，展現思考過程

## 參考資料

- [LeetCode 1653 題目連結](https://leetcode.com/problems/minimum-deletions-to-make-string-balanced/)
- [LeetCode CN 1653 題目連結](https://leetcode.cn/problems/minimum-deletions-to-make-string-balanced/)

## 授權

本專案採用 MIT 授權條款。
