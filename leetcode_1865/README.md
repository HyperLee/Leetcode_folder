# LeetCode 1865: Finding Pairs With a Certain Sum

## 題目說明

你需要設計一個資料結構 `FindSumPairs`，支援以下兩種操作：

1. **Add(index, val)**：將 val 加到 nums2[index]。
2. **Count(tot)**：計算有多少對 (i, j) 使得 nums1[i] + nums2[j] == tot。

初始化時會給你兩個整數陣列 nums1 和 nums2。nums1 只讀，nums2 可以被修改。你需要高效地執行上述兩種操作。

---

## 題目需求

- **初始化**：給定 nums1 和 nums2，建立 FindSumPairs 物件。
- **Add 操作**：能夠即時將 val 加到 nums2 的指定索引，並正確更新內部狀態。
- **Count 操作**：能夠快速計算有多少對 (i, j) 滿足 nums1[i] + nums2[j] == tot。

效能要求：

- Add 操作需為 O(1)。
- Count 操作需為 O(n1)，其中 n1 為 nums1 長度。

---

## 解題思路與解題方法

### 解題思路

- 由於 nums1 長度小於等於 nums2，查詢時以 nums1 為外層，nums2 用哈希表（Dictionary）記錄每個數字出現次數。
- Add 操作時，先將 nums2[index] 的舊值在哈希表中的次數減 1，更新 nums2[index]，再將新值在哈希表中的次數加 1。
- Count 操作時，遍歷 nums1 的每個元素 num，查詢哈希表中 tot - num 的出現次數，累加即為答案。

### 解題方法

1. **初始化**：將 nums2 的每個數字及其出現次數存入哈希表 cnt。
2. **Add 操作**：
    - 取得 nums2[index] 的舊值，將 cnt[oldVal] 減 1。
    - 更新 nums2[index]。
    - 將 cnt[newVal] 加 1。
3. **Count 操作**：
    - 遍歷 nums1，每個 num 查詢 cnt[tot - num]，累加所有結果。

這種設計能確保 Add 操作 O(1)，Count 操作 O(n1)，符合題目效能需求。

---

## 專案說明

本專案為 LeetCode 第 1865 題「Finding Pairs With a Certain Sum」的 C# 解題專案，採用 .NET 8.0，並遵循現代 C# 13 語法與最佳實踐。

### 專案結構

- `leetcode_1865.sln`：Visual Studio 解決方案檔案。
- `leetcode_1865/`：主要原始程式碼資料夾。
    - `leetcode_1865.csproj`：C# 專案檔，目標框架為 .NET 8.0，啟用 Nullable 參考型別。
    - `Program.cs`：主程式與 `FindSumPairs` 類別實作。
    - `bin/`、`obj/`：編譯產生的中間與輸出檔案。

### 主要類別與方法

#### FindSumPairs

- `FindSumPairs(int[] nums1, int[] nums2)`：初始化物件，儲存兩陣列並建立 `nums2` 的計數哈希表。
- `void Add(int index, int val)`：將 `val` 加到 `nums2[index]`，並即時更新哈希表。
- `int Count(int tot)`：計算有多少對 (i, j) 使得 `nums1[i] + nums2[j] == tot`。

#### 設計說明

- 查詢時以 `nums1` 為外層，`nums2` 用哈希表儲存每個值的出現次數，提升查詢效率。
- `Add` 操作時間複雜度 O(1)，`Count` 操作 O(n1)，n1 為 `nums1` 長度。
- 詳細註解與設計理念請參考 `Program.cs`。

### 開發與執行

1. **建構專案**

    ```bash
    dotnet build
    ```

2. **執行程式**

    ```bash
    dotnet run --project leetcode_1865/leetcode_1865.csproj
    ```

### 程式碼風格與規範

- 採用 `.editorconfig` 強制程式碼風格。
- 變數、方法、類別命名遵循 C# 標準慣例。
- 公開 API 皆有 XML 註解。
- 例外處理與邊界情境皆有考量。

### 測試

- 建議於專案中加入單元測試以驗證關鍵路徑。

### 參考

- [LeetCode 1865. Finding Pairs With a Certain Sum](https://leetcode.com/problems/finding-pairs-with-a-certain-sum/)
- [C# 官方文件](https://learn.microsoft.com/zh-tw/dotnet/csharp/)
- [.NET 8.0 文件](https://learn.microsoft.com/zh-tw/dotnet/core/whats-new/dotnet-8)
