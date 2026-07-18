# LeetCode 1493 .NET 10 Migration Design

## 目標與範圍

依 repository 根目錄的 `LEETCODE_NET10_MIGRATION_SPEC.md`，將 `leetcode_1493/`
從舊式 .NET Framework 4.8 專案翻新為 SDK-style .NET 10 題目專案。所有 tracked
changes 僅限 `leetcode_1493/`，並完成單一 commit、PR、squash merge、Issue #2 與
合併後驗證。

## 演算法設計

保留既有滑動視窗解法與 `public static int LongestSubarray(int[] nums)` API。視窗內
最多保留一個 `0`；當第二個 `0` 進入時移動左指標直到不變量恢復。候選答案使用
`right - left`，等於視窗元素數減去題目要求必刪的一個元素，因此同時涵蓋刪除 `0`
及全為 `1` 時刪除一個 `1`。時間複雜度 `O(n)`、輔助空間 `O(1)`，不修改輸入。

## 專案與驗收設計

巢狀專案改為 SDK-style `net10.0`，啟用 implicit usings 與 nullable；逐檔移除舊
`.sln`、`App.config` 與 `AssemblyInfo.cs`。題目根目錄承載 VS Code、共用設定、
AGENTS、繁中 README 與文件範本。

`Main` 是唯一 console I/O 邊界，以十個案例涵蓋三個官方範例、最小輸入、全零、
邊界零、合併兩段 1、連續零收縮及長度 100000 spot check。每案同時驗證答案與
輸入不變；成功摘要固定為 `Summary: 10/10 checks passed.`，失敗時 exit code 為 1。

## TDD、文件與發布

legacy baseline 預期為 `MSB3644`。SDK 專案與 harness 先呼叫尚未存在的 API，以
`CS0103` 作為有效 RED，再加入最小滑動視窗實作取得 GREEN。README 記錄繁中教學、
複雜度、案例與 fresh run，完整輸出是唯一 `text` fence。

本機 gate 包含 JSON、build、run、transcript diff、唯一 fence、whitespace、scope 與
legacy absence。獨立唯讀 review 無 Critical／Important 問題後，以單一 commit 發佈
Draft PR，確認 head SHA、checks 與 mergeability，轉 Ready 並 squash merge。只有 GitHub
確認合併後才更新 Issue #2，最後同步並重驗 `main`。
