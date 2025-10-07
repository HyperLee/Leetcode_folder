# leetcode_1488 — Avoid Flood in The City

本專案收錄 LeetCode 題目 1488（避免洪水泛濫）的兩種 C# 解法範例與說明。程式檔位於 `leetcode_1488/Program.cs`。

## 快速入門

在 macOS 或其他支援 .NET 的環境中，執行下列指令來編譯與執行範例：

```bash
# 建置
dotnet build ./leetcode_1488/leetcode_1488.csproj

# 執行（Main 會示範兩個解法的輸出）
dotnet run --project ./leetcode_1488/leetcode_1488.csproj
```

## 題目摘要

給定陣列 `rains`：
- `rains[i] > 0` 表示第 `rains[i]` 號湖在第 i 天下雨，該湖變滿。
- `rains[i] == 0` 表示第 i 天無雨，可以選擇清空任一湖。

要求回傳陣列 `ans`（長度與 `rains` 相同），使得：
- 若 `rains[i] > 0`，則 `ans[i] == -1`。
- 若 `rains[i] == 0`，則 `ans[i]` 表示第 i 天你選擇清空哪個湖。

若無法避免洪水（同一個滿水湖再次下雨且你沒在之前的乾日抽乾它），回傳空陣列。

## 專案檔案

- `leetcode_1488/Program.cs`：包含兩個靜態方法 `AvoidFlood`（解法1）與 `AvoidFlood2`（解法2），以及 `Main` 範例呼叫。

## 解法總覽

此專案包含兩種常見且可通過 LeetCode 的解法：

- 解法1（有序集合，Greedy + OrderedSet）
- 解法2（優先佇列，Greedy + PriorityQueue）

兩者皆屬貪心策略，核心想法都是在乾日盡可能抽乾「最迫切需要被抽乾」的湖，以避免未來造成洪水；差別在於如何選擇與維護「迫切度」與可用的乾日。

---

## 解法1：有序集合（SortedSet）

概念要點

- 維護 `fullDay`（Dictionary）：記錄每個湖最後一次被淋滿的日子。
- 維護 `dryDay`（SortedSet）：保存所有可用的乾日（下標）。乾日以索引排序，便於搜尋「在某日之後的最早乾日」。

演算法

1. 正向遍歷 `rains`：
   - 若 `rains[i] == 0`：將 `i` 加入 `dryDay`，並把 `ans[i]` 預設為 1（任意安全值，之後可能被覆寫）。
   - 若 `rains[i] > 0`：
     - 若 `fullDay` 中已有該湖的上次填滿日 `j`，代表在 `j` 與 `i` 之間必須有一天把它抽乾。
     - 在 `dryDay` 中搜尋第一個大於 `j` 的乾日（最早可用且在 j 之後）。
       - 若找不到，則無解（回傳空陣列）。
       - 若找到，將該乾日的 `ans` 設成該湖編號，並從 `dryDay` 移除該乾日（已使用）。
     - 把當前下雨日 `i` 記錄到 `fullDay[lake] = i`。

時間/空間複雜度

- 時間複雜度：O(n log n)（主要來自 SortedSet 的搜尋/移除為對數時間）。
- 空間複雜度：O(n)。

優點

- 程式直觀，容易理解與維護。
- SortedSet 能直接找到「大於某個值的最小元素」，符合題意的要求。

缺點與限制

- SortedSet 的操作成本為 log n，對非常大的 n 可能較優先佇列略慢（但同級別）。
- 若語言平台沒有內建有序集合，需額外實作或找函式庫。

何時使用

- 需要直接按照索引大小搜尋第一個大於某值的乾日時最合適。

---

## 解法2：優先佇列（PriorityQueue）

概念要點

- 先用反向掃描預處理 `nextRains[i]`：對於每個下雨日 `i`，預計該湖下一次下雨的索引（若無則用 `length` 表示沒有下一次）。
- 正向遍歷時，維護 `fullLakes`（HashSet）表示目前哪些湖已滿；並用 `pq`（PriorityQueue）存放元素 `(lake, nextIndex)`，優先權為 `nextIndex`（下次下雨越早，優先級越高）。

演算法

1. 反向遍歷 `rains` 來填 `nextRains`：
   - 目的為知道每個下雨點之後的下一次同湖下雨位置（作為 deadline）。

2. 正向遍歷 `rains`：
   - 若 `rains[i] > 0`：
     - 若該湖已存在於 `fullLakes`，代表下雨時該湖已滿 -> 洪水 -> 無解。
     - 否則把該湖加入 `fullLakes`，並且若 `nextRains[i] < length`，將 `(lake, nextRains[i])` 推入 `pq`。
   - 若 `rains[i] == 0`（乾日）：
     - 若 `pq` 為空，任意抽乾湖（設定 `ans[i] = 1`）。
     - 否則從 `pq` 取出一個元素（nextIndex 最小），抽乾該湖（`ans[i] = lake`），並從 `fullLakes` 移除該湖。

時間/空間複雜度

- 時間複雜度：O(n log n)（優先佇列 enqueue/dequeue 為對數時間；另有一次反向 O(n) 掃描）。
- 空間複雜度：O(n)。

優點

- 思路清晰，透過預處理得到每個下雨日的 deadline（nextRains）後，優先佇列直接處理最緊迫的抽乾需求。
- 在 .NET 中內建 `PriorityQueue<TElement,TPriority>`，實作簡潔。

缺點與限制

- 需要額外的 `nextRains` 陣列與反向掃描步驟，實作略長。
- 若不熟悉 deadline/優先佇列的邏輯，閱讀成本較高。

何時使用

- 想以「deadline 最短優先」的觀念處理問題時，或希望在乾日使用一個優先級策略選擇要抽乾哪個湖。

---

## 兩解法比較（簡表）

| 比較面向 | 解法1（SortedSet） | 解法2（PriorityQueue） |
|---|---:|---:|
| 核心資料結構 | SortedSet（有序乾日索引） | PriorityQueue（(lake, nextIndex)） + nextRains 預處理 |
| 時間複雜度 | O(n log n) | O(n log n) |
| 空間複雜度 | O(n) | O(n) |
| 直觀性 | 高（直接找大於 j 的最早乾日） | 中（需理解 deadline 與反向預處理） |
| 平台支援 | 需要有序集合支援（C# 有 SortedSet） | .NET 內建 PriorityQueue，實作簡潔 |
| 運行效率 | 與 PQ 類似；實際差異取決於容器常數與實作 | 與 SortedSet 類似；在某些平台 PQ 常數可能更小 |

總結：兩者時間/空間等級相同。若偏好「直接在乾日尋找可用索引」，解法1 較直觀；若喜歡「先算 deadline 再以優先順序處理」，解法2 較具策略性。

---

## 測試與驗證

建議新增單元測試（xUnit）來驗證兩個方法在以下情況的行為：

- 範例：`[1,2,0,0,2,1]`（期望不會洪水）
- 無解範例：`[1,0,1]`（會發生洪水）
- 邊界：全為乾日、全為下雨、湖號非常大等。

可以在 `Main` 中或建立 xUnit 專案來執行大量隨機測資比對兩種方法的結果是否一致。

## 可改進處

- 若要更高效的常數時間或特定平台優化，可針對容器實作做微優化（例如自訂二分索引、平衡樹實作或更精簡的 PQ wrapper）。
- 可加入更完整的測試檔與 CI 任務（例如 GitHub Actions）來在每次提交時自動建置與執行測試（此專案目前以簡潔示範為主）。

---

如果你希望我直接為此專案新增 xUnit 測試、BenchmarkDotNet 基準或把 README 內容搬到 repo 根目錄，我可以幫你「建立」相關檔案並執行測試。歡迎告訴我下一步要做哪件事情。
