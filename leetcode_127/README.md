# LeetCode 127 — Word Ladder（單詞接龍）

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![LeetCode](https://img.shields.io/badge/LeetCode-127%20Hard-FFA116)](https://leetcode.com/problems/word-ladder/)

以 C# 與 .NET 10 實作三種廣度優先搜尋（Breadth-First Search, BFS），比較單向搜尋、集合式雙向搜尋，以及佇列式雙向搜尋如何找出最短單詞轉換序列。

## 快速導覽

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [三種解法比較](#三種解法比較)
- [解法一：HashSet 雙向 BFS](#解法一hashset-雙向-bfs)
- [解法二：Queue 單向 BFS](#解法二queue-單向-bfs)
- [解法三：Queue 雙向 BFS](#解法三queue-雙向-bfs)
- [建置與執行](#建置與執行)
- [驗收案例](#驗收案例)

## 題目說明

給定起始單詞 `beginWord`、目標單詞 `endWord` 與字典 `wordList`，一條有效的轉換序列必須符合：

1. 每一對相鄰單詞恰好只有一個字母不同。
2. 除了 `beginWord` 不一定要在字典中，其餘轉換後的單詞都必須出現在 `wordList`。
3. 序列最後一個單詞必須是 `endWord`。

回傳最短轉換序列包含的單詞數量；如果不存在有效序列則回傳 `0`。

例如：

```text
hit -> hot -> dot -> dog -> cog
```

這條序列包含 5 個單詞，因此答案是 `5`。

題目原文：[LeetCode 127. Word Ladder](https://leetcode.com/problems/word-ladder/description/)

## 限制條件

- `1 <= beginWord.length <= 10`
- `endWord.length == beginWord.length`
- `1 <= wordList.length <= 5000`
- `wordList[i].length == beginWord.length`
- `beginWord`、`endWord` 與 `wordList[i]` 只包含小寫英文字母。
- `beginWord != endWord`
- `wordList` 中的單詞互不重複。

程式中的三個公開解法都假設輸入符合上述限制，不另外處理 `null`、不同字長或重複字典項目。

## 解題概念與出發點

可以把每個單詞視為圖上的節點；如果兩個單詞只差一個字母，就在兩個節點之間連一條邊。每次轉換的成本相同，因此問題等價於「在無權圖中尋找從 `beginWord` 到 `endWord` 的最短路徑」。

BFS 會按距離由近到遠展開節點，所以第一次抵達目標時，走過的層數就是最短距離。本專案不預先建立所有邊，而是對目前單詞的每個位置嘗試 `'a'` 到 `'z'`，即時產生可能的相鄰單詞，再使用 `HashSet` 判斷它是否存在於字典。

若 `endWord` 不在 `wordList` 中，依題意不可能形成有效序列，三種解法都會立即回傳 `0`。

## 三種解法比較

令：

- `N` 為 `wordList` 的單詞數。
- `L` 為每個單詞的長度。
- `A` 為字母表大小，本題固定為 `26`。

| 解法 | 搜尋方向 | 主要資料結構 | 特點 |
| --- | --- | --- | --- |
| `LadderLength` | 雙向 | `HashSet` 前沿 | 程式精簡，固定展開節點較少的一端 |
| `LadderLength2` | 單向 | `Queue`、`Dictionary` | 最接近標準 BFS，層級變化容易理解 |
| `LadderLength3` | 雙向 | 兩組 `Queue`、`Dictionary` | 明確保留兩端距離，完整按層展開 |

每個被拜訪的單詞最多嘗試 `A × L` 個替換。若把建立候選字串與字串雜湊視為單次操作，常見簡化時間複雜度寫作 `O(N × A × L)`；在 C# 中建立與雜湊長度為 `L` 的字串需要 `O(L)`，納入後較嚴格的上界是 `O(N × A × L²)`。三種方法的最壞情況相同，但雙向 BFS 通常只需探索較小的實際搜尋空間。

佇列、前沿與拜訪紀錄最多保存 `O(N)` 個單詞參考；若連同所代表的字元內容一起計算，空間可寫成 `O(N × L)`。

## 解法一：HashSet 雙向 BFS

### 設計

`LadderLength` 使用 `beginSet` 與 `endSet` 表示兩端目前的 BFS 前沿，並用 `wordSet` 保存尚可拜訪的字典單詞。

每一輪都選擇較小的前沿進行展開。對前沿中的每個單詞逐一替換字母：

1. 新單詞若存在於另一端前沿，代表兩個搜尋方向相遇，立即回傳目前序列長度加一。
2. 新單詞若仍在 `wordSet`，便移除它並加入下一層；移除動作同時達成去重，避免重複搜尋。
3. 完成一層後，以 `nextLevel` 取代目前前沿，並增加序列長度。

BFS 前沿只包含相同距離的節點，因此兩端首次相遇時得到的就是最短序列。

### 範例演示

以 `hit` 到 `cog` 為例：

| 序列長度 | 起點端前沿 | 終點端前沿 | 動作 |
| ---: | --- | --- | --- |
| 1 | `{hit}` | `{cog}` | 展開 `hit`，找到 `hot` |
| 2 | `{hot}` | `{cog}` | 展開 `hot`，找到 `dot`、`lot` |
| 3 | `{dot, lot}` | `{cog}` | 起點端較大，交換兩端後展開 `cog` |
| 4 | `{dog, log}` | `{dot, lot}` | `dog` 可變成另一端的 `dot` |
| 5 | 相遇 | 相遇 | 回傳 `5` |

這種寫法用集合直接表達「一整層前沿」，也能自然地交換搜尋方向。

## 解法二：Queue 單向 BFS

### 設計

`LadderLength2` 從 `beginWord` 開始執行標準 BFS：

1. `Queue<string>` 保證單詞按照距離由近到遠被取出。
2. `Dictionary<string, int>` 記錄每個單詞第一次被找到時的層級。
3. 合法且尚未拜訪的候選單詞以 `level + 1` 入列。
4. 第一次從佇列取出 `endWord` 時，其層級就是最短序列長度。

相較於雙向版本，這個方法可能展開更多節點，但資料流最直接，適合作為理解 BFS 的基準解法。

### 範例演示

| BFS 層級 | 本層單詞 | 新找到的單詞 |
| ---: | --- | --- |
| 1 | `hit` | `hot` |
| 2 | `hot` | `dot`、`lot` |
| 3 | `dot`、`lot` | `dog`、`log` |
| 4 | `dog`、`log` | `cog` |
| 5 | `cog` | 首次取出目標，回傳 `5` |

即使存在多條路徑，例如經過 `dot` 或 `lot`，FIFO 順序仍保證先處理較短的路徑。

## 解法三：Queue 雙向 BFS

### 設計

`LadderLength3` 為起點端與終點端各建立一組 `Queue` 和 `Dictionary`：

- Queue 保存下一個要完整展開的 BFS 層。
- Dictionary 保存該方向首次抵達每個單詞時的層級。
- 每輪選擇佇列較小的一端，降低實際產生的候選數。

`ExpandQueue` 先固定本層節點數，再完整處理該層。若候選 `newWord` 已存在於另一端的拜訪紀錄，完整序列長度為：

```text
目前單詞在本端的層級 + newWord 在另一端的層級
```

相遇點並未在兩個加數中重複，因此不需要再減 `1`。方法會掃完目前層並保留最短相遇值，避免因同層處理順序而先回傳較長連線。

### 範例演示

| 展開方向 | 本層與層級 | 產生或相遇 |
| --- | --- | --- |
| 起點端 | `hit (1)` | `hot (2)` |
| 起點端 | `hot (2)` | `dot (3)`、`lot (3)` |
| 終點端 | `cog (1)` | `dog (2)`、`log (2)` |
| 起點端 | `dot (3)`、`lot (3)` | `dot` 產生另一端已拜訪的 `dog (2)` |

兩端距離相加得到 `3 + 2 = 5`。完整按層展開是維持最短路徑保證的關鍵。

## 專案結構

```text
leetcode_127/
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_127.sln
└── leetcode_127/
    ├── leetcode_127.csproj
    └── Program.cs
```

主要專案以 .NET 10 為目標框架。目前沒有正式的自動化測試專案；驗收使用專案建置與 `Main` 中可重複執行的 console harness。

## 建置與執行

從本目錄執行：

```bash
dotnet restore leetcode_127/leetcode_127.csproj
dotnet build leetcode_127/leetcode_127.csproj --no-restore --nologo
dotnet run --project leetcode_127/leetcode_127.csproj --no-build
```

若要檢查本次檔案的空白與換行問題：

```bash
git diff --check
git diff --check -- leetcode_127
```

## 驗收案例

每組案例都會分別執行三種解法。每次呼叫會取得獨立的字典副本，避免任何解法的內部操作影響後續結果。

| 案例 | 重點 | 預期 |
| ---: | --- | ---: |
| 1 | 官方可達範例 | 5 |
| 2 | `endWord` 不在字典 | 0 |
| 3 | 只需一次字母變換 | 2 |
| 4 | `endWord` 存在但圖不連通 | 0 |
| 5 | 長度為 1 的邊界輸入 | 2 |

實際執行輸出：

```text
案例 1：官方可達範例
輸入：beginWord = "hit", endWord = "cog", wordList = ["hot", "dot", "dog", "lot", "log", "cog"]
  LadderLength | Expected: 5 | Actual: 5 | PASS
  LadderLength2 | Expected: 5 | Actual: 5 | PASS
  LadderLength3 | Expected: 5 | Actual: 5 | PASS

案例 2：終點不在字典
輸入：beginWord = "hit", endWord = "cog", wordList = ["hot", "dot", "dog", "lot", "log"]
  LadderLength | Expected: 0 | Actual: 0 | PASS
  LadderLength2 | Expected: 0 | Actual: 0 | PASS
  LadderLength3 | Expected: 0 | Actual: 0 | PASS

案例 3：一次字母變換
輸入：beginWord = "log", endWord = "dog", wordList = ["hot", "dot", "dog", "lot", "log"]
  LadderLength | Expected: 2 | Actual: 2 | PASS
  LadderLength2 | Expected: 2 | Actual: 2 | PASS
  LadderLength3 | Expected: 2 | Actual: 2 | PASS

案例 4：終點存在但不連通
輸入：beginWord = "hit", endWord = "cog", wordList = ["hot", "dot", "tod", "cog"]
  LadderLength | Expected: 0 | Actual: 0 | PASS
  LadderLength2 | Expected: 0 | Actual: 0 | PASS
  LadderLength3 | Expected: 0 | Actual: 0 | PASS

案例 5：單字母邊界
輸入：beginWord = "a", endWord = "c", wordList = ["b", "c"]
  LadderLength | Expected: 2 | Actual: 2 | PASS
  LadderLength2 | Expected: 2 | Actual: 2 | PASS
  LadderLength3 | Expected: 2 | Actual: 2 | PASS

總結：15/15 項檢查通過
```
