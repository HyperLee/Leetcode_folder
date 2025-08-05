# Fruits Into Baskets II

> Leetcode 3477. Fruits Into Baskets II

## 專案簡介

本專案為 Leetcode 題目「3477. Fruits Into Baskets II」的 C# 解題範例，並附有詳細註解與多組測試資料。專案以 .NET 8 為基礎，適合用於學習演算法模擬與 C# 程式設計。

## 題目說明

給定兩個整數陣列 `fruits` 和 `baskets`，長度皆為 n，`fruits[i]` 代表第 i 種水果的數量，`baskets[j]` 代表第 j 個籃子的容量。

- 從左到右，依下列規則放置水果：
  1. 每種水果必須放入容量大於等於該水果數量的最左邊可用籃子。
  2. 每個籃子只能放一種水果。
  3. 若某種水果無法放入任何籃子，則該水果無法被放置。

請回傳所有無法被放置的水果種類數量。

[Leetcode 題目連結 (英文)](https://leetcode.com/problems/fruits-into-baskets-ii/description/?envType=daily-question&envId=2025-08-05)

[Leetcode 題目連結 (中文)](https://leetcode.cn/problems/fruits-into-baskets-ii/description/?envType=daily-question&envId=2025-08-05)

## 使用方式

```sh
# 建構專案
$ dotnet build

# 執行專案
$ dotnet run --project leetcode_3477/leetcode_3477.csproj
```

執行後會自動輸出多組測試資料的結果。

## 範例輸出

```
TestCase 1: 未放置水果種類數量 = 1
TestCase 2: 未放置水果種類數量 = 0
TestCase 3: 未放置水果種類數量 = 3
```

## 專案結構

- `leetcode_3477/Program.cs`：主程式與解題邏輯，包含詳細註解與測試資料。
- `leetcode_3477/leetcode_3477.csproj`：專案檔案。
- 其他目錄為 .NET 產生的建構與暫存檔案。

## 技術細節

- 使用 C# 12 與 .NET 8。
- 採用模擬法（Simulation）直接遍歷水果與籃子，資料量小時效率佳。
- 程式碼遵循專案內部 C# 命名與註解規範。

### 解法說明與流程

本題採用「模擬」策略，流程如下：

1. 依序遍歷每一種水果。
2. 對於每種水果，從左到右檢查每個籃子：
   - 若該籃子容量大於等於當前水果數量，則將該水果放入此籃子，並將該籃子容量設為 0（表示已被佔用），然後處理下一種水果。
   - 若所有籃子都無法放下該水果，則將未放置水果種類數量加一。
3. 最終回傳無法被放置的水果種類數量。

此方法簡單直觀，適合資料量不大時直接模擬所有情境。

> [!TIP]
> 可自由修改 `Program.cs` 內的測試資料，驗證不同情境下的演算法正確性。

## 參考資源
- [C# 官方文件](https://learn.microsoft.com/zh-tw/dotnet/csharp/)
- [Leetcode Discuss](https://leetcode.com/problems/fruits-into-baskets-ii/discuss/)
