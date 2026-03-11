# LeetCode 1009 — 十進位整數的補數

以 C# 解答 [LeetCode 1009](https://leetcode.com/problems/complement-of-base-10-integer/)，展示以位元運算搭配遮罩（mask）求非負整數補數的方式。

## 題目描述

> 給定一個非負整數 `n`，回傳其**補數** — 即將其二進位表示中每個 `0` 翻轉為 `1`、每個 `1` 翻轉為 `0` 所得到的整數。

**限制條件：** `0 <= n < 10^9`

### 範例

| 輸入 `n` | 二進位  | 補數二進位 | 輸出 |
|:--------:|:-------:|:----------:|:----:|
| 5        | `101`   | `010`      | 2    |
| 7        | `111`   | `000`      | 0    |
| 10       | `1010`  | `0101`     | 5    |
| 0        | `0`     | `1`        | 1    |

## 解題思路 — 位元運算搭配遮罩

### 為何不直接翻轉全部 32 個位元？

電腦以 32 位元儲存整數。例如 `n = 5` 在內部表示為：

```
0000 0000 0000 0000 0000 0000 0000 0101
```

若翻轉全部 32 個位元，前面的零會變成一，導致結果錯誤。我們只需翻轉**有效位元** — 從最低有效位元到最高有效位元（含）。

### 演算法

1. **找出最高有效位元的位置 `i`**，在 `[1, 30]` 的範圍內迭代：

$$2^i \leq n < 2^{i+1}$$

2. **建構遮罩**，使恰好 `i + 1` 個位元全部為 `1`：

$$\text{mask} = 2^{i+1} - 1$$

3. **將 `n` 與遮罩做 XOR**，精確翻轉這些位元：

| `n` 的位元 | `mask` 的位元 | `n XOR mask` |
|:----------:|:-------------:|:------------:|
| 0          | 1             | 1 ✓          |
| 1          | 1             | 0 ✓          |
| 0（高位）  | 0             | 0（不變）    |

> [!NOTE]
> 當 `i = 30` 時，計算 `1 << 31` 會造成帶號 32 位元整數溢位。
> 程式碼以常數 `0x7FFFFFFF`（即 $2^{31} - 1$）直接處理此邊界情況。

### 逐步執行範例：`n = 5`

```
n     = 5  →  二進位：  0 … 0  1  0  1
                                ↑
                         最高有效位元位於 i = 2

mask  = 2^(2+1) - 1 = 7  →  二進位：  0 … 0  1  1  1

n XOR mask：
    0 … 0  1  0  1   (5)
  ⊕ 0 … 0  1  1  1   (7)
  ─────────────────
    0 … 0  0  1  0   (2)  ✓
```

### 時間與空間複雜度

| 複雜度 | 數值 |
|:------:|:----:|
| 時間   | O(log n) — 最多 30 次迭代 |
| 空間   | O(1) — 不需額外的資料結構 |

## 專案結構

```
leetcode_1009/
├── leetcode_1009.sln
└── leetcode_1009/
    ├── leetcode_1009.csproj
    └── Program.cs          # 解題實作與測試案例
```

## 執行方式

```bash
dotnet run --project leetcode_1009/leetcode_1009.csproj
```

預期輸出：

```
BitwiseComplement(5)  = 2
BitwiseComplement(7)  = 0
BitwiseComplement(10) = 5
BitwiseComplement(0)  = 1
```

## 參考資料

- [LeetCode 1009（英文版）](https://leetcode.com/problems/complement-of-base-10-integer/description/?envType=daily-question&envId=2026-03-11)
- [LeetCode 1009（中文版）](https://leetcode.cn/problems/complement-of-base-10-integer/description/?envType=daily-question&envId=2026-03-11)
