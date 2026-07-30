# LeetCode 424：替換後的最長重複字元

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![Language](https://img.shields.io/badge/C%23-Console-239120)
![LeetCode](https://img.shields.io/badge/LeetCode-424-FFA116)

這是一個使用 C# 與 .NET 10 實作的教學型主控台專案。程式以滑動窗口在一次線性掃描中，
找出最多替換 `k` 個字元後，能由同一個大寫英文字母組成的最長子字串。`Main` 內建七筆
可重複執行的案例，會自動比較預期值與實際值。

- [LeetCode English](https://leetcode.com/problems/longest-repeating-character-replacement/)
- [LeetCode 中文](https://leetcode.cn/problems/longest-repeating-character-replacement/)

## 題目說明

給定字串 `s` 與整數 `k`。每次操作可以把字串中的任一字元改成另一個大寫英文字母，
最多執行 `k` 次。請回傳完成替換後，可以只包含同一個字母的最長連續子字串長度。

例如：

| 輸入 | 輸出 | 說明 |
| --- | ---: | --- |
| `s = "ABAB", k = 2` | `4` | 將兩個 `A` 改成 `B`，或將兩個 `B` 改成 `A`，整段長度為 4。 |
| `s = "AABABBA", k = 1` | `4` | 可把中間的 `A` 改成 `B`，形成長度為 4 的 `BBBB`。 |

## 限制條件

- `1 <= s.Length <= 100000`
- `s` 僅由大寫英文字母 `A` 到 `Z` 組成。
- `0 <= k <= s.Length`
- `CharacterReplacement` 預期接收符合題目限制的非 `null` 輸入，不另外定義無效輸入行為。
- 公開方法只計算並回傳長度，不修改輸入字串，也不直接輸出主控台內容。

## 解題概念與出發點

若固定一段窗口，要把窗口內所有字元改成同一個字母，最佳目標必然是窗口中出現次數最多的
字母。其他字元才需要被替換，因此：

```text
需要替換的字元數 = 窗口長度 - 窗口內最高字元頻率
```

當需要替換的字元數不超過 `k`，目前窗口可以形成合法答案，可以繼續向右擴張；一旦超過
`k`，就把左邊界向右移一格。這正是適合滑動窗口的單調結構：

1. 右邊界只向右掃描，每個字元加入窗口一次。
2. 左邊界只在替換成本超過預算時向右移動，每個字元至多離開窗口一次。
3. 使用長度為 26 的 `count` 陣列記錄窗口內各大寫字母的頻率。
4. 使用 `maxCount` 保存掃描過程曾達到的最高字元頻率。
5. 使用 `res` 保存已證明可以達成的最長長度。

相較於枚舉所有子字串再計算字元頻率的暴力解法，滑動窗口不會重複掃描相同區段，可把最壞
時間從平方等級降為線性。

## 解法：滑動窗口與歷史最高頻率

### 設計步驟

`CharacterReplacement` 的處理流程如下：

1. 長度小於 2 時直接回傳字串長度。
2. 令 `[left, right)` 表示目前窗口；`right` 指向下一個要加入的字元。
3. 將右側字元加入 `count`，並在出現更高頻率時更新 `maxCount`。
4. `right` 右移後，以 `right - left - maxCount` 計算判斷用的替換成本。
5. 若成本大於 `k`，移除左側字元並把 `left` 右移一格。
6. 更新目前找到的最長窗口 `res`，直到掃描完整個字串。

窗口每次只增加一個字元，而 `maxCount` 不會下降，所以每輪最多只需要讓左邊界前進一次。
當窗口超出預算時，程式不是嘗試把它縮到任意更短的合法長度，而是維持「目前已找到的候選
長度」，等待之後出現更高頻率的字元組合。

### 為什麼 `maxCount` 不需要下降

左邊界移動後，真正的窗口最高頻率可能降低，但程式不重新掃描 26 個計數，也不降低
`maxCount`。此時 `maxCount` 應理解為「掃描至今曾證明可達到的最高頻率」，而不一定是
當前窗口的精確最高頻率。

這個延遲值可能讓目前窗口看起來比實際更寬鬆，卻不會讓答案錯誤：

- `res` 只記錄曾經由真實頻率建立出的候選長度。
- 過大的歷史 `maxCount` 最多只是讓左邊界暫時不再前進，不會憑空增加窗口長度。
- 若要得到比 `res` 更長的窗口，後續必須真的出現足夠高的新頻率，`maxCount` 才能提升並
  支撐更長答案。

因此不必在每次縮窗後重新求最大頻率，仍能正確得到最長長度。

### 提前返回

縮窗後，程式會估算理論上還能達到的最大長度：

```text
目前窗口長度 + 尚未掃描的字元數
```

若這個上界仍不大於 `res`，即使剩餘字元全部接入窗口也不可能產生更長答案，可以立即回傳
`res`。這項最佳化不改變答案，只略過不可能改善結果的尾端掃描。

### 複雜度

令 `n` 為 `s.Length`：

| 項目 | 複雜度 | 原因 |
| --- | --- | --- |
| 時間 | `O(n)` | 左、右邊界皆只向右移動，每個字元至多加入與移除一次。 |
| 輔助空間 | `O(1)` | `count` 固定保存 26 個大寫英文字母的頻率。 |

## 範例演示：`s = "AABABBA", k = 1`

下表的「加入後窗口」是縮窗判斷前的內容；`maxCount` 是歷史最高頻率。

| 加入位置 | 加入字元 | 加入後窗口 | `maxCount` | 判斷成本 | 動作 | `res` |
| ---: | :---: | --- | ---: | ---: | --- | ---: |
| 0 | `A` | `A` | 1 | 0 | 合法，保留窗口 | 1 |
| 1 | `A` | `AA` | 2 | 0 | 合法，保留窗口 | 2 |
| 2 | `B` | `AAB` | 2 | 1 | 合法，保留窗口 | 3 |
| 3 | `A` | `AABA` | 3 | 1 | 合法，保留窗口 | 4 |
| 4 | `B` | `AABAB` | 3 | 2 | 超過 `k`，移除左側 `A`，窗口成為 `ABAB` | 4 |
| 5 | `B` | `ABABB` | 3 | 2 | 超過 `k`，移除左側 `A`，窗口成為 `BABB` | 4 |
| 6 | `A` | `BABBA` | 3 | 2 | 超過 `k`，移除左側 `B`；剩餘上界無法超越 4 | 4 |

在位置 3 時，窗口 `AABA` 已證明只需替換一個 `B` 就能成為 `AAAA`，因此答案至少為 4。
後續沒有任何窗口能證明長度 5 可在一次替換內完成，最後回傳 `4`。

## Acceptance Harness

專案目前沒有 xUnit、NUnit 或 MSTest 測試專案。`Main` 是可重複執行的 acceptance
harness，每筆案例都使用手算的預期值呼叫真實的 `CharacterReplacement`：

| # | 案例 | `s` | `k` | 預期 |
| ---: | --- | --- | ---: | ---: |
| 1 | 官方範例二 | `AABABBA` | 1 | 4 |
| 2 | 官方範例一 | `ABAB` | 2 | 4 |
| 3 | 全部字元相同 | `AAAA` | 2 | 4 |
| 4 | 較大的替換額度 | `AABABBA` | 2 | 5 |
| 5 | 最小輸入 | `A` | 0 | 1 |
| 6 | 不允許替換 | `ABCDE` | 0 | 1 |
| 7 | 足以替換整個窗口 | `ABCDE` | 4 | 5 |

每筆案例會顯示 Expected、Actual 與 PASS/FAIL。只要有任一案例失敗，程式就會把 process
exit code 設為 1，讓命令列或自動化流程能偵測錯誤。

## 建置、驗證與執行

需求：

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

從此題目的 repository root 執行：

```bash
dotnet restore leetcode_424/leetcode_424.csproj
dotnet build leetcode_424/leetcode_424.csproj --nologo --no-restore
dotnet run --no-build --project leetcode_424/leetcode_424.csproj
git diff --check
```

目前沒有正式測試專案，因此行為驗證由建置與 `Main` 中的七筆 acceptance cases 完成。

### 實際執行輸出

以下內容來自完成建置後的 fresh run：

```text
Case: Official example 2
Input: s = "AABABBA", k = 1
Expected: 4
Actual: 4
Result: PASS

Case: Official example 1
Input: s = "ABAB", k = 2
Expected: 4
Actual: 4
Result: PASS

Case: All characters identical
Input: s = "AAAA", k = 2
Expected: 4
Actual: 4
Result: PASS

Case: Larger replacement budget
Input: s = "AABABBA", k = 2
Expected: 5
Actual: 5
Result: PASS

Case: Minimum input
Input: s = "A", k = 0
Expected: 1
Actual: 1
Result: PASS

Case: No replacements allowed
Input: s = "ABCDE", k = 0
Expected: 1
Actual: 1
Result: PASS

Case: Replace the whole window
Input: s = "ABCDE", k = 4
Expected: 5
Actual: 5
Result: PASS

Summary: 7/7 checks passed.
```

## 專案結構

```plaintext
.
├── AGENTS.md
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_424.sln
└── leetcode_424/
    ├── Program.cs
    └── leetcode_424.csproj
```
