# LeetCode 2322: 從樹中刪除邊的最小分數

## 問題描述

給定一棵有 n 個節點的無向連通樹，節點標號為 0 到 n-1，並有 n-1 條邊。你有一個長度為 n 的整數陣列 nums，其中 nums[i] 表示第 i 個節點的值。還有一個長度為 n-1 的二維整數陣列 edges，其中 edges[i] = [ai, bi] 表示存在一條連接節點 ai 和 bi 的邊。

請移除樹上的兩條不同的邊，使樹分成三個連通元件。對於每一對被移除的邊，定義如下步驟：

1. 分別計算三個元件中所有節點值的異或和（XOR）
2. 取這三個異或值中的最大值與最小值之差，作為該對邊的分數

返回所有可能移除邊對的最小分數。

## 演算法解法概述

本解法使用 **DFS 序（DFS Order）** 和 **子樹異或和** 的概念來有效解決問題。核心思想是：

1. 透過 DFS 遍歷建立時間戳，用於快速判斷節點間的祖先關係
2. 計算每個子樹的異或和
3. 枚舉所有可能的節點對，根據祖先關係計算分割結果
4. 找出所有分割方案中的最小分數

### 時間複雜度：O(n²)
### 空間複雜度：O(n)

## 核心函式詳解

### 1. MinimumScore 函式 - 主要演算法邏輯

```csharp
/// <summary>
/// 計算從樹中刪除兩條邊後的最小分數
/// </summary>
/// <param name="nums">每個節點的值陣列</param>
/// <param name="edges">樹的邊集合</param>
/// <returns>所有可能刪除邊對的最小分數</returns>
public int MinimumScore(int[] nums, int[][] edges)
```

**功能：** 計算從樹中刪除兩條邊後的最小分數

**詳細步驟：**

#### 步驟 1：建立鄰接表
```csharp
// 建立鄰接表來表示樹的結構
List<List<int>> adj = new List<List<int>>();
for (int i = 0; i < n; i++)
{
    adj.Add(new List<int>());
}

// 將邊加入鄰接表，因為是無向樹，所以雙向建立連接
foreach (var e in edges)
{
    adj[e[0]].Add(e[1]);
    adj[e[1]].Add(e[0]);
}
```
- 將輸入的邊轉換為鄰接表表示法
- 由於是無向樹，每條邊都需要雙向建立連接

#### 步驟 2：初始化資料結構
```csharp
int[] sum = new int[n];    // 每個子樹的異或和
int[] in_ = new int[n];    // DFS序中每個節點開始遍歷的時間戳
int[] out_ = new int[n];   // DFS序中每個節點結束遍歷的時間戳
int cnt = 0;               // DFS遍歷的計數器
```

#### 步驟 3：DFS 遍歷計算基礎資訊
```csharp
Dfs(0, -1, nums, adj, sum, in_, out_, ref cnt);
```
- 從根節點 0 開始進行深度優先搜尋
- 同時計算子樹異或和和 DFS 序

#### 步驟 4：枚舉所有節點對並計算分數
```csharp
for (int u = 1; u < n; u++)
{
    for (int v = u + 1; v < n; v++)
    {
        // 根據祖先關係判斷分割方式
        if (in_[v] > in_[u] && in_[v] < out_[u]) 
        {
            // u 是 v 的祖先
            res = Math.Min(res, Calc(sum[0] ^ sum[u], sum[u] ^ sum[v], sum[v]));
        } 
        else if (in_[u] > in_[v] && in_[u] < out_[v]) 
        {
            // v 是 u 的祖先
            res = Math.Min(res, Calc(sum[0] ^ sum[v], sum[v] ^ sum[u], sum[u]));
        } 
        else 
        {
            // u 和 v 不互為祖先
            res = Math.Min(res, Calc(sum[0] ^ sum[u] ^ sum[v], sum[u], sum[v]));
        }
    }
}
```

**三種分割情況：**

1. **u 是 v 的祖先：** 刪除邊將樹分成三部分
   - 第一部分：整個樹除去 u 的子樹 → `sum[0] ^ sum[u]`
   - 第二部分：u 的子樹除去 v 的子樹 → `sum[u] ^ sum[v]`
   - 第三部分：v 的子樹 → `sum[v]`

2. **v 是 u 的祖先：** 對稱情況
   - 第一部分：整個樹除去 v 的子樹 → `sum[0] ^ sum[v]`
   - 第二部分：v 的子樹除去 u 的子樹 → `sum[v] ^ sum[u]`
   - 第三部分：u 的子樹 → `sum[u]`

3. **u 和 v 無祖先關係：** 兩個獨立的子樹
   - 第一部分：整個樹除去兩個子樹 → `sum[0] ^ sum[u] ^ sum[v]`
   - 第二部分：u 的子樹 → `sum[u]`
   - 第三部分：v 的子樹 → `sum[v]`

### 2. Dfs 函式 - 深度優先搜尋

```csharp
/// <summary>
/// 深度優先搜尋，計算每個子樹的異或和以及DFS序
/// </summary>
/// <param name="x">當前遍歷的節點</param>
/// <param name="fa">當前節點的父節點</param>
/// <param name="nums">節點值陣列</param>
/// <param name="adj">樹的鄰接表表示</param>
/// <param name="sum">儲存每個子樹異或和的陣列</param>
/// <param name="in_">儲存每個節點DFS開始時間的陣列</param>
/// <param name="out_">儲存每個節點DFS結束時間的陣列</param>
/// <param name="cnt">DFS遍歷的時間計數器</param>
private void Dfs(int x, int fa, int[] nums, List<List<int>> adj, int[] sum, int[] in_, int[] out_, ref int cnt)
```

**功能：** 透過 DFS 遍歷同時完成三個重要任務

**詳細執行步驟：**

#### 步驟 1：記錄開始時間戳
```csharp
// 記錄節點x開始被訪問的時間（DFS序的開始時間）
in_[x] = cnt++;
```
- 記錄節點 x 開始被訪問的時間
- 這個時間戳用於後續的祖先關係判斷

#### 步驟 2：初始化節點異或和
```csharp
// 初始化當前節點的異或和為節點本身的值
sum[x] = nums[x];
```
- 將當前節點的異或和初始化為節點本身的值

#### 步驟 3：遍歷所有子節點
```csharp
// 遍歷當前節點的所有鄰居節點
foreach (int y in adj[x])
{
    // 跳過父節點，避免回到已經訪問過的節點
    if (y == fa)
    {
        continue;
    }
    
    // 遞迴訪問子節點
    Dfs(y, x, nums, adj, sum, in_, out_, ref cnt);
    
    // 將子樹的異或和合併到當前節點
    // 這裡使用異或運算的性質：A ^ A = 0, A ^ 0 = A
    sum[x] ^= sum[y];
}
```
- 對每個鄰居節點進行遞迴訪問
- 跳過父節點避免回到已訪問的節點
- 利用異或運算的性質合併子樹的異或和

#### 步驟 4：記錄結束時間戳
```csharp
// 記錄節點x訪問結束的時間（DFS序的結束時間）
out_[x] = cnt;
```
- 記錄節點 x 訪問結束的時間
- 這個時間戳與開始時間戳一起用於祖先關係判斷

**DFS 序的祖先判斷原理：**
- 如果節點 x 是節點 y 的祖先，則必須滿足：`in_[x] < in_[y] < out_[x]`
- 這是因為祖先節點會先開始訪問，並且在所有子孫節點處理完畢後才結束訪問

### 3. Calc 函式 - 分數計算

```csharp
/// <summary>
/// 計算三個部分異或值的分數（最大值與最小值的差）
/// </summary>
/// <param name="part1">第一個部分的異或值</param>
/// <param name="part2">第二個部分的異或值</param>
/// <param name="part3">第三個部分的異或值</param>
/// <returns>三個異或值中最大值與最小值的差</returns>
private int Calc(int part1, int part2, int part3)
```

**功能：** 計算三個部分異或值的分數（最大值與最小值的差）

**實作細節：**
```csharp
// 計算三個異或值中的最大值和最小值，然後返回它們的差
return Math.Max(part1, Math.Max(part2, part3)) - Math.Min(part1, Math.Min(part2, part3));
```

**為什麼需要巢狀呼叫？**
- `Math.Max` 和 `Math.Min` 函式一次只能比較兩個值
- 需要巢狀呼叫來找出三個值中的最大值和最小值
- 先比較 `part2` 和 `part3`，再與 `part1` 比較

**運算步驟：**
1. `Math.Max(part2, part3)` - 找出後兩個值的最大值
2. `Math.Max(part1, ...)` - 與第一個值比較得到全域最大值
3. `Math.Min(part2, part3)` - 找出後兩個值的最小值
4. `Math.Min(part1, ...)` - 與第一個值比較得到全域最小值
5. 回傳最大值與最小值的差

## 異或運算的巧妙運用

### 基本性質
- `A ^ A = 0` - 任何值與自己異或得 0
- `A ^ 0 = A` - 任何值與 0 異或得自己
- 異或運算具有交換律和結合律

### 在演算法中的應用
1. **計算子樹異或和：** `sum[x] ^= sum[y]` 累積所有子樹的異或值
2. **計算剩餘部分：** `sum[0] ^ sum[u]` 從整體中「減去」某個子樹
3. **分割計算：** 利用異或的可逆性質快速計算分割後各部分的異或值

## DFS 序與祖先關係判斷

### 祖先關係的時間戳特徵
在 DFS 遍歷中，如果節點 A 是節點 B 的祖先，則：
- A 會比 B 更早開始被訪問：`in_[A] < in_[B]`
- A 會比 B 更晚結束訪問：`out_[A] > out_[B]`

這是因為 DFS 必須完全處理完 A 的所有子樹（包含 B）之後，才會結束對 A 的訪問。

### 視覺化範例
想像 DFS 遍歷像是一個「括號配對」的過程：

```
節點 0: (
  節點 1: (
    節點 3: ( )
  )
  節點 2: (
    節點 4: ( )
    節點 5: ( )
  )
)
```

如果節點 v 的括號完全包含在節點 u 的括號內部，那麼 u 就是 v 的祖先。

## 範例執行過程

### 測試範例 1
```
節點值: [1, 5, 5, 4, 11]
邊: [[0, 1], [1, 2], [1, 3], [3, 4]]
```

**樹結構：**
```
    0(1)
    |
    1(5)
   / \
  2(5) 3(4)
       |
       4(11)
```

**DFS 遍歷過程：**
1. 訪問節點 0: `in_[0] = 0`
2. 訪問節點 1: `in_[1] = 1`
3. 訪問節點 2: `in_[2] = 2`, `out_[2] = 3`
4. 訪問節點 3: `in_[3] = 4`
5. 訪問節點 4: `in_[4] = 5`, `out_[4] = 6`
6. 完成節點 3: `out_[3] = 7`
7. 完成節點 1: `out_[1] = 8`
8. 完成節點 0: `out_[0] = 9`

**子樹異或和計算：**
- `sum[4] = 11`
- `sum[3] = 4 ^ 11 = 15`
- `sum[2] = 5`
- `sum[1] = 5 ^ 5 ^ 15 = 15`
- `sum[0] = 1 ^ 15 = 14`

**祖先關係驗證：**
- 節點 0 是節點 1 的祖先：`in_[0] < in_[1] < out_[0]` → `0 < 1 < 9` ✓
- 節點 1 是節點 3 的祖先：`in_[1] < in_[3] < out_[1]` → `1 < 4 < 8` ✓
- 節點 3 是節點 4 的祖先：`in_[3] < in_[4] < out_[3]` → `4 < 5 < 7` ✓

## 效能分析

### 時間複雜度：O(n²)
- DFS 遍歷：O(n)
- 枚舉所有節點對：O(n²)
- 每次計算分數：O(1)
- 總時間複雜度：O(n²)

### 空間複雜度：O(n)
- 鄰接表：O(n)
- DFS 序陣列：O(n)
- 子樹異或和陣列：O(n)
- 總空間複雜度：O(n)

## 演算法優勢

1. **高效的祖先判斷：** 使用 DFS 序進行 O(1) 時間的祖先關係判斷
2. **異或運算優化：** 利用異或的數學性質快速計算分割結果
3. **完整的情況覆蓋：** 三個條件分支涵蓋所有可能的節點關係
4. **空間效率：** 只需要 O(n) 的額外空間

## 設計模式與最佳實務

### 函式設計原則
1. **單一職責原則：** 每個函式都有明確的單一責任
2. **參數明確性：** 使用描述性的參數名稱和 XML 文件註解
3. **錯誤處理：** 雖然此題假設輸入有效，但在實際應用中應加入邊界檢查

### 程式碼品質特點
1. **可讀性：** 清晰的變數命名和詳細的註解
2. **可維護性：** 模組化的函式設計，易於理解和修改
3. **效率：** 使用最適合的資料結構和演算法

## 總結

這個解法巧妙地結合了圖論中的 DFS 序概念和位元運算的數學性質，在合理的時間複雜度內解決了這個複雜的樹分割問題。關鍵創新點包括：

1. **DFS 序的創新應用：** 將傳統的祖先關係判斷轉化為簡單的數值比較
2. **異或運算的數學巧思：** 利用異或的可逆性質實現高效的集合分割計算
3. **完整的情況分析：** 系統性地考慮所有可能的節點關係並給出對應的處理方案

這種設計使得演算法不僅效能優秀，而且程式碼結構清晰，易於理解和維護。