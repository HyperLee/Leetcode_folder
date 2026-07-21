# LeetCode 1657 — 判定兩個字串是否接近

![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4)
![LeetCode 1657](https://img.shields.io/badge/LeetCode-1657-FFA116)

這個 .NET 10 主控台專案示範如何判斷兩個字串能否透過「任意交換字元位置」與「交換兩種既有字元的所有出現位置」互相轉換。專案保留兩種解法：Dictionary + 排序頻率，以及固定 26 格計數陣列。

- [題目說明](#題目說明)
- [解題概念與出發點](#解題概念與出發點)
- [解法一Dictionary--排序頻率](#解法一dictionary--排序頻率)
- [解法二26-格計數陣列](#解法二26-格計數陣列)
- [兩種解法比較](#兩種解法比較)
- [建置與執行](#建置與執行)

## 題目說明

給定兩個字串 `word1` 與 `word2`，可以不限次數使用以下操作：

1. 交換任意兩個既有字元的位置，例如 `abcde -> aecdb`。
2. 選擇兩個已存在的字元，交換它們在整個字串中的所有出現位置。例如在 `aacabb` 中交換 `a` 與 `b`，會得到 `bbcbaa`。

如果能透過這些操作把一個字串變成另一個字串，兩者就是「接近字串」，回傳 `true`；否則回傳 `false`。

題目連結：[1657. Determine if Two Strings Are Close](https://leetcode.com/problems/determine-if-two-strings-are-close/description/)

### 官方範例

| 範例 | `word1` | `word2` | 輸出 | 原因摘要 |
| --- | --- | --- | --- | --- |
| 1 | `"abc"` | `"bca"` | `true` | 只需重新排列字元位置。 |
| 2 | `"a"` | `"aa"` | `false` | 兩字串長度不同，任何允許的操作都不會改變長度。 |
| 3 | `"cabbba"` | `"abbccc"` | `true` | 兩邊字元集合相同，且頻率 `1`、`2`、`3` 可以重新指派給不同字元。 |

### 限制條件

- `1 <= word1.length, word2.length <= 100000`
- `word1` 與 `word2` 只包含小寫英文字母。

## 解題概念與出發點

題目的兩種操作看似能做很多變化，但它們仍然保留一些無法改變的性質。從這些性質出發，比直接模擬每一步轉換更簡單。

### 操作 1 能改變什麼？

任意交換位置代表同一個字串中的字元可以自由重新排列。因此，原本的字元順序不重要；真正重要的是每種字元出現幾次。

例如 `abbc` 可以透過交換位置排列成 `cbba`，但無論怎麼交換，都仍然是：

- `a` 出現 1 次
- `b` 出現 2 次
- `c` 出現 1 次

### 操作 2 能改變什麼？

操作 2 可以交換兩個既有字元的身分。假設某字串中 `a` 出現 2 次、`b` 出現 3 次，交換 `a` 與 `b` 後，就會變成 `a` 出現 3 次、`b` 出現 2 次。

這表示「某個頻率屬於哪個字元」可以改變，但仍有兩個限制：

1. 不能創造原本不存在的字元，也不能讓既有字元完全消失。
2. 頻率數值只能在既有字元之間重新分配，頻率的整體集合不會改變。

### 充分必要條件

因此，兩個字串接近，必須同時滿足：

1. **出現過的字元集合相同。**
2. **所有字元頻率形成的多重集合相同。**

「多重集合」會保留重複次數。例如 `[1, 1, 3]` 與 `[1, 2, 2]` 即使總和相同，仍是不同的頻率多重集合。實作時將頻率排序，就能逐項比較它們是否完全一致。

以 `word1 = "cabbba"`、`word2 = "abbccc"` 為例：

| 字串 | 各字元頻率 | 字元集合 | 排序後頻率 |
| --- | --- | --- | --- |
| `cabbba` | `c:1, a:2, b:3` | `{a,b,c}` | `[1,2,3]` |
| `abbccc` | `a:1, b:2, c:3` | `{a,b,c}` | `[1,2,3]` |

字元集合和排序後頻率都相同，所以答案是 `true`。

反例 `word1 = "uau"`、`word2 = "ssx"` 的排序後頻率都是 `[1,2]`，但字元集合分別為 `{a,u}` 與 `{s,x}`。操作 2 只能交換已存在的字元，無法把第一組字元變成第二組，因此答案仍是 `false`。

## 解法一：Dictionary + 排序頻率

對應方法：`CloseStrings(string word1, string word2)`

### 設計說明

這個解法以 Dictionary 直接表達「字元 → 出現次數」：

1. 先比較兩個字串的長度。長度不同時立即回傳 `false`。
2. 分別走訪兩個字串，建立 `Dictionary<char, int>` 頻率表。
3. 逐一檢查 `word1` 出現過的 key 是否也存在於 `word2` 的 Dictionary；缺少任一字元就回傳 `false`。
4. 將兩個 Dictionary 的所有 value 放入各自的 List。
5. 將兩份頻率 List 排序。
6. 逐項比較排序結果；任一位置不同就回傳 `false`，全部相同則回傳 `true`。

長度已相同，且 `word1` 的每個字元都能在 `word2` 找到時，兩邊不同字元的數量也會一致，因此接著可以安全比較兩份頻率 List。

### 範例演示：`"cabbba"` 與 `"abbccc"`

建立第一份 Dictionary：

| 讀取字元 | `dic1` 狀態 |
| --- | --- |
| `c` | `{ c:1 }` |
| `a` | `{ c:1, a:1 }` |
| `b` | `{ c:1, a:1, b:1 }` |
| `b` | `{ c:1, a:1, b:2 }` |
| `b` | `{ c:1, a:1, b:3 }` |
| `a` | `{ c:1, a:2, b:3 }` |

第二個字串得到 `dic2 = { a:1, b:2, c:3 }`。接著檢查字元集合：`c`、`a`、`b` 都存在於 `dic2`，第一個條件成立。

最後只取出 Dictionary 的 value 並排序：

```text
dic1 values: [1, 2, 3] -> 排序後 [1, 2, 3]
dic2 values: [1, 2, 3] -> 排序後 [1, 2, 3]
```

兩份 List 逐項相等，因此回傳 `true`。這裡不要求 `a` 在兩邊具有相同頻率，因為操作 2 可以將頻率重新指派給其他既有字元。

### 為什麼正確

- Dictionary 的 key 完整記錄字串中出現過的字元，所以 key 存在性檢查能驗證字元集合相同。
- Dictionary 的 value 完整記錄每個字元的頻率；排序後逐項相等，等價於兩邊頻率多重集合相同。
- 這兩項正是接近字串的充分必要條件，因此兩項都成立時回傳 `true`，任一項失敗時回傳 `false`。

### 複雜度

令 `n` 為字串長度、`k` 為不同字元數量；由於輸入只有小寫英文字母，`k <= 26`。

- 時間複雜度：`O(n + k log k)`，建立頻率表需要 `O(n)`，排序頻率需要 `O(k log k)`；在本題固定字母表下可視為 `O(n)`。
- 空間複雜度：`O(k)`；在固定 26 個小寫字母下可視為 `O(1)`。

## 解法二：26 格計數陣列

對應方法：`CloseStrings2(string word1, string word2)`

### 設計說明

因為輸入限定為 `a` 到 `z`，可以用長度固定為 26 的 `int[]` 取代 Dictionary：

1. 建立 `count1` 與 `count2`，索引 `0` 代表 `a`、索引 `1` 代表 `b`，依此類推。
2. 走訪 `word1`，以 `count1[c - 'a']++` 累計頻率；`word2` 也採相同方式。
3. 檢查每個索引是否在兩邊同時為零或同時大於零。只有一邊大於零時，表示字元集合不同，立即回傳 `false`。
4. 排序兩個 26 格陣列，使所有頻率依數值排列；未出現字元的 `0` 也會一起排序到前方。
5. 使用 `Enumerable.SequenceEqual` 比較完整陣列，相同時回傳 `true`，否則回傳 `false`。

雖然陣列中包含未出現字元的 0，但兩份陣列長度固定相同，而且前一步已驗證哪些字元存在，因此保留並比較這些 0 不會改變判斷結果。

### 範例演示：`"cabbba"` 與 `"abbccc"`

只列出 `a`、`b`、`c` 三個有關的索引：

| 陣列 | `a` | `b` | `c` |
| --- | ---: | ---: | ---: |
| `count1`（`cabbba`） | 2 | 3 | 1 |
| `count2`（`abbccc`） | 1 | 2 | 3 |

存在性檢查時，`a`、`b`、`c` 在兩邊都大於 0，其餘 23 個字母在兩邊都等於 0，所以字元集合條件成立。

完整 26 格陣列排序後為：

```text
count1: [0 × 23, 1, 2, 3]
count2: [0 × 23, 1, 2, 3]
```

`SequenceEqual` 比較每一格都相等，因此回傳 `true`。

### 反例演示：`"uau"` 與 `"ssx"`

| 字串 | 非零計數 |
| --- | --- |
| `uau` | `a:1, u:2` |
| `ssx` | `s:2, x:1` |

兩邊排序後的非零頻率雖然都是 `[1,2]`，但檢查索引 `a` 時會發現 `count1['a'] > 0`、`count2['a'] == 0`，方法會在排序前回傳 `false`。這正是字元集合檢查不可省略的原因。

### 為什麼正確

- 每個陣列索引固定代表一個字母，因此「是否為零」可以精確表示該字母有沒有出現。
- 逐索引存在性檢查保證兩邊字元集合一致。
- 排序完整計數陣列後使用 `SequenceEqual`，可以精確驗證頻率多重集合一致。
- 兩個充分必要條件都由固定陣列直接驗證，因此回傳結果正確。

### 複雜度

- 時間複雜度：`O(n + 26 log 26)`，簡化為 `O(n)`。
- 空間複雜度：兩個固定 26 格陣列，即 `O(1)`。

## 兩種解法比較

| 比較項目 | Dictionary + 排序頻率 | 26 格計數陣列 |
| --- | --- | --- |
| 對應方法 | `CloseStrings` | `CloseStrings2` |
| 頻率儲存方式 | `Dictionary<char, int>` | `int[26]` |
| 字元集合檢查 | 檢查 Dictionary key | 比較相同索引是否同時出現 |
| 頻率集合檢查 | 將 value 放入 List、排序後逐項比較 | 排序完整陣列後使用 `SequenceEqual` |
| 時間複雜度 | `O(n + k log k)`，本題為 `O(n)` | `O(n + 26 log 26)`，即 `O(n)` |
| 空間複雜度 | `O(k)`，本題可視為 `O(1)` | `O(1)` |
| 優點 | 語意直接，也容易延伸至較大的字元集合 | 結構固定、配置可預期，特別適合限定小寫字母的題目 |
| 取捨 | 需要 Dictionary 與兩份 List | 依賴輸入只能是 `a` 到 `z` 的題目限制 |

若只考慮本題限制，26 格計數陣列較精簡且常數成本穩定；Dictionary 解法則更直接呈現「字元對應頻率」的概念，對理解充分必要條件很有幫助。

## 建置與執行

需求：安裝 [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)。

在此 README 所在的專案根目錄執行：

```powershell
dotnet build .\leetcode_1657\leetcode_1657.csproj
dotnet run --project .\leetcode_1657\leetcode_1657.csproj
```

目前 runner 會讓兩種解法執行三組官方範例及一組字元集合反例。實際輸出如下：

```text
Example 1: word1 = "abc", word2 = "bca", Expected = True
  CloseStrings:  Actual = True (PASS)
  CloseStrings2: Actual = True (PASS)

Example 2: word1 = "a", word2 = "aa", Expected = False
  CloseStrings:  Actual = False (PASS)
  CloseStrings2: Actual = False (PASS)

Example 3: word1 = "cabbba", word2 = "abbccc", Expected = True
  CloseStrings:  Actual = True (PASS)
  CloseStrings2: Actual = True (PASS)

Character set mismatch: word1 = "uau", word2 = "ssx", Expected = False
  CloseStrings:  Actual = False (PASS)
  CloseStrings2: Actual = False (PASS)
```

## 專案結構

```text
leetcode_1657/
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── docs/
│   └── readme-template.md
├── leetcode_1657/
│   ├── leetcode_1657.csproj
│   └── Program.cs
├── AGENTS.md
└── README.md
```

- `Program.cs`：原始雙語題目說明、兩種解法與固定 PASS/FAIL 範例。
- `leetcode_1657.csproj`：目標框架為 `net10.0` 的主控台專案設定。
- `docs/readme-template.md`：建立 README 時使用的內容與驗證指引。
- `.vscode/`：從 VS Code 直接建置與偵錯巢狀主控台專案的設定。
