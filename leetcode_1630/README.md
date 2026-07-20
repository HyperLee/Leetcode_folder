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

### 核心直覺：直接找出每個值排序後的位置

最直覺的做法是先排序子陣列，再比較相鄰差值。這個解法則不真的排序，而是先利用最小值、最大值和元素數量，推導出唯一可能的等差數列，再確認每個值是否剛好占據其中一個位置。

假設某筆查詢包含 `k` 個元素。若它能重新排列成等差數列，排序後一定具有以下形式：

```text
minimum,
minimum + difference,
minimum + 2 × difference,
...,
minimum + (k - 1) × difference
```

因此只要確認兩件事：

1. 最大值與最小值能否推導出整數公差。
2. `k` 個元素能否分別落在 `k` 個不重複的等差位置上。

### 為什麼公差只有一種可能？

長度為 `k` 的數列共有 `k - 1` 個相鄰間隔。排序後，第一項是 `minimum`，最後一項是 `maximum`，所以必須滿足：

```text
maximum = minimum + difference × (k - 1)
```

移項後得到：

```text
difference = (maximum - minimum) / (k - 1)
```

程式中的 `intervalCount` 就是 `k - 1`。查詢範圍包含左右端點，因此：

```text
k             = right - left + 1
intervalCount = k - 1
              = right - left
```

如果 `(maximum - minimum) % intervalCount != 0`，表示最大值與最小值之間無法平均切成 `k - 1` 個整數間隔。由於陣列元素都是整數，這筆查詢不可能形成等差數列，可以直接回傳 `false`。

### 每筆查詢的判斷流程

1. 掃描查詢範圍，找出 `minimum` 與 `maximum`。
2. 若兩者相等，代表所有元素相同，直接記錄 `true`。
3. 計算間隔數量 `intervalCount = right - left`。
4. 檢查 `maximum - minimum` 能否被 `intervalCount` 整除。
5. 算出唯一可能的公差 `difference`，並建立長度為 `k` 的 `seen`。
6. 將每個元素換算成等差數列中的位置；若位置不合法或重複，記錄 `false`。
7. 所有元素都通過檢查時，記錄 `true`。

### `valueOffset`、`position` 與 `seen`

對目前元素 `x`，程式先計算它和最小值之間的距離：

```text
valueOffset = x - minimum
```

合法的等差元素必須符合：

```text
x = minimum + position × difference
```

所以也可以寫成：

```text
position = (x - minimum) / difference
         = valueOffset / difference
```

這裡需要依序檢查：

- `valueOffset % difference == 0`：確認 `x` 確實落在某個等差位置，而不是兩個位置之間。
- `seen[position] == false`：確認該位置尚未被其他元素占用。

通過後將 `seen[position]` 設為 `true`。`seen[0]` 代表最小值的位置，`seen[1]` 代表 `minimum + difference`，依此類推。

### 完整追蹤：成功案例 `[4, 6, 5]`

這筆查詢有 `k = 3` 個元素，因此共有 `2` 個間隔：

```text
minimum      = 4
maximum      = 6
intervalCount = 2
difference   = (6 - 4) / 2 = 1
```

唯一可能的等差數列是 `[4, 5, 6]`，所以建立：

```text
seen = [false, false, false]
```

| 目前值 `x` | `valueOffset = x - 4` | `position = valueOffset / 1` | 標記後的 `seen` |
| ---: | ---: | ---: | --- |
| 4 | 0 | 0 | `[true, false, false]` |
| 6 | 2 | 2 | `[true, false, true]` |
| 5 | 1 | 1 | `[true, true, true]` |

三個元素都落在合法且不重複的位置，因此原本即使是 `[4, 6, 5]`，仍可重新排列成 `[4, 5, 6]`。

### 失敗情況一：最大值與最小值的差無法整除

以 `[4, 6, 5, 9]` 為例：

```text
k              = 4
intervalCount  = 3
minimum        = 4
maximum        = 9
maximum - minimum = 5
```

因為 `5 % 3 != 0`，無法用整數公差把 `4` 到 `9` 平均切成三段，所以不必繼續檢查元素即可判定為 `false`。

### 失敗情況二：不同元素占用相同位置

以 `[3, 5, 5, 9]` 為例。根據最小值、最大值與元素數量，唯一可能的等差數列是 `[3, 5, 7, 9]`，公差為 `2`。

兩個 `5` 都會得到：

```text
position = (5 - 3) / 2 = 1
```

第一個 `5` 將 `seen[1]` 設為 `true`；第二個 `5` 再次抵達位置 `1` 時就會發現重複。這也代表位置 `2` 應有的數字 `7` 缺少，因此不能形成完整的等差數列。

### 常見疑問

#### 為什麼要先處理公差為零？

若 `minimum == maximum`，範圍內所有元素都相同，例如 `[7, 7, 7, 7]`。它們的相鄰差都是 `0`，本身就是等差數列，所以可以直接記錄 `true`。

這個分支也避免後面執行 `valueOffset % difference` 時，因 `difference == 0` 而發生除以零例外。

#### 為什麼 `position` 不會超出 `seen`？

每個 `x` 都來自目前查詢範圍，因此必定滿足：

```text
minimum <= x <= maximum
```

在整除條件成立時，`maximum - minimum = difference × intervalCount`，所以合法位置一定介於 `0` 和 `intervalCount`。而 `seen` 的長度是 `intervalCount + 1`，合法索引正好也是 `0..intervalCount`。

#### 為什麼最後不用再檢查每個 `seen` 都是 `true`？

子陣列共有 `k` 個元素，`seen` 也剛好有 `k` 個合法位置。程式已確認每個元素都落在合法位置，而且沒有兩個元素使用同一位置。`k` 個元素放進 `k` 個互不重複的位置時，所有位置必然剛好各被占用一次，因此不需要再次掃描 `seen`。

### 複雜度

對長度為 `k` 的單筆查詢：

- 第一次掃描找最小值與最大值，需要 O(k) 時間。
- 第二次掃描驗證每個元素的位置，需要 O(k) 時間。
- `seen` 陣列包含 `k` 個布林值，需要 O(k) 額外空間。

因此每筆查詢的時間複雜度為 O(k)，額外空間複雜度為 O(k)。

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
