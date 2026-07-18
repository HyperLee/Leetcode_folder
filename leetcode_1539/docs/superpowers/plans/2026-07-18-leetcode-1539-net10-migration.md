# LeetCode 1539 .NET 10 Migration Implementation Plan

**Goal:** 將 `leetcode_1539` 翻新為具純函式逐一枚舉解法、九案例十八檢查 acceptance
harness、繁體中文教學文件與完整發布證據的 SDK-style .NET 10 專案。

**Architecture:** `FindKthPositive` 以正整數候選值、前向陣列索引與缺失計數器找出第
`k` 個缺失值。`Main` 是唯一 console I/O 邊界；題目根目錄承載 VS Code、README、
AGENTS 與 canonical 共用設定。

**Tech Stack:** C# 14、.NET 10 SDK、SDK-style `.csproj`、VS Code CoreCLR、Git。

## Global Constraints

- 所有 tracked changes 限於 `leetcode_1539/`。
- 公開 API 固定為 `public static int FindKthPositive(int[] arr, int k)`。
- API 不輸出、不修改輸入，也不新增題目限制外行為。
- 不新增第三方套件或正式測試專案。
- README 使用繁體中文，fresh run 是唯一 `text` fence。
- 本機只有一個功能 commit；交付接續推送 branch、建立 draft PR、標記 Ready、以 expected
  head SHA squash merge、精確更新一個 Issue #2 checkbox，並完成 post-merge gate。

---

### Task 1: 建立 SDK 專案與 TDD RED

- [x] 將巢狀 `.csproj` 改為 SDK-style `net10.0`，啟用 implicit usings 與 nullable。
- [x] 逐檔移除 `.sln`、`App.config`、`Properties/AssemblyInfo.cs`。
- [x] 建立九案例十八檢查 harness，但暫不加入 production API。
- [x] 執行明確巢狀 build，確認唯一 production failure 為缺少 `FindKthPositive` 的
  `CS0103`。

### Task 2: 加入解法並取得 GREEN

- [x] 加入 approved sequential-enumeration 公開 API。
- [x] 執行 build，確認 0 warnings、0 errors。
- [x] 執行 harness，確認十八項 PASS、摘要精確且 exit code 0。

### Task 3: 完成設定與教學文件

- [x] 加入 canonical 共用設定、problem-root `.vscode`、`AGENTS.md` 與 README 範本。
- [x] 完成雙語題述 XML、繁中 API XML 與高訊號不變量註解。
- [x] 撰寫繁中 README，涵蓋限制、不變量、複雜度、走查、案例、命令與結構。
- [x] 以 fresh run 填入唯一 `text` transcript。

### Task 4: 交付前驗證與本機提交

- [x] 通過 JSON、build、run、transcript、fence、canonical、whitespace、scope 與 legacy
  checks。
- [ ] 完成完整 diff 的獨立唯讀審查，修正所有 Critical／Important 與規格不一致問題。
- [ ] 暫存僅限 `leetcode_1539/`，驗證 cached whitespace 與 scope，並建立相對
  `origin/main` 的單一 feature commit。

### Task 5: 完整發佈與 post-merge 驗證

- [ ] 推送單一 feature commit 並建立 draft PR。
- [ ] 完成審查後標記 Ready，確認 expected head SHA 後執行 squash merge。
- [ ] 只更新目標 Issue #2 checkbox，重新讀取確認變更正確。
- [ ] 對 merge 後的 `main` 執行 JSON、build、run、README transcript、fence、whitespace、
  scope、canonical 與 legacy-absence 完整 gate。
