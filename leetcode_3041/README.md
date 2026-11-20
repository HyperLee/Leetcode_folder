# LeetCode 3041：修改陣列後最大化連續元素數量

使用 C# / .NET 11 實作「排序＋Dictionary DP」與「排序＋兩個 DP 狀態」，由 `Main` 執行範例及正確性驗證。

- [英文題目](https://leetcode.com/problems/maximize-consecutive-elements-in-an-array-after-modification/description/)
- [中文題目](https://leetcode.cn/problems/maximize-consecutive-elements-in-an-array-after-modification/description/)

## 題目說明與限制

給定正整數陣列 `nums`，每個元素都可以獨立選擇保持原值或增加 `1`，最多一次。修改完成後選取一些元素，使它們排序後成為連續且不重複的整數，回傳最多可選幾個元素。

選取的元素不必在原陣列中相鄰，也不必使用全部元素。例如 `[3,4,5]` 合法，`[3,4,6]` 有缺口，`[1,1,2,3]` 有重複值。

- `1 <= nums.length <= 100000`
- `1 <= nums[i] <= 1000000`
- 修改後最大值為 `1000001`，數值與答案皆可使用 C# `int`。
- 兩種解法均以符合題目限制的非空陣列為前提，不額外定義 `null` 或空陣列行為。

範例一：`[2,1,5,1,1]` 的答案為 `3`。

```text
Original:      [2, 1, 5, 1, 1]
Modification:  +1  0  0 +1  0
Final:         [3, 1, 5, 2, 1]
Selected:       3  1     2
Sorted:        [1] -> [2] -> [3]       length = 3
```

範例二：`[1,4,7,10]` 的答案為 `1`。相鄰原值差 `3`，較小者即使加一，仍無法接到下一個值。

## 解題概念與出發點

暴力列舉每個元素是否加一，共有 `2^n` 種選擇，無法處理十萬筆輸入。我們需要將「哪些連續序列可以形成」壓縮成最佳長度。

首先排序：原位置不影響選取結果；從小到大處理時，較小值形成的序列可以用來延伸目前值。排序不會漏掉必須交換處理順序的答案，因為原值 `u < v` 時，`u+1 <= v`，修改後不會逆序；相同原值則可交換元素身分。

重複值仍然有用：兩個 `1` 可以分別提供 `1`、`2`。但三個 `1` 也只能提供這兩種不同值，不能形成長度 `3` 的連續序列。

## 解法一：排序＋Dictionary 動態規劃

方法：`public int MaxSelectedElements(int[] nums)`。

### 狀態與轉移設計

`dp[v]` 表示只使用已處理元素，能形成的「以 v 結尾」的最長連續序列長度。不存在的 key 視為 `0`。只儲存長度即可，因為題目沒有要求回傳選取路徑。

處理目前元素 `x` 前，保存三個舊狀態：

```text
prev = old dp[x-1]
curr = old dp[x]
next = old dp[x+1]

Keep x:       [..., x-2, x-1] + [x]   => dp[x]   = max(curr, prev+1)
Increase x:   [..., x-1, x]   + [x+1] => dp[x+1] = max(next, curr+1)
```

保持原值時，接在以 `x-1` 結尾的序列後；加一時，接在以 `x` 結尾的序列後。前綴不存在時，`0+1` 自然代表從目前元素開始的新序列。

取 `max` 是為了保留不使用目前元素的既有答案，避免重複值覆蓋較佳狀態。每一步也更新全域最大長度。

### 為什麼必須保存舊狀態？

同一元素只能使用一次。若先改寫 `dp[x]`，再讀取它更新 `dp[x+1]`，可能將同一元素同時當成原值及加一後的值。

```text
Input: [1]
Wrong:
  dp[1] = 1
  dp[2] = dp[1] + 1 = 2    <-- same element used twice

Correct:
  old dp[1] = 0
  dp[2] = 0 + 1 = 1
  dp[1] = 0 + 1 = 1
```

本實作先保存 `prev`、`curr`、`next`，兩種更新都只讀舊狀態。兩個狀態代表不同選擇下的最佳長度，不表示兩條序列需要同時存在於同一次修改中。

### 範例演示

輸入 `[2,1,5,1,1]`，排序為 `[1,1,1,2,5]`。下表列出每一步的完整 Dictionary；`v:L` 表示結尾為 `v`、長度為 `L`。

| 步驟 | x | 舊 prev / curr / next | 更新後狀態 | 全域答案 |
|---|---:|---|---|---:|
| 1 | 1 | 0 / 0 / 0 | `{1:1, 2:1}` | 1 |
| 2 | 1 | 0 / 1 / 1 | `{1:1, 2:2}` | 2 |
| 3 | 1 | 0 / 1 / 2 | `{1:1, 2:2}` | 2 |
| 4 | 2 | 1 / 2 / 0 | `{1:1, 2:2, 3:3}` | 3 |
| 5 | 5 | 0 / 0 / 0 | `{1:1, 2:2, 3:3, 5:1, 6:1}` | 3 |

```text
Sorted input:    1a    1b    1c     2     5
Selected use:     1     2    skip    3    skip
                  |     |           |
Chain:           [1] -> [2] ------> [3]
Length:           1     2           3

Last 5 starts [5] or [6]; earlier best length 3 remains.
```

`1a`、`1b`、`1c` 是不同位置的重複元素，實際程式不需要這些標記。

### 正確性理由

初始沒有元素，所有結尾的長度為零。假設舊狀態正確，加入 `x` 後，任何使用目前元素的新序列結尾只能是 `x` 或 `x+1`，分別由舊 `dp[x-1]`、`dp[x]` 延伸得到。

轉移完整列出兩種選擇，也保留不使用目前元素的舊答案，因此更新後狀態仍正確。所有步驟的最大長度就是答案。

### 複雜度與輸入副作用

- 排序 `O(n log n)`，DP 為 `O(n)` 次預期常數時間的 Dictionary 操作，總時間為預期 `O(n log n)`。
- 每個輸入最多產生兩種 key，Dictionary 額外空間為 `O(n)`。
- `Array.Sort(nums)` 會原地排序輸入；要保留原順序，請先複製陣列。

## 解法二：排序＋兩個 DP 狀態

方法：`public int MaxSelectedElementsConstantSpace(int[] nums)`。

### 壓縮狀態的出發點

Dictionary 保留所有結尾，但下一個元素不會小於目前原值 `p`。要建立下一個結尾，只需追蹤以 `p`、`p+1` 結尾的最佳長度；更早的較小結尾若可連接，已由這兩種狀態代表，否則會因距離太大而失去延伸機會。

無法延伸的歷史最佳序列仍由全域答案保存，不會因狀態重設而遺失。

- `unchanged`，以下記為 `a`：以最近原值 `p` 結尾的最佳長度。
- `incremented`，記為 `b`：以 `p+1` 結尾的最佳長度。
- `best`：目前曾經出現的最大長度。

狀態名稱描述的是結尾值，不保證序列一定使用最近那一筆元素。第一個元素可形成 `[p]` 或 `[p+1]`，所以 `a = b = best = 1`。

### 四種差值的轉移推導

令下一個值為 `x`、`gap = x-p`。下表右側一律使用更新前的 `a`、`b`。

| gap | 新 a：結尾 x | 新 b：結尾 x+1 | 原因 |
|---|---|---|---|
| 0 | `a` | `max(b,a+1)` | 相同值不能接在自己後面；加一可延伸舊 a |
| 1 | `max(a+1,b)` | `b+1` | 舊 a 可接上 x，舊 b 本來就以 x 結尾，也要保留 |
| 2 | `b+1` | `1` | 舊 b 的結尾是 x-1；目前值若加一則仍有缺口 |
| >2 | `1` | `1` | 兩種新結尾皆無法接上，重新開始 |

```text
gap=0:   old a ends at x     -- append x+1 --> new b
         old a stays                         new a
         old b competes with new b

gap=1:   old a ends at x-1   -- append x   --> new a
         old b ends at x    -- keep chain --> new a
         old b ends at x    -- append x+1 --> new b

gap=2:   old b ends at x-1   -- append x   --> new a
         no old endpoint at x                new b = 1

gap>2:   [old chain] ... missing values ... [x] or [x+1]
                                             1       1
```

程式以 `oldUnchanged`、`oldIncremented` 保存舊值，避免賦值順序影響轉移；完成後更新 `best`，並將 `p` 移到 `x`。

### 範例演示

同樣使用 `[2,1,5,1,1]`，排序後為 `[1,1,1,2,5]`。

| 已處理的 x | gap | a：結尾 x | b：結尾 x+1 | best |
|---|---|---:|---:|---:|
| 第一個 1 | 初始化 | 1 | 1 | 1 |
| 第二個 1 | 0 | 1 | 2 | 2 |
| 第三個 1 | 0 | 1 | 2 | 2 |
| 2 | 1 | 2 | 3 | 3 |
| 5 | 3 | 1 | 1 | 3 |

```text
Read 1a:   a: [1]          b: [2]              best=1
Read 1b:   a: [1]          b: [1 -> 2]         best=2
Read 1c:   a: [1]          b: [1 -> 2]         best=2
Read 2:    a: [1 -> 2]     b: [1 -> 2 -> 3]    best=3
Read 5:    a: [5]          b: [6]              best=3
```

最後狀態都變為 `1`，但 `best` 仍保存先前的 `3`。

差值為 `2` 的 `[1,3]`：

```text
Original:    [1]       [3]
Choices:    1 or 2    3 or 4
Select:        [2] -> [3]       length=2

old b ends at 2; keep 3 => new a=old b+1=2
current 3 becomes 4    => new b=1
```

差值為 `3` 的 `[1,4]` 無法橋接：`{1,2}` 與 `{4,5}` 間至少缺一個整數，答案為 `1`。

### 正確性理由

第一個元素的兩種結尾正確。假設目前 `a`、`b` 是以 `p`、`p+1` 結尾的最佳長度，下一個值與 `p` 的差只可能落在表中四種情況。

`gap=0` 時，更早元素不可能已提供 `x+2`；`gap=1` 時也不可能已提供 `x+1`，故表中無須另保留這些結尾。`gap>=2` 時，更早元素的最大可能值為 `p+1`，只剩表中列出的橋接或重新開始。

表格已列出保持、加一、保留舊序列的所有可行連接，因此新狀態仍正確；無法延伸的歷史答案由 `best` 保存，最終回傳全域最佳長度。

### 複雜度與輸入副作用

- 排序 `O(n log n)`、DP 掃描 `O(n)`，總時間 `O(n log n)`。
- **DP 狀態**額外空間為 `O(1)`；計入 .NET 陣列排序的呼叫堆疊，整體輔助空間為 `O(log n)`。
- 同樣會原地排序輸入。名稱 ConstantSpace 指排序後的 DP 狀態，不表示排序完全不用堆疊。

## 兩種解法比較

| 面向 | Dictionary DP | 兩狀態 DP |
|---|---|---|
| 狀態 | 每個已知結尾的最佳長度 | 最近原值與原值加一的最佳長度 |
| 時間 | 預期 O(n log n) | O(n log n) |
| DP 額外空間 | O(n) | O(1) |
| 含排序的輔助空間 | O(n) | O(log n) |
| 學習重點 | 依結尾值推導轉移 | 利用排序與最大增量壓縮狀態 |
| 注意事項 | 更新只依賴舊狀態 | 四種差值與全域 best |

建議先理解 Dictionary 版本，再理解壓縮版本。本專案沒有量測兩者的效能差異。

## 可執行測試設計

`Main` 呼叫 `RunTests()`，不需要參數或外部測試套件。

1. **11 組固定案例**：兩個題目範例、單一元素、兩個與三個重複值、已連續數列、可橋接與不可橋接缺口、重複值延伸、最大值、反向輸入。
2. **791 組小範圍窮舉**：長度 1 到 7、值域 1 到 5 的所有非遞減陣列，兩種解法均與獨立暴力法比對。答案只與元素多重集合有關，無須重複枚舉排列；另以反向固定案例驗證排序行為。
3. **2 組長度上限案例**：100000 個 `1`，預期 `2`；`1..100000` 連續數列，預期 `100000`。

暴力法列舉 `2^m` 種加一選擇，以 `SortedSet` 去重排序，直接掃描最長連續段，不使用正式解法的 DP 轉移。去重不會損失答案，因相同最終值最多只能選一個。

暴力驗證時間為 `O(2^m * m log m)`、額外空間 `O(m)`，僅供長度最多 7 的小案例。非遞減陣列總數為 `sum(C(m+4,4), m=1..7) = C(12,5)-1 = 791`。

每次呼叫解法都傳入獨立副本，避免原地排序污染原始資料。固定案例顯示兩種結果，窮舉與大案例僅顯示摘要。若結果不符，例外會列出輸入、預期值及兩種解法的實際值，程序以非零狀態結束。

## 建置、執行與格式檢查

安裝支援 `net11.0` 的 .NET 11 SDK，在本 README 所在的 `leetcode_3041` 目錄執行：

```powershell
dotnet build leetcode_3041/leetcode_3041.csproj
dotnet run --project leetcode_3041/leetcode_3041.csproj
dotnet format leetcode_3041/leetcode_3041.csproj --verify-no-changes
git diff --check
```

沒有獨立測試專案，`dotnet run` 同時執行範例與完整驗證。成功時先輸出 11 行固定案例，例如：

```text
PASS [2,1,5,1,1] expected=3, DictionaryDP=3, ConstantSpaceDP=3
```

最後摘要為：

```text
Fixed cases passed: 11
Exhaustive cases passed: 791 (length 1..7, values 1..5)
Large cases passed: 2 (length 100000)
All 804 cases passed for both solutions.
```

### 實際驗證紀錄

2026-11-20，使用 .NET SDK 11.0.100：

| 命令 | 結果 |
|---|---|
| `dotnet build leetcode_3041/leetcode_3041.csproj` | 成功，0 警告、0 錯誤 |
| `dotnet run --project leetcode_3041/leetcode_3041.csproj` | 兩種解法皆通過 804 組案例 |
| `dotnet format leetcode_3041/leetcode_3041.csproj --verify-no-changes` | 成功，結束碼 0 |
| `git diff --check` | 成功，無空白或換行錯誤 |
