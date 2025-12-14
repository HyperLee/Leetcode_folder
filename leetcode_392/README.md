# LeetCode 392 — Is Subsequence (C#)

簡短說明：此專案包含 LeetCode 題號 **392. Is Subsequence** 的完整題解說明與一個可執行的 C# 範例（`Program.cs`），並在終端列印多組測試結果以利驗證。

> [!note]
> `Program.cs` 以清晰、可讀的方式實作了「雙指針（Greedy）」解法，適合作為學習與測試範例。

## 內容總覽

- **題目**：問題說明與輸入限制
- **解法概念**：演算法思路與複雜度分析
- **程式碼說明**：重點程式片段與邊界處理
- **範例演示**：逐步說明與輸出範例
- **如何執行**：建置與執行指令

---

## 題目描述（繁體中文）

給定兩個字串 `s` 與 `t`，判斷 `s` 是否為 `t` 的子序列（subsequence）。

- 子序列定義：可從 `t` 刪除部分字元（不一定連續）但保留原有順序，若能得到 `s`，則 `s` 為 `t` 的子序列。

範例：`"ace"` 是 `"abcde"` 的子序列；`"aec"` 不是。

題目連結：[LeetCode — Is Subsequence](https://leetcode.com/problems/is-subsequence/)

## 解法概念

使用「雙指針（Greedy）」：

- 令 `i` 指向 `s` 的當前字元，`j` 指向 `t` 的當前字元。
- 逐步掃描 `t`：若 `s[i] == t[j]`，則 `i++`（匹配下一個字元）；不論是否相符，`j++`（繼續掃描 `t`）。
- 若最終 `i == s.Length`，代表 `s` 的所有字元均被按順序找到 → 回傳 `true`；否則回傳 `false`。

此作法為貪婪且正確，時間複雜度 O(n + m)，空間複雜度 O(1)。

## 程式碼重點說明

- 參考實作：[`Program.cs`](leetcode_392/Program.cs)
- 先檢查 `s` 或 `t` 是否為 `null` 並擲回 `ArgumentNullException`。
- 視空字串（或僅由空白組成）為子序列：`if (s.Trim().Length == 0) return true;`。
- 關鍵邏輯：

```csharp
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

## 範例演示（逐步）

範例 1：`s = "abc"`, `t = "ahbgdc"`

- 初始 `i=0` 指向 `'a'`、`j=0` 指向 `'a'` → 相符，`i=1, j=1`。
- 接著 `i` 指向 `'b'`，走過 `h`（`j=2`）、遇到 `b`（`j=3`）→ 相符，`i=2, j=4`。
- 最後找到 `c`，`i` 到達 `s.Length` → 回傳 `true`。

範例 2：`s = "axc"`, `t = "ahbgdc"`

- 找到 `a`，接著在 `t` 中找不到 `x`，最終 `i < s.Length` → 回傳 `false`。

## 邊界情況與建議

- 若 `s` 為空（或僅空白），此實作回傳 `true`。
- 若 `t.Length < s.Length`，可直接回傳 `false`（雙指針亦會處理）。
- 若需要處理大量不同的 `s` 並查詢同一個 `t`，可考慮預處理 `t`（建立每個字元的出現位置索引）以加速多次查詢。

## 如何執行

```bash
dotnet build leetcode_392/leetcode_392.csproj -c Debug
dotnet run --project leetcode_392/leetcode_392.csproj
```

輸出範例：

```text
s="abc", t="ahbgdc" => True
s="axc", t="ahbgdc" => False
s="", t="ahbgdc" => True
s="aaa", t="aa" => False
```

---

若你要我再把 README 翻成英文、加入單元測試或加入更進階的多次查詢優化示範，告訴我想要哪一項即可，我來處理。
