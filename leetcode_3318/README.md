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

### 方法一: 暴力解法

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

### 方法二: 滑動視窗優化

這個方法優化了頻率統計的部分,避免每次都重新計算整個子陣列的頻率。

#### 演算法步驟

1. **初始化第一個視窗**:
   - 統計前 k 個元素的頻率
   - 計算第一個視窗的 x-sum

2. **滑動視窗移動**:
   - 移除最左邊離開視窗的元素:
     - 將該元素的頻率減 1
     - 如果頻率變為 0,從字典中移除該元素
   - 加入最右邊進入視窗的元素:
     - 將該元素的頻率加 1

3. **重新計算 x-sum**:
   - 每次視窗移動後,用更新過的頻率字典計算新的 x-sum
   - 排序和求和的邏輯與方法一相同

4. **重複步驟 2-3**:
   - 直到處理完所有 n-k+1 個視窗

#### 核心優化思路

滑動視窗的關鍵在於**增量更新**:

```text
視窗 i:     [a, b, c, d, e]
視窗 i+1:      [b, c, d, e, f]
             ↑移除      ↑加入
```

- 從視窗 i 到視窗 i+1,只有兩個元素發生變化
- 移除: `nums[i-1]` 離開視窗,頻率 -1
- 加入: `nums[i+k-1]` 進入視窗,頻率 +1
- 其他 k-2 個元素保持不變,不需重新統計

#### 程式碼實作重點

##### 1. 頻率增量更新

```csharp
// 移除離開視窗的元素
int leftElement = nums[i - 1];
frequencyMap[leftElement]--;
if (frequencyMap[leftElement] == 0)
{
    frequencyMap.Remove(leftElement);
}

// 加入進入視窗的元素
int rightElement = nums[i + k - 1];
frequencyMap.TryGetValue(rightElement, out int count);
frequencyMap[rightElement] = count + 1;
```

- 只更新兩個元素的頻率,時間複雜度 O(1)
- 及時清理頻率為 0 的元素,保持字典精簡

##### 2. 抽取計算邏輯

```csharp
private int CalculateXSum(IDictionary<int, int> frequencyMap, int x)
{
    // 排序 + 計算加權和
    // ...
}
```

- 將排序和計算邏輯抽取為獨立方法
- 提高程式碼複用性和可讀性
- 方便測試和維護

##### 3. 第一個視窗單獨處理

```csharp
// 初始化第一個視窗
for (int i = 0; i < k; i++)
{
    frequencyMap.TryGetValue(nums[i], out int count);
    frequencyMap[nums[i]] = count + 1;
}
result[0] = CalculateXSum(frequencyMap, x);
```

- 第一個視窗需要完整統計 k 個元素
- 之後的視窗都是增量更新

#### 複雜度分析

**時間複雜度**: O((n-k+1) × k × log k)

- 初始化第一個視窗: O(k)
- 對於每個後續視窗:
  - 頻率更新: O(1) ✨ (相比方法一的 O(k))
  - 轉換為列表: O(k)
  - 排序: O(k log k)
  - 計算和: O(min(x, k))
- 總複雜度仍由排序主導,但常數因子更小

**空間複雜度**: O(k)

- `frequencyMap`: 最多存儲 k 個不同元素
- `numsWithFrequency`: 臨時列表,最多 k 個項目
- 與方法一相同

#### 何時適用滑動視窗

滑動視窗在以下情況特別有優勢:

1. **k 值較大**: 當 k 接近 n 時,避免重複統計的收益明顯
2. **資料規模大**: n 很大時,減少常數因子的影響顯著
3. **頻繁查詢**: 需要多次對同一陣列計算不同參數的 x-sum

#### 進一步優化的可能性

雖然時間複雜度仍是 O((n-k+1) × k log k),但可以考慮:

1. **使用雙堆結構**:
   - 維護兩個堆:「前 x 大」和「其餘元素」
   - 當元素頻率改變時,只需調整堆的結構
   - 可將排序降為 O(log k)

2. **使用 SortedSet**:
   - 維護排序後的元素集合
   - 插入/刪除操作 O(log k)
   - 但實作較複雜

3. **快取排序結果**:
   - 如果連續視窗的頻率變化不大
   - 可以增量調整排序,而非完全重排

## 方法比較

### 方法一 vs 方法二

| 比較項目 | 方法一: 暴力解法 | 方法二: 滑動視窗 |
|---------|----------------|-----------------|
| **時間複雜度** | O((n-k+1) × k × log k) | O((n-k+1) × k × log k) |
| **頻率統計** | 每次 O(k) | 增量 O(1) ✨ |
| **實際效能** | 較慢 | 較快 |
| **程式碼複雜度** | 簡單直觀 | 稍複雜 |
| **記憶體使用** | O(k) | O(k) |
| **適用場景** | 小規模、一次性計算 | 大規模、多次查詢 |

### 效能分析

假設 n = 1000, k = 500:

- **方法一**: 每個視窗統計 500 次 → 總共 501 × 500 = 250,500 次操作
- **方法二**: 第一個視窗 500 次 + 之後每次 1 次 → 500 + 500 = 1,000 次操作
- **提升**: 約 250 倍的頻率統計效率提升 ⚡

### 選擇建議

**使用方法一 (暴力解法) 當**:

- 資料規模很小 (n ≤ 50,如本題)
- 程式碼簡潔性優先
- 一次性計算,不需要優化
- 學習或面試時展示基礎理解

**使用方法二 (滑動視窗) 當**:

- 資料規模較大 (n > 100)
- 需要展示優化思維
- 實際生產環境要求效能
- k 值較大,重複計算成本高
- 需要多次對同一陣列查詢

### 實測結果

在本題的測試案例中:

```csharp
// 範例 1
nums = [1,1,2,2,3,4,2,3], k = 6, x = 2
方法一輸出: [6, 10, 12] ✓
方法二輸出: [6, 10, 12] ✓

// 範例 2  
nums = [3,8,7,8,7,5], k = 2, x = 2
方法一輸出: [11, 15, 15, 15, 12] ✓
方法二輸出: [11, 15, 15, 15, 12] ✓
```

兩種方法結果完全一致,正確性有保證。

## 約束條件

- `1 ≤ n == nums.length ≤ 50`
- `1 ≤ nums[i] ≤ 50`
- `1 ≤ x ≤ k ≤ nums.length`

根據這些約束:

- 資料規模很小,暴力解法完全可行
- 不需要考慮過度優化
- 程式碼的可讀性和正確性更重要

## 測試案例

程式中包含兩個測試案例,並使用兩種方法進行驗證:

```csharp
// 測試範例 1
nums = [1,1,2,2,3,4,2,3], k = 6, x = 2
方法一輸出: [6, 10, 12] ✓
方法二輸出: [6, 10, 12] ✓
預期輸出: [6, 10, 12]

// 測試範例 2  
nums = [3,8,7,8,7,5], k = 2, x = 2
方法一輸出: [11, 15, 15, 15, 12] ✓
方法二輸出: [11, 15, 15, 15, 12] ✓
預期輸出: [11, 15, 15, 15, 12]
```

兩種方法的輸出完全一致,驗證了實作的正確性。

## 執行程式

```bash
cd leetcode_3318
dotnet run
```

## 學習重點

### 方法一重點

1. **字典的靈活使用**: 統計頻率是常見的模式
2. **LINQ 排序**: 多重排序條件的優雅寫法
3. **邊界條件處理**: 注意元素數量可能少於 x 的情況

### 方法二重點

1. **滑動視窗思維**: 增量更新而非重複計算
2. **頻率維護技巧**: 及時清理頻率為 0 的元素
3. **程式碼重構**: 抽取共用邏輯提高可維護性
4. **時間空間權衡**: 理解不同場景下的最佳選擇

### 通用重點

1. **演算法優化思路**: 從暴力到優化的思考過程
2. **複雜度分析**: 理解時間和空間複雜度的實際影響
3. **測試驗證**: 用多個測試案例確保正確性

## 相關題目

- LeetCode 239: Sliding Window Maximum
- LeetCode 347: Top K Frequent Elements
- LeetCode 692: Top K Frequent Words

這些題目都涉及滑動視窗和頻率統計的概念。
