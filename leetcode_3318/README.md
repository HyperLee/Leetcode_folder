# LeetCode 3318. Find X-Sum of All K-Long Subarrays I

## 題目描述

### 中文題目

給定一個整數陣列 `nums`(長度為 `n`)以及兩個整數 `k` 和 `x`。

對於一個陣列,其 **x-sum** 定義如下:

1. **統計頻率**: 統計陣列中每個元素的出現次數
2. **選擇元素**: 保留出現次數最多的前 `x` 個不同元素的所有出現
   - 若兩個元素出現次數相同,則數值較大的元素視為較頻繁
3. **計算總和**: 將保留下來的元素全部相加,得到該陣列的 x-sum

**特殊情況**: 若陣列中的不同元素少於 `x`,則 x-sum 即為整個陣列的總和。

請回傳一個長度為 `n - k + 1` 的整數陣列 `answer`,其中 `answer[i]` 是子陣列 `nums[i..i+k-1]` 的 x-sum。

### English Description

You are given an array `nums` of `n` integers and two integers `k` and `x`.

The **x-sum** of an array is calculated by the following procedure:

1. Count the occurrences of all elements in the array
2. Keep only the occurrences of the top `x` most frequent elements. If two elements have the same number of occurrences, the element with the bigger value is considered more frequent
3. Calculate the sum of the resulting array

**Note**: If an array has less than `x` distinct elements, its x-sum is the sum of the array.

Return an integer array `answer` of length `n - k + 1` where `answer[i]` is the x-sum of the subarray `nums[i..i + k - 1]`.

### 題目連結

- [LeetCode English](https://leetcode.com/problems/find-x-sum-of-all-k-long-subarrays-i/description/?envType=daily-question&envId=2025-11-04)
- [LeetCode 中文](https://leetcode.cn/problems/find-x-sum-of-all-k-long-subarrays-i/description/?envType=daily-question&envId=2025-11-04)

## 範例說明

### 範例 1

**輸入**:

```text
nums = [1,1,2,2,3,4,2,3], k = 6, x = 2
```

**輸出**:

```text
[6,10,12]
```

**解釋**:

- **子陣列 [1, 1, 2, 2, 3, 4]**:
  - 頻率統計: {1: 2次, 2: 2次, 3: 1次, 4: 1次}
  - 排序規則: 出現次數相同時,數值大的優先
  - 前 2 個: 2(出現2次) 和 1(出現2次)
  - x-sum = 2×2 + 1×2 = 4 + 2 = 6

- **子陣列 [1, 2, 2, 3, 4, 2]**:
  - 頻率統計: {1: 1次, 2: 3次, 3: 1次, 4: 1次}
  - 前 2 個: 2(出現3次) 和 4(出現1次,數值最大)
  - x-sum = 2×3 + 4×1 = 6 + 4 = 10

- **子陣列 [2, 2, 3, 4, 2, 3]**:
  - 頻率統計: {2: 3次, 3: 2次, 4: 1次}
  - 前 2 個: 2(出現3次) 和 3(出現2次)
  - x-sum = 2×3 + 3×2 = 6 + 6 = 12

### 範例 2

**輸入**:

```text
nums = [3,8,7,8,7,5], k = 2, x = 2
```

**輸出**:

```text
[11,15,15,15,12]
```

**解釋**:

由於 `k = 2`,每個子陣列恰好有 2 個元素,且 `x = 2`,所以每個子陣列的 x-sum 就是所有元素的總和:

- 子陣列 [3, 8]: 3 + 8 = 11
- 子陣列 [8, 7]: 8 + 7 = 15
- 子陣列 [7, 8]: 7 + 8 = 15
- 子陣列 [8, 7]: 8 + 7 = 15
- 子陣列 [7, 5]: 7 + 5 = 12

## 解題思路

### 暴力解法(當前實作)

這是最直觀的解法,適合題目的資料範圍限制。

#### 演算法步驟

1. **遍歷所有子陣列**:
   - 陣列長度為 `n`,會有 `n - k + 1` 個長度為 `k` 的子陣列
   - 使用外層迴圈遍歷每個子陣列的起始位置

2. **統計頻率**:
   - 對於每個子陣列,使用 `Dictionary<int, int>` 統計元素出現次數
   - 鍵是元素值,值是該元素的出現次數

3. **建立頻率列表**:
   - 將字典轉換為 `List<int[]>`,每個項目是 `[元素值, 頻率]`
   - 這樣便於後續排序操作

4. **排序**:
   - 使用 LINQ 的 `OrderByDescending` 和 `ThenByDescending`
   - 第一優先: 按頻率降序(出現次數多的在前)
   - 第二優先: 按元素值降序(數值大的在前)

5. **計算加權和**:
   - 取前 `x` 個元素(若總數不足 `x` 則取全部)
   - 對每個元素計算: 元素值 × 出現次數
   - 累加得到該子陣列的 x-sum

6. **儲存結果**:
   - 將每個子陣列的 x-sum 存入結果陣列

#### 複雜度分析

**時間複雜度**: O((n-k+1) × k × log k)

- 外層迴圈執行 `n-k+1` 次
- 對於每個子陣列:
  - 統計頻率: O(k)
  - 建立列表: O(k)
  - 排序: O(k log k)
  - 計算和: O(min(x, k))
- 總複雜度由排序主導

**空間複雜度**: O(k)

- `frequencyMap`: 最多存儲 k 個不同元素
- `numsWithFrequency`: 最多 k 個項目
- `result`: O(n-k+1),但這是必需的輸出空間

### 程式碼實作重點

#### 1. 頻率統計技巧

```csharp
frequencyMap.TryGetValue(nums[i + j], out int count);
frequencyMap[nums[i + j]] = count + 1;
```

- 使用 `TryGetValue` 安全地獲取當前計數
- 如果鍵不存在,`count` 會是 0(int 的預設值)
- 這避免了需要先檢查鍵是否存在

#### 2. 雙重排序條件

```csharp
numsWithFrequency
    .OrderByDescending(pair => pair[1])  // 先按頻率降序
    .ThenByDescending(pair => pair[0])   // 再按數值降序
```

- 完美實現題目要求的優先順序
- LINQ 方法讓程式碼簡潔易讀

#### 3. 邊界處理

```csharp
int maxElements = Math.Min(x, numsWithFrequency.Count);
```

- 處理不同元素少於 `x` 的情況
- 確保不會超出列表範圍

## 約束條件

- `1 ≤ n == nums.length ≤ 50`
- `1 ≤ nums[i] ≤ 50`
- `1 ≤ x ≤ k ≤ nums.length`

根據這些約束:

- 資料規模很小,暴力解法完全可行
- 不需要考慮過度優化
- 程式碼的可讀性和正確性更重要

## 可能的優化方向

雖然當前解法已經足夠,但如果資料規模更大,可以考慮:

### 1. 滑動視窗優化

- 使用滑動視窗技術維護頻率字典
- 從一個子陣列移動到下一個時:
  - 移除最左邊的元素
  - 加入最右邊的新元素
- 可以將統計頻率的時間從 O(k) 降為 O(1)

### 2. 資料結構優化

- 使用有序資料結構(如 `SortedSet`)維護排序
- 避免每次都重新排序
- 但實作複雜度會增加

### 3. 增量更新

- 維護前 x 個元素的狀態
- 只在必要時更新排序
- 適合 x 遠小於 k 的情況

## 測試案例

程式中包含兩個測試案例:

```csharp
// 測試範例 1
nums = [1,1,2,2,3,4,2,3], k = 6, x = 2
預期輸出: [6, 10, 12]

// 測試範例 2  
nums = [3,8,7,8,7,5], k = 2, x = 2
預期輸出: [11, 15, 15, 15, 12]
```

## 執行程式

```bash
cd leetcode_3318
dotnet run
```

## 學習重點

1. **字典的靈活使用**: 統計頻率是常見的模式
2. **LINQ 排序**: 多重排序條件的優雅寫法
3. **滑動視窗思維**: 雖然此解法未使用,但要理解這個概念
4. **邊界條件處理**: 注意元素數量可能少於 x 的情況
5. **時間空間權衡**: 在小資料規模下,程式碼清晰度優先

## 相關題目

- LeetCode 239: Sliding Window Maximum
- LeetCode 347: Top K Frequent Elements
- LeetCode 692: Top K Frequent Words

這些題目都涉及滑動視窗和頻率統計的概念。
