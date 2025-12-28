# LeetCode 1342. 將數字變成 0 的操作次數

[![LeetCode](https://img.shields.io/badge/LeetCode-1342-orange?style=flat-square&logo=leetcode)](https://leetcode.com/problems/number-of-steps-to-reduce-a-number-to-zero/)
[![Difficulty](https://img.shields.io/badge/難度-Easy-green?style=flat-square)](https://leetcode.com/problems/number-of-steps-to-reduce-a-number-to-zero/)
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)

## 題目描述

給定一個非負整數 `num`，回傳將其變為 `0` 所需的步數。

**規則：**
- 若目前數字為**偶數**，則除以 2
- 若目前數字為**奇數**，則減 1

### 範例

| 輸入 | 輸出 | 說明 |
|------|------|------|
| `num = 14` | `6` | 14 → 7 → 6 → 3 → 2 → 1 → 0 |
| `num = 8` | `4` | 8 → 4 → 2 → 1 → 0 |
| `num = 123` | `12` | 需要 12 步才能變成 0 |

### 限制條件

- `0 <= num <= 10^6`

## 解題概念與出發點

### 問題分析

這道題目的核心是**模擬**題目所描述的過程。我們需要根據數字的奇偶性來決定下一步操作：

1. **偶數**：可以被 2 整除，所以除以 2 會讓數字快速減半
2. **奇數**：減 1 後必定變成偶數

### 關鍵觀察

從二進位的角度來思考這個問題會更直觀：

- **偶數除以 2**：相當於二進位右移一位（去掉最右邊的 0）
- **奇數減 1**：相當於把二進位最右邊的 1 變成 0

> [!TIP]
> 這意味著每個二進位的 `1` 需要兩步操作（減 1 變成 0，然後右移），而每個 `0` 只需要一步（右移）。

## 解法詳解

### 方法一：迴圈模擬（本解法採用）

最直觀的方法是直接模擬題目描述的過程：

```csharp
public int NumberOfSteps(int num)
{
    int steps = 0;

    while (num > 0)
    {
        if (num % 2 == 0)
        {
            num /= 2;    // 偶數：除以 2
        }
        else
        {
            num -= 1;    // 奇數：減 1
        }
        steps++;
    }

    return steps;
}
```

**複雜度分析：**
- **時間複雜度**：O(log n) — 偶數時除以 2，數值會快速減少
- **空間複雜度**：O(1) — 只使用常數額外空間

### 方法二：數學（二進位性質）

從二進位角度分析，兩種操作的影響：

- **偶數除以 2**：相當於二進位整體右移一位
- **奇數減 1**：相當於消減最低位的 1

因此整個模擬過程其實是：不斷右移直到最低位為 1，然後消減最低位的 1，直到結果為 0。

**總操作次數 = 右移次數 + 消減次數**：

- **右移次數**：`num` 中最高位 1 所在的位置
- **消減次數**：`num` 中 1 的個數

由於最後一步右移和消減同時完成，需減 1。

```csharp
public int NumberOfStepsV2(int num)
{
    return Math.Max(GetHighestBitPosition(num) + GetBitCount(num) - 1, 0);
}

/// <summary>
/// 取得數字二進位表示中最高位 1 的位置（從 1 開始計數）。
/// </summary>
private static int GetHighestBitPosition(int x)
{
    for (int i = 31; i >= 0; i--)
    {
        if (((x >> i) & 1) == 1)
        {
            return i + 1;
        }
    }

    return 0;
}

/// <summary>
/// 計算數字二進位表示中 1 的個數（使用 lowbit 技巧）。
/// </summary>
private static int GetBitCount(int x)
{
    int count = 0;

    while (x != 0)
    {
        // lowbit: x & -x 取得最低位的 1
        x -= x & -x;
        count++;
    }

    return count;
}
```

**以 `num = 14` 為例：**

- 二進位：`1110`
- 最高位 1 在第 4 位 → 右移次數 = 4
- 有 3 個 1 → 消減次數 = 3
- 總步數 = 4 + 3 - 1 = **6**

**複雜度分析：**

- **時間複雜度**：O(log n) — 遍歷二進位位數
- **空間複雜度**：O(1) — 只使用常數額外空間

## 範例演示流程

以 `num = 14` 為例，逐步演示整個過程：

```text
初始值: 14 (二進位: 1110)
步驟 1: 14 是偶數 → 14 ÷ 2 = 7  (二進位: 0111)
步驟 2:  7 是奇數 →  7 - 1 = 6  (二進位: 0110)
步驟 3:  6 是偶數 →  6 ÷ 2 = 3  (二進位: 0011)
步驟 4:  3 是奇數 →  3 - 1 = 2  (二進位: 0010)
步驟 5:  2 是偶數 →  2 ÷ 2 = 1  (二進位: 0001)
步驟 6:  1 是奇數 →  1 - 1 = 0  (二進位: 0000)

總步數: 6
```

### 流程圖

```text
┌─────────────────────────────────────────────────────┐
│                    開始                              │
│                   num = 14                          │
└─────────────────────┬───────────────────────────────┘
                      ▼
┌─────────────────────────────────────────────────────┐
│                 num > 0 ?                           │
└─────────────────────┬───────────────────────────────┘
                      │ 是
                      ▼
┌─────────────────────────────────────────────────────┐
│              num 是偶數嗎?                           │
├──────────────┬──────────────────────────────────────┤
│     是       │              否                       │
│ num = num/2  │         num = num - 1                │
└──────────────┴──────────────────────────────────────┘
                      │
                      ▼
               steps = steps + 1
                      │
                      ▼
              （回到判斷 num > 0）
                      │
                      │ 否 (num = 0)
                      ▼
┌─────────────────────────────────────────────────────┐
│              回傳 steps                              │
└─────────────────────────────────────────────────────┘
```

## 執行方式

```bash
# 建構專案
dotnet build

# 執行程式
dotnet run --project leetcode_1342/leetcode_1342.csproj
```

## 相關題目

- [LeetCode 191. Number of 1 Bits](https://leetcode.com/problems/number-of-1-bits/)
- [LeetCode 338. Counting Bits](https://leetcode.com/problems/counting-bits/)

## 參考資料

- [LeetCode 題目連結 (英文)](https://leetcode.com/problems/number-of-steps-to-reduce-a-number-to-zero/)
- [力扣題目連結 (中文)](https://leetcode.cn/problems/number-of-steps-to-reduce-a-number-to-zero/)
