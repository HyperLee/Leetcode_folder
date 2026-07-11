# LeetCode 443：String Compression／壓縮字串

這是一個以 C# 撰寫的 .NET 10 主控台專案。公開的 `Compress` 方法在輸入
`char[]` 的同一個緩衝區內寫入壓縮前綴並回傳其長度；`Main` 則提供可重複
執行的 acceptance harness，涵蓋單字元、多個 run、十進位計數與題目上限。

- [英文題目：443. String Compression](https://leetcode.com/problems/string-compression/)
- [中文題目：443. 壓縮字串](https://leetcode.cn/problems/string-compression/)

## 題目說明

給定字元陣列 `chars`，把每一段連續相同的字元（run）原地壓縮：先保留該
字元；若 run 長度大於 1，再緊接寫入其十進位計數。方法回傳壓縮後前綴的長度，
判題程式只會讀取 `chars[0..length)`。

例如 `['a','a','b','b','c','c','c']` 壓縮為前綴
`['a','2','b','2','c','3']`，並回傳 `6`。run 長度為 1 時不寫入 `1`。

## 限制條件

- `1 <= chars.length <= 2000`
- `chars[i]` 可以是英文字母、數字或符號。
- 解法依照 LeetCode 的有效輸入契約設計，不另外定義無效輸入行為。

## 讀寫指標與反轉數字的不變量

- `read`：尚未處理區段的起點；每次向右掃描到一個連續 run 的結尾。
- `write`：下一個可寫入位置；在每輪結束後，`chars[0..write)` 恰好是所有
  已處理 runs 的正確壓縮前綴。
- `runLength`：以 `read - runStart` 計算；只有大於 1 才將計數寫入前綴。
- 計數以 `% 10` 依個位數到高位數寫入，所以暫時是反向的；立刻用 `Reverse`
  反轉剛寫入的範圍，便恢復正常十進位順序，例如 `12` 會先寫成 `2, 1`，再變成
  `1, 2`。

這些不變量讓輸入與輸出共用同一個陣列而不覆寫尚未讀取的資料：`write` 從不會
超過已完成掃描的 `read` 範圍。

## `aabcccccaaa` 走查

| 已完成的 run | `runLength` | 寫入內容 | 壓縮前綴 |
| --- | ---: | --- | --- |
| `aa` | 2 | `a`、`2` | `a2` |
| `b` | 1 | `b` | `a2b` |
| `ccccc` | 5 | `c`、`5` | `a2bc5` |
| `aaa` | 3 | `a`、`3` | `a2bc5a3` |

因此方法回傳 `7`，而有效結果就是陣列前七個位置的 `a2bc5a3`。

## 複雜度

- 時間複雜度：`O(n)`。`read` 對每個輸入字元只前進一次；計數位數的處理總量
  也不超過輸入長度。
- 額外輔助空間：`O(1)`。除少量指標與計數變數外，不配置與輸入長度成比例的
  演算法空間；輸出直接寫回傳入的陣列。

## 可執行驗證案例

`Main` 會複製每組輸入，並同時比較回傳長度與壓縮前綴：

| 案例 | 輸入 | 預期壓縮前綴 |
| --- | --- | --- |
| 1：Repeated runs | `aabbccc` | `a2b2c3` |
| 2：Single character | `a` | `a` |
| 3：Mixed run lengths | `aabcccccaaa` | `a2bc5a3` |
| 4：Two-digit letter count | 12 個 `a` | `a12` |
| 5：Digit character with two-digit count | 12 個 `'1'` | `112` |
| 6：Four-digit count | 2000 個 `a` | `a2000` |

第六案只顯示長度與重複字元，不會把 2000 個字元輸出到主控台。若任何案例失敗，
程式會將 `Environment.ExitCode` 設為 1。

## 建置與執行

請從此 README 所在的外層 `leetcode_443` 目錄執行：

```bash
dotnet build leetcode_443/leetcode_443.csproj --nologo
dotnet run --no-build --project leetcode_443/leetcode_443.csproj
```

以下是重新建置後執行第二個命令的完整輸出：

```text
LeetCode 443 acceptance harness

Case 1: Repeated runs
Input: aabbccc
Expected: a2b2c3 (length 6)
Actual: a2b2c3 (length 6)
PASS

Case 2: Single character
Input: a
Expected: a (length 1)
Actual: a (length 1)
PASS

Case 3: Mixed run lengths
Input: aabcccccaaa
Expected: a2bc5a3 (length 7)
Actual: a2bc5a3 (length 7)
PASS

Case 4: Two-digit letter count
Input: aaaaaaaaaaaa
Expected: a12 (length 3)
Actual: a12 (length 3)
PASS

Case 5: Digit character with two-digit count
Input: 111111111111
Expected: 112 (length 3)
Actual: 112 (length 3)
PASS

Case 6: Four-digit count
Input: 2000 'a' characters
Expected: a2000 (length 5)
Actual: a2000 (length 5)
PASS

Summary: 6/6 checks passed.
```

## 專案結構

```plaintext
.
├── .editorconfig              # C# 與結構化檔案的格式規範
├── .gitattributes             # 文字與二進位檔案屬性
├── .gitignore                 # .NET／IDE 產生檔案排除規則
├── .vscode/
│   ├── launch.json            # 直接偵錯 net10.0 輸出
│   └── tasks.json             # 預設建置工作
├── docs/
│   └── readme-template.md     # 初次建立 README 的範本
├── leetcode_443/
│   ├── Program.cs             # 純壓縮解法與可執行驗證器
│   └── leetcode_443.csproj    # .NET 10 SDK 專案設定
├── AGENTS.md                  # 本專案協作指南
└── README.md                  # 題目、解法與驗證紀錄
```
