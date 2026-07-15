# 儲存庫協作指南

## 專案結構與模組配置

此題目資料夾包含一個 .NET 10 主控台專案。純函式 `MostCommonWord` 解法、雙語題目
XML 摘要與確定性驗收程式都應保留在
`leetcode_819/Program.cs`。巢狀的
`leetcode_819/leetcode_819.csproj` 定義可執行專案；`.vscode/` 包含直接建置與
偵錯的設定，而 `docs/readme-template.md` 只用於初次建立 README。

## 建置、執行與開發命令

請將外層 `leetcode_819/` 資料夾作為 VS Code 工作區開啟，並從該目錄使用明確
的巢狀專案路徑執行：

```bash
dotnet build leetcode_819/leetcode_819.csproj --nologo
dotnet run --no-build --project leetcode_819/leetcode_819.csproj
```

使用 `--no-build` 前必須先建置。在 VS Code 中請使用
`Debug leetcode_819`。不要執行未指定專案的 `dotnet build` 或
`dotnet test`：此資料夾根目錄沒有方案或專案，也沒有正式測試專案，因此
這些命令無法驗證本題。

## 程式風格與解法契約

遵循 `.editorconfig`：C# 使用四個空格縮排、控制流程加上大括號、使用明確型別而非
`var`、適合時使用檔案範圍命名空間；公開成員使用 PascalCase，區域變數與
參數使用 camelCase，私有靜態欄位使用 `s_camelCase`。保留 `Main` 上方的
雙語 XML 題目摘要。

`public static string MostCommonWord(string paragraph, string[] banned)` 必須維持
純函式：回傳小寫結果、不輸出至主控台。遵循 LeetCode 有效輸入契約，不自行加入
無效輸入例外。將 `banned` 單字正規化、由左至右掃描 `paragraph`、把每個非字母與
`paragraph` 結尾視為邊界，且只在合法單字次數嚴格超越目前最大值時更新答案。
驗收輸出只能由 `Main` 負責。

## 驗證指南

可執行檔內的驗收程式是目前的驗證機制。它執行恰好七項確定性結果檢查，涵蓋標點
邊界、最後單字結算、`banned` 單字正規化，以及 1000 字元的 `paragraph`。必須達成
乾淨建置、`Summary: 7/7 checks passed.` 與結束碼 0。由於儲存庫沒有獨立測試專案，
不得宣稱具有測試框架涵蓋率。

## 提交與 Pull Request

Git 中繼資料位於上層儲存庫根目錄。請從該根目錄使用
`git diff --check -- leetcode_819` 與 `git status --short` 檢查限定範圍的變更，
再只暫存 `leetcode_819/`。提交與 Pull Request 必須限制在此題目資料夾。使用限定
範圍的提交主旨 `feat(leetcode-819): migrate project to .NET 10`；審查資料應說明
字元掃描不變量、複雜度與已驗證的 7/7 驗收結果。
