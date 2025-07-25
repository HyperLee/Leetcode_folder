# Leetcode 3487: Maximum Unique Subarray Sum After Deletion

本專案為 Leetcode 第 3487 題「刪除後的最大子陣列元素和」的 C# 解題專案，使用 .NET 8.0 建立，並包含多組測試案例。

## 題目簡述
給定一個整數陣列 `nums`，你可以刪除任意數量的元素（但不能刪成空陣列），然後選擇一個所有元素皆唯一的子陣列，使其元素和最大。請回傳這樣的最大和。

- 題目連結：
  - [Leetcode 英文版](https://leetcode.com/problems/maximum-unique-subarray-sum-after-deletion/description/?envType=daily-question&envId=2025-07-25)
  - [Leetcode 中文版](https://leetcode.cn/problems/maximum-unique-subarray-sum-after-deletion/description/?envType=daily-question&envId=2025-07-25)

## 專案結構
```
leetcode_3487.sln                # Visual Studio 解決方案檔
leetcode_3487/                   # 專案主目錄
  leetcode_3487.csproj           # C# 專案檔
  Program.cs                     # 主程式與解題邏輯
  bin/                           # 編譯輸出目錄
  obj/                           # 中繼檔目錄
```

## 主要檔案說明
- `Program.cs`：
  - 包含主程式進入點與 `MaxSum` 解題函式。
  - 內建多組測試案例，執行時會自動驗證各種情境（正數、負數、全零、重複等）。
- `leetcode_3487.csproj`：
  - .NET 8.0 專案描述檔，已啟用 Nullable 參考型別與隱式 using。

## 建構與執行
### 需求
- .NET 8.0 SDK 以上

### 建構
在專案根目錄下執行：
```pwsh
# 建構專案
 dotnet build leetcode_3487/leetcode_3487.csproj
```

### 執行
```pwsh
# 執行主程式
 dotnet run --project leetcode_3487/leetcode_3487.csproj
```

執行後會顯示各組測試案例的最大和結果。

## 解題說明與步驟

### 題目重點

- 可刪除任意數量元素（但不能刪成空陣列）。
- 最終選擇一段所有元素皆唯一的子陣列，且元素和最大。

### 核心解題思路

無論採用哪種實作方式，核心思路都是貪心策略：

1. **正數唯一最大和**：由於可以刪除任意元素，最佳策略是保留所有正數且不重複，因為正數越多和越大。
2. **全為非正數情況**：若陣列中沒有正數，則只能選擇一個元素（不能為空），此時最大和為陣列中的最大值。

### 三種解題方式詳細比較

本專案提供三種不同的解題實作方式，各有其特點和適用場景：

#### 方法一：LINQ 優化版本 (`MaxSum`)

```csharp
public int MaxSum(int[] nums)
{
    var uniquePositiveSum = nums.Where(x => x > 0).Distinct().Sum();
    return uniquePositiveSum > 0 ? uniquePositiveSum : nums.Max();
}
```

**特點：**

- **程式碼簡潔**：僅用 2 行程式碼完成整個邏輯
- **可讀性高**：使用 LINQ 鏈式呼叫，邏輯清晰易懂
- **開發效率**：快速實作，減少錯誤機率

**適用場景：**

- 快速原型開發
- 程式碼可讀性要求高的專案
- 小到中等規模的資料集

**優缺點：**

- ✅ 程式碼簡潔，維護性佳
- ✅ 不易出錯，邏輯清晰
- ❌ 效能相對較低（需要多次遍歷）
- ❌ 記憶體使用可能較高

#### 方法二：手動實作版本 (`MaxSumManual`)

```csharp
public int MaxSumManual(int[] nums)
{
    var positiveNums = nums.Where(x => x > 0).OrderBy(x => x).ToArray();
    if (positiveNums.Length == 0) return nums.Max();
    
    int sum = positiveNums[0];
    for (int i = 1; i < positiveNums.Length; i++)
    {
        if (positiveNums[i] != positiveNums[i - 1])
            sum += positiveNums[i];
    }
    return sum;
}
```

**特點：**

- **記憶體優化**：避免 HashSet 的額外空間開銷
- **排序去重**：利用排序特性進行去重，減少雜湊運算
- **精細控制**：可以更精確控制記憶體使用和運算流程

**適用場景：**

- 記憶體受限的環境
- 需要精確控制效能的場景
- 大型資料集處理

**優缺點：**

- ✅ 記憶體使用相對較少
- ✅ 避免雜湊運算開銷
- ✅ 在某些情況下效能較佳
- ❌ 程式碼較複雜
- ❌ 需要額外的排序操作

#### 方法三：HashSet 去重版本 (`MaxSum2`)

```csharp
public int MaxSum2(int[] nums)
{
    HashSet<int> set = new HashSet<int>();
    for (int i = 0; i < nums.Length; i++)
    {
        if (nums[i] > 0) set.Add(nums[i]);
    }
    return set.Count == 0 ? nums.Max() : set.Sum();
}
```

**特點：**

- **經典實作**：使用傳統的 HashSet 去重方法
- **邏輯直觀**：明確展示「篩選正數 → 去重 → 求和」的步驟
- **效能穩定**：HashSet 操作具有 O(1) 平均時間複雜度

**適用場景：**

- 教學和演示用途
- 需要展示演算法步驟的場合
- 中等規模資料集

**優缺點：**

- ✅ 演算法邏輯清晰
- ✅ HashSet 操作效率高
- ✅ 易於理解和除錯
- ❌ 需要額外的 HashSet 空間
- ❌ 程式碼相對冗長

### 效能與複雜度比較

| 方法 | 時間複雜度 | 空間複雜度 | 程式碼行數 | 可讀性 | 記憶體效率 |
|------|------------|------------|------------|--------|------------|
| LINQ版本 | O(n) | O(k) | 2行 | ⭐⭐⭐⭐⭐ | ⭐⭐⭐ |
| 手動版本 | O(n log n) | O(k) | 8行 | ⭐⭐⭐ | ⭐⭐⭐⭐⭐ |
| HashSet版本 | O(n) | O(k) | 6行 | ⭐⭐⭐⭐ | ⭐⭐⭐⭐ |

> **註：** k 為唯一正數的個數

### 選擇建議

1. **一般開發情況**：推薦使用 **LINQ版本**，程式碼簡潔且效能足夠
2. **記憶體敏感場景**：選擇 **手動版本**，可獲得更好的記憶體效率
3. **教學或演示**：使用 **HashSet版本**，邏輯步驟最為清晰
4. **大型資料集**：建議先測試比較三種方法的實際效能表現

### 實際效能測試

本專案內建 100,000 個元素的大陣列效能測試，可以直觀比較三種方法的執行時間差異。在不同的硬體環境和資料特徵下，各方法的效能表現可能會有所不同。

## 設計說明

- 採用 C# 12 語法，程式碼風格遵循 .editorconfig 與最佳實踐。
- 解題邏輯：
  - 先將所有正數去重後加總，若無正數則回傳陣列最大值。
  - 時間複雜度 O(n)，空間複雜度 O(n)。
- 具備良好註解，便於理解與維護。

## 測試

- 內建多組涵蓋邊界情境的測試案例。
- 可依需求擴充更多測試。

## 其他

- 若需擴充單元測試，建議新增 xUnit 或 NUnit 測試專案。
- 如需部署或容器化，可參考 .NET 8 內建容器支援。

---
本專案僅用於 Leetcode 題目解題與 C# 練習參考。
