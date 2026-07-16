# leetcode_1372 .NET 10 遷移設計

## 目標與範圍

將 LeetCode 1372「Longest ZigZag Path in a Binary Tree／二元樹中的最長交錯路徑」
從 .NET Framework 4.8 遷移為可建置、執行與驗證的 .NET 10 單題專案。所有 tracked
changes 限於 `leetcode_1372/`，並完成 PR、squash merge 與 Issue #2 更新。

## 專案與演算法

- 使用 SDK-style `net10.0` executable，啟用 implicit usings 與 nullable。
- 公開 API 為 `public int LongestZigZag(TreeNode? root)`；不修改輸入、不輸出、不保存跨呼叫狀態。
- 迭代 DFS 狀態保存目前節點、上一條邊方向與長度；交替時加一，同方向時重設為一。
- 時間複雜度 `O(n)`，結果空間 `O(1)`，輔助空間最壞 `O(h)`。
- 不保留遞迴替代解法，避免 50,000 節點深鏈的呼叫堆疊風險。

## Acceptance Harness

`Main` 是唯一 console I/O 邊界。九個案例涵蓋兩個官方範例、單節點、左右第一步、
root 下方最佳起點、50,000 節點同方向鏈、50,000 節點交錯鏈與同一實體狀態隔離。
每項輸出 Expected、Actual 與 PASS/FAIL；成功結尾固定為
`Summary: 9/9 checks passed.`，失敗則設定 exit code 1。

## 文件、審查與發佈

題目根目錄補齊標準設定、VS Code、`AGENTS.md`、繁中 README、README 範本與
Superpowers 文件。逐檔移除 legacy artifacts。完成 JSON、build、run、README
transcript、scope 與 whitespace gate 後進行唯讀審查，再以單一 commit 建立 PR、
squash merge、更新 Issue #2，最後於 `main` 重跑驗證。

## 自審重點

- API、edge-count 定義、任意起點與方向重設皆有 harness 證據。
- 大型案例不展開輸入，且不依賴遞迴或跨呼叫欄位。
- 文件命令以直接開啟外層 `leetcode_1372/` 為準，沒有錯誤的額外路徑層級。
