# LeetCode 70：Climbing Stairs（爬樓梯）

這是一個使用 C# 與 .NET 10 撰寫的主控台專案，示範三種計算爬樓梯走法的方式：
黃金比例公式、純遞迴，以及使用固定額外空間的迭代動態規劃。

- [題目連結](https://leetcode.com/problems/climbing-stairs/)
- [解題概念](#解題概念與出發點)
- [三種解法比較](#三種解法比較)
- [建置與執行](#建置與執行)

## 題目說明

有一座共 `n` 階的樓梯，每次只能向上走 `1` 階或 `2` 階。請計算抵達第 `n` 階共有多少種不同走法。

例如：

- `n = 2` 時有 `1 + 1`、`2`，共 `2` 種。
- `n = 3` 時有 `1 + 1 + 1`、`1 + 2`、`2 + 1`，共 `3` 種。

### 限制條件

```text
1 <= n <= 45
```

## 解題概念與出發點

思考抵達第 `n` 階之前的最後一步：

1. 如果最後走 `1` 階，前一個位置一定是第 `n - 1` 階。
2. 如果最後走 `2` 階，前一個位置一定是第 `n - 2` 階。
3. 兩種情況不會重疊，因此可以把走法數相加。

所以得到共同遞推式：

```text
f(n) = f(n - 1) + f(n - 2)
f(1) = 1
f(2) = 2
```

這與費波那契數列相同，只是索引向後移一位：

```text
爬 n 階的答案 = F(n + 1)
```

例如 `n = 5`：

```text
f(1) = 1
f(2) = 2
f(3) = 3
f(4) = 5
f(5) = 8
```

三種解法的主要差別，不是遞推關係不同，而是「如何取得 `F(n + 1)`」。

## 三種解法比較

| 方法 | 專案方法 | 核心設計 | 時間複雜度 | 空間複雜度 | 特性 |
| --- | --- | --- | --- | --- | --- |
| 黃金比例公式 | `ClimbStairs` | 直接套用費波那契一般式 | `O(log n)`* | `O(1)` | 程式短，但依賴浮點運算 |
| 純遞迴 | `ClimbStairs2` | 將問題拆成 `n-1` 與 `n-2` | `O(2^n)` | `O(n)` | 最接近遞推定義，但有大量重複計算 |
| 迭代動態規劃 | `ClimbStairs3` | 保存前兩階答案並逐階更新 | `O(n)` | `O(1)` | 容易理解、效能穩定，是本專案推薦解法 |

> [!NOTE]
> `ClimbStairs` 的複雜度把 `Math.Pow` 視為快速冪，因此記為 `O(log n)`；
> 在固定大小的 `double` 運算模型中，也常把這個標準函式呼叫視為常數成本。

## 解法一：黃金比例公式

### 設計說明

費波那契數列可以使用 Binet's Formula（黃金比例一般式）直接求值：

```text
F(k) = (φ^k - ψ^k) / √5

φ = (1 + √5) / 2
ψ = (1 - √5) / 2
```

爬 `n` 階對應 `F(n + 1)`，因此程式將指數設為 `n + 1`：

```text
ClimbStairs(n) = (φ^(n+1) - ψ^(n+1)) / √5
```

程式中的變數對應關係：

- `a1`：`1 / √5`
- `b2`：`φ^(n+1)`
- `c3`：`ψ^(n+1)`
- `fx`：將公式結果轉為 `int`

這種作法不需要保存先前階梯的答案，也不會建立遞迴呼叫樹。不過公式使用
`double`，理論上可能產生浮點誤差；本題限制在 `n <= 45`，目前實作可得到正確整數結果。

### `n = 5` 演示流程

此時要計算 `F(6)`：

```text
φ ≈ 1.6180339887
ψ ≈ -0.6180339887

φ^6 ≈ 17.94427191
ψ^6 ≈ 0.05572809

F(6)
= (17.94427191 - 0.05572809) / √5
= 17.88854382 / 2.23606798
= 8
```

所以爬 `5` 階共有 `8` 種走法。

## 解法二：純遞迴

### 設計說明

`ClimbStairs2` 直接把遞推式翻成程式：

```text
ClimbStairs2(n)
= ClimbStairs2(n - 1)
+ ClimbStairs2(n - 2)
```

終止條件是：

- `n = 1`：只有走 `1` 階，共 `1` 種。
- `n = 2`：可以走 `1 + 1` 或 `2`，共 `2` 種。

這個版本很適合用來理解題目，但不同分支會重複計算相同問題。例如計算
`f(5)` 時，`f(3)` 會在 `f(4)` 裡計算一次，之後又被直接計算一次。
輸入變大時，重複呼叫數量會快速增加。

### `n = 5` 演示流程

```text
f(5)
├─ f(4)
│  ├─ f(3)
│  │  ├─ f(2) = 2
│  │  └─ f(1) = 1
│  │  => f(3) = 3
│  └─ f(2) = 2
│  => f(4) = 5
└─ f(3)
   ├─ f(2) = 2
   └─ f(1) = 1
   => f(3) = 3

f(5) = f(4) + f(3)
     = 5 + 3
     = 8
```

從呼叫樹可以看到 `f(3)`、`f(2)` 都被重複處理，這正是純遞迴效率較低的原因。

## 解法三：迭代動態規劃

### 設計說明

要計算目前階梯的答案，只需要前兩階的答案，不必保存完整陣列：

- `pre`：較前一階的答案，初始為 `f(1) = 1`
- `next`：前一階的答案，初始為 `f(2) = 2`
- `result`：目前階梯的答案，即 `pre + next`

每一輪計算完成後依序更新：

```text
result = pre + next
pre = next
next = result
```

必須先算出 `result` 才能移動 `pre` 與 `next`，否則會提早覆蓋仍需使用的舊值。
這個方法只保留三個整數，因此額外空間固定為 `O(1)`。

### `n = 5` 演示流程

初始狀態已知 `f(1) = 1`、`f(2) = 2`：

| 目標階梯 | 計算前 `pre` | 計算前 `next` | `result = pre + next` | 更新後狀態 |
| ---: | ---: | ---: | ---: | --- |
| 3 | 1 | 2 | 3 | `pre = 2`, `next = 3` |
| 4 | 2 | 3 | 5 | `pre = 3`, `next = 5` |
| 5 | 3 | 5 | 8 | `pre = 5`, `next = 8` |

迴圈結束後回傳 `8`。

## 可執行測試資料

`Main` 會用同一組固定案例驗證三種解法：

| `n` | 預期答案 | 案例目的 |
| ---: | ---: | --- |
| 1 | 1 | 最小輸入與第一個終止條件 |
| 2 | 2 | 第二個終止條件 |
| 3 | 3 | 第一次套用遞推式 |
| 5 | 8 | 一般小型案例 |
| 10 | 89 | 多輪遞推案例 |

沒有把 `n = 45` 放入共用 runner，因為純遞迴解法會產生大量重複呼叫；
這不影響題目支援的輸入限制。

## 建置與執行

請從此 repository 的 `leetcode_070` 工作區根目錄執行：

```powershell
dotnet build leetcode_070/leetcode_070.csproj --nologo
dotnet run --project leetcode_070/leetcode_070.csproj --no-build
```

目前沒有獨立的自動化測試專案；建置與 `Main` 的固定案例是此專案的驗收方式。

### 執行結果

```text
LeetCode 70 - Climbing Stairs
n=1, expected=1
  ClimbStairs: actual=1, PASS
  ClimbStairs2: actual=1, PASS
  ClimbStairs3: actual=1, PASS
n=2, expected=2
  ClimbStairs: actual=2, PASS
  ClimbStairs2: actual=2, PASS
  ClimbStairs3: actual=2, PASS
n=3, expected=3
  ClimbStairs: actual=3, PASS
  ClimbStairs2: actual=3, PASS
  ClimbStairs3: actual=3, PASS
n=5, expected=8
  ClimbStairs: actual=8, PASS
  ClimbStairs2: actual=8, PASS
  ClimbStairs3: actual=8, PASS
n=10, expected=89
  ClimbStairs: actual=89, PASS
  ClimbStairs2: actual=89, PASS
  ClimbStairs3: actual=89, PASS
Overall: 15/15 passed.
```

## 專案結構

```text
leetcode_070/
├─ docs/
│  └─ readme-template.md
├─ leetcode_070/
│  ├─ leetcode_070.csproj
│  └─ Program.cs
├─ leetcode_070.sln
└─ README.md
```
