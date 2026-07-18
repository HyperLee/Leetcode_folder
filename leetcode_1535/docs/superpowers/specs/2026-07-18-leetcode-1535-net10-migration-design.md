# LeetCode 1535 .NET 10 Migration Design

## 目標與範圍

依 repository 根目錄的 `LEETCODE_NET10_MIGRATION_SPEC.md`，將 `leetcode_1535/`
從舊式 .NET Framework 4.8 專案翻新為 SDK-style .NET 10 題目專案。所有 tracked
changes 僅限 `leetcode_1535/`。完成本機單一功能 commit 後，交付包含推送 branch、建立
draft PR、完成審查後標記 Ready、以預期 head SHA squash merge、精確更新 Issue #2 的一個
checkbox，並在 merge 後重新執行完整 gate。

## 演算法設計

唯一公開 API `GetWinner(int[] arr, int k)` 使用 approved one-pass champion 與連勝計數。
掃描時由目前 champion 對每個新值比較；若 champion 較大則增加連勝，反之以新值取代
champion 並把連勝設為一。當連勝到達 `k` 即回傳；若掃描完成仍未到達，champion 已是全域
最大值，回傳它。時間為 `O(n)`，結果與輔助空間皆為 `O(1)`；函式不輸出也不改寫輸入。

## 專案與驗收設計

巢狀專案改為 SDK-style `net10.0`，啟用 implicit usings 與 nullable；逐檔移除舊
`.sln`、`App.config` 與 `AssemblyInfo.cs`。題目根目錄承載 VS Code、共用設定、AGENTS、
繁中 README 與文件範本。

`Main` 是唯一 console I/O 邊界，使用八個固定案例，涵蓋官方範例、`k` 超出陣列回合、
長連勝、十億級 `k`、兩元素、既有 champion、值上限與長度 100,000。每案檢查預期答案
與輸入內容不變；成功摘要固定為 `Summary: 8/8 checks passed.`，失敗時 exit code 為 1。

## TDD、文件與交付

舊版 baseline 在 macOS 因 .NET Framework 4.8 reference assemblies 缺失而出現
`MSB3644`。SDK 專案與完整八案例 harness 先呼叫尚未存在的 `GetWinner`，以 `CS0103`
作為有效 RED，再加入最小一次掃描實作取得 GREEN。README 記錄繁中教學、複雜度、案例與
fresh run，完整輸出是唯一 `text` fence。

本機 gate 包含 JSON、build、run、transcript、fence、whitespace、scope 與 legacy
absence。本機自我審查完成後，建立相對 `origin/main` 的一個功能 commit，接著以該 commit
的 expected head SHA 推進 draft PR、Ready、squash merge、Issue #2 readback 與 post-merge
verification；任何 publish 前 base 變動都必須重新整合並重跑 gate。
