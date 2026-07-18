# LeetCode 1481 .NET 10 Migration Design

## 目標與範圍

依 repository 根目錄的 `LEETCODE_NET10_MIGRATION_SPEC.md`，將 `leetcode_1481/`
從舊式 .NET Framework 4.8 專案完整翻新為可建置、可執行、可教學且可發布的
SDK-style .NET 10 題目專案。所有 tracked changes 僅限 `leetcode_1481/`；不修改其他題目。

交付包含隔離分支、單一功能 commit、Draft PR、獨立唯讀 review、Ready PR、squash merge、
Issue #2 的單一核取方塊更新，以及合併後驗證。

## 既有狀態

- 公開 API 為 `public static int FindLeastNumOfUniqueInts(int[] arr, int k)`。
- 舊解法已採用「統計頻率後優先移除低頻整數」的正確核心概念，應保留其教學價值。
- 舊實作在列舉 `Dictionary` 時移除元素，存在執行期例外風險。
- 舊專案為非 SDK-style `.NET Framework 4.8`；macOS baseline build 以 `MSB3644` 失敗。
- 題目沒有正式測試專案；`Main` acceptance harness 是可執行驗證來源。

## 演算法設計

採用頻率統計加升冪排序：

1. 以 `Dictionary<int, int>` 統計每個整數的出現次數。
2. 將所有頻率升冪排序。
3. 以相異整數數量初始化 `remainingUniqueCount`。
4. 依序處理最低頻率；只有當 `frequency <= k` 時才扣除整組頻率並將答案減一。
5. 第一個無法完整移除的頻率出現時立即停止，因後續頻率不會更小。

公開 API 不輸出、不修改 `arr`，並假設輸入符合 LeetCode 約束，不新增額外的
invalid-input 例外。令 `n` 為陣列長度、`u` 為相異整數數量，時間複雜度為
`O(n + u log u)`，結果空間為 `O(1)`，輔助空間為 `O(u)`。

## 專案與元件設計

- `leetcode_1481/leetcode_1481/leetcode_1481.csproj` 改為 SDK-style `net10.0`，啟用
  implicit usings 與 nullable。
- `leetcode_1481/leetcode_1481/Program.cs` 包含純函式公開 API 與由 `Main` 控制的
  確定性 acceptance harness；所有 console I/O 僅存在於 `Main`。
- `.editorconfig`、`.gitattributes`、`.gitignore`、`.vscode/`、`AGENTS.md`、`README.md`
  與 `docs/readme-template.md` 位於題目根目錄，將 `leetcode_1481/` 直接開啟為 VS Code
  workspace 時可立即建置與除錯。
- 逐檔移除舊 `.sln`、`App.config` 與 `Properties/AssemblyInfo.cs`。

## Acceptance Harness 設計

Harness 使用九個確定性案例：

1. 官方範例 `[5, 5, 4]`, `k = 1`，答案 `1`。
2. 官方範例 `[4, 3, 1, 1, 3, 3, 2]`, `k = 3`，答案 `2`。
3. 最小輸入 `[1]`, `k = 0`，答案 `1`。
4. 單元素全部移除 `[1]`, `k = 1`，答案 `0`。
5. `k = 0` 時保留全部相異值。
6. `k` 不足以完整移除下一個頻率組時，不得減少 unique count。
7. 多組相同頻率時，排序 tie 不得影響答案。
8. 高數值與重複值案例，確認結果只取決於頻率。
9. 長度 `100000` 的上限 spot check，不輸出整份巨大陣列。

每個案例在呼叫 API 前複製輸入；只有答案正確且輸入完整未變時才是 PASS。每項輸出案例、
Input、Expected、Actual 與 PASS／FAIL，最後精確輸出 `Summary: 9/9 checks passed.`；若有任何
失敗，`Main` 設定 `Environment.ExitCode = 1`。

## TDD 與錯誤處理

先完成可編譯的 SDK 專案與 acceptance harness，但讓 production API 尚未存在。以明確巢狀
`.csproj` 執行 build，預期得到因缺少 `FindLeastNumOfUniqueInts` 所造成的 `CS0103`，作為有效
RED。接著加入最小正確實作，使用相同 build/run 路徑取得 GREEN，再整理命名、文件與輸出。

公開 API 不處理題目限制外輸入。驗證器只將不符合預期或輸入遭修改視為 FAIL，並以非零 process
exit code 回報；不引入測試框架或第三方套件。

## 文件設計

`README.md` 使用繁體中文，包含雙語題名、官方英文與中文連結、題意、限制、核心不變量、
常見陷阱、演算法與複雜度、逐步範例、九案例表、實際 build/run 命令、fresh run 完整輸出與
最終結構。完整執行輸出是唯一的 `text` fence，內容必須與 fresh run 精確一致。

`Main` 的 XML summary 保留並補足雙語題名、雙語題述與官方網址。公開 API 的繁體中文 XML
summary 說明用途、演算法、有效輸入與回傳結果；行內註解只解釋「優先完整移除最低頻率群組」
這項核心不變量。

## 發布與驗證設計

本機 gate 包含：

- `jq empty` 驗證兩個 VS Code JSON。
- 明確 nested `.csproj` 的 `dotnet build --nologo`，要求 0 warnings、0 errors。
- `dotnet run --no-build`，要求九項全數 PASS 且 exit code 0。
- README transcript 與 fresh run 的精確 diff，以及唯一 `text` fence 檢查。
- `git diff --check`、changed-path scope、legacy absence 與 SDK-style 契約檢查。
- 一次獨立唯讀 review；Critical／Important 問題必須修正並重驗。

通過 gate 後維持相對 `origin/main` 單一 Conventional Commit：
`feat(leetcode-1481): migrate project to .NET 10`。推送並建立含 `Refs #2` 的 Draft PR，確認
head SHA、changed files、checks 與 clean mergeability 後轉 Ready，再以 expected head SHA
squash merge。GitHub 確認 merged 與 merge SHA 後，僅將 Issue #2 的 `leetcode_1481` 改為
已勾選，讀回確認 `leetcode_1493` 仍未勾選。最後同步本機 `main` 至 merge SHA，重跑完整驗證並
確認工作區乾淨。
