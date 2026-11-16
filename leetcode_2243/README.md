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

## 解法一：`StringBuilder` 迭代分組

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

## 解法二：`Span<char>` 原地覆寫

下方是可貼到 LeetCode 的完整 C# 提交版本，包含 `Solution` 類別與 `CultureInfo` 所需的 `using System.Globalization;`。本機專案以 `DigitSumInPlace` 呼叫同一套演算法。

初始長度已經 `<= k` 時，直接回傳 `s`。其餘情況用 `stackalloc` 在堆疊上配置一塊與輸入等長的字元緩衝區，把 `s` 複製進去。往後每輪都在這塊緩衝區內讀取與寫入，最後只把有效範圍轉成結果字串。

```csharp
using System;
using System.Globalization;

public class Solution
{
    public string DigitSum(string s, int k)
    {
        int length = s.Length;
        if (length <= k)
        {
            return s;
        }

        Span<char> buffer = stackalloc char[length];
        s.AsSpan().CopyTo(buffer);

        while (length > k)
        {
            int writeIndex = 0;
            for (int groupStart = 0; groupStart < length; groupStart += k)
            {
                int groupEnd = Math.Min(groupStart + k, length);
                int sum = 0;

                for (int readIndex = groupStart; readIndex < groupEnd; readIndex++)
                {
                    sum += buffer[readIndex] - '0';
                }

                // 寫入起點不超過群組起點，且總和的位數至多等於群組長度。
                _ = sum.TryFormat(buffer[writeIndex..groupEnd], out int written, provider: CultureInfo.InvariantCulture);
                writeIndex += written;
            }

            length = writeIndex;
        }

        return new string(buffer[..length]);
    }
}
```

### 先讀完整組，再寫回

每輪開始時，`groupStart` 和 `writeIndex` 都從 `0` 出發。長度為 `t` 的群組，其和最多是 `9t`，而 `9t` 至多有 `t` 位。每組的輸出因此最多和該組一樣長。處理到下一組時，累計寫入的字元數 `writeIndex` 至多等於先前已讀取的字元數 `groupStart`。

讀完整組後，`buffer[writeIndex..groupEnd]` 一定能容納總和，而且最後一個可寫位置在 `groupEnd` 之前。`TryFormat` 用十進位直接寫入字元，`written` 是實際寫入的位數，包含總和為 `0` 時寫入的一位。每輪結束後，`length = writeIndex` 將下一輪的讀取範圍限在已寫入的前綴。

以 `s = "9999"`、`k = 2` 為例：

| 輪次 | 讀取範圍 | 讀到的字元 | 數字和 | `writeIndex` 變化 | 寫入後的整塊緩衝區 |
| ---: | --- | --- | ---: | --- | --- |
| 1 | `[0, 2)` | `99` | 18 | `0 → 2` | `1899` |
| 1 | `[2, 4)` | `99` | 18 | `2 → 4` | `1818` |
| 2 | `[0, 2)` | `18` | 9 | `0 → 1` | `9818` |
| 2 | `[2, 4)` | `18` | 9 | `1 → 2` | `9918` |

第 1 輪結束時，有效長度仍是 `4`，因此繼續。第 2 輪結束時，有效長度變成 `2`，因此只取 `[0, 2)` 回傳 `"99"`。緩衝區尾端的 `18` 超出有效範圍。

### 攤銷分析與兩種解法的比較

兩種解法單輪都只掃描一次當輪字元。即使某輪等長，所有輪次合計仍只需掃描 `O(n)` 個字元。令 `D` 為當前字串的數位總和，使用 `Φ = 字串長度 + D / 9` 追蹤剩餘工作量。

例如一組 `99` 變成 `18`，其數位總和從 `18` 降到 `9`，所以即使輸出保留兩個字元，`Φ` 仍下降 `1`。一般來說，若群組和 `x` 有 `d` 位，`x` 減去自身的數位總和至少是 `9(d - 1)`。一位數的差是 `0`，兩位數的差至少是 `9`，三位數的差至少是 `99`。題目中群組和最多三位，因此每組對下一輪 `Φ` 的貢獻，至多是 `1 + x / 9`。

長度為 `m` 的一輪有 `⌈m/k⌉` 組，所以 `Φ` 至少下降 `m - ⌈m/k⌉`。由於 `k >= 2`，這個差至少是 `⌊m/2⌋`。初始 `Φ` 最多是 `2n`，且始終非負，因此全部輪次的掃描字元數總和為 `O(n)`。最壞情況第一輪至少要掃描 `n` 個字元，總時間為 `Θ(n)`。

| 解法 | 最壞總時間 | 輔助空間 | 字串配置 |
| --- | --- | --- | --- |
| `StringBuilder` | `Θ(n)` | `O(n)` | 每輪建立下一輪字串。 |
| `Span<char>` | `Θ(n)` | `O(n)` | 需要處理時只建立最終字串，最多使用 100 個 `char` 的堆疊緩衝區。 |

## Acceptance Harness

`Main` 執行八個確定性案例。每案都將 `DigitSum` 與 `DigitSumInPlace` 的回傳值分別和同一個預期值比較，兩版皆正確才算通過。八項皆成功時，輸出以 `Summary: 8/8 checks passed.` 結尾。任一失敗時，process exit code 為 `1`。

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
Actual (Span): 135
PASS
Case: All zeroes; Input: "00000000", k = 3
Expected: 000
Actual: 000
Actual (Span): 000
PASS
Case: Single character; Input: "1", k = 2
Expected: 1
Actual: 1
Actual (Span): 1
PASS
Case: Already k characters; Input: "123", k = 3
Expected: 123
Actual: 123
Actual (Span): 123
PASS
Case: Two complete groups; Input: "123456", k = 3
Expected: 615
Actual: 615
Actual (Span): 615
PASS
Case: Final short group; Input: "1234567", k = 3
Expected: 127
Actual: 127
Actual (Span): 127
PASS
Case: Multiple rounds; Input: "987654321", k = 2
Expected: 36
Actual: 36
Actual (Span): 36
PASS
Case: Near limit group; Input: "9999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999", k = 99
Expected: 8919
Actual: 8919
Actual (Span): 8919
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
