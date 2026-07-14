# LeetCode 744：Find Smallest Letter Greater Than Target／尋找比目標字母大的最小字母

這是一個以 C# 撰寫的 .NET 10 主控台專案。核心方法
`NextGreatestLetter` 使用 lower-bound 二分搜尋找出第一個嚴格大於目標的字元；
`Main` 則負責可重複執行的 acceptance harness 與所有主控台輸出。

- [英文題目：744. Find Smallest Letter Greater Than Target](https://leetcode.com/problems/find-smallest-letter-greater-than-target/)
- [中文題目：744. 寻找比目标字母大的最小字母](https://leetcode.cn/problems/find-smallest-letter-greater-than-target/)

## 題目說明

給定依非遞減順序排列的字元陣列 `letters` 與目標字元 `target`，回傳最小且嚴格大於
`target` 的字元。若所有字元都不大於 `target`，答案要環繞並回傳 `letters[0]`。

## 限制條件

- `2 <= letters.Length <= 10^4`
- `letters[i]` 與 `target` 均為小寫英文字母。
- `letters` 依非遞減順序排列。

實作只處理題目定義的有效輸入，不另外建立無效輸入例外或替代語意。

## Lower-bound 二分搜尋不變量

搜尋區間採半開區間 `[low, high)`。每次迴圈開始時，所有小於 `low` 的位置都已證明
不可能嚴格大於 `target`，而第一個可能答案仍在 `[low, high)`：

1. 若 `letters[middle] > target`，`middle` 可能就是最小答案，因此令 `high = middle`。
2. 否則 `middle` 及其左側都不符合嚴格大於條件，因此令 `low = middle + 1`。
3. 收斂時 `low` 是第一個合法答案；若它等於長度，`low % letters.Length` 會正確環繞為零。

重複字元不能提早回傳。例如 `['a', 'a', 'b', 'c', 'c']` 與目標 `'a'` 必須回傳
`'b'`，所以比較必須是嚴格的 `>`。

公開 API 為：

```csharp
public static char NextGreatestLetter(char[] letters, char target)
```

它只回傳答案，不會修改輸入，也不會直接輸出到主控台。

## 官方案例逐步走查

以 `letters = ['c', 'f', 'j']`、`target = 'd'` 為例：

| 步驟 | `[low, high)` | `middle` | `letters[middle]` | 判斷 | 下一個區間 |
| ---: | --- | ---: | --- | --- | --- |
| 1 | `[0, 3)` | 1 | `'f'` | `'f' > 'd'`，保留較小候選 | `[0, 1)` |
| 2 | `[0, 1)` | 0 | `'c'` | `'c' <= 'd'`，排除左側 | `[1, 1)` |

收斂於索引 `1`，因此答案是 `'f'`。目標為 `'j'` 時會收斂於索引 `3`，再透過取模環繞為索引 `0`，答案是 `'c'`。

## 複雜度

- 時間複雜度：`O(log n)`，每輪把搜尋區間至少縮小一半。
- 結果空間：`O(1)`，只回傳一個字元。
- 輔助空間：`O(1)`，僅使用固定數量索引變數。

## 可執行驗證案例

`Main` 提供 8 組案例、共 9 項檢查。每項都輸出 Input、Expected、Actual 與
PASS/FAIL；任何失敗都會設定 `Environment.ExitCode = 1`。本題沒有獨立的 test
project，acceptance harness 是目前的驗證機制。

| 案例 | 輸入 | 檢查數 | 驗證重點 |
| --- | --- | ---: | --- |
| 1–5 | `['c', 'f', 'j']` 與 `'a'`、`'c'`、`'d'`、`'g'`、`'j'` | 5 | 官方輸出、嚴格比較與環繞 |
| 6 | `['a', 'z']`，目標 `'a'` | 1 | 最小有效長度 |
| 7 | `['a', 'a', 'b', 'c', 'c']`，目標 `'a'` | 1 | 重複字元不會被誤判為答案 |
| 8 | 9,999 個 `'m'` 後接 `'z'` | 2 | 10,000 長度上限與環繞 spot checks |

## 建置與執行

請從此 README 所在的外層 `leetcode_744` 目錄執行：

```bash
dotnet build leetcode_744/leetcode_744.csproj --nologo
dotnet run --no-build --project leetcode_744/leetcode_744.csproj
```

以下是完成建置後執行第二個命令的完整輸出：

```text
LeetCode 744 acceptance harness

Case 1: Official example (target = 'a')
Input: letters = [c, f, j], target = 'a'
PASS | Next greatest letter | Expected: 'c' | Actual: 'c'

Case 2: Official example (target = 'c')
Input: letters = [c, f, j], target = 'c'
PASS | Next greatest letter | Expected: 'f' | Actual: 'f'

Case 3: Official example (target = 'd')
Input: letters = [c, f, j], target = 'd'
PASS | Next greatest letter | Expected: 'f' | Actual: 'f'

Case 4: Official example (target = 'g')
Input: letters = [c, f, j], target = 'g'
PASS | Next greatest letter | Expected: 'j' | Actual: 'j'

Case 5: Official example (target = 'j')
Input: letters = [c, f, j], target = 'j'
PASS | Next greatest letter | Expected: 'c' | Actual: 'c'

Case 6: Minimum valid length
Input: letters = [a, z], target = 'a'
PASS | Next greatest letter | Expected: 'z' | Actual: 'z'

Case 7: Duplicate letters
Input: letters = [a, a, b, c, c], target = 'a'
PASS | Next greatest letter | Expected: 'b' | Actual: 'b'

Case 8: Maximum-length spot checks
Input: letters = 9,999 × 'm' followed by 'z'
PASS | Target = 'm' | Expected: 'z' | Actual: 'z'

Case 8: Maximum-length spot checks
Input: letters = 9,999 × 'm' followed by 'z'
PASS | Target = 'z' | Expected: 'm' | Actual: 'm'

Summary: 9/9 checks passed.
```

## 專案結構

```plaintext
.
├── .editorconfig              # C# 與結構化檔案格式規範
├── .gitattributes             # 文字與二進位檔案屬性
├── .gitignore                 # .NET／IDE 產生檔案排除規則
├── .vscode/
│   ├── launch.json            # 直接偵錯 net10.0 輸出
│   └── tasks.json             # 預設建置工作
├── AGENTS.md                  # 本題協作指南
├── README.md                  # 題目、解法與驗證紀錄
├── docs/
│   ├── readme-template.md     # 初次建立 README 的範本
│   └── superpowers/
│       ├── plans/
│       │   └── 2026-07-15-leetcode-744-net10-migration.md
│       └── specs/
│           └── 2026-07-15-leetcode-744-net10-migration-design.md
└── leetcode_744/
    ├── Program.cs             # 純二分搜尋解法與 acceptance harness
    └── leetcode_744.csproj    # .NET 10 SDK 專案設定
```
