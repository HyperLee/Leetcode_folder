# LeetCode 1952 — Three Divisors

> 三除數｜.NET 10 主控台專案

- [English problem](https://leetcode.com/problems/three-divisors/)
- [中文題目](https://leetcode.cn/problems/three-divisors/)

## 題目說明

給定整數 `n`，判斷它是否恰好有三個正因數。若正整數 `m` 能滿足某個整數 `k` 使
`n = k × m`，則 `m` 是 `n` 的因數。

題目限制：

- `1 <= n <= 10000`

## 核心不變量

若 `d` 整除 `n`，則 `n / d` 也整除 `n`，所以非平方根因數會成對出現；完全平方數的
平方根則只出現一次。因此，正因數數量為奇數的數一定是完全平方數。

要恰有三個正因數，`n` 必須是某個質數 `p` 的平方：

```plaintext
n = p²，正因數恰為 1、p、p²。
```

若平方根是合數，就會帶入更多非平凡因數。例如 `16 = 4²`，其正因數為
`1、2、4、8、16`，所以不能只檢查「是否為完全平方數」。

## 解法一：成對統計正因數

公開 API：

```csharp
public static bool IsThree(int n)
```

從 `1` 枚舉至 `√n`。每次找到整除者時，若目前因數不是平方根，就將因數與商一起計入；
若兩者相同則只計一次。最後判斷總數是否為 `3`。

- 時間複雜度：`O(√n)`
- 結果空間：`O(1)`
- 輔助空間：`O(1)`

這個版本保留舊解法的教學結構，可直接觀察因數如何成對出現。

## 解法二：質數平方判定

公開 API：

```csharp
public static bool IsThree2(int n)
```

先取整數平方根並確認它的平方能還原 `n`，再以試除法判斷平方根是否為質數。平方根最大
只有 `100`，而質數檢查只需枚舉至平方根的平方根。

- 時間複雜度：`O(n^(1/4))`
- 結果空間：`O(1)`
- 輔助空間：`O(1)`

這個版本直接使用「恰有三個正因數等價於質數平方」的數論性質，迭代次數較少，但需要先
理解該定理。

## 逐步範例

以 `n = 25` 為例：

```plaintext
√25 = 5，且 5 × 5 = 25，所以 25 是完全平方數。
5 不能被 2 整除，因此 5 是質數。
25 的正因數恰為 1、5、25，答案為 true。
```

對 `n = 16`，平方根 `4` 是合數，因此除了 `1、4、16` 外還有 `2、8`，答案為
`false`。

## Acceptance Harness

`Main` 對兩個公開方法執行相同十一個確定性案例，每案產生兩項結果檢查，共 22 項；任何
失敗都會把 process exit code 設為 `1`。

| # | 輸入 | 預期 | 驗證目的 |
| ---: | ---: | :---: | --- |
| 1 | `2` | `false` | 官方案例，質數只有兩個因數 |
| 2 | `4` | `true` | 官方案例，最小質數平方 |
| 3 | `1` | `false` | 最小有效輸入 |
| 4 | `9` | `true` | 小型奇質數平方 |
| 5 | `16` | `false` | 合數平方不可誤判 |
| 6 | `81` | `false` | 奇數合數平方，防止錯把 `9` 判為質數 |
| 7 | `8` | `false` | 非平方合數 |
| 8 | `97` | `false` | 質數但不是平方 |
| 9 | `25` | `true` | 另一個質數平方 |
| 10 | `9409` | `true` | `97²`，接近上限的質數平方 |
| 11 | `10000` | `false` | 最大輸入且平方根為合數 |

## 建置與執行

已從 repository 根目錄驗證：

```bash
dotnet build leetcode_1952/leetcode_1952/leetcode_1952.csproj --nologo
dotnet run --no-build --project leetcode_1952/leetcode_1952/leetcode_1952.csproj
```

若直接開啟題目根目錄 `leetcode_1952/`，使用：

```bash
dotnet build leetcode_1952/leetcode_1952.csproj --nologo
dotnet run --no-build --project leetcode_1952/leetcode_1952.csproj
```

以下是 fresh run 的完整輸出：

```text
LeetCode 1952 Acceptance Harness
Case: Official example 1
Input: n = 2
PASS IsThree result | Expected: False | Actual: False
PASS IsThree2 result | Expected: False | Actual: False

Case: Official example 2
Input: n = 4
PASS IsThree result | Expected: True | Actual: True
PASS IsThree2 result | Expected: True | Actual: True

Case: Minimum input
Input: n = 1
PASS IsThree result | Expected: False | Actual: False
PASS IsThree2 result | Expected: False | Actual: False

Case: Small prime square
Input: n = 9
PASS IsThree result | Expected: True | Actual: True
PASS IsThree2 result | Expected: True | Actual: True

Case: Composite square
Input: n = 16
PASS IsThree result | Expected: False | Actual: False
PASS IsThree2 result | Expected: False | Actual: False

Case: Odd composite square
Input: n = 81
PASS IsThree result | Expected: False | Actual: False
PASS IsThree2 result | Expected: False | Actual: False

Case: Non-square composite
Input: n = 8
PASS IsThree result | Expected: False | Actual: False
PASS IsThree2 result | Expected: False | Actual: False

Case: Prime but not square
Input: n = 97
PASS IsThree result | Expected: False | Actual: False
PASS IsThree2 result | Expected: False | Actual: False

Case: Another prime square
Input: n = 25
PASS IsThree result | Expected: True | Actual: True
PASS IsThree2 result | Expected: True | Actual: True

Case: Near-limit prime square
Input: n = 9409
PASS IsThree result | Expected: True | Actual: True
PASS IsThree2 result | Expected: True | Actual: True

Case: Maximum input
Input: n = 10000
PASS IsThree result | Expected: False | Actual: False
PASS IsThree2 result | Expected: False | Actual: False

Summary: 22/22 checks passed.
```

## 專案結構

```plaintext
leetcode_1952/
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── AGENTS.md
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_1952/
    ├── Program.cs
    └── leetcode_1952.csproj
```
