# LeetCode 014 - Longest Common Prefix

這個專案使用 C# console app 示範 LeetCode 14「最長共同前綴」的兩種解法，並在 `Main` 內建可直接執行的測試資料。

## 題目說明

給定一個字串陣列 `strs`，找出所有字串共同擁有的最長前綴字串。如果不存在共同前綴，回傳空字串 `""`。

範例：

```text
Input: strs = ["flower","flow","flight"]
Output: "fl"

Input: strs = ["dog","racecar","car"]
Output: ""
```

## 限制條件

- `1 <= strs.length <= 200`
- `0 <= strs[i].length <= 200`
- 若 `strs[i]` 非空，內容只包含小寫英文字母。

## 解題概念與出發點

共同前綴必須從每個字串的第 0 個字元開始連續相同，因此不需要搜尋字串中間或尾端。只要從左到右比較同一索引位置的字元，第一次出現長度不足或字元不一致時，就能確定答案。

本專案保留兩種解法：

- `LongestCommonPrefixByShortestString`：先找最短字串，再以它作為比較上限。
- `LongestCommonPrefixByVerticalScanning`：以第一個字串為基準，逐欄掃描所有字串。

## 解法一：最短字串比對

設計重點：

1. 先走訪一次 `strs`，找出長度最短的字串。
2. 最長共同前綴不可能比最短字串更長，因此只需要比較最短字串的每個索引。
3. 任一字串在目前索引的字元不同，就回傳目前累積的結果。

範例流程：

```text
strs = ["flower", "flow", "flight"]
最短字串 = "flow"

索引 0：f / f / f，相同，結果為 "f"
索引 1：l / l / l，相同，結果為 "fl"
索引 2：o / o / i，不同，停止並回傳 "fl"
```

時間複雜度為 `O(n * m)`，其中 `n` 是字串數量，`m` 是最短字串長度。額外空間複雜度為 `O(1)`，不計輸出字串。

## 解法二：縱向掃描

設計重點：

1. 以 `strs[0]` 作為基準字串。
2. 從左到右檢查每一欄，也就是所有字串在同一索引位置的字元。
3. 若某個字串長度不足，或該欄字元與基準字串不同，就回傳基準字串在目前索引前的子字串。

範例流程：

```text
strs = ["interspecies", "interstellar", "interstate"]

索引 0 ~ 5：皆為 "inters"，繼續
索引 6：p / t / t，不同，停止並回傳 "inters"
```

時間複雜度為 `O(n * m)`，其中 `n` 是字串數量，`m` 是第一個不一致位置之前的比較長度。額外空間複雜度為 `O(1)`，不計輸出字串。

## 執行方式

建置專案：

```powershell
dotnet build leetcode_014\leetcode_014.csproj
```

執行範例：

```powershell
dotnet run --project leetcode_014\leetcode_014.csproj
```

預期輸出：

```text
Case 1: ["flower", "flow", "flight"]
Expected: "fl"
Shortest string: "fl"
Vertical scanning: "fl"

Case 2: ["dog", "racecar", "car"]
Expected: ""
Shortest string: ""
Vertical scanning: ""

Case 3: ["interspecies", "interstellar", "interstate"]
Expected: "inters"
Shortest string: "inters"
Vertical scanning: "inters"

Case 4: [""]
Expected: ""
Shortest string: ""
Vertical scanning: ""

Case 5: ["prefix", "prefix", "prefix"]
Expected: "prefix"
Shortest string: "prefix"
Vertical scanning: "prefix"
```

檢查 diff 空白與換行：

```powershell
git diff --check
```

## 專案結構

```text
leetcode_014/
├── docs/
│   └── readme-template.md
├── leetcode_014/
│   ├── Program.cs
│   └── leetcode_014.csproj
└── README.md
```
