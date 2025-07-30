# Leetcode 2419: Longest Subarray With Maximum Bitwise AND

## 題目說明

- [Leetcode 2419. Longest Subarray With Maximum Bitwise AND (英文)](https://leetcode.com/problems/longest-subarray-with-maximum-bitwise-and/description/?envType=daily-question&envId=2025-07-30)
- [Leetcode 2419. 按位與最大的最長子陣列 (中文)](https://leetcode.cn/problems/longest-subarray-with-maximum-bitwise-and/description/?envType=daily-question&envId=2025-07-30)

**題目描述：**

給定一個整數陣列 `nums`，請找出所有子陣列中按位與（Bitwise AND）值最大的子陣列，並返回這些子陣列的最長長度。

- 子陣列必須是連續的一段元素。
- 按位與是將陣列中所有數字進行 AND 運算的結果。

## 專案結構

```text
leetcode_2419.sln
leetcode_2419/
    leetcode_2419.csproj
    Program.cs   // 主程式與解法
    bin/         // 編譯產物
    obj/         // 中間產物
```

- `Program.cs`：包含兩種解法與測試資料。
- `Main` 方法會自動執行多組測試案例，並輸出兩種解法的結果。

## 兩種解法說明

### 1. `LongestSubarray`（兩次遍歷）

#### 思路

1. 子陣列的 AND 最大值必然等於陣列中的某個元素（因為 AND 只會讓值變小或不變）。
2. 第一次遍歷找出最大值 `maxVal`。
3. 第二次遍歷統計連續出現 `maxVal` 的最長長度。

#### 程式碼片段

```csharp
public int LongestSubarray(int[] nums)
{
    int maxVal = int.MinValue;
    int maxLength = 0;
    int currentLength = 0;
    foreach (int num in nums)
    {
        maxVal = Math.Max(maxVal, num);
    }
    foreach (int num in nums)
    {
        if (num == maxVal)
        {
            currentLength++;
            maxLength = Math.Max(maxLength, currentLength);
        }
        else
        {
            currentLength = 0;
        }
    }
    return maxLength;
}
```

#### 時間複雜度

O(n)（兩次遍歷）

#### 優點

- 思路直觀，易於理解與維護。
- 適合初學者。

#### 缺點

- 需遍歷兩次陣列，雖然都是 O(n)，但常數略大。

---

### 2. `LongestSubarray2`（單次遍歷）

#### 思路

- 只需一次遍歷，動態追蹤最大值 `maxVal` 及其連續出現的最大長度。
- 每遇到比 `maxVal` 更大的值，重置計數；遇到等於 `maxVal` 的值，累加連續長度。

#### 程式碼片段

```csharp
public int LongestSubarray2(int[] nums)
{
    int maxLength = 0;
    int maxVal = 0;
    int currentLength = 0;
    foreach (int num in nums)
    {
        if (num > maxVal)
        {
            maxVal = num;
            currentLength = 1;
            maxLength = 1;
        }
        else if (num == maxVal)
        {
            currentLength++;
            maxLength = Math.Max(maxLength, currentLength);
        }
        else
        {
            currentLength = 0;
        }
    }
    return maxLength;
}
```

#### 時間複雜度

O(n)（單次遍歷）

#### 優點

- 只需一次遍歷，效率更高。
- 適合追求最佳效能的場景。

#### 缺點

- 邏輯較為緊湊，對初學者稍有挑戰。
- 若需額外統計最大值出現位置，需額外處理。

---

## 兩種解法比較

| 解法             | 遍歷次數 | 可讀性 | 效能 | 適用場景           |
|------------------|----------|--------|------|--------------------|
| LongestSubarray  | 2        | 高     | O(n) | 直觀、易維護       |
| LongestSubarray2 | 1        | 中     | O(n) | 追求效能、一次遍歷 |

- 兩者皆為 O(n) 時間複雜度，差異在於遍歷次數與程式碼可讀性。
- 若資料量極大，建議用 `LongestSubarray2`。
- 若重視可維護性與直觀，建議用 `LongestSubarray`。

## 如何執行與測試

1. 使用 .NET 8 或以上版本：

   ```zsh
   dotnet run --project leetcode_2419/leetcode_2419.csproj
   ```

2. 程式會自動執行多組測試資料，並輸出兩種解法的結果。

## 其他補充

- 程式碼已依據 C# 13 標準與最佳實踐撰寫，並有詳細註解。
- 測試資料涵蓋一般、邊界、極值等情境。
- 歡迎根據需求擴充更多測試案例。

## 聯絡方式

如有建議或問題，歡迎開 issue 或 PR。
