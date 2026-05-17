# LeetCode 49 - Group Anagrams

這是一個 .NET 10 Console 專案，用 C# 實作 LeetCode 49「Group Anagrams / 字母異位詞分組」。

專案目前提供兩種解法：

- 排序 Key 解法：將每個字串排序後作為 Dictionary key。
- 字母計數解法：統計 26 個小寫英文字母出現次數後作為 Dictionary key。

## 題目說明

給定一個字串陣列 `strs`，請將所有字母異位詞分組。答案可以用任意順序回傳。

字母異位詞代表字母種類與出現次數完全相同，但排列順序可以不同。例如：

- `"eat"`、`"tea"`、`"ate"` 互為字母異位詞。
- `"tan"`、`"nat"` 互為字母異位詞。
- `"bat"` 沒有其他同組字串時，會單獨形成一組。

## 限制條件

依題目設定：

- `1 <= strs.length <= 10^4`
- `0 <= strs[i].Length <= 100`
- `strs[i]` 只包含小寫英文字母。
- 輸出群組順序不拘，群組內字串順序也不拘。

> 注意：`GroupAnagrams2` 使用長度 26 的陣列計算 `c - 'a'`，因此輸入必須符合小寫英文字母限制。

## 解題概念與出發點

這題的核心是「如何判斷兩個字串是否屬於同一組字母異位詞」。

直接兩兩比較所有字串會讓複雜度過高。比較好的做法是為每個字串建立一個標準化 key，讓同一組字母異位詞產生相同 key，再用 Dictionary 將原始字串累積到同一個群組。

本專案實作兩種 key 的設計：

- 排序後字串：`"eat"`、`"tea"`、`"ate"` 排序後都會變成 `"aet"`。
- 字母計數：`"eat"`、`"tea"`、`"ate"` 的字母出現次數都會變成 `"a1e1t1"`。

## `Dictionary.TryGetValue` 用法說明

兩種解法都會使用 `Dictionary<string, IList<string>> groupsByKey` 來保存分組結果：

- `key`：代表一組字母異位詞的標準化結果，例如 `"aet"` 或 `"a1e1t1"`。
- `value`：這個 key 對應到的原始字串清單，例如 `["eat", "tea"]`。

`TryGetValue` 的用途是「嘗試用 key 從 Dictionary 取出既有 value」：

```csharp
bool found = groupsByKey.TryGetValue(key, out IList<string>? group);
```

- 如果 Dictionary 已經有這個 `key`，`TryGetValue` 會回傳 `true`，並把既有群組放進 `group`。
- 如果 Dictionary 還沒有這個 `key`，`TryGetValue` 會回傳 `false`，此時需要自己建立新的群組。

本題使用的寫法如下：

```csharp
if (!groupsByKey.TryGetValue(key, out IList<string>? group))
{
    group = new List<string>();
    groupsByKey[key] = group;
}

group.Add(strs[i]);
```

這段可以拆成三個步驟理解：

1. `TryGetValue(key, out group)` 先檢查這個 key 是否已經有群組。
2. 前面的 `!` 表示「如果找不到」，就進入 `if` 建立新的 `List<string>`，再用 `groupsByKey[key] = group` 放回 Dictionary。
3. 離開 `if` 後，`group` 一定會指向某個清單；可能是剛建立的新清單，也可能是 Dictionary 裡原本就存在的清單，所以可以直接呼叫 `group.Add(strs[i])`。

以排序 Key 解法為例：

| 原始字串 | key | `TryGetValue` 結果 | 後續動作 |
| --- | --- | --- | --- |
| `"eat"` | `"aet"` | 找不到，回傳 `false` | 建立新清單，再加入 `"eat"` |
| `"tea"` | `"aet"` | 找到，回傳 `true` | 取出既有清單，再加入 `"tea"` |
| `"ate"` | `"aet"` | 找到，回傳 `true` | 取出既有清單，再加入 `"ate"` |

重要觀念是：`group` 指向的是 Dictionary 裡保存的同一個 `List<string>` 物件。因此當執行 `group.Add(...)` 時，Dictionary 中對應 key 的清單內容也會一起更新。

這種寫法比先 `ContainsKey` 再用 `groupsByKey[key]` 取值更直接，因為 `TryGetValue` 可以一次完成「確認 key 是否存在」與「取出既有 value」兩件事，也能避免在 key 不存在時直接用索引取值造成錯誤。

## 解法一：排序 Key

### 設計說明

`GroupAnagrams` 會逐一處理輸入字串：

1. 將字串轉成 `char[]`。
2. 對字元陣列排序。
3. 將排序後的字元陣列轉回字串，作為 Dictionary key。
4. 若 key 已存在，將原始字串加入既有群組。
5. 若 key 不存在，建立新群組。
6. 最後回傳 Dictionary 內的所有群組。

這個方法容易理解，也能處理空字串。空字串排序後仍是空字串，因此所有空字串會被放入同一組。

### 範例演示流程

輸入：

```text
["eat", "tea", "tan", "ate", "nat", "bat"]
```

逐步建立 key：

| 原始字串 | 排序後 key | Dictionary 分組狀態 |
| --- | --- | --- |
| `"eat"` | `"aet"` | `"aet"`: `["eat"]` |
| `"tea"` | `"aet"` | `"aet"`: `["eat", "tea"]` |
| `"tan"` | `"ant"` | `"ant"`: `["tan"]` |
| `"ate"` | `"aet"` | `"aet"`: `["eat", "tea", "ate"]` |
| `"nat"` | `"ant"` | `"ant"`: `["tan", "nat"]` |
| `"bat"` | `"abt"` | `"abt"`: `["bat"]` |

正規化展示後的結果：

```text
[["ate", "eat", "tea"], ["bat"], ["nat", "tan"]]
```

### 複雜度

假設 `n` 是字串數量，`k` 是單一字串最大長度：

- 時間複雜度：`O(n * k log k)`，每個字串都需要排序。
- 空間複雜度：`O(n * k)`，Dictionary 需要保存 key 與所有原始字串。

## 解法二：字母計數 Key

### 設計說明

`GroupAnagrams2` 會利用題目限制「字串只包含小寫英文字母」：

1. 為每個字串建立長度 26 的 `int[] counts`。
2. 掃描字串中的每個字元，將 `counts[c - 'a']` 加 1。
3. 將非 0 的字母與次數組成 key，例如 `"eat"` 會得到 `"a1e1t1"`。
4. 若 key 已存在，將原始字串加入既有群組。
5. 若 key 不存在，建立新群組。
6. 最後回傳 Dictionary 內的所有群組。

這個方法避免對每個字串排序。當字串較長時，計數法通常比排序法更有利。

空字串沒有任何字母，key 會是空字串 `""`，因此仍能正確分組。

### 範例演示流程

輸入：

```text
["eat", "tea", "tan", "ate", "nat", "bat"]
```

逐步建立 key：

| 原始字串 | 計數 key | Dictionary 分組狀態 |
| --- | --- | --- |
| `"eat"` | `"a1e1t1"` | `"a1e1t1"`: `["eat"]` |
| `"tea"` | `"a1e1t1"` | `"a1e1t1"`: `["eat", "tea"]` |
| `"tan"` | `"a1n1t1"` | `"a1n1t1"`: `["tan"]` |
| `"ate"` | `"a1e1t1"` | `"a1e1t1"`: `["eat", "tea", "ate"]` |
| `"nat"` | `"a1n1t1"` | `"a1n1t1"`: `["tan", "nat"]` |
| `"bat"` | `"a1b1t1"` | `"a1b1t1"`: `["bat"]` |

正規化展示後的結果：

```text
[["ate", "eat", "tea"], ["bat"], ["nat", "tan"]]
```

### 複雜度

假設 `n` 是字串數量，`k` 是單一字串最大長度：

- 時間複雜度：`O(n * (k + 26))`，每個字串掃描一次，並固定檢查 26 個字母。
- 空間複雜度：`O(n * k)`，Dictionary 需要保存 key 與所有原始字串。

## 兩種解法比較與推薦

| 比較項目 | 排序 Key 解法：`GroupAnagrams` | 字母計數解法：`GroupAnagrams2` |
| --- | --- | --- |
| 核心想法 | 將每個字串排序後作為分組 key。 | 統計 26 個小寫英文字母出現次數後作為分組 key。 |
| 時間複雜度 | `O(n * k log k)` | `O(n * (k + 26))`，可視為 `O(n * k)` |
| 優點 | 概念直覺、程式碼容易理解，也較容易延伸到不同字元集合。 | 不需要排序，符合題目只含小寫英文字母的限制時通常效率較好。 |
| 缺點 | 每個字串都需要排序，字串越長時成本越明顯。 | 依賴小寫英文字母限制；若輸入可能包含其他字元，需要調整計數方式或 key 設計。 |
| 適用情境 | 適合第一次理解題目、面試中快速寫出清楚解法，或輸入字元集合不固定時。 | 適合 LeetCode 原題限制下追求較佳時間複雜度的解法。 |

推薦使用 `GroupAnagrams2` 字母計數解法作為本題主要答案，因為 LeetCode 題目已明確限制輸入只包含小寫英文字母，計數 key 可以避免每個字串排序，時間複雜度較有優勢。

如果重點是教學、可讀性，或想先用最直覺的方式說明字母異位詞分組，`GroupAnagrams` 排序 Key 解法也很適合保留作為入門版本。

## 專案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_049/
    ├── Program.cs
    └── leetcode_049.csproj
```

## 建置與執行

請從 repository root 執行命令。

建置：

```bash
dotnet build leetcode_049/leetcode_049.csproj
```

成功時應看到 `建置成功。`，並且結果為 `0 個警告`、`0 個錯誤`。

執行可執行範例：

```bash
dotnet run --project leetcode_049/leetcode_049.csproj
```

程式輸出：

```text
49. Group Anagrams - 可執行範例

範例 1：一般分組
Input: ["eat", "tea", "tan", "ate", "nat", "bat"]
排序 Key 解法: [["ate", "eat", "tea"], ["bat"], ["nat", "tan"]]
字母計數解法: [["ate", "eat", "tea"], ["bat"], ["nat", "tan"]]

範例 2：空字串
Input: [""]
排序 Key 解法: [[""]]
字母計數解法: [[""]]

範例 3：單一字串
Input: ["a"]
排序 Key 解法: [["a"]]
字母計數解法: [["a"]]

範例 4：重複字與多組
Input: ["", "b", "bb", "b", "abc", "cab", "bac"]
排序 Key 解法: [[""], ["abc", "bac", "cab"], ["b", "b"], ["bb"]]
字母計數解法: [[""], ["abc", "bac", "cab"], ["b", "b"], ["bb"]]
```

> 本機 macOS/.NET SDK 環境可能在程式輸出前顯示 `CSSM_ModuleLoad(): One or more parameters passed to a function were not valid.`。這是 SDK 或系統層級訊息，不是此專案的 `Console.WriteLine` 輸出。

## 驗證

目前沒有獨立測試專案，因此以建置、範例執行與 Git 空白檢查作為驗證。

```bash
dotnet build leetcode_049/leetcode_049.csproj
dotnet run --project leetcode_049/leetcode_049.csproj
git diff --check
```

驗證重點：

- `dotnet build` 應成功，且沒有警告或錯誤。
- `dotnet run` 的程式輸出應與 README 中的範例一致。
- `git diff --check` 不應輸出多餘空白或換行問題。
