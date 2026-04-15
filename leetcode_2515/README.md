# LeetCode 2515

## Shortest Distance to Target String in a Circular Array

以 C# 實作環狀陣列最短距離問題，本專案整理兩種常見解法：

- 解法一：一次線性遍歷，對每個符合 target 的位置同時計算「直接走」與「繞一圈走」兩種距離，最後取全域最小值。
- 解法二：從 startIndex 開始，依照距離由小到大往左右兩側擴散，第一個命中的 target 就是答案。

[題目說明](#題目說明) • [解法一](#解法一) • [解法二](#解法二) • [複雜度](#複雜度) • [執行方式](#執行方式)

> [!NOTE]
> 解法一的關鍵不是真的去模擬往左走或往右走，而是先把兩種可能距離寫清楚：
>
> - 直接距離 = |i - startIndex|
> - 繞環距離 = n - |i - startIndex|
> - 兩者取最小值後，再和目前答案比較

## 題目說明

給定一個 0-indexed 的環狀字串陣列 words、一個目標字串 target，以及起點 startIndex。

- 你每一步可以往左或往右移動一格。
- 陣列是環狀的，代表尾端與開頭相連。
- 目標是找出從 startIndex 出發，到任一個 target 所需的最短步數。
- 如果陣列中根本沒有 target，答案就是 -1。

這題的本質，是在環狀陣列中比較兩點之間的最短距離。

## 解法一

### 解題概念與出發點

假設目前掃描到索引 i，且 words[i] 恰好等於 target。

從 startIndex 到 i 會有兩條可行路徑：

1. 不跨越首尾，直接走過去，距離是 |i - startIndex|。
2. 反方向繞過陣列首尾，距離是 n - |i - startIndex|。

因此，對每一個符合條件的索引 i，我們只要計算：

min(|i - startIndex|, n - |i - startIndex|)

接著把所有符合 target 的位置都掃過一次，持續更新最小值即可。

> [!TIP]
> 看到環狀陣列時，先把問題拆成「直接走」與「繞一圈走」兩種距離，通常就能把題目還原成一般的距離比較問題。

### 解法詳解

整體步驟如下：

1. 先用變數 minDistance 記錄目前最小答案，初始值設為 int.MaxValue。
2. 從左到右遍歷整個 words 陣列。
3. 如果目前 words[i] 不是 target，就直接略過。
4. 如果 words[i] 等於 target，就計算：
   - directDistance = |i - startIndex|
   - wrappedDistance = n - directDistance
5. 用兩者較小值更新 minDistance。
6. 遍歷結束後，如果 minDistance 仍然沒有被更新，表示 target 不存在，回傳 -1；否則回傳 minDistance。

這個方法不需要額外資料結構，也不需要真的模擬往左或往右一步一步走，因為最短距離已經可以直接由公式得到。

### 流程示範

以下用一組代表性測資示範：

```text
words = ["hello", "i", "am", "leetcode", "hello"]
target = "hello"
startIndex = 1
n = 5
```

### 掃描過程

| 索引 i | words[i] | 是否命中 target | 直接距離 | 繞環距離 | 本次最短 | 目前答案 |
| --- | --- | --- | --- | --- | --- | --- |
| 0 | hello | 是 | 1 | 4 | 1 | 1 |
| 1 | i | 否 | - | - | - | 1 |
| 2 | am | 否 | - | - | - | 1 |
| 3 | leetcode | 否 | - | - | - | 1 |
| 4 | hello | 是 | 3 | 2 | 2 | 1 |

最後答案為 1。

原因是從索引 1 往左走一步就能到索引 0 的 hello，不需要繞更遠的路。

### 找不到 target 的情況

如果：

```text
words = ["x", "y", "z"]
target = "hello"
startIndex = 1
```

整個遍歷過程都不會命中 target，minDistance 會維持 int.MaxValue，最後回傳 -1。

## 解法二

### 解題概念與出發點

這個方法不先掃描所有 target 位置，而是反過來思考：「如果最短答案是 k，那麼從 startIndex 往左 k 步或往右 k 步，至少有一邊會命中 target。」

因此我們可以直接枚舉答案 distance = 0, 1, 2, ..., ⌊n / 2⌋：

1. 先檢查 startIndex 本身，也就是 distance = 0。
2. 接著檢查距離 1 的左右兩側位置。
3. 再檢查距離 2、距離 3，依此類推。
4. 一旦命中 target，當前 distance 就是最短距離，可以立刻回傳。

因為環狀陣列允許從頭尾穿越，所以：

- 左側索引可以寫成 (startIndex - distance + n) % n
- 右側索引可以寫成 (startIndex + distance) % n

左側公式中的 + n，重點不是讓寫法看起來對稱，而是避免減法先算出負數。

例如當 startIndex = 0、distance = 1、n = 3 時，startIndex - distance = -1。若在 C# 直接計算 -1 % 3，結果仍然是 -1，不能當成合法陣列索引，所以必須先加上 n，把它拉回非負範圍，再做 % n：

(0 - 1 + 3) % 3 = 2

右側則不同，startIndex + distance 不會小於 0，只可能超過 n - 1，因此直接用 % n 就能把索引折回合法範圍。例如 (2 + 1) % 3 = 0。技術上右側寫成 (startIndex + distance + n) % n 也正確，但只是多做一步，通常沒有必要。

> [!TIP]
> 解法二的優點是思路很貼近題意：既然每一步只能往左或往右走，那就直接照步數一圈一圈往外擴散。第一次找到 target 時，不需要再做額外比較。

### 解法詳解

整體步驟如下：

1. 令 n 為 words 的長度。
2. 由於環狀陣列兩點間的最短距離不會超過 ⌊n / 2⌋，所以最多只需要枚舉到這個距離。
3. 對每個 distance：
   - 計算 leftIndex = (startIndex - distance + n) % n
   - 計算 rightIndex = (startIndex + distance) % n
4. 如果 words[leftIndex] 或 words[rightIndex] 等於 target，就立刻回傳 distance。
5. 如果所有 distance 都檢查完仍然沒有命中，表示 target 不存在，回傳 -1。

這個方法同樣不需要額外資料結構，空間複雜度維持 O(1)。

### 流程示範

以下用一組能清楚看出環狀索引效果的測資示範：

```text
words = ["a", "b", "leetcode"]
target = "leetcode"
startIndex = 0
n = 3
```

### 擴散過程

| 距離 distance | leftIndex | words[leftIndex] | rightIndex | words[rightIndex] | 是否命中 target |
| --- | --- | --- | --- | --- | --- |
| 0 | (0 - 0 + 3) % 3 = 0 | a | (0 + 0) % 3 = 0 | a | 否 |
| 1 | (0 - 1 + 3) % 3 = 2 | leetcode | (0 + 1) % 3 = 1 | b | 是，回傳 1 |

最後答案為 1。

這個例子剛好展示了解法二的核心重點：

- 往左走 1 步時，會先得到索引 -1；因為 C# 的負數 % n 仍可能是負數，所以要先加上 n，再用 % n 轉成索引 2。
- 因為在距離 1 就已經找到 target，所以不需要再繼續檢查更遠的位置。

### 找不到 target 的情況

如果：

```text
words = ["x", "y", "z"]
target = "hello"
startIndex = 1
```

distance = 0 會檢查索引 1，distance = 1 會檢查索引 0 與 2。所有可能最短距離都檢查完後仍然沒有命中，因此最後回傳 -1。

## 複雜度

- 解法一：時間複雜度 O(n)，空間複雜度 O(1)
- 解法二：時間複雜度 O(n)，空間複雜度 O(1)

其中 n 是 words 的長度。

## 專案內容

| 路徑 | 說明 |
| --- | --- |
| leetcode_2515/Program.cs | 題目兩種解法與 Main 內建測試資料 |
| leetcode_2515/leetcode_2515.csproj | .NET 10 主控台專案設定 |

## 執行方式

在專案根目錄執行：

```bash
dotnet build leetcode_2515/leetcode_2515.csproj
dotnet run --project leetcode_2515/leetcode_2515.csproj
```

程式會輸出 Main 中內建的測試資料，方便快速確認兩種解法的結果是否一致：

- 一般情況
- 需要考慮環狀繞行的情況
- startIndex 本身就是 target 的情況
- target 不存在的情況
