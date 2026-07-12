# leetcode_590 .NET 10 Migration Design

## Goal

依 `LEETCODE_NET10_MIGRATION_SPEC.md` 將 `leetcode_590` 從 .NET Framework 4.8
舊式專案遷移為 SDK-style .NET 10 console project，保留 N 元樹後序遞迴 DFS
解法，並以可重複執行的 acceptance harness 驗證題目契約。

## Approved Design

- 使用題目根目錄 `leetcode_590/` 作為 README、AGENTS、`.vscode` 與文件根目錄。
- 將巢狀專案改為 SDK-style `net10.0`，啟用 implicit usings 與 nullable；不建立正式測試專案或共用 solution。
- 保留公開 API `public static IList<int> Postorder(Node? root)`，以遞迴 DFS 依序走訪每個 child，最後加入目前節點值。
- `Node` 的空建構子與值建構子都初始化空的 `children` 集合；`Postorder` 對 null root 回傳空集合，helper 不產生主控台輸出。
- `Main` 集中建立並輸出 8 個 deterministic checks：兩個官方範例、null root、single leaf、巢狀 children 順序、重複呼叫、1000 層 chain spot check，以及 traversal invariants。
- 每個案例輸出 Input、Expected、Actual 與 PASS/FAIL，結尾輸出 `Summary: 8/8 checks passed.`；任何失敗設定非零 exit code。
- README 使用繁體中文教學形式，完整 fresh run 只放在唯一的 `text` fence，其餘示範使用 `plaintext` fence。
- 交付範圍包含隔離 worktree、單一 Conventional Commit、draft PR、Ready、squash merge、Issue #2 精準更新，以及合併後 `main` 驗證。

## Algorithm Contract

後序遍歷的不變量是：對每個非空節點，所有 `children` 子樹必須按照原始順序
完整走訪後，才將該節點值加入結果。若樹有 `n` 個節點、樹高為 `h`，時間複雜度
為 `O(n)`，結果空間為 `O(n)`，遞迴呼叫堆疊的輔助空間為 `O(h)`。官方限制的
節點數為 `[0, 10^4]`、高度不超過 `1000`，因此保留遞迴版本同時符合題目契約與
原始程式的教學方向。

## TDD and Verification Contract

- 舊專案的 `MSB3644` 只作為 legacy baseline 證據，不視為有效 RED。
- 在 SDK-style 專案成立後，先讓 acceptance harness 呼叫尚未存在的 `Postorder`
  產生 implementation-specific compiler RED，再加入最小正確實作取得 GREEN。
- 必須通過 JSON parsing、0 warnings/0 errors 的 build、8/8 run、README transcript
  精確 diff、唯一 `text` fence、legacy file absence、scope 與 `git diff --check`。
- 獨立唯讀 review 不得有未處理的 Critical 或 Important 問題；發布前所有修正都要
  重新執行完整驗證與 review。
