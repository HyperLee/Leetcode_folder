# LeetCode 208 — Implement Trie (Prefix Tree)

![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4)
![C#](https://img.shields.io/badge/C%23-Console-239120)

使用 C# 與 .NET 10 實作 Trie（Prefix Tree，前綴樹）。專案保留一種固定 26 格子節點陣列的解法，並由 `Main` 執行可重播的操作序列，驗證完整單字搜尋、前綴搜尋、共享路徑、重複插入與單字元邊界。

## 題目連結

- [LeetCode 英文題目](https://leetcode.com/problems/implement-trie-prefix-tree/description/)
- [LeetCode 中文題目](https://leetcode.cn/problems/implement-trie-prefix-tree/description/)

## 題目說明

Trie 是用來儲存與查詢字串集合的樹狀資料結構。這題要求實作 `Trie` 類別，支援三個操作：

- `Insert(string word)`：把 `word` 插入 Trie。
- `Search(string word)`：只有當 `word` 曾被完整插入時才回傳 `true`。
- `StartsWith(string prefix)`：只要存在任何已插入單字以 `prefix` 開頭，就回傳 `true`。

`Search` 和 `StartsWith` 的差別是本題最重要的判斷：

```text
只插入 "apple"

Search("apple")    => true   路徑存在，而且 e 是完整單字結尾
Search("app")      => false  路徑存在，但 p 尚未被標記為完整單字結尾
StartsWith("app")  => true   前綴查詢只要求路徑存在
```

## 限制條件

- `1 <= word.length, prefix.length <= 2000`
- `word` 與 `prefix` 只包含小寫英文字母 `a` 到 `z`
- `Insert`、`Search` 與 `StartsWith` 合計最多呼叫 `3 * 10^4` 次

本實作依照題目契約，直接以 `character - 'a'` 計算子節點索引；呼叫端應提供符合上述限制的輸入。

## 解題概念與出發點

如果用一般集合保存完整單字，能快速判斷某個單字是否存在，但「是否有任意單字以某段文字開頭」就需要額外掃描。Trie 把每個字元視為樹上的一層，讓擁有相同前綴的單字共用節點。

例如依序插入 `car`、`card`、`care`：

```text
(root)
   └─ c
      └─ a
         └─ r*       car
            ├─ d*    card
            └─ e*    care
```

`*` 表示該節點的 `isEnd` 為 `true`。三個單字共用 `c -> a -> r`，只有分歧後的字元需要建立不同節點。

這個設計帶來兩個核心性質：

1. 操作時間只與輸入字串長度有關，不必掃描所有已插入單字。
2. 路徑是否存在與路徑是否構成完整單字分開記錄，因此同時支援 `Search` 和 `StartsWith`。

## 解法一：固定 26 格子節點陣列

### 節點設計

本專案讓每個 `Trie` 物件同時代表一個節點，包含：

- `Trie?[] children`：固定長度 26 的子節點陣列。
- `bool isEnd`：目前路徑是否已被插入為完整單字。

字元與索引的對應方式為：

```text
'a' - 'a' = 0
'b' - 'a' = 1
...
'z' - 'a' = 25
```

陣列中的 `null` 表示該字元路徑尚未建立。由於題目保證輸入只有小寫英文字母，因此不需要額外的雜湊或字元範圍映射。

### 為什麼需要 `isEnd`

只靠子節點無法分辨「完整單字」與「某個較長單字的前綴」。

插入 `apple` 後，`a -> p -> p` 的路徑確實存在，但這只能證明 `app` 是已知前綴。直到另外執行 `Insert("app")`，第二個 `p` 節點才會把 `isEnd` 設為 `true`，讓 `Search("app")` 成立。

同一節點可以同時：

- 是某個完整單字的結尾。
- 是其他更長單字會繼續共用的前綴。

`car`、`card` 與 `care` 中的 `r` 節點就是這種情況。

### `Insert` 流程

輸入 `word` 後：

1. 從根節點開始。
2. 逐字讀取 `word`。
3. 用 `character - 'a'` 算出子節點索引。
4. 若該位置是 `null`，建立新節點；否則沿用既有的共享前綴。
5. 移動到子節點並處理下一個字元。
6. 所有字元完成後，把最後節點的 `isEnd` 設為 `true`。

重複插入同一個單字只會再次把相同節點的 `isEnd` 設為 `true`，不會建立重複路徑，也不會改變查詢語意。

### `SearchPrefix` 共用走訪

`Search` 與 `StartsWith` 都需要確認字元路徑，因此共用私有方法 `SearchPrefix`：

1. 從根節點開始逐字走訪。
2. 任一索引沒有子節點時立即回傳 `null`。
3. 全部字元都能走完時，回傳最後一個節點。

這個方法只回答「路徑是否存在」，不判斷該路徑是不是完整單字。

### `Search` 流程

`Search(word)` 先呼叫 `SearchPrefix(word)`：

- 回傳 `null`：路徑不存在，結果是 `false`。
- 回傳節點但 `isEnd == false`：只有前綴存在，結果仍是 `false`。
- 回傳節點且 `isEnd == true`：完整單字曾被插入，結果是 `true`。

### `StartsWith` 流程

`StartsWith(prefix)` 也呼叫 `SearchPrefix(prefix)`，但不檢查 `isEnd`：

- 找不到路徑時回傳 `false`。
- 只要完整走完前綴就回傳 `true`。

原因是前綴本身不必是一個已插入的完整單字。

## 範例演示流程

### 範例一：官方 `apple`／`app` 流程

| 步驟 | 操作 | 結果 | 說明 |
| ---: | --- | --- | --- |
| 1 | `Insert("apple")` | — | 建立 `a -> p -> p -> l -> e`，只標記 `e` |
| 2 | `Search("apple")` | `true` | 路徑完整，`e.isEnd == true` |
| 3 | `Search("app")` | `false` | 路徑存在，但第二個 `p` 尚未標記 |
| 4 | `StartsWith("app")` | `true` | 前綴只需要路徑存在 |
| 5 | `Insert("app")` | — | 沿用既有路徑，把第二個 `p` 標記為結尾 |
| 6 | `Search("app")` | `true` | `app` 現在也是完整單字 |

最終同一條路徑上會有兩個完整單字結尾：

```text
a -> p -> p* -> l -> e*
          app          apple
```

### 範例二：共享前綴

依序插入 `car`、`card`、`care` 時：

1. `car` 建立三個字元節點，並標記 `r`。
2. `card` 沿用 `c -> a -> r`，只新增 `d`。
3. `care` 同樣沿用前三個節點，只新增 `e`。

查詢結果：

| 操作 | 結果 | 原因 |
| --- | --- | --- |
| `Search("car")` | `true` | `r` 是完整單字結尾 |
| `Search("ca")` | `false` | 路徑存在，但 `a` 不是完整單字結尾 |
| `StartsWith("ca")` | `true` | 三個已插入單字都共享此前綴 |
| `Search("card")` | `true` | `d` 是完整單字結尾 |
| `Search("care")` | `true` | `e` 是完整單字結尾 |

### 範例三：不存在的路徑

在已有 `car`、`card`、`care` 的 Trie 中執行 `StartsWith("cat")`：

```text
c -> a -> ?
          需要 t，但索引 19 沒有子節點
```

`SearchPrefix` 在讀到 `t` 時發現路徑中斷，立即回傳 `null`，因此 `StartsWith` 回傳 `false`。演算法不需要繼續檢查其他單字。

### 範例四：重複插入

```text
Insert("dog")
Insert("dog")
```

第二次插入會沿用第一次建立的 `d -> o -> g`，最後再次設定 `g.isEnd = true`。所以：

- `Search("dog")` 為 `true`。
- `StartsWith("do")` 為 `true`。
- `Search("dogs")` 為 `false`，因為 `g` 之後沒有 `s` 子節點。

### 範例五：單字元邊界

插入 `a` 後，根節點索引 0 直接連到一個 `isEnd == true` 的節點：

- `Search("a")` 為 `true`。
- `StartsWith("a")` 為 `true`。
- `Search("b")` 為 `false`。
- `StartsWith("z")` 為 `false`。

這證明長度為 1 的最小合法輸入會使用和一般單字相同的走訪規則，不需要特殊分支。

## 正確性理由

可以用路徑不變量理解此解法：

- 插入完成後，從根節點依序選擇每個字元的索引，一定能走到該單字的最後節點。
- 最後節點的 `isEnd` 一定為 `true`，而未被插入為完整單字的中途節點不會因路徑存在而自動變成結尾。
- `SearchPrefix` 只有在所有輸入字元都具有對應子節點時才回傳節點。

因此：

- `Search` 的「路徑存在且 `isEnd` 為真」恰好等價於完整單字曾被插入。
- `StartsWith` 的「路徑存在」恰好等價於至少一個已插入單字具有該前綴。

## 複雜度分析

令 `L` 為本次插入或查詢字串的長度，`N` 為整棵 Trie 已建立的節點數。

| 操作 | 時間複雜度 | 額外操作空間 |
| --- | --- | --- |
| `Insert` | `O(L)` | `O(L)` 最壞情況下建立 `L` 個節點 |
| `Search` | `O(L)` | `O(1)` |
| `StartsWith` | `O(L)` | `O(1)` |

整體儲存空間為 `O(N)`：每個節點固定配置 26 個子節點參考與一個 `isEnd`。若把字母表大小寫成變數 `Σ`，更一般的表示是 `O(N * Σ)`；本題 `Σ = 26` 為常數，因此簡化為 `O(N)`。

`N` 最多是根節點加上所有插入字串總字元數；共享前綴會讓實際節點數低於這個上限。

## 可執行驗證

`Main` 會執行 4 組互相獨立的操作序列。每組案例都建立新的 `Trie`，避免前一組插入內容影響下一組，並收集所有 `Search` 與 `StartsWith` 的預期值和實際值。

目前沒有獨立測試專案，因此本 repo 的驗收方式是：

- restore 與 build 確認專案可編譯。
- console harness 驗證 17 次查詢。
- `git diff --check` 檢查空白與換行。

## 建置與執行

需要安裝 [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)。從本 README 所在的 `leetcode_208` 目錄執行：

```bash
dotnet restore leetcode_208/leetcode_208.csproj
dotnet build leetcode_208/leetcode_208.csproj --nologo --no-restore
dotnet run --project leetcode_208/leetcode_208.csproj --no-build
git diff --check -- leetcode_208
git diff --check
```

## 實際執行輸出

以下內容來自：

```bash
dotnet run --project leetcode_208/leetcode_208.csproj --no-build
```

```text
LeetCode 208 - Implement Trie (Prefix Tree)
解法：每個節點使用 26 格子節點陣列，並以 isEnd 區分完整單字與前綴

[PASS] 官方範例 - apple 與 app | Expected: [True, False, True, True] | Actual: [True, False, True, True]
[PASS] 共享前綴與不存在路徑 | Expected: [True, False, True, False, True, True] | Actual: [True, False, True, False, True, True]
[PASS] 重複插入同一單字 | Expected: [True, True, False] | Actual: [True, True, False]
[PASS] 單字元與不存在分支 | Expected: [True, True, False, False] | Actual: [True, True, False, False]

總結：4/4 組案例通過，17/17 次查詢驗證通過。
```

## 專案結構

```text
leetcode_208/
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_208.sln
└── leetcode_208/
    ├── Program.cs
    └── leetcode_208.csproj
```

