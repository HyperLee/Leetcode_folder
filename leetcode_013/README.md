# LeetCode 13 - Roman to Integer

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-console-239120?style=flat-square&logo=csharp&logoColor=white)
![LeetCode](https://img.shields.io/badge/LeetCode-13%20Roman%20to%20Integer-FFA116?style=flat-square&logo=leetcode&logoColor=black)
![Status](https://img.shields.io/badge/status-solved-brightgreen?style=flat-square)

使用 C# 實作 LeetCode 13「Roman to Integer」，透過一次由左到右的掃描，將羅馬數字轉換成整數。

[題目](#題目) • [解法重點](#解法重點) • [執行方式](#執行方式) • [範例](#範例) • [專案結構](#專案結構)

## Overview

這個專案是一個精簡的 .NET Console App，示範兩種羅馬數字轉整數的寫法：

- `RomanToInt`：在方法內建立符號對應表。
- `RomanToInt2`：使用共用欄位保存符號對應表，避免多次呼叫時重複建立字典。

兩種解法的核心判斷相同，差別在於羅馬符號對應表的放置位置。

## 題目

羅馬數字由 7 種符號組成：

| 符號 | 數值 |
| --- | ---: |
| `I` | 1 |
| `V` | 5 |
| `X` | 10 |
| `L` | 50 |
| `C` | 100 |
| `D` | 500 |
| `M` | 1000 |

一般情況下，羅馬數字會由大到小、從左到右排列，因此可以直接相加，例如 `III = 3`、`XII = 12`。

但當較小的符號出現在較大的符號左邊時，要套用減法規則：

| 組合 | 數值 | 說明 |
| --- | ---: | --- |
| `IV` | 4 | `5 - 1` |
| `IX` | 9 | `10 - 1` |
| `XL` | 40 | `50 - 10` |
| `XC` | 90 | `100 - 10` |
| `CD` | 400 | `500 - 100` |
| `CM` | 900 | `1000 - 100` |

> [!NOTE]
> LeetCode 題目會提供有效的羅馬數字字串，因此目前程式專注在轉換邏輯，沒有額外處理非法字元或無效格式。

## 解法重點

從左到右掃描字串，每次比較目前符號與下一個符號的數值：

```text
目前值 < 下一個值：扣掉目前值
目前值 >= 下一個值：加上目前值
目前符號是最後一位：加上目前值
```

這個規則可以同時處理一般加法與 `IV`、`IX`、`MCMXCIV` 這類含有減法組合的輸入。

## Solutions

| 方法 | 說明 | 適合情境 |
| --- | --- | --- |
| `RomanToInt` | 每次呼叫時在方法內建立 `Dictionary<char, int>` | 展示完整解法流程，容易單獨閱讀 |
| `RomanToInt2` | 將符號對應表放在 `symbolValues` 欄位 | 同一個物件多次呼叫時可重複使用對應表 |

## 執行方式

需要安裝 [.NET SDK 10](https://dotnet.microsoft.com/download/dotnet/10.0) 或相容版本。

```powershell
dotnet run --project .\leetcode_013\leetcode_013.csproj
```

## 範例

`Program.cs` 目前內建以下測試資料，會同時執行兩種解法並輸出是否通過：

| 輸入 | 預期輸出 | 涵蓋情境 |
| --- | ---: | --- |
| `III` | 3 | 連續加法 |
| `IV` | 4 | `I` 在 `V` 前面的減法 |
| `IX` | 9 | `I` 在 `X` 前面的減法 |
| `LVIII` | 58 | 多個一般加法 |
| `MCMXCIV` | 1994 | 多組減法與加法混合 |

以 `MCMXCIV` 為例：

| 位置 | 符號 | 判斷 | 累計 |
| ---: | --- | --- | ---: |
| 0 | `M` | `1000 >= 100`，加上 1000 | 1000 |
| 1 | `C` | `100 < 1000`，扣掉 100 | 900 |
| 2 | `M` | `1000 >= 10`，加上 1000 | 1900 |
| 3 | `X` | `10 < 100`，扣掉 10 | 1890 |
| 4 | `C` | `100 >= 1`，加上 100 | 1990 |
| 5 | `I` | `1 < 5`，扣掉 1 | 1989 |
| 6 | `V` | 最後一位，加上 5 | 1994 |

## Complexity

| 方法 | 時間複雜度 | 空間複雜度 |
| --- | --- | --- |
| `RomanToInt` | `O(n)` | `O(1)` |
| `RomanToInt2` | `O(n)` | `O(1)` |

`n` 是羅馬數字字串長度；符號對應表固定只有 7 個符號，因此空間使用量為常數。

## 專案結構

```text
.
├── README.md
├── leetcode_013.slnx
└── leetcode_013
    ├── Program.cs
    └── leetcode_013.csproj
```
