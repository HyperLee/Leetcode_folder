# Leetcode 118: 楊輝三角 C# 實作

## 專案簡介

本專案以 C# 13 實作 Leetcode 118 題「Pascal's Triangle（楊輝三角）」，展示如何產生前 N 行楊輝三角，並以最佳程式設計原則撰寫。

## 目錄結構

```text
leetcode_118.sln                # Visual Studio 解決方案檔
leetcode_118/                   # 主專案資料夾
    leetcode_118.csproj         # C# 專案描述檔
    Program.cs                  # 主程式，包含 Generate 函式
    bin/                        # 編譯後執行檔
    obj/                        # 編譯中間檔
```

## 主要程式碼說明

- `Program.cs`：
  - `Main` 方法：測試並輸出前 5 行楊輝三角。
  - `Generate(int numRows)`：依輸入行數產生楊輝三角，回傳巢狀 List 結構。
    - 邊界檢查：行數小於等於 0 回傳空集合。
    - 第一行固定為 [1]。
    - 其餘行每個元素為上一行左上與正上方元素之和，首尾皆為 1。

## 執行方式

1. 安裝 .NET 8 SDK（或更新版本）。

2. 於專案根目錄執行：

   ```zsh
   dotnet build
   dotnet run --project leetcode_118/leetcode_118.csproj
   ```

3. 終端機將顯示前 5 行楊輝三角。

## 延伸學習資源

- [Leetcode 118 題目說明](https://leetcode.com/problems/pascals-triangle/)
- [C# 官方文件](https://learn.microsoft.com/zh-tw/dotnet/csharp/)
- [.NET 開發指南](https://learn.microsoft.com/zh-tw/dotnet/)

## 設計原則

- 採用 C# 13 最新語法與最佳實踐。
- 程式碼具備完整註解與 XML 文件。
- 邊界檢查、例外處理完善。
- 易於擴充與測試。

---

## Generate 函式詳細解說

### 函式用途

`Generate(int numRows)` 用於產生楊輝三角（Pascal's Triangle）前 `numRows` 行，回傳巢狀 List 結構。此演算法廣泛應用於組合數學、動態規劃與面試題。

### 程式流程步驟

1. **建立儲存空間**

   ```csharp
   var triangle = new List<IList<int>>(numRows);
   ```

   - 預先分配容量，提升效能。

2. **邊界檢查**

   ```csharp
   if (numRows <= 0)
   {
       return triangle;
   }
   ```

   - 行數小於等於 0 時，直接回傳空集合，避免例外。

3. **加入第一行**

   ```csharp
   triangle.Add(new List<int> { 1 });
   ```

   - 楊輝三角第一行永遠是 [1]。

4. **逐行產生後續行**

   ```csharp
   for (int i = 1; i < numRows; i++)
   {
       var row = new List<int>(i + 1);
       row.Add(1);
       for (int j = 1; j < i; j++)
       {
           row.Add(triangle[i - 1][j - 1] + triangle[i - 1][j]);
       }
       row.Add(1);
       triangle.Add(row);
   }
   ```

   - 每行首尾皆為 1。
   - 中間元素由上一行左上與正上方元素相加。
   - 完成一行後加入結果。

5. **回傳結果**

   ```csharp
   return triangle;
   ```

   - 回傳所有已產生的行。

### 設計原則與 C# 實作亮點

- 採用 C# 13 最新語法，程式碼簡潔易懂。
- 使用泛型 `IList<IList<int>>`，提升相容性與延展性。
- 充分利用 List 預先分配容量，減少記憶體重分配。
- 以註解與 XML 文件輔助理解，便於維護。
- 邊界檢查與例外處理完善。

### 範例流程（numRows = 5）

1. triangle = []
2. triangle = [[1]]
3. triangle = [[1], [1, 1]]
4. triangle = [[1], [1, 1], [1, 2, 1]]
5. triangle = [[1], [1, 1], [1, 2, 1], [1, 3, 3, 1]]
6. triangle = [[1], [1, 1], [1, 2, 1], [1, 3, 3, 1], [1, 4, 6, 4, 1]]

### 常見問題與延伸討論

- **為何首尾皆為 1？**

  - 楊輝三角每行的第一個與最後一個元素，數學上皆為組合數 C(n,0) 與 C(n,n)，值皆為 1。

- **如何處理大數？**

  - 若行數極大，可改用 `BigInteger` 或其他資料結構。

- **如何改寫為遞迴？**

  - 本解法採用迴圈，遞迴寫法可參考組合數公式。

- **如何優化記憶體？**

  - 若只需最後一行，可用一維陣列動態更新。

---

如需進一步學習 C# 或 Leetcode 題解，歡迎參考上述資源。
