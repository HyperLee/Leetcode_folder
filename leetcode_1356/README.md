# LeetCode 1356 — Sort Integers by The Number of 1 Bits

> 依二進制 1-bit 數量排序整數陣列，同 bit 數者按數值升序排列。

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-14-239120?logo=csharp)](https://learn.microsoft.com/zh-tw/dotnet/csharp/)
[![LeetCode](https://img.shields.io/badge/LeetCode-1356-FFA116?logo=leetcode)](https://leetcode.com/problems/sort-integers-by-the-number-of-1-bits/)

---

## 題目說明

- **題目連結**：[LeetCode 1356](https://leetcode.com/problems/sort-integers-by-the-number-of-1-bits/) / [中文版](https://leetcode.cn/problems/sort-integers-by-the-number-of-1-bits/)
- **難度**：Easy

給定一個整數陣列 `arr`，請依照以下規則排序後回傳：

1. **主鍵**：每個整數在二進制中 `1` 的個數（Popcount / Hamming Weight），由少到多排列。
2. **次鍵**：若兩個整數的 `1` 的個數相同，則依**數值大小升序**排列。

**範例 1**

```
Input:  [0, 1, 2, 3, 4, 5, 6, 7, 8]
Output: [0, 1, 2, 4, 8, 3, 5, 6, 7]
```

**範例 2**

```
Input:  [1024, 512, 256, 128, 64, 32, 16, 8, 4, 2, 1]
Output: [1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024]
```

> [!NOTE]
> 題目保證 `0 <= arr[i] <= 10^4`，且 `1 <= arr.length <= 500`，輸入均為非負整數。

---

## 解題概念與出發點

### 核心問題拆解

此題可分兩個子問題：

| 子問題 | 說明 |
|--------|------|
| **計算 Popcount** | 計算一個整數二進制中 `1` 的位元個數 |
| **自訂排序** | 以 Popcount 為主鍵、數值為次鍵排序 |

### 排序策略選擇

本解法採用「**先升序 → 再分桶**」的兩步驟策略，利用 `SortedDictionary` 的自動 Key 排序特性，避免撰寫複雜的自訂比較器：

```
步驟 1: Array.Sort(arr)          ← 先整體升序，保證同桶內數值已排好
步驟 2: 逐一計算 Popcount → 放入對應桶
步驟 3: 依 Key（Popcount）順序收集結果
```

這種做法邏輯直觀、程式碼可讀性高，且時間複雜度為 **O(n log n)**。

---

## 解法詳細說明

### `SortByBits` — 主排序函式

```
SortedDictionary<int, List<int>>
  key   → 二進制中 1 的個數（Popcount）
  value → 擁有該 Popcount 的整數列表（插入時已升序）
```

```csharp
public int[] SortByBits(int[] arr)
{
    Array.Sort(arr);  // 先升序，確保同桶內已排好

    var dict = new SortedDictionary<int, List<int>>();

    for(int i = 0; i < arr.Length; i++)
    {
        int key = PopCount2(arr[i]);  // 計算 Popcount 作為桶索引

        if(dict.ContainsKey(key))
            dict[key].Add(arr[i]);
        else
            dict.Add(key, new List<int>() { arr[i] });
    }

    var ret = new int[arr.Length];
    int idx = 0;

    foreach(var kvp in dict)        // SortedDictionary 保證 key 升序
        foreach(var num in kvp.Value)
            ret[idx++] = num;

    return ret;
}
```

---

### `PopCount` — Brian Kernighan's Bit Trick

**核心觀察**：`n & (n - 1)` 每次執行都會清除 `n` 中最低的有效位元（lowest set bit），因此執行次數恰好等於 1-bit 的個數。

| 步驟 | n（二進制） | n - 1（二進制） | n & (n-1) |
|------|-------------|-----------------|-----------|
| 初始 | `1100`（12）| `1011`（11）    | `1000`（8）|
| 第二次 | `1000`（8）| `0111`（7）   | `0000`（0）|
| 結束 | —           | —               | 共 2 次 → 含 2 個 `1` |

```csharp
public static int PopCount(int n)
{
    int counter = 0;
    while(n > 0)
    {
        counter++;
        n = n & (n - 1);  // 清除最低有效位元
    }
    return counter;
}
```

- **時間複雜度**：O(k)，k 為 1-bit 個數（最快路徑）
- **空間複雜度**：O(1)

---

### `PopCount2` — 逐位相除法

對 2 連續取餘數（`n % 2`）與整除（`n /= 2`），模擬手動轉二進制的流程，每個最低位元為 `1` 時累加計數器。

| 步驟 | n | n % 2 | res |
|------|---|-------|-----|
| 初始 | 6 (110₂) | — | 0 |
| 第 1 次 | 6 | 0 | 0 |
| 第 2 次 | 3 | 1 | 1 |
| 第 3 次 | 1 | 1 | 2 |
| 結束 | 0 | — | **2** |

```csharp
public static int PopCount2(int n)
{
    int res = 0;
    while(n != 0)
    {
        res += (n % 2);  // 最低位元為 1 則累加
        n /= 2;          // 右移一位（等同 n >>= 1）
    }
    return res;
}
```

- **時間複雜度**：O(log n)
- **空間複雜度**：O(1)

---

## 完整流程演示

以 `arr = [0, 1, 2, 3, 4, 5, 6, 7, 8]` 為例：

**步驟 1 — 先升序排序**

```
[0, 1, 2, 3, 4, 5, 6, 7, 8]  ← 已是升序，不變
```

**步驟 2 — 計算每個數的 Popcount**

| 數值 | 二進制 | Popcount |
|------|--------|----------|
| 0    | `0000` | 0 |
| 1    | `0001` | 1 |
| 2    | `0010` | 1 |
| 3    | `0011` | 2 |
| 4    | `0100` | 1 |
| 5    | `0101` | 2 |
| 6    | `0110` | 2 |
| 7    | `0111` | 3 |
| 8    | `1000` | 1 |

**步驟 3 — 放入 SortedDictionary 分桶**

```
Bucket[0] → [0]
Bucket[1] → [1, 2, 4, 8]
Bucket[2] → [3, 5, 6]
Bucket[3] → [7]
```

**步驟 4 — 依 Key 升序收集結果**

```
Bucket[0]: 0
Bucket[1]: 1, 2, 4, 8
Bucket[2]: 3, 5, 6
Bucket[3]: 7

Output: [0, 1, 2, 4, 8, 3, 5, 6, 7]  ✓
```

---

## 執行方式

```bash
cd leetcode_1356
dotnet run
```

預期輸出：

```
測試 1: 0, 1, 2, 4, 8, 3, 5, 6, 7
測試 2: 1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024
PopCount(7)  = 3
PopCount(10) = 2
PopCount2(7)  = 3
PopCount2(10) = 2
```
