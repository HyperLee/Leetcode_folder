<div align="center">

# LeetCode 1318
## Minimum Flips to Make a OR b Equal to c

[![.NET](https://img.shields.io/badge/.NET-10-512BD4?style=flat-square&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C%23](https://img.shields.io/badge/C%23-14-239120?style=flat-square&logo=csharp&logoColor=white)](https://learn.microsoft.com/dotnet/csharp/)
[![LeetCode](https://img.shields.io/badge/LeetCode-1318-f89f1b?style=flat-square&logo=leetcode&logoColor=white)](https://leetcode.com/problems/minimum-flips-to-make-a-or-b-equal-to-c/)

[題目重點](#題目重點) • [解題概念](#解題概念) • [解法說明](#解法說明) • [流程演示](#流程演示) • [複雜度](#複雜度) • [執行方式](#執行方式)

</div>

這個專案使用 .NET 10 與 C# 實作 LeetCode 第 1318 題，重點在於把條件 `(a OR b) == c` 轉成逐位元判斷問題，直接計算最少翻轉次數，而不是實際模擬所有翻轉組合。

> [!NOTE]
> 本題的關鍵是「每一個二進位位元彼此獨立」。只要能算出每一位最少需要翻幾次，整體答案就是這些最小值的總和。

## 題目重點

給定三個正整數 `a`、`b`、`c`，你可以翻轉 `a` 或 `b` 任一位元上的值：

- `0 -> 1`
- `1 -> 0`

目標是讓：

```text
(a OR b) == c
```

你需要回傳最少翻轉次數。

換句話說，對於每一個位元位置，都必須讓：

```text
aBit OR bBit == cBit
```

只要某一位不符合，就必須在該位做必要的翻轉。

## 解題概念

解題出發點有兩個：

1. OR 運算可以逐位獨立判斷，不同位元之間互不影響。
2. 每一位只要知道 `aBit`、`bBit`、`cBit`，就能立即得出該位的最少翻轉次數。

當前位元只會有兩種需要處理的失配情況：

| 條件 | 最少翻轉次數 | 原因 |
| --- | --- | --- |
| `cBit = 1` 且 `aBit OR bBit = 0` | `1` | 只要把 `aBit` 或 `bBit` 其中一個翻成 `1` 即可 |
| `cBit = 0` 且 `aBit OR bBit = 1` | `aBit + bBit` | 該位所有的 `1` 都必須翻成 `0` |

這代表每一位的最佳解都是局部最優，而且不會影響其他位，因此把每一位的最小翻轉次數加總，就是全域最優解。

> [!TIP]
> 最容易漏掉的情況是 `cBit = 0`、`aBit = 1`、`bBit = 1`。這時候必須翻兩次，不是一次。

## 解法說明

實作時不需要先把整數轉成字串或陣列，只要透過位元運算逐步處理最低位即可。

1. 初始化答案 `flips = 0`。
2. 只要 `a`、`b`、`c` 其中任一個還大於 `0`，就持續迴圈。
3. 用 `& 1` 取出三個數目前的最低位：`aBit`、`bBit`、`cBit`。
4. 如果 `(aBit | bBit) == cBit`，代表該位已經符合條件，不需要翻轉。
5. 如果不相等：
   - `cBit == 1` 時，答案加 `1`。
   - `cBit == 0` 時，答案加 `aBit + bBit`。
6. 將 `a`、`b`、`c` 全部右移一位，處理下一個位元。
7. 當三個數都變成 `0` 時，代表所有有效位都處理完成，回傳答案。

核心實作位於 [leetcode_1318/Program.cs](./leetcode_1318/Program.cs)。

```csharp
public int MinFlips(int a, int b, int c)
{
    var flips = 0;

    while (a > 0 || b > 0 || c > 0)
    {
        var aBit = a & 1;
        var bBit = b & 1;
        var cBit = c & 1;

        if ((aBit | bBit) != cBit)
        {
            flips += cBit == 1 ? 1 : aBit + bBit;
        }

        a >>= 1;
        b >>= 1;
        c >>= 1;
    }

    return flips;
}
```

## 流程演示

以下用經典範例 `a = 2`、`b = 6`、`c = 5` 說明。

二進位表示如下：

```text
a = 0010
b = 0110
c = 0101
```

從最低位開始逐位檢查：

| 位元位置 | `aBit` | `bBit` | `cBit` | `aBit OR bBit` | 本位翻轉次數 | 累計 |
| --- | --- | --- | --- | --- | --- | --- |
| 0 | 0 | 0 | 1 | 0 | 1 | 1 |
| 1 | 1 | 1 | 0 | 1 | 2 | 3 |
| 2 | 0 | 1 | 1 | 1 | 0 | 3 |

流程解讀：

1. 第 0 位需要得到 `1`，但目前 `0 OR 0 = 0`，所以翻一次即可。
2. 第 1 位需要得到 `0`，但目前有兩個 `1`，因此必須翻兩次。
3. 第 2 位原本就符合 `0 OR 1 = 1`，不需處理。

最後得到最少翻轉次數為 `3`。

## 複雜度

- 時間複雜度：`O(log M)`，其中 `M` 是 `a`、`b`、`c` 的最大值。
- 空間複雜度：`O(1)`。

## 專案結構

```text
leetcode_1318.sln
leetcode_1318/
├── leetcode_1318.csproj
└── Program.cs
```

## 執行方式

在專案根目錄執行：

```bash
dotnet run --project leetcode_1318/leetcode_1318.csproj
```

主程式目前包含 4 組測試資料：

- `(2, 6, 5) -> 3`
- `(4, 2, 7) -> 1`
- `(1, 2, 3) -> 0`
- `(8, 3, 5) -> 3`

執行後會輸出每組測資的實際結果與預期值，方便快速檢查實作是否正確。