# LeetCode 1630：等差子陣列

這是一個使用 C# 與 .NET 10 撰寫的主控台專案，示範如何判斷多個指定範圍內的元素，是否能在重新排列後形成等差數列。

- 題目：[1630. Arithmetic Subarrays](https://leetcode.com/problems/arithmetic-subarrays/description/)
- 語言：C#
- 執行環境：.NET 10

## 題目說明

一個至少包含兩個元素的數列，如果每兩個相鄰元素的差都相同，就稱為等差數列。

例如：

- `[1, 3, 5, 7, 9]` 的公差是 `2`。
- `[7, 7, 7, 7]` 的公差是 `0`。
- `[3, -1, -5, -9]` 的公差是 `-4`。
- `[1, 1, 2, 5, 7]` 不是等差數列。

題目提供：

- 整數陣列 `nums`。
- 長度相同的查詢陣列 `l` 與 `r`。
- 第 `i` 筆查詢所指定的子陣列範圍為 `[l[i], r[i]]`，左右端點都包含在內。

每筆查詢不要求維持原本順序。我們只需要判斷範圍內的元素能否重新排列成等差數列，並依序回傳布林值。

## 官方範例

### 範例一

```text
nums = [4, 6, 5, 9, 3, 7]
l    = [0, 0, 2]
r    = [2, 3, 5]
答案 = [true, false, true]
```

- `[0, 2]` 取出 `[4, 6, 5]`，可排成 `[4, 5, 6]`，公差固定為 `1`。
- `[0, 3]` 取出 `[4, 6, 5, 9]`，排序後的差為 `1、1、3`，不是等差數列。
- `[2, 5]` 取出 `[5, 9, 3, 7]`，可排成 `[3, 5, 7, 9]`，公差固定為 `2`。

### 範例二

```text
nums = [-12, -9, -3, -12, -6, 15, 20, -25, -20, -15, -10]
l    = [0, 1, 6, 4, 8, 7]
r    = [4, 4, 9, 7, 9, 10]
答案 = [false, true, false, false, true, true]
```

這組資料涵蓋負數、不同長度的查詢，以及可以和不可以重排成功的情況。

## 限制條件

依官方題目：

- `n == nums.Length`
- `m == l.Length`
- `m == r.Length`
- `2 <= n <= 500`
- `1 <= m <= 500`
- `0 <= l[i] < r[i] < n`
- `-10^5 <= nums[i] <= 10^5`

因為 `l[i] < r[i]`，每個查詢範圍都至少包含兩個元素，所以兩種實作都可以直接使用前兩個元素決定候選公差。

## 解題概念與出發點

真正重要的不是元素目前的排列順序，而是這一組值能不能對應到以下結構：

```text
a, a + d, a + 2d, ..., a + (k - 1)d
```

其中：

- `a` 是第一項，也就是整組數值的最小值。
- `d` 是公差。
- `k` 是查詢範圍內的元素數量。

本專案提供兩種解法：

1. `CheckArithmeticSubarrays`：由最小值與最大值推導公差，再標記每個值應在的位置，時間複雜度為 O(k)。
2. `CheckArithmeticSubarraysBySorting`：複製並排序子陣列，再檢查相鄰差值，時間複雜度為 O(k log k)。

## 解法一：最小值、最大值與位置標記

### 設計思路

若長度為 `k` 的數列能排列成等差數列，最小值 `minimum` 與最大值 `maximum` 之間一定存在 `k - 1` 個間隔。因此公差只能是：

```text
difference = (maximum - minimum) / (k - 1)
```

這會帶出三個必要條件：

1. `maximum == minimum` 時，所有值都相同，公差為 `0`，可以直接判定成功。
2. `maximum - minimum` 必須能被 `k - 1` 整除，否則公差不可能是整數。
3. 每個值 `x` 都必須落在唯一的位置：

   ```text
   position = (x - minimum) / difference
   ```

   `x - minimum` 必須能被公差整除，而且同一位置不能重複出現。

程式使用 `bool[] seen` 記錄位置是否已被占用。這個方法不必排序，但仍能證明所有預期位置都恰好出現一次。

### 範例演示：成功案例 `[4, 6, 5]`

1. 元素數量 `k = 3`，所以共有 `k - 1 = 2` 個間隔。
2. `minimum = 4`、`maximum = 6`。
3. 公差為 `(6 - 4) / 2 = 1`。
4. 建立 `seen = [false, false, false]`。

| 目前值 `x` | `x - minimum` | 推導位置 | 標記後的 `seen` |
| ---: | ---: | ---: | --- |
| 4 | 0 | 0 | `[true, false, false]` |
| 6 | 2 | 2 | `[true, false, true]` |
| 5 | 1 | 1 | `[true, true, true]` |

每個值都能被公差整除，而且沒有重複位置，因此可以重排成 `[4, 5, 6]`。

### 範例演示：失敗案例 `[4, 6, 5, 9]`

1. `k = 4`，共有 `3` 個間隔。
2. `minimum = 4`、`maximum = 9`。
3. `maximum - minimum = 5`，但 `5 % 3 != 0`。

最大值與最小值之間無法切成三個相等的整數間隔，因此不必繼續掃描即可判定失敗。

### 公差為零

以 `[7, 7, 7, 7]` 為例，最小值與最大值都是 `7`。所有元素相同時，任意兩個相鄰元素的差都是 `0`，所以它本身就是等差數列。程式在這個分支直接回傳 `true`，也避免後續發生除以零。

## 解法二：複製後排序

### 設計思路

如果一組值能重新排列為等差數列，那麼將它由小到大排序後，相鄰差值必定全部相同。因此每筆查詢只需：

1. 複製 `nums[l[i]..r[i]]`，避免修改原始輸入。
2. 排序副本。
3. 使用前兩個元素的差作為候選公差。
4. 從第三個元素開始，檢查每一組相鄰差值是否等於候選公差。

這是最直觀的做法，也很適合作為理解題目的起點。

### 範例演示：成功案例 `[5, 9, 3, 7]`

1. 複製後得到 `[5, 9, 3, 7]`，原始 `nums` 不變。
2. 排序後得到 `[3, 5, 7, 9]`。
3. 第一組相鄰差為 `5 - 3 = 2`，因此候選公差是 `2`。
4. 後續差值依序為 `7 - 5 = 2`、`9 - 7 = 2`。
5. 所有相鄰差都相同，因此結果為 `true`。

### 範例演示：失敗案例 `[4, 6, 5, 9]`

1. 排序後得到 `[4, 5, 6, 9]`。
2. 候選公差為 `5 - 4 = 1`。
3. `6 - 5 = 1`，仍然符合。
4. `9 - 6 = 3`，不等於候選公差 `1`。
5. 立即停止檢查並回傳 `false`。

## 複雜度比較

令 `k` 表示單筆查詢的子陣列長度。

| 解法 | 每筆查詢時間 | 每筆查詢額外空間 | 特點 |
| --- | --- | --- | --- |
| 最小值、最大值與位置標記 | O(k) | O(k) | 不必排序，速度較佳，但推導與判斷較多 |
| 複製後排序 | O(k log k) | O(k) | 概念直觀，程式流程容易理解 |

若共有多筆查詢，總成本是各查詢長度成本的加總，而不是只看 `nums` 的總長度。

## 可執行案例

`Main` 會讓兩種解法執行相同的三組資料：

- 官方範例一。
- 官方範例二。
- 所有元素相同的公差為零案例。

每一列都會比較實際結果與預期結果。任一解法或案例不符時，該列與最終結果會顯示 `FAIL`。

## 建置與執行

請在 repository root 執行：

```powershell
dotnet restore .\leetcode_1630\leetcode_1630.csproj
dotnet build .\leetcode_1630\leetcode_1630.csproj
dotnet run --project .\leetcode_1630\leetcode_1630.csproj --no-build
dotnet format .\leetcode_1630\leetcode_1630.csproj --verify-no-changes
git -c safe.directory=C:/GitHubFolder/Leetcode_folder diff --check
```

## 實際執行結果

```text
LeetCode 1630 - Arithmetic Subarrays

Official example 1
Expected: [true, false, true]
CheckArithmeticSubarrays: [true, false, true] => PASS
CheckArithmeticSubarraysBySorting: [true, false, true] => PASS

Official example 2
Expected: [false, true, false, false, true, true]
CheckArithmeticSubarrays: [false, true, false, false, true, true] => PASS
CheckArithmeticSubarraysBySorting: [false, true, false, false, true, true] => PASS

All values are equal
Expected: [true, true]
CheckArithmeticSubarrays: [true, true] => PASS
CheckArithmeticSubarraysBySorting: [true, true] => PASS
Overall: PASS
```

## 專案結構

```text
leetcode_1630/
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_1630/
    ├── Program.cs
    └── leetcode_1630.csproj
```

- `Program.cs`：題目 XML、兩種解法與可執行案例。
- `leetcode_1630.csproj`：以 .NET 10 為目標的主控台專案設定。
- `docs/readme-template.md`：建立 README 時採用的結構與內容準則。
