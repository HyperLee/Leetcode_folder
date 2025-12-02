# 高階概念（總覽）

先把點按 y（高度）分組。對於某個高度 y，有 p 個點時，該高度上能形成的水平邊數為：

$$\text{edges} = C(p,2) = \dfrac{p(p-1)}{2}$$

任選兩個不同高度各選一條水平邊，可構成一個「水平梯形」。因此總數為所有高度對 (i, j) 的邊數乘積總和：

$$\sum_{i<j} \text{edges}_i \cdot \text{edges}_j$$

為了把 O(k^2) 轉成 O(k)，我們維護一個累加和 sum（表示已處理的 edges 之和）。當遇到當前高度的 edges 為 e 時，新增梯形數為 e * sum，然後把 sum 累加 e。

 
## 程式流程（對應程式碼）

1. 使用 `Dictionary<int,int> pointCount` 計算每個 y 的點數 p_y（迴圈遍歷所有點）。
2. 對每個 p_y 計算 `edge = C(p_y, 2)`。
3. 維護兩個 `long` 變數：`sum`（已處理邊之和）與 `res`（結果）。
   每次執行 `res = (res + edge * sum) % MOD`，然後 `sum = (sum + edge) % MOD`。
4. 最後回傳 `(int)res`。

 
## 為何這樣能正確計算？

- 累加法等價於計算所有 unordered pair `edges_i * edges_j`（i < j）。用 `sum` 只需要一趟掃描就能統計所有之前高度與當前高度的配對數。
- 使用 `long` 做中間計算，並在每步做 `% MOD`，可避免溢位並保留正確模值。

## 數學等價性

$$\sum_{i<j} e_i e_j = \frac{1}{2}\left(\left(\sum_i e_i\right)^2 - \sum_i e_i^2\right)$$

程式方法實際上是以線性方式收集前綴總和 `sum` 來實現上述求和。

 
## 逐步範例演示

### 範例 1（程式內 Test 1）

輸入：兩行，每行 3 個點：

- `y = 0`: 3 個點 → `edges = C(3,2) = 3`
- `y = 1`: 3 個點 → `edges = 3`

執行流程：

- 先計算 `pointCount` → `{0: 3, 1: 3}`
- edges 序列為 `[3, 3]`
  - 初始 `sum = 0`, `res = 0`
  - 讀取 `edge = 3`：`res += 3 * 0 = 0` → `res = 0`; `sum = 3`
  - 讀取 `edge = 3`：`res += 3 * 3 = 9` → `res = 9`; `sum = 6`
- 回傳 `9`（符合預期）

### 範例 2（Test 2）

輸入：只有一行 `3` 個點：

- `p_y = 3` → `edges = 3`

- 只有一個高度，無法與其他不同高度配對，`res` 最終為 `0`。

### 範例 3（Test 3）

輸入：`y = 0` 有 `2` 點，`y = 1` 有 `3` 點

- edges：`y=0` → `1`, `y=1` → `3`

- 遍歷 `[1, 3]`：`res = 0` -> `sum = 1` -> `res += 3 * 1 = 3` -> `sum = 4` -> 回傳 `3`

再舉一個三行例子：

- `p = [4, 3, 2]` → `edges = [6, 3, 1]`

- 應得總數：`6*3 + 6*1 + 3*1 = 18 + 6 + 3 = 27`

- 累加流程：`sum = 0` -> `6` => `sum = 6` -> `3` => `res += 3*6 = 18`, `sum = 9` -> `1` => `res += 1*9 = 9` -> `res = 27`

 
## 注意事項 / Gotchas

- 列舉順序不影響結果：只要每個高度的 `edge` 與之前高度的 `sum` 相乘，即等價於統計所有 `i < j` 的配對。
- 當 `p_y < 2` 時，`C(p_y, 2) = 0`（無邊數）。
- 使用 `long` 做中間計算，並在每步做 `% MOD`，可避免溢位。
- `Dictionary` 可接受負值或任意整數的 y。
- 若希望遍歷高度時有確定順序，可先對 `pointCount.Keys` 做排序，但此題不需要排序。
- 若輸入點數非常多，記憶體消耗取決於不同 y 值數量 `k`（一般 `k <= n`）。

 
## 小優化／可讀性建議（非必要，但建議）

- 計數 y 的寫法可改成更簡潔的 `TryGetValue`：

```csharp
// filepath: /Users/qiuzili/Leetcode/Leetcode_folder/leetcode_3623/leetcode_3623/Program.cs
// ...existing code...
if (!pointCount.TryGetValue(y, out int c))
{
    pointCount[y] = 1;
}
else
{
    pointCount[y] = c + 1;
}
// ...existing code...
```

- 或者使用 LINQ：

```csharp
// filepath: /Users/qiuzili/Leetcode/Leetcode_folder/leetcode_3623/leetcode_3623/Program.cs
// ...existing code...
var edges = points.GroupBy(p => p[1])
                  .Select(g => (long)g.Count() * (g.Count() - 1) / 2)
                  .Where(e => e > 0);
// ...existing code...
```

## 時間與空間複雜度

- 時間：`O(n)`（先遍歷點得 `p_y`，再遍歷不同的 y 值 `k`，總共 `O(n + k)` 約等於 `O(n)`）
- 空間：`O(k)`，`k` 為不同的高度數量

