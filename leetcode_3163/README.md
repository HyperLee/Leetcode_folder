# LeetCode 3163 — String Compression III

[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![LeetCode 3163](https://img.shields.io/badge/LeetCode-3163-FFA116?logo=leetcode&logoColor=white)](https://leetcode.com/problems/string-compression-iii/)

這是一個以 .NET 10 撰寫的主控台教學專案，實作 LeetCode 3163「String Compression III」。專案保留兩種線性時間解法，並由 `Main` 執行固定案例，直接比較 Expected、Actual 與 PASS/FAIL。

## 快速導覽

- [題目說明](#題目說明)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：逐字累計並即時輸出](#解法一逐字累計並即時輸出)
- [解法二：雙指標分組後拆塊](#解法二雙指標分組後拆塊)
- [兩種解法比較](#兩種解法比較)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)

## 題目說明

給定字串 `word`，從左到右反覆取出最長的相同字元前綴，但每次最多只能取 9 個字元。每取出一段，就把「該段長度」與「字元」依序附加到壓縮字串 `comp`，最後回傳 `comp`。

例如，14 個連續的 `a` 不能寫成 `14a`，因為每一段的長度必須是單一數字；它必須拆成 `9a5a`。

### 輸入與輸出

- 輸入：非空字串 `word`。
- 輸出：由多組「一位數長度＋原字元」組成的壓縮字串。
- 每組長度介於 1 到 9，因此壓縮結果不需要額外分隔符號。

### 官方範例

| 輸入 | 輸出 | 說明 |
|---|---|---|
| `abcde` | `1a1b1c1d1e` | 每個字元都各自形成長度 1 的區段。 |
| `aaaaaaaaaaaaaabb` | `9a5a2b` | 14 個 `a` 拆成 9 個與 5 個，再輸出 2 個 `b`。 |

### 限制條件

- `1 <= word.Length <= 2 * 10^5`
- `word` 只包含小寫英文字母。
- 本專案依題目契約實作，不額外處理 `null`、空字串或其他字元。

題目連結：[3163. String Compression III](https://leetcode.com/problems/string-compression-iii/)

## 解題概念與出發點

這題的核心不是一般的 Run-Length Encoding，而是多了一條「單一區段最多 9 個字元」的限制。因此，不論採用哪一種掃描方式，都必須維持以下不變條件：

1. 字元必須依照原字串順序處理。
2. 相鄰且相同的字元可以合併，但每次輸出的數量不能超過 9。
3. 遇到不同字元、字串結尾或數量到達 9 時，必須結束目前輸出區段。

兩種解法的差別在於切割時機：解法一邊掃描邊決定何時輸出；解法二先找出完整的同字元區段，再把區段拆成合法大小。

## 解法一：逐字累計並即時輸出

### 設計說明

`CompressedString` 使用一個 `count` 記錄目前壓縮區段已累計多少個字元。迴圈每讀到一個字元就遞增 `count`，並檢查以下三個輸出條件：

1. `count == 9`：已達單一壓縮區段的最大長度，必須立即輸出。
2. 已到字串最後一個位置：沒有下一輪可以處理目前區段，必須輸出。
3. 下一個字元與目前字元不同：連續區段已結束，必須在切換字元前輸出。

輸出後把 `count` 歸零。下一個字元無論是同一字元的第 10 個，或是全新的字元，都會從新的壓縮區段重新計數。

### 範例演示：`aaaaaaaaaaaaaabb`

| 掃描範圍 | 狀態與判斷 | 新增輸出 | 累積結果 |
|---|---|---|---|
| 索引 0～8 | 累計到第 9 個 `a`，觸發數量上限 | `9a` | `9a` |
| 索引 9～13 | 再累計 5 個 `a`，下一字元變成 `b` | `5a` | `9a5a` |
| 索引 14～15 | 累計 2 個 `b`，抵達字串結尾 | `2b` | `9a5a2b` |

這個流程只保存目前字元的計數，不需要先知道整個連續區段有多長。

### 正確性理由

- 只有在到達 9、遇到字元邊界或抵達結尾時才輸出，因此不會提早切斷小於 9 的合法同字元區段。
- 到達 9 時強制輸出，保證每組長度都能用一位數表示。
- 每個輸入字元只被計數一次，且輸出順序與輸入順序一致，所以不會遺漏、重複或重新排列字元。

### 複雜度

- 時間複雜度：`O(n)`，每個字元只掃描一次。
- 輸出空間：`O(n)`。
- 不計回傳用的 `StringBuilder` 時，額外工作空間為 `O(1)`。

## 解法二：雙指標分組後拆塊

### 設計說明

`CompressedString2` 使用 `[left, right)` 表示一段完整的連續相同字元：

1. `left` 指向區段的第一個字元。
2. `right` 持續向右移動，直到遇到不同字元或字串結尾。
3. `right - left` 就是完整區段長度。
4. 使用 `Math.Min(9, remaining)`，把完整區段拆成每批最多 9 個。
5. 區段處理完成後令 `left = right`，開始尋找下一段。

這個版本將「找出連續區段」與「依規則拆分區段」分成兩個清楚階段，適合用來理解題目規則與一般 Run-Length Encoding 的差異。

### 範例演示：`aaaaaaaaaaaaaabb`

| 雙指標區段 | 完整段長 | 拆分方式 | 累積結果 |
|---|---:|---|---|
| `[0, 14)`，字元為 `a` | 14 | 先取 9，再取剩餘 5 | `9a5a` |
| `[14, 16)`，字元為 `b` | 2 | 直接取 2 | `9a5a2b` |

以第一段為例，`remaining` 起初是 14：

- `chunkLength = min(9, 14) = 9`，輸出 `9a`，剩餘 5。
- `chunkLength = min(9, 5) = 5`，輸出 `5a`，剩餘 0。
- 剩餘數量歸零後，移動到下一個完整區段。

### 正確性理由

- 內層雙指標掃描會找到從 `left` 開始的最大同字元區段，不會跨越字元邊界。
- 每次從 `remaining` 取出不超過 9 的正數，因此所有輸出區段都符合題目限制。
- 每輪扣除的數量總和恰好等於原區段長度，所以每個字元都會被輸出一次，且原有順序不變。

### 複雜度

- 時間複雜度：`O(n)`。`right` 只會向右走過每個字元一次；拆塊次數也受輸入長度線性限制。
- 輸出空間：`O(n)`。
- 不計回傳用的 `StringBuilder` 時，額外工作空間為 `O(1)`。

## 兩種解法比較

| 比較項目 | 解法一：逐字累計 | 解法二：雙指標分組 |
|---|---|---|
| 公開方法 | `CompressedString` | `CompressedString2` |
| 切割時機 | 掃描途中遇到三種條件就輸出 | 先取得完整區段，再依 9 拆分 |
| 主要狀態 | 目前區段計數 `count` | 區段邊界與剩餘長度 |
| 時間複雜度 | `O(n)` | `O(n)` |
| 額外工作空間 | `O(1)` | `O(1)` |
| 教學重點 | 邊界判斷與即時輸出 | 區段辨識與規則化拆塊 |

## 可執行測試資料

`Main` 會對兩種解法分別執行以下 6 組案例，共進行 12 項驗證。任一結果不符預期時，程式會顯示 `FAIL` 並設定非零退出碼。

| 案例 | 輸入 | Expected | 驗證重點 |
|---:|---|---|---|
| 1 | `abcde` | `1a1b1c1d1e` | 官方範例、每段長度為 1 |
| 2 | `aaaaaaaaaaaaaabb` | `9a5a2b` | 官方範例、長區段拆分 |
| 3 | `x` | `1x` | 最小合法輸入 |
| 4 | `aaaaaaaaa` | `9a` | 剛好到達上限 9 |
| 5 | `aaaaaaaaaa` | `9a1a` | 超過上限一個字元 |
| 6 | `aaabbaa` | `3a2b2a` | 字元切換及相同字元分段重現 |

## 專案結構

```text
leetcode_3163/
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_3163.sln
└── leetcode_3163/
    ├── leetcode_3163.csproj
    └── Program.cs
```

## 建置與執行

### 環境需求

- .NET 10 SDK

從此 repository 根目錄依序執行：

```bash
dotnet restore leetcode_3163/leetcode_3163.csproj
dotnet build leetcode_3163/leetcode_3163.csproj --nologo
dotnet run --project leetcode_3163/leetcode_3163.csproj
```

目前沒有獨立的自動化測試專案；驗收方式是成功建置，再執行 `Main` 中的固定案例。建置結果應為 0 個警告、0 個錯誤；兩種解法應各自通過 `6/6` 案例，總計為 `12/12` 項驗證通過。

## 實際執行結果

以下內容來自 `dotnet run --project leetcode_3163/leetcode_3163.csproj`：

```text
LeetCode 3163 - String Compression III

案例 1：官方範例：每個字元皆不同
輸入："abcde"
Expected："1a1b1c1d1e"
解法一 Actual："1a1b1c1d1e" => PASS
解法二 Actual："1a1b1c1d1e" => PASS

案例 2：官方範例：連續字元超過 9 個
輸入："aaaaaaaaaaaaaabb"
Expected："9a5a2b"
解法一 Actual："9a5a2b" => PASS
解法二 Actual："9a5a2b" => PASS

案例 3：最小合法輸入
輸入："x"
Expected："1x"
解法一 Actual："1x" => PASS
解法二 Actual："1x" => PASS

案例 4：連續字元剛好 9 個
輸入："aaaaaaaaa"
Expected："9a"
解法一 Actual："9a" => PASS
解法二 Actual："9a" => PASS

案例 5：連續字元超過上限一個
輸入："aaaaaaaaaa"
Expected："9a1a"
解法一 Actual："9a1a" => PASS
解法二 Actual："9a1a" => PASS

案例 6：相同字元分段重現
輸入："aaabbaa"
Expected："3a2b2a"
解法一 Actual："3a2b2a" => PASS
解法二 Actual："3a2b2a" => PASS

解法一：6/6 案例通過
解法二：6/6 案例通過
總結：12/12 項驗證通過
```
