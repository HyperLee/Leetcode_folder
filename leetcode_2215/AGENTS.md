# Repository Guidelines

## 專案結構與命令

本資料夾包含巢狀 .NET 10 主控台專案：
`leetcode_2215/leetcode_2215.csproj`。從題目根目錄執行：

    dotnet build leetcode_2215/leetcode_2215.csproj --nologo
    dotnet run --no-build --project leetcode_2215/leetcode_2215.csproj

從 repository 根目錄則使用 `leetcode_2215/leetcode_2215/leetcode_2215.csproj`。先建置後才使用
`--no-build`。VS Code 請使用 `Debug leetcode_2215`。此題沒有 solution、根目錄專案或正式測試專案。

## 程式風格與解法契約

遵守 `.editorconfig`：C# 使用四空格縮排、控制流程保留大括號、明確型別、公開成員採
PascalCase、區域變數採 camelCase。保留 `Main` 的雙語題目 XML summary，以及公開解法的繁體中文
XML summary；行內註解只說明雙向集合差集或位元狀態表的高訊號不變量。

保留兩個公開 API：`FindDifference(int[] nums1, int[] nums2)` 與
`FindDifference2(int[] nums1, int[] nums2)`。兩者皆為純函式：不得修改輸入陣列、輸出主控台，
或加入題目契約外的無效輸入行為。題目保證兩個陣列長度皆為 1 至 1,000，元素值為 -1,000 至 1,000。

`FindDifference` 將兩個輸入各自放入 `HashSet<int>` 去重，再分別移除另一側出現過的值；平均時間為
O(n + m)，輔助空間與結果空間皆為 O(n + m)。`FindDifference2` 使用 `byte[2001]` 與 offset 1000，
以 bit 1／bit 2 記錄兩側；時間為 O(n + m + 2001)、輔助空間為 O(2001)、結果空間為 O(n + m)。
回傳結果固定有兩個列表；解法二目前自然升冪，但兩個 API 的契約都不保證內部元素順序。

## Harness 與 Git 範圍

`Main` 是唯一 console I/O 邊界，具有 8 個確定性 acceptance cases。每個 API 使用獨立輸入副本，
並各自驗證結果外層長度、兩側無序集合答案與兩份輸入保存，共 10 checks／案例、80 個檢查。
輸出使用穩定的 `PASS/FAIL` 行；全部成功時必須以 `Summary: 80/80 checks passed.` 結尾，
任一失敗時 exit code 必須為 1。

Git metadata 位於 parent repository 根目錄。任何 commit 或 PR 都必須只涵蓋 `leetcode_2215/`，
且此次 follow-up 僅能修改 `leetcode_2215/leetcode_2215/Program.cs`、`leetcode_2215/README.md`
與 `leetcode_2215/AGENTS.md`；不可加入測試專案、套件、第三種解法或題目契約外的行為。
