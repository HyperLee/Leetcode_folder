# leetcode_1464 .NET 10 遷移執行計畫

## Goal

將 LeetCode 1464 從舊式 .NET Framework 專案遷移為已文件化、可重現驗證的 .NET 10 主控台
專案，並整理為一個 `feat(leetcode-1464): migrate project to .NET 10` commit。

## 實作步驟

1. 盤點舊專案與三個 confirmed legacy files，保留 `MaxProduct(int[])` API。
2. 替換為 SDK-style `net10.0` 專案，先寫八案例的輸入不變性 harness，移除生產 API，建置確認
   `CS0103` RED。
3. 以一趟最大／次大值掃描加入最小實作，重新建置與執行，確認八項 PASS 與 exit code 0。
4. 只在 GREEN 後補上雙語 XML、繁中 README、AGENTS、問題根目錄 VS Code 設定與遷移設計紀錄。
5. 從 repository root 執行 JSON、build、run、README transcript diff、fence count、whitespace、
   scope 與 legacy absence 檢查；進行唯讀 review。
6. 暫存僅 `leetcode_1464/` 的檔案，檢查 staged whitespace 後建立唯一功能 commit。

## Acceptance Criteria

- `.csproj` 為 SDK-style executable，且啟用 `net10.0`、implicit usings 與 nullable。
- `MaxProduct` 不排序、不修改輸入，使用 O(n) 時間及 O(1) 結果／輔助空間。
- 八個指定案例均列印 Case、Input、Expected、Actual、Result，並精確結尾為
  `Summary: 8/8 checks passed.`。
- README 的唯一 `text` fence 與 fresh run 完全一致；所有 tracked paths 都位於此題資料夾。
