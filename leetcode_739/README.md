# LeetCode 739：Daily Temperatures／每日溫度

這是一個以 C# 撰寫的 .NET 10 主控台專案。核心方法
`DailyTemperatures` 以單調遞減索引堆疊在線性時間計算答案；`Main` 則負責可重複
執行的 acceptance harness 與所有主控台輸出。

- [英文題目：739. Daily Temperatures](https://leetcode.com/problems/daily-temperatures/)
- [中文題目：739. 每日温度](https://leetcode.cn/problems/daily-temperatures/)

## 題目說明

給定每日溫度陣列 `temperatures`，對每一天 `i` 回傳後面第一個溫度嚴格高於
`temperatures[i]` 的日期距離。若之後不存在更高溫，該位置回傳 `0`。

例如 `[73, 74, 75, 71, 69, 72, 76, 73]` 的答案是
`[1, 1, 4, 2, 1, 1, 0, 0]`：第 3 天的 `71` 要等到第 5 天的 `72`，因此等待
天數為 `2`。

## 限制條件

- `1 <= temperatures.Length <= 10^5`
- `30 <= temperatures[i] <= 100`

實作依照題目的有效輸入契約設計，不另外定義無效輸入的行為。

## 單調遞減堆疊不變量

`Stack<int>` 儲存「尚未找到下一個更高溫」的日期索引。由堆疊底部到頂部，對應的
溫度保持非遞增；因此讀到第 `day` 天的溫度時，只要它高於堆疊頂端的溫度，就能確定
頂端索引第一次遇到的更高溫正是今天。

1. 彈出未解決索引 `unresolvedDay`。
2. 寫入 `day - unresolvedDay`，這就是最短等待天數。
3. 持續結算所有比今天低溫的索引，再把今天的索引推入堆疊。
4. 走訪結束仍留在堆疊的索引沒有更高溫，結果陣列的預設值 `0` 即為正確答案。

比較必須使用嚴格的 `>`，因為相同溫度不是「更高」；`[70, 70, 71]` 必須得到
`[2, 1, 0]`，而不是把第一個 `70` 在第二天結算。

公開 API 為：

```csharp
public static int[] DailyTemperatures(int[] temperatures)
```

它只回傳結果，不會修改輸入陣列，也不會直接輸出到主控台。

## 官方範例逐步走查

| 日期 | 溫度 | 處理後未解決索引（底 → 頂） | 新結算結果 |
| ---: | ---: | --- | --- |
| 0 | 73 | `[0]` | 無 |
| 1 | 74 | `[1]` | `answer[0] = 1` |
| 2 | 75 | `[2]` | `answer[1] = 1` |
| 3 | 71 | `[2, 3]` | 無 |
| 4 | 69 | `[2, 3, 4]` | 無 |
| 5 | 72 | `[2, 5]` | `answer[4] = 1`、`answer[3] = 2` |
| 6 | 76 | `[6]` | `answer[5] = 1`、`answer[2] = 4` |
| 7 | 73 | `[6, 7]` | 無 |

最後第 6、7 天後面沒有更高溫，因此保留 `0`，完整答案為
`[1, 1, 4, 2, 1, 1, 0, 0]`。

## 複雜度

- 時間複雜度：`O(n)`。每個索引最多推入與彈出堆疊各一次。
- 結果空間：`O(n)`。回傳陣列與輸入等長。
- 輔助空間：`O(n)`。最壞情況下所有索引都暫存在堆疊中。

## 可執行驗證案例

`Main` 提供 7 組案例、共 10 項檢查。每項都輸出 Input、Expected、Actual 與
PASS/FAIL；任何失敗都會設定 `Environment.ExitCode = 1`。本題沒有獨立的 test
project，acceptance harness 是目前的驗證機制。

| 案例 | 輸入 | 檢查數 | 驗證重點 |
| --- | --- | ---: | --- |
| 1 | `[73, 74, 75, 71, 69, 72, 76, 73]` | 1 | 官方範例與多次等待距離 |
| 2 | `[30]` | 1 | 最小有效輸入 |
| 3 | `[30, 40, 50, 60]` | 1 | 每天立即遇到更高溫 |
| 4 | `[60, 50, 40, 30]` | 1 | 全部保持預設 `0` |
| 5 | `[70, 70, 71]` | 1 | 嚴格高於而非相等溫度 |
| 6 | `[70, 65, 60, 80]` | 1 | 一天連續結算多個未解決索引 |
| 7 | 99,999 個 `30` 後接一個 `100` | 4 | 上限長度、首項、倒數第二項與末項 spot checks |

## 建置與執行

請從此 README 所在的外層 `leetcode_739` 目錄執行：

```bash
dotnet build leetcode_739/leetcode_739.csproj --nologo
dotnet run --no-build --project leetcode_739/leetcode_739.csproj
```

以下是完成建置後執行第二個命令的完整輸出：

```text
LeetCode 739 acceptance harness

Case 1: Official example
Input: [73, 74, 75, 71, 69, 72, 76, 73]
PASS | Waiting days | Expected: [1, 1, 4, 2, 1, 1, 0, 0] | Actual: [1, 1, 4, 2, 1, 1, 0, 0]

Case 2: Minimum valid input
Input: [30]
PASS | Waiting days | Expected: [0] | Actual: [0]

Case 3: Strictly increasing
Input: [30, 40, 50, 60]
PASS | Waiting days | Expected: [1, 1, 1, 0] | Actual: [1, 1, 1, 0]

Case 4: Strictly decreasing
Input: [60, 50, 40, 30]
PASS | Waiting days | Expected: [0, 0, 0, 0] | Actual: [0, 0, 0, 0]

Case 5: Equal temperatures
Input: [70, 70, 71]
PASS | Waiting days | Expected: [2, 1, 0] | Actual: [2, 1, 0]

Case 6: Chained resolution
Input: [70, 65, 60, 80]
PASS | Waiting days | Expected: [3, 2, 1, 0] | Actual: [3, 2, 1, 0]

Case 7: Maximum-length spot checks
Input: 99,999 × 30 followed by 100
PASS | Input length | Expected: 100000 | Actual: 100000
PASS | First waiting days | Expected: 99999 | Actual: 99999
PASS | Penultimate waiting days | Expected: 1 | Actual: 1
PASS | Last waiting days | Expected: 0 | Actual: 0

Summary: 10/10 checks passed.
```

## 專案結構

```plaintext
.
├── .editorconfig              # C# 與結構化檔案格式規範
├── .gitattributes             # 文字與二進位檔案屬性
├── .gitignore                 # .NET／IDE 產生檔案排除規則
├── .vscode/
│   ├── launch.json            # 直接偵錯 net10.0 輸出
│   └── tasks.json             # 預設建置工作
├── docs/
│   └── readme-template.md     # 初次建立 README 的範本
├── leetcode_739/
│   ├── Program.cs             # 純堆疊解法與 acceptance harness
│   └── leetcode_739.csproj    # .NET 10 SDK 專案設定
├── AGENTS.md                  # 本題協作指南
└── README.md                  # 題目、解法與驗證紀錄
```
