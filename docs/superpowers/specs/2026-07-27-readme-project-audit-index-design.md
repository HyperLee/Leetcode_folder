# README 專案稽核索引與缺失追蹤 Issue 設計

## 目標

以 repository 根目錄的 `PROJECT_DOTNET_VERSION_AUDIT.md` 為唯一資料來源，更新
根目錄 `README.md` 的「完整題目索引」，並建立一個 GitHub 彙總 Issue，讓讀者可
直接查看每個題目專案的 .NET 版本、專案格式與題目根目錄必要檔案完整性。

## 範圍

- 更新根目錄 `README.md` 的「完整題目索引」說明與表格。
- 保留現有題號、題目資料夾及專案檔的連結。
- 表格欄位固定為：
  `題號`、`資料夾`、`專案檔`、`TargetFramework`、`SDK-style`、
  `.editorconfig`、`.gitignore`、`README.md`。
- 建立一個新的 GitHub Issue，彙總所有缺少上述三種題目根目錄檔案的專案。
- 不修改任何題目專案、`.csproj` 或缺失檔案本身。
- 不修改 `PROJECT_DOTNET_VERSION_AUDIT.md`。

## README 呈現規則

- `TargetFramework` 使用反引號顯示稽核值，例如 `net10.0`。
- `SDK-style` 使用「是」或「否」顯示；數值直接取自稽核明細。
- `.editorconfig`、`.gitignore` 與 `README.md`：
  - 檔案存在時顯示可點擊的相對路徑連結。
  - 檔案不存在時顯示粗體「缺少」。
- 表格依現有題號數字排序；題號相同時維持稽核明細順序。
- 表格前加入狀態說明，並連結 `PROJECT_DOTNET_VERSION_AUDIT.md`，清楚標示資料
  來源及稽核口徑。

## GitHub Issue 設計

建立一個新的彙總 Issue，內容包括：

1. 稽核來源與範圍：608 個題目主專案、4 個測試專案，合計 612 個
   `.csproj`。README 維持一題一列，只索引 608 個題目主專案。
2. 缺失統計：
   - 缺少 `.editorconfig`：191 個題目資料夾。
   - 缺少 `.gitignore`：175 個題目資料夾。
   - 缺少 `README.md`：164 個題目資料夾。
   - 至少缺少一項：196 個題目資料夾。
3. 以「每個受影響專案一個核取方塊」列出清單；每列同時註明該專案缺少哪些
   檔案。只有列出的缺失全部補齊後才勾選該專案。
4. 完成條件：
   - 清單中的缺失檔案均已補齊。
   - 重新執行稽核後三項缺失數均為 0。
   - README 完整題目索引已同步新的稽核結果。

這種清單可避免同一專案在三個分類重複出現，同時仍能清楚顯示每個專案的實際
缺失。

## 資料流程

1. 解析 `PROJECT_DOTNET_VERSION_AUDIT.md` 的「全部專案明細」表格，確認 612 個
   `.csproj` 後篩出 608 個題目主專案。
2. 由 608 個題目主專案產生 README 索引列；4 個測試專案不另增重複題號。
3. 從解析結果篩選任一必要檔案為「缺少」的專案，產生 Issue 核取清單。
4. 以稽核報告摘要數字及檔案系統唯讀檢查交叉驗證產出。

## 錯誤處理

- 若稽核明細不是 612 筆，停止更新並回報。
- 若表格欄位缺失、TargetFramework 為空或狀態不是「有／缺少」，停止更新並
  回報。
- 若 README 既有索引無法唯一定位，停止替換，避免誤改其他內容。
- GitHub Issue 建立前先檢查是否已有同目的的未關閉 Issue，避免重複建立。
- Issue 建立後讀回標題與內容，確認統計及清單完整。

## 驗證

- README 索引資料列共 608 筆。
- README 每列皆有 8 欄，題目資料夾與專案檔連結均符合稽核路徑。
- README 題目主專案的 `TargetFramework` 分布為 `net8.0` 294、`net9.0` 6、
  `net10.0` 308。
- 稽核內全部 612 個專案的 `SDK-style` 均為「是」。
- README 與 Issue 的缺失統計均為：
  `.editorconfig` 191、`.gitignore` 175、`README.md` 164。
- Issue 每個受影響專案只出現一次，且列出的缺失集合與稽核明細一致。
- `git diff --check` 通過，且修改範圍只有設計文件與後續核准的 README。
