# 819. Most Common Word｜最常見的單字

[英文題目](https://leetcode.com/problems/most-common-word/) ·
[中文題目](https://leetcode.cn/problems/most-common-word/)

這個 .NET 10 主控台專案實作 LeetCode 819：給定一段文字與禁用單字清單，在不分
大小寫的前提下，找出出現次數最多、且不在禁用清單中的單字。題目保證合法答案
存在且唯一，回傳值一律為小寫。

## 題目限制

- `1 <= paragraph.length <= 1000`。
- `paragraph` 只包含英文字母、空白與 `!?',;.`。
- 單字比對不分大小寫，答案以小寫回傳。
- `0 <= banned.length <= 100`。
- 至少存在一個未被禁用的單字，且最高頻合法答案唯一。
- 本實作只處理 LeetCode 的有效輸入契約，不另外定義 `null`、空答案或其他無效輸入政策。

## 解法：單次字元掃描

`MostCommonWord` 從左到右掃描 `paragraph`，維持「`currentWord` 只包含目前尚未
結算、且已用不受文化特性影響的規則轉為小寫的連續英文字母」這個不變量。遇到任何非字母時，
該字元就是單字邊界；只有非空單字且不在禁用集合時，才更新頻率字典。

迴圈刻意執行到 `i == paragraph.Length`。這個位置視為虛擬邊界，因此即使最後
一個單字後沒有標點或空白，也會被結算並納入統計。若只在實際非字母出現時
結算，案例 `edge middle middle edge edge` 的最後一個 `edge` 就會遺失。

### 禁用清單與頻率追蹤

1. 先把 `banned` 的每個單字用 `ToLowerInvariant()` 正規化，放進
   `HashSet<string>`。
2. 每讀到字母，就用 `char.ToLowerInvariant` 加入目前單字。
3. 遇到邊界後，略過空單字與禁用單字。
4. 使用 `Dictionary<string, int>` 累加合法單字次數。
5. 次數「嚴格大於」目前最大值時才更新答案；不需要排序整個頻率字典。

## 公開 API 與純函式邊界

```csharp
public static string MostCommonWord(string paragraph, string[] banned)
```

公開方法只讀取輸入並回傳答案，不輸出到主控台，也不修改 `paragraph` 或 `banned`。
所有案例名稱、輸入、預期結果、實際結果與 PASS/FAIL 都由 `Main` 負責，讓解法
維持可提交至 LeetCode 的純計算介面。

## 複雜度

令 `n` 為 `paragraph` 長度、`b` 為禁用輸入的總處理量，`u` 為儲存的不同合法
單字數量：

- 時間複雜度：`O(n + b)`。`paragraph` 掃描一次，`banned` 也只正規化一次。
- 輔助空間複雜度：`O(u + b)`，用於頻率字典、禁用集合與目前單字。
- 實作不使用 `Regex`，也不排序頻率字典。

## 逐步走查

以官方範例為例：

```plaintext
paragraph = "Bob hit a ball, the hit BALL flew far after it was hit."
banned = ["hit"]
```

- `Bob` 正規化為 `bob`，合法次數變為 1，暫時成為答案。
- 三次 `hit` 都命中禁用集合，不進入頻率字典。
- `ball` 與 `BALL` 都正規化為 `ball`；第二次出現時次數變為 2。
- 其餘合法單字各只出現一次，因此最高頻合法單字為 `ball`。

## 驗收程式

專案不建立正式測試專案；`Main` 內的確定性驗收程式是目前的驗證機制。每個案例
各產生一項結果檢查，失敗時會把 `Environment.ExitCode` 設為 1。

| # | 案例 | 驗證重點 | 預期結果 |
|---:|---|---|---|
| 1 | 官方範例 | 官方範例與禁用 `hit` | `ball` |
| 2 | 官方最小範例 | 最短 `paragraph` 與空禁用清單 | `a` |
| 3 | 大小寫正規化 | `paragraph` 與 `banned` 大小寫正規化 | `tea` |
| 4 | 標點邊界 | `!`、`?`、單引號、逗號、分號、句號皆切開單字 | `alpha` |
| 5 | 排除原始最高頻單字 | 排除原始最高頻禁用單字 | `blue` |
| 6 | 最後單字結算 | 無尾端標點時仍結算最後單字 | `edge` |
| 7 | 恰好 1000 字元 | 建構並先確認上限長度，不輸出完整巨大輸入 | `a` |

成功條件是所有七項結果皆為 PASS，最後一行精確顯示
`Summary: 7/7 checks passed.`，且程序結束碼為 0。

## 建置與執行

請把外層 `leetcode_819/` 當成工作目錄。以下命令已用實際巢狀專案路徑驗證：

```bash
dotnet build leetcode_819/leetcode_819.csproj --nologo
dotnet run --no-build --project leetcode_819/leetcode_819.csproj
```

## 最新驗證輸出

以下是執行 `dotnet run --no-build --project leetcode_819/leetcode_819.csproj` 的完整
輸出，也是本 README 唯一的 `text` fence：

```text
LeetCode 819 acceptance harness

Case 1: Official example
Input: paragraph = "Bob hit a ball, the hit BALL flew far after it was hit.", banned = ["hit"]
PASS | Expected: ball | Actual: ball

Case 2: Official minimum example
Input: paragraph = "a.", banned = []
PASS | Expected: a | Actual: a

Case 3: Case normalization
Input: paragraph = "Tea tea coffee COFFEE coffee.", banned = ["COFFEE"]
PASS | Expected: tea | Actual: tea

Case 4: Punctuation boundaries
Input: paragraph = "alpha!beta?alpha'gamma,alpha;beta.gamma.", banned = ["gamma"]
PASS | Expected: alpha | Actual: alpha

Case 5: Exclude raw highest frequency
Input: paragraph = "red red red blue blue green.", banned = ["red"]
PASS | Expected: blue | Actual: blue

Case 6: Final word flush
Input: paragraph = "edge middle middle edge edge", banned = []
PASS | Expected: edge | Actual: edge

Case 7: Exactly 1000 characters
Input: paragraph = 1000 characters (334 x "a ", 165 x "b ", then "a."), banned = []
PASS | Expected: a | Actual: a

Summary: 7/7 checks passed.
```

## 專案結構

```plaintext
leetcode_819/
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
└── leetcode_819/
    ├── Program.cs
    └── leetcode_819.csproj
```
