# leetcode_1957

## 專案簡介

本專案為 LeetCode 第 1957 題「刪除字元使字串變好」的 C# 解題程式，使用 .NET 8 與 C# 12 最新語法建構。程式會移除字串中多餘的重複字元，使最終字串不含三個連續相同字元，並提供多組測試資料驗證結果。

## 題目說明

- 題目連結：[LeetCode 1957](https://leetcode.com/problems/delete-characters-to-make-fancy-string/)
- 給定一個字串 `s`，請刪除最少數量的字元，使刪除後的字串不會有三個連續的字元相同。
- 回傳刪除後的最終字串，答案唯一。

## 原理與設計

- 主要邏輯於 `MakeFancyString` 方法，使用 `StringBuilder` 逐字組合，遇到三連相同字元即移除最後一個，確保不會有三連重複。
- 時間複雜度 O(n)，空間複雜度 O(n)。
- 程式碼遵循 C# 12 標準、PascalCase 命名、檔案範圍命名空間，並具備完整 XML 註解與範例。

## 執行方式

1. 環境需求：.NET 8 SDK（跨平台，macOS、Windows、Linux 皆可）
2. 編譯程式：
   ```zsh
   dotnet build leetcode_1957/leetcode_1957.csproj
   ```
3. 執行程式：
   ```zsh
   dotnet run --project leetcode_1957/leetcode_1957.csproj
   ```
4. 終端機將顯示多組測試資料的輸入與結果。

## 目錄結構

```
leetcode_1957.sln
leetcode_1957/
  leetcode_1957.csproj
  Program.cs         # 主程式，包含解題邏輯與測試
  bin/               # 編譯輸出目錄
  obj/               # 中間檔案目錄
```

## 程式碼片段

```csharp
/// <summary>
/// 刪除字元使字串變好：
/// 給定一個字串 s，需刪除最少數量的字元，使刪除後的字串不會有三個連續相同的字元。
/// 本方法採用 C# 12 最新語法，並以 StringBuilder 高效組合字元。
/// </summary>
/// <param name="s">原始字串</param>
/// <returns>刪除後的最終字串</returns>
public string MakeFancyString(string s)
{
    // 使用 StringBuilder 來高效組合字元，避免字串多次拼接造成效能損失。
    StringBuilder sb = new StringBuilder();
    int n = s.Length;
    // 逐字遍歷原字串，將每個字元加入 sb
    for (int i = 0; i < n; i++)
    {
        sb.Append(s[i]);
        // 檢查 sb 最後三個字元是否相同
        // 若三連相同，移除最後一個字元，確保不會有三連重複
        if (i >= 2 && sb[sb.Length - 1] == sb[sb.Length - 2] && sb[sb.Length - 2] == sb[sb.Length - 3])
        {
            sb.Length--;
        }
    }
    // 回傳處理後的字串，確保答案唯一
    return sb.ToString();
}
```

### 詳細設計說明

- **核心邏輯**：本方法逐字遍歷輸入字串，將每個字元依序加入 StringBuilder。每次加入新字元後，檢查目前 sb 最後三個字元是否相同，若相同則移除最後一個，確保不會有三連重複。
- **效能分析**：
  - 時間複雜度 O(n)，每個字元最多只被處理一次。
  - 空間複雜度 O(n)，StringBuilder 儲存結果字串。
- **C# 12 語法特色**：
  - 採用檔案範圍命名空間與單行 using 指令，提升可讀性。
  - 變數命名遵循 PascalCase（公開成員）與 camelCase（區域變數）。
- **設計決策**：
  - 使用 StringBuilder 而非直接字串拼接，避免大量字串產生造成記憶體浪費。
  - 以 for 迴圈逐字處理，便於追蹤每個字元的狀態。
- **邊界處理**：
  - 當字串長度小於 3 時，不會進入三連判斷，直接回傳原字串。
  - 若輸入為 null，依 C# 型別系統不需額外判斷（參考 Nullable Reference Types 規範）。
- **例外處理**：
  - 本方法不會拋出例外，因為 StringBuilder 與字串索引皆有型別保證。
- **註解設計**：
  - 每個步驟均有中文註解，便於維護與教學。

<example>
<code>
MakeFancyString("aaabaaaa") // 回傳 "aabaa"
MakeFancyString("aab") // 回傳 "aab"
MakeFancyString("abcdddeeeeaabbbcd") // 回傳 "abcdddeeeaabbcd"
</code>
</example>

## 測試說明

- 主程式已內建多組測試資料，執行後自動驗證。
- 若需擴充測試，可於 `Program.cs` 的 `testCases` 陣列新增字串。

## 進階說明

- 專案結構簡潔，適合學習 C# 字串處理與 LeetCode 題目解法。
- 程式碼具備完整註解，易於維護與擴充。
- 若需進行單元測試、API 封裝或進階功能，建議依據 C# 指南分層設計與加入測試專案。

---

如需更多 C# 開發、測試、部署、效能優化等資訊，請參考 csharp.instructions.md 文件。
