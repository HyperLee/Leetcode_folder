# 2911. Minimum Changes to Make K Semi-palindromes

[Official LeetCode problem statement](https://leetcode.com/problems/minimum-changes-to-make-k-semi-palindromes/description/)

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

## Examples

### Example 1

**Input:** `s = "abcac", k = 2`  
**Output:** `1`

**Explanation:** Divide `s` into `"ab"` and `"cac"`. The substring `"cac"` is already a semi-palindrome. Change `"ab"` to `"aa"`; with `d = 1`, `"aa"` is a palindrome. The total number of changes is `1`.

### Example 2

**Input:** `s = "abcdef", k = 2`  
**Output:** `2`

**Explanation:** Divide `s` into `"abc"` and `"def"`. Each substring needs one change to become a semi-palindrome: for example, change `"abc"` to `"aba"` and `"def"` to `"ded"`. With `d = 1`, both resulting substrings are palindromes, for a total of `2` changes.

### Example 3

**Input:** `s = "aabbaa", k = 3`  
**Output:** `0`

**Explanation:** Divide `s` into `"aa"`, `"bb"`, and `"aa"`. Each substring is already a palindrome with `d = 1`, so no changes are needed.

## 繁體中文

### 2911. 得到 K 個半回文串的最少修改次數

給定字串 `s` 和整數 `k`。你要把 `s` **恰好切成 `k` 個連續的子字串**，再修改其中一些字元，讓每個子字串都成為半回文串。請回傳最少需要修改的字元總數。

可以把題目想成兩件事：決定在哪裡切開，以及要改掉哪些字元。切開本身不算修改；每替換一個位置上的字母，才算一次修改。要讓總修改次數最少。

### 先看懂題目中的幾個詞

- **子字串**：從原字串中取出一段連續字元。切分時要用完所有字元、維持原來順序，而且各段不能重疊。例如，`"abcdef"` 可以切成 `"abc"` 和 `"def"`，不能跳著挑出 `"ace"` 和 `"bdf"`。
- **恰好 `k` 個**：切出的段數必須等於 `k`，不能多也不能少。
- **修改一個字元**：把某個位置上的字母換成另一個小寫英文字母，算一次修改；不需要、也不能靠插入、刪除或重新排列字元來完成題目。
- **迴文串**：從左往右讀和從右往左讀相同，例如 `"abba"` 和 `"abcba"`。

### 什麼是半回文串？

一個字串只要**存在一個**符合條件的 `d`，使依照 `d` 分成的每一組都是迴文串，它就是半回文串。這裡的 `d` 是該字串長度的正因數，但不能等於字串長度；也就是長度要能被 `d` 整除，而且 `d` 必須小於長度。這樣分出的每一組會有相同數量的字元。

位置從 `1` 開始數。選定 `d` 後，第一組取位置 `1`、`1 + d`、`1 + 2d`⋯的字元；第二組取位置 `2`、`2 + d`、`2 + 2d`⋯的字元；依此類推，一共會有 `d` 組。**這些組不一定是連在一起的字元，也不必彼此相同；只要每一組各自都是迴文串即可。**

以 `"abcabc"` 為例，長度是 `6`，小於 `6` 且能整除 `6` 的 `d` 有 `1`、`2`、`3`：

| `d` | 依位置分組 | 結果 |
| --- | --- | --- |
| `1` | `"abcabc"`（位置 `1, 2, 3, 4, 5, 6`） | 不是迴文串 |
| `2` | `"acb"`（位置 `1, 3, 5`）、`"bac"`（位置 `2, 4, 6`） | 兩組都不是迴文串 |
| `3` | `"aa"`（位置 `1, 4`）、`"bb"`（位置 `2, 5`）、`"cc"`（位置 `3, 6`） | 每一組都是迴文串 |

因此，`"abcabc"` 是半回文串：找到 `d = 3` 就足夠，不需要 `d = 1` 或 `d = 2` 也成立。注意，`"aa"`、`"bb"`、`"cc"` 這幾組彼此不同，但它們各自都是迴文串。

### `k` 和 `d` 的差別

- `k` 是題目給定的數字，決定整個字串 `s` 要切成幾個連續子字串。
- `d` 是在某一個子字串裡使用的數字，決定要依照哪些位置把字元分組；它不是把子字串再切成連續小段。每個子字串都可以依自己的長度選擇 `d`。

例如，`s = "abcabc"`、`k = 1` 時，先把整個字串當成一段；在這一段裡選 `d = 3`，就會依位置分成 `"aa"`、`"bb"`、`"cc"`。而 `s = "aabbaa"`、`k = 3` 時，先切成 `"aa"`、`"bb"`、`"aa"` 三段；每段長度都是 `2`，各自可選 `d = 1`。

### 題目要最小化什麼？

每一段子字串都必須能成為半回文串，但可以透過替換字元來達成；原字串不必一開始就符合條件。每段各自判斷半回文時，可以選擇適合該段的 `d`。把所有段為了符合條件而替換的字元數加起來，就是總修改次數；題目要你在所有可行切法和修改方式中，讓這個總數最小。

## 範例

### 範例 1

**輸入：** `s = "abcac", k = 2`  
**輸出：** `1`

**說明：** 將 `s` 切分為 `"ab"` 和 `"cac"`。子字串 `"cac"` 已經是半回文串。將 `"ab"` 修改為 `"aa"`；當 `d = 1` 時，`"aa"` 是迴文串。總共需要 `1` 次字元變更。

### 範例 2

**輸入：** `s = "abcdef", k = 2`  
**輸出：** `2`

**說明：** 將 `s` 切分為 `"abc"` 和 `"def"`。每個子字串都需要修改一個字元才能成為半回文串。例如，將 `"abc"` 修改為 `"aba"`，並將 `"def"` 修改為 `"ded"`。當 `d = 1` 時，修改後的兩個子字串都是迴文串，因此總共需要 `2` 次變更。

### 範例 3

**輸入：** `s = "aabbaa", k = 3`  
**輸出：** `0`

**說明：** 將 `s` 切分為 `"aa"`、`"bb"` 和 `"aa"`。當 `d = 1` 時，每個子字串本身都是迴文串，因此不需要修改任何字元。

### 題目限制

- `2 <= s.length <= 200`：字串長度介於 `2` 到 `200`。
- `1 <= k <= s.length / 2`：要切出的段數至少為 `1`，最多為字串長度的一半。
- `s` 只包含小寫英文字母。

長度為 `1` 的字串沒有可用的 `d`，因此不能成為半回文串；每一段至少要有 `2` 個字元。`k` 不超過字串長度的一半，表示字串長度足以切成 `k` 段、且每段至少有 `2` 個字元。
