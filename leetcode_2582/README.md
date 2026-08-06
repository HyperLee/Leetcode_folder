# LeetCode 2582：遞枕頭（Pass the Pillow）

本專案以 .NET 10 console application 示範 LeetCode 2582「遞枕頭」的三種解法，並在 `Main` 中使用固定案例比較每種解法的結果。

- 題目：[Pass the Pillow](https://leetcode.com/problems/pass-the-pillow/description/?envType=daily-question&envId=2024-07-06)
- 中文題目：[遞枕頭](https://leetcode.cn/problems/pass-the-pillow/description/)
- Target framework：`net10.0`

## 題目說明

有 `n` 個人站成一列，編號從 `1` 到 `n`，一開始由 1 號持有枕頭。每經過一秒，持有人就把枕頭傳給相鄰的下一個人。

當枕頭傳到隊伍最右端的 `n` 號後，傳遞方向會反轉，改為往左傳；當枕頭傳回 1 號後，方向再次反轉。給定人數 `n` 與傳遞秒數 `time`，請回傳經過 `time` 秒後持有枕頭的人員編號。

例如 `n = 4`、`time = 5` 時，傳遞路徑為：

```text
1 -> 2 -> 3 -> 4 -> 3 -> 2
```

經過五秒後，答案是 `2`。

## 限制條件

依照[官方題目限制](https://leetcode.com/problems/pass-the-pillow/description/?envType=daily-question&envId=2024-07-06)：

- `2 <= n <= 1000`
- `1 <= time <= 1000`
- 人員編號從 `1` 開始，因此答案一定落在 `1` 到 `n` 之間。
- 題目保證輸入符合以上限制，實作不另外處理不符合題意的輸入。

## 解題概念與出發點

這題的關鍵是辨認傳遞位置會重複出現週期：

```text
1 -> 2 -> ... -> n -> n - 1 -> ... -> 2 -> 1
```

從 1 號走到 `n` 號需要 `n - 1` 秒，從 `n` 號走回 1 號也需要 `n - 1` 秒，因此完整往返週期長度是：

```text
cycleLength = 2 * (n - 1)
```

只要知道 `time` 在這個週期中的位置，就不需要真的模擬所有可能的傳遞秒數。這個專案保留原本的數學公式解，並加入逐秒模擬與週期表兩種方法，對照三種不同的思考方式。

## 解法一：O(1) 往返週期公式

對應方法：`PassThePillow`

### 設計步驟

1. 計算完整週期長度 `2 * (n - 1)`。
2. 將 `time` 對週期長度取餘數，得到目前位於一輪傳遞中的 `position`。
3. 如果 `position < n`，代表仍在從左到右的去程。週期位置 `0` 對應 1 號，因此答案是 `position + 1`。
4. 否則代表正在從右到左的回程。回程從 `n - 1` 號開始遞減，答案是 `n - (position - (n - 1))`。

### 範例演示

以 `n = 4`、`time = 5` 為例：

```text
cycleLength = 2 * (4 - 1) = 6
position = 5 % 6 = 5
```

週期中的位置如下：

| `position` | 0 | 1 | 2 | 3 | 4 | 5 |
| ---: | ---: | ---: | ---: | ---: | ---: | ---: |
| 持有人 | 1 | 2 | 3 | 4 | 3 | 2 |

因為 `position = 5` 不小於 `n = 4`，位於回程：

```text
answer = 4 - (5 - 3) = 2
```

### 複雜度

- 時間複雜度：`O(1)`
- 額外空間複雜度：`O(1)`

這是三種方法中最有效率的版本，適合在只需要答案、不需要列出傳遞過程時使用。

## 解法二：逐秒模擬方向

對應方法：`PassThePillow2`

### 設計步驟

1. 初始持有人設為 `1`，初始方向設為向右 `+1`。
2. 重複 `time` 次：將目前人員編號加上方向。
3. 如果抵達 1 號或 `n` 號，代表下一秒要折返，因此將方向乘以 `-1`。
4. 模擬結束後回傳目前人員編號。

### 範例演示

以 `n = 4`、`time = 5` 為例：

| 秒數 | 傳遞前 | 方向 | 傳遞後 | 端點處理 |
| ---: | ---: | ---: | ---: | --- |
| 1 | 1 | `+1` | 2 | 無 |
| 2 | 2 | `+1` | 3 | 無 |
| 3 | 3 | `+1` | 4 | 抵達右端，下一秒改向左 |
| 4 | 4 | `-1` | 3 | 無 |
| 5 | 3 | `-1` | 2 | 無 |

最後回傳 `2`。這個方法直接按照題目敘述執行，最容易驗證傳遞方向，但當 `time` 增大時需要逐秒處理。

### 複雜度

- 時間複雜度：`O(time)`
- 額外空間複雜度：`O(1)`

## 解法三：建立一輪週期表

對應方法：`PassThePillow3`

### 設計步驟

1. 建立長度為 `2 * (n - 1)` 的陣列。
2. 前半段填入去程 `[1, 2, ..., n]`。
3. 後半段填入回程 `[n - 1, ..., 2]`，避免重複加入端點 `n` 與起點 `1`。
4. 使用 `time % cycleLength` 取得週期陣列索引並回傳對應的人員編號。

### 範例演示

以 `n = 4` 為例，一輪完整路徑是：

```text
cycle = [1, 2, 3, 4, 3, 2]
```

`time = 5` 時：

```text
index = 5 % 6 = 5
answer = cycle[5] = 2
```

這個方法把週期具體保存下來，比直接套公式更容易觀察完整路徑；代價是每次呼叫都要建立陣列。

### 複雜度

- 時間複雜度：`O(n)`
- 額外空間複雜度：`O(n)`

## 三種解法比較

| 方法 | 核心思路 | 時間複雜度 | 額外空間 | 教學重點 |
| --- | --- | --- | --- | --- |
| `PassThePillow` | 週期取模與去程/回程公式 | `O(1)` | `O(1)` | 直接利用數學規律，效能最佳 |
| `PassThePillow2` | 逐秒更新持有人與方向 | `O(time)` | `O(1)` | 最貼近題目敘述，容易追蹤狀態 |
| `PassThePillow3` | 建立 `[1..n..2]` 週期表後取模 | `O(n)` | `O(n)` | 將週期路徑具體化，方便觀察與查找 |

三種方法都使用相同的週期觀察，因此會得到相同答案；差異在於是否逐步執行，以及是否把週期額外保存成陣列。

## Main 測試 harness

`Main` 會建立 9 個符合限制條件的固定案例，並讓三種解法各驗證一次：

| 案例 | `n` | `time` | 預期結果 | 覆蓋情境 |
| --- | ---: | ---: | ---: | --- |
| 官方範例一 | 4 | 5 | 2 | 包含去程與回程 |
| 官方範例二 | 3 | 2 | 3 | 抵達右端點 |
| 最少人數第一秒 | 2 | 1 | 2 | 最小 `n`、第一次傳遞 |
| 最少人數完整多輪 | 2 | 1000 | 1 | 最小週期與最大 `time` |
| 抵達右端點 | 5 | 4 | 5 | `time = n - 1` |
| 折返後第一秒 | 5 | 5 | 4 | 抵達端點後反向 |
| 完整週期回到起點 | 4 | 6 | 1 | `time = 2 * (n - 1)` |
| 最大人數抵達右端點 | 1000 | 999 | 1000 | 最大 `n` 的端點案例 |
| 限制上限折返 | 1000 | 1000 | 999 | 最大 `n` 與最大 `time` |

每個案例會顯示三種方法的實際結果與 `PASS`/`FAIL`。全部 27 項驗證通過時，程式回傳結束碼 `0`；任一驗證失敗時回傳 `1`。程式不使用 `Console.ReadKey()`，因此可以在 CI 或輸出重新導向環境執行。

## 執行方式

請在本專案根目錄 `/Users/qiuzili/Leetcode/Leetcode_folder/leetcode_2582` 執行：

```bash
dotnet restore leetcode_2582/leetcode_2582.csproj
dotnet build leetcode_2582/leetcode_2582.csproj --nologo
dotnet run --project leetcode_2582/leetcode_2582.csproj
```

本專案目前沒有獨立的自動化測試專案，因此以明確 project path 的 restore/build 與可執行的 `Main` harness 作為驗證方式。若要檢查格式與差異，可再執行：

```bash
dotnet format leetcode_2582/leetcode_2582.csproj --verify-no-changes --no-restore
git diff --check
```

## 範例執行結果

以下內容取自 `dotnet run --project leetcode_2582/leetcode_2582.csproj` 的實際執行結果：

```text
案例：官方範例一
n = 4, time = 5
預期 = 2
PassThePillow    實際 = 2 => PASS
PassThePillow2   實際 = 2 => PASS
PassThePillow3   實際 = 2 => PASS

案例：官方範例二
n = 3, time = 2
預期 = 3
PassThePillow    實際 = 3 => PASS
PassThePillow2   實際 = 3 => PASS
PassThePillow3   實際 = 3 => PASS

案例：最少人數第一秒
n = 2, time = 1
預期 = 2
PassThePillow    實際 = 2 => PASS
PassThePillow2   實際 = 2 => PASS
PassThePillow3   實際 = 2 => PASS

案例：最少人數完整多輪
n = 2, time = 1000
預期 = 1
PassThePillow    實際 = 1 => PASS
PassThePillow2   實際 = 1 => PASS
PassThePillow3   實際 = 1 => PASS

案例：抵達右端點
n = 5, time = 4
預期 = 5
PassThePillow    實際 = 5 => PASS
PassThePillow2   實際 = 5 => PASS
PassThePillow3   實際 = 5 => PASS

案例：折返後第一秒
n = 5, time = 5
預期 = 4
PassThePillow    實際 = 4 => PASS
PassThePillow2   實際 = 4 => PASS
PassThePillow3   實際 = 4 => PASS

案例：完整週期回到起點
n = 4, time = 6
預期 = 1
PassThePillow    實際 = 1 => PASS
PassThePillow2   實際 = 1 => PASS
PassThePillow3   實際 = 1 => PASS

案例：最大人數抵達右端點
n = 1000, time = 999
預期 = 1000
PassThePillow    實際 = 1000 => PASS
PassThePillow2   實際 = 1000 => PASS
PassThePillow3   實際 = 1000 => PASS

案例：限制上限折返
n = 1000, time = 1000
預期 = 999
PassThePillow    實際 = 999 => PASS
PassThePillow2   實際 = 999 => PASS
PassThePillow3   實際 = 999 => PASS

總結：27/27 項驗證通過
```

## 專案結構

```text
leetcode_2582/
├── leetcode_2582/
│   ├── Program.cs
│   └── leetcode_2582.csproj
├── docs/
│   └── readme-template.md
├── leetcode_2582.sln
└── README.md
```
