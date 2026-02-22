# LeetCode 868 — Binary Gap（二進位間距）

> **難度**：Easy &nbsp;|&nbsp; **分類**：Bit Manipulation &nbsp;|&nbsp; **語言**：C# / .NET 10

---

## 題目說明

給定一個正整數 `n`，找出並回傳其**二進位表示**中，任意兩個「相鄰的 1」之間的**最長距離**。  
若不存在兩個相鄰的 1，則回傳 `0`。

### 定義

- **相鄰的 1**：兩個 1 之間只有 0（或沒有 0）相隔，即在二進位表示中找不到另一個 1 出現在它們中間。
- **距離**：兩個 1 所在**位元位置**（bit position）的絕對差。

### 範例

| 輸入 `n` | 二進位表示 | 相鄰 1 的位置對 | 最大距離 |
|---|---|---|---|
| `22` | `10110` | (1,2)→1, (2,4)→2 | **2** |
| `5`  | `101`   | (0,2)→2            | **2** |
| `6`  | `110`   | (1,2)→1            | **1** |
| `8`  | `1000`  | 只有一個 1          | **0** |

---

## 解題概念與出發點

### 核心觀察

把整數 `n` 拆解成二進位後，問題轉化為：

> 在一個 0/1 序列中，找出所有 **1 出現的位置**，計算相鄰兩個位置之差，求最大值。

### 為何使用位元運算？

直接對整數 `n` 施加位元運算，可以避免將其轉換為字串或陣列，達到 **O(log n) 時間、O(1) 空間**的最佳效能：

- `n & 1`：取得 `n` 目前最低位（bit 0）是 0 還是 1。
- `n >>= 1`：將 `n` 右移一位，下一次迴圈便處理原本的 bit 1，依此類推。

---

## 解法詳解：位元運算掃描（Bit Manipulation Scan）

### 演算法步驟

```
1. 初始化變數：
   - last = -1   // 記錄「上一個找到的 1」的位元索引；-1 表示尚未找到
   - res  = 0    // 記錄目前已知的最大相鄰距離

2. 以迴圈從 bit 0 到最高位逐位掃描（直到 n 變為 0）：
   a. 若 (n & 1) == 1，表示當前 bit i 為 1：
      - 若 last != -1，計算距離 dist = i - last，更新 res = max(res, dist)
      - 令 last = i
   b. 將 n 右移一位：n >>= 1
   c. i 遞增 1

3. 回傳 res
```

### C# 程式碼

```csharp
public int BinaryGap(int n)
{
    int last = -1;  // 上一個 1 的位元索引
    int res  = 0;   // 最大相鄰距離

    for (int i = 0; n != 0; i++)
    {
        if ((n & 1) == 1)               // 當前最低位為 1
        {
            if (last != -1)
                res = Math.Max(res, i - last);  // 更新最大距離
            last = i;                   // 更新上一個 1 的位置
        }
        n >>= 1;                        // 右移，處理下一個位元
    }

    return res;
}
```

### 複雜度分析

| 複雜度 | 說明 |
|---|---|
| 時間 O(log n) | `n` 最多有 ⌊log₂ n⌋ + 1 個位元需要掃描 |
| 空間 O(1) | 僅使用 `last`、`res`、`i` 三個整數變數 |

---

## 逐步演示（以 n = 22 為例）

`22` 的二進位表示為 **`10110`**，位元分佈如下（從低位 bit 0 到高位 bit 4）：

```
bit index:  4  3  2  1  0
bit value:  1  0  1  1  0
```

| 迴圈次數 | `i` | `n & 1` | 動作 | `last` | `res` |
|---|---|---|---|---|---|
| 初始 | — | — | 初始化 | -1 | 0 |
| 第 1 次 | 0 | 0 | 非 1，跳過 | -1 | 0 |
| 第 2 次 | 1 | 1 | 找到 1，`last == -1` 不計算距離，令 `last = 1` | 1 | 0 |
| 第 3 次 | 2 | 1 | 找到 1，距離 = 2 - 1 = **1**，`res = max(0,1) = 1`，令 `last = 2` | 2 | 1 |
| 第 4 次 | 3 | 0 | 非 1，跳過 | 2 | 1 |
| 第 5 次 | 4 | 1 | 找到 1，距離 = 4 - 2 = **2**，`res = max(1,2) = 2`，令 `last = 4` | 4 | 2 |
| n 變為 0 | — | — | 迴圈結束 | — | — |

**最終回傳 `res = 2`** ✓

---

## 執行測試

```bash
dotnet run --project leetcode_868/leetcode_868.csproj
```

預期輸出：

```
Input: 22 (二進位: 10110)
Output: 2

Input: 5 (二進位: 101)
Output: 2

Input: 6 (二進位: 110)
Output: 1

Input: 8 (二進位: 1000)
Output: 0
```

---

## 相關題目

- [LeetCode 201 - Bitwise AND of Numbers Range](https://leetcode.com/problems/bitwise-and-of-numbers-range/)
- [LeetCode 191 - Number of 1 Bits](https://leetcode.com/problems/number-of-1-bits/)
- [LeetCode 338 - Counting Bits](https://leetcode.com/problems/counting-bits/)

---

> **題目連結**：[LeetCode 868](https://leetcode.com/problems/binary-gap/) &nbsp;|&nbsp; [力扣 868](https://leetcode.cn/problems/binary-gap/)
