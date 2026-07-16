# leetcode_1351 .NET 10 遷移設計

## 目標與範圍

將 LeetCode 1351「Count Negative Numbers in a Sorted Matrix／統計有序矩陣中的負數」從舊式 .NET Framework 4.8 主控台專案遷移為可在 .NET 10 建置、執行與驗證的單題專案。所有 tracked changes 僅限 `leetcode_1351/`；本階段交付一個相對 `origin/main` 的 publish-ready 本地 commit，不包含 push、PR、合併或 Issue #2 更新。

## 專案與演算法

- 使用 SDK-style `net10.0` executable project，啟用 implicit usings 與 nullable。
- 公開 API 僅保留 `public static int CountNegatives(int[][] grid)`。
- 從矩陣右上角開始。遇到負數時，利用欄的非遞增順序一次加入 `grid.Length - row` 並往左；遇到非負數時，利用列的非遞增順序往下。
- 解法不修改輸入、不輸出 console 文字，也不加入 LeetCode 有效輸入以外的 null 或 invalid-input 行為。
- 時間複雜度為 `O(m+n)`，結果空間與輔助空間皆為 `O(1)`。

## Acceptance Harness

`Main` 是唯一 console I/O 邊界。它依序執行四個官方範例、最小非負數、非方形回歸案例，以及兩個 `100x100` 題目上限案例。每個案例輸出 Case、Input、Expected、Actual 與 PASS/FAIL；大型案例只顯示矩陣尺寸與固定填充值。任一失敗設定 `Environment.ExitCode = 1`，成功結尾固定為 `Summary: 8/8 checks passed.`。

## 文件與驗證

題目根目錄補齊共用 Git/editor 設定、無提示 VS Code build/debug、`AGENTS.md`、繁中 `README.md`、README 範本與 Superpowers 文件。逐檔移除舊 `.sln`、`App.config` 與 `Properties/AssemblyInfo.cs`。先以完整 harness 呼叫不存在的 `CountNegatives` 觀察 `CS0103` RED，再加入階梯式掃描取得 GREEN；最終驗證 JSON、build、run、README transcript、唯一 `text` fence、whitespace、scope 與 legacy absence。

## 自審重點

- 公開 API、純函式、右上角階梯不變量與複雜度均有程式和文件證據。
- 非方形案例能抓出誤用欄數批次計數的實作；大型案例不傾印完整矩陣。
- 文件沒有 placeholder、第三方 dependency、正式測試專案宣稱或目標資料夾外變更。
