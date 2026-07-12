# leetcode_501 .NET 10 Migration Design

## 目標

將 `leetcode_501` 的舊式 .NET Framework 4.8 主控台專案翻新為可獨立執行的
SDK-style .NET 10 專案，同時保留 LeetCode 501「Find Mode in Binary Search Tree」的
公開解法契約、加入可重複執行的驗收驗證器，並完成單一 commit、PR、squash merge
及 Issue #2 勾選流程。

## 範圍與限制

- 只變更 `leetcode_501/`；不修改其他題目或 repository 根目錄檔案。
- 使用巢狀專案 `leetcode_501/leetcode_501/leetcode_501.csproj`，目標為 `net10.0`。
- 移除舊式 `.sln`、`App.config` 與 `Properties/AssemblyInfo.cs` 三個明確檔案。
- 不新增第三方套件或正式測試專案；主控台 acceptance harness 是正式驗證機制。
- 所有 console I/O 都由 `Main` 負責，解法與 helper 不得輸出。
- 最終分支相對 `origin/main` 僅保留一個功能 commit：
  `feat(leetcode-501): migrate project to .NET 10`。

## 演算法決策

考慮三種方案：

1. 保留原本的遞迴中序走訪與靜態欄位。改動最少，但同一次處理程序的多次呼叫可能
   共用狀態，而且深度為 10,000 的偏斜樹可能耗盡呼叫堆疊。
2. 以顯式 `Stack<TreeNode>` 做迭代中序走訪，並在 `FindMode` 內持有局部計數狀態。
   這保留 BST 的核心教學不變量，且能安全處理偏斜樹；採用此方案。
3. 以字典統計所有值。雖然直觀，卻需要 O(n) 額外空間，也沒有利用 BST 的排序特性。

中序走訪會產生非遞減序列，因此每個相同值必定連續。演算法只追蹤前一個值、目前
連續次數、最高次數與目前眾數清單：新最高次數清空並替換清單，平手則附加值。

- 公開 API：`public static int[] FindMode(TreeNode? root)`。
- 時間複雜度：O(n)。
- 額外空間：O(h)，其中 h 為樹高；不計回傳的眾數陣列。
- 不變量：處理目前節點後，眾數清單精確包含目前已走訪節點中出現 `maxFrequency`
  次的所有值。

## 程式結構

`Program.cs` 會包含下列界線清楚的單位：

- `TreeNode`：符合題目定義的二元搜尋樹節點。
- `FindMode`：純解法 API；不寫入 console，也不保存跨呼叫的靜態狀態。
- `RecordValue`：更新單一中序值的連續計數、最高頻率與眾數清單。
- `BuildTree` 與 `BuildRightChain`：僅供 acceptance harness 建立可讀取的測資。
- `RunCase` 與 `CaseResult`：產生 expected/actual/pass 資料，不自行輸出。
- `Main`：逐案列印輸入、Expected、Actual、PASS/FAIL 與精確總結；任一失敗時設為
  `Environment.ExitCode = 1`。

主要解法與核心狀態更新 helper 會使用繁體中文 XML summary；只在中序排序與同頻率
更新這兩個關鍵位置加入解釋原因的註解。

## Acceptance Harness

驗證器會穩定輸出八項檢查，每項都列出案例、輸入、Expected、Actual 與 PASS/FAIL：

1. 官方範例一：`[1,null,2,2]`，預期 `[2]`。
2. 官方範例二暨最小有效樹：`[0]`，預期 `[0]`。
3. 值域邊界且全異：`[-100000,0,100000]`，三個值皆為眾數。
4. 並列眾數：中序序列 `[1,1,2,2]`，預期 `[1,2]`。
5. 右子樹出現最多次：中序序列 `[1,2,2]`，預期 `[2]`。
6. 負值重複：中序序列 `[-1,-1,0]`，預期 `[-1]`。
7. 同一程序的連續兩次呼叫：先得到 `[1]`、再得到 `[2]`，證明沒有靜態狀態殘留。
8. 10,000 節點的右偏斜重複值樹，預期 `[7]`，驗證迭代走訪與題目上限。

## 文件與工具設定

問題根目錄會新增 `.editorconfig`、`.gitattributes`、`.gitignore`、`.vscode/tasks.json`、
`.vscode/launch.json`、`AGENTS.md`、`README.md` 與 `docs/readme-template.md`。VS Code
設定假設直接開啟 `leetcode_501/`，並直接建置/啟動巢狀 .NET 10 專案。

README 使用繁體中文教學格式，說明 BST 的連續區段不變量、迭代中序走訪、複雜度、
逐步範例、八項驗收案例與實際命令。完整 fresh-run transcript 是 README 唯一的
`text` fence，並以精確 diff 驗證。

## TDD 與交付

baseline 已記錄舊專案因 `MSB3644` 無法在 macOS 建置，僅作為舊式專案證據。有效
RED 會先建立呼叫尚不存在 `FindMode` 的 acceptance harness，確認建置因 `CS0103`
失敗；接著加入最小迭代中序實作得到 GREEN，最後只做不改變行為的文件與命名整理。

本機驗證會包含 JSON 解析、明確專案的 build/run、README transcript diff、
`git diff --check`、legacy files absence 與 scoped-path 檢查。完成後進行獨立唯讀
review，建立單一 commit、推送、建立 draft PR、驗證後轉 Ready 並 squash merge，最後
只勾選 Issue #2 的 `leetcode_501` 項目，讀回確認後於合併後 main 重新驗證。
