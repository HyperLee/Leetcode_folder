# Leetcode 136: Single Number

## 題目說明

給定一個非空整數陣列 `nums`，除了某一個元素只出現一次以外，其餘每個元素都會出現兩次。請找出那個只出現一次的元素。

題目要求解法必須具備線性時間複雜度，並且只能使用常數額外空間。

- 題目連結：[Leetcode 136. Single Number](https://leetcode.com/problems/single-number/description/)
- 中文連結：[Leetcode 136. 只出現一次的數字](https://leetcode.cn/problems/single-number/description/)

## 限制條件

- `1 <= nums.Length <= 3 * 10^4`
- `-3 * 10^4 <= nums[i] <= 3 * 10^4`
- 除了某一個元素只出現一次以外，其餘每個元素都會出現兩次。

## 解題概念與出發點

這題的核心是「成對元素會重複出現，只有答案不成對」。可以從兩個方向思考：

1. 直接統計每個數字出現次數，再找出次數為 1 的數字。
2. 使用 XOR 的抵消特性，讓成對數字互相抵消，只留下唯一答案。

本專案實作三種方法：

| 方法 | 函式 | 核心概念 | 時間複雜度 | 額外空間 |
| --- | --- | --- | --- | --- |
| 方法一 | `SingleNumber` | Dictionary 統計次數，再用 LINQ 找答案 | O(n) | O(n) |
| 方法二 | `SingleNumber2` | Dictionary 統計次數，再手動遍歷找答案 | O(n) | O(n) |
| 方法三 | `SingleNumber3` | XOR 抵消成對數字 | O(n) | O(1) |

> 題目要求常數額外空間，因此 `SingleNumber3` 是最符合題目限制的解法；前兩種 Dictionary 解法適合用來理解問題與驗證答案。

## 方法一：Dictionary + LINQ 查找

`SingleNumber` 先建立 `Dictionary<int, int>`，把每個數字對應到它的出現次數。統計完成後，透過 `FirstOrDefault(x => x.Value == 1)` 找出次數為 1 的項目。

### 範例演示

以 `nums = [4, 1, 2, 1, 2]` 為例：

1. 掃描 `4`，紀錄 `{ 4: 1 }`
2. 掃描 `1`，紀錄 `{ 4: 1, 1: 1 }`
3. 掃描 `2`，紀錄 `{ 4: 1, 1: 1, 2: 1 }`
4. 再次掃描 `1`，更新為 `{ 4: 1, 1: 2, 2: 1 }`
5. 再次掃描 `2`，更新為 `{ 4: 1, 1: 2, 2: 2 }`
6. 找出次數為 `1` 的 key，答案是 `4`

## 方法二：Dictionary + foreach 查找

`SingleNumber2` 的統計方式與方法一相同，差異在於最後不用 LINQ，而是手動遍歷 Dictionary，找到 `Value == 1` 的項目後直接回傳。

### 範例演示

以 `nums = [2, 2, 1]` 為例：

1. 統計後得到 `{ 2: 2, 1: 1 }`
2. 遍歷 Dictionary 時先檢查 `2`，出現次數為 `2`，略過
3. 檢查 `1`，出現次數為 `1`，回傳 `1`

這個方法和方法一的答案相同，但查找流程更明確，不依賴 LINQ。

## 方法三：XOR 位元運算

`SingleNumber3` 使用 XOR 的三個特性：

- `a ^ 0 = a`
- `a ^ a = 0`
- XOR 具有交換律與結合律

因此，所有成對出現的數字最後都會抵消為 `0`，只出現一次的數字會被保留下來。

### 範例演示

以 `nums = [4, 1, 2, 1, 2]` 為例：

```text
res = 0
res = 0 ^ 4 = 4
res = 4 ^ 1 = 5
res = 5 ^ 2 = 7
res = 7 ^ 1 = 6
res = 6 ^ 2 = 4
```

最後 `res = 4`，答案是 `4`。

## 執行方式

建置專案：

```sh
dotnet build leetcode_136.sln
```

執行範例：

```sh
dotnet run --project leetcode_136/leetcode_136.csproj
```

範例輸出：

```text
Input: nums = [2, 2, 1]
Expected: 1
SingleNumber  (Dictionary + LINQ lookup): 1
SingleNumber2 (Dictionary + foreach):     1
SingleNumber3 (XOR):                      1

Input: nums = [4, 1, 2, 1, 2]
Expected: 4
SingleNumber  (Dictionary + LINQ lookup): 4
SingleNumber2 (Dictionary + foreach):     4
SingleNumber3 (XOR):                      4

Input: nums = [1]
Expected: 1
SingleNumber  (Dictionary + LINQ lookup): 1
SingleNumber2 (Dictionary + foreach):     1
SingleNumber3 (XOR):                      1

Input: nums = [-1, -1, -2]
Expected: -2
SingleNumber  (Dictionary + LINQ lookup): -2
SingleNumber2 (Dictionary + foreach):     -2
SingleNumber3 (XOR):                      -2
```

> 在部分 macOS/.NET 環境中，執行時可能會先輸出 `CSSM_ModuleLoad(): One or more parameters passed to a function were not valid.`。這是環境訊息，不影響本專案範例輸出與結果。

## 檔案結構

- `leetcode_136/Program.cs`：主程式、範例測試資料與三種解法
- `leetcode_136/leetcode_136.csproj`：C# 專案檔
- `leetcode_136.sln`：Solution 檔

## 學習重點

- Dictionary 解法能直觀呈現「統計次數後找唯一值」的想法。
- XOR 解法利用位元運算抵消成對數字，是本題最符合限制條件的解法。
- 當題目要求 O(1) 額外空間時，應優先思考是否存在可抵消、可累積或可原地處理的性質。
