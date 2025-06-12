# leetcode_3423

## 題目簡介

本專案為 LeetCode 第 3423 題「循環陣列中相鄰元素的最大差值」的 C# 解題程式碼。

- 題目連結 (英文)：<https://leetcode.com/problems/maximum-difference-between-adjacent-elements-in-a-circular-array/>
- 題目連結 (中文)：<https://leetcode.cn/problems/maximum-difference-between-adjacent-elements-in-a-circular-array/>

## 解題說明

給定一個循環陣列 `nums`，請找出所有相鄰元素之間的最大絕對差值。

- 在循環陣列中，第一個和最後一個元素也視為相鄰。
- 程式會遍歷陣列，計算每一對相鄰元素的差值絕對值，並考慮首尾元素。
- 回傳最大差值。

## 程式碼片段

```csharp
public int MaxAdjacentDistance(int[] nums)
{
    int n = nums.Length;
    if (n < 2)
        return 0;
    int res = 0;
    for (int i = 1; i < n; i++)
        res = Math.Max(res, Math.Abs(nums[i] - nums[i - 1]));
    res = Math.Max(res, Math.Abs(nums[0] - nums[n - 1]));
    return res;
}
```

## 執行方式

1. 使用 .NET 8.0 或相容版本。
2. 編譯並執行 `Program.cs`。

```sh
# 建構
 dotnet build
# 執行
 dotnet run
```

## 測試與品質

- 目前程式碼無編譯錯誤。
- 歡迎貢獻更多測試案例。

## 相關資訊

- 類別：演算法、陣列處理
- 語言：C#
