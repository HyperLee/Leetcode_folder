# LeetCode 318 - Maximum Product of Word Lengths

這個專案是 LeetCode 318「Maximum Product of Word Lengths」的 C# / .NET console 解法整理。程式保留兩種位元遮罩解法，並在 `Main` 中加入固定測資，方便直接執行、比對預期值與實際輸出。

## 題目說明

給定一個字串陣列 `words`，請找出兩個不共享任何相同字母的單字 `words[i]` 與 `words[j]`，並回傳 `words[i].Length * words[j].Length` 的最大值。若不存在這樣的兩個單字，回傳 `0`。

舉例來說，`"abcw"` 與 `"xtfn"` 沒有共同字母，因此可以形成乘積 `4 * 4 = 16`；但 `"abcw"` 與 `"abcdef"` 都含有 `a`、`b`、`c`，所以不能配對。

## 限制條件

- `2 <= words.length <= 1000`
- `1 <= words[i].length <= 1000`
- `words[i]` 只包含小寫英文字母 `a` 到 `z`

## 解題概念與出發點

直覺作法是枚舉每一對單字，再逐字檢查兩者是否有共同字母。但如果每次配對都重新掃描字元，判斷成本會被單字長度放大。

本題的關鍵觀察是：只需要知道某個字母是否出現過，不需要知道出現幾次。因為字母只有 26 個，可以用一個 `int` 的低 26 個 bit 表示一個單字的字母集合：

- bit 0 表示 `a`
- bit 1 表示 `b`
- ...
- bit 25 表示 `z`

建立遮罩時，對每個字母執行：

```csharp
mask |= 1 << (letter - 'a');
```

如果兩個單字的遮罩做 AND 後為 `0`：

```csharp
(mask1 & mask2) == 0
```

代表兩個字母集合完全沒有交集，也就是這兩個單字可以配對。

## 解法一：`MaxProduct`

`MaxProduct` 是基礎位元遮罩解法，做法分成兩段。

第一段先為每個單字建立一個 26-bit 遮罩，放進與 `words` 同長度的 `masks` 陣列。這讓後續判斷兩個單字是否有共同字母時，可以用一次位元 AND 完成。

第二段枚舉所有 `i < j` 的單字配對。只要 `(masks[i] & masks[j]) == 0`，就代表兩個單字沒有共用字母，可以用 `words[i].Length * words[j].Length` 更新最大值。

### 解法一範例流程

以 `["abcw", "baz", "foo", "bar", "xtfn", "abcdef"]` 為例：

1. `"abcw"` 會轉成包含 `a`、`b`、`c`、`w` 的遮罩。
2. `"xtfn"` 會轉成包含 `x`、`t`、`f`、`n` 的遮罩。
3. 兩個遮罩 AND 後為 `0`，表示沒有共同字母。
4. 乘積為 `4 * 4 = 16`，目前最大值更新為 `16`。
5. 其他合法配對不會超過 `16`，所以最後回傳 `16`。

### 複雜度

- 時間複雜度：`O(S + n^2)`，其中 `S` 是所有單字字元總數，`n` 是 `words.Length`。
- 空間複雜度：`O(n)`，需要保存每個單字的遮罩。

## 解法二：`MaxProduct2`

`MaxProduct2` 在解法一的基礎上增加「相同 mask 壓縮」。如果兩個單字的字母集合完全相同，例如 `"ab"` 與 `"aabb"`，它們能配對的對象完全一致；差別只在長度。因此對同一個 mask，只需要保留最長單字的長度。

實作上使用 `Dictionary<int, int>`：

- key：字母集合的 bitmask
- value：該 bitmask 對應到的最大單字長度

建立完字典後，再枚舉所有 mask 配對。若兩個 mask 沒有交集，就用兩個已壓縮過的最大長度更新答案。

### 解法二範例流程

以 `["ab", "aabb", "cd"]` 為例：

1. `"ab"` 的 mask 表示 `{a, b}`，目前保存長度 `2`。
2. `"aabb"` 的 mask 仍然是 `{a, b}`，但長度是 `4`，所以把同一個 mask 的最大長度更新成 `4`。
3. `"cd"` 的 mask 表示 `{c, d}`，保存長度 `2`。
4. `{a, b}` 與 `{c, d}` 沒有交集，所以最大乘積為 `4 * 2 = 8`。

這個壓縮在大量單字擁有相同字母集合時特別有用，因為後續比較的對象會從單字數量 `n` 降為不同 mask 的數量 `m`。

### 複雜度

- 時間複雜度：`O(S + m^2)`，其中 `S` 是所有單字字元總數，`m` 是不同 mask 的數量，且 `m <= n`。
- 空間複雜度：`O(m)`，只保存每種字母集合的最大長度。

## 解法比較

| 解法 | 核心設計 | 適合情境 | 時間複雜度 | 空間複雜度 |
| --- | --- | --- | --- | --- |
| `MaxProduct` | 每個單字一個 mask，枚舉所有單字配對 | 概念直觀，適合作為基礎解法 | `O(S + n^2)` | `O(n)` |
| `MaxProduct2` | 相同 mask 只保留最大長度，再比較不同 mask | 有許多重複字母集合時可減少比較量 | `O(S + m^2)` | `O(m)` |

## 執行範例

執行專案：

```bash
dotnet run --project leetcode_318/leetcode_318.csproj
```

目前輸出：

```text
LeetCode 318 - Maximum Product of Word Lengths
案例 1: words = ["abcw", "baz", "foo", "bar", "xtfn", "abcdef"]
  Expected: 16
  MaxProduct  Result: 16 (PASS)
  MaxProduct2 Result: 16 (PASS)

案例 2: words = ["a", "ab", "abc", "d", "cd", "bcd", "abcd"]
  Expected: 4
  MaxProduct  Result: 4 (PASS)
  MaxProduct2 Result: 4 (PASS)

案例 3: words = ["a", "aa", "aaa", "aaaa"]
  Expected: 0
  MaxProduct  Result: 0 (PASS)
  MaxProduct2 Result: 0 (PASS)

案例 4: words = ["ab", "aabb", "cd"]
  Expected: 8
  MaxProduct  Result: 8 (PASS)
  MaxProduct2 Result: 8 (PASS)

案例 5: words = ["abcdefghijklmnopqrstuvwxyz", "abc", "bcd"]
  Expected: 0
  MaxProduct  Result: 0 (PASS)
  MaxProduct2 Result: 0 (PASS)
總結：10/10 項驗證通過
```

## 驗證指令

本 repo 目前沒有獨立測試專案或測試框架；主要驗證方式是 `Main` 中的固定 sample harness。`dotnet test` 可以確認專案可還原並完成測試命令流程，但不代表有正式單元測試被執行。

```bash
dotnet build leetcode_318/leetcode_318.csproj
dotnet run --project leetcode_318/leetcode_318.csproj
dotnet test leetcode_318/leetcode_318.csproj
git diff --check
```

## 專案結構

```text
leetcode_318/
├── leetcode_318/
│   ├── Program.cs
│   └── leetcode_318.csproj
├── docs/
│   └── readme-template.md
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── AGENTS.md
└── README.md
```
