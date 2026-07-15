# LeetCode 767：Reorganize String／重構字串

這是一個以 C# 撰寫的 .NET 10 主控台專案，使用字母計數與偶數／奇數索引配置，
建立任兩個相鄰字元皆不同的字串排列。

- [英文題目：767. Reorganize String](https://leetcode.com/problems/reorganize-string/)
- [中文題目：767. 重構字串](https://leetcode.cn/problems/reorganize-string/)

## 題目說明

給定一個只含小寫英文字母的字串 `s`，重新排列其中所有字元，使任兩個相鄰字元
都不相同。若存在合法排列，回傳任一種排列；若不存在，回傳空字串。

## 限制條件

- `1 <= s.Length <= 500`
- `s` 只含小寫英文字母。
- 實作只處理題目契約內的有效輸入，不另外定義無效輸入的例外行為。

## 可行性不變量

令最高字元頻率為 `maximumCount`。若想隔開所有最高頻字元，最多能使用字串中的
偶數索引位置，其數量為 `(s.Length + 1) / 2`。因此：

```csharp
maximumCount <= (s.Length + 1) / 2
```

是存在答案的充要條件。超過門檻時，即使把其他字元全放在最高頻字元之間，仍會
留下至少一組相鄰重複，必須回傳空字串。

## 演算法與取捨

1. 以長度 26 的陣列統計每個小寫英文字母的出現次數。
2. 找出最高頻字元；若超過可行性門檻，立即回傳空字串。
3. 先把最高頻字元放在索引 `0, 2, 4, ...`。
4. 依字母順序放置其他字元：先填完剩餘偶數索引，越界後從索引 `1` 開始填奇數
   索引。
5. 將結果字元陣列轉成字串。

最高頻字元先占據間隔最大的偶數位置；其餘字元沿相同序列填入並在適當時機轉到
奇數位置，可維持相鄰字元不同。這種作法比優先佇列更直接，且因字母表固定為 26
個字母，不需要額外的堆積結構；取捨是它依賴本題固定的小寫英文字母契約。

- 時間複雜度：`O(n + 26)`
- 輔助空間（不含輸出）：`O(26)`
- 輸出空間：`O(n)`

公開方法 `ReorganizeString` 不修改輸入且不寫入主控台；所有輸出只由 `Main`
負責。

## `aaabc` 逐步走查

`s.Length = 5`，可行性門檻為 `(5 + 1) / 2 = 3`；字元 `a` 恰好出現 3 次，
因此位於邊界但仍可重組。

```plaintext
初始：_ _ _ _ _
放入 a 的偶數索引：a _ a _ a
剩餘 b、c 轉入奇數索引：a b a c a
結果：abaca
```

## 可執行驗證案例

`Main` 使用以下九組案例。六組可行案例各驗證非空、長度、字元 multiset 與相鄰
字元，共 24 項；三組不可行案例各驗證回傳空字串，共 3 項，合計 27 項。

| 案例 | 輸入 | 預期 | 驗證重點 |
| --- | --- | --- | --- |
| 1 | `aab` | 可行 | 官方範例 |
| 2 | `aaab` | 不可行 | 官方範例 |
| 3 | `z` | 可行 | 單一字元 |
| 4 | `aa` | 不可行 | 最小重複不可行案例 |
| 5 | `aaabc` | 可行 | 奇數長度可行性邊界 |
| 6 | `aaabbc` | 可行 | 偶數長度可行性邊界 |
| 7 | `vvvlo` | 可行 | 一般回歸案例 |
| 8 | 250 個 `a` + 250 個 `b` | 可行 | 長度上限 |
| 9 | 251 個 `a` + 249 個 `b` | 不可行 | 長度上限門檻失敗 |

任一檢查失敗時，程式會將 `Environment.ExitCode` 設為 `1`。本題沒有獨立 test
project 或第三方測試框架；console acceptance harness 是目前的驗證機制。對長度
500 的案例只顯示長度與字元統計，不輸出完整字串。

## 建置與執行

請從此 README 所在的外層 `leetcode_767` 目錄執行：

```bash
dotnet build leetcode_767/leetcode_767.csproj --nologo
dotnet run --no-build --project leetcode_767/leetcode_767.csproj
```

以下是完成建置後執行第二個命令的完整輸出：

```text
LeetCode 767 acceptance harness

Case 1: Official possible example
Input: "aab"
Output: "aba"
PASS | Output is non-empty | Expected: True | Actual: True
PASS | Output length equals input length | Expected: True | Actual: True
PASS | Output has same character multiset | Expected: True | Actual: True
PASS | Output has no equal adjacent characters | Expected: True | Actual: True

Case 2: Official impossible example
Input: "aaab"
Output: ""
PASS | Output is empty | Expected: True | Actual: True

Case 3: Single character
Input: "z"
Output: "z"
PASS | Output is non-empty | Expected: True | Actual: True
PASS | Output length equals input length | Expected: True | Actual: True
PASS | Output has same character multiset | Expected: True | Actual: True
PASS | Output has no equal adjacent characters | Expected: True | Actual: True

Case 4: Minimum impossible repetition
Input: "aa"
Output: ""
PASS | Output is empty | Expected: True | Actual: True

Case 5: Odd-length feasibility boundary
Input: "aaabc"
Output: "abaca"
PASS | Output is non-empty | Expected: True | Actual: True
PASS | Output length equals input length | Expected: True | Actual: True
PASS | Output has same character multiset | Expected: True | Actual: True
PASS | Output has no equal adjacent characters | Expected: True | Actual: True

Case 6: Even-length feasibility boundary
Input: "aaabbc"
Output: "ababac"
PASS | Output is non-empty | Expected: True | Actual: True
PASS | Output length equals input length | Expected: True | Actual: True
PASS | Output has same character multiset | Expected: True | Actual: True
PASS | Output has no equal adjacent characters | Expected: True | Actual: True

Case 7: General regression
Input: "vvvlo"
Output: "vlvov"
PASS | Output is non-empty | Expected: True | Actual: True
PASS | Output length equals input length | Expected: True | Actual: True
PASS | Output has same character multiset | Expected: True | Actual: True
PASS | Output has no equal adjacent characters | Expected: True | Actual: True

Case 8: Maximum-length possible
Input: length 500; counts: a=250, b=250
Output: length 500; counts: a=250, b=250
PASS | Output is non-empty | Expected: True | Actual: True
PASS | Output length equals input length | Expected: True | Actual: True
PASS | Output has same character multiset | Expected: True | Actual: True
PASS | Output has no equal adjacent characters | Expected: True | Actual: True

Case 9: Maximum-length threshold failure
Input: length 500; counts: a=251, b=249
Output: ""
PASS | Output is empty | Expected: True | Actual: True

Summary: 27/27 checks passed.
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
├── leetcode_767/
│   ├── Program.cs
│   └── leetcode_767.csproj
├── AGENTS.md
└── README.md
```
