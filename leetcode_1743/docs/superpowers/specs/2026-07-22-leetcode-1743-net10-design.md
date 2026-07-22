# LeetCode 1743 .NET 10 翻新設計

## 目標與範圍

依 repository 根目錄的 `LEETCODE_NET10_MIGRATION_SPEC.md`，只翻新
`leetcode_1743/`。交付包含 SDK-style .NET 10 專案、可重複執行的 acceptance
harness、繁體中文教學文件、題目根目錄的 VS Code 設定、獨立唯讀審查、單一功能
commit、PR、squash merge、Issue #2 更新與合併後驗證。

不修改其他題目，不建立額外測試專案，也不加入第三方套件。

## 解法設計

保留既有 `public int[] RestoreArray(int[][] adjacentPairs)` 公開 API，以及原本正確的
「無向圖鄰接表 + 從端點線性走訪」解法。

1. 將每組相鄰元素雙向加入鄰接表。
2. 找出鄰居數為 1 的任一端點，作為還原陣列的第一個元素。
3. 第二個元素是端點唯一的鄰居。
4. 後續位置查看目前元素的鄰居，排除前一個元素後選取另一個鄰居。

題目保證存在唯一陣列（忽略整體反向），且所有元素互異。因此內部節點恰有兩個鄰居、
兩個端點恰有一個鄰居；演算法不需額外 visited 集合。時間複雜度為 `O(n)`，結果空間
為 `O(n)`，鄰接表的輔助空間為 `O(n)`。方法只讀取 `adjacentPairs`，不修改輸入。

## 程式結構與輸出邊界

- `Program.Main`：建立所有案例、統一輸出 Input／Expected／Actual／PASS 或 FAIL，並在
  任一失敗時設定 `Environment.ExitCode = 1`。
- `Solution.RestoreArray`：純演算法 API，不讀寫 console。
- 測試與格式化 helper：只回傳結構化結果或字串，不自行輸出。
- `Main` 保留題號、雙語題名、官方網址與雙語題述的 XML summary。
- 核心解法與承載重要判斷的 helper 使用繁體中文 XML summary，行內註解只說明端點與
  排除回頭路的不變量。

## 驗證設計

Acceptance harness 涵蓋：

- 兩組官方範例。
- 最小有效輸入（單一 pair）。
- 含負數與非排序值。
- pairs 順序與每組方向打亂。
- 可揭露錯誤起點或回頭選擇的較長鏈。
- 合理的大型鏈 spot check，不輸出整份巨大結果。
- 每個案例驗證輸入內容在呼叫後保持不變。

因正確答案允許整體反向，案例以「長度正確、元素集合一致、每組輸出相鄰值都存在於
輸入 pair 集合」判定有效，不強迫固定方向。輸出仍提供穩定、精簡且可與 README 完整
比對的 Actual 摘要。

## 專案與文件產物

題目根目錄將包含 `.editorconfig`、`.gitattributes`、`.gitignore`、`.vscode/`、
`AGENTS.md`、`README.md`、`docs/readme-template.md` 與本設計文件。巢狀專案只保留
`Program.cs` 與 SDK-style `leetcode_1743.csproj`；逐檔移除舊 `.sln`、`App.config`
與 `Properties/AssemblyInfo.cs`。

README 將以繁體中文說明題目、限制、不變量、複雜度、逐步範例、案例表、實際命令、
fresh run 完整輸出與最終結構。完整輸出使用唯一的 `text` fence。

## 交付與失敗處理

先記錄 legacy build baseline，再以先完成 harness、讓缺少 production API 產生有效 RED
的方式進入 GREEN；重構後重跑驗證。發布前檢查 JSON、精確 VS Code task 路徑、build
的 0 warnings／0 errors、run exit code、README transcript diff、單一 `text` fence、
whitespace、範圍與 legacy 檔案不存在。

唯讀審查若發現 Critical、Important 或規格不一致問題，修正後重新審查。全部通過後將
本地開發歷史整理為相對 `origin/main` 的單一 Conventional Commit，建立 Draft PR、確認
head SHA 與 checks、轉 Ready 並以 expected head SHA squash merge。只有確認 merged 後才
更新 Issue #2 的 `leetcode_1743` 唯一核取方塊，最後在合併後的 main 重跑完整驗證。
