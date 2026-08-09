# LeetCode 3110：字串的分數（Score of a String）

![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)

本專案以 .NET 10 console application 示範 LeetCode 3110「字串的分數」的三種線性掃描解法，並在 `Main` 中使用固定案例比較每種解法的實際結果。

- 英文題目：[Score of a String](https://leetcode.com/problems/score-of-a-string/description/?envType=daily-question&envId=2024-06-01)
- 中文題目：[字符串的分数](https://leetcode.cn/problems/score-of-a-string/description/)
- Target framework：`net10.0`

## 題目說明

給定一個字串 `s`，字串的分數定義為每一對相鄰字元 ASCII 值之差的絕對值總和。請計算並回傳 `s` 的分數。

若字串為 `"hello"`，各字元 ASCII 值為：

```text
'h' = 104
'e' = 101
'l' = 108
'l' = 108
'o' = 111
```

因此分數為：

```text
|104 - 101| + |101 - 108| + |108 - 108| + |108 - 111|
= 3 + 7 + 0 + 3
= 13
```

## 限制條件

依照[官方題目限制](https://leetcode.com/problems/score-of-a-string/description/)：

- `2 <= s.length <= 100`
- `s` 僅由小寫英文字母組成。
- 題目保證輸入符合以上條件，因此三種演算法不另外處理 `null`、空字串、大寫字母或其他題目範圍外的輸入。
- 小寫英文字母的字元碼順序與 ASCII 相同，所以 C# 可直接將兩個 `char` 相減，再以 `Math.Abs` 取得絕對差。

## 解題概念與出發點

長度為 `n` 的字串共有 `n - 1` 組相鄰字元：

```text
(s[0], s[1]), (s[1], s[2]), ..., (s[n - 2], s[n - 1])
```

每一組都要貢獻：

```text
Math.Abs(右側字元 - 左側字元)
```

因此不需要排序、搜尋或額外的動態規劃狀態；只要由左至右掃描一次，確保每組相鄰字元恰好計算一次即可。本專案保留原本的索引迴圈，並加入保存前一字元與 LINQ `Zip` 兩種版本，以比較三種描述同一個相鄰關係的方式。

## 解法一：索引迴圈

對應方法：`ScoreOfString`

### 設計說明

1. 將累積分數 `score` 初始化為 `0`。
2. 因為第一個字元沒有前一個相鄰字元，所以索引從 `1` 開始。
3. 每一輪以 `s[index]` 和 `s[index - 1]` 形成一組相鄰字元。
4. 將兩個字元碼之差的絕對值累加到 `score`。
5. 掃描完成後回傳總分。

索引 `index` 每次向右移動一格，因此 `(index - 1, index)` 會依序覆蓋全部 `n - 1` 組相鄰字元，且不會重複或遺漏。

### 範例演示：`"hello"`

| `index` | 前一字元 | 目前字元 | 絕對差 | 累積分數 |
| ---: | :---: | :---: | ---: | ---: |
| 1 | `h` (104) | `e` (101) | 3 | 3 |
| 2 | `e` (101) | `l` (108) | 7 | 10 |
| 3 | `l` (108) | `l` (108) | 0 | 10 |
| 4 | `l` (108) | `o` (111) | 3 | 13 |

最後回傳 `13`。

### 複雜度

- 時間複雜度：`O(n)`
- 額外空間複雜度：`O(1)`

這個版本直接反映字串索引與相鄰位置，沒有額外抽象或配置，是三種解法中最直觀且適合作為基準的實作。

## 解法二：保存前一字元的狀態掃描

對應方法：`ScoreOfString2`

### 設計說明

1. 將累積分數設為 `0`，前一字元 `previous` 設為尚未存在。
2. 使用 `foreach` 依序讀取每一個 `current` 字元。
3. 第一次迭代時沒有前一字元，只將 `current` 保存到 `previous`。
4. 後續每一輪都計算 `current` 與 `previous` 的字元碼絕對差。
5. 累加後把 `previous` 更新成 `current`，供下一輪配對。

這個版本不需要手動管理索引，而是把「上一個讀到的字元」視為跨迭代保存的狀態。核心不變量是：進入每一輪計分時，`previous` 必定是 `current` 左邊緊鄰的字元。

### 範例演示：`"zaz"`

| 讀取字元 | 讀取前的 `previous` | 動作 | 累積分數 |
| :---: | :---: | --- | ---: |
| `z` | 無 | 設定 `previous = 'z'`，不計分 | 0 |
| `a` | `z` | `|97 - 122| = 25` | 25 |
| `z` | `a` | `|122 - 97| = 25` | 50 |

最後回傳 `50`。

### 複雜度

- 時間複雜度：`O(n)`
- 額外空間複雜度：`O(1)`

這個版本適合用來理解串流式處理：演算法不需要保留整段歷史，只要記住能與下一個元素形成相鄰配對的單一狀態。

## 解法三：LINQ `Zip` 相鄰配對

對應方法：`ScoreOfString3`

### 設計說明

1. 第一個序列使用原字串 `s`。
2. 第二個序列使用 `s.Skip(1)`，也就是略過第一個字元後的序列。
3. `Zip` 將兩個序列的相同位置配成一組：

   ```text
   s           = [s[0], s[1], ..., s[n - 2], s[n - 1]]
   s.Skip(1)   = [s[1], s[2], ..., s[n - 1]]
   Zip 結果    = [(s[0], s[1]), ..., (s[n - 2], s[n - 1])]
   ```

4. 對每組字元計算 ASCII 值的絕對差。
5. 使用 `Sum` 加總所有差值並回傳。

`Zip` 會在較短的第二個序列結束時停止，因此不會產生缺少右側字元的最後一組。LINQ 使用延遲列舉，不需要先建立完整的配對陣列。

### 範例演示：`"aabb"`

```text
s           = ['a', 'a', 'b', 'b']
s.Skip(1)   = ['a', 'b', 'b']
Zip 配對    = [('a', 'a'), ('a', 'b'), ('b', 'b')]
差值        = [0, 1, 0]
Sum         = 1
```

最後回傳 `1`。

### 複雜度

- 時間複雜度：`O(n)`
- 額外空間複雜度：`O(1)`；僅使用固定數量的延遲列舉迭代器，不建立與輸入長度成比例的集合。

這個版本最接近「先建立相鄰配對，再加總映射結果」的宣告式描述，程式碼精簡，但讀者需要熟悉 `Skip`、`Zip` 與 `Sum` 的列舉行為。

## 三種解法比較

| 方法 | 核心思路 | 時間複雜度 | 額外空間 | 教學重點 |
| --- | --- | --- | --- | --- |
| `ScoreOfString` | 用索引 `(i - 1, i)` 取得相鄰字元 | `O(n)` | `O(1)` | 直接、無額外抽象，適合作為基準解 |
| `ScoreOfString2` | 用 `previous` 保存前一字元 | `O(n)` | `O(1)` | 串流式狀態與迴圈不變量 |
| `ScoreOfString3` | 用錯開一位的序列進行 `Zip` | `O(n)` | `O(1)` | 宣告式相鄰配對與延遲列舉 |

三種方法都會掃描每一組相鄰字元，因此漸進時間複雜度相同。主要差異在於如何表示「前一個字元與目前字元」的關係，而不是計算公式不同。

## Main 測試 harness

`Main` 會建立 8 個符合題目限制的固定案例，並讓三種解法各驗證一次：

| 案例 | 輸入 | 預期結果 | 覆蓋情境 |
| --- | --- | ---: | --- |
| 官方範例一 | `"hello"` | 13 | 一般輸入與重複相鄰字元 |
| 官方範例二 | `"zaz"` | 50 | 大幅度來回變化 |
| 最短同字元 | `"aa"` | 0 | 最小長度與零分 |
| 最短最大差 | `"az"` | 25 | 最小長度與單組最大差 |
| 含重複相鄰字元 | `"aabb"` | 1 | 多組零差與一組非零差 |
| 交錯最大差 | `"azaz"` | 75 | 每組皆為最大差 |
| 一般遞增字元 | `"abcde"` | 4 | 多組固定差值 |
| 長度上限同字元 | 100 個 `z` | 0 | 最大長度與全部零差 |

每個案例會顯示三種方法的實際結果與 `PASS`/`FAIL`。全部 24 項驗證通過時，程式回傳結束碼 `0`；任一驗證失敗時回傳 `1`。程式不使用 `Console.ReadKey()`，可在 CI 或輸出重新導向環境中執行。

## 執行方式

請在本專案根目錄執行：

```bash
dotnet restore leetcode_3110/leetcode_3110.csproj
dotnet build leetcode_3110/leetcode_3110.csproj --nologo
dotnet run --no-build --project leetcode_3110/leetcode_3110.csproj
```

本專案目前沒有獨立的自動化測試專案，因此以明確 project path 的 restore、build 與可自動判定結果的 `Main` harness 作為驗證。格式與差異檢查命令為：

```bash
dotnet format leetcode_3110/leetcode_3110.csproj --verify-no-changes --no-restore
git diff --check
```

## 範例執行結果

以下內容取自 `dotnet run --no-build --project leetcode_3110/leetcode_3110.csproj` 的實際執行結果：

<!-- RUN-OUTPUT-START -->
```text
案例：官方範例一
輸入 = "hello"
預期 = 13
ScoreOfString    實際 = 13 => PASS
ScoreOfString2   實際 = 13 => PASS
ScoreOfString3   實際 = 13 => PASS

案例：官方範例二
輸入 = "zaz"
預期 = 50
ScoreOfString    實際 = 50 => PASS
ScoreOfString2   實際 = 50 => PASS
ScoreOfString3   實際 = 50 => PASS

案例：最短同字元
輸入 = "aa"
預期 = 0
ScoreOfString    實際 = 0 => PASS
ScoreOfString2   實際 = 0 => PASS
ScoreOfString3   實際 = 0 => PASS

案例：最短最大差
輸入 = "az"
預期 = 25
ScoreOfString    實際 = 25 => PASS
ScoreOfString2   實際 = 25 => PASS
ScoreOfString3   實際 = 25 => PASS

案例：含重複相鄰字元
輸入 = "aabb"
預期 = 1
ScoreOfString    實際 = 1 => PASS
ScoreOfString2   實際 = 1 => PASS
ScoreOfString3   實際 = 1 => PASS

案例：交錯最大差
輸入 = "azaz"
預期 = 75
ScoreOfString    實際 = 75 => PASS
ScoreOfString2   實際 = 75 => PASS
ScoreOfString3   實際 = 75 => PASS

案例：一般遞增字元
輸入 = "abcde"
預期 = 4
ScoreOfString    實際 = 4 => PASS
ScoreOfString2   實際 = 4 => PASS
ScoreOfString3   實際 = 4 => PASS

案例：長度上限同字元
輸入 = "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz"
預期 = 0
ScoreOfString    實際 = 0 => PASS
ScoreOfString2   實際 = 0 => PASS
ScoreOfString3   實際 = 0 => PASS

總結：24/24 項驗證通過
```
<!-- RUN-OUTPUT-END -->

## 專案結構

```text
leetcode_3110/
├── leetcode_3110/
│   ├── Program.cs
│   └── leetcode_3110.csproj
├── docs/
│   └── readme-template.md
├── leetcode_3110.sln
└── README.md
```
