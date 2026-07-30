# LeetCode 125 — Valid Palindrome（驗證回文串）

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![Language](https://img.shields.io/badge/Language-C%23-239120)

這是一個以 C# 與 .NET 10 實作的主控台教學範例。專案使用左右雙指針直接掃描原始字串，判斷忽略非英數字元與大小寫後，內容是否仍為回文。

- [題目說明](#題目說明)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：原字串雙指針](#解法一原字串雙指針)
- [範例演示流程](#範例演示流程)
- [建置與執行](#建置與執行)

## 題目說明

[LeetCode 125. Valid Palindrome](https://leetcode.com/problems/valid-palindrome/)

給定字串 `s`，先將大寫英文字母視為對應的小寫字母，並忽略所有非英數字元。若處理後的字串由左至右與由右至左讀取皆相同，回傳 `true`；否則回傳 `false`。

英數字元包含英文字母與數字。例如：

- `"A man, a plan, a canal: Panama"` 忽略空白、逗號、冒號與大小寫後為 `amanaplanacanalpanama`，因此是回文。
- `"race a car"` 處理後為 `raceacar`，因此不是回文。
- `" "` 不包含有效英數字元，處理後相當於空字串，因此是回文。

## 限制條件

- `1 <= s.Length <= 2 * 10^5`
- `s` 僅包含可列印 ASCII 字元。

> [!NOTE]
> 可執行範例另外加入 `""`，用來確認方法面對空字串時仍能安全回傳；這是健全性檢查，不屬於 LeetCode 的正式輸入範圍。

## 解題概念與出發點

最直觀的作法是先建立一個只保留英數字元、且全部轉成小寫的新字串，再反轉或從兩端比較。然而這需要額外的 `O(n)` 空間。

本專案直接在原始字串上放置兩個指針：

- `left` 從字串開頭向右移動。
- `right` 從字串結尾向左移動。
- 指針遇到非英數字元時跳過，不把該字元納入比較。
- 兩端都停在有效字元後，忽略大小寫進行比較。
- 若兩端不同，立即回傳 `false`；若相同，兩個指針繼續向中央收斂。
- 當 `left >= right`，代表所有需要配對的字元均已通過檢查，回傳 `true`。

這個出發點保留了雙指針解法的線性掃描效率，也避免建立清理後字串或反轉字串。

## 解法一：原字串雙指針

### 設計流程

1. 將 `left` 設為 `0`，`right` 設為 `s.Length - 1`。
2. 當 `left < right` 時持續檢查：
   - 若 `s[left]` 不是字母或數字，遞增 `left`。
   - 若 `s[right]` 不是字母或數字，遞減 `right`。
3. 使用 `char.ToLower` 將左右有效字元轉為相同大小寫後比較。
4. 若不同，已找到破壞回文條件的一對字元，直接回傳 `false`。
5. 若相同，遞增 `left` 並遞減 `right`，繼續比較下一對。
6. 迴圈結束後回傳 `true`。

### 正確性要點

- 非英數字元不影響回文結果，因此移動指針略過即可。
- 每一輪都比較目前最外側尚未處理的有效字元。
- 任何一對有效字元不同時，字串必定不是回文，可以提早結束。
- 若指針相遇或交錯前都沒有失敗，所有對稱位置均相同，因此字串是回文。

### 複雜度

- 時間複雜度：`O(n)`。左右指針各自最多掃過字串一次。
- 額外空間複雜度：`O(1)`。只使用兩個指針與少量區域變數，沒有建立與輸入長度相關的新集合或字串。

## 範例演示流程

### 回文案例：`"A man, a plan, a canal: Panama"`

忽略非英數字元並統一大小寫後，可視為：

```text
amanaplanacanalpanama
```

| 比較順序 | 左側有效字元 | 右側有效字元 | 判斷 |
| --- | --- | --- | --- |
| 1 | `A` → `a` | `a` | 相同，繼續 |
| 2 | `m` | `m` | 相同，繼續 |
| 3 | `a` | `a` | 相同，繼續 |
| 4 | `n` | `n` | 相同，繼續 |
| 5 | 略過逗號與空白後取得 `a` | `a` | 相同，繼續 |
| 後續 | 依序比較 `p/p`、`l/l`、`a/a`、`n/n`、`a/a` |  | 全部相同 |
| 結束 | 指針在中央字元 `c` 相遇 |  | 回傳 `true` |

### 非回文案例：`"race a car"`

處理後可視為 `raceacar`。前三組有效字元分別為 `r/r`、`a/a`、`c/c`，均相同；下一組是 `e/a`，兩端不同，因此立即回傳 `false`，不必再檢查其餘位置。

## 可執行案例

`Main` 會執行七筆固定案例，逐筆比較預期值與實際值：

| 案例 | 輸入 | 預期 | 驗證目的 |
| ---: | --- | :---: | --- |
| 1 | `"A man, a plan, a canal: Panama"` | `true` | 官方回文案例；包含大小寫、空白與標點 |
| 2 | `"race a car"` | `false` | 官方非回文案例 |
| 3 | `" "` | `true` | 官方案例；只有被忽略的字元 |
| 4 | `""` | `true` | 正式限制外的空字串健全性檢查 |
| 5 | `".,!?"` | `true` | 只有標點符號 |
| 6 | `"0P"` | `false` | 數字與字母不相同 |
| 7 | `"No 'x' in Nixon"` | `true` | 混合大小寫、空白與標點的回文 |

## 建置與執行

請從 `leetcode_125` 儲存庫根目錄執行：

```bash
dotnet restore leetcode_125/leetcode_125.csproj
dotnet build leetcode_125/leetcode_125.csproj --nologo --no-restore
dotnet run --project leetcode_125/leetcode_125.csproj --no-build
```

此專案目前沒有獨立的自動化測試專案；驗收方式為成功建置，並執行 `Main` 中會自行比對預期與實際結果的案例。

## 實際執行結果

```text
案例 1
輸入："A man, a plan, a canal: Panama"
預期：True
實際：True
結果：PASS

案例 2
輸入："race a car"
預期：False
實際：False
結果：PASS

案例 3
輸入：" "
預期：True
實際：True
結果：PASS

案例 4
輸入：""
預期：True
實際：True
結果：PASS

案例 5
輸入：".,!?"
預期：True
實際：True
結果：PASS

案例 6
輸入："0P"
預期：False
實際：False
結果：PASS

案例 7
輸入："No 'x' in Nixon"
預期：True
實際：True
結果：PASS

總結：7/7 筆測試通過
```

## 專案結構

```text
leetcode_125/
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_125.sln
└── leetcode_125/
    ├── Program.cs
    └── leetcode_125.csproj
```
