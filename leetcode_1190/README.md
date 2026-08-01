# LeetCode 1190 — 反轉每對括號間的子字串

![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4)
![Difficulty Medium](https://img.shields.io/badge/Difficulty-Medium-F9A825)

這是一個可直接執行的 .NET 10 主控台教學專案，示範如何由內而外反轉每對括號中的字元。本專案保留原本的「堆疊保存前綴」解法，並加入時間複雜度為 `O(n)` 的「括號配對跳躍」解法；`Main` 會用固定案例同時驗證兩種實作。

## 快速導覽

- [題目說明](#題目說明)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：堆疊保存前綴](#解法一堆疊保存前綴)
- [解法二：括號配對與方向切換](#解法二括號配對與方向切換)
- [執行與驗證](#執行與驗證)

## 題目說明

給定字串 `s`，其中只包含小寫英文字母、左括號 `(` 與右括號 `)`。需要從最內層括號開始，反轉每一對括號之間的字元，最後回傳不含任何括號的結果。

例如：

```text
輸入：s = "(u(love)i)"
輸出："iloveu"
```

處理順序是：

1. 最內層 `(love)` 反轉為 `evol`，得到 `(uevoli)`。
2. 外層 `uevoli` 再反轉為 `iloveu`。
3. 移除所有括號後得到答案。

題目連結：

- [LeetCode 1190 — Reverse Substrings Between Each Pair of Parentheses](https://leetcode.com/problems/reverse-substrings-between-each-pair-of-parentheses/)
- [力扣 1190 — 反轉每對括號間的子串](https://leetcode.cn/problems/reverse-substrings-between-each-pair-of-parentheses/)

### 限制條件

- `1 <= s.length <= 2000`
- `s` 只包含小寫英文字母及括號。
- 所有括號保證正確配對。

因此演算法可以直接依照括號必定平衡的條件操作，不需要處理缺少配對括號或 `null` 等題目範圍外的輸入。

## 解題概念與出發點

真正的關鍵不只是「反轉字串」，而是如何表達巢狀括號造成的方向變化。

- 遇到左括號時，代表進入一個新的內層區段。
- 遇到右括號時，最內層區段已完整，可以先完成它的反轉。
- 每跨越一層括號，閱讀方向就會反轉一次；跨越偶數層後方向恢復，跨越奇數層後方向相反。

本專案用兩種不同角度實作這個規則：

| 解法 | 核心做法 | 時間複雜度 | 空間複雜度 |
| --- | --- | --- | --- |
| 堆疊保存前綴 | 每完成一層括號就實際反轉目前字串 | 最壞 `O(n²)` | `O(n)` |
| 括號配對跳躍 | 不搬動中間字元，改變索引走訪方向 | `O(n)` | `O(n)` |

## 解法一：堆疊保存前綴

### 設計說明

`ReverseParentheses` 使用兩個主要容器：

- `Stack<string> outerPrefixes`：保存每個左括號之前、暫時不能完成的外層前綴。
- `StringBuilder current`：保存目前所在括號層級已讀取的字元。

走訪每個字元時分成三種情況：

1. 遇到 `(`：把目前前綴推入堆疊，清空 `current`，開始收集新的內層片段。
2. 遇到 `)`：目前片段必定是尚未處理的最內層，先原地反轉，再從堆疊取回上一層前綴並接在前方。
3. 遇到英文字母：直接加入 `current`。

堆疊的後進先出特性與「最內層括號最先完成」完全一致。每次右括號處理完成後，`current` 都代表目前已解析部分的正確結果；最後走訪結束時，它就是完整答案。

### 範例演示流程

以 `s = "(u(love)i)"` 為例：

| 讀到的內容 | `outerPrefixes` | `current` | 說明 |
| --- | --- | --- | --- |
| `(` | `[""]` | `""` | 保存最外層空前綴 |
| `u` | `[""]` | `"u"` | 收集字母 |
| `(` | `["", "u"]` | `""` | 保存 `u`，進入內層 |
| `love` | `["", "u"]` | `"love"` | 收集內層字母 |
| `)` | `[""]` | `"uevol"` | 反轉成 `evol`，接回前綴 `u` |
| `i` | `[""]` | `"uevoli"` | 繼續收集外層字母 |
| `)` | `[]` | `"iloveu"` | 反轉外層並接回空前綴 |

### 複雜度分析

- 時間複雜度：最壞 `O(n²)`。巢狀括號可能讓逐漸增長的片段被重複反轉及插入。
- 空間複雜度：`O(n)`。堆疊、目前字串及反轉內容的總規模與輸入長度成正比。

## 解法二：括號配對與方向切換

### 設計說明

`ReverseParentheses2` 將「反轉字元」改寫成「反向走訪字元」，分成兩趟處理：

1. 第一趟掃描字串：
   - 遇到 `(` 就把索引推入 `Stack<int>`。
   - 遇到 `)` 就取出最近的左括號索引。
   - 在 `matchingParenthesis` 陣列中建立雙向配對，例如 `2 ↔ 7`。
2. 第二趟使用 `index` 與 `direction` 走訪：
   - 一般字母直接加入答案。
   - 遇到任一括號時，跳到配對括號索引，並令 `direction = -direction`。
   - 括號本身不加入輸出。

每對括號都像一個傳送點：從一端跳到另一端後，走訪方向反轉。這正好模擬括號內字串被反轉的效果，而且不需要反覆搬動已讀取的字元。

### 範例演示流程

同樣使用 `s = "(u(love)i)"`。第一趟會得到括號配對 `0 ↔ 9`、`2 ↔ 7`。

| 當前索引／字元 | 動作 | 下一步方向 | 已輸出字串 |
| --- | --- | --- | --- |
| `0 / (` | 跳到索引 `9` | 向左 | `""` |
| `8 / i` | 加入 `i` | 向左 | `"i"` |
| `7 / )` | 跳到索引 `2` | 向右 | `"i"` |
| `3..6 / love` | 依序加入字母 | 向右 | `"ilove"` |
| `7 / )` | 跳回索引 `2` | 向左 | `"ilove"` |
| `1 / u` | 加入 `u` | 向左 | `"iloveu"` |
| `0 / (` | 跳到索引 `9` | 向右 | `"iloveu"` |

索引接著離開字串範圍，答案即為 `iloveu`。

### 複雜度分析

- 時間複雜度：`O(n)`。建立配對與產生結果各完整掃描一次字串。
- 空間複雜度：`O(n)`。索引配對陣列、左括號堆疊與輸出字串皆不超過輸入規模。

## 測試案例設計

`Main` 固定執行七組資料，每組都驗證兩種解法：

| 案例 | 驗證重點 |
| --- | --- |
| 四組官方範例 | 單層、巢狀、多層與括號外字元 |
| `"a"` | 長度下界及沒有括號 |
| `"(ab)(cd)"` | 彼此相鄰但互不巢狀的括號區段 |
| 2000 個 `a` | 題目長度上界及穩定輸出格式 |

比較時會使用完整字串；只有主控台顯示長度 2000 的案例時，才縮略為前後各二十個字元，避免輸出紀錄失去可讀性。任何一項失敗都會讓程式設定非零結束碼。

## 執行與驗證

請從本 repository 根目錄執行：

```bash
dotnet restore leetcode_1190/leetcode_1190.csproj
dotnet build leetcode_1190/leetcode_1190.csproj --nologo
dotnet run --project leetcode_1190/leetcode_1190.csproj --no-build
```

目前沒有獨立的自動化測試專案；驗收方式是成功建置，加上 `Main` 中可重複執行的 Expected/Actual/PASS-FAIL 案例。

### 實際執行結果

```text
案例：官方範例一
輸入：s = "(abcd)"
預期："dcba"
解法一（堆疊保存前綴）實際："dcba" => PASS
解法二（括號配對跳躍）實際："dcba" => PASS

案例：官方範例二
輸入：s = "(u(love)i)"
預期："iloveu"
解法一（堆疊保存前綴）實際："iloveu" => PASS
解法二（括號配對跳躍）實際："iloveu" => PASS

案例：官方範例三
輸入：s = "(ed(et(oc))el)"
預期："leetcode"
解法一（堆疊保存前綴）實際："leetcode" => PASS
解法二（括號配對跳躍）實際："leetcode" => PASS

案例：官方範例四
輸入：s = "a(bcdefghijkl(mno)p)q"
預期："apmnolkjihgfedcbq"
解法一（堆疊保存前綴）實際："apmnolkjihgfedcbq" => PASS
解法二（括號配對跳躍）實際："apmnolkjihgfedcbq" => PASS

案例：長度下界且沒有括號
輸入：s = "a"
預期："a"
解法一（堆疊保存前綴）實際："a" => PASS
解法二（括號配對跳躍）實際："a" => PASS

案例：相鄰括號區段
輸入：s = "(ab)(cd)"
預期："badc"
解法一（堆疊保存前綴）實際："badc" => PASS
解法二（括號配對跳躍）實際："badc" => PASS

案例：長度 2000 上界
輸入：s = "aaaaaaaaaaaaaaaaaaaa…aaaaaaaaaaaaaaaaaaaa"（長度 2000）
預期："aaaaaaaaaaaaaaaaaaaa…aaaaaaaaaaaaaaaaaaaa"（長度 2000）
解法一（堆疊保存前綴）實際："aaaaaaaaaaaaaaaaaaaa…aaaaaaaaaaaaaaaaaaaa"（長度 2000） => PASS
解法二（括號配對跳躍）實際："aaaaaaaaaaaaaaaaaaaa…aaaaaaaaaaaaaaaaaaaa"（長度 2000） => PASS

總結：14/14 項驗證通過。
```

## 專案結構

```text
leetcode_1190/
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_1190.sln
└── leetcode_1190/
    ├── leetcode_1190.csproj
    └── Program.cs
```

- `Program.cs`：題目 XML、兩種演算法與固定案例進入點。
- `leetcode_1190.csproj`：以 `net10.0` 為目標的主控台專案設定。
- `docs/readme-template.md`：本 README 採用的 repository 文件指引。
