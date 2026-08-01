# LeetCode 1248 — 統計「優美子陣列」

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![C%23](https://img.shields.io/badge/C%23-console-239120)

這個專案使用 C# 與 .NET 10 實作 [LeetCode 1248：Count Number of Nice Subarrays](https://leetcode.com/problems/count-number-of-nice-subarrays/)，並以可直接執行的案例比較兩種線性時間解法。

## 目錄

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：滑動視窗與邊界組合](#解法一滑動視窗與邊界組合)
- [解法二：奇數索引與哨兵](#解法二奇數索引與哨兵)
- [解法比較](#解法比較)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)

## 題目說明

給定正整數陣列 `nums` 與整數 `k`。如果某個非空連續子陣列恰好包含 `k` 個奇數，該子陣列就稱為「優美子陣列」。請回傳陣列中所有優美子陣列的數量。

例如：

```text
輸入：nums = [1, 1, 2, 1, 1], k = 3
輸出：2
```

符合條件的子陣列為 `[1, 1, 2, 1]` 與 `[1, 2, 1, 1]`。

## 限制條件

- `1 <= nums.length <= 50000`
- `1 <= nums[i] <= 100000`
- `1 <= k <= nums.length`
- 子陣列必須連續且非空。

## 解題概念與出發點

直接列舉所有起點與終點需要檢查 O(n²) 個子陣列。更有效率的觀察是：當一段區間已經固定包含連續的 `k` 個奇數後，答案只取決於這組奇數左右兩側有多少個偶數。

假設：

- 第一個奇數左側緊鄰 `leftEvenCount` 個偶數。
- 第 `k` 個奇數右側緊鄰 `rightEvenCount` 個偶數。

起點可以選第一個奇數本身，或其左側任一偶數，因此有 `leftEvenCount + 1` 種選擇；終點同理有 `rightEvenCount + 1` 種選擇。本組奇數能形成的答案為：

```text
(leftEvenCount + 1) × (rightEvenCount + 1)
```

兩種實作都使用這個組合概念，差別只在於如何定位每組連續的 `k` 個奇數。

## 解法一：滑動視窗與邊界組合

`NumberOfSubarrays` 使用左右指標維護滑動視窗。

### 設計流程

1. `right` 向右移動，遇到奇數便增加 `oddCount`。
2. 當視窗恰好包含 `k` 個奇數時，繼續向右掃過第 `k` 個奇數後方的連續偶數，得到 `rightEvenCount`。
3. 從 `left` 掃過第一個奇數前方的連續偶數，得到 `leftEvenCount`。
4. 把 `(leftEvenCount + 1) × (rightEvenCount + 1)` 加入答案。
5. 將 `left` 越過第一個奇數並把 `oddCount` 減一，接著尋找下一組 `k` 個奇數。

每個指標都只向右移動，不會重複回頭掃描，也不會修改輸入陣列。

### 範例演示

```text
nums = [2, 2, 2, 1, 2, 2, 1, 2, 2, 2], k = 2
索引 =   0  1  2  3  4  5  6  7  8  9
```

- 視窗找到索引 3 與 6 的兩個奇數。
- 第一個奇數左側有 3 個連續偶數，所以合法起點共有 `3 + 1 = 4` 種：索引 0、1、2、3。
- 第二個奇數右側有 3 個連續偶數，所以合法終點共有 `3 + 1 = 4` 種：索引 6、7、8、9。
- 此視窗貢獻 `4 × 4 = 16` 個優美子陣列。

### 複雜度

- 時間複雜度：O(n)，左右指標各自只向前走。
- 額外空間複雜度：O(1)。

## 解法二：奇數索引與哨兵

`NumberOfSubarrays2` 先記錄所有奇數的位置，再直接計算每組連續 `k` 個奇數的左右邊界選擇數。

### 設計流程

1. 將每個奇數的索引依序存入 `oddIndices`。
2. 在最前方加入 `-1`，代表陣列左邊界外的位置。
3. 在最後方加入 `nums.Length`，代表陣列右邊界外的位置。
4. 對每一組連續 `k` 個奇數，令第一個奇數索引位置為 `i`：

```text
leftChoices  = oddIndices[i]     - oddIndices[i - 1]
rightChoices = oddIndices[i + k] - oddIndices[i + k - 1]
本組答案     = leftChoices × rightChoices
```

哨兵讓第一組與最後一組奇數可以使用相同公式，不需要另外撰寫首尾特例。迴圈只列舉實際存在的奇數群組；若奇數總數少於 `k`，便不會進入計算，直接回傳 0。

### 範例演示

沿用相同輸入：

```text
nums = [2, 2, 2, 1, 2, 2, 1, 2, 2, 2], k = 2
奇數索引加上哨兵 = [-1, 3, 6, 10]
```

唯一一組兩個奇數位於索引 3 與 6：

```text
leftChoices  = 3 - (-1) = 4
rightChoices = 10 - 6   = 4
答案         = 4 × 4    = 16
```

### 複雜度

- 時間複雜度：O(n)，先掃描陣列，再線性列舉奇數群組。
- 額外空間複雜度：O(n)，用於保存奇數索引與兩個哨兵。

## 解法比較

| 項目 | 解法一：滑動視窗 | 解法二：奇數索引 |
| --- | --- | --- |
| 核心做法 | 即時維護包含 `k` 個奇數的視窗 | 先收集奇數位置，再套用索引差公式 |
| 時間複雜度 | O(n) | O(n) |
| 額外空間 | O(1) | O(n) |
| 優點 | 空間固定，只需一次方向性的掃描 | 公式直接，首尾用哨兵統一處理 |
| 適合學習 | 滑動視窗狀態與邊界移動 | 索引壓縮、哨兵與組合計數 |
| 是否修改輸入 | 否 | 否 |

## 建置與執行

需求：已安裝支援 `net10.0` 的 .NET SDK。

請從此 repository 根目錄執行：

```bash
dotnet restore leetcode_1248/leetcode_1248.csproj
dotnet build leetcode_1248/leetcode_1248.csproj --nologo
dotnet run --no-build --project leetcode_1248/leetcode_1248.csproj
```

目前沒有獨立的自動化測試專案。`Main` 會執行 7 組固定案例，兩種解法合計接受 14 項 Expected/Actual 檢查；只要任一檢查失敗，程式就會設定非零結束碼。

## 實際執行結果

以下內容來自實際執行 `dotnet run --no-build --project leetcode_1248/leetcode_1248.csproj`：

```text
Case 1: nums = [1, 1, 2, 1, 1], k = 3
Expected: 2
NumberOfSubarrays Actual: 2 => PASS
NumberOfSubarrays2 Actual: 2 => PASS

Case 2: nums = [2, 4, 6], k = 1
Expected: 0
NumberOfSubarrays Actual: 0 => PASS
NumberOfSubarrays2 Actual: 0 => PASS

Case 3: nums = [2, 2, 2, 1, 2, 2, 1, 2, 2, 2], k = 2
Expected: 16
NumberOfSubarrays Actual: 16 => PASS
NumberOfSubarrays2 Actual: 16 => PASS

Case 4: nums = [1], k = 1
Expected: 1
NumberOfSubarrays Actual: 1 => PASS
NumberOfSubarrays2 Actual: 1 => PASS

Case 5: nums = [2, 1, 2], k = 2
Expected: 0
NumberOfSubarrays Actual: 0 => PASS
NumberOfSubarrays2 Actual: 0 => PASS

Case 6: nums = [2, 2, 1, 2, 2], k = 1
Expected: 9
NumberOfSubarrays Actual: 9 => PASS
NumberOfSubarrays2 Actual: 9 => PASS

Case 7: nums = [1, 3, 5, 7], k = 2
Expected: 3
NumberOfSubarrays Actual: 3 => PASS
NumberOfSubarrays2 Actual: 3 => PASS

Summary: 14/14 checks passed.
```

## 專案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_1248.sln
└── leetcode_1248/
    ├── leetcode_1248.csproj
    └── Program.cs
```