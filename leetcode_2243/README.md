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

核心流程是「分組 → 各組逐位加總 → 依序串接 → 檢查是否需要下一輪」。每輪由左至右處理所有字元，最後不足 `k` 個字元的群組也必須求和；只有當字串長度 `<= k` 時才停止。

### 核心程式碼

以下對應 `leetcode_2243/Program.cs` 的實作：

```csharp
public static string DigitSum(string s, int k)
{
    while (s.Length > k)
    {
        StringBuilder next = new();

        // 每個群組從 groupStart 起，直到不超過字串尾端的 groupEnd 為止。
        for (int groupStart = 0; groupStart < s.Length; groupStart += k)
        {
            int groupEnd = Math.Min(groupStart + k, s.Length);
            int sum = 0;

            for (int i = groupStart; i < groupEnd; i++)
            {
                sum += s[i] - '0';
            }

            next.Append(sum);
        }

        s = next.ToString();
    }

    return s;
}
```

### 三層迴圈的分工

| 控制流程 | 工作 | 關鍵條件 |
| --- | --- | --- |
| `while` | 控制是否需要再處理一輪。 | `s.Length > k` 才進入。 |
| 外層 `for` | 依序選出本輪的每個群組。 | 起點從 `0` 開始，每次增加 `k`。 |
| 內層 `for` | 計算目前群組的數字和。 | 從 `groupStart` 走到 `groupEnd` 前一個索引。 |

例如 `s = "123"`、`k = 3` 時，`3 > 3` 不成立，會直接回傳 `"123"`。題目依長度決定停止時機，不需要繼續加總成一位數。

### 群組邊界與尾端短群組

`groupStart` 是群組起點，`groupEnd` 是不包含在群組內的結束索引。群組採用半開區間 `[groupStart, groupEnd)`，也就是包含起點、不包含終點。

```csharp
int groupEnd = Math.Min(groupStart + k, s.Length);
```

一般群組有 `k` 個字元；若尾端剩餘字元不足 `k` 個，就以 `s.Length` 作為終點。以 `"11111222223"`、`k = 3` 為例：

| `groupStart` | `groupStart + k` | `groupEnd` | 實際讀取索引 | 群組 | 數字和 |
| ---: | ---: | ---: | --- | --- | ---: |
| 0 | 3 | 3 | 0、1、2 | `"111"` | 3 |
| 3 | 6 | 6 | 3、4、5 | `"112"` | 4 |
| 6 | 9 | 9 | 6、7、8 | `"222"` | 6 |
| 9 | 12 | 11 | 9、10 | `"23"` | 5 |

最後一組取 `Math.Min(12, 11) = 11`，因此只讀取索引 `9`、`10`，不會超出字串範圍，也不會漏掉 `"23"`。

每次以 `groupStart += k` 移到下一組，維持以下不變量：已處理的群組彼此不重疊，保持原有由左至右順序；整輪結束時，所有群組恰好覆蓋原字串。

### 字元轉數字與群組加總

```csharp
int sum = 0;

for (int i = groupStart; i < groupEnd; i++)
{
    sum += s[i] - '0';
}
```

數字字元的編碼連續，因此 `s[i] - '0'` 可以取得該字元代表的整數，例如 `'3' - '0' = 3`。題目已保證輸入只含數字，這裡直接逐位轉換並加總。

每個群組開始時都重新設定 `sum = 0`，讓各組獨立計算。例如 `"112"` 的累加過程為 `0 → 1 → 2 → 4`，得到的是 `1 + 1 + 2 = 4`。

### 串接群組和與更新下一輪

`next.Append(sum)` 會將整數的十進位文字附加到 `StringBuilder`，保留群組和的全部位數。例如 `"346"` 的數字和為 `13`，接著處理 `"5"` 得到 `5`，附加過程為：

```plaintext
next = ""
Append(13) → "13"
Append(5)  → "135"
```

零也會正常附加：`"00000000"`、`k = 3` 分成 `000 | 000 | 00`，各組總和都是 `0`，串接後為 `"000"`，長度恰為 `3`，因此回傳 `"000"`。

每輪重新建立 `next`，以 `s` 提供本輪讀取資料、`next` 收集各組結果。直到所有群組處理完成，才執行 `s = next.ToString()`，將結果設為下一輪輸入。這讓本輪的索引始終對應同一份字串。

C# 字串不可變；重新指定區域參數 `s` 只會讓它指向下一輪字串，不會改寫呼叫端原本的字串內容。

停止條件必須每輪重新檢查，不能假設每輪長度一定嚴格縮短。例如 `k = 2` 時，一個 `"99"` 群組的和為 `"18"`，該組的輸出仍有兩個字元；整輪是否繼續，仍取決於新字串長度是否大於 `k`。

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

### 複雜度與設計取捨

令 `L` 為所有實際處理輪次的字元數總和，`n` 為初始字串長度。

- **時間複雜度為 `O(L)`**：外層 `for` 選取群組，內層 `for` 只走訪該組字元；同一輪中，每個字元恰好被讀取一次。上述範例處理長度 `11`、`4` 的字串，因此 `L = 11 + 4 = 15`；最後的 `"135"` 不再進入迴圈。
- **輔助空間為 `O(n)`，結果空間為 `O(n)`**：`StringBuilder` 儲存下一輪內容，並在整輪完成時建立新字串。這是同時需要的空間上界，不是將所有輪次的配置量累加。

這個做法直接對應題意，沒有遞迴堆疊成本，也以一個下一輪緩衝區隔開讀取與寫入。代價是每輪都會建立新字串；這些內容也是下一輪計算所需的資料。

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
