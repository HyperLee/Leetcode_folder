# LeetCode 1482：製作 m 束花所需的最少天數

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![Language](https://img.shields.io/badge/language-C%23-239120)](https://learn.microsoft.com/dotnet/csharp/)

這是一個可直接執行的 .NET 10 主控台教學專案，示範如何以「答案範圍二分搜尋」和「排序候選開花日」解決 LeetCode 1482。`Main` 會執行固定案例，自動比對 Expected、Actual，並確認兩種解法都不會修改輸入陣列。

## 快速連結

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [共用判斷：能否製作足夠花束](#共用判斷能否製作足夠花束)
- [解法一：答案範圍二分搜尋](#解法一答案範圍二分搜尋)
- [解法二：排序候選開花日](#解法二排序候選開花日)
- [兩種解法比較](#兩種解法比較)
- [範例演示流程](#範例演示流程)
- [建置與執行](#建置與執行)

## 題目說明

有一排共 `n` 朵花，第 `i` 朵花會在 `bloomDay[i]` 當天開花。要製作一束花，必須使用 `k` 朵**相鄰且已開花**的花；每朵花只能使用一次。

給定需要製作的花束數量 `m`，請回傳能完成 `m` 束花的最少等待天數。如果花朵數量或排列方式使任務無法完成，回傳 `-1`。

題目連結：[1482. Minimum Number of Days to Make m Bouquets](https://leetcode.com/problems/minimum-number-of-days-to-make-m-bouquets/description/)

### 關鍵規則

1. 第 `days` 天時，只有滿足 `bloomDay[i] <= days` 的花可以使用。
2. 同一束中的 `k` 朵花必須在陣列中連續相鄰。
3. 一朵花放入某束後，不能再放入其他花束。
4. 只要需要的花朵總量 `m * k` 大於 `n`，一定無解。

## 限制條件

- `bloomDay.length == n`
- `1 <= n <= 10^5`
- `1 <= bloomDay[i] <= 10^9`
- `1 <= m <= 10^6`
- `1 <= k <= n`

> [!NOTE]
> 本專案另外測試空陣列並預期回傳 `-1`，確認花朵不足的防禦流程可正常運作。空陣列不是題目正式輸入範圍的一部分。

## 解題概念與出發點

直接嘗試所有日期並不實際，因為開花日最高可達 `10^9`。真正重要的觀察有兩個：

1. **可行性具有單調性。** 如果第 `d` 天已能完成 `m` 束花，那麼第 `d + 1` 天及之後也一定能完成，因為可用花朵只會增加，不會減少。
2. **答案只可能在某朵花開花時改變。** 若第 5 天到第 9 天沒有任何新花開放，這些日期能使用的花完全相同，不需要逐日重複檢查。

因此可以形成兩種解法：

- 在最小與最大開花日之間，以二分搜尋找出第一個可行日。
- 將所有可能改變狀態的開花日排序，只檢查不重複候選值。

兩種方法都共用 `CanMake`，判斷指定日期是否能形成足夠花束。

## 共用判斷：能否製作足夠花束

方法：`CanMake(int[] bloomDay, int days, int m, int k)`

### 設計說明

由左至右掃描花園，使用 `flowers` 記錄目前連續開花的數量，並以 `bouquets` 記錄已完成的花束數：

1. 若 `bloomDay[i] <= days`，目前花朵已開花，將 `flowers` 加一。
2. 當 `flowers == k` 時完成一束，將 `bouquets` 加一，並把 `flowers` 歸零。歸零代表這些花已被使用，不能重複放入下一束。
3. 若 `bloomDay[i] > days`，目前花朵尚未開放，相鄰區段被切斷，將 `flowers` 歸零。
4. 一旦 `bouquets == m` 即可提前停止，不必再掃描剩餘元素。

這個貪心計數是安全的：對每個連續開花區段，從左側每湊滿 `k` 朵就立刻製作一束，能得到該區段可形成的最大花束數。

### 相鄰性示例

考慮 `bloomDay = [7, 7, 7, 7, 12, 7, 7]`、`m = 2`、`k = 3`：

- 第 7 天的狀態是 `[開, 開, 開, 開, 未開, 開, 開]`。
- 前四朵只能形成一束，剩餘一朵不足以形成第二束。
- 日期 12 的花會切斷相鄰區段，因此最後兩朵也不能和前面的剩餘花合併。
- 到第 12 天所有花都開放，連續七朵可以形成兩束，因此答案是 12。

### 複雜度

- 時間複雜度：O(n)，每朵花最多檢查一次。
- 額外空間複雜度：O(1)。

## 解法一：答案範圍二分搜尋

方法：`MinDays(int[] bloomDay, int m, int k)`

### 設計說明

先以 `long` 計算 `m * k`。若需求大於花朵總數，立刻回傳 `-1`；使用 `long` 是為了避免兩個 `int` 相乘時溢位，造成錯誤的可行性判斷。

若花朵數量足夠，答案必定位於：

- `low`：最小開花日。在此之前沒有任何花可用。
- `high`：最大開花日。到這一天所有花都已開放；由於總數足夠，必定能按原排列切成至少 `m` 組、每組 `k` 朵相鄰花。

每輪取中點 `days = low + (high - low) / 2`：

1. `CanMake(...) == true`：`days` 已可行，但可能還能更早完成，因此令 `high = days`。
2. `CanMake(...) == false`：`days` 以及更早日期都不可行，因此令 `low = days + 1`。
3. 當 `low == high` 時，該值就是第一個可行日。

中點使用 `low + (high - low) / 2`，避免直接計算 `(low + high) / 2` 可能發生的整數加法溢位。

### 正確性重點

`CanMake` 的結果會隨 `days` 呈現「一段 false，接著一段 true」。二分搜尋始終保留第一個 true 所在區間：可行時保留左半部，不可行時排除中點及左半部。區間縮小到一個值時，便得到最小可行日期。

### 複雜度

令 `D = max(bloomDay) - min(bloomDay) + 1`：

- 時間複雜度：O(n log D)。每輪二分搜尋呼叫一次 O(n) 的 `CanMake`。
- 額外空間複雜度：O(1)。
- 是否修改輸入：否。

## 解法二：排序候選開花日

方法：`MinDays2(int[] bloomDay, int m, int k)`

### 設計說明

第二種解法不搜尋整個日期範圍，而是只搜尋「花園狀態可能改變」的日期：

1. 同樣先使用 `long` 檢查 `m * k`，花朵總數不足時回傳 `-1`。
2. 複製 `bloomDay`，避免排序改動呼叫者的輸入。
3. 排序複本，取得由小到大的候選日期。
4. 跳過相同候選值，因為同一天重複出現多朵花，只會形成同一個整體狀態。
5. 依序呼叫 `CanMake`；第一個回傳 true 的候選值就是最少天數。
6. 若沒有任何候選日期可行，回傳 `-1`。

這不是逐日枚舉。即使開花日從 1 跳到 `10^9`，中間沒有花開放的日期都不會被檢查。

### 為何第一個可行候選就是答案

在兩個相鄰候選開花日之間，可用花朵集合完全不變，所以製作花束的結果也不會改變。若某天是最早可行日，當天必然至少有一朵新花開放，因此該天一定存在於排序後的候選集合中。由小到大找到的第一個可行候選自然就是答案。

### 複雜度

令 `u` 為不同開花日的數量，且 `u <= n`：

- 排序時間：O(n log n)。
- 可行性檢查：最多執行 `u` 次，每次 O(n)，合計 O(un)。
- 總時間複雜度：O(n log n + un)，最壞為 O(n²)。
- 額外空間複雜度：O(n)，用於排序的輸入副本。
- 是否修改輸入：否。

## 兩種解法比較

| 項目 | `MinDays`：答案二分搜尋 | `MinDays2`：排序候選日 |
| --- | --- | --- |
| 搜尋範圍 | 最小到最大開花日 | 不重複的實際開花日 |
| 核心依據 | 可行性的 false → true 單調性 | 答案只會在花朵開放時改變 |
| 時間複雜度 | O(n log D) | O(n log n + un)，最壞 O(n²) |
| 額外空間 | O(1) | O(n) |
| 是否修改輸入 | 否 | 否，排序輸入副本 |
| 教學價值 | 展示答案二分搜尋與下界查找 | 直接呈現「只測試有效候選」的推理 |
| 建議用途 | 正式提交與大型輸入 | 小型輸入、概念驗證與解法比較 |

`MinDays` 是本題較適合正式提交的解法；`MinDays2` 則用來說明候選答案集合與效能取捨。

## 範例演示流程

使用官方範例：

```text
bloomDay = [1, 10, 3, 10, 2]
m = 3
k = 1
```

每束只需要一朵花，因此第 3 天可使用開花日為 1、2、3 的三朵花，預期答案是 `3`。

### 解法一演示：答案範圍二分搜尋

初始搜尋範圍為 `[1, 10]`：

| `low` | `high` | `days` | 已開花數量 | 能否製作 3 束 | 更新 |
| ---: | ---: | ---: | ---: | --- | --- |
| 1 | 10 | 5 | 3 | 可以 | `high = 5` |
| 1 | 5 | 3 | 3 | 可以 | `high = 3` |
| 1 | 3 | 2 | 2 | 不可以 | `low = 3` |

最後 `low == high == 3`，回傳第 3 天。

### 解法二演示：排序候選開花日

排序後為 `[1, 2, 3, 10, 10]`，去除重複候選後依序檢查 `[1, 2, 3, 10]`：

| 候選日 | 可用花朵位置 | 可製作花束數 | 判斷 |
| ---: | --- | ---: | --- |
| 1 | 索引 0 | 1 | 不足 3 束，繼續 |
| 2 | 索引 0、4 | 2 | 不足 3 束，繼續 |
| 3 | 索引 0、2、4 | 3 | 第一個可行候選，回傳 3 |

候選日 10 不再需要測試，因為已找到最早可行日期。

### 測試案例涵蓋

- 官方可行案例：驗證最小日期搜尋。
- 花朵總量不足：驗證 `-1` 與 `long` 需求量檢查。
- 相鄰性案例：驗證未開花位置會切斷連續區段。
- 重複值：驗證候選日去重與同日完成多束花。
- `10^9` 上界：驗證大日期不適合逐日枚舉。
- 空陣列：驗證正式限制外的防禦結果。

## 建置與執行

請從 repository 根目錄執行：

```bash
dotnet restore leetcode_1482/leetcode_1482.csproj
dotnet build leetcode_1482/leetcode_1482.csproj --nologo
dotnet run --no-build --project leetcode_1482/leetcode_1482.csproj
```

目前沒有獨立的自動化測試專案；`Main` 會執行 6 組案例。每組驗證兩種解法的答案及輸入不變契約，共 24 項檢查。任何檢查失敗時，程式會設定非零結束碼。

### 實際執行結果

```text
Case 1: 官方範例一：每束只需要一朵花
Input: bloomDay = [1, 10, 3, 10, 2], m = 3, k = 1
Expected: 3
MinDays Actual: 3
MinDays Result: PASS
MinDays Input unchanged: PASS
MinDays2 Actual: 3
MinDays2 Result: PASS
MinDays2 Input unchanged: PASS

Case 2: 官方範例二：花朵總數不足
Input: bloomDay = [1, 10, 3, 10, 2], m = 3, k = 2
Expected: -1
MinDays Actual: -1
MinDays Result: PASS
MinDays Input unchanged: PASS
MinDays2 Actual: -1
MinDays2 Result: PASS
MinDays2 Input unchanged: PASS

Case 3: 官方範例三：花朵必須相鄰
Input: bloomDay = [7, 7, 7, 7, 12, 7, 7], m = 2, k = 3
Expected: 12
MinDays Actual: 12
MinDays Result: PASS
MinDays Input unchanged: PASS
MinDays2 Actual: 12
MinDays2 Result: PASS
MinDays2 Input unchanged: PASS

Case 4: 重複值：同一天可完成所有花束
Input: bloomDay = [1, 1, 1, 1], m = 2, k = 2
Expected: 1
MinDays Actual: 1
MinDays Result: PASS
MinDays Input unchanged: PASS
MinDays2 Actual: 1
MinDays2 Result: PASS
MinDays2 Input unchanged: PASS

Case 5: 邊界值：開花日為十億
Input: bloomDay = [1000000000, 1000000000], m = 1, k = 2
Expected: 1000000000
MinDays Actual: 1000000000
MinDays Result: PASS
MinDays Input unchanged: PASS
MinDays2 Actual: 1000000000
MinDays2 Result: PASS
MinDays2 Input unchanged: PASS

Case 6: 防禦性案例：空陣列
Input: bloomDay = [], m = 1, k = 1
Expected: -1
MinDays Actual: -1
MinDays Result: PASS
MinDays Input unchanged: PASS
MinDays2 Actual: -1
MinDays2 Result: PASS
MinDays2 Input unchanged: PASS

Summary: 24/24 checks passed.
```

## 專案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_1482.sln
└── leetcode_1482/
    ├── leetcode_1482.csproj
    └── Program.cs
```
