# LeetCode 50 - Pow(x, n)

這個專案使用 C# 實作 LeetCode 第 50 題 `Pow(x, n)`，並保留三種可執行的解法：

- `MyPow`: 迭代式快速冪
- `MyPow2`: 使用 helper 的遞迴快速冪
- `MyPow3`: 直接自我遞迴的分治快速冪

主程式會一次執行所有案例，逐一列出 PASS 或 FAIL，方便用 `dotnet run` 直接驗證每種解法是否正確。

## 題目說明

實作 `pow(x, n)`，計算 `x^n`。

範例：

- `x = 2.00000, n = 10`，結果為 `1024.00000`
- `x = 2.10000, n = 3`，結果為 `9.26100`
- `x = 2.00000, n = -2`，結果為 `0.25000`

負指數代表倒數：

```text
2^-2 = 1 / 2^2 = 1 / 4 = 0.25
```

## 限制條件

- `-100.0 < x < 100.0`
- `-2^31 <= n <= 2^31 - 1`
- `n` 一定是整數
- `x != 0` 或 `n > 0`
- 題目保證 `-10^4 <= x^n <= 10^4`

## 解題概念與出發點

這題的核心困難不是「怎麼乘」，而是「怎麼少乘」。

最直覺的做法是把 `x` 連乘 `n` 次，但當 `n` 很大時，時間複雜度會是 `O(n)`，很容易超時。這題真正要掌握的是快速冪（Exponentiation by Squaring）：

```text
x^10 = (x^5)^2
x^5  = (x^2)^2 * x
```

也就是說，每次都把指數砍半，問題規模會快速縮小，時間複雜度可以降到 `O(log n)`。

另外有兩個邊界條件必須先想清楚：

1. `n < 0`

```text
x^-n = 1 / x^n
```

所以可以先把負指數改寫成倒數問題，再處理正指數。

2. `n = int.MinValue`

```text
int.MinValue = -2147483648
```

如果直接對 `int.MinValue` 做取負，會發生溢位，因此程式裡要先把指數提升成 `long` 再處理。

## 專案執行與驗證指令

在目前資料夾根目錄執行：

```bash
dotnet build leetcode_050/leetcode_050.csproj -c Release
dotnet run --project leetcode_050/leetcode_050.csproj
git diff --check
```

用途說明：

- `dotnet build`: 驗證專案能成功編譯
- `dotnet run`: 執行 `Main` 內建的可執行測試資料
- `git diff --check`: 檢查多餘空白與換行問題

## Main 目前驗證的案例

`Main` 會對三種解法都執行以下案例：

- `Example 1`: `2^10 = 1024`
- `Example 2`: `2.1^3 = 9.261`
- `Example 3`: `2^-2 = 0.25`
- `Zero exponent`: `3.5^0 = 1`
- `Identity with int.MinValue`: `1^int.MinValue = 1`
- `Negative one with int.MinValue`: `(-1)^int.MinValue = 1`
- `Negative base odd exponent`: `(-2)^5 = -32`
- `Negative base even exponent`: `(-2)^6 = 64`

總共 `8` 組案例，搭配 `3` 種解法，一次執行 `24` 筆驗證。

## 解法一：`MyPow` 迭代式快速冪

### 設計說明

這個版本是最常見、也最穩定的快速冪寫法。

做法分成兩段：

1. 如果 `n` 是負數，先把問題改寫成 `(1 / x)^abs(n)`
2. 進入 while 迴圈，用位元判斷目前指數是不是奇數

當前指數如果是奇數，代表這一層的因子要乘進答案；不論奇偶，每一輪都會：

- 把 `factor` 平方
- 把 `exponent` 右移一位

這樣每次都把問題縮小一半，所以比逐次連乘快很多。

### 為什麼這樣設計

- 迭代版本沒有遞迴呼叫成本
- `long exponent = n` 可以安全處理 `int.MinValue`
- 位元運算 `& 1` 與 `>> 1` 很適合表達快速冪的奇偶拆分

### 時間與空間複雜度

- 時間複雜度：`O(log n)`
- 空間複雜度：`O(1)`

### 範例演示流程

以 `x = 2, n = 10` 為例：

| 步驟 | exponent | factor | result | 說明 |
| --- | ---: | ---: | ---: | --- |
| 初始 | 10 | 2 | 1 | 準備開始 |
| 1 | 10 | 2 | 1 | 偶數，不乘進 result |
| 2 | 5 | 4 | 1 | factor 平方，指數右移 |
| 3 | 5 | 4 | 4 | 奇數，先乘進 result |
| 4 | 2 | 16 | 4 | factor 平方，指數右移 |
| 5 | 1 | 256 | 4 | 偶數層處理完 |
| 6 | 1 | 256 | 1024 | 奇數，乘進最後一個 factor |
| 結束 | 0 | 65536 | 1024 | 回傳 1024 |

## 解法二：`MyPow2` 使用 helper 的遞迴快速冪

### 設計說明

這個版本把「負指數轉換」和「快速冪遞迴」拆成兩層：

1. `MyPow2`
   - 負責判斷 `n == 0`
   - 把 `int` 指數轉成 `long`
   - 如果是負指數，改成 `1 / PowRecursive(x, -exponent)`
2. `PowRecursive`
   - 只處理 `exponent >= 0`
   - 每次先算 `x^(n/2)`，再把結果平方
   - 如果 `n` 是奇數，就再多乘一次 `x`

這種切法的好處是責任清楚：

- 對外方法負責「入口與邊界」
- helper 負責「純粹的快速冪遞迴」

### 為什麼這樣設計

- 遞迴 helper 比較容易直接對照數學定義
- `long` 可以正確處理 `int.MinValue`
- helper 只接受非負指數，邏輯比直接處理正負數更單純

### 時間與空間複雜度

- 時間複雜度：`O(log n)`
- 空間複雜度：`O(log n)`，來自遞迴呼叫堆疊

### 範例演示流程

以 `x = 2, n = -5` 為例：

```text
MyPow2(2, -5)
= 1 / PowRecursive(2, 5)
```

接著展開 `PowRecursive(2, 5)`：

```text
PowRecursive(2, 5)
= PowRecursive(2, 2)^2 * 2

PowRecursive(2, 2)
= PowRecursive(2, 1)^2

PowRecursive(2, 1)
= 2
```

往回組合：

```text
PowRecursive(2, 2) = 2^2 = 4
PowRecursive(2, 5) = 4^2 * 2 = 32
MyPow2(2, -5) = 1 / 32 = 0.03125
```

## 解法三：`MyPow3` 直接自我遞迴的分治快速冪

### 設計說明

這個版本不另外開 helper，而是直接在 `MyPow3` 本身遞迴：

1. 先處理 `n == 0`
2. 遞迴求 `half = MyPow3(x, n / 2)`
3. 把 `half * half` 組回主要結果
4. 如果 `n` 是奇數：
   - `n > 0` 時補乘一次 `x`
   - `n < 0` 時補除一次 `x`

這種寫法比較像直接把數學式翻成程式：

```text
x^n = (x^(n/2))^2           , n 為偶數
x^n = (x^(n/2))^2 * x       , n 為正奇數
x^n = (x^(n/2))^2 / x       , n 為負奇數
```

### 為什麼這樣設計

- 寫法簡潔，概念清楚
- 不需要先把正負指數拆成不同 helper
- 遞迴過程自然表現「先算半邊，再重建完整答案」

### 時間與空間複雜度

- 時間複雜度：`O(log n)`
- 空間複雜度：`O(log n)`

### 範例演示流程

以 `x = -2, n = 5` 為例：

```text
MyPow3(-2, 5)
half = MyPow3(-2, 2)
```

繼續展開：

```text
MyPow3(-2, 2)
half = MyPow3(-2, 1)

MyPow3(-2, 1)
half = MyPow3(-2, 0) = 1
squared = 1 * 1 = 1
n 為正奇數，所以回傳 1 * (-2) = -2
```

往回組合：

```text
MyPow3(-2, 2)
= (-2)^2
= 4

MyPow3(-2, 5)
= 4^2 * (-2)
= 16 * (-2)
= -32
```

## 三種解法比較

| 解法 | 核心技巧 | 時間複雜度 | 空間複雜度 | 特點 |
| --- | --- | --- | --- | --- |
| `MyPow` | 迭代快速冪 | `O(log n)` | `O(1)` | 最穩定、最適合實戰 |
| `MyPow2` | helper 遞迴快速冪 | `O(log n)` | `O(log n)` | 邊界處理與遞迴核心分離 |
| `MyPow3` | 直接分治遞迴 | `O(log n)` | `O(log n)` | 寫法最接近數學遞迴定義 |

## 實際執行輸出

以下輸出來自：

```bash
dotnet run --project leetcode_050/leetcode_050.csproj
```

```text
[MyPow]
  Example 1: x = 2, n = 10, expected = 1024, actual = 1024, PASS
  Example 2: x = 2.1, n = 3, expected = 9.261, actual = 9.261, PASS
  Example 3: x = 2, n = -2, expected = 0.25, actual = 0.25, PASS
  Zero exponent: x = 3.5, n = 0, expected = 1, actual = 1, PASS
  Identity with int.MinValue: x = 1, n = -2147483648, expected = 1, actual = 1, PASS
  Negative one with int.MinValue: x = -1, n = -2147483648, expected = 1, actual = 1, PASS
  Negative base odd exponent: x = -2, n = 5, expected = -32, actual = -32, PASS
  Negative base even exponent: x = -2, n = 6, expected = 64, actual = 64, PASS

[MyPow2]
  Example 1: x = 2, n = 10, expected = 1024, actual = 1024, PASS
  Example 2: x = 2.1, n = 3, expected = 9.261, actual = 9.261, PASS
  Example 3: x = 2, n = -2, expected = 0.25, actual = 0.25, PASS
  Zero exponent: x = 3.5, n = 0, expected = 1, actual = 1, PASS
  Identity with int.MinValue: x = 1, n = -2147483648, expected = 1, actual = 1, PASS
  Negative one with int.MinValue: x = -1, n = -2147483648, expected = 1, actual = 1, PASS
  Negative base odd exponent: x = -2, n = 5, expected = -32, actual = -32, PASS
  Negative base even exponent: x = -2, n = 6, expected = 64, actual = 64, PASS

[MyPow3]
  Example 1: x = 2, n = 10, expected = 1024, actual = 1024, PASS
  Example 2: x = 2.1, n = 3, expected = 9.261, actual = 9.261, PASS
  Example 3: x = 2, n = -2, expected = 0.25, actual = 0.25, PASS
  Zero exponent: x = 3.5, n = 0, expected = 1, actual = 1, PASS
  Identity with int.MinValue: x = 1, n = -2147483648, expected = 1, actual = 1, PASS
  Negative one with int.MinValue: x = -1, n = -2147483648, expected = 1, actual = 1, PASS
  Negative base odd exponent: x = -2, n = 5, expected = -32, actual = -32, PASS
  Negative base even exponent: x = -2, n = 6, expected = 64, actual = 64, PASS

Summary: 24/24 passed, 0 failed.
```

## 專案結構

```text
leetcode_050/
├─ README.md
├─ docs/
│  └─ readme-template.md
└─ leetcode_050/
   ├─ Program.cs
   └─ leetcode_050.csproj
```

## 重點整理

- 這題的關鍵不是暴力連乘，而是把指數持續對半拆解
- 負指數要先轉成倒數問題
- `int.MinValue` 是實作時最容易漏掉的邊界
- 三種解法都能通過目前 `Main` 內建的 `24/24` 驗證案例
