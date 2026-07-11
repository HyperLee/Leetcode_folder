# LeetCode 舊版專案升級至 .NET 10 開發規格書

> 文件版本：1.0
>
> 最後更新：2026-07-11
>
> 適用 repository：`HyperLee/Leetcode_folder`
>
> 進度來源：[GitHub Issue #2](https://github.com/HyperLee/Leetcode_folder/issues/2)
>
> 完整案例：[leetcode_412](leetcode_412/)

## 1. 文件目的

本文件定義將 repository 內舊版 .NET Framework LeetCode 題目專案，逐題升級為
SDK-style .NET 10 專案的標準作業流程、技術契約、驗證門檻與交付方式。

本規格同時扮演兩種角色：

1. **可重複使用的開發規格**：後續每一題都應依本文件執行。
2. **已完成工作的稽核紀錄**：附錄記錄 `leetcode_412` 的實際遷移證據。

即時完成狀態永遠以 Issue #2 為唯一進度來源。本文件中的題目狀態只代表特定
日期的快照，不可取代 Issue。

## 2. 適用範圍

本規格適用於 Issue #2 中尚未勾選，且仍使用下列任一舊式設定的題目專案：

- `.csproj` 內含 `<TargetFrameworkVersion>v4.5</TargetFrameworkVersion>`。
- `.csproj` 內含 `<TargetFrameworkVersion>v4.8</TargetFrameworkVersion>`。
- 使用舊式非 SDK-style 專案格式。
- 仍依賴 `App.config`、手寫 `Properties/AssemblyInfo.cs` 或舊 `.sln`。

每次只處理一個 `leetcode_xxx` 題目資料夾。不得為了節省時間而同時批次修改、
刪除或提交多題。

### 2.1 不在本規格範圍內

- 不重新設計整個 repository 的根目錄結構。
- 不建立全 repository 共用 solution。
- 不為每題預設新增測試專案；若既有測試專案存在，才將其納入驗證。
- 不以升級為理由加入與題目無關的第三方套件或架構。
- 不修改 Issue #2 中其他題目的核取方塊。
- 不以通過編譯取代演算法與執行結果驗證。

## 3. 核心原則

### 3.1 Issue 驅動

- 依 Issue #2 的未完成項目順序逐題處理。
- 開始前讀取 Issue，確認目標題仍為 `[ ]`。
- PR 合併成功前，禁止將題目改為 `[x]`。
- 勾選後必須重新讀取 Issue，確認該題已完成且下一題仍未勾選。

### 3.2 單題隔離

- 每題使用獨立分支與隔離 worktree。
- 分支名稱採 `codex/leetcode-xxx-net10`。
- 所有 tracked changes 必須限制在當題的 `leetcode_xxx/` 內。
- 每題在推送前只有一個功能 commit，合併後的 `main` 也只能有一個 squash commit。
- PR 公開後若仍需修改，必須先確認 published-history 處理策略；不得自行 force-push，
  也不得在未記錄例外的情況下悄悄增加 commit。

### 3.3 完整翻修，不只替換 Target Framework

升級必須同時涵蓋專案格式、程式品質、可執行驗證、文件、編輯器設定、Git
設定與交付流程。只把 `TargetFramework` 改為 `net10.0` 不算完成。

### 3.4 證據先於完成宣告

不得依賴「理論上能執行」、舊的終端輸出或代理回報。宣告成功、建立 PR 或合併
前，都必須由控制端重新執行完整驗證並讀取 exit code 與輸出。

## 4. 名詞與路徑

以下以 `leetcode_421` 作為佔位範例；實作時必須替換成當題名稱。

| 名稱 | 範例 | 說明 |
| --- | --- | --- |
| Repository 根目錄 | `Leetcode_folder/` | Git metadata 所在位置 |
| 題目根目錄 | `leetcode_421/` | README、Git 與 VS Code 設定所在位置 |
| 巢狀專案目錄 | `leetcode_421/leetcode_421/` | `Program.cs` 與 `.csproj` 所在位置 |
| 專案檔 | `leetcode_421/leetcode_421/leetcode_421.csproj` | 從 repository 根目錄使用的完整路徑 |
| 題目外層命令目錄 | `leetcode_421/` | 題目 README 內建置命令的工作目錄 |

除非實際結構不同，否則驗證命令一律使用明確的巢狀 `.csproj` 路徑。Repository
根目錄沒有共用 solution，不能以裸 `dotnet build` 或 `dotnet test` 代替單題驗證。

## 5. 開始前盤點

### 5.1 遠端與 Git 狀態

1. 確認 GitHub CLI 可用且已登入。
2. 讀取 Issue #2，確認目標題仍未完成。
3. 確認本機 `main` 工作區乾淨。
4. 以 `git pull --ff-only origin main` 同步最新 `main`。
5. 記錄 `origin/main` SHA，作為後續審查與差異基準。
6. 建立單題分支與隔離 worktree。

範例：

```bash
git pull --ff-only origin main
git worktree add /private/tmp/codex-leetcode-421-net10 \
  -b codex/leetcode-421-net10 origin/main
```

若主工作區有不屬於本題的未提交變更，不得覆寫、暫存或清除；應使用隔離
worktree，或在無法安全隔離時停止並請求使用者處理。

### 5.2 題目專案盤點

開始修改前必須讀取：

- `Program.cs`
- `.csproj`
- 舊 `.sln`
- `App.config`
- `Properties/AssemblyInfo.cs`
- 現有 `README.md`
- 現有 `docs/readme-template.md`
- 題目資料夾內其他教學文件
- 最近相關 commit 與 `git status --short`

同時確認：

- 官方英文與中文題目網址。
- 官方題名、題述、限制與公開 API。
- 舊解法是否正確、是否有教學價值、是否包含 console side effect。
- 是否已存在正式測試專案。
- 題目是否含隨機、非唯一順序或會修改輸入的行為。

### 5.3 Baseline 證據

在改動前執行舊專案能合理支援的建置命令，記錄成功或失敗。macOS 上舊 .NET
Framework 專案常因缺少 reference assemblies 而出現 `MSB3644`；這能證明基準
狀態，但不算 TDD 的有效 RED。

## 6. 固定遷移產物

完成後，題目資料夾至少包含：

```text
leetcode_xxx/
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── AGENTS.md
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_xxx/
    ├── Program.cs
    └── leetcode_xxx.csproj
```

應逐檔移除：

- `leetcode_xxx/leetcode_xxx.sln`
- `leetcode_xxx/leetcode_xxx/App.config`
- `leetcode_xxx/leetcode_xxx/Properties/AssemblyInfo.cs`

刪除只能針對已確認的單一檔案執行。禁止 `rm -rf`、`rm -r`、`find . -delete`、
`trash -r` 或其他批次刪除方式。

## 7. `.csproj` 契約

新專案必須使用 SDK-style 並鎖定 .NET 10：

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

</Project>
```

除非題目原本確實需要，否則不得保留：

- `<TargetFrameworkVersion>`
- `<ProjectGuid>`
- 舊式 `Compile Include`
- 手動 assembly metadata
- 舊 NuGet restore targets
- 無實際用途的套件參考

建置結果必須為 0 warnings、0 errors。不得以停用 nullable、停用 analyzer 或壓制
警告來掩蓋升級問題。

## 8. 共用設定檔契約

### 8.1 `.editorconfig`、`.gitattributes`、`.gitignore`

- 以最近已合併且通過驗證的題目作為來源，例如 `leetcode_412`。
- 共用內容原則上保持一致，不加入當題無關的例外。
- 複製後仍須確認檔案實際位於題目根目錄。

### 8.2 `docs/readme-template.md`

- 使用最近已合併版本的範本。
- 範本是未來建立 README 的起點，不取代當題專屬 README。
- 不得把未替換的題號、方法名稱或 placeholder 留在正式 README。

### 8.3 `.vscode/tasks.json`

必須直接建置巢狀專案，不要求使用者選擇組態：

```json
{
  "version": "2.0.0",
  "tasks": [
    {
      "label": "build leetcode_421",
      "type": "process",
      "command": "dotnet",
      "args": [
        "build",
        "${workspaceFolder}/leetcode_421/leetcode_421.csproj"
      ],
      "group": {
        "kind": "build",
        "isDefault": true
      },
      "problemMatcher": "$msCompile"
    }
  ]
}
```

### 8.4 `.vscode/launch.json`

必須直接啟動 .NET 10 DLL，且 `preLaunchTask` 與 task label 完全一致：

```json
{
  "version": "0.2.0",
  "configurations": [
    {
      "name": "Debug leetcode_421",
      "type": "coreclr",
      "request": "launch",
      "preLaunchTask": "build leetcode_421",
      "program": "${workspaceFolder}/leetcode_421/bin/Debug/net10.0/leetcode_421.dll",
      "args": [],
      "cwd": "${workspaceFolder}/leetcode_421",
      "console": "integratedTerminal",
      "stopAtEntry": false
    }
  ]
}
```

VS Code 的 `${workspaceFolder}` 預期為題目外層 `leetcode_xxx/`。若使用者從 repository
根目錄開啟 VS Code，必須另行確認路徑，不能假設同一設定同時符合兩種 workspace。

### 8.5 `AGENTS.md`

每題必須有簡潔、專屬且可執行的協作指南，至少包含：

- 真實專案結構與巢狀路徑。
- 從題目根目錄執行的 build/run 命令。
- 是否存在正式測試專案。
- `.editorconfig` 的主要程式風格。
- 公開 API、純函式與題目特有不變量。
- acceptance harness 的成功條件。
- Git metadata 位於 parent repository 的提醒。
- commit 與 PR 應限制在本題資料夾。

## 9. `Program.cs` 契約

### 9.1 保留與整理舊解法

- 保留正確且有教學價值的舊解法。
- 修正錯誤、未完成或會破壞題目契約的程式。
- 移除 `Console.ReadKey()`、無用 placeholder、重複輸出與與題目無關的程式。
- 解法方法不得自行輸出；console I/O 由 `Main` 的驗證器負責。
- 若舊解法不值得保留，可改為標準解法，但 README 必須說明最終採用的設計。
- 不為展示數量而加入同複雜度、無額外教學價值的替代解法。

### 9.2 公開 API

- 方法名稱、參數、回傳型別與題目契約一致。
- 不新增題目未要求的 invalid-input 行為或例外。
- 使用必要的 helper 時，責任與命名必須清楚。
- 若演算法會修改輸入，驗證多個解法時必須為各解法建立獨立輸入副本。

### 9.3 `Main` XML summary

`Main` 上方緊鄰位置必須有 XML `<summary>`，並包含：

1. 題號與英文題名。
2. 題號與繁體中文題名。
3. LeetCode 英文官方網址。
4. LeetCode 中文官方網址。
5. 精簡但完整的英文題述。
6. 對應的繁體中文題述。

若既有 XML 區塊就是原始題述或需求，應保留原文，再以必要的 summary 或 remarks
補足資訊，不能任意刪改歷史需求。

### 9.4 函式 XML summary 與關鍵註解

除 `Main` 外，每個主要 function 都必須以繁體中文 XML `<summary>` 說明用途、
解題概念、有效輸入條件與回傳結果。

- 主要 function 指公開解法 API，以及承載題目核心演算法、狀態轉換或關鍵判斷的
  helper。
- 不包含 `Main`、純 console 輸出、格式化或簡單測試工具；`Main` 的題述 XML 仍依
  9.3 節處理。
- `void` 方法應以狀態改變或可觀察結果說明輸出結果。
- 不得因補充文件而新增題目未要求的 invalid-input 行為或例外。

行內註解僅用於說明演算法不變量、關鍵判斷理由、指標或索引移動、以及狀態更新的
原因；不得加入無意義的逐行註解或重述程式碼。文件與註解不得改變公開 API、演算法
行為或 console 輸出。

### 9.5 Acceptance harness

`Main` 必須提供確定性驗證，至少涵蓋：

- 官方範例。
- 最小有效輸入。
- 題目重要邊界值。
- 能破壞錯誤實作的回歸案例。
- 資料結構、索引或狀態更新的不變量。
- 題目上限的合理 spot checks；避免輸出整份巨大結果。

每項檢查必須顯示：

- 輸入或案例名稱。
- Expected。
- Actual。
- `PASS` 或 `FAIL`。

結尾必須輸出精確通過數，例如：

```plaintext
Summary: 11/11 checks passed.
```

任何失敗都必須設定：

```csharp
Environment.ExitCode = 1;
```

### 9.6 非確定性題目

對 `GetRandom()` 或答案順序不唯一的題目，不可要求不合理的固定輸出：

- 隨機結果驗證其屬於合法集合。
- 單一元素時驗證確切值。
- 無序集合在比較前轉為穩定順序，但不得改變公開 API 的語意。
- 除非題目或教學需要，不為了測試方便改寫隨機契約。

## 10. TDD 開發流程

### 10.1 RED

先建立或完成驗證器，再加入或修正 production behavior。有效 RED 必須因尚未實作
的行為或可重現的錯誤而失敗，例如：

- 計畫中的公開方法尚不存在，產生明確的 compiler error。
- 舊解法在新增的回歸案例中得到錯誤結果。
- 狀態更新、邊界值或不變量檢查失敗。

下列情況不算有效 RED：

- `.csproj` 尚未完成導致語法或組態錯誤。
- 舊 `AssemblyInfo.cs` 造成 duplicate attributes。
- 缺少 .NET Framework reference assemblies。
- 測試本身拼字錯誤或使用錯誤 API。

RED 紀錄至少包含工作目錄、命令、exit code 與關鍵錯誤。

### 10.2 GREEN

加入滿足題目契約的最小正確實作後，使用與 RED 相同的 build/harness 命令驗證：

- build exit code 0。
- 0 warnings、0 errors。
- 所有 acceptance checks 通過。
- run exit code 0。

### 10.3 REFACTOR

只在 GREEN 後整理命名、helper、輸出與文件。每次重構後重新執行 build/run，不得
以重構為由改變公開 API、題目契約或已驗證輸出。

## 11. `README.md` 規格

README 使用繁體中文教學形式，至少包含：

1. 題號、英文題名與繁中題名。
2. 英文與中文官方題目連結。
3. 題目說明。
4. 限制條件。
5. 核心不變量與容易出錯的地方。
6. 每個保留解法的設計與取捨。
7. 時間、結果空間與輔助空間複雜度。
8. 至少一個逐步範例。
9. acceptance harness 案例表。
10. 實際驗證過的 build/run 命令。
11. 與 fresh run 完全一致的完整輸出。
12. 最終專案結構。

不得：

- 宣稱不存在的 test project 或測試框架。
- 手寫未實際執行的輸出。
- 在 README 與程式碼間保留不同的方法名稱、複雜度或案例數。
- 使用模糊的「應該可以」代替驗證結果。

### 11.1 輸出 fence 唯一性

完整執行輸出必須是 README 中唯一使用 ````text` 的 code fence。逐步走查、單行
結果或其他純文字示範應使用 ````plaintext`。這讓自動抽取命令能唯一取得實際
執行 transcript。

正確：

````markdown
```plaintext
這是演算法走查，不是完整執行輸出。
```

```text
這裡是 dotnet run 的完整輸出。
```
````

### 11.2 README transcript 驗證

從 repository 根目錄執行，並以當題名稱替換 `leetcode_xxx`：

```bash
dotnet run --no-build \
  --project leetcode_xxx/leetcode_xxx/leetcode_xxx.csproj \
  > /private/tmp/leetcode_xxx.actual.txt

awk '/^```text$/{copy=1;next} copy && /^```$/{exit} copy' \
  leetcode_xxx/README.md \
  > /private/tmp/leetcode_xxx.readme.txt

diff -u \
  /private/tmp/leetcode_xxx.readme.txt \
  /private/tmp/leetcode_xxx.actual.txt
```

`diff -u` 必須 exit 0 且沒有輸出。另以 `rg -c '^```text$'` 確認 README 中只有一個
`text` fence 起始標記。

## 12. 完整驗證門檻

以下命令從 repository 根目錄執行。以 `leetcode_421` 為例：

```bash
jq empty \
  leetcode_421/.vscode/launch.json \
  leetcode_421/.vscode/tasks.json

dotnet build \
  leetcode_421/leetcode_421/leetcode_421.csproj \
  --nologo

dotnet run \
  --no-build \
  --project leetcode_421/leetcode_421/leetcode_421.csproj

git diff --check -- leetcode_421
```

必須同時滿足：

- `jq` exit 0。
- build exit 0，0 warnings、0 errors。
- run 所有檢查均為 PASS，summary 數量正確，exit 0。
- README transcript diff 為空。
- `git diff --check` exit 0。
- changed paths 全部位於目標 `leetcode_xxx/`。
- 舊 `.sln`、`App.config`、`AssemblyInfo.cs` 不存在。
- `.csproj` 只使用 SDK-style `net10.0`。
- worktree 沒有未提交或未追蹤的非預期檔案。

若題目已有正式測試專案，除上述驗證外，還必須執行該測試專案的明確路徑；不得
用 root-level `dotnet test` 猜測專案位置。

## 13. Code Review

### 13.1 審查方式

- 完成單題實作後，必須進行一次獨立、唯讀 code review。
- Reviewer 不得修改 tracked files、commit、push、建立 PR 或更新 Issue。
- CodeRabbit 目前不是必要條件，也不應假設使用者已有帳號。
- 除非使用者日後明確啟用，預設略過 CodeRabbit，使用獨立唯讀 reviewer。

### 13.2 審查範圍

Reviewer 至少檢查：

- 所有需求是否逐項完成。
- 演算法與公開 API 是否符合題目契約。
- acceptance harness 是否可能漏掉重要錯誤。
- `Main` XML summary 是否包含雙語題名、網址與題述。
- 主要 function 的 XML summary 是否以繁體中文說明用途、解題概念、有效輸入條件與
  回傳結果或狀態改變。
- 關鍵演算法或判斷流程的註解是否說明原因，且沒有無意義的逐行註解。
- README 是否與程式、命令及 fresh output 一致。
- VS Code 巢狀路徑是否正確。
- legacy files 是否全部移除。
- changed paths 是否只有當題。
- 是否存在不必要複雜度、side effect、死碼或隱藏例外。

發現 Critical 或 Important 問題時必須修正並重新審查。Minor 問題若會造成規格
不一致也應於發布前修正。

## 14. Commit 與 PR 契約

### 14.1 Commit

每題使用單一 Conventional Commit：

```text
feat(leetcode-421): migrate project to .NET 10
```

commit 前必須確認：

- staged paths 只含 `leetcode_421/`。
- `git diff --cached --check` 通過。
- 完整 build/run 已重新執行。
- commit count 相對 `origin/main` 為 1。

### 14.2 Draft PR

推送分支後建立同名 draft PR。PR body 至少包含：

- 完成的遷移內容。
- 演算法與核心不變量。
- 時間與空間複雜度。
- 實際執行的驗證與結果。
- 獨立 review 結果。
- `Refs #2`，不得使用會自動關閉整個 Issue 的 `Closes #2`。

### 14.3 Ready 與合併門檻

Draft PR 只有在下列條件全部成立後才能轉為 Ready：

- head SHA 等於控制端已驗證 SHA。
- PR changed files 與 commit 數符合預期。
- PR 可合併，merge state 為 clean。
- 沒有 failed、cancelled 或 pending checks。
- 獨立 reviewer 沒有未處理的 Critical/Important 問題。

採 squash merge，並傳入 expected head SHA，避免審查後分支被修改仍遭合併。每題在
`main` 最終只留下單一 migration commit。

## 15. Issue 更新契約

只有 GitHub 已確認 PR `merged: true` 並回傳 merge SHA 後，才能更新 Issue #2：

1. 重新讀取 Issue 最新 body。
2. 確認目標 `- [ ] \`leetcode_xxx\`` 恰好出現一次。
3. 只替換該行為 `- [x] \`leetcode_xxx\``。
4. 更新 Issue，不改變其他內容、標籤或狀態。
5. 再次讀回 Issue。
6. 確認已勾選項目存在、未勾選版本消失。
7. 確認下一個依序項目仍為 `[ ]`。

若 PR 未合併、merge SHA 不明、Issue entry 不唯一或 Issue 已被其他人更新，必須停止
並重新分析，不能猜測替換位置。

## 16. 合併後驗證

PR 合併後：

1. 確認主工作區位於乾淨的 `main`。
2. 執行 `git pull --ff-only origin main`。
3. 確認本機 `HEAD` 與 `origin/main` 都是 GitHub 回傳的 merge SHA。
4. 在合併後的 `main` 重新執行 JSON、build、run 與 whitespace 檢查。
5. 確認 `git status --short` 為空。

建立 PR 的 worktree 應保留至 PR 工作全部結束；不要在可能需要處理 review feedback
時提早清除。

## 17. 異常處理與停止條件

### 17.1 常見問題

| 問題 | 根因 | 處理方式 |
| --- | --- | --- |
| `MSB1003` | 在無 solution/project 的目錄執行裸 `dotnet build/test` | 改用明確巢狀 `.csproj` 路徑 |
| `MSB3644` | macOS 缺少舊 .NET Framework reference assemblies | 記錄 baseline，完成 SDK 遷移後改用 .NET 10 驗證 |
| Duplicate assembly attributes | 舊 `AssemblyInfo.cs` 與 SDK 自動產生資訊重複 | 逐檔移除舊 `AssemblyInfo.cs` |
| README diff 選到錯誤區塊 | README 有多個 `text` fence | 完整輸出保留唯一 `text`，其他改用 `plaintext` |
| README 與實際輸出漂移 | 範例由人工輸入或程式重構後未更新 | 以 fresh run 為來源並執行精確 diff |
| Git `index.lock` 權限錯誤 | worktree Git metadata 位於 parent `.git/worktrees` | 使用受控且限定範圍的 Git 寫入權限，不改以危險命令規避 |
| PR 顯示不可合併 | `main` 漂移、衝突或 GitHub 尚未計算完成 | 重新 fetch、確認 ancestry 與 merge state；有衝突時停止合併 |
| 隨機題目輸出不穩定 | Harness 強迫隨機值等於固定答案 | 驗證合法集合與單元素確定性，不扭曲題目契約 |

### 17.2 必須停止的情況

- 發現目標以外的 user changes，且無法安全隔離。
- 需要批次刪除資料夾或不確定刪除範圍。
- 官方題目契約、公開 API 或限制無法確認。
- Build、run、README diff 或 code review 尚未通過。
- PR head SHA 與已驗證 SHA 不一致。
- GitHub checks 失敗或仍在執行。
- PR 無法 clean merge。
- Issue 目標行不是唯一匹配。
- GitHub 權限或登入狀態無法完成必要操作。

### 17.3 修正策略

- PR 建立前：修正後 amend 原 commit，維持單一 commit。
- PR 建立後、尚未合併：修正、重跑全部驗證、重新 review，並先取得使用者對
  published history 的明確指示。未取得授權不得 force-push；若改以 follow-up
  commit 處理，必須在 PR 記錄此單一 commit 規則的例外，最終仍採 squash merge。
- PR 合併後：不得 force rewrite `main`；以新的 follow-up PR 修正。
- 任何修正都不能只跑局部檢查後直接合併。

## 18. Definition of Done

單題只有在以下全部勾選時才算完成：

- [ ] Issue #2 在開始時顯示該題未完成。
- [ ] 使用最新 `origin/main` 建立獨立分支與 worktree。
- [ ] 所有變更只在當題 `leetcode_xxx/`。
- [ ] `.csproj` 為 SDK-style `net10.0`。
- [ ] `ImplicitUsings` 與 Nullable 已啟用。
- [ ] 舊 `.sln`、`App.config`、`AssemblyInfo.cs` 已逐檔移除。
- [ ] 必要設定檔、VS Code 檔案、範本與 `AGENTS.md` 已加入。
- [ ] 解法公開 API 與題目契約一致，且沒有非必要 console side effect。
- [ ] `Main` XML summary 包含雙語題名、網址與雙語題述。
- [ ] 主要 function XML summary 與關鍵註解符合 9.4 節。
- [ ] TDD RED 由真正缺少或錯誤的 behavior 觸發。
- [ ] GREEN build 為 0 warnings、0 errors。
- [ ] Acceptance harness 全部 PASS，失敗可產生非零 exit code。
- [ ] README 是繁中教學文件且涵蓋所有規定章節。
- [ ] README 完整輸出是唯一 `text` fence。
- [ ] README transcript 與 fresh run 精確一致。
- [ ] JSON、whitespace、scope 與 legacy absence 檢查通過。
- [ ] 獨立唯讀 review 無未解決 Critical/Important 問題。
- [ ] 只有一個符合命名規則的 commit。
- [ ] Draft PR 使用 `Refs #2`，並在驗證後轉 Ready。
- [ ] PR 以已驗證 head SHA squash merge。
- [ ] Issue #2 只在合併後勾選目標題。
- [ ] Issue 已讀回確認，下一題仍未勾選。
- [ ] 合併後 `main` 已重新驗證且工作區乾淨。

## 19. 每題執行紀錄範本

每一題的工作報告或 PR 說明可使用以下欄位：

```markdown
# leetcode_xxx migration record

## Identity

- Issue entry:
- Base SHA:
- Branch:
- Worktree:
- Local commit SHA:
- PR:
- Merge SHA:

## Algorithm

- Public API:
- Preserved/replaced solution:
- Core invariant:
- Time complexity:
- Space complexity:

## TDD

- RED command:
- RED evidence:
- GREEN command:
- GREEN evidence:

## Acceptance harness

- Cases:
- Check count:
- Final summary:
- Exit code:

## Verification

- JSON:
- Build:
- Run:
- README transcript diff:
- Git diff check:
- Scope:
- Legacy file absence:
- Independent review:

## Delivery

- Commit subject:
- Draft/Ready state:
- Mergeability/checks:
- Merge method:
- Issue readback:
- Next issue item:
```

## 20. 實際案例：`leetcode_412`

本節記錄 2026-07-11 完成的 LeetCode 412「Fizz Buzz」遷移，作為後續題目的
參考實作。

### 20.1 Identity

| 欄位 | 實際值 |
| --- | --- |
| 題目 | `leetcode_412` |
| Base SHA | `8354faf3893d3d56ff494bedaf7d11178825bd51` |
| Branch | `codex/leetcode-412-net10` |
| Worktree | `/private/tmp/codex-leetcode-412-net10` |
| 最終本地功能 commit | `15d768dfeae5fde18cfb5bae9346d17423fab826` |
| Commit subject | `feat(leetcode-412): migrate project to .NET 10` |
| PR | [#5](https://github.com/HyperLee/Leetcode_folder/pull/5) |
| Merge method | Squash |
| Merge SHA | `ff884b05747e0638b2bf2ffda41dba615c19d426` |

### 20.2 實作內容

- 將專案改為 SDK-style `net10.0`，啟用 implicit usings 與 nullable。
- 保留並整理單一條件判斷解法：
  `public static IList<string> FizzBuzz(int n)`。
- 解法方法只回傳結果；所有輸出由 `Main` 負責。
- 先判斷 15 的倍數，再判斷 3 與 5，維持 `FizzBuzz` 不變量。
- 使用 `result[i - 1]` 對應題目從 1 起算與陣列從 0 起算的差異。
- 加入五組完整序列與上限 `n = 10000` 的六項 spot checks，共 11 項檢查。
- 加入所有固定遷移產物，並逐檔移除舊 `.sln`、`App.config` 與
  `Properties/AssemblyInfo.cs`。

### 20.3 TDD 證據

Baseline 在 macOS 因舊 .NET Framework 4.8 reference assemblies 缺失而得到
`MSB3644`，此結果只作為舊專案狀態證據。

有效 RED 是在完成 SDK 遷移與驗證器後，刻意讓 production `FizzBuzz` 方法尚未
存在。Build 得到兩個 `CS0103`，0 warnings、2 errors，且兩個錯誤都指向缺少
`FizzBuzz`。

加入最小正確實作後，GREEN 證據為：

- Build exit code 0。
- 0 warnings、0 errors。
- Run exit code 0。
- 11 個 PASS。
- `Summary: 11/11 checks passed.`

### 20.4 README transcript 修正

第一次控制端驗證使用「抽取第一個 `text` fence」的命令時，讀到 `n = 15` 的單行
走查，而非完整執行輸出。根因是 README 同時有兩個 `text` fence。

最小修正是：

- 將走查 fence 從 `text` 改為 `plaintext`。
- 保留完整執行輸出為唯一 `text` fence。
- 重新抽取並執行 `diff -u`，結果 exit 0 且沒有差異。
- Amend 原 commit，仍維持單一功能 commit。
- 對 amended commit 重新進行唯讀 review。

這個案例形成本規格第 11.1 節的 fence 唯一性要求。

### 20.5 Review 與交付證據

- 獨立唯讀 reviewer 對原 commit 與 amended commit 都沒有 Critical、Important 或
  Minor findings。
- CodeRabbit 依使用者指示略過。
- PR #5 的 head SHA 與已驗證 SHA 一致。
- PR 狀態為 `MERGEABLE`，merge state 為 `CLEAN`，沒有失敗或 pending checks。
- PR 使用 expected head SHA 執行 squash merge。
- GitHub 回傳 `merged: true` 與 merge SHA。
- 合併後才將 Issue #2 的 `leetcode_412` 更新為 `[x]`。
- Issue 讀回確認 `leetcode_421` 仍為下一個 `[ ]` 項目。
- 本機 `main` fast-forward 至 merge SHA 後，再次通過 JSON、build、run 與
  whitespace 驗證。

### 20.6 2026-07-11 狀態快照

- `leetcode_380`：已完成並合併。
- `leetcode_389`：已完成並合併。
- `leetcode_412`：已完成並合併。
- 下一題：`leetcode_421`。

此清單是歷史快照；未來仍須重新讀取 Issue #2，不能直接依本節判斷進度。

## 21. 後續每題的最短操作摘要

1. 讀 Issue，取得下一個未完成題目。
2. 同步 `main`，建立單題分支與隔離 worktree。
3. 盤點舊程式、專案格式、官方契約與基準狀態。
4. 先建立會正確失敗的 acceptance harness。
5. 完成 .NET 10 遷移與最小正確解法。
6. 加入固定設定檔、VS Code、AGENTS、範本與專屬 README。
7. 執行 build/run、JSON、transcript、whitespace、scope 與 legacy checks。
8. 建立單一 commit，交由獨立 reviewer 唯讀審查。
9. 修正後重新驗證與 review。
10. 推送分支，建立 draft PR，確認 Ready、checks 與 head SHA。
11. Squash merge。
12. 合併後勾選 Issue，讀回確認下一題。
13. 同步並重新驗證合併後的 `main`。

只要任何一步缺少新鮮證據，就不得跳到下一步。
