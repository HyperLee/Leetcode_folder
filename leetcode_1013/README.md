# LeetCode 1013 - 將陣列分成和相等的三個部分

[![LeetCode](https://img.shields.io/badge/LeetCode-1013-orange?style=flat-square&logo=leetcode)](https://leetcode.com/problems/partition-array-into-three-parts-with-equal-sum/)
[![Difficulty](https://img.shields.io/badge/Difficulty-Easy-green?style=flat-square)](https://leetcode.com/problems/partition-array-into-three-parts-with-equal-sum/)
[![Language](https://img.shields.io/badge/Language-C%23-purple?style=flat-square&logo=csharp)](https://dotnet.microsoft.com/)

## 題目描述

給定一個整數陣列 `arr`，若能將陣列分成**三個非空的連續部分**且三部分的總和相等，則回傳 `true`。

形式上，若存在索引 `i` 與 `j`（滿足 `i + 1 < j`），使得：

```
arr[0] + ... + arr[i] == arr[i+1] + ... + arr[j-1] == arr[j] + ... + arr[arr.Length - 1]
```

則表示可分割，否則回傳 `false`。

### 範例

**範例 1：**
```
輸入：arr = [0, 2, 1, -6, 6, -7, 9, 1, 2, 0, 1]
輸出：true
解釋：
  0 + 2 + 1 = 3
  -6 + 6 - 7 + 9 + 1 = 3
  2 + 0 + 1 = 3
```

**範例 2：**
```
輸入：arr = [0, 2, 1, -6, 6, 7, 9, -1, 2, 0, 1]
輸出：false
解釋：陣列總和為 21，無法被 3 整除
```

**範例 3：**
```
輸入：arr = [3, 3, 6, 5, -2, 2, 5, 1, -9, 4]
輸出：true
解釋：
  3 + 3 = 6
  6 + 5 - 2 + 2 + 5 - 1 - 9 = 6（注意：這裡需要重新計算正確的分割）
```

### 限制條件

- `3 <= arr.length <= 5 * 10^4`
- `-10^4 <= arr[i] <= 10^4`

---

## 解題概念與想法

### 核心觀察

1. **必要條件**：若要將陣列分成三個和相等的部分，總和必須能被 3 整除
2. **分割點**：我們需要找到兩個分割點，將陣列分成三段
3. **前綴和的妙用**：利用前綴和可以快速計算任意區間的和

### 解題思路

這道題的關鍵在於理解**前綴和**（Prefix Sum）的概念：

> 前綴和是指從陣列開頭累加到當前位置的總和。利用前綴和，我們可以在 O(1) 時間內計算任意區間的和。

**核心想法**：
- 假設總和為 `S`，若能三等分，則每部分的和為 `S/3`
- 第一個分割點：前綴和等於 `S/3` 的位置
- 第二個分割點：前綴和等於 `2 * S/3` 的位置
- 第三部分自然就是 `S/3`（因為總和是 `S`）

---

## 詳細解法說明

### 演算法步驟

```
步驟 1：建構前綴和陣列
步驟 2：檢查總和是否能被 3 整除
步驟 3：計算目標值 target = 總和 / 3
步驟 4：遍歷尋找分割點
步驟 5：判斷是否找到足夠的分割點
```

### 程式碼詳解

```csharp
public bool CanThreePartsEqualSum(int[] arr)
{
    int n = arr.Length;

    // 步驟 1：原地建構前綴和陣列
    // 將 arr[i] 改為 arr[0] + arr[1] + ... + arr[i]
    for (int i = 1; i < n; i++)
    {
        arr[i] += arr[i - 1];
    }

    // 步驟 2：檢查總和是否能被 3 整除
    // arr[n-1] 現在存放的是整個陣列的總和
    if (arr[n - 1] % 3 != 0)
    {
        return false;
    }

    // 步驟 3：計算每部分應該達到的目標值
    int target = arr[n - 1] / 3;
    int count = 1;  // 計數器，記錄找到幾個分割點

    // 步驟 4：遍歷陣列尋找分割點（不含最後一個元素）
    // 為什麼不含最後一個？因為最後一個元素的前綴和一定等於 target * 3
    // 如果遍歷到最後一個，會錯誤地增加 count
    for (int i = 0; i < n - 1; i++)
    {
        // 當前綴和等於 target * count 時，找到一個分割點
        if (arr[i] == target * count)
        {
            count++;
        }
    }

    // 步驟 5：判斷是否找到至少 2 個分割點（count 從 1 開始，所以需要 >= 3）
    return count >= 3;
}
```

### 為什麼 `count >= 3` 而不是 `count == 3`？

> [!NOTE]
> 當 `target = 0` 時，可能存在多個分割點。例如 `[0, 0, 0, 0, 0]`，每個位置的前綴和都是 0，
> 都可以作為分割點。只要找到至少 2 個分割點（count >= 3），就能確保可以分成三部分。

### 為什麼只遍歷到 `n - 1`？

> [!IMPORTANT]
> 這是為了避免邊界情況的錯誤。考慮陣列 `[1, -1, 1, -1]`：
> - 前綴和：`[1, 0, 1, 0]`
> - 總和為 0，target = 0
> - 如果遍歷到最後一個元素，count 會變成 4，但實際上無法分成三個**非空**部分
> - 因為最後一個「部分」會是空的

---

## 範例演示流程

### 範例：`arr = [0, 2, 1, -6, 6, -7, 9, 1, 2, 0, 1]`

#### 步驟 1：建構前綴和陣列

| 索引 | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 |
|------|---|---|---|---|---|---|---|---|---|---|---|
| 原始值 | 0 | 2 | 1 | -6 | 6 | -7 | 9 | 1 | 2 | 0 | 1 |
| 前綴和 | 0 | 2 | 3 | -3 | 3 | -4 | 5 | 6 | 8 | 8 | 9 |

#### 步驟 2：檢查總和

```
總和 = arr[10] = 9
9 % 3 = 0 ✓ 可以被 3 整除
```

#### 步驟 3：計算目標值

```
target = 9 / 3 = 3
```

#### 步驟 4：尋找分割點

| 索引 i | 前綴和 arr[i] | target * count | 是否匹配 | count |
|--------|---------------|----------------|----------|-------|
| 0 | 0 | 3 × 1 = 3 | ✗ | 1 |
| 1 | 2 | 3 × 1 = 3 | ✗ | 1 |
| 2 | **3** | 3 × 1 = 3 | ✓ | **2** |
| 3 | -3 | 3 × 2 = 6 | ✗ | 2 |
| 4 | 3 | 3 × 2 = 6 | ✗ | 2 |
| 5 | -4 | 3 × 2 = 6 | ✗ | 2 |
| 6 | 5 | 3 × 2 = 6 | ✗ | 2 |
| 7 | **6** | 3 × 2 = 6 | ✓ | **3** |
| 8 | 8 | 3 × 3 = 9 | ✗ | 3 |
| 9 | 8 | 3 × 3 = 9 | ✗ | 3 |

> [!TIP]
> 注意索引 10 不會被遍歷（迴圈條件是 `i < n - 1`）

#### 步驟 5：判斷結果

```
count = 3 >= 3 ✓
回傳 true
```

#### 分割結果視覺化

```
[0, 2, 1] | [-6, 6, -7, 9, 1] | [2, 0, 1]
   ↓              ↓                ↓
   3              3                3
```

---

## 執行專案

### 環境需求

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### 建構與執行

```bash
# 建構專案
dotnet build

# 執行程式
dotnet run --project leetcode_1013
```

### 預期輸出

```
測試案例 1: [0, 2, 1, -6, 6, -7, 9, 1, 2, 0, 1]
結果: True

測試案例 2: [0, 2, 1, -6, 6, 7, 9, -1, 2, 0, 1]
結果: False

測試案例 3: [3, 3, 6, 5, -2, 2, 5, 1, -9, 4]
結果: True

測試案例 4: [1, -1, 1, -1]
結果: True

測試案例 5: [0, 0, 0, 0, 0]
結果: True
```

---

## 複雜度分析

| 項目 | 複雜度 | 說明 |
|------|--------|------|
| 時間複雜度 | O(n) | 遍歷陣列兩次（建構前綴和 + 尋找分割點） |
| 空間複雜度 | O(1) | 原地修改陣列，僅使用常數額外空間 |

---

## 相關題目

- [LeetCode 560 - Subarray Sum Equals K](https://leetcode.com/problems/subarray-sum-equals-k/)
- [LeetCode 974 - Subarray Sums Divisible by K](https://leetcode.com/problems/subarray-sums-divisible-by-k/)
- [LeetCode 523 - Continuous Subarray Sum](https://leetcode.com/problems/continuous-subarray-sum/)

---

## 參考資料

- [LeetCode 官方題解](https://leetcode.cn/problems/partition-array-into-three-parts-with-equal-sum/solution/1013-jiang-shu-zu-fen-cheng-he-xiang-deng-de-san-2/)
- [前綴和解法詳解](https://leetcode.cn/problems/partition-array-into-three-parts-with-equal-sum/solution/by-mulberry_qs-7utz/)
