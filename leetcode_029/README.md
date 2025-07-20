# Leetcode 29: Divide Two Integers

## 題目描述
給定兩個整數 dividend 和 divisor，不使用乘法、除法和 mod 運算子，實現兩數相除。整數除法需向零截斷（即捨去小數部分）。返回相除後的商，若商超過 32 位元整數範圍，則回傳對應邊界值。

- LeetCode 題目連結：[Divide Two Integers](https://leetcode.com/problems/divide-two-integers/)

## 解題思路

### 1. 溢位處理
- 32 位元 int 範圍為 [-2,147,483,648, 2,147,483,647]。
- 當 dividend 為 int.MinValue 且 divisor 為 -1 時，數學上結果會超過 int.MaxValue，需直接回傳 int.MaxValue。

### 2. 全程以負數運算
- 因為負數能表示的範圍比正數大（int.MinValue 沒有對應的正數），所以將 dividend 和 divisor 都轉為負數處理，避免溢位。
- 最後根據原本的正負號決定回傳正值或負值。

### 3. 倍增法（Exponential Search / Doubling Method）
- 傳統暴力法每次只用除數去減被除數，時間複雜度 O(n)。
- 倍增法每次將除數與商都倍增（即每次乘以 2），直到超過被除數或即將溢位，這樣每次都用最大可行的倍數去減，將時間複雜度降為 O(log n)。
- 具體流程：
  1. 外層 while 迴圈：只要被除數 a <= 除數 b（皆為負數），就持續進行。
  2. 內層 while 迴圈：每次將 c（當前除數）和 d（對應商）倍增，直到 c 超過 a 或即將溢位（c >= LIMIT）。
  3. 用最大可行的 c 去減 a，並將 d 累加到答案。
  4. 重複直到 a > b。
- 這種方法大幅提升效率，且不需使用 long 型別。

#### 倍增法範例
以 15 ÷ 3 為例（全程以負數處理）：

1. a = -15, b = -3, ans = 0
2. 先用 -3 去減，然後倍增成 -6、-12，直到再倍增會超過 a（-24 < -15，不行）。
3. 這時用最大的倍增值 -12 去減 a，a 變成 -3，商累加 -4。
4. 再用 -3 去減，a 變成 0，商累加 -1。
5. 結果為 -5，根據正負號回傳 5。

### 4. 關鍵程式碼片段
```csharp
while (a <= b)
{
    int c = b, d = -1;
    while (c >= LIMIT && d >= LIMIT && c >= a - c)
    {
        c += c; // 除數倍增
        d += d; // 商倍增
    }
    a -= c; // 減去當前倍增後的除數
    ans += d; // 累加對應倍增後的商
}
```

## 完整 C# 解法
```csharp
public static int Divide(int dividend, int divisor)
{
    const int MIN = int.MinValue;
    const int MAX = int.MaxValue;
    const int LIMIT = -1073741824; // MIN 的一半，防止倍增溢位

    if (dividend == MIN && divisor == -1)
    {
        return MAX;
    }

    bool negative = (dividend > 0 && divisor < 0) || (dividend < 0 && divisor > 0);
    int a = dividend > 0 ? -dividend : dividend;
    int b = divisor > 0 ? -divisor : divisor;
    int ans = 0;
    while (a <= b)
    {
        int c = b, d = -1;
        while (c >= LIMIT && d >= LIMIT && c >= a - c)
        {
            c += c;
            d += d;
        }
        a -= c;
        ans += d;
    }
    return negative ? ans : -ans;
}
```

## 其他說明
- 本解法不使用 long 型別，完全符合題目限制。
- 倍增法是競賽與面試常見的高效技巧，適合處理大數據量與嚴格運算符號限制的問題。
- 詳細註解與說明請見原始程式碼。
