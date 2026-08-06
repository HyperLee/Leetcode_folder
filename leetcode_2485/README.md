# LeetCode 2485：找出中樞整數

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![Language](https://img.shields.io/badge/Language-C%23-239120)](https://learn.microsoft.com/dotnet/csharp/)

這是一個以 .NET 10 Console App 示範 LeetCode 2485 的教學型專案。程式保留直觀的逐一枚舉解法，並加入線性累加與數學公式解法，讓三種不同複雜度的思考方式可以用同一組固定案例直接比較。

題目連結：

- [LeetCode 2485 - Find the Pivot Integer](https://leetcode.com/problems/find-the-pivot-integer/description/)
- [力扣 2485 - 找出中樞整數](https://leetcode.cn/problems/find-the-pivot-integer/description/)

## 快速導覽

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：逐一枚舉並重算總和](#解法一逐一枚舉並重算總和)
- [解法二：線性累加](#解法二線性累加)
- [解法三：數學公式](#解法三數學公式)
- [三種解法比較](#三種解法比較)
- [測試案例設計](#測試案例設計)
- [實際執行結果](#實際執行結果)
- [建置與執行](#建置與執行)

## 題目說明

給定一個正整數 `n`，請找出中樞整數 `x`，使得從 `1` 到 `x` 的所有整數總和，等於從 `x` 到 `n` 的所有整數總和：

```text
1 + 2 + ... + x = x + (x + 1) + ... + n
```

中樞值 `x` 會同時出現在左右兩個區間中。如果不存在符合條件的 `x`，回傳 `-1`。題目保證一個輸入最多只有一個中樞整數。

以 `n = 8` 為例，`x = 6`：

```text
1 + 2 + 3 + 4 + 5 + 6 = 21
6 + 7 + 8             = 21
```

因此答案是 `6`。

## 限制條件

- `1 <= n <= 1000`
- `x` 必須落在 `1` 到 `n` 的範圍內。
- 若不存在中樞整數，回傳 `-1`。
- 三個公開方法都假設輸入符合題目限制，不另外處理小於 `1` 的非法輸入。

## 解題概念與出發點

這題的核心是判斷候選值 `x` 的兩個區間總和是否相同。三種解法的差異，在於「如何取得左右總和」：

1. `PivotInteger` 對每個候選值重新計算左右兩段，流程最直接，適合作為正確性基準。
2. `PivotIntegerLinear` 先算出 `1..n` 的總和，再用累加中的前綴和推導右側總和，避免重複掃描。
3. `PivotIntegerByFormula` 將等式化簡為完全平方數判斷，不需要逐一檢查候選值。

三種方法都不修改輸入，輸入與輸出介面也相同：

```csharp
int result1 = Program.PivotInteger(n);
int result2 = Program.PivotIntegerLinear(n);
int result3 = Program.PivotIntegerByFormula(n);
```

找不到答案時統一回傳 `-1`。

## 解法一：逐一枚舉並重算總和

API：`PivotInteger(int n)`

### 設計說明

這是最接近題目敘述的基準解法。從 `x = 1` 依序嘗試到 `n`，每次執行兩次內層迴圈：

1. 累加 `1..x`，得到左側總和。
2. 累加 `x..n`，得到右側總和。
3. 若兩個總和相等，立即回傳目前的 `x`。
4. 所有候選值都不符合時回傳 `-1`。

中樞值必須同時列入兩側，所以右側迴圈從 `i = x` 開始，而不是從 `x + 1` 開始。

### 範例演示流程

以 `n = 8` 為例：

| 候選 `x` | `1..x` 總和 | `x..8` 總和 | 判斷 |
|---:|---:|---:|---|
| 1 | 1 | 36 | 不相等 |
| 2 | 3 | 35 | 不相等 |
| 3 | 6 | 33 | 不相等 |
| 4 | 10 | 30 | 不相等 |
| 5 | 15 | 26 | 不相等 |
| 6 | 21 | 21 | 找到，回傳 6 |

### 複雜度

- 時間複雜度：`O(n²)`。外層最多嘗試 `n` 個候選值，每個候選值都可能重新累加兩段區間。
- 額外空間複雜度：`O(1)`。
- 優點：流程直觀，容易從題目定義驗證正確性。
- 缺點：同一個數字區間會被重複累加，效率不是最佳。

## 解法二：線性累加

API：`PivotIntegerLinear(int n)`

### 設計說明

先利用等差級數總和公式計算整段 `1..n`：

```text
total = n(n + 1) / 2
```

掃描候選值時維護 `leftSum = 1 + ... + x`。因為 `leftSum` 已經包含 `x`，所以 `1..x-1` 的總和是 `leftSum - x`，右側總和可以直接寫成：

```text
rightSum = total - (leftSum - x)
         = total - leftSum + x
```

每個候選值只需要一次累加與幾次算術運算：

1. 將 `x` 加入 `leftSum`。
2. 用 `total - leftSum + x` 計算 `x..n`。
3. 比較左右總和，相等時回傳 `x`。

### 範例演示流程

對 `n = 8`，`total = 8 * 9 / 2 = 36`：

| 候選 `x` | 累加後 `leftSum` | `total - leftSum + x` | 判斷 |
|---:|---:|---:|---|
| 1 | 1 | 36 | 不相等 |
| 2 | 3 | 35 | 不相等 |
| 3 | 6 | 33 | 不相等 |
| 4 | 10 | 30 | 不相等 |
| 5 | 15 | 26 | 不相等 |
| 6 | 21 | 21 | 找到，回傳 6 |

和解法一相比，`1..x` 不會在每次候選檢查時重新計算，右側總和也由整段總和快速推得。

### 複雜度

- 時間複雜度：`O(n)`，只需由 `1` 掃描到 `n` 一次。
- 額外空間複雜度：`O(1)`。
- 優點：保留累加思維，且移除了重複計算。
- 注意：總和使用 `long` 計算，讓算術意圖更清楚，也避免擴充限制後發生整數溢位。

## 解法三：數學公式

API：`PivotIntegerByFormula(int n)`

### 設計說明

把左右總和分別寫成等差級數公式：

```text
leftSum  = x(x + 1) / 2
rightSum = n(n + 1) / 2 - x(x - 1) / 2
```

令兩者相等：

```text
x(x + 1) / 2 = n(n + 1) / 2 - x(x - 1) / 2
```

兩邊乘以 `2` 並整理後得到：

```text
x² = n(n + 1) / 2
```

因此只要：

1. 計算 `target = n(n + 1) / 2`。
2. 取得 `target` 的整數平方根。
3. 若平方根的平方仍等於 `target`，該平方根就是 `x`；否則回傳 `-1`。

### 範例演示流程

#### `n = 8`

```text
target = 8 * 9 / 2 = 36
sqrt(36) = 6
6 * 6 = 36
```

所以回傳 `6`。

#### `n = 49`

```text
target = 49 * 50 / 2 = 1225
sqrt(1225) = 35
35 * 35 = 1225
```

所以回傳 `35`。若 `target` 不是完全平方數，例如 `n = 4` 時 `target = 10`，就回傳 `-1`。

### 複雜度

- 時間複雜度：`O(1)`，只進行固定數量的算術與平方根運算。
- 額外空間複雜度：`O(1)`。
- 優點：不需要掃描候選值，效率最高。
- 注意：平方根只是候選值，仍必須透過 `squareRoot * squareRoot == target` 驗證，不能只直接轉型後回傳。

## 三種解法比較

| 比較項目 | 逐一枚舉並重算 | 線性累加 | 數學公式 |
|---|---|---|---|
| 公開方法 | `PivotInteger` | `PivotIntegerLinear` | `PivotIntegerByFormula` |
| 核心概念 | 逐一計算左右總和 | 總和與前綴和 | 完全平方數 |
| 時間複雜度 | `O(n²)` | `O(n)` | `O(1)` |
| 額外空間 | `O(1)` | `O(1)` | `O(1)` |
| 是否修改輸入 | 否 | 否 | 否 |
| 教學價值 | 最直觀的基準 | 展示避免重複計算 | 展示代數化簡與數學觀察 |
| 適合情境 | 初次理解題意 | 想保留掃描流程並最佳化 | 已能接受公式推導 |

三種方法都應回傳相同答案。專案中的 `Main` 會使用同一批輸入同時呼叫三個 API，讓最佳化版本能與直觀基準直接比對。

## 測試案例設計

主控台入口固定執行六個案例，每個案例都交給三種解法處理，共 18 項檢查：

| 案例 | 輸入 | 預期結果 | 驗證重點 |
|---|---:|---:|---|
| 官方範例一 | `n = 8` | `6` | 題目主要範例，存在中樞值 |
| 官方範例二 | `n = 1` | `1` | 最小合法輸入，中樞值同時是兩側唯一元素 |
| 官方範例三 | `n = 4` | `-1` | 合法輸入但不存在中樞值 |
| 小型無解案例 | `n = 2` | `-1` | 小範圍無解情況 |
| 較大有效案例 | `n = 49` | `35` | 驗證較大的完全平方數結果 |
| 上限案例 | `n = 1000` | `-1` | 覆蓋題目輸入上限 |

每筆輸出包含：

- `Expected`：案例預先定義的正確答案。
- `Actual (...)`：指定解法實際回傳的結果。
- `PASS` 或 `FAIL`：實際值是否等於預期值。

若任何一個解法失敗，`Main` 會回傳 `1`；全部通過時回傳 `0`，因此可以直接用程序結束狀態作為自動化驗收依據。

## 實際執行結果

以下內容來自完成建置後執行 `dotnet run --no-build --project leetcode_2485/leetcode_2485.csproj` 的輸出：

```text
=== 測試案例 ===

案例：官方範例一
輸入：n = 8
Expected: 6
Actual (PivotInteger): 6 - PASS
Actual (PivotIntegerLinear): 6 - PASS
Actual (PivotIntegerByFormula): 6 - PASS

案例：官方範例二
輸入：n = 1
Expected: 1
Actual (PivotInteger): 1 - PASS
Actual (PivotIntegerLinear): 1 - PASS
Actual (PivotIntegerByFormula): 1 - PASS

案例：官方範例三
輸入：n = 4
Expected: -1
Actual (PivotInteger): -1 - PASS
Actual (PivotIntegerLinear): -1 - PASS
Actual (PivotIntegerByFormula): -1 - PASS

案例：小型無解案例
輸入：n = 2
Expected: -1
Actual (PivotInteger): -1 - PASS
Actual (PivotIntegerLinear): -1 - PASS
Actual (PivotIntegerByFormula): -1 - PASS

案例：較大有效案例
輸入：n = 49
Expected: 35
Actual (PivotInteger): 35 - PASS
Actual (PivotIntegerLinear): 35 - PASS
Actual (PivotIntegerByFormula): 35 - PASS

案例：上限案例
輸入：n = 1000
Expected: -1
Actual (PivotInteger): -1 - PASS
Actual (PivotIntegerLinear): -1 - PASS
Actual (PivotIntegerByFormula): -1 - PASS

總結：18/18 項測試通過
```

## 建置與執行

請從專案根目錄 `/Users/qiuzili/Leetcode/Leetcode_folder/leetcode_2485` 執行：

```bash
dotnet restore leetcode_2485/leetcode_2485.csproj
dotnet build leetcode_2485/leetcode_2485.csproj --nologo
dotnet run --no-build --project leetcode_2485/leetcode_2485.csproj
```

格式與差異檢查：

```bash
dotnet format leetcode_2485/leetcode_2485.csproj --verify-no-changes --no-restore
git diff --check
```

本專案沒有獨立的自動化測試專案；`Main` 中的固定案例、Expected/Actual 比較、PASS/FAIL 輸出與程序結束碼共同構成可重複執行的驗收測試。

## 專案結構

```text
leetcode_2485/
├── README.md
├── docs/
│   └── readme-template.md
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── leetcode_2485.sln
└── leetcode_2485/
    ├── Program.cs
    └── leetcode_2485.csproj
```
