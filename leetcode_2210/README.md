# leetcode_2210

## 題目簡介

**2210. 統計數組中峰和谷的數量**

- 題目連結：[LeetCode 英文](https://leetcode.com/problems/count-hills-and-valleys-in-an-array/description/?envType=daily-question&envId=2025-07-27)
- 題目連結：[LeetCode 中文](https://leetcode.cn/problems/count-hills-and-valleys-in-an-array/description/?envType=daily-question&envId=2025-07-27)

### 題目描述（繁體中文）
給定一個 0-indexed 的整數陣列 nums。
- 當索引 i 的最近且不相等的左右鄰居都比 nums[i] 小時，i 屬於「峰」。
- 當索引 i 的最近且不相等的左右鄰居都比 nums[i] 大時，i 屬於「谷」。
- 若相鄰的索引 i 和 j 滿足 nums[i] == nums[j]，則 i 和 j 屬於同一個峰或谷。
- 注意：一個索引要被視為峰或谷，必須左右都有不相等的鄰居。

## 解法說明：CountHillValley 方法

### 解題思路
1. **去除連續重複元素**：
   - 題目規定相鄰且相等的元素屬於同一個峰或谷，因此只保留每段連續相同元素的第一個。
   - 例如：`[1,1,2,2,1,1]` 去重後為 `[1,2,1]`。
2. **判斷峰與谷**：
   - 只需一次判斷：`(filtered[i-1] < filtered[i] > filtered[i+1]) || (filtered[i-1] > filtered[i] < filtered[i+1])`
   - 只統計左右都有不相等鄰居的元素（即不包含頭尾）。

### 實作細節
```csharp
public int CountHillValley(int[] nums)
{
    // 去除連續重複元素，僅保留每段的第一個
    var filtered = new List<int>();
    filtered.Add(nums[0]); // 保留第一個元素
    for (int i = 1; i < nums.Length; i++)
    {
        // 若與前一個元素不同，則保留
        if (nums[i] != nums[i - 1])
        {
            filtered.Add(nums[i]);
        }
    }

    int res = 0;
    // 從第二個到倒數第二個，判斷是否為峰或谷
    for (int i = 1; i < filtered.Count - 1; i++)
    {
        // 一次判斷「峰」或「谷」：左鄰居 < 當前 > 右鄰居 或 左鄰居 > 當前 < 右鄰居
        if ((filtered[i] > filtered[i - 1] && filtered[i] > filtered[i + 1]) ||
            (filtered[i] < filtered[i - 1] && filtered[i] < filtered[i + 1]))
        {
            res++;
        }
    }
    // 回傳峰與谷的總數
    return res;
}
```

#### 設計細節與優化
- 先去重，避免重複計算，提升效率。
- 只需一次遍歷即可完成判斷，時間複雜度 O(n)。
- 充分利用 C# List 結構，程式碼簡潔易懂。
- 關鍵判斷條件已加上註解，方便維護。

## 執行方式

1. 安裝 .NET 8 SDK 以上版本。
2. 於專案根目錄執行：
   ```zsh
   dotnet run --project leetcode_2210/leetcode_2210.csproj
   ```
3. 終端機將顯示測試結果。

## 測試範例

```csharp
int[] nums = {2, 4, 1, 1, 6, 5};
var program = new Program();
int result = program.CountHillValley(nums);
Console.WriteLine($"Hills and Valleys count: {result}"); // 預期輸出 3
```

---

如需更多 LeetCode 題解或 C# 筆記，歡迎交流！
