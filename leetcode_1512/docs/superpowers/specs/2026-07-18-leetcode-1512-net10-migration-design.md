# LeetCode 1512 .NET 10 Migration Design

## 目標與範圍

依 repository 根目錄的 `LEETCODE_NET10_MIGRATION_SPEC.md`，將 `leetcode_1512/`
從舊式 .NET Framework 4.8 專案翻新為 SDK-style .NET 10 題目專案。所有 tracked
changes 僅限 `leetcode_1512/`；本次交付只建立單一已驗證的本機功能 commit，不推送、
不建立或合併 PR，也不更新 Issue #2。

## 演算法設計

保留兩個公開 API。`NumIdenticalPairs(int[] nums)` 以雙迴圈枚舉每個 `i < j` 的索引
配對並計數相同值，時間為 `O(n^2)`，結果與輔助空間均為 `O(1)`。
`NumIdenticalPairs2(int[] nums)` 在單次掃描時，先將該值既有出現次數加入答案，再增加
字典記錄，時間為 `O(n)`、輔助空間為 `O(k)`、結果空間為 `O(1)`。兩者皆不輸出、
不修改輸入，也不額外定義無效輸入行為。

## 專案與驗收設計

巢狀專案改為 SDK-style `net10.0`，啟用 implicit usings 與 nullable；逐檔移除舊
`.sln`、`App.config` 與 `AssemblyInfo.cs`。題目根目錄承載 VS Code、共用設定、
AGENTS、繁中 README 與文件範本。

`Main` 是唯一 console I/O 邊界，使用九個固定案例涵蓋三個官網範例、最小輸入、兩元素
配對、非相鄰重複、值邊界、混合頻率及長度 100 的上限資料。每案建立兩份獨立輸入，
並同時驗證兩個答案與輸入陣列不變；成功摘要固定為
`Summary: 9/9 checks passed.`，失敗時 exit code 為 1。

## TDD、文件與交付

舊版 baseline 在 macOS 因 .NET Framework 4.8 reference assemblies 缺失而出現
`MSB3644`。SDK 專案與完整九案例 harness 先呼叫尚未存在的兩個 API，以 `CS0103`
作為有效 RED，再加入最小演算法實作取得 GREEN。README 記錄繁中教學、複雜度、
案例與 fresh run，完整輸出是唯一 `text` fence。

本機 gate 包含 JSON、build、run、transcript diff、唯一 fence、whitespace、scope 與
legacy absence。本機自我審查完成後，建立相對 `origin/main` 的一個功能 commit。
