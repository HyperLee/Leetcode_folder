# LeetCode 88 - Merge Sorted Array

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![Language](https://img.shields.io/badge/C%23-Console-239120)

這個專案是 LeetCode 88「Merge Sorted Array」的 C# console 解題紀錄。程式在 `Main` 中提供可直接執行的範例資料，並展示三種合併策略：逆向雙指針、正向雙指針與直接合併後排序。

## 題目說明

給定兩個以非遞減順序排序的整數陣列 `nums1` 和 `nums2`，以及兩個整數 `m` 和 `n`，分別代表 `nums1` 和 `nums2` 中有效元素的數量。

目標是將 `nums2` 合併到 `nums1`，使 `nums1` 成為單一非遞減排序陣列。函式不需要回傳新陣列，合併結果必須直接寫回 `nums1`。

`nums1` 的長度為 `m + n`，前 `m` 個元素是有效資料，最後 `n` 個位置是預留空間。`nums2` 的長度為 `n`。

參考連結：

- [LeetCode - Merge Sorted Array](https://leetcode.com/problems/merge-sorted-array/description/)
- [LeetCode Wiki - 88. Merge Sorted Array](https://leetcode.doocs.org/en/lc/88/)

## 限制條件

- `nums1.length == m + n`
- `nums2.length == n`
- `0 <= m, n <= 200`
- `1 <= m + n <= 200`
- `-10^9 <= nums1[i], nums2[j] <= 10^9`
- `nums1` 前 `m` 個元素與 `nums2` 前 `n` 個元素皆已依非遞減順序排序。

## 解題概念與出發點

這題的核心不是「如何排序」，而是「如何在 `nums1` 既有空間內完成合併」。因為 `nums1` 後方已經保留 `n` 個空位，最有效率的做法是從尾端開始填入目前最大的元素，避免覆蓋 `nums1` 前段尚未比較的有效資料。

本專案保留三種解法，方便比較不同思路：

1. `Merge`：逆向雙指針，從尾端往前填，時間複雜度 `O(m + n)`，額外空間 `O(1)`。
2. `Merge2`：正向雙指針，先寫入暫存陣列再複製回 `nums1`，時間複雜度 `O(m + n)`，額外空間 `O(m + n)`。
3. `Merge3`：直接把 `nums2` 放到 `nums1` 尾端後排序，時間複雜度取決於排序，額外空間取決於 `Array.Sort` 實作。

## 解法一：逆向雙指針

`Merge` 使用三個指標：

- `p1 = m - 1`：指向 `nums1` 有效區間的最後一個元素。
- `p2 = n - 1`：指向 `nums2` 的最後一個元素。
- `tail = m + n - 1`：指向 `nums1` 目前要填入的位置。

每次比較 `nums1[p1]` 與 `nums2[p2]`，把較大的值放到 `nums1[tail]`，然後對應指標往前移。因為結果是從最大值開始放到最後面，所以不會覆蓋 `nums1` 中還沒比較的有效元素。

這是本題最推薦的解法，原因是它直接利用題目提供的預留空間，不需要額外陣列，也符合 follow-up 要求的 `O(m + n)` 時間。

### 範例演示

輸入：

```text
nums1 = [1, 2, 3, 0, 0, 0], m = 3
nums2 = [2, 5, 6], n = 3
```

流程：

```text
tail = 5，比較 3 和 6，放入 6 -> [1, 2, 3, 0, 0, 6]
tail = 4，比較 3 和 5，放入 5 -> [1, 2, 3, 0, 5, 6]
tail = 3，比較 3 和 2，放入 3 -> [1, 2, 3, 3, 5, 6]
tail = 2，比較 2 和 2，放入 nums2 的 2 -> [1, 2, 2, 3, 5, 6]
tail = 1，nums2 已處理完，保留 nums1 剩餘元素
```

結果：

```text
[1, 2, 2, 3, 5, 6]
```

## 解法二：正向雙指針

`Merge2` 從兩個陣列的有效區間開頭開始比較：

- `p1` 指向 `nums1` 的目前候選元素。
- `p2` 指向 `nums2` 的目前候選元素。
- `sorted` 暫存合併後的結果。

每次取較小值放入 `sorted`，直到兩邊元素都被取完，最後再把 `sorted` 複製回 `nums1`。

這個做法直覺且容易理解，但因為從前方寫入會覆蓋 `nums1` 尚未處理的有效資料，所以需要額外暫存陣列。它適合用來理解 merge sort 中的合併概念，但不是本題空間最佳解。

### 範例演示

輸入：

```text
nums1 = [1, 2, 3, 0, 0, 0], m = 3
nums2 = [2, 5, 6], n = 3
```

流程：

```text
比較 1 和 2，取 1 -> sorted = [1]
比較 2 和 2，取 nums1 的 2 -> sorted = [1, 2]
比較 3 和 2，取 nums2 的 2 -> sorted = [1, 2, 2]
比較 3 和 5，取 3 -> sorted = [1, 2, 2, 3]
nums1 已取完，依序取 5、6 -> sorted = [1, 2, 2, 3, 5, 6]
複製 sorted 回 nums1
```

結果：

```text
[1, 2, 2, 3, 5, 6]
```

## 解法三：直接合併後排序

`Merge3` 先把 `nums2` 的所有元素放入 `nums1` 後方預留位置，接著呼叫 `Array.Sort(nums1)`。

這是最簡短的做法，也最容易確認結果正確，但它沒有利用兩個輸入陣列原本已排序的特性。若面試或練習目標是展示演算法思路，應優先使用逆向雙指針。

### 範例演示

輸入：

```text
nums1 = [1, 2, 3, 0, 0, 0], m = 3
nums2 = [2, 5, 6], n = 3
```

流程：

```text
把 nums2 放入 nums1 預留位置 -> [1, 2, 3, 2, 5, 6]
排序整個 nums1 -> [1, 2, 2, 3, 5, 6]
```

結果：

```text
[1, 2, 2, 3, 5, 6]
```

## 執行方式

建置專案：

```powershell
dotnet build leetcode_088/leetcode_088.csproj
```

執行範例：

```powershell
dotnet run --project leetcode_088/leetcode_088.csproj
```

目前範例輸出：

```text
Merge - Example 1: PASS -> [1, 2, 2, 3, 5, 6]
Merge - Example 2: PASS -> [1]
Merge - Example 3: PASS -> [1]
Merge - Left tail move: PASS -> [1, 2]
Merge2 - Example 1: PASS -> [1, 2, 2, 3, 5, 6]
Merge2 - Example 2: PASS -> [1]
Merge2 - Example 3: PASS -> [1]
Merge2 - Left tail move: PASS -> [1, 2]
Merge3 - Example 1: PASS -> [1, 2, 2, 3, 5, 6]
Merge3 - Example 2: PASS -> [1]
Merge3 - Example 3: PASS -> [1]
Merge3 - Left tail move: PASS -> [1, 2]
All examples passed.
```

檢查 Git diff 是否有多餘空白：

```powershell
git diff --check
```

> [!NOTE]
> 目前專案沒有獨立測試專案，因此驗證以 `dotnet build` 與 `dotnet run` 中的 console 範例為主。

## 專案結構

```text
.
├── docs/
│   └── readme-template.md
├── leetcode_088/
│   ├── Program.cs
│   └── leetcode_088.csproj
└── README.md
```
