# leetcode_153 — Find Minimum in Rotated Sorted Array

> 解決 LeetCode 153 題「尋找旋轉排序陣列中的最小值」的 C# 範例專案

---

## 專案簡介

本專案提供 C# 解法，針對 LeetCode 153 題「Find Minimum in Rotated Sorted Array」進行高效解題，並附上詳細註解與測試案例。適合用於學習二分搜尋法、陣列處理與 C# 語法。

## 題目說明

給定一個經過旋轉的遞增排序整數陣列，請找出其中的最小值。要求時間複雜度為 O(log n)。

- [LeetCode 題目連結 (英文)](https://leetcode.com/problems/find-minimum-in-rotated-sorted-array/)
- [LeetCode 題目連結 (中文)](https://leetcode.cn/problems/find-minimum-in-rotated-sorted-array/)

## 解法概念

> [!tip]
> 本專案提供兩種主要解法，並說明暴力法為何不建議實作：

1. **暴力法**
   - 直接遍歷陣列找最小值，時間複雜度 O(n)。
   - 不符合題目 O(log n) 要求，僅作為理論參考。
2. **二分搜尋法（推薦）**
   - 透過比較中間值與右邊界值，逐步縮小搜尋範圍。
   - 具體步驟如下：
     1. 設定左右指標 `left`、`right`，分別指向陣列開頭與結尾。
     2. 當 `left < right` 時，重複以下步驟：
        - 計算中間索引 `mid = left + (right - left) / 2`，避免整數溢位。
        - 若 `nums[mid] > nums[right]`，代表最小值在右半部，將 `left` 移到 `mid + 1`。
        - 否則（`nums[mid] <= nums[right]`），代表最小值在左半部（包含中間值），將 `right` 移到 `mid`。
     3. 迴圈結束時，`left` 即為最小值索引。
   - 此法利用旋轉排序陣列的特性，能在 O(log n) 時間內找到最小值。
   - 空間複雜度 O(1)。
3. **C# API 方式**
   - 使用 `nums.Min()` 直接取得最小值，時間複雜度 O(n)。
   - 僅供一般需求參考，不適合練習二分搜尋法。

## 專案結構

```
leetcode_153.sln
leetcode_153/
  ├─ Program.cs         # 主程式與解法實作
  └─ leetcode_153.csproj
```

## 如何執行

1. 需安裝 [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
2. 於專案根目錄執行：

```pwsh
# 建構專案
$ dotnet build leetcode_153/leetcode_153.csproj

# 執行專案
$ dotnet run --project leetcode_153/leetcode_153.csproj
```

## 範例輸出

```
測試案例 1: [3, 4, 5, 1, 2]
[解法一] 二分法 最小值為: 1
[解法二] LINQ Min() 最小值為: 1

測試案例 2: [4, 5, 6, 7, 0, 1, 2]
[解法一] 二分法 最小值為: 0
[解法二] LINQ Min() 最小值為: 0

...
```

## 參考資料

- [C# 官方文件](https://learn.microsoft.com/dotnet/csharp/)
- [LeetCode Discuss](https://leetcode.com/problems/find-minimum-in-rotated-sorted-array/discuss/)

> [!note]
> 本專案僅供學習與演算法練習用途。
