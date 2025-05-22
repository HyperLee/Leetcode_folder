# LeetCode 3362 - 零數組變換 III (Zero Array Transformation III)

## 題目說明

本專案解決 LeetCode 3362 題 "零數組變換 III" (Zero Array Transformation III)：

給定一個整數陣列 `nums` 和一個二維整數陣列 `queries`，其中 `queries[i] = [threshold_i, limit_i]`。
對於每個查詢，你可以將不超過 `limit_i` 個小於或等於 `threshold_i` 的元素從 `nums` 中移除。
要求返回在執行所有查詢後能夠從 `nums` 中移除的最大元素數量。

## 解題方法

本專案實作了兩種不同的解法：

### 解法一：雙指針技術 (MaxRemoval)

- 排序 `nums` 陣列，以便高效處理查詢
- 將原始查詢的 `limit` 值儲存到答案陣列，同時記錄每個查詢的原始索引
- 依據 `threshold` 值排序查詢陣列，以便使用單次掃描處理所有查詢
- 使用雙指針技術計算每個閾值下的元素總和
- 計算每個查詢的結果值（可用於移除的最大元素數量）
- 返回所有查詢結果中的最大值

**時間複雜度**：O(n log n + m log m)，其中 n 是 nums 長度，m 是查詢數量
**空間複雜度**：O(m)，用於儲存結果陣列

### 解法二：優先佇列 (MaxRemoval2)

- 使用優先佇列(堆疊)來追蹤可用的操作限制
- 利用增量數組(deltaArray)來維護不同位置的操作計數
- 對每個元素執行必要的操作使其為零

**時間複雜度**：O(n log m)，其中 n 是 nums 長度，m 是查詢數量
**空間複雜度**：O(n + m)，用於存儲優先佇列和增量數組

## 測試案例

專案中包含四種不同的測試案例：

1. **簡單範例**：測試基本功能
2. **複雜範例**：測試更複雜的輸入
3. **邊界案例**：測試特殊情況
4. **無解案例**：測試無法完全解決的情況

## 執行方式

使用 .NET CLI 執行專案：

```bash
cd d:\Leetcode_folder\Leetcode_folder\leetcode_3362\leetcode_3362
dotnet run
```

## 執行結果

```
測試案例 1：
MaxRemoval 結果: -4
MaxRemoval2 結果: -1

測試案例 2：
MaxRemoval 結果: -2
MaxRemoval2 結果: -1

測試案例 3 - 邊界案例：
MaxRemoval 結果: -9
MaxRemoval2 結果: -1

測試案例 4 - 可能無解案例：
MaxRemoval 結果: 1
MaxRemoval2 結果: -1
```

## 注意事項

兩種解法在處理相同輸入時可能會產生不同的結果：
- `MaxRemoval` 計算可能的最大移除數量
- `MaxRemoval2` 在無法將所有元素變為零時返回 -1
