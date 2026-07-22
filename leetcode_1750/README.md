# 1750. Minimum Length of String After Deleting Similar Ends／刪除字串兩端相同字元後的最短長度

給定只含 `a`、`b`、`c` 的字串，反覆刪除由同一字元組成、互不重疊且字元相同的非空
前綴與後綴，求最後可得到的最短字串長度。本專案保留單一、純函式的雙指標解法。

- [LeetCode English](https://leetcode.com/problems/minimum-length-of-string-after-deleting-similar-ends/)
- [LeetCode 中文](https://leetcode.cn/problems/minimum-length-of-string-after-deleting-similar-ends/)

## 題目說明與限制條件

每次操作可選擇一個所有字元相同的非空前綴，以及一個所有字元相同的非空後綴；兩者
不可重疊，且使用的字元必須相同。刪除後可繼續操作，直到兩端不同或字串為空。

- `1 <= s.length <= 100000`
- `s` 僅包含 `a`、`b`、`c`。
- `MinimumLength` 只處理題目保證的有效輸入，不加入額外的無效輸入行為。

## 保留解法：雙指標跳過邊界區段

`left` 與 `right` 表示目前尚未刪除的閉區間。當兩端字元不同時，題目不允許繼續刪除，
目前區間長度就是答案；當兩端相同時，先記住該字元，再分別將左右指標移過完整的同字元
連續區段。每個字元至多被指標經過一次。

核心不變量：每一輪開始時，`[left, right]` 恰好表示先前合法操作後仍然存在的字串；
每一輪結束後，兩側所有等於該輪邊界字元的連續字元都已完整移除。

容易出錯之處：

- 只各刪除一個字元會漏掉整段相同前綴或後綴，例如 `aaabca`。
- 刪除一輪後可能形成新的相同邊界，例如 `abbbbbba`。
- 指標可能交錯而代表空字串；此時 `right - left + 1` 正好為 0。
- 前綴與後綴不能重疊，因此外層判斷必須要求 `left < right`。

## 複雜度

| 方法 | 時間 | 結果空間 | 輔助空間 |
| --- | --- | --- | --- |
| `MinimumLength`（雙指標） | `O(n)` | `O(1)` | `O(1)` |

方法只回傳一個整數；除了左右指標與邊界字元外，不建立與輸入長度相關的資料結構。

## 逐步走查

以 `aabccabba` 為例：

| 步驟 | 尚未刪除字串 | 動作 |
| ---: | --- | --- |
| 1 | `aabccabba` | 兩端皆為 `a`，移除左側 `aa` 與右側 `a` |
| 2 | `bccabb` | 兩端皆為 `b`，移除左側 `b` 與右側 `bb` |
| 3 | `cca` | 兩端為 `c`、`a`，停止 |

最後剩餘長度為 3。

## Acceptance Harness

`Main` 執行十個確定性案例。每案都印出 Case、Input、Expected、Actual 與 PASS/FAIL；
任何案例失敗會將 process exit code 設為 1。

| # | 輸入 | 預期 | 驗證目的 |
| ---: | --- | ---: | --- |
| 1 | `ca` | 2 | 官方範例：兩端不同 |
| 2 | `cabaabac` | 0 | 官方範例：多輪後全刪 |
| 3 | `aabccabba` | 3 | 官方範例：多輪後停止 |
| 4 | `a` | 1 | 最小有效輸入 |
| 5 | `aa` | 0 | 最短可完全刪除字串 |
| 6 | `aaabca` | 2 | 左右邊界區段長度不同 |
| 7 | `abbbbbba` | 0 | 刪除後形成新邊界並連鎖全刪 |
| 8 | `aaaabaaaa` | 1 | 指標最後停在單一字元 |
| 9 | 100,000 個 `a` | 0 | 最大長度、單一連續區段 |
| 10 | `a` + 99,998 個 `b` + `c` | 100000 | 最大長度、兩端不同 |

## 建置與執行

已從 repository 根目錄驗證：

```bash
dotnet build leetcode_1750/leetcode_1750/leetcode_1750.csproj --nologo
dotnet run --no-build --project leetcode_1750/leetcode_1750/leetcode_1750.csproj
```

若直接開啟題目根目錄 `leetcode_1750/`，使用：

```bash
dotnet build leetcode_1750/leetcode_1750.csproj --nologo
dotnet run --no-build --project leetcode_1750/leetcode_1750.csproj
```

以下是 fresh run 的完整輸出：

```text
Case: 1 - Official example 1
Input: "ca"
Expected: 2
Actual: 2
Result: PASS

Case: 2 - Official example 2
Input: "cabaabac"
Expected: 0
Actual: 0
Result: PASS

Case: 3 - Official example 3
Input: "aabccabba"
Expected: 3
Actual: 3
Result: PASS

Case: 4 - Minimum input
Input: "a"
Expected: 1
Actual: 1
Result: PASS

Case: 5 - Two matching characters
Input: "aa"
Expected: 0
Actual: 0
Result: PASS

Case: 6 - Longer left boundary run
Input: "aaabca"
Expected: 2
Actual: 2
Result: PASS

Case: 7 - Chained complete deletion
Input: "abbbbbba"
Expected: 0
Actual: 0
Result: PASS

Case: 8 - Single character remains
Input: "aaaabaaaa"
Expected: 1
Actual: 1
Result: PASS

Case: 9 - Maximum length all equal
Input: 100000 x 'a'
Expected: 0
Actual: 0
Result: PASS

Case: 10 - Maximum length different ends
Input: 'a' + 99998 x 'b' + 'c'
Expected: 100000
Actual: 100000
Result: PASS

Summary: 10/10 checks passed.
```

## 專案結構

```plaintext
leetcode_1750/
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── AGENTS.md
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_1750/
    ├── Program.cs
    └── leetcode_1750.csproj
```
