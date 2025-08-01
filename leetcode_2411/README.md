# LeetCode 2411 - 按位或最大的最小子數組長度

## 問題描述

### 2411. Smallest Subarrays With Maximum Bitwise OR

給你一個長度為 n 的 0 索引陣列 nums，陣列由非負整數組成。對於每個索引 i（0 <= i < n），你需要找出一個最小長度的非空子陣列，這個子陣列從 i 開始（包含 i），其按位或的值等於從 i 到 n-1 所有子陣列按位或的最大值。

換句話說，設 Bij 為子陣列 nums[i...j] 的按位或，你需要找到最小的 j，使得 nums[i...j] 的按位或等於 max(Bik)，其中 i <= k <= n-1。

**範例：**

```text
輸入: nums = [1,0,2,1,3]
輸出: [3,3,2,2,1]
解釋:
- 對於 i = 0: 從位置 0 開始的子陣列，最大按位或為 3 (1|0|2|1|3)，最小長度為 3
- 對於 i = 1: 從位置 1 開始的子陣列，最大按位或為 3 (0|2|1|3)，最小長度為 3
- 對於 i = 2: 從位置 2 開始的子陣列，最大按位或為 3 (2|1|3)，最小長度為 2
- 對於 i = 3: 從位置 3 開始的子陣列，最大按位或為 3 (1|3)，最小長度為 2
- 對於 i = 4: 從位置 4 開始的子陣列，最大按位或為 3 (3)，最小長度為 1
```

**連結：**

- [LeetCode 英文版](https://leetcode.com/problems/smallest-subarrays-with-maximum-bitwise-or/description/?envType=daily-question&envId=2025-07-29)
- [LeetCode 中文版](https://leetcode.cn/problems/smallest-subarrays-with-maximum-bitwise-or/description/?envType=daily-question&envId=2025-07-29)

## 專案結構

```text
leetcode_2411/
├── leetcode_2411.csproj          # C# 專案檔案
├── Program.cs                    # 主程式，包含兩種解法實作
├── Solution explanation1.prompt.md   # 解法一詳細說明
├── Solution explanation2.prompt.md   # 解法二問答說明
├── Solution explanation3.prompt.md   # 解法三補充說明
├── Solution explanation4.prompt.md   # 解法四總結說明
└── README.md                     # 專案說明文件
```

## 解法說明

本專案提供了兩種不同的解法來解決這個問題：

### 解法一：反向滑動視窗 + 單調堆疊

#### 核心概念

使用反向滑動視窗配合單調堆疊的思想，從右到左遍歷陣列，利用按位或運算的單調性質。

#### 詳細演算法步驟

1. **初始化設定**

   ```csharp
   // 最後一個元素的子陣列長度必為 1
   res[n - 1] = 1;
   
   // 建構初始堆疊：將最後兩個元素進行按位或運算
   nums[n - 1] |= nums[n - 2];
   ```

2. **反向遍歷處理**

   ```csharp
   for (int left = n - 2; left >= 0; left--)
   {
       leftOr |= nums[left];  // 將當前元素加入按位或運算
   }
   ```

3. **視窗縮小判斷**

   ```csharp
   // 當子陣列 [left,right] 的按位或值等於 [left,right-1] 時
   // 說明 nums[right] 對結果沒有貢獻，可以縮小視窗
   while (right > left && (leftOr | nums[right]) == (leftOr | nums[right - 1]))
   {
       right--; // 縮小右端點
   }
   ```

4. **堆疊重建機制**

   ```csharp
   if (bottom >= right)
   {
       // 重新建構堆疊，保持單調性質
       for (int i = left + 1; i <= right; i++)
       {
           nums[i] |= nums[i - 1];
       }
       bottom = left;
       leftOr = 0;
   }
   ```

#### 核心原理解析

- **單調性質**：按位或運算具有單調性，隨著元素增加，結果只會變大或不變
- **視窗縮小**：當發現某個元素對最終結果沒有貢獻時，可以安全地從視窗中移除
- **堆疊維護**：保證堆疊中至少有兩個數，方便進行視窗縮小的判斷

**時間複雜度：** O(n log U)，其中 U 是陣列中的最大值  
**空間複雜度：** O(1)，不考慮輸出陣列

### 解法二：記錄每個二進制位的最近出現位置

#### 核心理念

追蹤每個二進制位最近一次出現為 1 的位置，利用按位或運算的位元特性來精確計算最小子陣列長度。

#### 詳細算法流程

1. **位元追蹤初始化**

   ```csharp
   int[] pos = new int[31];  // 記錄每個二進制位最近出現為 1 的位置
   Array.Fill(pos, -1);      // 初始化為 -1，表示尚未出現
   ```

2. **反向遍歷與位元檢查**

   ```csharp
   for (int i = n - 1; i >= 0; --i)
   {
       int j = i;  // 初始化右邊界
       
       // 檢查每個二進制位（0 到 30，共 31 位）
       for (int bit = 0; bit < 31; ++bit)
       {
           // 使用位元運算檢查第 bit 位
           if ((nums[i] & (1 << bit)) == 0)
           {
               // 第 bit 位為 0，需要找到該位為 1 的最近位置
               if (pos[bit] != -1)
               {
                   j = Math.Max(j, pos[bit]);
               }
           }
           else
           {
               // 第 bit 位為 1，更新該位最近出現的位置
               pos[bit] = i;
           }
       }
   }
   ```

3. **位元運算技巧**

   ```csharp
   // 檢查第 bit 位是否為 1
   (nums[i] & (1 << bit)) == 0
   
   // 1 << bit：建立一個只有第 bit 位為 1 的數
   // & 運算：提取指定位元的值
   ```

#### 按位或運算的關鍵特性

1. **單調性**：`A | B >= A` 且 `A | B >= B`
2. **冪等性**：`A | A = A`
3. **位元獨立性**：每個二進制位的運算互不影響
4. **結合律**：`(A | B) | C = A | (B | C)`

#### 位元追蹤策略

- **位元為 1**：一旦某位變成 1，在後續的按位或運算中會保持為 1
- **位元為 0**：需要找到最近的包含該位為 1 的元素
- **右邊界計算**：取所有需要的位元最近出現位置的最大值

#### 演算法正確性證明

對於位置 `i`，最大按位或值等於從 `i` 到 `n-1` 所有可能子陣列的按位或最大值。由於按位或的單調性，這個最大值等於從 `i` 到 `n-1` 的全域按位或值。

通過追蹤每個位元最近出現的位置，我們可以精確找到達到這個最大值所需的最小範圍。

**時間複雜度：** O(n × 31) = O(n)  
**空間複雜度：** O(31) = O(1)，不考慮輸出陣列

#### 位元運算優勢

- **效率高**：位元運算比算術運算更快
- **記憶體友善**：只需要固定大小的陣列（31 個位置）
- **邏輯清晰**：每個位元獨立處理，便於理解和除錯

## 如何執行

### 前置要求

- .NET 8.0 或更高版本
- Visual Studio Code 或其他 C# 開發環境

### 建構和執行

1. **複製專案**

   ```bash
   git clone <repository-url>
   cd leetcode_2411
   ```

2. **建構專案**

   ```bash
   dotnet build
   ```

3. **執行程式**

   ```bash
   dotnet run --project leetcode_2411/leetcode_2411.csproj
   ```

### 測試資料

程式包含以下測試案例：

```csharp
int[][] testCases = new int[][]
{
    new int[] {1, 0, 2, 1, 3},    // 範例測試
    new int[] {0, 1, 2, 3, 4},    // 遞增序列
    new int[] {7, 7, 7, 7},       // 相同元素
    new int[] {1},                // 單一元素
    new int[] {8, 1, 2, 12, 7, 6} // 隨機測試
};
```

### 預期輸出

```text
TestCase 1: [1, 0, 2, 1, 3]
解法一: [3, 3, 2, 2, 1]
解法二: [3, 3, 2, 2, 1]

TestCase 2: [0, 1, 2, 3, 4]
解法一: [5, 4, 3, 2, 1]
解法二: [5, 4, 3, 2, 1]

...
```

## 程式碼特色

### C# 12 特性應用

- 使用最新的 C# 12 語法特性
- 檔案範圍命名空間宣告
- 完整的 XML 文件註解
- 遵循 Microsoft 編碼規範

### 程式碼品質

- **清晰的註解**：每個函式都有詳細的 XML 文件註解
- **錯誤處理**：適當的邊界條件處理
- **可維護性**：程式碼結構清晰，邏輯分離
- **效能最佳化**：兩種解法都針對時間和空間複雜度進行最佳化

### 測試驗證

- 提供多組測試資料驗證解法正確性
- 兩種解法結果對比，確保一致性
- 包含邊界情況測試（單一元素、相同元素等）

## 學習重點

### 演算法概念

1. **滑動視窗技術**
   - **反向滑動視窗**：從右到左進行視窗滑動，適用於需要「未來資訊」的問題
   - **視窗縮小策略**：當發現元素對結果無貢獻時主動縮小視窗
   - **動態邊界調整**：根據運算結果動態調整視窗左右邊界

2. **位元運算應用**
   - **按位或運算特性**：單調性、冪等性、結合律的深度應用
   - **位元獨立處理**：每個二進制位可以獨立追蹤和處理
   - **位元運算優化**：`(nums[i] & (1 << bit))` 高效檢查特定位元
   - **位元狀態追蹤**：記錄每個位元最近出現位置，實現精確範圍計算

3. **單調堆疊設計**
   - **堆疊重建機制**：當堆疊不滿足條件時的重建策略
   - **原地修改技巧**：直接在輸入陣列上建構堆疊，節省額外空間
   - **邊界維護**：使用 `bottom` 變數追蹤堆疊有效範圍

4. **貪心演算法思想**
   - **局部最優選擇**：每次選擇對當前位置最有利的右邊界
   - **全域最優保證**：通過按位或運算的單調性保證全域最優
   - **提前終止條件**：利用運算特性提前判斷並終止不必要的計算

### 演算法深度解析

#### 解法一：滑動視窗 + 單調堆疊

**關鍵洞察**：

- 按位或運算的結果隨著元素增加呈單調遞增趨勢
- 當 `nums[right]` 對 `leftOr | nums[right]` 沒有貢獻時，可以安全移除

**堆疊操作細節**：

```csharp
// 堆疊重建的核心邏輯
for (int i = left + 1; i <= right; i++)
{
    nums[i] |= nums[i - 1];  // 建立前綴或運算鏈
}
```

這個操作建立了一個「前綴或運算鏈」，確保後續的比較操作能正確判斷元素的貢獻度。

**時間複雜度分析**：

- 外層迴圈：O(n)
- 內層 while 迴圈：每個元素最多被處理 log U 次（U 為最大值）
- 總體：O(n log U)

#### 解法二：位元追蹤法

**核心數學原理**：

對於位置 i，最大按位或值為：

```text
max_or(i) = nums[i] | nums[i+1] | ... | nums[n-1]
```

由於按位或運算的單調性，這等價於找到包含所有必要位元的最小範圍。

**位元處理邏輯**：

```csharp
// 位元檢查的數學意義
if ((nums[i] & (1 << bit)) == 0)
{
    // 當前數字的第 bit 位為 0
    // 需要找到最近的第 bit 位為 1 的數字
    if (pos[bit] != -1)
    {
        j = Math.Max(j, pos[bit]);
    }
}
```

**最佳化策略**：

- 使用 31 位元追蹤（對應 32 位整數的有效位元）
- 反向遍歷確保 `pos` 陣列始終記錄最新的位元位置
- `Math.Max` 操作確保包含所有必要位元

### 進階程式設計技巧

1. **倒序遍歷的優勢**
   - 可以即時獲得「未來資訊」
   - 避免重複計算，提高演算法效率
   - 適用於需要右側資訊的動態規劃問題

2. **空間最佳化技術**
   - **原地修改**：直接在輸入陣列上進行操作
   - **滾動變數**：使用有限的變數記錄狀態
   - **位元壓縮**：使用位元操作減少記憶體佔用

3. **複雜度最佳化策略**
   - **提前終止**：利用運算特性避免不必要的計算
   - **批次處理**：一次處理多個位元，減少迴圈次數
   - **快取友善**：考慮記憶體存取模式，提高實際執行效率

4. **錯誤處理與邊界條件**
   - **單元素處理**：特別處理 n=1 的情況
   - **整數溢出**：雖然本題不涉及，但位元運算中需要注意
   - **初始化策略**：正確設定初始值，避免邊界錯誤

### 實戰應用場景

1. **位元運算最佳化**：在需要處理大量位元操作的系統中
2. **滑動視窗演算法**：處理子陣列/子字串問題的通用技巧
3. **動態規劃優化**：當狀態轉移具有單調性時的優化策略
4. **資料結構設計**：單調堆疊在解決「最近較小/較大元素」問題中的應用

## 兩種解法詳細比較

| 比較維度 | 解法一：滑動視窗+單調堆疊 | 解法二：位元追蹤法 |
|---------|----------------------|----------------|
| **核心思想** | 動態維護視窗範圍，利用單調性縮小視窗 | 追蹤每個位元最近出現位置 |
| **時間複雜度** | O(n log U) | O(n × 31) = O(n) |
| **空間複雜度** | O(1) 原地修改 | O(31) = O(1) |
| **實作難度** | 較高，需要理解堆疊重建邏輯 | 中等，位元操作相對直觀 |
| **記憶體使用** | 原地修改輸入陣列 | 需要額外的 pos 陣列 |
| **可讀性** | 較複雜，涉及多個狀態變數 | 較清晰，邏輯相對簡單 |
| **擴展性** | 適用於其他單調性問題 | 適用於位元相關問題 |
| **除錯難度** | 較難，狀態變化複雜 | 較易，每個位元獨立處理 |
| **實際效能** | 在小範圍數值時表現更好 | 在大範圍數值時表現更穩定 |
| **學習價值** | 高，涉及多種進階技巧 | 中，主要學習位元運算 |

### 選擇建議

- **學習目的**：建議先掌握解法二，再挑戰解法一
- **面試準備**：解法二更容易在有限時間內實作完成
- **實際應用**：根據數據特性選擇（數值範圍、陣列大小等）
- **程式碼維護**：解法二的維護成本更低

### 演算法延伸思考

1. **其他位元運算問題**：如何將位元追蹤法應用到按位與、按位異或等運算
2. **滑動視窗變體**：如何處理視窗大小固定的類似問題
3. **單調堆疊應用**：在哪些其他問題中可以運用類似的堆疊技巧
4. **複雜度分析**：為什麼解法一的複雜度包含 log U 因子

### 程式設計技巧

1. **倒序遍歷**：當需要未來資訊時的處理策略
2. **空間最佳化**：原地修改 vs 額外空間的權衡
3. **複雜度分析**：如何分析和最佳化演算法效能
4. **程式碼重構**：從暴力解法到最佳化解法的演進過程

## 參考資料

- [LeetCode 官方題解](https://leetcode.cn/problems/smallest-subarrays-with-maximum-bitwise-or/solutions/)
- [反向滑動視窗解法詳解](https://leetcode.cn/problems/smallest-subarrays-with-maximum-bitwise-or/solutions/1830911/by-endlesscheng-zai1/)
- [C# 位元運算指南](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators)

## 授權

此專案遵循 MIT 授權條款。

---

**作者：** HyperLee  
**建立日期：** 2025年7月29日  
**LeetCode 每日一題：** 2025-07-29
