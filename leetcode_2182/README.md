# LeetCode 2182：建構限制重複的字串

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![Language](https://img.shields.io/badge/language-C%23-239120)](https://learn.microsoft.com/dotnet/csharp/)

這是一個以 .NET 10 Console App 實作的 LeetCode 教學專案。專案提供「頻率陣列＋雙指標」與「最大優先佇列」兩種貪婪解法，並透過固定案例比較兩種設計是否都能建立符合連續限制的字典序最大字串。

## 快速導覽

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：頻率陣列與雙指標](#解法一頻率陣列與雙指標)
- [解法二：最大優先佇列](#解法二最大優先佇列)
- [解法比較](#解法比較)
- [可執行測試設計](#可執行測試設計)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)

## 題目說明

給定只含小寫英文字母的字串 `s` 與正整數 `repeatLimit`，使用 `s` 中的字元建立一個新字串，並符合以下要求：

1. 同一字元連續出現的次數不得超過 `repeatLimit`。
2. 每個字元的使用次數不得超過它在 `s` 中的出現次數。
3. 不要求使用 `s` 的所有字元；若剩餘字元無法合法加入，可以捨棄。
4. 在所有合法結果中，回傳字典序最大的字串。

比較字典序時，先看兩個字串第一個不同的位置，該位置字母較大的字串較大；若較短長度內的字元完全相同，則較長的字串較大。

題目連結：[2182. Construct String With Repeat Limit](https://leetcode.com/problems/construct-string-with-repeat-limit/description/)

### 官方範例一

```text
輸入：s = "cczazcc", repeatLimit = 3
輸出："zzcccac"
```

結果優先使用字典序最大的 `z`，再使用 `c`。四個 `c` 不能連續放在一起，因此以 `a` 打斷後再放入最後一個 `c`。

### 官方範例二

```text
輸入：s = "aababab", repeatLimit = 2
輸出："bbabaa"
```

輸入共有三個 `b` 與四個 `a`。結果先取兩個 `b`，以一個 `a` 中斷，再取剩餘的 `b`。最後只能加入兩個 `a`，因為已沒有其他字元可打斷連續區段，所以必須捨棄最後一個 `a`。

## 限制條件

- `1 <= repeatLimit <= s.Length <= 10^5`
- `s` 只包含小寫英文字母 `a` 到 `z`。

題目保證輸入符合上述條件，因此兩個公開方法專注於演算法本身，不額外處理 `null`、空字串、非小寫字母或超出範圍的 `repeatLimit`。

## 解題概念與出發點

目標是「字典序最大」，因此每個位置都應優先選擇目前仍可合法使用的最大字元。這形成三個關鍵貪婪判斷：

1. 若最大字元尚未達到連續上限，加入它一定比加入任何較小字元更好。
2. 若最大字元已達上限但仍有剩餘，必須加入一個不同字元作為分隔；此時選擇可用的次大字元，才能讓目前位置保持最大。
3. 一個分隔字元已足以解除連續限制。加入後應立即回頭嘗試最大字元，不應多放較小字元。

如果最大字元仍有剩餘、卻找不到任何較小字元作為分隔，就無法再合法延長結果。此時直接停止才是正確行為，不能為了用完輸入而破壞限制。

兩種實作都先計算 26 個字母的頻率，差異在於如何維護「目前最大」與「目前次大」字元：

- `RepeatLimitedString` 以兩個由大往小移動的索引直接掃描頻率陣列。
- `RepeatLimitedStringWithPriorityQueue` 將仍有剩餘數量的字元放入優先佇列，動態取出最大與次大字元。

## 解法一：頻率陣列與雙指標

### 設計說明

`RepeatLimitedString` 使用長度為 26 的 `remainingCount` 保存每個字母尚未使用的數量，並維護兩個索引：

- `primaryIndex`：目前仍有剩餘數量的最大字元。
- `separatorIndex`：需要中斷連續區段時，可使用的次大字元候選。

此外，`consecutiveCount` 記錄 `primaryIndex` 對應字元目前已連續加入幾次。

演算法流程如下：

1. 統計每個字母的出現次數。
2. `primaryIndex` 從 `z` 開始向較小字母移動。
3. 若目前最大字元已用完，將 `primaryIndex` 左移並重設連續計數。
4. 若連續次數尚未達上限，加入一個最大字元並更新剩餘數量與連續計數。
5. 若已達上限，讓 `separatorIndex` 向下尋找仍有剩餘、且小於 `primaryIndex` 的最大字元。
6. 找到分隔字元後只加入一個，接著將連續計數歸零。
7. 若找不到分隔字元，迴圈結束；無法合法加入的字元不使用。

`primaryIndex` 與 `separatorIndex` 都只會由大往小移動，不會反覆從 `z` 重新掃描。每次成功加入字元也會消耗一個輸入字元，因此整體工作量與輸入長度成正比。

### 虛擬碼

```text
remainingCount = 26 個字母的頻率
primary = z
separator = y
consecutive = 0

while primary 與 separator 仍在有效範圍:
    if primary 已用完:
        primary 往較小字母移動
        consecutive = 0
    else if consecutive < repeatLimit:
        加入 primary
        consecutive += 1
    else if separator 不可用或不小於 primary:
        separator 往較小字母移動
    else:
        加入一個 separator
        consecutive = 0

return result
```

### 範例演示流程

使用 `s = "cczazcc"`、`repeatLimit = 3`，初始頻率為 `z:2, c:4, a:1`：

| 步驟 | 最大字元 | 分隔字元 | 動作 | 結果 |
| --- | --- | --- | --- | --- |
| 1 | `z:2` | 尚不需要 | `z` 未達限制，加入兩次 | `zz` |
| 2 | `c:4` | 尚不需要 | `z` 用完後移到 `c` | `zz` |
| 3 | `c:4` | 尚不需要 | 連續加入三個 `c`，達到限制 | `zzccc` |
| 4 | `c:1` | `a:1` | `c` 仍有剩餘，加入一個 `a` 打斷 | `zzccca` |
| 5 | `c:1` | 不再需要 | 連續計數歸零，可加入最後一個 `c` | `zzcccac` |
| 6 | 無 | 無 | 所有可用字元皆已處理，停止 | `zzcccac` |

### 正確性直覺

只要最大字元仍可合法加入，改放任何較小字元都會讓結果在目前位置變小。最大字元達到上限時，結果若要繼續就必須放入不同字元；選擇可用的最大分隔字元，能讓這個被迫降低的字元位置盡可能大。分隔一次後立即恢復使用最大字元，也避免不必要地提前加入更多較小字元。

### 複雜度

- 時間複雜度：`O(n + 26)`，其中 `n` 為 `s.Length`；每個被使用的字元只會附加一次，兩個索引也只會單向掃過字母表。
- 額外空間複雜度：`O(26)`，不含輸出字串本身。

## 解法二：最大優先佇列

### 設計說明

`RepeatLimitedStringWithPriorityQueue` 將每種仍有剩餘數量的字元與數量存成 `(LetterIndex, RemainingCount)`，放入 `PriorityQueue`。由於 .NET 的 `PriorityQueue` 預設先取最小 priority，實作使用負的字母索引作為 priority：`z` 的 priority 為 `-25`，因此會比 `a` 的 `0` 更早出列，形成最大堆效果。

每輪處理步驟如下：

1. 取出目前字典序最大的字元。
2. 一次加入 `min(剩餘數量, repeatLimit)` 個，減少不必要的逐字元佇列操作。
3. 若最大字元已用完，直接進入下一輪。
4. 若最大字元仍有剩餘，必須再取出目前次大的字元作為分隔。
5. 若佇列已空，表示沒有分隔字元，停止並捨棄最大字元的剩餘數量。
6. 加入一個分隔字元；分隔字元若還有剩餘便重新入列。
7. 將尚未用完的最大字元重新入列，下一輪再次參與排序。

優先佇列讓「最大」與「次大」的選擇不依賴固定字母表掃描。若字元種類不是固定 26 個，這種設計也較容易延伸。

### 虛擬碼

```text
maxHeap = 所有出現過的字母與剩餘數量

while maxHeap 不為空:
    current = 取出最大字元
    加入最多 repeatLimit 個 current

    if current 已用完:
        continue

    if maxHeap 已空:
        break

    separator = 取出次大字元
    加入一個 separator

    若 separator 仍有剩餘，放回 maxHeap
    將仍有剩餘的 current 放回 maxHeap

return result
```

### 範例演示流程

使用 `s = "aababab"`、`repeatLimit = 2`，初始優先佇列包含 `b:3, a:4`：

| 輪次 | 取出的最大字元 | 批次加入 | 分隔處理 | 重新入列 | 結果 |
| --- | --- | --- | --- | --- | --- |
| 1 | `b:3` | 加入 `bb`，剩 `b:1` | 取出 `a:4` 並加入一個 `a` | `b:1, a:3` | `bba` |
| 2 | `b:1` | 加入 `b`，`b` 用完 | 不需要分隔 | `a:3` | `bbab` |
| 3 | `a:3` | 加入 `aa`，剩 `a:1` | 佇列已空，沒有其他字元可分隔 | 不再入列 | `bbabaa` |

最後一個 `a` 若繼續加入會形成三個連續的 `a`，超過限制，因此正確答案必須停在 `"bbabaa"`。

### 正確性直覺

優先佇列每次都提供仍可選擇的最大字元。批次加入最大字元直到限制，不會錯過更大的結果；若仍有剩餘，下一個位置只能使用不同字元，而佇列中下一個元素正是可用的最大分隔字元。重新入列後重複相同選擇，因此每個位置都採用當下合法的最大字元。

### 複雜度

- 時間複雜度：`O(n log 26)`；每次出列或入列的成本為 `O(log 26)`，而處理次數不超過輸入規模的常數倍。
- 額外空間複雜度：`O(26)`，優先佇列最多保存 26 種字元，不含輸出字串本身。

## 解法比較

| 解法 | 核心資料結構 | 取最大／次大方式 | 時間複雜度 | 額外空間 | 特點 |
| --- | --- | --- | --- | --- | --- |
| `RepeatLimitedString` | 長度 26 的頻率陣列 | 兩個單向遞減索引 | `O(n + 26)` | `O(26)` | 充分利用固定小寫字母表，效率與常數成本最佳 |
| `RepeatLimitedStringWithPriorityQueue` | 頻率陣列＋`PriorityQueue` | 動態出列最大與次大元素 | `O(n log 26)` | `O(26)` | 狀態轉移直觀，也較容易延伸到更大的字元集合 |

兩個方法都不會修改輸入字串，並且都可能捨棄無法在限制內合法加入的剩餘字元。

## 可執行測試設計

`Main` 會執行 6 組固定案例，每組分別驗證兩種解法，共 12 項檢查：

- 兩個官方範例。
- 只有單一種類字元，達到上限後必須停止。
- `repeatLimit` 足以容納每種字元，不需捨棄內容。
- 次大字元需要多次作為分隔。
- 長度為 1 的最小合法輸入。

每項檢查都列出 Expected、Actual 與 PASS/FAIL。若任一檢查失敗，程式會設定非零結束碼，讓命令列或自動化流程可以偵測失敗。

## 專案結構

```text
leetcode_2182/
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_2182.sln
└── leetcode_2182/
    ├── leetcode_2182.csproj
    └── Program.cs
```

## 建置與執行

需求：安裝支援 `net10.0` 的 .NET 10 SDK。

從本 repository 根目錄執行：

```bash
dotnet restore leetcode_2182/leetcode_2182.csproj
dotnet build leetcode_2182/leetcode_2182.csproj --nologo --no-restore
dotnet run --no-build --project leetcode_2182/leetcode_2182.csproj
```

目前沒有獨立的自動化測試專案；`Main` 中的 12 項自我檢查、成功建置與實際執行結果共同作為行為驗收依據。

若要確認程式碼格式與差異空白，可再執行：

```bash
dotnet format leetcode_2182/leetcode_2182.csproj --verify-no-changes --no-restore
git diff --check
```

## 實際執行結果

以下內容來自 `dotnet run --no-build --project leetcode_2182/leetcode_2182.csproj` 的實際輸出：

```text
Case: 官方範例一
Input: s = "cczazcc", repeatLimit = 3
RepeatLimitedString:
  Expected: "zzcccac"
  Actual:   "zzcccac"
  Result:   PASS
RepeatLimitedStringWithPriorityQueue:
  Expected: "zzcccac"
  Actual:   "zzcccac"
  Result:   PASS

Case: 官方範例二
Input: s = "aababab", repeatLimit = 2
RepeatLimitedString:
  Expected: "bbabaa"
  Actual:   "bbabaa"
  Result:   PASS
RepeatLimitedStringWithPriorityQueue:
  Expected: "bbabaa"
  Actual:   "bbabaa"
  Result:   PASS

Case: 無分隔字元時捨棄剩餘內容
Input: s = "zzzz", repeatLimit = 2
RepeatLimitedString:
  Expected: "zz"
  Actual:   "zz"
  Result:   PASS
RepeatLimitedStringWithPriorityQueue:
  Expected: "zz"
  Actual:   "zz"
  Result:   PASS

Case: 限制未造成截斷
Input: s = "abcabc", repeatLimit = 3
RepeatLimitedString:
  Expected: "ccbbaa"
  Actual:   "ccbbaa"
  Result:   PASS
RepeatLimitedStringWithPriorityQueue:
  Expected: "ccbbaa"
  Actual:   "ccbbaa"
  Result:   PASS

Case: 多次使用次大字元分隔
Input: s = "ccbccb", repeatLimit = 2
RepeatLimitedString:
  Expected: "ccbccb"
  Actual:   "ccbccb"
  Result:   PASS
RepeatLimitedStringWithPriorityQueue:
  Expected: "ccbccb"
  Actual:   "ccbccb"
  Result:   PASS

Case: 最小合法輸入
Input: s = "a", repeatLimit = 1
RepeatLimitedString:
  Expected: "a"
  Actual:   "a"
  Result:   PASS
RepeatLimitedStringWithPriorityQueue:
  Expected: "a"
  Actual:   "a"
  Result:   PASS

Summary: 12/12 checks passed.
```
