# Repository Guidelines

## 專案結構與命令

本資料夾包含巢狀 .NET 10 主控台專案：
`leetcode_2418/leetcode_2418.csproj`。從題目根目錄執行：

    dotnet build leetcode_2418/leetcode_2418.csproj --nologo
    dotnet run --no-build --project leetcode_2418/leetcode_2418.csproj

從 repository 根目錄則使用 `leetcode_2418/leetcode_2418/leetcode_2418.csproj`。先建置後才使用
`--no-build`。VS Code 請使用 `Debug leetcode_2418`。此題沒有 solution、根目錄專案或正式測試專案。

## 程式風格與解法契約

遵守 `.editorconfig`：C# 使用四空格縮排、控制流程保留大括號、明確型別、file-scoped
namespace，以及公開成員採 PascalCase、區域變數採 camelCase。`Main` 保留雙語題目 XML
summary；兩個公開 API 使用繁體中文 XML summary，行內註解只說明唯一身高與索引排序不變量。

公開 API 為 `SortPeople(string[] names, int[] heights)` 與
`SortPeople2(string[] names, int[] heights)`，分別使用 Dictionary 降冪列舉與索引排序。
兩者接收等長的姓名、身高陣列；姓名只包含英文字母，身高為 1 至 100000 的互異正整數。
兩個方法都必須回傳依身高由高至低排列的新姓名陣列；不得修改輸入、輸出主控台、保留
跨呼叫狀態或新增題目契約外的例外。

## Harness 與 Git 範圍

`Main` 是唯一 console I/O 邊界，具有 7 組確定性案例。每案以獨立副本分別驗證兩個 API
的精確結果、姓名陣列未修改及身高陣列未修改，共 42 項檢查；涵蓋兩個官方範例、最小輸入、
錯誤排序回歸、數值與字串邊界，以及長度 1000 上限。全部成功時輸出必須以
`Summary: 42/42 checks passed.` 結尾，任一失敗時 exit code 必須為 1。

Git metadata 位於 parent repository 根目錄。任何 commit 或 PR 都必須只涵蓋
`leetcode_2418/`；不可加入測試專案、套件、額外公開 API 或題目契約外的行為。
