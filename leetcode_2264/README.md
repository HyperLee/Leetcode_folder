# leetcode_2264

題目：2264. Largest 3-Same-Digit Number in String

說明

給定一個代表大整數的字串 `num`（允許前導零），找出字串中最大的 "good" 整數，定義為長度為 3 且三個字元相同的子字串（例如 "777" 或 "000"）。如果不存在這樣的子字串，回傳空字串 `""`。

目錄

- 簡介
- 如何執行
- 兩種實作方案
  - 解法 A：`LargestGoodInteger`（char 比較 / 線性掃描）
  - 解法 B：`LargestGoodInteger2`（枚舉 + int.Parse）
- 詳細比較（原理、步驟、邊界情況、時間/空間複雜度、優缺點）
- 範例與輸出

## 簡介

此專案以最小可執行 C# 範例（.NET 8）實作 LeetCode 2264 題目，並包含兩種可行解法，目的是呈現不同思路的實作差異與權衡。

## 如何執行

在已安裝 .NET SDK（建議 8.0）的環境中：

```bash
# 在專案根目錄執行
dotnet run --project ./leetcode_2264/leetcode_2264.csproj
```

主程式會印出範例輸入與兩個方法的結果，方便比較。

## 兩種實作方案

### 解法 A：LargestGoodInteger（char 比較 / 線性掃描）

概念要點

- 對字串 `num` 執行一次線性掃描，對每個索引 `i` 檢查 `num[i]`, `num[i+1]`, `num[i+2]` 是否相等。
- 若三個字元相等，則該三字串是 good 整數，使用字元直接比較（`'0'..'9'` 的字元比較對應數值順序）來更新目前最大字元。
- 最後若找到最大字元 `x`，則回傳 `new string(x, 3)`，否則回傳空字串。

演算法步驟（詳細）

1. 若 `num` 為 null 或長度小於 3，返回空字串。
2. 初始化 `maxDigit` 為 `\0`（代表尚未找到）。
3. 對 `i` 從 0 到 `num.Length - 3`（含）:
   - 讀取 `a = num[i]`, `b = num[i+1]`, `c = num[i+2]`。
   - 若 `a == b && b == c`，則此三字串為 good，若 `a > maxDigit`，更新 `maxDigit = a`。
4. 若 `maxDigit` 仍為 `\0`，回傳空字串，否則回傳 `new string(maxDigit, 3)`。

輸入/輸出契約

- 輸入：字串 `num`（允許前導零）
- 輸出：長度為 3 的字串（例如 "777" 或 "000"），或空字串 `""`。

時間複雜度

- O(n)，其中 n = `num.Length`。

空間複雜度

- O(1)，僅使用常數額外空間。

優點

- 不需要額外轉換或解析，程式簡潔且高效。
- 對於處理 "000" 等情況直接以字元比較，不需特別處理轉換結果。

缺點

- 幾乎沒有，可讀性與效能都很好。


### 解法 B：LargestGoodInteger2（枚舉 + int.Parse）

概念要點

- 枚舉所有長度為 3 的子字串，若三個字元相等，將該子字串透過 `int.Parse` 轉為整數並與目前最大值比較更新。
- 最後根據最大整數決定回傳字串（特別處理 0 與未找到的情況）。

演算法步驟（詳細）

1. 若 `num` 為 null 或長度小於 3，返回空字串。
2. 初始化 `res` 為 -1，代表尚未找到 good。
3. 對 `i` 從 0 到 `num.Length - 3`（含）:
   - 若 `num[i] == num[i+1] && num[i+1] == num[i+2]`，則取得 `num.Substring(i, 3)`，並 `int.Parse(...)` 轉為整數 `val`。
   - 更新 `res = Math.Max(res, val)`。
4. 若 `res == -1`，返回空字串。
5. 若 `res == 0`，返回 `"000"`（確保前導零保留）。否則返回 `res.ToString("D3")`（將數字格式化為三位數，補齊前導零）。

輸入/輸出契約

- 輸入：字串 `num`（允許前導零）
- 輸出：長度為 3 的字串，或空字串 `""`。

時間複雜度

- O(n)，其中 n = `num.Length`。但每次找到 good 子字串會額外呼叫 `Substring` 與 `int.Parse`（常數時間，但常數成本比 char 比較高）。

空間複雜度

- O(1) 額外空間（`Substring` 會建立新的字串片段，取決於 runtime 版本可能會分配短期記憶體）。

優點

- 寫法直觀：以數值比較的方式更新最大值，對於習慣整數比較的人來說更容易理解。

缺點

- 轉換（`int.Parse` 與 `Substring`）造成額外的運算與記憶體分配成本，雖然時間上仍為 O(n)，但常數因子較大。
- 需特別處理 `"000"`（因為 `int.Parse("000") == 0`），導致額外分支處理。


## 詳細比較總結

- 正確性：兩者在功能上等價，皆會正確找出最大的 good 子字串。
- 效能（實務）：`LargestGoodInteger`（char 比較）較佳，因為避免了字串切割與解析的開銷。
- 可讀性：兩者都易讀；`LargestGoodInteger2` 對於以數值思考的人更直觀，但需要注意前導零處理。
- 錯誤面（邊界情況）：兩方法皆需處理長度不足或空字串的情況；`LargestGoodInteger2` 需額外處理 `0` 對應到 `"000"` 的回傳。

## 範例輸出

執行 `dotnet run` 的範例輸出（範例程式碼中印出兩方法的結果與是否相等）：

```text
input=6777133339 => LargestGoodInteger=777, LargestGoodInteger2=777, equal=True (expected=777)
input=2300019 => LargestGoodInteger=000, LargestGoodInteger2=000, equal=True (expected=000)
input=42352338 => LargestGoodInteger=, LargestGoodInteger2=, equal=True (expected=)
input=000 => LargestGoodInteger=000, LargestGoodInteger2=000, equal=True (expected=000)
input=11122111 => LargestGoodInteger=111, LargestGoodInteger2=111, equal=True (expected=111)
```

## 結語

此專案展示了兩個等價但實作細節不同的解法：一個以字元直接比較為主、簡潔且高效；另一個以枚舉並解析為整數來比較，概念直觀但在效能與記憶體上有較多常數開銷。

如需我可：

- 將 `LargestGoodInteger2` 改寫為完全不使用 `int.Parse` 的版本，或
- 為此專案新增 xUnit 測試以自動化覆蓋更多測資。

---
