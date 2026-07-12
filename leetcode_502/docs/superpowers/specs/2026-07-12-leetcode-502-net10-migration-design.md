# LeetCode 502 .NET 10 遷移設計

## 目標

將 `leetcode_502` 從 .NET Framework 4.8 舊式專案完整遷移為可直接執行的
SDK-style .NET 10 主控台專案，保留 LeetCode 公開 API
`FindMaximizedCapital(int k, int w, int[] profits, int[] capital)`，並依
Issue #2 的完整發佈流程交付。

## 決策與替代方案

採用「依啟動資金遞增排序，加上可執行專案的最大堆」方案。每輪先將
`capital <= currentCapital` 的專案加入以 profit 排序的最大堆，再取出最高獲利
專案，最多重複 `k` 次。這能在輸入未排序時仍正確選擇，時間複雜度為
`O(n log n)`，空間複雜度為 `O(n)`。

未採用每輪掃描所有未選專案的 `O(kn)` 方案，因其不適用於
`n` 與 `k` 可達 100000 的限制。也不保留舊 Dictionary 排序方案，因它假設輸入
排序，且在沒有可啟動專案時錯誤地回傳 `0`，而非原有資金 `w`。

## 專案與程式設計

題目根目錄新增 `.editorconfig`、`.gitattributes`、`.gitignore`、`.vscode/`、
`AGENTS.md`、`README.md` 與 `docs/readme-template.md`。VS Code 設定假設直接以
`leetcode_502/` 作為 workspace，並建置與啟動巢狀
`leetcode_502/leetcode_502.csproj`。

巢狀專案改為啟用 `ImplicitUsings` 與 nullable 的 `net10.0` SDK-style executable。
逐一移除舊 `.sln`、`App.config` 與 `Properties/AssemblyInfo.cs`。

`FindMaximizedCapital` 保持純函式：建立 `(requiredCapital, profit)` 配對並依
required capital 遞增排序；掃描指標只前進一次。`PriorityQueue<int, int>` 以負的
profit 作為 priority 形成最大堆。當堆為空時立即停止，並回傳目前資金，絕不產生
console side effect。

`Main` 是唯一的 console I/O 邊界，使用資料導向的案例結果渲染 Expected、Actual 與
PASS/FAIL。所有主要非 `Main` 函式都會加入繁體中文 XML summary；註解只說明可啟動
集合與最大堆選擇的演算法不變量。

## Acceptance 與 TDD

先在完成 SDK 專案骨架後建立 acceptance harness，讓它呼叫尚未存在的
`FindMaximizedCapital`，以明確的 `CS0103` 作為有效 RED；舊專案的 `MSB3644` 僅記錄
為 legacy baseline。最小實作加入後，同一個 build/run 命令必須得到 0 warnings、
0 errors 及全部 PASS。

Harness 固定覆蓋：兩個官方範例、單一可啟動專案（`Single affordable project`）、初始資金不足、同時可執行時取最高 profit、
完成一案後解鎖更高 profit、`k` 大於專案數、零 profit，以及 `n = 100000` 的
spot check。任何失敗都設定 `Environment.ExitCode = 1`。

## 文件、驗證與交付

README 使用繁體中文，說明題意、限制、堆的不變量、複雜度、逐步例子與所有 acceptance
案例。其唯一 `text` code fence 由 fresh `dotnet run --no-build` 輸出取得並以 `diff -u`
精確驗證。

本題完成後執行 JSON、build、run、README transcript、whitespace、scope 與 legacy
absence 檢查，接著進行獨立唯讀 review。發佈採單一
`feat(leetcode-502): migrate project to .NET 10` commit、Draft PR、驗證後 Ready、帶
expected head SHA 的 squash merge；只有 GitHub 回報合併成功後，才將 Issue #2 中唯一的
`leetcode_502` 核取項目改為完成並讀回確認下一題仍未勾選。
