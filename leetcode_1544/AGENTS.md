# Repository Guidelines

## 專案結構與命令

本資料夾包含一個巢狀 .NET 10 主控台專案：
leetcode_1544/leetcode_1544.csproj。從此題目根目錄執行：

    dotnet build leetcode_1544/leetcode_1544.csproj --nologo
    dotnet run --no-build --project leetcode_1544/leetcode_1544.csproj

先建置後才使用 --no-build。使用 Debug leetcode_1544 VS Code 組態；此題沒有
solution、根目錄專案或正式測試專案。

## 程式風格與解法契約

遵守 .editorconfig：C# 使用四空格縮排、控制流程保留大括號、明確型別、公開成員
採 PascalCase、區域變數採 camelCase。保留雙語題目 XML summary 與繁體中文的
MakeGood XML summary。

只保留 public static string MakeGood(string s) 這個公開解法 API。它必須保持純函式，
不輸出、不加入題目有效輸入之外的行為。使用 Stack<char> 維護已整理完成的前綴：
新字元和堆疊頂端為同字母異大小寫時彈出，否則推入。

## 驗證與 Git 範圍

Main 是唯一 console I/O 邊界，具有十個確定性 acceptance cases。每案都輸出
Case、Input、Expected、Actual 與 PASS/FAIL；全部成功的結尾必須是
Summary: 10/10 checks passed.，失敗時 exit code 必須為 1。

Git metadata 位於 parent repository 根目錄。任何 commit 或 PR 都必須只涵蓋
leetcode_1544/，且不可加入測試專案或套件。
