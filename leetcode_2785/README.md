# LeetCode 2785：將字串中的母音字母排序

這是一個以 C# 撰寫的 .NET 10 主控台專案。它保留 LeetCode 的
`public static string SortVowels(string s)` 契約：只排序字串中的母音，
所有非母音字元都保留在原本的位置。`Main` 則是無輸入、可重複執行的
acceptance harness，負責顯示驗證結果。

- [英文題目：2785. Sort Vowels in a String](https://leetcode.com/problems/sort-vowels-in-a-string/)
- [中文題目：2785. 將字串中的母音字母排序](https://leetcode.cn/problems/sort-vowels-in-a-string/)

## 題目契約

給定字串 `s`，找出全部母音 `A E I O U a e i o u`，依 ASCII 遞增順序
排序，再依序寫回原本的母音位置。每個非母音字元的位置與值都不能改變。
實作遵循 LeetCode 的有效輸入契約，不另外發明無效輸入的行為。

例如輸入 `lEetcOde` 的母音依出現順序為 `E e O e`，ASCII 排序後為
`E O e e`，放回相同的四個母音位置，結果是 `lEOtcede`。

## 演算法與不變量

`SortVowels` 採用原本的三階段方法：

1. 掃描字元陣列，收集每個母音到清單。
2. 對母音清單使用 `Sort()`，得到 ASCII 遞增順序。
3. 再次掃描原陣列；只在母音位置依序取出排序後的母音寫回。

核心不變量是：第二次掃描時，非母音位置從不寫入；第 `k` 個母音位置一定
接收排序後母音清單的第 `k` 個元素。因此非母音位置保持不變，母音位置的
集合與數量保持不變，只改變其順序。

`IsVowel` 直接檢查 `AEIOUaeiou` 的十個 ASCII 成員，不使用依文化特性的
大小寫轉換。這讓 `tr-TR` 文化下的 `I` 仍被識別為母音，避免舊版
`char.ToLower` 造成的錯誤。

## 複雜度

令 `n` 為字串長度、`v` 為母音數量：

- 時間複雜度：`O(n + v log v)`；兩次掃描共 `O(n)`，母音排序為
  `O(v log v)`。
- 輔助／結果儲存：`O(n + v)`；字元結果儲存需要 `O(n)`，母音清單需要
  `O(v)`。

## 走查：`AaEe`

原字串的每個位置都是母音，收集結果為 `A a E e`。依 ASCII 排序後為
`A E a e`，再依序放回四個母音位置，得到 `AEae`。

```plaintext
輸入位置： 0 1 2 3
原字元：   A a E e
排序母音： A E a e
結果：     A E a e
```

## 可執行驗證案例

`Main` 共執行六個案例、九項檢查：

| 案例 | 輸入 | 檢查數 | 驗證內容 |
| ---: | --- | ---: | --- |
| 1 | `lEetcOde` | 1 | 混合大小寫母音的排序結果為 `lEOtcede` |
| 2 | `lYmpH` | 1 | 沒有母音時字串完全不變 |
| 3 | `a` | 1 | 單一母音維持不變 |
| 4 | `AaEe` | 1 | ASCII 排序結果為 `AEae` |
| 5 | `IbA`（`tr-TR`） | 1 | 文化特性不影響 `I` 的母音判斷，結果為 `AbI` |
| 6 | 50000 個 `u` 後接 50000 個 `A` | 4 | 長度、前半段、後半段、完整精確結果 |

大型案例的精確預期結果是 50000 個 `A` 後接 50000 個 `u`；程式不會把
十萬個字元完整印出，僅輸出每項斷言的摘要。

## 建置與執行

請從本 README 所在的外層 `leetcode_2785` 目錄執行：

```plaintext
dotnet build leetcode_2785/leetcode_2785.csproj --nologo
dotnet run --no-build --project leetcode_2785/leetcode_2785.csproj
```

以下是重新建置後執行第二個命令的完整輸出：

```text
LeetCode 2785 acceptance harness

Case 1: Mixed-case vowel sorting
Input: lEetcOde
PASS | Sorted result | Expected: lEOtcede | Actual: lEOtcede

Case 2: No vowels
Input: lYmpH
PASS | Sorted result | Expected: lYmpH | Actual: lYmpH

Case 3: Single vowel
Input: a
PASS | Sorted result | Expected: a | Actual: a

Case 4: ASCII order
Input: AaEe
PASS | Sorted result | Expected: AEae | Actual: AEae

Case 5: Turkish culture regression
Culture: tr-TR
Input: IbA
PASS | Sorted result | Expected: AbI | Actual: AbI

Case 6: Large input
Input: 50000 'u' characters followed by 50000 'A' characters
PASS | Result length | Expected: 100000 | Actual: 100000
PASS | First half | Expected: all A | Actual: all A
PASS | Second half | Expected: all u | Actual: all u
PASS | Exact result | Expected: 50000 A followed by 50000 u | Actual: 50000 A followed by 50000 u

Summary: 9/9 checks passed.
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
├── leetcode_2785/
│   ├── Program.cs             # 純解法與可執行驗證器
│   └── leetcode_2785.csproj   # .NET 10 SDK 專案設定
├── AGENTS.md                  # 本專案協作指南
└── README.md                  # 題目、解法與驗證紀錄
```
