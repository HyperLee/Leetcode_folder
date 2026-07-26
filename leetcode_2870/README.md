# LeetCode 2870：使陣列為空的最少操作次數

這是一個以 C# 撰寫的 .NET 10 主控台專案。`MinOperations` 使用雜湊表統計每個
數值的出現次數，再以兩個或三個相同元素為一組，計算清空陣列需要的最少操作數。
解法方法只負責計算；`Main` 提供可重複執行的 acceptance harness。

- [英文題目：2870. Minimum Number of Operations to Make Array Empty](https://leetcode.com/problems/minimum-number-of-operations-to-make-array-empty/)
- [中文題目：2870. 使陣列為空的最少操作次數](https://leetcode.cn/problems/minimum-number-of-operations-to-make-array-empty/)

## 題目說明

給定一個由正整數組成、索引從 0 開始的陣列 `nums`。每次操作可以：

- 刪除兩個值相等的元素；或
- 刪除三個值相等的元素。

回傳清空陣列所需的最少操作次數；若無法清空，回傳 `-1`。

## 限制條件

- `2 <= nums.length <= 10^5`
- `1 <= nums[i] <= 10^6`
- 實作遵循 LeetCode 的有效輸入契約，不另外定義無效輸入的行為。

## 核心不變量

不同數值不能放進同一次刪除操作，因此每個數值的頻率可以獨立計算，最後再把操作
次數相加。對單一頻率 `count`：

- `count == 1`：無法組成兩個或三個一組，整題答案為 `-1`。
- 其餘情況優先取三個一組，因為一次移除三個比一次移除兩個更有效率。
- 若除以 3 仍有餘數，再增加一次操作即可完成分組。

## 餘數 0、1、2 的推導

令 `count = 3q + r`：

| 餘數 `r` | 最少分組方式 | 操作數 |
| ---: | --- | ---: |
| 0 | `q` 組三個 | `q` |
| 1 | `count >= 4`，把一組三個改成兩組兩個 | `q + 1` |
| 2 | `q` 組三個，再加一組兩個 | `q + 1` |

例如頻率 7 不能拆成 `3 + 3 + 1`，必須拆成 `3 + 2 + 2`，所以需要 3 次操作。
這也是驗證器中特別保留的餘數 1 回歸案例。

公開介面如下：

```csharp
public static int MinOperations(int[] nums)
```

方法不寫入主控台、不修改輸入陣列；所有案例輸出與 PASS／FAIL 統計都由 `Main`
負責。

## 複雜度

設 `n` 為陣列長度，`k` 為不同數值的數量：

- 時間複雜度：`O(n + k)`，因為先掃描輸入，再掃描頻率表；由於 `k <= n`，可簡寫為 `O(n)`。
- 結果空間：`O(1)`，方法只回傳一個整數。
- 額外輔助空間：`O(k)`，用 Dictionary 保存每個不同數值的頻率。

## 官方範例逐步說明

輸入：

```plaintext
[2, 3, 3, 2, 2, 4, 2, 3, 4]
```

各數值的頻率與操作數：

| 數值 | 頻率 | 最少分組 | 操作數 |
| ---: | ---: | --- | ---: |
| 2 | 4 | `2 + 2` | 2 |
| 3 | 3 | `3` | 1 |
| 4 | 2 | `2` | 1 |

總操作數為 `2 + 1 + 1 = 4`。

## 可執行驗證案例

`Main` 共執行 12 項檢查：

| 案例 | 驗證內容 | 預期 |
| ---: | --- | ---: |
| 1 | 官方範例一 | 4 |
| 2 | 官方範例二，含單一頻率 | -1 |
| 3 | 最小有效配對，頻率 2 | 1 |
| 4 | 頻率 3 | 1 |
| 5 | 頻率 4 | 2 |
| 6 | 頻率 5 | 2 |
| 7 | 頻率 6 | 2 |
| 8 | 頻率 7 的餘數 1 回歸案例 | 3 |
| 9 | 頻率分別為 3、4、5 的混合資料 | 5 |
| 10 | 可移除群組中混有單一頻率 | -1 |
| 11 | 100,000 個相同值的上限 spot check | 33,334 |
| 12 | 呼叫後輸入陣列保持不變 | 相同 |

每項檢查都輸出輸入、Expected、Actual 與 PASS／FAIL。若任何檢查失敗，程式會將
`Environment.ExitCode` 設為 1。此專案沒有獨立測試專案或測試框架；可執行
驗證器是目前的主要驗證方式。

## 建置與執行

請從此 README 所在的外層 `leetcode_2870` 目錄執行：

```bash
dotnet build leetcode_2870/leetcode_2870.csproj --nologo
dotnet run --no-build --project leetcode_2870/leetcode_2870.csproj
```

以下是重新建置後執行第二個命令的完整輸出：

```text
LeetCode 2870 acceptance harness

Case 1: Official example 1
Input: nums = [2, 3, 3, 2, 2, 4, 2, 3, 4]
PASS | Minimum operations | Expected: 4 | Actual: 4

Case 2: Official example 2
Input: nums = [2, 1, 2, 2, 3, 3]
PASS | Minimum operations | Expected: -1 | Actual: -1

Case 3: Minimum pair
Input: nums = [8, 8]
PASS | Minimum operations | Expected: 1 | Actual: 1

Case 4: One triple
Input: nums = [7, 7, 7]
PASS | Minimum operations | Expected: 1 | Actual: 1

Case 5: Frequency four
Input: nums = [5, 5, 5, 5]
PASS | Minimum operations | Expected: 2 | Actual: 2

Case 6: Frequency five
Input: nums = [6, 6, 6, 6, 6]
PASS | Minimum operations | Expected: 2 | Actual: 2

Case 7: Frequency six
Input: nums = [9, 9, 9, 9, 9, 9]
PASS | Minimum operations | Expected: 2 | Actual: 2

Case 8: Frequency seven remainder regression
Input: nums = [4, 4, 4, 4, 4, 4, 4]
PASS | Minimum operations | Expected: 3 | Actual: 3

Case 9: Mixed frequencies
Input: nums = [1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 3]
PASS | Minimum operations | Expected: 5 | Actual: 5

Case 10: Singleton among removable groups
Input: nums = [1, 1, 1, 2, 2, 3]
PASS | Minimum operations | Expected: -1 | Actual: -1

Case 11: Upper-bound frequency
Input: nums = 100000 copies of 42
PASS | Minimum operations | Expected: 33334 | Actual: 33334

Case 12: Input remains unchanged
Input: nums = [2, 2, 2, 3, 3]
PASS | Input sequence | Expected: [2, 2, 2, 3, 3] | Actual: [2, 2, 2, 3, 3]

Summary: 12/12 checks passed.
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
├── leetcode_2870/
│   ├── Program.cs             # 純 MinOperations 解法與可執行驗證器
│   └── leetcode_2870.csproj   # .NET 10 SDK 專案設定
├── AGENTS.md                  # 本專案協作指南
└── README.md                  # 題目、解法與驗證紀錄
```
