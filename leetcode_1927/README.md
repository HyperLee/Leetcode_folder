# LeetCode 1927：Sum Game（求和遊戲）

這個專案使用 .NET 10 Console App 實作 LeetCode 1927「Sum Game」。程式保留
`SumGame(string num)` 作為主要解法，並在 `Main` 中提供不需要互動輸入的固定測試案例，
方便直接建置、執行與閱讀解題流程。

題目來源：[LeetCode 1927. Sum Game](https://leetcode.com/problems/sum-game/description/)

## 題目說明

Alice 與 Bob 輪流進行遊戲，由 Alice 先手。給定一個偶數長度的字串 `num`，字串只包含
數字與問號 `?`。

每個回合若仍存在 `?`，目前玩家必須：

1. 選擇一個值為 `?` 的位置。
2. 將該位置替換成 `'0'` 到 `'9'` 之間的任一數字。

當所有 `?` 都被替換後，計算 `num` 左半部與右半部的數字總和：

- 若兩半總和相等，Bob 獲勝。
- 若兩半總和不相等，Alice 獲勝。

假設雙方都採用最佳策略，`SumGame` 必須回傳：

- `true`：Alice 可以保證獲勝。
- `false`：Bob 可以保證獲勝。

例如：

- `5023` 沒有可操作的問號，`5 + 0 == 2 + 3`，因此回傳 `false`。
- `25??` 中 Alice 可以先將一個問號填成 `9`，使 Bob 無法再平衡兩側，因此回傳 `true`。

## 限制條件

依照[官方題目限制](https://leetcode.com/problems/sum-game/description/)：

- `2 <= num.length <= 10^5`
- `num.length` 必定是偶數。
- `num` 只包含數字字元與 `?`。

因此解法必須能在線性時間內處理最多 `10^5` 個字元，不能枚舉所有問號的填法或模擬所有遊戲分支。

## 解題概念與出發點

### 將遊戲轉成左右差值

直接模擬遊戲會遇到兩個困難：玩家可以選擇不同的問號位置，而且每次可以填入十種數字。
真正需要判斷的不是完整遊戲過程，而是雙方是否能讓最後的左右總和相等。

令：

- `n0`：左半部目前已知數字的總和。
- `n1`：右半部目前已知數字的總和。
- `q0`：左半部 `?` 的數量。
- `q1`：右半部 `?` 的數量。

已知數字直接形成初始差值 `n0 - n1`；問號則由玩家輪流決定對哪一側增加多少數字。
`Get` 只需要一次掃描，就能取得一半字串的已知總和與問號數量。

### 問號總數為奇數

若 `q0 + q1` 是奇數，Alice 會操作最後一個問號。對最後一個位置來說，最多只有一個數字
能讓左右總和剛好相等，因此 Alice 可以選擇其他數字，保證最後不平衡。

所以問號總數為奇數時，直接回傳 `true`。

### 問號總數為偶數

若問號總數為偶數，Alice 與 Bob 的操作可以分成一組一組的回合來觀察：

- 當兩個問號分別在左右兩側時，Bob 可以依照 Alice 的選擇填入相同數字，抵消這一組對左右差值的影響。
- 當兩個問號位於同一側時，Bob 可以選擇與 Alice 互補的數字，使這兩次填入的總和固定為 `9`。

經過這種配對後，Bob 能保證平衡的條件是：

```text
n0 - n1 = 9 * (q1 - q0) / 2
```

程式不直接做除法，而是將等式兩側同乘 `2`：

```text
2 * (n0 - n1) = 9 * (q1 - q0)
```

這樣可避免整數除法截斷，也能直接比較整數。若等式成立，Bob 可以保證獲勝，
因此 Alice 的回傳結果是條件的相反值：

```text
return 2 * (n0 - n1) != 9 * (q1 - q0);
```

## 解法一：問號計數與數學條件

### 設計流程

1. 以 `num.Length / 2` 將字串切成左右兩半。
2. 呼叫 `Get`，分別取得 `(已知數字總和, 問號數量)`。
3. 若 `q0 + q1` 為奇數，Alice 可以控制最後一步，回傳 `true`。
4. 若 `q0 + q1` 為偶數，使用
   `2 * (n0 - n1) != 9 * (q1 - q0)` 判斷 Alice 是否能獲勝。

### `Get` 的責任

`Get(string s)` 只處理一個半段字串：

- 遇到 `?` 時增加 `questionCount`。
- 遇到數字時，以 `ch - '0'` 轉成整數並加入 `digitSum`。
- 回傳 `(digitSum, questionCount)`。

這個 helper 不需要知道 Alice、Bob 或遊戲策略，只負責整理 `SumGame` 所需的統計資料。

### 範例演示一：沒有問號且兩側平衡

輸入：`5023`

```text
左半部：50  -> n0 = 5, q0 = 0
右半部：23  -> n1 = 5, q1 = 0
問號總數：0，為偶數
2 * (n0 - n1) = 2 * (5 - 5) = 0
9 * (q1 - q0) = 9 * (0 - 0) = 0
```

等式成立，代表 Bob 可以保證平衡，因此 Alice 回傳 `false`。

### 範例演示二：問號總數為奇數

輸入：`?123`

```text
左半部：?1  -> n0 = 1, q0 = 1
右半部：23  -> n1 = 5, q1 = 0
問號總數：1，為奇數
```

Alice 操作最後一個問號，可以避開唯一的平衡數字，因此回傳 `true`。

### 範例演示三：右側多兩個問號且 Bob 無法平衡

輸入：`25??`

```text
左半部：25  -> n0 = 7, q0 = 0
右半部：??  -> n1 = 0, q1 = 2
問號總數：2，為偶數
2 * (n0 - n1) = 2 * (7 - 0) = 14
9 * (q1 - q0) = 9 * (2 - 0) = 18
```

兩側不相等，Bob 無法保證平衡，因此 Alice 回傳 `true`。

### 範例演示四：官方第三個案例

輸入：`?3295???`

```text
左半部：?329  -> n0 = 14, q0 = 1
右半部：5???  -> n1 = 5,  q1 = 3
問號總數：4，為偶數
2 * (n0 - n1) = 2 * (14 - 5) = 18
9 * (q1 - q0) = 9 * (3 - 1) = 18
```

等式成立，Bob 可以透過配對策略保證最後兩側相等，因此回傳 `false`。

## 固定測試案例

`Main` 會直接執行以下九個具名案例，測試官方範例與主要判斷分支：

| 案例 | 輸入 | Expected | 覆蓋重點 |
| --- | --- | --- | --- |
| `Official_5023` | `5023` | `false` | 沒有問號且兩側平衡 |
| `Official_25Question` | `25??` | `true` | 官方案例；右側多兩個問號且無法平衡 |
| `Official_Question3295Questions` | `?3295???` | `false` | 官方案例；偶數問號且符合 Bob 平衡條件 |
| `OddQuestionCount` | `?123` | `true` | 問號總數為奇數 |
| `KnownUnequalSums` | `1234` | `true` | 沒有問號但已知總和不相等 |
| `EvenQuestions_SameKnownSums` | `1?1?` | `false` | 左右各一個問號且已知總和相等 |
| `AllQuestions` | `????????` | `false` | 左右問號數量相等，Bob 可配對 |
| `QuestionsOnLeft` | `??00` | `true` | 問號集中在左側 |
| `QuestionsOnRight` | `00??` | `true` | 問號集中在右側 |

每一筆資料都會列印 `Expected`、`Actual` 與 `PASS` 或 `FAIL`。若任何案例失敗，程式會以非零結束碼結束，方便 shell、CI 或人工檢查發現問題。

## 複雜度分析

- 時間複雜度：`O(n)`，`Get` 只掃描兩半字串各一次。
- 核心計數所需的額外空間：`O(1)`，只使用總和與數量等固定數量的變數。
- 目前 C# 程式以 `Substring` 建立左右兩個半段字串；若將這些暫存字串配置也計入，實際額外記憶體為 `O(n)`。

## 專案結構

```text
leetcode_1927/
├── leetcode_1927/
│   ├── Program.cs                 # 解法、XML 文件與可執行測試入口
│   └── leetcode_1927.csproj       # .NET 10 Console 專案設定
├── .vscode/
│   ├── launch.json                # coreclr 直接偵錯設定
│   └── tasks.json                 # 預設建置工作
├── docs/
│   └── readme-template.md         # README 初次建立範本
└── README.md
```

## 建置與執行

請從本 README 所在的專案目錄執行，並明確指定巢狀專案檔：

### 還原套件

```bash
dotnet restore leetcode_1927/leetcode_1927.csproj
```

### 建置

```bash
dotnet build leetcode_1927/leetcode_1927.csproj --nologo
```

### 執行固定測試案例

建置完成後使用 `--no-build`，確保執行的是剛才建置的結果：

```bash
dotnet run --project leetcode_1927/leetcode_1927.csproj --no-build
```

本專案目前沒有獨立測試框架或測試專案，因此不使用未指定專案路徑的 `dotnet test`；固定案例 harness 是目前的可執行驗證方式。

### 嚴格檢查 XML 文件

若要確認 XML 文件沒有重複標籤或格式錯誤，可執行：

```bash
dotnet build leetcode_1927/leetcode_1927.csproj --nologo -p:GenerateDocumentationFile=true -warnaserror:CS1570,CS1571
```

### 格式與差異檢查

```bash
dotnet format leetcode_1927/leetcode_1927.csproj --verify-no-changes --no-restore
git diff --check
```

README 是新檔案，若要單獨檢查其空白差異，可額外執行：

```bash
git diff --no-index --check /dev/null README.md
```

此命令在檔案與 `/dev/null` 比較時可能以 `1` 表示「存在差異」；只要沒有列出空白或換行診斷，即代表檢查乾淨。

## 實際執行輸出

以下內容應來自建置完成後的全新 `dotnet run --no-build` 執行：

```text
LeetCode 1927 - Sum Game
Official_5023: Input="5023", Expected=False, Actual=False, PASS
Official_25Question: Input="25??", Expected=True, Actual=True, PASS
Official_Question3295Questions: Input="?3295???", Expected=False, Actual=False, PASS
OddQuestionCount: Input="?123", Expected=True, Actual=True, PASS
KnownUnequalSums: Input="1234", Expected=True, Actual=True, PASS
EvenQuestions_SameKnownSums: Input="1?1?", Expected=False, Actual=False, PASS
AllQuestions: Input="????????", Expected=False, Actual=False, PASS
QuestionsOnLeft: Input="??00", Expected=True, Actual=True, PASS
QuestionsOnRight: Input="00??", Expected=True, Actual=True, PASS
Summary: 9/9 PASS
```