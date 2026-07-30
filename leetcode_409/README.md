# LeetCode 409 — 最長迴文

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![LeetCode](https://img.shields.io/badge/LeetCode-409%20Longest%20Palindrome-FFA116)](https://leetcode.com/problems/longest-palindrome/)

以 .NET 10 Console App 示範如何從字元出現次數推導可構成的最長迴文長度。本專案保留計數陣列解法，並加入 HashSet 配對解法，透過可直接執行的案例比較兩者結果。

## 快速導覽

- [題目說明](#題目說明)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：ASCII 計數陣列](#解法一ascii-計數陣列)
- [解法二：HashSet 即時配對](#解法二hashset-即時配對)
- [解法比較](#解法比較)
- [建置與執行](#建置與執行)

## 題目說明

給定一個由大寫或小寫英文字母組成的字串 `s`，使用其中的字元構成迴文，回傳能構成的最長迴文長度。

題目要計算的是「重新排列並使用輸入字元後，最多能組成多長的迴文」，不是在原字串中尋找最長迴文子字串。字母區分大小寫，因此 `"Aa"` 的兩個字元不能互相配對。

### 限制條件

- `1 <= s.Length <= 2000`
- `s` 僅包含大寫和／或小寫英文字母
- `s` 不為 `null`

> [!NOTE]
> 執行範例額外加入空字串，作為官方限制以外的防禦性驗證。兩種解法都會自然回傳 `0`，但這不改變題目的正式輸入限制。

題目來源：[409. Longest Palindrome](https://leetcode.com/problems/longest-palindrome/)

## 解題概念與出發點

迴文具有左右對稱結構，因此放在中心以外的每個字元都必須成對出現：

1. 某個字元每取得兩個，就能分別放在迴文的左側與右側。
2. 偶數次數的字元可以全部使用。
3. 奇數次數的字元先使用其中最大的偶數部分。
4. 若至少存在一種奇數次字元，最後可以再選一個字元放在正中央。
5. 即使有多種奇數次字元，中心仍然只有一個位置。

例如 `"cccaaa"` 中，`c` 與 `a` 都各出現三次。兩者各能提供一組配對，共使用四個字元；剩餘字元中只能再選一個作為中心，所以答案是 `5`，而不是 `6`。

## 解法一：ASCII 計數陣列

### 設計動機

題目保證輸入只包含英文字母，字元範圍固定且很小，因此可以使用長度為 `128` 的陣列，以字元的 ASCII 值直接作為索引。這樣不需要建立字典，也不需要排序。

### 執行步驟

1. 掃描字串，將每個字元對應的計數加一。
2. 掃描計數陣列，對每個出現次數 `frequency` 加入 `(frequency / 2) * 2`：
   - 整數除法會捨去無法配對的單一字元。
   - 乘以二後得到該字元能放入迴文左右兩側的總數。
3. 同時記錄是否至少存在一個奇數次字元。
4. 若存在奇數次字元，在所有配對長度之外再加入一個中心字元。

核心不變量是：`length` 始終只包含已成對、可安全放在迴文兩側的字元；中心字元只會在最後加入一次。

### `"abccccdd"` 演示流程

| 字元 | 出現次數 | 可使用的配對長度 | 是否留下奇數字元 |
| --- | ---: | ---: | --- |
| `a` | 1 | 0 | 是 |
| `b` | 1 | 0 | 是 |
| `c` | 4 | 4 | 否 |
| `d` | 2 | 2 | 否 |

- 所有配對合計：`0 + 0 + 4 + 2 = 6`
- 至少存在一個奇數次字元，可選 `a` 或 `b` 放在中心。
- 最長長度：`6 + 1 = 7`
- 其中一種可構成的迴文為 `"dccaccd"`。

### 複雜度

- 時間複雜度：`O(n)`，先掃描輸入，再掃描固定長度的計數陣列。
- 輔助空間複雜度：`O(1)`，計數陣列固定為 128 格，不隨輸入長度成長。

## 解法二：HashSet 即時配對

### 設計動機

如果只需要答案長度，不一定要保存每個字元的完整次數。HashSet 可以只追蹤「目前尚未配對」的字元：

- 第一次遇到字元時，把它加入集合。
- 再次遇到相同字元時，從集合移除並立即把答案加二。
- 同一字元第三次出現時會重新加入，第四次出現時又完成另一組配對。

這個切換過程等同於追蹤每個字元出現次數的奇偶性。

### 執行步驟

1. 建立 `unmatchedCharacters`，保存目前出現奇數次、尚未配對的字元。
2. 逐字掃描：
   - 若 `Remove(c)` 成功，代表集合已有相同字元，完成一組配對並將長度加二。
   - 否則將 `c` 加入集合，等待下一個相同字元。
3. 掃描完成後，如果集合非空，代表至少有一個未配對字元可放在中心，答案再加一。

核心不變量是：集合中每個字元都只代表一個尚未配對的剩餘量，而 `length` 只包含已完成的配對。

### `"abccccdd"` 演示流程

| 讀入字元 | 尚未配對集合 | 已配對長度 |
| --- | --- | ---: |
| `a` | `{ a }` | 0 |
| `b` | `{ a, b }` | 0 |
| 第一個 `c` | `{ a, b, c }` | 0 |
| 第二個 `c` | `{ a, b }` | 2 |
| 第三個 `c` | `{ a, b, c }` | 2 |
| 第四個 `c` | `{ a, b }` | 4 |
| 第一個 `d` | `{ a, b, d }` | 4 |
| 第二個 `d` | `{ a, b }` | 6 |

掃描結束時集合仍有 `a`、`b`，表示可以選其中一個作為中心，因此答案為 `6 + 1 = 7`。

### 複雜度

- 時間複雜度：平均 `O(n)`，每個字元執行一次 HashSet 查找、加入或移除。
- 輔助空間複雜度：在一般字元集下為 `O(k)`，其中 `k` 是不同字元數；依本題最多 52 種英文字母的固定限制，可視為 `O(1)`。

## 解法比較

| 比較項目 | 計數陣列 | HashSet 即時配對 |
| --- | --- | --- |
| 公開方法 | `LongestPalindrome` | `LongestPalindrome2` |
| 儲存資訊 | 每個 ASCII 字元的完整次數 | 尚未配對字元 |
| 計算時機 | 統計完成後統一計算 | 掃描時即時完成配對 |
| 時間複雜度 | `O(n)` | 平均 `O(n)` |
| 題目限制下的輔助空間 | `O(1)` | `O(1)` |
| 優點 | 結構直接、效能穩定 | 清楚呈現配對狀態，不需完整次數 |
| 注意事項 | 仰賴輸入位於陣列索引範圍 | HashSet 操作有雜湊成本 |

## 可執行案例

`Main` 會執行七組固定案例，兩種解法各驗證一次：

| 案例 | 輸入 | 預期 |
| --- | --- | ---: |
| 防禦性空輸入 | `""` | 0 |
| 單一字元 | `"a"` | 1 |
| 單一字元配對 | `"aa"` | 2 |
| 兩個不同字元 | `"ab"` | 1 |
| 官方範例 | `"abccccdd"` | 7 |
| 大小寫敏感 | `"Aa"` | 1 |
| 多組奇數次字元 | `"cccaaa"` | 5 |

每個案例都會顯示預期值、兩種解法的實際值及 PASS/FAIL。只要有任一驗證失敗，程式便會設定非零結束碼。

## 專案結構

```text
leetcode_409/
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_409/
    ├── leetcode_409.csproj
    └── Program.cs
```

## 建置與執行

需求：已安裝 [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)。

從此 README 所在的 repository 目錄執行：

```bash
dotnet restore leetcode_409/leetcode_409.csproj
dotnet build leetcode_409/leetcode_409.csproj --nologo --no-restore
dotnet run --project leetcode_409/leetcode_409.csproj --no-build
```

本專案目前沒有獨立的自動化測試專案，因此以成功建置及 `Main` 的固定案例作為行為驗證。

## 實際執行結果

以下內容來自 `dotnet run --project leetcode_409/leetcode_409.csproj --no-build`：

```text
案例：空字串（防禦性）
輸入：s = ""
預期（Expected）：0
實際（LongestPalindrome）：0 => PASS
實際（LongestPalindrome2）：0 => PASS

案例：單一字元
輸入：s = "a"
預期（Expected）：1
實際（LongestPalindrome）：1 => PASS
實際（LongestPalindrome2）：1 => PASS

案例：單一字元配對
輸入：s = "aa"
預期（Expected）：2
實際（LongestPalindrome）：2 => PASS
實際（LongestPalindrome2）：2 => PASS

案例：兩個不同字元
輸入：s = "ab"
預期（Expected）：1
實際（LongestPalindrome）：1 => PASS
實際（LongestPalindrome2）：1 => PASS

案例：官方範例
輸入：s = "abccccdd"
預期（Expected）：7
實際（LongestPalindrome）：7 => PASS
實際（LongestPalindrome2）：7 => PASS

案例：大小寫敏感
輸入：s = "Aa"
預期（Expected）：1
實際（LongestPalindrome）：1 => PASS
實際（LongestPalindrome2）：1 => PASS

案例：多組奇數次字元
輸入：s = "cccaaa"
預期（Expected）：5
實際（LongestPalindrome）：5 => PASS
實際（LongestPalindrome2）：5 => PASS

總結：14/14 項驗證通過
```
