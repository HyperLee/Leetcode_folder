# LeetCode 2024 — Maximize the Confusion of an Exam

> 考試的最大困擾度｜.NET 10 主控台專案

- [English problem](https://leetcode.com/problems/maximize-the-confusion-of-an-exam/)
- [中文題目](https://leetcode.cn/problems/maximize-the-confusion-of-an-exam/)

## 題目說明

給定只包含 `T`（正確）與 `F`（錯誤）的答案字串 `answerKey`，以及整數 `k`。最多可以
變更 `k` 個答案，目標是讓某一段連續答案全部相同，並回傳能得到的最長區段長度。

題目限制：

- `1 <= answerKey.length <= 5 * 10^4`
- `answerKey[i]` 只會是 `T` 或 `F`。
- `1 <= k <= answerKey.length`

## 核心不變量

`MaxConsecutiveChar(answerKey, k, ch)` 將 `ch` 視為允許被替換的字元。滑動視窗
`[left, right]` 內的 `ch` 數量必須始終不超過 `k`；其餘字元本來就相同，因此整個視窗
都能在替換後變成另一種答案。

當加入 `right` 後使 `ch` 數量超過 `k`，便持續右移 `left`，直到視窗重新合法。每個索引
最多被左右指標各走訪一次。

```plaintext
answerKey = "TTFTTFTT", k = 1，ch = 'F'

視窗擴張到 "TTFTT"：包含 1 個 F，可全部變成 T，長度為 5。
再遇到第二個 F 時超出額度，移動 left 直到只剩 1 個 F。
此方向的最長合法視窗為 5。
```

容易出錯的地方：

- `ch` 是要被替換的字元，不是替換後的目標字元。
- 超出額度後必須持續收縮，而不是只移動左界一次。
- 只計算把 `F` 變成 `T` 會漏掉答案主要由 `F` 組成的情況，因此兩個方向都要驗證。
- 最大長度 50,000 的案例仍應維持線性時間，不可列舉所有區段。

## 雙滑動視窗解法

公開入口：

```csharp
public static int MaxConsecutiveAnswers(string answerKey, int k)
```

入口分別呼叫：

```csharp
public static int MaxConsecutiveChar(string answerKey, int k, char ch)
```

第一次以 `ch = 'T'` 尋找最多替換 `k` 個 `T` 後能形成的最長 `F` 區段；第二次以
`ch = 'F'` 尋找最長 `T` 區段，最後取兩者最大值。這保留舊解法清楚區分兩個替換方向的
教學結構。

- 時間複雜度：`O(n)`；兩次線性掃描仍為 `O(n)`。
- 結果空間：`O(1)`。
- 輔助空間：`O(1)`。

## Acceptance Harness

`Main` 執行九個確定性案例。每案驗證公開入口、直接替換 `T` 的 helper 結果，以及直接
替換 `F` 的 helper 結果，共 27 項檢查；任何失敗都會把 process exit code 設為 `1`。
長度 50,000 的輸入只顯示頭尾各 16 個字元，實際比較仍使用完整字串。

| # | 輸入 | `k` | 替換 `T` | 替換 `F` | 整體 | 驗證目的 |
| ---: | --- | ---: | ---: | ---: | ---: | --- |
| 1 | `TTFF` | 2 | 4 | 4 | 4 | 官方案例；兩方向皆可覆蓋整段 |
| 2 | `TFFT` | 1 | 3 | 2 | 3 | 官方案例；兩方向答案不同 |
| 3 | `TTFTTFTT` | 1 | 2 | 5 | 5 | 官方案例；替換 `F` 形成長 `T` 區段 |
| 4 | `T` | 1 | 1 | 1 | 1 | 最小有效輸入 |
| 5 | `FFFF` | 1 | 4 | 1 | 4 | 全部相同且最佳方向不需替換 |
| 6 | `TFTFTF` | 1 | 3 | 3 | 3 | 交錯答案 |
| 7 | `TTFFFTTT` | 1 | 4 | 4 | 4 | 超額後反覆收縮左界 |
| 8 | `TFTF` | 4 | 4 | 4 | 4 | 替換額度等於字串長度 |
| 9 | 長度 50,000 的 `TF` 交錯字串 | 1 | 3 | 3 | 3 | 上限線性掃描與穩定輸出 |

## 建置與執行

已從 repository 根目錄驗證：

```bash
dotnet build leetcode_2024/leetcode_2024/leetcode_2024.csproj --nologo
dotnet run --no-build --project leetcode_2024/leetcode_2024/leetcode_2024.csproj
```

若直接開啟題目根目錄 `leetcode_2024/`，使用：

```bash
dotnet build leetcode_2024/leetcode_2024.csproj --nologo
dotnet run --no-build --project leetcode_2024/leetcode_2024.csproj
```

以下是 fresh run 的完整輸出：

```text
LeetCode 2024 Acceptance Harness
Case: Official example 1
Input: answerKey = "TTFF", k = 2
PASS MaxConsecutiveAnswers result | Expected: 4 | Actual: 4
PASS MaxConsecutiveChar replacing 'T' | Expected: 4 | Actual: 4
PASS MaxConsecutiveChar replacing 'F' | Expected: 4 | Actual: 4

Case: Official example 2
Input: answerKey = "TFFT", k = 1
PASS MaxConsecutiveAnswers result | Expected: 3 | Actual: 3
PASS MaxConsecutiveChar replacing 'T' | Expected: 3 | Actual: 3
PASS MaxConsecutiveChar replacing 'F' | Expected: 2 | Actual: 2

Case: Official example 3
Input: answerKey = "TTFTTFTT", k = 1
PASS MaxConsecutiveAnswers result | Expected: 5 | Actual: 5
PASS MaxConsecutiveChar replacing 'T' | Expected: 2 | Actual: 2
PASS MaxConsecutiveChar replacing 'F' | Expected: 5 | Actual: 5

Case: Minimum input
Input: answerKey = "T", k = 1
PASS MaxConsecutiveAnswers result | Expected: 1 | Actual: 1
PASS MaxConsecutiveChar replacing 'T' | Expected: 1 | Actual: 1
PASS MaxConsecutiveChar replacing 'F' | Expected: 1 | Actual: 1

Case: All answers equal
Input: answerKey = "FFFF", k = 1
PASS MaxConsecutiveAnswers result | Expected: 4 | Actual: 4
PASS MaxConsecutiveChar replacing 'T' | Expected: 4 | Actual: 4
PASS MaxConsecutiveChar replacing 'F' | Expected: 1 | Actual: 1

Case: Alternating answers
Input: answerKey = "TFTFTF", k = 1
PASS MaxConsecutiveAnswers result | Expected: 3 | Actual: 3
PASS MaxConsecutiveChar replacing 'T' | Expected: 3 | Actual: 3
PASS MaxConsecutiveChar replacing 'F' | Expected: 3 | Actual: 3

Case: Window shrink regression
Input: answerKey = "TTFFFTTT", k = 1
PASS MaxConsecutiveAnswers result | Expected: 4 | Actual: 4
PASS MaxConsecutiveChar replacing 'T' | Expected: 4 | Actual: 4
PASS MaxConsecutiveChar replacing 'F' | Expected: 4 | Actual: 4

Case: Replacement budget equals length
Input: answerKey = "TFTF", k = 4
PASS MaxConsecutiveAnswers result | Expected: 4 | Actual: 4
PASS MaxConsecutiveChar replacing 'T' | Expected: 4 | Actual: 4
PASS MaxConsecutiveChar replacing 'F' | Expected: 4 | Actual: 4

Case: Maximum-length alternating input
Input: answerKey = "TFTFTFTFTFTFTFTF...TFTFTFTFTFTFTFTF" (length: 50000), k = 1
PASS MaxConsecutiveAnswers result | Expected: 3 | Actual: 3
PASS MaxConsecutiveChar replacing 'T' | Expected: 3 | Actual: 3
PASS MaxConsecutiveChar replacing 'F' | Expected: 3 | Actual: 3

Summary: 27/27 checks passed.
```

## 專案結構

```plaintext
leetcode_2024/
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
└── leetcode_2024/
    ├── Program.cs
    └── leetcode_2024.csproj
```
