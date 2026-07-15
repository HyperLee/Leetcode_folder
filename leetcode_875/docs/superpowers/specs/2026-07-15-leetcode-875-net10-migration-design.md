# leetcode_875 .NET 10 遷移設計

## 目標與範圍

將 LeetCode 875「Koko Eating Bananas／愛吃香蕉的珂珂」從舊式 .NET Framework 4.8 主控台專案遷移成可在 .NET 10 建置、執行與驗證的單題專案。所有 tracked changes 僅限 `leetcode_875/`。

## 專案與 API

- 使用 SDK-style `net10.0` console project，啟用 implicit usings 與 nullable。
- 公開 API 固定為 `public static int MinEatingSpeed(int[] piles, int h)`。
- 私有 helper 固定為 `private static long CalculateRequiredHours(int[] piles, int speed)`。
- 解法在 `[1, piles.Max()]` 上執行 lower-bound binary search；可行速度保留於右界，不可行速度排除至左界之外。
- helper 使用 `((long)pile + speed - 1) / speed` 與 `long` 累加器，避免上取整及總時數溢位。
- 解法不修改輸入、不輸出，也不新增題目有效輸入契約以外的錯誤處理；移除重複的 `MinEatingSpeed2`。

## Acceptance Harness

`Main` 是唯一 console I/O 邊界，固定依序執行九項檢查：三個官方範例、單堆最小輸入、極大可用時數、每堆恰好一小時、上取整邊界、十億香蕉溢位回歸，以及一萬堆上限案例。每項輸出案例名稱、Input、Expected、Actual 與 PASS/FAIL；失敗設定 `Environment.ExitCode = 1`，全綠結尾為 `Summary: 9/9 checks passed.`。

## 文件與驗證

題目根目錄補齊共用設定、VS Code、`AGENTS.md`、繁中 `README.md` 與 README 範本；逐檔移除舊 `.sln`、`App.config`、`Properties/AssemblyInfo.cs`。先以缺少 `MinEatingSpeed` 的 `CS0103` 證明 RED，再完成 GREEN、failure-path 檢查、README transcript 精確比對及完整驗證門檻。最終維持相對 `origin/main` 的單一 commit。

## 自審

- API、九項檢查、演算法、複雜度與 commit 範圍均已鎖定。
- 無 placeholder、未決需求、第三方 dependency 或正式測試專案。
- 不修改其他題目或 repository 根目錄的 tracked files。
