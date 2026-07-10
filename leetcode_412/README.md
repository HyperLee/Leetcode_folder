# LeetCode 412：Fizz Buzz

這是一個以 C# 撰寫的 .NET 10 主控台專案，使用單一條件判斷解法產生
Fizz Buzz 序列。`FizzBuzz` 方法只負責計算並回傳結果，`Main` 則提供可重複
執行的 acceptance harness，驗證五組完整序列與題目上限的六個關鍵值。

- [英文題目：412. Fizz Buzz](https://leetcode.com/problems/fizz-buzz/)
- [中文題目：412. Fizz Buzz](https://leetcode.cn/problems/fizz-buzz/)

## 題目說明

給定整數 `n`，建立一個從 1 到 `n` 的字串序列。對每個整數 `i`：

- 若 `i` 同時是 3 和 5 的倍數，加入 `"FizzBuzz"`。
- 否則，若 `i` 是 3 的倍數，加入 `"Fizz"`。
- 否則，若 `i` 是 5 的倍數，加入 `"Buzz"`。
- 其餘情況加入 `i` 的十進位字串。

回傳的集合必須保持 1 到 `n` 的原始順序；`FizzBuzz` 本身不輸出任何內容。

## 限制條件

- `1 <= n <= 10^4`
- 實作依照 LeetCode 的有效輸入契約設計，不另外定義無效輸入的行為。

## 判斷順序為什麼重要

15、30 等數字同時可以被 3 和 5 整除。若先判斷 `i % 3 == 0`，15 會過早
得到 `"Fizz"`，後續便沒有機會變成 `"FizzBuzz"`。因此條件必須依序判斷：

1. `i % 15 == 0`：同時為 3 與 5 的倍數，輸出 `"FizzBuzz"`。
2. `i % 3 == 0`：只需處理剩餘的 3 倍數，輸出 `"Fizz"`。
3. `i % 5 == 0`：只需處理剩餘的 5 倍數，輸出 `"Buzz"`。
4. 其餘數字轉成字串。

這個「先處理最具體條件」的順序，是整個解法的核心不變量。

## 零起始索引對應

題目中的整數從 1 開始，但 C# 陣列索引從 0 開始，因此整數 `i` 必須寫入
`result[i - 1]`：

| 題目整數 `i` | 陣列索引 `i - 1` | 儲存值 |
| ---: | ---: | --- |
| 1 | 0 | `"1"` |
| 3 | 2 | `"Fizz"` |
| 5 | 4 | `"Buzz"` |
| 15 | 14 | `"FizzBuzz"` |

這也說明上限驗證中「value for 10000」要讀取索引 `9999`。

## 解法設計

`FizzBuzz` 先建立長度為 `n` 的字串陣列，再讓 `i` 從 1 走到 `n`。每次迭代
只做固定次數的取餘數、條件判斷與一次指定位置寫入，最後直接回傳該陣列。
方法不寫入主控台，也不修改外部狀態；所有顯示與通過／失敗統計都留在
`Main` 的 acceptance harness。

公開介面如下：

```csharp
public static IList<string> FizzBuzz(int n)
```

## 複雜度

- 時間複雜度：`O(n)`，每個整數恰好處理一次。
- 結果空間：`O(n)`，回傳集合必須保存 `n` 個字串。
- 額外輔助空間：`O(1)`，不計回傳集合，只使用固定數量的區域變數。

## `n = 15` 詳細走查

| `i` | 15 的倍數 | 3 的倍數 | 5 的倍數 | 寫入 `result[i - 1]` |
| ---: | :---: | :---: | :---: | --- |
| 1 | 否 | 否 | 否 | `"1"` |
| 2 | 否 | 否 | 否 | `"2"` |
| 3 | 否 | 是 | 否 | `"Fizz"` |
| 4 | 否 | 否 | 否 | `"4"` |
| 5 | 否 | 否 | 是 | `"Buzz"` |
| 6 | 否 | 是 | 否 | `"Fizz"` |
| 7 | 否 | 否 | 否 | `"7"` |
| 8 | 否 | 否 | 否 | `"8"` |
| 9 | 否 | 是 | 否 | `"Fizz"` |
| 10 | 否 | 否 | 是 | `"Buzz"` |
| 11 | 否 | 否 | 否 | `"11"` |
| 12 | 否 | 是 | 否 | `"Fizz"` |
| 13 | 否 | 否 | 否 | `"13"` |
| 14 | 否 | 否 | 否 | `"14"` |
| 15 | 是 | 是 | 是 | `"FizzBuzz"` |

完成後得到：

```plaintext
["1", "2", "Fizz", "4", "Buzz", "Fizz", "7", "8", "Fizz", "Buzz", "11", "Fizz", "13", "14", "FizzBuzz"]
```

## 可執行驗證案例

`Main` 共執行六組案例與 11 項檢查：

| 案例 | 輸入 | 檢查數 | 驗證內容 |
| ---: | ---: | ---: | --- |
| 1 | `n = 1` | 1 | 最小輸入的完整序列 |
| 2 | `n = 3` | 1 | 第一個 `Fizz` 的完整序列 |
| 3 | `n = 5` | 1 | 第一個 `Buzz` 的完整序列 |
| 4 | `n = 15` | 1 | 第一個 `FizzBuzz` 的完整序列 |
| 5 | `n = 16` | 1 | `FizzBuzz` 後恢復一般數字 |
| 6 | `n = 10000` | 6 | 長度，以及 1、3、5、15、10000 對應值 |

每項檢查都輸出預期值、實際值與 `PASS`／`FAIL`。若任何檢查失敗，程式會將
`Environment.ExitCode` 設為 1。此專案沒有獨立測試專案或測試框架；可執行
驗證器是目前的主要驗證方式。

## 建置與執行

請從此 README 所在的外層 `leetcode_412` 目錄執行：

```bash
dotnet build leetcode_412/leetcode_412.csproj --nologo
dotnet run --no-build --project leetcode_412/leetcode_412.csproj
```

以下是重新建置後執行第二個命令的完整輸出：

```text
LeetCode 412 acceptance harness

Case 1: Full sequence
Input: n = 1
PASS | Full sequence | Expected: ["1"] | Actual: ["1"]

Case 2: Full sequence
Input: n = 3
PASS | Full sequence | Expected: ["1", "2", "Fizz"] | Actual: ["1", "2", "Fizz"]

Case 3: Full sequence
Input: n = 5
PASS | Full sequence | Expected: ["1", "2", "Fizz", "4", "Buzz"] | Actual: ["1", "2", "Fizz", "4", "Buzz"]

Case 4: Full sequence
Input: n = 15
PASS | Full sequence | Expected: ["1", "2", "Fizz", "4", "Buzz", "Fizz", "7", "8", "Fizz", "Buzz", "11", "Fizz", "13", "14", "FizzBuzz"] | Actual: ["1", "2", "Fizz", "4", "Buzz", "Fizz", "7", "8", "Fizz", "Buzz", "11", "Fizz", "13", "14", "FizzBuzz"]

Case 5: Full sequence
Input: n = 16
PASS | Full sequence | Expected: ["1", "2", "Fizz", "4", "Buzz", "Fizz", "7", "8", "Fizz", "Buzz", "11", "Fizz", "13", "14", "FizzBuzz", "16"] | Actual: ["1", "2", "Fizz", "4", "Buzz", "Fizz", "7", "8", "Fizz", "Buzz", "11", "Fizz", "13", "14", "FizzBuzz", "16"]

Case 6: Upper-bound spot checks
Input: n = 10000
PASS | Result count | Expected: 10000 | Actual: 10000
PASS | Value for 1 | Expected: 1 | Actual: 1
PASS | Value for 3 | Expected: Fizz | Actual: Fizz
PASS | Value for 5 | Expected: Buzz | Actual: Buzz
PASS | Value for 15 | Expected: FizzBuzz | Actual: FizzBuzz
PASS | Value for 10000 | Expected: Buzz | Actual: Buzz

Summary: 11/11 checks passed.
```

## 專案結構

```plaintext
.
├── .editorconfig              # C# 與結構化檔案的格式規範
├── .gitattributes             # 文字與二進位檔案屬性
├── .gitignore                 # .NET／IDE 產生檔案排除規則
├── .vscode/
│   ├── launch.json            # 直接偵錯 net10.0 輸出
│   └── tasks.json             # 預設建置工作
├── docs/
│   └── readme-template.md     # 初次建立 README 的範本
├── leetcode_412/
│   ├── Program.cs             # 純 FizzBuzz 解法與可執行驗證器
│   └── leetcode_412.csproj    # .NET 10 SDK 專案設定
├── AGENTS.md                  # 本專案協作指南
└── README.md                  # 題目、解法與驗證紀錄
```
