# 題解：LeetCode 392 — Is Subsequence

## 題目描述（繁體中文）

給定兩個字串 `s` 和 `t`，判斷 `s` 是否為 `t` 的子序列（subsequence）。

- 子序列的定義：可以從字串 `t` 中刪除部分字元（不一定連續）後，保留字元原本的相對順序，若能得到 `s`，則 `s` 為 `t` 的子序列。
- 範例：`"ace"` 是 `"abcde"` 的子序列；`"aec"` 不是。

題目連結：[LeetCode — Is Subsequence](https://leetcode.com/problems/is-subsequence/)

### 輸入限制（以題目為準）

- 字串長度可達數萬（取決於題目提示），因此需要線性時間解法。

## 解題概念與想法

這題最直觀且效能良好的方法是使用「雙指針（Greedy）」：

- 令 `i` 指向 `s` 的當前字元，`j` 指向 `t` 的當前字元。
- 從左到右掃過 `t`，若 `s[i] == t[j]`，就把 `i++`（表示此字元已匹配），否則只把 `j++`。
- 若最後 `i == s.Length`，代表 `s` 的所有字元都被找到並保持順序 → 回傳 `true`；否則回傳 `false`。

此策略是貪婪且正確的，因為當前找到第一個能匹配 `s[i]` 的字元是最好的選擇（不影響後續匹配）。

## 時間與空間複雜度

- 時間：O(n + m)，其中 `n = s.Length`，`m = t.Length`（最多各掃描一次）。
- 空間：O(1)，僅使用固定數量的變數。

## 程式碼說明（C# 範例）

程式實作於 [Program.cs](../leetcode_392/Program.cs)，要點如下：

- 先檢查 `s` 或 `t` 為 `null` 的情況並擲回 `ArgumentNullException`。
- 若 `s.Trim().Length == 0`（空字串或僅有空白），視為子序列，回傳 `true`。
- 使用 `while (i < s.Length && j < t.Length)` 比較字元，遇到相符時將 `i++`，每個迴圈結尾 `j++`。
- 最後回傳 `i == s.Length` 表示所有字元已被匹配。

```csharp
// 關鍵邏輯：雙指針
while (i < n && j < m)
{
    if (s[i] == t[j])
    {
        i++;
    }
    j++;
}
return i == n;
```

## 範例演示（逐步說明）

範例 1：`s = "abc"`, `t = "ahbgdc"`

- 初始化 `i=0` 指向 `'a'`，`j=0` 指向 `'a'` → 相符，`i=1`、`j=1`。
- `i` 指向 `'b'`，走過 `h` (`j=2`)、`b` (`j=3`) → 相符，`i=2`、`j=4`。
- `i` 指向 `'c'`，`j` 走到 `'g'`、`d'`、`c'` → 相符，最後 `i==s.Length` → 回傳 `true`。

範例 2：`s = "axc"`, `t = "ahbgdc"`

- `a` 匹配，接著尋找 `x`，`t` 中找不到 `x`，最終 `i < s.Length` → 回傳 `false`。

## 邊界與常見考量

- 若 `s` 為空（或僅有空白），一般視為子序列（此實作使用 `s.Trim().Length == 0` 判斷）。
- 若 `t` 長度小於 `s`，可以直接回傳 `false`（不過雙指針會自然處理）。

## 如何在本專案執行範例

```bash
dotnet build leetcode_392/leetcode_392.csproj -c Debug
dotnet run --project leetcode_392/leetcode_392.csproj
```

程式會在終端印出數組測試的布林結果，方便驗證各種情況。

---

若要我把更多變體加入（例如處理大量 `t` 而多次查詢不同 `s` 的最佳化），我可以再新增說明與實作（例如預處理 `t` 的字元次序索引以加速多次查詢）。
