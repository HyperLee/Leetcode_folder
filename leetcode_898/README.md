# LeetCode 898: Bitwise ORs of Subarrays

[![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-13.0-blue.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![License](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)

## 📋 題目描述

給定一個整數陣列 `arr`，回傳所有非空子陣列的按位或運算的不同結果數量。

- **子陣列的按位或運算**：該子陣列中每個整數的按位或運算
- **單一整數的子陣列**：按位或運算就是該整數本身
- **子陣列**：陣列中連續非空的元素序列

### 範例

```text
輸入: arr = [0,1,1,2]
輸出: 3
解釋: 可能的結果為 {0, 1, 2, 3}，但題目要求的是 3 個

輸入: arr = [1,1,2]
輸出: 3

輸入: arr = [1,2,4]
輸出: 6
```

## 🔗 相關連結

- [LeetCode 原題](https://leetcode.com/problems/bitwise-ors-of-subarrays/)
- [LeetCode 中文版](https://leetcode.cn/problems/bitwise-ors-of-subarrays/)

## 🏗️ 專案結構

```text
leetcode_898/
├── leetcode_898.sln              # Visual Studio 解決方案檔案
├── README.md                     # 專案說明文件
├── leetcode_898/
│   ├── leetcode_898.csproj       # C# 專案檔案
│   ├── Program.cs                # 主要程式碼
│   ├── bin/                      # 編譯輸出目錄
│   └── obj/                      # 中間編譯檔案
└── .github/
    └── instructions/
        └── csharp.instructions.md # C# 開發指南
```

## 🚀 快速開始

### 環境需求

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) 或更高版本
- Visual Studio 2022 或 Visual Studio Code (推薦)

### 執行專案

```bash
# 複製專案
git clone <repository-url>
cd leetcode_898

# 建構專案
dotnet build

# 執行程式
dotnet run --project leetcode_898
```

### 使用 VS Code 工作區

```bash
# 開啟專案目錄
code leetcode_898

# 使用 VS Code 建構任務
Ctrl/Cmd + Shift + P → "Tasks: Run Task" → "build"
```

## 💡 解法分析

本專案實作了三種解決方案，從暴力法到優化演算法的完整展示，包含兩種不同風格的優化實現。

### 方法一：暴力解法 (Brute Force)

#### 演算法思路（暴力解法）

使用雙重迴圈枚舉所有可能的子陣列，計算每個子陣列的按位或運算結果。

```csharp
public int SubarrayBitwiseORs(int[] arr)
{
    HashSet<int> result = new HashSet<int>();
    
    // 外層迴圈：枚舉所有可能的子陣列起始位置
    for (int i = 0; i < arr.Length; i++)
    {
        int current = 0;
        
        // 內層迴圈：枚舉以位置 i 為起點的所有子陣列的結束位置
        for (int j = i; j < arr.Length; j++)
        {
            current |= arr[j];  // 累積按位或運算
            result.Add(current); // 自動去重
        }
    }
    
    return result.Count;
}
```

#### 複雜度分析（暴力解法）

- **時間複雜度**：`O(n²)`
  - 外層迴圈：`O(n)`
  - 內層迴圈：每次最多執行 `n` 次
  - 總計：`n × n = O(n²)`

- **空間複雜度**：`O(k)`
  - `k` 是不同按位或結果的數量
  - 最壞情況下 `k ≤ 32 × n`（32位元整數）

#### 優缺點（暴力解法）

✅ **優點**：

- 邏輯直觀，容易理解
- 實作簡單，不易出錯
- 適合小規模資料

❌ **缺點**：

- 時間複雜度高，大資料集效能差
- 對於長陣列，執行時間呈二次方成長

### 方法二：優化解法 (Optimized)

#### 演算法思路（優化解法）

利用按位或運算的**單調性特性**和**結果數量有限性**進行優化。

```csharp
public int SubarrayBitwiseORsOptimized(int[] arr)
{
    HashSet<int> allResults = new HashSet<int>();
    HashSet<int> currentResults = new HashSet<int>();
    
    foreach (int num in arr)  // O(n)
    {
        HashSet<int> newResults = new HashSet<int>();
        newResults.Add(num);  // 單元素子陣列
        
        // 關鍵優化：內層迴圈大小 ≤ 32
        foreach (int prevResult in currentResults)  // O(log(max))
        {
            newResults.Add(prevResult | num);
        }
        
        allResults.UnionWith(newResults);
        currentResults = newResults;
    }
    
    return allResults.Count;
}
```

#### 核心優化機制

1. **按位或運算單調性**：
   - `a | b ≥ a` 且 `a | b ≥ b`
   - 結果只會增加或保持不變，永不減少

2. **結果數量限制**：
   - 對於 32 位元整數，不同結果最多 32 個
   - `currentResults.Count ≤ 32`

3. **狀態轉移優化**：
   - 只維護「以當前位置結尾」的所有可能結果
   - 避免重複計算相同的中間結果

#### 複雜度分析（優化解法）

- **時間複雜度**：`O(n × log(max(arr)))`
  - 外層迴圈：`O(n)`
  - 內層迴圈：`O(log(max(arr))) ≈ O(32)` = `O(1)`
  - 總計：`n × 32 = O(n)`

- **空間複雜度**：`O(log(max(arr)))`
  - `allResults` 大小：`O(n × log(max(arr)))`
  - `currentResults` 大小：`O(log(max(arr))) ≈ O(32)`

#### 優缺點（優化解法）

✅ **優點**：

- 時間複雜度準線性，效能優異
- 空間使用效率高
- 適合大規模資料處理

❌ **缺點**：

- 演算法較複雜，需要深入理解按位運算特性
- 除錯難度相對較高

### 方法三：Java風格優化解法 (Java-style Optimized)

#### 演算法思路（Java風格解法）

這是與方法二完全相同演算法的另一種實現風格，更接近 LeetCode 官方解答。

```csharp
public int SubarrayBitwiseORsJavaStyle(int[] arr)
{
    HashSet<int> ans = new HashSet<int>();
    HashSet<int> cur = new HashSet<int> { 0 }; // 初始為 {0}
    
    foreach (int x in arr)
    {
        HashSet<int> cur2 = new HashSet<int>();
        
        // 先與所有之前的結果做按位或運算
        foreach (int y in cur)
        {
            cur2.Add(x | y);
        }
        
        cur2.Add(x); // 確保包含當前元素
        cur = cur2;
        ans.UnionWith(cur);
    }
    
    return ans.Count;
}
```

#### 方法二與方法三的核心差異

雖然兩種方法的演算法核心完全相同，但在實現細節上有關鍵差異：

**1. 初始化差異**：

- **方法二**：`currentResults` 初始為空集合 `{}`
- **方法三**：`cur` 初始為 `{0}`

**2. 處理順序差異**：

- **方法二**：「先加入元素，再做運算」

  ```csharp
  newResults.Add(num);  // 先加入當前元素
  foreach (int prevResult in currentResults)  // 再與前值運算
  ```

- **方法三**：「先做運算，再確保元素」

  ```csharp
  foreach (int y in cur)  // 先與前值運算（包含0）
      cur2.Add(x | y);
  cur2.Add(x);  // 再確保包含當前元素
  ```

**3. 為什麼初始化不同？**

- **方法二**：因為先加入元素，所以空集合沒問題
- **方法三**：因為先做運算，必須初始化 `{0}` 避免第一輪運算時空集合造成錯誤
- 利用「0 是按位或恆等元素」(`x | 0 = x`) 巧妙解決

#### 複雜度分析（Java風格解法）

- **時間複雜度**：`O(n × log(max(arr)))` - 與方法二完全相同
- **空間複雜度**：`O(log(max(arr)))` - 與方法二完全相同

## 📊 效能比較

### 理論分析比較表

| 項目 | 暴力解法 | 優化解法（方法二） | Java風格解法（方法三） | 改善倍數 |
|------|----------|--------------------|-----------------------|----------|
| 時間複雜度 | O(n²) | O(n × log(max)) | O(n × log(max)) | ≈ n/32 |
| 空間複雜度 | O(k) | O(log(max)) | O(log(max)) | 視情況而定 |
| 實作風格 | 直觀 | C# 風格 | LeetCode 官方風格 | - |
| 實作難度 | 簡單 | 中等 | 中等 | - |
| 可讀性 | 高 | 中等 | 中等 | - |

**註**：方法二和方法三的效能完全相同，只是實現風格不同。

### 實際效能測試

程式內建效能比較功能，測試 1000 個元素的陣列：

```text
=== 效能比較 ===
暴力解法耗時: XXX ms, 結果: YYYY
優化解法耗時: XXX ms, 結果: YYYY
Java風格解法耗時: XXX ms, 結果: YYYY
```

## 🧪 測試案例

### 內建測試

```csharp
// 測試案例 1: [0, 1, 1, 2] → 預期結果: 3
// 測試案例 2: [1, 1, 2]    → 預期結果: 3  
// 測試案例 3: [1, 2, 4]    → 預期結果: 6
```

### 詳細子陣列分析

程式提供多個輔助方法幫助理解演算法：

**1. `ShowAllSubarraysWithOR`** - 可視化展示所有子陣列及其按位或結果：

```text
=== 所有子陣列及其按位或結果 ===
[0] → 0
[0, 1] → 1
[0, 1, 1] → 1
[0, 1, 1, 2] → 3
[1] → 1
[1, 1] → 1
[1, 1, 2] → 3
[1] → 1
[1, 2] → 3
[2] → 2

不同的結果：{0, 1, 2, 3}
總共 4 個不同結果
```

**2. `DemonstrateSetMethod`** - 演示集合方法的執行過程：

```text
=== 集合方法演算法執行過程 ===
輸入陣列: [0, 1, 1, 2]

初始化: cur = {0}

步驟 1: 處理元素 A[0] = 0
  當前 cur = {0}
  計算新結果 cur2:
    0 | 0 = 0
    添加單元素: 0
  更新後 cur = {0}
  cur 的大小: 1 (≤ 32)
  ...
```

**3. `AnalyzeInitializationDifference`** - 詳細分析兩種優化方法的初始化差異：

```text
=== 初始化方式差異分析 ===
【方法二】currentResults 初始為空集合：
【方法三】cur 初始為 {0}：
=== 關鍵差異總結 ===
1. 【核心差異】邏輯順序與初始化的因果關係...
```

## 🔍 深入理解

### 按位或運算特性

1. **交換律**：`a | b = b | a`
2. **結合律**：`(a | b) | c = a | (b | c)`
3. **單調性**：`a | b ≥ max(a, b)`
4. **冪等性**：`a | a = a`
5. **恆等元素**：`a | 0 = a`（0 是按位或恆等元素）

### 優化解法的關鍵洞察

```csharp
// 為什麼內層迴圈變小了？
// 暴力解法：內層迴圈大小 = n（陣列長度）
// 優化解法：內層迴圈大小 = currentResults.Count ≤ 32

// 範例：處理陣列 [1, 2, 4, 8]
// i=0: currentResults = {1}           → 大小: 1
// i=1: currentResults = {2, 3}        → 大小: 2  
// i=2: currentResults = {4, 6, 7}     → 大小: 3
// i=3: currentResults = {8, 12, 14, 15} → 大小: 4
```

### 兩種優化解法的實現差異分析

雖然方法二和方法三演算法核心相同，但實現順序的不同帶來了有趣的設計思路：

**方法二設計思路**：

- 邏輯更直觀：先處理「當前元素本身」，再處理「與之前結果的組合」
- 適合理解：容易想到「每個元素都是一個單元素子陣列」

**方法三設計思路**：  

- 更接近數學本質：利用恆等元素統一處理邏輯
- 更簡潔：透過初始值 `{0}` 避免特殊處理第一個元素

**共同點**：

- 都利用了按位或運算的單調性
- 都實現了 O(n × log(max)) 的時間複雜度
- 內層迴圈大小都限制在 32 以內

## 📚 學習要點

### 對初學者

1. **理解暴力解法**：掌握雙重迴圈的基本思路
2. **位元運算基礎**：了解按位或運算的性質，特別是恆等元素概念
3. **HashSet 的使用**：學習自動去重的重要性
4. **演算法比較**：通過三種方法了解不同實現風格的差異

### 對進階開發者

1. **演算法優化思維**：從 O(n²) 到 O(n) 的優化過程
2. **數學特性應用**：利用運算特性降低複雜度
3. **狀態轉移技巧**：動態維護中間結果集合
4. **實現風格研究**：對比兩種優化方法的設計哲學差異
5. **初始化策略**：理解不同初始化方式對演算法實現的影響

### 演算法設計心得

1. **同一演算法多種實現**：展示了相同核心思想的不同表達方式
2. **恆等元素的巧妙運用**：方法三利用 0 是按位或恆等元素簡化邏輯
3. **邊界條件處理**：不同的處理順序需要不同的邊界條件考量

## 🛠️ 開發工具與最佳實踐

### C# 專案特性

- 使用 **C# 13** 最新語法特性
- 啟用 **Nullable Reference Types** 提高程式碼安全性
- 遵循 **PascalCase** 命名慣例
- 完整的 **XML 文件註解**

### 程式碼品質

- 豐富的註解說明演算法思路
- 清晰的方法分離和職責劃分
- 完整的範例和測試案例
- 效能比較和分析工具
- 三種解法的詳細比較與初始化差異分析
- 演示方法幫助理解演算法執行過程

## 🤝 貢獻指南

歡迎提交 Issue 和 Pull Request！

### 貢獻重點

1. **演算法優化**：提出更高效的解法或新的實現風格
2. **測試案例**：增加邊緣情況測試
3. **文件改善**：完善說明和註解
4. **效能分析**：提供更詳細的效能測試
5. **實現比較**：探討不同實現方式的優缺點

## 📄 授權條款

本專案採用 MIT 授權條款。詳見 [LICENSE](LICENSE) 檔案。

## 🔗 相關資源

- [.NET 官方文件](https://docs.microsoft.com/en-us/dotnet/)
- [C# 程式設計指南](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [LeetCode 演算法學習](https://leetcode.com/)
- [位元運算詳解](https://en.wikipedia.org/wiki/Bitwise_operation)

---

**💡 提示**：這個專案展示了從暴力解法到優化演算法的完整思考過程，包含兩種不同風格的優化實現，是學習演算法優化和實現技巧的絕佳範例！特別是透過對比兩種優化方法的初始化差異，可以深入理解演算法設計中邊界條件處理的重要性。
