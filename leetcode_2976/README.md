# LeetCode 2976 - Minimum Cost to Convert String I

> 將字串轉換成目標字串的最小成本計算

## 題目說明

給定兩個長度相同的字串 `source` 和 `target`，以及字元替換規則（`original`、`changed`、`cost` 陣列），求將 `source` 轉換成 `target` 所需的最小總成本。若無法完成轉換則回傳 `-1`。

### 題目連結

- [LeetCode English](https://leetcode.com/problems/minimum-cost-to-convert-string-i/description/)
- [LeetCode 中文](https://leetcode.cn/problems/minimum-cost-to-convert-string-i/description/)

### 輸入說明

- `source`: 起始字串（僅包含小寫英文字母）
- `target`: 目標字串（僅包含小寫英文字母，長度與 source 相同）
- `original`: 可替換的原始字元陣列
- `changed`: 對應的目標字元陣列
- `cost`: 每次替換的成本陣列

其中 `cost[i]` 表示將 `original[i]` 轉換成 `changed[i]` 的成本。

### 輸出說明

回傳將 `source` 轉換成 `target` 的最小總成本，若無法完成轉換則回傳 `-1`。

### 範例

**範例 1:**
```
輸入: 
source = "abcd"
target = "acbe"
original = ['a','b','c','c','e','d']
changed = ['b','c','b','e','b','e']
cost = [2,5,5,1,2,20]

輸出: 28

說明:
- 'a' → 'a': 成本 0（不需替換）
- 'b' → 'c': 成本 5
- 'c' → 'b': 成本 5
- 'd' → 'e': 成本 20
總成本 = 0 + 5 + 5 + 20 = 30

但實際上可以透過間接路徑降低成本：
- 'a' → 'a': 成本 0
- 'b' → 'a' → 'c': 成本 2 + 5 = 7（比直接 b→c 的成本 5 還高，所以選直接路徑）
- 'b' → 'c': 成本 5
- 'c' → 'e' → 'b': 成本 1 + 2 = 3（比直接 c→b 的成本 5 便宜）
- 'd' → 'e': 成本 20
最小總成本 = 0 + 5 + 3 + 20 = 28
```

**範例 2:**
```
輸入:
source = "aaaa"
target = "bbbb"
original = ['a','c']
changed = ['c','b']
cost = [1,2]

輸出: 12

說明:
每個 'a' 都需要轉換成 'b'
路徑: a → c → b，成本 = 1 + 2 = 3
總成本 = 3 × 4 = 12
```

**範例 3:**
```
輸入:
source = "abcd"
target = "abcd"
original = ['a']
changed = ['e']
cost = [10000]

輸出: 0

說明:
source 與 target 已經相同，不需任何轉換
```

## 解題概念

### 問題本質

這是一個**最短路徑**問題。我們可以將字元替換視為圖論中的邊：
- 每個字母是一個**節點**
- 從 `original[i]` 到 `changed[i]` 有一條權重為 `cost[i]` 的**有向邊**
- 問題轉化為：找出每個字母到另一字母的**最小成本路徑**

### 為什麼選擇 Floyd-Warshall？

雖然可以用 Dijkstra 對每個起始字母跑一次最短路徑，但 Floyd-Warshall 更適合這個問題：

1. **節點數固定且小**：只有 26 個英文字母，Floyd-Warshall 的 O(V³) 複雜度完全可接受
2. **需要所有點對的最短路徑**：一次計算就能得到所有字母對之間的最小轉換成本
3. **程式碼簡潔**：三層迴圈即可完成，無需複雜的資料結構
4. **處理多條相同路徑**：自動選擇最小成本

### 核心思路

1. **建立距離矩陣** `dis[i][j]`：表示字母 i 轉換成字母 j 的最小成本
2. **初始化**：所有距離設為無限大（表示尚未找到路徑）
3. **填入直接轉換**：根據題目給的 original、changed、cost 建立初始邊
4. **Floyd-Warshall 更新**：透過中繼點找出更短的路徑
5. **計算答案**：將 source 每個位置轉換成 target 的成本累加

## 演算法詳解

### Floyd-Warshall 演算法

Floyd-Warshall 是一個**動態規劃**演算法，用於找出圖中所有點對之間的最短路徑。

**核心概念**：
```
對於每對節點 (i, j)，嘗試透過中繼點 k 來縮短距離

如果 dis[i][j] > dis[i][k] + dis[k][j]
則更新 dis[i][j] = dis[i][k] + dis[k][j]
```

**演算法步驟**：

```csharp
// 1. 初始化距離矩陣
for(int i = 0; i < 26; i++)
    for(int j = 0; j < 26; j++)
        dis[i][j] = ∞

// 2. 填入直接邊的權重
for(每條邊 (x, y, cost))
    dis[x][y] = min(dis[x][y], cost)

// 3. Floyd-Warshall 核心：逐個嘗試中繼點
for(int k = 0; k < 26; k++)          // k: 中繼點
{
    dis[k][k] = 0                     // 自己到自己距離為 0
    for(int i = 0; i < 26; i++)       // i: 起點
        for(int j = 0; j < 26; j++)   // j: 終點
            dis[i][j] = min(dis[i][j], dis[i][k] + dis[k][j])
}
```

### 為何使用 `int.MaxValue / 2`？

在初始化時使用 `int.MaxValue / 2` 而非 `int.MaxValue`，是為了避免算術溢位：

```csharp
// 若使用 int.MaxValue
int.MaxValue + int.MaxValue  // 溢位！變成負數

// 使用 int.MaxValue / 2
(int.MaxValue / 2) + (int.MaxValue / 2)  // 仍在 int 範圍內
```

這樣在比較 `dis[i][k] + dis[k][j]` 時不會因為溢位導致錯誤結果。

## 實作細節

### 完整流程

```csharp
public long MinimumCost(string source, string target, 
                        char[] original, char[] changed, int[] cost)
{
    // ===== 步驟 1: 建立並初始化距離矩陣 =====
    int[][] dis = new int[26][];
    for(int i = 0; i < 26; i++)
    {
        dis[i] = new int[26];
        Array.Fill(dis[i], int.MaxValue / 2);  // 初始化為無限大
    }

    // ===== 步驟 2: 填入直接轉換成本 =====
    for(int i = 0; i < cost.Length; i++)
    {
        int x = original[i] - 'a';  // 字元轉索引 (0-25)
        int y = changed[i] - 'a';
        dis[x][y] = Math.Min(dis[x][y], cost[i]);  // 多條相同邊取最小
    }

    // ===== 步驟 3: Floyd-Warshall 計算最短路徑 =====
    for(int k = 0; k < 26; k++)  // 中繼點
    {
        dis[k][k] = 0;  // 自己到自己成本為 0
        for(int i = 0; i < 26; i++)  // 起點
        {
            for(int j = 0; j < 26; j++)  // 終點
            {
                // 嘗試透過 k 更新 i→j 的最短路徑
                dis[i][j] = Math.Min(dis[i][j], dis[i][k] + dis[k][j]);
            }
        }
    }

    // ===== 步驟 4: 計算 source → target 的總成本 =====
    long result = 0;
    for(int i = 0; i < source.Length; i++)
    {
        int d = dis[source[i] - 'a'][target[i] - 'a'];
        
        if(d >= int.MaxValue / 2)  // 無法轉換
            return -1;
        
        result += d;
    }
    
    return result;
}
```

## 範例演示

讓我們用**範例 1**來詳細演示整個流程。

### 輸入資料

```
source = "abcd"
target = "acbe"
original = ['a', 'b', 'c', 'c', 'e', 'd']
changed  = ['b', 'c', 'b', 'e', 'b', 'e']
cost     = [ 2,   5,   5,   1,   2,  20 ]
```

### 步驟 1: 建立初始距離矩陣

初始化後（僅顯示相關字母）：

```
     a    b    c    d    e
a [  ∞    ∞    ∞    ∞    ∞  ]
b [  ∞    ∞    ∞    ∞    ∞  ]
c [  ∞    ∞    ∞    ∞    ∞  ]
d [  ∞    ∞    ∞    ∞    ∞  ]
e [  ∞    ∞    ∞    ∞    ∞  ]
```

### 步驟 2: 填入直接轉換成本

根據 original、changed、cost：
- a → b: 2
- b → c: 5
- c → b: 5
- c → e: 1
- e → b: 2
- d → e: 20

```
     a    b    c    d    e
a [  ∞    2    ∞    ∞    ∞  ]
b [  ∞    ∞    5    ∞    ∞  ]
c [  ∞    5    ∞    ∞    1  ]
d [  ∞    ∞    ∞    ∞   20  ]
e [  ∞    2    ∞    ∞    ∞  ]
```

### 步驟 3: Floyd-Warshall 更新

**k = 0 (字母 'a')**
- 設定 dis[0][0] = 0（a → a: 0）
- 檢查是否有路徑可透過 'a' 縮短
- 結果：無明顯更新

**k = 1 (字母 'b')**
- 設定 dis[1][1] = 0（b → b: 0）
- 檢查透過 'b' 的路徑：
  - a → b → c: 2 + 5 = 7 (新增！)
  - c → b → c: 5 + 5 = 10 (比原本 c→e→b→c 還差，不更新)

```
     a    b    c    d    e
a [  0    2    7    ∞    ∞  ]
b [  ∞    0    5    ∞    ∞  ]
c [  ∞    5    0    ∞    1  ]
d [  ∞    ∞    ∞    0   20  ]
e [  ∞    2    ∞    ∞    0  ]
```

**k = 2 (字母 'c')**
- 設定 dis[2][2] = 0
- 檢查透過 'c' 的路徑：
  - a → c → e: 7 + 1 = 8 (新增！)
  - a → c → b: 7 + 5 = 12 (比原本 a→b 的 2 還差)
  - b → c → e: 5 + 1 = 6 (新增！)
  - e → c → b: ∞ + 5 = ∞ (無法更新)

```
     a    b    c    d    e
a [  0    2    7    ∞    8  ]
b [  ∞    0    5    ∞    6  ]
c [  ∞    5    0    ∞    1  ]
d [  ∞    ∞    ∞    0   20  ]
e [  ∞    2    ∞    ∞    0  ]
```

**k = 3 (字母 'd')**
- 無新路徑（d 只有出邊沒有入邊）

**k = 4 (字母 'e')**
- 設定 dis[4][4] = 0
- 檢查透過 'e' 的路徑：
  - a → e → b: 8 + 2 = 10 (比原本 a→b 的 2 還差)
  - b → e → b: 6 + 2 = 8 (比原本 b→b 的 0 還差)
  - c → e → b: 1 + 2 = 3 (更新！原本是 5)
  - d → e → b: 20 + 2 = 22 (新增！)

**最終距離矩陣**：
```
     a    b    c    d    e
a [  0    2    7    ∞    8  ]
b [  ∞    0    5    ∞    6  ]
c [  ∞    3    0    ∞    1  ]
d [  ∞   22    ∞    0   20  ]
e [  ∞    2    ∞    ∞    0  ]
```

### 步驟 4: 計算 source → target 的總成本

```
source = "abcd"
target = "acbe"

位置 0: 'a' → 'a' = dis[0][0] = 0
位置 1: 'b' → 'c' = dis[1][2] = 5
位置 2: 'c' → 'b' = dis[2][1] = 3  ← 透過 Floyd-Warshall 找到的更短路徑！
位置 3: 'd' → 'e' = dis[3][4] = 20

總成本 = 0 + 5 + 3 + 20 = 28
```

### 關鍵發現

原本 `c → b` 的直接成本是 5，但透過 Floyd-Warshall 發現：
```
c → e → b = 1 + 2 = 3
```
這條間接路徑成本更低，因此最終答案是 28 而非 30。

## 複雜度分析

### 時間複雜度

- **初始化矩陣**: O(26²) = O(676)
- **填入直接邊**: O(m)，m 是 cost 陣列長度
- **Floyd-Warshall**: O(26³) = O(17,576)
- **計算答案**: O(n)，n 是字串長度

**總時間複雜度**: O(26³ + m + n) ≈ **O(n + m)**（因為 26³ 是常數）

### 空間複雜度

- **距離矩陣**: O(26²) = O(676)

**總空間複雜度**: **O(1)**（常數空間）

## 測試案例

專案中包含三個測試案例：

```csharp
// 測試 1: 基本範例 - 需要找間接路徑
source = "abcd", target = "acbe"
預期輸出: 28

// 測試 2: 需要間接轉換
source = "aaaa", target = "bbbb"
預期輸出: 12

// 測試 3: 已經相同
source = "abcd", target = "abcd"
預期輸出: 0
```

## 執行方式

```bash
# 編譯並執行
dotnet run

# 或使用 Visual Studio / VS Code 的執行功能
```

## 延伸思考

### 1. 若字母不限於小寫英文怎麼辦？

可以使用 `Dictionary<char, int>` 來動態對應字元與索引，或直接使用字元作為鍵值建立圖。

### 2. 為何不用 Dijkstra？

Dijkstra 需要對每個可能的起始字母執行一次（最多 26 次），總複雜度為 O(26 × 26 log 26)，雖然漸進複雜度相近，但實作較繁瑣，且常數因子不一定更優。

### 3. 能否用 BFS？

BFS 適用於邊權重相同的情況，本題邊權重不同，需要使用 Dijkstra 或 Floyd-Warshall。

### 4. 若要求輸出轉換路徑？

需要在 Floyd-Warshall 過程中額外記錄路徑資訊，可透過 `next[i][j]` 陣列記錄 i→j 路徑上的下一個節點。

## 相關題目

- [LeetCode 2977 - Minimum Cost to Convert String II](https://leetcode.com/problems/minimum-cost-to-convert-string-ii/) - 進階版，替換的是字串而非單一字元
- [LeetCode 787 - Cheapest Flights Within K Stops](https://leetcode.com/problems/cheapest-flights-within-k-stops/) - 帶限制的最短路徑
- [LeetCode 1334 - Find the City With the Smallest Number of Neighbors](https://leetcode.com/problems/find-the-city-with-the-smallest-number-of-neighbors-at-a-threshold-distance/) - Floyd-Warshall 應用

---

**演算法標籤**: 圖論 · 最短路徑 · Floyd-Warshall · 動態規劃  
**難度**: Medium  
**出處**: LeetCode Daily Question (2026-01-29)
