# LeetCode 2864：Maximum Odd Binary Number（最大二進位奇數）

這是一個以 C# 撰寫的 .NET 10 主控台專案。`MaximumOddBinaryNumber` 使用貪心
策略重新排列輸入位元，方法本身只負責計算並回傳結果；`Main` 提供可重複執行的
acceptance harness，驗證七組完整結果與題目上限的五項關鍵性質。

- [英文題目：2864. Maximum Odd Binary Number](https://leetcode.com/problems/maximum-odd-binary-number/)
- [中文題目：2864. 最大二进制奇数](https://leetcode.cn/problems/maximum-odd-binary-number/)

## 題目說明

給定一個至少包含一個 `1` 的二進位字串 `s`，重新排列其中所有位元，使結果同時：

1. 是奇數，也就是最低位必須為 `1`。
2. 在所有可行排列中具有最大數值。

回傳重新排列後的字串。結果允許包含前導零。

## 限制條件

- `1 <= s.length <= 100`
- `s` 只包含 `0` 與 `1`
- `s` 至少包含一個 `1`
- 實作遵循 LeetCode 的有效輸入契約，不另外定義無效輸入行為

## 核心不變量與容易出錯之處

二進位奇數的最低位一定是 `1`，因此必須先保留一個 `1` 給最後一位。剩餘位元要
形成最大數值，就必須把其餘 `1` 全部放在高位，再把所有 `0` 放在中間：

```plaintext
[其餘的 1][所有的 0][保留的 1]
```

若把所有 `1` 都放在前方，結果可能變成偶數；若在高位留下不必要的 `0`，結果則
不是可行排列中的最大值。輸出必須保留原字串的長度，以及 `0`、`1` 的個數。

## 解法設計

公開介面維持題目契約：

```csharp
public static string MaximumOddBinaryNumber(string s)
```

實作先統計 `1` 的數量，以字串長度減去該數量得到 `0` 的數量。接著依序加入：

1. `ones - 1` 個 `1`
2. 全部 `0`
3. 最後一個 `1`

解法不輸出主控台內容，也不修改輸入；所有顯示、PASS/FAIL 統計與 exit code 都由
`Main` 的驗收器負責。

## 複雜度

- 時間複雜度：`O(n)`，統計與建立結果各線性走訪一次。
- 結果空間：`O(n)`，回傳字串包含與輸入相同的 `n` 個位元。
- 額外輔助空間：`O(n)`，`StringBuilder` 暫存建構中的結果。

## `s = "0101"` 逐步走查

輸入包含兩個 `1` 與兩個 `0`：

| 步驟 | 放置內容 | 暫存結果 |
| ---: | --- | --- |
| 1 | 保留一個 `1` 給最低位 | `""` |
| 2 | 將剩餘一個 `1` 放在最前方 | `"1"` |
| 3 | 將兩個 `0` 放在中間 | `"100"` |
| 4 | 加入保留的最低位 `1` | `"1001"` |

結果 `"1001"` 是使用相同位元能組成的最大二進位奇數。

## 可執行驗證案例

`Main` 共執行八組案例與 12 項檢查：

| 案例 | 輸入 | 檢查數 | 驗證目的 |
| ---: | --- | ---: | --- |
| 1 | `"1"` | 1 | 最小有效輸入 |
| 2 | `"010"` | 1 | 官方範例：唯一的 `1` 必須置底 |
| 3 | `"0101"` | 1 | 官方範例：同時排列多個 `0`、`1` |
| 4 | `"111"` | 1 | 全部為 `1` |
| 5 | `"1000"` | 1 | 單一 `1` 與多個 `0` |
| 6 | `"1100"` | 1 | 偶數排列必須轉為最大奇數 |
| 7 | `"101010"` | 1 | 交錯位元的排列回歸 |
| 8 | 50 個 `1` 加 50 個 `0` | 5 | 長度、位元數、前段、中段與最低位 |

每項檢查都輸出 Expected、Actual 與 PASS/FAIL。若任何檢查失敗，程式會將
`Environment.ExitCode` 設為 1。本專案沒有獨立測試專案或測試框架；可執行驗收器
是目前的驗證方式。

## 建置與執行

請從此 README 所在的外層 `leetcode_2864` 目錄執行：

```bash
dotnet build leetcode_2864/leetcode_2864.csproj --nologo
dotnet run --no-build --project leetcode_2864/leetcode_2864.csproj
```

以下是重新建置後執行第二個命令的完整輸出：

```text
LeetCode 2864 acceptance harness

Case 1: Exact result
Input: s = "1"
PASS | Maximum odd binary number | Expected: 1 | Actual: 1

Case 2: Exact result
Input: s = "010"
PASS | Maximum odd binary number | Expected: 001 | Actual: 001

Case 3: Exact result
Input: s = "0101"
PASS | Maximum odd binary number | Expected: 1001 | Actual: 1001

Case 4: Exact result
Input: s = "111"
PASS | Maximum odd binary number | Expected: 111 | Actual: 111

Case 5: Exact result
Input: s = "1000"
PASS | Maximum odd binary number | Expected: 0001 | Actual: 0001

Case 6: Exact result
Input: s = "1100"
PASS | Maximum odd binary number | Expected: 1001 | Actual: 1001

Case 7: Exact result
Input: s = "101010"
PASS | Maximum odd binary number | Expected: 110001 | Actual: 110001

Case 8: Upper-bound spot checks
Input: 50 ones followed by 50 zeros
PASS | Result length | Expected: 100 | Actual: 100
PASS | Bit counts preserved | Expected: ones=50, zeros=50 | Actual: ones=50, zeros=50
PASS | Leading ones | Expected: 49 | Actual: 49
PASS | Middle zeros | Expected: 00000000000000000000000000000000000000000000000000 | Actual: 00000000000000000000000000000000000000000000000000
PASS | Least-significant bit | Expected: 1 | Actual: 1

Summary: 12/12 checks passed.
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
├── docs/
│   └── readme-template.md
├── leetcode_2864/
│   ├── Program.cs
│   └── leetcode_2864.csproj
├── AGENTS.md
└── README.md
```
