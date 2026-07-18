# LeetCode 1539 .NET 10 Migration Design

## 目標與範圍

依 repository 根目錄的 `LEETCODE_NET10_MIGRATION_SPEC.md`，將 `leetcode_1539/`
從舊式 .NET Framework 4.8 專案翻新為 SDK-style .NET 10 題目專案。所有 tracked
changes 僅限 `leetcode_1539/`。交付包含單一功能 commit、draft PR、審查後標記 Ready、
以預期 head SHA squash merge、精確更新 Issue #2 的一個 checkbox，並在 merge 後重新執行
完整 gate。

## 演算法設計

唯一公開 API `FindKthPositive(int[] arr, int k)` 保留核准的逐一枚舉解法。`current`
從 1 逐步增加；當它等於目前陣列元素時只推進 `arrayIndex`，否則增加 `missingCount`。
找到第 `k` 個缺失數時回傳剛處理的 `current`。時間為 `O(n + k)`，結果與輔助空間皆為
`O(1)`；函式不輸出、不改寫輸入，也不新增題目有效輸入以外的行為。

## 專案與驗收設計

巢狀專案改為 SDK-style `net10.0`，啟用 implicit usings 與 nullable；逐檔移除舊
`.sln`、`App.config` 與 `AssemblyInfo.cs`。題目根目錄承載 VS Code、canonical 共用
設定、AGENTS、繁中 README 與文件範本。

`Main` 是唯一 console I/O 邊界，使用九個固定案例，涵蓋兩個官方範例、最小值、第一個
元素前、陣列內與陣列後的缺口、最大 `k`、最大首元素及長度 1,000。每案分別檢查答案與
輸入內容不變，共十八項檢查；成功摘要固定為 `Summary: 18/18 checks passed.`，失敗時
exit code 為 1。

## TDD、文件與交付

舊版 baseline 在 macOS 因 .NET Framework 4.8 reference assemblies 缺失而出現
`MSB3644`。SDK 專案與完整 harness 先呼叫尚未存在的 `FindKthPositive`，以 `CS0103`
作為有效 RED，再加入最小枚舉實作取得 GREEN。README 記錄繁中教學、複雜度、案例與
fresh run，完整輸出是唯一 `text` fence。

本機 gate 包含 JSON、build、run、transcript、fence、canonical equality、whitespace、
scope 與 legacy absence。獨立唯讀 review 通過後建立相對 `origin/main` 的一個功能
commit，接著以該 commit 的 expected head SHA 推進 draft PR、Ready、squash merge、
Issue #2 readback 與 post-merge verification；任何發布前 base 變動都必須重新整合並重跑
完整 gate。
