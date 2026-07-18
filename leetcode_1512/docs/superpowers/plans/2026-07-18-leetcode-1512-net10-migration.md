# LeetCode 1512 .NET 10 Migration Implementation Plan

**Goal:** 將 `leetcode_1512` 翻新為具兩種純函式計數解法、九案例 acceptance harness、
繁體中文教學文件與完整本機驗證證據的 SDK-style .NET 10 專案。

**Architecture:** `NumIdenticalPairs` 以雙迴圈枚舉所有 `i < j` 配對；
`NumIdenticalPairs2` 以字典累積先前出現次數。`Main` 是唯一 console I/O 邊界；
題目根目錄承載 VS Code、README、AGENTS 與共用設定。

**Tech Stack:** C# 14、.NET 10 SDK、SDK-style `.csproj`、VS Code CoreCLR、Git。

## Global Constraints

- 所有 tracked changes 限於 `leetcode_1512/`。
- 公開 API 固定為 `public static int NumIdenticalPairs(int[] nums)` 與
  `public static int NumIdenticalPairs2(int[] nums)`。
- API 不輸出、不修改輸入，也不新增題目限制外行為。
- 不新增第三方套件或正式測試專案。
- README 使用繁體中文，fresh run 是唯一 `text` fence。
- 本次只建立一個本機 commit；不推送、不建立或合併 PR，也不更新 Issue #2。

---

### Task 1: 建立 SDK 專案與 TDD RED

- [x] 將巢狀 `.csproj` 改為 SDK-style `net10.0`，啟用 implicit usings 與 nullable。
- [x] 逐檔移除 `.sln`、`App.config`、`Properties/AssemblyInfo.cs`。
- [x] 建立九案例 harness，但暫不加入兩個 production API。
- [x] 執行明確巢狀 build，確認唯一 production failure 為缺少 API 的 `CS0103`。

### Task 2: 加入解法並取得 GREEN

- [x] 加入兩個公開 API；雙迴圈版枚舉配對，字典版先累加舊次數再遞增。
- [x] 執行 build，要求 0 warnings、0 errors。
- [x] 執行 harness，要求 9 個 PASS、摘要精確且 exit code 0。

### Task 3: 完成設定與教學文件

- [x] 加入共用設定、problem-root `.vscode`、`AGENTS.md` 與 README 範本。
- [x] 完成雙語題述 XML、繁中 API XML 與高訊號不變量註解。
- [x] 撰寫繁中 README，內容涵蓋限制、不變量、複雜度、走查、案例、命令與結構。
- [x] 以 fresh run 填入唯一 `text` transcript。

### Task 4: 交付前驗證與本機提交

- [x] 通過 JSON、build、run、transcript、fence、whitespace、scope 與 legacy checks。
- [x] 完成完整 diff 的唯讀自我審查，修正所有 Critical／Important 與規格不一致問題。
- [x] 建立相對 `origin/main` 的單一 feature commit。
