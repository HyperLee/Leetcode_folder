# LeetCode 1334 — 閾值距離內鄰居最少的城市

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![C%23](https://img.shields.io/badge/C%23-console-239120)

這個專案使用 C# 與 .NET 10 實作 [LeetCode 1334：Find the City With the Smallest Number of Neighbors at a Threshold Distance](https://leetcode.com/problems/find-the-city-with-the-smallest-number-of-neighbors-at-a-threshold-distance/)，並以可直接執行的案例比較 Floyd-Warshall、鄰接矩陣 Dijkstra，以及鄰接表搭配優先佇列的 Dijkstra。

## 目錄

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：Floyd-Warshall](#解法一floyd-warshall)
- [解法二：鄰接矩陣 Dijkstra](#解法二鄰接矩陣-dijkstra)
- [解法三：鄰接表與優先佇列 Dijkstra](#解法三鄰接表與優先佇列-dijkstra)
- [解法比較](#解法比較)
- [測試案例](#測試案例)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)

## 題目說明

有 `n` 座城市，編號為 `0` 到 `n - 1`。`edges[i] = [from, to, weight]` 表示城市 `from` 與城市 `to` 之間有一條權重為 `weight` 的雙向道路。

給定距離閾值 `distanceThreshold`，需要找出透過某條路徑可達、且最短路徑距離不超過閾值的其他城市數量。回傳鄰居數量最少的城市；如果有多座城市同票，回傳編號最大者。

路徑距離是沿途所有邊權重的總和。演算法不能只檢查直接相連的道路，因為經過其他城市的間接路徑可能更短。

例如：

```text
n = 4
edges = [[0,1,3], [1,2,1], [1,3,4], [2,3,1]]
distanceThreshold = 4
```

各城市在閾值內可達的其他城市為：

```text
城市 0 -> [1, 2]
城市 1 -> [0, 2, 3]
城市 2 -> [0, 1, 3]
城市 3 -> [1, 2]
```

城市 0 與城市 3 都只有兩個可達鄰居；依同票規則選擇編號較大的城市 3。

## 限制條件

- `2 <= n <= 100`
- `1 <= edges.length <= n * (n - 1) / 2`
- `edges[i].length == 3`
- `0 <= from_i < to_i < n`
- `1 <= weight_i <= 10^4`
- `1 <= distanceThreshold <= 10^4`
- 所有 `(from_i, to_i)` 城市配對皆不重複。
- 所有道路權重皆為正數，因此可安全使用 Dijkstra。
- 三種公開解法都只讀取 `edges`，不會修改輸入陣列。

## 解題概念與出發點

這題表面上是在計數，核心其實是「求每一座城市到其他所有城市的最短距離」。取得最短距離後，對每個來源城市執行以下步驟：

1. 排除來源城市本身。
2. 計算最短距離小於等於 `distanceThreshold` 的城市數量。
3. 與目前最少數量比較。
4. 若數量更少就更新答案；若數量相同也更新答案。

程式依城市編號由小到大走訪，並在 `reachableCount <= minimumReachableCount` 時更新。這個 `<=` 很重要：同票時，後出現的城市編號較大，最後留下的自然就是題目要求的最大編號。

三種解法的差異在於如何取得最短距離：

- Floyd-Warshall 一次計算所有城市對。
- 矩陣 Dijkstra 從每個來源城市各跑一次，並線性尋找下一個最近城市。
- 優先佇列 Dijkstra 同樣逐一處理來源城市，但用最小堆快速取得下一個最近城市。

## 解法一：Floyd-Warshall

`FindTheCity` 使用二維距離矩陣 `distances[from][to]`，一次求出任意兩座城市之間的最短距離。

### 初始化

- 城市到自己的距離設為 0。
- 有直接道路的兩個方向都填入道路權重。
- 沒有直接道路的位置填入 `int.MaxValue / 2`，代表目前無法到達。

使用 `int.MaxValue / 2` 而不是 `int.MaxValue`，可避免兩段不可達距離相加時發生整數溢位。

### 狀態轉移

依序允許每座城市 `k` 成為中繼點。對任意 `from` 與 `to`，比較原本路徑和經過 `k` 的路徑：

```text
distances[from][to] = min(
    distances[from][to],
    distances[from][k] + distances[k][to]
)
```

處理完中繼點 `k` 後，矩陣已包含「只允許城市 0 到 k 作為中繼點」時的最短距離。當所有中繼點都處理完，矩陣就是完整的所有點對最短距離。

### 範例演示

考慮案例：

```text
edges = [[0,1,10], [0,2,1], [1,2,1], [1,3,1]]
distanceThreshold = 2
```

城市 0 到城市 1 原本的直接距離是 10。允許城市 2 作為中繼點後：

```text
0 -> 2 -> 1 = 1 + 1 = 2
min(10, 2) = 2
```

因此城市 0 可在閾值 2 內到達城市 1。這也說明為什麼只檢查直接道路會得到錯誤答案。

### 複雜度

- 時間複雜度：O(n³)，三層迴圈枚舉中繼點、起點與終點。
- 額外空間複雜度：O(n²)，儲存所有城市對的距離。

## 解法二：鄰接矩陣 Dijkstra

`FindTheCity2` 先建立鄰接矩陣，再把每座城市依序當作來源，執行一次 Dijkstra。

### 設計流程

1. 建立 `distances`，將來源距離設為 0，其餘設為無限大。
2. 使用 `visited` 記錄已確定最短距離的城市。
3. 線性掃描所有尚未確定的城市，選出目前距離最小的 `current`。
4. 若找不到可達城市便提早結束；否則將 `current` 標記為已確定。
5. 透過鄰接矩陣枚舉所有可能鄰居並執行鬆弛。
6. 完成單一來源後，統計閾值內的其他城市，再處理下一個來源。

鬆弛公式為：

```text
candidateDistance = distances[current] + graph[current][neighbor]
distances[neighbor] = min(distances[neighbor], candidateDistance)
```

因為所有權重皆為正數，一旦某座城市成為尚未確定城市中的最小距離者，它的最短距離就不會再被之後的路徑改善。

### 範例演示

沿用城市 0 作為來源：

```text
初始距離：[0, 10, 1, ∞]
```

1. 先確定城市 0，得到城市 1 距離 10、城市 2 距離 1。
2. 尚未確定城市中，城市 2 的距離 1 最小，因此下一個處理城市 2。
3. 經過城市 2 到城市 1 的候選距離為 `1 + 1 = 2`，把城市 1 從 10 更新為 2。
4. 接著確定城市 1，經由權重 1 的道路把城市 3 更新為距離 3。

閾值為 2 時，城市 0 可達城市 1 與城市 2，但城市 3 的距離 3 超過閾值。

### 複雜度

- 時間複雜度：每個來源需要 O(n²)，全部來源共 O(n³)。
- 額外空間複雜度：O(n²)，主要來自鄰接矩陣。

## 解法三：鄰接表與優先佇列 Dijkstra

`FindTheCity3` 使用鄰接表保存實際存在的道路，並用 .NET `PriorityQueue` 依目前距離由小到大取出候選城市。

### 設計流程

1. 每條雙向道路分別加入兩個端點的鄰接串列。
2. 將來源城市以距離 0 放入優先佇列。
3. 每次取出佇列中距離最短的城市。
4. 同一城市可能因距離改善而多次入列；如果取出的距離已不等於陣列中的最佳距離，代表它是過期項目，直接略過。
5. 枚舉實際鄰居；找到更短距離時更新陣列並重新入列。
6. 完成單一來源後統計閾值內城市，再處理下一個來源。

### 範例演示

仍以城市 0 為來源：

```text
佇列初始內容：(城市 0, 距離 0)
```

1. 取出城市 0，將 `(城市 1, 10)` 與 `(城市 2, 1)` 入列。
2. 優先取出城市 2，透過 `2 -> 1` 把城市 1 改善為距離 2，並加入 `(城市 1, 2)`。
3. 取出距離 2 的城市 1，更新城市 3 為距離 3。
4. 之後取出的 `(城市 1, 10)` 已不是城市 1 的最佳距離，因此略過，不重複處理鄰居。

相較於矩陣版本，這個方法只枚舉圖中實際存在的邊，通常更適合道路較少的稀疏圖。

### 複雜度

令 `E` 為道路數量：

- 時間複雜度：每個來源約 O((E+n) log n)，全部來源為 O(n(E+n) log n)。
- 額外空間複雜度：O(n+E)，包含鄰接表、距離陣列與優先佇列。

## 解法比較

| 解法 | 最短路徑範圍 | 圖結構 | 時間複雜度 | 額外空間 | 特點 |
| --- | --- | --- | --- | --- | --- |
| `FindTheCity` | 一次求所有點對 | 距離矩陣 | O(n³) | O(n²) | 程式結構直接，適合 `n <= 100` |
| `FindTheCity2` | 每個來源各跑一次 | 鄰接矩陣 | O(n³) | O(n²) | 清楚展示基本 Dijkstra 的選點與鬆弛 |
| `FindTheCity3` | 每個來源各跑一次 | 鄰接表與最小堆 | O(n(E+n) log n) | O(n+E) | 只走實際道路，稀疏圖通常較有效率 |

三種方法共用相同的計數與同票規則，因此差異集中在最短距離的取得方式。題目的 `n` 上限只有 100，Floyd-Warshall 已足以通過；另外兩種實作則提供資料結構和圖密度對演算法選擇的教學比較。

## 測試案例

`Main` 內建 8 組固定案例，每組分別檢查三種解法，共 24 項驗證：

| 案例 | 重點 | 預期城市 |
| --- | --- | ---: |
| 1 | 官方範例一、同票取最大編號 | 3 |
| 2 | 官方範例二 | 0 |
| 3 | `n = 2`、權重剛好等於閾值 | 1 |
| 4 | 所有城市皆無閾值內鄰居 | 3 |
| 5 | 間接路徑短於直接道路 | 3 |
| 6 | 不連通圖與孤立城市 | 4 |
| 7 | 多條道路具有相同權重 | 4 |
| 8 | `n = 100` 的城市數上界 | 99 |

任何解法得到錯誤結果時，該列會顯示 `FAIL`，程式也會以非零結束碼結束，方便在終端機或自動化環境中辨識失敗。

## 建置與執行

從此 repository 根目錄執行：

```bash
dotnet restore leetcode_1334/leetcode_1334.csproj
dotnet build leetcode_1334/leetcode_1334.csproj --nologo
dotnet run --project leetcode_1334/leetcode_1334.csproj --no-build
```

專案沒有獨立的自動化測試專案；console harness 的 24 項 Expected/Actual 比對就是目前的行為驗收入口。

## 實際執行結果

以下內容來自上述 `dotnet run` 命令的實際輸出：

```text
Case 1: n = 4, edges = [[0, 1, 3], [1, 2, 1], [1, 3, 4], [2, 3, 1]], distanceThreshold = 4
Expected: 3
FindTheCity Actual: 3 => PASS
FindTheCity2 Actual: 3 => PASS
FindTheCity3 Actual: 3 => PASS

Case 2: n = 5, edges = [[0, 1, 2], [0, 4, 8], [1, 2, 3], [1, 4, 2], [2, 3, 1], [3, 4, 1]], distanceThreshold = 2
Expected: 0
FindTheCity Actual: 0 => PASS
FindTheCity2 Actual: 0 => PASS
FindTheCity3 Actual: 0 => PASS

Case 3: n = 2, edges = [[0, 1, 10000]], distanceThreshold = 10000
Expected: 1
FindTheCity Actual: 1 => PASS
FindTheCity2 Actual: 1 => PASS
FindTheCity3 Actual: 1 => PASS

Case 4: n = 4, edges = [[0, 1, 5]], distanceThreshold = 4
Expected: 3
FindTheCity Actual: 3 => PASS
FindTheCity2 Actual: 3 => PASS
FindTheCity3 Actual: 3 => PASS

Case 5: n = 4, edges = [[0, 1, 10], [0, 2, 1], [1, 2, 1], [1, 3, 1]], distanceThreshold = 2
Expected: 3
FindTheCity Actual: 3 => PASS
FindTheCity2 Actual: 3 => PASS
FindTheCity3 Actual: 3 => PASS

Case 6: n = 5, edges = [[0, 1, 1], [1, 2, 1]], distanceThreshold = 1
Expected: 4
FindTheCity Actual: 4 => PASS
FindTheCity2 Actual: 4 => PASS
FindTheCity3 Actual: 4 => PASS

Case 7: n = 5, edges = [[0, 1, 2], [1, 2, 2], [2, 3, 2], [3, 4, 2]], distanceThreshold = 2
Expected: 4
FindTheCity Actual: 4 => PASS
FindTheCity2 Actual: 4 => PASS
FindTheCity3 Actual: 4 => PASS

Case 8: n = 100, edges = [[0, 1, 10000]], distanceThreshold = 10000
Expected: 99
FindTheCity Actual: 99 => PASS
FindTheCity2 Actual: 99 => PASS
FindTheCity3 Actual: 99 => PASS

Summary: 24/24 checks passed.
```