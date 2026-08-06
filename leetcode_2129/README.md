# LeetCode 2129：將標題首字母大寫

[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/download/dotnet/10.0)

本專案是 LeetCode 2129 的 .NET 10 console 教學範例。程式保留以 `Split` 與 `StringBuilder` 逐字組裝的解法，並加入字元陣列區段掃描解法；`Main` 內建五組固定案例，可直接比較兩種解法並自動判定結果。

## 目錄

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：分割單字與逐字組裝](#解法一分割單字與逐字組裝)
- [解法二：字元陣列區段掃描](#解法二字元陣列區段掃描)
- [兩種解法比較](#兩種解法比較)
- [可執行測試資料](#可執行測試資料)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)
- [專案結構](#專案結構)

## 題目說明

官方題目：

- [LeetCode 英文題目](https://leetcode.com/problems/capitalize-the-title/description/)
- [LeetCode 中文題目](https://leetcode.cn/problems/capitalize-the-title/description/)

給定一個由一個或多個英文單字組成的標題 `title`，單字之間以單一空白分隔。請依每個單字的長度調整大小寫：

- 長度為 `1` 或 `2`：所有字母都改為小寫。
- 長度大於 `2`：首字母改為大寫，其餘字母改為小寫。

最後回傳完成正規化的新標題。

例如：

```text
Input："First leTTeR of EACH Word"
Output："First Letter of Each Word"
```

`of` 的長度為 `2`，所以必須全部小寫；其餘單字的長度都大於 `2`，因此只有首字母大寫。

## 限制條件

| 條件 | 官方範圍 |
| --- | --- |
| 標題長度 | `1 <= title.Length <= 100` |
| 分隔方式 | 單字之間恰好有一個空白 |
| 字串邊界 | 沒有前導或尾端空白 |
| 單字內容 | 每個單字非空，且只含大小寫英文字母 |

兩個公開方法都依照 LeetCode 的合法輸入契約運作，不額外定義 `null`、空字串、多重空白或非英文字母輸入的行為。

## 解題概念與出發點

### 1. 規則取決於單字長度

本題不是把所有單字都轉成一般標題格式。關鍵分界是單字長度是否超過 `2`：

| 單字 | 長度 | 處理方式 | 結果 |
| --- | ---: | --- | --- |
| `A` | 1 | 全部小寫 | `a` |
| `OF` | 2 | 全部小寫 | `of` |
| `tHe` | 3 | 首字母大寫，其餘小寫 | `The` |

因此，每次處理一個單字時，必須先知道完整長度，才能決定第一個字母要大寫還是小寫。

### 2. 每個字母都只需要被正規化一次

不論採用分割字串或掃描區間，每個英文字母最後都只有兩種操作：

- 長單字的第一個字母使用 `char.ToUpperInvariant`。
- 其他字母使用 `char.ToLowerInvariant`。

使用 invariant casing 可以讓英文字母的轉換不受作業系統目前文化設定影響，使範例輸出保持一致。

### 3. 保留原本的空白結構

題目保證單字之間只有一個空白。第一種解法在重新組裝單字時補回空白；第二種解法不改動字元陣列中的空白，因此兩者都能保留原有單字順序與分隔方式。

## 解法一：分割單字與逐字組裝

### API

```csharp
public static string CapitalizeTitle(string title)
```

### 設計說明

此解法延續專案原本的設計：

1. 使用 `title.Split(' ')` 把標題分割成單字陣列。
2. 依序讀取每個單字。
3. 若單字長度大於 `2`，把第一個字母轉為大寫；否則轉為小寫。
4. 從第二個字母開始全部轉為小寫。
5. 使用 `StringBuilder` 依原順序加入單字，並在相鄰單字之間加入空白。

`StringBuilder` 預先使用 `title.Length` 作為容量，因為大小寫轉換不會改變本題英文字母與空白的數量。

### 範例演示流程

輸入：

```text
"First leTTeR of EACH Word"
```

分割後依序處理：

| 步驟 | 原單字 | 長度 | 首字母規則 | 其餘字母 | 本次結果 |
| ---: | --- | ---: | --- | --- | --- |
| 1 | `First` | 5 | `F` 大寫 | `irst` 小寫 | `First` |
| 2 | `leTTeR` | 6 | `L` 大寫 | `etter` 小寫 | `Letter` |
| 3 | `of` | 2 | 首字母也小寫 | `f` 小寫 | `of` |
| 4 | `EACH` | 4 | `E` 大寫 | `ach` 小寫 | `Each` |
| 5 | `Word` | 4 | `W` 大寫 | `ord` 小寫 | `Word` |

`StringBuilder` 最後組合為：

```text
"First Letter of Each Word"
```

### 正確性說明

`Split` 會依題目保證取得每個完整且非空的單字。對任一單字：

- 若長度不超過 `2`，演算法把首字母與其餘字母全部轉小寫，符合短單字規則。
- 若長度超過 `2`，演算法把首字母轉大寫，再把其餘字母全部轉小寫，符合長單字規則。

所有單字都依原順序處理並以一個空白連接，因此輸出中的每個單字及分隔方式都符合題目要求。

### 複雜度與輸入契約

令 `n` 為 `title` 的總長度：

- 時間複雜度：`O(n)`，每個單字與字母都只做常數次處理。
- 額外空間：`O(n)`，包含分割後的單字資料與 `StringBuilder` 輸出。
- 修改輸入：否；C# 字串不可變，方法會建立並回傳新字串。
- 優點：步驟直觀，容易直接對照題目的「逐單字」描述。
- 取捨：`Split` 會建立單字陣列與各單字字串。

## 解法二：字元陣列區段掃描

### API

```csharp
public static string CapitalizeTitle2(string title)
```

### 設計說明

第二種解法不建立分割後的單字陣列：

1. 使用 `ToCharArray()` 建立可修改的字元陣列。
2. 令 `wordStart` 指向目前單字的第一個字元。
3. 從左到右尋找空白或整個字串的結尾，該位置就是目前單字的右邊界。
4. 由 `boundary - wordStart` 算出單字長度。
5. 直接在該區間內調整大小寫。
6. 跨過空白，讓 `wordStart` 指向下一個單字。
7. 所有區間完成後，從字元陣列建立結果字串。

空白只負責界定區間，不會被修改或搬動。

### 範例演示流程

輸入：

```text
"a AB abc"
```

字元索引：

```text
索引： 0 1 2 3 4 5 6 7
字元： a _ A B _ a b c
```

其中 `_` 代表空白。掃描流程如下：

| 單字區間 | 單字 | 長度 | 區間內處理 | 結果 |
| --- | --- | ---: | --- | --- |
| `[0, 1)` | `a` | 1 | 全部小寫 | `a` |
| `[2, 4)` | `AB` | 2 | 全部小寫 | `ab` |
| `[5, 8)` | `abc` | 3 | 首字母大寫，其餘小寫 | `Abc` |

處理後的字元陣列為：

```text
a _ a b _ A b c
```

因此結果是：

```text
"a ab Abc"
```

### 正確性說明

題目保證只有單一空白分隔，且沒有前後空白，所以每次找到空白或字串結尾時，`[wordStart, boundary)` 必定恰好是一個完整單字。

演算法根據該區間的長度，對區間中第一個與其餘字元套用相應規則；因此每個單字都會得到正確大小寫。每個非空白字元恰好屬於一個單字區間，而空白保持不變，所以建立出的新字串完整且符合題目要求。

### 複雜度與輸入契約

令 `n` 為 `title` 的總長度：

- 時間複雜度：`O(n)`；邊界掃描與區間轉換的總工作量都與字串長度成正比。
- 額外空間：`O(n)`，主要是可修改的字元陣列與最後建立的結果字串。
- 修改輸入：否；方法只修改由輸入複製出的 `char[]`。
- 優點：不需要建立分割後的單字陣列，可清楚展示以索引處理字串區間的技巧。
- 取捨：索引與右邊界的控制比 `Split` 解法稍複雜。

## 兩種解法比較

| 比較項目 | `CapitalizeTitle` | `CapitalizeTitle2` |
| --- | --- | --- |
| 核心方法 | `Split` 後逐字組裝 | `char[]` 上定位並處理單字區間 |
| 時間複雜度 | `O(n)` | `O(n)` |
| 額外空間 | `O(n)` | `O(n)` |
| 修改輸入 | 否 | 否，只修改複製出的陣列 |
| 單字定位方式 | 字串分割 | 空白與字串結尾 |
| 主要配置 | 單字陣列、分割字串、Builder | 字元陣列、結果字串 |
| 教學重點 | 直接映射題意、字串組裝 | 區間邊界、索引掃描 |

兩種解法的漸進複雜度相同。第一種較容易閱讀，第二種則避免 `Split` 的單字集合，適合比較不同的字串處理模型。

## 可執行測試資料

`Main` 執行五組固定案例，每組呼叫兩種解法，因此共有十項答案檢查。任一檢查失敗時，程式會設定非零結束代碼。

| 案例 | 輸入 | 預期 | 涵蓋重點 |
| --- | --- | --- | --- |
| 官方範例一 | `"capiTalIze tHe titLe"` | `"Capitalize The Title"` | 所有單字長度大於 2 |
| 官方範例二 | `"First leTTeR of EACH Word"` | `"First Letter of Each Word"` | 長短單字混合與不規則大小寫 |
| 官方範例三 | `"i lOve leetcode"` | `"i Love Leetcode"` | 長度為 1 的單字 |
| 單字長度臨界值 | `"a AB abc"` | `"a ab Abc"` | 同時驗證長度 1、2、3 |
| 輸入長度上限 | 100 個大寫 `A` | `A` 加上 99 個小寫 `a` | `title.Length = 100` |

## 建置與執行

請在 `leetcode_2129` repository 根目錄執行：

```bash
dotnet restore leetcode_2129/leetcode_2129.csproj
dotnet build leetcode_2129/leetcode_2129.csproj --no-restore --nologo
dotnet run --no-build --project leetcode_2129/leetcode_2129.csproj
```

本 repository 目前沒有獨立的自動化測試專案；`Main` 的十項固定答案檢查就是可重複執行的驗收 harness。成功時程式的結束代碼為 `0`，且最後一行為：

```text
總結：10/10 項測試通過
```

## 實際執行結果

以下內容來自修改後實際執行 `dotnet run --no-build --project leetcode_2129/leetcode_2129.csproj` 的輸出：

```text
案例：1. 官方範例一
Input：title = "capiTalIze tHe titLe"
解法一：CapitalizeTitle（分割單字與逐字組裝）
Expected："Capitalize The Title"
Actual："Capitalize The Title"
Result：PASS
解法二：CapitalizeTitle2（字元陣列單次掃描）
Expected："Capitalize The Title"
Actual："Capitalize The Title"
Result：PASS

案例：2. 官方範例二
Input：title = "First leTTeR of EACH Word"
解法一：CapitalizeTitle（分割單字與逐字組裝）
Expected："First Letter of Each Word"
Actual："First Letter of Each Word"
Result：PASS
解法二：CapitalizeTitle2（字元陣列單次掃描）
Expected："First Letter of Each Word"
Actual："First Letter of Each Word"
Result：PASS

案例：3. 官方範例三
Input：title = "i lOve leetcode"
解法一：CapitalizeTitle（分割單字與逐字組裝）
Expected："i Love Leetcode"
Actual："i Love Leetcode"
Result：PASS
解法二：CapitalizeTitle2（字元陣列單次掃描）
Expected："i Love Leetcode"
Actual："i Love Leetcode"
Result：PASS

案例：4. 單字長度臨界值
Input：title = "a AB abc"
解法一：CapitalizeTitle（分割單字與逐字組裝）
Expected："a ab Abc"
Actual："a ab Abc"
Result：PASS
解法二：CapitalizeTitle2（字元陣列單次掃描）
Expected："a ab Abc"
Actual："a ab Abc"
Result：PASS

案例：5. 輸入長度上限
Input：title = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"
解法一：CapitalizeTitle（分割單字與逐字組裝）
Expected："Aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
Actual："Aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
Result：PASS
解法二：CapitalizeTitle2（字元陣列單次掃描）
Expected："Aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
Actual："Aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
Result：PASS

總結：10/10 項測試通過
```

## 專案結構

```text
leetcode_2129/
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_2129.sln
└── leetcode_2129/
    ├── leetcode_2129.csproj
    └── Program.cs
```

- `Program.cs`：兩種演算法、固定案例 harness 與 console 輸出。
- `leetcode_2129.csproj`：以 `net10.0` 為目標的 console project 設定。
- `docs/readme-template.md`：本 README 使用的初始文件規範。
