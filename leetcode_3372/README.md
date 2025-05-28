# LeetCode 3372 — 連接兩棵樹後最大化目標節點的數量 I

## 題目說明

有兩棵無向樹，分別包含 `n` 和 `m` 個節點，其節點標籤分別在區間 `[0, n - 1]` 和 `[0, m - 1]` 之內，且互不重複。

給定兩個長度分別為 `n - 1` 和 `m - 1` 的二維整數陣列 `edges1` 和 `edges2`，其中 `edges1[i] = [ai, bi]` 表示第一棵樹中的節點 `ai` 和 `bi` 之間有一條邊；`edges2[i] = [ui, vi]` 表示第二棵樹中的節點 `ui` 和 `vi` 之間有一條邊。你還會得到一個整數 `k`。

若從節點 `u` 到節點 `v` 的路徑邊數小於或等於 `k`，則稱節點 `u` 是節點 `v` 的**目標節點 (target)**。請注意，節點永遠是自己的目標節點。

請你回傳一個長度為 `n` 的整數陣列 `answer`，其中 `answer[i]` 表示如果你可以從第一棵樹中的某個節點與第二棵樹中的某個節點連接一條邊，則節點 `i` 最多可以擁有多少個目標節點 (來自兩棵樹的節點)，使得從這些目標節點到節點 `i` 的邊數不超過 `k`。

> 每個查詢是獨立的，也就是說你在計算某個 `i` 的結果時新增了一條邊，在計算下一個節點時，必須先移除這條新增的邊。

---

## 範例

```
Tree1(節點 0~2)：
  0
  |
  1
  |
  2

edges1 = [[0, 1], [1, 2]]

Tree2(節點 0~1)：
  0
  |
  1

edges2 = [[0, 1]]

k = 2
```

答案：

```
answer = [5, 5, 5]
```

---

## 程式架構與解法

本專案提供兩種解法，皆以 C# 撰寫，並於 `Program.cs` 中驗證。

### 解法 1：MaxTargetNodes

- 利用 DFS 計算每個節點在自身樹內距離 `k` 內可達的節點數。
- 再找出第二棵樹中距離 `k-1` 可達的最大節點數。
- 對於 Tree1 的每個節點，最大目標節點數 = 自己樹內可達 + 第二棵樹最大可達。
- 時間複雜度：O ((n+m) * n)，空間複雜度 O (n+m)
- 特色：
  - 只需對每棵樹各自做 DFS，效率高。
  - 適合 n, m 較大時使用。

### 解法 2：MaximumNodes2

- 先計算兩棵樹所有節點對之間的距離 (全節點對全節點距離)。
- 枚舉所有連接方式 (Tree1 的 u, Tree2 的 v)，窮舉所有可能連接點。
- 對於每個 Tree1 節點，計算最大目標節點數。
- 時間複雜度：O (n^2 * m^2)，空間複雜度 O (n^2 + m^2)
- 特色：
  - 實作直觀，容易理解。
  - 適合 n, m 較小時驗證正確性。

---

## 程式碼說明與邏輯

本專案核心程式碼位於 `Program.cs`，主要分為三個部分：

### 1. MaxTargetNodes (解法1)

- 利用 DFS 計算每個節點在自身樹內距離 k 內可達的節點數。
- 再找出第二棵樹中距離 k-1 可達的最大節點數。
- 對於 Tree1 的每個節點，最大目標節點數 = 自己樹內可達 + 第二棵樹最大可達。
- 適合 n, m 較大時使用，效能佳。

#### 主要邏輯（解法1）

- 先對兩棵樹分別計算每個節點在距離 k (或 k-1) 內可達的節點數（呼叫 Build 與 Dfs）。
- 對於每個 Tree1 節點，答案為自己樹內可達節點數加上 Tree2 最大可達節點數。

### 2. MaximumNodes2 (解法2)

- 先計算兩棵樹所有節點對之間的距離 (全節點對全節點距離)。
- 枚舉所有連接方式 (Tree1 的 u, Tree2 的 v)，窮舉所有可能連接點。
- 對於每個 Tree1 節點，計算最大目標節點數。
- 適合 n, m 較小時驗證正確性，直觀易懂。

#### 主要邏輯（解法2）

- 先對兩棵樹分別計算所有節點對距離（呼叫 AllPairDistancesDFS 與 DFS）。
- 枚舉所有連接點組合，計算每個 Tree1 節點的最大可達目標節點數。

### 3. Build、Dfs、BuildGraph、AllPairDistancesDFS

- `Build`：將邊資訊轉為鄰接串列，並對每個節點執行 DFS。
- `Dfs`：深度優先搜尋，計算距離 k 內可達節點數。
- `BuildGraph`：建立鄰接表。
- `AllPairDistancesDFS`：計算所有節點對距離。

---

## 主要程式片段說明

```csharp
// 解法1主體
public int[] MaxTargetNodes(int[][] edges1, int[][] edges2, int k)
{
    int n = edges1.Length + 1;
    int m = edges2.Length + 1;
    int[] count1 = Build(edges1, k);
    int[] count2 = Build(edges2, k - 1);
    int maxCount2 = 0;
    foreach (int c in count2) maxCount2 = Math.Max(maxCount2, c);
    int[] res = new int[n];
    for (int i = 0; i < n; i++) res[i] = count1[i] + maxCount2;
    return res;
}

// 單樹可達節點數
private int[] Build(int[][] edges, int k)
{
    int n = edges.Length + 1;
    List<List<int>> children = new List<List<int>>(n);
    for (int i = 0; i < n; i++) children.Add(new List<int>());
    foreach (var edge in edges) { children[edge[0]].Add(edge[1]); children[edge[1]].Add(edge[0]); }
    int[] res = new int[n];
    for (int i = 0; i < n; i++) res[i] = Dfs(i, -1, children, k);
    return res;
}

// DFS
private int Dfs(int node, int parent, List<List<int>> children, int k)
{
    if (k < 0) return 0;
    int count = 1;
    foreach (int child in children[node])
        if (child != parent) count += Dfs(child, node, children, k - 1);
    return count;
}
```

---

## 程式碼可讀性與效能分析

- 解法1（MaxTargetNodes）
  - 優點：
    - 結構簡潔，僅需對每棵樹各自做 DFS。
    - 效能佳，適合大資料量。
    - 主要瓶頸在於每個節點都需 DFS 一次，但樹結構下效率仍高。
  - 缺點：
    - 需理解「合併後最大可達」的推導。

- 解法2（MaximumNodes2）
  - 優點：
    - 實作直觀，窮舉所有連接方式，容易理解與驗證。
    - 適合小資料集對拍、驗證正確性。
  - 缺點：
    - 效能較差，n, m 稍大時會超時。
    - 記憶體消耗較高。

---

## 效能與可讀性比較

| 項目    | 解法 1 MaxTargetNodes | 解法 2 MaximumNodes2 |
| ----- | ------------------- | ------------------ |
| 時間複雜度 | O((n+m) * n)       | O(n^2 * m^2)      |
| 空間複雜度 | O(n+m)              | O(n^2 + m^2)       |
| 可讀性   | 較高 (簡潔)             | 直觀 (易理解)           |
| 適用場景  | n, m 較大             | n, m 較小            |
| 驗證正確性 | 適合主力解               | 適合對拍 / 驗證          |

- 解法 1 適合用於 LeetCode 正式提交，效能佳。
- 解法 2 適合用於小資料集驗證、理解題意。

---

## 如何執行

1. 使用 Visual Studio 或 `dotnet run` 執行專案。
2. `Main` 內建測試資料，會同時輸出兩種解法結果並驗證是否一致。

---

## 參考文件

- [LeetCode 題目連結 (英文)](https://leetcode.com/problems/maximize-the-number-of-target-nodes-after-connecting-trees-i/)
- [LeetCode 題目連結 (中文)](https://leetcode.cn/problems/maximize-the-number-of-target-nodes-after-connecting-trees-i/)

---

如需更多說明，請參考 `Hint.md` 內的詳細題解與範例。
