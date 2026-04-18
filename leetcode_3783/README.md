# LeetCode 3783 — Mirror Distance of an Integer

> **Daily Question · 2026-04-18**
> [English](https://leetcode.com/problems/mirror-distance-of-an-integer/description/?envType=daily-question&envId=2026-04-18) ·
> [繁體中文](https://leetcode.cn/problems/mirror-distance-of-an-integer/description/?envType=daily-question&envId=2026-04-18)

## 題目說明

給定一個整數 `n`，定義其**鏡像距離（Mirror Distance）**為：

$$\text{MirrorDistance}(n) = \lvert n - \text{reverse}(n) \rvert$$

其中 $\text{reverse}(n)$ 表示將 `n` 的十進位數字**逐位反轉**後所形成的整數（前導零自動省略）。

**回傳** `n` 的鏡像距離。

### 範例

| 輸入 `n` | `reverse(n)` | 計算過程          | 輸出  |
|:--------:|:------------:|:-----------------:|:-----:|
| `1`      | `1`          | \|1 − 1\| = 0     | `0`   |
| `120`    | `21`         | \|120 − 21\| = 99 | `99`  |
| `123`    | `321`        | \|123 − 321\| = 198 | `198` |

---

## 解題概念與出發點

### 觀察

- `reverse(n)` 只是對 `n` 的各位數字做排列變換，其**位數差**直接決定了兩者的大小差距。
- 前導零的省略使得尾部含有 `0` 的數字（如 `120`）的反轉結果位數更少，差距因此更大。

### 核心想法（數學法）

1. **逐位提取末位數字**：每輪取 `n % 10` 作為反轉數的新末位，將其拼接到 `reversed` 的末位：

   $$\text{reversed} \leftarrow \text{reversed} \times 10 + (n \bmod 10)$$

2. **縮短原始數字**：將 `n` 右移一位：

   $$n \leftarrow \lfloor n / 10 \rfloor$$

3. 重複以上步驟直到 `n = 0`，即完成反轉。

4. 最終回傳 $\lvert n_{\text{原始}} - \text{reversed} \rvert$。

> [!NOTE]
> 此方法純粹在整數層面操作，**不需要字串轉換**，對數字位數 $D$ 的時間複雜度為 $O(D)$，空間複雜度為 $O(1)$。

---

## 解法詳解

### 步驟拆解

```
輸入: n = 120

初始: reversed = 0

第一輪: n % 10 = 0  → reversed = 0 * 10 + 0 = 0  , n = 12
第二輪: n % 10 = 2  → reversed = 0 * 10 + 2 = 2  , n = 1
第三輪: n % 10 = 1  → reversed = 2 * 10 + 1 = 21 , n = 0

結束: reverse(120) = 21

答案: |120 - 21| = 99
```

### C# 實作

```csharp
// 反轉整數
private int ReverseNum(int num)
{
    int reversed = 0;
    while (num > 0)
    {
        reversed = reversed * 10 + num % 10; // 將末位數字附加到 reversed 末位
        num /= 10;                            // 移除 num 的末位數字
    }
    return reversed;
}

// 計算鏡像距離
public int MirrorDistance(int n)
{
    int rev = ReverseNum(n); // 取得鏡像數
    return Math.Abs(n - rev); // 回傳兩者差的絕對值
}
```

---

## 演示流程

以三組測試資料完整演示：

### Case 1 — `n = 1`（回文數）

```
n = 1

ReverseNum(1):
  第一輪: reversed = 0 * 10 + 1 = 1, n = 0
  結束 → reversed = 1

MirrorDistance: |1 - 1| = 0
```

> 回文數（正讀反讀相同）的鏡像距離永遠為 0。

---

### Case 2 — `n = 120`（尾部含零）

```
n = 120

ReverseNum(120):
  第一輪: reversed = 0  * 10 + 0 = 0 , n = 12
  第二輪: reversed = 0  * 10 + 2 = 2 , n = 1
  第三輪: reversed = 2  * 10 + 1 = 21, n = 0
  結束 → reversed = 21

MirrorDistance: |120 - 21| = 99
```

> 尾部的 `0` 被省略，使得 `reverse(120) = 21`（而非 `021`）。

---

### Case 3 — `n = 123`（一般數）

```
n = 123

ReverseNum(123):
  第一輪: reversed = 0   * 10 + 3 = 3  , n = 12
  第二輪: reversed = 3   * 10 + 2 = 32 , n = 1
  第三輪: reversed = 32  * 10 + 1 = 321, n = 0
  結束 → reversed = 321

MirrorDistance: |123 - 321| = 198
```

---

## 複雜度分析

| 指標     | 值     | 說明                        |
|:--------:|:------:|:---------------------------:|
| 時間複雜度 | $O(D)$ | $D$ 為 `n` 的十進位位數     |
| 空間複雜度 | $O(1)$ | 僅使用常數個額外變數         |

---

## 執行測試

```bash
cd leetcode_3783
dotnet run
```

預期輸出：

```
[PASS] MirrorDistance(1)   = 0    (expected: 0)
[PASS] MirrorDistance(120) = 99   (expected: 99)
[PASS] MirrorDistance(123) = 198  (expected: 198)
```
