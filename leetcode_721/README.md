# LeetCode 721 — Accounts Merge（帳戶合併）

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![Language](https://img.shields.io/badge/C%23-console-239120)](https://learn.microsoft.com/dotnet/csharp/)
[![LeetCode](https://img.shields.io/badge/LeetCode-721-FFA116)](https://leetcode.com/problems/accounts-merge/)

這是一個可直接執行的 .NET 10 Console 教學專案，使用「深度優先搜尋」與「帳戶索引並查集」兩種方式解決帳戶合併問題。程式入口會執行五組固定案例，對兩種解法進行共 10 項結果與輸入保持驗證。

## 快速連結

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：DFS 尋找連通分量](#解法一dfs-尋找連通分量)
- [解法二：帳戶索引 Union-Find](#解法二帳戶索引-union-find)
- [兩種解法比較](#兩種解法比較)
- [可執行驗證案例](#可執行驗證案例)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)

## 題目說明

每筆帳戶資料的第一個字串是姓名，其後是該帳戶擁有的 email：

```text
["John", "johnsmith@mail.com", "john_newyork@mail.com"]
```

若兩筆帳戶至少共享一個相同 email，就能確定它們屬於同一人。這項關係具有傳遞性：如果帳戶 A 與 B 共享 email，B 又與 C 共享另一個 email，則 A、B、C 都必須合併。

合併後的每筆結果需要：

1. 將姓名放在第一欄。
2. 收集同一人的所有不重複 email。
3. 將 email 依字典序排列。

不同人可能擁有相同姓名，因此不能只用姓名判斷是否需要合併。題目允許最外層結果採任意順序。

題目連結：

- [LeetCode 721. Accounts Merge](https://leetcode.com/problems/accounts-merge/)
- [力扣 721. 账户合并](https://leetcode.cn/problems/accounts-merge/)

### 官方範例

輸入：

```text
[
  ["John", "johnsmith@mail.com", "john_newyork@mail.com"],
  ["John", "johnsmith@mail.com", "john00@mail.com"],
  ["Mary", "mary@mail.com"],
  ["John", "johnnybravo@mail.com"]
]
```

其中前兩筆 John 帳戶共享 `johnsmith@mail.com`，所以它們屬於同一個連通群組。Mary 帳戶與使用 `johnnybravo@mail.com` 的另一筆 John 帳戶都沒有和其他帳戶共享 email。

其中一種合法輸出為：

```text
[
  ["John", "john00@mail.com", "john_newyork@mail.com", "johnsmith@mail.com"],
  ["Mary", "mary@mail.com"],
  ["John", "johnnybravo@mail.com"]
]
```

## 限制條件

- `1 <= accounts.Count <= 1000`
- `2 <= accounts[i].Count <= 10`
- `1 <= accounts[i][j].Length <= 30`
- `accounts[i][0]` 僅由英文字母組成。
- `accounts[i][j]`（`j > 0`）是有效的 email。
- 同一人的所有帳戶都使用相同姓名。
- 不同人可以使用相同姓名。

本專案的方法依題目契約接收非空且格式有效的資料，不另外處理 `null`、空帳戶或無 email 的非題目輸入。

## 解題概念與出發點

這題的核心不是字串整理，而是找出「哪些帳戶屬於同一個連通群組」。

可以把每筆帳戶視為一個節點：

- 兩個帳戶共享 email：兩個節點互相連通。
- 多個節點透過直接或間接共享 email 連在一起：它們是同一個連通分量。
- 每個連通分量：對應一筆合併後帳戶。

本專案用兩種方式實現相同的連通關係：

| 解法 | 連通關係的表示方式 | 找群組的方式 |
| --- | --- | --- |
| `AccountsMerge` | `email -> 帳戶索引列表` 形成隱含圖 | DFS 走訪每個連通分量 |
| `AccountsMerge2` | `parent` 與 `rank` 陣列形成集合森林 | 共享 email 時執行 Union |

為了精確描述複雜度，以下使用：

- `A`：帳戶數量。
- `E`：所有帳戶中的 email 出現總數，重複 email 會重複計算。
- `U`：不重複 email 數量。
- `Uᵢ`：第 `i` 個合併群組中的不重複 email 數量。

不論採用哪種分組方式，最後都必須排序各群組 email。總排序成本是：

```text
Σ O(Uᵢ log Uᵢ)
```

最差情況是所有 email 都在同一群組，成本為 `O(U log U)`。

## 解法一：DFS 尋找連通分量

### 設計說明

`AccountsMerge` 先建立：

```text
emailToIndexes[email] = 所有包含此 email 的帳戶索引
```

例如：

```text
alex-b@mail.com -> [0, 2]
alex-c@mail.com -> [1, 2]
```

這份反向映射相當於圖的鄰接資訊。從帳戶 0 看到 `alex-b@mail.com` 時，可以前往帳戶 2；帳戶 2 又能透過 `alex-c@mail.com` 前往帳戶 1。

DFS 使用兩種狀態：

- `visited[accountIndex]`：避免同一帳戶被不同起點重複建立結果。
- `componentEmails`：收集目前連通分量內的唯一 email，也避免同一 email 的鄰接帳戶被重複展開。

每找到一個尚未拜訪的帳戶，就啟動一次 DFS：

1. 標記目前帳戶已拜訪。
2. 逐一處理姓名之後的 email。
3. 新 email 加入 `componentEmails`。
4. 從反向映射取得所有共享該 email 的帳戶。
5. 對尚未拜訪的帳戶繼續 DFS。
6. 完成整個連通分量後，以 Ordinal 規則排序 email，再把姓名插入最前方。

### 為什麼不會重複展開

帳戶可能形成環：

```text
帳戶 0 --email A-- 帳戶 1
帳戶 1 --email B-- 帳戶 2
帳戶 2 --email C-- 帳戶 0
```

`visited` 阻止 DFS 再次進入已處理帳戶；`componentEmails.Add(email)` 回傳 `false` 時直接跳過，讓每個唯一 email 的鄰接清單只在該連通分量展開一次。

### 傳遞合併演示

輸入：

```text
帳戶 0 = ["Alex", "alex-a@mail.com", "alex-b@mail.com"]
帳戶 1 = ["Alex", "alex-c@mail.com", "alex-d@mail.com"]
帳戶 2 = ["Alex", "alex-b@mail.com", "alex-c@mail.com"]
```

反向映射：

```text
alex-a@mail.com -> [0]
alex-b@mail.com -> [0, 2]
alex-c@mail.com -> [1, 2]
alex-d@mail.com -> [1]
```

走訪流程：

| 步驟 | 目前帳戶 | 新增 email | 找到的相鄰帳戶 |
| --- | ---: | --- | --- |
| 1 | `0` | `alex-a@mail.com` | `0` |
| 2 | `0` | `alex-b@mail.com` | `0, 2`，前往帳戶 `2` |
| 3 | `2` | `alex-c@mail.com` | `1, 2`，前往帳戶 `1` |
| 4 | `1` | `alex-d@mail.com` | `1` |

雖然帳戶 0 與帳戶 1 沒有直接共享 email，但 DFS 經過帳戶 2 找到完整連通分量。排序後得到：

```text
["Alex", "alex-a@mail.com", "alex-b@mail.com", "alex-c@mail.com", "alex-d@mail.com"]
```

### 正確性重點

若兩筆帳戶共享 email，反向映射會讓 DFS 能在兩筆帳戶間移動；若多筆帳戶以共享 email 鏈接，DFS 會沿鏈走訪全部節點。因此同一人的所有帳戶都會被收進同一連通分量。反之，沒有任何共享 email 路徑的帳戶不可能被 DFS 到達，所以不會被錯誤合併。

### 複雜度

- 建立反向映射：`O(E)`。
- DFS 走訪帳戶、email 與反向映射：`O(A + E)`。
- 排序：`O(Σ Uᵢ log Uᵢ)`，最差 `O(U log U)`。
- 總時間：`O(A + E + U log U)`。
- 額外空間：`O(A + E + U)`，包含反向映射、拜訪陣列、email 集合與最差 `O(A)` 的遞迴堆疊。

## 解法二：帳戶索引 Union-Find

### 設計說明

`AccountsMerge2` 不建立完整鄰接清單，而是讓每個帳戶索引先各自成為一個集合：

```text
parent[i] = i
rank[i] = 0
```

掃描每個 email 時，使用：

```text
emailOwner[email] = 第一次出現此 email 的帳戶索引
```

- 第一次看到 email：記錄目前帳戶索引。
- 再次看到相同 email：目前帳戶和第一次登記的帳戶屬於同一人，呼叫 `Union` 合併集合。

所有合併完成後，再走訪每個唯一 email：

1. 由 `emailOwner` 取得一個擁有者帳戶。
2. 用 `Find` 找出該帳戶的最終根節點。
3. 把 email 加到 `rootToEmails[root]`。
4. 排序每個根節點的 email。
5. 使用根節點帳戶的姓名建立結果。

題目保證同一人的帳戶姓名相同，因此集合內選擇哪一個帳戶作為根節點，都能取得正確姓名。

### Path Compression

`Find` 在找到根節點後，會讓查詢路徑上的節點直接指向根：

```text
原本：3 -> 2 -> 0
壓縮：3 ------> 0
      2 ------> 0
```

後續查詢相同集合時，不必再次沿完整鏈向上走。

### Union by Rank

`Union` 先找出兩個集合的根，再比較 `rank`：

- rank 較小的根接到 rank 較大的根。
- rank 相同時選擇一個根作為新根，並將其 rank 加一。
- 已在同一集合時不做任何事。

這能避免集合樹退化成很長的鏈。配合 path compression，單次操作的均攤成本接近常數。

### 傳遞合併演示

使用同一組 Alex 資料：

```text
帳戶 0 = ["Alex", "alex-a@mail.com", "alex-b@mail.com"]
帳戶 1 = ["Alex", "alex-c@mail.com", "alex-d@mail.com"]
帳戶 2 = ["Alex", "alex-b@mail.com", "alex-c@mail.com"]
```

初始狀態：

```text
parent = [0, 1, 2]
rank   = [0, 0, 0]
```

掃描流程：

| email | 目前帳戶 | 先前擁有者 | 動作 |
| --- | ---: | ---: | --- |
| `alex-a@mail.com` | `0` | 無 | 記錄擁有者 `0` |
| `alex-b@mail.com` | `0` | 無 | 記錄擁有者 `0` |
| `alex-c@mail.com` | `1` | 無 | 記錄擁有者 `1` |
| `alex-d@mail.com` | `1` | 無 | 記錄擁有者 `1` |
| `alex-b@mail.com` | `2` | `0` | `Union(2, 0)` |
| `alex-c@mail.com` | `2` | `1` | `Union(2, 1)` |

第一次合併讓帳戶 2 與 0 同組；第二次合併再把帳戶 1 接入同一集合。最後三個帳戶的 `Find` 都得到相同根節點，因此四個唯一 email 會被放入同一結果。

### 正確性重點

每次發現共享 email 時，Union-Find 都會合併兩個帳戶集合，所以直接相連的帳戶必定有相同根節點。集合合併具有傳遞性，因此經過多個共享 email 間接連通的帳戶也會得到相同根節點。不同根節點間不存在共享 email 路徑，所以分組時不會混入其他人的 email。

### 複雜度

- 掃描並合併：`O(E α(A))` 均攤時間。
- 依根節點彙整唯一 email：`O(U α(A))`。
- 排序：`O(Σ Uᵢ log Uᵢ)`，最差 `O(U log U)`。
- 總時間：`O((E + U) α(A) + U log U)`。
- 額外空間：`O(A + U)`，包含 `parent`、`rank`、email 擁有者與分組映射。

其中 `α` 是反阿克曼函數，在實際輸入規模下可視為極小常數。

## 兩種解法比較

| 比較項目 | DFS | Union-Find |
| --- | --- | --- |
| 公開方法 | `AccountsMerge` | `AccountsMerge2` |
| 核心資料結構 | Dictionary、HashSet、遞迴堆疊 | Dictionary、parent、rank |
| 建模方式 | 帳戶圖的連通分量 | 動態合併帳戶集合 |
| 傳遞關係 | DFS 沿共享 email 繼續走訪 | Union 的集合合併自然傳遞 |
| 主要優點 | 圖論流程直觀，容易追蹤群組 | 操作均攤接近常數，適合動態合併 |
| 注意事項 | 深層連通鏈會使用遞迴堆疊 | 需要正確實作 Find、路徑壓縮與 rank |
| 是否修改輸入 | 否 | 否 |

兩種解法都使用 `StringComparer.Ordinal` 排序 email，避免結果受到作業系統文化設定影響。

## 可執行驗證案例

`Main` 會執行下列五組合法資料：

| 案例 | 驗證重點 | 預期群組數 |
| --- | --- | ---: |
| 官方合併案例 | 共享 email 合併，其他帳戶保持獨立 | 3 |
| 傳遞合併 | A 與 C 透過 B 間接相連 | 1 |
| 同名但不相連 | 不可只靠姓名合併 | 2 |
| 多個獨立元件 | 同一次輸入含兩組合併及一組獨立帳戶 | 3 |
| 單一帳戶與字典序排序 | 最小帳戶數與未排序 email | 1 |

每組案例分別以深層副本呼叫兩種解法。單項驗證只有在以下條件都成立時才算通過：

1. 合併結果等於預期。
2. 每組 email 順序正確。
3. 原始帳戶列與欄位順序完全未被修改。

由於題目允許最外層帳戶採任意順序，比對器只排序外層帳戶列；列內欄位不會重新排序，以免掩蓋解法的 email 排序錯誤。

## 建置與執行

需求：

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

從此 `leetcode_721` repository 根目錄執行：

```bash
dotnet restore leetcode_721/leetcode_721.csproj
dotnet build leetcode_721/leetcode_721.csproj --nologo --no-restore
dotnet run --project leetcode_721/leetcode_721.csproj --no-build
```

目前沒有獨立自動化測試專案；`dotnet build` 加上程式入口可重複執行的固定案例，就是本專案的驗收方式。

## 實際執行結果

以下內容來自：

```bash
dotnet run --project leetcode_721/leetcode_721.csproj --no-build
```

```text
案例 1：官方合併案例
輸入：[[John,johnsmith@mail.com,john_newyork@mail.com], [John,johnsmith@mail.com,john00@mail.com], [Mary,mary@mail.com], [John,johnnybravo@mail.com]]
預期：[[John,john00@mail.com,john_newyork@mail.com,johnsmith@mail.com], [Mary,mary@mail.com], [John,johnnybravo@mail.com]]
解法一（DFS）：
  實際：[[John,john00@mail.com,john_newyork@mail.com,johnsmith@mail.com], [Mary,mary@mail.com], [John,johnnybravo@mail.com]]
  輸入未修改：True
  驗證：PASS
解法二（Union-Find）：
  實際：[[John,john00@mail.com,john_newyork@mail.com,johnsmith@mail.com], [Mary,mary@mail.com], [John,johnnybravo@mail.com]]
  輸入未修改：True
  驗證：PASS

案例 2：傳遞合併
輸入：[[Alex,alex-a@mail.com,alex-b@mail.com], [Alex,alex-c@mail.com,alex-d@mail.com], [Alex,alex-b@mail.com,alex-c@mail.com]]
預期：[[Alex,alex-a@mail.com,alex-b@mail.com,alex-c@mail.com,alex-d@mail.com]]
解法一（DFS）：
  實際：[[Alex,alex-a@mail.com,alex-b@mail.com,alex-c@mail.com,alex-d@mail.com]]
  輸入未修改：True
  驗證：PASS
解法二（Union-Find）：
  實際：[[Alex,alex-a@mail.com,alex-b@mail.com,alex-c@mail.com,alex-d@mail.com]]
  輸入未修改：True
  驗證：PASS

案例 3：同名但不相連
輸入：[[Lee,lee-one@mail.com], [Lee,lee-two@mail.com]]
預期：[[Lee,lee-one@mail.com], [Lee,lee-two@mail.com]]
解法一（DFS）：
  實際：[[Lee,lee-one@mail.com], [Lee,lee-two@mail.com]]
  輸入未修改：True
  驗證：PASS
解法二（Union-Find）：
  實際：[[Lee,lee-one@mail.com], [Lee,lee-two@mail.com]]
  輸入未修改：True
  驗證：PASS

案例 4：多個獨立元件
輸入：[[Bob,bob-b@mail.com,bob-a@mail.com], [Carol,carol@mail.com], [Dana,dana-c@mail.com,dana-b@mail.com], [Bob,bob-c@mail.com,bob-b@mail.com], [Dana,dana-a@mail.com,dana-b@mail.com]]
預期：[[Bob,bob-a@mail.com,bob-b@mail.com,bob-c@mail.com], [Carol,carol@mail.com], [Dana,dana-a@mail.com,dana-b@mail.com,dana-c@mail.com]]
解法一（DFS）：
  實際：[[Bob,bob-a@mail.com,bob-b@mail.com,bob-c@mail.com], [Carol,carol@mail.com], [Dana,dana-a@mail.com,dana-b@mail.com,dana-c@mail.com]]
  輸入未修改：True
  驗證：PASS
解法二（Union-Find）：
  實際：[[Bob,bob-a@mail.com,bob-b@mail.com,bob-c@mail.com], [Carol,carol@mail.com], [Dana,dana-a@mail.com,dana-b@mail.com,dana-c@mail.com]]
  輸入未修改：True
  驗證：PASS

案例 5：單一帳戶與字典序排序
輸入：[[Eve,zeta@mail.com,alpha@mail.com,middle@mail.com]]
預期：[[Eve,alpha@mail.com,middle@mail.com,zeta@mail.com]]
解法一（DFS）：
  實際：[[Eve,alpha@mail.com,middle@mail.com,zeta@mail.com]]
  輸入未修改：True
  驗證：PASS
解法二（Union-Find）：
  實際：[[Eve,alpha@mail.com,middle@mail.com,zeta@mail.com]]
  輸入未修改：True
  驗證：PASS

總結：10/10 項驗證通過
```

## 專案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_721.sln
└── leetcode_721/
    ├── leetcode_721.csproj
    └── Program.cs
```

主要程式、固定案例與兩種解法都位於 `leetcode_721/Program.cs`，專案目標框架為 `net10.0`。
