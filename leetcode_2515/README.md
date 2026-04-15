# LeetCode 2515

## Shortest Distance to Target String in a Circular Array

以 C# 實作環狀陣列中到目標字串的最短距離問題，本專案收錄兩種解法：

- 解法一（公式掃描）：從左到右遍歷整個陣列，當找到 target 時，同時計算不跨越首尾的直接距離與跨越首尾的環狀距離，持續更新最小答案。
- 解法二（雙向擴散）：直接從 startIndex 出發，依照距離 0、1、2... 逐層向左右兩側擴散，第一次命中的 target 就是答案。

[題目說明](#題目說明) • [解法一](#解法一公式掃描) • [解法二](#解法二雙向擴散) • [複雜度](#複雜度) • [執行方式](#執行方式)

> [!NOTE]
> 解法一的核心觀察：從 startIndex 到索引 i 的最短步數，一定等於 $\min(|i - startIndex|,\ n - |i - startIndex|)$。
> 不需要真的去模擬往左走或往右走，只需一次線性掃描，就能找出所有 target 位置中的最短距離。

## 題目說明

給定一個 0-indexed 的環狀字串陣列 `words` 與一個字串 `target`。環狀陣列表示陣列的結尾會連接回陣列的開頭。

更正式地說，`words[i]` 的下一個元素是 `words[(i + 1) % n]`，前一個元素是 `words[(i - 1 + n) % n]`，其中 n 為 `words` 的長度。

從 `startIndex` 開始，你每一步都可以移動到下一個單字或前一個單字。

回傳到達字串 `target` 所需的最短距離。如果 `words` 中不存在 `target`，則回傳 -1。

在環狀陣列中，從一個位置走到另一個位置永遠有兩條路可以走：順時針（直接走）與逆時針（繞一圈走）。這題的本質，就是在這兩條路之間取最短。

## 解法一（公式掃描）

### 解題概念與出發點

這個解法從左到右遍歷整個陣列，當找到 target 時，同時計算不跨越首尾的直接距離與跨越首尾的環狀距離，並持續更新最小答案。

假設目前掃描到索引 i，且 `words[i]` 恰好等於 target。從 startIndex 到 i 會有兩條可行路徑：

1. **不跨越陣列首尾**，直接走到目標位置的距離：$|i - startIndex|$。
2. **環狀陣列可以反方向繞一圈**，跨越首尾的距離：$n - |i - startIndex|$。

核心觀察是：從 startIndex 到索引 i 的最短步數，一定等於：

$$\min(|i - startIndex|,\ n - |i - startIndex|)$$

因此只需要一次線性掃描，就能找出所有 target 位置中的最短距離。不需要額外資料結構，也不需要真的模擬往左或往右一步一步走，因為最短距離已經可以直接由公式得到。

> [!TIP]
> 看到環狀陣列的距離問題時，先嘗試把路徑拆成「直接走」與「繞一圈走」兩種距離，通常就能還原成單純的距離比較。

### 解法詳解

整體步驟如下：

1. 取得陣列長度 n，並用變數 `minDistance` 記錄目前最小答案，初始值設為 `int.MaxValue`。
2. 從索引 0 到 n - 1 依序遍歷 words 陣列。
3. 如果 `words[i]` 不是 target，直接跳過（`continue`）。
4. 如果 `words[i]` 等於 target，進行下列計算：
   - `directDistance = Math.Abs(i - startIndex)`：不跨越陣列首尾時，直接走到目標位置的距離。
   - `wrappedDistance = n - directDistance`：環狀陣列可以反方向繞一圈，因此也要比較跨越首尾的距離。
5. 取 `directDistance` 與 `wrappedDistance` 的較小值，再與 `minDistance` 比較後更新。
6. 遍歷結束後，若 `minDistance` 仍然等於 `int.MaxValue`，表示 target 不存在於陣列中，回傳 -1；否則回傳 `minDistance`。

### 流程示範

以下用程式碼中的第一組測資示範，該組測資在陣列中有兩個 target 出現，可以清楚看出公式如何在多個候選位置中挑出最短距離：

```text
words = ["hello", "i", "am", "leetcode", "hello"]
target = "hello"
startIndex = 1
n = 5
```

### 掃描過程

| 索引 i | words[i] | 是否命中 target | directDistance | wrappedDistance | 本次最短 | minDistance |
| --- | --- | --- | --- | --- | --- | --- |
| 0 | hello | 是 | \|0 - 1\| = 1 | 5 - 1 = 4 | 1 | 1 |
| 1 | i | 否 | - | - | - | 1 |
| 2 | am | 否 | - | - | - | 1 |
| 3 | leetcode | 否 | - | - | - | 1 |
| 4 | hello | 是 | \|4 - 1\| = 3 | 5 - 3 = 2 | 2 | 1 |

最後答案為 **1**。

索引 0 的 hello 直接距離只有 1 步（從索引 1 往左走到索引 0），而索引 4 的 hello 最近也要 2 步（繞環方向），因此最小答案維持在 1。

### startIndex 本身就是 target 的情況

當起點本身就命中 target 時：

```text
words = ["start", "middle", "target", "tail"]
target = "start"
startIndex = 0
```

掃描到索引 0 時，`directDistance = |0 - 0| = 0`，答案直接就是 0，不需要移動任何步數。

### 需要考慮環狀繞行的情況

下面這組測資可以看出環狀距離比直接距離更短的情境：

```text
words = ["a", "target", "c", "d", "e"]
target = "target"
startIndex = 4
n = 5
```

| 索引 i | words[i] | 是否命中 target | directDistance | wrappedDistance | 本次最短 | minDistance |
| --- | --- | --- | --- | --- | --- | --- |
| 0 | a | 否 | - | - | - | MaxValue |
| 1 | target | 是 | \|1 - 4\| = 3 | 5 - 3 = 2 | 2 | 2 |
| 2 | c | 否 | - | - | - | 2 |
| 3 | d | 否 | - | - | - | 2 |
| 4 | e | 否 | - | - | - | 2 |

最後答案為 **2**。

從索引 4 直接往左走到索引 1 需要 3 步，但反方向繞過首尾只需要 2 步（4 → 0 → 1），環狀距離比直接距離更短。

### 找不到 target 的情況

如果：

```text
words = ["x", "y", "z"]
target = "hello"
startIndex = 1
```

整個遍歷過程都不會命中 target，`minDistance` 會維持 `int.MaxValue`，最後回傳 -1。

## 解法二（雙向擴散）

### 解題概念與出發點

這個解法直接從 startIndex 出發，依照距離 0、1、2... 逐層向左右兩側擴散。對於每一個候選距離，都檢查 startIndex 往左與往右走相同步數後的位置是否命中 target。

因為我們是依照可能答案由小到大枚舉，所以第一次找到 target 的距離一定就是最短距離，不需要額外比較。

配合環狀陣列的特性，任何超出範圍的索引都可以用模運算轉回 $[0, n - 1]$ 的合法範圍：

- **左側索引**：`(startIndex - distance + n) % n`
- **右側索引**：`(startIndex + distance) % n`

左側公式中的 `+ n`，並不是為了讓寫法對稱，而是為了避免減法先算出負數。

例如當 startIndex = 0、distance = 1、n = 5 時，`startIndex - distance = -1`。在 C# 中直接計算 `-1 % 5` 結果仍然是 -1，無法當成合法的陣列索引。因此必須先加上 n，把它拉回非負範圍，再做 `% n`：

$(0 - 1 + 5) \% 5 = 4$

右側則不同，`startIndex + distance` 不會小於 0，只可能超過 n - 1，因此直接用 `% n` 就能把索引折回合法範圍。例如 $(4 + 1) \% 5 = 0$。

> [!IMPORTANT]
> **兩個公式的差別純粹是算術層面，與環繞方向無關。** 左側與右側在環狀陣列中都會繞過首尾——右移同樣會從尾端穿越回開頭，例如 `startIndex = 4, distance = 2, n = 5` 時，`rightIndex = (4 + 2) % 5 = 1`，索引已經繞過尾端回到索引 1 了，並非走到尾端就停止。
>
> 之所以只有左側公式需要 `+ n`，是因為減法 `startIndex - distance` 可能算出負數，而 C# 的 `%` 運算子對負數不會自動折回正數（`-1 % 5` 的結果仍然是 `-1`），所以必須先加上 n 確保結果為非負，再做 `% n`。右側用的是加法，兩個非負整數相加結果永遠 $\geq 0$，`% n` 本身就能處理超過上界的情況，不需要額外 `+ n`。

由於環狀陣列兩點間的最短距離不會超過 $\lfloor n / 2 \rfloor$，所以最多只需要枚舉到這個距離就能涵蓋所有可能。

> [!TIP]
> 解法二的思路很貼近題意：既然每一步只能往左或往右走，那就直接照步數一圈一圈往外擴散。由於距離是從小到大枚舉，第一次命中就是最短距離。

### 解法詳解

整體步驟如下：

1. 取得陣列長度 n，並計算最大搜尋距離 `maxDistance = n / 2`。
2. 由 distance = 0 開始，逐步遞增到 maxDistance：
   - 計算左移索引 `leftIndex = (startIndex - distance + n) % n`：左移可能先算出負值，因此先加上 n，再用 `% n` 折回合法索引。
   - 計算右移索引 `rightIndex = (startIndex + distance) % n`：右移只會超過上界，不會變成負值，直接 `% n` 即可折回合法索引。
3. 如果 `words[leftIndex]` 或 `words[rightIndex]` 等於 target，由於距離是從小到大枚舉，第一次命中就是最短距離，立刻回傳 distance。
4. 如果所有 distance 都檢查完仍然沒有命中，表示 target 不存在於陣列中，回傳 -1。

這個方法同樣不需要額外資料結構，空間複雜度維持 O(1)。

### 流程示範

以下用一組能清楚看出環狀索引折回效果的測資示範。startIndex 位於陣列尾端，右移時索引會穿越首尾：

```text
words = ["a", "target", "c", "d", "e"]
target = "target"
startIndex = 4
n = 5
maxDistance = 2
```

### 擴散過程

| 距離 distance | leftIndex | words[leftIndex] | rightIndex | words[rightIndex] | 是否命中 target |
| --- | --- | --- | --- | --- | --- |
| 0 | (4 - 0 + 5) % 5 = 4 | e | (4 + 0) % 5 = 4 | e | 否 |
| 1 | (4 - 1 + 5) % 5 = 3 | d | (4 + 1) % 5 = 0 | a | 否 |
| 2 | (4 - 2 + 5) % 5 = 2 | c | (4 + 2) % 5 = 1 | target | 是，回傳 2 |

最後答案為 **2**。

這個範例展示了解法二的兩個重點：

- **右移索引穿越首尾**：distance = 1 時，`(4 + 1) % 5 = 0`，索引從陣列尾端折回到開頭；distance = 2 時，`(4 + 2) % 5 = 1`，繼續往前推進到索引 1，命中 target。
- **在 distance = 2 命中後立即回傳**：不需要檢查剩餘位置，因為由小到大枚舉保證了第一次命中的距離就是最短。

### 起點本身就是 target 的情況

```text
words = ["start", "middle", "target", "tail"]
target = "start"
startIndex = 0
```

distance = 0 時，leftIndex 與 rightIndex 都指向索引 0，`words[0] == "start"` 直接命中，立刻回傳 0。

### 找不到 target 的情況

如果：

```text
words = ["x", "y", "z"]
target = "hello"
startIndex = 1
n = 3
maxDistance = 1
```

| 距離 distance | leftIndex | words[leftIndex] | rightIndex | words[rightIndex] | 是否命中 target |
| --- | --- | --- | --- | --- | --- |
| 0 | (1 - 0 + 3) % 3 = 1 | y | (1 + 0) % 3 = 1 | y | 否 |
| 1 | (1 - 1 + 3) % 3 = 0 | x | (1 + 1) % 3 = 2 | z | 否 |

所有可能距離都檢查完仍然沒有命中，回傳 -1。

## 複雜度

- 解法一（公式掃描）：時間複雜度 O(n)，空間複雜度 O(1)
- 解法二（雙向擴散）：時間複雜度 O(n)，空間複雜度 O(1)

其中 n 是 words 的長度。

兩種解法的時間上界相同，但解法二在 target 出現在 startIndex 附近時，能更早結束迴圈。

## 專案內容

| 路徑 | 說明 |
| --- | --- |
| leetcode_2515/Program.cs | 題目兩種解法（`ClosestTarget`、`ClosestTargetByExpandingFromStartIndex`）與 Main 內建測試資料 |
| leetcode_2515/leetcode_2515.csproj | .NET 10 主控台專案設定 |

## 執行方式

在專案根目錄執行：

```bash
dotnet build leetcode_2515/leetcode_2515.csproj
dotnet run --project leetcode_2515/leetcode_2515.csproj
```

程式會輸出 Main 中內建的五組測試資料，方便快速確認兩種解法的結果是否一致：

- Case 1：陣列中有多個 target，驗證能挑出最短距離
- Case 2：target 位於環狀繞行才能更快到達的位置
- Case 3：startIndex 本身就是 target，距離為 0
- Case 4：需要穿越首尾才能取得最短距離
- Case 5：target 不存在於陣列中，回傳 -1
