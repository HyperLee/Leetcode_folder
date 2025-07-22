# leetcode_1695 - Maximum Erasure Value

## 專案簡介

本專案為 Leetcode 第 1695 題「Maximum Erasure Value」的 C# 解法，包含兩種不同的滑動窗口實作方式，並附有詳細註解與測試範例。

## 題目說明

給定一個正整數陣列 `nums`，你可以刪除一個只包含唯一元素的子陣列。刪除該子陣列後，你獲得的分數等於其所有元素之和。請返回你能夠通過刪除恰好一個子陣列所能獲得的最大分數。

- 子陣列：連續的元素區間。
- 分數：子陣列所有元素的和，且元素必須唯一。

## 解法一：MaximumUniqueSubarray

### 解題思路

- 使用滑動窗口（雙指標）與 `HashSet<int>` 來維護目前窗口內的唯一性。
- 右指標遍歷陣列，每次將新元素加入窗口和。
- 若遇到重複元素，左指標右移並移除集合中的元素，直到窗口內不含重複元素。
- 每次更新最大分數。

### 程式碼片段

```csharp
public int MaximumUniqueSubarray(int[] nums)
{
    int n = nums.Length;
    HashSet<int> seen = new HashSet<int>();
    int res = 0, currSum = 0;
    for (int i = 0, j = 0; i < n; i++)
    {
        currSum += nums[i];
        while (seen.Contains(nums[i]))
        {
            seen.Remove(nums[j]);
            currSum -= nums[j];
            j++;
        }
        seen.Add(nums[i]);
        res = Math.Max(res, currSum);
    }
    return res;
}
```

### 特點

- 時間複雜度：O(n)
- 空間複雜度：O(n)（最壞情況下所有元素都唯一）
- 適用於元素範圍大、但陣列長度有限的情境

## 解法二：MaximumUniqueSubarrayV2

### 解題思路

- 先遍歷陣列取得最大值，建立布林陣列 `has` 以記錄每個元素是否在目前窗口內。
- 右指標遍歷陣列，若遇到重複元素，左指標右移並移除窗口內重複元素，確保窗口內元素唯一。
- 每次更新最大分數。
- 若元素範圍極大，建議改用 `HashSet<int>` 以避免記憶體消耗過大。

### 程式碼片段

```csharp
public int MaximumUniqueSubarrayV2(int[] nums)
{
    if (nums is null)
        throw new ArgumentNullException(nameof(nums));
    int mx = 0;
    foreach (var x in nums)
        mx = Math.Max(mx, x);
    var has = new bool[mx + 1];
    int ans = 0, s = 0, left = 0;
    foreach (var x in nums)
    {
        while (has[x])
        {
            has[nums[left]] = false;
            s -= nums[left];
            left++;
        }
        has[x] = true;
        s += x;
        ans = Math.Max(ans, s);
    }
    return ans;
}
```

### 特點

- 時間複雜度：O(n)
- 空間複雜度：O(mx)（mx 為陣列最大值）
- 適用於元素範圍小、陣列長度不限的情境
- 查找速度快，空間利用率高

## 兩個解法比較

| 解法 | 資料結構 | 空間複雜度 | 適用場景 | 優點 | 缺點 |
|------|----------|------------|----------|------|------|
| MaximumUniqueSubarray | HashSet | O(n) | 元素範圍大 | 通用性高，無需預先知道元素範圍 | 查找速度略慢於陣列，空間隨陣列長度增長 |
| MaximumUniqueSubarrayV2 | 布林陣列 | O(mx) | 元素範圍小 | 查找速度極快，空間利用率高 | 元素範圍大時記憶體消耗高，需預先遍歷取得最大值 |

### 詳細比較

- **查找速度**：布林陣列（V2）查找速度優於 HashSet（V1），因為陣列存取為 O(1)，HashSet 需雜湊運算。
- **空間消耗**：V1 空間隨陣列長度增長，V2 空間隨元素最大值增長。若元素範圍極大（如 10^9），V2 不適用。
- **通用性**：V1 適合所有情境，V2 適合元素範圍明確且較小的題目。
- **程式碼可讀性**：兩者皆易於理解，V2 需額外處理最大值預先遍歷。

## 測試範例

```csharp
int[] nums = {4, 2, 4, 5, 6};
int result1 = MaximumUniqueSubarray(nums); // 預期 17
int result2 = MaximumUniqueSubarrayV2(nums); // 預期 17
```

## 結論

- 若題目元素範圍小，建議使用 MaximumUniqueSubarrayV2（布林陣列）。
- 若元素範圍未知或極大，建議使用 MaximumUniqueSubarray（HashSet）。
- 兩者皆可正確解決題目，請依據實際資料特性選擇最佳解法。

---

如需更多 Leetcode 題解，歡迎參考本專案其他檔案。
