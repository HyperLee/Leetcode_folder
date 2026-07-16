# leetcode_1423 .NET 10 遷移設計

## 目標與範圍

將 LeetCode 1423「Maximum Points You Can Obtain from Cards／可獲得的最大點數」從
.NET Framework 4.8 遷移為可建置、執行與驗證的 .NET 10 單題專案。所有 tracked
changes 限於 `leetcode_1423/`，並完成 PR、squash merge 與 Issue #2 更新。

## 專案與演算法

- 使用 SDK-style `net10.0` executable，啟用 implicit usings 與 nullable。
- 保留 `public static int MaxScore(int[] cardPoints, int k)`；方法不修改輸入、不輸出。
- 先計算全部點數，再以固定長度 `n-k` 的滑動視窗找出必須留下的最小總和；答案為總和減去該視窗。
- 當 `k == n` 時，視窗長度為零並直接回傳全部總和，避免零長度視窗的索引陷阱。
- 時間複雜度 `O(n)`，結果空間與輔助空間皆為 `O(1)`。

## Acceptance Harness

`Main` 是唯一 console I/O 邊界。八個案例涵蓋三個官方範例、單張牌、只取一張時
比較兩端、左右混合優於單側的回歸案例、`k == n`，以及 100,000 張牌的上限 spot
check。每項輸出案例、Input、Expected、Actual 與 PASS/FAIL；成功結尾固定為
`Summary: 8/8 checks passed.`，失敗則設定 exit code 1。

## 文件、審查與發佈

題目根目錄補齊標準設定、VS Code、`AGENTS.md`、繁中 README、README 範本與
Superpowers 文件。逐檔移除 legacy artifacts。完成 JSON、build、run、README
transcript、scope 與 whitespace gate 後進行唯讀審查，再以單一 commit 建立 PR、
squash merge、更新 Issue #2，最後於 `main` 重跑驗證。

## 自審重點

- 公開 API、只從兩端取牌的限制、零長度補集視窗與輸入不變性皆有 harness 證據。
- 大型案例只輸出摘要，避免 100,000 個元素污染 transcript。
- 文件命令以直接開啟外層 `leetcode_1423/` 為準，沒有錯誤的額外路徑層級。
