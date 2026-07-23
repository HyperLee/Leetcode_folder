# Repository Guidelines

## 專案結構與命令

本資料夾包含巢狀 .NET 10 主控台專案：
`leetcode_1887/leetcode_1887.csproj`。從題目根目錄執行：

    dotnet build leetcode_1887/leetcode_1887.csproj --nologo
    dotnet run --no-build --project leetcode_1887/leetcode_1887.csproj

先建置後才使用 `--no-build`。VS Code 請使用 `Debug leetcode_1887`。此題沒有 solution、
根目錄專案或正式測試專案。

## 程式風格與解法契約

遵守 `.editorconfig`：C# 使用四空格縮排、控制流程保留大括號、明確型別、公開成員採
PascalCase、區域變數採 camelCase。保留 `Main` 的雙語題目 XML summary，以及公開解法
的繁體中文 XML summary 與關鍵不變量註解。

公開 API 為 `public static int ReductionOperations(int[] nums)`。此方法為純函式：不得
修改輸入或輸出主控台，只處理題目保證長度 1 至 50000、元素值 1 至 50000 的陣列。
解法先複製並排序輸入；由小到大掃描時，已遇到的相異值層數等於目前元素要降低的操作
次數。不得把數值差距誤當成操作次數。

## 驗證與 Git 範圍

`Main` 是唯一 console I/O 邊界，具有九個確定性 acceptance cases。每案同時驗證回傳值
與輸入保存，並輸出 Case、Input、Expected、Actual、Input preserved 與 PASS/FAIL。全部
成功的結尾必須是 `Summary: 9/9 checks passed.`，失敗時 exit code 必須為 1。

Git metadata 位於 parent repository 根目錄。任何 commit 或 PR 都必須只涵蓋
`leetcode_1887/`，不可加入測試專案、套件或題目契約外的無效輸入行為。
