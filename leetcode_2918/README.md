# Leetcode 2918 - Minimum Equal Sum of Two Arrays After Replacing Zeros

## 題目說明

給定兩個整數陣列 `nums1` 和 `nums2`，你可以將這兩個陣列中的所有 0 替換為任意正整數，使得兩個陣列的和相等。請找出可能的最小相等和。如果無法達成，則回傳 -1。

- [Leetcode 題目連結 (英文)](https://leetcode.com/problems/minimum-equal-sum-of-two-arrays-after-replacing-zeros/description/)
- [Leetcode 題目連結 (中文)](https://leetcode.cn/problems/minimum-equal-sum-of-two-arrays-after-replacing-zeros/description/)

---

## 解題概念

1. 0 只能替換為正整數，所以每個 0 至少替換為 1。
2. 計算兩個陣列的初始和 (將 0 替換為 1 後)。
3. 若某個陣列無 0 且和較小，則無法達成相等和。
4. 若有解，最小相等和即為兩陣列替換後和值的較大者。

---

## 主要程式碼說明

本專案提供兩種解法：

### 1. MinSum 函式

- 直接 for 迴圈遍歷兩個陣列，計算總和與 0 的數量 (0 直接加 1)。
- 判斷是否有解，最後回傳較大總和。
- 適合一般情境，程式碼簡潔。

### 2. MinimumSum 函式

- for 迴圈分別計算非零總和與 0 的數量。
- 針對三種情境 (都沒 0、都有 0、只有一邊有 0) 做更細緻判斷。
- 對於極端情況 (如大數、溢位) 處理更安全。
- 適合需要嚴謹處理所有極端狀況。

---

## 範例

```csharp
int[] nums1 = { 3, 2, 0, 1, 0 };
int[] nums2 = { 6, 5, 0 };
long minSumResult = program.MinSum(nums1, nums2);         // 輸出 13
long minimumSumResult = program.MinimumSum(nums1, nums2); // 輸出 13

int[] nums3 = { 2, 3, 4 };
int[] nums4 = { 10, 0 };
long minSumResult2 = program.MinSum(nums3, nums4);         // 輸出 -1
long minimumSumResult2 = program.MinimumSum(nums3, nums4); // 輸出 -1
```

---

## 效能說明

- 兩種解法時間複雜度皆為 O (n + m)，空間複雜度皆為 O (1)。
- MinimumSum 對邊界情況處理更嚴謹，適合需要高穩定性的場景。

---

## 執行方式

1. 使用 .NET 8.0 或以上版本。
2. 編譯並執行 `Program.cs`，即可看到測試結果。

---

## 檔案結構

- `Program.cs`：主程式與兩種解法實作
- 其他為 .NET 建構相關檔案

---
