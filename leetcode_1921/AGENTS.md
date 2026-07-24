# Repository Guidelines

## 專案結構與命令

本資料夾包含巢狀 .NET 10 主控台專案：
`leetcode_1921/leetcode_1921.csproj`。從題目根目錄執行：

    dotnet build leetcode_1921/leetcode_1921.csproj --nologo
    dotnet run --no-build --project leetcode_1921/leetcode_1921.csproj

先建置後才使用 `--no-build`。VS Code 請使用 `Debug leetcode_1921`。此題沒有 solution、
根目錄專案或正式測試專案。

## 程式風格與解法契約

遵守 `.editorconfig`：C# 使用四空格縮排、控制流程保留大括號、明確型別、公開成員採
PascalCase、區域變數採 camelCase。保留 `Main` 的雙語題目 XML summary，以及公開解法
的繁體中文 XML summary 與高訊號不變量註解。

公開 API 僅為 `public static int EliminateMaximum(int[] dist, int[] speed)`。它是純函式：
不得修改輸入或輸出主控台，僅處理題目保證的有效輸入：兩個等長陣列長度介於 1 至 100000，
各值皆介於 1 至 100000。將每個抵達時間以 `(dist[i] - 1) / speed[i] + 1` 向上取整、
排序後，若第 `i` 個最早抵達時間小於等於 `i`，城市會在第 `i` 分鐘攻擊前失守；方法回傳
此前可消滅的數量。演算法只排序新的抵達時間陣列，故輸入維持不變。

## Harness 與 Git 範圍

`Main` 是唯一 console I/O 邊界，具有九個確定性 acceptance cases。每案驗證回傳值及
dist/speed 的合併輸入保存，共 18 個檢查。輸出以穩定的 `PASS/FAIL EliminateMaximum result`
及 `PASS/FAIL Input preserved` 行表示。全部成功的結尾必須是
`Summary: 18/18 checks passed.`，任一失敗時 exit code 必須為 1。`General partial loss`
以抵達時間 `[1,2,2,10]` 驗證答案為 `2`，避免把所有部分失敗誤判為 `1`。

Git metadata 位於 parent repository 根目錄。任何 commit 或 PR 都必須只涵蓋
`leetcode_1921/`，不可加入測試專案、套件或題目契約外的無效輸入行為。
