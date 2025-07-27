# leetcode_98 專案說明文件

## 專案簡介

本專案為 LeetCode 第 98 題「驗證二元搜尋樹」（Validate Binary Search Tree）的 C# 解答範例，使用 .NET 8 開發。程式碼遵循最新 C# 12 語法與最佳實踐，並依據官方題目要求，實作了兩種驗證 BST 的方法。

## 目標與功能

- 驗證一棵二元搜尋樹（BST）是否有效。
- 提供兩種演算法：
  - 區間遞迴法（helper）
  - 中序遍歷法（InorderCheck）
- 具備完整註解與 XML 文件，方便學習與維護。

## 解法詳細說明與比較

### 1. 區間遞迴法（helper）

**原理說明**：
- 透過遞迴，每次呼叫時都傳入一個「合法值區間」(lowerbound, upperbound)。
- 檢查目前節點值是否落在此區間內，若不合法則直接返回 false。
- 左子樹遞迴時，更新上界為目前節點值；右子樹遞迴時，更新下界為目前節點值。
- 初始呼叫時區間為 (-∞, +∞)，確保整棵樹都在合法範圍。

**步驟**：
1. 若節點為 null，視為有效 BST（遞迴終止條件）。
2. 檢查 node.val 是否在 (lowerbound, upperbound) 開區間內。
3. 遞迴檢查左子樹（上界設為 node.val）、右子樹（下界設為 node.val）。

**優點**：
- 能正確處理所有 BST 邊界條件（包含重複值、極端值）。
- 適合用於任意型態的節點值（如 long、double）。
- 實作簡潔，遞迴結構清晰。

**缺點**：
- 每次遞迴都需傳遞上下界，程式碼略顯冗長。
- 若樹很深，遞迴層數多，可能導致 stack overflow（但一般情況下不影響）。

**效能分析**：
- 時間複雜度：O(n)，每個節點只訪問一次。
- 空間複雜度：O(h)，h 為樹高（遞迴棧空間）。

**適用場景**：
- 需嚴格驗證 BST 定義，或節點值型態非 int 時。
- 需處理複雜樹結構或自訂型態。

---

### 2. 中序遍歷法（InorderCheck）

**原理說明**：
- BST 的中序遍歷結果必須是嚴格遞增的序列。
- 透過遞迴中序遍歷，記錄上一次訪問的節點值，若有不遞增則返回 false。

**步驟**：
1. 遞迴左子樹。
2. 檢查目前節點值是否大於上一次訪問的值（prev）。
3. 更新 prev 為目前節點值。
4. 遞迴右子樹。

**優點**：
- 程式碼簡潔，易於理解。
- 只需一個 prev 變數即可追蹤狀態。
- 適合用於 int 節點值的標準 BST。

**缺點**：
- 若節點值型態非 int，需注意 prev 初始值選擇。
- 無法處理部分特殊 BST 定義（如允許重複值在右子樹）。

**效能分析**：
- 時間複雜度：O(n)，每個節點只訪問一次。
- 空間複雜度：O(h)，h 為樹高（遞迴棧空間）。

**適用場景**：
- 標準 BST 驗證，且節點值型態為 int。
- 需快速驗證 BST 性質時。

---

### 3. 兩種解法比較

| 項目         | 區間遞迴法（helper） | 中序遍歷法（InorderCheck） |
| ------------ | ------------------- | -------------------------- |
| 原理         | 節點值區間驗證      | 中序遍歷嚴格遞增           |
| 程式碼複雜度 | 適中                | 最簡                      |
| 邊界處理     | 最嚴謹              | 需注意 prev 初始值         |
| 適用型態     | 任意型態            | int 為主                  |
| 效能         | O(n), O(h)          | O(n), O(h)                |
| 可讀性       | 清楚                | 極佳                      |
| 特殊情境     | 支援複雜樹結構      | 標準 BST 驗證             |

**選用建議**：
- 若需嚴格驗證 BST 定義或節點值型態多元，建議使用「區間遞迴法」。
- 若僅需驗證標準 BST 且追求簡潔，可選「中序遍歷法」。
- 兩者效能相同，皆為 O(n) 時間、O(h) 空間。

---

## 程式架構

- `Program.cs`：主程式入口，包含 `TreeNode` 類別與兩種驗證 BST 的靜態方法。
  - `IsValidBST(TreeNode root)`：區間遞迴法，判斷每個節點值是否在合法範圍內。
  - `IsValidBST_Inorder(TreeNode root)`：中序遍歷法，判斷遍歷結果是否嚴格遞增。
- 所有程式碼均採用 file-scoped namespace 與最新 C# 格式。

## 如何執行

1. 安裝 .NET 8 SDK（建議使用最新版）
2. 於終端機執行以下指令建構並執行程式：

```zsh
# 建構專案
dotnet build leetcode_98/leetcode_98.csproj

# 執行專案
dotnet run --project leetcode_98/leetcode_98.csproj
```

3. 終端機將顯示兩種方法的驗證結果。

## 測試方式

- 目前主程式已內建測試資料（如 LeetCode 範例樹），可直接執行觀察結果。
- 若需擴充測試，可於 `Main` 方法新增不同的樹結構。
- 建議後續可加入單元測試專案，並使用 xUnit 或 MSTest 進行自動化測試。

## 相關連結與參考

- 題目說明：[LeetCode 98. Validate Binary Search Tree](https://leetcode.com/problems/validate-binary-search-tree/)
- C# 官方文件：[docs.microsoft.com/zh-tw/dotnet/csharp/](https://docs.microsoft.com/zh-tw/dotnet/csharp/)
- .NET 8 文件：[docs.microsoft.com/zh-tw/dotnet/core/dotnet-eight](https://docs.microsoft.com/zh-tw/dotnet/core/dotnet-eight)

## 開發規範

- 使用 C# 12 最新語法
- 依據 `.editorconfig` 格式化程式碼
- 具備完整註解與 XML 文件
- 變數命名遵循 PascalCase（公開成員、方法）、camelCase（私有欄位、區域變數）
- 例外處理與邊界條件皆有考量

---

如有任何問題或建議，歡迎提出！
