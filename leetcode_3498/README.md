# LeetCode 3498：字串的反轉度

這個專案以 .NET 10 console project 示範 LeetCode 3498「字串的反轉度」的兩種線性時間解法，並在程式進入點以固定測試資料同時驗證兩個方法。

- 題目：[3498. Reverse Degree of a String](https://leetcode.com/problems/reverse-degree-of-a-string/description/)
- 中文題目：[3498. 字串的反轉度](https://leetcode.cn/problems/reverse-degree-of-a-string/description/)
- 專案檔：[leetcode_3498/leetcode_3498.csproj](leetcode_3498/leetcode_3498.csproj)
- 主要程式：[leetcode_3498/Program.cs](leetcode_3498/Program.cs)

## 快速開始

請在本 README 所在的專案根目錄執行：

```bash
dotnet restore leetcode_3498/leetcode_3498.csproj
dotnet build leetcode_3498/leetcode_3498.csproj --nologo --no-restore
dotnet run --project leetcode_3498/leetcode_3498.csproj --no-build --no-restore --nologo
```

程式不會要求輸入姓名或測資。`Main` 會直接執行固定案例，逐一列出兩種解法的預期值、實際值與通過結果；若有任何檢查失敗，程序結束碼會是 `1`。

## 題目說明

給定只包含小寫英文字母的字串 `s`，請計算它的反轉度（reverse degree）。

每個字元有兩個位置：

1. 字元在反轉字母表中的位置：`'a' = 26`、`'b' = 25`、……、`'z' = 1`。
2. 字元在原字串中的位置，從 `1` 開始計算。

對每個字元，把這兩個位置相乘，再將所有乘積加總，就是字串的反轉度。

### 限制條件

依照官方題目規格：

- `1 <= s.length <= 1000`
- `s` 只包含小寫英文字母

因為每個反轉字母位置最多是 `26`，在限制條件下答案上限為：

```text
26 × (1 + 2 + ... + 1000) = 13,013,000
```

因此使用 C# `int` 儲存結果足夠。輸入限制與原始題意可參考 [LeetCode 官方題目](https://leetcode.com/problems/reverse-degree-of-a-string/description/)。

## 解題概念與出發點

題目的核心是把「反轉字母表位置」轉換成容易在迴圈中計算的數值。

對於小寫字元 `c`，它距離 `'a'` 的偏移量是 `c - 'a'`：

```text
'a' - 'a' = 0
'b' - 'a' = 1
...
'z' - 'a' = 25
```

反轉字母表位置因此可以寫成：

```text
reversePosition(c) = 26 - (c - 'a')
```

若目前字元在字串中的 1-based 位置是 `i`，它對答案的貢獻就是：

```text
reversePosition(c) × i
```

整個字串只需要被走訪一次，不需要建立反轉字串、排序字元或額外的查表結構，所以兩種方法都能達到 `O(n)` 時間與 `O(1)` 額外空間。

## 方法一：直接模擬

方法一對應 `ReverseDegree`。

### 設計步驟

1. 建立累加器 `res`，初始值為 `0`。
2. 使用 `i` 從 `1` 走到 `s.Length`，讓迴圈變數直接代表題目要求的 1-based 位置。
3. 取出目前字元 `s[i - 1]`。陣列索引是 0-based，因此要減去 `1`。
4. 以 `26 - (s[i - 1] - 'a')` 計算反轉字母位置。
5. 將反轉字母位置乘以 `i`，加入 `res`。
6. 走訪完成後回傳 `res`。

### 公式對照

```text
res += (26 - (s[i - 1] - 'a')) × i
```

這種寫法最直接地對應題目定義：每次迴圈處理一個字元，計算該字元的單獨貢獻，再累加到答案。

### 正確性說明

對任一位置 `i`：

- `s[i - 1] - 'a'` 是字元在正常字母表中的 0-based 偏移量。
- `26 - (s[i - 1] - 'a')` 會把 `'a'` 映射到 `26`，把 `'z'` 映射到 `1`，正好是反轉字母表位置。
- 再乘以 `i`，就是題目對該字元定義的貢獻。

迴圈會處理所有字元，且每個字元只加入一次自己的貢獻，因此最後的 `res` 就是所有乘積的總和。

## 方法二：利用字元碼差值

方法二對應 `ReverseDegree2`。它保留相同的累加概念，但利用小寫英文字元碼連續排列的特性，省略顯式的 `- 'a'` 與 `26 -` 組合。

### 關鍵觀察

在 C# 的 `char` 數值中，`'{'` 緊接在 `'z'` 後面：

```text
'{' - 'a' = 26
'{' - 'b' = 25
...
'{' - 'z' = 1
```

因此：

```text
'{' - s[i]
```

就直接是目前字元在反轉字母表中的位置。由於 `i` 是 0-based 索引，字串中的題目位置要使用 `i + 1`。

### 設計步驟

1. 建立累加器 `res`，初始值為 `0`。
2. 使用 `i` 從 `0` 走到 `s.Length - 1`。
3. 以 `'{' - s[i]` 計算反轉字母位置。
4. 將它乘以 `i + 1`，轉換成題目要求的 1-based 字串位置後加入 `res`。
5. 走訪完成後回傳 `res`。

### 公式對照

```text
res += ('{' - s[i]) × (i + 1)
```

方法二的結果與方法一相同，差別只在於反轉字母位置的算式寫法不同。這個字元碼技巧成立的前提是輸入只包含連續的小寫英文字母，而這正是題目限制保證的條件。

### 正確性說明

因為 `'{'` 的字元碼比 `'z'` 大 `1`，而 `'a'` 到 `'z'` 依序排列，所以對任何合法輸入字元 `c`，`'{' - c` 的值會從 `26` 遞減到 `1`，等於 `c` 的反轉字母位置。

方法二用 `i + 1` 還原題目使用的 1-based 位置，再將每個字元的正確貢獻加入總和。因此回傳值與題目定義一致。

## 範例演示

以下兩個官方範例會用相同的數學結果驗證兩種方法；差異在於兩種方法如何取得反轉字母位置。

### 範例一：`s = "abc"`

#### 方法一的流程

| 字元 | 字串位置 `i` | `26 - (c - 'a')` | 單次貢獻 |
| --- | ---: | ---: | ---: |
| `a` | 1 | 26 | 26 × 1 = 26 |
| `b` | 2 | 25 | 25 × 2 = 50 |
| `c` | 3 | 24 | 24 × 3 = 72 |

最後：`26 + 50 + 72 = 148`。

程式中的 `ReverseDegree` 會依序使用 `s[0]`、`s[1]`、`s[2]`，但把它們分別乘上 `1`、`2`、`3`。

#### 方法二的流程

方法二的索引是 `0`、`1`、`2`，所以字串位置使用 `i + 1`：

| 字元 | 0-based 索引 `i` | `'{' - c` | `i + 1` | 單次貢獻 |
| --- | ---: | ---: | ---: | ---: |
| `a` | 0 | 26 | 1 | 26 × 1 = 26 |
| `b` | 1 | 25 | 2 | 25 × 2 = 50 |
| `c` | 2 | 24 | 3 | 24 × 3 = 72 |

總和同樣是 `148`。

### 範例二：`s = "zaza"`

#### 方法一的流程

| 字元 | 字串位置 `i` | 反轉字母位置 | 單次貢獻 |
| --- | ---: | ---: | ---: |
| `z` | 1 | 1 | 1 × 1 = 1 |
| `a` | 2 | 26 | 26 × 2 = 52 |
| `z` | 3 | 1 | 1 × 3 = 3 |
| `a` | 4 | 26 | 26 × 4 = 104 |

最後：`1 + 52 + 3 + 104 = 160`。

#### 方法二的流程

方法二會得到完全相同的四個反轉字母位置：`'{' - 'z' = 1`、`'{' - 'a' = 26`。再分別乘以 `1`、`2`、`3`、`4`，得到相同的總和 `160`。

## 複雜度比較

| 方法 | 反轉位置的計算方式 | 時間複雜度 | 額外空間 | 特點 |
| --- | --- | --- | --- | --- |
| `ReverseDegree` | `26 - (c - 'a')` | `O(n)` | `O(1)` | 公式直觀，最容易直接對照題意 |
| `ReverseDegree2` | `'{' - c` | `O(n)` | `O(1)` | 利用連續字元碼，以差值直接取得位置 |

兩種方法都不會修改輸入字串，也不需要保存每個字元的中間結果。

## 可執行驗證

目前沒有額外的單元測試專案，因此 `Main` 是這個教學專案的可執行 smoke test。固定測試資料包含兩個官方範例與兩個單字元邊界案例，兩種方法各執行一次，共 `8` 個檢查。

### 測試案例

| 案例 | 輸入 | 預期結果 | 覆蓋重點 |
| ---: | --- | ---: | --- |
| 1 | `"abc"` | 148 | 官方範例、連續字母與不同位置權重 |
| 2 | `"zaza"` | 160 | 官方範例、`a` 與 `z` 的反覆出現 |
| 3 | `"a"` | 26 | 最小長度與最大反轉字母位置 |
| 4 | `"z"` | 1 | 最小反轉字母位置 |

### 實際執行輸出

下列內容是執行 `dotnet run --project leetcode_3498/leetcode_3498.csproj --no-build --no-restore --nologo` 的實際 console 輸出：

```text
Case 1: s = "abc", Expected = 148
  ReverseDegree: Actual = 148, Result = PASS
  ReverseDegree2: Actual = 148, Result = PASS
Case 2: s = "zaza", Expected = 160
  ReverseDegree: Actual = 160, Result = PASS
  ReverseDegree2: Actual = 160, Result = PASS
Case 3: s = "a", Expected = 26
  ReverseDegree: Actual = 26, Result = PASS
  ReverseDegree2: Actual = 26, Result = PASS
Case 4: s = "z", Expected = 1
  ReverseDegree: Actual = 1, Result = PASS
  ReverseDegree2: Actual = 1, Result = PASS
Summary: 8/8 checks passed.
```

## 建置、格式與差異檢查

從專案根目錄執行以下命令：

```bash
dotnet restore leetcode_3498/leetcode_3498.csproj
dotnet build leetcode_3498/leetcode_3498.csproj --nologo --no-restore
dotnet build leetcode_3498/leetcode_3498.csproj --nologo --no-restore -t:Rebuild -p:GenerateDocumentationFile=true -warnaserror:CS1570,CS1571
dotnet run --project leetcode_3498/leetcode_3498.csproj --no-build --no-restore --nologo
dotnet format leetcode_3498/leetcode_3498.csproj --verify-no-changes --no-restore
git diff --check
```

第二個 `dotnet build` 會強制重新產生 XML 文件，並把 `CS1570`、`CS1571` 視為錯誤，確保 XML 註解結構有效。`dotnet format` 用來確認 C# 空白與格式符合 `.editorconfig`；`git diff --check` 用來檢查差異中的多餘空白。

## 專案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_3498/
    ├── Program.cs
    └── leetcode_3498.csproj
```

- `leetcode_3498/Program.cs`：題目 XML、兩種解法與固定測試入口。
- `leetcode_3498/leetcode_3498.csproj`：.NET 10 console project 設定。
- `docs/readme-template.md`：README 初始建立時採用的文件結構指引。
- `bin/` 與 `obj/`：建置產物，不納入版本控制。
