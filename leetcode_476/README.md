# LeetCode 476：數字的補數

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![Language](https://img.shields.io/badge/C%23-Console-239120)
![LeetCode](https://img.shields.io/badge/LeetCode-476-FFA116)

這是一個使用 C# 與 .NET 10 實作的教學型主控台專案。專案保留兩種以位元特性求整數補數的
解法，並透過 `Main` 中可重複執行的 acceptance harness，同時驗證兩種方法在官方範例、
邊界值與不同位元排列下的結果。

- [LeetCode English](https://leetcode.com/problems/number-complement/)
- [LeetCode 中文](https://leetcode.cn/problems/number-complement/)

## 題目說明

給定一個正整數 `num`，將它的二進位表示中每個有效位元反轉：

- `0` 變成 `1`
- `1` 變成 `0`

再把反轉後的二進位數轉回十進位，得到 `num` 的補數。計算時只處理從最高有效位元到最低位
的範圍，不把前導零視為輸入的一部分。

例如 `5` 的二進位表示為 `101`：

```
原數：101
反轉：010
結果：  2
```

另一個官方案例是 `num = 1`。其有效二進位表示只有一位 `1`，反轉後為 `0`，所以補數是
`0`。

> [!IMPORTANT]
> 本題的「補數」是反轉有效二進位位元，不是用來表示負數的二補數（two's complement）。
> 也不應反轉 `int` 的全部 32 位，否則前導零會被錯誤地納入結果。

## 限制條件

- `1 <= num < 2^31`
- 輸入一定是可由 C# `int` 表示的正整數。
- 公開方法預期接收符合題目限制的值，不另外定義 `0` 或負數的行為。
- `FindComplement` 與 `FindComplement2` 都不修改輸入，也不直接輸出主控台內容。
- 主控台輸入輸出集中在 `Main`，兩個演算法方法只負責計算並回傳結果。

## 解題概念與出發點

直接使用 C# 的位元 NOT 運算子 `~num` 會反轉 `int` 的全部 32 位，其中包含題目不計算的
前導零，因此會得到負數，而不是題目要求的補數。兩種解法都先解決同一個核心問題：

> 如何限定「只反轉最高有效位元以下的位元」？

對一個二進位長度為 `k` 的正整數，可以建立 `k` 個 `1` 組成的值：

```
mask = 2^k - 1
```

例如 `num = 5 = 101₂` 有三個有效位元，因此遮罩是 `111₂ = 7`。接下來有兩種等價觀點：

1. `num XOR mask`：`mask` 的每一位都是 `1`，XOR 會反轉 `num` 的每個有效位元。
2. `mask - num`：在固定的 `k` 位範圍內，全 `1` 的值減去原數，正好得到逐位反轉的結果。

## 解法比較

| 解法 | 核心做法 | 時間複雜度 | 輔助空間 | 特點 |
| --- | --- | --- | --- | --- |
| `FindComplement` | 找出最高有效位元，建立遮罩後 XOR | `O(log num)` | `O(1)` | 位元運算意圖直接，適合練習 lowbit 與清除最低位技巧 |
| `FindComplement2` | 產生 `1、3、7、15...`，再減去原數 | `O(log num)` | `O(1)` | 算術關係直觀，不需要直接組合 XOR 遮罩 |

兩種方法的回傳值與輸入資料規模相同，結果空間皆為 `O(1)`。

## 解法一：最高有效位元遮罩與 XOR

### 設計說明

`FindComplement` 使用兩個常見的位元技巧：

```csharp
x & -x
```

可取出 `x` 最低位的 `1`；而：

```csharp
x & (x - 1)
```

會清除 `x` 最低位的 `1`。方法反覆清除最低位的 `1`，每次把目前找到的位置記在
`highbit`。最後一次找到的位元就是原數的最高有效位元。

若最高有效位元是 `highbit`，則：

```csharp
(highbit << 1) - 1
```

會得到從最高有效位元到最低位全部為 `1` 的遮罩。最後執行：

```csharp
num ^ mask
```

因為任何位元和 `1` XOR 都會反轉，所以能在不影響前導位元的情況下得到答案。

實際迴圈次數等於 `num` 中 `1` 的數量，最壞情況不超過它的有效位元數，因此時間複雜度可
表示為 `O(log num)`；方法只使用固定數量的整數變數，輔助空間為 `O(1)`。

### 範例演示：`num = 5`

`5` 的二進位表示為 `101`。

| 迴圈 | `x` | `x & -x` 得到的 `highbit` | `x & (x - 1)` 後的 `x` |
| ---: | ---: | ---: | ---: |
| 1 | `101` | `001` | `100` |
| 2 | `100` | `100` | `000` |

迴圈結束時：

```
highbit = 100₂
mask    = (100₂ << 1) - 1
        = 1000₂ - 1
        = 111₂
```

執行 XOR：

```
  101
^ 111
-----
  010
```

`010₂` 轉為十進位即為 `2`。

## 解法二：建立全 1 整數後相減

### 設計說明

`FindComplement2` 從 `sum = 1` 開始，反覆計算：

```csharp
sum = sum * 2 + 1;
```

二進位左移一位相當於乘以 `2`，再加 `1` 會把新出現的最低位設為 `1`，所以 `sum` 依序
形成：

```
1₂, 11₂, 111₂, 1111₂, ...
```

也就是十進位的：

```
1, 3, 7, 15, ...
```

當 `sum >= num` 時，`sum` 已經具有與 `num` 相同的有效位數，而且所有位元都是 `1`。
固定在這個位數範圍內，`sum - num` 等價於逐位反轉，因此可直接回傳補數。

每輪增加一個二進位位數，最多執行 `O(log num)` 輪；方法只使用一個額外整數，輔助空間為
`O(1)`。

### 範例演示：`num = 5`

| 步驟 | `sum` 十進位 | `sum` 二進位 | 是否已涵蓋 `num` |
| ---: | ---: | ---: | --- |
| 初始 | 1 | `1` | 否 |
| 1 | 3 | `11` | 否 |
| 2 | 7 | `111` | 是 |

因此：

```
sum - num = 7 - 5 = 2
```

從二進位觀察也是：

```
111₂ - 101₂ = 010₂
```

## Acceptance Harness

專案目前沒有 xUnit、NUnit 或 MSTest 專案。`Main` 是可重複執行的 acceptance harness，
每組案例都會：

1. 呼叫 `FindComplement`。
2. 呼叫 `FindComplement2`。
3. 分別將兩個結果與手工推導的預期值比較。
4. 只有兩種解法都正確時才顯示 `PASS`。
5. 任一案例失敗時，將 process exit code 設為 `1`。

| # | 案例 | 輸入 | 預期 | 驗證重點 |
| ---: | --- | ---: | ---: | --- |
| 1 | Minimum input | 1 | 0 | 題目允許的最小值 |
| 2 | Single set bit | 2 | 1 | 最高位是 `1`、其餘有效位為 `0` |
| 3 | Official example | 5 | 2 | 官方 `101₂ → 010₂` 範例 |
| 4 | All bits are one | 7 | 0 | 所有有效位元反轉後皆為 `0` |
| 5 | Mixed bits | 10 | 5 | `1010₂ → 0101₂` 的交錯位元 |
| 6 | Maximum input | `int.MaxValue` | 0 | 題目上界 `2^31 - 1` |

## 建置、測試與執行

需求：

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

從此題目的 repository root 執行：

```bash
dotnet restore leetcode_476/leetcode_476.csproj
dotnet build leetcode_476/leetcode_476.csproj --nologo --no-restore
dotnet run --no-build --project leetcode_476/leetcode_476.csproj
git diff --check
```

目前沒有正式測試專案，因此不另外執行 `dotnet test`；行為驗證由 `Main` 中的六組案例完成。

### 實際執行輸出

以下內容來自完成建置後的 fresh run：

```text
Case: Minimum input
Input: 1
Expected: 0
FindComplement: 0
FindComplement2: 0
Result: PASS

Case: Single set bit
Input: 2
Expected: 1
FindComplement: 1
FindComplement2: 1
Result: PASS

Case: Official example
Input: 5
Expected: 2
FindComplement: 2
FindComplement2: 2
Result: PASS

Case: All bits are one
Input: 7
Expected: 0
FindComplement: 0
FindComplement2: 0
Result: PASS

Case: Mixed bits
Input: 10
Expected: 5
FindComplement: 5
FindComplement2: 5
Result: PASS

Case: Maximum input
Input: 2147483647
Expected: 0
FindComplement: 0
FindComplement2: 0
Result: PASS

Summary: 6/6 cases passed.
```

## 專案結構

```plaintext
.
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
├── leetcode_476.sln
└── leetcode_476/
    ├── Program.cs
    └── leetcode_476.csproj
```

- `Program.cs`：包含兩種數字補數演算法與六組可執行驗證案例。
- `leetcode_476.csproj`：目標框架為 `net10.0` 的主控台專案。
- `docs/readme-template.md`：本 README 遵循的初始文件指引。
- `.vscode/`：提供預設建置工作與直接啟動 `leetcode_476` 的偵錯設定。
