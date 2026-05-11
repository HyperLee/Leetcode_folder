# LeetCode 387 - First Unique Character in a String

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![Language](https://img.shields.io/badge/language-C%23-239120)

這個專案是 LeetCode 387「First Unique Character in a String」的 C# 主控台解法整理。程式在 `Main` 中提供可直接執行的範例資料，並比較四種不同實作方式的輸出結果。

## 題目說明

給定一個字串 `s`，找出第一個不重複字元，並回傳它在字串中的索引。如果不存在不重複字元，回傳 `-1`。

範例：

| 輸入 | 輸出 | 說明 |
| --- | ---: | --- |
| `"leetcode"` | `0` | `l` 是第一個只出現一次的字元 |
| `"loveleetcode"` | `2` | `v` 是第一個只出現一次的字元 |
| `"aabb"` | `-1` | 所有字元都重複 |
| `""` | `-1` | 空字串沒有唯一字元 |

## 限制條件

LeetCode 原題限制如下：

- `1 <= s.length <= 10^5`
- `s` 只包含小寫英文字母

本專案的範例執行另外包含空字串，用來展示目前實作在沒有字元時會回傳 `-1`。

## 解題概念與出發點

題目核心是「找到第一個出現次數為 1 的字元」。要符合「第一個」的要求，解法通常會分成兩個動作：

1. 判斷每個字元是否唯一。
2. 依照原字串順序找出第一個唯一字元。

目前 `leetcode_387/Program.cs` 保留四種解法，用來比較不同資料結構與時間複雜度的取捨。

## 解法設計

### `FirstUniqChar`：字典記錄索引與重複標記

使用 `Dictionary<char, int>` 記錄每個字元第一次出現的索引。若後續再次遇到相同字元，就把該字元的值改為 `-1`，表示它已經不可能成為答案。

- 時間複雜度：`O(n)`
- 空間複雜度：`O(k)`，`k` 為不同字元數

以 `"loveleetcode"` 為例：

1. 第一次掃描後，`l`、`o`、`e` 等重複字元會被標記為 `-1`。
2. `v` 只出現一次，保留索引 `2`。
3. 第二次依序掃描原字串，`l` 與 `o` 都是 `-1`，到 `v` 時回傳 `2`。

### `FirstUniqChar2`：比較第一次與最後一次出現位置

逐一檢查每個字元的 `IndexOf` 與 `LastIndexOf`。如果兩者相同，代表該字元在整個字串中只出現一次。

- 時間複雜度：`O(n^2)`
- 空間複雜度：`O(1)`

以 `"loveleetcode"` 為例：

1. `l` 的第一次位置是 `0`，最後一次位置是 `4`，不是唯一。
2. `o` 的第一次位置是 `1`，最後一次位置是 `9`，不是唯一。
3. `v` 的第一次位置與最後一次位置都是 `2`，回傳 `2`。

### `FirstUniqChar3`：ASCII 頻率陣列

使用長度為 `128` 的整數陣列，以字元的 ASCII code 作為索引來統計次數。因為原題限制為小寫英文字母，所以可用固定大小陣列處理。

- 時間複雜度：`O(n)`
- 空間複雜度：`O(1)`

以 `"loveleetcode"` 為例：

1. 第一次掃描累計各字元次數，例如 `l`、`o`、`e` 都大於 `1`。
2. `v` 的次數為 `1`。
3. 第二次依原字串順序掃描，第一個次數為 `1` 的位置是索引 `2`。

### `FirstUniqChar4`：字典統計頻率

使用 `Dictionary<char, int>` 統計每個字元出現次數，再依照原字串順序找出第一個頻率為 `1` 的字元。

- 時間複雜度：`O(n)`
- 空間複雜度：`O(k)`，`k` 為不同字元數

以 `"loveleetcode"` 為例：

1. 第一次掃描建立頻率表。
2. `l` 與 `o` 的頻率大於 `1`。
3. 掃描到 `v` 時頻率為 `1`，回傳 `2`。

## 建置與執行

請在 repository 根目錄執行：

```powershell
dotnet build leetcode_387/leetcode_387.csproj
```

執行範例資料：

```powershell
dotnet run --project leetcode_387/leetcode_387.csproj
```

目前沒有獨立測試專案，因此未提供 `dotnet test` 指令。

## 範例輸出

```text
Input: "leetcode", Expected: 0
  FirstUniqChar: 0 (PASS)
  FirstUniqChar2: 0 (PASS)
  FirstUniqChar3: 0 (PASS)
  FirstUniqChar4: 0 (PASS)

Input: "loveleetcode", Expected: 2
  FirstUniqChar: 2 (PASS)
  FirstUniqChar2: 2 (PASS)
  FirstUniqChar3: 2 (PASS)
  FirstUniqChar4: 2 (PASS)

Input: "aabb", Expected: -1
  FirstUniqChar: -1 (PASS)
  FirstUniqChar2: -1 (PASS)
  FirstUniqChar3: -1 (PASS)
  FirstUniqChar4: -1 (PASS)

Input: "", Expected: -1
  FirstUniqChar: -1 (PASS)
  FirstUniqChar2: -1 (PASS)
  FirstUniqChar3: -1 (PASS)
  FirstUniqChar4: -1 (PASS)
```

## 專案結構

```text
.
├── docs/
│   └── readme-template.md
├── leetcode_387/
│   ├── Program.cs
│   └── leetcode_387.csproj
├── leetcode_387.slnx
└── README.md
```

> [!NOTE]
> `leetcode_387.slnx` 目前未列出專案，建置與執行請優先使用 project-level `dotnet` 指令。
