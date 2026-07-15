# leetcode_859 .NET 10 遷移設計

## 目標

將 LeetCode 859「Buddy Strings／親密字串」從舊式 .NET Framework 4.8 主控台專案遷移為可在 .NET 10 執行、可重複驗證且可完整發布的單題專案。

## 範圍與交付

- 僅修改 `leetcode_859/`；不調整其他題目或 repository 根目錄結構。
- 使用 `codex/leetcode-859-net10` 與 `/private/tmp/codex-leetcode-859-net10` 隔離工作。
- 最終交付為單一 Conventional Commit、Draft PR、已驗證的 squash merge，以及合併後對 GitHub Issue #2 唯一 `leetcode_859` 核取方塊的更新。
- 舊 `.sln`、`App.config`、`Properties/AssemblyInfo.cs` 逐檔移除，不使用批次刪除命令。

## 專案結構

題目根目錄加入 `.editorconfig`、`.gitattributes`、`.gitignore`、`.vscode/`、`AGENTS.md`、`README.md`、`docs/readme-template.md` 與 Superpowers 設計／計畫紀錄。巢狀 `leetcode_859/` 只保留 `Program.cs` 與 SDK-style `leetcode_859.csproj`，目標為 `net10.0` 並啟用 implicit usings 與 nullable。

## 演算法與 API

唯一公開 API 為 `public static bool BuddyStrings(string s, string goal)`。若長度不同直接失敗；單次掃描最多記錄兩個 mismatch，並用固定 26 格狀態追蹤是否出現重複字母。

- 字串相同時，只有存在重複字母才能交換兩個不同索引後仍維持原字串。
- 字串不同時，必須恰有兩個 mismatch，且兩端字元能交叉配對。
- 第三個 mismatch 可立即回傳 `false`。

方法不修改輸入、不寫入主控台，也不新增題目契約外的 invalid-input 行為。時間複雜度為 `O(n)`，輔助空間為 `O(1)`。舊 `buddyStrings2` 與主要解法目的及複雜度相同，且 API 命名不一致，因此不保留。

## Acceptance Harness

`Main` 擁有所有輸出，執行 10 項 deterministic checks：三個官方案例、最短輸入、長度不同、恰好一個 mismatch、合法雙 mismatch、無法交叉配對、超過兩個 mismatch，以及長度 20,000 的上限 spot check。每項顯示 Input、Expected、Actual 與 PASS/FAIL；成功 summary 固定為 `Summary: 10/10 checks passed.`，任一失敗設定非零 exit code。

## 文件、驗證與發布

README 使用繁體中文說明題意、恰好交換兩個不同索引的契約、兩種判斷分支、複雜度、逐步案例、驗收案例、實際命令與 fresh run 的唯一 `text` transcript。VS Code 設定以題目根目錄為 workspace，直接建置和偵錯巢狀專案。

舊專案 `MSB3644` 僅作 legacy baseline；有效 RED 是 harness 呼叫尚不存在的 `BuddyStrings` 所產生的單一 `CS0103`。GREEN 後執行 JSON、build、run、README transcript diff、fence 計數、whitespace、scope、legacy absence 與獨立唯讀 review。發布採單一 commit、Draft PR、Ready、expected-head squash merge；GitHub 確認 merged 後才更新 Issue #2，最後在合併後的 `main` 重跑完整門檻。

## 自審結果

- 無待填內容、未決需求或未定義介面。
- 演算法、harness、README、VS Code 路徑與發布方式互相一致。
- 所有 tracked changes 與 Issue 更新都限制在已核准的單題交付邊界。
