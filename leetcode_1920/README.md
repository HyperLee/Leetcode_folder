# LeetCode 1920. Build Array from Permutation

## 題目描述

給定一個 0 索引的整數陣列 `nums`，長度為 n。要求建立一個和 `nums` 相同長度的陣列 `ans`，使得 `ans[i] = nums[nums[i]]`。題目保證 `nums` 是一個排列，即 0 <= nums[i] < n。

### 重點理解
- **排列**：陣列中包含所有從 0 到 n-1 的整數，每個數字恰好出現一次
- 因為 `nums` 是排列，所以 `nums[i]` 一定是一個有效的索引，不會超出範圍
- 實現 `ans[i] = nums[nums[i]]` 需要兩次索引查找

## 範例解釋

### 範例 1
```
輸入：nums = [0,2,1,5,3,4]
輸出：[0,1,2,4,5,3]
```

計算過程：
- ans[0] = nums[nums[0]] = nums[0] = 0
- ans[1] = nums[nums[1]] = nums[2] = 1
- ans[2] = nums[nums[2]] = nums[1] = 2
- ans[3] = nums[nums[3]] = nums[5] = 4
- ans[4] = nums[nums[4]] = nums[3] = 5
- ans[5] = nums[nums[5]] = nums[4] = 3

### 範例 2
```
輸入：nums = [5,0,1,2,3,4]
輸出：[4,5,0,1,2,3]
```

計算過程：
- ans[0] = nums[nums[0]] = nums[5] = 4
- ans[1] = nums[nums[1]] = nums[0] = 5
- ans[2] = nums[nums[2]] = nums[1] = 0
- ans[3] = nums[nums[3]] = nums[2] = 1
- ans[4] = nums[nums[4]] = nums[3] = 2
- ans[5] = nums[nums[5]] = nums[4] = 3

## 解題思路

這個問題可以直接按照題目要求進行實現：
1. 建立一個與輸入陣列 `nums` 相同長度的新陣列 `ans`
2. 使用 for 迴圈遍歷 `nums` 中的每個索引 `i`
3. 對於每個索引 `i`，計算 `nums[nums[i]]` 並將結果存入 `ans[i]`

因為題目保證 `nums` 是排列，所以索引一定在有效範圍內，不需要額外的邊界檢查。

## 視覺化理解

對於範例 1: `nums = [0,2,1,5,3,4]`

索引:  0  1  2  3  4  5
nums: [0, 2, 1, 5, 3, 4]

操作過程:
1. ans[0] = nums[nums[0]] = nums[0] = 0
2. ans[1] = nums[nums[1]] = nums[2] = 1
3. ans[2] = nums[nums[2]] = nums[1] = 2
... 依此類推

最終結果: ans = [0, 1, 2, 4, 5, 3]

## 程式碼實作

```csharp
public int[] BuildArray(int[] nums)
{
    int n = nums.Length;
    int[] ans = new int[n];
    
    for (int i = 0; i < n; i++)
    {
        ans[i] = nums[nums[i]];
    }
    
    return ans;
}
```

## 時間與空間複雜度

- **時間複雜度**: O(n) - 只需遍歷陣列一次
- **空間複雜度**: O(n) - 需要建立一個新的陣列來儲存結果

## 如何執行

1. 確保您已安裝 .NET SDK
2. 在專案目錄中執行以下命令:

```bash
dotnet run
```

程式將執行兩個測試案例並顯示結果。

## 延伸思考

是否有辦法進行原地計算（不使用額外空間）？可以考慮使用位元操作或數學技巧來在原陣列上進行修改，而不使用額外的 O(n) 空間。