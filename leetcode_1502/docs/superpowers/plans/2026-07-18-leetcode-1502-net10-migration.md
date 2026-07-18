# LeetCode 1502 .NET 10 Migration Implementation Plan

**Goal:** 將 `leetcode_1502` 翻新為具純函式排序解法、十案例 acceptance harness、
繁體中文教學文件與完整本機驗證證據的 SDK-style .NET 10 專案。

**Architecture:** `CanMakeArithmeticProgression` 複製輸入並只排序複本，然後以第一個
相鄰差驗證全部相鄰差。`Main` 是唯一 console I/O 邊界；題目根目錄承載 VS Code、
README、AGENTS 與共用設定。

**Tech Stack:** C# 14、.NET 10 SDK、SDK-style `.csproj`、VS Code CoreCLR、Git。

## Global Constraints

- 所有 tracked changes 限於 `leetcode_1502/`。
- 公開 API 固定為 `public static bool CanMakeArithmeticProgression(int[] arr)`。
- API 不輸出、不修改輸入，也不新增題目限制外行為。
- 不新增第三方套件、正式測試專案或第二套 production 解法。
- README 使用繁體中文，fresh run 是唯一 `text` fence。
- 不在本實作階段 commit、push、建立 PR 或更新 Issue #2。

---

### Task 1: 建立 SDK 專案與 TDD RED

- [x] 將巢狀 `.csproj` 改為 SDK-style `net10.0`，啟用 implicit usings 與 nullable。
- [x] 逐檔移除 `.sln`、`App.config`、`Properties/AssemblyInfo.cs`。
- [x] 建立十案例 harness，但暫不加入 `CanMakeArithmeticProgression`。
- [x] 執行明確巢狀 build，確認唯一 production failure 為缺少 API 的 `CS0103`。

### Task 2: 加入排序驗證並取得 GREEN

- [x] 加入 `CanMakeArithmeticProgression(int[] arr)`，複製輸入後只排序複本。
- [x] 將每一個排序後相鄰差與第一個公差比較。
- [x] 執行 build，要求 0 warnings、0 errors。
- [x] 執行 harness，要求 10 個 PASS、摘要精確且 exit code 0。

### Task 3: 完成設定與教學文件

- [x] 加入共用設定、problem-root `.vscode`、`AGENTS.md` 與 README 範本。
- [x] 完成雙語題述 XML、繁中 API XML 與高訊號不變量註解。
- [x] 撰寫繁中 README，內容涵蓋限制、不變量、複雜度、走查、案例、命令與結構。
- [x] 以 fresh run 填入唯一 `text` transcript。

### Task 4: 交付前驗證與控制端工作

- [x] 通過 JSON、build、run、transcript、fence、whitespace、scope 與 legacy checks。
- [ ] 由控制端安排獨立唯讀 review，修正所有 Critical／Important 與規格不一致問題後重驗。
- [ ] 由控制端建立單一 commit、push、Draft PR、squash merge、更新 Issue #2，並完成合併後驗證。
