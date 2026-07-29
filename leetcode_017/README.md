# LeetCode 17：電話號碼的字母組合

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![Language](https://img.shields.io/badge/Language-C%23-239120)

使用 C# 與深度優先回溯法，列舉電話按鍵數字能代表的所有字母組合。本專案包含可直接執行的主控台驗證案例，會比較完整的預期與實際序列。

## 快速連結

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：深度優先回溯](#解法一深度優先回溯)
- [範例演示流程](#範例演示流程)
- [建置與執行](#建置與執行)

## 題目說明

給定一個只包含數字 `2` 至 `9` 的字串 `digits`，回傳這些數字依照電話按鍵映射後，能產生的所有字母組合。答案順序不限，但本實作會依輸入數字與各按鍵字母的既定順序產生結果。

電話按鍵映射如下：

| 數字 | 字母 |
| --- | --- |
| `2` | `abc` |
| `3` | `def` |
| `4` | `ghi` |
| `5` | `jkl` |
| `6` | `mno` |
| `7` | `pqrs` |
| `8` | `tuv` |
| `9` | `wxyz` |

例如輸入 `"23"`，第一個位置從 `a、b、c` 選一個字母，第二個位置從 `d、e、f` 選一個字母，因此共有 9 種組合：

```text
["ad", "ae", "af", "bd", "be", "bf", "cd", "ce", "cf"]
```

題目連結：

- [LeetCode](https://leetcode.com/problems/letter-combinations-of-a-phone-number/)
- [力扣中國](https://leetcode.cn/problems/letter-combinations-of-a-phone-number/)

## 限制條件

- `0 <= digits.Length <= 4`
- `digits[i]` 的值介於 `'2'` 與 `'9'`。
- 空字串沒有可建立的字母位置，因此回傳空集合。
- 每個輸入位置必須保留原本順序；不能交換不同按鍵的位置。

## 解題概念與出發點

每一個輸入數字都代表一組候選字母。若逐層決定每個位置的字母，所有合法答案就會形成一棵決策樹：

- 樹的第 `index` 層對應 `digits[index]`。
- 每一條分支代表從目前按鍵選擇一個字母。
- 走到深度 `digits.Length` 時，目前路徑正好是一個完整答案。
- 收集答案後撤銷最後一次選擇，便能回到上一層繼續嘗試其他分支。

這正是回溯法適合處理的「列舉所有選擇」問題。`StringBuilder` 保存目前路徑，避免每深入一層就建立大量中間字串；只有找到完整組合時才呼叫 `ToString()` 保存結果。

目前專案只有一種主要解法：深度優先回溯。

## 解法一：深度優先回溯

### 設計說明

`LetterCombinations` 負責準備本次搜尋狀態：

1. 保存輸入字串。
2. 建立新的結果清單，避免前一筆測試結果殘留。
3. 若輸入為空，直接回傳空集合。
4. 從索引 `0` 與空的 `StringBuilder` 開始呼叫 `Backtrack`。

`Backtrack(index, sb)` 負責列舉一層決策：

1. 若 `index == digits.Length`，代表 `sb` 已包含每個輸入位置對應的一個字母，將完整字串加入結果。
2. 將 `digits[index]` 從字元轉成電話按鍵索引，取得該按鍵的候選字母。
3. 依序選擇每個候選字母並附加到 `sb`。
4. 遞迴處理 `index + 1`。
5. 遞迴返回後縮短 `sb`，撤銷本層選擇，再嘗試下一個字母。

### 搜尋不變條件

每次進入 `Backtrack(index, sb)` 時，`sb.Length == index`，而且 `sb` 的每個字母都依序來自輸入中已處理的按鍵。選擇一個字母後，兩者長度同時增加一；返回上一層時撤銷該字母，因此不變條件會持續成立。

### 正確性說明

- 每層只從目前數字對應的字母集合中選擇，所以產生的每個組合都符合電話按鍵映射與輸入順序。
- 遞迴會逐一走訪每層的所有候選字母，因此每一種合法選擇序列都會被走訪。
- 一條路徑只由唯一的一連串選擇產生，所以同一組合不會重複加入。

因此演算法會恰好產生所有合法字母組合。

### 複雜度分析

令 `n` 為輸入長度，`m` 為實際組合數：

- 時間複雜度：`O(n × m)`；每個結果都需要建立一個長度為 `n` 的字串。由於每個按鍵最多有 4 個字母，最壞情況可寫成 `O(n × 4^n)`。
- 輔助空間複雜度：`O(n)`，包含遞迴呼叫堆疊與目前路徑的 `StringBuilder`。
- 結果空間複雜度：`O(n × m)`，最壞為 `O(n × 4^n)`。

## 範例演示流程

### 輸入 `"23"`

1. `index = 0`，數字 `2` 對應 `abc`。
2. 選擇 `a`，目前路徑為 `"a"`。
3. `index = 1`，數字 `3` 對應 `def`：
   - 選擇 `d`，得到 `"ad"`，收集後撤銷 `d`。
   - 選擇 `e`，得到 `"ae"`，收集後撤銷 `e`。
   - 選擇 `f`，得到 `"af"`，收集後撤銷 `f`。
4. 回到第一層，依相同流程選擇 `b`，得到 `"bd"、"be"、"bf"`。
5. 第一層改選 `c`，得到 `"cd"、"ce"、"cf"`。
6. 所有分支走訪完畢，共收集 9 個組合。

```text
                 ""
          /       |       \
        "a"      "b"      "c"
       / | \     / | \     / | \
     ad ae af  bd be bf   cd ce cf
```

### 輸入空字串

`LetterCombinations` 在開始回溯前發現長度為 `0`，直接回傳 `[]`。這可避免把空路徑誤判成一個有效組合。

## 專案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_017/
│   ├── Program.cs
│   └── leetcode_017.csproj
└── leetcode_017.sln
```

## 建置與執行

需求：安裝支援 `net10.0` 的 .NET 10 SDK。

從 `leetcode_017` 專案根目錄執行：

```bash
dotnet restore leetcode_017/leetcode_017.csproj
dotnet build leetcode_017/leetcode_017.csproj --nologo
dotnet run --no-build --project leetcode_017/leetcode_017.csproj
```

目前沒有獨立的自動化測試專案；建置成功加上 `Main` 中五筆 expected/actual 案例全部通過，作為本專案的驗收方式。

## 實際執行結果

以下內容來自本專案執行 `dotnet run --no-build --project leetcode_017/leetcode_017.csproj` 的實際輸出：

```text
測試案例：兩個數字組合
輸入："23"
預期：["ad", "ae", "af", "bd", "be", "bf", "cd", "ce", "cf"]
實際：["ad", "ae", "af", "bd", "be", "bf", "cd", "ce", "cf"]
結果：PASS

測試案例：空字串
輸入：""
預期：[]
實際：[]
結果：PASS

測試案例：單個數字
輸入："2"
預期：["a", "b", "c"]
實際：["a", "b", "c"]
結果：PASS

測試案例：三個數字組合
輸入："234"
預期：["adg", "adh", "adi", "aeg", "aeh", "aei", "afg", "afh", "afi", "bdg", "bdh", "bdi", "beg", "beh", "bei", "bfg", "bfh", "bfi", "cdg", "cdh", "cdi", "ceg", "ceh", "cei", "cfg", "cfh", "cfi"]
實際：["adg", "adh", "adi", "aeg", "aeh", "aei", "afg", "afh", "afi", "bdg", "bdh", "bdi", "beg", "beh", "bei", "bfg", "bfh", "bfi", "cdg", "cdh", "cdi", "ceg", "ceh", "cei", "cfg", "cfh", "cfi"]
結果：PASS

測試案例：包含 7 和 9
輸入："79"
預期：["pw", "px", "py", "pz", "qw", "qx", "qy", "qz", "rw", "rx", "ry", "rz", "sw", "sx", "sy", "sz"]
實際：["pw", "px", "py", "pz", "qw", "qx", "qy", "qz", "rw", "rx", "ry", "rz", "sw", "sx", "sy", "sz"]
結果：PASS

總結：5/5 筆測試通過
```
