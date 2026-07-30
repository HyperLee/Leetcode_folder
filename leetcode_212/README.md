# LeetCode 212 - Word Search II

這個專案整理 LeetCode 212「Word Search II（單詞搜索 II）」的 C# / .NET 10 解法。核心實作使用 Trie（字典樹）保存所有候選單詞，再從字母網格的每一格進行 DFS 與回溯搜尋。`Main` 內含五組固定案例，可直接比較預期答案、實際答案，以及確認搜尋完成後輸入網格仍保持原狀。

## 題目說明

給定一個 `m x n` 的字母網格 `board` 與一組單詞 `words`，請找出所有能在網格中組成的單詞。

組成單詞時必須遵守：

- 每一步只能移動到水平或垂直相鄰的格子。
- 必須依照單詞中的字母順序移動。
- 同一個網格位置不能在同一條單詞路徑中重複使用。
- 回傳所有成功找到的單詞，順序不限。

例如：

```text
board = [
  ["o", "a", "a", "n"],
  ["e", "t", "a", "e"],
  ["i", "h", "k", "r"],
  ["i", "f", "l", "v"]
]
words = ["oath", "pea", "eat", "rain"]

找到的單詞 = ["eat", "oath"]
```

`"oath"` 與 `"eat"` 都能沿相鄰格子走出完整路徑；`"pea"` 與 `"rain"` 則找不到。

## 限制條件

依照 [LeetCode 官方題目](https://leetcode.com/problems/word-search-ii/description/?tab=Description)：

- `m == board.length`
- `n == board[i].length`
- `1 <= m, n <= 12`
- `board[i][j]` 只包含小寫英文字母
- `1 <= words.length <= 3 * 10^4`
- `1 <= words[i].length <= 10`
- `words[i]` 只包含小寫英文字母
- `words` 內的單詞彼此不同

## 解題概念與出發點

最直覺的作法是針對每個單詞，各自從網格執行一次 DFS。然而 `words` 最多有三萬個單詞，如果許多單詞擁有相同前綴，例如 `"o"`、`"oa"`、`"oat"`、`"oath"`，逐一搜尋會反覆走訪相同路徑。

這份實作改用「Trie + DFS + 回溯」：

1. 先把所有候選單詞插入 Trie。
2. 網格上的搜尋路徑同時也是 Trie 中的前綴路徑。
3. 如果目前字元在 Trie 對應節點下沒有分支，代表沒有任何候選單詞使用這個前綴，可以立即停止。
4. 如果走到的 Trie 節點保存了完整單詞，就把它加入結果集合。
5. DFS 進入某格後，暫時把該格改成 `'#'`，避免同一路徑重複使用。
6. 探索完四個方向後恢復原字元，讓其他起點與分支可以再次使用該格。

Trie 把大量單詞的共用前綴合併成一條路徑，因此 DFS 不需要為每個單詞重新開始。

## 解法一：Trie + DFS 回溯

這個 repo 只保留一種主要解法。實作可分成 Trie 建構、網格搜尋、結果去重與狀態還原四個部分。

### 1. 建立 Trie

`Trie.children` 是長度為 26 的陣列：

- 索引 `0` 對應 `'a'`
- 索引 `1` 對應 `'b'`
- 依此類推，索引 `25` 對應 `'z'`

插入字元時使用：

```csharp
int index = c - 'a';
```

如果分支不存在才建立新節點，因此具有共同前綴的單詞會共用節點。以 `"oat"` 與 `"oath"` 為例，兩者會共用 `o -> a -> t`，只有 `"oath"` 再多出 `h` 分支。

每個 Trie 節點的 `word` 欄位有兩種狀態：

- 空字串：目前路徑只是前綴，還不是完整單詞。
- 非空字串：目前節點對應一個完整單詞。

直接保存完整單詞可讓 DFS 找到終點時立即加入答案，不必另外重建路徑字串。

### 2. 從每個網格位置啟動 DFS

任何格子都可能是單詞的第一個字母，因此 `FindWords` 會遍歷整個 board，並從每個位置呼叫 `DFS`。

進入某格後，先把字元轉成 Trie 子節點索引。如果對應分支不存在，就立即返回：

```csharp
int index = board[row][col] - 'a';
if (node.children[index] == null)
{
    return;
}
```

這是本解法最重要的剪枝。它表示目前網格路徑不是任何候選單詞的前綴，繼續探索只會浪費時間。

### 3. 找到單詞並避免重複

移動到下一個 Trie 節點後，如果 `node.word` 非空，代表目前路徑形成完整單詞。結果使用 `HashSet<string>` 保存，因此即使同一單詞能由網格中的多條不同路徑組成，也只會回傳一次。

公開方法 `FindWords` 不保證回傳順序。console 驗證器會在比較與顯示前，以序數規則排序預期與實際結果，使每次執行輸出一致。

### 4. 暫時標記與回溯還原

同一個格子不能在單一單詞路徑中重複使用。DFS 進入某格後會先保存原字元，再暫時標記：

```csharp
char c = board[row][col];
board[row][col] = '#';
```

接著只探索仍在邊界內、且尚未標記為 `'#'` 的上下左右相鄰位置。完成所有分支後恢復：

```csharp
board[row][col] = c;
```

因此 `FindWords` 執行期間會暫時修改 board，但正常返回前會恢復所有內容。範例驗證器會在每個案例執行前深層複製 board，執行後逐列比較，讓這項回溯契約成為可執行檢查。

## 範例演示流程

### 案例一：官方 4 x 4 範例

輸入：

```text
board = [["o", "a", "a", "n"], ["e", "t", "a", "e"], ["i", "h", "k", "r"], ["i", "f", "l", "v"]]
words = ["oath", "pea", "eat", "rain"]
```

流程：

1. 所有單詞先插入 Trie，根節點產生 `o`、`p`、`e`、`r` 四個有效起始分支。
2. DFS 從左上角 `o` 出發，依序走到 `a -> t -> h`，Trie 節點保存 `"oath"`，因此加入結果。
3. 從第二列右側的 `e` 出發，可以走到 `a -> t`，找到 `"eat"`。
4. `"pea"` 與 `"rain"` 的必要前綴無法在相鄰格中延伸，搜尋被 Trie 剪枝。
5. 排序後結果為 `["eat", "oath"]`，board 也完整還原。

### 案例二：同一格不可重複使用

輸入：

```text
board = [["a", "b"], ["c", "d"]]
words = ["abcb"]
```

流程：

1. 可以從 `a` 移動到相鄰的 `b`。
2. 下一個字母需要 `c`，但從右上角 `b` 無法直接移到左下角 `c`。
3. 即使嘗試其他分支，也不能重新使用已標記的格子湊出 `"abcb"`。
4. 最終結果為空集合 `[]`，並確認 board 已還原。

### 案例三：單格邊界

輸入：

```text
board = [["a"]]
words = ["a"]
```

流程：

1. Trie 根節點存在 `a` 分支，而且該節點保存完整單詞 `"a"`。
2. DFS 一進入唯一格子就找到答案。
3. 網格沒有其他相鄰位置，回溯後恢復唯一格。
4. 結果為 `["a"]`。

### 案例四：共用前綴

輸入：

```text
board = [["o", "a", "t", "h"]]
words = ["o", "oa", "oat", "oath", "hat"]
```

流程：

1. `"o"`、`"oa"`、`"oat"`、`"oath"` 在 Trie 中共用同一條前綴路徑。
2. DFS 從左向右移動，每走到一個保存完整單詞的節點，就加入一個答案。
3. 依序找到 `"o"`、`"oa"`、`"oat"`、`"oath"`。
4. `"hat"` 無法由相鄰格依正確順序組成，因此不會加入結果。

這個案例展示 Trie 如何在一次路徑探索中同時辨識多個共用前綴的單詞。

### 案例五：多條路徑與結果去重

輸入：

```text
board = [["a", "a"], ["a", "a"]]
words = ["a", "aa", "aaa", "aaaa"]
```

流程：

1. 每一格都可以作為 `"a"` 的起點，也有多種方式可以走出 `"aa"`、`"aaa"` 與 `"aaaa"`。
2. DFS 以 `'#'` 標記目前路徑，確保建立 `"aaaa"` 時四個位置各使用一次。
3. 同一單詞雖然可能由不同起點或不同方向找到，`HashSet` 只保留一份。
4. 排序後結果固定為 `["a", "aa", "aaa", "aaaa"]`。

## 複雜度分析

定義：

- `m`、`n`：網格的列數與欄數
- `S`：`words` 中所有字元的總數
- `L`：候選單詞的最大長度
- `R`：輸出結果中所有單詞的總字元數

### 時間複雜度

- 建立 Trie：`O(S)`，每個候選單詞字元插入一次。
- 網格搜尋：最壞情況可表示為 `O(mn × 4 × 3^(L-1))`。
  - 每一格都可能成為起點。
  - 第一層最多有四個方向。
  - 之後因為前一格已標記、不能立刻重複使用，每層最多繼續往三個方向延伸。
- 常見的較寬鬆寫法是 `O(mn × 4^L)`。
- 實際執行通常會因 Trie 前綴不存在而提早剪枝，遠少於完整列舉所有路徑。

### 空間複雜度

- Trie：`O(S)`。
- DFS 遞迴呼叫堆疊：`O(L)`。
- 去除回傳結果後的主要輔助空間為 `O(S + L)`。
- 若把輸出集合也計入，還需要 `O(R)`。
- DFS 直接在 board 上暫時標記，因此不需要額外的 `m x n` visited 陣列。

## 執行範例

從 `leetcode_212` repository 根目錄執行：

```bash
dotnet run --project leetcode_212/leetcode_212.csproj
```

目前輸出：

```text
LeetCode 212 - Word Search II
案例 1：官方 4 x 4 範例
  Board: [["o", "a", "a", "n"], ["e", "t", "a", "e"], ["i", "h", "k", "r"], ["i", "f", "l", "v"]]
  Words: ["oath", "pea", "eat", "rain"]
  Expected: ["eat", "oath"]
  Actual:   ["eat", "oath"] (PASS)
  Board restored: PASS

案例 2：同一格不可重複使用
  Board: [["a", "b"], ["c", "d"]]
  Words: ["abcb"]
  Expected: []
  Actual:   [] (PASS)
  Board restored: PASS

案例 3：單格邊界
  Board: [["a"]]
  Words: ["a"]
  Expected: ["a"]
  Actual:   ["a"] (PASS)
  Board restored: PASS

案例 4：共用前綴
  Board: [["o", "a", "t", "h"]]
  Words: ["o", "oa", "oat", "oath", "hat"]
  Expected: ["o", "oa", "oat", "oath"]
  Actual:   ["o", "oa", "oat", "oath"] (PASS)
  Board restored: PASS

案例 5：多條路徑與結果去重
  Board: [["a", "a"], ["a", "a"]]
  Words: ["a", "aa", "aaa", "aaaa"]
  Expected: ["a", "aa", "aaa", "aaaa"]
  Actual:   ["a", "aa", "aaa", "aaaa"] (PASS)
  Board restored: PASS

總結：10/10 項驗證通過
```

## 驗證指令

本 repo 目前沒有獨立測試專案；驗證方式是還原與建置主專案，再執行 `Main` 中的固定 sample harness。

```bash
dotnet restore leetcode_212/leetcode_212.csproj
dotnet build leetcode_212/leetcode_212.csproj --nologo
dotnet run --project leetcode_212/leetcode_212.csproj
git diff --check
```

每個 sample 有兩項檢查：

1. 排序後的實際單詞集合是否等於預期集合。
2. DFS 完成後 board 是否與執行前完全相同。

## 專案結構

```text
leetcode_212/
├── leetcode_212/
│   ├── Program.cs
│   └── leetcode_212.csproj
├── docs/
│   └── readme-template.md
├── AGENTS.md
├── leetcode_212.sln
└── README.md
```
