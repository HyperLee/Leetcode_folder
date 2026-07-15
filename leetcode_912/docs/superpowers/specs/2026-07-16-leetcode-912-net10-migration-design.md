# leetcode_912 .NET 10 遷移設計

## 目標與範圍

將 LeetCode 912「Sort an Array／排序陣列」從舊式 .NET Framework 4.8 主控台專案遷移為可在 .NET 10 建置、執行與驗證的單題專案。所有 tracked changes 僅限 `leetcode_912/`，發佈、PR、合併與 Issue #2 更新不屬於本地實作範圍。

## 專案與演算法

- 使用 SDK-style `net10.0` executable project，啟用 implicit usings 與 nullable。
- 公開 API 固定為 `public static int[] SortArray(int[] nums)`，直接修改並回傳原陣列參考。
- 由 `nums.Length / 2 - 1` 向前呼叫迭代式 `SiftDown` 建立最大堆。
- 每輪把堆頂最大值交換到未排序尾端，縮小堆範圍，再從根節點恢復最大堆。
- 不使用內建排序、LINQ ordering、第三方套件、隨機化或遞迴，也不加入題目有效輸入以外的行為。
- 最壞時間複雜度為 `O(n log n)`，結果空間為 `O(1)`，輔助空間為 `O(1)`。

## Acceptance Harness

`Main` 是唯一 console I/O 邊界。它依序執行兩個官方範例、單一元素、已排序、反向排序、全重複、重複邊界值與 50,000 元素上限案例。上限案例分別檢查完整遞增結果與回傳參考身分，因此八個案例共九項檢查。每項輸出案例名稱、Input、Expected、Actual 與 PASS/FAIL；任一失敗設定 `Environment.ExitCode = 1`，成功結尾固定為 `Summary: 9/9 checks passed.`。

## 文件與驗證

題目根目錄補齊共用 Git/editor 設定、無提示 VS Code build/debug、`AGENTS.md`、繁中 `README.md`、README 範本與 Superpowers 文件。逐檔移除舊 `.sln`、`App.config` 與 `Properties/AssemblyInfo.cs`。先以 harness 呼叫不存在的 `SortArray` 觀察 `CS0103` RED，再加入最大堆排序得到 GREEN；最終驗證 JSON、build、run、README transcript、唯一 `text` fence、whitespace、scope 與 legacy absence。

## 自審重點

- API、原地修改、同參考回傳、最大堆不變量及複雜度均有程式與 harness 證據。
- 大型案例只摘要輸出，expected 由明確序列產生，不依賴任何排序 oracle。
- 文件沒有 placeholder、第三方 dependency、正式測試專案或目標資料夾外變更。
