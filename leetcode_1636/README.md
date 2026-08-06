# LeetCode 1636 - Sort Array by Increasing Frequency（按照頻率將陣列升序排序）

- [LeetCode English](https://leetcode.com/problems/sort-array-by-increasing-frequency/)
- [LeetCode 中文](https://leetcode.cn/problems/sort-array-by-increasing-frequency/)

## 題目說明

給定整數陣列 `nums`，依每個數值在陣列中的出現頻率重新排序：

1. 頻率不同時，出現次數較少的數值排在前面。
2. 頻率相同時，數值較大的排在前面。

方法需回傳排序後的陣列。本專案提供兩種實作，兩者都建立新的輸出陣列，不會修改呼叫者傳入的 `nums`。

## 限制條件

- `1 <= nums.Length <= 100`
- `-100 <= nums[i] <= 100`
- 輸入由題目保證為非 `null` 且符合上述值域；兩個解法不額外處理題目範圍外的輸入。

## 公開 API

```csharp
public static int[] FrequencySort(int[] nums);
public static int[] FrequencySort2(int[] nums);
```

兩個方法都遵守相同契約：

- 輸入：符合題目限制的整數陣列。
- 輸出：依「頻率升序、同頻數值降序」排列的新陣列。
- 輸入保持：呼叫完成後，`nums` 的內容與順序不變。

## 解題概念與出發點

排序規則包含兩層優先順序，所以無論採用哪一種方法，都必須先知道每個數值的頻率：

1. 第一排序鍵為頻率，方向是升序。
2. 第二排序鍵為數值，方向是降序。

第一種解法直接將這兩個鍵寫入比較器，適用於較一般的整數值域。第二種解法則利用本題值域只有
`-100..100` 的條件，把數值映射到固定陣列並依頻率分桶，避開元素之間的比較排序。

## 解法一：Dictionary + 自訂比較排序

### 設計說明

`FrequencySort` 分成兩個階段：

1. 走訪 `nums`，以 `Dictionary<int, int>` 保存「數值 → 頻率」。
2. 將 `nums` 複製到 `List<int>`，呼叫 `List.Sort` 並提供自訂比較器：
   - 若兩個數值的頻率不同，比較頻率，較小者在前。
   - 若頻率相同，反向比較數值，較大者在前。

排序發生在 List 副本上，因此原始陣列不會被改動。Dictionary 只保存不同數值的頻率，不依賴題目的
固定值域，所以如果數值範圍日後擴大，這個方法仍可直接使用。

### 為什麼正確

比較器先判斷第一排序鍵「頻率」。頻率較小的元素一定被放到頻率較大的元素之前，因此所有頻率群組
會依出現次數升序排列。在同一頻率群組內，比較器改用 `second.CompareTo(first)`，使較大的數值排在
較小的數值之前。兩層比較順序正好對應題目規則，因此排序結果正確。

### 複雜度

令 `n` 為陣列長度、`k` 為不同數值數量：

- 時間複雜度：統計頻率為 `O(n)`，比較排序為 `O(n log n)`，合計 `O(n log n)`。
- 空間複雜度：Dictionary、List 副本與回傳陣列合計 `O(n + k)`，可簡化為 `O(n)`。

### 範例演示：`[2,3,1,3,2]`

先統計頻率：

| 數值 | 頻率 |
| ---: | ---: |
| 2 | 2 |
| 3 | 2 |
| 1 | 1 |

排序過程的判斷：

1. `1` 的頻率為 1，比頻率為 2 的 `2`、`3` 優先，因此先輸出 `1`。
2. `2` 與 `3` 的頻率同為 2，改以數值降序比較，所以 `3` 排在 `2` 前。
3. 每個值仍保留原本的出現次數，得到 `[1,3,3,2,2]`。

## 解法二：固定值域計數 + 頻率桶

### 設計說明

`FrequencySort2` 利用 `nums[i]` 一定介於 `-100` 與 `100`：

1. 建立長度 201 的 `frequencies`，用 `value + 100` 將負數轉成合法索引並統計頻率。
2. 建立 `nums.Length + 1` 個頻率桶；索引 `f` 代表出現 `f` 次的不同數值。
3. 從數值 `100` 反向掃描至 `-100`，將每個存在的數值放入對應頻率桶。因為掃描方向由大到小，
   同一桶內的數值自然符合降序，不需要再次排序。
4. 從頻率 1 依序走到 `nums.Length`，把桶中的每個數值重複寫入 `f` 次。

此方法只讀取 `nums` 並建立新的計數、分桶與輸出結構，因此同樣保留輸入不變。

### 為什麼正確

外層以頻率 1、2、3……的順序展開各桶，所以較低頻率一定先寫入結果。每個桶的數值是在反向掃描
固定值域時加入，因此同頻數值保持由大到小。最後，每個數值依記錄的頻率寫入相同次數，不會遺漏
或增加元素。這三個性質共同滿足題目的完整排序與元素數量要求。

### 複雜度

令 `n` 為陣列長度、`R = 201` 為固定值域大小：

- 時間複雜度：統計 `O(n)`、掃描值域 `O(R)`、展開結果 `O(n)`，合計 `O(n + R)`；本題 `R` 固定時可視為 `O(n)`。
- 空間複雜度：計數陣列、頻率桶與輸出陣列合計 `O(n + R)`。

### 範例演示：`[2,3,1,3,2]`

1. 計數結果為 `frequency[1] = 1`、`frequency[2] = 2`、`frequency[3] = 2`。
2. 由數值 100 往 -100 掃描：
   - 頻率 1 的桶得到 `[1]`。
   - 頻率 2 的桶依降序得到 `[3,2]`。
3. 先展開頻率 1：寫入 `1` 一次，結果為 `[1]`。
4. 再展開頻率 2：寫入 `3` 兩次、`2` 兩次，結果為 `[1,3,3,2,2]`。

## 解法比較

| 項目 | `FrequencySort` | `FrequencySort2` |
| --- | --- | --- |
| 核心策略 | Dictionary + 自訂比較排序 | 固定值域計數 + 頻率桶 |
| 時間複雜度 | `O(n log n)` | `O(n + R)`，`R = 201` |
| 空間複雜度 | `O(n + k)` | `O(n + R)` |
| 值域依賴 | 不依賴固定值域 | 依賴 `-100..100` |
| 同頻降序 | 比較器反向比較數值 | 反向掃描值域 |
| 是否修改輸入 | 否 | 否 |

第一種解法較通用，也直接表達雙排序鍵；第二種解法善用題目限制，以固定掃描成本換取線性展開流程。

## Acceptance harness

`Main` 對每個案例分別執行兩種解法。每次執行都使用獨立輸入副本，並同時檢查預期輸出與輸入保持：

| Case | 輸入 | 預期輸出 | 驗證重點 |
| --- | --- | --- | --- |
| Existing sample | `[1,5,0,5]` | `[1,0,5,5]` | 保留原專案範例。 |
| Official example 1 | `[1,1,2,2,2,3]` | `[3,1,1,2,2,2]` | 三種不同頻率。 |
| Official example 2 | `[2,3,1,3,2]` | `[1,3,3,2,2]` | 同頻時數值降序。 |
| Official example 3 | `[-1,1,-6,4,5,-6,1,4,1]` | `[5,-1,4,4,-6,-6,1,1,1]` | 正負數與多頻率群組。 |
| Single lower bound | `[-100]` | `[-100]` | 最小值與單一元素。 |
| Repeated boundaries | `[-100,100,-100,100,0]` | `[0,100,100,-100,-100]` | 上下界與同頻排序。 |
| All distinct tie-break | `[4,-2,7,0]` | `[7,4,0,-2]` | 全部同頻時完全降序。 |
| All equal | `[7,7,7]` | `[7,7,7]` | 單一數值重複。 |

任一結果錯誤或輸入遭修改時，程式會將結束碼設為 1，適合在終端機或 CI 環境重複驗證。

## 建置與執行

以下命令的工作目錄是 `leetcode_1636/` 題目根目錄：

```bash
dotnet restore leetcode_1636/leetcode_1636.csproj
dotnet build leetcode_1636/leetcode_1636.csproj --nologo
dotnet run --project leetcode_1636/leetcode_1636.csproj --no-build
```

本專案沒有獨立測試專案；`Main` 中的 deterministic acceptance harness 是目前的可執行驗證入口。

## Fresh run 輸出

```text
Case: Existing sample
Solution: FrequencySort - dictionary and comparison sort
Input: [1,5,0,5]
Expected: [1,0,5,5]
Actual: [1,0,5,5]
Input preserved: PASS
Result: PASS

Case: Existing sample
Solution: FrequencySort2 - frequency buckets
Input: [1,5,0,5]
Expected: [1,0,5,5]
Actual: [1,0,5,5]
Input preserved: PASS
Result: PASS

Case: Official example 1
Solution: FrequencySort - dictionary and comparison sort
Input: [1,1,2,2,2,3]
Expected: [3,1,1,2,2,2]
Actual: [3,1,1,2,2,2]
Input preserved: PASS
Result: PASS

Case: Official example 1
Solution: FrequencySort2 - frequency buckets
Input: [1,1,2,2,2,3]
Expected: [3,1,1,2,2,2]
Actual: [3,1,1,2,2,2]
Input preserved: PASS
Result: PASS

Case: Official example 2
Solution: FrequencySort - dictionary and comparison sort
Input: [2,3,1,3,2]
Expected: [1,3,3,2,2]
Actual: [1,3,3,2,2]
Input preserved: PASS
Result: PASS

Case: Official example 2
Solution: FrequencySort2 - frequency buckets
Input: [2,3,1,3,2]
Expected: [1,3,3,2,2]
Actual: [1,3,3,2,2]
Input preserved: PASS
Result: PASS

Case: Official example 3
Solution: FrequencySort - dictionary and comparison sort
Input: [-1,1,-6,4,5,-6,1,4,1]
Expected: [5,-1,4,4,-6,-6,1,1,1]
Actual: [5,-1,4,4,-6,-6,1,1,1]
Input preserved: PASS
Result: PASS

Case: Official example 3
Solution: FrequencySort2 - frequency buckets
Input: [-1,1,-6,4,5,-6,1,4,1]
Expected: [5,-1,4,4,-6,-6,1,1,1]
Actual: [5,-1,4,4,-6,-6,1,1,1]
Input preserved: PASS
Result: PASS

Case: Single lower bound
Solution: FrequencySort - dictionary and comparison sort
Input: [-100]
Expected: [-100]
Actual: [-100]
Input preserved: PASS
Result: PASS

Case: Single lower bound
Solution: FrequencySort2 - frequency buckets
Input: [-100]
Expected: [-100]
Actual: [-100]
Input preserved: PASS
Result: PASS

Case: Repeated boundaries
Solution: FrequencySort - dictionary and comparison sort
Input: [-100,100,-100,100,0]
Expected: [0,100,100,-100,-100]
Actual: [0,100,100,-100,-100]
Input preserved: PASS
Result: PASS

Case: Repeated boundaries
Solution: FrequencySort2 - frequency buckets
Input: [-100,100,-100,100,0]
Expected: [0,100,100,-100,-100]
Actual: [0,100,100,-100,-100]
Input preserved: PASS
Result: PASS

Case: All distinct tie-break
Solution: FrequencySort - dictionary and comparison sort
Input: [4,-2,7,0]
Expected: [7,4,0,-2]
Actual: [7,4,0,-2]
Input preserved: PASS
Result: PASS

Case: All distinct tie-break
Solution: FrequencySort2 - frequency buckets
Input: [4,-2,7,0]
Expected: [7,4,0,-2]
Actual: [7,4,0,-2]
Input preserved: PASS
Result: PASS

Case: All equal
Solution: FrequencySort - dictionary and comparison sort
Input: [7,7,7]
Expected: [7,7,7]
Actual: [7,7,7]
Input preserved: PASS
Result: PASS

Case: All equal
Solution: FrequencySort2 - frequency buckets
Input: [7,7,7]
Expected: [7,7,7]
Actual: [7,7,7]
Input preserved: PASS
Result: PASS

Summary: 16/16 checks passed.
```

## 專案結構

```text
leetcode_1636/
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_1636/
    ├── Program.cs
    └── leetcode_1636.csproj
```