# LeetCode 3310：移除可疑的方法

這是一個以 .NET 10 撰寫的 C# 主控台專案，實作 LeetCode 3310「Remove Methods From Project」。專案保留兩種圖論解法：

- `RemainingMethods`：DFS 找出可疑方法，再掃描是否存在外部呼叫。
- `RemainingMethods2`：BFS 找出可疑方法，同時利用入度判斷是否存在外部呼叫。

`Main` 內含 5 組固定案例，會自動執行兩種解法並輸出 `PASS/FAIL`，不需要輸入資料或按鍵。

題目連結：

- [3310. Remove Methods From Project](https://leetcode.com/problems/remove-methods-from-project/description/)
- [3310. 移除可疑的方法](https://leetcode.cn/problems/remove-methods-from-project/description/)

## 先用白話理解題意

這題最容易卡住的地方，是「什麼情況下可以移除可疑方法」。可以先把專案想成一張有向圖：`invocations[i] = [a, b]` 就是一條 `a -> b` 的邊，表示方法 `a` 會呼叫方法 `b`。

### Step 1：先找出所有可疑方法

方法 `k` 已知有 bug。只要是下列方法，都會被視為可疑：

- `k` 本身
- `k` 直接呼叫的方法
- 以上方法再直接或間接呼叫的方法

例如：

```text
0 -> 1 -> 2 -> 5
      |
      v
      3
4
```

如果 `k = 1`，從 `1` 沿著呼叫方向可以走到 `2`、`3`、`5`，所以：

```text
Suspicious = {1, 2, 3, 5}
```

這一步就是從 `k` 開始執行 DFS 或 BFS，找出所有可達節點。圖中即使有環，只要記錄已經拜訪過的節點，就不會重複探索或無限循環。

### Step 2：確認可疑集合能不能整批移除

找到可疑集合後，我們希望把它們全部刪除。但刪除前必須確認：

> 外面的正常方法不能呼叫裡面的可疑方法。

原因很直觀。如果仍然存在：

```csharp
method0()
{
    method1(); // method1 將被移除
}
```

刪除 `method1` 後，還活著的 `method0` 就會呼叫不存在的方法，專案可能無法編譯。因此，只要有一條 `非可疑方法 -> 可疑方法` 的邊，就不能只刪除可疑集合，而要回傳全部方法。

### 呼叫方向判斷

判斷重點是「誰呼叫誰」，不是只看可疑方法是否呼叫了正常方法：

| 呼叫方向 | 是否阻止移除 | 原因 |
| --- | --- | --- |
| 可疑 → 可疑 | 否 | 兩個方法會一起被移除。 |
| 可疑 → 非可疑 | 否 | 呼叫者本身會被移除，留下的方法不會依賴它。 |
| 非可疑 → 非可疑 | 否 | 與可疑集合的移除無關。 |
| 非可疑 → 可疑 | 是 | 存活的方法會呼叫已被刪除的方法。 |

### 三個案例的直覺推導

#### Example 1：外部呼叫阻止移除

```text
1 -> 2
0 -> 1
3 -> 2

k = 1
```

從 `1` 出發得到 `Suspicious = {1, 2}`。但 `0 -> 1` 與 `3 -> 2` 都是從外部指向可疑集合的邊，因此不能刪除任何方法，答案是 `[0, 1, 2, 3]`。

#### Example 2：可疑集合與外部隔離

```text
0 -> 1
0 -> 2
1 -> 2

3 -> 4

k = 0
```

可疑集合為 `{0, 1, 2}`。外部方法只有 `3`、`4`，而且沒有任何外部方法呼叫 `0`、`1` 或 `2`，所以可以整批移除，留下 `[3, 4]`。

#### Example 3：整個專案都是可疑方法

```text
2 -> 0
0 -> 1
1 -> 2

k = 2
```

這是一個環，從 `2` 可以走到 `0`、`1`，最後又回到 `2`。因此所有方法都是可疑方法，移除後沒有剩餘方法，答案是 `[]`。

### 把整題縮成兩個判斷

1. 從 `k` 執行 DFS 或 BFS，標記所有可疑方法。
2. 掃描每條邊 `(u, v)`：如果 `u` 非可疑但 `v` 可疑，代表存在外部呼叫，直接回傳 `[0, 1, ..., n - 1]`；如果完全沒有這種邊，就回傳所有非可疑方法。

以上是題目的白話直覺；下面再用正式定義、限制條件與兩種圖論實作，說明如何完成這個判斷。

### 參考資料

- [LeetCode 3310 解說頁](https://leetcode.com/problems/remove-methods-from-project/solutions/)
- [AlgoMonster：3310. Remove Methods From Project - In-Depth Explanation](https://algo.monster/liteproblems/3310)

## 題目說明

專案共有 `n` 個方法，編號為 `0` 到 `n - 1`。`invocations[i] = [a, b]` 表示方法 `a` 會呼叫方法 `b`，因此每筆資料可以視為一條有向邊 `a -> b`。

方法 `k` 已知有 bug。`k` 本身，以及從 `k` 沿著呼叫關係可以直接或間接到達的所有方法，都稱為「可疑方法」，目標是移除它們。

但是，只有在群組外沒有方法呼叫群組內方法時，才可以安全移除這個群組。因此需要判斷：

1. 從 `k` 出發可以到達哪些方法，形成可疑集合？
2. 是否存在一條從非可疑方法指向可疑方法的呼叫邊？

若不存在外部呼叫，就回傳所有非可疑方法；若存在外部呼叫，則不能只移除可疑方法，必須回傳全部方法。

題目允許答案以任意順序回傳。本專案為了讓主控台輸出與 README 容易閱讀，會以升冪順序列出方法編號。

## 限制條件

依照官方題目限制：

- `1 <= n <= 10^5`
- `0 <= k <= n - 1`
- `0 <= invocations.length <= 2 * 10^5`
- `invocations[i] == [a_i, b_i]`
- `0 <= a_i, b_i <= n - 1`
- `a_i != b_i`
- `invocations[i] != invocations[j]`

令 `m = invocations.length`。兩種解法都以 `O(n + m)` 時間處理圖；第一種解法額外使用 DFS 的遞迴呼叫堆疊，第二種解法額外使用 BFS queue、入度陣列與可疑標記陣列。

## 解題概念與出發點

把方法和 invocation 視為有向圖後，題目的核心可以簡化成「找出從 `k` 出發的可達區域，然後檢查這個區域的外部邊界」。

```text
非可疑方法 ──> 可疑方法   代表外部仍依賴可疑方法，不能移除
可疑方法   ──> 可疑方法   兩者一起移除，不會形成外部依賴
非可疑方法 ──> 非可疑方法 不影響可疑群組是否能移除
```

因此，判斷條件不是「可疑方法有沒有呼叫別人」，而是「有沒有非可疑方法呼叫可疑方法」。這個方向判斷是兩種解法都必須維持的核心不變量。

## 兩種解法比較

| 解法 | 找可疑方法 | 判斷外部呼叫 | 時間複雜度 | 額外空間 | 特點 |
| --- | --- | --- | --- | --- | --- |
| `RemainingMethods` | 從 `k` 執行 DFS | 再掃描每條 invocation | `O(n + m)` | `O(n + m)`，另有 `O(n)` 遞迴深度 | 邏輯直接，先建立集合再檢查邊界 |
| `RemainingMethods2` | 從 `k` 執行 BFS | 以入度扣除可疑區域內的邊 | `O(n + m)` | `O(n + m)`，另有 `O(n)` queue/陣列 | 不需要第二次用集合判斷每條邊，將外部呼叫保留在入度中 |

兩種方法的公開介面相同：

```csharp
IList<int> RemainingMethods(int n, int k, int[][] invocations)
IList<int> RemainingMethods2(int n, int k, int[][] invocations)
```

## 解法一：DFS 加上外部邊掃描

### 設計流程

`RemainingMethods` 分成三個階段：

1. **建立鄰接表**：對每一條 `[a, b]`，把 `b` 放入 `graph[a]`，表示從 `a` 可以走到 `b`。
2. **標記可疑集合**：從 `k` 呼叫 `DFS`。每次進入節點就把 `isSuspicious[node]` 設為 `true`，再探索它的鄰居。
3. **檢查外部呼叫並建立答案**：掃描所有 invocation。如果發現 `source` 非可疑、`target` 可疑，代表外部方法仍依賴待刪除方法，直接回傳 `[0, 1, ..., n - 1]`。若沒有這種邊，就收集所有非可疑方法。

`isSuspicious` 同時扮演「可疑集合」與「visited」標記。即使圖中有環，已經標記過的節點也不會再次遞迴。

### 範例演示：官方 Example 2

輸入：

```text
n = 5
k = 0
invocations = [[1,2], [0,2], [0,1], [3,4]]
```

建立圖後可表示為：

```text
0 ──> 1 ──> 2
└────> 2
3 ──> 4
```

從 `k = 0` 執行 DFS：

1. 標記 `0`。
2. 沿著 `0 -> 2` 標記 `2`。
3. 沿著 `0 -> 1` 標記 `1`；`1 -> 2` 已經到過，不再重複探索。
4. 最終可疑集合為 `{0, 1, 2}`。

接著掃描每條邊：

- `1 -> 2`：可疑到可疑，可以一起移除。
- `0 -> 2`：可疑到可疑，可以一起移除。
- `0 -> 1`：可疑到可疑，可以一起移除。
- `3 -> 4`：非可疑到非可疑，不會阻止移除。

沒有出現「非可疑 -> 可疑」的邊，所以可以移除 `{0, 1, 2}`，結果為 `[3, 4]`。

## 解法二：BFS 加上入度判斷

### 設計流程

`RemainingMethods2` 仍然先建立有向圖，但額外統計每個節點的入度：

1. `edges[a]` 儲存從 `a` 出發的所有目標節點。
2. 每遇到一條 `a -> b`，就讓 `inDegree[b]++`。
3. 從 `k` 開始 BFS，標記所有可疑節點。
4. 處理可疑節點 `u` 的每條 outgoing edge `u -> v` 時，將 `inDegree[v]--`。這代表刪除可疑來源 `u` 後，這條可疑區域內的呼叫邊也不再是外部依賴。
5. BFS 結束後，如果某個可疑節點的 `inDegree` 仍大於 0，剩下的入度必定來自非可疑方法，表示無法安全移除全部可疑方法。

每個節點只會在第一次被發現時入隊，因此有向環不會造成無限 BFS。

### 範例演示：入度如何留下外部呼叫

仍使用官方 Example 2：

```text
n = 5
k = 0
invocations = [[1,2], [0,2], [0,1], [3,4]]
```

所有邊加入後，初始入度依節點編號 `0` 到 `4` 為：

```text
inDegree = [0, 1, 2, 0, 1]
```

BFS 的變化如下：

| 處理節點 | 扣除的邊 | 入度變化 | 新發現節點 |
| --- | --- | --- | --- |
| `0` | `0 -> 2`、`0 -> 1` | `2: 2 -> 1`、`1: 1 -> 0` | `1`、`2` |
| `1` | `1 -> 2` | `2: 1 -> 0` | 無，`2` 已標記 |
| `2` | 無 | 無 | 無 |

可疑集合為 `{0, 1, 2}`，其中每個可疑節點的剩餘入度都是 `0`，表示沒有非可疑方法呼叫它們。最後收集非可疑節點，得到 `[3, 4]`。

相反地，在 `0 -> 2` 且 `0` 非可疑、`2` 可疑時，`0` 不會被 BFS 處理，因此這條邊不會被扣除；`inDegree[2]` 會保留大於 `0`，程式便能判斷整個可疑群組不能單獨移除。

## Main 的固定測試案例

runner 會使用以下 5 組資料，並讓每組資料分別通過 `RemainingMethods` 與 `RemainingMethods2`：

| 案例 | `n` | `k` | `invocations` | 預期結果 |
| --- | ---: | ---: | --- | --- |
| Example 1 - external callers block removal | 4 | 1 | `[[1,2], [0,1], [3,2]]` | `[0,1,2,3]` |
| Example 2 - suspicious group can be removed | 5 | 0 | `[[1,2], [0,2], [0,1], [3,4]]` | `[3,4]` |
| Example 3 - every method is suspicious | 3 | 2 | `[[1,2], [0,1], [2,0]]` | `[]` |
| Boundary - no invocations | 5 | 2 | `[]` | `[0,1,3,4]` |
| Boundary - suspicious cycle has an external caller | 4 | 2 | `[[2,3], [3,2], [0,2]]` | `[0,1,2,3]` |

由於題目允許任意順序，runner 會先排序實際結果和預期結果再比較；主控台則固定以升冪輸出。

## 建置與執行

請在目前目錄 `C:\GitHubFolder\Leetcode_folder\leetcode_3310` 執行：

```powershell
dotnet build .\leetcode_3310\leetcode_3310.csproj
dotnet run --project .\leetcode_3310\leetcode_3310.csproj
```

本專案沒有獨立測試專案；`Main` 的固定案例是可直接重複執行的 smoke test。

若要從 Git parent 目錄檢查差異中的空白問題，請在 `C:\GitHubFolder\Leetcode_folder` 執行：

```powershell
git -c safe.directory=C:/GitHubFolder/Leetcode_folder diff --check -- leetcode_3310
```

由於新建立的 `README.md` 尚未被 Git 追蹤，必要時可再從本目錄執行尾端空白掃描：

```powershell
rg -n "[ \t]+$" .\README.md .\leetcode_3310\Program.cs
```

## 實際執行輸出

以下內容由 `dotnet run --project .\leetcode_3310\leetcode_3310.csproj` 實際產生：

```text
Example 1 - external callers block removal: Expected = [0, 1, 2, 3]
  RemainingMethods:  Actual = [0, 1, 2, 3] (PASS)
  RemainingMethods2: Actual = [0, 1, 2, 3] (PASS)

Example 2 - suspicious group can be removed: Expected = [3, 4]
  RemainingMethods:  Actual = [3, 4] (PASS)
  RemainingMethods2: Actual = [3, 4] (PASS)

Example 3 - every method is suspicious: Expected = []
  RemainingMethods:  Actual = [] (PASS)
  RemainingMethods2: Actual = [] (PASS)

Boundary - no invocations: Expected = [0, 1, 3, 4]
  RemainingMethods:  Actual = [0, 1, 3, 4] (PASS)
  RemainingMethods2: Actual = [0, 1, 3, 4] (PASS)

Boundary - suspicious cycle has an external caller: Expected = [0, 1, 2, 3]
  RemainingMethods:  Actual = [0, 1, 2, 3] (PASS)
  RemainingMethods2: Actual = [0, 1, 2, 3] (PASS)

Summary: 10/10 passed.
```

## 專案結構

```text
leetcode_3310/
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── docs/
│   └── readme-template.md
├── leetcode_3310/
│   ├── Program.cs
│   └── leetcode_3310.csproj
├── AGENTS.md
└── README.md
```

- `leetcode_3310/Program.cs`：原始雙語題目 XML、兩種解法、XML 文件、關鍵演算法註解與固定 runner。
- `leetcode_3310/leetcode_3310.csproj`：目標框架為 `net10.0` 的主控台專案設定。
- `docs/readme-template.md`：README 初始建立時使用的結構與驗證指引。
- `.vscode/`：從 VS Code 建置與執行巢狀主控台專案的設定。
