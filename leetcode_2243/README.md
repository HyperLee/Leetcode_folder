# LeetCode 2243 — Calculate Digit Sum of a String

> 計算字串的數位和｜.NET 10 主控台專案

- [English problem](https://leetcode.com/problems/calculate-digit-sum-of-a-string/)
- [中文題目](https://leetcode.cn/problems/calculate-digit-sum-of-a-string/)

## 題目說明

給定僅由數字組成的字串 `s` 與整數 `k`。只要 `s.Length > k`，就由左至右將字串切成每組至多 `k` 個字元的連續群組，計算每組數字總和，並把每個總和的十進位表示依序串接為下一輪字串。當字串長度不超過 `k` 時，回傳它。

題目限制：

- `1 <= s.Length <= 100`
- `2 <= k <= 100`
- `s` 僅包含數字字元。

公開 API：

```csharp
public static string DigitSum(string s, int k)
```

`DigitSum` 是純函式：不輸出主控台、不修改呼叫端可觀察的外部資料，僅回傳最終字串。所有 console I/O 都位於 `Main` 的 acceptance harness。

## 迭代分組設計

每一輪以 `groupStart += k` 前進，並以 `Math.Min(groupStart + k, s.Length)` 取得群組尾端；內層迴圈將 `s[i] - '0'` 累加，再把總和附加到 `StringBuilder`。

不變量是：每一輪中，已處理的群組彼此不重疊、維持原有由左至右順序，且其聯集恰好覆蓋原字串。最後不足 `k` 字元的尾端群組仍會照常求和；當新字串長度 `<= k` 時停止，不再做任何分組。

這個做法直接對應題意、無遞迴堆疊成本，並以一個下一輪緩衝區清楚隔離讀取與寫入。代價是每一輪都會建立新字串；不過每輪必須產生的輸出正是下一輪必要資料。

令 `L` 為所有輪次實際處理字元數的總和，時間複雜度為 `O(L)`；輔助空間為 `O(n)`，結果空間為 `O(n)`，其中 `n` 是初始字串長度。

### 範例推演：`"11111222223"`，`k = 3`

```plaintext
第 1 輪：111 | 112 | 222 | 23
數字和：  3 |   4 |   6 |  5
新字串：3465

第 2 輪：346 | 5
數字和： 13 | 5
新字串：135

135 的長度為 3，不大於 k，因此停止並回傳 135。
```

## Acceptance Harness

`Main` 執行八個確定性案例，每案比較一次 `DigitSum` 的回傳值；八項皆成功時，輸出必須以 `Summary: 8/8 checks passed.` 結尾，任一失敗則 process exit code 為 `1`。

| # | 輸入 | 預期值 | 驗證目的／可捕捉的錯誤 |
| ---: | --- | --- | --- |
| 1 | `"11111222223"`, `k = 3` | `"135"` | 官方範例；驗證多組與多輪縮約。 |
| 2 | `"00000000"`, `k = 3` | `"000"` | 驗證零的群組和及前導零不會被錯誤刪除。 |
| 3 | `"1"`, `k = 2` | `"1"` | 驗證初始長度小於 `k` 時立刻停止。 |
| 4 | `"123"`, `k = 3` | `"123"` | 驗證初始長度恰為 `k` 時不應多做一輪。 |
| 5 | `"123456"`, `k = 3` | `"615"` | 驗證完整群組邊界與多位數群組和的串接。 |
| 6 | `"1234567"`, `k = 3` | `"127"` | 驗證末尾短群組不能遺漏，且後續輪次正確。 |
| 7 | `"987654321"`, `k = 2` | `"36"` | 驗證小 `k` 的多輪迭代，而非只處理第一輪。 |
| 8 | 100 個 `"9"`, `k = 99` | `"8919"` | 驗證接近上限的群組與最後一個單字元尾組。 |

## 建置與執行

已從 repository 根目錄驗證：

```bash
dotnet build leetcode_2243/leetcode_2243/leetcode_2243.csproj --nologo
dotnet run --no-build --project leetcode_2243/leetcode_2243/leetcode_2243.csproj
```

若直接開啟題目根目錄 `leetcode_2243/`，使用：

```bash
dotnet build leetcode_2243/leetcode_2243.csproj --nologo
dotnet run --no-build --project leetcode_2243/leetcode_2243.csproj
```

以下是 fresh run 的完整輸出：

```text
Case: Official example; Input: "11111222223", k = 3
Expected: 135
Actual: 135
PASS
Case: All zeroes; Input: "00000000", k = 3
Expected: 000
Actual: 000
PASS
Case: Single character; Input: "1", k = 2
Expected: 1
Actual: 1
PASS
Case: Already k characters; Input: "123", k = 3
Expected: 123
Actual: 123
PASS
Case: Two complete groups; Input: "123456", k = 3
Expected: 615
Actual: 615
PASS
Case: Final short group; Input: "1234567", k = 3
Expected: 127
Actual: 127
PASS
Case: Multiple rounds; Input: "987654321", k = 2
Expected: 36
Actual: 36
PASS
Case: Near limit group; Input: "9999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999", k = 99
Expected: 8919
Actual: 8919
PASS
Summary: 8/8 checks passed.
```

## 舊版檔案整理

本題已移除舊式 solution 檔 `leetcode_2243.sln`、`App.config` 與手寫 `Properties/AssemblyInfo.cs`。SDK-style `net10.0` 專案由 `leetcode_2243.csproj` 集中管理組件資訊與建置設定，因此不保留這些舊版 .NET Framework 產物。

## 專案結構

```plaintext
leetcode_2243/
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
└── leetcode_2243/
    ├── Program.cs
    └── leetcode_2243.csproj
```
