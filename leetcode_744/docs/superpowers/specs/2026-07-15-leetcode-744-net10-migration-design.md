# leetcode_744 .NET 10 遷移設計

## 目標

將 LeetCode 744「Find Smallest Letter Greater Than Target／尋找比目標字母大的最小字母」從舊式 .NET Framework 4.8 主控台專案遷移為可在 .NET 10 執行、可重複驗證且可完整發布的單題專案。

## 範圍與交付

- 僅修改 `leetcode_744/`；不調整其他題目或 repository 根目錄結構。
- 使用 `codex/leetcode-744-net10` 與 `/private/tmp/codex-leetcode-744-net10` 隔離工作。
- 最終交付為一個 Conventional Commit、Draft PR、已驗證的 squash merge，以及合併後對 GitHub Issue #2 的唯一 `leetcode_744` 核取方塊更新。
- 舊 `.sln`、`App.config`、`Properties/AssemblyInfo.cs` 會逐檔移除；不使用批次刪除命令。

## 專案結構

題目根目錄新增 `.editorconfig`、`.gitattributes`、`.gitignore`、`.vscode/`、`AGENTS.md`、`README.md`、`docs/readme-template.md` 與本設計紀錄。巢狀 `leetcode_744/` 保留 `Program.cs`，並以 SDK-style `leetcode_744.csproj` 設為 `net10.0`、啟用 implicit usings 與 nullable。

## 演算法與 API

保留公開 API `public static char NextGreatestLetter(char[] letters, char target)`。`letters` 依題目契約為遞增排列的小寫字元，方法以 lower-bound 二分搜尋找第一個嚴格大於 `target` 的位置：

1. 搜尋區間是完整的索引範圍。
2. 若中點字元大於 `target`，保留左半部以尋找更小的合法答案。
3. 否則捨棄該中點及左半部。
4. 搜尋結束後，若索引等於長度，代表答案依題意環繞為第一個字元。

此設計不修改輸入、不新增無效輸入例外、不寫入主控台；時間複雜度為 `O(log n)`，額外空間為 `O(1)`。

## Acceptance Harness

`Main` 只負責輸出，逐項顯示案例、輸入、Expected、Actual 與 PASS/FAIL。案例包含官方範例、最小有效輸入、目標小於首字元、目標等於或大於尾字元時的環繞、重複字元，以及長度 10,000 的 spot checks。所有檢查通過時輸出精確 summary；任一失敗會設定非零 exit code。

## 文件與設定

`Program.cs` 的 `Main` 保留雙語題名、官方中英文連結與雙語題述 XML summary；公開 API 與核心二分 helper（若需要）使用繁體中文 XML summary，且只保留說明 lower-bound 收斂或環繞理由的高訊號註解。

README 使用繁體中文說明題意、限制、二分搜尋不變量、逐步案例、複雜度、acceptance cases、實際 build/run 命令與由 fresh run 複製的唯一 `text` transcript。VS Code 從題目根目錄直接建置和偵錯巢狀專案。

## 驗證與發布

有效 RED 將在 .NET 10 專案與 harness 就緒後，以尚未存在的 `NextGreatestLetter` 編譯錯誤證明；舊專案的 `MSB3644` 僅記錄為 legacy baseline。GREEN 後執行 JSON、build、run、README transcript diff、text fence 計數、whitespace、scope 與 legacy absence 檢查，並進行獨立唯讀 review。

發布前將本設計紀錄與所有遷移內容維持為相對 `origin/main` 的單一 commit。推送後建立 Draft PR，核對已驗證 head SHA、changed files、檢查狀態與可合併性，才轉 Ready 並以 expected head SHA squash merge。GitHub 確認 merged 後，才更新並讀回 Issue #2，最後在合併後的 `main` 重跑驗證。

## 自審結果

- 無 placeholder、未決需求或未定義的 API。
- 範圍限制為 `leetcode_744/`，發布流程不會勾選其他 Issue 項目。
- 演算法、harness、README、設定與驗證方式彼此一致。
