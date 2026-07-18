# LeetCode 1539 Binary Search Solution Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** 保留逐一枚舉解法並新增 `O(log n)` 二分搜尋解法，讓兩個 API 通過相同的
九案例三十六項驗收並完成教學文件與 follow-up 發布。

**Architecture:** `FindKthPositive2` 在索引半開區間搜尋第一個累積缺失數不少於 `k` 的
位置，最後以 `left + k` 回傳答案。`Main` 透過具名 delegates 與獨立輸入副本驗證兩個
純函式，並維持唯一 console I/O 邊界。

**Tech Stack:** C# 14、.NET 10 SDK、SDK-style `.csproj`、VS Code CoreCLR、Git。

## Global Constraints

- 所有 tracked changes 限於 `leetcode_1539/`。
- 保留 `FindKthPositive(int[] arr, int k)`，新增同簽章回傳型別的 `FindKthPositive2`。
- 兩個 API 都不輸出、不修改輸入，也不新增題目限制外行為。
- 不新增第三方套件、正式測試專案或 Issue #2 變更。
- README 使用繁體中文，fresh run 是唯一 `text` fence。
- 使用一個 follow-up commit，經 draft PR、Ready 與 expected-head squash merge 後完成
  post-merge gate。

---

### Task 1: 建立雙解法 harness 與 TDD RED

- [x] 將兩個解法表示為具名 delegates，共用九個 fixtures。
- [x] 每次執行使用獨立 clone，分別檢查答案與輸入保存。
- [x] 在 production API 尚不存在時執行 build，確認 `FindKthPositive2` 產生 `CS0103`。

### Task 2: 實作二分搜尋並取得 GREEN

- [x] 使用 `[left, right)` lower bound 與 `arr[mid] - mid - 1` 實作新 API。
- [x] 加入繁中 XML 與只說明公式、區間不變量及答案推導的關鍵註解。
- [x] 執行 build/run，確認 0 warnings、0 errors 與 `36/36`。

### Task 3: 同步文件與完整 gate

- [x] 更新 README、AGENTS、binary-search design 與 implementation plan。
- [x] 驗證 README transcript、唯一 `text` fence、JSON、whitespace、scope 與 clean status。
- [ ] 完成獨立唯讀 review並建立唯一 follow-up commit。

### Task 4: Follow-up 發布

- [ ] 推送 branch、建立 draft PR並確認 reviewed head、單一 commit及 clean merge state。
- [ ] 標記 Ready並以 expected head SHA squash merge。
- [ ] 同步 `main` 並重跑完整 gate；確認 Issue #2 未被修改。
