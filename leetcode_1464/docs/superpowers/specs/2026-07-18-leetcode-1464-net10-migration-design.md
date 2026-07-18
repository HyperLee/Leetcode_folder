# leetcode_1464 .NET 10 遷移設計

## 目標與範圍

將 LeetCode 1464「Maximum Product of Two Elements in an Array／陣列中兩個元素的最大乘積」
遷移為獨立、SDK-style 的 .NET 10 console project。所有 tracked changes 僅限
`leetcode_1464/`；VS Code 與文件都把該資料夾視為 workspace root，巢狀專案路徑為
`leetcode_1464/leetcode_1464.csproj`。

## 核准的解法

公開 API 固定為 `public static int MaxProduct(int[] nums)`。單趟掃描以 `largest` 與
`secondLargest` 維持已掃描元素前兩大值：新值大於最大值時，原最大值下移；否則若大於
次大值即取代次大值。掃描後回傳 `(largest - 1) * (secondLargest - 1)`。

此設計不排序、不修改輸入，也不在 API 內輸出或新增有效題目範圍外的例外。時間複雜度為
`O(n)`；結果空間與輔助空間都是 `O(1)`。

## Acceptance Harness

`Main` 執行八個確定性案例：三個官方範例、最小與最大值、最大值先到的次大值回歸、一般未排序
回歸，以及 500 個元素的上限案例。每次呼叫前複製完整陣列，PASS 同時要求期望值相符與輸入陣列
未變。上限案例只列印長度和值域摘要。全綠時結尾必須為 `Summary: 8/8 checks passed.`；任何
失敗設置非零 exit code。

## TDD 與驗證

先將專案殼改為 net10.0 並建立最終 harness，但故意不定義 `MaxProduct`，以 `CS0103` 作為
有效 RED。接著加入最小一趟實作取得 GREEN，最後才加入 XML、工具設定與繁體中文文件。發布前
驗證 JSON、build、run、README transcript、唯一 `text` fence、whitespace、scope 與 legacy
file absence，並執行唯讀自我審查。
