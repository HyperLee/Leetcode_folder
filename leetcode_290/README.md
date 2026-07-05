# LeetCode 290 - Word Pattern

這個專案是 LeetCode 290「Word Pattern」的 C# Console 範例。主程式會直接執行固定測資，驗證 `pattern` 中的每個字母是否能和 `s` 中的每個單字形成完整的一對一雙射關係。

## 題目說明

給定一個字串 `pattern` 與一個以空白分隔單字的字串 `s`，判斷 `s` 是否遵循 `pattern`。

「遵循」表示必須完整匹配，且 `pattern` 字母與 `s` 單字之間存在雙射：

- 每個 `pattern` 字母只能對應到一個唯一單字。
- 每個唯一單字也只能對應到一個 `pattern` 字母。
- 不能有兩個字母對應到同一個單字，也不能有一個字母在不同位置對應到不同單字。

範例：

```text
pattern = "abba"
s = "dog cat cat dog"
結果 = true
```

對應關係為 `a -> dog`、`b -> cat`，後續位置都維持同樣對應，因此符合規律。

## 限制條件

- `1 <= pattern.length <= 300`
- `pattern` 只包含小寫英文字母。
- `1 <= s.length <= 3000`
- `s` 只包含小寫英文字母與空白。
- `s` 不會有前導或尾端空白。
- `s` 中所有單字都以單一空白分隔。

## 解題概念與出發點

這題的核心不是單向查表，而是「雙向一致」。

若只檢查 `pattern` 字母到單字的方向，可能會漏掉多個字母共用同一個單字的情況。例如：

```text
pattern = "ab"
s = "dog dog"
```

只看正向時，`a -> dog`、`b -> dog` 似乎都能建立對應，但這違反「每個唯一單字只能對應到一個字母」的規則，所以答案必須是 `false`。

因此實作上同時維護兩個 Dictionary：

- `patternToWord`: 記錄 pattern 字元對應到哪個單字。
- `wordToPattern`: 記錄單字反向對應到哪個 pattern 字元。

只要任一方向出現既有對應不一致，就代表雙射關係被破壞，可以立即回傳 `false`。

## 解法一：雙向 Dictionary

### 設計步驟

1. 使用 `s.Split(' ')` 將輸入字串切成單字陣列。
2. 先比較 `pattern.Length` 與單字數量：
   - 數量不同時，代表無法完整一對一配對，直接回傳 `false`。
3. 從左到右同步掃描 `pattern[i]` 與 `words[i]`。
4. 檢查正向對應：
   - 若 `patternCharacter` 已存在於 `patternToWord`，既有單字必須等於目前單字。
   - 若不存在，加入 `patternCharacter -> word`。
5. 檢查反向對應：
   - 若 `word` 已存在於 `wordToPattern`，既有字元必須等於目前字元。
   - 若不存在，加入 `word -> patternCharacter`。
6. 全部位置都沒有衝突時，回傳 `true`。

### 為什麼需要兩個 Dictionary

以 `pattern = "abba"`、`s = "dog dog dog dog"` 為例：

| 位置 | pattern 字元 | 單字 | 正向檢查 | 反向檢查 |
| --- | --- | --- | --- | --- |
| 0 | `a` | `dog` | 建立 `a -> dog` | 建立 `dog -> a` |
| 1 | `b` | `dog` | 可建立 `b -> dog` | `dog` 已對應到 `a`，與目前 `b` 衝突 |

如果只做正向檢查，會允許 `a` 和 `b` 同時對應到 `dog`。反向 Dictionary 可以補上這個缺口。

### 範例演示

#### 範例 1：符合規律

```text
pattern = "abba"
s = "dog cat cat dog"
```

流程：

| 位置 | 比對 | 結果 |
| --- | --- | --- |
| 0 | `a` 與 `dog` | 建立 `a -> dog`、`dog -> a` |
| 1 | `b` 與 `cat` | 建立 `b -> cat`、`cat -> b` |
| 2 | `b` 與 `cat` | 與既有對應一致 |
| 3 | `a` 與 `dog` | 與既有對應一致 |

所有對應都一致，所以回傳 `true`。

#### 範例 2：正向對應衝突

```text
pattern = "abba"
s = "dog cat cat fish"
```

最後一個位置遇到 `a` 與 `fish`。但前面已經建立 `a -> dog`，同一個字母對到不同單字，因此回傳 `false`。

#### 範例 3：反向對應衝突

```text
pattern = "abc"
s = "dog cat dog"
```

第 0 個位置建立 `a -> dog` 與 `dog -> a`。第 2 個位置遇到 `c -> dog`，雖然 `c` 尚未建立正向對應，但 `dog` 已經反向對應到 `a`，所以回傳 `false`。

#### 範例 4：長度不一致

```text
pattern = "abba"
s = "dog cat cat"
```

`pattern` 有 4 個字元，但 `s` 只有 3 個單字，無法完整匹配，直接回傳 `false`。

### 複雜度分析

令 `n` 為 `pattern.Length`，也就是需要比對的位置數。

- 時間複雜度：`O(n)`，每個位置只掃描一次，Dictionary 查詢與寫入平均為 `O(1)`。
- 空間複雜度：`O(n)`，最壞情況下每個字元與每個單字都不同，需要記錄所有對應。

## 專案結構

```text
leetcode_290/
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_290/
    ├── Program.cs
    └── leetcode_290.csproj
```

## 執行方式

建置專案：

```bash
dotnet build leetcode_290/leetcode_290.csproj
```

執行範例測資：

```bash
dotnet run --project leetcode_290/leetcode_290.csproj
```

目前沒有獨立的測試專案；驗證方式是透過 `Main` 中的固定測資輸出，以及 `dotnet build` 確認專案可以成功編譯。

## 實際輸出

以下輸出來自 `dotnet run --project leetcode_290/leetcode_290.csproj`：

```text
LeetCode 290 - Word Pattern
解法：雙向 Dictionary 檢查 pattern 字元與單字是否形成雙射

案例 1：官方範例 1 - 符合 abba 對應
pattern: "abba"
s: "dog cat cat dog"
預期：true，實際：true => PASS

案例 2：官方範例 2 - a 第二次對到不同單字
pattern: "abba"
s: "dog cat cat fish"
預期：false，實際：false => PASS

案例 3：官方範例 3 - 同一字母不能對到不同單字
pattern: "aaaa"
s: "dog cat cat dog"
預期：false，實際：false => PASS

案例 4：常見範例 - 不同字母不能共用同一單字
pattern: "abba"
s: "dog dog dog dog"
預期：false，實際：false => PASS

案例 5：長度不一致 - pattern 與單字數不同
pattern: "abba"
s: "dog cat cat"
預期：false，實際：false => PASS

案例 6：全部唯一 - 每個字母對應不同單字
pattern: "abc"
s: "dog cat fish"
預期：true，實際：true => PASS

案例 7：反向衝突 - dog 已經對應到 a
pattern: "abc"
s: "dog cat dog"
預期：false，實際：false => PASS

總結：7/7 筆測試通過
```
