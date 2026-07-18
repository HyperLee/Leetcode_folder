# LeetCode 1493 .NET 10 Migration Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** 將 `leetcode_1493` 翻新為具純函式滑動視窗解法、十案例 acceptance harness、繁體中文教學文件與完整發布證據的 SDK-style .NET 10 專案。

**Architecture:** `LongestSubarray` 維持最多一個 0 的滑動視窗，使用視窗長度減一表達必須刪除恰好一個元素。`Main` 是唯一 console I/O 邊界；題目根目錄承載 VS Code、README、AGENTS 與共用設定。

**Tech Stack:** C# 14、.NET 10 SDK、SDK-style `.csproj`、VS Code CoreCLR、Git／GitHub CLI。

## Global Constraints

- 所有 tracked changes 限於 `leetcode_1493/`。
- 公開 API 固定為 `public static int LongestSubarray(int[] nums)`。
- API 不輸出、不修改輸入，也不新增題目限制外行為。
- 不新增第三方套件、正式測試專案或第二套 production 解法。
- README 使用繁體中文，fresh run 是唯一 `text` fence。
- 相對 `origin/main` 維持單一 commit：`feat(leetcode-1493): migrate project to .NET 10`。
- PR 合併前不得更新 Issue #2。

---

### Task 1: 建立 SDK 專案與 TDD RED

- [ ] 將巢狀 `.csproj` 改為 SDK-style `net10.0`，啟用 implicit usings 與 nullable。
- [ ] 逐檔移除 `.sln`、`App.config`、`Properties/AssemblyInfo.cs`。
- [ ] 建立十案例 harness，但暫不加入 `LongestSubarray`。
- [ ] 執行明確巢狀 build，確認唯一 production failure 為缺少 API 的 `CS0103`。

### Task 2: 加入滑動視窗並取得 GREEN

- [ ] 加入 `LongestSubarray(int[] nums)`，以 `left`、`right`、`zeroCount` 維持最多一個 0。
- [ ] 以 `right - left` 更新答案，保留必刪一個元素的語意。
- [ ] 執行 build，要求 0 warnings、0 errors。
- [ ] 執行 harness，要求 10 個 PASS、摘要精確且 exit code 0。

### Task 3: 完成設定與教學文件

- [ ] 加入共用設定、problem-root `.vscode`、`AGENTS.md` 與 README 範本。
- [ ] 完成雙語題述 XML、繁中 API XML 與高訊號不變量註解。
- [ ] 撰寫繁中 README，內容涵蓋限制、不變量、複雜度、走查、案例、命令與結構。
- [ ] 以 fresh run 填入唯一 `text` transcript。

### Task 4: 驗證、Review 與發布

- [ ] 通過 JSON、build、run、transcript、fence、whitespace、scope 與 legacy checks。
- [ ] 進行獨立唯讀 review，修正所有 Critical／Important 與規格不一致問題後重驗。
- [ ] 建立單一 commit、push、Draft PR；確認 head SHA 與 checks 後轉 Ready。
- [ ] 以已驗證 head SHA squash merge，合併後才更新 Issue #2。
- [ ] 同步 `main` 至 merge SHA，重跑完整 gate 並確認工作區乾淨。
