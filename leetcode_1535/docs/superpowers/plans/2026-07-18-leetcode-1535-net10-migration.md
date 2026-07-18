# LeetCode 1535 .NET 10 Migration Implementation Plan

**Goal:** 將 `leetcode_1535` 翻新為具純函式 one-pass champion 解法、八案例 acceptance
harness、繁體中文教學文件與完整本機驗證證據的 SDK-style .NET 10 專案。

**Architecture:** `GetWinner` 依序維護目前 champion 與其連勝數；達成 `k` 時立即回傳，
否則掃描結束的 champion 即為最大值。`Main` 是唯一 console I/O 邊界；題目根目錄承載
VS Code、README、AGENTS 與共用設定。

**Tech Stack:** C# 14、.NET 10 SDK、SDK-style `.csproj`、VS Code CoreCLR、Git。

## Global Constraints

- 所有 tracked changes 限於 `leetcode_1535/`。
- 公開 API 固定為 `public static int GetWinner(int[] arr, int k)`。
- API 不輸出、不修改輸入，也不新增題目限制外行為。
- 不新增第三方套件或正式測試專案。
- README 使用繁體中文，fresh run 是唯一 `text` fence。
- 本機只有一個功能 commit；交付接續推送 branch、建立 draft PR、標記 Ready、以 expected
  head SHA squash merge、精確更新一個 Issue #2 checkbox，並完成 post-merge gate。

---

### Task 1: 建立 SDK 專案與 TDD RED

- [x] 將巢狀 `.csproj` 改為 SDK-style `net10.0`，啟用 implicit usings 與 nullable。
- [x] 逐檔移除 `.sln`、`App.config`、`Properties/AssemblyInfo.cs`。
- [x] 建立八案例 harness，但暫不加入 production API。
- [x] 執行明確巢狀 build，確認唯一 production failure 為缺少 `GetWinner` 的 `CS0103`。

### Task 2: 加入解法並取得 GREEN

- [x] 加入 approved one-pass champion/consecutive-win 公開 API。
- [x] 執行 build，確認 0 warnings、0 errors。
- [x] 執行 harness，確認 8 個 PASS、摘要精確且 exit code 0。

### Task 3: 完成設定與教學文件

- [x] 加入共用設定、problem-root `.vscode`、`AGENTS.md` 與 README 範本。
- [x] 完成雙語題述 XML、繁中 API XML 與高訊號不變量註解。
- [x] 撰寫繁中 README，涵蓋限制、不變量、複雜度、走查、案例、命令與結構。
- [x] 以 fresh run 填入唯一 `text` transcript。

### Task 4: 交付前驗證與本機提交

- [x] 通過 JSON、build、run、transcript、fence、whitespace、scope 與 legacy checks。
- [x] 完成完整 diff 的唯讀自我審查，修正所有 Critical／Important 與規格不一致問題。
- [x] 暫存僅限 `leetcode_1535/`，驗證 cached whitespace 與 scope，並建立相對 `origin/main` 的
  單一 feature commit。

### Task 5: 完整發佈與 post-merge 驗證

- [ ] 推送單一 feature commit 並建立 draft PR。
- [ ] 完成審查後標記 Ready，確認 expected head SHA 後執行 squash merge。
- [ ] 只更新目標 Issue #2 checkbox，重新讀取確認變更正確。
- [ ] 對 merge 後的 `main` 執行 JSON、build、run、README transcript、fence、whitespace、
  scope 與 legacy-absence 完整 gate。
