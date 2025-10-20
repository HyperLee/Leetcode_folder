# leetcode_2011

此專案收錄 LeetCode 題目 2011（Final Value of Variable After Performing Operations） 的 C# 範例實作與說明。

## 簡介

這個小型專案示範如何使用 C#（.NET 8）解題並包含一個簡單的 `Program.cs`，實作了題目的解法並在 `Main` 中用三組範例輸入輸出驗證結果。

題目來源：
- 題目頁面（英文）: https://leetcode.com/problems/final-value-of-variable-after-performing-operations/
- 題目頁面（中文）: https://leetcode.cn/problems/final-value-of-variable-after-performing-operations/

## 題目說明（中文）

有一種程式語言只有四種操作和一個變數 X：

- ++X 與 X++ 將變數 X 的值增加 1；
- --X 與 X-- 將變數 X 的值減少 1。

起始時 X 的值為 0。給定一個字串陣列 `operations`，包含一系列操作；執行所有操作後，回傳 X 的最終值。

## 解題思路（詳盡）

1. 觀察

   - 題目中每個操作字串長度固定為 3，且只有四種可能的操作。因此，我們可以用最簡潔的方式判斷是遞增或遞減。

2. 直接模擬

   - 初始令 `x = 0`，逐一遍歷 `operations` 陣列。
   - 對於每個字串，檢查是否包含字元 `'+'`：
     - 若包含 `+`，表示該操作為遞增（`++X` 或 `X++`），令 `x++`。
     - 否則視為遞減（`--X` 或 `X--`），令 `x--`。

3. 為何這樣做是安全的

   - 題目保證操作字串僅為四種可能之一，因此以是否包含 `+` 作為判斷條件是充分且簡潔的。

4. 邊界與防禦性處理

   - 若 `operations` 為 `null` 或長度為 0，則回傳 0。
   - 若需要更嚴格的輸入驗證，可以額外檢查每個字串是否等於 `"++X"`, `"X++"`, `"--X"` 或 `"X--"`，並在遇到無效輸入時丟出 `ArgumentException` 或忽略該項。

## 時間與空間複雜度

- 時間複雜度：O(n)，其中 n 為 `operations.Length`。每個字串長度固定，判斷為 O(1)。
- 空間複雜度：O(1)，只使用常數額外空間。

## 程式碼說明（`Program.cs`）

- `FinalValueAfterOperations(string[] operations)`：解題的主函式，實作如上模擬邏輯。
- `Main`：包含三組範例測試（註解標示預期輸出），並將結果印出到主控台。

主要實作重點片段（節錄）：

```csharp
public int FinalValueAfterOperations(string[] operations)
{
    if (operations is null || operations.Length == 0) return 0;

    int x = 0;
    foreach (string operation in operations)
    {
        if (!string.IsNullOrEmpty(operation) && operation.Contains('+')) x++;
        else x--;
    }

    return x;
}
```

## 範例

範例輸入與預期輸出：

- `ops1 = ["--X","X++","X++"]` → `1`
- `ops2 = ["++X","++X","X++"]` → `3`
- `ops3 = ["X--","--X","++X","X++","--X"]` → `-1`

執行 `dotnet run`（在專案資料夾）會列印上述三個範例的計算結果。

## 如何執行

在 macOS / Linux / Windows 的終端機中，於專案根目錄（含 `leetcode_2011.csproj`）執行：

```bash
dotnet run --project leetcode_2011/leetcode_2011.csproj
```

或先建置再執行：

```bash
dotnet build
dotnet run --project leetcode_2011/leetcode_2011.csproj
```

## 延伸練習

- 改用更嚴格的驗證（顯式比對四種合法操作），並撰寫單元測試覆蓋正常與異常輸入。
- 將函式改寫為 LINQ 風格的單行實作，練習 C# 的表達能力。

## 備註

此專案以教學與示範為主，程式碼風格遵循簡潔與可讀性優先的原則。若要做為生產程式碼的一部分，建議補上完整的單元測試與更嚴格的輸入驗證。
