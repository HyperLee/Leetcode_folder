# leetcode_2966

本專案為 LeetCode 題目「2966. 划分陣列並滿足最大差限制」的 C# 解題程式。

## 題目簡述

- 給定一個整數陣列 `nums` (長度為 n，且 n 為 3 的倍數) 與正整數 `k`。
- 請將 `nums` 分成 n/3 個長度為 3 的子陣列，且每個子陣列內任兩元素的差值皆小於等於 `k`。
- 若無法分割則回傳空陣列，若有多種答案可回傳任一種。

## 解題概念與出發點

本題的核心在於：
- 由於每組需 3 個元素且最大差值需 ≤ k，若將陣列排序，則連續 3 個元素的差值最小，這是分組的最佳策略。
- 只需檢查排序後每 3 個元素的最大與最小差是否超過 k，若有一組不符即無法分組。
- 這樣能保證所有組的最大差值都不超過 k，且能有效率地完成分組。

此方法的出發點：
- 排序後，將元素依序分組，能讓每組內的最大差值最小化。
- 不排序則無法保證正確性，也無法用更低複雜度完成。

## 專案結構

- `leetcode_2966.sln`：Visual Studio 解決方案檔
- `leetcode_2966/leetcode_2966.csproj`：C# 專案檔
- `leetcode_2966/Program.cs`：主程式與解題邏輯
- `bin/`、`obj/`：建構產生的暫存與輸出資料夾

## 主要程式碼說明

### `Program.cs`

- `Main` 方法：
  - 提供測試資料，呼叫解法並輸出分組結果。
- `DivideArray(int[] nums, int k)` 函式：
  - 先將 `nums` 排序，確保每 3 個連續元素的差值最小。
  - 每次取 3 個元素，檢查最大與最小的差是否小於等於 `k`。
  - 若有任一組不符合條件，直接回傳空陣列。
  - 全部分組都符合則回傳分組結果。
  - 時間複雜度：O (n log n)，主要來自排序。
  - 空間複雜度：O (n)，用於儲存分組結果。

## 如何執行

1. 需安裝 .NET 8.0 SDK 以上版本。
2. 在專案根目錄下執行：

```sh
# 建構專案
$ dotnet build leetcode_2966/leetcode_2966.csproj

# 執行程式
$ dotnet run --project leetcode_2966/leetcode_2966.csproj
```

## 相關連結

- [LeetCode 題目說明 (英文)](https://leetcode.com/problems/divide-array-into-arrays-with-max-difference/)
- [LeetCode 題目說明 (中文)](https://leetcode.cn/problems/divide-array-into-arrays-with-max-difference/)

---

本專案僅供學習與參考，歡迎討論與指正。
