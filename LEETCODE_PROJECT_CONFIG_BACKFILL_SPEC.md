# LeetCode 題目專案設定檔補齊開發規格書

> 文件版本：1.0
>
> 最後更新：2026-07-29
>
> 適用 repository：`HyperLee/Leetcode_folder`
>
> 唯一範本：[leetcode_003](leetcode_003/)

## 1. 文件目的

本文件定義如何以 `leetcode_003` 為唯一範本，安全補齊題目資料夾缺少的
`.editorconfig`、`.gitattributes`、`.gitignore` 與
`docs/readme-template.md`。執行時必須先從根目錄 `README.md` 取得候選資料夾，
再以檔案系統現況確認實際目標；不得只依賴 README 中可能已過期的缺失標記。

本規格採取「只補缺檔」原則。任何既有檔案，不論內容是否與範本一致，均不得
覆寫、截斷、重新格式化或刪除。

## 2. 適用範圍

### 2.1 候選資料夾

根目錄 README 的「完整題目索引」共有 608 筆主要題目資料列。若某列的
`.editorconfig` 或 `.gitignore` 欄位至少一項顯示 `**缺少**`，該題目資料夾即為
候選。

候選只用來縮小本次盤點範圍。正式執行前仍須檢查候選資料夾內的實際檔案：

- `.editorconfig` 與 `.gitignore` 都已存在：從實際目標清單排除。
- 任一檔案仍不存在：列入實際目標，並逐一檢查本規格要求的四個檔案。

`.gitattributes` 與 `docs/readme-template.md` 只補到上述實際目標，不得因此擴大
掃描至其他 README 未列為候選的題目。

### 2.2 允許的本機變更

未來依本規格執行時，只允許：

- 在實際目標題目根目錄新增缺少的 `.editorconfig`。
- 在實際目標題目根目錄新增缺少的 `.gitattributes`。
- 在實際目標題目根目錄新增缺少的 `.gitignore`。
- 必要時建立目標題目的 `docs/` 目錄，並新增缺少的
  `docs/readme-template.md`。
- 補檔完成後，更新根目錄 README 索引中的 `.editorconfig` 與 `.gitignore`
  欄位。

### 2.3 不在範圍內

- 不修改任何 `Program.cs`、`.csproj`、solution、測試或演算法。
- 不新增或修改題目專屬 `README.md`。
- 不修改既有的四種設定或範本檔案。
- 不補齊 `.vscode`、`AGENTS.md` 或其他未列出的檔案。
- 不修改 `PROJECT_DOTNET_VERSION_AUDIT.md`。
- 不建立或更新 GitHub Issue。
- 不建立分支、worktree、commit、PR，不推送或合併遠端內容。
- 不新增或修改任何公開 API、型別或執行時行為。

## 3. 唯一範本與完整性

四個來源檔案固定如下，且必須位於 `leetcode_003` 題目根目錄：

| 產物 | 唯一來源 | 2026-07-29 SHA-256 |
| --- | --- | --- |
| `.editorconfig` | `leetcode_003/.editorconfig` | `b48aee94f30114683a81339518ab32bece14e93dc198ace3c250e8fe2f5406e3` |
| `.gitattributes` | `leetcode_003/.gitattributes` | `d808c8d570876cabebc520c6d89e284e2cbb43e01d03a2c24511f6558a881fb2` |
| `.gitignore` | `leetcode_003/.gitignore` | `46848133b5e695ee8dea5bfef0c826ffaa478810cc5ea376f027be7918c00659` |
| `docs/readme-template.md` | `leetcode_003/docs/readme-template.md` | `08eceb22d28ef85325534d83e15d0581a6b51789e5b4a963aed4df1b483caa00` |

執行前必須重新計算四個來源檔案的 SHA-256，並與表格完全相符。來源缺失、
不是一般檔案或雜湊不同時，立即停止；不得自行接受新版範本或更新本表。

複製必須保留來源的完整位元組內容，不替換題號、路徑、標題、換行或其他文字。
每個新增檔案的 SHA-256 必須與對應來源相同。

## 4. 2026-07-29 基準快照

本快照記錄規格制定時的現況，只作為差異偵測與審查依據，不取代正式執行前的
重新盤點。

| 項目 | 數量 |
| --- | ---: |
| README 主要題目資料列 | 608 |
| README 候選資料夾 | 191 |
| 檔案系統確認後的實際目標 | 190 |
| 待新增 `.editorconfig` | 190 |
| 待新增 `.gitignore` | 174 |
| 待新增 `.gitattributes` | 190 |
| 待新增 `docs/readme-template.md` | 190 |
| 預估新增檔案總數 | 744 |

README 仍把 `leetcode_003` 的 `.editorconfig` 與 `.gitignore` 標示為缺少，但兩檔
目前都已存在，因此 `leetcode_003` 必須從實際目標排除。

其餘 190 個實際目標中，有 16 個已存在 `.gitignore`，且內容與
`leetcode_003/.gitignore` 不同。這 16 個既有檔案必須保留；不得為了統一內容而
覆寫。基準時 190 個實際目標均缺少 `.editorconfig`、`.gitattributes`、
`docs/readme-template.md` 與 `docs/` 目錄。

若正式執行時數量與本快照不同，應在 manifest 與執行摘要中說明差異。只要
README 結構、來源範本及安全條件仍符合本規格，即以重新盤點結果為準；不得為了
符合舊數字而新增或覆寫不需要的檔案。

## 5. 執行前盤點與 manifest

### 5.1 工作區與來源檢查

1. 從 repository 根目錄執行所有命令。
2. 讀取 `git status --short`，記錄執行前已有的變更；不得修改、暫存或清除它們。
3. 確認四個來源路徑皆為一般檔案。
4. 計算來源 SHA-256，逐一比對第 3 節。
5. 確認根目錄 README 的索引表能唯一定位，標題與八欄結構完整。

### 5.2 README 解析契約

README 索引欄位依序必須為：

```text
題號 | 資料夾 | 專案檔 | TargetFramework | SDK-style |
.editorconfig | .gitignore | README.md
```

解析器必須：

- 只接受 608 筆主要題目資料列。
- 從「資料夾」欄位的 Markdown 連結取得 `leetcode_*` 相對路徑。
- 拒絕絕對路徑、`..`、repository 外路徑及無法唯一解析的資料夾連結。
- 只把 `.editorconfig` 或 `.gitignore` 欄位為 `**缺少**` 的列納入候選。
- 驗證每個候選資料夾實際存在且為 repository 根目錄的直接子資料夾。
- 使用檔案系統現況決定是否列入實際目標。

### 5.3 Manifest 契約

任何寫入前必須先完成唯讀 manifest。每筆預定新增檔案至少記錄：

- 題目資料夾。
- 來源相對路徑。
- 目標相對路徑。
- 來源 SHA-256。
- 缺檔判定結果。

Manifest 必須同時列出：

- README 候選數與實際目標數。
- 四種檔案各自的新增數量。
- 因 `.editorconfig` 與 `.gitignore` 都已存在而排除的候選。
- 實際目標中已存在、因此必須保留的四種檔案及其執行前 SHA-256。
- 需要建立 `docs/` 目錄的題目。

Manifest 完成後必須確認沒有重複目標路徑、沒有來源與目標相同、沒有 repository
外路徑，且預定新增總數等於四種檔案新增數之和。

## 6. 安全寫入契約

### 6.1 逐檔新增

- 依 manifest 逐筆處理，不得使用會覆寫目標的普通複製模式。
- 實作必須使用具備「目標已存在即失敗」語意的獨佔建立方式，例如 Node.js
  `copyFile` 搭配 `COPYFILE_EXCL`。
- 寫入每個檔案前再次確認目標不存在，以提供清楚的錯誤訊息；真正的安全門檻仍
  由獨佔建立操作保證，避免檢查後發生競態。
- `docs/` 不存在時才建立；若路徑已存在但不是目錄，立即停止。
- 不得變更新檔案內容，也不得執行會重寫這些檔案的 formatter。

### 6.2 失敗處理

遇到以下任一情況必須立即停止後續寫入：

- 來源檔案或來源雜湊不符合第 3 節。
- README 表格無法唯一解析或不是 608 筆主要題目資料列。
- 候選或目標路徑超出 repository、不是預期的 `leetcode_*` 直接子資料夾。
- Manifest 有重複、衝突或計數不一致。
- 預定新增的目標在實際寫入時已存在。
- `docs` 路徑存在但不是目錄。
- 複製後雜湊與來源不同。

若批次在中途失敗，保留 manifest、已完成清單與錯誤證據，且不得自動覆寫、
批次刪除或回復使用者原有檔案。本 repository 禁止 `rm -rf`、`rm -r`、
`find . -delete` 與 `trash -r`；若需要大量回復新增檔案，停止並由使用者決定
後續處理方式。

## 7. 根 README 同步規則

四種檔案全部依 manifest 成功新增並驗證後，才可同步根目錄 README。

- 重新讀取 608 筆索引資料列。
- 依檔案系統現況更新每列的 `.editorconfig` 與 `.gitignore` 欄位。
- 存在時分別使用 `[.editorconfig](leetcode_xxx/.editorconfig)` 與
  `[.gitignore](leetcode_xxx/.gitignore)`。
- 不存在時保留 `**缺少**`。
- 不修改題號、資料夾、專案檔、TargetFramework、SDK-style 或 `README.md`
  欄位。
- 不重新排序資料列，也不修改索引以外的 README 內容。

完成後 `.editorconfig` 與 `.gitignore` 的 `**缺少**` 數量都必須為 0。若任一
缺失仍存在，README 不得宣告補齊完成，並須回報剩餘資料夾。

## 8. 驗證與驗收

### 8.1 檔案驗證

- 每個 manifest 預定新增檔案均存在且為一般檔案。
- 每個新增檔案的 SHA-256 與對應的 `leetcode_003` 來源完全相同。
- 實際新增數與 manifest 四類計數完全一致。
- 所有執行前已存在檔案的 SHA-256 均未改變。
- `leetcode_003` 未出現在實際目標或新增清單。
- 基準中的 16 份既有 `.gitignore` 均未被覆寫；若正式執行時數量改變，則以
  manifest 記錄的全部既有檔案為準。

### 8.2 README 驗證

- 索引仍為 608 筆主要題目資料列，每列維持八欄。
- `.editorconfig` 與 `.gitignore` 欄位不存在 `**缺少**`。
- 兩欄所有 Markdown 連結均能從 repository 根目錄解析至一般檔案。
- 除上述兩欄外，每筆資料的其他六欄與執行前完全相同。

### 8.3 Git 範圍驗證

執行：

```bash
git diff --check
git status --short
git diff -- README.md
```

並檢查所有未追蹤檔案。驗收時只允許：

- Manifest 中列出的新增設定或範本檔案。
- 需要容納範本而新增的 `docs/` 目錄。
- 根目錄 `README.md` 的兩個設定檔欄位變更。
- 執行前已記錄、且未被本次流程修改的既有工作樹變更。

因新增檔案尚未追蹤，普通 `git diff --check` 不足以驗證完整範圍；必須以
manifest、`git status --short`、逐檔雜湊與 README 結構檢查共同判定。

## 9. 完成條件

只有同時符合下列條件，才可宣告本機補檔工作完成：

1. 來源路徑與 SHA-256 全部符合第 3 節。
2. Manifest 已在任何寫入前產生並通過路徑、重複與計數檢查。
3. 所有 manifest 新增檔案均以禁止覆寫的方式建立。
4. 所有新增檔案與來源 byte-for-byte 相同。
5. 所有執行前既有檔案保持不變。
6. 根 README 仍有 608 筆八欄資料列，兩個設定檔缺失數均為 0。
7. `git diff --check` 通過，且工作樹變更範圍符合第 8.3 節。
8. 未執行任何 Issue、分支、worktree、commit、PR、push 或 merge 操作。

本規格的數量屬於 2026-07-29 快照。未來正式執行時，manifest 與當時的檔案
系統現況才是該次工作的驗收依據。
