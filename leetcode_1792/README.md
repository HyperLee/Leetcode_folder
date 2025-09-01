# LeetCode 1792 — Maximum Average Pass Ratio

此專案包含 C#（.NET）針對 LeetCode 題目 1792「Maximum Average Pass Ratio」的解法與說明。

## 題目簡述

給定 `classes`（二維整數陣列），其中 `classes[i] = [pass_i, total_i]`。每個班級原本有 `pass_i` 個會通過、`total_i` 個總人數。
另外有 `extraStudents` 名額外優秀學生，他們被分配到某班級後保證通過。分配完後，求所有班級通過率的最大平均值（允許 1e-5 誤差）。

## 解題要點（核心思想）

- 目標是最大化所有班級通過率之和（最後再除以班級數）。

- 當把一名學生加入班級 `(pass, total)` 時，該班通過率的增量為：

  delta(pass, total) = (pass+1)/(total+1) - pass/total。

- 因為我們每多分配一名學生，會立即影響總通過率和，因此每一步都應選擇能帶來最大「邊際增益（delta）」的班級。這是一個能被貪婪（greedy）策略解決的問題。

## 數學推導（為何選 delta 最大）

- 我們要最大化 S = sum_i pass_i/total_i（最終會除以 n，但除以常數不影響 argmax）。

- 每次把一名學生分配到班 j 對 S 的貢獻增加量正是 delta(pass_j, total_j)。

- 因此每一步選擇使 S 增加最大的班級等同於選擇 delta 最大的班級。

注意：直接選擇「當前通過率最低的班級」未必等價，因為 delta 同時被 pass 與 total 共同影響。

## 演算法與實作細節

1. 使用最大堆（priority queue）追蹤每個班級的當前 delta。

1. 初始時把所有班級放入堆中，key 為 delta。

1. 重複 `extraStudents` 次：從堆取出 delta 最大的班級，將該班的 pass 與 total 各加 1，重新計算其 delta，並放回堆中。

1. 分配完成後，堆中各班的 pass/total 相加並除以班級數，得到答案。

### 為何用 PriorityQueue 而不是每次 linear scan？

- 若每次都 O(n) 找最大 delta，總複雜度為 O(n * k)，k = extraStudents。使用堆能將每次選擇與更新降為 O(log n)，總時間 O((n + k) log n)。

## 程式實作細節（C#）

- 使用 .NET `PriorityQueue<TElement, TPriority>`，但它預設為 min-heap（依 priority 小者先出）。

- 有兩種常見做法：

  1. 把 priority 設為 `-delta`，以達到 max-heap 行為。

  2. 傳入自訂 `Comparer<double>`（`Comparer<double>.Create((a,b)=>b.CompareTo(a))`）以反轉排序，直接 enqueue 正的 delta（本專案採用此做法以提高可讀性）。

關鍵函數片段（節錄自 `Program.cs`）:

```csharp
var descComparer = Comparer<double>.Create((a, b) => b.CompareTo(a));
var pq = new PriorityQueue<(int pass, int total), double>(descComparer);

foreach (var c in classes)
{
    int p = c[0];
    int t = c[1];
    double delta = Delta(p, t);
    pq.Enqueue((p, t), delta);
}

for (int i = 0; i < extraStudents; i++)
{
    var top = pq.Dequeue();
    int p = top.pass + 1;
    int t = top.total + 1;
    double delta = Delta(p, t);
    pq.Enqueue((p, t), delta);
}

// 計算平均
double sum = 0.0;
while (pq.Count > 0)
{
    var item = pq.Dequeue();
    sum += (double)item.pass / item.total;
}

return sum / classes.Length;
```

`Delta` 函數：

```csharp
static double Delta(int pass, int total)
{
    return (double)(pass + 1) / (total + 1) - (double)pass / total;
}
```

## 時間與空間複雜度

- 時間：O((n + k) log n)，其中 n = classes.Length，k = extraStudents。初始化堆需 O(n)，每次分配是 O(log n)。

- 空間：O(n)，堆中儲存 n 個班級狀態。

## 邊界情況與注意事項

- 題目保證 `total >= pass >= 0`。若 `classes` 為空，回傳 0。若 `extraStudents` 為 0，回傳初始平均。

- 使用 double 計算足以滿足 1e-5 的精度要求，但應避免傳入 NaN/Infinity 作為 priority。

## 如何執行

1. 使用 Visual Studio 或 `dotnet build` 建置專案。範例：

```bash
dotnet build ./leetcode_1792/leetcode_1792.csproj
```

1. 可在 `Program.cs` 中補入測試呼叫 `MaxAverageRatio` 或使用單元測試框架建立測試。

## 參考

題目連結: [https://leetcode.com/problems/maximum-average-pass-ratio](https://leetcode.com/problems/maximum-average-pass-ratio)

---

README 已建立，若要我把 README 翻成繁體中文版或加入範例測資與單元測試，我可以接著處理。
