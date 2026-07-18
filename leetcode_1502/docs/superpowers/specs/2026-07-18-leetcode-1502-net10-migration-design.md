# LeetCode 1502 .NET 10 Migration Design

## 目標與範圍

依 repository 根目錄的 `LEETCODE_NET10_MIGRATION_SPEC.md`，將 `leetcode_1502/`
從舊式 .NET Framework 4.8 專案翻新為 SDK-style .NET 10 題目專案。所有 tracked
changes 僅限 `leetcode_1502/`；commit、PR、merge 與 Issue #2 更新由控制端在完成
本機驗證與審查後處理。

## 演算法設計

保留 `public static bool CanMakeArithmeticProgression(int[] arr)` API，改為先複製
`arr` 再排序複本。排序後第一對元素的差即為候選公差；每一對後續相鄰元素都必須
具有相同差，否則無法形成等差數列。此作法不修改輸入、沒有 console side effect，
時間複雜度為 `O(n log n)`，輔助空間為 `O(n)`，結果空間為 `O(1)`。

## 專案與驗收設計

巢狀專案改為 SDK-style `net10.0`，啟用 implicit usings 與 nullable；逐檔移除舊
`.sln`、`App.config` 與 `AssemblyInfo.cs`。題目根目錄承載 VS Code、共用設定、
AGENTS、繁中 README 與文件範本。

`Main` 是唯一 console I/O 邊界，以十個固定案例涵蓋兩個官網範例、最小長度、零
公差、負數、跨越零、尾端破壞、重複值破壞、數值邊界與長度 1000 的遞減資料。每案
同時驗證答案與輸入不變；成功摘要固定為 `Summary: 10/10 checks passed.`，失敗時
exit code 為 1。

## TDD、文件與交付

舊版 baseline 預期在 macOS 因 .NET Framework 4.8 reference assemblies 缺失而出現
`MSB3644`。SDK 專案與十案例 harness 先呼叫尚未存在的 API，以 `CS0103` 作為有效
RED，再加入最小複製加排序實作取得 GREEN。README 記錄繁中教學、複雜度、案例與
fresh run，完整輸出是唯一 `text` fence。

本機 gate 包含 JSON、build、run、transcript diff、唯一 fence、whitespace、scope 與
legacy absence。控制端應在獨立唯讀 review 無 Critical／Important 問題後，依規格
完成單一 commit、PR、squash merge、Issue #2 readback 與合併後驗證。
