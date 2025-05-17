# LeetCode 001 - Two Sum

## 題目描述

給定一個整數陣列 `nums` 和一個整數目標值 `target`，請你在該陣列中找出和為目標值 `target` 的那兩個整數，並回傳它們的陣列索引。

- 你可以假設每種輸入只會對應一個答案。
- 陣列中同一個元素在答案裡不能重複出現。
- 你可以按任意順序回傳答案。

### 範例

```
輸入：nums = [2,7,11,15], target = 9
輸出：[0,1]
解釋：因為 nums[0] + nums[1] = 2 + 7 = 9，所以回傳 [0, 1]
```

## 解題思路

- 使用 Dictionary (雜湊表) 儲存已遍歷過的數字及其索引。
- 對於每個數字 `nums[i]`，計算 `target - nums[i]`，檢查這個差值是否已存在於雜湊表中。
- 若存在，代表找到配對，回傳兩個索引。
- 若不存在，將當前數字與索引加入雜湊表。
- 只需遍歷一次陣列，時間複雜度 O (n)。

## 專案結構

```
leetcode_001/
├── leetcode_001.csproj
├── Program.cs
```

## 執行方式

1. 確認已安裝 .NET 8.0 或以上版本。
2. 在終端機執行：
   ```sh
   dotnet run --project leetcode_001/leetcode_001.csproj
   ```
3. 預期輸出：
   ```
   ref: [0,1]
   ```

## 參考連結

- [LeetCode Two Sum 英文題目](https://leetcode.com/problems/two-sum/)
- [LeetCode 兩數之和 中文題目](https://leetcode.cn/problems/two-sum/description/)
