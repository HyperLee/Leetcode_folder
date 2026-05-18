# LeetCode 58 - 最後一個單字的長度

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![Language](https://img.shields.io/badge/Language-C%23-239120)

這個專案使用 C# .NET console app 實作 LeetCode 58「Length of Last Word」。

- English: <https://leetcode.com/problems/length-of-last-word/description/>
- 中文: <https://leetcode.cn/problems/length-of-last-word/description/>

## 題目說明

給定一個由單字與空格組成的字串 `s`，回傳字串中最後一個單字的長度。

單字定義為只由非空格字元組成的最大連續子字串。

## 限制條件

- `1 <= s.Length <= 10^4`
- `s` 只包含英文字母與空格 `' '`
- `s` 至少包含一個單字

> [!NOTE]
> 目前實作依照題目限制設計，假設輸入一定至少包含一個單字，因此不額外處理空字串或全空格字串。

## 解題概念與出發點

題目只要求最後一個單字的長度，不需要知道所有單字，也不需要建立新的字串陣列。因此可以避免使用 `Split` 產生額外配置，直接從字串尾端往前掃描。

核心觀察：

1. 字串尾端可能有多個空格，這些空格不屬於最後一個單字。
2. 略過尾端空格後，第一個遇到的非空格字元就是最後一個單字的結尾。
3. 從該位置繼續往左數，直到遇到空格或抵達字串開頭，累計數量就是答案。

## 解法設計

### 解法一：反向掃描

實作位置：`leetcode_058/Program.cs`

方法簽章：

```csharp
public int LengthOfLastWord(string s)
```

設計流程：

1. 將索引 `index` 指向字串最後一個位置，也就是 `s.Length - 1`。
2. 由右往左跳過尾端所有空格，讓 `index` 停在最後一個單字的最後一個字元。
3. 建立 `wordLength` 計數器。
4. 由右往左累計所有連續非空格字元。
5. 當遇到空格或 `index` 小於 `0` 時停止，回傳 `wordLength`。

這個做法只掃描必要字元；最壞情況仍可能掃過整個字串，但不需要額外陣列或字串切割。

時間複雜度：`O(n)`，其中 `n` 是字串長度。

空間複雜度：`O(1)`。

## 範例演示流程

### 範例一：一般句子

輸入：

```text
"Hello World"
```

流程：

1. 從尾端 `d` 開始，尾端不是空格。
2. 往左計算 `d -> l -> r -> o -> W`。
3. 下一個字元是空格，停止。
4. 最後一個單字是 `World`，長度為 `5`。

輸出：

```text
5
```

### 範例二：尾端有空格

輸入：

```text
"   fly me   to   the moon  "
```

流程：

1. 從尾端開始，先略過最後兩個空格。
2. 停在 `moon` 的最後一個字元 `n`。
3. 往左計算 `n -> o -> o -> m`。
4. 下一個字元是空格，停止。
5. 最後一個單字是 `moon`，長度為 `4`。

輸出：

```text
4
```

### 範例三：最後單字在字串尾端

輸入：

```text
"luffy is still joyboy"
```

流程：

1. 尾端字元 `y` 不是空格，直接開始計數。
2. 往左計算 `y -> o -> b -> y -> o -> j`。
3. 下一個字元是空格，停止。
4. 最後一個單字是 `joyboy`，長度為 `6`。

輸出：

```text
6
```

### 範例四：只有一個單字

輸入：

```text
"a"
```

流程：

1. 尾端字元 `a` 不是空格。
2. 往左計算到字串開頭。
3. 最後一個單字是 `a`，長度為 `1`。

輸出：

```text
1
```

## 執行方式

建置專案：

```bash
dotnet build leetcode_058/leetcode_058.csproj
```

執行內建範例：

```bash
dotnet run --project leetcode_058/leetcode_058.csproj
```

在部分 macOS 環境中，.NET CLI 可能會先輸出 `CSSM_ModuleLoad(): One or more parameters passed to a function were not valid.`。這是環境層訊息，不是本專案的應用程式輸出。

預期應用程式輸出：

```text
Input: "Hello World" | Expected: 5 | Actual: 5 | PASS
Input: "   fly me   to   the moon  " | Expected: 4 | Actual: 4 | PASS
Input: "luffy is still joyboy" | Expected: 6 | Actual: 6 | PASS
Input: "a" | Expected: 1 | Actual: 1 | PASS
Input: "Today is a nice day   " | Expected: 3 | Actual: 3 | PASS
```

檢查是否有多餘空白或換行問題：

```bash
git diff --check
```

## 專案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_058/
    ├── Program.cs
    └── leetcode_058.csproj
```
