# LeetCode 211：添加與搜尋單字－資料結構設計

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![Language](https://img.shields.io/badge/Language-C%23-239120)
![Validation](https://img.shields.io/badge/Validation-30%2F30_PASS-brightgreen)

以 .NET 10 Console App 實作
[LeetCode 211. Design Add and Search Words Data Structure](https://leetcode.com/problems/design-add-and-search-words-data-structure/description/)。
專案提供固定 26 子節點的 Trie＋DFS，以及依長度分桶後逐字比對兩種解法，
並以相同的狀態操作序列驗證精確搜尋、萬用字元、共享字首、長度差異與重複加入。

## 快速連結

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：固定陣列 Trie＋DFS](#解法一固定陣列-triedfs)
- [解法二：長度分桶＋逐字比對](#解法二長度分桶逐字比對)
- [兩種解法比較](#兩種解法比較)
- [可執行案例](#可執行案例)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)

## 題目說明

設計一個 `WordDictionary`，支援以下操作：

- `WordDictionary()`：建立空的單字資料結構。
- `AddWord(word)`：加入一個單字，之後可以搜尋該單字。
- `Search(word)`：判斷是否存在與 `word` 完整匹配的已加入單字。

`Search` 的模式可以包含句點 `.`。每個 `.` 只代表「任意一個」小寫英文字母，
不能代表零個字元或多個字元，因此模式與候選單字的長度必須相同。

官方操作範例：

```text
WordDictionary wordDictionary = new WordDictionary();
wordDictionary.AddWord("bad");
wordDictionary.AddWord("dad");
wordDictionary.AddWord("mad");
wordDictionary.Search("pad"); // false
wordDictionary.Search("bad"); // true
wordDictionary.Search(".ad"); // true
wordDictionary.Search("b.."); // true
```

`pad` 不存在；`bad` 可以精確匹配；`.ad` 可以匹配 `bad`、`dad` 或 `mad`；
`b..` 則可以匹配以 `b` 開頭、總長度為 3 的 `bad`。

## 限制條件

- `1 <= word.length <= 25`。
- `AddWord` 的 `word` 只包含小寫英文字母。
- `Search` 的 `word` 只包含小寫英文字母或句點 `.`。
- 每個搜尋模式最多包含兩個句點。
- `AddWord` 與 `Search` 的呼叫總數最多為 `10^4`。
- 搜尋要求完整單字匹配；已存在的字首不等於已加入的完整單字。

## 解題概念與出發點

這題同時包含兩種查詢需求：

1. **精確字元**：目前位置只能沿指定字母前進。
2. **萬用字元**：目前位置可以選擇任一字母，但仍只消耗一個字元。

若逐次掃描所有已加入單字，精確查詢也要付出與單字總數相關的成本。Trie 將相同
字首合併成同一條路徑，普通字元能直接定位唯一分支；只有遇到 `.` 時才需要分岔。
這是本題最典型的解法。

不過，`Search` 一定要求完整長度相同。利用這個條件，也可以先將單字依長度分類，
把搜尋範圍縮小到同長度集合，再逐字檢查萬用字元。第二種解法的程式與資料結構
較直接，適合用來理解「加入成本、搜尋成本、記憶體配置」之間的取捨。

以下複雜度使用：

- `m`：加入單字或搜尋模式的長度。
- `d`：搜尋模式中的句點數，本題 `d <= 2`。
- `k_m`：目前已儲存、長度恰好為 `m` 的不同單字數。
- `t`：Trie 中實際建立的節點總數。
- `S`：所有不同單字的字元總數。

## 解法一：固定陣列 Trie＋DFS

### 資料結構設計

`WordDictionary` 保存一個 Trie 根節點。每個 `Trie` 節點包含：

- `children[0..25]`：分別代表 `a` 到 `z` 的下一個節點。
- `isEnd`：標記從根走到目前位置是否形成一個已加入的完整單字。

例如依序加入 `bad`、`dad`、`mad` 後，根節點會有 `b`、`d`、`m` 三條分支：

```text
root
├── b ── a ── d*
├── d ── a ── d*
└── m ── a ── d*
```

星號表示該節點的 `isEnd` 為 `true`。終點標記不能省略：若只加入 `apple`，
`app` 雖然是 Trie 中存在的路徑，仍不是已加入的完整單字。

固定 26 格陣列讓普通字元可以透過 `character - 'a'` 直接定位，不需雜湊或逐一
尋找子節點。代價是即使節點只有一個子節點，也仍保留 26 個參考位置。

### `AddWord` 演算法

1. 從根節點開始。
2. 對單字的每個字元計算 `0..25` 索引。
3. 若對應子節點不存在，建立新的 `Trie` 節點。
4. 移動到該子節點並處理下一個字元。
5. 走完單字後，將最後節點的 `isEnd` 設為 `true`。

相同字首會重用既有路徑。例如先加入 `apple`，再加入 `apply` 時，
`a → p → p → l` 完全共享，只需新增最後的 `y` 分支。

### `Search` 與 DFS 演算法

DFS 狀態由「目前 Trie 節點」及「下一個模式索引」共同決定：

1. 若索引已到模式末端，只回傳目前節點的 `isEnd`。
2. 若目前字元是普通字母：
   - 計算唯一子節點索引。
   - 子節點不存在時立即失敗。
   - 子節點存在時遞迴比對下一個位置。
3. 若目前字元是 `.`：
   - 依序檢查目前節點的 26 個子節點。
   - 對每個存在的子節點遞迴比對下一個位置。
   - 任一分支成功即可回傳 `true`；全部失敗才回傳 `false`。

這裡不會將 `.` 寫入 Trie。句點只在搜尋時改變「下一步可以選哪些既有分支」。

### 正確性說明

加入單字時，每個字元依序對應一條 Trie 邊，因此從根到最後節點的路徑恰好表示
該單字；最後設定 `isEnd`，可區分完整單字與純字首。

搜尋普通字元時，任何合法匹配都只能選擇該字元對應的唯一子節點。搜尋 `.` 時，
合法匹配可以使用任一小寫字母，而演算法會完整枚舉目前存在的所有字母分支。
所以 DFS 不會遺漏任何可能匹配，也不會接受與模式字元不相容的路徑。

最後，只有模式與 Trie 路徑同時結束且 `isEnd` 為 `true` 才成功，因此回傳
`true` 當且僅當資料結構中存在與整個模式完整匹配的單字。

### 官方範例演示

先加入 `bad`、`dad`、`mad`：

| 操作 | Trie 動作 | 結果 |
| --- | --- | --- |
| `AddWord("bad")` | 建立 `b → a → d`，標記最後的 `d` | 保存 `bad` |
| `AddWord("dad")` | 建立 `d → a → d`，標記最後的 `d` | 保存 `dad` |
| `AddWord("mad")` | 建立 `m → a → d`，標記最後的 `d` | 保存 `mad` |

接著搜尋：

| 模式 | DFS 流程 | 結果 |
| --- | --- | --- |
| `pad` | 根節點沒有 `p` 子節點 | `false` |
| `bad` | 唯一路徑 `b → a → d` 存在，終點 `isEnd=true` | `true` |
| `.ad` | `.` 嘗試根的分支；`b → a → d` 可完成匹配 | `true` |
| `b..` | 先走 `b`；兩個 `.` 分別選到 `a`、`d`，且抵達單字終點 | `true` |

DFS 在找到第一條成功路徑後會提早回傳，不必繼續嘗試其他分支。

### 複雜度

- `AddWord` 時間：`O(m)`，每個字元處理一次。
- 精確搜尋時間：`O(m)`。
- 含萬用字元搜尋時間：最壞 `O(26^d × m)`；實際只走 Trie 中存在的分支，
  且本題 `d <= 2`。
- 搜尋額外空間：`O(m)`，來自最深 `m` 層的遞迴呼叫堆疊。
- 儲存空間：`O(26t)` 個子節點參考；因字母表大小固定，也常簡寫為 `O(t)`。
- 輸出空間：`O(1)`，只回傳布林值。

## 解法二：長度分桶＋逐字比對

### 資料結構設計

`WordDictionary2` 使用：

```text
Dictionary<int, HashSet<string>> wordsByLength
```

字典鍵是單字長度，值是該長度的不同單字集合。例如加入目前範例單字後：

```text
1 → { "a" }
2 → { "at" }
3 → { "bad", "dad", "mad", "app" }
4 → { "code" }
5 → { "apple", "apply", "coder" }
```

`HashSet<string>` 有兩個目的：

1. 重複呼叫 `AddWord("app")` 不會保存重複項目。
2. 不含 `.` 的精確模式可以直接使用集合查找，不必掃描整個分桶。

### `AddWord` 演算法

1. 以 `word.Length` 查找分桶。
2. 分桶不存在時建立新的 `HashSet<string>`。
3. 將完整單字加入集合。

### `Search` 演算法

1. 以模式長度尋找分桶；不存在同長度單字時直接回傳 `false`。
2. 若模式不含 `.`，直接呼叫該分桶的 `Contains`。
3. 若模式含 `.`，逐一處理同長度候選：
   - 普通模式字元必須等於候選的同位置字元。
   - 模式字元為 `.` 時跳過字元相等檢查。
   - 任一候選的所有位置都相容時回傳 `true`。
4. 所有候選都不匹配時回傳 `false`。

### 正確性說明

句點只匹配一個字元，因此任何完整匹配的候選必須和模式等長。先選擇相同長度
分桶不會排除合法答案。

對每個同長度候選，逐字比對會拒絕任一普通字元不同的位置，並接受 `.` 所在位置
的任意小寫字母。因此候選通過檢查，當且僅當它與模式的每個位置都相容。
演算法檢查所有同長度候選，所以回傳 `true` 當且僅當分桶中至少存在一個完整匹配。

### 官方範例演示

加入 `bad`、`dad`、`mad` 後，長度 3 的分桶為：

```text
3 → { "bad", "dad", "mad" }
```

搜尋流程：

| 模式 | 分桶與逐字比對 | 結果 |
| --- | --- | --- |
| `pad` | 直接查長度 3 集合，沒有 `pad` | `false` |
| `bad` | 直接查長度 3 集合，找到 `bad` | `true` |
| `.ad` | 掃描長度 3；`.` 接受 `b`，後兩位 `a`、`d` 相同 | `true` |
| `b..` | 掃描長度 3；`bad` 首位是 `b`，後兩位由 `.` 接受 | `true` |

若搜尋 `app.`，只會檢查長度 4 的分桶。即使資料中已有 `apple` 與 `apply`，
它們位於長度 5 分桶，不會被誤認為匹配。

### 複雜度

- `AddWord` 平均時間：`O(m)`，主要來自字串雜湊。
- 精確搜尋平均時間：`O(m)`，直接計算模式雜湊並查集合。
- 含萬用字元搜尋時間：`O(k_m × m)`，最壞需比較同長度的每個候選。
- 搜尋額外空間：`O(1)`，逐字比對只使用索引與目前候選參考。
- 儲存空間：`O(S)`，另有分桶與雜湊集合的管理成本。
- 輸出空間：`O(1)`，只回傳布林值。

## 兩種解法比較

| 比較項目 | 固定陣列 Trie＋DFS | 長度分桶＋逐字比對 |
| --- | --- | --- |
| 加入方式 | 逐字建立或重用節點 | 將完整字串加入對應長度集合 |
| 精確搜尋 | 沿唯一 Trie 路徑 | `HashSet.Contains` |
| 萬用字元搜尋 | 只在 `.` 位置探索 Trie 分支 | 掃描所有同長度候選 |
| 加入時間 | `O(m)` | 平均 `O(m)` |
| 精確搜尋時間 | `O(m)` | 平均 `O(m)` |
| 萬用字元搜尋時間 | 最壞 `O(26^d × m)` | `O(k_m × m)` |
| 主要額外空間 | 每節點固定 26 個參考 | 完整字串與雜湊集合 |
| 重複加入 | 重走同一路徑並重設終點 | `HashSet` 自動去重 |
| 優點 | 大量單字共享字首時能剪除無效路徑 | 實作直觀，長度不符時立即失敗 |
| 注意事項 | 稀疏節點仍配置 26 格陣列 | 同長度單字很多時，萬用字元搜尋需大量比較 |

Trie 更貼近本題希望練習的資料結構，也讓普通字元快速縮小搜尋範圍。長度分桶
適合資料量較小、需要簡單實作或想避免遞迴時使用。兩者都遵守相同公開操作語意。

## 可執行案例

`Main` 會建立兩個獨立資料結構，依序執行相同的四個階段。每個階段先加入單字，
再比較每次 `Search` 的 Expected 與 Actual。狀態會在同一解法的階段間保留，
但不會在兩種解法之間共用。

| 階段 | 加入單字 | 搜尋與預期結果 | 驗證重點 |
| --- | --- | --- | --- |
| 官方基本案例 | `bad`, `dad`, `mad` | `pad=false`, `bad=true`, `.ad=true`, `b..=true` | 精確命中、缺少字首、前置與尾端萬用字元 |
| 共享前綴與長度 | `apple`, `apply` | `app=false`, `a..le=true`, `a..ly=true`, `app.=false` | 字首不等於完整單字、共享字首、長度必須相同 |
| 前綴成為完整單字 | `app`, `app` | `app=true`, `ap.=true` | 新增終點標記與重複加入 |
| 單字元、長度與失敗分支 | `a`, `at`, `code`, `coder` | `.=true`, `..=true`, `c.de=true`, `c..er=true`, `z.=false` | 最短長度、兩個萬用字元、內部萬用字元與失敗分支 |

每種解法有 15 項搜尋驗證，總計 30 項。若任何 Actual 與 Expected 不同，
該項顯示 `FAIL`，總結顯示 `Overall: FAIL`，並設定非零程序結束碼。

## 專案結構

```text
leetcode_211/
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── docs/
│   └── readme-template.md
├── leetcode_211/
│   ├── 208與211題目比對.md
│   ├── leetcode_211.csproj
│   └── Program.cs
├── AGENTS.md
├── leetcode_211.sln
└── README.md
```

## 建置與執行

需求：

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

請從本 README 所在的 `leetcode_211` 專案根目錄依序執行：

```bash
dotnet restore leetcode_211/leetcode_211.csproj
dotnet build leetcode_211/leetcode_211.csproj --no-restore --nologo
dotnet run --project leetcode_211/leetcode_211.csproj --no-build
```

目前沒有獨立的自動化測試專案。驗收方式是確認專案以零警告完成建置，
再執行 `Main` 中可重複執行的 Expected/Actual 案例。

若要額外確認程式碼格式與 Git 空白問題，可執行：

```bash
dotnet format leetcode_211/leetcode_211.csproj --no-restore --verify-no-changes
git diff --check
```

## 實際執行結果

以下內容來自：

```bash
dotnet run --project leetcode_211/leetcode_211.csproj --no-build
```

```text
LeetCode 211：添加與搜尋單字 - 雙解法驗證

解法一：固定陣列 Trie + DFS
  階段 1：官方基本案例
  AddWord: bad, dad, mad
    Search("pad") | Expected: False | Actual: False => PASS
    Search("bad") | Expected: True | Actual: True => PASS
    Search(".ad") | Expected: True | Actual: True => PASS
    Search("b..") | Expected: True | Actual: True => PASS
  階段 2：共享前綴與長度
  AddWord: apple, apply
    Search("app") | Expected: False | Actual: False => PASS
    Search("a..le") | Expected: True | Actual: True => PASS
    Search("a..ly") | Expected: True | Actual: True => PASS
    Search("app.") | Expected: False | Actual: False => PASS
  階段 3：前綴成為完整單字
  AddWord: app, app
    Search("app") | Expected: True | Actual: True => PASS
    Search("ap.") | Expected: True | Actual: True => PASS
  階段 4：單字元、長度與失敗分支
  AddWord: a, at, code, coder
    Search(".") | Expected: True | Actual: True => PASS
    Search("..") | Expected: True | Actual: True => PASS
    Search("c.de") | Expected: True | Actual: True => PASS
    Search("c..er") | Expected: True | Actual: True => PASS
    Search("z.") | Expected: False | Actual: False => PASS
  小計：15/15 項驗證通過

解法二：長度分桶 + 逐字比對
  階段 1：官方基本案例
  AddWord: bad, dad, mad
    Search("pad") | Expected: False | Actual: False => PASS
    Search("bad") | Expected: True | Actual: True => PASS
    Search(".ad") | Expected: True | Actual: True => PASS
    Search("b..") | Expected: True | Actual: True => PASS
  階段 2：共享前綴與長度
  AddWord: apple, apply
    Search("app") | Expected: False | Actual: False => PASS
    Search("a..le") | Expected: True | Actual: True => PASS
    Search("a..ly") | Expected: True | Actual: True => PASS
    Search("app.") | Expected: False | Actual: False => PASS
  階段 3：前綴成為完整單字
  AddWord: app, app
    Search("app") | Expected: True | Actual: True => PASS
    Search("ap.") | Expected: True | Actual: True => PASS
  階段 4：單字元、長度與失敗分支
  AddWord: a, at, code, coder
    Search(".") | Expected: True | Actual: True => PASS
    Search("..") | Expected: True | Actual: True => PASS
    Search("c.de") | Expected: True | Actual: True => PASS
    Search("c..er") | Expected: True | Actual: True => PASS
    Search("z.") | Expected: False | Actual: False => PASS
  小計：15/15 項驗證通過

總結：30/30 項驗證通過
Overall: PASS
```
