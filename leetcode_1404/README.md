# LeetCode 1404 — 將二進位表示減到 1 的步驟數

> **Number of Steps to Reduce a Number in Binary Representation to One**

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com)
[![Language](https://img.shields.io/badge/language-C%23-239120?logo=csharp)](https://learn.microsoft.com/dotnet/csharp/)
[![Difficulty](https://img.shields.io/badge/difficulty-Medium-orange)](https://leetcode.com/problems/number-of-steps-to-reduce-a-number-in-binary-representation-to-one/)

---

## 題目描述

給定一個整數的**二進位表示字串** `s`，依照以下規則計算將其化為 `"1"` 所需的**最少步驟數**：

1. 若當前數字為**偶數**，將其除以 2。
2. 若當前數字為**奇數**，將其加 1。

保證所有測試資料最終都能達到 1。

**來源**：[LeetCode 1404](https://leetcode.com/problems/number-of-steps-to-reduce-a-number-in-binary-representation-to-one/)

### 輸入 / 輸出範例

| 輸入 `s` | 輸出 | 說明 |
|----------|------|------|
| `"1101"` | `6`  | 見下方完整演示 |
| `"10"`   | `1`  | 2 → 1 |
| `"1"`    | `0`  | 已是 1，不需步驟 |
| `"1111"` | `5`  | 特殊進位情況 |

---

## 解題概念與出發點

### 關鍵觀察：直接在二進位字串上操作

與其將字串轉為整數再計算，可以直接對字串進行位元操作，避免大數溢位問題：

| 數學操作 | 對應二進位字串操作 |
|--------|-----------------|
| 偶數 ÷ 2 | 移除字串最右側的 `'0'` |
| 奇數 + 1 | 從最低位進行二進位加法（往高位傳遞進位）|

### 奇數加 1 的細節

奇數的最低位一定是 `'1'`，加 1 後會發生進位傳遞：

- 從最低位（最右端）往最高位掃描。
- 遇到 `'1'` → 該位歸零（`'1'` → `'0'`），進位繼續向左傳遞。
- 遇到 `'0'` → 該位設 1（`'0'` → `'1'`），進位停止。
- **特殊情況**：若整串全為 `'1'`（例如 `"1111"`），進位會超出最高位，需在字串前端插入 `'1'`（例如 `"1111"` → `"10000"`）。

---

## 解法說明

### 方法：模擬（Simulation）

直接依照題意逐步執行，以 `StringBuilder` 代表當前的二進位數值：

```csharp
public int NumSteps(string s)
{
    int steps = 0;
    StringBuilder sb = new StringBuilder(s);

    while (sb.ToString() != "1")
    {
        steps++;

        if (sb[sb.Length - 1] == '0')
        {
            // 偶數：截去末尾 '0'（等同除以 2）
            sb.Length--;
        }
        else
        {
            // 奇數：從最低位往高位模擬二進位加 1
            for (int i = sb.Length - 1; i >= 0; i--)
            {
                if (sb[i] == '1')
                {
                    sb[i] = '0'; // 歸零，進位繼續向左
                    if (i == 0)
                    {
                        sb.Insert(0, '1'); // 全為 1 的特殊進位
                        break;
                    }
                }
                else
                {
                    sb[i] = '1'; // 接收進位，停止傳遞
                    break;
                }
            }
        }
    }

    return steps;
}
```

### 複雜度分析

| 面向 | 複雜度 | 說明 |
|------|--------|------|
| 時間 | O(n²)  | n 為字串長度；每次奇數操作最多掃描整串 |
| 空間 | O(n)   | `StringBuilder` 額外佔用字串大小的記憶體 |

---

## 演示流程

以 `s = "1101"`（十進位 13）為例，逐步追蹤：

```
步驟  目前值        操作                  動作
----  ----------   -------------------   ---------------------------
 0    "1101" (13)  最低位為 '1' → 奇數    +1（二進位進位）
 1    "1110" (14)  最低位為 '0' → 偶數    ÷2（移除末尾 '0'）
 2    "111"  ( 7)  最低位為 '1' → 奇數    +1（全為 '1'，插入進位）
 3    "1000" ( 8)  最低位為 '0' → 偶數    ÷2
 4    "100"  ( 4)  最低位為 '0' → 偶數    ÷2
 5    "10"   ( 2)  最低位為 '0' → 偶數    ÷2
 6    "1"    ( 1)  等於 "1" → 結束
```

共 **6 步**。

---

以 `s = "1111"`（十進位 15）為例，說明全為 `'1'` 的特殊進位情況：

```
步驟  目前值         操作                   動作
----  -----------   --------------------   ---------------------------------
 0    "1111" (15)   最低位為 '1' → 奇數     +1（全為 '1'，往前插入 '1'）
 1    "10000" (16)  最低位為 '0' → 偶數     ÷2
 2    "1000"  ( 8)  最低位為 '0' → 偶數     ÷2
 3    "100"   ( 4)  最低位為 '0' → 偶數     ÷2
 4    "10"    ( 2)  最低位為 '0' → 偶數     ÷2
 5    "1"     ( 1)  等於 "1" → 結束
```

共 **5 步**。

---

## 執行專案

### 環境需求

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### 建構與執行

```bash
# 建構
dotnet build

# 執行
dotnet run --project leetcode_1404/leetcode_1404.csproj
```

預期輸出：

```
LeetCode 1404 - 將二進位表示減到 1 的步驟數
--------------------------------------------------
[PASS] NumSteps("1101") = 6  (預期: 6)
[PASS] NumSteps("10") = 1  (預期: 1)
[PASS] NumSteps("1") = 0  (預期: 0)
[PASS] NumSteps("1111") = 5  (預期: 5)
```
