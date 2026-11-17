# 2911. Minimum Changes to Make K Semi-palindromes

## English

Given a string `s` and an integer `k`, partition `s` into `k` substrings such that the letter changes needed to make each substring a semi-palindrome are minimized.

Return the minimum number of letter changes required.

A **semi-palindrome** is a special type of string that can be divided into palindromes based on a repeating pattern. To check if a string is a semi-palindrome:

1. Choose a positive divisor `d` of the string's length. `d` can range from `1` up to, but not including, the string's length. A string of length `1` has no valid divisor because its only divisor is its length, which is not allowed.
2. For a given divisor `d`, divide the string into groups of characters that follow a repeating pattern of length `d`. The first group contains the characters at positions `1`, `1 + d`, `1 + 2d`, and so on; the second group contains the characters at positions `2`, `2 + d`, `2 + 2d`, and so on.
3. The string is a semi-palindrome if every group forms a palindrome.

Consider the string `"abcabc"`:

- Its length is `6`, so the valid divisors are `1`, `2`, and `3`.
- For `d = 1`, the entire string `"abcabc"` forms one group, which is not a palindrome.
- For `d = 2`, the groups are `"acb"` (positions `1`, `3`, `5`) and `"bac"` (positions `2`, `4`, `6`). Neither group is a palindrome.
- For `d = 3`, the groups are `"aa"`, `"bb"`, and `"cc"` (positions `1`, `4`; `2`, `5`; and `3`, `6`, respectively). Every group is a palindrome, so `"abcabc"` is a semi-palindrome.

## 繁體中文

### 2911. 得到 K 個半回文串的最少修改次數

給定字串 `s` 與整數 `k`，請將 `s` 切分為 `k` 個子字串，並使將每個子字串修改為半回文串所需的字元變更次數總和最少。

請回傳所需的最少字元變更次數。

**半回文串**是一種特殊字串，可以依照重複模式分組，且每一組都是迴文串。判斷一個字串是否為半回文串的步驟如下：

1. 選擇字串長度的正因數 `d`。`d` 可以從 `1` 到小於字串長度的數值。長度為 `1` 的字串沒有有效的因數，因為它唯一的因數就是字串長度，而這不符合條件。
2. 對於選定的因數 `d`，依照長度為 `d` 的重複模式將字元分組。第一組包含位置 `1`、`1 + d`、`1 + 2d` 等字元；第二組包含位置 `2`、`2 + d`、`2 + 2d` 等字元，依此類推。
3. 如果每一組都是迴文串，這個字串就是半回文串。

以字串 `"abcabc"` 為例：

- 字串長度為 `6`，有效因數為 `1`、`2` 和 `3`。
- 當 `d = 1` 時，整個字串 `"abcabc"` 為同一組，但它不是迴文串。
- 當 `d = 2` 時，分組結果為 `"acb"`（位置 `1`、`3`、`5`）和 `"bac"`（位置 `2`、`4`、`6`），兩組都不是迴文串。
- 當 `d = 3` 時，分組結果為 `"aa"`、`"bb"` 和 `"cc"`（位置分別為 `1`、`4`；`2`、`5`；以及 `3`、`6`）。每一組都是迴文串，因此 `"abcabc"` 是半回文串。
